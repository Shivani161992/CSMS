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

public partial class Report_IssueCenter_Faq_NonFaq : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    string issueid = "" ;
    string dist = "" ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            issueid = Session["issue_id"].ToString();
            dist = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                string getdis = "select Society.Society_Id , Society.Society_Name + ',' + Society.SocPlace from Society where DistrictId = 23" + dist + " and Society_Id in (select distinct Purchase_Center from SCSC_Procurement where Distt_ID = '" + dist + "')";

                SqlCommand cmd = new SqlCommand(getdis, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlsociety.DataSource = ds.Tables[0];

                    ddlsociety.DataTextField = "Society_Name";

                    ddlsociety.DataValueField = "Society_Id";

                    ddlsociety.DataBind();

                    ddlsociety.Items.Insert(0, "-Select-");
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();

                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlsociety.SelectedValue == "0" || ddlsociety.SelectedItem.Text == "-Select-")
        {

        }

        else
        {
            if (tx_date.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date'); </script> ");
                return;
            }

            if (tx_todate.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date'); </script> ");
                return;
            }



            string fdate = getDate_MDY(tx_date.Text.ToString());

            string todate = getDate_MDY(tx_todate.Text.ToString());

            string IssueId = Session["issue_id"].ToString();

            string DistrictId = Session["dist_id"].ToString();

            string Pcid = ddlsociety.SelectedValue;

            getData(fdate, todate,DistrictId, IssueId, Pcid);

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

    private void getData(string fdate, string todate, string DistrictId, string IssueId, string pcid)
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

        ReportViewer1.ServerReport.ReportPath = folder + "FAq_NoFaq";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("Distid", DistrictId, false));
        paramList.Add(new ReportParameter("issuecenter", IssueId, false));

        paramList.Add(new ReportParameter("frmdate", fdate, false));


        paramList.Add(new ReportParameter("todate", todate, false));
        paramList.Add(new ReportParameter("Purchase_Center", pcid, false));

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
