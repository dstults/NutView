Partial Module ModFileIO

    Private Enum IMode
        Unset
        NutCheck
        AdvancedIPScanner
        NetCat
        Metasploit
    End Enum

    Public Sub ParseFile(FilePath As String)
        Dim FileInput() As String = IO.File.ReadAllLines(FilePath)
        Dim InputTime As DateTime = DateTime.Now
        Dim InputMode As Integer = IMode.NutCheck
        For Each iLine As String In FileInput
            Dim iPart() As String = Split(iLine, ",")
            Select Case iPart(0)
                Case "TEST TIME", "INPUT IPS", "INPUT PORTS", "TIMEOUT", "TESTS"
                    InputMode = IMode.NutCheck
                Case Else
                    If Net.iPart(0) Then
            End Select
        Next
        For Each iLine As String In FileInput
            Select Case InputMode
                Case IMode.NutCheck
                    Dim iPart() As String = Split(iLine, ",")
                    NutCheckImport(FileInput)
                Case IMode.AdvancedIPScanner
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
