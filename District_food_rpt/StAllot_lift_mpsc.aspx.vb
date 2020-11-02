Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Net
Imports Microsoft.Reporting.WebForms
Imports Microsoft.VisualBasic
Imports System.Security.Principal
Imports System.Data.SqlClient
Imports System.Exception
Partial Class StAllot_lift_mpsc
    Inherits System.Web.UI.Page
    Public con As New SqlConnection(ConfigurationManager.ConnectionStrings("constr").ToString())
    Dim cmd As New SqlCommand()
    Dim dr As SqlDataReader
    Public dist_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                If Session("dist_id") = "" Then
                    Response.Redirect("~/MainLogin.aspx")
                End If
            Catch ex As Exception
            End Try
            ddl_year.Items.Add(DateTime.Today.Year - 1)
            ddl_year.Items.Add(DateTime.Today.Year)
            ddl_year.Items.Add(DateTime.Today.Year + 1)
            ddl_year.SelectedIndex = 1
            ddl_month.SelectedValue = DateTime.Today.Month
        End If
        dist_code = Session("dist_id").ToString()
        get_distname()
    End Sub
    Public NotInheritable Class ReportServerCredentials
        Implements IReportServerCredentials

#Region " Objects "

#End Region
#Region " Methods "
        Public Function GetFormsCredentials(ByRef authCookie As System.Net.Cookie, _
                                            ByRef userName As String, _
                                            ByRef password As String, _
                                            ByRef authority As String) _
                                            As Boolean _
                Implements IReportServerCredentials.GetFormsCredentials

            authCookie = Nothing
            userName = Nothing
            password = Nothing
            authority = Nothing

            'Not using form credentials
            Return False

        End Function
#End Region
#Region " Properties "
        Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
     Implements IReportServerCredentials.ImpersonationUser
            Get
                'Use the default windows user.  Credentials will be provided 
                'by the NetworkCredentials property.
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property NetworkCredentials() As _
     Net.ICredentials Implements IReportServerCredentials.NetworkCredentials
            Get

                'User name, Password & Domain


                'Dim domain As String = "demo.mp.nic.in"
                Dim userName As String = ConfigurationManager.ConnectionStrings("uname").ProviderName
                Dim password As String = ConfigurationManager.ConnectionStrings("psw").ProviderName
                Dim domain As String = ConfigurationManager.ConnectionStrings("domain").ProviderName
                Return New Net.NetworkCredential(userName, password, domain)

            End Get
        End Property
#End Region

    End Class
    Public NotInheritable Class MyReportServerCredentials
        Implements IReportServerCredentials
        Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
                Implements IReportServerCredentials.ImpersonationUser
            Get

                'Use the default windows user.  Credentials will be
                'provided by the NetworkCredentials property.
                Return Nothing

            End Get
        End Property
        Public ReadOnly Property NetworkCredentials() As ICredentials _
                Implements IReportServerCredentials.NetworkCredentials
            Get

                'Read the user information from the web.config file.  
                'By reading the information on demand instead of storing 
                'it, the credentials will not be stored in session, 
                'reducing the vulnerable surface area to the web.config 
                'file, which can be secured with an ACL.

                'User name

                Dim userName As String = ConfigurationManager.ConnectionStrings("uname").ProviderName
                'ConfigurationManager.AppSettings("MyReportViewerUser")

                If (String.IsNullOrEmpty(userName)) Then
                    Throw New Exception("Missing user name from web.config file")
                End If

                'Password

                Dim password As String = ConfigurationManager.ConnectionStrings("psw").ProviderName
                'ConfigurationManager.AppSettings("MyReportViewerPassword")

                If (String.IsNullOrEmpty(password)) Then
                    Throw New Exception("Missing password from web.config file")
                End If

                'Domain
                'Dim domain As String = "demo.mp.nic.in"
                Dim domain As String = ConfigurationManager.ConnectionStrings("domain").ProviderName
                'ConfigurationManager.AppSettings("MyReportViewerDomain")

                If (String.IsNullOrEmpty(domain)) Then
                    Throw New Exception("Missing domain from web.config file")
                End If

                Return New NetworkCredential(userName, password, domain)

            End Get
        End Property

        Public Function GetFormsCredentials(ByRef authCookie As Cookie, _
                                            ByRef userName As String, _
                                            ByRef password As String, _
                                            ByRef authority As String) _
                                            As Boolean _
                Implements IReportServerCredentials.GetFormsCredentials

            authCookie = Nothing
            userName = Nothing
            password = Nothing
            authority = Nothing

            'Not using form credentials
            Return False

        End Function

    End Class
    Private Sub get_Data()
        Dim distcode As String = "", smonth As String = "", syear As String = "", smonth1 As String = "", syear1 As String = ""
        distcode = dist_code
        smonth = ddl_month.SelectedItem.Value
        syear = ddl_year.SelectedItem.Text
        smonth1 = smonth + 1
        syear1 = ddl_year.SelectedItem.Text
        If (smonth = 12) Then
            smonth1 = 1
            syear1 = syear + 1
        End If
        Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName
        Dim reportURL As String = ""
        'reportURL = "http://staging.mp.nic.in/ReportServer"
        reportURL = ConfigurationManager.ConnectionStrings("rpturl").ProviderName
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New Uri(reportURL)
        'ReportViewer1.ServerReport.ReportPath = "/report/SendSmsReport"
        If ddl_commodity.SelectedItem.Value = "rice" Then
            ReportViewer1.ServerReport.ReportPath = folder & "allot_lift_mpsc_rice"
        End If
        If ddl_commodity.SelectedItem.Value = "wheat" Then
            ReportViewer1.ServerReport.ReportPath = folder & "allot_lift_mpsc_wheat"
        End If
        If ddl_commodity.SelectedItem.Value = "sks" Then
            ReportViewer1.ServerReport.ReportPath = folder & "allot_lift_mpsc_sks"
        End If
        ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials
        Dim pInfo As ReportParameterInfoCollection
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim a1 As New ReportParameter
        paramList.Add(New ReportParameter("distCode", distcode, False))
        paramList.Add(New ReportParameter("selMonth", smonth, False))
        paramList.Add(New ReportParameter("selYear", syear, False))
        paramList.Add(New ReportParameter("selMonth1", smonth1, False))
        paramList.Add(New ReportParameter("selYear1", syear1, False))
        'paramList.Add(New ReportParameter("pdistcd", Session("distcd").ToString, False))
        ReportViewer1.ServerReport.SetParameters(paramList)
        pInfo = ReportViewer1.ServerReport.GetParameters()
        ReportViewer1.ServerReport.Refresh()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ddl_commodity.SelectedItem.Text = "Select" Then
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity');</script>")
        Else
            get_Data()
        End If
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("~/District/frmReports.aspx")
    End Sub
    Protected Sub get_distname()
        cmd.CommandText = "select * from pds.districtsmp where district_code='" & dist_code & "'"
        cmd.Connection = con
        con.Open()
        dr = cmd.ExecuteReader()
        While (dr.Read())
            lbl_dist.Text = dr("district_name").ToString()
        End While
        dr.Close()
        con.Close()
    End Sub
End Class
