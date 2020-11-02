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

public partial class PaddyMilling_AcceptRegistration : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    static HttpWebRequest request = null;
    static Stream dataStream;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_name"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["DistName"] = Session["dist_name"].ToString();
                GetCropYearValues();
                GetHindiDistName();
                rbMPState.Checked = true;
               // GetDistName();
                txtdist.Text = Session["dist_name"].ToString();
                ViewState["CountMPDistrict"] = "0";
                ViewState["CountOtherDistrict"] = "0";
            }

            //if (rbMPState.Checked)
            //{
            //    if (ViewState["CountMPDistrict"].ToString() == "0")
            //    {
            //        ddlOtherStates.Items.Clear();
            //        GetDistName();
            //        ViewState["CountMPDistrict"] = "1";
            //        ViewState["CountOtherDistrict"] = "0";
            //    }
            //}
            //else if (rbOtherState.Checked)
            //{
            //    if (ViewState["CountOtherDistrict"].ToString() == "0")
            //    {
            //        GetOtherStates();
            //        ViewState["CountOtherDistrict"] = "1";
            //        ViewState["CountMPDistrict"] = "0";
            //    }
            //}
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetCropYearValues()
    {
        ddlCropyear.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetHindiDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = string.Format("select Dist_name from pds.districtsmp where district_name = '{0}'", ViewState["DistName"]);
                con.Open();
                cmd = new SqlCommand(select, con);
                ViewState["H_DistName"] = cmd.ExecuteScalar();
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
    //public void GetOtherStates()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = string.Format("SELECT State_Code ,State_Name FROM State_Master where Status = 'Y' and State_Code!=23 order by State_Name");
    //            da = new SqlDataAdapter(select, con);

    //            ds = new DataSet();
    //            da.Fill(ds, "State_Master");
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlOtherStates.DataSource = ds.Tables[0];
    //                ddlOtherStates.DataTextField = "State_Name";
    //                ddlOtherStates.DataValueField = "State_Code";
    //                ddlOtherStates.DataBind();
    //                ddlOtherStates.Items.Insert(0, "--Select--");
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

    //public void GetDistName()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlMPDist.DataSource = ds.Tables[0];
    //                    ddlMPDist.DataTextField = "district_name";
    //                    ddlMPDist.DataValueField = "district_code";
    //                    ddlMPDist.DataBind();
    //                    ddlMPDist.Items.Insert(0, "--Select--");
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

    public void Search()
    {
        if (ddlCropyear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Crop Year चुने|'); </script> ");
            return;
        }
        //RequiredFieldValidator5.Visible = RequiredFieldValidator6.Visible = false;
        if (rbMPState.Checked)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    //string fromdate = Request.Form[txtFromDate.UniqueID];
                    //txtFromDate.Text = fromdate;
                    //string todate = Request.Form[txtToDate.UniqueID];
                    //txtToDate.Text = todate;

                    //ConvertServerDate ServerDate = new ConvertServerDate();
                    //string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
                    //string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

                    string select = "";
                    // Search Only Login District Miller
                    //string select = string.Format("Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where (R.Created_date between '{0} 00:00:00' and '{1} 23:59:59') and R.Status='0' and R.District_Code='{2}' order by R.Mill_Name", ConvertFromDate, ConvertToDate, ViewState["DistCode"].ToString());

                    // Search All District Miller
                    //string select = string.Format("Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where (R.Created_date between '{0} 00:00:00' and '{1} 23:59:59') and R.Status='0' order by D.Dist_name", ConvertFromDate, ConvertToDate);

                    if (rbMPState.Checked)
                    {
                        if (txtdist.Text=="")
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया M.P State के जिले का चुनाव करे|'); </script> ");
                            return;
                        }
                        else
                        {
                            string DistCode = Session["dist_id"].ToString();
                           
                            //select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where R.Created_date between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59'  and R.Status='0' and R.District_Code='" + ddlMPDist.SelectedValue.ToString() + "' order by D.Dist_name";
                            
                            select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where R.CropYear='" + ddlCropyear.SelectedValue.ToString() + "'  and R.Status='0' and R.District_Code='" + DistCode + "' and Updated='Y' order by D.Dist_name";
                       
                        }
                    }
                    //else if (rbOtherState.Checked)
                    //{
                    //    if (ddlOtherStates.SelectedIndex <= 0)
                    //    {
                    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Other State का चुनाव करे|'); </script> ");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        //select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where R.Created_date between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and R.Status='0' and R.State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' order by D.Dist_name";
                    //        select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.Dist_name District,T.Tehsil_Name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date from Miller_Registration_2017 R Join pds.districtsmp D on R.District_Code=D.district_code join pds.Tehsilmp T on R.tehsil_code = T.TehsilCode where R.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and R.Status='0' and R.State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' order by D.Dist_name";
                    //    }
                    //}

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds, "R.Miller_Registration_2017");

                    if (ds.Tables["R.Miller_Registration_2017"].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables["R.Miller_Registration_2017"];
                        GridView1.DataBind();
                        //btnAccept.Enabled = btnReject.Enabled = true;
                        btnAccept.Enabled = true;
                    }
                    else
                    {
                        string DistCode = Session["dist_id"].ToString();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Millers Is Not Available on " + DistCode + " District'); </script> ");
                        GridView1.DataSource = "";
                        GridView1.DataBind();
                        // btnAccept.Enabled = btnReject.Enabled = false;
                        btnAccept.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    GridView1.DataSource = "";
                    GridView1.DataBind();
                    //btnAccept.Enabled = btnReject.Enabled = false;
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

        //else if (rbOtherState.Checked)
        //{
        //    if (ddlOtherStates.SelectedIndex > 0)
        //    {
        //        using (con = new SqlConnection(strcon))
        //        {
        //            try
        //            {
        //                con.Open();
        //                //string fromdate = Request.Form[txtFromDate.UniqueID];
        //                //txtFromDate.Text = fromdate;
        //                //string todate = Request.Form[txtToDate.UniqueID];
        //                //txtToDate.Text = todate;

        //                //ConvertServerDate ServerDate = new ConvertServerDate();
        //                //string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
        //                //string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

        //                string select = "";
        //                //select = string.Format("Select R.Registration_ID,R.Mill_Name,R.operator_name,D.district_name District,T.subdistrict_name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date From Miller_Registration_2017 R join OtherState_DistrictCode D on R.District_Code=D.district_code join Sub_Division T on R.tehsil_code=t.subdistrict_code where (R.Created_date between '{0} 00:00:00' and '{1} 23:59:59') and R.Status='0' and R.State_Code='{2}' and R.State_Code!='23'", ConvertFromDate, ConvertToDate, ddlOtherStates.SelectedValue.ToString());
        //                select = "Select R.Registration_ID,R.Mill_Name,R.operator_name,D.district_name District,T.subdistrict_name Tehsil,R.Miller_MobileNo,R.milling_capacity_arwa,R.milling_capacity_usna,R.mill_phone,R.Created_date From Miller_Registration_2017 R join OtherState_DistrictCode D on R.District_Code=D.district_code join Sub_Division T on R.tehsil_code=t.subdistrict_code where R.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and R.Status='0' and R.State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' and R.State_Code!='23'";

        //                da = new SqlDataAdapter(select, con);
        //                ds = new DataSet();
        //                da.Fill(ds, "R.Miller_Registration_2017");

        //                if (ds.Tables["R.Miller_Registration_2017"].Rows.Count > 0)
        //                {
        //                    GridView1.DataSource = ds.Tables["R.Miller_Registration_2017"];
        //                    GridView1.DataBind();
        //                    //btnAccept.Enabled = btnReject.Enabled = true;
        //                    btnAccept.Enabled = true;
        //                }
        //                else
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Millers Is Not Available on " + ddlOtherStates.SelectedItem.ToString() + " State'); </script> ");
        //                    GridView1.DataSource = "";
        //                    GridView1.DataBind();
        //                    //btnAccept.Enabled = btnReject.Enabled = false;
        //                    btnAccept.Enabled = false;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        //                GridView1.DataSource = "";
        //                GridView1.DataBind();
        //                //btnAccept.Enabled = btnReject.Enabled = false;
        //                btnAccept.Enabled = false;
        //            }

        //            finally
        //            {
        //                if (con.State != ConnectionState.Closed)
        //                {
        //                    con.Close();
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया राज्य चुनें|'); </script> ");
        //    }
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
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
                        string DistCode = Session["dist_id"].ToString();
                       
                        string useragent = Session["DistrictManager"].ToString();

                        string Update = string.Format("Update Miller_Registration_2017 set Status='1',DM_IP_Address='{0}',DM_Current_DateTime={1},DM_User_Agent='{2}',DM_District='{3}' where Registration_ID='{4}' ", GetIp.ToString(), "GETDATE()", useragent, ViewState["DistName"].ToString(), Reg_Id.Text);
                       

                        con.Open();
                        cmd = new SqlCommand(Update, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            string strMobileNo = Miller_MobileNo.Text;
                            string strMessage = "आपके धान मिलिंग का रजिस्ट्रेशन स्वीकार कर लिया गया है तथा आपका रजिस्ट्रेशन नंबर '" + Reg_Id.Text + "' हैं| आप धान मिलिंग के अनुबंध के लिए जिला कार्यालय '" + ViewState["H_DistName"] + "' से संपर्क करें|";
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Registration is Accepted...'); </script> ");

                            //btnAccept.Enabled = btnReject.Enabled = false;
                            btnAccept.Enabled = false;
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

    //protected void btnReject_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow gr in GridView1.Rows)
    //    {
    //        CheckBox Check = (CheckBox)gr.FindControl("chk_select");
    //        Label Reg_Id = (Label)gr.FindControl("Label1");
    //        Label Miller_MobileNo = (Label)gr.FindControl("Miller_MobileNo");

    //        if (Check.Checked == true)
    //        {
    //            using (con = new SqlConnection(strcon))
    //            {
    //                try
    //                {
    //                    ClientIP objClientIP = new ClientIP();
    //                    string GetIp = (objClientIP.GETIP() + "  " + objClientIP.GETHOST());

    //                    string browser = Request.Browser.Browser.ToString();
    //                    string version = Request.Browser.Version.ToString();
    //                    string useragent = browser + version;

    //                    string Update = string.Format("Update Miller_Registration_2017 set Status='2',DM_IP_Address='{0}',DM_Current_DateTime={1},DM_User_Agent='{2}',DM_District='{3}' where Registration_ID='{4}' ", GetIp.ToString(), "GETDATE()", useragent, ViewState["DistName"].ToString(), Reg_Id.Text);

    //                    con.Open();
    //                    cmd = new SqlCommand(Update, con);
    //                    int count = cmd.ExecuteNonQuery();

    //                    if (count > 0)
    //                    {
    //                        string strMobileNo = Miller_MobileNo.Text;
    //                        string strMessage = "आपके धान मिलिंग का रजिस्ट्रेशन अस्वीकार किया गया हैं| अधिक जानकारी के लिए जिला कार्यालय '" + ViewState["H_DistName"] + "' से संपर्क करें|";
    //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Registration is Rejected...'); </script> ");

    //                        btnAccept.Enabled = btnReject.Enabled = false;
    //                        SMS Message = new SMS();
    //                        Message.SendSMS(strMobileNo, strMessage);

    //                        Search();
    //                    }
    //                    else
    //                    {
    //                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //                }

    //                finally
    //                {
    //                    if (con.State != ConnectionState.Closed)
    //                    {
    //                        con.Close();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    protected void Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void rbMPState_CheckedChanged(object sender, EventArgs e)
    {
        // btnAccept.Enabled = btnReject.Enabled = false;
        btnAccept.Enabled = false;
        //ddlOtherStates.Enabled = false;
        //lblOtherStates.Visible = false;
        GridView1.DataSource = "";
        GridView1.DataBind();
       // GetDistName();
       // ddlOtherStates.Items.Clear();
    }
    //protected void rbOtherState_CheckedChanged(object sender, EventArgs e)
    //{
    //    //btnAccept.Enabled = btnReject.Enabled = false;
    //    btnAccept.Enabled = false;
    //    ddlOtherStates.Enabled = true;
    //    lblOtherStates.Visible = true;
    //    GridView1.DataSource = "";
    //    GridView1.DataBind();
    //    GetOtherStates();
    //    ddlMPDist.Items.Clear();
    //}

    //protected void ddlOtherStates_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //lblOtherStates.Visible = false;
    //    GridView1.DataSource = "";
    //    GridView1.DataBind();
    //}

    protected void New_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}