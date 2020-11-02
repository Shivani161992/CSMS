using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccess;
using Data;

public partial class State_PM_BlackListedMiller : System.Web.UI.Page
{

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    // protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ddlCropYear.Items.Insert(0, "--Select--");
            ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
            ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
            GetDist();
           // ViewState["User_Name"] = Session["st_Name"].ToString();

            Fillgrid();
            //GETLotNo();
           // GETCMRPercentNo();
        }
    }

    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddldist.DataSource = ds.Tables[0];
                        ddldist.DataTextField = "district_name";
                        ddldist.DataValueField = "district_code";
                        ddldist.DataBind();
                        ddldist.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
                    }
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

    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "  select Mill_Name ,district_name from Miller_Registration_2017 AS MR inner join pds.districtsmp as MP on MP.district_code=MR.District_Code where Black_listed='Y'";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    public void GetMillName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select Mill_Name ,Registration_ID from Miller_Registration_2017 where District_Code='" + ddldist.SelectedValue.ToString() + "' and  Status='1' order by Mill_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMN.DataSource = ds.Tables[0];
                        ddlMN.DataTextField = "Mill_Name";
                        ddlMN.DataValueField = "Registration_ID";
                        ddlMN.DataBind();
                        ddlMN.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
                    }
                }
                else 
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस जिले में कोई भी मिलर उपलभ्ध नहीं है'); </script> ");
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
    protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldist.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }
    protected void ddlMN_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Bttadd_click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {

                con.Open();
                //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string select = "";
                select = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                select += "  insert into Miller_Registration_Log_2017 select * from Miller_Registration_2017";
                select += " update Miller_Registration_2017 set Black_listed='"+ddlBL.SelectedValue.ToString() +"' where Registration_ID='"+ddlMN.SelectedValue.ToString()+"'";
                select += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                cmd = new SqlCommand(select, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                //txt_Inspname.Enabled = false;
                //txt_PH.Enabled = false;
               // Ddldepart.Enabled = false;
               // Ddldist.Enabled = false;


            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        Fillgrid();
        //txt_Inspname.Text = "";
        //txt_PH.Text = "";
       
    
    }

   
}
