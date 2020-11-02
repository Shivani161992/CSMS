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

public partial class State_CMS_BillStatus : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    public string user, DistName;
    public string DistId;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()); //For IssueCentre
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null || Session["Region_id"] != null || Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

                GetCommodities();
                txtTdate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                string Month = DateTime.Now.ToString("MM");

                if (Month == "01" || Month == "02" || Month == "04" || Month == "06" || Month == "09" || Month == "11")
                {
                    txtFdate.Text = DateTime.Now.AddDays(-31).ToString("dd-MM-yyyy");
                }
                else
                {
                    txtFdate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtFdate.Text = Request.Form[txtFdate.UniqueID];
        txtTdate.Text = Request.Form[txtTdate.UniqueID];
    }
    public void GetCommodities()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('33','64', '63') order by Commodity_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlcomm.DataSource = ds.Tables[0];
                    ddlcomm.DataTextField = "Commodity_Name";
                    ddlcomm.DataValueField = "Commodity_Id";
                    ddlcomm.DataBind();
                    ddlcomm.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txtFdate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select From Date'); </script> ");
            return;
        }
        else if (txtTdate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select To Date'); </script> ");
            return;
        }
        else
        {

            string fromdate = Request.Form[txtFdate.UniqueID];
            txtFdate.Text = fromdate;
            string todate = Request.Form[txtTdate.UniqueID];
            txtTdate.Text = todate;

            ConvertServerDate ServerDate = new ConvertServerDate();
            string Fromdate = ServerDate.getDate_MDY(fromdate.ToString()) + " 00:00:00";
            string ToDate = ServerDate.getDate_MDY(todate.ToString()) + " 23:59:59";
            getData(ddlcomm.SelectedValue.ToString(), Fromdate, ToDate);
        }

    }

    private void getData(string cmd, string Fromdate, string ToDate)
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

        ReportViewer1.ServerReport.ReportPath = folder + "CMS_BillStatus";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("CMD", cmd, false));
        paramList.Add(new ReportParameter("fdate", Fromdate, false));
        paramList.Add(new ReportParameter("tdate", ToDate, false));
        //paramList.Add(new ReportParameter("DistId", DistId, false));
        // paramList.Add(new ReportParameter("CropYear", CropYear, false));



        ReportViewer1.ServerReport.SetParameters(paramList);
        pInfo = ReportViewer1.ServerReport.GetParameters();
        ReportViewer1.ServerReport.Refresh();
    }

    //protected void lnkbtnBack_Click(object sender, EventArgs e)
    //{
    //    string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName;

    //    if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
    //    {
    //        this.ReportViewer1.PerformBack();
    //    }
    //    else
    //    {
    //        if (Session["st_id"] != null)  //For HO
    //        {
    //            if (Session["st_Name"].ToString() == "Markfed")
    //            {
                   
    //            }
    //            else
    //            {
    //                Response.Redirect("~/State/FrmPaddyMillingRpt.aspx");
    //            }
    //        }
    //        else if (Session["Region_id"] != null) //For RM
    //        {
    //           // Response.Redirect("~/Regional_Office/frmReports.aspx");
    //        }
    //        else if (Session["dist_id"] != null) //For DM
    //        {
    //           // Response.Redirect("~/District/FrmPaddyMillingReports.aspx");
    //        }
    //    }
    //}

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