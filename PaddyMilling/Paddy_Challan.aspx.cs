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
using Data;
using DataAccess;

public partial class PaddyMilling_Paddy_Challan : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "", strBranchval = "", strCommodity = "", strGodownVal = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["Paddy_Challan"] = "";
                Session["MillCode"] = "";
                Session["Agreement_ID"] = "";

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                GetCropYearValues();
                GetDistName();
              
                GetBagsType();

                //rdbSBT.Checked = true;

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }
    public void GetBagsType()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format(" select Bag_Type_ID, BagType from FIN_Bag_Type where Bag_Type_ID!='4' order by BagType desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlbagstype.DataSource = ds.Tables[0];
                    ddlbagstype.DataTextField = "BagType";
                    ddlbagstype.DataValueField = "Bag_Type_ID";
                    ddlbagstype.DataBind();
                    ddlbagstype.Items.Insert(0, "--Select--");
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
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
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

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code!='" + Dist_Id + "' Order By district_name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist.DataSource = ds.Tables[0];
                        ddlFrmDist.DataTextField = "district_name";
                        ddlFrmDist.DataValueField = "district_code";
                        ddlFrmDist.DataBind();
                        ddlFrmDist.Items.Insert(0, "--Select--");
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

    public void GetBranch()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + IC_Id + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfGodown.Value = "";
        ddlGodown.Items.Clear();
        ddlDOnumber.Items.Clear();

        txtBalQtyInGodown.Text = txtBalBagInGodown.Text = txtLotNo.Text = txtMillName.Text = txtDODate.Text = txtDOLastDate.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtIssuedQty.Text = txtIssuedBags.Text = txtTCNo.Text = txtIssuedDate.Text = "";

        if (ddlBranch.SelectedIndex > 0)
        {
            if (chkChange.Checked)
            {
                GETMappingGodown(); //For Other Dist Paddy Godown
                if (hdfGodown.Value != "")
                {
                    OnlyMappedGodown();
                }
            }
            else
            {
                GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
        }
    }

    public void GetGodown()
    {
        if(ddlbagstype.SelectedIndex<=0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bags Type'); </script> ");
            ddlBranch.ClearSelection();
            return;
        
        }

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown Not Available'); </script> ");
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

    public void OnlyMappedGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID IN(" + hdfGodown.Value + ") order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मुख्यालय से गोदाम की मैपिंग कीजिये|'); </script> ");
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

    public void GETMappingGodown()
    {
        Dist_Id = Session["dist_id"].ToString();
        hdfGodown.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "Select Godown_id From PM_DistToGodown Where FrmDistrict='" + Dist_Id + "' and ToDistrict='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfGodown.Value += ((hdfGodown.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Godown_id"].ToString() + "'";
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मुख्यालय से गोदाम की मैपिंग कीजिये|'); </script> ");
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

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDOnumber.Items.Clear();
        ddlstackNumber.Items.Clear();
        txtBalQtyInGodown.Text = txtBalBagInGodown.Text = txtLotNo.Text = txtMillName.Text = txtDODate.Text = txtDOLastDate.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtIssuedQty.Text = txtIssuedBags.Text = txtTCNo.Text = txtIssuedDate.Text = "";

        if (ddlGodown.SelectedIndex > 0)
        {
            GetGodownBalance();
            GetStackNumber();
            GetDONumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
        }
    }
    public void GetStackNumber()
    {


        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Stack_ID, Stack_Name  from tbl_MetaData_STACK where Stack_Killed='N' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' order by Stack_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlstackNumber.DataSource = ds.Tables[0];
                        ddlstackNumber.DataTextField = "Stack_Name";
                        ddlstackNumber.DataValueField = "Stack_ID";
                        ddlstackNumber.DataBind();
                        ddlstackNumber.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Stack Number Not Available'); </script> ");
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

    public void GetDONumber()
    {
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                //if (Session["Markfed"].ToString() == "Y")
                //{
                //    //Return_CommonRice is RemCommon Dhan in DO
                //    //select = "Select Check_DO,DO_Number From PaddyMilling_DO where District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and (Issue_Centre='" + IC_Id + "' OR Issue_Centre='" + ddlBranch.SelectedValue.ToString() + "') and Godown_Code='" + ddlGodown.SelectedValue.ToString() + "' and (Alloted_CommonDhan-0.01)>ISNULL(Return_CommonRice,0) and User_Agent='DDMO'";
                //    select = "Select Check_DO,DO_Number From PaddyMilling_DO where District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and Godown_Code='" + ddlGodown.SelectedValue.ToString() + "' and (Alloted_CommonDhan-0.01)>ISNULL(Return_CommonRice,0) and User_Agent='DDMO'";
                //}
                //else
                //{
                //    //Return_CommonRice is RemCommon Dhan in DO
                //    //select = "Select Check_DO,DO_Number From PaddyMilling_DO where District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and (Issue_Centre='" + IC_Id + "' OR Issue_Centre='" + ddlBranch.SelectedValue.ToString() + "') and Godown_Code='" + ddlGodown.SelectedValue.ToString() + "' and (Alloted_CommonDhan-0.01)>ISNULL(Return_CommonRice,0) and User_Agent!='DDMO'";
                //    select = "Select Check_DO,DO_Number From PaddyMilling_DO where District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and Godown_Code='" + ddlGodown.SelectedValue.ToString() + "' and (Alloted_CommonDhan-0.01)>ISNULL(Return_CommonRice,0) and User_Agent!='DDMO'";
                //}

                select = "Select Check_DO,DO_Number From PaddyMilling_DO_2017 where District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "' and Godown_Code='" + ddlGodown.SelectedValue.ToString() + "' and (Alloted_CommonDhan-0.01)>ISNULL(Return_CommonRice,0)";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDOnumber.DataSource = ds.Tables[0];
                    ddlDOnumber.DataTextField = "Check_DO";
                    ddlDOnumber.DataValueField = "DO_Number";
                    ddlDOnumber.DataBind();
                    ddlDOnumber.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम में कोई भी DO Number उपलब्ध नहीं है|'); </script> ");
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

    public void GetGodownBalance()
    {
        if (ddlbagstype.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();
        strBranchval = ddlBranch.SelectedValue.ToString();
        strGodownVal = ddlGodown.SelectedValue.ToString();
        strCommodity = "13"; //Paddy-Common
        string openingdate = "04/01/2016";

        string pqry = "balanceDetails";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                if (chkChange.Checked)
                {
                    //cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = ddlFrmDist.SelectedValue.ToString();
                    //cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = ddlIssueCentre.SelectedValue.ToString();
                    //cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
                    //cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = strCommodity;
                    ////cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
                    //cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;

                    cmdpqty.Parameters.Add("@cropyear", SqlDbType.VarChar).Value = txtYear.Text;
                    cmdpqty.Parameters.Add("@godown", SqlDbType.VarChar).Value = strGodownVal;
                    cmdpqty.Parameters.Add("@cmd", SqlDbType.VarChar).Value = strCommodity;
                    cmdpqty.Parameters.Add("@BagsType", SqlDbType.Int).Value = ddlbagstype.SelectedValue.ToString();
                }
                else
                {
                    //cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = Dist_Id;
                    //cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = IC_Id;
                    //cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
                    //cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = strCommodity;
                    ////cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
                    //cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;

                    cmdpqty.Parameters.Add("@cropyear", SqlDbType.VarChar).Value = txtYear.Text;
                    cmdpqty.Parameters.Add("@godown", SqlDbType.VarChar).Value = strGodownVal;
                    cmdpqty.Parameters.Add("@cmd", SqlDbType.VarChar).Value = strCommodity;
                    cmdpqty.Parameters.Add("@BagsType", SqlDbType.Int).Value = ddlbagstype.SelectedValue.ToString();
                }

                DataSet ds = new DataSet();
                SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

                dr.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Qtybalance"].ToString()), 5);

                    double bags = Convert.ToDouble(ds.Tables[0].Rows[0]["BalanceBags"].ToString());

                    txtBalQtyInGodown.Text = Convert.ToString(stock);
                    txtBalBagInGodown.Text = Convert.ToString(bags);
                    
                    // switch on this condition when uploading

                    //if (stock == 0 || stock < 0)
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस गोदाम में स्टॉक उपलभ नहीं है इसलिए इस गोदाम से चालान जारी नहीं होंगे |'); </script> ");
                    //    btnRecptSubmit.Enabled = false;
                    //    return;

                    //}
                    //if (bags == 0 || bags < 0)
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस गोदाम में Bags उपलभ नहीं है इसलिए इस गोदाम से चालान जारी नहीं होंगे |'); </script> ");
                    //    btnRecptSubmit.Enabled = false;
                    //    return;
                    //}



                    //txtcurntcap.Text = Convert.ToString(stock);
                    //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

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

    protected void ddlDOnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLotNo.Text = txtMillName.Text = txtDODate.Text = txtDOLastDate.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtIssuedQty.Text = txtIssuedBags.Text = txtTCNo.Text = txtIssuedDate.Text = "";
        hdfAgrmtID.Value = hdfMillCode.Value = hdfMillingType.Value = hdfDhanLot.Value = "";

        if (ddlDOnumber.SelectedIndex > 0)
        {
            GetDOData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
        }
    }

    public void GetDOData()
    {
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select DO.DhanLot,DO.Milling_Type,DO.LotNo,DO.Mill_Code As MillCode,MR.Mill_Name,DO.Agreement_ID,DO.From_Date,DO.To_Date,DO.Alloted_CommonDhan,DO.Alloted_GradeADhan,(DO.Alloted_CommonDhan-ISNULL(DO.Return_CommonRice,0)) As RemCDhan,(DO.Alloted_GradeADhan-ISNULL(DO.Return_GradeARice,0)) As RemGradeDhan From PaddyMilling_DO_2017 As DO Left Join Miller_Registration_2017 As MR ON(DO.Mill_Code=MR.Registration_ID and MR.CropYear='" + txtYear.Text + "') where DO.DO_Number='" + ddlDOnumber.SelectedValue.ToString() + "' and DO.District='" + Dist_Id + "' and DO.CropYear='" + txtYear.Text + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfDhanLot.Value = ds.Tables[0].Rows[0]["DhanLot"].ToString();
                    hdfMillingType.Value = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    txtLotNo.Text = ds.Tables[0].Rows[0]["LotNo"].ToString();
                    hdfMillCode.Value = ds.Tables[0].Rows[0]["MillCode"].ToString();
                    txtMillName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    hdfAgrmtID.Value = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();

                    DateTime DOFrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["From_Date"].ToString());
                    txtDODate.Text = DOFrmDate.ToString("dd/MMM/yyyy");

                    DateTime DOEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["To_Date"].ToString());
                    txtDOLastDate.Text = DOEndDate.ToString("dd/MMM/yyyy");

                    txtTotalCDhan.Text = ds.Tables[0].Rows[0]["Alloted_CommonDhan"].ToString();
                    txtTotalGDhan.Text = ds.Tables[0].Rows[0]["Alloted_GradeADhan"].ToString();
                    txtRemCommonDhan.Text = ds.Tables[0].Rows[0]["RemCDhan"].ToString();
                    txtRemGradeADhan.Text = ds.Tables[0].Rows[0]["RemGradeDhan"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('DO Number की अन्य जानकारी उपलब्ध नहीं है|'); </script> ");
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        double dblCPaddy = 0, dblIssuedPaddy = 0;

        int strIssuedBags = int.Parse(txtIssuedBags.Text);

        dblCPaddy = double.Parse(txtRemCommonDhan.Text);
        dblIssuedPaddy = double.Parse(txtIssuedQty.Text);

        ConvertServerDate ServerDate = new ConvertServerDate();
        string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);
        string DODate = ServerDate.getDate_MDY(txtDODate.Text);
        string DOLastDate = ServerDate.getDate_MDY(txtDOLastDate.Text);

        DateTime DTIssuedDate = Convert.ToDateTime(IssuedDate);
        DateTime DTDODate = Convert.ToDateTime(DODate);

        int result = DateTime.Compare(DTIssuedDate, DTDODate);

        if (result == -1)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Date of Issue की दिनांक DO की दिनांक से कम नहीं होना चाहिए|'); </script> ");
            return;
        }

        if (dblIssuedPaddy > dblCPaddy)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issued Qty की मात्रा, Rem. Qty की मात्रा से ज्यादा नहीं होना चाहिए|'); </script> ");
            return;
        }
        if (txtIssuedDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया दिनांक चुने|'); </script> ");
            return;
        }
        else if (txtIssuedQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Qty की मात्रा इंटर करे|'); </script> ");
            return;
        }
        else if (txtIssuedBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Bags की मात्रा इंटर करे|'); </script> ");
            return;
        }
        else if (txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Truck Number इंटर करे|'); </script> ");
            return;
        }
        else if (ddlDOnumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
            return;
        }
        else if (ddlstackNumber.SelectedIndex<=0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Stack Number'); </script> ");
            return;
        }
        else if (ddlbagstype.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        Dist_Id = Session["dist_id"].ToString();
                        IC_Id = Session["issue_id"].ToString();
                        string opid = Session["OperatorId"].ToString();

                        string browser = Request.Browser.Browser.ToString();
                        string version = Request.Browser.Version.ToString();
                        string useragent = browser + version;

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        string year_do = System.DateTime.Now.Date.ToString("yy");

                        string instr = "", SubDCDate = "", DCDate = "", Paddy_Challan = "", Bagstype = "", delivery_order_no_OpenSale = "";

                        //if (rdbSBT.Checked)
                        //{
                        //    Bagstype = "9";
                        //}
                        //else if (rdbOldSBT.Checked)
                        //{
                        //    Bagstype = "10";
                        //}
                        //else
                        //{
                        //    Bagstype = "4";
                        //}

                        con.Open();

                        string selectmax = "";
                        if (chkChange.Checked && ddlIssueCentre.SelectedIndex > 0)
                        {
                            selectmax = "select max(delivery_order_no) as Do_Num,CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate from OpenSale_DO where district_code='" + ddlFrmDist.SelectedValue.ToString() + "'";
                        }
                        else if (chkChange.Checked == false && ddlIssueCentre.SelectedIndex <= 0)
                        {
                            selectmax = "select max(delivery_order_no) as Do_Num,CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate from OpenSale_DO where district_code='" + Dist_Id + "'";
                        }
                        
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            SubDCDate = DCDate.Substring(2);

                            string DO_ID = ds.Tables[0].Rows[0]["Do_Num"].ToString();

                            if (DO_ID == "")
                            {
                                if (chkChange.Checked && ddlIssueCentre.SelectedIndex > 0)
                                {
                                    DO_ID = ddlFrmDist.SelectedValue.ToString() + year_do + "1000";
                                }
                                else
                                {
                                    DO_ID = Dist_Id + year_do + "1000";
                                }
                            }
                            else
                            {
                                string fordo = DO_ID.Substring(DO_ID.Length - 4);

                                Int64 DO_ID_new = Convert.ToInt64(fordo);

                                DO_ID_new = DO_ID_new + 1;

                                string combine = DO_ID_new.ToString();

                                if (chkChange.Checked && ddlIssueCentre.SelectedIndex > 0)
                                {
                                    DO_ID = ddlFrmDist.SelectedValue.ToString() + year_do + combine;
                                }
                                else
                                {
                                    DO_ID = Dist_Id + year_do + combine;
                                }
                            }

                            delivery_order_no_OpenSale = DO_ID;
                        }

                        if (SubDCDate != "" && delivery_order_no_OpenSale != "")
                        {
                            Paddy_Challan = "PDY" + SubDCDate;

                            if (chkChange.Checked && ddlIssueCentre.SelectedIndex>0) // Paddy Stored in Other District
                            {
                                //Return_CommonRice is RemCommon Dhan in DO
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Update PaddyMilling_DO_2017 set Return_CommonRice=(ISNULL(Return_CommonRice,0)+" + dblIssuedPaddy + "),Return_GradeARice='0' where DO_Number='" + ddlDOnumber.SelectedValue.ToString() + "' and District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "';";

                                //issue_to = 1 is Paddy To Miller & gate_pass is Truck No & NoTransaction is BagType In PaddyMilling_IssueAgainst_DO Table
                                //disp_time is DhanLot & remark is DO_Number In PaddyMilling_IssueAgainst_DO From PaddyMilling_DO Database Table
                                instr += "Insert Into PaddyMilling_IssueAgainst_DO(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,LotNumber,Milling_Type,Issued_CommonDhan,Issued_GradeADhan,Agreement_ID,Branch,disp_time,remark,updated_date, StackNumber, StackName) values('" + Paddy_Challan + "','" + ddlDOnumber.SelectedItem.ToString() + "','" + Dist_Id + "','" + ddlIssueCentre.SelectedValue.ToString() + "','1','" + dblIssuedPaddy + "','" + strIssuedBags + "','01','" + ddlGodown.SelectedValue.ToString() + "','" + IssuedDate + "','" + txtTCNo.Text + "',GETDATE(),'N','" + GetIp + "','" + opid + "','" + ddlbagstype.SelectedValue.ToString() + "','" + hdfMillCode.Value + "','" + txtYear.Text + "','" + useragent + "','" + txtLotNo.Text + "',N'" + hdfMillingType.Value + "','" + dblIssuedPaddy + "','0','" + hdfAgrmtID.Value + "','" + ddlBranch.SelectedValue.ToString() + "','" + hdfDhanLot.Value + "','" + ddlDOnumber.SelectedValue.ToString() + "','','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";

                                //User_Agent is  Check_DO (ddlDOnumber.SelectedItem.ToString()) & Remarks is DO_NO (ddlDOnumber.SelectedValue.ToString()) From PaddyMilling_DO Database Table
                                //Milling_Type is Bag Type
                                instr += "Insert Into issue_against_OpenSale_do(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,disp_time,remark,updated_date,LotNumber,Milling_Type,Agreement_ID,Branch) values('" + Paddy_Challan + "','" + delivery_order_no_OpenSale + "','" + ddlFrmDist.SelectedValue.ToString() + "','" + ddlIssueCentre.SelectedValue.ToString() + "','1','" + dblIssuedPaddy + "','" + strIssuedBags + "','01','" + ddlGodown.SelectedValue.ToString() + "','" + IssuedDate + "','" + txtTCNo.Text + "',GETDATE(),'N','" + GetIp + "','" + opid + "','N','" + hdfMillCode.Value + "','" + txtYear.Text + "','" + ddlDOnumber.SelectedItem.ToString() + "','','" + ddlDOnumber.SelectedValue.ToString() + "','','" + hdfDhanLot.Value + "','" + Bagstype + "','" + hdfAgrmtID.Value + "','" + ddlBranch.SelectedValue.ToString() + "');";

                                //dd_no is Paddy Challan Number              
                                instr += "Insert into OpenSale_DO(delivery_order_no,district_code,issue_type,do_date,payment_mode,dd_no,dd_date,quantity,rate_per_qtls,tot_amount,amount,bank_id,commodity_id,scheme_id,created_date,IP,OperatorID,Partyname,DO_ValidDate,crop_year) values('" + delivery_order_no_OpenSale + "','" + ddlFrmDist.SelectedValue.ToString() + "','1','" + DODate + "','AD','" + Paddy_Challan + "','','" + dblIssuedPaddy + "','0','0','0','N','13','0',GETDATE(),'" + GetIp + "','" + opid + "','" + hdfMillCode.Value + "','" + DOLastDate + "','" + txtYear.Text + "') ;";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                            else if (chkChange.Checked == false && ddlIssueCentre.SelectedIndex <= 0)
                            {
                                //Return_CommonRice is RemCommon Dhan in DO
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Update PaddyMilling_DO_2017 set Return_CommonRice=(ISNULL(Return_CommonRice,0)+" + dblIssuedPaddy + "),Return_GradeARice='0' where DO_Number='" + ddlDOnumber.SelectedValue.ToString() + "' and District='" + Dist_Id + "' and CropYear='" + txtYear.Text + "';";

                                //issue_to = 1 is Paddy To Miller & gate_pass is Truck No & NoTransaction is BagType In PaddyMilling_IssueAgainst_DO Table
                                //disp_time is DhanLot & remark is DO_Number In PaddyMilling_IssueAgainst_DO From PaddyMilling_DO Database Table
                                instr += "Insert Into PaddyMilling_IssueAgainst_DO(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,LotNumber,Milling_Type,Issued_CommonDhan,Issued_GradeADhan,Agreement_ID,Branch,disp_time,remark,updated_date, StackNumber, StackName) values('" + Paddy_Challan + "','" + ddlDOnumber.SelectedItem.ToString() + "','" + Dist_Id + "','" + IC_Id + "','1','" + dblIssuedPaddy + "','" + strIssuedBags + "','01','" + ddlGodown.SelectedValue.ToString() + "','" + IssuedDate + "','" + txtTCNo.Text + "',GETDATE(),'N','" + GetIp + "','" + opid + "','" + ddlbagstype.SelectedValue.ToString() + "','" + hdfMillCode.Value + "','" + txtYear.Text + "','" + useragent + "','" + txtLotNo.Text + "',N'" + hdfMillingType.Value + "','" + dblIssuedPaddy + "','0','" + hdfAgrmtID.Value + "','" + ddlBranch.SelectedValue.ToString() + "','" + hdfDhanLot.Value + "','" + ddlDOnumber.SelectedValue.ToString() + "','','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";

                                //User_Agent is  Check_DO (ddlDOnumber.SelectedItem.ToString()) & Remarks is DO_NO (ddlDOnumber.SelectedValue.ToString()) From PaddyMilling_DO Database Table
                                //Milling_Type is Bag Type
                                instr += "Insert Into issue_against_OpenSale_do(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,disp_time,remark,updated_date,LotNumber,Milling_Type,Agreement_ID,Branch) values('" + Paddy_Challan + "','" + delivery_order_no_OpenSale + "','" + Dist_Id + "','" + IC_Id + "','1','" + dblIssuedPaddy + "','" + strIssuedBags + "','01','" + ddlGodown.SelectedValue.ToString() + "','" + IssuedDate + "','" + txtTCNo.Text + "',GETDATE(),'N','" + GetIp + "','" + opid + "','N','" + hdfMillCode.Value + "','" + txtYear.Text + "','" + ddlDOnumber.SelectedItem.ToString() + "','','" + ddlDOnumber.SelectedValue.ToString() + "','','" + hdfDhanLot.Value + "','" + ddlbagstype.SelectedValue.ToString() + "','" + hdfAgrmtID.Value + "','" + ddlBranch.SelectedValue.ToString() + "');";

                                //dd_no is Paddy Challan Number              
                                instr += "Insert into OpenSale_DO(delivery_order_no,district_code,issue_type,do_date,payment_mode,dd_no,dd_date,quantity,rate_per_qtls,tot_amount,amount,bank_id,commodity_id,scheme_id,created_date,IP,OperatorID,Partyname,DO_ValidDate,crop_year) values('" + delivery_order_no_OpenSale + "','" + Dist_Id + "','1','" + DODate + "','AD','" + Paddy_Challan + "','','" + dblIssuedPaddy + "','0','0','0','N','13','0',GETDATE(),'" + GetIp + "','" + opid + "','" + hdfMillCode.Value + "','" + DOLastDate + "','" + txtYear.Text + "') ;";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            btnPrint.Enabled = true;

                            Label2.Visible = true;
                            Label2.Text = "Your Paddy Challan Number Is : " + Paddy_Challan;

                            ddlFrmDist.Enabled = ddlIssueCentre.Enabled = ddlBranch.Enabled = ddlGodown.Enabled = ddlDOnumber.Enabled = false;

                            Session["Paddy_Challan"] = Paddy_Challan.ToString();
                            Session["MillCode"] = hdfMillCode.Value;
                            Session["Agreement_ID"] = hdfAgrmtID.Value;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Paddy Challan Number Is " + Paddy_Challan + "'); </script> ");
                            txtTotalCDhan.Text = txtTotalGDhan.Text = "";

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        btnRecptSubmit.Enabled = false;
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
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PDhan_Challan.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/PaddyMillingHome.aspx");
    }

    protected void chkChange_CheckedChanged(object sender, EventArgs e)
    {
        ddlFrmDist.SelectedIndex = 0;
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (chkChange.Checked)
        {
            trMsg.Visible = true;
            ddlstackNumber.Items.Clear();
            ddlbagstype.ClearSelection();
            ddlDOnumber.Items.Clear();
           
            txtBalQtyInGodown.Text = txtBalBagInGodown.Text = txtLotNo.Text = txtMillName.Text = txtDODate.Text = txtDOLastDate.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtIssuedQty.Text = txtIssuedBags.Text = txtTCNo.Text = txtIssuedDate.Text = "";
        }
        else
        {
            trMsg.Visible = false;
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueCentre.Items.Clear();
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist.SelectedValue.ToString() + "' order by DepotName";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueCentre.DataSource = ds.Tables[0];
                    ddlIssueCentre.DataTextField = "DepotName";
                    ddlIssueCentre.DataValueField = "DepotID";
                    ddlIssueCentre.DataBind();
                    ddlIssueCentre.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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
    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            GetBranch1();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
            return;
        }
    }

    public void GetBranch1()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ddlIssueCentre.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddlFrmDist.SelectedValue.ToString() + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
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
    protected void txtIssuedQty_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlbagstype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();
        ddlstackNumber.Items.Clear();
        txtBalBagInGodown.Text = "";
        txtBalQtyInGodown.Text = "";
        btnRecptSubmit.Enabled = true;
        if (ddlbagstype.SelectedIndex > 0)
        {
            GetBranch();
        }
        else {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
    }
}