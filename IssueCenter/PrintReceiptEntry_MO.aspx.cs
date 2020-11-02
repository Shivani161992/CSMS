using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IssueCenter_PrintReceiptEntry_MO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string Dist_Id = "", QRDetails = "", ChallanNo = "", SubChallanNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                lblReceiptID.Text = Session["ReceiptID"].ToString();
                lblDCNO.Text = Session["ChallanNo"].ToString();

                Dist_Id = Session["dist_id"].ToString();

                GetData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select  BT.BagType  as BagsTypeNew, Variation_qty,Variation_Bags,RecChange_Godown,RecDefault_Godown,RecTruck_No,(select DepotName from tbl_MetaData_DEPOT where DepotID=REMO.RecIssue_Center) RecICName,RecIssue_Center,RecTruck_No,RecdBags,RecQty,ToulReceiptNo,SendingBags_Type,SendingBags,SendingQty,SendingGodown,(select DepotName from tbl_MetaData_DEPOT where DepotID=REMO.SendingIssue_Center) SendICName,SendingIssue_Center,(select district_name from pds.districtsmp where district_code=REMO.ToDist) ToDistName,ToDist,(select district_name from pds.districtsmp where district_code=REMO.FrmDist) FrmDistName,FrmDist,SendingTruck_No,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=REMO.Commodity) ComdtyName,Commodity,TO_No,MoveOrdernum,SendingDate,RecDate,CreatedDate,GETDATE() CurrentDate From ReceiptEntry_MO REMO  inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=REMO.SendingBags_Type where ReceiptID='" + lblReceiptID.Text + "' and DC_MO='" + lblDCNO.Text + "' and ToDist='" + Dist_Id + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDate"].ToString();

                    lblMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    lblTranspNo.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();
                    lblComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    lblSendingTCNO.Text = ds.Tables[0].Rows[0]["SendingTruck_No"].ToString();
                    lblSendingDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    lblDistName.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                    lblSendingIC.Text = ds.Tables[0].Rows[0]["SendICName"].ToString();
                    lblSendingGodown.Text = ds.Tables[0].Rows[0]["SendingGodown"].ToString();

                    lblSendingQty.Text = ds.Tables[0].Rows[0]["SendingQty"].ToString();
                    lblSendingBags.Text = ds.Tables[0].Rows[0]["SendingBags"].ToString();
                    lblSendingBagType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();
                    lblToulReceipt.Text = ds.Tables[0].Rows[0]["ToulReceiptNo"].ToString();

                    lblRecdQty.Text = ds.Tables[0].Rows[0]["RecQty"].ToString();
                    lblRecdBags.Text = ds.Tables[0].Rows[0]["RecdBags"].ToString();
                    lblICName.Text = ds.Tables[0].Rows[0]["RecICName"].ToString();
                    lblRecdTruckNo.Text = ds.Tables[0].Rows[0]["RecTruck_No"].ToString();
                    lblRecdVarBags.Text = ds.Tables[0].Rows[0]["Variation_Bags"].ToString();
                    lblRecdVarQty.Text = ds.Tables[0].Rows[0]["Variation_qty"].ToString();

                    hdfRecChange_Godown.Value = ds.Tables[0].Rows[0]["RecChange_Godown"].ToString();
                    hdfRecDefault_Godown.Value = ds.Tables[0].Rows[0]["RecDefault_Godown"].ToString();
                    hdfFrmDist.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    hdfToDist.Value = ds.Tables[0].Rows[0]["ToDist"].ToString();

                    if (hdfRecChange_Godown.Value == "00")
                    {
                        lblRecdGodam.Text = hdfRecDefault_Godown.Value;
                    }
                    else
                    {
                        lblRecdGodam.Text = hdfRecChange_Godown.Value;
                    }

                    string commodityID = ds.Tables[0].Rows[0]["Commodity"].ToString();

                    if (commodityID == "25")
                    {
                        lblQtls0.Text = lblQtls1.Text = lblQtls.Text = "(Bales)";
                    }
                    else
                    {
                        lblQtls0.Text = lblQtls1.Text = lblQtls.Text = "(Qtls)";
                    }

                    DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    lblDate.Text = CreatedDate.ToString("dd/MMM/yyyy");

                    DateTime RecdDate = DateTime.Parse(ds.Tables[0].Rows[0]["RecDate"].ToString());
                    lblRecdDate.Text = RecdDate.ToString("dd/MMM/yyyy");

                    DateTime SendDate = DateTime.Parse(ds.Tables[0].Rows[0]["SendingDate"].ToString());
                    lblTranspDate.Text = SendDate.ToString("dd/MMM/yyyy");

                    GetBGName();

                    ChallanNo = lblDCNO.Text;
                    SubChallanNo = ChallanNo.Substring(0, 5);

                    if (SubChallanNo == "MORTR" || SubChallanNo == "MGRTR")
                    {
                        LblBookNo.Text = "बुक नंबर";
                        lblPageNo.Text = "पेज नंबर";

                        GetBookPageNo();
                    }


                    QRDetails = "ReceiptNo=" + lblReceiptID.Text + ", MONo=" + lblMONo.Text + ", TONo=" + lblTranspNo.Text + ", DCNo=" + lblDCNO.Text + ", SendingDist=" + lblSendingDist.Text + ", SendingQty=" + lblSendingQty.Text + ", SendingBags=" + lblSendingBags.Text + ", RecDist=" + lblDistName.Text + ", RecQty=" + lblRecdQty.Text + ", RecBags=" + lblRecdBags.Text;
                    ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRDetails;
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

    public void GetBGName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select1 = string.Format("select (select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblRecdGodam.Text + "') RecdGodown_Name,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblSendingGodown.Text + "') SendingGodown_Name");
                da1 = new SqlDataAdapter(select1, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        lblSendingGodown.Text = ds1.Tables[0].Rows[0]["SendingGodown_Name"].ToString();
                        lblRecdGodam.Text = ds1.Tables[0].Rows[0]["RecdGodown_Name"].ToString();
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

    public void GetBookPageNo()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Book_No,Page_No From DeliveryChallan_MO where DC_MO='" + Session["ChallanNo"].ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblSendingIC.Text = ds.Tables[0].Rows[0]["Book_No"].ToString();
                    lblSendingGodown.Text = ds.Tables[0].Rows[0]["Page_No"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Book Number Is Not Available On These District'); </script> ");
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