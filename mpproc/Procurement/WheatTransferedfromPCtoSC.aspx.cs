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
using Data;
using System.Data.SqlClient;
using DataAccess;

public partial class mpproc_Procurement_WheatTransferedfromPCtoSC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    Districts Dobj = null;
    Agency Aobj = null;
    MarketingSeas Mobj = null;
    cropYear cobj = null;
    comodity CdObj = null;
    SqlString SqlObj = null;
    DataReader objDr = null;
    Depot Depo_Obj = null;
    PurchaseCenter PcObj = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
        string distid = Session["dist_id"].ToString();
        string cropyear = Session["cropyear"].ToString();
        lblNoRecord.Visible = false;
        btn_AddNew.Enabled = true;
        string tdate = DateTime.Today.ToString("MM/dd/yyyy");
        hydTodayDate.Value = tdate;
        btn_AddNew.Attributes.Add("onclick", "return chkDate()");
        
        if (distid != "")
        {
            string marketinseasonid = Session["Markseas_id"].ToString();
            lbl_DistRes.Text = "District:" + " " + Session["dist_name"].ToString();
            lbl_AgencyRes.Text = "Agency:" + " " + Session["Ag_Name"].ToString();
            lbl_MarSeasRes.Text = "Marketing Season:" + " " + Session["Mark_Seas"].ToString();
            lbl_CropYearRes.Text ="Crop Year:" + " " + Session["cropyear"].ToString();
            string agencyid = Session["Ag_id"].ToString();
            if (!IsPostBack)
            {
                
                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                getPurchaseCenter(distid,marketinseasonid,cropyear);
                DDL_PC.SelectedValue = Session["pcId"].ToString();
                getCommodity(marketinseasonid);
                getDistrcts();
                float progressiveQty = getProgressiveQty(distid, marketinseasonid, cropyear, DDL_Commodity.SelectedValue.ToString(), DDL_PC.SelectedValue.ToString(),agencyid);
                lblProgressiveProc.Text = progressiveQty.ToString();
                float qtyLifted = getQtyLifted(distid, marketinseasonid, cropyear, DDL_Commodity.SelectedValue.ToString(), DDL_PC.SelectedValue.ToString(),agencyid);
                lblQtyLifted.Text = qtyLifted.ToString();
                
            }
            fillGrid();
        }
        txtQtyTransfromPC.Attributes.Add("onchange", "return checkQuantity()");

        txtQtyTransfromPC.Attributes.Add("onchange", "return CheckIsNumeric(event,this)");
        txtQtyDepositedGodown.Attributes.Add("onchange", "return CheckIsNumeric(event,this)");

    }

    private void fillGrid()
    {
        string cropyr = Session["Markseas_id"].ToString();
        string mrktseason = Session["cropyear"].ToString();
        string disno = Session["dist_id"].ToString();
        string agencyid = Session["Ag_id"].ToString();
        string puchasecenterid = Session["pcId"].ToString();
        string date = "";
        if (DaintyDate1.Text != "")
        {
            date = getDate_MDY(DaintyDate1.Text);
        }
        objDr = new DataReader(ComObj);
        string cropyear = Session["cropyear"].ToString();

        //string strqsl = "select CommodityTransfertoDepot.TransactionID,CONVERT(varchar,CommodityTransfertoDepot.TransferDate) ,PurchaseCenterMaster.PurchaseCenterName,CommodityMaster.CommodityName,DEPOTMASTER.GodownName ,CommodityTransfertoDepot.QtyTransferred from  CommodityTransfertoDepot left join DEPOTMASTER on CommodityTransfertoDepot.ToDepotID=DEPOTMASTER.GodownID left join PurchaseCenterMaster on PurchaseCenterMaster.PcId=CommodityTransfertoDepot.PCID left join CommodityMaster on CommodityMaster.CommodityId=CommodityTransfertoDepot.CommodityId where CommodityTransfertoDepot.CropYearId='" + cropyr + "' and CommodityTransfertoDepot.MarketingSeason='" + mrktseason + "' and CommodityTransfertoDepot.DistrictId='" + disno + "' and CommodityTransfertoDepot.PCType_ID_Agency='" + agencyid + "' and CommodityTransfertoDepot.PCID='" + puchasecenterid + "' and CommodityTransfertoDepot.TransferDate='" + date + "'";
        string strqsl = "select distinct ctd.TransactionID,CONVERT(varchar,ctd.TransferDate,106) as TransferDate,pm.PurchaseCenterName as PurchaseCenter, cm.CommodityName as Commodity,dm.GodownName as StorageCenter,ctd.QtyTransferred,ctd.QtyDeposited from CommodityTransfertoDepot ctd,PurchaseCenterMaster pm,CommodityMaster cm,DEPOTMASTER dm where cm.CommodityId=ctd.CommodityId and pm.PcId=ctd.PCID and dm.GodownID=ctd.ToDepotID and ctd.CropYearId='" + cropyr + "' and ctd.MarketingSeason='" + mrktseason + "' and ctd.DistrictId='" + disno + "' and ctd.PCType_ID_Agency='" + agencyid + "' and ctd.PCID='" + puchasecenterid + "' and ctd.TransferDate='" + date + "'";
        DataSet ds = objDr.selectAny(strqsl);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView_Transfered.Visible = true;
                GridView_Transfered.DataSource = ds.Tables[0];
                GridView_Transfered.DataBind(); ;
                lblNoRecord.Text = "";
            }
            else
            {
                lblNoRecord.Visible = true;
                GridView_Transfered.Visible = false;
            }
        }
        else
        {
            lblNoRecord.Visible = true;
            GridView_Transfered.Visible = false;
        }
    }

    private void getStorageCenter(string distno)
    {
        Depo_Obj = new Depot(ComObj);
        //string strcom = Session["Markseas_id"].ToString();
        string strsql = "SELECT * FROM DEPOTMASTER where DistrictId='" + distno + "'";
        DataSet ds = Depo_Obj.selectAny(strsql);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_StorageCenter.DataSource = ds.Tables[0];
                ddl_StorageCenter.DataTextField = "GodownName";
                ddl_StorageCenter.DataValueField = "GodownID";
                ddl_StorageCenter.DataBind();
                ddl_StorageCenter.Items.Insert(0, "--Select--");
            }
        }
    }

    private void getDistrcts()
    {
        Dobj = new Districts(ComObj);

        string qrySelect = "SELECT * FROM DistrictMaster order by DistrictName";

        DataSet ds = Dobj.selectAny(qrySelect);
        if (ds == null)
        {

        }
        else
        {

            DDL_Dist.DataSource = ds.Tables[0];
            DDL_Dist.DataValueField = "DistrictCode";
            DDL_Dist.DataTextField = "DistrictName";
            DDL_Dist.DataBind();
            DDL_Dist.Items.Insert(0, "--Select--");

        }
    }

    private float getProgressiveQty(string disno, string mid, string crpyr, string commodity, string purchasecenter, string agencyid)
    {
        float progressiveQty = 0.0F;
        string date = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));        
        objDr = new DataReader(ComObj);
        string strqsl = "Select sum(QtyProcured) as QtyProcured from CommodityProcurementByAgencyFromFarmer where DistrictId='" + disno + "' and CropYear='" + crpyr + "' and MarketingSeasonId='" + mid + "' and PCID='" + purchasecenter + "' and CommodityId='" + commodity + "' and PCType_ID_Agency='" + agencyid + "' and ProcurementDate <= '" + date + "'";
        DataSet ds = objDr.selectAny(strqsl);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string qty = ds.Tables[0].Rows[0]["QtyProcured"].ToString();
                progressiveQty = CheckFloat(qty);
            }
        }
        return progressiveQty;
            
 
    }
    private float getQtyLifted(string disno, string mid, string crpyr, string commodity, string purchasecenter, string agencyid)
    {
        float qtyLifted = 0.0F;
        string date = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));
        objDr = new DataReader(ComObj);
        string strqsl = "Select sum(QtyTransferred) as QtyTransferred from CommodityTransfertoDepot where DistrictId='" + disno + "' and MarketingSeason='" + crpyr + "' and CropYearId='" + mid + "' and PCID='" + purchasecenter + "' and CommodityId='" + commodity + "' and PCType_ID_Agency='" + agencyid + "' and TransferDate <='" + date + "'";
        DataSet ds = objDr.selectAny(strqsl);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string qty = ds.Tables[0].Rows[0]["QtyTransferred"].ToString();
                qtyLifted = CheckFloat(qty);
            }
        }
        return qtyLifted;
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    private void getCommodity(string strcom)
    {
        CdObj = new comodity(ComObj);
        //string strcom = Session["Markseas_id"].ToString();
        string strsql = "SELECT * FROM CommodityMaster where MarkSeasId='" + strcom + "'";
        DataSet ds = CdObj.selectAny(strsql);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Commodity.DataSource = ds.Tables[0];
                DDL_Commodity.DataTextField = "CommodityName";
                DDL_Commodity.DataValueField = "CommodityId";
                DDL_Commodity.DataBind();
            }
        }
    }
    private float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    private void getPurchaseCenter(string distid,string mid,string cropyear)
    {
        PcObj = new PurchaseCenter(ComObj);
        string str = "SELECT * FROM PurchaseCenterMaster,MarketingSeasonMaster where PurchaseCenterMaster.DistrictId = '" + distid + "'  and  PurchaseCenterMaster.MarkSeasId = '" + mid + "' and cropyear ='" + cropyear + "' and MarketingSeasonMaster.MarkSeasId = PurchaseCenterMaster.MarkSeasId order by PurchaseCenterName ";
        DataSet ds = PcObj.selectAny(str);
        if (ds == null)
        { }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_PC.DataSource = ds.Tables[0];
                DDL_PC.DataTextField = "PurchaseCenterName";
                DDL_PC.DataValueField = "PcId";
                DDL_PC.DataBind();

            }
        }
    }
    protected void btn_fecth_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        DaintyDate1.Text = "";
        txtQtyTransfromPC.Text = "";
        txtQtyDepositedGodown.Text = "";
    }
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        if (txtQtyTransfromPC.Text != "" && txtQtyDepositedGodown.Text != "" && DaintyDate1.Text != "")
        {
            string cropyr = Session["Markseas_id"].ToString();
            string mrktseason = Session["cropyear"].ToString();
            int disno = int.Parse(Session["dist_id"].ToString());
            int agencyid = int.Parse(Session["Ag_id"].ToString());
            int puchasecenterid = int.Parse(Session["pcId"].ToString());
            string date = getDate_MDY(DaintyDate1.Text);
            string todepot = ddl_StorageCenter.SelectedValue.ToString();
            int commodity = int.Parse(DDL_Commodity.SelectedValue.ToString());
            float qtytransfered = CheckFloat(txtQtyTransfromPC.Text);
            float qtydep = CheckFloat(txtQtyDepositedGodown.Text);
            float liftedqty = CheckFloat(lblQtyLifted.Text) + qtytransfered;
            lblQtyLifted.Text = liftedqty.ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            Int64 transactionid = 1;
            objDr = new DataReader(ComObj);
            string qrey = "select Max(TransactionID) as TransactionID  from CommodityTransfertoDepot ctd where ctd.CropYearId='" + cropyr + "' and ctd.MarketingSeason='" + mrktseason + "' and ctd.DistrictId='" + disno + "' and ctd.PCType_ID_Agency='" + agencyid + "' and ctd.PCID='" + puchasecenterid + "'";
            DataSet ds = objDr.selectAny(qrey);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["TransactionID"].ToString() != "")
                    {
                        transactionid = Convert.ToInt64(ds.Tables[0].Rows[0]["TransactionID"].ToString());
                        transactionid = transactionid + 1;
                    }
                    else 
                    {
                        transactionid = 1;
                    }
                }
                else
                {
                    transactionid = 1;
                }

            }
            else
            {
                transactionid = 1;
            }
            try
            {
                string qryins = "Insert into CommodityTransfertoDepot(TransactionID,PCType_ID_Agency,DistrictId,CropYearId,MarketingSeason,PCID,TransferDate,ToDepotID,CommodityId,QtyTransferred,QtyDeposited,Date_Of_Creation,Date_Of_Updation,Ip_Address) values(" + transactionid + "," + agencyid + "," + disno + ",'" + cropyr + "','" + mrktseason + "'," + puchasecenterid + ",'" + date + "','" + todepot + "'," + commodity + "," + qtytransfered + "," + qtydep + ",getDate(),'','" + ip + "') ";
                con.Open();
                cmd = new SqlCommand(qryins, con);
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saves successfully.............'); </script> ");
                btn_AddNew.Enabled = false;
                txtQtyDepositedGodown.Text = "";
                txtQtyTransfromPC.Text = "";
                fillGrid();
            }
            catch (Exception ex)
            {
                Label8.Visible = true;
                Label8.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please fill all the above entries.............'); </script> ");
        }
    }
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string distno=DDL_Dist.SelectedValue.ToString();
        getStorageCenter(distno);
    }


}
