Imports System.Data.SqlClient
Partial Class leadsoc_creat
    Inherits System.Web.UI.Page
    Public con As New SqlConnection(ConfigurationManager.ConnectionStrings("constr_opdms").ToString())

    Public concsms As New SqlConnection(ConfigurationManager.ConnectionStrings("constr").ToString())


    'Public cont As New SqlConnection(ConfigurationManager.ConnectionStrings("constr_opdms").ToString())   
    Dim dist As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("dist_id") = "" Then
                Response.Redirect("~/Session_Expire_Dist.aspx")
            Else
                dist = Session("dist_id").ToString()
            End If
        Catch ex As Exception

        End Try
        If Page.IsPostBack = False Then
            Dim cmds As New SqlCommand()
            cmds.Connection = con
            Dim dr1 As SqlDataReader
            cmds.CommandText = "select district_name from pds.districtsmp  where district_code='" & dist & "'"
            con.Open()
            dr1 = cmds.ExecuteReader()
            While dr1.Read()
                distname.Text = dr1("district_name").ToString()
            End While
            dr1.Close()
            con.Close()
            DistSel()
            issuename.Focus()
        End If
        leadmob.Attributes.Add("onkeypress", "return CheckIsNumeric(this);")
        leadtele.Attributes.Add("onkeypress", "return CheckIsNumeric(this);")
        leadtele.Style("TEXT-ALIGN") = TextAlign.Right.ToString()
        leadmob.Style("TEXT-ALIGN") = TextAlign.Right.ToString()
        leadname_h.Style("TEXT-ALIGN") = TextAlign.Left.ToString()
    End Sub
    Protected Sub DistSel()
        If distname.Text <> "" Then
            Button1.Visible = True
            update.Visible = False
            issuename.Items.Clear()
            blockname.Items.Clear()
            fpsname.Items.Clear()
            fpslist.Items.Clear()
            lead.Items.Clear()
            Dim distcode As String = dist
            Dim cmds As New SqlCommand()
            cmds.Connection = con
            Dim dr1 As SqlDataReader
            cmds.CommandText = "select * from pds.block_master  where District_code='" & distcode & "' order by Block_name"
            con.Open()
            dr1 = cmds.ExecuteReader()
            While dr1.Read()
                Dim lstitem As New ListItem
                lstitem.Text = dr1("Block_Uname").ToString()
                lstitem.Value = dr1("block_code").ToString()
                blockname.Items.Add(lstitem)
            End While
            blockname.Items.Insert("0", "Select")
            dr1.Close()
            con.Close()
            distcode = "23" + distcode
            Dim cmdd As New SqlCommand()
            cmdd.Connection = concsms

            Dim drd As SqlDataReader
            cmdd.CommandText = "select * from dbo.tbl_MetaData_DEPOT  where DistrictId='" & distcode & "' order by DepotName"

            concsms.Open()
            drd = cmdd.ExecuteReader()
            While drd.Read()
                Dim lstitem As New ListItem
                lstitem.Text = drd("DepotName").ToString()
                lstitem.Value = drd("DepotID").ToString()
                issuename.Items.Add(lstitem)
            End While
            Dim lstitem1 As New ListItem
            lstitem1.Text = "Select"
            lstitem1.Value = ""
            issuename.Items.Insert("0", lstitem1)
            drd.Close()
            concsms.Close()
            blockname.Focus()
        Else
            issuename.Items.Clear()
            blockname.Items.Clear()
            fpsname.Items.Clear()
            fpslist.Items.Clear()
            lead.Items.Clear()
        End If
    End Sub

    Protected Sub issuename_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles issuename.SelectedIndexChanged
        Label1.Text = ""
        If issuename.SelectedItem.Text <> "Select" And lead.SelectedItem.Text <> "Select" Then
            Dim temp As String = ""
            Dim c As Integer = lst_issue.Items.Count - 1
            While c >= 0
                If lst_issue.Items(c).Value = issuename.SelectedItem.Value Then
                    'MsgBox("Fair Price Shop Already Exist..")
                    Label1.Text = "IssueCentre: " + issuename.SelectedItem.Text + " Already Exists.."
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('IssueCentre already Exist ...');</script>")
                    temp = "N"
                    Exit While
                End If
                c = c - 1
            End While
            If temp = "" Then
                Dim lstitem As New ListItem
                lstitem.Text = issuename.SelectedItem.Text
                lstitem.Value = issuename.SelectedItem.Value
                lst_issue.Items.Add(lstitem)
                Label1.Text = ""
            End If
        End If
    End Sub

    Protected Sub blockname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles blockname.SelectedIndexChanged
        Label1.Text = ""
        leadname.Text = ""
        leadname_h.Text = ""
        leadadd.Text = ""
        leadtele.Text = ""
        leadmob.Text = ""
        leadname.Enabled = False
        leadname_h.Enabled = False
        fpsname.Items.Clear()
        fpslist.Items.Clear()
        lead.Items.Clear()
        lst_issue.Items.Clear()
        issuename.Enabled = False
        If blockname.SelectedItem.Text <> "Select" Then
            Button1.Enabled = True
            update.Enabled = True
            leadname.Enabled = True
            leadname_h.Enabled = True
            Dim distcode As String = dist
            Dim blockcode As String = blockname.SelectedItem.Value
            Dim cmds As New SqlCommand()
            cmds.Connection = con
            Dim dr1 As SqlDataReader
            cmds.CommandText = "select * from pds.fps_master where district_code='" & distcode & "' and block_code='" & blockcode & "' order by fps_name"
            con.Open()
            dr1 = cmds.ExecuteReader()
            While dr1.Read()
                Dim lstitem As New ListItem
                lstitem.Text = dr1("fps_code").ToString() + " - " + dr1("fps_Uname").ToString()
                lstitem.Value = dr1("fps_code").ToString()
                fpsname.Items.Add(lstitem)
                fpslist.Items.Add(lstitem)
            End While
            fpsname.Items.Insert("0", "Select")
            dr1.Close()
            con.Close()
            Dim temp As String = ""
            Dim cmdd As New SqlCommand()
            cmdd.Connection = con
            Dim drd As SqlDataReader
            cmdd.CommandText = "select * from dbo.m_LeadSoc where District_code='" & distcode & "' and block_Code='" & blockcode & "'"
            con.Open()
            drd = cmdd.ExecuteReader()
            While drd.Read()
                Dim lstitem As New ListItem
                lstitem.Text = drd("LeadSoc_nameU").ToString()
                lstitem.Value = drd("LeadSoc_Code").ToString()
                lead.Items.Add(lstitem)
                temp = "YYY"
            End While

            If temp = "YYY" Then
                lead.Items.Insert(0, "Select")
                lead.Items.Insert(1, "New Lead")
                leadname.Enabled = False
                leadname_h.Enabled = False
            Else
                lead.Items.Insert(0, "New Lead")
                Button1.Visible = True
                update.Visible = False
            End If
            drd.Close()
            con.Close()
            issuename.Enabled = True
        End If
    End Sub
    Protected Sub fpsname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fpsname.SelectedIndexChanged
        Label1.Text = ""
        If fpsname.SelectedItem.Text <> "Select" And lead.SelectedItem.Text <> "Select" Then
            Dim temp As String = ""
            Dim c As Integer = fpslist.Items.Count - 1
            While c >= 0
                If fpslist.Items(c).Value = fpsname.SelectedItem.Value Then
                    'MsgBox("Fair Price Shop Already Exist..")
                    Label1.Text = "FPS: " + fpsname.SelectedItem.Text + " Already Exists.."
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('FPS already Exist ...');</script>")
                    temp = "N"
                    Exit While
                End If
                c = c - 1
            End While
            If temp = "" Then
                Dim lstitem As New ListItem
                lstitem.Text = fpsname.SelectedItem.Text
                lstitem.Value = fpsname.SelectedItem.Value
                fpslist.Items.Add(lstitem)
                Label1.Text = ""
            End If
        End If
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim ldcode As String = lead.SelectedItem.Value
        If leadname.Text = "" And leadname_h.Text = "" Then
            Label1.Text = "Please enter Lead Society Name ..."
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Please enter Lead Society Name ...');</script>")
        ElseIf blockname.SelectedItem.Text <> "Select" And fpslist.Items.Count > 0 Then
            Dim S1 As String = ""
            Dim num As Double = 0
            Dim distcode As String = dist
            Dim blockcode As String = blockname.SelectedItem.Value
            Dim lead_code As String = ""
            lead_code = distcode + blockcode
            Dim maxcode As String = ""
            Dim Auto As New SqlCommand
            Auto.CommandText = "select max(LeadSoc_Code) ANID from dbo.m_LeadSoc where District_code='" & distcode & "' and block_code='" & blockcode & "'"
            Auto.Connection = con
            Dim autodr As SqlDataReader
            con.Open()
            autodr = Auto.ExecuteReader()
            While autodr.Read
                maxcode = autodr("ANID").ToString()
            End While
            autodr.Close()
            con.Close()
            If maxcode = "" Then
                lead_code = lead_code + "01"
            Else
                num = CType(maxcode, Double) + 1
                lead_code = num.ToString()
            End If
            If lead_code.Length < 6 Then
                lead_code = "0" + lead_code
            End If
            'Response.Write("lead_code=" & lead_code)
            Dim dcode, icode, bcode As String
            dcode = dist
            icode = issuename.SelectedItem.Value
            bcode = blockname.SelectedItem.Value

            Dim cmd As New SqlCommand()
            cmd.Connection = con
            S1 = "insert into dbo.m_LeadSoc (District_code ,block_code ,LeadSoc_Code,LeadSoc_name,LeadSoc_nameU,LeadSoc_address ,LeadSoc_tel ,LeadSoc_mob,created_date,updated_date) values "
            S1 = S1 & " ('" & dcode & "','" & blockcode & "','" & lead_code & "','" & leadname.Text.Trim() & "','" & chkSha(leadname_h.Text.Trim()) & "','" & leadadd.Text.Trim() & "','" & leadtele.Text.Trim() & "','" & leadmob.Text.Trim() & "',getdate(),'')"
            cmd.CommandText = S1
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Dim itm_code As String = ""
            Dim c As Integer = fpslist.Items.Count - 1
            While c >= 0
                'lt_fps.SelectedIndex = c
                itm_code = fpslist.Items(c).Value
                Dim cmd2 As New SqlCommand()
                S1 = "insert into dbo.Lead_soc_fps(LeadSoc_Code,District_code,block_code,fps_code,created_date,updated_date) values('" & lead_code & "','" & dcode & "','" & bcode & "','" & itm_code & "',getdate(),'')"
                cmd2.Connection = con
                cmd2.CommandText = S1
                con.Open()
                cmd2.ExecuteNonQuery()
                con.Close()
                c = c - 1
            End While
            c = lst_issue.Items.Count - 1
            While c >= 0
                'lt_fps.SelectedIndex = c
                itm_code = lst_issue.Items(c).Value
                Dim cmdi As New SqlCommand()
                S1 = "insert into dbo.Lead_soc_issuecentre(District_code,block_code,LeadSoc_Code,issueCentre_code) values('" & dcode & "','" & bcode & "','" & lead_code & "','" & itm_code & "')"
                cmdi.Connection = con
                cmdi.CommandText = S1
                con.Open()
                cmdi.ExecuteNonQuery()
                con.Close()
                c = c - 1
            End While
            Label1.Text = "Data Saved Successfully..."
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>")
            Button1.Enabled = False
            update.Enabled = False
            blockname.SelectedIndex = 0
        Else
            Label1.Text = "Please select Block name... "
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Please select Block ...');</script>")
        End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Label1.Text = ""
        Dim c As Integer = fpslist.SelectedIndex
        Dim distcode As String = dist
        Dim Str As String = ""
        If c <> -1 Then
            Dim ldcode As String = lead.SelectedItem.Value
            If lead.SelectedItem.Text <> "Select" And lead.SelectedItem.Text <> "New Lead" Then
                Str = fpslist.Items(c).Value
                Dim cmd As New SqlCommand
                cmd.Connection = con
                cmd.CommandText = "delete from dbo.Lead_soc_fps where fps_code='" & Str & "' and LeadSoc_Code='" & ldcode & "' and District_code='" & distcode & "' and block_code='" & blockname.SelectedItem.Value & "'"
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End If
            Label1.Text = "FPS: " + fpslist.Items(c).Text + " Removed Successfully ..."
            fpslist.Items.RemoveAt(c)
        End If
    End Sub
    Protected Sub update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update.Click
        Label1.Text = ""
        If leadname.Text = "" And leadname_h.Text = "" Then
            Label1.Text = "Please enter Lead Society Name ..."
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Please enter Lead Society Name ...');</script>")
        ElseIf lead.SelectedItem.Text <> "Select" Or fpslist.Items.Count > 0 Then
            Dim ldcode As String = lead.SelectedItem.Value
            Dim cmd As New SqlCommand()
            cmd.Connection = con
            ' For FPS Updation
            Dim str1 As String = "update dbo.m_LeadSoc set LeadSoc_name='" & leadname.Text.Trim() & "',LeadSoc_nameU='" & chkSha(leadname_h.Text.Trim()) & "',LeadSoc_address='" & leadadd.Text.Trim() & "',LeadSoc_tel='" & leadtele.Text.Trim() & "',LeadSoc_mob='" & leadmob.Text.Trim() & "' where LeadSoc_Code='" & ldcode & "'"
            cmd.CommandText = str1
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Dim temp As String = ""
            Dim str2 As String = ""
            Dim c As Integer = fpslist.Items.Count - 1
            Dim itm_code As String = ""
            While c >= 0
                itm_code = fpslist.Items(c).Value
                Dim dcode, bcode As String
                dcode = dist
                bcode = blockname.SelectedItem.Value
                temp = ""
                Dim cmd4 As New SqlCommand()
                cmd4.Connection = con
                Dim dr4 As SqlDataReader
                cmd4.CommandText = "select fps_code from dbo.Lead_soc_fps where LeadSoc_Code='" & ldcode & "' and District_code='" & dcode & "' and block_code='" & bcode & "' "
                con.Open()
                dr4 = cmd4.ExecuteReader()
                While dr4.Read()
                    If itm_code = dr4("fps_code").ToString() Then
                        temp = "N"
                        Exit While
                    End If
                End While
                dr4.Close()
                con.Close()
                If temp = "" Then
                    Dim cmdc As New SqlCommand()
                    cmdc.Connection = con
                    str2 = "insert into dbo.Lead_soc_fps(LeadSoc_Code,District_code,block_code,fps_code,created_date,updated_date) values('" & ldcode & "','" & dcode & "','" & bcode & "','" & itm_code & "',getdate(),'')"
                    cmdc.CommandText = str2
                    con.Open()
                    cmdc.ExecuteNonQuery()
                    con.Close()
                End If
                c = c - 1
            End While

            ' For IssueCentre Updation            
            temp = ""
            str2 = ""
            c = lst_issue.Items.Count - 1
            itm_code = ""
            While c >= 0
                itm_code = lst_issue.Items(c).Value
                Dim dcode, bcode As String
                dcode = dist
                bcode = blockname.SelectedItem.Value
                temp = ""
                Dim cmd4 As New SqlCommand()
                cmd4.Connection = con
                Dim dr4 As SqlDataReader
                cmd4.CommandText = "select issueCentre_code from dbo.Lead_soc_issuecentre where LeadSoc_Code='" & ldcode & "' and District_code='" & dcode & "' and block_code='" & bcode & "' "
                con.Open()
                dr4 = cmd4.ExecuteReader()
                While dr4.Read()
                    If itm_code = dr4("issueCentre_code").ToString() Then
                        temp = "N"
                        Exit While
                    End If
                End While
                dr4.Close()
                con.Close()
                If temp = "" Then
                    Dim cmdc As New SqlCommand()
                    cmdc.Connection = con
                    str2 = "insert into dbo.Lead_soc_issuecentre(District_code,block_code,LeadSoc_Code,issueCentre_code) values('" & dcode & "','" & bcode & "','" & ldcode & "','" & itm_code & "')"
                    cmdc.CommandText = str2
                    con.Open()
                    cmdc.ExecuteNonQuery()
                    con.Close()
                End If
                c = c - 1
            End While
            Button1.Enabled = False
            update.Enabled = False
            blockname.SelectedIndex = 0
            Label1.Text = "Data Saved Successfully..."
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>")
        Else
            Label1.Text = "Please select Lead Society ... "
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Please select Lead Society ...');</script>")
        End If
    End Sub
    Protected Sub lead_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lead.SelectedIndexChanged
        Label1.Text = ""
        leadname.Text = ""
        leadname_h.Text = ""
        leadadd.Text = ""
        leadtele.Text = ""
        leadmob.Text = ""
        leadname.Enabled = True
        leadname_h.Enabled = True
        Button1.Visible = True
        update.Visible = False
        Button1.Enabled = True
        update.Enabled = True
        If lead.SelectedItem.Text = "Select" Then
            fpslist.Items.Clear()
            lst_issue.Items.Clear()
            leadname.Enabled = False
            leadname_h.Enabled = False
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "asdsad", "<script language=javascript > alert('Please select Lead Society ...');</script>")
        ElseIf lead.SelectedItem.Text = "New Lead" Then
            lst_issue.Items.Clear()
            fpslist.Items.Clear()
            Dim distcode As String = dist
            Dim blockcode As String = blockname.SelectedItem.Value
            Dim cmds As New SqlCommand()
            cmds.Connection = con
            Dim dr1 As SqlDataReader
            cmds.CommandText = "select * from pds.fps_master where district_code='" & distcode & "' and block_code='" & blockcode & "' order by fps_name"
            con.Open()
            dr1 = cmds.ExecuteReader()
            While dr1.Read()
                Dim lstitem As New ListItem
                lstitem.Text = dr1("fps_code").ToString() + " - " + dr1("fps_name").ToString()
                lstitem.Value = dr1("fps_code").ToString()
                fpsname.Items.Add(lstitem)
                fpslist.Items.Add(lstitem)
            End While
            fpsname.Items.Insert("0", "Select")
            dr1.Close()
            con.Close()
            Button1.Visible = True
            update.Visible = False
        Else
            fpslist.Items.Clear()
            Dim ldcode As String = lead.SelectedItem.Value
            Dim distcode As String = dist, issuecode As String = ""
            issuecode = issuename.SelectedItem.Value
            ldcode = lead.SelectedItem.Value
            Dim cmdd As New SqlCommand()
            cmdd.Connection = con
            Dim drd As SqlDataReader
            cmdd.CommandText = "select * from dbo.m_LeadSoc where LeadSoc_Code='" & ldcode & "' and District_code='" & distcode & "' and block_code='" & blockname.SelectedItem.Value & "' "
            con.Open()
            drd = cmdd.ExecuteReader()
            While drd.Read()
                leadname.Text = drd("LeadSoc_name").ToString()
                leadname_h.Text = drd("LeadSoc_nameU")
                leadadd.Text = drd("LeadSoc_address").ToString()
                leadtele.Text = drd("LeadSoc_tel").ToString()
                leadmob.Text = drd("LeadSoc_mob").ToString()
                Button1.Visible = False
                update.Visible = True
            End While
            drd.Close()
            con.Close()
            'leadname_h.Text = lead.SelectedItem.Text
            'For Adding fps name to the fps List from the multiple fps table 
            Dim fps_code As String = ""
            Dim cmds As New SqlCommand
            cmds.Connection = con
            Dim dr1 As SqlDataReader
            fpslist.Items.Clear()

            cmds.CommandText = "select fps_master.fps_code,fps_master.fps_name from dbo.Lead_soc_fps inner join pds.fps_master_Newon Lead_soc_fps.fps_code=fps_master.fps_code where Lead_soc_fps.LeadSoc_Code='" & ldcode & "' and Lead_soc_fps.District_code='" & distcode & "' and Lead_soc_fps.block_code='" & blockname.SelectedItem.Value & "'"
            con.Open()
            dr1 = cmds.ExecuteReader()

            While dr1.Read()
                fps_code = dr1("fps_code").ToString()
                Dim lstitem As New ListItem
                lstitem.Text = fps_code + " - " + dr1("fps_Uname").ToString()
                lstitem.Value = fps_code
                fpslist.Items.Add(lstitem)
            End While
            dr1.Close()
            con.Close()
            'For Adding IssueCentre name to the IssueCentre List from the multiple IssueCentre table 
            lst_issue.Items.Clear()
            Dim cmdi As New SqlCommand()
            cmdi.Connection = con
            Dim dri As SqlDataReader
            cmdi.CommandText = "SELECT tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.DepotID  FROM   dbo.Lead_soc_issuecentre INNER JOIN dbo.tbl_MetaData_DEPOT ON '23'+Lead_soc_issuecentre.District_code = tbl_MetaData_DEPOT.DistrictId AND Lead_soc_issuecentre.issueCentre_code = tbl_MetaData_DEPOT.DepotID where LeadSoc_Code='" & ldcode & "' and District_code='" & distcode & "' and block_code='" & blockname.SelectedItem.Value & "'"
            con.Open()
            dri = cmdi.ExecuteReader()
            While dri.Read()
                Dim lstitm As New ListItem
                lstitm.Text = dri("DepotName").ToString()
                lstitm.Value = dri("DepotID").ToString()
                lst_issue.Items.Add(lstitm)
            End While
            dri.Close()
            con.Close()
        End If
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("~/District/FrmLogOut.aspx")
    End Sub
    Protected Function chkSha(ByVal St As String) As String
        Dim St2 As String
        If St.IndexOf("'") > 0 Then
            St2 = St.Replace("'", Microsoft.VisualBasic.Chr(147))
            St = St2
        End If
        If St.IndexOf(Chr(39)) > 0 Then
            'St.Replace(Chr(39), Chr(147))
            St2 = St.Replace(Chr(39), Microsoft.VisualBasic.Chr(145))
        Else
            St2 = St
        End If
        Return St2
    End Function
    Protected Sub removeIC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles removeIC.Click
        Label1.Text = ""
        Dim c As Integer = lst_issue.SelectedIndex
        Dim distcode As String = dist
        Dim Str As String = ""
        If c <> -1 Then
            Dim ldcode As String = lead.SelectedItem.Value
            If lead.SelectedItem.Text <> "Select" And lead.SelectedItem.Text <> "New Lead" Then
                Str = lst_issue.Items(c).Value
                Dim cmd As New SqlCommand
                cmd.Connection = con
                cmd.CommandText = "delete from dbo.Lead_soc_issuecentre where issueCentre_code='" & Str.ToString() & "' and LeadSoc_Code='" & ldcode & "' and District_code='" & distcode & "' and block_code='" & blockname.SelectedItem.Value & "'"
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End If
            Label1.Text = "IssueCentre: " + lst_issue.Items(c).Text + " Removed Successfully ..."
            lst_issue.Items.RemoveAt(c)
        End If
    End Sub
End Class
