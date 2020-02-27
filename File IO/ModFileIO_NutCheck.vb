Partial Module ModFileIO

    Private Sub NutCheckImport(iPart() As String)
        Select Case LCase(iPart(0))
            Case "test time"
                FileIoDateTime = CDate(iPart(1))
            Case "input ips"
                    ' IGNORE
            Case "input ports"
                    ' IGNORE
            Case "timeout"
                    ' IGNORE
            Case "tests"
                ' IGNORE
            Case Else
                Dim aHost As ClsHost = AllHosts.Find(Function(p) p.IP = iPart(0))
                If aHost Is Nothing Then
                    aHost = New ClsHost
                    AllHosts.Add(aHost)
                    aHost.IP = iPart(0)
                    aHost.IP_Date = FileIoDateTime
                End If
                Dim tcpMode As Boolean = False
                Dim udpMode As Boolean = False
                Dim commentMode As Boolean = False
                Dim intA As Integer = 1
                Do Until intA >= iPart.Count
                    Select Case iPart(intA)
                        Case "+ping", "ping hit"
                            aHost.Ping.Add(True, FileIoDateTime)
                        Case "-ping", "ping miss"
                            aHost.Ping.Add(False, FileIoDateTime)
                        Case "tcp"
                            tcpMode = True
                            udpMode = False
                            commentMode = False
                        Case "udp"
                            tcpMode = False
                            udpMode = True
                            commentMode = False
                        Case "comment"
                            tcpMode = False
                            udpMode = False
                            commentMode = True
                        Case Else
                            If tcpMode Then
                                Select Case Left(iPart(intA), 1)
                                    Case "+"
                                        Dim aPort As Integer = CInt(Mid(iPart(intA), 2))
                                        aHost.Tcp.Add(True, aPort, FileIoDateTime)
                                    Case "-"
                                        Dim aPort As Integer = CInt(Mid(iPart(intA), 2))
                                        aHost.Tcp.Add(False, aPort, FileIoDateTime)
                                    Case Else
                                        If LCase(iPart(intA)) = "comment" Then
                                            commentMode = True
                                            tcpMode = False
                                        End If
                                        ' DO NOTHING
                                End Select
                            ElseIf commentMode Then
                                aHost.Comments.Add(iPart(intA))
                            End If
                    End Select
                    intA += 1
                Loop
                If aHost.IsEmpty Then AllHosts.Remove(aHost)
        End Select
    End Sub

End Module
