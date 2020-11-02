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

public partial class ReportForms_District_Proc_frmOtherDist : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);

            GetCommodity();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string cropyear = ddlcropyear.SelectedItem.Text;

        string commid = ddlcomm.SelectedValue;

        string Dist_Code = Session["dist_id"].ToString();

        get_Data(cropyear, commid, Dist_Code);
    }

    private void get_Data(string cropyear, string commid, string Distid)
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

        ReportViewer1.ServerReport.ReportPath = folder + "procDeposit_OtherDist";
        ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
        ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
        System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

        paramList.Add(new ReportParameter("Crop_Year", cropyear, false));
        paramList.Add(new ReportParameter("Commodity_Id", commid, false));
        paramList.Add(new ReportParameter("district_code", Distid, false));
       
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

    void GetCommodity()
    {

        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {

                    con.Open();
                }

                string qrysel = "select Commodity_Id , Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (12,13,14,22) order by Commodity_Id desc";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con);
                /// 
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcomm.DataSource = ds.Tables[0];
                        ddlcomm.DataTextField = "Commodity_Name";
                        ddlcomm.DataValueField = "Commodity_Id";
                        ddlcomm.DataBind();
                        ddlcomm.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            if (con.State == ConnectionState.Open)
            {

                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {

                con.Close();
            }
        }
    }
}
