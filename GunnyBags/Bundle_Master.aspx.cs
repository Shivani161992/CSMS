using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class GunnyBags_Bundle_Master : System.Web.UI.Page
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
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
               // GetCropYear();
               // GetDistance();
                Fillgrid();
               // bttSubmit.Visible = true;
               // bttUpdate.Enabled = true;

            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(BundleID) as BundleID from GunnyBags_BundleType where LEN(BundleID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["BundleID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "10" + "0";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strinsert = "insert into GunnyBags_BundleType(BundleID, BundleType, CreatedDate, IP) values ('" + gatepass + "','" + txtBundleType.Text + "',getdate(),'" + ip + "')";
                cmd = new SqlCommand(strinsert, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                Fillgrid();
                Response.Redirect(Request.Url.AbsoluteUri);
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

    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select BundleID, BundleType from GunnyBags_BundleType";
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

    protected void lnkdelete_Click(object sender, EventArgs e)
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            LinkButton lnkbtn = sender as LinkButton;
            //getting particular row linkbutton
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //getting userid of particular row
            int BundleID = Convert.ToInt32(GridView1.DataKeys[gvrow.RowIndex].Value.ToString());
            //string username = gvrow.Cells[0].Text;
            con.Open();

            string instr = "";

            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                              "insert into GunnyBags_BundleType_Log select * from GunnyBags_BundleType where BundleID='" + BundleID + "';";
            instr += "delete from GunnyBags_BundleType where BundleID='" + BundleID+"';";
            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
            cmd = new SqlCommand(instr, con);
            //SqlCommand cmd = new SqlCommand("delete from GunnyBags_BundleType where BundleID=" + BundleID, con);
            int result = cmd.ExecuteNonQuery();
            Fillgrid();
            //Displaying alert message after successfully deletion of user
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Deleted Successfully'); </script> ");
            con.Close();
            if (result == 1)
            {
                Fillgrid();
                //Displaying alert message after successfully deletion of user
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Deleted Successfully'); </script> ");
            }
        }
    }

    
}