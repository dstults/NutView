Module ModFileIO_Save

    Private CurrentProgress As Integer
    Private MyOutputFile As String
    Private OutputFull As Boolean = True
    Private MachineCodeBuffer As String

    Public Sub SaveNutView(FilePath As String, full As Boolean)

        MyOutputFile = FilePath
        OutputFull = full

        CurrentProgress = 0
        MachineCodeBuffer = ""

        Dim MyProgress As New FormProgress(FTask.Save)
        MyProgress.Show()

        ' Set Header and Write Out file
        IO.File.WriteAllText(MyOutputFile, "NUTVIEW," & DateTime.Now.ToString & vbNewLine)
        'OutputToFileOneLine()
        OutputToBufferOneLine()

    End Sub

    Public Function ContinueSaving() As Integer

        CurrentProgress += 1
        'OutputToFileOneLine()
        OutputToBufferOneLine()

        Dim result As Integer = CInt(98 * CurrentProgress / AllHosts.Count)
        If CurrentProgress = AllHosts.Count - 1 Then result = 100
        If result = 100 Then If MachineCodeBuffer <> "" Then IO.File.AppendAllText(MyOutputFile, MachineCodeBuffer)
        Return result

    End Function

    Private Sub OutputToFileOneLine()
        Dim OneLine As String = AppendHost()
        IO.File.AppendAllText(MyOutputFile, OneLine)
    End Sub

    Private Sub OutputToBufferOneLine()
        MachineCodeBuffer &= AppendHost()
    End Sub

    Public Function AppendHost() As String
        Dim cDel As String = "," & vbTab ' Custom Delimeter
        Dim iHost As ClsHost = AllHosts(CurrentProgress)

        Dim MachineReport As String = "HOST"
        MachineReport &= cDel & "CUSTOM NAME" & cDel & iHost.CustomName
        MachineReport &= cDel & "IP" & cDel & iHost.IP
        MachineReport &= cDel & "HOSTNAME" & cDel & iHost.HostName
        MachineReport &= cDel & "MAC" & cDel & iHost.MacAddress
        MachineReport &= cDel & "MANUFACTURER" & cDel & iHost.Manufacturer

        If OutputFull Then ' FULL MODE ================================================

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

        End If ' ================================================
        If iHost.Comments.Count > 0 Then
            For intA As Integer = 0 To iHost.Comments.Count - 1
                MachineReport &= cDel & "COMMENT"
                MachineReport &= cDel & iHost.Comments(intA)
            Next
        End If
        MachineReport &= vbNewLine
        Return MachineReport
    End Function

End Module
