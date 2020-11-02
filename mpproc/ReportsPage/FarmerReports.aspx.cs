using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Security.Principal;
using System.Data.SqlClient;
public partial class mpproc_ReportsPage_FarmerReports : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] == "")
        {

            Response.Redirect("../frmLogin.aspx");

        }
      
      
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = "FarmerCheckList";
    
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
        
            if (!IsPostBack)
            {

                string distId = Session["dist_id"].ToString();
                string servername = ConfigurationManager.ConnectionStrings["rpturl_mpproc"].ProviderName.ToString();

                ReportViewer1.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer1.Visible = true;

                uname = ConfigurationManager.ConnectionStrings["uname_mpproc"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw_mpproc"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain_mpproc"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder_mpproc"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl_mpproc"].ProviderName.ToString();
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

                ReportViewer1.ServerReport.ReportPath = folder + url;
                ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();

                ReportViewer1.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "DistrictId";
                reportParameterCollection[0].Values.Add(distId);

                ReportViewer1.ServerReport.SetParameters(reportParameterCollection);
                ReportViewer1.ServerReport.Refresh();

            }

        }

        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + ex.Message.ToString()+ "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }

    }


    [Serializable]
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
                //string domain_warehouseName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname_mpproc"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw_mpproc"].ProviderName;
                string domain_warehouseName = ConfigurationManager.ConnectionStrings["domain_mpproc"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domain_warehouseName);
            }
        }

        #endregion

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}

