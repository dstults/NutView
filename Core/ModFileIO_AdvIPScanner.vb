Partial Module ModFileIO

    Private Sub AdvIpScannerImport(iPart() As String)
        Dim AIS_Head() As String

        If LCase(iPart(0)) = "Status" Then
            ' ONE DAY I MIGHT ACTUALLY USE THIS, FOR NOW THEY ARE ACTUALLY HARD-CODED.
            ReDim AIS_Head(iPart.Count - 1)
            For intA As Integer = 0 To iPart.Count - 1
                AIS_Head(intA) = iPart(intA)
            Next
        ElseIf iPart(0) = "On" Or iPart(0) = "Dead" Then
            Dim aHost As ClsHost = AllHosts.Find(Function(p) p.MacAddress = iPart(12))  ' First search by MAC address
            If aHost Is Nothing Then aHost = AllHosts.Find(Function(p) p.IP = iPart(2))  ' Second search by IP address
            If aHost Is Nothing Then
                aHost = New ClsHost
                AllHosts.Add(aHost)
                aHost.IP = iPart(2)
            End If


        End If

    End Sub

End Module
