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

public partial class ReportForms_District_Rpt_Rack_frmProcurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public string distid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                get_commodity_name();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void get_commodity_name()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = "SELECT [Commodity_Id],[Commodity_Name],[Status] FROM [dbo].[tbl_MetaData_STORAGE_COMMODITY] where Status='Y' and Commodity_Id in ('13','14','8','11','12','40','22')";
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
        ddlcrop.Items.Insert(0, "--Select--");
    }

    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlracknumber.Items.Clear();
        if (ddlcrop.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else
        {
            if (ddlcrop.SelectedValue.ToString() == "22")
            {
                getrack();
            }
            else
            {
                getKharifrack();
            }
        }
    }

    private void getrack()
    {
        string qry = "select distinct SCSC_Procurement2016.RackNumber from SCSC_Procurement2016 where Distt_ID = '" + distid + "' and RackNumber is not null and RackNumber !=''";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlracknumber.DataSource = ds.Tables[0];
            ddlracknumber.DataTextField = "RackNumber";
            ddlracknumber.DataBind();
            ddlracknumber.Items.Insert(0, "All");
        }
    }

    private void getKharifrack()
    {
        string qry = "select distinct SCSC_Procurement_Kharif2016.RackNumber from SCSC_Procurement_Kharif2016 where Distt_ID = '" + distid + "' and RackNumber is not null and RackNumber !=''";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlracknumber.DataSource = ds.Tables[0];
            ddlracknumber.DataTextField = "RackNumber";
            ddlracknumber.DataValueField = "RackNumber";
            ddlracknumber.DataBind();
            ddlracknumber.Items.Insert(0, "All");
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Rack Number Is Not Available'); </script> ");
            return;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlracknumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rack Number'); </script> ");
            return;
        }
        else
        {
            if (ddlcrop.SelectedValue.ToString() == "22")
            {
                if (ddlracknumber.SelectedItem.Text == "All")
                {
                    get_Data(distid);
                }

                else
                {
                    string racknumber = ddlracknumber.SelectedItem.Text;

                    get_RackData(distid, racknumber);
                }
            }
            else
            {
                if (ddlracknumber.SelectedItem.Text == "All")
                {
                    getKharif_Data(distid);
                }

                else
                {
                    string racknumber = ddlracknumber.SelectedItem.Text;

                    getKharif_RackData(distid, racknumber);
                }
            }
        }
    }

    private void get_Data(string districtId)
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

        ReportViewer1.ServerReport.ReportPath = folder + "RackReport_Dist";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("DistID", districtId, false));

        //district_code

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    private void getKharif_Data(string districtId)
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

        ReportViewer1.ServerReport.ReportPath = folder + "RackReport_DistKharif2016";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("DistID", districtId, false));

        //district_code

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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

        if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        else
        {
            if (Session["Collector/DIO"] == "Collector" || Session["Collector/DIO"] == "DIO")
            {
                Response.Redirect("~/District/frmReport_Coll_DIO.aspx");
            }

            else if (Session["DistrictManager"] == "DM" || Session["OperatorIDDM"] == "DM" || Session["Collector/DIO"] == "DMMPSCSC" || Session["DistrictManager"] == "OP")
            {
                Response.Redirect("~/District/Frm_Procurement_Reports.aspx");
            }
        }
    }

    private void get_RackData(string districtId, string racknum)
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

        ReportViewer1.ServerReport.ReportPath = folder + "RackReport_frmprocurement_RacknumWise";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("DistID", districtId, false));

        paramList.Add(new ReportParameter("RackNumber", racknum, false));

        //district_code

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

}
