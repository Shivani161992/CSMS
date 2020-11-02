using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

public partial class State_CMS_BillGeneration : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    public string gatepass, InspectionID = "";
    public int getnum;
    SqlDataReader dr;
    protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    decimal QtyTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;

                GetBillGenerate();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        lblDate.Text = Request.Form[lblDate.UniqueID];
    }

    public void GetBillGenerate()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select BillNumber  from tbl_GeneratedBill_CSM2018 order by BillNumber";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBillNumber.DataSource = ds.Tables[0];
                        ddlBillNumber.DataTextField = "BillNumber";
                        ddlBillNumber.DataValueField = "BillNumber";
                        ddlBillNumber.DataBind();
                        ddlBillNumber.Items.Insert(0, "--Select--");


                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Bill Number Not Available|'); </script> ");
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
    protected void ddlBillNumber_SelectedIndexChanged(object sender, EventArgs e)
    {


        lblCommodity.Text = Label10.Text = Label11.Text = Label9.Text = Label1.Text = "";
        Label12.Text = Label13.Text = Label14.Text = "";
        Label43.Text = Label44.Text = Label45.Text = "";
        Label3.Text = Label5.Text = "";
        Label4.Text = Label6.Text = "";

        Label23.Text = Label24.Text = Label25.Text = "";
        Label26.Text = Label27.Text = Label28.Text = "";

        Label46.Text = Label47.Text =
       Label42.Text = Label41.Text = "";
        if (ddlBillNumber.SelectedIndex > 0)
        {
            Session["BillNumber"] = ddlBillNumber.SelectedValue.ToString();


            GetCheckData();

            if (hdfCheckFlag.Value == "Y")//submitted
            {
                GetPostData();

                if (hdfPost.Value == "Y")// Posted
                {
                    btAdd.Visible = false;
                    btAdd.Enabled = false;
                    btnPosted.Visible = false;
                    btnPosted.Enabled = false;
                    btnPrint.Enabled = true;
                    btnPrint.Visible = true;
                    GetGeneratedData();
                }
                else
                {
                    btAdd.Visible = false;
                    btAdd.Enabled = false;
                    btnPosted.Visible = true;
                    btnPosted.Enabled = true;
                    btnPrint.Enabled = true;
                    btnPrint.Visible = true;




                    GetGeneratedData();
                }

            }
            else
            {
                GetCommodity();
                btAdd.Visible = true;
                btAdd.Enabled = true;
                btnPosted.Visible = false;
                btnPosted.Enabled = false;
                btnPrint.Enabled = false;
                btnPrint.Visible = false;

                DateTime today = DateTime.Today;
                lblDate.Text = Convert.ToString(today.ToString("dd/MM/yyyy"));







                if (hdfCommodity.Value == "33")
                {
                    GetGST();
                    trMustard.Visible = true;

                }


            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bill Number|'); </script> ");
            return;
        }

    }

    public void GetCheckData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select  BillGenerated from tbl_GeneratedBill_CSM2018 as GB where BillNumber='" + ddlBillNumber.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        hdfCheckFlag.Value = ds.Tables[0].Rows[0]["BillGenerated"].ToString();

                    }
                    else
                    {
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

    public void GetPostData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select  IsPosted from CMS_BillGenerate as GB where BillNumber='" + ddlBillNumber.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        hdfPost.Value = ds.Tables[0].Rows[0]["IsPosted"].ToString();

                    }
                    else
                    {
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

    public void GetGeneratedData()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select Nirakshit_Percentage, CommToSA_Percentage, CommToSociety_Percentage, C.Commodity_Name, BillNumber, BG.CommodityID,  CONVERT(DECIMAL(18,5), Qty) as Qty, NumOfBags,  convert(varchar(18), Date, 103 ) as Date       , CONVERT(DECIMAL(18,2), NackedRate) as NackedRate, CONVERT(DECIMAL(18,2), NackedAmt) as NackedAmt  , CONVERT(DECIMAL(18,2), CentralBonus) as CentralBonus       , CONVERT(DECIMAL(18,2), CentralAmt) as CentralAmt       ,CONVERT(DECIMAL(18,2), LabourRate) as LabourRate, CONVERT(DECIMAL(18,2), LabourAmt) as LabourAmt, CONVERT(DECIMAL(18,2), TransporttionRate) as TransporttionRate, CONVERT(DECIMAL(18,2), TransporttionAmt) as TransporttionAmt,  CONVERT(DECIMAL(18,2), CommToSocietyRate) as CommToSocietyRate, CONVERT(DECIMAL(18,2), CommToSocietyAmt) as CommToSocietyAmt  ,CONVERT(DECIMAL(18,2), CommToStateAgencyRate) as CommToStateAgencyRate      , CONVERT(DECIMAL(18,2), CommToStateAgencyAmt) as CommToStateAgencyAmt     ,CONVERT(DECIMAL(18,2), DriShortRate) as DriShortRate      ,CONVERT(DECIMAL(18,2), DriShortAmt) as DriShortAmt      , CONVERT(DECIMAL(18,2), CostOfGunnyRate) as CostOfGunnyRate   ,CONVERT(DECIMAL(18,2), CostOfGunnyAmt) as CostOfGunnyAmt ,CONVERT(DECIMAL(18,2), CusAndMattRate) as CusAndMattRate      ,CONVERT(DECIMAL(18,2), CusAndMattAmt) as CusAndMattAmt   ,CONVERT(DECIMAL(18,2), InterestRate) as InterestRate ,CONVERT(DECIMAL(18,2), InterestAmt) as  InterestAmt ,CONVERT(DECIMAL(18,2), GstRate) as GstRate, CONVERT(DECIMAL(18,2), GstAmt) as GstAmt    , TotalAmount,   CONVERT(DECIMAL(18,2), Interest) as Interest,  CONVERT(DECIMAL(18,2), CGST) as CGST,  CONVERT(DECIMAL(18,2), SGST) as SGST ,   CONVERT(DECIMAL(18,2), NirashritShulka_Rate) as NirashritShulka_Rate,   CONVERT(DECIMAL(18,2), NirashritShulka_Amt) as NirashritShulka_Amt  from CMS_BillGenerate as BG inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id= BG.CommodityID where BillNumber='" + ddlBillNumber.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {



                        hdfCmd.Value = ds.Tables[0].Rows[0]["CommodityID"].ToString();

                        if (hdfCmd.Value == "33")
                        {
                            lblHSN.Text = "1207";
                        }
                        else if (hdfCmd.Value == "63")
                        {
                            lblHSN.Text = "0713";
                        }
                        else if (hdfCmd.Value == "64")
                        {
                            lblHSN.Text = "0713";
                        }
                        if (hdfCmd.Value == "33")
                        {
                            trMustard.Visible = true;

                            Label8.Text = ds.Tables[0].Rows[0]["CGST"].ToString();
                            Label15.Text = ds.Tables[0].Rows[0]["SGST"].ToString();

                            Label16.Text = Convert.ToString((Convert.ToDecimal(Label8.Text) + Convert.ToDecimal(Label15.Text)));
                        }

                        lblBillNo.Text = ds.Tables[0].Rows[0]["BillNumber"].ToString();
                        lblCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                        lblDate.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();

                        Label1.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
                        Label9.Text = ds.Tables[0].Rows[0]["NackedRate"].ToString();
                        Label23.Text = ds.Tables[0].Rows[0]["NackedAmt"].ToString();

                        Label10.Text = ds.Tables[0].Rows[0]["CentralBonus"].ToString();
                        Label24.Text = ds.Tables[0].Rows[0]["CentralAmt"].ToString();

                        Label11.Text = ds.Tables[0].Rows[0]["LabourRate"].ToString();
                        Label25.Text = ds.Tables[0].Rows[0]["LabourAmt"].ToString();

                        //--
                        Label12.Text = ds.Tables[0].Rows[0]["TransporttionRate"].ToString();
                        Label26.Text = ds.Tables[0].Rows[0]["TransporttionAmt"].ToString();

                        Label13.Text = ds.Tables[0].Rows[0]["CommToSocietyRate"].ToString();
                        Label27.Text = ds.Tables[0].Rows[0]["CommToSocietyAmt"].ToString();

                        //--
                        Label14.Text = ds.Tables[0].Rows[0]["CommToStateAgencyRate"].ToString();
                        Label28.Text = ds.Tables[0].Rows[0]["CommToStateAgencyAmt"].ToString();

                        Label3.Text = ds.Tables[0].Rows[0]["DriShortRate"].ToString();
                        Label4.Text = ds.Tables[0].Rows[0]["DriShortAmt"].ToString();

                        //--
                        Label43.Text = ds.Tables[0].Rows[0]["CostOfGunnyRate"].ToString();
                        Label46.Text = ds.Tables[0].Rows[0]["CostOfGunnyAmt"].ToString();

                        Label44.Text = ds.Tables[0].Rows[0]["CusAndMattRate"].ToString();
                        Label47.Text = ds.Tables[0].Rows[0]["CusAndMattAmt"].ToString();

                        Label17.Text = ds.Tables[0].Rows[0]["NirashritShulka_Rate"].ToString();
                        Label18.Text = ds.Tables[0].Rows[0]["NirashritShulka_Amt"].ToString();

                        //--
                        Label45.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
                        Label48.Text = ds.Tables[0].Rows[0]["InterestAmt"].ToString();

                        Label5.Text = ds.Tables[0].Rows[0]["GstRate"].ToString();
                        Label6.Text = ds.Tables[0].Rows[0]["GstAmt"].ToString();
                        lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                        //--
                        Label41.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                        QtyTotal = Convert.ToDecimal(Label41.Text);

                        QtyTotal = Math.Round(QtyTotal, 0, MidpointRounding.AwayFromZero);
                        Label41.Text = Convert.ToString(QtyTotal);



                        Label42.Text = NumbersToWords(QtyTotal.ToString());
                        Label41.Text = Label41.Text + ".00";
                        Label7.Text = ds.Tables[0].Rows[0]["Interest"].ToString();

                        Label20.Text = ds.Tables[0].Rows[0]["NumOfBags"].ToString();


                        Label21.Text = ds.Tables[0].Rows[0]["Nirakshit_Percentage"].ToString();
                        Label29.Text = ds.Tables[0].Rows[0]["CommToSA_Percentage"].ToString();
                        Label22.Text = ds.Tables[0].Rows[0]["CommToSociety_Percentage"].ToString();



                    }
                    else
                    {
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

    public void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select  C.Commodity_Name, GB.Commodity, TotalBags, CONVERT(DECIMAL(18,5), TotalQty) as  TotalQty,  CONVERT(DECIMAL(18,2), Rate_Transportation) as Rate_Transportation, CONVERT(DECIMAL(18,2), Rate_CostOfBags) as Rate_CostOfBags,  CONVERT(DECIMAL(18,2), CostOfBags) as CostOfBags, TotalAcceptance  from tbl_GeneratedBill_CSM2018 as GB inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id=GB.Commodity where BillNumber='" + ddlBillNumber.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label20.Text = ds.Tables[0].Rows[0]["TotalBags"].ToString();
                        //lblDate.Text = ds.Tables[0].Rows[0]["GetDate"].ToString();
                        lblBillNo.Text = ddlBillNumber.SelectedValue.ToString();
                        lblCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                        hdfCommodity.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();

                        if (hdfCommodity.Value == "33")
                        {
                            lblHSN.Text = "1207";
                        }
                        else if (hdfCommodity.Value == "63")
                        {
                            lblHSN.Text = "0713";
                        }
                        else if (hdfCommodity.Value == "64")
                        {
                            lblHSN.Text = "0713";
                        }

                        Label1.Text = ds.Tables[0].Rows[0]["TotalQty"].ToString();

                        decimal NackedCost = Convert.ToDecimal(Label1.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 5);
                        // NackedCostRound = NackedCostRound + 0.00000;

                        Label1.Text = Convert.ToString(NackedCostRound);


                        Label30.Text = ds.Tables[0].Rows[0]["TotalAcceptance"].ToString();

                        //-------------------------------------------
                        //Label12.Text = ds.Tables[0].Rows[0]["Rate_Transportation"].ToString();
                        //decimal TransportaionCharges = Convert.ToDecimal(Label12.Text);

                        //decimal TransportaionChargesround = Math.Round(TransportaionCharges, 2);
                        //Label12.Text = Convert.ToString(TransportaionChargesround);
                        //hdftransporCost.Value = Label12.Text;
                        //-------------------------------------------
                        Label46.Text = ds.Tables[0].Rows[0]["CostOfBags"].ToString();
                        Label43.Text = ds.Tables[0].Rows[0]["Rate_CostOfBags"].ToString();
                        decimal Gunny = Convert.ToDecimal(Label43.Text);

                        decimal GunnyRound = Math.Round(Gunny, 2);
                        Label43.Text = Convert.ToString(GunnyRound);
                        hdfCostOfGunny.Value = Label43.Text;
                        GetTransportationRate();

                        if (hdfCommodity.Value == "33")
                        { }
                        else { }


                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Bill Number Not Available|'); </script> ");
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

    public void GetTransportationRate()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), Transport_rate) as  Transport_rate from CMS_Transportation_Charges where cmd='" + hdfCommodity.Value + "' and Transport_rate in ( select top 1  Transport_rate  from CMS_Transportation_Charges where cmd='" + hdfCommodity.Value + "' and GETDATE() >=valid_frm and GETDATE() <=valid_to order by created_date desc ,  valid_to desc  ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label12.Text = ds.Tables[0].Rows[0]["Transport_rate"].ToString();
                        decimal TransportaionCharges = Convert.ToDecimal(Label12.Text);

                        decimal TransportaionChargesround = Math.Round(TransportaionCharges, 2);
                        Label12.Text = Convert.ToString(TransportaionChargesround);
                        hdftransporCost.Value = Label12.Text;

                        GetNackedRates();

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transportation Rate Not Available|'); </script> ");
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

    public void GetNackedRates()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs from CMS_NackedCostMaster where Commodity='" + hdfCommodity.Value + "' and Rate_Rs in ( select top 1  Rate_Rs  from CMS_NackedCostMaster where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc , ToDate desc  ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label9.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();
                        decimal NackedCost = Convert.ToDecimal(Label9.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label9.Text = Convert.ToString(NackedCostRound);

                        hdfnacked.Value = Label9.Text;

                        GetCentralBonus();


                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Nacked Cost Not Available|'); </script> ");
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

    public void GetCentralBonus()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), Rate_Rs) as  Rate_Rs from CMS_CentralCost_Master where Commodity='" + hdfCommodity.Value + "' and Rate_Rs in ( select top 1  Rate_Rs  from CMS_CentralCost_Master where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc , ToDate desc  )  ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label10.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();

                        decimal NackedCost = Convert.ToDecimal(Label10.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label10.Text = Convert.ToString(NackedCostRound);

                        hdfcentral.Value = Label10.Text;

                        GetLabourCost();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Central Bonus Not Available|'); </script> ");
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

    public void GetLabourCost()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select  CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs from CMS_LabourCharges_Master where Commodity='" + hdfCommodity.Value + "' and Rate_Rs in ( select top 1  Rate_Rs  from CMS_LabourCharges_Master where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc  )   ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label11.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();
                        decimal NackedCost = Convert.ToDecimal(Label11.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label11.Text = Convert.ToString(NackedCostRound);
                        hdflabour.Value = Label11.Text;

                        GetNirashritShulka();

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Labour Charges Not Available|'); </script> ");
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

    public void GetNirashritShulka()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                // select = "select   CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs, Percentage  from CMS_NirashritShulka where Commodity='" + hdfCommodity.Value + "' and Rate_Rs in ( select top 1  Rate_Rs  from CMS_NirashritShulka where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc  ) ";

                select = "select  top 1 CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs, Percentage   from CMS_NirashritShulka where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label21.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                        Label17.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();
                        decimal NackedCost = Convert.ToDecimal(Label17.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label17.Text = Convert.ToString(NackedCostRound);
                        hdfNirakshritShulka.Value = Label17.Text;

                        GetCommissionTosociety();

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Commission to society Not Available|'); </script> ");
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

    public void GetCommissionTosociety()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                // select = "select  CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs, Percentage from CMS_CommisssionToSociety where Commodity='" + hdfCommodity.Value + "' and Rate_Rs in ( select top 1  Rate_Rs  from CMS_CommisssionToSociety where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc  ) ";

                select = "select top 1  CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs, Percentage from CMS_CommisssionToSociety where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label22.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                        Label13.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();
                        decimal NackedCost = Convert.ToDecimal(Label13.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label13.Text = Convert.ToString(NackedCostRound);
                        hdfcommtosociety.Value = Label13.Text;

                        GetCommissionToStateAgency();

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Commission to society Not Available|'); </script> ");
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

    public void GetCommissionToStateAgency()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                // select = "select   CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs, Percentage from CMS_CommisssionToStateAgency where Commodity='" + hdfCommodity.Value + "' and Rate_Rs in ( select top 1  Rate_Rs  from CMS_CommisssionToStateAgency where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc  ) ";

                select = "select top 1  CONVERT(DECIMAL(18,2), Rate_Rs) as Rate_Rs, Percentage  from CMS_CommisssionToStateAgency where Commodity='" + hdfCommodity.Value + "' and GETDATE() >=FromDate and GETDATE() <=ToDate order by Createddate desc, ToDate desc ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label29.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                        Label14.Text = ds.Tables[0].Rows[0]["Rate_Rs"].ToString();

                        decimal NackedCost = Convert.ToDecimal(Label14.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label14.Text = Convert.ToString(NackedCostRound);
                        hdfcommtostaagen.Value = Label14.Text;


                        GetDriageShortage();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Commission to state agency Not Available|'); </script> ");
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

    public void GetDriageShortage()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), D_rate) as  D_rate from CMS_Driage_master where cmd='" + hdfCommodity.Value + "' and D_rate in ( select top 1  D_rate  from CMS_Driage_master where cmd='" + hdfCommodity.Value + "' and GETDATE() >=valid_frm and GETDATE() <=valid_to order by created_date desc ,  valid_to desc  ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label3.Text = ds.Tables[0].Rows[0]["D_rate"].ToString();

                        decimal NackedCost = Convert.ToDecimal(Label3.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label3.Text = Convert.ToString(NackedCostRound);
                        hdfDriageCost.Value = Label3.Text;

                        GetCustodyAndMattCharges();

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Driage/Shortage @ 0.15 of MSP Not Available|'); </script> ");
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

    public void GetCustodyAndMattCharges()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), fixed_Rate) as fixed_Rate from CMS_CmdMaintenance_Master where cmd_id='" + hdfCommodity.Value + "' and fixed_Rate in (select top 1  fixed_Rate  from CMS_CmdMaintenance_Master where cmd_id='" + hdfCommodity.Value + "' and GETDATE() >=valid_from and GETDATE() <=valid_to order by created_date desc ,  valid_to desc  ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label44.Text = ds.Tables[0].Rows[0]["fixed_Rate"].ToString();

                        decimal NackedCost = Convert.ToDecimal(Label44.Text);

                        decimal NackedCostRound = Math.Round(NackedCost, 2);
                        Label44.Text = Convert.ToString(NackedCostRound);
                        hdfcustody.Value = Label44.Text;

                        GetInterestTwoMonths();

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Custody and Mantt Charges Not Available|'); </script> ");
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

    public void GetInterestTwoMonths()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), I_rate) as I_rate from CMS_Interest_Rate_Master where commodity='" + hdfCommodity.Value + "' and I_rate in (select top 1  I_rate  from CMS_Interest_Rate_Master where commodity='" + hdfCommodity.Value + "' and GETDATE() >=valid_frm and GETDATE() <=valid_to order by created_date desc ,  valid_to desc  ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        decimal Interest = Convert.ToDecimal(ds.Tables[0].Rows[0]["I_rate"].ToString());
                        Label7.Text = ds.Tables[0].Rows[0]["I_rate"].ToString();

                        decimal Sum = Convert.ToDecimal(hdfnacked.Value) + Convert.ToDecimal(hdflabour.Value) + Convert.ToDecimal(hdfcentral.Value) + Convert.ToDecimal(hdftransporCost.Value) + Convert.ToDecimal(hdfCostOfGunny.Value) + Convert.ToDecimal(hdfcommtosociety.Value) + Convert.ToDecimal(hdfcommtostaagen.Value) + Convert.ToDecimal(hdfDriageCost.Value) + Convert.ToDecimal(hdfcustody.Value) + Convert.ToDecimal(hdfNirakshritShulka.Value);
                        decimal value = ((((Sum * Interest) / 100) / 12) * 2);


                        decimal NackedCost = value;

                        decimal NackedCostRound = Math.Round(NackedCost, 2);



                        Label45.Text = Convert.ToString(NackedCostRound);

                        if (hdfCommodity.Value == "33")
                        {
                            GetGST();

                        }
                        else
                        {

                            GetAmount();
                        }


                        // GetAmount();


                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Interest Rate Not Available'); </script> ");
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

    public void GetGST()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select CONVERT(DECIMAL(18,2), CGST) as  CGST, CONVERT(DECIMAL(18,2), SGST) as SGST from Cms_Gst_Master where cmd='" + hdfCommodity.Value + "' and CGST in (select top 1  CGST  from Cms_Gst_Master where cmd='" + hdfCommodity.Value + "' and GETDATE() >=validfrm and GETDATE() <=validto order by created_date desc,  validto desc  ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Label8.Text = ds.Tables[0].Rows[0]["CGST"].ToString();
                        Label15.Text = ds.Tables[0].Rows[0]["SGST"].ToString();
                        Label16.Text = Convert.ToString(Convert.ToDecimal(Label8.Text) + Convert.ToDecimal(Label15.Text));




                        GetAmount();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CGST and SGST  Not Available'); </script> ");
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



    public void GetAmount()
    {


        //NacAmt
        decimal NacketAmt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label9.Text);
        Label23.Text = Convert.ToString(NacketAmt);
        decimal NackedAMT = Convert.ToDecimal(Label23.Text);
        decimal NackedAMTRound = Math.Round(NackedAMT, 2);
        Label23.Text = Convert.ToString(NackedAMTRound);

        if (Label23.Text == "0")
        {
            Label23.Text = Label23.Text + ".00";
        }

        //CBAMT
        decimal CentralBonusAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label10.Text);
        Label24.Text = Convert.ToString(CentralBonusAMt);
        decimal CBAMT = Convert.ToDecimal(Label24.Text);
        decimal CBAMTRound = Math.Round(CBAMT, 2);
        Label24.Text = Convert.ToString(CBAMTRound);
        if (Label24.Text == "0")
        {
            Label24.Text = Label24.Text + ".00";
        }

        //LBCharges
        decimal LBAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label11.Text);
        Label25.Text = Convert.ToString(LBAMt);
        decimal LBAMT = Convert.ToDecimal(Label25.Text);
        decimal LBAMTRound = Math.Round(LBAMT, 2);
        Label25.Text = Convert.ToString(LBAMTRound);
        if (Label25.Text == "0")
        {
            Label25.Text = Label25.Text + ".00";
        }

        //TransCharges
        decimal TCAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label12.Text);
        Label26.Text = Convert.ToString(TCAMt);
        decimal TCAMT = Convert.ToDecimal(Label26.Text);
        decimal TCAMTRound = Math.Round(TCAMT, 2);
        Label26.Text = Convert.ToString(TCAMTRound);
        if (Label26.Text == "0")
        {
            Label26.Text = Label26.Text + ".00";
        }

        //CommToSociety
        decimal CSAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label13.Text);
        Label27.Text = Convert.ToString(CSAMt);
        decimal CSAMT = Convert.ToDecimal(Label27.Text);
        decimal CSAMTRound = Math.Round(CSAMT, 2);
        Label27.Text = Convert.ToString(CSAMTRound);
        if (Label27.Text == "0")
        {
            Label27.Text = Label27.Text + ".00";
        }

        //CommToStateAgen
        decimal CSAAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label14.Text);
        Label28.Text = Convert.ToString(CSAAMt);
        decimal CSAAMT = Convert.ToDecimal(Label28.Text);
        decimal CSAAMTRound = Math.Round(CSAAMT, 2);
        Label28.Text = Convert.ToString(CSAAMTRound);
        if (Label28.Text == "0")
        {
            Label28.Text = Label28.Text + ".00";
        }

        //DriageShortage
        decimal DSAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label3.Text);
        Label4.Text = Convert.ToString(DSAMt);
        decimal DSAMT = Convert.ToDecimal(Label4.Text);
        decimal DSAMTRound = Math.Round(DSAMT, 2);
        Label4.Text = Convert.ToString(DSAMTRound);
        if (Label4.Text == "0")
        {
            Label4.Text = Label4.Text + ".00";
        }

        //CostOFGunny
        //decimal CGAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label43.Text);
        //Label46.Text = Convert.ToString(CGAMt);
        //decimal CGAMT = Convert.ToDecimal(Label46.Text);
        //decimal CGAMTRound = Math.Round(CGAMT, 2);
        //Label46.Text = Convert.ToString(CGAMTRound);
        if (Label46.Text == "0")
        {
            Label46.Text = Label46.Text + ".00";
        }


        //CusMattCharges
        decimal CMCAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label44.Text);
        Label47.Text = Convert.ToString(CMCAMt);
        decimal CMCAMT = Convert.ToDecimal(Label47.Text);
        decimal CMCAMTRound = Math.Round(CMCAMT, 2);
        Label47.Text = Convert.ToString(CMCAMTRound);
        if (Label47.Text == "0")
        {
            Label47.Text = Label47.Text + ".00";
        }

        //InterestCharges
        decimal IAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label45.Text);
        Label48.Text = Convert.ToString(IAMt);
        decimal IAMT = Convert.ToDecimal(Label48.Text);
        decimal IAMTRound = Math.Round(IAMT, 2);
        Label48.Text = Convert.ToString(IAMTRound);
        if (Label48.Text == "0")
        {
            Label48.Text = Label48.Text + ".00";
        }

        //Nirakshrit Shulka
        decimal NRAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label17.Text);
        Label18.Text = Convert.ToString(NRAMt);
        decimal NRAMT = Convert.ToDecimal(Label18.Text);
        decimal NRAMTRound = Math.Round(NRAMT, 2);
        Label18.Text = Convert.ToString(NRAMTRound);
        if (Label18.Text == "0")
        {
            Label18.Text = Label18.Text + ".00";
        }
        if (hdfCommodity.Value == "33")
        {
            //decimal TotalAmount = (Convert.ToDecimal(Label9.Text) + Convert.ToDecimal(Label10.Text) + Convert.ToDecimal(Label11.Text) + Convert.ToDecimal(Label12.Text) + Convert.ToDecimal(Label13.Text) + Convert.ToDecimal(Label14.Text) + Convert.ToDecimal(Label3.Text) + Convert.ToDecimal(Label43.Text) + Convert.ToDecimal(Label44.Text) + Convert.ToDecimal(Label45.Text) + Convert.ToDecimal(Label17.Text));
            //decimal GSTRate = ((TotalAmount * Convert.ToDecimal(Label16.Text)) / 100);
            //Label5.Text = Convert.ToString(GSTRate);
            //decimal GAMTsum = Convert.ToDecimal(Label5.Text);
            //decimal GSTRoundsum = Math.Round(GAMTsum, 2);
            //Label5.Text = Convert.ToString(GSTRoundsum);
            //if (Label5.Text == "0")
            //{
            //    Label5.Text = Label5.Text + ".00";
            //}

            ////GSTCharges
            //decimal GSTAMt = Convert.ToDecimal(Label1.Text) * Convert.ToDecimal(Label5.Text);
            //Label6.Text = Convert.ToString(GSTAMt);
            //decimal GAMT = Convert.ToDecimal(Label6.Text);
            //decimal GSTRound = Math.Round(GAMT, 2);
            //Label6.Text = Convert.ToString(GSTRound);

            //if (Label6.Text == "0")
            //{
            //    Label6.Text = Label6.Text + ".00";
            //}

            decimal TotalAmount = (Convert.ToDecimal(Label23.Text) + Convert.ToDecimal(Label24.Text) + Convert.ToDecimal(Label25.Text) + Convert.ToDecimal(Label26.Text) + Convert.ToDecimal(Label27.Text) + Convert.ToDecimal(Label28.Text) + Convert.ToDecimal(Label4.Text) + Convert.ToDecimal(Label46.Text) + Convert.ToDecimal(Label47.Text) + Convert.ToDecimal(Label48.Text) + Convert.ToDecimal(Label18.Text));
            decimal GSTRate = ((TotalAmount * Convert.ToDecimal(Label16.Text)) / 100);
            Label6.Text = Convert.ToString(GSTRate);
            decimal GAMTsum = Convert.ToDecimal(Label6.Text);
            decimal GSTRoundsum = Math.Round(GAMTsum, 2);
            Label6.Text = Convert.ToString(GSTRoundsum);
            if (Label6.Text == "0")
            {
                Label6.Text = Label5.Text + ".00";
            }


            ////GSTRate
            Label5.Text = Convert.ToString(Convert.ToDecimal(Label6.Text) / Convert.ToDecimal(Label1.Text));
            decimal GAMTrate = Convert.ToDecimal(Label5.Text);
            decimal GSTRoundrate = Math.Round(GAMTrate, 2);
            Label5.Text = Convert.ToString(GSTRoundrate);

            if (Label5.Text == "0")
            {
                Label5.Text = Label5.Text + ".00";
            }

        }






        Label41.Text = Convert.ToString(Convert.ToDecimal(Label23.Text) + Convert.ToDecimal(Label24.Text) + Convert.ToDecimal(Label25.Text) + Convert.ToDecimal(Label26.Text) + Convert.ToDecimal(Label27.Text) + Convert.ToDecimal(Label28.Text) + Convert.ToDecimal(Label4.Text) + Convert.ToDecimal(Label46.Text) + Convert.ToDecimal(Label47.Text) + Convert.ToDecimal(Label48.Text) + Convert.ToDecimal(Label18.Text));
        if (hdfCommodity.Value == "33")
        {

            Label41.Text = Convert.ToString(Convert.ToDecimal(Label6.Text) + Convert.ToDecimal(Label41.Text));
        }

        QtyTotal = Convert.ToDecimal(Label41.Text);

        QtyTotal = Math.Round(QtyTotal, 0, MidpointRounding.AwayFromZero);
        Label41.Text = Convert.ToString(QtyTotal);



        Label42.Text = NumbersToWords(QtyTotal.ToString());
        Label41.Text = Label41.Text + ".00";


    }

    private static string NumbersToWords(string inputNumber)
    {

        int inputNo = Convert.ToInt32(inputNumber);

        if (inputNo == 0)
            return "Zero";

        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }

        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        //lblnotowords.Text = sb.ToString();
        return sb.ToString().TrimEnd();


    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {



                con.Open();

                string qrey = "select max(BillID) as BillID  from CMS_BillGenerate where  LEN(BillID)<8 and CommodityID='" + hdfCommodity.Value + "' ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["BillID"].ToString();

                if (hdfCommodity.Value == "64")
                {
                    if (gatepass == "")
                    {
                        gatepass = "64" + "01";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);

                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                }

                else if (hdfCommodity.Value == "63")
                {
                    if (gatepass == "")
                    {
                        gatepass = "63" + "01";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);

                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                }

                else if (hdfCommodity.Value == "33")
                {
                    if (gatepass == "")
                    {
                        gatepass = "33" + "01";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);

                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                }



                string strselect = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // ConvertServerDate ServerDate = new ConvertServerDate();
                // string ConvertDateofdispatch = ServerDate.getDate_MDY(lblDate.Text);

                string BookNumber = ddlBillNumber.SelectedValue.ToString() + gatepass;



                strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                    "update tbl_GeneratedBill_CSM2018 set BillGenerated='Y' where BillNumber='" + ddlBillNumber.SelectedValue.ToString() + "'";

                strselect += "insert into CMS_BillGenerate ([BillID]      ,[CommodityID]      ,[BillNumber]      ,[Qty]       ,[NackedRate]      ,[NackedAmt]      ,[CentralBonus]       ,[CentralAmt]       ,[LabourRate]       ,[LabourAmt]      ,[TransporttionRate]       ,[TransporttionAmt]      ,[CommToSocietyRate]       ,[CommToSocietyAmt]      ,[CommToStateAgencyRate]      ,[CommToStateAgencyAmt]      ,[DriShortRate]      ,[DriShortAmt]      ,[CostOfGunnyRate]      ,[CostOfGunnyAmt]      ,[CusAndMattRate]      ,[CusAndMattAmt]       ,[InterestRate] ,[InterestAmt],[GstRate] ,[GstAmt]    ,[TotalAmount],[CreatedDate]   ,[IP]   ,[Dated],[Date] ,[BookNumber], IsPosted, Payment, Interest, CGST, SGST, NumOfBags, NirashritShulka_Rate, NirashritShulka_Amt, Nirakshit_Percentage, CommToSA_Percentage, CommToSociety_Percentage) values ('" + gatepass + "', '" + hdfCommodity.Value + "', '" + ddlBillNumber.SelectedValue.ToString() + "','" + Label1.Text + "', '" + Label9.Text + "', '" + Label23.Text + "', '" + Label10.Text + "', '" + Label24.Text + "' , '" + Label11.Text + "' , '" + Label25.Text + "', '" + Label12.Text + "' , '" + Label26.Text + "', '" + Label13.Text + "', '" + Label27.Text + "', '" + Label14.Text + "', '" + Label28.Text + "', '" + Label3.Text + "', '" + Label4.Text + "', '" + Label43.Text + "', '" + Label46.Text + "', '" + Label44.Text + "', '" + Label47.Text + "', '" + Label45.Text + "', '" + Label48.Text + "', '" + Label5.Text + "', '" + Label6.Text + "', '" + Label41.Text + "', Getdate(), '" + ip + "', Getdate(), Getdate(), '" + BookNumber + "', 'N', 'N', '" + Label7.Text + "', '" + Label8.Text + "','" + Label15.Text + "', '" + Label20.Text + "', '" + Label17.Text + "', '" + Label18.Text + "', '" + Label21.Text + "', '" + Label29.Text + "', '" + Label22.Text + "')";
                strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";



                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                btAdd.Enabled = false;
                ddlBillNumber.Enabled = false;
                trNumber.Visible = true;
                Label2.Visible = true;
                Label2.Text = "Your Bill Number is :- " + BookNumber;
                btnPrint.Enabled = true;
                btnPrint.Visible = true;

            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }


    }
    protected void btnPosted_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {



                con.Open();

                string strselect = "";




                strselect = "update CMS_BillGenerate set IsPosted='Y', PostDate=GetDate() where BillNumber='" + ddlBillNumber.SelectedValue.ToString() + "'";



                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                btnPosted.Enabled = false;
                btAdd.Enabled = false;
                ddlBillNumber.Enabled = false;



            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }


    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "CMS_BillGeneration_Print.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}