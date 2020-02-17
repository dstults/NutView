Partial Module ModFileIO

    Private Sub NutViewImport(iPart() As String)
        Select Case LCase(iPart(0))
            Case "host"
                ' main process
                Dim aHost As New ClsHost
                Dim intA As Integer = 1

                Do
                    Select Case LCase(iPart(intA))
                        Case "nutview"
                            If iPart.Length > 1 Then
                                intA += 1
                                InputTime = CDate(iPart(intA))
                            End If
                        Case "custom name"
                            intA += 1
                            aHost.CustomName = iPart(intA)
                        Case "ip"
                            intA += 1
                            aHost.IP = iPart(intA)
                        Case "mac"
                            intA += 1
                            aHost.MacAddress = iPart(intA)
                        Case "manufacturer"
                            intA += 1
                            aHost.Manufacturer = iPart(intA)
                        Case "hostname"
                            intA += 1
                            aHost.HostName = iPart(intA)
                        Case "+ping"
                            intA += 1
                            aHost.Ping.Add(True, CDate(iPart(intA)))
                        Case "-ping"
                            intA += 1
                            aHost.Ping.Add(False, CDate(iPart(intA)))
                        Case "+tcp"
                            intA += 2
                            aHost.Tcp.Add(True, iPart(intA - 1), CDate(iPart(intA)))
                        Case "-tcp"
                            intA += 2
                            aHost.Tcp.Add(False, iPart(intA - 1), CDate(iPart(intA)))
                        Case "+udp"

                        Case "-udp"

                        Case "comment"
                            intA += 1
                            aHost.Comments.Add(iPart(intA))
                        Case Else
                            If iPart(intA).Contains(":") Then
                                Dim getVals() As String = Split(iPart(intA), ":")
                                Select Case LCase(getVals(0))
                                    Case "ping"
                                        aHost.Ping.SetVal(getVals(1), InputTime)
                                    Case "tcp"
                                        aHost.Tcp.SetVal(getVals(1), getVals(2), InputTime)
                                    Case "udp"
                                        'aHost.Udp.SetVal(getVals(1), getVals(2), InputTime)
                                End Select
                            End If
                    End Select
                    intA += 1
                Loop Until intA >= iPart.Count
                aHost.GetEmptiness()

                Dim bHost As ClsHost = Nothing
                If aHost.MacAddress <> "" Then bHost = AllHosts.Find(Function(p) p.MacAddress = aHost.MacAddress)  ' First search by MAC address
                If bHost Is Nothing Then bHost = AllHosts.Find(Function(p) p.IP = aHost.IP)  ' Second search by IP address
                If bHost Is Nothing Then
                    AllHosts.Add(aHost)
                Else
                    MergeHosts(aHost, bHost)
                End If

            Case Else
                'ignore
        End Select
    End Sub

    Private Sub MergeHosts(aHost As ClsHost, bHost As ClsHost)
        ' bhost is already in allhosts
        CheckMergeStrings(aHost.CustomName, bHost.CustomName)
        If bHost.CustomName.Contains("&") Then bHost.Comments.Add("CUSTOM NAME CONFLICT")
        CheckMergeStrings(aHost.IP, bHost.IP)
        If bHost.IP.Contains("&") Then bHost.Comments.Add("IP ADDRESS CONFLICT")
        CheckMergeStrings(aHost.MacAddress, bHost.MacAddress)
        If bHost.MacAddress.Contains("&") Then bHost.Comments.Add("MAC ADDRESS CONFLICT")
        CheckMergeStrings(aHost.Manufacturer, bHost.Manufacturer)
        If bHost.Manufacturer.Contains("&") Then bHost.Comments.Add("MANUFACTURER CONFLICT")
        CheckMergeStrings(aHost.HostName, bHost.HostName)
        If bHost.HostName.Contains("&") Then bHost.Comments.Add("HOSTNAME CONFLICT")
        For intA As Integer = 0 To aHost.Ping.Hits.Count - 1
            If bHost.Ping.Times.Contains(aHost.Ping.Times(intA)) Then
                ' SOMETHING MIGHT BE MESSED UP -- BUT IGNORE FOR NOW
            Else
                Dim NewMax As Integer = bHost.Ping.Hits.Count
                ReDim bHost.Ping.Hits(NewMax), bHost.Ping.Times(NewMax)
                bHost.Ping.Hits(NewMax) = aHost.Ping.Hits(intA)
                bHost.Ping.Times(NewMax) = aHost.Ping.Times(intA)
            End If
        Next
        For intA As Integer = 0 To aHost.Tcp.Hits.Count - 1
            If bHost.Tcp.Times.Contains(aHost.Tcp.Times(intA)) Then
                ' SOMETHING MIGHT BE MESSED UP -- BUT IGNORE FOR NOW
            Else
                Dim NewMax As Integer = bHost.Tcp.Hits.Count
                ReDim bHost.Tcp.Hits(NewMax), bHost.Tcp.Ports(NewMax), bHost.Tcp.Times(NewMax)
                bHost.Tcp.Hits(NewMax) = aHost.Tcp.Hits(intA)
                bHost.Tcp.Ports(NewMax) = aHost.Tcp.Ports(intA)
                bHost.Tcp.Times(NewMax) = aHost.Tcp.Times(intA)
            End If
        Next
        For Each iComment As String In aHost.Comments
            bHost.Comments.Add(iComment) ' SortedSets will automatically delete redundant entries
        Next
        If bHost.IsEmpty And Not aHost.IsEmpty Then bHost.IsEmpty = False
    End Sub

    Private Sub CheckMergeStrings(ByRef StringAdd As String, ByRef StringTgt As String)
        If StringTgt <> StringAdd Then If Not StringTgt.Contains(StringAdd) Then StringTgt &= " & " & StringAdd
    End Sub

End Module
