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


public partial class PaddyMilling_PM_Update_MillerRegistration : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1, daI, daCMR, daSMO;
    DataSet ds, ds1, dsI, dsCMR, dsSMO;
    string SMO;
    string millerDist;

    //string Gdistance = "";
    //string Mdistance = "";


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

                Session["ICGBQ"] = null;


                GetCropYearValues();
                txtdist.Text = Session["dist_name"].ToString();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());


            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropyear.DataSource = ds.Tables[0];
                    ddlCropyear.DataTextField = "CropYear";
                    ddlCropyear.DataValueField = "CropYear";
                    ddlCropyear.DataBind();
                    ddlCropyear.Items.Insert(0, "--Select--");
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
    protected void ddlCropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        txtarwa.Text = "";
        txtushna.Text = "";
        txtqty.Text = "";
        if (ddlCropyear.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            return;

        }
    }

    public void GetMillName()
    {


        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string Dist = Session["dist_id"].ToString();
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select mill_name, Registration_ID from Miller_Registration_2017 where District_Code='" + Dist + "' and Status='0' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "'order by mill_name Asc";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "mill_name";
                    ddlMillName.DataValueField = "Registration_ID";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने पंजीकरण नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlMillName.SelectedIndex > 0)
        {
            GetMillData();
        }
        else
        {
            return;

        }

    }
    public void GetMillData()
    {
        string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select milling_capacity_arwa, milling_capacity_usna, upcoming_SixMonths, VacantCap, TotalVacantCap from Miller_Registration_2017 where District_Code='" + DistCode + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Status='0' and Registration_ID='" + ddlMillName.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "Miller_Registration_2017");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtarwa.Enabled = true;
                    txtushna.Enabled = true;
                    txtqty.Enabled = true;
                    txtTotalVacantCap.Enabled = true;
                    txtVacantCap.Enabled = true;
                    txtarwa.Text = ds.Tables[0].Rows[0]["milling_capacity_arwa"].ToString();
                    txtushna.Text = ds.Tables[0].Rows[0]["milling_capacity_usna"].ToString();
                    txtqty.Text = ds.Tables[0].Rows[0]["upcoming_SixMonths"].ToString();
                    txtTotalVacantCap.Text = ds.Tables[0].Rows[0]["TotalVacantCap"].ToString();
                    txtVacantCap.Text = ds.Tables[0].Rows[0]["VacantCap"].ToString();
                    bttnupdate.Enabled = true;


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

    protected void bttnupdate_Click(object sender, EventArgs e)
    {
        if (ddlCropyear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Crop Year चुने|'); </script> ");
            return;
        }
        else if (ddlMillName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिलर का नाम  चुने|'); </script> ");
            return;
        }
        else if (txtarwa.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलिंग कपैसिटी अरवा भरें|'); </script> ");
            return;
        }
        else if (txtushna.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलिंग कपैसिटी उष्ण भरें|'); </script> ");
            return;
        }
        else if (txtqty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आगामी 16/01/2018 से 30/06/2018 तक कितनी मात्रा की मिलिंग कर सकेंगे (in MT)'); </script> ");
            return;
        }
        else if (txtTotalVacantCap.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कुल भण्डारण शम्ता भरें|'); </script> ");
            return;
        }
        else if (txtVacantCap.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('15/1/2018 को मिलर्स रिक्त भण्डारण शम्ता भरें|'); </script> ");
            return;
        }
        else
        {
            string districtid = Session["dist_id"].ToString();
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    //string Updated = "";

                    string instr = "";
                    {

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                    "insert into Miller_Registration_Log_2017 select  District_Code, tehsil_code, Registration_ID, Mill_Name, mill_pincode, mill_Address, mill_phone, mill_ownership, mill_proto_name, mill_proto_address, mill_proto_city, firm_type, operator_name, operator_father, partner1, partner2, pratner3, partner4, Operator_permanent_add, Operator_telephone1, Operator_telephone2, Operator_telephone3, Operator_telephone4, mill_running_status, mill_ownership_status, leez_mill_ownername, leez_mill_owneraddress, leez_mill_enddate, milling_capacity_arwa, milling_capacity_usna, salstax_no, alloted_servicetaxno, current_servicetax, mandipra_lisance, mandivyapar_lisance, mill_akshansh, mill_deshant, ic1, distance1, ic2, distance2, ic3, distance3, ic4, distance4, last_yearmillng_qunt, reprenstaive_name, id_card, id_address, Created_date, ip, firmliasnce, millpassword, Pan_No, Status, DM_IP_Address, DM_Current_DateTime, DM_User_Agent, CropYear, DM_District, State, State_Code, Miller_MobileNo, AprUnit, AprEmp, AprShift, MayUnit, MayEmp, MayShift, JuneUnit, JuneEmp, JuneShift, JulyUnit, JulyEmp, JulyShift, AugUnit, AugEmp, AugShift, SepUnit, SepEmp, SepShift, CropYear_Shift, Black_listed, upcoming_SixMonths, Total_AgreeQty, Total_BRLQty_Insp, Upgraded_BRLQty, Remain_qty, Aadhar_number, Did_millinglastyear, OldGunnybags, VacantCap, TotalVacantCap, Updated from Miller_Registration_2017 where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Registration_ID='" + ddlMillName.SelectedValue.ToString() + "' and District_Code='" + districtid + "'; ";

                        instr += "update Miller_Registration_2017 set milling_capacity_arwa='" + txtarwa.Text + "', milling_capacity_usna='" + txtushna.Text + "', upcoming_SixMonths='" + txtqty.Text + "', VacantCap='" + txtVacantCap.Text + "', TotalVacantCap='" + txtTotalVacantCap.Text + "', Updated='Y' where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Registration_ID='" + ddlMillName.SelectedValue.ToString() + "' and District_Code='" + districtid + "'; ";

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                    }

                    cmd = new SqlCommand(instr, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {



                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated Successfully '); </script> ");
                        txtarwa.Enabled = false;
                        txtushna.Enabled = false;
                        txtqty.Enabled = false;
                        txtTotalVacantCap.Enabled = false;
                        txtVacantCap.Enabled = false;
                        ddlCropyear.Enabled = false;
                        ddlMillName.Enabled = false;
                        bttnupdate.Enabled = false;


                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
    }
}