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

public partial class State_Godown_Distance : System.Web.UI.Page
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


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // string districtid = Session["dist_id"].ToString();
            Fillgrid();
            GetDist();
            //GetMPIssueCentre();
        }


        //GetDist();
        //Fillgrid();
    }


    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "  select ID,  district_name, district_code, Max_distance from Godown_Distance_Master as GM  inner join pds.districtsmp as D on D.district_code=GM.district ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
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
                        Ddldist.DataSource = ds.Tables[0];
                        Ddldist.DataTextField = "district_name";
                        Ddldist.DataValueField = "district_code";
                        Ddldist.DataBind();
                        Ddldist.Items.Insert(0, "--Select--");
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnrecptupdate.Enabled = true;
        btnRecptSave.Enabled = false;
        txt_dist.Enabled = true;
        Ddldist.Enabled = true;
        txt_dist.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
       // txt_dist.Text = GridView1.SelectedRow.Cells[1].Text;
        Ddldist.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
        //Ddldist.SelectedValue = GridView1.DataKeys[GridView1.SelectedIndex].Values["district_name"].ToString();
        //using (con = new SqlConnection(strcon))
            //try
          //  {

              //  con.Open();
                //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
               // string strselect = "insert into Godown_Distance_Master(ID, district, distance, IP, Created_Date) values ('" + gatepass + "','" + Ddldist.SelectedValue.ToString() + "','" + txt_dist.Text + "','" + ip + "',getdate())";
               // cmd = new SqlCommand(strselect, con);
                //string check = (string)cmd.ExecuteScalar();

               // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
        
          //  }
            //catch
            //{
               // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
           // }

           // finally
           // {
           //     if (con.State != ConnectionState.Closed)
               // {
               //     con.Close();
               // }
           // }
    }
    protected void btnRecptSave_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(ID) as ID from Godown_Distance_Master where  LEN(ID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["ID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "17" + "001";
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
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into Godown_Distance_Master(ID, district, Max_distance, IP, Created_Date) values ('" + gatepass + "','" + Ddldist.SelectedValue.ToString() + "','" + txt_dist.Text + "','" + ip + "',getdate())";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                btnRecptSave.Enabled = false;
                btnrecptNew.Enabled = true;
                txt_dist.Enabled = false;
                Ddldist.Enabled = false;
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
       // Ddldist.Items.Clear();
       // ddlIC.Items.Clear();
        //txt_name.Text = "";
        //txt_desig.Text = "";
       // GetDist();
        //GetMPIssueCentre();
    }


    protected void btnRecptUpdate_Click(object sender, EventArgs e)
    {
        Ddldist.Enabled = false;
         using (con = new SqlConnection(strcon))
        {
            try
            {
                //string districtid = Session["dist_id"].ToString();
                con.Open();

                string select = "";
                select = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                select += "insert into Godown_Distance_Master_log_2017 select * from Godown_Distance_Master where district='" + Ddldist.SelectedValue.ToString() + "'";
                select += "update Godown_Distance_Master set  Max_distance='" + txt_dist.Text + "'  Where district='" + Ddldist.SelectedValue.ToString() + "'";
                select += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                cmd = new SqlCommand(select, con);
                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    //btnRecptSubmit.Enabled = txtOpeningBal.Enabled = txtBags.Enabled = false;

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Saved Successfully'); </script> ");
                    Fillgrid();
                    txt_dist.Enabled = false;
                    btnrecptNew.Enabled = true;

                    //Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    //Label2.Visible = true;
                    //Label2.Text = "Data Is Saved Successfully";
                    //btnRecptUpdate.Enabled = true;
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
        if (Ddldist.SelectedIndex > 0)
        {
            //GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया जिला चुनें|'); </script> ");
        }
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Ddldist.Enabled = true;
        txt_dist.Enabled = true;
        btnRecptSave.Enabled = true;
        btnrecptupdate.Enabled = false;
        GetDist();
        txt_dist.Text = "";
    }
}