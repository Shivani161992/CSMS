using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

public partial class Report_IssueCenter_DailyReceipt_Register : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    public string dist_code = "";
    public string IC_code = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
              
                tx_from_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                tx_to_date.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                               
                tx_from_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
                tx_to_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                get_distname();
                get_ICname();
            }


            dist_code = Session["dist_id"].ToString();
            IC_code = Session["issue_id"].ToString();

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (tx_from_date.Text == "" || tx_to_date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date'); </script> ");
            return;
        }


        string fdate = null;
        string tdate = null;


        fdate = getDate_MDY(tx_from_date.Text.ToString());

        tdate = getDate_MDY(tx_to_date.Text.ToString());

        get_Data(fdate, tdate, dist_code, IC_code);
    }

   

    protected void get_distname()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        cmd.CommandText = "select * from pds.districtsmp where district_code='" + dist_code + "'";
        cmd.Connection = con;
      
        dr = cmd.ExecuteReader();

        while ((dr.Read()))
        {
            Label3.Text = dr["district_name"].ToString();

        }
        dr.Close();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        
    }

    protected void get_ICname()
    {
        cmd.CommandText = "select DepotName from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_code + "'";
        cmd.Connection = con;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        dr = cmd.ExecuteReader();

        while ((dr.Read()))
        {
            Label2.Text = dr["DepotName"].ToString();

        }
        dr.Close();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected String getDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        return dat;
    }

    private void get_Data(string From_Date, string to_Date, string Dist_ID, string Depot_ID)
    {
            Label1.Visible = false;
            ReportViewer1.Visible = true;

            string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

            // For Report
            string reportURL = "";

            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);
                                
            ReportViewer1.ServerReport.ReportPath = folder + "DailyReceiptRegistor2016";
               
            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();

            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

            paramList.Add(new ReportParameter("fromDate", From_Date, false));
            paramList.Add(new ReportParameter("toDate", to_Date, false));
            paramList.Add(new ReportParameter("DistID", Dist_ID, false));
            paramList.Add(new ReportParameter("Depot_ID", Depot_ID, false));
           
            ReportViewer1.ServerReport.SetParameters(paramList);

            pInfo = ReportViewer1.ServerReport.GetParameters();

            ReportViewer1.ServerReport.Refresh();
     
    }

    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName,
        out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        // Specifies the user to impersonate when connecting to a report server. 
        //A WindowsIdentity object representing the user to impersonate.
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        // Returns network credentials to be used for authentication with the report server. 
        //A NetworkCredentials object.
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                //you can place below settings in configuration xml file
                //string userName = "Administrator";
                //string password = "nic123";
                //string domainName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion
    }
}
