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
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
public partial class ReportForms_District_Fin_DistrictSubsidy_AllDist_Report : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {


                try
                {
                    BindMonth();
                    BindCrop();
                    ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                    ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);
                    ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 2).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
                    ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 3).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 2).ToString());
                    ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 4).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 3).ToString());
                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Problem Occured,Please Try Again |');</script>");
                }


            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void BindCrop()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = "SELECT [Commodity_Id],[Commodity_Name],[Status] FROM [dbo].[tbl_MetaData_STORAGE_COMMODITY] where Status='Y'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        ddlcrop.DataSource = ds.Tables[0];
        ddlcrop.DataTextField = "Commodity_Name";
        ddlcrop.DataValueField = "Commodity_Id";
        ddlcrop.DataBind();
        ddlcrop.Items.Insert(0, "--चुनें--");
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
    private void getData(string CropYear, string Crop_ID, string MonthID, string MonthName)
    {
        while (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }

        pnlreport.Visible = true;
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        string reportURL = "";

        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

        ReportViewer1.ServerReport.ReportPath = folder + "Fin_DCP_Stock_AllDistrict";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();


        //paramList.Add(new ReportParameter("DistrictID", DistrictID, false));
        paramList.Add(new ReportParameter("CropYear", CropYear, false));
        paramList.Add(new ReportParameter("Crop_ID", Crop_ID, false));
        paramList.Add(new ReportParameter("MonthID", MonthID, false));
        paramList.Add(new ReportParameter("MonthName", MonthName, false));

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

    }
    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getData(ddlcropyear.SelectedValue, ddlcrop.SelectedValue, ddlmonth.SelectedValue, ddlmonth.SelectedItem.Text);
        }
        catch
        {
        }
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCrop();
        }
        catch
        {
        }
    }
    protected void BindMonth()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = "SELECT [MonthID],[MonthShort] FROM [dbo].[FIN_MonthMaster]";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        ddlmonth.DataSource = ds.Tables[0];
        ddlmonth.DataTextField = "MonthShort";
        ddlmonth.DataValueField = "MonthID";
        ddlmonth.DataBind();
        ddlmonth.Items.Insert(0, "--चुनें--");
    }
}
