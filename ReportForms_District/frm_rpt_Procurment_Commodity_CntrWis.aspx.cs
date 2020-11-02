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
using System.Security.Principal;
using System.Data.SqlClient;
public partial class ReportForms_District_frm_rpt_Procurment_Commodity_CntrWis : System.Web.UI.Page
{
    public SqlConnection conCS = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static string _ReportName = "";
    public string Report_Name;
    static DataSet dsSendSMS;
    string dist_code = "";
    static string con_name;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            try
            {
                dist_code = Session["dist_id"].ToString();
                if (!IsPostBack)
                {
                    distname();
                    get_Data_Paddy();
                }
            }
            catch (Exception ex)
            {


            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    private void distname()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "select * from pds.districtsmp where district_code='" + dist_code + "'";
        cmd.Connection = conCS;
        conCS.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Label3.Text = dr["district_name"].ToString();
        }

        dr.Close();
        conCS.Close();
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        get_Data_Paddy();
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
                
                string userName = ConfigurationManager.ConnectionStrings["uname_PPMS2013"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw_PPMS2013"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain_PPMS2013"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion

    }

    private void get_Data_Paddy()
    {
        string district = "23" + dist_code;
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        string folder = ConfigurationManager.ConnectionStrings["rptfolder_PPMS2013"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl_PPMS2013"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "rpt_CenterWiseProcTrans_CSMS_Dist";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("district", district, false));
        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    private void get_Data_CG()
    {
        string district = "23" + dist_code;
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        string folder = ConfigurationManager.ConnectionStrings["rptfolder_MPMS2013"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl_MPMS2013"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "rpt_CenterWiseProcTrans_CSMS_Dist";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("district", district, false));
        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }


    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        get_Data_CG();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder_PPMS2013"].ProviderName;
        if (Session["dist_id"] != null)
        {

            if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
            {
                this.ReportViewer1.PerformBack();
            }
            else
            {

                Response.Redirect("~/District/frmReports.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}

