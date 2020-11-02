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


public partial class State_CMS_BillGeneration_Print : System.Web.UI.Page
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

                hdfBillNumber.Value = Session["BillNumber"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetGeneratedData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
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
                select = "select Nirakshit_Percentage, CommToSA_Percentage, CommToSociety_Percentage, C.Commodity_Name, BillNumber, BG.CommodityID,  CONVERT(DECIMAL(18,5), Qty) as Qty, NumOfBags,  convert(varchar(18), Date, 103 ) as Date       , CONVERT(DECIMAL(18,2), NackedRate) as NackedRate, CONVERT(DECIMAL(18,2), NackedAmt) as NackedAmt  , CONVERT(DECIMAL(18,2), CentralBonus) as CentralBonus       , CONVERT(DECIMAL(18,2), CentralAmt) as CentralAmt       ,CONVERT(DECIMAL(18,2), LabourRate) as LabourRate, CONVERT(DECIMAL(18,2), LabourAmt) as LabourAmt, CONVERT(DECIMAL(18,2), TransporttionRate) as TransporttionRate, CONVERT(DECIMAL(18,2), TransporttionAmt) as TransporttionAmt,  CONVERT(DECIMAL(18,2), CommToSocietyRate) as CommToSocietyRate, CONVERT(DECIMAL(18,2), CommToSocietyAmt) as CommToSocietyAmt  ,CONVERT(DECIMAL(18,2), CommToStateAgencyRate) as CommToStateAgencyRate      , CONVERT(DECIMAL(18,2), CommToStateAgencyAmt) as CommToStateAgencyAmt     ,CONVERT(DECIMAL(18,2), DriShortRate) as DriShortRate      ,CONVERT(DECIMAL(18,2), DriShortAmt) as DriShortAmt      , CONVERT(DECIMAL(18,2), CostOfGunnyRate) as CostOfGunnyRate   ,CONVERT(DECIMAL(18,2), CostOfGunnyAmt) as CostOfGunnyAmt ,CONVERT(DECIMAL(18,2), CusAndMattRate) as CusAndMattRate      ,CONVERT(DECIMAL(18,2), CusAndMattAmt) as CusAndMattAmt   ,CONVERT(DECIMAL(18,2), InterestRate) as InterestRate ,CONVERT(DECIMAL(18,2), InterestAmt) as  InterestAmt ,CONVERT(DECIMAL(18,2), GstRate) as GstRate, CONVERT(DECIMAL(18,2), GstAmt) as GstAmt    , TotalAmount,   CONVERT(DECIMAL(18,2), Interest) as Interest,  CONVERT(DECIMAL(18,2), CGST) as CGST,  CONVERT(DECIMAL(18,2), SGST) as SGST ,   CONVERT(DECIMAL(18,2), NirashritShulka_Rate) as NirashritShulka_Rate,   CONVERT(DECIMAL(18,2), NirashritShulka_Amt) as NirashritShulka_Amt  from CMS_BillGenerate as BG inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id= BG.CommodityID where BillNumber='" + hdfBillNumber.Value + "'";
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

                        GetCommodity();

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

    public void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "select  C.Commodity_Name, GB.Commodity, TotalBags, CONVERT(DECIMAL(18,5), TotalQty) as  TotalQty,  CONVERT(DECIMAL(18,2), Rate_Transportation) as Rate_Transportation, CONVERT(DECIMAL(18,2), Rate_CostOfBags) as Rate_CostOfBags,  CONVERT(DECIMAL(18,2), CostOfBags) as CostOfBags, TotalAcceptance  from tbl_GeneratedBill_CSM2018 as GB inner join tbl_MetaData_STORAGE_COMMODITY as C on C.Commodity_Id=GB.Commodity where BillNumber='" + hdfBillNumber.Value + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        

                        Label30.Text = ds.Tables[0].Rows[0]["TotalAcceptance"].ToString();

           
                      

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
}