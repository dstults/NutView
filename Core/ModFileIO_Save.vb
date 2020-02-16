Module ModFileIO_Save

    Private CurrentProgress As Integer
    Private MyOutputFile As String

    Public Sub SaveNutView(FilePath As String)
        CurrentProgress = 0
        MyOutputFile = FilePath
        Dim MyProgress As New FormProgress(FileTask.Save)
        MyProgress.Show()
        IO.File.WriteAllText(MyOutputFile, "NUTVIEW" & vbNewLine)
        AppendHost(CurrentProgress)
    End Sub

    Public Function ContinueSaving() As Integer

        CurrentProgress += 1
        AppendHost(CurrentProgress)

        Dim result As Integer = CInt(98 * CurrentProgress / AllHosts.Count)
        If CurrentProgress = AllHosts.Count - 1 Then result = 100
        Return result

    End Function

    Public Sub AppendHost(iHostNum As Integer)
        Dim cDel As String = "," & vbTab ' Custom Delimeter
        Dim iHost As ClsHost = AllHosts(iHostNum)

        Dim MachineReport As String = "HOST"
        MachineReport &= cDel & "CUSTOM NAME" & cDel & iHost.CustomName
        MachineReport &= cDel & "IP" & cDel & iHost.IP
        MachineReport &= cDel & "HOSTNAME" & cDel & iHost.HostName
        MachineReport &= cDel & "MAC" & cDel & iHost.MacAddress
        MachineReport &= cDel & "MANUFACTURER" & cDel & iHost.Manufacturer
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
