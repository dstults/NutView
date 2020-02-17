Public Class FormNutView

    Private ShownPorts As New SortedSet(Of Integer)
    Private FinishedLoading As Boolean = False

    Private Enum CT
        CustomName
        IpAddress
        MacAddress
        Manufacturer
        Hostname
        Ping
        Ports
    End Enum

    Private Sub FormNutView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Darren's NutView " & ProgVersion
        RedoColumns()
    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        Select Case OpenFileDialog1.ShowDialog()
            Case DialogResult.Cancel, DialogResult.Abort
                ' Do Nothing
            Case Else
                ImportFiles(OpenFileDialog1.FileNames)
        End Select
    End Sub

    Private Sub ResetHosts()
        DataDisplay.Rows.Clear()
        For Each aHost As ClsHost In KnownHosts
            DataDisplay.Rows.Add()
            Dim jRow As Integer = DataDisplay.Rows.Count - 1 ' (-1 header & -1 adder)

            DataDisplay.Rows(jRow).Cells(CT.CustomName).Value = aHost.CustomName
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(CT.CustomName))
            DataDisplay.Rows(jRow).Cells(CT.IpAddress).Value = aHost.IP
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(CT.IpAddress))
            DataDisplay.Rows(jRow).Cells(CT.MacAddress).Value = aHost.MacAddress
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(CT.MacAddress))
            DataDisplay.Rows(jRow).Cells(CT.Manufacturer).Value = aHost.Manufacturer
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(CT.Manufacturer))
            DataDisplay.Rows(jRow).Cells(CT.Hostname).Value = aHost.HostName
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(CT.Hostname))

            DataDisplay.Rows(jRow).Cells(CT.Ping).Style.BackColor = GetColorFromValue(aHost.Ping.Value)
            If aHost.Ping.Value >= NetState.MissingNew Then
                DataDisplay.Rows(jRow).Cells(CT.Ping).Value = "."
            ElseIf aHost.Ping.Value = NetState.MissingStale Then
                DataDisplay.Rows(jRow).Cells(CT.Ping).Value = "!"
            End If

            For intA As Integer = 0 To ShownPorts.Count - 1
                DataDisplay.Rows(jRow).Cells(CT.Ports + intA).Style.BackColor = GetColorFromValue(aHost.Tcp.Value(ShownPorts(intA)))
                If aHost.Tcp.Value(ShownPorts(intA)) >= NetState.MissingNew Then
                    DataDisplay.Rows(jRow).Cells(CT.Ports + intA).Value = "."
                ElseIf aHost.Tcp.Value(ShownPorts(intA)) = NetState.MissingStale Then
                    DataDisplay.Rows(jRow).Cells(CT.Ports + intA).Value = "!"
                End If
            Next

            For Each aComment As String In aHost.Comments
                If DataDisplay.Rows(jRow).Cells(DataDisplay.Columns.Count - 1).Value = "" Then
                    DataDisplay.Rows(jRow).Cells(DataDisplay.Columns.Count - 1).Value = aComment
                Else
                    DataDisplay.Rows(jRow).Cells(DataDisplay.Columns.Count - 1).Value &= " // " & aComment
                End If
            Next
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(DataDisplay.Columns.Count - 1))
        Next
        FinishedLoading = True
    End Sub

    Private Sub DoCellBestColorCheck(myCell As DataGridViewCell)
        If myCell.Value Is Nothing Then
            myCell.Style.BackColor = Color.Gray
        ElseIf myCell.Value.contains(" & ") Then
            myCell.Style.BackColor = Color.Red
        ElseIf myCell.Value = "-" Or myCell.Value = "" Then
            myCell.Style.BackColor = Color.Gray
        Else
            myCell.Style.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Function GetColorFromValue(aVal As Single) As Color
        Select Case aVal
            Case NetState.Untested
                Return Color.Black
            Case NetState.Dead
                Return Color.Black
            Case NetState.MissingStale
                Return Color.Red
            Case NetState.MissingNew
                Return Color.Yellow
            Case NetState.AliveStale
                Return Color.Green
            Case NetState.AliveNew
                Return Color.Aqua
            Case Else
                Return Color.Orange
        End Select
    End Function

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
        FinishedLoading = False
        Do Until DataDisplay.Columns.Count <= CT.Ports
            DataDisplay.Columns.Remove(DataDisplay.Columns(CT.Ports))
        Loop
        If ChkAutoPort.Checked Then
            ShownPorts.Clear()
            For Each aHost In KnownHosts
                For Each aPort In aHost.Tcp.OpenPorts
                    ShownPorts.Add(aPort)
                Next
            Next
        Else ' Do the port list
            ShownPorts.Clear()
            Dim strPorts() As String = Split(TxtPorts.Text, " ")
            For intA As Integer = 0 To strPorts.Count - 1
                ShownPorts.Add(Val(strPorts(intA)))
            Next
        End If
        For Each iPort As Integer In ShownPorts
            DataDisplay.Columns.Add("Column" & DataDisplay.Columns.Count + 1, iPort)
            DataDisplay.Columns(DataDisplay.Columns.Count - 1).Width = 24
        Next
        DataDisplay.Columns.Add("Column" & DataDisplay.Columns.Count + 1, "Comments")
        DataDisplay.Columns(DataDisplay.Columns.Count - 1).Width = 900
        ResetHosts()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        AllHosts.Clear()
        KnownHosts.Clear()
        EmptyHosts.Clear()
        RedoColumns()
    End Sub

    Private Sub BtnSave1_Click(sender As Object, e As EventArgs) Handles BtnSave1.Click
        SaveFileDialog1.FileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm") & " NutView.csv"
        Select Case SaveFileDialog1.ShowDialog()
            Case DialogResult.Cancel, DialogResult.Abort
                ' Do nothing
            Case Else
                SaveNutView(SaveFileDialog1.FileName, True)
        End Select
    End Sub

    Private Sub FormNutView_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
    End Sub

    Private Sub BtnSave2_Click(sender As Object, e As EventArgs) Handles BtnSave2.Click
        SaveFileDialog1.FileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm") & " NutView.csv"
        Select Case SaveFileDialog1.ShowDialog()
            Case DialogResult.Cancel, DialogResult.Abort
                ' Do nothing
            Case Else
                SaveNutView(SaveFileDialog1.FileName, False)
        End Select

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MyProgress As New FormProgress(FTask.Test)
        MyProgress.Show()
    End Sub

    Private Sub DataDisplay_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataDisplay.CellValueChanged
        If FinishedLoading Then
            Dim outTxt As String = DataDisplay.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If outTxt = "-" Then outTxt = ""
            Select Case e.ColumnIndex
                Case CT.CustomName
                    KnownHosts(e.RowIndex).CustomName = outTxt
                Case CT.Hostname
                    KnownHosts(e.RowIndex).HostName = outTxt
                Case CT.IpAddress
                    KnownHosts(e.RowIndex).IP = outTxt
                Case CT.MacAddress
                    KnownHosts(e.RowIndex).MacAddress = outTxt
                Case CT.Manufacturer
                    KnownHosts(e.RowIndex).Manufacturer = outTxt
                Case DataDisplay.Columns.Count - 1 ' comments
                    KnownHosts(e.RowIndex).Comments.Clear()
                    KnownHosts(e.RowIndex).Comments.Add(outTxt)
            End Select
            DoCellBestColorCheck(DataDisplay.Rows(e.RowIndex).Cells(e.ColumnIndex))
        End If
    End Sub

    Public Sub RefreshDisplay()
        RedoColumns()
    End Sub

End Class
