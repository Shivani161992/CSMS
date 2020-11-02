Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports Data.chksql
Imports Data
Imports DataAccess

Partial Class frmStAlloc1
    Inherits System.Web.UI.Page
    Public con As New SqlConnection(ConfigurationManager.ConnectionStrings("constr_opdms").ToString())
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("st_id") = "" Then
                Response.Redirect("~/MainLogin.aspx")
            End If
        Catch ex As Exception

        End Try
        If Page.IsPostBack = False Then
            dd_year.Items.Add((Date.Today.Year) - 1)
            dd_year.Items.Add(Date.Today.Year)
            dd_year.Items.Add((Date.Today.Year) + 1)
            'dd_year.Items.Insert(0, (Date.Today.Year) - 1)
            dd_year.SelectedIndex = 1
            dd_month.SelectedIndex = Date.Today.Month - 1
            Session("year") = dd_year.SelectedValue
            Session("month") = dd_month.SelectedValue
        End If
        Dim ctrllist As New ArrayList
        Dim i As Integer = 0
        For i = 0 To gvDistrict.Rows.Count - 1 Step 1
            Dim qtytxt As TextBox = DirectCast(gvDistrict.Rows(i).Cells(3).FindControl("TextBox1"), TextBox)
            ctrllist.Add(qtytxt.Text)
        Next
        Dim class_chk As New Data.chksql
        If class_chk Is Nothing Then
        Else
            Dim chkstr As Boolean = class_chk.chksql_server(ctrllist)
            If chkstr = True Then
                Page.Server.Transfer(HttpContext.Current.Request.Path)
            End If
        End If
    End Sub
    Protected Sub save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles save.Click
        msg.Text = ""
        If gvDistrict.Rows.Count <= 0 And ddl_commodity.SelectedItem.Text = "Select" Then
            Page.RegisterClientScriptBlock("asdsad", " <script language=javascript > alert('Please Select Commodity ...');</script>")
            msg.Text = "Please Select Commodity ..."
        Else
            Dim yr, mth As Integer
            Dim commodity As String = ddl_commodity.SelectedItem.Value + "_alloc"
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
            yr = CType(dd_year.SelectedItem.Text, Integer)
            mth = CType(dd_month.SelectedItem.Value, Integer)
            Dim cmdc As New SqlCommand()
            Dim str2 As String = ""
            cmdc.Connection = con
            con.Open()
            Dim i As Integer = 0
            Try
                For i = 0 To gvDistrict.Rows.Count - 1 Step 1
                    Dim qtytxt As TextBox = DirectCast(gvDistrict.Rows(i).Cells(3).FindControl("TextBox1"), TextBox)
                    If qtytxt.Text = "" Then
                        qtytxt.Text = "0"
                    End If
                    'for enabling save/update 
                    Dim comm As Integer = 1
                    Dim sqlcmd As New SqlCommand
                    sqlcmd = New SqlCommand("select count(*) from pds.state_alloc where pds.state_alloc.month=" & mth & " and pds.state_alloc.year=" & yr & " and district_code='" & gvDistrict.Rows(i).Cells(2).Text & "'", con)
                    comm = sqlcmd.ExecuteScalar()
                    If comm <= 0 Then
                        str2 = "insert into pds.state_alloc(district_code,year,month,Ip_address," & commodity & ",date) values('" & gvDistrict.Rows(i).Cells(2).Text & "'," & yr & "," & mth & ",'" & ip & "'," & qtytxt.Text & ",getdate() )"
                    Else
                        str2 = "update pds.state_alloc set " & commodity & " =" & qtytxt.Text & " , updated_date=getdate() where pds.state_alloc.month=" & mth & " and pds.state_alloc.year=" & yr & " and district_code='" & gvDistrict.Rows(i).Cells(2).Text & "' "
                    End If
                    cmdc.CommandText = str2
                    cmdc.ExecuteNonQuery()
                Next
                msg.Text = "Allocation successfully ..."
                Page.RegisterClientScriptBlock("asdsad", " <script language=javascript > alert('Data Saved Successfully...');</script>")
                save.Enabled = False
            Catch ex As Exception
                msg.Text = ex.Message
            Finally
                con.Close()
            End Try
        End If
    End Sub
    Protected Sub dd_year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_year.SelectedIndexChanged
        Session("year") = dd_year.SelectedValue
    End Sub
    Protected Sub dd_month_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_month.SelectedIndexChanged
        ddl_commodity.SelectedIndex = 0
        gvDistrict.Dispose()
        gvDistrict.DataBind()
    End Sub
    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Session.Abandon()
        Page.Response.Redirect("default.aspx")
    End Sub
    Protected Sub gvDistrict_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDistrict.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim qtytxt As TextBox
            qtytxt = DirectCast(e.Row.Cells(1).FindControl("TextBox1"), TextBox)
            qtytxt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);")
            qtytxt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);")
            qtytxt.Attributes.Add("onchange", "return chksqltxt(this);")
            qtytxt.Style("TEXT-ALIGN") = TextAlign.Right.ToString()
        End If
    End Sub
    Protected Sub ddl_commodity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_commodity.SelectedIndexChanged
        save.Enabled = True
        If ddl_commodity.SelectedItem.Text = "Select" Then
            Page.RegisterClientScriptBlock("asdsad", " <script language=javascript > alert('Please Select Commodity ...');</script>")
        Else
            msg.Text = ""
            Dim dist_code As String = ""
            Dim qty As String = ""
            Dim commodity As String = ddl_commodity.SelectedItem.Value + "_alloc"
            Dim yr, mth As Integer
            yr = CType(dd_year.SelectedItem.Text, Integer)
            mth = CType(dd_month.SelectedItem.Value, Integer)
            Dim str As String = "SELECT  Left(pds.division.Division,Len(pds.division.Division)-9) as division,pds.districtsmp.division_code,pds.districtsmp.district_code, pds.districtsmp.district_name, pds.districtsmp.Dist_name, pds.state_alloc." + commodity + " as qty FROM   pds.Division LEFT  JOIN  pds.districtsmp  ON pds.division.division_code = pds.districtsmp.division_code  LEFT  JOIN pds.state_alloc ON pds.districtsmp.district_code = pds.state_alloc.district_code and pds.state_alloc.month=" & mth & " and pds.state_alloc.year=" & yr & " where pds.districtsmp.district_code <> '99' order by pds.districtsmp.division_code"
            Dim da As New SqlDataAdapter(str, con)
            Dim ds As New DataSet()
            da.Fill(ds, "xyz")
            gvDistrict.DataSource = ds.Tables("xyz")
            gvDistrict.DataBind()
            'For dupplicate division
            Dim dt As New DataTable()
            da.Fill(dt)
            Dim i As Integer = 0
            Dim basediv As String = gvDistrict.Rows(0).Cells(0).Text
            For i = 1 To gvDistrict.Rows.Count - 1 Step 1
                Dim div1 As String = gvDistrict.Rows(i).Cells(0).Text
                If basediv = div1 Then
                    dt.Rows(i)(0) = ""
                Else
                    basediv = div1
                End If
            Next
            gvDistrict.DataSource = dt
            gvDistrict.DataBind()
        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        Session("year") = dd_year.SelectedValue
        Session("month") = dd_month.SelectedValue
        Response.Redirect("~/frmPrnDistAlloc.aspx")
    End Sub
End Class
