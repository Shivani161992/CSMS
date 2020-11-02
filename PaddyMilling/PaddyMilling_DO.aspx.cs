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

public partial class PaddyMilling_PaddyMilling_DO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd, cmd1, cmd2;
    SqlDataAdapter da, da1, da2, dar, dac;
    DataSet ds, ds1, ds2, dsr, dsc;
    double FDRvalue, ChequeValue = 0;
    string IsSociety, SocietyDist, Society, IsGodown, IssueCenter1, Godown1 = "";

    public string TotalDate, MillingType, strcheckDO, fordo, Final_DO_Number, CheckRejectedLot;
    public decimal TotalDhan, RArva, RUsnha, RemCommonDhan, RemGradeADhan, TotalCDhanRem, TotalGADhanRem, D_TotalCDhan, D_TotalGADhan, U_RemCommonDhan, U_RemGradeADhan, U_RemTotalDhan, LotLimit;
    public int count, TotalCount, TotalLength, U_RemDhanLot, LotOnlyNumber;

    public string districtid = "";
    double QtyTotal = 0;
    int received, Issued, Yreceived, allowed, lot_Allowed, AllLot, SumLotAll, lot_AllowedMaster;
    decimal addChequeValue, Allowed_Lot;
    decimal LotAmt;
    double FDRvaluereal, ChequeValuereal, TotalFDR = 0, totalCheque = 0;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;

                ViewState["DistName"] = Session["dist_name"].ToString();
                txtDistManager.Text = ViewState["DistName"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                //GetMPIssueCentre();
                GetMillName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];
    }

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                {
                    select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' and PM.User_Agent!='DDMO' order by MillName Asc";
                }
                else if (Session["DistrictManager"].ToString() == "DDMO")
                {
                    select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' and PM.User_Agent='DDMO' order by MillName Asc";
                }
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    /*public void GetMillName()
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        string name = string.Empty;
        string id = string.Empty;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string selectIssueCentre = string.Format("Select distinct MR.Mill_Name Mill_Name,MR.Registration_ID Mill_Registration from PaddyMilling_Agreement MA join Miller_Registration MR on MA.Mill_Name=MR.Registration_ID where MA.CropYear='{0}' and MA.District='{1}' order by MR.Mill_Name", txtYear.Text, ViewState["DistName"].ToString());
                da = new SqlDataAdapter(selectIssueCentre, con);
                ds = new DataSet();
                da.Fill(ds);
                da.Fill(dt);

                if (ds != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            id = dt.Rows[i]["Mill_Registration"].ToString();
                            name = dt.Rows[i]["Mill_Name"].ToString();
                            ddlMillName.Items.Add(new ListItem(name, id));
                        }
                    }

                }

                string selectIssueCentre1 = string.Format("Select distinct MM.Miller_Name Mill_Name,MM.Miller_ID Mill_Registration from PaddyMilling_Agreement MA join Miller_Master MM on MA.Mill_Name=MM.Miller_ID where MA.CropYear='{0}' and MA.District='{1}' order by MM.Miller_Name", txtYear.Text, ViewState["DistName"].ToString());
                da1 = new SqlDataAdapter(selectIssueCentre1, con);
                ds1 = new DataSet();
                da1.Fill(ds1);
                da1.Fill(dt1);

                if (ds1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            id = dt1.Rows[i]["Mill_Registration"].ToString();
                            name = dt1.Rows[i]["Mill_Name"].ToString();
                            ddlMillName.Items.Add(new ListItem(name, id));
                        }
                    }

                }

                ddlMillName.Items.Insert(0, "--Select--");

                if (dt1.Rows.Count <= 0 && dt.Rows.Count <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
                    ddlMillName.Items.Clear();
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
    }*/


    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetMillerRatio();
        //GetSecurityData();
        GetData();
        


    }

    public void GetMillerRatio()
    {


        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";


                select = "select FDR, checks, LOT_Amount from PM_RatioMaster where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "'";





                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    LotAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["LOT_Amount"].ToString());
                    txtFDRBS.Text = ds.Tables[0].Rows[0]["FDR"].ToString();
                    txtCheque.Text = ds.Tables[0].Rows[0]["checks"].ToString();



                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Ratio for FDR and Checks is not available for this miller'); </script> ");
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

    //public void GetSecurityData()
    //{
    //    int Lot;

    //     string DistCode = Session["dist_id"].ToString();

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            string select = "";
    //            //FDR_Value, Cheque_Value, Lots_Allowed

    //            select = "select sum(FDR_Value) as FDR_Value ,sum (Cheque_Value) as Cheque_Value   from PM_FDR_and_Cheque_Master where MillerID='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";

    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                //LotAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["One_LotAmount"].ToString());

    //               // txtFDRBS.Text = ds.Tables[0].Rows[0]["MinFDR"].ToString();
    //               // txtCheque.Text = ds.Tables[0].Rows[0]["Max_Cheque"].ToString();
    //                txtvalueFDR.Text = ds.Tables[0].Rows[0]["FDR_Value"].ToString();
    //                txtvalueCheck.Text = ds.Tables[0].Rows[0]["Cheque_Value"].ToString();

    //                decimal TotalBalMaster = Convert.ToDecimal(txtvalueFDR.Text) + Convert.ToDecimal(txtvalueCheck.Text);
    //                decimal Lots = TotalBalMaster / LotAmt;
    //                //txtRemDhanlLot.Text = ds.Tables[0].Rows[0]["Lots_Allowed"].ToString();
    //                //Lot = Convert.ToInt16(ds.Tables[0].Rows[0]["Lots_Allowed"].ToString());
    //                GetAddCheque();

    //                decimal floor2 = Math.Floor(Lots);
    //                lot_AllowedMaster = Convert.ToInt32(floor2);

    //                SumLotAll = lot_AllowedMaster + lot_Allowed;
    //                allowed = Convert.ToInt32(SumLotAll);
    //                GetCheckLots();
    //                txtRemDhanlLot.Text = Convert.ToString(allowed);

    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने की सिक्यूरिटी राशि जमा नहीं की है इसलिए इसका Delivery Order issue नहीं होगा|'); </script> ");
    //            }
    //            //select = "  select isnull(sum(isnull(ChequeValue,0)),0) as ChequeValue   from PM_Add_ChequeAgainstFDR where CropYear='" + txtYear.Text + "' and Miller_Dist='" + DistCode + "' and Miller_ID='" + ddlMillName.SelectedValue.ToString() + "'";

    //            //da = new SqlDataAdapter(select, con);
    //            //ds = new DataSet();
    //            //da.Fill(ds);

    //            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            //{
    //            //}
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

    public void GetAgreementNumber()
    {
        string DistCode = Session["dist_id"].ToString();
        //txtRemDhanlLot.Text = ""
        txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtFromDate.Text = txtToDate.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = "";
        hdfFCI.Value = "";
        ddlDhanLot.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        FCI.Visible = false;



        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                    {
                        select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and User_Agent!='DDMO' order by Agreement_ID";
                    }
                    else if (Session["DistrictManager"].ToString() == "DDMO")
                    {
                        select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and User_Agent='DDMO' order by Agreement_ID";
                    }

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");
                        ddlDhanLot.Enabled = true;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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
    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Empty;
        //txtRemDhanlLot.Text = "";
        txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtFromDate.Text = txtToDate.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtLotNumber.Text = txtQuantity.Text = txtQty.Text = txtGodownMapRemQty.Text = "";
        hdfFCI.Value = "";
        ddlDhanLot.Items.Clear();
        ddlIssueCentre.Items.Clear();
        ddlGodown.Items.Clear();
        FCI.Visible = false;

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "", DeliverdToFCI = "", StateCode = "", GetState = "";
                select = "Select State_Code,DeliverdToFCI,Common_Dhan,GradeA_Dhan,DhanAmountDetails,Rem_Common_Dhan,Rem_GradeA_Dhan,Rem_DhanLot,Milling_Type From PaddyMilling_Agreement_2017 where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtTotalCDhan.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();
                    txtTotalGDhan.Text = ds.Tables[0].Rows[0]["GradeA_Dhan"].ToString();

                    // txtRemDhanlLot.Text = ds.Tables[0].Rows[0]["Rem_DhanLot"].ToString();
                    int DhanLotAgrmt = int.Parse(ds.Tables[0].Rows[0]["DhanAmountDetails"].ToString());

                    txtRemCommonDhan.Text = ds.Tables[0].Rows[0]["Rem_Common_Dhan"].ToString();
                    txtRemGradeADhan.Text = ds.Tables[0].Rows[0]["Rem_GradeA_Dhan"].ToString();

                    hdfMillingType.Value = ds.Tables[0].Rows[0]["Milling_Type"].ToString();

                    DeliverdToFCI = hdfFCI.Value = ds.Tables[0].Rows[0]["DeliverdToFCI"].ToString();
                    StateCode = ds.Tables[0].Rows[0]["State_Code"].ToString();
                    //GetCheck();

                    //if (DeliverdToFCI != "Y")
                    //{
                    //    if (txtRemDhanlLot.Text == "0" || (int.Parse(txtRemDhanlLot.Text)) < 0)
                    //    {
                    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अधिकतम धान आबंटन की सीमा समाप्त हो चुकी हैं|'); </script> ");
                    //        btnSubmit.Enabled = btnAdd.Enabled = false;
                    //        return;
                    //    }
                    //}
                    if (txtRemCommonDhan.Text == "0" && txtRemGradeADhan.Text == "0")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जितने धान का अनुबंध किया था, उसके अनुसार पूरा धान ले जा चूका है, कृपया नया अनुबंध करें|'); </script> ");
                        btnSubmit.Enabled = btnAdd.Enabled = false;
                        return;
                    }

                    for (int i = 1; i <= DhanLotAgrmt; i++)
                    {
                        ddlDhanLot.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddlDhanLot.Items.Insert(0, "--Select--");
                    //con.Open();
                    string checkExistPage = "Select distinct (Cast(DhanLot As INT)) As DhanLot From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and District='" + districtid + "' and CropYear='" + txtYear.Text + "'";
                    da1 = new SqlDataAdapter(checkExistPage, con);
                    ds1 = new DataSet();
                    da1.Fill(ds1);

                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            string checkpage = ds1.Tables[0].Rows[i][0].ToString();

                            if (ddlDhanLot.Items.FindByValue(checkpage) != null)
                            {
                                ddlDhanLot.Items.FindByValue(checkpage).Enabled = false;
                            }
                        }
                    }

                    btnAdd.Enabled = true;

                    //int count = 1;

                    //if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    //    {
                    //        string checkpage = ds1.Tables[0].Rows[i][0].ToString();

                    //        if (ddlDhanLot.Items.FindByValue(checkpage) != null)
                    //        {
                    //            ddlDhanLot.Items.FindByValue(checkpage).Enabled = false;
                    //            count = count + 1;
                    //        }
                    //    }
                    //}

                    //for (int i = 0; i <= ddlDhanLot.Items.Count; i++)
                    //{
                    //    string check = ddlDhanLot.Items[i].ToString();

                    //    if (i == 0 || i == count)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ddlDhanLot.Items.FindByValue(check).Enabled = false;
                    //    }
                    //}

                    //btnAdd.Enabled = true;

                    if (DeliverdToFCI == "Y")
                    {
                        FCI.Visible = true;
                        GetState = "Select State_Name From State_Master where State_Code='" + StateCode + "'";
                        da2 = new SqlDataAdapter(GetState, con);
                        ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                        {
                            lblDist.Text = ds2.Tables[0].Rows[0]["State_Name"].ToString();
                        }
                    }
                    //GetCheck();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear,ArvaChawal,UshnaChawal FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfArvaChawalRs.Value = ds.Tables[0].Rows[0]["ArvaChawal"].ToString();
                    hdfUshnaChawalRs.Value = ds.Tables[0].Rows[0]["UshnaChawal"].ToString();

                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetMPIssueCentre()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                hdfDistanceDist.Value = hdfMappedDist.Value = "";

                //select = "Select distinct IC.DepotID,IC.DepotName From PaddyMapping_Godown As MAP Left Join tbl_MetaData_DEPOT As IC ON(MAP.IssueCenter=IC.DepotID and '23'+MAP.District = IC.DistrictId)where MAP.District='" + districtid + "' and MAP.CropYear='" + txtYear.Text + "' and MAP.Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MAP.Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "'";

                select = "Select distinct IssueCenter As DepotID,District From PaddyMapping_Godown As MAP where MAP.CropYear='" + txtYear.Text + "' and MAP.Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MAP.Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfDistanceDist.Value = hdfMappedDist.Value = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfMappedDist.Value += ((hdfMappedDist.Value == "") ? "" : " , ") + "'23" + ds.Tables[0].Rows[i]["District"].ToString() + "'";
                        hdfDistanceDist.Value += ((hdfDistanceDist.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["DepotID"].ToString() + "'";
                    }

                    GetWareHouseIC();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर को किस प्रदाय केंद्र तथा किस गोदाम से धान ले जाना है, कृपया उसकी Mapping करे|'); </script> ");
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

    public void GetWareHouseIC()
    {
        districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId IN(" + hdfMappedDist.Value + ") and StateId='23' and DepotID IN(" + hdfDistanceDist.Value + ") order by DepotName";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlIssueCentre.DataSource = ds.Tables[0];
                        ddlIssueCentre.DataTextField = "DepotName";
                        ddlIssueCentre.DataValueField = "DepotID";
                        ddlIssueCentre.DataBind();
                        ddlIssueCentre.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        txtGodownMapRemQty.Text = "";

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            GetCSMSGodown();
            GetWarehouseGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
        }
    }

    public void GetCSMSGodown()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select distinct Godown_id From PaddyMapping_Godown As MAP where MAP.CropYear='" + txtYear.Text + "' and MAP.Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MAP.Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and MAP.IssueCenter='" + ddlIssueCentre.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlGodown.DataSource = ds.Tables[0];
                    ddlGodown.DataTextField = "Godown_id";
                    ddlGodown.DataValueField = "Godown_id";
                    ddlGodown.DataBind();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('गोदाम उपलब्ध नहीं है|'); </script> ");
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

    public void GetWarehouseGodown()
    {
        string select = "", GodownID = "";
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();

                for (int i = 0; i < ddlGodown.Items.Count; i++)
                {
                    GodownID += ((GodownID == "") ? "" : " , ") + "'" + ddlGodown.Items[i].ToString() + "'";
                }

                select += "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and Godown_ID IN (" + GodownID + ")";
                da = new SqlDataAdapter(select, con_MPStorage);
                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        districtid = Session["dist_id"].ToString();

        double TotalQty = double.Parse(txtQuantity.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (TotalQty != 0 && AllotedQty != 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Alloted All 403 Or 397 Qtls Quantity'); </script> ");
            return;
        }

        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            if (txtLotNumber.Text != "" && txtYear.Text != "")
            {
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

                            ClientIP objClientIP = new ClientIP();
                            string GetIp = objClientIP.GETIP();

                            //string browser = Request.Browser.Browser.ToString();
                            //string version = Request.Browser.Version.ToString();
                            string useragent = Session["DistrictManager"].ToString();

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string ConvertFromDate = ServerDate.getDate_MDY(txtFromDate.Text);
                            string ConvertToDate = ServerDate.getDate_MDY(txtToDate.Text);

                            Final_DO_Number = ddlAgtmtNumber.SelectedItem.ToString() + ddlDhanLot.SelectedItem.ToString();

                            int Rem_Alloted_CommonDhan = 0;
                            string instr = "";
                            decimal ActualValue = 0;

                            if (hdfMillingType.Value == "अरवा")
                            {
                                Rem_Alloted_CommonDhan = ((int.Parse(txtRemCommonDhan.Text)) - 403);

                                if (hdfFCI.Value == "Y")
                                {
                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                       "Update PaddyMilling_Agreement_2017 Set Rem_Common_Dhan=(Rem_Common_Dhan-403) , Rem_Total_Dhan=(Rem_Total_Dhan-403) where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";
                                }
                                else
                                {
                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                       "Update PaddyMilling_Agreement_2017 Set Rem_Common_Dhan=(Rem_Common_Dhan-403) , Rem_Total_Dhan=(Rem_Total_Dhan-403) , Rem_DhanLot=(Rem_DhanLot-1) where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";
                                }

                                MillingType = "अरवा";

                                DataTable dt = adddetails();
                                if (dt != null)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {

                                        if (rdbSociety.Checked == true)
                                        {
                                            string RMO = Final_DO_Number + (i + 1);
                                            ActualValue += (decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString()));
                                            RArva = (((decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString())) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);

                                            IsSociety = "Y";


                                            //IsGodown, IssueCenter, Godown = "";
                                            instr += "Insert Into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNo,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNumber,   IsSociety, SocietyDist, SocietyID, IsGodown) Values('" + RMO + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + districtid + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + MillingType + "','" + dt.Rows[i]["QuantityQtls"] + "','0','" + RArva + "','0','0','" + GetIp + "',GETDATE(),'" + useragent + "','" + Final_DO_Number + "','N','" + ddlDhanLot.SelectedItem.ToString() + "','0000','0','" + GridView2.Rows[i].Cells[0].Text + "','" + Rem_Alloted_CommonDhan + "','0','" + RMO + "','" + IsSociety + "', '" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["GodownVal"] + "','N') ;";
                                            // instr += "Update PaddyMapping_Godown set Rem_CommonPaddy=(Rem_CommonPaddy-" + dt.Rows[i]["QuantityQtls"] + ") where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and Godown_id='" + dt.Rows[i]["GodownVal"] + "' ";
                                            //  instr = "Insert Into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNo,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNumber,   IsSociety, SocietyDist, SocietyID, IsGodown) Values('" + RMO + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + districtid + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + MillingType + "','" + dt.Rows[i]["QuantityQtls"] + "','0','" + RArva + "','0','" + IssueCenter1 + "','" + GetIp + "',GETDATE(),'" + useragent + "','" + Final_DO_Number + "','N','" + ddlDhanLot.SelectedItem.ToString() + "','0000','" + Godown1 + "','" + GridView1.Rows[i].Cells[0].Text + "','" + Rem_Alloted_CommonDhan + "','0','" + RMO + "','" + IsSociety + "', '" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["GodownVal"] + "','N') ;";


                                        }
                                        else if (rdbGodown.Checked == true)
                                        {
                                            string RMO = Final_DO_Number + (i + 1);
                                            ActualValue += (decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString()));
                                            RArva = (((decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString())) * decimal.Parse(hdfArvaChawalRs.Value)) / 100);

                                            string IsSociety, SocietyDist, Society, IsGodown, IssueCenter, Godown = "";

                                            IsGodown = "Y";

                                            instr += "Insert Into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNo,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNumber,   IsSociety, SocietyDist, SocietyID, IsGodown) Values('" + RMO + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + districtid + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + MillingType + "','" + dt.Rows[i]["QuantityQtls"] + "','0','" + RArva + "','0','" + dt.Rows[i]["ICVal"] + "','" + GetIp + "',GETDATE(),'" + useragent + "','" + Final_DO_Number + "','N','" + ddlDhanLot.SelectedItem.ToString() + "','0000','" + dt.Rows[i]["GodownVal"] + "','" + GridView1.Rows[i].Cells[0].Text + "','" + Rem_Alloted_CommonDhan + "','0','" + RMO + "','0', '0','0','" + IsGodown + "') ;";
                                            instr += "Update PaddyMapping_Godown set Rem_CommonPaddy=(Rem_CommonPaddy-" + dt.Rows[i]["QuantityQtls"] + ") where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and Godown_id='" + dt.Rows[i]["GodownVal"] + "' ";

                                        }

                                        //    instr += "Insert Into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNo,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNumber) Values('" + RMO + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + districtid + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + MillingType + "','" + dt.Rows[i]["QuantityQtls"] + "','0','" + RArva + "','0','" + dt.Rows[i]["ICVal"] + "','" + GetIp + "',GETDATE(),'" + useragent + "','" + Final_DO_Number + "','N','" + ddlDhanLot.SelectedItem.ToString() + "','0000','" + dt.Rows[i]["GodownVal"] + "','" + GridView1.Rows[i].Cells[0].Text + "','" + Rem_Alloted_CommonDhan + "','0','" + RMO + "') ;";
                                        //    instr += "Update PaddyMapping_Godown set Rem_CommonPaddy=(Rem_CommonPaddy-" + dt.Rows[i]["QuantityQtls"] + ") where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and Godown_id='" + dt.Rows[i]["GodownVal"] + "' ";
                                        //}
                                    }
                                }

                                if (ActualValue == 403)
                                {
                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपकी Qty 403 से ज्यादा या कम नहीं होनी चाहिए|'); </script> ");
                                    return;
                                }
                            }
                            else if (hdfMillingType.Value == "उसना")
                            {
                                Rem_Alloted_CommonDhan = ((int.Parse(txtRemCommonDhan.Text)) - 397);

                                if (hdfFCI.Value == "Y")
                                {
                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                "Update PaddyMilling_Agreement_2017 Set Rem_Common_Dhan=(Rem_Common_Dhan-397) , Rem_Total_Dhan=(Rem_Total_Dhan-397) where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";
                                }
                                else
                                {
                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                "Update PaddyMilling_Agreement_2017 Set Rem_Common_Dhan=(Rem_Common_Dhan-397) , Rem_Total_Dhan=(Rem_Total_Dhan-397), Rem_DhanLot=(Rem_DhanLot-1) where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "'; ";
                                }

                                MillingType = "उसना";

                                DataTable dt = adddetails();
                                if (dt != null)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {

                                        if (rdbSociety.Checked == true)
                                        {
                                            string RMO = Final_DO_Number + (i + 1);
                                            ActualValue += (decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString()));
                                            RUsnha = (((decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString())) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                            string IsSociety, SocietyDist, Society, IsGodown, IssueCenter, Godown = "";
                                            IsSociety = "Y";
                                            instr += "Insert Into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNo,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNumber, IsSociety, SocietyDist, SocietyID, IsGodown) Values('" + RMO + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + districtid + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + MillingType + "','" + dt.Rows[i]["QuantityQtls"] + "','0','0','" + RUsnha + "','0','" + GetIp + "',GETDATE(),'" + useragent + "','" + Final_DO_Number + "','N','" + ddlDhanLot.SelectedItem.ToString() + "','0000','0','" + GridView2.Rows[i].Cells[0].Text + "','" + Rem_Alloted_CommonDhan + "','0','" + RMO + "','" + IsSociety + "', '" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["GodownVal"] + "','N') ;";

                                        }
                                        else if (rdbGodown.Checked == true)
                                        {
                                            string RMO = Final_DO_Number + (i + 1);
                                            ActualValue += (decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString()));
                                            RUsnha = (((decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString())) * decimal.Parse(hdfUshnaChawalRs.Value)) / 100);
                                            string IsSociety, SocietyDist, Society, IsGodown, IssueCenter, Godown = "";

                                            IsGodown = "Y";
                                            instr += "Insert Into PaddyMilling_DO_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,DhanLot,Branch_Code,Godown_Code,LotNo,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,LotNumber, IsSociety, SocietyDist, SocietyID, IsGodown) Values('" + RMO + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + districtid + "','" + txtYear.Text + "','" + ConvertFromDate + "','" + ConvertToDate + "',N'" + MillingType + "','" + dt.Rows[i]["QuantityQtls"] + "','0','0','" + RUsnha + "','" + dt.Rows[i]["ICVal"] + "','" + GetIp + "',GETDATE(),'" + useragent + "','" + Final_DO_Number + "','N','" + ddlDhanLot.SelectedItem.ToString() + "','0000','" + dt.Rows[i]["GodownVal"] + "','" + GridView1.Rows[i].Cells[0].Text + "','" + Rem_Alloted_CommonDhan + "','0','" + RMO + "','0', '0','0','" + IsGodown + "') ;";
                                            instr += "Update PaddyMapping_Godown set Rem_CommonPaddy=(Rem_CommonPaddy-" + dt.Rows[i]["QuantityQtls"] + ") where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and Godown_id='" + dt.Rows[i]["GodownVal"] + "' ";

                                        }

                                    }
                                }

                                if (ActualValue == 397)
                                {
                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपकी Qty 397 से ज्यादा या कम नहीं होनी चाहिए|'); </script> ");
                                    return;
                                }
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Delivery Order Number Is " + Final_DO_Number + " '); </script> ");
                                btnPrint.Enabled = true;
                                btnSubmit.Enabled = false;
                                lblDONumber.Text = "Your Delivery Order Number Is : " + Final_DO_Number;
                                lblDONumber.Visible = true;
                                Session["AgrmtNo"] = Final_DO_Number;
                                Session["ICGBQ"] = null;
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
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया सही दिनांक चुनें|'); </script> ");
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया लॉट क्रमांक प्रविष्ट करें|'); </script> ");
            }
        }
        else
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PMilling_DO.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void ddlDhanLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQuantity.Text = txtLotNumber.Text = txtQty.Text = "";
        txtQty.Enabled = btnAdd.Enabled = false;

        if (ddlDhanLot.SelectedIndex > 0)
        {
            string DistCode = Session["dist_id"].ToString();

            using (con = new SqlConnection(strcon))
            {
                try
                {
                    //string select = "";
                    //select = "select Agreement_ID from PaddyMilling_Agreement_2017 where GETDATE ()<To_Date AND Mill_Name='"+ddlMillName.SelectedValue.ToString()+"' and District='"+DistCode+"' and Agreement_ID='"+ddlAgtmtNumber.SelectedValue.ToString()+"'";

                    string select = "";
                    select = "select Agreement_ID from PaddyMilling_Agreement_2017 where Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and District='" + DistCode + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "'";


                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        txtLotNumber.Text = "Lot" + ddlDhanLot.SelectedItem.ToString();

                        if (hdfMillingType.Value == "उसना")
                        {
                            txtQuantity.Text = txtQty.Text = "397";
                        }
                        else
                        {
                            txtQuantity.Text = txtQty.Text = "403";
                        }
                        txtQty.Enabled = btnAdd.Enabled = true;
                    }

                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस अनुबंध की वैधता समाप्त हो गयी है|'); </script> ");
                        // ddlMillName.ClearSelection();
                        // btnSubmit.Enabled = false;
                        ddlDhanLot.ClearSelection();
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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया धान के प्रदाय का लॉट चुने|'); </script> ");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (rdbSociety.Checked == false && rdbGodown.Checked == false)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown or society'); </script> ");
            return;
        }

        if (rdbSociety.Checked == true)
        {
            tblgodown.Visible = false;
            tblsociety.Visible = true;

            double TotalQty = double.Parse(txtQuantity.Text);
            double AllotedQty = double.Parse(txtQty.Text);
            if (ddlDhanLot.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया धान के प्रदाय का लॉट चुने|'); </script> ");
                return;
            }
            if (ddlsocietydist.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Society District'); </script> ");
                return;
            }
            if (ddlsociety.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Society'); </script> ");
                return;
            }
            else if (txtQty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Quantity'); </script> ");
                return;
            }
            else if (TotalQty < AllotedQty)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Allow Because Add Quantity Is Greater Than Rem. Quantity'); </script> ");
                return;
            }
            //else if (GodamRemQty <= 0)
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping के आधार पर गोदाम में बची हुई मात्रा कम होने के कारण आप इस गोदाम से DO जारी नहीं कर सकते|'); </script> ");
            //    return;
            //}
            //else if (GodamRemQty < AllotedQty)
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping के आधार पर गोदाम में बची हुई मात्रा से ज्यादा की मात्रा आप Add नहीं कर सकते|'); </script> ");
            //    return;
            //}
            else
            {
                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("SocietyDist");
                    dt.Columns.Add("ICVal");
                    dt.Columns.Add("SocietyName");
                    dt.Columns.Add("GodownVal");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("QuantityQtls");
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (ddlsociety.SelectedValue.ToString() == dt.Rows[i]["GodownVal"].ToString())
                    {
                        count = 1;
                        break;
                    }
                }

                if (count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["SocietyDist"] = ddlsocietydist.SelectedItem.Text;
                    dr["ICVal"] = ddlsocietydist.SelectedValue;
                    dr["SocietyName"] = ddlsociety.SelectedItem.Text;
                    dr["GodownVal"] = ddlsociety.SelectedValue;
                    dr["Quantity"] = txtQty.Text;
                    dr["QuantityQtls"] = ((double.Parse(txtQty.Text)));

                    dr["quantity"] = (AllotedQty).ToString("0.00");
                    dt.Rows.Add(dr);
                    Session["ICGBQ"] = dt;
                    ddlDhanLot.Enabled = ddlAgtmtNumber.Enabled = ddlMillName.Enabled = false;
                    fillgrid();

                    txtQuantity.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();

                    if (txtQty.Text == "0" || txtQuantity.Text == "0")
                    {
                        btnAdd.Enabled = false;
                        txtQty.Enabled = false;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        txtQty.Enabled = true;
                        btnSubmit.Enabled = false;
                    }
                    txtQty.Focus();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Society Is Not Allowed'); </script> ");
                    return;
                }

            }
        }
        else if (rdbGodown.Checked == true)
        {
            tblsociety.Visible = false;
            tblgodown.Visible = true;

            double TotalQty = double.Parse(txtQuantity.Text);
            double AllotedQty = double.Parse(txtQty.Text);
            double GodamRemQty = double.Parse(txtGodownMapRemQty.Text);

            if (ddlDhanLot.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया धान के प्रदाय का लॉट चुने|'); </script> ");
                return;
            }
            if (ddlGodown.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown'); </script> ");
                return;
            }
            else if (txtQty.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Quantity'); </script> ");
                return;
            }
            else if (TotalQty < AllotedQty)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Allow Because Add Quantity Is Greater Than Rem. Quantity'); </script> ");
                return;
            }
            else if (GodamRemQty <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping के आधार पर गोदाम में बची हुई मात्रा कम होने के कारण आप इस गोदाम से DO जारी नहीं कर सकते|'); </script> ");
                return;
            }
            else if (GodamRemQty < AllotedQty)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mapping के आधार पर गोदाम में बची हुई मात्रा से ज्यादा की मात्रा आप Add नहीं कर सकते|'); </script> ");
                return;
            }
            else
            {
                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("ICName");
                    dt.Columns.Add("ICVal");
                    dt.Columns.Add("GodownName");
                    dt.Columns.Add("GodownVal");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("QuantityQtls");
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (ddlGodown.SelectedValue.ToString() == dt.Rows[i]["GodownVal"].ToString())
                    {
                        count = 1;
                        break;
                    }
                }

                if (count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                    dr["ICVal"] = ddlIssueCentre.SelectedValue;
                    dr["GodownName"] = ddlGodown.SelectedItem.Text;
                    dr["GodownVal"] = ddlGodown.SelectedValue;
                    dr["Quantity"] = txtQty.Text;
                    dr["QuantityQtls"] = ((double.Parse(txtQty.Text)));

                    dr["quantity"] = (AllotedQty).ToString("0.00");
                    dt.Rows.Add(dr);
                    Session["ICGBQ"] = dt;
                    ddlDhanLot.Enabled = ddlAgtmtNumber.Enabled = ddlMillName.Enabled = false;
                    fillgrid();

                    txtQuantity.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();

                    if (txtQty.Text == "0" || txtQuantity.Text == "0")
                    {
                        btnAdd.Enabled = false;
                        txtQty.Enabled = false;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        txtQty.Enabled = true;
                        btnSubmit.Enabled = false;
                    }
                    txtQty.Focus();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Godown Is Not Allowed'); </script> ");
                    return;
                }
            }
        }

    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["ICGBQ"];
        return dt;
    }

    public void fillgrid()
    {
        if (rdbSociety.Checked == true)
        {

            DataTable dt = adddetails();
            if (dt == null)
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
        }
        else if (rdbGodown.Checked == true)
        {
            DataTable dt = adddetails();
            if (dt == null)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;

            e.Row.Cells[3].Text = "Quantity (Qtls)";

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = txtLotNumber.Text + "/" + (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[3].Text));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total Qty = " + QtyTotal.ToString("0.00");
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (rdbSociety.Checked == true)
        {
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("SocietyDist");
                dt.Columns.Add("ICVal");
                dt.Columns.Add("SocietyName");
                dt.Columns.Add("GodownVal");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("QuantityQtls");
            }
            else
            {
                string QtyRowValue = dt.Rows[e.RowIndex]["Quantity"].ToString();

                double TotalQty = double.Parse(txtQuantity.Text);
                double AllotedQty = double.Parse(QtyRowValue);

                txtQuantity.Text = txtQty.Text = (TotalQty + AllotedQty).ToString();

                dt.Rows.RemoveAt(e.RowIndex);

                if (txtQty.Text == "0" || txtQuantity.Text == "0")
                {
                    btnAdd.Enabled = false;
                    txtQty.Enabled = false;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = true;
                    txtQty.Enabled = true;
                    btnSubmit.Enabled = false;
                }
            }
            Session["ICGBQ"] = dt;
            fillgrid();
            txtQty.Focus();
        }
        else if (rdbGodown.Checked == true)
        {
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("ICName");
                dt.Columns.Add("ICVal");
                dt.Columns.Add("GodownName");
                dt.Columns.Add("GodownVal");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("QuantityQtls");
            }
            else
            {
                string QtyRowValue = dt.Rows[e.RowIndex]["Quantity"].ToString();

                double TotalQty = double.Parse(txtQuantity.Text);
                double AllotedQty = double.Parse(QtyRowValue);

                txtQuantity.Text = txtQty.Text = (TotalQty + AllotedQty).ToString();

                dt.Rows.RemoveAt(e.RowIndex);

                if (txtQty.Text == "0" || txtQuantity.Text == "0")
                {
                    btnAdd.Enabled = false;
                    txtQty.Enabled = false;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = true;
                    txtQty.Enabled = true;
                    btnSubmit.Enabled = false;
                }
            }
            Session["ICGBQ"] = dt;
            fillgrid();
            txtQty.Focus();
        }

    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGodownMapRemQty.Text = "";

        if (ddlGodown.SelectedIndex > 0)
        {
            GodamRemBalance();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम का नाम चुने|'); </script> ");
        }
    }

    public void GodamRemBalance()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Rem_CommonPaddy From PaddyMapping_Godown As MAP where MAP.CropYear='" + txtYear.Text + "' and MAP.Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MAP.Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and MAP.Godown_id='" + ddlGodown.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtGodownMapRemQty.Text = ds.Tables[0].Rows[0]["Rem_CommonPaddy"].ToString();
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



    //public void GetCheck()
    //{
    //    if (rdbSociety.Checked == true)
    //    {
    //        lblsocietydistGodownIC.Visible = true;
    //        lblsociety_Godownname.Visible = true;
    //        lblsocietydistGodownIC.Text = "Society District";
    //        lblsociety_Godownname.Text="Society Name";
    //        ddlIssueCentre.Visible = false;
    //        ddlGodown.Visible = false;
    //        ddlsocietydist.Visible = true;
    //        ddlsociety.Visible = true;
    //        GetSocietyDist();


    //    }
    //    else if(rdbGodown.Checked==true)
    //    {
    //        lblsocietydistGodownIC.Visible = true;
    //        lblsociety_Godownname.Visible = true;
    //        lblsocietydistGodownIC.Text = "Issue Center";
    //        lblsociety_Godownname.Text = "Godown";
    //        ddlsocietydist.Visible = false;
    //        ddlsociety.Visible = false;
    //        ddlIssueCentre.Visible = true;
    //        ddlGodown.Visible=true;

    //    }


    //}
    public void GetSocietyDist()
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
                        ddlsocietydist.DataSource = ds.Tables[0];
                        ddlsocietydist.DataTextField = "district_name";
                        ddlsocietydist.DataValueField = "district_code";
                        ddlsocietydist.DataBind();
                        ddlsocietydist.Items.Insert(0, "--Select--");
                        //ddlsocietydist.SelectedValue = Session["dist_id"].ToString();
                        //GetMPIssueCentre();
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
    public void GetSociety()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select SocietyID, Society_Name+' ('+ SocietyID +')' as SocietyName from MillerToSocietyMapping2017 as MM inner join Society_Kharif17 as SO on SO.Society_Id=MM.SocietyID where MillerId='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and SocietyDistrictId='" + ddlsocietydist.SelectedValue.ToString() + "' order by Society_Name ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlsociety.DataSource = ds.Tables[0];
                        ddlsociety.DataTextField = "SocietyName";
                        ddlsociety.DataValueField = "SocietyID";
                        ddlsociety.DataBind();
                        ddlsociety.Items.Insert(0, "--Select--");
                        //ddlsocietydist.SelectedValue = Session["dist_id"].ToString();
                        //GetMPIssueCentre();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर से किसी भी सोसाइटी की मप्पिंग नहीं की गयी है|'); </script> ");
                        return;
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

    protected void ddlsocietydist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsocietydist.SelectedIndex > 0)
        {
            GetSociety();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Society District'); </script> ");
            return;
        }

    }
    protected void rdbGodown_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSociety.Checked == true)
        {
            rdbGodown.Checked = false;
            lblsocietydistGodownIC.Visible = true;
            lblsociety_Godownname.Visible = true;
            lblsocietydistGodownIC.Text = "Society District";
            lblsociety_Godownname.Text = "Society Name";
            ddlIssueCentre.Visible = false;
            ddlGodown.Visible = false;
            ddlsocietydist.Visible = true;
            ddlsociety.Visible = true;
            tdRemgodownqty.Visible = false;
            tdremqtygo.Visible = false;

            GetSocietyDist();



        }
        else if (rdbGodown.Checked == true)
        {
            rdbSociety.Checked = false;
            lblsocietydistGodownIC.Visible = true;
            lblsociety_Godownname.Visible = true;
            lblsocietydistGodownIC.Text = "Issue Center";
            lblsociety_Godownname.Text = "Godown";
            ddlsocietydist.Visible = false;
            ddlsociety.Visible = false;
            ddlIssueCentre.Visible = true;
            ddlGodown.Visible = true;
            tdRemgodownqty.Visible = true;
            tdremqtygo.Visible = true;
            GetMPIssueCentre();


        }


    }
    protected void rdbSociety_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSociety.Checked == true)
        {
            rdbGodown.Checked = false;
            lblsocietydistGodownIC.Visible = true;
            lblsociety_Godownname.Visible = true;
            lblsocietydistGodownIC.Text = "Society District";
            lblsociety_Godownname.Text = "Society Name";
            ddlIssueCentre.Visible = false;
            ddlGodown.Visible = false;
            ddlsocietydist.Visible = true;
            ddlsociety.Visible = true;
            tdRemgodownqty.Visible = false;
            tdremqtygo.Visible = false;

            GetSocietyDist();


        }
        else if (rdbGodown.Checked == true)
        {
            rdbSociety.Checked = false;
            lblsocietydistGodownIC.Visible = true;
            lblsociety_Godownname.Visible = true;
            lblsocietydistGodownIC.Text = "Issue Center";
            lblsociety_Godownname.Text = "Godown";
            ddlsocietydist.Visible = false;
            ddlsociety.Visible = false;
            ddlIssueCentre.Visible = true;
            ddlGodown.Visible = true;
            tdRemgodownqty.Visible = true;
            tdremqtygo.Visible = true;
            GetMPIssueCentre();


        }

    }

    public void GetCheckLots()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";


                select = "select count(distinct Check_DO) as Check_DO from PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string issueddo = ds.Tables[0].Rows[0]["Check_DO"].ToString();
                    Issued = Convert.ToInt32(issueddo);
                }
                string instr = "";


                instr = "select COUNT (Lot_No) received from CMR_DepositOrder where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and IsAccepted='Y' and (IsStackAccepted='' or IsStackAccepted is null or IsStackAccepted='YES' ) and (IsStackRejected='' or IsStackRejected is null or IsStackRejected='NO')";

                dar = new SqlDataAdapter(instr, con);
                dsr = new DataSet();
                dar.Fill(dsr);
                if (dsr.Tables.Count > 0 && dsr.Tables[0].Rows.Count > 0)
                {
                    string Receiveddo = dsr.Tables[0].Rows[0]["received"].ToString();
                    received = Convert.ToInt32(Receiveddo);
                }
                Yreceived = Issued - received;

                if (Issued == received)
                {
                    GetAgreementNumber();
                }
                else if (Yreceived < allowed)
                {
                    GetAgreementNumber();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('For new Delivery order, the amount is not available also CMR is not yet received to release the amount which is deposited by the miller.'); </script> ");
                    // ddlMillName.ClearSelection();

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


    public void GetAddCheque()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";


                select = "select isnull(sum(isnull(ChequeValue,0)),0) as ChequeValue from PM_Add_ChequeAgainstFDR where Miller_ID='" + ddlMillName.SelectedValue.ToString() + "'  and CropYear='" + txtYear.Text + "' and DateCheque+180>=Getdate()";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string Addcheque = ds.Tables[0].Rows[0]["ChequeValue"].ToString();
                    txtvalueCheck.Text = Convert.ToString(Convert.ToDecimal(txtvalueCheck.Text) + Convert.ToDecimal(Addcheque));
                    addChequeValue = Convert.ToInt32(Addcheque);
                    Allowed_Lot = addChequeValue / LotAmt;
                    decimal floor1 = Math.Floor(Allowed_Lot);
                    lot_Allowed = Convert.ToInt32(floor1);
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
    protected void ddlsociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                select = "select SocietyID from MillerToSocietyMapping2017 where MillerId='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "'  and  SocietyID='" + ddlsociety.SelectedValue.ToString() + "' and SocietyDistrictId='" + ddlsocietydist.SelectedValue.ToString() + "' and GETDATE()<=ToDate ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस उपार्जन समिति की मैपिंग वैधता समाप्त हो गयी है|'); </script> ");
                    // ddlMillName.ClearSelection();
                    // btnSubmit.Enabled = false;
                    ddlsociety.ClearSelection();
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

    public void GetData()
    {
    //    string DistCode = Session["dist_id"].ToString();

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            string select, selectcheck = "";


    //            select = "select FDR_ChequeID, FDR_Value from PM_FDR_and_Cheque_Master where convert(varchar(10), FDR_Maturity, 103)>=convert(varchar(10),GetDate(), 103) and CropYear='" + txtYear.Text + "' and  MillerID='" + ddlMillName.SelectedValue.ToString() + "'";

    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);
    //            int count = ds.Tables[0].Rows.Count;
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {

    //                for (int i = 0; i < count; i++)
    //                {

    //                    string ID = ds.Tables[0].Rows[i]["FDR_ChequeID"].ToString();

    //                    FDRvaluereal = Convert.ToDouble(ds.Tables[0].Rows[i]["FDR_Value"].ToString());
    //                    TotalFDR = TotalFDR + FDRvaluereal;

    //                    selectcheck = "select Cheque_Value from PM_FDR_and_Cheque_Master where convert(varchar(10),Cheque_date, 103)<=convert(varchar(10),GetDate(), 103)  and CropYear='" + txtYear.Text + "' and FDR_ChequeID='" + ID + "' and  MillerID='" + ddlMillName.SelectedValue.ToString() + "'";

    //                    dac = new SqlDataAdapter(selectcheck, con);
    //                    dsc = new DataSet();
    //                    dac.Fill(dsc);
    //                    if (dsc.Tables[0].Rows.Count > 0)
    //                    {

    //                        {
    //                            ChequeValuereal = Convert.ToDouble(dsc.Tables[0].Rows[0]["Cheque_Value"].ToString());
    //                            totalCheque = totalCheque + ChequeValuereal;
    //                        }
    //                    }



    //                }
    //                txtvalueFDR.Text = Convert.ToString(TotalFDR);
    //                txtvalueCheck.Text = Convert.ToString(totalCheque);
    //                decimal TotalBalMaster = Convert.ToDecimal(txtvalueFDR.Text) + Convert.ToDecimal(txtvalueCheck.Text);
    //                decimal Lots = TotalBalMaster / LotAmt;
    //                decimal floor2 = Math.Floor(Lots);
    //                lot_AllowedMaster = Convert.ToInt32(floor2);

    //                GetAddCheque();


    //                SumLotAll = lot_AllowedMaster + lot_Allowed;
    //                allowed = Convert.ToInt32(SumLotAll);
    //                GetCheckLots();
    //                txtRemDhanlLot.Text = Convert.ToString(allowed);



    //            }

    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने की सिक्यूरिटी राशि जमा नहीं की है इसलिए इसका Delivery Order issue नहीं होगा|'); </script> ");
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
        GetFDRValues();
        GetChequeValues();
        decimal TotalBalMaster = Convert.ToDecimal(txtvalueFDR.Text) + Convert.ToDecimal(txtvalueCheck.Text);
        decimal Lots = TotalBalMaster / LotAmt;
        decimal floor2 = Math.Floor(Lots);
        lot_AllowedMaster = Convert.ToInt32(floor2);

        GetAddCheque();


        SumLotAll = lot_AllowedMaster + lot_Allowed;
        allowed = Convert.ToInt32(SumLotAll);
        if(allowed==0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने की सिक्यूरिटी राशि जमा नहीं की है इसलिए इसका Delivery Order issue नहीं होगा|'); </script> ");
            return;
        }
        else if (allowed!=0)
        { GetCheckLots(); }         
     
        txtRemDhanlLot.Text = Convert.ToString(allowed);

    }

    public void GetFDRValues()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select, selectcheck = "";


                select = "select  isnull(sum(FDR_Value),0) as FDR_Value from PM_FDR_and_Cheque_Master where CropYear='" + txtYear.Text + "' and MillerID='" + ddlMillName.SelectedValue.ToString() + "' and  FDR_Maturity>=GetDate()";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {




                    txtvalueFDR.Text = ds.Tables[0].Rows[0]["FDR_Value"].ToString();
                    if (txtvalueFDR.Text=="0")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने की सिक्यूरिटी राशि जमा नहीं की है इसलिए इसका Delivery Order issue नहीं होगा|'); </script> ");
                        return;
                    }
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने की सिक्यूरिटी राशि जमा नहीं की है इसलिए इसका Delivery Order issue नहीं होगा|'); </script> ");
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
    public void GetChequeValues()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select, selectcheck = "";


                select = "select  isnull(sum(Cheque_Value),0) as Cheque_Value from PM_FDR_and_Cheque_Master where CropYear='"+txtYear.Text+"' and MillerID='"+ddlMillName.SelectedValue.ToString()+"' and (FDR_Maturity>=GetDate() and  Cheque_date+180>=GetDate())";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {





                    txtvalueCheck.Text = ds.Tables[0].Rows[0]["Cheque_Value"].ToString();


                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने की सिक्यूरिटी राशि जमा नहीं की है इसलिए इसका Delivery Order issue नहीं होगा|'); </script> ");
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