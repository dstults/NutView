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
                If Value < 0 Then Value = 0
            End If
            Dim PingHits As Integer = Hits.Count
            ReDim Preserve Hits(PingHits), Times(PingHits)
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
                    Value(aPort) = 0
                End If
                OpenPorts.Remove(aPort)
            End If
            Dim TcpHits As Integer = Hits.Count
            ReDim Preserve Hits(TcpHits), Ports(TcpHits), Times(TcpHits)
            Hits(TcpHits) = aHit
            Ports(TcpHits) = aPort
            Times(TcpHits) = aDateTime
        End Sub

    End Class

    Public IP As String
    Public MacAddress As String
    Public Hardware As String
    Public Ping As New ClsPingData
    Public Tcp As New ClsTcpData
    Public IsEmpty As Boolean

    Private Comments As New HashSet(Of String)

    Public Function IpAddress() As Net.IPAddress
        Return Net.IPAddress.Parse(IP)
    End Function

    Public Sub GetEmptiness()
        Dim HasMeta As Boolean = False
        If MacAddress <> "" Then HasMeta = True
        If Hardware <> "" Then HasMeta = True
        If Comments.Count > 0 Then HasMeta = True
        If HasMeta Or Ping.Value > 0 Or Tcp.OpenPorts.Count > 0 Then IsEmpty = False Else IsEmpty = True
    End Sub

End Class