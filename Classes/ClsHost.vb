Public Class ClsHost

    Public Class ClsPingData
        Public Value As Integer
        Public Hits(0) As Boolean
        Public Times(0) As DateTime

        Public Sub New()
            Value = PortState.Untested
        End Sub

        Public Sub Add(aHit As Boolean, aDateTime As DateTime)
            Select Case Value
                Case PortState.Untested, PortState.Dead
                    If aHit Then Value = PortState.AliveNew
                    If Not aHit Then Value = PortState.Dead
                Case PortState.AliveNew, PortState.AliveStale
                    If aHit Then Value = PortState.AliveStale
                    If Not aHit Then Value = PortState.MissingNew
                Case PortState.MissingNew, PortState.MissingStale
                    If aHit Then Value = PortState.AliveNew
                    If Not aHit Then Value = PortState.MissingStale
            End Select
            ArchivePings(aHit, aDateTime)
        End Sub

        Public Sub SetVal(aVal As Single, aDateTime As DateTime)
            Value = aVal
            Dim aHit As Boolean
            If Value >= PortState.AliveStale Then aHit = True
            ArchivePings(aHit, aDateTime)
        End Sub

        Private Sub ArchivePings(aHit As Boolean, aDateTime As DateTime)
            Dim PingHits As Integer
            If Times(0) = Nothing Then
                PingHits = 0
            Else
                PingHits = Hits.Count
                ReDim Preserve Hits(PingHits), Times(PingHits)
            End If
            Hits(PingHits) = aHit
            Times(PingHits) = aDateTime
        End Sub

    End Class

    Public Class ClsTcpData
        Public OpenPorts As New SortedSet(Of Integer)
        Public Value(65535) As Integer
        Public Hits(0) As Boolean
        Public Ports(0) As Integer ' Not used by ping, naturally.
        Public Times(0) As DateTime

        Public Sub New()
            For intA As Integer = 0 To Value.Count - 1
                Value(intA) = PortState.Untested
            Next
        End Sub

        Public Sub Add(aHit As Boolean, aPort As Integer, aDateTime As DateTime)
            Select Case Value(aPort)
                Case PortState.Untested, PortState.Dead
                    If aHit Then Value(aPort) = PortState.AliveNew
                    If Not aHit Then Value(aPort) = PortState.Dead
                Case PortState.AliveNew, PortState.AliveStale
                    If aHit Then Value(aPort) = PortState.AliveStale
                    If Not aHit Then Value(aPort) = PortState.MissingNew
                Case PortState.MissingNew, PortState.MissingStale
                    If aHit Then Value(aPort) = PortState.AliveNew
                    If Not aHit Then Value(aPort) = PortState.MissingStale
            End Select
            If aHit Then OpenPorts.Add(aPort)
            ArchiveTcps(aHit, aPort, aDateTime)
        End Sub

        Public Sub SetVal(aPort As Integer, aVal As Single, aDateTime As DateTime)
            Value(aPort) = aVal
            Dim aHit As Boolean
            If Value(aPort) >= PortState.MissingStale Then OpenPorts.Add(aPort)
            If Value(aPort) >= PortState.AliveStale Then aHit = True
            ArchiveTcps(aHit, aPort, aDateTime)
        End Sub

        Private Sub ArchiveTcps(aHit As Boolean, aPort As Integer, aDateTime As DateTime)
            Dim TcpHits As Integer
            If Times(0) = Nothing Then
                TcpHits = 0
            Else
                TcpHits = Hits.Count
                ReDim Preserve Hits(TcpHits), Ports(TcpHits), Times(TcpHits)
            End If
            Hits(TcpHits) = aHit
            Ports(TcpHits) = aPort
            Times(TcpHits) = aDateTime
        End Sub

    End Class

    Public CustomName As String
    Public HostName As String
    Public IP As String
    Public MacAddress As String
    Public Manufacturer As String
    Public Ping As New ClsPingData
    Public Tcp As New ClsTcpData
    Public IsEmpty As Boolean

    Public Comments As New HashSet(Of String)

    Public Function IpAddress() As Net.IPAddress
        Return Net.IPAddress.Parse(IP)
    End Function

    Public Sub GetEmptiness()
        Dim HasMeta As Boolean = False
        If CustomName <> "" Then HasMeta = True
        If MacAddress <> "" Then HasMeta = True
        If Manufacturer <> "" Then HasMeta = True
        If Comments.Count > 0 Then HasMeta = True
        If HasMeta Or Ping.Value >= PortState.MissingStale Or Tcp.OpenPorts.Count > 0 Then IsEmpty = False Else IsEmpty = True
    End Sub

End Class