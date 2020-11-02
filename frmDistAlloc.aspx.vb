Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls
Partial Class frmDistAlloc
    Inherits System.Web.UI.Page
    Dim sql_dtr As SqlDataReader
    Dim sql_str As String
    Dim sql_cmd As New SqlClient.SqlCommand
    Dim max_gcd As Object
    Dim sqlconn As SqlConnection
    Dim conn As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        labelPage()
    End Sub
    Protected Sub labelPage()
        conn = System.Configuration.ConfigurationManager.ConnectionStrings("constr_opdms").ToString()
        sqlconn = New SqlConnection(conn)
        Dim S1 As String = ""
        ' Dim form1 As New HtmlForm()
        '  form1.ID = "Form1"
        ' form1.Method = "post"
        Response.Write("<table border='1' width='100%'>")
        Try
            sql_cmd = New SqlCommand("Select * from pds.districtsmp", sqlconn)
            sqlconn.Open()
            sql_dtr = sql_cmd.ExecuteReader()
            If sql_dtr.HasRows Then
                While sql_dtr.Read()
                    S1 = "lbl" & sql_dtr("district_code")
                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Dim ss As New Label()
                    Dim tt As New TextBox()
                    ss.Text = sql_dtr("district_code")
                    form1.Controls.Add(ss)
                    Response.Write("</td>")
                    Response.Write("<td>")
                    Dim ssDist As New Label()
                    ssDist.Text = sql_dtr("district_name")
                    form1.Controls.Add(ssDist)
                    Response.Write("</td>")

                    Response.Write("<td>")
                    form1.Controls.Add(tt)
                    Response.Write("</td>")
                    Dim ttRice As New TextBox()
                    Response.Write("<td>")
                    form1.Controls.Add(ttRice)
                    Response.Write("</td>")
                    Dim ttSugar As New TextBox()
                    Response.Write("<td>")
                    form1.Controls.Add(ttSugar)
                    Response.Write("</td>")
                    Dim ttKer As New TextBox()
                    Response.Write("<td>")
                    form1.Controls.Add(ttKer)
                    Response.Write("</td>")

                    Dim ttSalt As New TextBox()
                    Response.Write("<td>")
                    form1.Controls.Add(ttSalt)
                    Response.Write("</td>")
                    Response.Write("</tr>")
                    Response.Write("<br>")


                End While

            End If
            Controls.Add(form1)
            Response.Write("</table>")
        Catch ex As Exception
            lblmsg.text = ex.Message
        Finally
            sqlconn.Close()
        End Try
    End Sub
End Class
