
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
Partial Class District_food_rpt_Rack_Reconciliation_rpt
    Inherits System.Web.UI.Page
    Public con As New SqlConnection(ConfigurationManager.ConnectionStrings("constr").ToString())
    'Public coni As New SqlConnection(ConfigurationManager.ConnectionStrings("constr").ToString())
    Dim cmd As New SqlCommand()
    Dim dr As SqlDataReader
    Public dist_code As String = ""
    'Public issue_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                If Session("dist_id") = "" Then
                    Response.Redirect("~/MainLogin.aspx")
                End If
            Catch ex As Exception
            End Try
            get_Rackno()
            get_distname()
            dist_code = Session("dist_id").ToString()
        End If

        'issue_code = Session("issue_id").ToString()







        'get_issuename()

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

    Private Sub get_Data(ByVal DistID As String, ByVal rackno As String)
        Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName


        ' For Report

        Dim reportURL As String = ""
        'reportURL = "http://staging.mp.nic.in/ReportServer"
        reportURL = ConfigurationManager.ConnectionStrings("rpturl").ProviderName
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New Uri(reportURL)

        'ReportViewer1.ServerReport.ReportPath = "/report/SendSmsReport"
        ReportViewer1.ServerReport.ReportPath = folder & "Rack_Reconsiliation_Rpt"
        ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials
        Dim pInfo As ReportParameterInfoCollection
        Dim paramList As New Generic.List(Of ReportParameter)
        'Dim a1 As New ReportParameter
        paramList.Add(New ReportParameter("DistID", DistID, False))
        paramList.Add(New ReportParameter("rackno", rackno, False))

        ' paramList.Add(New ReportParameter("year", Year.ToString, False))

        '        paramList.Add(New ReportParameter("pdistcd", Session("distcd").ToString, False))
        ReportViewer1.ServerReport.SetParameters(paramList)

        pInfo = ReportViewer1.ServerReport.GetParameters()

        ReportViewer1.ServerReport.Refresh()
    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DistID As String
        Dim rackno As String
        Dim dist_code As String = Session("dist_id").ToString()
        DistID = dist_code
        rackno = ddlrackno.SelectedValue


        get_Data(DistID, rackno)




    End Sub


    Protected Function getDate_MDY(ByVal inDate As String)

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
        Dim dtProjectStartDate As DateTime = Convert.ToDateTime(inDate)
        Return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"))


    End Function

    Protected Sub get_distname()
        Dim dist_code As String = Session("dist_id").ToString()
        cmd.CommandText = "select * from pds.districtsmp where district_code='" + dist_code + "'"
        cmd.Connection = con
        con.Open()
        dr = cmd.ExecuteReader()
        While (dr.Read())

            Label3.Text = dr("district_name").ToString()

        End While
        dr.Close()
        con.Close()
    End Sub
    Protected Sub get_Rackno()
        ddlrackno.Items.Clear()
        Dim dist_code As String = Session("dist_id").ToString()
        cmd.CommandText = "select Rack_No from dbo.tbl_RackMaster where district_code='" + dist_code + "'" '"
        cmd.Connection = con
        con.Open()
        dr = cmd.ExecuteReader()
        While (dr.Read())

            ddlrackno.Items.Add(dr("Rack_No"))

        End While
        dr.Close()
        con.Close()
    End Sub

    Protected Sub ddlrackno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlrackno.SelectedIndexChanged

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName
        'If ReportViewer1.ServerReport.ReportPath = folder & "No_of_do" Then
        '    ReportViewer1.PerformBack()
        'Else
        If (Session("Collector/DIO") = "Collector") Then
            Response.Redirect("~/District/frmReport_Coll_DIO.aspx")

        ElseIf (Session("Collector/DIO") = "DIO") Then
            Response.Redirect("~/District/frmReport_Coll_DIO.aspx")

        Else
            Response.Redirect("~/District/frmReports.aspx")

        End If

        'End If

    End Sub
End Class
