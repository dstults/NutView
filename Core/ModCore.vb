﻿' Acorn Icon: https://www.iconfinder.com/icons/92461/acorn_icon
' Free Use License: https://creativecommons.org/licenses/by/3.0/us/

Module ModCore

    Public ProgVersion As String = "v0.59"
    Public MainWindow As New FormNutView

    Public AllHosts As New List(Of ClsHost)
    Public KnownHosts As New HashSet(Of ClsHost)
    Public ShownHosts As New HashSet(Of ClsHost)
    Public EmptyHosts As New HashSet(Of ClsHost)
    Public ShownPorts As New SortedSet(Of Integer)
    Public ShownState(5) As Boolean

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
        For intA As Integer = 0 To 5
            ShownState(intA) = True
        Next
    End Sub

    Public Sub GetKnownHosts()
        ' Optimization is good!
        KnownHosts.Clear()
        EmptyHosts.Clear()
        For Each iHost In AllHosts
            If iHost.IsEmpty Then
                EmptyHosts.Add(iHost)
            Else
                KnownHosts.Add(iHost)
            End If
        Next
        GetShownHosts()
    End Sub

    Public Sub GetShownHosts()
        ShownHosts.Clear()
        Dim ShowThisHost As Boolean
        For Each iHost In AllHosts
            ShowThisHost = False
            For Each iPort As Integer In iHost.Tcp.OpenPorts
                If ShownState(PortState.AliveNew) And iHost.Tcp.Value(iPort) = PortState.AliveNew Then ShowThisHost = True
                If ShownState(PortState.AliveStale) And iHost.Tcp.Value(iPort) = PortState.AliveStale Then ShowThisHost = True
                If ShownState(PortState.MissingNew) And iHost.Tcp.Value(iPort) = PortState.MissingNew Then ShowThisHost = True
                If ShownState(PortState.MissingStale) And iHost.Tcp.Value(iPort) = PortState.MissingStale Then ShowThisHost = True
                If ShowThisHost Then Exit For
            Next
            If ShowThisHost Then ShownHosts.Add(iHost)
        Next
    End Sub

End Module
