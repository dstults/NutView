Partial Module ModFileIO

    Public Enum FTask
        Idle
        Test
        Import
        Save
        AutoClose
    End Enum

    Private Enum FType
        Unset
        NutCheck
        NutView
        AdvancedIPScanner
        NMap
        Metasploit
    End Enum

    Private InputMode As Integer = FType.NutCheck
    Private FileIoDateTime As DateTime = DateTime.Now
    Private CurrentProgress As Integer
    Private MyFileList() As String

    Public Sub ImportFiles(FilePaths() As String)

        Dim MyProgress As New FormProgress(FTask.Import)
        MyProgress.Show()

        CurrentProgress = 0
        MyFileList = FilePaths
        NextFile()

    End Sub

    Public Function ContinueImport() As Integer
        Dim result As Integer
        If MyFileList.Count > 1 Then
            CurrentProgress += 1
            NextFile()
            result = CInt(98 * CurrentProgress / (MyFileList.Count - 1))
            If CurrentProgress = MyFileList.Count - 1 Then result = 100
        Else
            result = 100
        End If
        If result = 100 Then GetKnownHosts()
        If result > 100 Then Return 100
        Return result
    End Function

    Private Sub NextFile()
        FileIoDateTime = DateTime.Now
        'Try
        ImportFile(MyFileList(CurrentProgress))
            'Catch ex As Exception
        'MsgBox("Error loading/parsing file:" & vbNewLine & ex.Message)
        'End Try
    End Sub

    Private Sub ImportFile(FilePath As String)

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
                    If LCase(Strings.Left(iPart(0), 4)) = "nmap" Or LCase(Strings.Left(iPart(0), 13)) = "starting nmap" Then InputMode = FType.NMap
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
                Case FType.NMap
                    NMapImport(iLine)
                Case FType.Metasploit
            End Select
        Next

    End Sub

End Module
