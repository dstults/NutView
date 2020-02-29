Public Class FormNutView

    Private FinishedLoading As Boolean = False

    Private Enum ColumnTag
        Ip4Address
        CustomName
        Ip6Address
        MacAddress
        PastIPs
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

            DataDisplay.Rows(jRow).Cells(ColumnTag.Ip4Address).Value = aHost.IP
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.Ip4Address))
            DataDisplay.Rows(jRow).Cells(ColumnTag.CustomName).Value = aHost.CustomName
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.CustomName))
            DataDisplay.Rows(jRow).Cells(ColumnTag.Ip6Address).Value = aHost.IP6
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.Ip6Address))
            DataDisplay.Rows(jRow).Cells(ColumnTag.MacAddress).Value = aHost.MacAddress
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.MacAddress))
            DataDisplay.Rows(jRow).Cells(ColumnTag.PastIPs).Value = aHost.PastIPs
            DoCellBestColorCheck(DataDisplay.Rows(jRow).Cells(ColumnTag.PastIPs))
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
                ChkPortShowFilter.Enabled = False
                TxtPorts.Enabled = False
            Case False
                ChkPortShowFilter.Enabled = True
                TxtPorts.Enabled = True
        End Select
        RedoColumns()
    End Sub

    Private Sub RedoColumns()
        GetShownPorts()
        GetShownHosts()
        FinishedLoading = False
        Do Until DataDisplay.Columns.Count <= ColumnTag.Ports
            DataDisplay.Columns.Remove(DataDisplay.Columns(ColumnTag.Ports))
        Loop
        For Each iPort As Integer In ShownPorts
            DataDisplay.Columns.Add("Column" & DataDisplay.Columns.Count + 1, iPort)
            DataDisplay.Columns(DataDisplay.Columns.Count - 1).Width = 24
            DataDisplay.Columns(DataDisplay.Columns.Count - 1).ReadOnly = True
        Next
        DataDisplay.Columns.Add("Column" & DataDisplay.Columns.Count + 1, "Comments")
        DataDisplay.Columns(DataDisplay.Columns.Count - 1).Width = 550
        DataDisplay.Columns(DataDisplay.Columns.Count - 1).ReadOnly = True
        ResetHosts()
    End Sub

    Private Sub GetShownPorts()
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
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        AllHosts.Clear()
        KnownHosts.Clear()
        ShownHosts.Clear()
        RedoColumns()
    End Sub

    Private Sub BtnSave1_Click(sender As Object, e As EventArgs)
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

    Private Sub DataDisplay_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataDisplay.CellValueChanged
        If FinishedLoading Then
            Dim outTxt As String = DataDisplay.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If outTxt = "-" Then outTxt = ""
            Select Case e.ColumnIndex
                Case ColumnTag.CustomName
                    KnownHosts(e.RowIndex).CustomName = outTxt
                Case ColumnTag.Hostname ' Locked?
                    KnownHosts(e.RowIndex).HostName = outTxt
                Case ColumnTag.Ip4Address ' Locked?
                    KnownHosts(e.RowIndex).IP = outTxt
                Case ColumnTag.Ip6Address ' Locked?
                    KnownHosts(e.RowIndex).IP6 = outTxt
                Case ColumnTag.PastIPs ' Locked?
                    KnownHosts(e.RowIndex).PastIPs = outTxt
                Case ColumnTag.MacAddress
                    KnownHosts(e.RowIndex).MacAddress = outTxt
                Case ColumnTag.Manufacturer
                    KnownHosts(e.RowIndex).Manufacturer = outTxt
                Case DataDisplay.Columns.Count - 1 ' comments
                    'KnownHosts(e.RowIndex).Comments.Clear()
                    'KnownHosts(e.RowIndex).Comments.Add(outTxt)
            End Select
            DoCellBestColorCheck(DataDisplay.Rows(e.RowIndex).Cells(e.ColumnIndex))
        End If
    End Sub

    Private Sub DataDisplay_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataDisplay.ColumnHeaderMouseClick
        If e.ColumnIndex = ColumnTag.Ip4Address Then
            Dim direction As Integer = DataDisplay.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection
            Select Case direction
                Case SortOrder.None
                    direction = SortOrder.Ascending
                Case SortOrder.Ascending
                    direction = SortOrder.Descending
                Case SortOrder.Descending
                    direction = SortOrder.Ascending
            End Select
            DataDisplay.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = direction
            If direction = SortOrder.Ascending Then direction = System.ComponentModel.ListSortDirection.Ascending Else direction = System.ComponentModel.ListSortDirection.Descending
            DataDisplay.Sort(DataDisplay.Columns(e.ColumnIndex), direction)
        End If
        If e.ColumnIndex <> ColumnTag.Ip4Address Then DataDisplay.Columns(ColumnTag.Ip4Address).HeaderCell.SortGlyphDirection = SortOrder.None
    End Sub

    Private Sub DataDisplay_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles DataDisplay.SortCompare
        Select Case e.Column.Index
            Case ColumnTag.Ip4Address
                Dim c1val As Int64 = GetIpVal(e.CellValue1.ToString)
                Dim c2val As Int64 = GetIpVal(e.CellValue2.ToString)
                e.SortResult = c1val.CompareTo(c2val)
                e.Handled = True
        End Select
    End Sub

    Public Sub RefreshDisplay()
        RedoColumns()
    End Sub

    Private Sub TxtPorts_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPorts.KeyDown
        If e.KeyCode = Keys.Enter Then
            RedoColumns()
            e.SuppressKeyPress = True ' Stop beeping (new line on monoline textbox)
            e.Handled = True ' To make sure it doesn't try to enter secret new line text
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

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MsgBox("AllHosts.Last.IP: " & AllHosts.Last.IP)
    End Sub

    Private Sub ChkPortShowFilter_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPortShowFilter.CheckedChanged
        RedoColumns()
    End Sub
End Class
