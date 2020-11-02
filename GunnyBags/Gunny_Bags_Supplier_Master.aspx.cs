using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_Gunny_Bags_Supplier_Master : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    //  string strconuparjan = ConfigurationManager.ConnectionStrings["uparjan"].ConnectionString;      //uparjan
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;

    //double QtyTotal = 0;
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
                
                GetCropYear();
                Fillgrid();
               
               
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

    protected void ddlGunnyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGunnyType.SelectedIndex > 0)
        {
            GetOtherStates();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Gunny Bags type State'); </script> ");

        }
    }
    public void GetOtherStates()
    {
        
        using (con = new SqlConnection(strcon))
        {
            
               
            try
            {
                con.Open();
                string qry = "SELECT State_Code ,State_Name FROM State_Master where Status = 'Y'  order by State_Name";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSuppState.DataSource = ds.Tables[0];
                    ddlSuppState.DataTextField = "State_Name";
                    ddlSuppState.DataValueField = "State_Code";
                    ddlSuppState.DataBind();
                    ddlSuppState.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlSuppState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSuppState.SelectedIndex > 0)
        {
            GetDist();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Supplier State'); </script> ");
        
        }

    }
    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                if (ddlSuppState.SelectedValue.ToString() == "23")
                {

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

                else 
                {
                    
                    string qry = string.Format("SELECT district_code ,district_name FROM OtherState_DistrictCode where State_Id = '{0}'  order by district_name", ddlSuppState.SelectedValue.ToString());
                    da = new SqlDataAdapter(qry, con);
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
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(SupplierID) as SupplierID from GunnyBags_SupplierMaster where LEN(SupplierID)<10 ";
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
                gatepass = ds.Tables[0].Rows[0]["SupplierID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "1717" + "0";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strinsert = "insert into GunnyBags_SupplierMaster(SupplierID, CropYear, BagsType, Supplier_Name, Supplier_State, Supplier_District, CreatedDate, IP) values ('" + gatepass + "','" + ddlCropYear.SelectedValue.ToString() + "','"+ddlGunnyType.SelectedValue.ToString()+"','"+txtSupplierName.Text+"','"+ddlSuppState.SelectedValue.ToString()+"','"+Ddldist.SelectedValue.ToString()+"',getdate(),'" + ip + "')";
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

            string select = "select SupplierID, CropYear, BagsType, Supplier_Name, SM.State_Name from GunnyBags_SupplierMaster as GSM inner join State_Master as SM on SM.State_Code=GSM.Supplier_State order by Supplier_Name ";
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
            int SupplierID = Convert.ToInt32(GridView1.DataKeys[gvrow.RowIndex].Value.ToString());
            //string username = gvrow.Cells[0].Text;
            con.Open();

            string instr = "";

            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                              "insert into GunnyBags_SupplierMaster_Log select * from GunnyBags_SupplierMaster where SupplierID='" + SupplierID + "';";
            instr += "delete from GunnyBags_SupplierMaster where SupplierID='" + SupplierID + "';";
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