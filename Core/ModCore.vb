' Acorn Icon: https://www.iconfinder.com/icons/92461/acorn_icon
' Free Use License: https://creativecommons.org/licenses/by/3.0/us/

Module ModCore

    Public ProgVersion As String = "v0.65"
    Public MainWindow As New FormNutView

    Public AllHosts As New List(Of ClsHost)
    Public KnownHosts As New SortedSet(Of ClsHost)
    Public ShownHosts As New SortedSet(Of ClsHost)
    Public ShownPorts As New SortedSet(Of Integer)
    Public ShownState(PortState.AliveNew) As Boolean

    Public Enum PortState
        Untested ' Dark Gray
        Dead ' Black
        MissingStale ' Red
        MissingNew ' Yellow
        AliveStale ' Green
        AliveNew ' Aqua
    End Enum

    Public Sub Main()
        GlobalSetup()
        MainWindow.Show()
        Application.Run()
    End Sub

    Private Sub GlobalSetup()
        For intA As Integer = PortState.MissingStale To PortState.AliveNew
            ShownState(intA) = True
        Next
    End Sub

    Public Sub GetKnownHosts()
        ' Optimization is good!
        KnownHosts.Clear()
        For Each iHost In AllHosts
            If iHost.IsEmpty Then
                MsgBox("This can probably be deleted? Debug?")
            Else
                KnownHosts.Add(iHost)
            End If
        Next
        GetShownHosts()
    End Sub

    Public Sub GetShownHosts()
        ShownHosts.Clear()
        Dim ShowThisHost1 As Boolean
        Dim ShowThisHost2 As Boolean
        Dim ShowEverybody As Boolean = ShownState(PortState.AliveNew) And ShownState(PortState.AliveStale) And ShownState(PortState.MissingNew) And ShownState(PortState.MissingStale)
        For Each iHost In KnownHosts
            ShowThisHost1 = False
            If MainWindow.ChkPortShowFilter.Checked And Not MainWindow.ChkAutoPort.Checked Then
                For Each iPort As Integer In iHost.Tcp.OpenPorts
                    If ShownPorts.Contains(iPort) Then
                        If iHost.Tcp.Value(iPort) > PortState.Dead Then ShowThisHost1 = True : Exit For
                    End If
                Next
            Else
                ShowThisHost1 = True
            End If
            ShowThisHost2 = False
            If ShowEverybody Then
                ShowThisHost2 = True
            Else
                If Not MainWindow.ChkPortShowFilter.Checked Or MainWindow.ChkAutoPort.Checked Then
                    If ShownState(iHost.Ping.Value) Then ShowThisHost2 = True
                End If
                If ShowThisHost2 = False Then
                    For Each iPort As Integer In iHost.Tcp.OpenPorts
                        'If iHost.IP = "172.16.1.106" Then MsgBox("CheckMe")
                        If ShownPorts.Contains(iPort) Then
                            If ShownState(iHost.Tcp.Value(iPort)) Then ShowThisHost2 = True : Exit For
                        End If
                    Next
                End If
            End If
            If ShowThisHost1 And ShowThisHost2 Then ShownHosts.Add(iHost)
        Next
    End Sub

    Public Function GetIpVal(ipAdd As String) As Int64
        If ipAdd = "" Then Return 0
        If ipAdd.Contains("&") Then Return 0
        Dim ipVars() As String = Split(ipAdd, ".")
        If ipVars.Count <> 4 Then Return 0
        Dim result As Int64 = 0
        result += ipVars(0) * 256 * 256 * 256
        result += ipVars(1) * 256 * 256
        result += ipVars(2) * 256
        result += ipVars(3)
        Return result
    End Function

End Module
