using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PaddyMilling_PM_Transporter_Rates_District : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;


    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                GetCropYear();
                GetDistance();
                Fillgrid();
                bttSubmit.Visible = true;
                bttUpdate.Enabled = true;
                label.Text = Session["dist_name"].ToString();
                string DistCode = Session["dist_id"].ToString();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }

    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    void GetDistance()
    {


        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //string DistCode = Session["dist_id"].ToString();

                string select = string.Format("Select * from dbo.Lead_Distance");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //ddllead.DataTextField = "Lead_Name";
                        //ddllead.DataValueField = "Lead_ID";
                        //ddllead.DataBind();
                        //ddllead.Items.Insert(0, "--Select--");

                        ddllead.DataSource = ds.Tables[0];
                        ddllead.DataTextField = "Lead_Name";
                        ddllead.DataValueField = "Lead_ID";
                        ddllead.DataBind();
                        ddllead.Items.Insert(0, "--Select--");


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

    public void GetData()
    {
        // string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string DistCode = Session["dist_id"].ToString();

                string select = string.Format("select RatesTrans, FCI_RatesTrans from PM_Transporter_Rates_District where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and LeadID='" + ddllead.SelectedValue.ToString() + "' and District='" + DistCode + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                //string rate;
                //rate=

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRate.Text = ds.Tables[0].Rows[0]["RatesTrans"].ToString();
                    txtFCIRate.Text = ds.Tables[0].Rows[0]["FCI_RatesTrans"].ToString();
                    bttUpdate.Enabled = true;
                    bttUpdate.Visible = true;
                    bttSubmit.Enabled = false;
                    bttSubmit.Visible = false;
                }
                else
                {
                    bttSubmit.Enabled = true;
                    bttSubmit.Visible = true;
                    bttUpdate.Enabled = false;
                    bttUpdate.Visible = false;

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
    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(TransRatesID) as TransRatesID from PM_Transporter_Rates_District where  LEN(TransRatesID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["TransRatesID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "142" + "00";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }
                string District = Session["dist_name"].ToString();

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strinsert = "insert into PM_Transporter_Rates_District( TransRatesID, CropYear, LeadID, RatesTrans, FCI_RatesTrans,  CreatedDate, IP, District) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddllead.SelectedValue.ToString() + "','" + txtRate.Text + "','"+txtFCIRate.Text+"',getdate(),'" + ip + "','" + DistCode + "')";
                cmd = new SqlCommand(strinsert, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                Fillgrid();
                ddlCropYear.ClearSelection();
                ddllead.ClearSelection();
                txtRate.Text = "";
                txtFCIRate.Text = "";
                //Response.Redirect(Request.Url.AbsoluteUri);
                //ddl_Depart.Enabled = false;


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

        //using (con = new SqlConnection(strcon))
        //    try
        //    {

        //        con.Open();
        //        string depart = "";
        //        string Postheld = "";
        //        string postingPlace = "";
        //        string qry = "";
        //        qry = "select Depart_ID, PostHeld, Posting_Place from PM_Stan_Comm_Trans_Rates where inspector_ID='" + Inspctr_name_ddl.SelectedValue.ToString() + "'";
        //        da = new SqlDataAdapter(qry, con);
        //        ds = new DataSet();
        //        da.Fill(ds, "PM_InsName_Team_Mas");
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            depart = ds.Tables[0].Rows[0][0].ToString();
        //            Postheld = ds.Tables[0].Rows[0][1].ToString();
        //            postingPlace = ds.Tables[0].Rows[0][2].ToString();
        //        }





        //    }
        //    catch
        //    {
        //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
        //    }

        //    finally
        //    {
        //        if (con.State != ConnectionState.Closed)
        //        {
        //            con.Close();
        //        }
        //    }
        // Fillgrid();

    }

    void Fillgrid()
    {

        string DistCode = Session["dist_id"].ToString();

        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select D.district_name,  CropYear, LeadID, LD.Lead_Name, RatesTrans, FCI_RatesTrans from PM_Transporter_Rates_District as SC inner join Lead_Distance as LD on LD.Lead_ID=SC.LeadID inner join pds.districtsmp as D on D.district_code=SC.District where District='" + DistCode + "' order by Lead_ID";
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
    protected void bttUpdate_Click(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();

                string instr = "";

                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                  "insert into PM_Transporter_Rates_District_Log select * from PM_Transporter_Rates_District where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and LeadID='" + ddllead.SelectedValue.ToString() + "' and District='" + DistCode + "';";
                instr += "Update PM_Transporter_Rates_District set RatesTrans='" + txtRate.Text + "', FCI_RatesTrans='"+txtFCIRate.Text+"' where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and LeadID='" + ddllead.SelectedValue.ToString() + "' and District='" + DistCode + "';";
                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                cmd = new SqlCommand(instr, con);
                int count = cmd.ExecuteNonQuery();



                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Updated successfully'); </script> ");
                Fillgrid();
                ddlCropYear.ClearSelection();
                ddllead.ClearSelection();
                txtRate.Text = "";

               // Response.Redirect(Request.Url.AbsoluteUri);
                //ddl_Depart.Enabled = false;


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
    protected void ddllead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllead.SelectedIndex > 0)
        {
            txtRate.Text = "";
            txtFCIRate.Text = "";
            GetData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Lead(Distance)'); </script> ");

        }
    }
}