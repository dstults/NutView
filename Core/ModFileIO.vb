Module ModFileIO

    Public Sub ParseFile(FilePath As String)
        Dim FileInput() As String = IO.File.ReadAllLines(FilePath)
        Dim InputTime As DateTime = DateTime.Now
        For Each iLine As String In FileInput
            Dim iPart() As String = Split(iLine, ",")
            Select Case LCase(iPart(0))
                Case "test time"
                    InputTime = CDate(iPart(1))
                Case "input ips"
                    ' IGNORE - ALREADY INCLUDED
                Case "input ports"
                    ' IGNORE - ALREADY INCLUDED
                Case "timeout"
                    ' IGNORE FOR NOW
                Case "tests"
                    ' IGNORE - ALREADY INCLUDED
                Case Else
                    Dim aHost As ClsHost = AllHosts.Find(Function(p) p.IP = iPart(0))
                    'If AllHosts.Count > 0 Then aHost
                    If aHost Is Nothing Then
                        aHost = New ClsHost
                        AllHosts.Add(aHost)
                    End If
                    Dim tcpMode As Boolean = False
                    Dim udpMode As Boolean = False
                    Dim intA As Integer = 1
                    Do Until intA >= iPart.Count
                        Select Case iPart(intA)
                            Case "+ping", "ping hit"
                                aHost.Ping.Add(True, InputTime)
                            Case "-ping", "ping miss"
                                aHost.Ping.Add(False, InputTime)
                            Case "tcp"
                                tcpMode = True
                                udpMode = False
                            Case "udp"
                                tcpMode = False
                                udpMode = True
                            Case Else
                                If tcpMode Then
                                    Select Case Left(iPart(intA), 1)
                                        Case "+"
                                            Dim aPort As Integer = CInt(Mid(iPart(intA), 2))
                                            aHost.Tcp.Add(True, aPort, InputTime)
                                        Case "-"
                                            Dim aPort As Integer = CInt(Mid(iPart(intA), 2))
                                            aHost.Tcp.Add(False, aPort, InputTime)
                                        Case Else
                                            ' DO NOTHING
                                    End Select
                                End If
                        End Select
                        aHost.GetEmptiness()
                        intA += 1
                    Loop
            End Select
        Next
        MsgBox("File Import Complete", vbOKOnly)
    End Sub

End Module
