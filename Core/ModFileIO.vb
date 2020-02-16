Partial Module ModFileIO

    Private Enum IMode
        Unset
        NutCheck
        AdvancedIPScanner
        NetCat
        Metasploit
    End Enum

    Private InputMode As Integer = IMode.NutCheck
    Private InputTime As DateTime = DateTime.Now

    Public Sub ParseFile(FilePath As String)
        Dim FileInput() As String = IO.File.ReadAllLines(FilePath)
        For Each iLine As String In FileInput
            Dim iPart() As String = Split(iLine, ",")
            Select Case LCase(iPart(0))
                Case "nutcheck", "test time", "input ips", "input ports", "timeout", "tests"
                    InputMode = IMode.NutCheck
                Case Else
                    Try
                        Dim testIP As Net.IPAddress = Net.IPAddress.Parse(iPart(0))
                        If testIP IsNot Nothing Then InputMode = IMode.NutCheck
                    Catch ex As Exception

                    End Try
                    If LCase(Strings.Left(iPart(0), 2)) = "on" Or LCase(Strings.Left(iPart(0), 4)) = "dead" Or LCase(Strings.Left(iPart(0), 6)) = "status" Then InputMode = IMode.AdvancedIPScanner

            End Select
            If InputMode <> IMode.Unset Then Exit For
        Next
        For Each iLine As String In FileInput
            Select Case InputMode
                Case IMode.NutCheck
                    Dim iPart() As String = Split(iLine, ",")
                    NutCheckImport(iPart)
                Case IMode.AdvancedIPScanner
                    Dim iPart() As String = Split(iLine, vbTab)
                    AdvIpScannerImport(iPart)
                Case IMode.NetCat
                Case IMode.Metasploit
            End Select
        Next
        ' Optimization is good!
        KnownHosts.Clear()
        EmptyHosts.Clear()
        For Each aHost In AllHosts
            If aHost.IsEmpty Then
                EmptyHosts.Add(aHost)
            Else
                KnownHosts.Add(aHost)
            End If
        Next
        'MsgBox("File Import Complete", vbOKOnly)
    End Sub

End Module
