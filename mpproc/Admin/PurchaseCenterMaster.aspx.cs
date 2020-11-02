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

public partial class mpproc_Admin_PurchaseCenterMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    Districts Dobj = null;
    PurchaseCenter Pobl = null;
    MarketingSeas Mobj = null;
    cropYear cobj = null;    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
        string admin=Session["admin"].ToString();
        if (admin != "")
        {
            if (!IsPostBack)
            {
                GetDistrict();
                GetAllPurchaseCeneters();
                GetMarketingSeason();
                GetCropYear();
            }
            
        }
    }

    private void GetCropYear()
    {
        cobj = new cropYear(ComObj);
        DataSet ds = cobj.selectAll();
        if (ds == null)
        {
        }
        else
        {
            //for first Crop Year DropdownList 
            ddl_CropYear_Fetch.DataSource = ds.Tables[0];
            ddl_CropYear_Fetch.DataTextField = "CropYear";
            ddl_CropYear_Fetch.DataValueField = "CropId";
            ddl_CropYear_Fetch.DataBind();
            ddl_CropYear_Fetch.Items.Insert(0, "--Select--");

            //for second Crop Year DropdownList 
            ddl_CropYear_Selected.DataSource = ds.Tables[0];
            ddl_CropYear_Selected.DataTextField = "CropYear";
            ddl_CropYear_Selected.DataValueField = "CropId";
            ddl_CropYear_Selected.DataBind();
            ddl_CropYear_Selected.Items.Insert(0, "--Select--");

            //for third Crop Year DropdownList 
            ddl_AddNewCropYear.DataSource = ds.Tables[0];
            ddl_AddNewCropYear.DataTextField = "CropYear";
            ddl_AddNewCropYear.DataValueField = "CropId";
            ddl_AddNewCropYear.DataBind();
            ddl_AddNewCropYear.Items.Insert(0, "--Select--");
        }
    }

    private void GetMarketingSeason()
    {
        Mobj = new MarketingSeas(ComObj);
        DataSet ds = Mobj.selectAll();
        if (ds == null)
        {
        }
        else
        {
            //for First MarketSeason DropdownList 
            ddl_MarketSeaon_Fetch.DataSource = ds.Tables[0];
            ddl_MarketSeaon_Fetch.DataValueField = "MarkSeasId";
            ddl_MarketSeaon_Fetch.DataTextField = "MarkSeaon";
            ddl_MarketSeaon_Fetch.DataBind();
            ddl_MarketSeaon_Fetch.Items.Insert(0, "--Select--");

            //for third MarketSeason DropdownList 
            ddl_MarketSeason_Selected.DataSource = ds.Tables[0];
            ddl_MarketSeason_Selected.DataValueField = "MarkSeasId";
            ddl_MarketSeason_Selected.DataTextField = "MarkSeaon";
            ddl_MarketSeason_Selected.DataBind();
            ddl_MarketSeason_Selected.Items.Insert(0, "--Select--");

            //for third MarketSeason DropdownList 
            ddl_AddNewMarketSeason.DataSource = ds.Tables[0];
            ddl_AddNewMarketSeason.DataValueField = "MarkSeasId";
            ddl_AddNewMarketSeason.DataTextField = "MarkSeaon";
            ddl_AddNewMarketSeason.DataBind();
            ddl_AddNewMarketSeason.Items.Insert(0, "--Select--");
        }

    }

    private void GetAllPurchaseCeneters()
    {
        Pobl = new PurchaseCenter(ComObj);

        string qrySelect = "select pcm.PcId,pcm.PurchaseCenterName as PurchaseCenterName,pcm.CommodityId,cm.CommodityName,pcm.DistrictId,dm.DistrictName as DistrictName,pcm.MarkSeasId,msm.MarkSeaon as MarketingSeason,pcm.cropyear as CropYear,pcm.Address as Address,pcm.Phone as Phone,pcm.PC_CategoryID,pcc.PC_Category as Category,pcm.Block as BlockTehsil,pcm.NodalOff as NodalOfficer from PurchaseCenterMaster pcm,DistrictMaster dm,CommodityMaster cm,MarketingSeasonMaster msm ,PurchaseCentreCategory pcc where cm.CommodityId=pcm.CommodityId and dm.DistrictCode=pcm.DistrictId and pcm.MarkSeasId=msm.MarkSeasId and pcc.PC_CategoryID=pcm.PC_CategoryID order by pcm.cropyear desc";

        DataSet ds = Pobl.selectAny(qrySelect);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                fillGrid(ds);
                ViewState["dsPurchase"] = ds;
            }
        }
    }

    private void GetPurchaseCenetersByDistrict()
    {
        Pobl = new PurchaseCenter(ComObj);

        string qrySelect = "select pcm.PcId,pcm.PurchaseCenterName as PurchaseCenterName,pcm.CommodityId,cm.CommodityName,pcm.DistrictId,dm.DistrictName as DistrictName,pcm.MarkSeasId,msm.MarkSeaon as MarketingSeason,pcm.cropyear as CropYear,pcm.Address as Address,pcm.Phone as Phone,pcm.PC_CategoryID,pcc.PC_Category as Category,pcm.Block as BlockTehsil,pcm.NodalOff as NodalOfficer from PurchaseCenterMaster pcm,DistrictMaster dm,CommodityMaster cm,MarketingSeasonMaster msm ,PurchaseCentreCategory pcc where cm.CommodityId=pcm.CommodityId and dm.DistrictCode=pcm.DistrictId and pcm.MarkSeasId=msm.MarkSeasId and pcc.PC_CategoryID=pcm.PC_CategoryID and pcm.DistrictId='" + ddl_District.SelectedValue.ToString() + "'";

        DataSet ds = Pobl.selectAny(qrySelect);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                fillGrid(ds);
                ViewState["dsPurchase"] = ds;
            }
        }
    }

    private void GetPurchaseCenetersByDistrictCropYearMarketSeason()
    {
        Pobl = new PurchaseCenter(ComObj);

        string qrySelect = "select pcm.PcId,pcm.PurchaseCenterName as PurchaseCenterName,pcm.CommodityId,cm.CommodityName,pcm.DistrictId,dm.DistrictName as DistrictName,pcm.MarkSeasId,msm.MarkSeaon as MarketingSeason,pcm.cropyear as CropYear,pcm.Address as Address,pcm.Phone as Phone,pcm.PC_CategoryID,pcc.PC_Category as Category,pcm.Block as BlockTehsil,pcm.NodalOff as NodalOfficer from PurchaseCenterMaster pcm,DistrictMaster dm,CommodityMaster cm,MarketingSeasonMaster msm ,PurchaseCentreCategory pcc where cm.CommodityId=pcm.CommodityId and dm.DistrictCode=pcm.DistrictId and pcm.MarkSeasId=msm.MarkSeasId and pcc.PC_CategoryID=pcm.PC_CategoryID and pcm.DistrictId='" + ddl_District_Fetch.SelectedValue.ToString() + "' and pcm.MarkSeasId='" + ddl_MarketSeaon_Fetch.SelectedValue.ToString() + "' and pcm.cropyear='" + ddl_CropYear_Fetch.SelectedItem.Text.ToString() + "'";

        DataSet ds = Pobl.selectAny(qrySelect);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                fillGridFetch(ds);
                ViewState["dsPurchaseFetch"] = ds;
            }
        }
    }

    private void fillGridFetch(DataSet ds)
    {
        GridView_Fetch.DataSource = null;
        GridView_Fetch.DataSource = ds.Tables[0];
        GridView_Fetch.DataBind();
    }

    private void GetDistrict()
    {
        Dobj = new Districts(ComObj);

        string qrySelect = "SELECT * FROM DistrictMaster order by DistrictName";

        DataSet ds = Dobj.selectAny(qrySelect);
        if (ds == null)
        {

        }
        else
        {
            //for first District DropdownList 
            ddl_District.DataSource = ds.Tables[0];
            ddl_District.DataValueField = "DistrictCode";
            ddl_District.DataTextField = "DistrictName";
            ddl_District.DataBind();
            ddl_District.Items.Insert(0, "All");

            //for sencond District DropdownList
            ddl_District_Fetch.DataSource = ds.Tables[0];
            ddl_District_Fetch.DataValueField = "DistrictCode";
            ddl_District_Fetch.DataTextField = "DistrictName";
            ddl_District_Fetch.DataBind();
            ddl_District_Fetch.Items.Insert(0, "--Select--");

            //for sencond District DropdownList
            ddl_District_Selected.DataSource = ds.Tables[0];
            ddl_District_Selected.DataValueField = "DistrictCode";
            ddl_District_Selected.DataTextField = "DistrictName";
            ddl_District_Selected.DataBind();
            ddl_District_Selected.Items.Insert(0, "--Select--");

            //for fourth District DropdownList
            ddl_AddNewDistrict.DataSource = ds.Tables[0];
            ddl_AddNewDistrict.DataValueField = "DistrictCode";
            ddl_AddNewDistrict.DataTextField = "DistrictName";
            ddl_AddNewDistrict.DataBind();
            ddl_AddNewDistrict.Items.Insert(0, "--Select--");

        }
    }

    protected void ddl_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_District.SelectedItem.Text == "All")
        {
            GetAllPurchaseCeneters();
        }
        else
        {
            GetPurchaseCenetersByDistrict();
        }
    }

    private void fillGrid(DataSet ds)
    {
        
        GridView_All.DataSource = ds.Tables[0];
        GridView_All.DataBind();
    }

    protected void GridView_All_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dsPurchase"];
        GridView_All.PageIndex = e.NewPageIndex;
        fillGrid(ds);
    }

    protected void GridView_Fetch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dsPurchaseFetch"];
        GridView_Fetch.PageIndex = e.NewPageIndex;
        fillGridFetch(ds);
    }
    protected void btnFetch_Click(object sender, EventArgs e)
    {
        Panel_Fetch.Visible = true;
        if (ddl_District_Fetch.SelectedValue != "--Select--" && ddl_CropYear_Fetch.SelectedValue != "--Select--" && ddl_MarketSeaon_Fetch.SelectedValue != "")
        {
            GetPurchaseCenetersByDistrictCropYearMarketSeason();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Records found for the given values'); </script> ");
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Panel_Adnew.Visible = true;
    }
    protected void btnAddSelected_Click(object sender, EventArgs e)
    {
        if (ddl_District_Selected.SelectedItem.Text != "--Select--" && ddl_MarketSeason_Selected.SelectedItem.Text != "--Select--" && ddl_CropYear_Selected.SelectedItem.Text != "--Select--")
        {
            foreach (GridViewRow grow in GridView_Fetch.Rows)
            {
                CheckBox cb = (CheckBox)grow.FindControl("cklSelect");
                if (cb.Checked == true)
                {
                    Int32 pcid = 0;
                    if (grow.Cells[10].Text != "" || grow.Cells[10].Text != "&nbsp;")
                    {
                        pcid = Convert.ToInt32(grow.Cells[10].Text);
                    }

                    Int32 commodityId = 0;
                    if (grow.Cells[11].Text != "" || grow.Cells[11].Text != "&nbsp;")
                    {
                        commodityId = Convert.ToInt32(grow.Cells[11].Text);
                    }
                    string districtId = ddl_District_Selected.SelectedValue.ToString();
                    string regionId = "2300";
                    string stateId = "23";
                    string marketseasonId = ddl_MarketSeason_Selected.SelectedValue.ToString();
                    string cyear = ddl_CropYear_Selected.SelectedItem.Text;

                    string purchasecentername = "";
                    if (grow.Cells[1].Text == "" || grow.Cells[1].Text == "&nbsp;")
                    {
                        purchasecentername = "";
                    }
                    else
                    {
                        purchasecentername = grow.Cells[1].Text;
                    }
                    string address = "";
                    if (grow.Cells[6].Text == "" || grow.Cells[6].Text == "&nbsp;")
                    {
                        address = "";                        
                    }
                    else
                    {
                        address = grow.Cells[6].Text;
                    }

                    string phone = "";
                    if (grow.Cells[8].Text == "" || grow.Cells[8].Text == "&nbsp;")
                    {
                        phone = "";
                    }
                    else
                    {
                        phone = grow.Cells[8].Text;
                    }
                    string PC_CategoryID = "";
                    if (grow.Cells[14].Text == "" || grow.Cells[14].Text == "&nbsp;")
                    {
                        PC_CategoryID = "";
                    }
                    else
                    {
                        PC_CategoryID = grow.Cells[14].Text;
                    }
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string nodaofficer = "";
                    if (grow.Cells[5].Text == "" || grow.Cells[5].Text == "&nbsp;")
                    {
                        nodaofficer = "";
                    }
                    else
                    {
                        nodaofficer = grow.Cells[5].Text;
                       
                    }
                    string block = "";
                    if (grow.Cells[7].Text == "" || grow.Cells[7].Text == "&nbsp;")
                    {
                        block = "";
                    }
                    else
                    {
                        block = grow.Cells[7].Text;
                        
                    }
                    string status = "4";
                    Int32 masterpcId = 0;
                    if (grow.Cells[10].Text != "" || grow.Cells[10].Text != "&nbsp;")
                    {
                        masterpcId = Convert.ToInt32(grow.Cells[10].Text);
                    }
                    string qryins = "Insert into PurchaseCenterMaster(PcId,CommodityId,DistrictId,RegionId,StateId,MarkSeasId,cropyear,PurchaseCenterName,Address,Phone,PC_CategoryID,CreatedBy,CreatedDate,NodalOff,Block,Status,Master_PCID) values(" + pcid + "," + commodityId + ",'" + districtId + "','" + regionId + "','" + stateId + "','" + marketseasonId + "','" + cyear + "','" + purchasecentername + "','" + address + "','" + phone + "'," + PC_CategoryID + ",'" + ip + "',getDate(),'" + nodaofficer + "','" + block + "','" + status + "'," + pcid + ")";
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand(qryins, con);
                        cmd.ExecuteNonQuery();
                        GetAllPurchaseCeneters();
                    }
                    catch (Exception ex)
                    {
                        Label13.Visible = true;
                        Label13.Text = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }

                }
            }
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Purchase Center added successfully.............'); </script> ");
            Panel_Fetch.Visible = false;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the District,Cropyear,MarketSeason.............'); </script> ");
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (ddl_AddNewCropYear.SelectedItem.Text != "--Select--" && ddl_AddNewDistrict.SelectedItem.Text != "--Select--" && ddl_AddNewMarketSeason.SelectedItem.Text != "--Select--" && txtPurchaseCeneter.Text!="")
        {
            string districtid = ddl_AddNewDistrict.SelectedValue.ToString();
            string marketseason = ddl_AddNewMarketSeason.SelectedValue.ToString();
            string crpyr = ddl_AddNewCropYear.SelectedItem.Text.ToString();
            string purchasecentername = txtPurchaseCeneter.Text.ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int PC_CategoryID = 1;
            int commodityId = 6;
            string status = "4";            
            string regionId = "2300";
            string stateId = "23";
            string qrymax = " Select max(PcId) from PurchaseCenterMaster";
            try
            {
                con.Open();
                cmd = new SqlCommand(qrymax, con);
                string pcid = cmd.ExecuteScalar().ToString();
                if (pcid != "")
                {
                    long pcidnew = Convert.ToInt64(pcid) + 1;
                    string qryins = "Insert into PurchaseCenterMaster(PcId,CommodityId,DistrictId,RegionId,StateId,MarkSeasId,cropyear,PurchaseCenterName,PC_CategoryID,CreatedBy,CreatedDate,Status,Master_PCID) values(" + pcidnew + "," + commodityId + ",'" + districtid + "','" + regionId + "','" + stateId + "','" + marketseason + "','" + crpyr + "','" + purchasecentername + "'," + PC_CategoryID + ",'" + ip + "',getDate(),'" + status + "'," + pcidnew + ")";
                    SqlCommand command = new SqlCommand(qryins, con);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Label13.Visible = true;
                Label13.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Purchase Centers added successfully.............'); </script> ");
            Panel_Adnew.Visible = false;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select the District,Cropyear,MarketSeason.............'); </script> ");
        }
    }

    protected void cklSelectAll_CheckedChanged(object sender, EventArgs e)
    {        
        GridViewRow headerrow = GridView_Fetch.HeaderRow;
        CheckBox cbh = (CheckBox)headerrow.FindControl("cklSelectAll");
        if (cbh.Checked == true)
        {
            foreach (GridViewRow grow in GridView_Fetch.Rows)
            {
                CheckBox cb = (CheckBox)grow.FindControl("cklSelect");
                cb.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow grow in GridView_Fetch.Rows)
            {
                CheckBox cb = (CheckBox)grow.FindControl("cklSelect");
                cb.Checked = false;
            }
        }
            

            
       
    }
    protected void GridView_All_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlTransaction trans = null;

        string pid=GridView_All.SelectedRow.Cells[10].Text;
        Int64 pcid=Convert.ToInt64(pid);
        if (pid != "")
        {
            cmd = new SqlCommand();
            try
            {
                con.Open();
                cmd.Connection = con;
                trans = con.BeginTransaction();
                cmd.Transaction = trans;
                if (con != null)
                {
                    string qryins = "Insert into PurchaseCenterMaster_Log Select * from PurchaseCenterMaster where PcId=" + pcid + "";
                    cmd.CommandText = qryins;
                    cmd.ExecuteNonQuery();
                    string qrydel = "delete from PurchaseCenterMaster where PcId=" + pcid + "";
                    cmd.CommandText = qrydel;
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    trans.Dispose();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Purchase Center deleted successfully.............'); </script> ");
                    GetAllPurchaseCeneters();                   
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Label13.Visible = true;
                Label13.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        
    }
    protected void ddl_District_Fetch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_District_Selected.SelectedValue = ddl_District_Fetch.SelectedValue;
    }
}
