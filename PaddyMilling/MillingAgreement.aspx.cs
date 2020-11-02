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

public partial class PaddyMilling_MillingAgreement : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da, Agrmtda, da1;
    DataSet ds, Agrmtds, ds1;
    DataTable Dt = new DataTable();

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    public string Operator = "", state = "", OtherState = "", DepositMoney = "", DhanAmountType = "", TotalDhanLot = "", IssueCentre = "", MillingType = "";
    float DhanLot;
    decimal RArva, RUsnha;
    public int CheckRejectedLot;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                //txtDhanAmountDetails.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                //txtDhanAmountDetails.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                //txtDhanAmountDetails.Attributes.Add("onchange", "return chksqltxt(this)");

                //txtYear.Text = (DateTime.Now.Year - 1) + "-" + DateTime.Now.ToString("yy");
                rbMaalik.Checked = true;
                rbMPState.Checked = true;
                //rbBankGuranty.Checked = true;
                rbR_Arva.Checked = true;
                Panellot.Visible = Panel1.Visible = false;

                ViewState["DistName"] = ViewState["H_DistName"] = txtDistManager.Text = Session["dist_name"].ToString();
                ViewState["DistCode"] = Session["dist_id"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                ViewState["CountMPDistrict"] = "0";
                ViewState["CountOtherDistrict"] = "0";

                ViewState["CountMPDivision"] = "0";
                ViewState["CountOtherDivision"] = "0";

                GetMPDistrict();
                GetDMName();
                GetLotNumber();
            }

            if (rbMPState.Checked)
            {
                if (ViewState["CountMPDistrict"].ToString() == "0")
                {
                    GetMPDistrict();
                    ViewState["CountMPDistrict"] = "1";
                    ViewState["CountOtherDistrict"] = "0";

                    ViewState["CountMPDivision"] = "0";
                    ViewState["CountOtherDivision"] = "1";
                }

            }
            else if (rbOtherState.Checked)
            {
                if (ViewState["CountOtherDistrict"].ToString() == "0")
                {
                    GetOtherStates();
                    ViewState["CountOtherDistrict"] = "1";
                    ViewState["CountMPDistrict"] = "0";

                    ViewState["CountMPDivision"] = "1";
                    ViewState["CountOtherDivision"] = "0";
                }
            }

            string date = Request.Form[txtDate.UniqueID];
            txtDate.Text = date;

            string fromdate = Request.Form[txtFromDate.UniqueID];
            txtFromDate.Text = fromdate;

            string todate = Request.Form[txtToDate.UniqueID];
            txtToDate.Text = todate;

            //string TotalDhan = Request.Form[txtTotalDhan.UniqueID];
            //txtTotalDhan.Text = TotalDhan;
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetLotNumber()
    {
        for (int i = 1; i <= 250; i++)
        {
            ddlAgrmtDhanLot.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlAgrmtDhanLot.Items.Insert(0, "--Select--");

        //for (int i = 1; i <= 5; i++)
        //{
        //    //ddlDhanLot.Items.Add(new ListItem(i.ToString(), i.ToString()));
        //}
        ////ddlDhanLot.Items.Insert(0, "--Select--");
    }

    public void GetDMName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                {
                    lblDM.Text = "जिला प्रबंधक";
                    select = "select DM_Name As DM_Name From officers_list where District_code= '" + ViewState["DistCode"].ToString() + "'";
                }
                else if (Session["DistrictManager"].ToString() == "DDMO")
                {
                    lblDM.Text = "जिला विपणन अधिकारी";
                    select = "select DMO_Name As DM_Name From officers_list where District_code= '" + ViewState["DistCode"].ToString() + "'";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds, "officers_list");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtDistManagerName.Text = ds.Tables[0].Rows[0]["DM_Name"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जिला प्रबंधक का नाम उपलब्ध नहीं है|'); </script> ");
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

    public void GetCropYearValues()
    {
        hdfldArva.Value = hdfldUshnaF3.Value = hdfldUshnaA3.Value = hdfldDepositMoney.Value = hdfArvaChawalRs.Value = hdfUshnaChawalRs.Value = hdfReturnAgrmtCMR_Percent.Value = "";
        txtYear.Text = txtDepositMoney.Text = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear,Arva ,Ushna_First3,Ushna_After3,Deposit_Money,ArvaChawal,UshnaChawal,Common_Dhan_Rs,GradeA_Dhan_Rs,ReturnAgrmtCMR_Percent FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfldArva.Value = ds.Tables[0].Rows[0]["Arva"].ToString();
                    hdfldUshnaF3.Value = ds.Tables[0].Rows[0]["Ushna_First3"].ToString();
                    hdfldUshnaA3.Value = ds.Tables[0].Rows[0]["Ushna_After3"].ToString();
                    hdfldDepositMoney.Value = ds.Tables[0].Rows[0]["Deposit_Money"].ToString();
                    hdfArvaChawalRs.Value = ds.Tables[0].Rows[0]["ArvaChawal"].ToString();
                    hdfUshnaChawalRs.Value = ds.Tables[0].Rows[0]["UshnaChawal"].ToString();
                    hdfReturnAgrmtCMR_Percent.Value = ds.Tables[0].Rows[0]["ReturnAgrmtCMR_Percent"].ToString();

                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtDepositMoney.Text = ds.Tables[0].Rows[0]["0"].ToString();

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

    public void GetMPDistrict()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        ddlOtherStates.Items.Clear();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_code ,district_name FROM pds.districtsmp order by district_name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "pds.districtsmp");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDist.DataSource = ds.Tables[0];
                    ddlMacersAddDist.DataTextField = "district_name";
                    ddlMacersAddDist.DataValueField = "district_code";
                    ddlMacersAddDist.DataBind();
                    ddlMacersAddDist.Items.Insert(0, "--Select--");
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

    public void GetOtherStates()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT State_Code ,State_Name FROM State_Master where Status = 'Y' and State_Code!=23 order by State_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "State_Master");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOtherStates.DataSource = ds.Tables[0];
                    ddlOtherStates.DataTextField = "State_Name";
                    ddlOtherStates.DataValueField = "State_Code";
                    ddlOtherStates.DataBind();
                    ddlOtherStates.Items.Insert(0, "--Select--");
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

    public void GetOtherDistrict()
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";
        lblOtherStates.Visible = false;

        string dist = ddlOtherStates.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "SELECT district_code ,district_name FROM OtherState_DistrictCode where State_Id = '" + dist + "'  order by district_name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "OtherState_DistrictCode");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersAddDist.DataSource = ds.Tables[0];
                    ddlMacersAddDist.DataTextField = "district_name";
                    ddlMacersAddDist.DataValueField = "district_code";
                    ddlMacersAddDist.DataBind();
                    ddlMacersAddDist.Items.Insert(0, "--Select--");
                    FCI.Visible = true;
                    rdbYes.Checked = false;
                    rdbNO.Checked = true;
                    lblDist.Text = "'" + ddlOtherStates.SelectedItem.ToString() + "'";
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

    protected void ddlOtherStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMacersAddDist.Items.Clear();
        ddlMacersName.Items.Clear();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";
        FCI.Visible = false;
        rdbYes.Checked = false;
        rdbNO.Checked = true;
        lblDist.Text = "";

        if (ddlOtherStates.SelectedIndex > 0)
        {
            GetOtherDistrict();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select State Name'); </script> ");
            return;
        }

    }

    protected void ddlMacersAddDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbMPState.Checked)
        {
            if (ViewState["CountMPDivision"].ToString() == "0")
            {
                getMillName();
            }

        }
        else if (rbOtherState.Checked)
        {
            if (ViewState["CountOtherDivision"].ToString() == "0")
            {
                getMillName();
            }
        }
    }

    protected void getMillName()
    {
        string district = ddlMacersAddDist.SelectedValue.ToString();
        ddlMacersName.Items.Clear();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";

        //DataTable dt = new DataTable(); DataTable dt1 = new DataTable(); string name = string.Empty; string id = string.Empty;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (rbMPState.Checked)
                {
                    select = "Select distinct Registration_ID,Mill_Name from Miller_Registration_2017 where District_Code='" + district + "' and Status = 1 and Black_listed='N' and CropYear = '" + txtYear.Text + "' and State_Code='23' and State='MP' order by Mill_Name";
                }
                else if (rbOtherState.Checked)
                {
                    select = "Select distinct Registration_ID,Mill_Name from Miller_Registration_2017 where District_Code='" + district + "' and Status = 1 and Black_listed='N' and CropYear = '" + txtYear.Text + "' and State_Code='" + ddlOtherStates.SelectedValue.ToString() + "' and State='Other' order by Mill_Name";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMacersName.DataSource = ds.Tables[0];
                    ddlMacersName.DataTextField = "Mill_Name";
                    ddlMacersName.DataValueField = "Registration_ID";
                    ddlMacersName.DataBind();
                    ddlMacersName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिल का नाम उपलब्ध नहीं है|'); </script> ");
                }

            }

            #region
            //using (con = new SqlConnection(strcon))
            //{
            //    try
            //    {
            //        if (con.State == ConnectionState.Closed)
            //        {
            //            con.Open();
            //        }

        //        string selectTehsil = string.Format("select distinct Registration_ID,Mill_Name from Miller_Registration_2017 where District_Code='{0}' and Status = 1 order by Mill_Name", district);
            //        da = new SqlDataAdapter(selectTehsil, con);
            //        ds = new DataSet();
            //        da.Fill(ds, "Miller_Registration_2017");
            //        da.Fill(dt);

        //        if (ds != null)
            //        {
            //            if (dt.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                {
            //                    id = dt.Rows[i]["Registration_ID"].ToString();
            //                    name = dt.Rows[i]["Mill_Name"].ToString();
            //                    ddlMacersName.Items.Add(new ListItem(name, id));
            //                }
            //            }
            //        }

        //        string selectTehsil1 = string.Format("select min(Miller_ID) Miller_ID,Miller_Name from Miller_Master where District_Code='{0}' and Miller_Name != '' group by Miller_Name order by Miller_Name", district);
            //        da1 = new SqlDataAdapter(selectTehsil1, con);
            //        ds1 = new DataSet();
            //        da1.Fill(ds1, "Miller_Master");
            //        da1.Fill(dt1);

        //        if (ds1 != null)
            //        {
            //            if (dt1.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dt1.Rows.Count; i++)
            //                {
            //                    id = dt1.Rows[i]["Miller_ID"].ToString();
            //                    name = dt1.Rows[i]["Miller_Name"].ToString();
            //                    //newName = id + " ---- " + name;
            //                    ddlMacersName.Items.Add(new ListItem(name, id));
            //                }
            //            }
            //        }

        //        ddlMacersName.Items.Insert(0, "--Select--");

        //        if (dt1.Rows.Count <= 0 && dt.Rows.Count <= 0)
            //        {
            //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिल का नाम उपलब्ध नहीं है|'); </script> ");
            //            ddlMacersName.Items.Clear();
            //        }

        //    }
            #endregion

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

    protected void ddlMacersName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMacersName.SelectedIndex > 0)
        {
            //for conditions both 50% and paddy lifting
           // MilingCpacity();
            //for working without condition 
            NoPre_Agreement();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
        }
    }

    protected void MilingCpacity()
    {
        hdfPreviousAgrmt.Value = hdfSubmittedCMRLot.Value = "";
        string district = ddlMacersAddDist.SelectedValue.ToString();
        string capacity = ddlMacersName.SelectedValue.ToString();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";
        ddlAgrmtDhanLot.Enabled =  false;
       // ddlDhanLot.Enabled =

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }



                    int districtID =Convert.ToInt32(Session["dist_id"].ToString());
                        string qry="";
                        int loginDist=0;
                       qry = "select district from PaddyMilling_Agreement_2017 where Mill_Name='" + ddlMacersName.SelectedValue.ToString() + "'";
                       da = new SqlDataAdapter(qry, con);
                       Agrmtds = new DataSet();
                       da.Fill(Agrmtds, "PaddyMilling_Agreement_2017");
                if (Agrmtds.Tables.Count > 0 && Agrmtds.Tables[0].Rows.Count > 0)
                {
                    
                       int totalrows = Agrmtds.Tables["PaddyMilling_Agreement_2017"].Rows.Count;
                       for (int i = 0; i < totalrows; i++)
                       {

                           loginDist = Convert.ToInt32(Agrmtds.Tables[0].Rows[i]["district"].ToString());
                           if (loginDist == districtID)
                           {
                               checkAgreement();
                               return;
                           }
                           else
                           {

                           }


                           //}
                           NoPre_Agreement();
                       }
                }
                else
                {
                 NoPre_Agreement();
                }
                        
                       
            }
            catch (Exception ex)
            {
                ddlMacersName.SelectedIndex = 0;
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

    protected void checkAgreement()
    { 
     hdfPreviousAgrmt.Value = hdfSubmittedCMRLot.Value = "";
        string district = ddlMacersAddDist.SelectedValue.ToString();
        string capacity = ddlMacersName.SelectedValue.ToString();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";
        ddlAgrmtDhanLot.Enabled =  false;
       // ddlDhanLot.Enabled =

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int districtID = Convert.ToInt32(Session["dist_id"].ToString());


                string CheckAgrmt = "Select ISNULL(MAX(Rem_Total_Dhan),0) As RemDhan,Sum(isnull(cast(DhanAmountDetails as int),0)) as TotalDhanLot From PaddyMilling_Agreement_2017 Where Mill_Addr_District='" + ddlMacersAddDist.SelectedValue.ToString() + "' and Mill_Name='" + ddlMacersName.SelectedValue.ToString() + "' and (IsAccepted='Y' OR IsAccepted='F') and District='" + districtID + "'";
                da = new SqlDataAdapter(CheckAgrmt, con);
                Agrmtds = new DataSet();
                da.Fill(Agrmtds, "PaddyMilling_Agreement_2017");
                if (Agrmtds.Tables.Count > 0 && Agrmtds.Tables[0].Rows.Count > 0)
                {
                    decimal RemDhan = 0;
                    RemDhan = decimal.Parse(Agrmtds.Tables[0].Rows[0]["RemDhan"].ToString());
                    string CheckTotalAgrmtDhanLot = Agrmtds.Tables[0].Rows[0]["TotalDhanLot"].ToString();
                    int TotalAgrmtDhanLot = 0;
                    if (CheckTotalAgrmtDhanLot != "")
                    {
                        TotalAgrmtDhanLot = int.Parse(Agrmtds.Tables[0].Rows[0]["TotalDhanLot"].ToString());
                    }
                    else
                    {

                    }
                    if (RemDhan >= 1)
                    {
                        hdfPreviousAgrmt.Value = "1";
                        ddlMacersName.SelectedIndex = ddlAgrmtDhanLot.SelectedIndex = 0;
                        //ddlDhanLot.SelectedIndex = 
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने जिस मिलर का चुनाव किया हैं, उस मिलर ने इसके पहले किये गए अनुबंध के विरुद्ध पूरा धान नहीं उठाया हैं, इसलिए आप नया अनुबंध नहीं कर सकते|'); </script> ");
                        return;
                    }
                    else
                    {

                        string ChechCMR = "Select Top 1 Agreement_ID,((R_Arva*" + hdfReturnAgrmtCMR_Percent.Value + ")/100) As ReturnExpRice From PaddyMilling_Agreement_2017 Where CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMacersName.SelectedValue.ToString() + "' and IsAccepted='Y' and District='" + ViewState["DistCode"].ToString() + "' order by Current_DateTime desc";
                        //string ChechCMR = "Select Count(LotNumber) As SubmittedLot From CMR_QualityInspection Where CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMacersName.SelectedValue.ToString() + "' and Submited='Y'";
                        da = new SqlDataAdapter(ChechCMR, con);
                        Agrmtds.Clear();
                        Agrmtds = new DataSet();
                        da.Fill(Agrmtds);

                        if (Agrmtds.Tables.Count > 0 && Agrmtds.Tables[0].Rows.Count > 0)
                        {
                            string ReturnAgrmtNo = Agrmtds.Tables[0].Rows[0]["Agreement_ID"].ToString();
                            float ReturnExpRice = float.Parse(Agrmtds.Tables[0].Rows[0]["ReturnExpRice"].ToString());

                            string CheckReturnActualTotalRice = "Select SUM(Accept_CommonRice) As SubmittedRice From CMR_QualityInspection Where CropYear='" + txtYear.Text + "' and Submited='Y' and Agreement_ID='" + ReturnAgrmtNo + "'";
                            da = new SqlDataAdapter(CheckReturnActualTotalRice, con);
                            Agrmtds.Clear();
                            Agrmtds = new DataSet();
                            da.Fill(Agrmtds);

                            if (Agrmtds.Tables.Count > 0 && Agrmtds.Tables[0].Rows.Count > 0)
                            {
                                float SubmittedRice = 0;
                                string CheckData = Agrmtds.Tables[0].Rows[0]["SubmittedRice"].ToString();

                                if (CheckData != "")
                                {
                                    SubmittedRice = float.Parse(Agrmtds.Tables[0].Rows[0]["SubmittedRice"].ToString());
                                }

                                if (SubmittedRice < ReturnExpRice)
                                {
                                    hdfSubmittedCMRLot.Value = "1";
                                    ddlMacersName.SelectedIndex = ddlAgrmtDhanLot.SelectedIndex = 0;
                                    //ddlDhanLot.SelectedIndex = 
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने जिस मिलर का चुनाव किया हैं, उस मिलर ने इसके पहले किये गए अनुबंध नंबर " + ReturnAgrmtNo + " के विरुद्ध " + hdfReturnAgrmtCMR_Percent.Value + "% CMR जमा नहीं किया हैं, इसलिए आप नया अनुबंध नहीं कर सकते|'); </script> ");
                                    return;
                                }

                                else
                                {

                                    //string CheckRejected = string.Format("Select Rejected From PaddyMilling_Agreement_2017 where Mill_Name='" + ddlMacersName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' order by Rejected desc");
                                    //da = new SqlDataAdapter(CheckRejected, con);
                                    //ds = new DataSet();
                                    //da.Fill(ds, "PaddyMilling_Agreement_2017");

                                    //if (ds.Tables[0].Rows.Count > 0)
                                    //{
                                    //    CheckRejectedLot = int.Parse(ds.Tables[0].Rows[0]["Rejected"].ToString());
                                    //}
                                    //else
                                    //{
                                    //    CheckRejectedLot = 0;
                                    //}

                                    //if (CheckRejectedLot < 3)
                                    //{
                                    string selectCapacity = "select milling_capacity_arwa,milling_capacity_usna,operator_name from  Miller_Registration_2017 where Registration_ID = '" + capacity + "'  and District_Code='" + district + "' and Status = 1 and CropYear = '" + txtYear.Text + "' ";
                                    da = new SqlDataAdapter(selectCapacity, con);
                                    ds = new DataSet();
                                    da.Fill(ds, "Miller_Registration_2017");

                                    if (ds != null)
                                    {
                                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            ddlAgrmtDhanLot.Enabled =  true;
                                            //ddlDhanLot.Enabled =
                                            txtCapacityArva.Text = ds.Tables[0].Rows[0]["milling_capacity_arwa"].ToString();
                                            txtCapacityUsna.Text = ds.Tables[0].Rows[0]["milling_capacity_usna"].ToString();
                                            txtOwnerName.Text = ds.Tables[0].Rows[0]["operator_name"].ToString();
                                            btnSubmit.Enabled =  true;
                                            //ddlDhanLot.Enabled =
                                        }
                                        else
                                        {
                                            txtOwnerName.Text = ddlMacersName.SelectedItem.ToString();
                                            btnSubmit.Enabled = true;
                                            //ddlDhanLot.Enabled = 
                                        }
                                    }
                                    //}
                                    //else
                                    //{
                                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके मिल को इस विपणन वर्ष के लिए काले सूची में रखा गया हैं, आप इस विपणन वर्ष में अनुबंध नहीं कर सकते|'); </script> ");
                                    //    btnSubmit.Enabled = ddlDhanLot.Enabled = false;
                                    //}
                                }
                            }
                        }

                    //when there is no agreement of that miller
                        else
                        {
                            string selectCapacity = "select milling_capacity_arwa,milling_capacity_usna,operator_name from  Miller_Registration_2017 where Registration_ID = '" + capacity + "'  and District_Code='" + district + "' and Status = 1 and CropYear = '" + txtYear.Text + "' ";
                            da = new SqlDataAdapter(selectCapacity, con);
                            ds = new DataSet();
                            da.Fill(ds, "Miller_Registration_2017");

                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    ddlAgrmtDhanLot.Enabled =  true;
                                    //ddlDhanLot.Enabled =
                                    txtCapacityArva.Text = ds.Tables[0].Rows[0]["milling_capacity_arwa"].ToString();
                                    txtCapacityUsna.Text = ds.Tables[0].Rows[0]["milling_capacity_usna"].ToString();
                                    txtOwnerName.Text = ds.Tables[0].Rows[0]["operator_name"].ToString();
                                    btnSubmit.Enabled =  true;
                                   // ddlDhanLot.Enabled =
                                }
                                else
                                {
                                    txtOwnerName.Text = ddlMacersName.SelectedItem.ToString();
                                    btnSubmit.Enabled = true;
                                    // ddlDhanLot.Enabled =
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ddlMacersName.SelectedIndex = 0;
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

    protected void NoPre_Agreement()
    {

        hdfPreviousAgrmt.Value = hdfSubmittedCMRLot.Value = "";
        string district = ddlMacersAddDist.SelectedValue.ToString();
        string capacity = ddlMacersName.SelectedValue.ToString();
        txtCapacityArva.Text = txtCapacityUsna.Text = txtOwnerName.Text = "";
        ddlAgrmtDhanLot.Enabled = false;
       // ddlDhanLot.Enabled = 

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string selectCapacity = "select milling_capacity_arwa,milling_capacity_usna,operator_name from  Miller_Registration_2017 where Registration_ID = '" + capacity + "'  and District_Code='" + district + "' and Status = 1 and CropYear = '" + txtYear.Text + "' ";
                da = new SqlDataAdapter(selectCapacity, con);
                ds = new DataSet();
                da.Fill(ds, "Miller_Registration_2017");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgrmtDhanLot.Enabled =  true;
                        //ddlDhanLot.Enabled =
                        txtCapacityArva.Text = ds.Tables[0].Rows[0]["milling_capacity_arwa"].ToString();
                        txtCapacityUsna.Text = ds.Tables[0].Rows[0]["milling_capacity_usna"].ToString();
                        txtOwnerName.Text = ds.Tables[0].Rows[0]["operator_name"].ToString();
                        btnSubmit.Enabled = true;
                        // ddlDhanLot.Enabled =
                    }
                    else
                    {
                        txtOwnerName.Text = ddlMacersName.SelectedItem.ToString();
                        btnSubmit.Enabled =  true;
                        //ddlDhanLot.Enabled =
                    }
                }

            }
            catch (Exception ex)
            {
                ddlMacersName.SelectedIndex = 0;
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
    protected void Submit_Click(object sender, EventArgs e)
    {
        //if (ddlDhanLot.SelectedIndex <= 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया मिलर के लॉट की सिक्यूरिटी राशि चुने|'); </script> ");
        //    return;
        //}
         if (ddlAgrmtDhanLot.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया धान के अनुबंध का लॉट चुने|'); </script> ");
            return;
        }
        else if (ddlMacersName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुने|'); </script> ");
            return;
        }
        else if (hdfPreviousAgrmt.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने जिस मिलर का चुनाव किया हैं, उस मिलर ने इसके पहले किये गए अनुबंध के विरुद्ध पूरा धान नहीं उठाया हैं, इसलिए आप नया अनुबंध नहीं कर सकते|'); </script> ");
            ddlMacersName.SelectedIndex  = ddlAgrmtDhanLot.SelectedIndex = 0;
            //ddlDhanLot.SelectedIndex=
            return;
        }
        else if (hdfSubmittedCMRLot.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने जिस मिलर का चुनाव किया हैं, उस मिलर ने इसके पहले किये गए अनुबंध के विरुद्ध पूरा CMR जमा नहीं किया हैं, इसलिए आप नया अनुबंध नहीं कर सकते|'); </script> ");
            ddlMacersName.SelectedIndex =  ddlAgrmtDhanLot.SelectedIndex = 0;
            //ddlDhanLot.SelectedIndex =
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "" && txtDate.Text != "" && txtDistManagerName.Text != "" && txtOwnerName.Text != "" && txtTotalDhan.Text != "" && txtTotalDhan.Text != "0" && ddlMacersName.SelectedIndex != 0)
                {
                    //DateTime frmDate = DateTime.Parse(txtFromDate.Text);
                    //txtFromDate.Text = frmDate.ToString("dd-MM-yyyy");

                    DateTime Fromdate = Convert.ToDateTime(DateTime.ParseExact(txtFromDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
                    DateTime Todate = Convert.ToDateTime(DateTime.ParseExact(txtToDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));

                    DateTime dt1 = Convert.ToDateTime(Fromdate);
                    DateTime dt2 = Convert.ToDateTime(Todate);

                    if (dt1 < dt2)
                    {
                        using (con = new SqlConnection(strcon))
                        {
                            try
                            {
                                con.Open();

                                //ClientIP objClientIP = new ClientIP();
                                //string GetIp = (objClientIP.GETIP() + "  " + objClientIP.GETHOST());

                                string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                //string browser = Request.Browser.Browser.ToString();
                                //string version = Request.Browser.Version.ToString();
                                string useragent = Session["DistrictManager"].ToString();

                                string date = Request.Form[txtDate.UniqueID].ToString();
                                string FromDate = Request.Form[txtFromDate.UniqueID].ToString();
                                string ToDate = Request.Form[txtToDate.UniqueID].ToString();

                                ConvertServerDate ServerDate = new ConvertServerDate();
                                string ConvertDate = ServerDate.getDate_MDY(date.ToString());
                                string ConvertFromDate = ServerDate.getDate_MDY(FromDate.ToString());
                                string ConvertToDate = ServerDate.getDate_MDY(ToDate.ToString());

                                string CropYear2D = txtYear.Text;
                                string AgrmtYear = CropYear2D.Substring(CropYear2D.Length - 2);
                                string AgrmtDivision = ddlMacersName.SelectedValue.ToString();
                                string Agrmtselectmax = "select isnull(max(cast(Agreement_ID as bigint)),0) as Agreement_ID from PaddyMilling_Agreement_2017 where Mill_Name='" + AgrmtDivision + "' and CropYear='" + txtYear.Text + "' and District='" + ViewState["DistCode"].ToString() + "' ";

                                Agrmtda = new SqlDataAdapter(Agrmtselectmax, con);
                                Agrmtds = new DataSet();
                                Agrmtda.Fill(Agrmtds, "PaddyMilling_Agreement_2017");

                                string Agrmt_ID = Agrmtds.Tables[0].Rows[0]["Agreement_ID"].ToString();

                                if (Agrmt_ID == "0")
                                {
                                    Agrmt_ID = ViewState["DistCode"].ToString() + AgrmtDivision + AgrmtYear + "100";
                                }
                                else
                                {
                                    string fordo = Agrmt_ID.Substring(Agrmt_ID.Length - 3);

                                    if (fordo == "000")
                                    {
                                        fordo = "1000";
                                    }

                                    Int64 DO_ID_new = Convert.ToInt64(fordo);

                                    DO_ID_new = DO_ID_new + 1;

                                    string combine = DO_ID_new.ToString();

                                    Agrmt_ID = ViewState["DistCode"].ToString() + AgrmtDivision + AgrmtYear + combine;

                                }

                                decimal Agreement_ID = decimal.Parse(Agrmt_ID);

                                if (rbMaalik.Checked)
                                {
                                    Operator = "मालिक";
                                }
                                else if (rbProp.Checked)
                                {
                                    Operator = "प्रोपराइटर";
                                }
                                else
                                {
                                    Operator = "अधिकृत प्रतिनिधि";
                                }

                                if (rbMPState.Checked)
                                {
                                    state = "MP";
                                    OtherState = "23";
                                }
                                else
                                {
                                    state = "Other";
                                    OtherState = ddlOtherStates.SelectedValue.ToString();
                                }

                                if (txtCommonDhan.Text == "")
                                {
                                    txtCommonDhan.Text = "0";
                                }
                                else if (txtGradeADhan.Text == "")
                                {
                                    txtGradeADhan.Text = "0";
                                }

                                //if (rbBankGuranty.Checked)
                                //{
                                //    DhanAmountType = "Bank Guranty";
                                //}
                                //else if (rbTDR.Checked)
                                //{
                                //    DhanAmountType = "FDR";
                                //}
                                //else if (rbDD.Checked)
                                //{
                                //    DhanAmountType = "DD";
                                //}

                                decimal TotalDhan = (decimal.Parse(txtCommonDhan.Text) + decimal.Parse(txtGradeADhan.Text));

                                if (rbR_Arva.Checked)
                                {
                                    //RArva = ((((TotalDhan) * 10) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);
                                    RArva = ((((TotalDhan)) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);
                                    RUsnha = 0;
                                    MillingType = "अरवा";
                                }
                                else if (rbR_Ushna.Checked)
                                {
                                    //RUsnha = ((((TotalDhan) * 10) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                    RUsnha = ((((TotalDhan)) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                    RArva = 0;
                                    MillingType = "उसना";
                                }

                                string instr = "";
                                //string instr = string.Format("Insert into PaddyMilling_Agreement_2017(Agrmt_Date,District,Dist_Manager_Name,Mill_Addr_District, Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmountType,DhanAmountDetails,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type) values ('{0}',N'{1}',N'{2}','{3}',{4},N'{5}',N'{6}','{7}','{8}','{9}',{10},{11},{12},{13},'{14}',{15},'{16}','{17}',{18},{19},{20},{21},N'{22}','{23}',{24},{25},{26},{27},{28},{29},N'{30}')", ConvertDate, txtDistManager.Text, txtDistManagerName.Text, ddlMacersAddDist.SelectedValue.ToString(), ddlMacersName.SelectedValue.ToString(), txtOwnerName.Text, Operator.ToString(), txtYear.Text, ConvertFromDate, ConvertToDate, (decimal.Parse(txtCommonDhan.Text) * 10), (decimal.Parse(txtGradeADhan.Text) * 10), ((TotalDhan) * 10), Agreement_ID, GetIp.ToString(), "GETDATE()", useragent, state, OtherState, txtDepositMoney.Text, ddlDhanLot.SelectedValue.ToString(), DhanAmountType, txtDhanAmountDetails.Text, RArva, RUsnha, (decimal.Parse(txtCommonDhan.Text) * 10), (decimal.Parse(txtGradeADhan.Text) * 10), ((TotalDhan) * 10), ddlDhanLot.SelectedValue.ToString(), MillingType);
                                //DhanAmountType != "" &&
                                if (MillingType != "" &&  state != "" && Operator != "")
                                {
                                    if (ddlOtherStates.SelectedIndex > 0 && rdbYes.Checked == true)
                                    {
                                        instr = "Insert into PaddyMilling_Agreement_2017(District,Dist_Manager_Name,Mill_Addr_District,Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmountType,DhanAmountDetails,Agrmt_Date,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type,Return_CommonRice,Return_GradeARice,Return_TotalRice,IsAccepted,MobileNO,DeliverdToFCI) Values('" + ViewState["DistCode"].ToString() + "',N'" + txtDistManagerName.Text + "','" + ddlMacersAddDist.SelectedValue.ToString() + "','" + ddlMacersName.SelectedValue.ToString() + "',N'" + txtOwnerName.Text + "',N'" + Operator.ToString() + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "','" + txtCommonDhan.Text + "','" + txtGradeADhan.Text + "','" + TotalDhan + "','" + Agreement_ID + "','" + GetIp.ToString() + "',GETDATE(),'" + useragent + "','" + state + "','" + OtherState + "','" + txtDepositMoney.Text + "','0','" + DhanAmountType + "','" + ddlAgrmtDhanLot.SelectedItem.ToString() + "','" + ConvertDate + "','" + RArva + "','" + RUsnha + "','" + txtCommonDhan.Text + "','" + txtGradeADhan.Text + "','" + TotalDhan + "','0',N'" + MillingType + "','0','0','0','F','" + txtMobileNo.Text + "','Y')";
                                    }
                                    else
                                    {
                                        instr = "Insert into PaddyMilling_Agreement_2017(District,Dist_Manager_Name,Mill_Addr_District,Mill_Name,Mill_Operator_Name,Mill_Operator,CropYear,From_Date,To_Date,Common_Dhan,GradeA_Dhan,Total_Dhan,Agreement_ID,IP_Address,Current_DateTime,User_Agent,State,State_Code,DepositMoney,DhanLot,DhanAmountType,DhanAmountDetails,Agrmt_Date,R_Arva,R_Ushna,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_Total_Dhan,Rem_DhanLot,Milling_Type,Return_CommonRice,Return_GradeARice,Return_TotalRice,IsAccepted,MobileNO,DeliverdToFCI) Values('" + ViewState["DistCode"].ToString() + "',N'" + txtDistManagerName.Text + "','" + ddlMacersAddDist.SelectedValue.ToString() + "','" + ddlMacersName.SelectedValue.ToString() + "',N'" + txtOwnerName.Text + "',N'" + Operator.ToString() + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "','" + txtCommonDhan.Text + "','" + txtGradeADhan.Text + "','" + TotalDhan + "','" + Agreement_ID + "','" + GetIp.ToString() + "',GETDATE(),'" + useragent + "','" + state + "','" + OtherState + "','" + txtDepositMoney.Text + "','0','" + DhanAmountType + "','" + ddlAgrmtDhanLot.SelectedItem.ToString() + "','" + ConvertDate + "','" + RArva + "','" + RUsnha + "','" + txtCommonDhan.Text + "','" + txtGradeADhan.Text + "','" + TotalDhan + "','0',N'" + MillingType + "','0','0','0','F','" + txtMobileNo.Text + "','N')";
                                    }
                                    cmd = new SqlCommand(instr, con);
                                    int count = cmd.ExecuteNonQuery();
                                    if (count > 0)
                                    {
                                        string strMobileNo = txtMobileNo.Text;
                                        string strMessage = "आपने जिला कार्यालय '" + ViewState["H_DistName"] + "' से '" + TotalDhan + "' Qtls धान('" + ddlAgrmtDhanLot.SelectedItem.ToString() + "' लॉट) के लिए दिनांक '" + FromDate + "' से दिनांक '" + ToDate + "' तक '" + MillingType + "' धान मिलिंग का अनुबंध किया है तथा आपकी अनुबंध संख्या '" + Agreement_ID + "' हैं| कृपया अपना अनुबंध नंबर सम्भाल कर रखे|";

                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Agreement Number Is " + Agreement_ID + " '); </script> ");
                                        Print.Enabled = true;
                                        Session["Agreement_ID"] = Agreement_ID.ToString();
                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                        lblLotNumber.Text = lblLotNumber0.Text = Agreement_ID.ToString();
                                        Panellot.Visible = Panel1.Visible = true;
                                        btnSubmit.Enabled = false;
                                        //Print.Enabled = true;

                                        SMS Message = new SMS();
                                        Message.SendSMS(strMobileNo, strMessage);
                                    }
                                    else
                                    {
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                                    }
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Check Your InterNet Connection'); </script> ");
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
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया सही दिनांक चुनें|'); </script> ");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter All Required Values'); </script> ");
                }
            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    //protected void Print_Click(object sender, EventArgs e)
    //{
    //    //Server.Transfer("~/PaddyMilling/Print/PMillingAgreement.aspx", true);

    //    string url = "Print/PMillingAgreement.aspx";
    //    string s = "window.open('" + url + "', 'popup_window');";
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    //}

    #region
    //public string Date
    //{
    //    get
    //    {
    //        string date = Request.Form[txtDate.UniqueID];
    //        return date;
    //    }
    //}

    //public string DistManager
    //{
    //    get { return txtDistManager.Text; }
    //}

    //public string DistrictManagerName
    //{
    //    get
    //    {
    //        string DistManagerName = Request.Form[txtDistManagerName.UniqueID];
    //        return DistManagerName;
    //    }
    //}

    //public string MacersName
    //{
    //    get { return ddlMacersName.SelectedItem.ToString(); }
    //}
    //public string MacersAddDist
    //{
    //    get { return ddlMacersAddDist.SelectedItem.ToString(); }
    //}

    ////public string MacersAddDivision
    ////{
    ////    get { return ddlMacersAddDivision.SelectedItem.ToString(); }
    ////}

    //public string OwnerName
    //{
    //    get { return txtOwnerName.Text; }
    //}

    //public string Owner
    //{
    //    get
    //    {
    //        if (rbMaalik.Checked)
    //        {
    //            return rbMaalik.Text;
    //        }
    //        else if (rbProp.Checked)
    //        {
    //            return rbProp.Text;
    //        }
    //        else
    //        {
    //            return rbOthers.Text;
    //        }
    //    }
    //}


    //public string Year
    //{
    //    get { return txtYear.Text; }
    //}

    //public string FromDate
    //{
    //    get
    //    {
    //        string fromdate = Request.Form[txtFromDate.UniqueID];
    //        return fromdate;
    //    }

    //}
    //public string ToDate
    //{
    //    get
    //    {
    //        string todate = Request.Form[txtToDate.UniqueID];
    //        return todate;
    //    }
    //}
    //public string CommonDhan
    //{
    //    get { return txtCommonDhan.Text; }
    //}
    //public string GradeADhan
    //{
    //    get { return txtGradeADhan.Text; }
    //}
    //public string TotalDhan
    //{
    //    get
    //    {
    //        string TotalDhan = Request.Form[txtTotalDhan.UniqueID];
    //        return TotalDhan;
    //    }
    //}

    ////public string Arva
    ////{
    ////    get { return hdfldArva.Value; }
    ////}
    ////public string UshnaF3
    ////{
    ////    get { return hdfldUshnaF3.Value; }
    ////}
    ////public string UshnaA3
    ////{
    ////    get { return hdfldUshnaA3.Value; }
    ////}
    ////public string DepositMoney
    ////{
    ////    get { return hdfldDepositMoney.Value; }
    ////}
    ////public string ArvaChawalRs
    ////{
    ////    get { return hdfArvaChawalRs.Value; }
    ////}
    ////public string UshnaChawalRs
    ////{
    ////    get { return hdfUshnaChawalRs.Value; }
    ////}

    #endregion

    protected void New_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void rbOtherState_CheckedChanged(object sender, EventArgs e)
    {
        ddlOtherStates.Enabled = true;
        lblOtherStates.Visible = true;
    }
    protected void rbMPState_CheckedChanged(object sender, EventArgs e)
    {
        ddlOtherStates.Enabled = false;
        lblOtherStates.Visible = false;
        FCI.Visible = false;
        rdbYes.Checked = false;
        rdbNO.Checked = true;
        lblDist.Text = "";
    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    //protected void ddlDhanLot_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDhanLot.SelectedIndex > 0)
    //    {
    //        DhanLot = float.Parse(ddlDhanLot.SelectedValue.ToString());
    //        //txtLotAmount.Text = (DhanLot * 700000).ToString();
    //        int MaxDhan = int.Parse((DhanLot * 27).ToString());

    //        //txtDhanAmountDetails.Text = txtCommonDhan.Text = txtGradeADhan.Text = txtTotalDhan.Text = "";
    //        //txtDhanAmountDetails.ReadOnly = txtCommonDhan.ReadOnly = txtGradeADhan.ReadOnly = false;

    //        txtCommonDhan.Text = txtGradeADhan.Text = txtTotalDhan.Text = "";
    //        txtCommonDhan.ReadOnly = txtGradeADhan.ReadOnly = false;
    //        ViewState["dt"] = null;
    //    }
    //    else
    //    {
    //        //txtLotAmount.Text = "";
    //        ViewState["dt"] = null;
    //        txtCommonDhan.ReadOnly = txtGradeADhan.ReadOnly = true;
    //        txtCommonDhan.Text = txtGradeADhan.Text = txtTotalDhan.Text = "";

    //        //txtDhanAmountDetails.ReadOnly = txtCommonDhan.ReadOnly = txtGradeADhan.ReadOnly = true;
    //        //txtDhanAmountDetails.Text = txtCommonDhan.Text = txtGradeADhan.Text = txtTotalDhan.Text = "";
    //    }
    //}

    protected void ddlAgrmtDhanLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCommonDhan.Text = txtTotalDhan.Text = "";
        if (ddlAgrmtDhanLot.SelectedIndex > 0)
        {
            GetTotalDhan();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया धान के अनुबंध का लॉट चुने|'); </script> ");
        }
    }

    public void GetTotalDhan()
    {
        int dhanlot = 0;
        dhanlot = int.Parse(ddlAgrmtDhanLot.SelectedItem.ToString());

        txtCommonDhan.Text = txtTotalDhan.Text = (dhanlot * 403).ToString();
    }

    protected void Print_Click(object sender, EventArgs e)
    {
        string url = "Print/Print_MillingAgreement.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}