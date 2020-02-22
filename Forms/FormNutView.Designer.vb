<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormNutView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNutView))
        Me.DataDisplay = New System.Windows.Forms.DataGridView()
        Me.BtnImport = New System.Windows.Forms.Button()
        Me.BtnSave1 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TxtPorts = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ChkAutoPort = New System.Windows.Forms.CheckBox()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.BtnSave2 = New System.Windows.Forms.Button()
        Me.LblLegendA1 = New System.Windows.Forms.Label()
        Me.LblLegendA2 = New System.Windows.Forms.Label()
        Me.LblLegendB2 = New System.Windows.Forms.Label()
        Me.LblLegendB1 = New System.Windows.Forms.Label()
        Me.LblLegendC2 = New System.Windows.Forms.Label()
        Me.LblLegendC1 = New System.Windows.Forms.Label()
        Me.LblLegendD2 = New System.Windows.Forms.Label()
        Me.LblLegendD1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataDisplay
        '
        Me.DataDisplay.AllowUserToAddRows = False
        Me.DataDisplay.AllowUserToDeleteRows = False
        Me.DataDisplay.AllowUserToResizeColumns = False
        Me.DataDisplay.AllowUserToResizeRows = False
        Me.DataDisplay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataDisplay.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Consolas", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataDisplay.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataDisplay.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Consolas", 8.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataDisplay.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataDisplay.Location = New System.Drawing.Point(6, 53)
        Me.DataDisplay.Name = "DataDisplay"
        Me.DataDisplay.RowHeadersVisible = False
        Me.DataDisplay.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataDisplay.Size = New System.Drawing.Size(740, 317)
        Me.DataDisplay.TabIndex = 0
        '
        'BtnImport
        '
        Me.BtnImport.Location = New System.Drawing.Point(83, 8)
        Me.BtnImport.Name = "BtnImport"
        Me.BtnImport.Size = New System.Drawing.Size(71, 35)
        Me.BtnImport.TabIndex = 1
        Me.BtnImport.Text = "Import"
        Me.BtnImport.UseVisualStyleBackColor = True
        '
        'BtnSave1
        '
        Me.BtnSave1.Location = New System.Drawing.Point(160, 8)
        Me.BtnSave1.Name = "BtnSave1"
        Me.BtnSave1.Size = New System.Drawing.Size(71, 35)
        Me.BtnSave1.TabIndex = 2
        Me.BtnSave1.Text = "Save Full"
        Me.BtnSave1.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "All Files|*.*|CSVs|*.csv"
        Me.OpenFileDialog1.Multiselect = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "All Files|*.*|CSVs|*.csv"
        '
        'TxtPorts
        '
        Me.TxtPorts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPorts.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.TxtPorts.Location = New System.Drawing.Point(411, 7)
        Me.TxtPorts.Name = "TxtPorts"
        Me.TxtPorts.Size = New System.Drawing.Size(335, 24)
        Me.TxtPorts.TabIndex = 3
        Me.TxtPorts.Text = "7 13 17 20 21 22 53 80 139 143 443 445 1723 3389 5900"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(314, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Ports:"
        '
        'ChkAutoPort
        '
        Me.ChkAutoPort.AutoSize = True
        Me.ChkAutoPort.Location = New System.Drawing.Point(362, 13)
        Me.ChkAutoPort.Name = "ChkAutoPort"
        Me.ChkAutoPort.Size = New System.Drawing.Size(48, 17)
        Me.ChkAutoPort.TabIndex = 5
        Me.ChkAutoPort.Text = "Auto"
        Me.ChkAutoPort.UseVisualStyleBackColor = True
        '
        'BtnClear
        '
        Me.BtnClear.Location = New System.Drawing.Point(6, 8)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(71, 35)
        Me.BtnClear.TabIndex = 6
        Me.BtnClear.Text = "Clear"
        Me.BtnClear.UseVisualStyleBackColor = True
        '
        'BtnSave2
        '
        Me.BtnSave2.Location = New System.Drawing.Point(237, 8)
        Me.BtnSave2.Name = "BtnSave2"
        Me.BtnSave2.Size = New System.Drawing.Size(71, 35)
        Me.BtnSave2.TabIndex = 7
        Me.BtnSave2.Text = "Save Short"
        Me.BtnSave2.UseVisualStyleBackColor = True
        '
        'LblLegendA1
        '
        Me.LblLegendA1.BackColor = System.Drawing.Color.Aquamarine
        Me.LblLegendA1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendA1.Location = New System.Drawing.Point(411, 34)
        Me.LblLegendA1.Name = "LblLegendA1"
        Me.LblLegendA1.Size = New System.Drawing.Size(16, 16)
        Me.LblLegendA1.TabIndex = 9
        Me.LblLegendA1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendA2
        '
        Me.LblLegendA2.BackColor = System.Drawing.Color.Aquamarine
        Me.LblLegendA2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendA2.Location = New System.Drawing.Point(428, 34)
        Me.LblLegendA2.Name = "LblLegendA2"
        Me.LblLegendA2.Size = New System.Drawing.Size(60, 16)
        Me.LblLegendA2.TabIndex = 10
        Me.LblLegendA2.Text = "New"
        Me.LblLegendA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendB2
        '
        Me.LblLegendB2.BackColor = System.Drawing.Color.Green
        Me.LblLegendB2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendB2.Location = New System.Drawing.Point(514, 34)
        Me.LblLegendB2.Name = "LblLegendB2"
        Me.LblLegendB2.Size = New System.Drawing.Size(60, 16)
        Me.LblLegendB2.TabIndex = 12
        Me.LblLegendB2.Text = "Up"
        Me.LblLegendB2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendB1
        '
        Me.LblLegendB1.BackColor = System.Drawing.Color.Green
        Me.LblLegendB1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendB1.Location = New System.Drawing.Point(497, 34)
        Me.LblLegendB1.Name = "LblLegendB1"
        Me.LblLegendB1.Size = New System.Drawing.Size(16, 16)
        Me.LblLegendB1.TabIndex = 11
        Me.LblLegendB1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendC2
        '
        Me.LblLegendC2.BackColor = System.Drawing.Color.Yellow
        Me.LblLegendC2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendC2.Location = New System.Drawing.Point(600, 34)
        Me.LblLegendC2.Name = "LblLegendC2"
        Me.LblLegendC2.Size = New System.Drawing.Size(60, 16)
        Me.LblLegendC2.TabIndex = 14
        Me.LblLegendC2.Text = "Missing"
        Me.LblLegendC2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendC1
        '
        Me.LblLegendC1.BackColor = System.Drawing.Color.Yellow
        Me.LblLegendC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendC1.Location = New System.Drawing.Point(583, 34)
        Me.LblLegendC1.Name = "LblLegendC1"
        Me.LblLegendC1.Size = New System.Drawing.Size(16, 16)
        Me.LblLegendC1.TabIndex = 13
        Me.LblLegendC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendD2
        '
        Me.LblLegendD2.BackColor = System.Drawing.Color.Red
        Me.LblLegendD2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendD2.Location = New System.Drawing.Point(686, 34)
        Me.LblLegendD2.Name = "LblLegendD2"
        Me.LblLegendD2.Size = New System.Drawing.Size(60, 16)
        Me.LblLegendD2.TabIndex = 16
        Me.LblLegendD2.Text = "Dead"
        Me.LblLegendD2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLegendD1
        '
        Me.LblLegendD1.BackColor = System.Drawing.Color.Red
        Me.LblLegendD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLegendD1.Location = New System.Drawing.Point(669, 34)
        Me.LblLegendD1.Name = "LblLegendD1"
        Me.LblLegendD1.Size = New System.Drawing.Size(16, 16)
        Me.LblLegendD1.TabIndex = 15
        Me.LblLegendD1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(322, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 15)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Filter by State:"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Custom Name"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 120
        '
        'Column2
        '
        Me.Column2.HeaderText = "IP Address"
        Me.Column2.Name = "Column2"
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.Column2.Width = 110
        '
        'Column3
        '
        Me.Column3.HeaderText = "MAC Address"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 130
        '
        'Column4
        '
        Me.Column4.HeaderText = "Manufacturer"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 140
        '
        'Column5
        '
        Me.Column5.HeaderText = "Hostname"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 120
        '
        'Column6
        '
        Me.Column6.HeaderText = "Ping"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 30
        '
        'FormNutView
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(758, 382)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblLegendD2)
        Me.Controls.Add(Me.LblLegendD1)
        Me.Controls.Add(Me.LblLegendC2)
        Me.Controls.Add(Me.LblLegendC1)
        Me.Controls.Add(Me.LblLegendB2)
        Me.Controls.Add(Me.LblLegendB1)
        Me.Controls.Add(Me.LblLegendA2)
        Me.Controls.Add(Me.LblLegendA1)
        Me.Controls.Add(Me.BtnSave2)
        Me.Controls.Add(Me.BtnClear)
        Me.Controls.Add(Me.ChkAutoPort)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtPorts)
        Me.Controls.Add(Me.BtnSave1)
        Me.Controls.Add(Me.BtnImport)
        Me.Controls.Add(Me.DataDisplay)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormNutView"
        Me.Text = "Darren's NutView"
        CType(Me.DataDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataDisplay As DataGridView
    Friend WithEvents BtnImport As Button
    Friend WithEvents BtnSave1 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents TxtPorts As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ChkAutoPort As CheckBox
    Friend WithEvents BtnClear As Button
    Friend WithEvents BtnSave2 As Button
    Friend WithEvents LblLegendA1 As Label
    Friend WithEvents LblLegendA2 As Label
    Friend WithEvents LblLegendB2 As Label
    Friend WithEvents LblLegendB1 As Label
    Friend WithEvents LblLegendC2 As Label
    Friend WithEvents LblLegendC1 As Label
    Friend WithEvents LblLegendD2 As Label
    Friend WithEvents LblLegendD1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
End Class
