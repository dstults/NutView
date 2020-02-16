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
            If iPart(0) = "Dead" Then aHost.Ping.Add(False, InputTime)
            If iPart(1) <> iPart(2) Then aHost.HostName = iPart(1)
            If iPart(4) <> "" Then
                aHost.Tcp.Add(True, 80, InputTime)
                aHost.Comments.Add("HTTP=" & iPart(4))
            End If
            If iPart(5) <> "" Then
                aHost.Tcp.Add(True, 443, InputTime)
                aHost.Comments.Add("HTTPS=" & iPart(5))
            End If
            If iPart(6) <> "" Then
                aHost.Tcp.Add(True, 21, InputTime)
                aHost.Comments.Add("FTP=" & iPart(6))
            End If
            If iPart(7) <> "" Then
                aHost.Tcp.Add(True, 3389, InputTime)
                aHost.Comments.Add("RDP=" & iPart(7))
            End If
            If iPart(8) <> "" Then aHost.Comments.Add("Shared Folders=" & iPart(8))
            If iPart(9) <> "" Then aHost.Comments.Add("Shared Printers=" & iPart(9))
            If iPart(10) <> "" Then aHost.Comments.Add("NetBIOS Group=" & iPart(10))
            If iPart(11) <> "" Then
                aHost.Manufacturer = iPart(11)
            End If
            If iPart(12) <> "" Then
                If aHost.MacAddress = "" Then
                    aHost.MacAddress = iPart(12)
                Else
                    If InStr(aHost.MacAddress, iPart(12)) > 0 Then
                        ' do nothing
                    Else
                        aHost.MacAddress &= "," & iPart(12)
                    End If
                End If
            End If
        End If
    End Sub

End Module
