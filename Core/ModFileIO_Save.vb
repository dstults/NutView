Module ModFileIO_Save

    Private CurrentProgress As Integer
    Private MyOutputFile As String
    Private OutputFull As Boolean = True

    Public Sub SaveNutView(FilePath As String, full As Boolean)

        MyOutputFile = FilePath
        OutputFull = full

        CurrentProgress = 0

        Dim MyProgress As New FormProgress(FileTask.Save)
        MyProgress.Show()

        ' Set Header and Write Out file
        IO.File.WriteAllText(MyOutputFile, "NUTVIEW" & vbNewLine)
        AppendHost(CurrentProgress, OutputFull)

    End Sub

    Public Function ContinueSaving() As Integer

        CurrentProgress += 1
        AppendHost(CurrentProgress, True)

        Dim result As Integer = CInt(98 * CurrentProgress / AllHosts.Count)
        If CurrentProgress = AllHosts.Count - 1 Then result = 100
        Return result

    End Function

    Public Sub AppendHost(iHostNum As Integer, full As Boolean)
        Dim cDel As String = "," & vbTab ' Custom Delimeter
        Dim iHost As ClsHost = AllHosts(iHostNum)

        Dim MachineReport As String = "HOST"
        MachineReport &= cDel & "CUSTOM NAME" & cDel & iHost.CustomName
        MachineReport &= cDel & "IP" & cDel & iHost.IP
        MachineReport &= cDel & "HOSTNAME" & cDel & iHost.HostName
        MachineReport &= cDel & "MAC" & cDel & iHost.MacAddress
        MachineReport &= cDel & "MANUFACTURER" & cDel & iHost.Manufacturer

        If full Then ' FULL MODE ================================================

            If iHost.Ping.Hits.Count > 0 Then
                For intA As Integer = 0 To iHost.Ping.Hits.Count - 1
                    Select Case iHost.Ping.Hits(intA)
                        Case True
                            MachineReport &= cDel & "+PING"
                        Case False
                            MachineReport &= cDel & "-PING"
                    End Select
                    MachineReport &= cDel & iHost.Ping.Times(intA)
                Next
            End If
            If iHost.Tcp.Hits.Count > 0 Then
                For intA As Integer = 0 To iHost.Tcp.Hits.Count - 1
                    Select Case iHost.Tcp.Hits(intA)
                        Case True
                            MachineReport &= cDel & "+TCP"
                        Case False
                            MachineReport &= cDel & "-TCP"
                    End Select
                    MachineReport &= cDel & iHost.Tcp.Ports(intA)
                    MachineReport &= cDel & iHost.Tcp.Times(intA)
                Next
            End If

        Else ' SHORT MODE ================================================

            MachineReport &= cDel & "PING:" & iHost.Ping.Value
            For Each iPort As Integer In iHost.Tcp.OpenPorts
                MachineReport &= cDel & "TCP:" & iPort & ":" & iHost.Tcp.Value(iPort)
            Next
            If iHost.Tcp.Hits.Count > 0 Then
                For intA As Integer = 0 To iHost.Tcp.Hits.Count - 1
                    Select Case iHost.Tcp.Hits(intA)
                        Case True
                            MachineReport &= cDel & "+TCP"
                        Case False
                            MachineReport &= cDel & "-TCP"
                    End Select
                    MachineReport &= cDel & iHost.Tcp.Ports(intA)
                    MachineReport &= cDel & iHost.Tcp.Times(intA)
                Next
            End If

        End If ' ================================================
        If iHost.Comments.Count > 0 Then
            For intA As Integer = 0 To iHost.Comments.Count - 1
                MachineReport &= cDel & "COMMENT"
                MachineReport &= cDel & iHost.Comments(intA)
            Next
        End If
        MachineReport &= vbNewLine
        IO.File.AppendAllText(MyOutputFile, MachineReport)
    End Sub

End Module
