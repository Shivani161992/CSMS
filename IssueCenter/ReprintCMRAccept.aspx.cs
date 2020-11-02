using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_ReprintCMRAccept : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da_MPStorage;
    DataSet ds, ds_MPStorage;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public string ICID, DistId, CropYear;
    public string DistCode, State, StateCode, BookNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                BookNo = Session["Book_Number"].ToString();
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                //CropYear = Session["CropYear"].ToString();
                // GetCropYearValues();
                GetDataCMRTesting();
                GetInspector();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYearValues()
    {
        //CropYear = Session["CropYear"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT * FROM PaddyMilling_CropYear where CropYear='" + lblYear.Text + "' order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    LblTotaGA.Text = ds.Tables[0].Rows[0]["TotaGA"].ToString();
                    LblTotaS.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    LblChoteToteGA.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    LblVijatiyeGA.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    LblDamageDaaneGA.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    LblBadrangDaaneGA.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    LblChaakiDaaneGA.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    LblLaalDaaneGA.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    LblOtherGA.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    LblOtherS.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    LblChokarDaaneS.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    LblNamiGA.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
                    LblNamiS.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
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


    public void GetInspector()
    {
        //  CropYear = Session["CropYear"].ToString();
        ICID = Session["issue_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //string select = " select Inspector_Name from Inspector_Master_02017 where  IssueCenter_code='" + ICID + "'";
                string select = " select Inspector_Name from CMR_QualityInspection as QI left join Inspector_Master_02017 as IM on IM.IssueCenter_code=QI.issueCentre_code and IM.district=QI.District and IM.Inspector_ID=QI.Inspector_ID where  IssueCenter_code='" + ICID + "' and QI.Book_Number='" + BookNo + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblInspector.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();

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


    public void GetDataCMRTesting()
    {
        // CropYear = Session["CropYear"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select BT.BagType  as BagsTypeNew, Godown_Code, Accept_CommonRice, Reject_CommonRice, LotNumber,  Truck_No, GETDATE() As ServerDate,MP.district_name,CMR.ToulReceiptNo,CMR.Milling_Type,MP.district_name,CMR.Submited,MR.Mill_Name,CMR.Agreement_ID,MR.Registration_ID,(Select State_Name From State_Master where State_Code=MR.State_Code) As MillerState,(Case when MR.State_Code='23' Then (Select district_name From pds.districtsmp where district_code=MR.District_Code) Else (Select district_name From OtherState_DistrictCode where district_code=MR.District_Code and State_Id=MR.State_Code) End) As MillerDist,CMR.Agreement_ID,CMR.Book_Number,CMR.Date,CMR.DO_Number,CMR.BagType,CMR.Tags,CMR.TagNo,('Lot'+CMR.LotNumber) As LotNumber ,Depot.DepotName,CMR.Godown_Code,CMR.Accept_CommonRice,CMR.Reject_CommonRice,CMR.Bags,CMR.Truck_No,TotaGA ,TotaS,TotaRemark,ChoteToteGA,ChoteToteS,ChoteToteRemark ,VijatiyeGA,VijatiyeS,VijatiyeRemark,DamageDaaneGA,DamageDaaneS,DamageDaaneRemark,BadrangDaaneGA,BadrangDaaneS,BadrangDaaneRemark,ChaakiDaaneGA,ChaakiDaaneS,ChaakiDaaneRemark,LaalDaaneGA,LaalDaaneS,LaalDaaneRemark,OtherGA,OtherS,OtherRemark,ChokarDaaneGA,ChokarDaaneS,ChokarDaaneRemark,NamiGA,NamiS,NamiRemark,Daane,(Case When CMR.Accept_CommonRice!=0 Then CMR.Accept_CommonRice Else CMR.Reject_CommonRice End) As CMRRecd, IsAgainst_MovementOrder, MoveOrdernum, CMR.CropYear as CropYear From CMR_QualityInspection As CMR inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=CMR.BagType Left Join pds.districtsmp As MP ON(MP.district_code=CMR.District) Left join Miller_Registration_2017 MR ON(MR.Registration_ID=CMR.Mill_Name and MR.CropYear=CMR.CropYear) Left Join tbl_MetaData_DEPOT As Depot ON(Depot.DepotID=CMR.issueCentre_code and Depot.DistrictId='23'+CMR.District) where CMR.Book_Number='" + BookNo + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string check = ds.Tables[0].Rows[0]["Submited"].ToString();
                    if (check == "Y")
                    {
                        lblQty.Text = ds.Tables[0].Rows[0]["Accept_CommonRice"].ToString();
                        lblCMRAcptRjct.Text = "CMR Acceptance Note";
                        lblAcptRjctNO.Text = "Acceptance No -";
                        lblAcptRjctDate.Text = "Acceptance Date -";
                        lblmsg1.Text = "1. उपरोक्तानुसार प्राप्त लॉट के चावल की केन्द्रीय निर्धारित मापदंड के अंतर्गत पायी गयी है| अत: स्वीकृत किया जाता हैं|";
                        lblmsg2.Text = "2. जमा कराये गये चावल की केन्द्रीय/ राज्य शासन/ म.प्र. स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड द्वारा कालांतर में आकस्मिक जाँच करायी जाती है तथा जाँच के दोरान निर्धारित मापदंड का चावल नहीं पाये जाने पर केन्द्रीय/ राज्य शासन/ म.प्र. स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड द्वारा अधिरोपित दंड/ आदेश मान्य होगा|";
                    }
                    else
                    {
                        lblQty.Text = ds.Tables[0].Rows[0]["Reject_CommonRice"].ToString();
                        lblCMRAcptRjct.Text = "CMR Rejection Note";
                        lblAcptRjctNO.Text = "Rejection No -";
                        lblAcptRjctDate.Text = "Rejection Date -";
                        lblmsg1.Text = "उपरोक्तानुसार प्रेक्षित लॉट के चावल की केन्द्रीय निर्धारित मापदंड के अंतर्गत नहीं पाये जाने के कारण अस्वीकृत किया जाता है|";
                    }
                    lblYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblMillerName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblAgrmtNo.Text = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();
                    lblMillerState.Text = ds.Tables[0].Rows[0]["MillerState"].ToString();
                    lblMillerDistrict.Text = ds.Tables[0].Rows[0]["MillerDist"].ToString();
                    lblAcptNo.Text = ds.Tables[0].Rows[0]["Book_Number"].ToString();
                    lblCMRDepositNo.Text = ds.Tables[0].Rows[0]["DO_Number"].ToString();
                    lblToulNo.Text = ds.Tables[0].Rows[0]["ToulReceiptNo"].ToString();

                    lblLot.Text = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    // lblQty.Text = ds.Tables[0].Rows[0]["ToulReceiptNo"].ToString();
                    lblTruckNumber.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();

                    string IsAgainMoveOrder = ds.Tables[0].Rows[0]["IsAgainst_MovementOrder"].ToString();
                    //if(IsAgainMoveOrder=="Yes")
                    //{
                    //    Label1.Visible = true;
                    //    lblIsMoveOrder.Visible = true;
                    //    lblIsMoveOrder.Text = "Against Movement Order Number:-";
                    //    Label1.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();

                    //}
                    //else if (IsAgainMoveOrder == "No")
                    //{
                    //    Label1.Visible = false;
                    //    lblIsMoveOrder.Visible = false;
                    //}

                    //string BagType = ds.Tables[0].Rows[0]["BagType"].ToString();
                    //if (BagType == "9")
                    //{
                    lblBagType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();

                    //}
                    //else if (BagType == "10")
                    //{
                    //    lblBagType.Text = "Old Jute(SBT)";
                    //}
                    //else if (BagType == "11")
                    //{
                    //    lblBagType.Text = "Once Used Jute(SBT)";
                    //}
                    //else if (BagType == "4")
                    //{
                    //    lblBagType.Text = "New PP(HDPE)";
                    //}
                    //else if (BagType == "2")
                    //{
                    //    lblBagType.Text = "Once Used PP(HDPE)";
                    //}
                    //else
                    //{
                    //    lblBagType.Text = "Old PP(HDPE)";
                    //}

                    string Tag = ds.Tables[0].Rows[0]["Tags"].ToString();
                    if (Tag == "Y")
                    {
                        lblTag.Text = "Yes";
                    }
                    else
                    {
                        lblTag.Text = "No";
                    }

                    lblTagNo.Text = ds.Tables[0].Rows[0]["TagNo"].ToString();

                    DateTime frmDate = DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
                    lblAcptDate.Text = frmDate.ToString("dd/MMM/yyyy");

                    string Qty = "", Bags = "", Truck = "", LotNo = "";
                    Qty = ds.Tables[0].Rows[0]["CMRRecd"].ToString();
                    Bags = ds.Tables[0].Rows[0]["Bags"].ToString();
                    Truck = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    LotNo = ds.Tables[0].Rows[0]["LotNumber"].ToString();

                    //For GradeA & Common Rice Values
                    LblTotaGAR.Text = ds.Tables[0].Rows[0]["TotaGA"].ToString();
                    LblTotaSR.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    LblTotaGARmk.Text = ds.Tables[0].Rows[0]["TotaRemark"].ToString();
                    LblChoteToteGAR.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteSR.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    LblChoteToteGARmk.Text = ds.Tables[0].Rows[0]["ChoteToteRemark"].ToString();
                    LblVijatiyeGAR.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeSR.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    LblVijatiyeGARmk.Text = ds.Tables[0].Rows[0]["VijatiyeRemark"].ToString();
                    LblDamageDaaneGAR.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneSR.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    LblDamageDaaneGARmk.Text = ds.Tables[0].Rows[0]["DamageDaaneRemark"].ToString();
                    LblBadrangDaaneGAR.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneSR.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    LblBadrangDaaneGARmk.Text = ds.Tables[0].Rows[0]["BadrangDaaneRemark"].ToString();
                    LblChaakiDaaneGAR.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneSR.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    LblChaakiDaaneGARmk.Text = ds.Tables[0].Rows[0]["ChaakiDaaneRemark"].ToString();
                    LblLaalDaaneGAR.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneSR.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    LblLaalDaaneGARmk.Text = ds.Tables[0].Rows[0]["LaalDaaneRemark"].ToString();
                    LblOtherGAR.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    LblOtherSR.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    LblOtherGARmk.Text = ds.Tables[0].Rows[0]["OtherRemark"].ToString();
                    LblChokarDaaneGAR.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    LblChokarDaaneSR.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    LblChokarDaaneGARmk.Text = ds.Tables[0].Rows[0]["ChokarDaaneRemark"].ToString();
                    LblNamiGAR.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
                    LblNamiSR.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
                    LblNamiGARmk.Text = ds.Tables[0].Rows[0]["NamiRemark"].ToString();
                    string godamname = ds.Tables[0].Rows[0]["Godown_Code"].ToString();
                    string qry = "";
                    qry = "select Godown_ID, Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + godamname + "'";

                    da = new SqlDataAdapter(qry, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    }

                    GetCropYearValues();

                    ////For Milling Type
                    //LblMType.Text = LblMType0.Text = LblMType1.Text = LblMType2.Text = LblMType3.Text = LblMType4.Text = LblMType5.Text = LblMType6.Text = LblMType7.Text = LblMType8.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();

                    string Daane = ds.Tables[0].Rows[0]["Daane"].ToString();
                    if (Daane == "Damage")
                    {
                        lblDaaneType.Text = "क्षतिग्रस्त दाने";
                    }
                    else
                    {
                        lblDaaneType.Text = "मामूली क्षतिग्रस्त दाने";
                    }

                    lblServerDateTime.Text = ds.Tables[0].Rows[0]["ServerDate"].ToString();

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    string QRGridDetails = "Dist=" + lblDistManagerName.Text + ", CropYear=" + lblYear.Text + ", Agrmt_No=" + lblAgrmtNo.Text + ", Acpt/Reject_No=" + lblAcptNo.Text + ", Lot_No=" + LotNo + ", Qty=" + Qty + "(Qtls), Bags=" + Bags + ", Truck_No=" + Truck + "";
                    ImgQRCode.ImageUrl = ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;


                   
                   
                }
                else
                {
                    GridView1.DataSource = "";
                    GridView1.DataBind();
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

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        // CropYear = Session["CropYear"].ToString();
        string GodownName = e.Row.Cells[3].Text;

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    e.Row.Cells[3].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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
}