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

public partial class ReportForms_District_rpt_storagewiseDetail : System.Web.UI.Page
{
    string distid;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                string depot = "select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = '23" + distid + "'";

                SqlCommand cmd = new SqlCommand(depot, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlissue.DataSource = ds.Tables[0];

                    ddlissue.DataTextField = "DepotName";

                    ddlissue.DataValueField = "DepotID";

                    ddlissue.DataBind();
                    ddlissue.Items.Insert(0, "-Select-");

                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();

                }

                txtDate.Text = "01-04-2015";
                txtDateTill.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

                get_commodity_name();

            }
            txtDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            txtDateTill.Attributes.Add("onkeypress", "return CheckCalDate(this)");
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
        ddlcrop.Items.Insert(0, "--चुनें--");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Session["dist_id"] != null)
        {

            if (txtDate.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select From Date'); </script> ");
                return;
            }

            if (txtDateTill.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select To Date'); </script> ");
                return;
            }

            if (ddlcrop.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity'); </script> ");
                return;
            }
            if (ddlgodown.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown'); </script> ");
                return;
            }
            # region withSchemeId


            string fdate = getDate_MDY(txtDate.Text.ToString());
            string tdate = getDate_MDY(txtDateTill.Text.ToString());

            string Dist_Code = Session["dist_id"].ToString();

            string godown = ddlgodown.SelectedValue;

            if (ddlcrop.SelectedValue.ToString() == "22")
            {
                get_Datacust(fdate, tdate, Dist_Code, godown);
            }
            else
            {
                get_DataPaddy(fdate, tdate, Dist_Code, godown);
            }

            # endregion


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    private void get_DataPaddy(string fdate, string tdate, string Dist_Code, string godown)
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

        ReportViewer1.ServerReport.ReportPath = folder + "GdnWiseRcpt_PdyProc2016_Dist";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();


        paramList.Add(new ReportParameter("fromDate", fdate, false));
        paramList.Add(new ReportParameter("toDate", tdate, false));

        paramList.Add(new ReportParameter("Distt_ID", Dist_Code, false));   // Dist ID

        paramList.Add(new ReportParameter("Recd_Godown", godown, false));   // Dist ID

        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();

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

    private void get_Datacust(string fdate, string tdate, string Dist_Code, string godown)
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

        ReportViewer1.ServerReport.ReportPath = folder + "Socitywise_StorageReport";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();


        paramList.Add(new ReportParameter("fromDate", fdate, false));
        paramList.Add(new ReportParameter("toDate", tdate, false));

        paramList.Add(new ReportParameter("Distt_ID", Dist_Code, false));   // Dist ID

        paramList.Add(new ReportParameter("Recd_Godown", godown, false));   // Dist ID

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

    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlissue.SelectedItem.Text == "-Select-" || ddlissue.SelectedValue == "0")
        {
            ddlissue.DataSource = "";

            ddlissue.DataBind();
            ddlissue.Items.Insert(0, "-Select-");
        }

        else
        {
            Getgon();
        }
    }

    private void Getgon()
    {
        try
        {

            if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }

            string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId = '23" + distid + "' and DepotId = '" + ddlissue.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, "--select--");
                }
            }


        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }

    }
    protected void txtDateTill_TextChanged(object sender, EventArgs e)
    {

    }
}