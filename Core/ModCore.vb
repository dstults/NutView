' Acorn Icon: https://www.iconfinder.com/icons/92461/acorn_icon
' Free Use License: https://creativecommons.org/licenses/by/3.0/us/

Module ModCore

    Public ProgVersion As String = "v0.55"
    Public MainWindow As New FormNutView

    Public AllHosts As New List(Of ClsHost)
    Public KnownHosts As New HashSet(Of ClsHost)
    Public EmptyHosts As New HashSet(Of ClsHost)

    Public Enum NetState
        Untested ' Dark Gray
        Dead ' Black
        MissingStale ' Red
        MissingNew ' Yellow
        AliveStale ' Green
        AliveNew ' Aqua
    End Enum

    Public Sub Main()
        MainWindow.Show()
        Application.Run()
    End Sub

End Module
