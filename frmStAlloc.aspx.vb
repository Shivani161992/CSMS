Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Data.chksql
Imports Data
Imports DataAccess

Partial Class _frmStAlloc
    Inherits System.Web.UI.Page
    Public con As New SqlConnection(ConfigurationManager.ConnectionStrings("constr_opdms").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                If Session("st_id") = "" Then
                    Response.Redirect("~/MainLogin.aspx")

                End If

            Catch ex As Exception

            End Try

            dd_year.Items.Add((Date.Today.Year) - 1)
            dd_year.Items.Add(Date.Today.Year)
            dd_year.Items.Add((Date.Today.Year) + 1)
            'dd_year.Items.Insert(0, (Date.Today.Year) - 1)
            dd_year.SelectedIndex = 1
            dd_month.SelectedIndex = Date.Today.Month - 1
            Session("year") = dd_year.SelectedValue
            Session("month") = dd_month.SelectedValue
            gvDistrict.Columns(2).Visible = True
            Dim sqldtr As SqlDataReader
            Dim sqlcmd As New SqlCommand
            sqlcmd = New SqlCommand("select * from pds.districtsmp where district_code <> '99' order by division_code", con)
            con.Open()
            sqldtr = sqlcmd.ExecuteReader()

            If sqldtr.HasRows Then
                gvDistrict.DataSource = sqldtr
                gvDistrict.DataBind()
            End If
            sqldtr.Close()
            con.Close()
            gvDistrict.Columns(2).Visible = False
        End If
        yrmth.Text = dd_month.SelectedItem.Text + " - " + dd_year.SelectedItem.Text
        tx_rice_apl.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_rice_bpl.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_rice_aay.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_wht_apl.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_wht_bpl.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_wht_aay.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_sugar.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_kerosene.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_salt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
        tx_rice_apl.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_rice_bpl.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_rice_aay.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_wht_apl.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_wht_bpl.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_wht_aay.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_sugar.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_kerosene.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_salt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
        tx_rice_apl.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_rice_bpl.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_rice_aay.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_wht_apl.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_wht_bpl.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_wht_aay.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_sugar.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_kerosene.Attributes.Add("onchange", "return chksqltxt(this);")
        tx_salt.Attributes.Add("onchange", "return chksqltxt(this);")
        Dim ctrllist As New ArrayList
        ctrllist.Add(tx_rice_apl.Text)
        ctrllist.Add(tx_rice_bpl.Text)
        ctrllist.Add(tx_rice_aay.Text)
        ctrllist.Add(tx_wht_apl.Text)
        ctrllist.Add(tx_wht_bpl.Text)
        ctrllist.Add(tx_wht_aay.Text)
        ctrllist.Add(tx_sugar.Text)
        ctrllist.Add(tx_kerosene.Text)
        ctrllist.Add(tx_salt.Text)
        Dim class_chk As New Data.chksql

        If class_chk Is Nothing Then
        Else
            Dim chkstr As Boolean = class_chk.chksql_server(ctrllist)
            If chkstr = True Then
                Page.Server.Transfer(HttpContext.Current.Request.Path)
            End If
        End If
        txt_rightalign()
    End Sub

    Protected Sub gvDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDistrict.SelectedIndexChanged

        save.Visible = True
        update.Visible = False

        tx_rice_apl.Text = ""
        tx_rice_bpl.Text = ""
        tx_rice_aay.Text = ""
        tx_wht_apl.Text = ""
        tx_wht_bpl.Text = ""
        tx_wht_aay.Text = ""
        tx_sugar.Text = ""
        tx_kerosene.Text = ""
        tx_salt.Text = ""

        msg.Text = ""
        Dim i As Integer = 0
        For i = 0 To gvDistrict.Rows.Count - 1 Step 1
            gvDistrict.Rows(i).BackColor = Drawing.Color.White
            'gvDistrict.Rows(i).Cells(1).Font.Name = "DVBW-TTYogeshEN"
            'gvDistrict.Rows(i).Cells(1).Font.Size = "Medium"
        Next

        Dim strdistcd As String
        strdistcd = gvDistrict.SelectedRow.Cells(1).Text
        gvDistrict.SelectedRow.BackColor = Drawing.Color.Orange
        lbl_dist.Text = strdistcd

        Dim dist_code As String = ""
        Dim yr, mth As Integer
        dist_code = gvDistrict.SelectedRow.Cells(2).Text
        yr = CType(dd_year.SelectedItem.Text, Integer)
        mth = CType(dd_month.SelectedItem.Value, Integer)

        Dim sqldtr As SqlDataReader
        Dim sqlcmd As New SqlCommand
        sqlcmd = New SqlCommand("select * from pds.state_alloc where district_code='" & dist_code & "' and year=" & yr & " and month=" & mth & " ", con)
        con.Open()
        sqldtr = sqlcmd.ExecuteReader()

        While sqldtr.Read
            tx_rice_apl.Text = sqldtr("rice_apl_alloc").ToString()
            tx_rice_bpl.Text = sqldtr("rice_bpl_alloc").ToString()
            tx_rice_aay.Text = sqldtr("rice_aay_alloc").ToString()
            tx_wht_apl.Text = sqldtr("wheat_apl_alloc").ToString()
            tx_wht_bpl.Text = sqldtr("wheat_bpl_alloc").ToString()
            tx_wht_aay.Text = sqldtr("wheat_aay_alloc").ToString()
            tx_sugar.Text = sqldtr("sugar_alloc").ToString()
            tx_kerosene.Text = sqldtr("kerosene_alloc").ToString()
            tx_salt.Text = sqldtr("salt_alloc").ToString()

            save.Visible = False
            update.Visible = True

        End While
        sqldtr.Close()
        con.Close()

        
    End Sub

    Protected Sub save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles save.Click
        If gvDistrict.SelectedIndex <> -1 Then
            msg.Text = ""
            Dim dist_code As String = ""
            Dim yr, mth As Integer
            dist_code = gvDistrict.SelectedRow.Cells(2).Text
            yr = CType(dd_year.SelectedItem.Text, Integer)
            mth = CType(dd_month.SelectedItem.Value, Integer)

            If tx_rice_apl.Text = "" Then
                tx_rice_apl.Text = 0.0
            End If

            If tx_rice_bpl.Text = "" Then
                tx_rice_bpl.Text = 0.0
            End If

            If tx_rice_aay.Text = "" Then
                tx_rice_aay.Text = 0.0
            End If

            If tx_wht_apl.Text = "" Then
                tx_wht_apl.Text = 0.0
            End If

            If tx_wht_bpl.Text = "" Then
                tx_wht_bpl.Text = 0.0
            End If

            If tx_wht_aay.Text = "" Then
                tx_wht_aay.Text = 0.0
            End If

            If tx_sugar.Text = "" Then
                tx_sugar.Text = 0.0
            End If

            If tx_kerosene.Text = "" Then
                tx_kerosene.Text = 0.0
            End If

            If tx_salt.Text = "" Then
                tx_salt.Text = 0.0
            End If
            Dim cmdc As New SqlCommand()
            Dim str2 As String = ""
            cmdc.Connection = con
            str2 = "insert into pds.state_alloc (district_code,year,month,Ip_address,rice_apl_alloc,rice_bpl_alloc,rice_aay_alloc,wheat_apl_alloc,wheat_bpl_alloc,wheat_aay_alloc,sugar_alloc,kerosene_alloc,salt_alloc,date) values('" & dist_code & "'," & yr & "," & mth & ",'" & Me.Request.UserHostAddress & "'," & tx_rice_apl.Text & "," & tx_rice_bpl.Text & "," & tx_rice_aay.Text & "," & tx_wht_apl.Text & "," & tx_wht_bpl.Text & "," & tx_wht_aay.Text & "," & tx_sugar.Text & "," & tx_kerosene.Text & "," & tx_salt.Text & ",getdate())"
            cmdc.CommandText = str2
            con.Open()
            cmdc.ExecuteNonQuery()
            con.Close()

            msg.Text = "Allocation successfully ..."
            save.Visible = False
            update.Visible = True
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "mymsg2", "<script language=javascript> alert('Please select district ...'); </script> ")
            msg.Text = "Please select district ..."
        End If
    End Sub

    Protected Sub update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update.Click
        If gvDistrict.SelectedIndex <> -1 Then
            msg.Text = ""
            Dim dist_code As String = ""
            Dim yr, mth As Integer
            dist_code = gvDistrict.SelectedRow.Cells(2).Text
            yr = CType(dd_year.SelectedItem.Text, Integer)
            mth = CType(dd_month.SelectedItem.Value, Integer)

            If tx_rice_apl.Text = "" Then
                tx_rice_apl.Text = 0.0
            End If

            If tx_rice_bpl.Text = "" Then
                tx_rice_bpl.Text = 0.0
            End If

            If tx_rice_aay.Text = "" Then
                tx_rice_aay.Text = 0.0
            End If

            If tx_wht_apl.Text = "" Then
                tx_wht_apl.Text = 0.0
            End If

            If tx_wht_bpl.Text = "" Then
                tx_wht_bpl.Text = 0.0
            End If

            If tx_wht_aay.Text = "" Then
                tx_wht_aay.Text = 0.0
            End If

            If tx_sugar.Text = "" Then
                tx_sugar.Text = 0.0
            End If

            If tx_kerosene.Text = "" Then
                tx_kerosene.Text = 0.0
            End If

            If tx_salt.Text = "" Then
                tx_salt.Text = 0.0
            End If
            Dim cmdc As New SqlCommand()
            Dim str2 As String = ""
            cmdc.Connection = con
            str2 = "update pds.state_alloc set Ip_address='" & Me.Request.UserHostAddress & "',rice_apl_alloc=" & tx_rice_apl.Text & ",rice_bpl_alloc=" & tx_rice_bpl.Text & ",rice_aay_alloc=" & tx_rice_aay.Text & ",wheat_apl_alloc=" & tx_wht_apl.Text & ",wheat_bpl_alloc=" & tx_wht_bpl.Text & ",wheat_aay_alloc=" & tx_wht_aay.Text & ",sugar_alloc=" & tx_sugar.Text & ",kerosene_alloc=" & tx_kerosene.Text & ",salt_alloc=" & tx_salt.Text & ",updated_date=getdate() where district_code='" & dist_code & "' and year=" & yr & " and month=" & mth & ""
            cmdc.CommandText = str2
            con.Open()
            cmdc.ExecuteNonQuery()
            con.Close()

            
            msg.Text = "Update allocation successfully ..."
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "mymsg2", "<script language=javascript> alert('Please select district ...'); </script> ")
            msg.Text = "Please select district ..."
        End If
    End Sub
    Private Sub txt_rightalign()
        tx_rice_apl.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_rice_bpl.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_rice_aay.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_wht_apl.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_wht_bpl.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_wht_aay.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_sugar.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_kerosene.Style("TEXT-ALIGN") = TextAlign.Right.ToString
        tx_salt.Style("TEXT-ALIGN") = TextAlign.Right.ToString
    End Sub

    Protected Sub dd_year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_year.SelectedIndexChanged
        Session("year") = dd_year.SelectedValue
    End Sub

    Protected Sub dd_month_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_month.SelectedIndexChanged
        Session("month") = dd_month.SelectedValue
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect(Page.Request.UrlReferrer.ToString)
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Session.Abandon()
        Page.Response.Redirect("default.aspx")
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        Session("year") = dd_year.SelectedValue
        Session("month") = dd_month.SelectedValue
        Response.Redirect("~/frmPrnDistAlloc.aspx")
    End Sub
End Class
