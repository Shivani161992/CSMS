using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;


public partial class State_Inspector_Master_Team : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ddlCropYear.Items.Insert(0, "--Select--");
            ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
            ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
            Fillgrid();
            add_new_btn.Enabled = false;
           // Bttupdate.Enabled = false;
           // GetDepart();
            //GetTeamName();
           // GetInspectorName();
            //  ViewState["User_Name"] = Session["st_Name"].ToString();

            // GETLotNo();
            //GETCMRPercentNo();
        }
    }
    
    protected void add_new_btn_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
        submit.Enabled = true;
       
    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    //txt_Inspname.Enabled = true;
    //    // txt_PH.Enabled = true;
    //    // Ddldepart.Enabled = true;
    //    // Ddldist.Enabled = true;

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            //string districtid = Session["dist_id"].ToString();
    //            con.Open();

    //            string select = "";
    //            select = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

    //            select += "insert into Inspector_Master_Team_Log select * from Inspector_Master_Team where Inspector_Name='" + Inspctr_name_ddl.SelectedValue.ToString() + "'";
    //            select += "update Inspector_Master_Team set Inspector_Name='" + Inspctr_name_ddl.SelectedValue.ToString() + "'   Where GroupName='" + grp_name_ddl.SelectedValue.ToString() + "'";
    //            select += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
    //            cmd = new SqlCommand(select, con);
    //            int count = cmd.ExecuteNonQuery();

    //            if (count > 0)
    //            {
    //                //btnRecptSubmit.Enabled = txtOpeningBal.Enabled = txtBags.Enabled = false;

    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Saved Successfully'); </script> ");
    //                add_new_btn.Enabled = false;
    //               // Fillgrid();
    //                //txt_dist.Enabled = false;
    //                // btnrecptNew.Enabled = true;
    //                //txt_Inspname.Enabled = false;
    //                //txt_PH.Enabled = false;
    //                // Ddldepart.Enabled = false;
    //                // Ddldist.Enabled = false;


    //                //Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

    //                //Label2.Visible = true;
    //                //Label2.Text = "Data Is Saved Successfully";
    //                //btnRecptUpdate.Enabled = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //    Fillgrid();

    //}
    protected void submit_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(GRP_ID) as GRP_ID from Inspector_Master_Team where  LEN(GRP_ID)<8 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);
                //mobj1 = new MoveChallan(ComObj);
                //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                //DataSet ds = new DataSet();
                // dmax.Fill(ds);
                // DataTable dt = ds.Tables[""];
                DataRow dr = ds.Tables[0].Rows[0];
                //gatepass = dr["Inspector_ID"].ToString();
                gatepass = ds.Tables[0].Rows[0]["GRP_ID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "11" + "00";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {

                con.Open();
                 string depart="";
                string Postheld="";
                string postingPlace="";
                string qry="";
                qry = "select Depart_ID, PostHeld, Posting_Place from PM_InsName_Team_Mas where inspector_ID='" + Inspctr_name_ddl.SelectedValue.ToString() + "'";
                    da = new SqlDataAdapter(qry, con);
                    ds = new DataSet();
                    da.Fill(ds, "PM_InsName_Team_Mas");
                 if (ds.Tables[0].Rows.Count > 0)
                    {
                     depart = ds.Tables[0].Rows[0][0].ToString();
                     Postheld= ds.Tables[0].Rows[0][1].ToString();
                     postingPlace=ds.Tables[0].Rows[0][2].ToString();
                 }


                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strinsert = "insert into Inspector_Master_Team( GRP_ID, CropYear,Team_ID, Depart_ID, Inspector_ID, IP, Created_Date, PostHeld, Posting_Place,Season) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + grp_name_ddl.SelectedValue.ToString() + "','" + depart + "','" + Inspctr_name_ddl.SelectedValue.ToString() + "','" + ip + "' ,getdate(),'" + Postheld + "','" + postingPlace + "','" + commo_ddl.SelectedValue.ToString() + "')";
                cmd = new SqlCommand(strinsert, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                //ddl_Depart.Enabled = false;
                add_new_btn.Enabled = true;
                submit.Enabled = false;
                ddlCropYear.Enabled = false;
                Inspctr_name_ddl.Enabled = false;
                grp_name_ddl.Enabled = false;
                commo_ddl.Enabled = false;
                
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
        

    }

    public void GetInspectorDetails()
    {
         using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select Depart_ID, PostHeld, Posting_Place from PM_InsName_Team_Mas";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

               
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

    public void GetTeamName()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT TeamID,TeamName FROM PM_Ins_GroupName_Master where Season='" + commo_ddl.SelectedValue.ToString() + "' order By TeamName ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        grp_name_ddl.DataSource = ds.Tables[0];
                        grp_name_ddl.DataTextField = "TeamName";
                        grp_name_ddl.DataValueField = "TeamID";
                        grp_name_ddl.DataBind();
                        grp_name_ddl.Items.Insert(0, "--Select--");
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
    public void GetInspectorName()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT inspector_ID,Inspector_Name FROM PM_InsName_Team_Mas order By Inspector_Name ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Inspctr_name_ddl.DataSource = ds.Tables[0];
                        Inspctr_name_ddl.DataTextField = "Inspector_Name";
                        Inspctr_name_ddl.DataValueField = "inspector_ID";
                        Inspctr_name_ddl.DataBind();
                        Inspctr_name_ddl.Items.Insert(0, "--Select--");
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

    //public void GetDepart()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            string select = "";
    //            select = "SELECT Depart_ID,Department_Name FROM PM_Depart_Master order By Department_Name";
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddl_Depart.DataSource = ds.Tables[0];
    //                    ddl_Depart.DataTextField = "Department_Name";
    //                    ddl_Depart.DataValueField = "Depart_ID";
    //                    ddl_Depart.DataBind();
    //                    ddl_Depart.Items.Insert(0, "--Select--");
    //                    //Ddldist.SelectedValue = Session["dist_id"].ToString();
    //                    // GetMPIssueCentre();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}

    void Fillgrid()
    {


       DataSet ds = new DataSet();
       using (SqlConnection con = new SqlConnection(strcon))
        {
           con.Open();

           string select = " select GN.[TeamName],GN.TeamID,IT.[Season],INT.[Inspector_Name],INT.inspector_ID, Department_Name,DM.Depart_ID, IT.[PostHeld],district_name,MP.district_code from Inspector_Master_Team as IT inner join PM_Ins_GroupName_Master as GN on GN.TeamID=IT.Team_ID inner join PM_InsName_Team_Mas as INT on INT.inspector_ID=IT.Inspector_ID inner join PM_Depart_Master as DM on DM.Depart_ID=IT.Depart_ID inner join pds.districtsmp as MP on MP.district_code=IT.Posting_Place";
          // GridView1.SelectedRow.Cells[5].Text = "Select PostHeld from Inspector_Master_Team where Inspector_Name="+Inspctr_name_ddl.SelectedValue.ToString();
          // GridView1.SelectedRow.Cells[6].Text = "Select Posting_Place from Inspector_Master_Team where Inspector_Name=" + Inspctr_name_ddl.SelectedValue.ToString();
           SqlCommand cmd = new SqlCommand(select, con);
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           da.Fill(ds);
          con.Close();
          GridView1.DataSource = ds;
           GridView1.DataBind();
      }
    }
   
    protected void grp_name_ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (commo_ddl.SelectedIndex > 0)
        {
            GetInspectorName();
        }
    }
    protected void commo_ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (commo_ddl.SelectedIndex > 0)
        {
            GetTeamName();
        }
    }
}