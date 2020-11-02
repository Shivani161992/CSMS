' VBScript File

Public Function getSeed()
    Randomize Timer  
    Dim sSeed
    Dim sLeft : Dim sRight
    sLeft = CStr(Int(Rnd * 99999)) : If Len(sLeft) < 5 Then sLeft = String(5 - Len(sLeft), "0") & sLeft
    sRight = CStr(Int(Rnd * 99999)) : If Len(sRight) < 5 Then sRight = String(5 - Len(sRight), "0") & sRight
    sSeed = sLeft & "." & sRight
'   Session("auth_seed") = sSeed
    getSeed = sSeed
End Function

Public Function isValidUser(sPassword,sValidPassword)
    Dim sSeed, sValidHash

    sSeed = Session("auth_seed")
    sValidHash = MD5(sSeed & sValidPassword)
    If ((sSeed <> "") AND (sValidHash =  sPassword)) Then
	Session("auth_granted") = "true"
	isValidUser = "true"
    Else
	Session("auth_granted") = "false"
	isValidUser = "false"
    End If
End Function 