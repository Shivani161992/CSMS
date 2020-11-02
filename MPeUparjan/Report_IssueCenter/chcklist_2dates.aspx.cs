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

public partial class Report_IssueCenter_chcklist_2dates : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                tx_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
                to_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                getcomm();
            }
           

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (tx_date.Text == "" || to_date.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date'); </script> ");
            return;
        }

        string year = getDate_YYYY(tx_date.Text);

        if (year == "2016")
        {
            string fdate = getDate_MDY(tx_date.Text.ToString());

            string todate = getDate_MDY(to_date.Text.ToString());

            string IssueId = Session["issue_id"].ToString();

            string DistrictId = Session["dist_id"].ToString();

            if (ddlcommodity.SelectedValue == "22")
            {
                getData1(fdate, todate, DistrictId, IssueId);
            }

        }

        else
        {
            string fdate = getDate_MDY(tx_date.Text.ToString());

            string todate = getDate_MDY(to_date.Text.ToString());

            string IssueId = Session["issue_id"].ToString();

            string DistrictId = Session["dist_id"].ToString();

            getData(fdate, todate, DistrictId, IssueId);
            
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        else
        {
            Response.Redirect("~/IssueCenter/issue_welcome.aspx");
        }
    }

    private void getData(string fdate, string todate, string DistrictId, string IssueId)
    {
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }


        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "CheckList_2date";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("Distt_ID", DistrictId, false));
        paramList.Add(new ReportParameter("IssueCenter_ID", IssueId, false));

        paramList.Add(new ReportParameter("Recd_Date", fdate, false));
        paramList.Add(new ReportParameter("To_Date", todate, false));

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

    void getcomm()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string com = "SELECT  [Commodity_Id] ,[Commodity_Name] FROM [dbo].[tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id in ('8','11','12','13','14','22')";
        SqlCommand cmd = new SqlCommand(com, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--select--");
        }

        else
        {
            ddlcommodity.DataSource = "";

            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }

    private void getData1(string fdate, string todate, string DistrictId, string IssueId)
    {
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }

        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "CheckList2016_2date";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("Distt_ID", DistrictId, false));
        paramList.Add(new ReportParameter("IssueCenter_ID", IssueId, false));

        paramList.Add(new ReportParameter("Recd_Date", fdate, false));
        paramList.Add(new ReportParameter("To_Date", todate, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

    }

    protected String getDate_YYYY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("yyyy"));
        }
        return dat;
    }
}
