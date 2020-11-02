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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Data.SqlClient;

public partial class Division_Report_Proc_Details : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_id"] != null)
        {
         
            if (!IsPostBack)
            {
                frmdate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

               
                todate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                todate.Text = DateTime.Now.ToString("dd-MM-yyyy");


                string selqry = "select tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , Commodity_Id from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (12,13,14,22) order by  Commodity_Id desc";
               
                SqlCommand cmd = new SqlCommand(selqry,con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcommmodity.DataSource = ds.Tables[0];

                    ddlcommmodity.DataTextField = "Commodity_Name";

                    ddlcommmodity.DataValueField = "Commodity_Id";

                    ddlcommmodity.DataBind();  
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
               
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void get_Data(string frmdate, string todate, string commodity , string regid)
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

        ReportViewer1.ServerReport.ReportPath = folder + "RM_DetailProc";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("frmdate", frmdate, false));
        paramList.Add(new ReportParameter("todate", todate, false));
        paramList.Add(new ReportParameter("Commodity_Id", commodity, false));

        paramList.Add(new ReportParameter("Division_code", regid, false));


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
            Response.Redirect("~/Regional_Office/frmReports.aspx");
           
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (frmdate.Text == "" || todate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date'); </script> ");
            return;
        }

        string fdate = getDate_MDY(frmdate.Text.ToString());

        string tdate = getDate_MDY(todate.Text.ToString());

        string commodity = ddlcommmodity.SelectedValue;

        string regid = Session["Region_id"].ToString();

        get_Data(fdate, tdate, commodity, regid);
    }
}
