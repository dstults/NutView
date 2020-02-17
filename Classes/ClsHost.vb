Public Class ClsHost

    Public Class ClsPingData
        Public Value As Single
        Public Hits(0) As Boolean
        Public Times(0) As DateTime

        Public Sub Add(aHit As Boolean, aDateTime As DateTime)
            If aHit Then
                Value = 1
            ElseIf Value > 0 Then
                Value -= 0.1
                If Value < 0 Then Value = 0.1 ' DON'T EVER LET IT COMPLETELY DIE OUT.
            End If
            ArchivePings(aHit, aDateTime)
        End Sub

        Public Sub SetVal(aVal As Single, aDateTime As DateTime)
            If Value > 0 And aVal = 0 Then
                Value = 0.1
            Else
                Value = aVal
            End If
            Dim aHit As Boolean = Value > 0
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
        Public Value(65535) As Single
        Public Hits(0) As Boolean
        Public Ports(0) As Integer ' Not used by ping, naturally.
        Public Times(0) As DateTime

        Public Sub Add(aHit As Boolean, aPort As Integer, aDateTime As DateTime)
            If aHit Then
                OpenPorts.Add(aPort)
                Value(aPort) = 1
            ElseIf Value(aPort) > 0 Then
                Value(aPort) -= 0.1
                If Value(aPort) <= 0 Then
                    Value(aPort) = 0.1 ' DON'T EVER LET IT COMPLETELY DIE OUT.
                    'OpenPorts.Remove(aPort)
                End If
            End If
            ArchiveTcps(aHit, aPort, aDateTime)
        End Sub

        Public Sub SetVal(aPort As Integer, aVal As Single, aDateTime As DateTime)
            If Value(aPort) > 0 And aVal = 0 Then
                Value(aPort) = 0.1
            Else
                Value(aPort) = aVal
            End If
            If Value(aPort) > 0 Then OpenPorts.Add(aPort)
            Dim aHit As Boolean = aVal > 0
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
        If MacAddress <> "" Then HasMeta = True
        If Manufacturer <> "" Then HasMeta = True
        If Comments.Count > 0 Then HasMeta = True
        If HasMeta Or Ping.Value > 0 Or Tcp.OpenPorts.Count > 0 Then IsEmpty = False Else IsEmpty = True
    End Sub

End Class