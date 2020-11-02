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

public partial class ReportsPage_Rep_WheatProcureFarmerWise : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    DataReader drObj = null; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] == "")
        {

            Response.Redirect("../frmLogin.aspx");

        }
        else
        {
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            if (!IsPostBack)
            {

               
                Label1.Text = Session["dist_name"].ToString();
                Label2.Text = Session["pc_name"].ToString();
            
                txtfromDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
              

            }


        }

       

    }

    //private void GetDistFromLog()
    //{
    //    if (Session["pcId"] != "")
    //    {

    //    string distname = Session["dist_name"].ToString();
    //    string PCName = Session["pc_name"].ToString();
    //    string distId= Session["dist_id"].ToString();

    //    string pcId = Session["pcId"].ToString(); 
        
        
    //    }
        
    //}

    //private void GetDist()
    //{
    //    drObj = new DataReader(ComObj);
    //    string strSql = "Select * from DistrictMaster order by DistrictName";
    //    DataSet ds = drObj.selectAny(strSql);
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            DDL_Dist.DataSource = ds.Tables[0];
    //            DDL_Dist.DataValueField = "DistrictCode";
    //            DDL_Dist.DataTextField = "DistrictName";
    //            DDL_Dist.DataBind();
    //            DDL_Dist.Items.Insert(0, "--Select--");

    //        }

    //    }
    
    //}
    //protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    //{ 
    //  try
    //  {
    //    if (Session["Markseas_id"] != "" && Session["cropyear"] != "")
    //    {
    //        string MarkId = Session["Markseas_id"].ToString();
    //        string cropyear = Session["cropyear"].ToString();
    //        Session["dist_id"] = DDL_Dist.SelectedValue.ToString();
    //        Session["pcId"] = DDL_PC.SelectedValue.ToString();
        
    //        drObj = new DataReader(ComObj);
    //        string strSql = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + DDL_Dist.SelectedValue + "'  and  PurchaseCenterMaster.MarkSeasId = '" + MarkId + "' and cropyear ='" + cropyear + "' and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
    //        DataSet ds = drObj.selectAny(strSql);
    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                DDL_PC.DataSource = ds.Tables[0];
    //                DDL_PC.DataValueField = "PcId";
    //                DDL_PC.DataTextField = "PurchaseCenterName";
    //                DDL_PC.DataBind();
    //                DDL_PC.Items.Insert(0, "--Select--");

    //            }

    //        }
    //    }
    //    else 
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Session Time out ,Please Relogin.. '); </script> ");
        
    //    }
    //}
    //    catch(Exception ex)
    //    {
    //     Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert(ex); </script> ");
        
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

                string userName = ConfigurationManager.ConnectionStrings["uname_mpproc"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw_mpproc"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain_mpproc"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion

    }



    protected void btn_ViewReport_Click(object sender, EventArgs e)
    {
             string distId = Session["dist_id"].ToString();
             string pcId = Session["pcId"].ToString(); 
            string fdate = getDate_MDY(txtfromDate.Text.ToString ());
            string tdate = getDate_MDY(txtTodate.Text.ToString());
           
          
           
        if (txtfromDate.Text != "" || txtTodate.Text!="")
        {

            string folder = ConfigurationManager.ConnectionStrings["rptfolder_mpproc"].ProviderName;

            string reportURL = "";

            reportURL = ConfigurationManager.ConnectionStrings["rpturl_mpproc"].ProviderName;
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);


            ReportViewer1.ServerReport.ReportPath = folder + "WheatProcurementFarmerWise";

            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();

            paramList.Add(new ReportParameter("distid", distId));
            paramList.Add(new ReportParameter("pcid", pcId));
            paramList.Add(new ReportParameter("fdate", fdate));
            paramList.Add(new ReportParameter("tdate", tdate));

            ReportViewer1.ServerReport.SetParameters(paramList);

            pInfo = ReportViewer1.ServerReport.GetParameters();

            ReportViewer1.ServerReport.Refresh();


        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
   
    }
    

