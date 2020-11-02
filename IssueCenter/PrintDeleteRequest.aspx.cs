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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class IssueCenter_PrintDeleteRequest : System.Web.UI.Page
{
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2013"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2013"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public string distname = "";
    public string dist = "";
    public string sid = "";
    public string ssid = "";
    public string dname = "";
    public string snid = "";
    public string distid = "";
    public string dipotid = "";
    public string cdate = "";
    public string issueid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
    

        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
         
           
            


            issueid = Session["issue_id"].ToString();
            dist = Session["dist_id"].ToString();
            lbl_dist.Text=  Session["distname"].ToString();
            lbl_issuecentre.Text =  Session["depotname"].ToString(); 
           
            lbl_subject.Text = Session["reason"].ToString();
            lbl_discription.Text = Session["descrip"].ToString();
            lblcrop.Text = Session["cropyear"].ToString();
            lblcomdty.Text = Session["commodity"].ToString();
          hd_pcid.Value=  Session["pcid"].ToString();

            lbl_acceptance.Text = Session["Acceptance"].ToString();
            lblsenddist.Text = Session["sendingdist"].ToString();


            lblpccenter.Text = Session["purchaseCentre"].ToString();
            lbl_oprater.Text = Session["opratername"].ToString();
            lbl_todaydate.Text = DateTime.Now.ToString("dd/MM/yyyy");







            GetDistt();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void GetDistt()
    {
        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        lbl_todaydate.Text = getDate_MDY(lbl_todaydate.Text);



        //lblgno.Text = sid;
        string daten = DateTime.Now.ToString();
        string gdaten = getdate(daten);
        //lblgdtae.Text = gdaten;

        # region Wheat
        if (Session["Commodity_Id"].ToString() == "1")
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string str = "select *from tbl_Delete_Request where IssueCenter_ID='" + issueid + "' and  Acceptance_No='" + lbl_acceptance.Text + "' and Purchase_Center='" + hd_pcid.Value + "' and Requested_Date='" + daten + "' ";

                SqlDataAdapter daP = new SqlDataAdapter(str, con);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
         
  
                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();

            }
            catch (Exception ex)
            {
              

            }
            finally
            {

                con_WPMS.Close();
            }
        }
        # endregion

        # region Paddy
        else if (Session["Commodity_Id"].ToString() == "2" || Session["Commodity_Id"].ToString() == "3")
        {
            try
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string str = "select *from tbl_Delete_Request where IssueCenter_ID='" + issueid + "' and  Acceptance_No='" + lbl_acceptance.Text + "' and Purchase_Center='" + hd_pcid.Value + "' and Requested_Date='" + daten + "' ";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_paddy);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);


                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();

           
            }
            catch (Exception ex)
            {
  
            }
            finally
            {
                con_paddy.Close();
            }
        }

        # endregion

        # region Maize
        else if (Session["Commodity_Id"].ToString() == "4" || Session["Commodity_Id"].ToString() == "5" || Session["Commodity_Id"].ToString() == "6" || Session["Commodity_Id"].ToString() == "7" || Session["Commodity_Id"].ToString() == "8")
        {
            try
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }
                string str = "select *from tbl_Delete_Request where IssueCenter_ID='" + issueid + "' and  Acceptance_No='" + lbl_acceptance.Text + "' and Purchase_Center='" + hd_pcid.Value + "' and Requested_Date='" + daten + "' ";
                SqlDataAdapter daP = new SqlDataAdapter(str, con_Maze);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();

              
             




            }
            catch (Exception ex)
            {
        

            }
            finally
            {
                con_Maze.Close();
            }
        }

        # endregion

        # region Others
        else
        {

            mobj1 = new MoveChallan(ComObj);
            string qrey4 = "select *from tbl_Delete_Request where IssueCenter_ID='" + issueid + "' and  Acceptance_No='" + lbl_acceptance.Text + "' and Purchase_Center='" + hd_pcid.Value + "' and Requested_Date='" + daten + "' ";

            DataSet ds4 = mobj1.selectAny(qrey4);
            DataRow dr4 = ds4.Tables[0].Rows[0];
            grd_viewDepot.DataSource = ds4.Tables[0];
            grd_viewDepot.DataBind();

            lblsenddist.Text = dr4["district_name"].ToString();
            lblpccenter.Text = dr4["PurchaseCenterName"].ToString();
            cdate = dr4["Dispatch_Date"].ToString();
            string gdate = getdate(cdate);


        }

        # endregion

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }
    }


    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy-hh:mm tt");
    }
    public string getdateM(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}