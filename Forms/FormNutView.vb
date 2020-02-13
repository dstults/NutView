Public Class FormNutView

    Private ShownPorts As New SortedSet(Of Integer)

    Private Sub FormNutView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Darren's NutView " & ProgVersion
        RedoColumns()
    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        OpenFileDialog1.ShowDialog()
        ParseInput()
    End Sub

    Private Sub ParseInput()
        Dim FilePath As String = OpenFileDialog1.FileName
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
                    Dim PossibleIP As Net.IPAddress
                    Try
                        PossibleIP = Net.IPAddress.Parse(iPart(0))
                        Dim intA As Integer = 1
                        Do Until intA >= iPart.Count
                            Select Case iPart(intA)
                                Case 
                            End Select
                            intA += 1
                        Loop
                    Catch ex As Exception
                        ' NOT IP - ignore for now
                    End Try
            End Select
        Next
    End Sub

    Private Sub ChkAutoPort_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAutoPort.CheckedChanged
        Select Case ChkAutoPort.Checked
            Case True
                TxtPorts.Enabled = False
            Case False
                TxtPorts.Enabled = True
        End Select
        RedoColumns()
    End Sub

    Private Sub RedoColumns()
        Do Until DataDisplay.Columns.Count <= 4
            DataDisplay.Columns.Remove(4)
        Loop
        Dim strPorts() As String = Split(TxtPorts.Text, " ")
        For intA As Integer = 0 To strPorts.Count - 1
            ShownPorts.Add(Val(strPorts(intA)))
        Next
        For Each iPort As Integer In ShownPorts
            DataDisplay.Columns.Add("Column" & DataDisplay.Columns.Count + 1, iPort)
            DataDisplay.Columns(DataDisplay.Columns.Count - 1).Width = 30
        Next
    End Sub


End Class
