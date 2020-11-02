Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports Data
Imports DataAccess
Imports System.Data.SqlClient
Imports System
Imports System.Net
Imports Microsoft.Reporting.WebForms
Imports Microsoft.VisualBasic
Imports System.Security.Principal
Imports System.Exception


Partial Class Reports_State_AllUserLoginLogoutReport
    Inherits System.Web.UI.Page

    Dim mobj As MoveChallan
    Dim ComObj As Common
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                If Session("st_id") = "" Then
                    Response.Redirect("~/MainLogin.aspx")
                Else
                    ComObj = New Common(ConfigurationManager.ConnectionStrings("constr").ToString())
                    GetLogin()

                End If
            Catch ex As Exception
            End Try

            

        End If



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
                'Dim userName As String = "pdsallot"
                'Dim password As String = "D#@mP-w08"
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
                'Dim userName As String = "pdsallot"
                Dim userName As String = ConfigurationManager.ConnectionStrings("uname").ProviderName
                'ConfigurationManager.AppSettings("MyReportViewerUser")

                If (String.IsNullOrEmpty(userName)) Then
                    Throw New Exception("Missing user name from web.config file")
                End If

                'Password
                'Dim password As String = "D#@mP-w08"
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

    Private Sub get_Data(ByVal type As String)
        Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName


       
        Dim reportURL As String = ""
        'reportURL = "http://staging.mp.nic.in/ReportServer"
        reportURL = ConfigurationManager.ConnectionStrings("rpturl").ProviderName
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New Uri(reportURL)

        'ReportViewer1.ServerReport.ReportPath = "/report/SendSmsReport"
        ReportViewer1.ServerReport.ReportPath = folder & "AllUserLoginDetails"

        ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials
        Dim pInfo As ReportParameterInfoCollection
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim a1 As New ReportParameter
        paramList.Add(New ReportParameter("type", type, False))


        ' paramList.Add(New ReportParameter("year", Year.ToString, False))

        '        paramList.Add(New ReportParameter("pdistcd", Session("distcd").ToString, False))
        ReportViewer1.ServerReport.SetParameters(paramList)

        pInfo = ReportViewer1.ServerReport.GetParameters()

        ReportViewer1.ServerReport.Refresh()
      
    End Sub




   
    'Protected Sub get_issuename()
    '    Dim dist As String = "23" + dist_code
    '    cmd.CommandText = "select DepotName from dbo.tbl_MetaData_DEPOT where DistrictId='" + dist + "' and DepotID='" & issue_code & "'"
    '    cmd.Connection = con
    '    con.Open()
    '    dr = cmd.ExecuteReader()
    '    While (dr.Read())

    '        Label2.Text = dr("DepotName").ToString()

    '    End While
    '    dr.Close()
    '    con.Close()
    'End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName
        
        Response.Redirect("~/State/frmReports_State.aspx")



    End Sub

    Private Sub GetLogin()
        mobj = New MoveChallan(ComObj)
        Dim qry As String = "select * from dbo.tbl_MetaData_LoginType"
        Dim ds As DataSet = mobj.selectAny(qry)
        ddllogintype.DataSource = ds
        ddllogintype.DataTextField = "Login_Type"
        ddllogintype.DataValueField = "Login_ID"
        ddllogintype.DataBind()
        ddllogintype.Items.Insert(0, "--Select--")


    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        If ddllogintype.SelectedItem.Text = "--Select--" Then
            Page.RegisterClientScriptBlock("asdsad", " <script language=javascript > alert('Please Select the Login Type');</script>")
        Else
            get_Data(ddllogintype.SelectedValue)
        End If


    End Sub
End Class
