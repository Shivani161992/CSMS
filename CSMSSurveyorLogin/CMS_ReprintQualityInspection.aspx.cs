using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
public partial class CSMSSurveyorLogin_CMS_ReprintQualityInspection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    public string gatepass, InspectionID = "";
    public int getnum;
    SqlDataReader dr;
    string UserName = "";
    public string GenerateOTP = "", OTPSMS = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userGodown"] != null)
        {
            if (!IsPostBack)
            {
                UserName = Session["userGodown"].ToString();
                string samepassword = Session["NotchangePassword"].ToString();
                if (samepassword == "SMSSUR")
                {
                    Response.Redirect("~/CSMSSurveyorLogin/GodownSurveyor_ChangePassword.aspx");
                }
                else if (samepassword != "SMSSUR")
                {
                    

                    Session["ICGBQ"] = null;

                    GetGodown();

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
        }
       
    }


    public void GetGodown()
    {
        UserName = Session["userGodown"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select distinct SM.Godown, G.Godown_Name from SMSCom_SurveyorMaster as SM inner join tbl_MetaData_GODOWN as G on G.Godown_ID=SM.Godown where MobNum='" + UserName + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGodown.DataSource = ds.Tables[0];
                    ddlGodown.DataTextField = "Godown_Name";
                    ddlGodown.DataValueField = "Godown";
                    ddlGodown.DataBind();
                    ddlGodown.Items.Insert(0, "--Select--");
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
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlcommodities.Items.Clear();
        ddlTruckChallan.Items.Clear();
        txtRejectionNum.Text = "";

        if (ddlGodown.SelectedIndex > 0)
        {
            GetTruckChallan();
        }
    }
    public void GetTruckChallan()
    {
        UserName = Session["userGodown"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select TruckChalanNo from IssueToSangrahanaKendra_CSM2018 where GodownNumber='" + ddlGodown.SelectedValue.ToString() + "' and GS_Status='R'  ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlTruckChallan.DataSource = ds.Tables[0];
                    ddlTruckChallan.DataTextField = "TruckChalanNo";
                    ddlTruckChallan.DataValueField = "TruckChalanNo";
                    ddlTruckChallan.DataBind();
                    ddlTruckChallan.Items.Insert(0, "--Select--");
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
    protected void ddlTruckChallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRejectionNum.Text = "";
        if (ddlGodown.SelectedIndex > 0)
        {
            GetChallanData();


        }

    }
    public void GetChallanData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select RejectionNumber from CMS_GodownSurveyor_Inspection where TruckChallan='"+ddlTruckChallan.SelectedValue.ToString()+"' and ReceivingGodown='"+ddlGodown.SelectedValue.ToString()+"' and Status='Reject'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRejectionNum.Text = ds.Tables[0].Rows[0]["RejectionNumber"].ToString();
                    btnPrint.Enabled = true;


                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejection Number Not Available'); </script> ");
                    return;
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["RejectionNumber"] = txtRejectionNum.Text;
        Session["TruckChallan"] = ddlTruckChallan.SelectedValue.ToString();
        Session["Godown"] = ddlGodown.SelectedValue.ToString();
        Session["GodownName"] = ddlGodown.SelectedItem.Text;


        string url = "Print_GodownSurveyor_QualityInspection.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }
}