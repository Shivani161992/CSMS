using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Data;
using DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;

public partial class mpproc_ReportsPage_StateLevel_Rep_DistwiseAgProc : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    DataReader drObj = null;
    SqlString SqlObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["StateLog_Agency"] == null )
        {

            Response.Redirect("../frmLogin.aspx");

        }
        else
        {
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            if (!IsPostBack)
            {

                lbl_AgRes.Text = Session["StateLog_Agency"].ToString();
                lbl_CYRes.Text = Session["StateLog_CropY"].ToString();
                lbl_MsRes.Text = Session["StateLog_MarkSeas"].ToString();
                GetCommodity();

            }


        }


    }
    private void GetCommodity()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strcom = Session["StateLog_MarkID"].ToString();
            string strsql = "SELECT * FROM CommodityMaster where MarkSeasId='" + strcom + "'";
            DataSet ds = SqlObj.selectAny(strsql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Commodity.DataSource = ds.Tables[0];
                DDL_Commodity.DataTextField = "CommodityName";
                DDL_Commodity.DataValueField = "CommodityId";
                DDL_Commodity.DataBind();
                DDL_Commodity.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

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


        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }


        public System.Net.ICredentials NetworkCredentials
        {
            get
            {

                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion

    }


    protected void btn_ViewReport_Click(object sender, EventArgs e)
    {
        if (Session["StateLog_Agency"] != null)
        {

            string Agname = Session["StateLog_Agency"].ToString();
            string Crop = Session["StateLog_CropY"].ToString();
            string MsId = Session["StateLog_MarkID"].ToString();
            string Comid = DDL_Commodity.SelectedValue;

            if (DDL_Commodity.SelectedItem.Text != "--Select--")
            {

                string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

                string reportURL = "";

                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);


                ReportViewer1.ServerReport.ReportPath = folder + "DistWiseProcByAgency";

                ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
                System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

                paramList.Add(new ReportParameter("CropYear", Crop));
                paramList.Add(new ReportParameter("MID", MsId));
                paramList.Add(new ReportParameter("ComId", Comid));
                paramList.Add(new ReportParameter("AG_Name", Agname));

                ReportViewer1.ServerReport.SetParameters(paramList);

                pInfo = ReportViewer1.ServerReport.GetParameters();

                ReportViewer1.ServerReport.Refresh();

            }
        }

    }
}
