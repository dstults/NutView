Public Class FormNutView

    Private FinishedLoading As Boolean = False

    Private Enum ColumnTag
        CustomName
        IpAddress
        MacAddress
        Manufacturer
        Hostname
        Ping
        Ports
    End Enum

    Private Sub FormNutView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MinimumSize = New System.Drawing.Size(774, 421)
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
        For Each aHost As ClsHost In ShownHosts
            DataDisplay.Rows.Add()
            Dim jRow As Integer = DataDisplay.Rows.Count - 1 ' (-1 header & -1 adder)

            DataDisplay.Rows(jRow).Cells(ColumnTag.CustomName).Value = aHost.CustomName
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.CustomName))
            DataDisplay.Rows(jRow).Cells(ColumnTag.IpAddress).Value = aHost.IP
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.IpAddress))
            DataDisplay.Rows(jRow).Cells(ColumnTag.MacAddress).Value = aHost.MacAddress
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.MacAddress))
            DataDisplay.Rows(jRow).Cells(ColumnTag.Manufacturer).Value = aHost.Manufacturer
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.Manufacturer))
            DataDisplay.Rows(jRow).Cells(ColumnTag.Hostname).Value = aHost.HostName
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.Hostname))

            DataDisplay.Rows(jRow).Cells(ColumnTag.Ping).Style.BackColor = GetColorFromValue(aHost.Ping.Value)
            If aHost.Ping.Value >= PortState.MissingNew Then
                DataDisplay.Rows(jRow).Cells(ColumnTag.Ping).Value = "."
            ElseIf aHost.Ping.Value = PortState.MissingStale Then
                DataDisplay.Rows(jRow).Cells(ColumnTag.Ping).Value = "!"
            End If

            For intA As Integer = 0 To ShownPorts.Count - 1
                DataDisplay.Rows(jRow).Cells(ColumnTag.Ports + intA).Style.BackColor = GetColorFromValue(aHost.Tcp.Value(ShownPorts(intA)))
                If aHost.Tcp.Value(ShownPorts(intA)) >= PortState.MissingNew Then
                    DataDisplay.Rows(jRow).Cells(ColumnTag.Ports + intA).Value = "."
                ElseIf aHost.Tcp.Value(ShownPorts(intA)) = PortState.MissingStale Then
                    DataDisplay.Rows(jRow).Cells(ColumnTag.Ports + intA).Value = "!"
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
            Case PortState.Untested
                Return Color.Black
            Case PortState.Dead
                Return Color.Black
            Case PortState.MissingStale
                Return Color.Red
            Case PortState.MissingNew
                Return Color.Yellow
            Case PortState.AliveStale
                Return Color.Green
            Case PortState.AliveNew
                Return Color.Aquamarine
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
        GetShownHosts()
        FinishedLoading = False
        Do Until DataDisplay.Columns.Count <= ColumnTag.Ports
            DataDisplay.Columns.Remove(DataDisplay.Columns(ColumnTag.Ports))
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
        DataDisplay.Columns(DataDisplay.Columns.Count - 1).Width = 700
        ResetHosts()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        AllHosts.Clear()
        KnownHosts.Clear()
        ShownHosts.Clear()
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
                Case ColumnTag.CustomName
                    KnownHosts(e.RowIndex).CustomName = outTxt
                Case ColumnTag.Hostname
                    KnownHosts(e.RowIndex).HostName = outTxt
                Case ColumnTag.IpAddress
                    KnownHosts(e.RowIndex).IP = outTxt
                Case ColumnTag.MacAddress
                    KnownHosts(e.RowIndex).MacAddress = outTxt
                Case ColumnTag.Manufacturer
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

    Private Sub TxtPorts_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPorts.KeyDown
        If e.KeyCode = Keys.Enter Then
            RedoColumns()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub LblLegendA_Click(sender As Object, e As EventArgs) Handles LblLegendA1.Click, LblLegendA2.Click
        ShownState(PortState.AliveNew) = Not ShownState(PortState.AliveNew)
        If ShownState(PortState.AliveNew) Then
            LblLegendA2.BackColor = Color.Aquamarine
            LblLegendA2.ForeColor = Color.Black
        Else
            LblLegendA2.BackColor = Color.Black
            LblLegendA2.ForeColor = Color.White
        End If
        RedoColumns()
    End Sub

    Private Sub LblLegendB_Click(sender As Object, e As EventArgs) Handles LblLegendB1.Click, LblLegendB2.Click
        ShownState(PortState.AliveStale) = Not ShownState(PortState.AliveStale)
        If ShownState(PortState.AliveStale) Then
            LblLegendB2.BackColor = Color.Green
            LblLegendB2.ForeColor = Color.Black
        Else
            LblLegendB2.BackColor = Color.Black
            LblLegendB2.ForeColor = Color.White
        End If
        RedoColumns()
    End Sub

    Private Sub LblLegendC_Click(sender As Object, e As EventArgs) Handles LblLegendC1.Click, LblLegendC2.Click
        ShownState(PortState.MissingNew) = Not ShownState(PortState.MissingNew)
        If ShownState(PortState.MissingNew) Then
            LblLegendC2.BackColor = Color.Yellow
            LblLegendC2.ForeColor = Color.Black
        Else
            LblLegendC2.BackColor = Color.Black
            LblLegendC2.ForeColor = Color.White
        End If
        RedoColumns()
    End Sub

    Private Sub LblLegendD_Click(sender As Object, e As EventArgs) Handles LblLegendD1.Click, LblLegendD2.Click
        ShownState(PortState.MissingStale) = Not ShownState(PortState.MissingStale)
        If ShownState(PortState.MissingStale) Then
            LblLegendD2.BackColor = Color.Red
            LblLegendD2.ForeColor = Color.Black
        Else
            LblLegendD2.BackColor = Color.Black
            LblLegendD2.ForeColor = Color.White
        End If
        RedoColumns()
    End Sub

End Class
