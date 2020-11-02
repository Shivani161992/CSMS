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


public partial class State_PM_InsName_Team_Mas : System.Web.UI.Page
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
            GetDepart();
            GetDist();
            //Label3.Text = "Inspector Name Master";
            table1.Visible = false;
            //GetMPIssueCentre();
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
    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select Inspector_Name, IT.Depart_ID, PostHeld, Posting_Place, district_name, Department_Name from PM_InsName_Team_Mas as IT inner join pds.districtsmp as D on D.district_code=IT.Posting_Place  inner join PM_Depart_Master as DM on DM.Depart_ID=IT.Depart_ID ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Inspname.Enabled = true;
        txt_PH.Enabled = true;
        Ddldepart.Enabled = true;
        Ddldist.Enabled = true;
        Bttupdate.Enabled = true;
        Bttadd.Enabled = false;
        table1.Visible = true;
        txt_Inspname.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
        txt_PH.Text = GridView1.SelectedRow.Cells[4].Text.Trim();
        Ddldist.SelectedValue = GridView1.SelectedRow.Cells[6].Text;
        Ddldepart.SelectedValue = GridView1.SelectedRow.Cells[3].Text;

    }
    public void GetDepart()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT Depart_ID,Department_Name FROM PM_Depart_Master order By Department_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Ddldepart.DataSource = ds.Tables[0];
                        Ddldepart.DataTextField = "Department_Name";
                        Ddldepart.DataValueField = "Depart_ID";
                        Ddldepart.DataBind();
                        Ddldepart.Items.Insert(0, "--Select--");
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(inspector_ID) as inspector_ID  from PM_InsName_Team_Mas where  LEN(inspector_ID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["inspector_ID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "17" + "0001";
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
                string strselect = "insert into PM_InsName_Team_Mas( inspector_ID, Inspector_Name, Depart_ID, PostHeld, Posting_Place, IP, Created_Date) values ('" + gatepass + "','" + txt_Inspname.Text + "','" + Ddldepart.SelectedValue.ToString() + "','" + txt_PH.Text + "','" + Ddldist.SelectedValue.ToString() + "','" + ip + "' ,getdate())";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                txt_Inspname.Enabled = false;
                txt_PH.Enabled = false;
                Ddldepart.Enabled = false;
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
        txt_Inspname.Text = "";
        txt_PH.Text = "";
       
    }
    protected void Ddldepart_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txt_Inspname.Enabled = true;
        txt_PH.Enabled = true;
        Ddldepart.Enabled = true;
        Ddldist.Enabled = true;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                //string districtid = Session["dist_id"].ToString();
                con.Open();

                string select = "";
                select = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                select += "insert into PM_InsName_Team_Mas_log select * from PM_InsName_Team_Mas where Inspector_Name='" + txt_Inspname.Text + "'";
                select += "update PM_InsName_Team_Mas set Depart_ID='" + Ddldepart.SelectedValue.ToString() + "', PostHeld='" + txt_PH.Text + "', Posting_Place='" + Ddldist.SelectedValue.ToString() + "'   Where Inspector_Name='" + txt_Inspname.Text + "'";
                select += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                cmd = new SqlCommand(select, con);
                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    //btnRecptSubmit.Enabled = txtOpeningBal.Enabled = txtBags.Enabled = false;

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Is Saved Successfully'); </script> ");
                    Fillgrid();
                    //txt_dist.Enabled = false;
                   // btnrecptNew.Enabled = true;
                    txt_Inspname.Enabled = false;
                    txt_PH.Enabled = false;
                    Ddldepart.Enabled = false;
                    Ddldist.Enabled = false;


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
    protected void Button3_Click(object sender, EventArgs e)
    {
        table1.Visible = true;
        GetDist();
        GetDepart();


        Bttupdate.Enabled = false;
        Bttadd.Enabled = true;
        txt_Inspname.Enabled = true;
        txt_PH.Enabled = true;
        Ddldepart.Enabled = true;
        Ddldist.Enabled = true;
        txt_Inspname.Text = "";
        txt_PH.Text = "";
       
    }
    protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
}