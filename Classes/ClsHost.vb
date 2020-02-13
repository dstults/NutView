Public Class ClsHost

    Public Class ClsHitRecord
        Public Hit As Boolean
        Public Port As Integer ' Not used by ping, naturally.
        Public Time As DateTime
    End Class

    Public Class ClsStringRecord
        Public Text As String
        Public Time As DateTime
    End Class

    Public IP As Net.IPAddress
    Private MAC As New HashSet(Of ClsStringRecord)
    Private Hardware As New HashSet(Of ClsStringRecord)
    Private PingRecords As New HashSet(Of ClsHitRecord)
    Private MacRecords As New HashSet(Of ClsHitRecord)
    Private TcpRecords As New HashSet(Of ClsHitRecord)
    Private Comments As New HashSet(Of ClsStringRecord)

    Public Function Pings() As Single
        Return GetResponseStrength(PingRecords)
    End Function

    Public Function TCP(aPort As Integer)
        Return GetResponseStrength(TcpRecords, aPort)
    End Function

    Private Function GetResponseStrength(aHash As HashSet(Of ClsHitRecord), Optional aPortFilter As Integer = 0) As Single
        Dim Responses As Integer
        Dim Attempts As Integer
        For Each iScan As ClsHitRecord In aHash
            If aPortFilter = iScan.Port Then
                Attempts += 1
                If iScan.Hit Then Responses += 1
            End If
        Next
        Return Responses / Attempts
    End Function
End Class