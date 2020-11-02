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

public partial class IssueCenter_Receipt_Entry_MO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    MoveChallan mobj = null;
    protected Common ComObj = null;
    DistributionCenters distobj = null;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string IC_Id = "", Dist_Id = "", strRecChangeBranchval = "", strRecChangeGodownVal = "", strSendDefault_Branch = "", strSendDefault_Godown = "", strSendChange_Branch = "", strSendChange_Godown = "", ReceiptID = "",VariationQty = "";
    double QtyTotal = 0, QtyRemTotal = 0, SendQty = 0, BalQty = 0, RecdQty = 0, VariationBags = 0;
    public long getnum = 0;
    int month, year;

    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        distobj = new DistributionCenters(ComObj);

        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                txtTCNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTCNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTCNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtToulReceiptNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtToulReceiptNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtToulReceiptNo.Attributes.Add("onchange", "return chksqltxt(this)");

                trMsg.Visible = trBG.Visible = trBG1.Visible = false;

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                Session["DC_MO"] = "";
                Session["ModeofDispatch"] = "";
                Session["ReceiptID"] = "";
                Session["ChallanNo"] = "";

                GetMPIssueCentre();
                //GetBranch();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }

    protected void ddlSArrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        trMsg.Visible = trBG.Visible = trBG1.Visible = trBookNo.Visible = false;

        txtTONo.Text = txtCommodity.Text = txtMONo.Text = txtTransporterName.Text = txtTranspDate.Text = txtFrmDist.Text = txtSendQty.Text = txtSendBags.Text = txtBagsType.Text = txtTruckNo.Text = txtSendIC.Text = txtSendBranch.Text = txtSendGodown.Text = txtCropYear.Text = txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = txtIssuedDate.Text = txtRecdQty.Text = txtRecdBags.Text = txtTCNo.Text = "";
        ViewState["hdfSMO"] = ViewState["hdfSTO_No"] = ViewState["hdfDC_No"] = ViewState["hdfTransporter_ID"] = ViewState["hdfFrmDist"] = ViewState["hdfToDist"] = ViewState["hdfCommodity"] = ViewState["hdfTOEndDate"] = ViewState["hdfModeofDispatch"] = ViewState["hdfSendIssue_Center"] = ViewState["hdfSendDefault_Branch"] = ViewState["hdfSendDefault_Godown"] = ViewState["hdfSendChange_Branch"] = ViewState["hdfSendChange_Godown"] = ViewState["hdfRecIC"] = ViewState["hdfRecBranch"] = ViewState["hdfRecGodown"] = ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = ViewState["hdfRecSMO"] = ViewState["hdfRecRMO"] = "";

        hdfSMO.Value = hdfSTO_No.Value = hdfDC_No.Value = hdfTransporter_ID.Value = hdfFrmDist.Value = hdfCommodity.Value = hdfTranspDate.Value = hdfModeofDispatch.Value = hdfToDist.Value = hdfTOEndDate.Value = hdfRecIC.Value = hdfRecBranch.Value = hdfRecGodown.Value = "";
        ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = "";

        GridView1.DataSource = "";
        GridView1.DataBind();

        ddlChallanNo.Items.Clear();

        if (ddlSArrival.SelectedIndex > 0)
        {
            if (ddlSArrival.SelectedValue == "02") //Other Depot
            {
                LblChallanNo.Text = "Challan Number";
                GetDeliveryChallan();
            }
            else if (ddlSArrival.SelectedValue == "07") //From RailHead
            {
                LblChallanNo.Text = "From District";
                trBookNo.Visible = true;
                GetDistName();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Source of Arrival चुनें |'); </script> ");
        }
    }

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlChallanNo.DataSource = ds.Tables[0];
                        ddlChallanNo.DataTextField = "district_name";
                        ddlChallanNo.DataValueField = "district_code";
                        ddlChallanNo.DataBind();
                        ddlChallanNo.Items.Insert(0, "--Select--");
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

    public void GetDeliveryChallan()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();



                string select = "Select DC_MO,REPLACE(CONVERT(NVARCHAR,CreatedDate, 111), '/', '-') + ' ' + CONVERT(varchar(35),CreatedDate,114) As CreatedDate From DeliveryChallan_MO where ModeofDispatch='12' and ToDist='" + Dist_Id + "' and (RecIC='" + IC_Id + "' or RecIC is null) and (IsReceived='N' and ((DATEADD(DAY,410,CreatedDate))>=Getdate())) and CreatedDate>='2015-10-1' and DC_MO NOT IN (Select challan_no From tbl_Receipt_Details where Dist_Id='" + Dist_Id + "' and S_of_arrival='02' and challan_no LIKE 'MORD%') order by DC_MO";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlChallanNo.DataSource = ds.Tables[0];
                    ddlChallanNo.DataTextField = "DC_MO";
                    ddlChallanNo.DataValueField = "CreatedDate";
                    ddlChallanNo.DataBind();
                    ddlChallanNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('चालान नंबर उपलब्ध नहीं है|'); </script> ");
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
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
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
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
        }
    }

    public void GetBranch()
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

    protected void ddlChallanNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        trMsg.Visible = trBG.Visible = trBG1.Visible = false;

        //For Road
        txtTONo.Text = txtCommodity.Text = txtMONo.Text = txtTransporterName.Text = txtTranspDate.Text = txtFrmDist.Text = txtSendQty.Text = txtSendBags.Text = txtBagsType.Text = txtTruckNo.Text = txtSendIC.Text = txtSendBranch.Text = txtSendGodown.Text = txtCropYear.Text = txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = txtIssuedDate.Text = txtRecdQty.Text = txtRecdBags.Text = txtTCNo.Text = txtToulReceiptNo.Text = "";
        ViewState["hdfSMO"] = ViewState["hdfSTO_No"] = ViewState["hdfDC_No"] = ViewState["hdfTransporter_ID"] = ViewState["hdfFrmDist"] = ViewState["hdfToDist"] = ViewState["hdfCommodity"] = ViewState["hdfTOEndDate"] = ViewState["hdfModeofDispatch"] = ViewState["hdfSendIssue_Center"] = ViewState["hdfSendDefault_Branch"] = ViewState["hdfSendDefault_Godown"] = ViewState["hdfSendChange_Branch"] = ViewState["hdfSendChange_Godown"] = ViewState["hdfRecIC"] = ViewState["hdfRecBranch"] = ViewState["hdfRecGodown"] = ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = ViewState["hdfRecSMO"] = ViewState["hdfRecRMO"] = "";

        //For Rack
        hdfSMO.Value = hdfSTO_No.Value = hdfDC_No.Value = hdfTransporter_ID.Value = hdfFrmDist.Value = hdfCommodity.Value = hdfTranspDate.Value = hdfModeofDispatch.Value = hdfToDist.Value = hdfTOEndDate.Value = hdfRecIC.Value = hdfRecBranch.Value = hdfRecGodown.Value = "";
        ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = "";

        GridView1.DataSource = "";
        GridView1.DataBind();

        if (ddlSArrival.SelectedValue == "02") //Other Depot
        {
            if (ddlChallanNo.SelectedIndex > 0)
            {
                GetChallanData();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan Number'); </script> ");
            }
        }
        else if (ddlSArrival.SelectedValue == "07") //From RailHead
        {
            if (ddlChallanNo.SelectedIndex > 0)
            {
                GetBookNoFrmDC();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select From District'); </script> ");
            }
        }
    }

    public void GetBookNoFrmDC()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();

        ddlBookNo.Items.Clear();
        ddlPageNo.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select distinct Book_No From DeliveryChallan_MO where FrmDist='" + ddlChallanNo.SelectedValue.ToString() + "' and ToDist='" + Dist_Id + "' and ModeofDispatch='13' and IsReceived='N' and Issue_Center='0000' and Default_Branch='0000' and Book_No Is Not Null and RecIC='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBookNo.DataSource = ds.Tables[0];
                    ddlBookNo.DataTextField = "Book_No";
                    ddlBookNo.DataValueField = "Book_No";
                    ddlBookNo.DataBind();
                    ddlBookNo.Items.Insert(0, "--Select--");
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

    protected void ddlBookNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPageNo.Items.Clear();

        GridView1.DataSource = "";
        GridView1.DataBind();

        txtTONo.Text = txtCommodity.Text = txtMONo.Text = txtTransporterName.Text = txtTranspDate.Text = txtFrmDist.Text = txtSendQty.Text = txtSendBags.Text = txtBagsType.Text = txtTruckNo.Text = txtSendIC.Text = txtSendBranch.Text = txtSendGodown.Text = txtCropYear.Text = txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = txtIssuedDate.Text = txtRecdQty.Text = txtRecdBags.Text = txtTCNo.Text = "";
        hdfSMO.Value = hdfSTO_No.Value = hdfDC_No.Value = hdfTransporter_ID.Value = hdfFrmDist.Value = hdfCommodity.Value = hdfTranspDate.Value = hdfModeofDispatch.Value = hdfToDist.Value = hdfTOEndDate.Value = hdfRecIC.Value = hdfRecBranch.Value = hdfRecGodown.Value = "";
        ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = ViewState["hdfRecRMO"] = "";

        if (ddlBookNo.SelectedIndex > 0)
        {
            GetPageNoFrmDC();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Book Number'); </script> ");
        }
    }


    public void GetPageNoFrmDC()
    {
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Page_No,DC_MO From DeliveryChallan_MO where FrmDist='" + ddlChallanNo.SelectedValue.ToString() + "' and ToDist='" + Dist_Id + "' and ModeofDispatch='13' and IsReceived='N' and Issue_Center='0000' and Default_Branch='0000' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and RecIC='" + IC_Id + "' order by CONVERT(INT, Replace(Page_No, 'Group',''))";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPageNo.DataSource = ds.Tables[0];
                    ddlPageNo.DataTextField = "Page_No";
                    ddlPageNo.DataValueField = "DC_MO";
                    ddlPageNo.DataBind();
                    ddlPageNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Page Number Is Not Available'); </script> ");
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

    protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = "";
        GridView1.DataBind();

        txtTONo.Text = txtCommodity.Text = txtMONo.Text = txtTransporterName.Text = txtTranspDate.Text = txtFrmDist.Text = txtSendQty.Text = txtSendBags.Text = txtBagsType.Text = txtTruckNo.Text = txtSendIC.Text = txtSendBranch.Text = txtSendGodown.Text = txtCropYear.Text = txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = txtIssuedDate.Text = txtRecdQty.Text = txtRecdBags.Text = txtTCNo.Text = "";
        hdfSMO.Value = hdfSTO_No.Value = hdfDC_No.Value = hdfTransporter_ID.Value = hdfFrmDist.Value = hdfCommodity.Value = hdfTranspDate.Value = hdfModeofDispatch.Value = hdfToDist.Value = hdfTOEndDate.Value = hdfRecIC.Value = hdfRecBranch.Value = hdfRecGodown.Value = "";
        ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = ViewState["hdfRecRMO"] =  "";

        if (ddlPageNo.SelectedIndex > 0)
        {
            GetPageNoDataFrmDC();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Page Number'); </script> ");
        }
    }

    public void GetPageNoDataFrmDC()
    {
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select BT.BagType  as BagsTypeNew,  (select DepotName from tbl_MetaData_DEPOT where DepotID=DC.RecIC) RecICName,DC.CropYear,(select district_name From pds.districtsmp where district_code= DC.FrmDist) FrmDistName,DC.FrmDist,DC.ToDist,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=DC.Commodity) ComdtyName,DC.Commodity,DC.Issued_Date,DC.Issued_Bags,DC.Issued_Qty,DC.Truck_No,DC.Bags_Type,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID =DC.Transporter_ID and Distt_ID=DC.FrmDist and Transport_ID in ('10','11','6','1') order by Transporter_Name desc) TransporterName,DC.Transporter_ID,DC.DC_No,DC.STO_No,DC.TO_No,DC.SMO,DC.MoveOrdernum,Dc.ModeofDispatch,Dc.TOEndDate,DC.RecGodown,DC.RecBranch,DC.RecIC From DeliveryChallan_MO DC inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=DC.Bags_Type where DC.FrmDist='" + ddlChallanNo.SelectedValue.ToString() + "' and DC.ToDist='" + Dist_Id + "' and RecIC='" + IC_Id + "' and DC.ModeofDispatch='13' and DC.IsReceived='N' and DC.Issue_Center='0000' and DC.Default_Branch='0000' and DC.Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and DC.Page_No='" + ddlPageNo.SelectedItem.ToString() + "' and DC.DC_MO='" + ddlPageNo.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    txtTONo.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();
                    txtTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();

                    DateTime SendDate = DateTime.Parse(ds.Tables[0].Rows[0]["Issued_Date"].ToString());
                    txtTranspDate.Text = SendDate.ToString("dd/MMM/yyyy");

                    txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    txtSendQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    txtSendBags.Text = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();

                    txtBagsType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();
                    hdfBagsType.Value=ds.Tables[0].Rows[0]["Bags_Type"].ToString();

                    txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtSendIC.Text = ds.Tables[0].Rows[0]["RecICName"].ToString();

                    hdfSMO.Value = ds.Tables[0].Rows[0]["SMO"].ToString();
                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
                    hdfDC_No.Value = ds.Tables[0].Rows[0]["DC_No"].ToString();
                    hdfTransporter_ID.Value = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
                    hdfFrmDist.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    hdfToDist.Value = ds.Tables[0].Rows[0]["ToDist"].ToString();
                    hdfCommodity.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();

                    
                    if (hdfCommodity.Value == "25")
                    {
                        lblQtls.Text = lblQtls0.Text = "(Bales)";
                        txtRecdBags.Text = "0";
                        txtRecdBags.Enabled = false;
                    }
                    else
                    {
                        lblQtls.Text = lblQtls0.Text = "(Qtls)";
                        txtRecdBags.Text = "";
                        txtRecdBags.Enabled = true;
                    }

                    hdfTranspDate.Value = ds.Tables[0].Rows[0]["Issued_Date"].ToString();
                    hdfTOEndDate.Value = ds.Tables[0].Rows[0]["TOEndDate"].ToString();
                    hdfModeofDispatch.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();

                    hdfRecIC.Value = ds.Tables[0].Rows[0]["RecIC"].ToString();
                    hdfRecBranch.Value = ds.Tables[0].Rows[0]["RecBranch"].ToString();
                    hdfRecGodown.Value = ds.Tables[0].Rows[0]["RecGodown"].ToString();

                    GetBGNameForRack();

                    if (hdfFrmDist.Value == hdfToDist.Value)
                    {
                        txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = "000";
                        trMsg.Visible = true;
                        chkChange.Checked = false;
                        trBG.Visible = trBG1.Visible = false;

                        trGridBG.Visible = trGridQty.Visible = false;
                        ddlIssueCentre.SelectedIndex = 0;
                        ddlBranch.Items.Clear();
                        ddlGodown.Items.Clear();

                        ViewState["hdfRecLoginBranch"] = hdfRecBranch.Value;
                        ViewState["hdfRecLoginGodown"] = hdfRecGodown.Value;
                        MaxCapInLoginGodown();
                    }
                    else
                    {
                        string select1 = "Select Branch,Godown,RequiredQuantity,RemQty,SMO,RMO From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + hdfSMO.Value + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and CropYear='" + txtCropYear.Text + "' and ModeofDispatch='13' and Issue_Center='" + IC_Id + "' and Branch='" + hdfRecBranch.Value + "' and Godown='" + hdfRecGodown.Value + "' and ModeofDist In('Both','Other')";
                        da1 = new SqlDataAdapter(select1, con);

                        ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            trGridBG.Visible = trGridQty.Visible = true;
                            ddlIssueCentre.SelectedIndex = 0;
                            ddlBranch.Items.Clear();
                            ddlGodown.Items.Clear();

                            trMsg.Visible = false;
                            chkChange.Checked = false;
                            trBG.Visible = trBG1.Visible = false;

                            GridView1.DataSource = ds1.Tables[0];
                            GridView1.DataBind();
                        }
                        else
                        {
                            txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = "000";
                            trMsg.Visible = true;
                            chkChange.Checked = false;
                            trBG.Visible = trBG1.Visible = false;

                            trGridBG.Visible = trGridQty.Visible = false;
                            ddlIssueCentre.SelectedIndex = 0;
                            ddlBranch.Items.Clear();
                            ddlGodown.Items.Clear();

                            ViewState["hdfRecLoginBranch"] = hdfRecBranch.Value;
                            ViewState["hdfRecLoginGodown"] = hdfRecGodown.Value;
                            MaxCapInLoginGodown();
                        }
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Available'); </script> ");
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

    public void GetBGNameForRack()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();

                string select1 = "select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + hdfRecBranch.Value + "') BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + hdfRecGodown.Value + "') Godown_Name";

                da1 = new SqlDataAdapter(select1, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    txtSendBranch.Text = ds1.Tables[0].Rows[0]["BranchName"].ToString();
                    txtSendGodown.Text = ds1.Tables[0].Rows[0]["Godown_Name"].ToString();
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

    public void GetChallanData()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select BT.BagType  as BagsTypeNew, RecGodown,RecBranch,(select DepotName from tbl_MetaData_DEPOT where DepotID=DCMO.RecIC) RecICName,RecIC,Truck_No,Bags_Type,Issued_Bags,Issued_Date,Issued_Qty,Change_Godown,Change_Branch,Default_Godown,Default_Branch,Issue_Center,ModeofDispatch,TOEndDate,CropYear,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=DCMO.Commodity) ComdtyName,Commodity,ToDist,MoveOrdernum,SMO,TO_No,STO_No,DC_No,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID =DCMO.Transporter_ID and Distt_ID=DCMO.FrmDist and Transport_ID in ('10','11','6') order by Transporter_Name desc) TransporterName,Transporter_ID,(select district_name From pds.districtsmp where district_code= DCMO.FrmDist) FrmDistName,FrmDist From DeliveryChallan_MO DCMO inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=DCMO.Bags_Type where DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "' and ToDist='" + Dist_Id + "' and ModeofDispatch='12' and (RecIC='" + IC_Id + "' or RecIC is null) and IsReceived='N' and CreatedDate='" + ddlChallanNo.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    txtTONo.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();
                    txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtSendQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    txtSendBags.Text = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();

                    txtBagsType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();
                    hdfBagsType.Value = ds.Tables[0].Rows[0]["Bags_Type"].ToString();

                    txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtSendIC.Text = ds.Tables[0].Rows[0]["RecICName"].ToString();
                    txtTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();

                    DateTime SendDate = DateTime.Parse(ds.Tables[0].Rows[0]["Issued_Date"].ToString());
                    txtTranspDate.Text = SendDate.ToString("dd/MMM/yyyy");
                    ViewState["hdfTranspDate"] = ds.Tables[0].Rows[0]["Issued_Date"].ToString();

                    ViewState["hdfSMO"] = ds.Tables[0].Rows[0]["SMO"].ToString();
                    ViewState["hdfSTO_No"] = ds.Tables[0].Rows[0]["STO_No"].ToString();
                    ViewState["hdfDC_No"] = ds.Tables[0].Rows[0]["DC_No"].ToString();
                    ViewState["hdfTransporter_ID"] = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
                    ViewState["hdfFrmDist"] = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    ViewState["hdfToDist"] = ds.Tables[0].Rows[0]["ToDist"].ToString();
                    ViewState["hdfCommodity"] = ds.Tables[0].Rows[0]["Commodity"].ToString();
                    ViewState["hdfTOEndDate"] = ds.Tables[0].Rows[0]["TOEndDate"].ToString();
                    ViewState["hdfModeofDispatch"] = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
                    ViewState["hdfSendIssue_Center"] = ds.Tables[0].Rows[0]["Issue_Center"].ToString();

                    ViewState["hdfSendDefault_Branch"] = ds.Tables[0].Rows[0]["Default_Branch"].ToString();
                    ViewState["hdfSendDefault_Godown"] = ds.Tables[0].Rows[0]["Default_Godown"].ToString();
                    ViewState["hdfSendChange_Branch"] = ds.Tables[0].Rows[0]["Change_Branch"].ToString();
                    ViewState["hdfSendChange_Godown"] = ds.Tables[0].Rows[0]["Change_Godown"].ToString();

                    ViewState["hdfRecIC"] = ds.Tables[0].Rows[0]["RecIC"].ToString();
                    ViewState["hdfRecBranch"] = ds.Tables[0].Rows[0]["RecBranch"].ToString();
                    ViewState["hdfRecGodown"] = ds.Tables[0].Rows[0]["RecGodown"].ToString();

                    GetBGName();

                    if (ViewState["hdfModeofDispatch"].ToString() == "12") //Dispatch By Road
                    {
                        if (ViewState["hdfRecGodown"].ToString() == "" || ViewState["hdfRecBranch"].ToString() == "")
                        {
                            string select1 = "Select Branch,Godown,RequiredQuantity,RemQty,SMO,RMO From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and FrmDist='" + ViewState["hdfFrmDist"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString() + "' and ModeofDispatch='12' and Issue_Center='" + IC_Id + "' and CropYear='" + txtCropYear.Text + "'";
                            da1 = new SqlDataAdapter(select1, con);

                            ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                GridView1.DataSource = ds1.Tables[0];
                                GridView1.DataBind();
                            }
                        }
                        else
                        {
                            string select1 = "Select Branch,Godown,RequiredQuantity,RemQty,SMO,RMO From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and FrmDist='" + ViewState["hdfFrmDist"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString() + "' and ModeofDispatch='12' and Issue_Center='" + IC_Id + "' and Branch='" + ViewState["hdfRecBranch"].ToString() + "' and Godown='" + ViewState["hdfRecGodown"].ToString() + "' and CropYear='" + txtCropYear.Text + "'";
                            da1 = new SqlDataAdapter(select1, con);

                            ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                trGridBG.Visible = trGridQty.Visible = true;
                                ddlIssueCentre.SelectedIndex = 0;
                                ddlBranch.Items.Clear();
                                ddlGodown.Items.Clear();

                                trMsg.Visible = false;
                                chkChange.Checked = false;
                                trBG.Visible = trBG1.Visible = false;

                                GridView1.DataSource = ds1.Tables[0];
                                GridView1.DataBind();
                            }
                            else
                            {
                                txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = "000";
                                trMsg.Visible = true;
                                chkChange.Checked = false;
                                trBG.Visible = trBG1.Visible = false;

                                trGridBG.Visible = trGridQty.Visible = false;
                                ddlIssueCentre.SelectedIndex = 0;
                                ddlBranch.Items.Clear();
                                ddlGodown.Items.Clear();

                                ViewState["hdfRecLoginBranch"] = ViewState["hdfRecBranch"].ToString();
                                ViewState["hdfRecLoginGodown"] = ViewState["hdfRecGodown"].ToString();
                                MaxCapInLoginGodown();
                            }
                        }
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अन्य जानकारी उपलब्ध नहीं है|'); </script> ");
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

                string select1 = "select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + ViewState["hdfRecBranch"].ToString() + "') BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + ViewState["hdfRecGodown"].ToString() + "') Godown_Name";

                da1 = new SqlDataAdapter(select1, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    txtSendBranch.Text = ds1.Tables[0].Rows[0]["BranchName"].ToString();
                    txtSendGodown.Text = ds1.Tables[0].Rows[0]["Godown_Name"].ToString();
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
            QtyRemTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string BranchName = "", GodownName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            BranchName = e.Row.Cells[1].Text;
            GodownName = e.Row.Cells[2].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + BranchName + "') BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') Godown_Name");
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            e.Row.Cells[1].Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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
                    if (con_MPStorage.State != ConnectionState.Closed)
                    {
                        con_MPStorage.Close();
                    }
                }
            }

            QtyTotal += (double.Parse(e.Row.Cells[3].Text));
            QtyRemTotal += (double.Parse(e.Row.Cells[4].Text));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "महायोग (Qtls)";
            e.Row.Cells[3].Text = QtyTotal.ToString("0.00");
            e.Row.Cells[4].Text = QtyRemTotal.ToString("0.00");
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["hdfRecLoginBranch"] = ViewState["hdfRecLoginGodown"] = ViewState["hdfRecRMO"] = "";
        txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = txtIssuedDate.Text = txtRecdQty.Text = txtRecdBags.Text = txtTCNo.Text = "";

        if (hdfCommodity.Value == "25")
        {
            txtRecdBags.Text = "0";
        }

        txtBranch.Text = GridView1.SelectedRow.Cells[1].Text;
        txtGodown.Text = GridView1.SelectedRow.Cells[2].Text;
        txtQtyTotal.Text = GridView1.SelectedRow.Cells[3].Text;
        txtQtyRemTotal.Text = GridView1.SelectedRow.Cells[4].Text;

        ViewState["hdfRecLoginBranch"] = GridView1.SelectedRow.Cells[5].Text;
        ViewState["hdfRecLoginGodown"] = GridView1.SelectedRow.Cells[6].Text;
        ViewState["hdfRecSMO"] = GridView1.SelectedRow.Cells[7].Text;
        ViewState["hdfRecRMO"] = GridView1.SelectedRow.Cells[8].Text;

        trMsg.Visible = true;
        chkChange.Checked = false;
        trBG.Visible = trBG1.Visible = false;
        ddlstackNumber.Items.Clear();
        GetStackNumber();
        ddlIssueCentre.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        MaxCapInLoginGodown();
    }

    protected void chkChange_CheckedChanged(object sender, EventArgs e)
    {
        ddlIssueCentre.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (chkChange.Checked)
        {
            trBG.Visible = trBG1.Visible = true;
        }
        else
        {
            trBG.Visible = trBG1.Visible = false;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();

        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
        }
    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='{0}'", ddlBranch.SelectedValue.ToString());
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
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
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

    public void MaxCapInLoginGodown()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT Godown_Capacity FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + Dist_Id + "' and DepotId='" + IC_Id + "' and Godown_ID='" + ViewState["hdfRecLoginGodown"].ToString() + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["hdfMaxCapInLoginGodown"] = (System.Math.Round(CheckNull(ds.Tables[0].Rows[0]["Godown_Capacity"].ToString()), 5)).ToString();
                }
                else
                {
                    ViewState["hdfMaxCapInLoginGodown"] = "0";
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

    public void MaxCapInChangeGodown()
    {
        IC_Id = ddlIssueCentre.SelectedValue.ToString();
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "SELECT Godown_Capacity FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + Dist_Id + "' and DepotId='" + IC_Id + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["hdfMaxCapInChangeGodown"] = (System.Math.Round(CheckNull(ds.Tables[0].Rows[0]["Godown_Capacity"].ToString()), 5)).ToString();
                }
                else
                {
                    ViewState["hdfMaxCapInChangeGodown"] = "0";
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
        if (ddlGodown.SelectedIndex > 0)
        {
            ddlstackNumber.Items.Clear();
            GetStackNumber();
            MaxCapInChangeGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें |'); </script> ");
        }
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)

    {
        string tags = "", BagType = "", gatepass = "", Rec_Jute_bags = "", Rec_OU_bags = "", Rec_PP_bags = "";

                      
        if (ddlstackNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Stack Number'); </script> ");
            return;
        }
        string ReceivedRecptID = "";
        if (hdfBagsType.Value == "1")
        {

            Rec_Jute_bags = "0";
            Rec_OU_bags = "0";
            Rec_PP_bags = txtRecdBags.Text;
        }

        else if
            (hdfBagsType.Value == "2")
        {

            Rec_Jute_bags = txtRecdBags.Text;
            Rec_OU_bags = "0";
            Rec_PP_bags = "0";
        }
        else if
           (hdfBagsType.Value == "3")
        {
            Rec_Jute_bags = "0";
            Rec_OU_bags = txtRecdBags.Text;
            Rec_PP_bags = "0";
        }

        if (txtCommodity.Text != "" && txtFrmDist.Text != "")
        {
            SendQty = 0; BalQty = 0; RecdQty = 0; VariationQty = ""; VariationBags = 0;

            SendQty = double.Parse(txtSendQty.Text);
            BalQty = double.Parse(txtQtyRemTotal.Text);
            RecdQty = double.Parse(txtRecdQty.Text);


            if (chkChange.Checked)
            {
                if (ddlGodown.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें |'); </script> ");
                    return;
                }
                else
                {
                    strRecChangeBranchval = ddlBranch.SelectedValue.ToString();
                    strRecChangeGodownVal = ddlGodown.SelectedValue.ToString();
                }
            }
            else
            {
                strRecChangeBranchval = "00";
                strRecChangeGodownVal = "00";
            }

            if (ViewState["hdfSendChange_Branch"].ToString() == "00" || ViewState["hdfSendChange_Godown"].ToString() == "00")
            {
                strSendChange_Branch = ViewState["hdfSendDefault_Branch"].ToString();
                strSendChange_Godown = ViewState["hdfSendDefault_Godown"].ToString();
            }
            else
            {
                strSendChange_Branch = ViewState["hdfSendChange_Branch"].ToString();
                strSendChange_Godown = ViewState["hdfSendChange_Godown"].ToString();
            }

            //if (BalQty < RecdQty)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम की बची हुई मात्रा " + txtQtyRemTotal.Text + " Qtls (मूवमेंट प्लान के अनुसार) ही कमोडिटी प्राप्त करें|'); </script> ");
            //    return;
            //}

            //if (SendQty < RecdQty)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया " + txtFrmDist.Text + " से भेजी गयी मात्रा " + txtSendQty.Text + " (Qtls) के अनुसार ही कमोडिटी प्राप्त करें|'); </script> ");
            //    return;
            //}
            //else
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);

                            if (chkChange.Checked)
                            {
                                IC_Id = ddlIssueCentre.SelectedValue.ToString();
                            }
                            else
                            {
                                IC_Id = Session["issue_id"].ToString();
                            }
                            Dist_Id = Session["dist_id"].ToString();
                            string opid = Session["OperatorId"].ToString();

                            VariationQty = (decimal.Parse(txtSendQty.Text) - decimal.Parse(txtRecdQty.Text)).ToString();
                            VariationBags = ((double.Parse(txtSendBags.Text)) - (double.Parse(txtRecdBags.Text)));

                            con.Open();

                            string instr = "";

                            if (ViewState["hdfModeofDispatch"].ToString() == "12") //Received By Road
                            {
                                month = int.Parse(DateTime.Today.Date.Month.ToString());
                                year = int.Parse(DateTime.Today.Year.ToString());

                                ReceiptID = "";
                                getnum = 0;
                                String SelectReceiptID = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + IC_Id + "' and Dist_Id='" + Dist_Id + "'";
                                da1 = new SqlDataAdapter(SelectReceiptID, con);
                                ds1 = new DataSet();
                                da1.Fill(ds1);
                                ReceiptID = ds1.Tables[0].Rows[0]["Receipt_id"].ToString();
                                if (ReceiptID == "")
                                {
                                    string issue = IC_Id.Substring(2, 5);
                                    ReceiptID = issue + month.ToString() + "001";
                                }
                                else
                                {
                                    getnum = Convert.ToInt64(ReceiptID);
                                    getnum = getnum + 1;
                                    ReceiptID = getnum.ToString();
                                }

                                string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());

                                string checkchallan = "Select challan_no from dbo.tbl_Receipt_Details where Dist_Id='" + Dist_Id + "' and  Depot_ID='" + IC_Id + "' and challan_no='" + ddlChallanNo.SelectedItem.ToString() + "'";
                                da1 = new SqlDataAdapter(checkchallan, con);
                                ds1 = new DataSet();
                                da1.Fill(ds1);
                                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan No Already Exist...'); </script> ");
                                    return;
                                }
                                else
                                {
                                    ReceivedRecptID = ddlChallanNo.SelectedItem.ToString() + "R";

                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                  "Update StateMovementOrder set SubmitedQty = (" + RecdQty + " + (ISNULL(SubmitedQty,0))) where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "';";

                                    if (txtBranch.Text != "000" && txtQtyTotal.Text != "000")
                                    {
                                        instr += "Update RecAgainst_StateMovementOrder set SubmitedQty = (" + RecdQty + " + (ISNULL(SubmitedQty,0))), RemQty=RemQty-" + RecdQty + " where MoveOrdernum='" + txtMONo.Text + "' and RMO='" + ViewState["hdfRecRMO"].ToString() + "' ;";
                                    }

                                    //instr += "Update DeliveryChallan_MO Set IsReceived='Y',Recd_Qty='" + RecdQty + "',Recd_Bags='" + txtRecdBags.Text + "',Recd_Date='" + IssuedDate + "'  Where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + txtTONo.Text + "' and STO_No='" + ViewState["hdfSTO_No"].ToString() + "' and DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "' and FrmDist='" + ViewState["hdfFrmDist"].ToString() + "' and Commodity='" + ViewState["hdfCommodity"].ToString() + "' and CropYear='" + txtCropYear.Text + "' and Truck_No='" + txtTruckNo.Text + "' and Issue_Center='" + ViewState["hdfSendIssue_Center"].ToString() + "' and Default_Branch='" + ViewState["hdfSendDefault_Branch"].ToString() + "' and Default_Godown='" + ViewState["hdfSendDefault_Godown"].ToString() + "' and Change_Branch='" + ViewState["hdfSendChange_Branch"].ToString() + "' and Change_Godown='" + ViewState["hdfSendChange_Godown"] + "' and RecIC='" + ViewState["hdfRecIC"].ToString() + "' and RecBranch='" + ViewState["hdfRecBranch"].ToString() + "' and RecGodown='" + ViewState["hdfRecGodown"].ToString() + "' and CreatedDate='" + ddlChallanNo.SelectedValue.ToString() + "';";

                                    instr += "Update DeliveryChallan_MO Set IsReceived='Y',Recd_Qty='" + RecdQty + "',Recd_Bags='" + txtRecdBags.Text + "',Recd_Date='" + IssuedDate + "'  Where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + txtTONo.Text + "' and STO_No='" + ViewState["hdfSTO_No"].ToString() + "' and DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "' and FrmDist='" + ViewState["hdfFrmDist"].ToString() + "' and Commodity='" + ViewState["hdfCommodity"].ToString() + "' and CropYear='" + txtCropYear.Text + "' and Truck_No='" + txtTruckNo.Text + "' and CreatedDate='" + ddlChallanNo.SelectedValue.ToString() + "';";

                                    instr += "Update SCSC_Truck_challan Set IsDeposit='Y' where Dist_ID='" + ViewState["hdfFrmDist"].ToString() + "' and Challan_No='" + ddlChallanNo.SelectedItem.ToString() + "';";

                                    instr += "Insert Into ReceiptEntry_MO(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,DC_RecIC,DC_RecBranch,DC_RecGodown,ReceiptID,Variation_qty,Variation_Bags,ToulReceiptNo, StackNumber, StackName) values('" + txtMONo.Text + "','" + ViewState["hdfSMO"].ToString() + "','" + txtTONo.Text + "','" + ViewState["hdfSTO_No"].ToString() + "','" + ViewState["hdfDC_No"].ToString() + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + ViewState["hdfFrmDist"].ToString() + "','" + Dist_Id + "','" + ViewState["hdfCommodity"].ToString() + "','" + txtCropYear.Text + "','" + ViewState["hdfTOEndDate"].ToString() + "','" + ViewState["hdfModeofDispatch"].ToString() + "','" + ddlChallanNo.SelectedItem.ToString() + "','" + ViewState["hdfSendIssue_Center"].ToString() + "','" + strSendChange_Branch + "','" + strSendChange_Godown + "','" + SendQty + "','" + txtSendBags.Text + "','" + ViewState["hdfTranspDate"].ToString() + "','" + hdfBagsType.Value + "','" + txtTruckNo.Text + "','" + IC_Id + "','" + ViewState["hdfRecLoginBranch"].ToString() + "','" + ViewState["hdfRecLoginGodown"].ToString() + "','" + strRecChangeBranchval + "','" + strRecChangeGodownVal + "','" + RecdQty + "','" + txtRecdBags.Text + "','" + IssuedDate + "','" + txtTCNo.Text + "','" + ddlSArrival.SelectedValue.ToString() + "',GETDATE(),'" + GetIp + "','" + opid + "','" + ViewState["hdfRecIC"].ToString() + "','" + ViewState["hdfRecBranch"].ToString() + "','" + ViewState["hdfRecGodown"].ToString() + "','" + ReceivedRecptID + "','" + VariationQty + "','" + VariationBags + "','" + txtToulReceiptNo.Text + "','"+ddlstackNumber.SelectedValue.ToString()+"','"+ddlstackNumber.SelectedItem.Text+"');";
                                    
                                    if (chkChange.Checked)
                                    {
                                        instr += "insert into dbo.tbl_Receipt_Details(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch,  typeofbags, Rec_PP_bags, Rec_Jute_bags, Rec_OU_bags, Sent_Jute_bags, Sent_OU_bags, Sent_PP_bags, whr, StackNumber, stackName)values('23','" + Dist_Id + "','" + IC_Id + "','" + ReceiptID + "','" + ddlSArrival.SelectedValue.ToString() + "','','" + ViewState["hdfFrmDist"].ToString() + "','" + ViewState["hdfSendIssue_Center"].ToString() + "','','','" + ViewState["hdfTranspDate"].ToString() + "','" + IssuedDate + "','" + ddlChallanNo.SelectedItem.ToString() + "','" + IssuedDate + "'," + SendQty + ",'" + ViewState["hdfCommodity"].ToString() + "','0','" + txtCropYear.Text + "','1','" + ViewState["hdfTransporter_ID"].ToString() + "','" + txtTruckNo.Text + "','" + time + "','Not Indicated'," + txtSendBags.Text + "," + RecdQty + "," + txtRecdBags.Text + ",'0',''," + VariationQty + "," + month + "," + year + ",'N','" + GetIp + "',getdate(),'','I','" + ddlGodown.SelectedValue.ToString() + "','" + opid + "','N','','" + ddlBranch.SelectedValue.ToString() + "','" + hdfBagsType.Value + "','" + Rec_PP_bags + "','" + Rec_Jute_bags + "','" + Rec_OU_bags + "','0','0','0','0','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";
                                    }
                                    else
                                    {
                                        instr += "insert into dbo.tbl_Receipt_Details(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch,  typeofbags, Rec_PP_bags, Rec_Jute_bags, Rec_OU_bags, Sent_Jute_bags, Sent_OU_bags, Sent_PP_bags, whr, StackNumber, stackName)values('23','" + Dist_Id + "','" + IC_Id + "','" + ReceiptID + "','" + ddlSArrival.SelectedValue.ToString() + "','','" + ViewState["hdfFrmDist"].ToString() + "','" + ViewState["hdfSendIssue_Center"].ToString() + "','','','" + ViewState["hdfTranspDate"].ToString() + "','" + IssuedDate + "','" + ddlChallanNo.SelectedItem.ToString() + "','" + IssuedDate + "'," + SendQty + ",'" + ViewState["hdfCommodity"].ToString() + "','0','" + txtCropYear.Text + "','1','" + ViewState["hdfTransporter_ID"].ToString() + "','" + txtTruckNo.Text + "','" + time + "','Not Indicated'," + txtSendBags.Text + "," + RecdQty + "," + txtRecdBags.Text + ",'0',''," + VariationQty + "," + month + "," + year + ",'N','" + GetIp + "',getdate(),'','I','" + ViewState["hdfRecLoginGodown"].ToString() + "','" + opid + "','N','','" + ViewState["hdfRecLoginBranch"].ToString() + "','" + hdfBagsType.Value + "','" + Rec_PP_bags + "','" + Rec_Jute_bags + "','" + Rec_OU_bags + "','0','0','0','0','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";
                                    }

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                            }
                            else if (hdfModeofDispatch.Value == "13") //Received By Rack
                            {
                                month = int.Parse(DateTime.Today.Date.Month.ToString());
                                year = int.Parse(DateTime.Today.Year.ToString());

                                string checkchallan = "Select TC_Number from dbo.RR_receipt_Depot where district_code='" + Dist_Id + "' and  DepotID='" + IC_Id + "' and TC_Number='" + ddlPageNo.SelectedValue.ToString() + "'";
                                da1 = new SqlDataAdapter(checkchallan, con);
                                ds1 = new DataSet();
                                da1.Fill(ds1);
                                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan No Already Exist...'); </script> ");
                                    return;
                                }
                                else
                                {
                                    ReceiptID = "";
                                    getnum = 0;
                                    String SelectReceiptID = "select max(Trans_ID) as Receipt_id from dbo.RR_receipt_Depot where district_code='" + Dist_Id + "' and Month ='" + month + "' and Year='" + year + "'";
                                    da1 = new SqlDataAdapter(SelectReceiptID, con);
                                    ds1 = new DataSet();
                                    da1.Fill(ds1);

                                    string mmonth = month.ToString();
                                    ReceiptID = ds1.Tables[0].Rows[0]["Receipt_id"].ToString();

                                    if (ReceiptID == "")
                                    {
                                        ReceiptID = Dist_Id + mmonth + "001";
                                    }
                                    else
                                    {
                                        getnum = Convert.ToInt32(ReceiptID);
                                        getnum = getnum + 1;
                                        ReceiptID = getnum.ToString();
                                    }

                                    string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());

                                    if (hdfCommodity.Value == "25")
                                    {
                                        ReceivedRecptID = ddlPageNo.SelectedValue.ToString() + "R";
                                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                                        if ((hdfFrmDist.Value != hdfToDist.Value))
                                        {
                                            instr += "Update RecAgainst_StateMovementOrder set SubmitedQty = (" + RecdQty + " + (ISNULL(SubmitedQty,0))), RemQty=RemQty-" + RecdQty + " where MoveOrdernum='" + txtMONo.Text + "' and RMO='" + ViewState["hdfRecRMO"].ToString() + "' ;";
                                        }

                                        instr += "Update DeliveryChallan_MO Set IsReceived='Y',Recd_Qty='" + RecdQty + "',Recd_Bags='" + txtRecdBags.Text + "',Recd_Date='" + IssuedDate + "' Where MoveOrdernum='" + txtMONo.Text + "' and TO_No='" + txtTONo.Text + "' and DC_MO='" + ddlPageNo.SelectedValue.ToString() + "' and CropYear='" + txtCropYear.Text + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and Page_No='" + ddlPageNo.SelectedItem.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' ;";

                                        instr += "Insert Into ReceiptEntry_MO(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,DC_RecIC,DC_RecBranch,DC_RecGodown,ReceiptID,Variation_qty,Variation_Bags,ToulReceiptNo, StackNumber, StackName) values('" + txtMONo.Text + "','" + hdfSMO.Value + "','" + txtTONo.Text + "','" + hdfSTO_No.Value + "','" + hdfDC_No.Value + "','" + hdfTransporter_ID.Value + "','" + hdfFrmDist.Value + "','" + Dist_Id + "','" + hdfCommodity.Value + "','" + txtCropYear.Text + "','" + hdfTOEndDate.Value + "','13','" + ddlPageNo.SelectedValue.ToString() + "','0000','0000','0000','" + SendQty + "','" + txtSendBags.Text + "','" + hdfTranspDate.Value + "','" + hdfBagsType.Value + "','" + txtTruckNo.Text + "','" + IC_Id + "','" + ViewState["hdfRecLoginBranch"].ToString() + "','" + ViewState["hdfRecLoginGodown"].ToString() + "','" + strRecChangeBranchval + "','" + strRecChangeGodownVal + "','" + RecdQty + "','" + txtRecdBags.Text + "','" + IssuedDate + "','" + txtTCNo.Text + "','" + ddlSArrival.SelectedValue.ToString() + "',GETDATE(),'" + GetIp + "','" + opid + "','" + hdfRecIC.Value + "','" + hdfRecBranch.Value + "','" + hdfRecGodown.Value + "','" + ReceivedRecptID + "','" + VariationQty + "','" + VariationBags + "','" + txtToulReceiptNo.Text + "','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";

                                        if (chkChange.Checked)
                                        {
                                            instr += "Insert into  dbo.RR_receipt_Depot(State_Id,district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_Date,Updated_Date,Ip_Address,Challan_st,Godown,Commodity,Scheme,OperatorID,NoTransaction,Cropyear,BagsType)values('23','" + Dist_Id + "','" + IC_Id + "','0000','0000','" + ddlPageNo.SelectedValue.ToString() + "','" + hdfTranspDate.Value + "','" + hdfTransporter_ID.Value + "','" + txtTCNo.Text + "','" + ReceiptID + "'," + txtSendBags.Text.Trim() + "," + SendQty + "," + txtRecdBags.Text + "," + RecdQty + "," + month + "," + year + ",getdate(),'','" + GetIp + "','I','" + ddlGodown.SelectedValue.ToString() + "','" + hdfCommodity.Value + "','0','" + opid + "','N','" + txtCropYear.Text + "','"+hdfBagsType.Value+"');";
                                        }
                                        else
                                        {
                                            instr += "Insert into  dbo.RR_receipt_Depot(State_Id,district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_Date,Updated_Date,Ip_Address,Challan_st,Godown,Commodity,Scheme,OperatorID,NoTransaction,Cropyear, BagsType)values('23','" + Dist_Id + "','" + IC_Id + "','0000','0000','" + ddlPageNo.SelectedValue.ToString() + "','" + hdfTranspDate.Value + "','" + hdfTransporter_ID.Value + "','" + txtTCNo.Text + "','" + ReceiptID + "'," + txtSendBags.Text.Trim() + "," + SendQty + "," + txtRecdBags.Text + "," + RecdQty + "," + month + "," + year + ",getdate(),'','" + GetIp + "','I','" + ViewState["hdfRecLoginGodown"].ToString() + "','" + hdfCommodity.Value + "','0','" + opid + "','N','" + txtCropYear.Text + "','" + hdfBagsType.Value + "');";
                                        }

                                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                    }
                                    else
                                    {
                                        ReceivedRecptID = ddlPageNo.SelectedValue.ToString() + "R";
                                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                                        if ((txtBranch.Text != "000" && txtQtyTotal.Text != "000") && (hdfFrmDist.Value != hdfToDist.Value))
                                        {
                                            instr += "Update RecAgainst_StateMovementOrder set SubmitedQty = (" + RecdQty + " + (ISNULL(SubmitedQty,0))), RemQty=RemQty-" + RecdQty + " where MoveOrdernum='" + txtMONo.Text + "' and RMO='" + ViewState["hdfRecRMO"].ToString() + "';";
                                        }

                                        instr += "Update DeliveryChallan_MO Set IsReceived='Y',Recd_Qty='" + RecdQty + "',Recd_Bags='" + txtRecdBags.Text + "',Recd_Date='" + IssuedDate + "' Where MoveOrdernum='" + txtMONo.Text + "' and TO_No='" + txtTONo.Text + "' and DC_MO='" + ddlPageNo.SelectedValue.ToString() + "' and CropYear='" + txtCropYear.Text + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and Page_No='" + ddlPageNo.SelectedItem.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' ;";

                                        instr += "Insert Into ReceiptEntry_MO(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,DC_MO,SendingIssue_Center,SendingBranch,SendingGodown,SendingQty,SendingBags,SendingDate,SendingBags_Type,SendingTruck_No,RecIssue_Center,RecDefault_Branch,RecDefault_Godown,RecChange_Branch,RecChange_Godown,RecQty,RecdBags,RecDate,RecTruck_No,RecSArrival,CreatedDate,IP,OperatorID,DC_RecIC,DC_RecBranch,DC_RecGodown,ReceiptID,Variation_qty,Variation_Bags,ToulReceiptNo, StackNumber, StackName) values('" + txtMONo.Text + "','" + hdfSMO.Value + "','" + txtTONo.Text + "','" + hdfSTO_No.Value + "','" + hdfDC_No.Value + "','" + hdfTransporter_ID.Value + "','" + hdfFrmDist.Value + "','" + Dist_Id + "','" + hdfCommodity.Value + "','" + txtCropYear.Text + "','" + hdfTOEndDate.Value + "','13','" + ddlPageNo.SelectedValue.ToString() + "','0000','0000','0000','" + SendQty + "','" + txtSendBags.Text + "','" + hdfTranspDate.Value + "','" + hdfBagsType.Value + "','" + txtTruckNo.Text + "','" + IC_Id + "','" + ViewState["hdfRecLoginBranch"].ToString() + "','" + ViewState["hdfRecLoginGodown"].ToString() + "','" + strRecChangeBranchval + "','" + strRecChangeGodownVal + "','" + RecdQty + "','" + txtRecdBags.Text + "','" + IssuedDate + "','" + txtTCNo.Text + "','" + ddlSArrival.SelectedValue.ToString() + "',GETDATE(),'" + GetIp + "','" + opid + "','" + hdfRecIC.Value + "','" + hdfRecBranch.Value + "','" + hdfRecGodown.Value + "','" + ReceivedRecptID + "','" + VariationQty + "','" + VariationBags + "','" + txtToulReceiptNo.Text + "','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";

                                        if (chkChange.Checked)
                                        {
                                            instr += "Insert into  dbo.RR_receipt_Depot(State_Id,district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_Date,Updated_Date,Ip_Address,Challan_st,Godown,Commodity,Scheme,OperatorID,NoTransaction,Cropyear,BagsType)values('23','" + Dist_Id + "','" + IC_Id + "','0000','0000','" + ddlPageNo.SelectedValue.ToString() + "','" + hdfTranspDate.Value + "','" + hdfTransporter_ID.Value + "','" + txtTCNo.Text + "','" + ReceiptID + "'," + txtSendBags.Text.Trim() + "," + SendQty + "," + txtRecdBags.Text + "," + RecdQty + "," + month + "," + year + ",getdate(),'','" + GetIp + "','I','" + ddlGodown.SelectedValue.ToString() + "','" + hdfCommodity.Value + "','0','" + opid + "','N','" + txtCropYear.Text + "','" + hdfBagsType.Value + "');";
                                        }
                                        else
                                        {
                                            instr += "Insert into  dbo.RR_receipt_Depot(State_Id,district_code,DepotID,Rack_No,S_RailHead,TC_Number,TC_date,Transporter_ID,Truck_No,Trans_ID,Disp_Bags,Disp_Qty,Recd_Bags,Recd_Qty,Month,Year,Created_Date,Updated_Date,Ip_Address,Challan_st,Godown,Commodity,Scheme,OperatorID,NoTransaction,Cropyear, BagsType)values('23','" + Dist_Id + "','" + IC_Id + "','0000','0000','" + ddlPageNo.SelectedValue.ToString() + "','" + hdfTranspDate.Value + "','" + hdfTransporter_ID.Value + "','" + txtTCNo.Text + "','" + ReceiptID + "'," + txtSendBags.Text.Trim() + "," + SendQty + "," + txtRecdBags.Text + "," + RecdQty + "," + month + "," + year + ",getdate(),'','" + GetIp + "','I','" + ViewState["hdfRecLoginGodown"].ToString() + "','" + hdfCommodity.Value + "','0','" + opid + "','N','" + txtCropYear.Text + "','" + hdfBagsType.Value + "');";
                                        }

                                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                    }
                                }
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = false;
                                ddlSArrival.Enabled = ddlChallanNo.Enabled = false;
                                txtRecdQty.Enabled = txtRecdBags.Enabled = txtTCNo.Enabled = txtToulReceiptNo.Enabled = false;
                                btnPrint.Enabled = true;

                                Session["ReceiptID"] = ReceivedRecptID;

                                if (ddlChallanNo.SelectedIndex > 0)
                                {
                                    Session["ChallanNo"] = ddlChallanNo.SelectedItem.ToString();
                                }
                               
                                if (ddlPageNo.SelectedIndex > 0)
                                {
                                    Session["ChallanNo"] = ddlPageNo.SelectedValue.ToString();
                                }

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Receipt Id Is " + ReceivedRecptID + "'); </script> ");
                                txtCommodity.Text = txtFrmDist.Text = "";

                                GridView1.DataSource = "";
                                GridView1.DataBind();

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                //Code For tbl_Receipt_Details
                                if (ddlSArrival.SelectedItem.Text == "Other Depot")
                                {
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.tbl_Receipt_Details where Commodity='" + ViewState["hdfCommodity"].ToString() + "'and Scheme ='0' and Dist_Id='" + Dist_Id + "'and Depot_ID='" + IC_Id + "'and S_of_arrival='" + ddlSArrival.SelectedValue.ToString() + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ViewState["hdfCommodity"].ToString() + "' and Scheme_ID='0' and DistrictId ='" + Dist_Id + "'and DepotID='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance from dbo.issue_opening_balance where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + ViewState["hdfCommodity"].ToString() + "'and Scheme_Id ='0'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }
                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + Dist_Id + "','" + IC_Id + "','" + ViewState["hdfCommodity"].ToString() + "','0'," + sropen + "," + 0 + "," + RecdQty + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd = new SqlCommand(qryinsr, con);
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Recieved_Otherg=" + mrod + " where Commodity_Id ='" + ViewState["hdfCommodity"].ToString() + "' and Scheme_ID='0'and DistrictId='" + Dist_Id + "'and DepotID='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                            cmd = new SqlCommand(qryinsU, con);
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                                else if (ddlSArrival.SelectedItem.Text == "From RailHead")
                                {
                                    string qrystock = "select Sum(convert(decimal(18,5),Recd_Qty)) as Qty from dbo.RR_receipt_Depot where Commodity='" + hdfCommodity.Value + "'and Scheme ='0' and district_code='" + Dist_Id + "'and DepotID='" + IC_Id + "' and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dspro = mobj.selectAny(qrystock);
                                    if (dspro.Tables[0].Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        DataRow drop = dspro.Tables[0].Rows[0];
                                        decimal mrod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + hdfCommodity.Value + "' and Scheme_ID='0' and DistrictId ='" + Dist_Id + "'and DepotID='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);
                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {
                                            string chkopenss = "Select Sum(convert(decimal(18,5),Current_Balance)) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + hdfCommodity.Value + "'and Scheme_Id ='0'";
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsqry = mobj.selectAny(chkopenss);
                                            if (dsqry == null)
                                            {

                                            }
                                            else
                                            {
                                                DataRow drss = dsqry.Tables[0].Rows[0];
                                                decimal sropen = CheckNull(drss["Current_Balance"].ToString());
                                                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + Dist_Id + "','" + IC_Id + "','" + hdfCommodity.Value + "','0'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'" + RecdQty + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                cmd = new SqlCommand(qryinsr, con);
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Received_RailHead=" + mrod + " where Commodity_Id ='" + hdfCommodity.Value + "' and Scheme_ID='0'and DistrictId='" + Dist_Id + "'and DepotID='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                            cmd = new SqlCommand(qryinsU, con);
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }

                                # region 
                                /* //Code For issue_opening_balance
                                if (ddlSArrival.SelectedItem.Text == "Other Depot")
                                {
                                    string gdwn = "";
                                    RecdQty = double.Parse(txtRecdQty.Text);
                                    int RecdBags = int.Parse(txtRecdBags.Text);
                                    decimal MaxCapGodown = 0;

                                    if (chkChange.Checked)
                                    {
                                        gdwn = ddlGodown.SelectedValue.ToString();
                                        MaxCapGodown = decimal.Parse(ViewState["hdfMaxCapInChangeGodown"].ToString());
                                    }
                                    else
                                    {
                                        gdwn = ViewState["hdfRecLoginGodown"].ToString();
                                        MaxCapGodown = decimal.Parse(ViewState["hdfMaxCapInLoginGodown"].ToString());
                                    }

                                    string chkopenbal = "Select * from dbo.issue_opening_balance where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + ViewState["hdfCommodity"].ToString() + "'and Scheme_Id='0' and Godown='" + gdwn + "' and Source='" + ddlSArrival.SelectedValue.ToString() + "'";
                                    distobj = new DistributionCenters(ComObj);
                                    DataSet dsbal = distobj.selectAny(chkopenbal);
                                    if (dsbal == null)
                                    {

                                    }
                                    else
                                    {
                                        if (dsbal.Tables[0].Rows.Count == 0)
                                        {
                                            string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('23','" + Dist_Id + "','" + IC_Id + "','" + ViewState["hdfCommodity"].ToString() + "','0','','" + gdwn + "','" + txtCropYear.Text + "','0','0','" + ddlSArrival.SelectedValue.ToString() + "'," + RecdQty + "," + txtRecdBags.Text + "," + month + "," + year + ",'" + GetIp + "',getdate(),getdate(),'',''" + ")";
                                            cmd = new SqlCommand(qreyins, con);
                                            cmd.Connection = con;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        else
                                        {
                                            string query = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5),Current_Balance)+" + RecdQty + ",Current_Bags=Current_Bags+" + RecdBags + " where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + ViewState["hdfCommodity"].ToString() + "'and Scheme_Id='0'and Source='" + ddlSArrival.SelectedValue.ToString() + "' and Godown='" + gdwn + "'";
                                            cmd = new SqlCommand(query, con);
                                            cmd.Connection = con;

                                            try
                                            {
                                                if (con.State == ConnectionState.Closed)
                                                {
                                                    con.Open();
                                                }
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }
                                            catch (Exception ex)
                                            {
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                            }
                                            finally
                                            {
                                                con.Close();
                                            }
                                        }
                                    }

                                    decimal openqty = 0, openbag = 0;

                                    decimal gcapacity = MaxCapGodown;
                                    decimal gcurrcap = gcapacity - openqty;

                                    string select = "Select * from Current_Godown_Position where District_Id='" + Dist_Id + "' and Depotid='" + IC_Id + "' and Godown='" + gdwn + "'";
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dsgdn = mobj.selectAny(select);
                                    if (dsgdn.Tables[0].Rows.Count == 0)
                                    {
                                        string qreygdn = "insert into dbo.Current_Godown_Position(District_Id,Depotid,Godown,Current_Balance,Current_Bags,Month,Year,IP_Address,CreatedDate,UpdatedDate,DeletedDate,Godown_Capacity,Current_Capacity) values('" + Dist_Id + "','" + IC_Id + "','" + gdwn + "'," + openqty + "," + openbag + "," + month + "," + year + ",'" + GetIp + "',getdate(),'',''," + gcapacity + "," + gcurrcap + ")";
                                        cmd = new SqlCommand(qreygdn, con);
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        DataRow drgdn = dsgdn.Tables[0].Rows[0];
                                        string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=convert(decimal(18,5),Current_Capacity)-" + RecdQty + ",Current_Bags=Current_Bags+" + RecdBags + ",Current_Balance=Current_Balance+" + RecdQty + " where District_Id='" + Dist_Id + "' and Depotid='" + IC_Id + "' and Godown='" + gdwn + "'";
                                        cmd = new SqlCommand(qreygdnU, con);
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                } */
                                /* else if (ddlSArrival.SelectedItem.Text == "From RailHead")
                                 {
                                     string gdwn = "";
                                     RecdQty = double.Parse(txtRecdQty.Text);
                                     int RecdBags = int.Parse(txtRecdBags.Text);
                                     decimal MaxCapGodown = 0;

                                     if (chkChange.Checked)
                                     {
                                         gdwn = ddlGodown.SelectedValue.ToString();
                                         MaxCapGodown = decimal.Parse(ViewState["hdfMaxCapInChangeGodown"].ToString());
                                     }
                                     else
                                     {
                                         gdwn = ViewState["hdfRecLoginGodown"].ToString();
                                         MaxCapGodown = decimal.Parse(ViewState["hdfMaxCapInLoginGodown"].ToString());
                                     }

                                     string chkopenbal = "Select * from dbo.issue_opening_balance where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + hdfCommodity.Value + "'and Scheme_Id='0' and Godown='" + gdwn + "' and Source='" + ddlSArrival.SelectedValue.ToString() + "'";
                                     distobj = new DistributionCenters(ComObj);
                                     DataSet dsbal = distobj.selectAny(chkopenbal);
                                     if (dsbal == null)
                                     {

                                     }
                                     else
                                     {
                                         if (dsbal.Tables[0].Rows.Count == 0)
                                         {
                                             string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('23','" + Dist_Id + "','" + IC_Id + "','" + hdfCommodity.Value + "','0','','" + gdwn + "','" + txtCropYear.Text + "','0','0','" + ddlSArrival.SelectedValue.ToString() + "'," + RecdQty + "," + txtRecdBags.Text + "," + month + "," + year + ",'" + GetIp + "',getdate(),getdate(),'',''" + ")";
                                             cmd = new SqlCommand(qreyins, con);
                                             cmd.Connection = con;
                                             if (con.State == ConnectionState.Closed)
                                             {
                                                 con.Open();
                                             }
                                             cmd.ExecuteNonQuery();
                                             con.Close();

                                         }
                                         else
                                         {
                                             string query = "Update dbo.issue_opening_balance set Current_Balance=convert(decimal(18,5),Current_Balance)+" + RecdQty + ",Current_Bags=Current_Bags+" + RecdBags + " where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + hdfCommodity.Value + "'and Scheme_Id='0'and Source='" + ddlSArrival.SelectedValue.ToString() + "' and Godown='" + gdwn + "'";
                                             cmd = new SqlCommand(query, con);
                                             cmd.Connection = con;

                                             try
                                             {
                                                 if (con.State == ConnectionState.Closed)
                                                 {
                                                     con.Open();
                                                 }
                                                 cmd.ExecuteNonQuery();
                                                 con.Close();
                                             }
                                             catch (Exception ex)
                                             {
                                                 Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                                             }
                                             finally
                                             {
                                                 con.Close();
                                             }
                                         }
                                     }

                                     decimal openqty = 0, openbag = 0;

                                     decimal gcapacity = MaxCapGodown;
                                     decimal gcurrcap = gcapacity - openqty;

                                     string select = "Select * from Current_Godown_Position where District_Id='" + Dist_Id + "' and Depotid='" + IC_Id + "' and Godown='" + gdwn + "'";
                                     mobj = new MoveChallan(ComObj);
                                     DataSet dsgdn = mobj.selectAny(select);
                                     if (dsgdn.Tables[0].Rows.Count == 0)
                                     {
                                         string qreygdn = "insert into dbo.Current_Godown_Position(District_Id,Depotid,Godown,Current_Balance,Current_Bags,Month,Year,IP_Address,CreatedDate,UpdatedDate,DeletedDate,Godown_Capacity,Current_Capacity) values('" + Dist_Id + "','" + IC_Id + "','" + gdwn + "'," + openqty + "," + openbag + "," + month + "," + year + ",'" + GetIp + "',getdate(),'',''," + gcapacity + "," + gcurrcap + ")";
                                         cmd = new SqlCommand(qreygdn, con);
                                         if (con.State == ConnectionState.Closed)
                                         {
                                             con.Open();
                                         }
                                         cmd.ExecuteNonQuery();
                                         con.Close();
                                     }
                                     else
                                     {
                                         DataRow drgdn = dsgdn.Tables[0].Rows[0];
                                         string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=convert(decimal(18,5),Current_Capacity)-" + RecdQty + ",Current_Bags=Current_Bags+" + RecdBags + ",Current_Balance=Current_Balance+" + RecdQty + " where District_Id='" + Dist_Id + "' and Depotid='" + IC_Id + "' and Godown='" + gdwn + "'";
                                         cmd = new SqlCommand(qreygdnU, con);
                                         if (con.State == ConnectionState.Closed)
                                         {
                                             con.Open();
                                         }
                                         cmd.ExecuteNonQuery();
                                         con.Close();
                                     }
                                 } */
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            //btnRecptSubmit.Enabled = false;
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "PrintReceiptEntry_MO.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/MovementOrderHome.aspx");
    }

    public void GetStackNumber()
    {
        ddlstackNumber.Items.Clear();
        if (chkChange.Checked == false)
        {
            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select Stack_ID, Stack_Name  from tbl_MetaData_STACK where Stack_Killed='N' and Godown_ID='" + ViewState["hdfRecLoginGodown"].ToString() + "' order by Stack_Name");
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

        else if (chkChange.Checked == true)
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

    }


}