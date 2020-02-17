Partial Module ModFileIO

    Public Enum FileTask
        Idle
        Test
        Import
        Save
    End Enum

    Private Enum FType
        Unset
        NutCheck
        NutView
        AdvancedIPScanner
        NetCat
        Metasploit
    End Enum

    Private InputMode As Integer = FType.NutCheck
    Private InputTime As DateTime = DateTime.Now

    Public Sub ParseFile(FilePath As String)
        Dim FileInput() As String = IO.File.ReadAllLines(FilePath)
        For Each iLine As String In FileInput
            Dim iPart() As String = Split(iLine, ",")
            Select Case LCase(iPart(0))
                Case "nutview", "host"
                    InputMode = FType.NutView
                Case "nutcheck", "test time", "input ips", "input ports", "timeout", "tests"
                    InputMode = FType.NutCheck
                Case Else
                    Try
                        Dim testIP As Net.IPAddress = Net.IPAddress.Parse(iPart(0))
                        If testIP IsNot Nothing Then InputMode = FType.NutCheck
                    Catch ex As Exception

                    End Try
                    If LCase(Strings.Left(iPart(0), 2)) = "on" Or LCase(Strings.Left(iPart(0), 4)) = "dead" Or LCase(Strings.Left(iPart(0), 6)) = "status" Then InputMode = FType.AdvancedIPScanner

            End Select
            If InputMode <> FType.Unset Then Exit For
        Next
        For Each iLine As String In FileInput
            Select Case InputMode
                Case FType.NutView
                    Dim iPart() As String = Split(iLine, "," & vbTab)
                    NutViewImport(iPart)
                Case FType.NutCheck
                    Dim iPart() As String = Split(iLine, ",")
                    NutCheckImport(iPart)
                Case FType.AdvancedIPScanner
                    Dim iPart() As String = Split(iLine, vbTab)
                    AdvIpScannerImport(iPart)
                Case FType.NetCat
                Case FType.Metasploit
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

    Public Function ContinueLoading() As Integer
        Return 0
    End Function

End Module
