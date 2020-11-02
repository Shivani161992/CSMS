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
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
public partial class PaddyMilling_AcceptRegistration_OtherState : System.Web.UI.Page
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
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {

                Session["ICGBQ"] = null;


                GetCropYearValues();
                //txtdist.Text = Session["dist_name"].ToString();
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

        if (ddlCropyear.SelectedIndex > 0)
        {
            GetOtherStates();
        }
        else
        {
            return;

        }
    }

    public void GetOtherStates()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT State_Code ,State_Name FROM State_Master where Status = 'Y' and State_Code!=23 order by State_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "State_Master");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlOtherState.DataSource = ds.Tables[0];
                    ddlOtherState.DataTextField = "State_Name";
                    ddlOtherState.DataValueField = "State_Code";
                    ddlOtherState.DataBind();
                    ddlOtherState.Items.Insert(0, "--Select--");
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
    protected void ddlOtherState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOtherState.SelectedIndex > 0)
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = false;
            tr4.Visible = false;
            btnAccept.Visible = false;
            bttnSearch.Enabled = true;
        }
        else
        {
            return;

        }

       
    }

    protected void bttnSearch_Click(object sender, EventArgs e)
    {
        string select = "";
        if (ddlCropyear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Crop Year चुने|'); </script> ");
            return;
        }

        else if (ddlOtherState.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Other State का चुनाव करे|'); </script> ");
            return;
        }
        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();


                    //select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where R.Created_date between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and R.Status='0' and R.State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' order by D.Dist_name";
                    select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.district_name District,T.subdistrict_name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date From Miller_Registration_2017 R join OtherState_DistrictCode D on R.District_Code=D.district_code join Sub_Division T on R.tehsil_code=t.subdistrict_code where R.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and R.Status='0' and R.State_Code='" + ddlOtherState.SelectedValue.ToString() + "' and R.State_Code!='23' and R.Updated='Y'";


                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds, "R.Miller_Registration_2017");

                    if (ds.Tables["R.Miller_Registration_2017"].Rows.Count > 0)
                    {
                        tr1.Visible = true;
                        tr2.Visible = true;
                        tr3.Visible = true;
                        tr4.Visible = true;
                        btnAccept.Visible = true;
                        btnAccept.Enabled = true;


                        GridView1.DataSource = ds.Tables["R.Miller_Registration_2017"];
                        GridView1.DataBind();
                        //btnAccept.Enabled = btnReject.Enabled = true;
                        //btnAccept.Enabled = true;
                    }
                    else
                    {
                        //string DistCode = Session["dist_id"].ToString();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Millers are not Available in " + ddlOtherState.SelectedItem.Text + " State'); </script> ");
                        GridView1.DataSource = "";
                        GridView1.DataBind();
                        tr1.Visible = false;
                        tr2.Visible = false;
                        tr3.Visible = false;
                        tr4.Visible = false;
                        btnAccept.Visible = false;

                        btnAccept.Enabled = false;


                        // btnAccept.Enabled = btnReject.Enabled = false;
                        //btnAccept.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    GridView1.DataSource = "";
                    GridView1.DataBind();
                    //btnAccept.Enabled = btnReject.Enabled = false;
                    // btnAccept.Enabled = false;
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

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chk_select = (CheckBox)e.Row.Cells[1].FindControl("chk_select");
            CheckBox chkBxHeader = (CheckBox)this.GridView1.HeaderRow.FindControl("chkBxHeader");
            chk_select.Attributes["onclick"] = string.Format
                                                   (
                                                      "javascript:ChildClick(this,'{0}');",
                                                      chkBxHeader.ClientID
                                                   );
        }
    }


    protected void btnAccept_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox Check = (CheckBox)gr.FindControl("chk_select");
            Label Reg_Id = (Label)gr.FindControl("Label1");
            Label Miller_MobileNo = (Label)gr.FindControl("Miller_MobileNo");

            if (Check.Checked == true)
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        //string DistCode = Session["dist_id"].ToString();
                        //string browser = Request.Browser.Browser.ToString();
                        //string version = Request.Browser.Version.ToString();
                       // string useragent = Session["DistrictManager"].ToString();
                        string useragent = Session["st_Name"].ToString();
                        string Update = string.Format("Update Miller_Registration_2017 set Status='1',DM_IP_Address='{0}',DM_Current_DateTime={1},DM_User_Agent='{2}',DM_District='{3}' where Registration_ID='{4}' ", GetIp.ToString(), "GETDATE()", useragent, useragent, Reg_Id.Text);
                        //string instr = "";
                        //instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                        //              "Update Miller_Registration_2017 set Status='1',DM_IP_Address='"+GetIp.ToString()+"',DM_Current_DateTime=GETDATE() , DM_User_Agent='"+useragent+"', DM_District='"+ViewState["DistName"].ToString()+"' where Registration_ID='"+Reg_Id.Text+"' ;";

                        //instr += "Insert Into PM_RatioMaster(Mill_ID, MillerDist, CropYear, Status, Total, FDR, Checks, IP, CreatedDate, ParticularMiller, Lot_amount, Frm_Date, To_Date) values('" + Reg_Id.Text + "','" + DistCode + "','" + ddlCropyear.SelectedValue.ToString() + "','1','','','','','','','','',''); ";

                        //instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";


                        con.Open();
                        cmd = new SqlCommand(Update, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            string strMobileNo = Miller_MobileNo.Text;
                            string strMessage = "आपके धान मिलिंग का रजिस्ट्रेशन स्वीकार कर लिया गया है तथा आपका रजिस्ट्रेशन नंबर '" + Reg_Id.Text + "' हैं|";
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Registration is Accepted...'); </script> ");

                            //btnAccept.Enabled = btnReject.Enabled = false;
                            btnAccept.Enabled = false;
                            bttnSearch.Enabled = false;
                            SMS Message = new SMS();
                            Message.SendSMS(strMobileNo, strMessage);

                            //Search();

                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                        btnAccept.Enabled = false;
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
   
}