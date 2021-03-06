using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

public partial class ReportForms_District_TestReprt : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString);
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2013"].ConnectionString);
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
             dist_code = Session["dist_id"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    distname();
                    //SqlCommand cmd2 = new SqlCommand("if exists (select * from isuuss) drop table isuuss", conCS);
                    SqlCommand cmd2 = new SqlCommand("Delete from tbl_MetaData_GODOWN where DistrictId='23" + Session["dist_id"].ToString() + "'", con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd2.ExecuteNonQuery();
                    //try
                    //{

                    //    SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[PurchTrana]([Society_ID] [nvarchar](10) NULL,[Society_Name] [nvarchar](120) NULL,[Qty_Purch] [float] NULL,[Qty_Transfer] [float] NULL,[Bag_Trans] [float] NULL,[Truck_Dispetch] int null,[Truck_Reciv] int null, [Qty_Recive] [float] NULL)", conCS);

                    //    // cmd.Connection.Open();
                    //    cmd.ExecuteNonQuery();
                    //}
                    //catch(Exception){}

                   
                    SqlCommand cmd1 = new SqlCommand();
                    if (cons.State == ConnectionState.Closed)
                    {
                        cons.Open();
                    }
                  
                    cmd1.CommandType = CommandType.Text;
                    //cmd1.CommandText = "select soc.Society_Id,(soc.Society_Name+','+soc.SocPlace)Society_Name,(select ISNULL(SUM(QtyReceived),0) from WPMS2013After10012013.dbo.CommodityReceived_Transaction where CommodityReceived_Transaction.Society_Id=soc.Society_Id)Qty_Purch,(select ISNULL(SUM(QtyTransffer),0) from WPMS2013After10012013.dbo.IssueToSangrahanaKendra where IssueToSangrahanaKendra.SocietyId=soc.Society_Id)Qty_Transfer,(select ISNULL(SUM(Bags),0) from WPMS2013After10012013.dbo.IssueToSangrahanaKendra where IssueToSangrahanaKendra.SocietyId=soc.Society_Id)Bag_Trans,(select ISNULL(count(TruckNo),0) from WPMS2013After10012013.dbo.IssueToSangrahanaKendra where IssueToSangrahanaKendra.SocietyId=soc.Society_Id)Truck_Dispetch,(select ISNULL(count(TruckNo),0) from WPMS2013After10012013.dbo.IssueCenterReceipt_Online where IssueCenterReceipt_Online.SocietyId=soc.Society_Id)Truck_Reciv,(select ISNULL(sum(Recv_Qty),0) from WPMS2013After10012013.dbo.IssueCenterReceipt_Online where IssueCenterReceipt_Online.SocietyId=soc.Society_Id)Qty_Recive from WPMS2013After10012013.dbo.Society soc where soc.DistrictId='23" + Session["dist_id"].ToString() + "'";
                    //SqlDataAdapter da = new SqlDataAdapter(qry, con);
                    string qry = "select * from tbl_MetaData_GODOWN where DistrictId='23" + Session["dist_id"].ToString() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(qry, cons);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    SqlCommand cmd3 = new SqlCommand();

                    cmd3.Connection = con;
                    cmd3.CommandType = CommandType.Text;
                    for(int i =0;i<=dt.Rows.Count-1;i++)
                    {
                        cmd3.CommandText = "insert into tbl_MetaData_GODOWN ([Godown_ID],[DistrictId],[DepotId],[Godown_Name],[Godown_Capacity],[Hired_Type],[Storage_Type]) values('" + dt.Rows[i]["Godown_ID"] + "','" + dt.Rows[i]["DistrictId"] + "','" + dt.Rows[i]["DepotId"] + "','" + dt.Rows[i]["Godown_Name"] + "','" + dt.Rows[i]["Godown_Capacity"] + "','" + dt.Rows[i]["Hired_Type"].ToString().Trim() + "','" + dt.Rows[i]["Storage_Type"].ToString().Trim() + "')";
                    
                    cmd3.ExecuteNonQuery();
                    }
                    
                    cmd3.Dispose();
                   // cmd.Connection.Close();
                    con.Close();
                    cons.Close();
            
                }
                catch (Exception ex)
                {
                   // SqlCommand cmd = new SqlCommand("drop table isuuss", conCS);

                   //// cmd.Connection.Open();
                   // cmd.ExecuteNonQuery();

                }
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

                string userName = ConfigurationManager.ConnectionStrings["uname_WPMS2013"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw_WPMS2013"].ProviderName;
                string domainName = ConfigurationManager.ConnectionStrings["domain_WPMS2013"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion

    }
    public string ReportName
    {
        get { return _ReportName; }
        set { _ReportName = value; }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string district = "23" + dist_code;
            string folder = ConfigurationManager.ConnectionStrings["rptfolder_WPMS2013"].ProviderName;
            //if (ReportViewer1.ServerReport.ReportPath == folder + "DailyProcurementReportCIndvDistrict")
            //{
            //    ReportViewer1.PerformBack();

            //} else {
            // For Report

            string reportURL = "";
            //reportURL = "http://staging.mp.nic.in/ReportServer"
            reportURL = ConfigurationManager.ConnectionStrings["rpturl_WPMS2013"].ProviderName;
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);

            //ReportViewer1.ServerReport.ReportPath = "/report/SendSmsReport"
            ReportViewer1.ServerReport.ReportPath = folder + "rpt_CenterWiseProcTrans_CSMS_Dist";

            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            System.Collections.Generic.List<ReportParameter> paramList = new System.Collections.Generic.List<ReportParameter>();
            //Dim a1 As New ReportParameter

            paramList.Add(new ReportParameter("district", district, false));


            //        paramList.Add(New ReportParameter("pdistcd", Session("distcd").ToString, False))
            ReportViewer1.ServerReport.SetParameters(paramList);

            pInfo = ReportViewer1.ServerReport.GetParameters();




            ReportViewer1.ServerReport.Refresh();

        
        }
        catch (Exception ex) { }
        finally
            {
                
            
            }
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder_WPMS2013"].ProviderName;
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
