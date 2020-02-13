Public Class FormNutView

    Private ShownPorts As New SortedSet(Of Integer)

    Private Sub FormNutView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Darren's NutView " & ProgVersion
        RedoColumns()
    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        OpenFileDialog1.ShowDialog()
        ParseFile(OpenFileDialog1.FileName)
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
