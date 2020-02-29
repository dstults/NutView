Partial Module ModFileIO

    Private Sub NMapImport(iLine As String)
        Static LastIPv6 As String
        If LCase(Strings.Left(iLine, 9)) = "nmap scan" Then
            'MsgBox(Mid(iLine, InStr(iLine, "for ") + 4))
            LastIPv6 = Mid(iLine, InStr(iLine, "for ") + 4)
        End If
        If LCase(Strings.Left(iLine, 13)) = "mac address: " Then
            Dim MacAndHardware() As String = Split(Mid(iLine, InStr(iLine, "mac address: ") + 14), " ")
            Dim MacAddy As String = MacAndHardware(0)
            Dim hardware As String = Mid(MacAndHardware(1), 2, MacAndHardware(1).Length - 2)
            Dim PossibleMatches As List(Of ClsHost) = AllHosts.FindAll(Function(p) p.MacAddress = MacAddy)
            If PossibleMatches Is Nothing Then
                AllHosts.Add(New ClsHost With {.IP6 = LastIPv6, .MacAddress = MacAddy, .Manufacturer = hardware})
            Else
                For Each iHost As ClsHost In PossibleMatches
                    If iHost.IP6 = "" Then iHost.IP6 = LastIPv6 Else iHost.IP6 = LastIPv6 & ", & " & iHost.IP6
                    If iHost.Manufacturer = "" Then iHost.Manufacturer = hardware
                Next
            End If
        End If
    End Sub

End Module
