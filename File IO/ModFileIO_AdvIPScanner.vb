Partial Module ModFileIO

    Private Sub AdvIpScannerImport(iPart() As String)
        Dim AIS_Head() As String

        'If LCase(iPart(0)) = "Status" Then
        '    ' ONE DAY I MIGHT ACTUALLY USE THIS, FOR NOW THEY ARE ACTUALLY HARD-CODED.
        '    ReDim AIS_Head(iPart.Count - 1)
        '    For intA As Integer = 0 To iPart.Count - 1
        '        AIS_Head(intA) = iPart(intA)
        '    Next
        'Try
        If iPart(0) = "On" Or iPart(0) = "Dead" Then
            Dim aHosts As List(Of ClsHost) = AllHosts.FindAll(Function(p) p.MacAddress = iPart(12) And p.IP = iPart(2))
            Dim aHost As ClsHost
            If aHosts.Count = 0 Then aHost = Nothing Else If aHosts.Count = 1 Then aHost = aHosts(0) Else MsgBox("AAAAH!")
            If aHost IsNot Nothing Then ' perfect mac and ip match
                aHost.IP_Date = FileIoDateTime
            Else ' no perfect match
                aHosts = AllHosts.FindAll(Function(p) p.MacAddress = iPart(12))  ' First search by MAC address
                If aHosts.Count = 0 Then aHost = Nothing Else If aHosts.Count = 1 Then aHost = aHosts(0) Else MsgBox("AAAAH!")
                If aHost Is Nothing Then
                    aHosts = AllHosts.FindAll(Function(p) p.IP = iPart(2))  ' Second search by IP address
                    If aHosts.Count = 0 Then aHost = Nothing Else If aHosts.Count = 1 Then aHost = aHosts(0) Else MsgBox("AAAAH!")
                    If aHost Is Nothing Then
                        aHost = New ClsHost
                        AllHosts.Add(aHost)
                        aHost.IP = iPart(2)
                    End If
                    aHost.IP_Date = FileIoDateTime
                    aHost.MacAddress = iPart(12)
                Else ' We have seen this MAC address before.
                    If aHost.IP = "" Then ' This was probably someone that was force-retired...
                        aHost.IP_Date = FileIoDateTime
                        aHost.IP = iPart(2)
                    ElseIf aHost.IP <> iPart(2) Then ' The host with a matching MAC address doesn't have a matching IP address.
                        If aHost.IP_Date = FileIoDateTime Then ' Same time? Must be MAC spoofing or cloned hardware.
                            ' !! ABOVE HERE, WE CONFIRM THAT THE SAME-MAC DIFFERENT-IP HOST WAS SET TO THE THAT IP IN THE SAME SCAN THAT WE HAVE NOW
                            ' !! THAT MEANS THERE SHOULD BE MULTIPLE SAME-MAC DIFFERENT-IP HOSTS IN THE SAME SCAN.
                            ' just stick this one on the same IP then
                            aHosts = AllHosts.FindAll(Function(p) p.IP = iPart(2))
                            If aHosts.Count = 0 Then aHost = Nothing Else If aHosts.Count = 1 Then aHost = aHosts(0) Else MsgBox("AAAAH!")
                            If aHost IsNot Nothing Then ' someone has the same ip
                                If aHost.MacAddress = "" Then ' probably the expected computer, just no mac address saved yet
                                    aHost.MacAddress = iPart(12)
                                ElseIf aHost.MacAddress <> iPart(12) Then ' Someone different is there, so ... ugh, retire whoever WAS there, and park there
                                    aHost.RetireIP()
                                    aHost = New ClsHost
                                    AllHosts.Add(aHost)
                                    aHost.MacAddress = iPart(12)
                                    aHost.IP = iPart(2)
                                End If
                            Else ' WELL, no one with the same IP but likely there's a single host with multiple IPs so let's keep track of them all
                                aHost = New ClsHost
                                AllHosts.Add(aHost)
                                aHost.IP = iPart(2)
                                aHost.IP_Date = FileIoDateTime
                                aHost.MacAddress = iPart(12)
                            End If
                        Else ' Earlier time? It was probably an old IP that changed.
                            Dim bHosts As List(Of ClsHost) = AllHosts.FindAll(Function(p) p.IP = iPart(2))
                            Dim bhost As ClsHost = Nothing
                            If bHosts.Count = 0 Then bhost = Nothing Else If bHosts.Count = 1 Then bhost = bHosts(0) Else MsgBox("AAAAH!")
                            If bhost IsNot Nothing Then
                                bhost.RetireIP()
                                If bhost.MacAddress = aHost.MacAddress Then MsgBox("DUDE YOU GOT ISSUES!")
                            End If
                            aHost.RetireIP()
                            aHost.IP = iPart(2)
                            aHost.IP_Date = FileIoDateTime
                        End If
                    End If
                End If
            End If
            If iPart(1) <> iPart(2) Then aHost.HostName = iPart(1)
            If iPart(4) <> "" Then aHost.Comments.Add("HTTP=" & iPart(4))
            If iPart(5) <> "" Then aHost.Comments.Add("HTTPS=" & iPart(5))
            If iPart(6) <> "" Then aHost.Comments.Add("FTP=" & iPart(6))
            If iPart(7) <> "" Then aHost.Comments.Add("RDP=" & iPart(7))
            If iPart(8) <> "" Then aHost.Comments.Add("Shared Folders=" & iPart(8))
            If iPart(9) <> "" Then aHost.Comments.Add("Shared Printers=" & iPart(9))
            If iPart(10) <> "" Then aHost.Comments.Add("NetBIOS Group=" & iPart(10))
            If iPart(11) <> "" Then aHost.Manufacturer = iPart(11)
            If iPart(12) <> "" Then
                If aHost.MacAddress = "" Then
                    aHost.MacAddress = iPart(12)
                Else
                    If Not aHost.MacAddress.Contains(iPart(12)) Then aHost.MacAddress &= " & " & iPart(12)
                End If
            End If
        End If
        'Catch ex As Exception
        'MsgBox("Error loading/parsing file:" & vbNewLine & ex.Message)
        'End Try

    End Sub

End Module
