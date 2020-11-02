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
Partial Class Report_IssueCenter_Daily_Receipt_Register
    Inherits System.Web.UI.Page
    Public cons As New SqlConnection(ConfigurationManager.ConnectionStrings("connstorage").ConnectionString)

    Public con As New SqlConnection(ConfigurationManager.ConnectionStrings("constr").ToString())
    Public coni As New SqlConnection(ConfigurationManager.ConnectionStrings("constr").ToString())
    Dim cmd As New SqlCommand()
    Dim dr As SqlDataReader
    Public dist_code As String = ""
    Public IC_code As String = ""
    'Public issue_code As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                If Session("dist_id") = "" Then
                    

                    Response.Redirect("~/Default.aspx")
                End If
            Catch ex As Exception
            End Try
            get_Source()

            tx_from_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy")
            tx_to_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy")
        End If
        tx_from_date.Attributes.Add("onkeypress", "return CheckCalDate(this)")
        tx_to_date.Attributes.Add("onkeypress", "return CheckCalDate(this)")
        dist_code = Session("dist_id").ToString()
        IC_code = Session("issue_id").ToString()
        get_distname()
        get_ICname()


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

    Private Sub get_Data(ByVal From_Date As String, ByVal to_Date As String, ByVal Dist_ID As String, ByVal Depot_ID As String)
        If ddlsource.SelectedItem.Text = "Sugar Factory" Then
            Label1.Visible = True
            Label1.Text = "Please Select Other Source"
            ReportViewer1.Visible = False

        Else
            Label1.Visible = False
            ReportViewer1.Visible = True

            Dim folder As String = ConfigurationManager.ConnectionStrings("rptfolder").ProviderName

            ' For Report

            Dim reportURL As String = ""
            'reportURL = "http://staging.mp.nic.in/ReportServer"
            reportURL = ConfigurationManager.ConnectionStrings("rpturl").ProviderName
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
            ReportViewer1.ServerReport.ReportServerUrl = New Uri(reportURL)

            'ReportViewer1.ServerReport.ReportPath = "/report/SendSmsReport"
            If ddlsource.SelectedItem.Text = "From FCI" Then
                ReportViewer1.ServerReport.ReportPath = folder & "DailyReceiptRegistor"
            ElseIf ddlsource.SelectedItem.Text = "Other Depot" Then
                ReportViewer1.ServerReport.ReportPath = folder & "Daily_Receipt_OtherGdn"
            ElseIf ddlsource.SelectedItem.Text = "Procurement" Then
                ReportViewer1.ServerReport.ReportPath = folder & "DailyStock_Procurement"
            ElseIf ddlsource.SelectedItem.Text = "From RailHead" Then
                ReportViewer1.ServerReport.ReportPath = folder & "DailyReceipt_RailHead"
            ElseIf ddlsource.SelectedItem.Text = "Other Source" Then
                ReportViewer1.ServerReport.ReportPath = folder & "DailyReceipt_OtherSource"
            ElseIf ddlsource.SelectedItem.Text = "CMR" Then
                ReportViewer1.ServerReport.ReportPath = folder & "Daily_Receipt_CMR"
            ElseIf ddlsource.SelectedItem.Text = "Levy Rice" Then
                ReportViewer1.ServerReport.ReportPath = folder & "Daily_Receipt_Levy"
            ElseIf ddlsource.SelectedItem.Text = "Tender Purchase(by Road)-Sugar/Salt" Then
                ReportViewer1.ServerReport.ReportPath = folder & "Rpt_dailyreceipt_TenderPurchaseByRoad"
            End If

            ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials
            Dim pInfo As ReportParameterInfoCollection
            Dim paramList As New Generic.List(Of ReportParameter)
            'Dim a1 As New ReportParameter
            paramList.Add(New ReportParameter("fromDate", From_Date, False))
            paramList.Add(New ReportParameter("toDate", to_Date, False))
            paramList.Add(New ReportParameter("DistID", Dist_ID, False))
            paramList.Add(New ReportParameter("Depot_ID", Depot_ID, False))
            ' paramList.Add(New ReportParameter("year", Year.ToString, False))

            '        paramList.Add(New ReportParameter("pdistcd", Session("distcd").ToString, False))
            ReportViewer1.ServerReport.SetParameters(paramList)

            pInfo = ReportViewer1.ServerReport.GetParameters()

            ReportViewer1.ServerReport.Refresh()
        End If
    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        If ddlsource.SelectedItem.Text = "Select" Then
            Label1.Text = "Please Select Source of Arrival"

        Else
            If ddlsource.SelectedItem.Text = "Procurement" Then
                Dim cmd2 As New SqlCommand("Delete from tbl_MetaData_GODOWN where DistrictId='" & Session("dist_id").ToString() & "' and CreatedBy='P'", con)
                If con.State = ConnectionState.Closed Then


                End If
                If True Then
                    con.Open()
                End If
                cmd2.ExecuteNonQuery()


                Dim cmd1 As New SqlCommand()
                If cons.State = ConnectionState.Closed Then
                    cons.Open()
                End If

                cmd1.CommandType = CommandType.Text

                Dim qry As String = "select * from tbl_MetaData_GODOWN where DistrictId='23" & Session("dist_id").ToString() & "'"
                Dim da As New SqlDataAdapter(qry, cons)
                Dim dt As New DataTable()
                da.Fill(dt)

                Dim cmd3 As New SqlCommand()

                cmd3.Connection = con
                cmd3.CommandType = CommandType.Text
                For i As Integer = 0 To dt.Rows.Count - 1

                    'Before Insert check the godown id that is present in DB or not, if Not then insert otherwise leave it.

                    Dim dupCount As Int16

                    Dim check As New SqlCommand("select COUNT(Godown_ID) as dupCount from tbl_MetaData_GODOWN where Godown_ID = '" & dt.Rows(i)("Godown_ID") & "' and DistrictId = '" & dt.Rows(i)("DistrictId").ToString().Substring(2, 2) & "' and DepotId = '" & dt.Rows(i)("DepotId") & "' ")

                    check.Connection = con
                    check.CommandType = CommandType.Text


                    dupCount = check.ExecuteScalar()

                    If dupCount = 0 Then

                        cmd3.CommandText = ("insert into tbl_MetaData_GODOWN ([Godown_ID],[StateId],[DistrictId],[DepotId],[Godown_Name],[Godown_Capacity],[CreatedBy]) values('" & dt.Rows(i)("Godown_ID") & "','23','" & dt.Rows(i)("DistrictId").ToString().Substring(2, 2) & "','" & dt.Rows(i)("DepotId") & "','" & dt.Rows(i)("Godown_Name") & "','" & dt.Rows(i)("Godown_Capacity") & "','P')")

                        cmd3.ExecuteNonQuery()
                    Else

                    End If



                Next

                cmd3.Dispose()

                con.Close()
                cons.Close()
                Label1.Visible = False
                Dim fdate As String
                Dim tdate As String

                fdate = getDate_MDY(tx_from_date.Text)
                tdate = getDate_MDY(tx_to_date.Text)

                get_Data(fdate, tdate, dist_code, IC_code)
            Else
                Label1.Visible = False
                Dim fdate As String
                Dim tdate As String

                fdate = getDate_MDY(tx_from_date.Text)
                tdate = getDate_MDY(tx_to_date.Text)

                get_Data(fdate, tdate, dist_code, IC_code)
            End If
        End If




    End Sub
    Protected Function getDate_MDY(ByVal inDate As String) As String
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
        Dim dtProjectStartDate As DateTime = Convert.ToDateTime(inDate)
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"))
    End Function
    Protected Sub get_Source()
        ddlsource.Items.Clear()
        cmd.CommandText = "select * from dbo.Source_Arrival_Type order by Source_ID"
        cmd.Connection = con
        con.Open()
        dr = cmd.ExecuteReader()
        While dr.Read()
            Dim lst As New ListItem
            lst.Text = dr("Source_Name").ToString()
            lst.Value = dr("Source_ID").ToString()
            ddlsource.Items.Add(lst)
        End While

        ddlsource.Items.Insert(0, "Select")
        dr.Close()
        con.Close()

    End Sub

    Protected Function getDate_MDY(ByVal inDate As DateTime)

        Dim dtProjectStartDate As DateTime = Convert.ToDateTime(inDate)
        Return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"))


    End Function

    Protected Sub get_distname()
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
    Protected Sub get_ICname()

        cmd.CommandText = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_code + "'"
        cmd.Connection = con
        con.Open()
        dr = cmd.ExecuteReader()
        While (dr.Read())

            Label2.Text = dr("DepotName").ToString()

        End While
        dr.Close()
        con.Close()
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

End Class
