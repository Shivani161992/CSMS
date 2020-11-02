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

public partial class PaddyMilling_CMR_DepositOrder : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1, daI, daCMR, daSMO;
    DataSet ds, ds1, dsI, dsCMR, dsSMO;
    string SMO;

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

                txtDistManager.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                //GetMillName();
                //GetMPIssueCentre();
                GetDist();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
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
            GetMillName();
        }
        else
        {
            return;

        }

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
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "' OR CDO.CMRDistrict='" + DistCode + "') and PM.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' and (PM.DeliverdToFCI='N' OR PM.DeliverdToFCI IS NULL) order by MillName Asc";
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.Enabled = false;
        ddlIC.SelectedIndex = 0;

        lbllot.Text = lbllot2.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = hdfAllQty.Value = "";
        btnRecptSubmit.Enabled = false;
        string DistCode = Session["dist_id"].ToString();
        ddlAgtmtNumber.Items.Clear();
        ddlLotNO.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    //Only For Agrmt Dist & Miller Dist.
                    //select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where (Mill_Addr_District='" + DistCode + "' Or District='" + DistCode + "') and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    //Agrmt Dist & Miller Dist & CMR Map. Dist
                    select = "Select distinct PM.Agreement_ID From PaddyMilling_Agreement_2017 AS PM Left Join CMRDO_OtherDistMap As CDO ON(CDO.CropYear=PM.CropYear and CDO.District=PM.District and  CDO.Agreement_ID=PM.Agreement_ID) where (PM.Mill_Addr_District='" + DistCode + "' Or PM.District='" + DistCode + "' OR CDO.CMRDistrict='" + DistCode + "') and PM.Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and PM.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' and (PM.DeliverdToFCI='N' OR PM.DeliverdToFCI IS NULL)  order by PM.Agreement_ID";

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
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जिस मिल का चुनाव किया है, वह मिल आपके जिले की नहीं है इसलिए आप इस मिल के लिए CMR DEPOSIT ORDER नहीं बना सकते|'); </script> ");
                        return;
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
        ddlIC.Enabled = false;
        ddlIC.SelectedIndex = 0;

        lbllot.Text = lbllot2.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = "";
        btnRecptSubmit.Enabled = false;
        ddlLotNO.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetLotNO();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetLotNO()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //((IsAccepted='Y') OR (IsAccepted='N' and IsRejected='N'))
                //string select = "Select distinct DhanLot, 'Lot' + cast(DhanLot as varchar) As LotNO From PaddyMilling_DO_2017 where District=(Select distinct District From PaddyMilling_DO_2017 where CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From CMR_DepositOrder Where CropYear='"+txtYear.Text+"' and Mill_ID='"+ddlMillName.SelectedValue.ToString()+"' and Agreement_ID='"+ddlAgtmtNumber.SelectedItem.ToString()+"' and IsAccepted='Y') ";

                string select = "Select distinct DhanLot, 'Lot' + cast(DhanLot as varchar) As LotNO From PaddyMilling_DO_2017 where District=(Select distinct District From PaddyMilling_DO_2017 where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From CMR_DepositOrder Where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and ((IsStackAccepted='Yes' and IsStackRejected='NO' and  (IsAccepted='Y') ) OR ( (IsStackAccepted='' OR IsStackAccepted is null ) and (IsStackRejected='' OR IsStackRejected is null) and IsAccepted='Y')  OR ( (IsStackAccepted='' OR IsStackAccepted is null ) and (IsStackRejected='' OR IsStackRejected is null) and IsAccepted='N' and IsRejected='N')) ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlLotNO.DataSource = ds.Tables[0];
                    ddlLotNO.DataTextField = "LotNO";
                    ddlLotNO.DataValueField = "DhanLot";
                    ddlLotNO.DataBind();
                    ddlLotNO.Items.Insert(0, "--Select--");


                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर के लिए DO के अनुसार कोई भी लॉट नंबर उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlLotNO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.Enabled = false;
        ddlIC.SelectedIndex = 0;

        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();
        btnRecptSubmit.Enabled = false;
        lbllot.Text = lbllot2.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = hdfAllQty.Value = "";

        if (ddlLotNO.SelectedIndex > 0)
        {
            GetMillerData();

            string districtid = Session["dist_id"].ToString();

            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string select = "select IsGodown, IsSociety, IsStackRejected from PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Check_DO='" + txtRONum.Text + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "'";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string IsGodown, IsSociety, IsStackRejected;
                        IsGodown = ds.Tables[0].Rows[0]["IsGodown"].ToString();
                        IsSociety = ds.Tables[0].Rows[0]["IsSociety"].ToString();
                        IsStackRejected = ds.Tables[0].Rows[0]["IsStackRejected"].ToString();
                        if (IsStackRejected == "YES")
                        {
                            GetStackChallanData();
                        }
                        else
                        {
                            if (IsGodown == "Y")
                            {
                                GetLotData();
                            }
                            else if (IsSociety == "Y")
                            {

                                GetSocietyLotData();
                            }
                        }

                        
                    }
                    else
                    {

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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया लॉट चुनें|'); </script> ");
        }
    }

    public void GetStackChallanData()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select isnull(SUM(isnull(Issued_Qty,0)),0) as Issued_Qty, (select max(Paddy_AgrmtDist) as Paddy_AgrmtDist from CMR_DepositOrder where DO_No='" + txtRONum.Text + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and AgreementID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and LotNumber='" + ddlLotNO.SelectedValue.ToString() + "')  as Dist  from StackRejection_DeliveryChallan where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "'and PaddyDO='" + txtRONum.Text + "' and LotNumber='" + ddlLotNO.SelectedValue.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and AgreementID='" + ddlAgtmtNumber.SelectedValue.ToString() + "'  group by AgreementID, LotNumber";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbltype.Visible = true;
                    lbltype2.Visible = true;
                    lbltype.Text = "चावल की";
                    lbltype2.Text = "चावल की";
                    decimal Recd_Qty, difference;
                    Recd_Qty = Convert.ToDecimal(ds.Tables[0].Rows[0]["Issued_Qty"].ToString());
                    difference = 270 - Recd_Qty;

                    if (difference == 0)
                    {
                        lbllot.Text = lbllot2.Text = ddlLotNO.SelectedValue.ToString();
                        lblMillingType.Visible = false;
                        lblDONO.Text = txtRONum.Text;
                        lblDOQty.Text = Convert.ToString(Recd_Qty);
                        lblDORem.Text = Convert.ToString(difference);
                        hdfAgrmtDist.Value = ds.Tables[0].Rows[0]["Dist"].ToString();

                        ddlIC.Enabled = true;
                        ddlIC.SelectedIndex = 0;
                    }
                    else
                    {
                        lbllot.Text = lbllot2.Text = ddlLotNO.SelectedValue.ToString();
                        lblMillingType.Visible = false;
                        lblDONO.Text = txtRONum.Text;
                        lblDOQty.Text = Convert.ToString(Recd_Qty);
                        lblDORem.Text = Convert.ToString(difference);
                        hdfAgrmtDist.Value = ds.Tables[0].Rows[0]["Dist"].ToString();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर ने इस लॉट नंबर के विरुद्ध पूरा चावल नहीं उठाया है, इसलिए इस लॉट नंबर के विरुद्ध CMR जमा नहीं किया जा सकता|'); </script> ");
                        return;
                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Data is not available'); </script> ");
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
    public void GetLotData()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                //string select = "Select SUM(Isnull(Return_CommonRice,0)) As LotQty,(403-SUM(Isnull(Return_CommonRice,0))) As RemLotQty,Max(Check_DO) As Check_DO,MAX(District) As AgrmtDist From PaddyMilling_DO_2017 where District=(Select distinct District From PaddyMilling_DO_2017 where CropYear='" + txtYear.Text + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From CMR_DepositOrder Where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and ((IsAccepted='Y') OR (IsAccepted='N' and IsRejected='N'))) ";

                string select = "Select SUM(Isnull(Return_CommonRice,0)) As LotQty,MAX(Milling_Type) As Milling_Type,Max(Check_DO) As Check_DO,MAX(District) As AgrmtDist,(Case When MAX(Milling_Type)=N'अरवा' Then (403-SUM(Isnull(Return_CommonRice,0))) Else (397-SUM(Isnull(Return_CommonRice,0))) End) As RemLotQty From PaddyMilling_DO_2017 where District=(Select distinct District From PaddyMilling_DO_2017 where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From CMR_DepositOrder Where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and ((IsAccepted='Y') OR (IsAccepted='N' and IsRejected='N'))) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbltype.Visible = true;
                    lbltype2.Visible = true;
                    lbltype.Text = "धान की";
                    lbltype2.Text = "धान की";
                    lbllot.Text = lbllot2.Text = ddlLotNO.SelectedValue.ToString();
                    lblDOQty.Text = ds.Tables[0].Rows[0]["LotQty"].ToString();
                    lblMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    lblDORem.Text = ds.Tables[0].Rows[0]["RemLotQty"].ToString();
                    lblDONO.Text = ds.Tables[0].Rows[0]["Check_DO"].ToString();
                    float Remlotqty = float.Parse(ds.Tables[0].Rows[0]["RemLotQty"].ToString());
                    hdfAgrmtDist.Value = ds.Tables[0].Rows[0]["AgrmtDist"].ToString();

                    if (Remlotqty <= 1)
                    {
                        ddlIC.Enabled = true;
                        ddlIC.SelectedIndex = 0;
                    }
                    else
                    {
                        string select1 = "";
                        select1 = "Select * From Paddy_Adjust_DO where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and DO_No='" + lblDONO.Text + "' and Lot_No='" + lbllot.Text + "'";
                        da1 = new SqlDataAdapter(select1, con);
                        ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            ddlIC.Enabled = true;
                            ddlIC.SelectedIndex = 0;
                        }
                        else
                        {
                            btnRecptSubmit.Enabled = false;
                            hdfAllQty.Value = "1";
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर ने इस लॉट नंबर के विरुद्ध पूरा धान नहीँ उठाया है, इसलिए इस लॉट नंबर के विरुद्ध CMR जमा नहीं किया जा सकता|'); </script> ");
                            return;
                        }
                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर के लिए DO के अनुसार कोई भी लॉट नंबर उपलब्ध नहीं है|'); </script> ");
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

    public void GetSocietyLotData()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select isnull(SUM(isnull(Recd_Qty,0)),0) as Recd_Qty, distt_id  from SCSC_Procurement_Kharif2017 where Miller_Id='" + ddlMillName.SelectedValue.ToString() + "'and DO_Number='" + txtRONum.Text + "' and Lot_Number='" + ddlLotNO.SelectedValue.ToString() + "' and Crop_Year='" + ddlCropyear.SelectedValue.ToString() + "' group by distt_id";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbltype.Visible = true;
                    lbltype2.Visible = true;
                    lbltype.Text = "धान की";
                    lbltype2.Text = "धान की";
                    decimal Recd_Qty, difference;
                    Recd_Qty = Convert.ToDecimal(ds.Tables[0].Rows[0]["Recd_Qty"].ToString());
                    difference = 403 - Recd_Qty;

                    if (difference == 0)
                    {
                        lbllot.Text = lbllot2.Text = ddlLotNO.SelectedValue.ToString();
                        lblMillingType.Visible = false;
                        lblDONO.Text = txtRONum.Text;
                        lblDOQty.Text = Convert.ToString(Recd_Qty);
                        lblDORem.Text = Convert.ToString(difference);
                        hdfAgrmtDist.Value = ds.Tables[0].Rows[0]["distt_id"].ToString();

                        ddlIC.Enabled = true;
                        ddlIC.SelectedIndex = 0;
                    }
                    else
                    {
                        lbllot.Text = lbllot2.Text = ddlLotNO.SelectedValue.ToString();
                        lblMillingType.Visible = false;
                        lblDONO.Text = txtRONum.Text;
                        lblDOQty.Text = Convert.ToString(Recd_Qty);
                        lblDORem.Text = Convert.ToString(difference);
                        hdfAgrmtDist.Value = ds.Tables[0].Rows[0]["distt_id"].ToString();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर ने इस लॉट नंबर के विरुद्ध पूरा धान नहीँ प्राप्त नहीं किया है, इसलिए इस लॉट नंबर के विरुद्ध CMR जमा नहीं किया जा सकता|'); </script> ");
                        return;
                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Data is not available'); </script> ");
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


    public void GetMillerData()
    {

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                //and District='" + DistCode + "'
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select distinct Check_DO,(select distinct  D.district_name From PaddyMilling_Agreement_2017 as PA inner join pds.districtsmp as D on D.district_code=PA.Mill_Addr_District where PA.Mill_Name= Mill_Code and PA.CropYear=PDO.CropYear) miller_district,   (select distinct  Mill_Addr_District From PaddyMilling_Agreement_2017 as PA where PA.Mill_Name= Mill_Code and PA.CropYear=PDO.CropYear) MillerDistCode from PaddyMilling_DO_2017 As PDO where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "'  and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtRONum.Text = ds.Tables[0].Rows[0]["Check_DO"].ToString();
                    txtMillDist.Text = ds.Tables[0].Rows[0]["miller_district"].ToString();
                    hdfMillerDist.Value = ds.Tables[0].Rows[0]["MillerDistCode"].ToString();

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Data is not available'); </script> ");
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
        string districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + Ddldist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIC.DataSource = ds.Tables[0];
                    ddlIC.DataTextField = "DepotName";
                    ddlIC.DataValueField = "DepotID";
                    ddlIC.DataBind();
                    ddlIC.Items.Insert(0, "--Select--");
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

    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();

        if (ddlIC.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्रदाय केंद्र चुनें|'); </script> ");
        }
    }

    public void GetBranch()
    {
        string districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddlIC.SelectedValue.ToString());
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
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
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
        ddlGodam.Items.Clear();

        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें|'); </script> ");
        }
    }

    public void GetGodown()
    {
        //if (Ddldist.SelectedValue.ToString() == hdfAgrmtDist.Value)
        //{

        //    using (con = new SqlConnection(strcon))
        //    {
        //        try
        //        {
        //            con.Open();
        //            string select = "select distinct Godown_Name, DMG.Godown_id ,distance from Distance_Master_Godown as DMG inner join Godown_Distance_Master as GDM on GDM.district=DMG.DistrictId inner join tbl_MetaData_GODOWN as G on G.DistrictId=DMG.DistrictId and G.Godown_ID=DMG.Godown_id where cast(DMG.distance as float)< cast (GDM.Max_distance as float) and PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "'  and DMG.IssueCenter='" + ddlIC.SelectedValue.ToString() + "' and Distance_For='11' order by Godown_Name";
        //            da = new SqlDataAdapter(select, strcon);

        //            ds = new DataSet();
        //            da.Fill(ds);
        //            // string qry = "select distance from Godown_Distance_Master where district='" + Ddldist.SelectedValue.ToString() + "'";
        //            // da = new SqlDataAdapter(select, con_MPStorage);

        //            // ds = new DataSet();
        //            //  da.Fill(ds);
        //            // if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            //{
        //            //  hdfReturnAgrmtCMR_Percent.Value = ds.Tables[0].Rows[0]["ReturnAgrmtCMR_Percent"].ToString();
        //            // }
        //            if (ds != null)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    ddlGodam.DataSource = ds.Tables[0];
        //                    ddlGodam.DataTextField = "Godown_Name";
        //                    ddlGodam.DataValueField = "Godown_id";
        //                    ddlGodam.DataBind();
        //                    ddlGodam.Items.Insert(0, "--Select--");
        //                }
        //                else
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown  is not available'); </script> ");
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
        //else
        //{
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
                            ddlGodam.DataSource = ds.Tables[0];
                            ddlGodam.DataTextField = "Godown_Name";
                            ddlGodam.DataValueField = "Godown_ID";
                            ddlGodam.DataBind();
                            ddlGodam.Items.Insert(0, "--Select--");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
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
        //}
    }


    protected void ddlGodam_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        hdfDistance.Value = "0";

        if (ddlGodam.SelectedIndex > 0)
        {
            GetDistance();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें|'); </script> ");
        }
    }

    public void GetDistance()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select distance From Distance_Master_Godown where DistrictId='" + Ddldist.SelectedValue.ToString() + "' and PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "' and Godown_id='" + ddlGodam.SelectedValue.ToString() + "' and Distance_For='11'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnRecptSubmit.Enabled = true;
                }
                else
                {
                    hdfDistance.Value = "1";
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल से गोदाम का Distance भरें| Distance भरने के बाद ही आप इस गोदाम के लिए CMR Deposit Order जारी कर सकते हो|'); </script> ");
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

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        if (ddlLotNO.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर को प्रदायित धान का लॉट नंबर चुने|'); </script> ");
            return;
        }
        else if (ddlGodam.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुने|'); </script> ");
            return;
        }
        else if (ddlIsAgainstMovementOrder.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('क्या ये Deposit Order Movement order के विरुद्ध है? कृपया हाँ या ना में जवाब दें'); </script> ");
            return;
        }
        else if (ddlIsAgainstMovementOrder.SelectedIndex == 1 && ddlMovementOrder.SelectedIndex <= 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Movement order चुनें'); </script> ");
            return;

        }

        else if (txtFromDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR जमा करने का दिनांक चुने|'); </script> ");
            return;
        }
        else if (hdfDistance.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल से गोदाम का Distance भरें| Distance भरने के बाद ही आप इस गोदाम के लिए CMR Deposit Order जारी कर सकते हो|'); </script> ");
            return;
        }
        else if (hdfAllQty.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर ने इस लॉट नंबर के विरुद्ध पूरा धान नहीँ उठाया है, इसलिए इस लॉट नंबर के विरुद्ध CMR जमा नहीं किया जा सकता|'); </script> ");
            return;
        }
        else
        {
            string districtid = Session["dist_id"].ToString();

            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = objClientIP.GETIP();


                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string ConvertFromDate = ServerDate.getDate_MDY(txtFromDate.Text);
                        string DistCode = Session["dist_id"].ToString();
                        string SubDCDate = "", DCDate = "", instr = "", CMRDO_ID = "";

                        string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);


                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            SubDCDate = DCDate.Substring(2);
                        }

                        if (ddlIsAgainstMovementOrder.SelectedIndex == 1)
                        {
                            string select = "";
                            select = "select isnull(SMO,0) as SMO from StateMovementOrder where Commodity='3' and FrmDist='" + DistCode + "' and ToDist='" + Ddldist.SelectedValue.ToString() + "' and MoveOrdernum='" + ddlMovementOrder.SelectedValue.ToString() + "' order by MoveOrdernum  ";
                            daSMO = new SqlDataAdapter(select, con);
                            dsSMO = new DataSet();
                            daSMO.Fill(dsSMO);
                            SMO = dsSMO.Tables[0].Rows[0]["SMO"].ToString();
                        }
                        if (ddlIsAgainstMovementOrder.SelectedValue.ToString() == "No" || ddlIsAgainstMovementOrder.SelectedIndex == 0)
                        {
                            ddlMovementOrder.SelectedValue = null;
                            SMO = null;
                        }
                        if (SubDCDate != "")
                        {
                            CMRDO_ID = "CDO" + SubDCDate;
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                        "Update PaddyMilling_DO_2017 Set DispatchDhan_IC='Y', IsStackAccepted='NO', IsStackRejected='NO' where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and District='" + hdfAgrmtDist.Value + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and Check_DO='" + lblDONO.Text + "'; ";

                            instr += "Insert Into CMR_DepositOrder(CMR_DO,CMR_RecdDist,Paddy_AgrmtDist,CropYear,Mill_ID,Agreement_ID,Lot_No,DO_No,IssueCenter,Branch,Godown_id,CMR_DODate,CreatedDate,IP_Address,IsAccepted,IsRejected, Ori_CMR_District, Ori_IC, Ori_Branch, Ori_Godown, Updated, IsAgainst_MovementOrder, MoveOrdernum, SMO, Quantity_qtls) values('" + CMRDO_ID + "','" + Ddldist.SelectedValue.ToString() + "','" + hdfAgrmtDist.Value + "','" + ddlCropyear.SelectedValue.ToString() + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + ddlLotNO.SelectedValue.ToString() + "','" + lblDONO.Text + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "','" + ConvertFromDate + "',GETDATE(),'" + GetIp + "','N','N', '" + Ddldist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodam.SelectedValue.ToString() + "', 'N','" + ddlIsAgainstMovementOrder.SelectedValue.ToString() + "','" + ddlMovementOrder.SelectedValue.ToString() + "','" + SMO + "','270'); ";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnPrint.Enabled = true;
                            btnRecptSubmit.Enabled = false;

                            Label2.Visible = true;
                            Label2.Text = "Your CMR Deposit Order Number Is : " + CMRDO_ID;

                            Session["CMRDO_ID"] = CMRDO_ID.ToString();

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your CMR Deposit Order Number Is " + CMRDO_ID.ToString() + "'); </script> ");
                            txtDistManager.Text = "";
                            ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlLotNO.Enabled = ddlIC.Enabled = ddlBranch.Enabled = ddlGodam.Enabled = false;

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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PrintCMR_DepositOrder.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodam.Items.Clear();



        if (Ddldist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
            trIsAgMovOrder.Visible = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
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
                        Ddldist.SelectedValue = Session["dist_id"].ToString();
                        GetMPIssueCentre();
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


    protected void ddlIsAgainstMovementOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsAgainstMovementOrder.SelectedValue == "Yes")
        {
            trMovementOrder.Visible = true;
            GetMovementOrder();

        }
        else if (ddlIsAgainstMovementOrder.SelectedValue == "No")
        {
            trMovementOrder.Visible = false;
        }

    }

    public void GetMovementOrder()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();



                string select = "";
                select = "select MoveOrdernum, Quantity from StateMovementOrder where Commodity='3' and FrmDist='" + DistCode + "' and ToDist='" + Ddldist.SelectedValue.ToString() + "' order by MoveOrdernum  ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                List<string> MovementOrder = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    string MoveOrdernum = ds.Tables[0].Rows[i]["MoveOrdernum"].ToString();
                    decimal Quantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"].ToString());

                    string query = "";
                    query = "select isnull(SUM(Issued_Qty),0) as Issued_Qty from DeliveryChallan_MO WHERE MoveOrdernum='" + MoveOrdernum + "' AND ToDist='" + Ddldist.SelectedValue.ToString() + "'";
                    daI = new SqlDataAdapter(query, con);
                    dsI = new DataSet();
                    daI.Fill(dsI);
                    decimal Issued_Qty = Convert.ToDecimal(dsI.Tables[0].Rows[0]["Issued_Qty"].ToString());

                    string query1 = "";
                    query1 = "select isnull(sum(Quantity_qtls),0) as Accept_CommonRice from CMR_DepositOrder where MoveOrdernum='" + MoveOrdernum + "' and CMR_RecdDist='" + Ddldist.SelectedValue.ToString() + "'";
                    daCMR = new SqlDataAdapter(query1, con);
                    dsCMR = new DataSet();
                    daCMR.Fill(dsCMR);
                    decimal Accept_CommonRice = Convert.ToDecimal(dsCMR.Tables[0].Rows[0]["Accept_CommonRice"].ToString());
                    decimal TotalIssued = Issued_Qty + Accept_CommonRice;

                    if (Quantity > TotalIssued)
                    {

                        MovementOrder.Add(MoveOrdernum);
                        //DataTable dt = new DataTable();
                        //dt.Columns.Add(MoveOrdernum);

                        //DataSet dsbind = new DataSet();
                        //dsbind.Tables.Add(dt);

                    }

                }
                if (MovementOrder.Count > 0)
                {
                    ddlMovementOrder.DataSource = MovementOrder;

                    ddlMovementOrder.DataBind();
                    ddlMovementOrder.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कोई भी Movement Order उपलभ नहीं है|'); </script> ");
                    trMovementOrder.Visible = false;
                    ddlMovementOrder.ClearSelection();
                    ddlIsAgainstMovementOrder.ClearSelection();
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