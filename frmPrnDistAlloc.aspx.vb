Imports System.Net
Imports Microsoft.Reporting.WebForms
Imports Microsoft.VisualBasic
Imports System.Security.Principal
Partial Class frmPrnDistAlloc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("st_id") = "" Then
                Response.Redirect("~/Session_Expire_Dist.aspx")

            End If
        Catch ex As Exception

        End Try
        If Not IsPostBack Then

            ' lbl_district.Text = Session("distname")
            PopCmb()

        End If

    End Sub
    Private Sub PopCmb()
        DDL_month.Items.Add("January")
        DDL_month.Items.Add("February")
        DDL_month.Items.Add("March")
        DDL_month.Items.Add("April")
        DDL_month.Items.Add("May")
        DDL_month.Items.Add("June")
        DDL_month.Items.Add("July")
        DDL_month.Items.Add("August")
        DDL_month.Items.Add("September")
        DDL_month.Items.Add("October")
        DDL_month.Items.Add("November")
        DDL_month.Items.Add("December")

        'Dim i As Integer
        'For i = 0 To 11 Step 1
        '    DDL_month.Items(i).Value = i + 1
        'Next


        DDL_Year.Items.Clear()
        DDL_Year.Items.Add(Year(Now) - 1)
        DDL_Year.Items.Add(Year(Now))
        DDL_Year.Items.Add(Year(Now) + 1)
        'If Month(Now) < 2 Then
        '    If Month(Now) = 1 Then
        '        DDL_month.SelectedIndex = 11
        '    Else
        '        If Month(Now) = 1 Then
        '            DDL_month.SelectedIndex = 12
        '        End If
        '    End If

        'Else
         '    DDL_Year.SelectedIndex = 1
        'End If
        'DDL_Year.SelectedValue = Session("year")
        'DDL_month.SelectedValue = Session("month")
        If Val(Session("year").ToString) > 1 Then
            DDL_Year.SelectedValue = Session("year")
            DDL_month.SelectedIndex = Session("month") - 1
        Else
            Session("year") = DDL_Year.SelectedValue
            Session("month") = DDL_month.SelectedValue
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
                Dim userName As String = "pdsallot"
                Dim password As String = "D#@mP-w08"
                Dim domain As String = "demo.mp.nic.in"

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

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim reportURL As String
        Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName
        reportURL = ConfigurationManager.ConnectionStrings("rpturl").ProviderName
        'reportURL = "http://10.131.2.195/ReportServer"
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New Uri(reportURL)

        ReportViewer1.ServerReport.ReportPath = "/PdsAllot-Reports/dist_alloc"

        ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials
        Dim pInfo As ReportParameterInfoCollection
        Dim paramList As New Generic.List(Of ReportParameter)
        Dim a1 As New ReportParameter
        paramList.Add(New ReportParameter("month", (DDL_month.SelectedIndex + 1).ToString, False))
        paramList.Add(New ReportParameter("year", DDL_Year.SelectedValue.ToString, False))

        '        paramList.Add(New ReportParameter("pdistcd", Session("distcd").ToString, False))
        ReportViewer1.ServerReport.SetParameters(paramList)

        pInfo = ReportViewer1.ServerReport.GetParameters()

        ReportViewer1.ServerReport.Refresh()
    End Sub
    Protected Sub DDL_Year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_Year.SelectedIndexChanged
        Session("year") = DDL_Year.SelectedValue
    End Sub

    Protected Sub DDL_month_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_month.SelectedIndexChanged
        Session("month") = DDL_month.SelectedValue
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Session.Abandon()
        Page.Response.Redirect("~/MainLogin.aspx")
    End Sub

    
End Class
