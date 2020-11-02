using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class District_Delete_PDSMO_DC : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    SqlCommand cmd;

    string districtid = "", ActualFrmGodown = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                rdbRoad.Checked = true;
                GetMPIssueCentre();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + Session["dist_id"].ToString() + "' order by DepotName");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlDC.Items.Clear();
        btnDelete.Enabled = false;
        hdfGatePass.Value = "";
        txtMONo.Text = txtRackNo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtFrmRailHead.Text = txtToRailHead.Text = txtIC.Text = txtGodown.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = "";
        hdfFrmDist.Value = hdfToDist.Value = hdfFrmRailHead.Value = hdfToRailHead.Value = hdfFrmIC.Value = hdfFrmGodown.Value = hdfChangeGodown.Value = hdfToIC.Value = hdfToGodown.Value = hdfIsRecd.Value = hdfSTO_No.Value = hdfSMO.Value = hdfIssued_Bags.Value = hdfCommodityVal.Value = hdfStock_Issued_From.Value = hdfIssued_Date.Value = "";

        if (ddlIssueCentre.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Centre'); </script> ");
            return;
        }
        else if (txtFromDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select From Date'); </script> ");
            return;
        }
        else if (txtToDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select To Date'); </script> ");
            return;
        }
        else
        {
            ddlDC.Items.Clear();
            GetDCNumber();
        }
    }

    public void GetDCNumber()
    {
        RequiredFieldValidator5.Visible = RequiredFieldValidator6.Visible = false;
        districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string fromdate = Request.Form[txtFromDate.UniqueID];
                txtFromDate.Text = fromdate;
                string todate = Request.Form[txtToDate.UniqueID];
                txtToDate.Text = todate;

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(fromdate.ToString());
                string ConvertToDate = ServerDate.getDate_MDY(todate.ToString());

                string select = "";

                if (rdbRoad.Checked == true)
                {
                    select = string.Format("SELECT DC_MO,REPLACE(CONVERT(NVARCHAR,CreatedDate, 111), '/', '-') + ' ' + CONVERT(varchar(35),CreatedDate,114) As CreatedDate FROM DeliveryChallan_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and FrmDist='" + districtid + "' and ModeofDispatch='12' and Issue_Center='" + ddlIssueCentre.SelectedValue.ToString() + "' order by DC_MO");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDC.DataSource = ds.Tables[0];
                            ddlDC.DataTextField = "DC_MO";
                            ddlDC.DataValueField = "CreatedDate";
                            ddlDC.DataBind();
                            ddlDC.Items.Insert(0, "--Select--");

                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस दिनांक में By Road के लिए कोई भी डिलीवरी चालान क्रमांक उपलब्ध नहीं है|'); </script> ");
                            ddlDC.DataSource = "";
                            ddlDC.DataBind();
                        }
                    }
                }
                else
                {
                    select = string.Format("SELECT DC_MO,REPLACE(CONVERT(NVARCHAR,CreatedDate, 111), '/', '-') + ' ' + CONVERT(varchar(35),CreatedDate,114) As CreatedDate FROM DeliveryChallan_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and FrmDist='" + districtid + "' and ModeofDispatch='13' and Issue_Center='" + ddlIssueCentre.SelectedValue.ToString() + "' and DC_MO like'MORK%' order by DC_MO");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDC.DataSource = ds.Tables[0];
                            ddlDC.DataTextField = "DC_MO";
                            ddlDC.DataValueField = "CreatedDate";
                            ddlDC.DataBind();
                            ddlDC.Items.Insert(0, "--Select--");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस दिनांक में By Rack के लिए कोई भी डिलीवरी चालान क्रमांक उपलब्ध नहीं है|'); </script> ");
                            ddlDC.DataSource = "";
                            ddlDC.DataBind();
                        }
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

    protected void ddlDC_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMONo.Text = txtRackNo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtFrmRailHead.Text = txtToRailHead.Text = txtIC.Text = txtGodown.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = "";
        hdfFrmDist.Value = hdfToDist.Value = hdfFrmRailHead.Value = hdfToRailHead.Value = hdfFrmIC.Value = hdfFrmGodown.Value = hdfChangeGodown.Value = hdfToIC.Value = hdfToGodown.Value = hdfIsRecd.Value = hdfSTO_No.Value = hdfSMO.Value = hdfIssued_Bags.Value = hdfCommodityVal.Value = hdfStock_Issued_From.Value = hdfIssued_Date.Value = "";
        hdfGatePass.Value = "";
        btnDelete.Enabled = false;

        if (ddlDC.SelectedIndex > 0)
        {
            CheckGatePass();
            if (hdfGatePass.Value != "1")
            {
                GetAllDCData();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो चालान नंबर Select किया है, उसका GatePass जारी किया जा चूका है, इसलिए आप इसे डिलीट नहीं कर सकते|'); </script> ");
                return;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया डिलेवरी चालान क्रमांक चुने |'); </script> ");
            btnDelete.Enabled = false;
        }
    }

    private void CheckGatePass()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();

                string select = "Select * From OtherDepoChallanDetails Where ChallanNum='" + ddlDC.SelectedItem.ToString() + "' ";
                da = new SqlDataAdapter(select, con_MPStorage);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfGatePass.Value = "1";
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

    public void GetAllDCData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select DC.IsReceived,DC.MoveOrdernum,DC.Rack_No,COM.Commodity_Name As ComdtyName,FrmDist.district_name As FrmDist,ToDist.district_name As ToDist,DC.TO_No,FrmRail.RailHead_Name As FrmRail,ToRail.RailHead_Name As ToRail" +
                                ",FrmIC.DepotName As FrmIC,ToIC.DepotName As ToIC ,DC.Truck_No, DC.Issued_Bags,DC.Issued_Qty " +
                                ",DC.Default_Branch,DC.Default_Godown, DC.Change_Branch, DC.Change_Godown, Dc.RecGodown, DC.FrmDist As FrmDistVal,DC.ToDist As ToDistVal,DC.FrmRailHaid As FrmRailHaidVal, DC.ToRailHaid As ToRailHaidVal,DC.Issue_Center As FrmICVal,DC.RecIC As ToICVal,DC.STO_No,DC.SMO,DC.Issued_Bags,DC.Commodity As CommodityVal,DC.Stock_Issued_From,DC.Issued_Date " +
                                "From DeliveryChallan_MO DC Left Join tbl_MetaData_STORAGE_COMMODITY COM ON(DC.Commodity=COM.Commodity_Id) " +
                                "left Join pds.districtsmp FrmDist ON(DC.FrmDist=FrmDist.district_code) " +
                                "left Join pds.districtsmp ToDist ON(DC.ToDist=ToDist.district_code) " +
                                "Left Join tbl_Rail_Head FrmRail ON(DC.FrmDist=FrmRail.district_code and DC.FrmRailHaid=FrmRail.RailHead_Code) " +
                                "Left Join tbl_Rail_Head ToRail ON(DC.ToDist=ToRail.district_code and DC.ToRailHaid=ToRail.RailHead_Code)" +
                                "Left Join tbl_MetaData_DEPOT FrmIC ON(DC.Issue_Center=FrmIC.DepotID and '23' + DC.FrmDist=FrmIC.DistrictId) " +
                                "Left Join tbl_MetaData_DEPOT ToIC ON(DC.RecIC=ToIC.DepotID and '23' + DC.ToDist=ToIC.DistrictId ) " +
                                "where DC.DC_MO='" + ddlDC.SelectedItem.ToString() + "' and DC.CreatedDate='" + ddlDC.SelectedValue.ToString() + "' and DC.FrmDist='" + Session["dist_id"].ToString() + "' and DC.Issue_Center='" + ddlIssueCentre.SelectedValue.ToString() + "' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    txtRackNo.Text = ds.Tables[0].Rows[0]["Rack_No"].ToString();
                    txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    txtTranspNO.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();
                    txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    txtToDist.Text = ds.Tables[0].Rows[0]["ToDist"].ToString();
                    txtFrmRailHead.Text = ds.Tables[0].Rows[0]["FrmRail"].ToString();
                    txtToRailHead.Text = ds.Tables[0].Rows[0]["ToRail"].ToString();
                    txtIC.Text = ds.Tables[0].Rows[0]["FrmIC"].ToString();
                    txtTCNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtIssuedQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    txtRecdIC.Text = ds.Tables[0].Rows[0]["ToIC"].ToString();

                    hdfFrmDist.Value = ds.Tables[0].Rows[0]["FrmDistVal"].ToString();
                    hdfToDist.Value = ds.Tables[0].Rows[0]["ToDistVal"].ToString();
                    hdfFrmRailHead.Value = ds.Tables[0].Rows[0]["FrmRailHaidVal"].ToString();
                    hdfToRailHead.Value = ds.Tables[0].Rows[0]["ToRailHaidVal"].ToString();
                    hdfFrmIC.Value = ds.Tables[0].Rows[0]["FrmICVal"].ToString();
                    hdfFrmGodown.Value = ds.Tables[0].Rows[0]["Default_Godown"].ToString();
                    hdfChangeGodown.Value = ds.Tables[0].Rows[0]["Change_Godown"].ToString();
                    hdfToIC.Value = ds.Tables[0].Rows[0]["ToICVal"].ToString();
                    hdfToGodown.Value = ds.Tables[0].Rows[0]["RecGodown"].ToString();
                    hdfIsRecd.Value = ds.Tables[0].Rows[0]["IsReceived"].ToString();
                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
                    hdfSMO.Value = ds.Tables[0].Rows[0]["SMO"].ToString();
                    hdfIssued_Bags.Value = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();
                    hdfCommodityVal.Value = ds.Tables[0].Rows[0]["CommodityVal"].ToString();
                    hdfStock_Issued_From.Value = ds.Tables[0].Rows[0]["Stock_Issued_From"].ToString();
                    hdfIssued_Date.Value = ds.Tables[0].Rows[0]["Issued_Date"].ToString();

                    if (hdfChangeGodown.Value == "00")
                    {
                        ActualFrmGodown = hdfFrmGodown.Value;
                    }
                    else
                    {
                        ActualFrmGodown = hdfChangeGodown.Value;
                    }

                    GetBG();
                    btnDelete.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अन्य जानकारी उपलब्ध नहीं हैं|'); </script> ");
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                btnDelete.Enabled = false;
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


    public void GetBG()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                string select = "";
                con_MPStorage.Open();

                select = string.Format("select (select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + ActualFrmGodown + "') As SendGodown_Name,  (Select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + hdfToGodown.Value + "') As RecddGodown_Name");
                da1 = new SqlDataAdapter(select, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    txtGodown.Text = ds1.Tables[0].Rows[0]["SendGodown_Name"].ToString();
                    txtRecdGodown.Text = ds1.Tables[0].Rows[0]["RecddGodown_Name"].ToString();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlDC.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया डिलेवरी चालान क्रमांक चुने |'); </script> ");
            return;
        }
        else if (hdfGatePass.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो चालान नंबर Select किया है, उसका GatePass जारी किया जा चूका है, इसलिए आप इसे डिलीट नहीं कर सकते|'); </script> ");
            return;
        }
        else if (txtIssuedQty.Text == "" || txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issued Qty Or Truck Number Is Not Allow Blank'); </script> ");
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
                        string DCddl = "", DCsubstring = "", instr = "", TOQty = "", strCreatedDate = "", strSubCreatedDate = "";
                        DCddl = ddlDC.SelectedItem.ToString();
                        DCsubstring = DCddl.Substring(0, 4);

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        if (hdfChangeGodown.Value == "00")
                        {
                            ActualFrmGodown = hdfFrmGodown.Value;
                        }
                        else
                        {
                            ActualFrmGodown = hdfChangeGodown.Value;
                        }

                        strCreatedDate = ddlDC.SelectedValue.ToString();
                        strSubCreatedDate = strCreatedDate.Substring(0, 16);

                        con.Open();

                        if (DCsubstring == "MORD") //By Road
                        {
                            if (hdfIsRecd.Value == "Y")
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्तकर्ता जिले द्वारा कमोडिटी प्राप्त कर ली गयी है, इसलिए आप इस डिलेवरी चालान को डिलीट नहीं कर सकते |'); </script> ");
                                return;
                            }
                            else
                            {
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Insert Into TO_AgainstHO_MO_Trans_Log(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,Month,Year,IsSend,State_Id,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist,DeletedDate,DeletedIP,Operation) select MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,Month,Year,IsSend,State_Id,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist,GETDATE(),'" + GetIp + "','U' From TO_AgainstHO_MO where STO_No='" + hdfSTO_No.Value + "' and MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='12'; ";

                                instr += "Update TO_AgainstHO_MO Set RemQty=(RemQty+" + txtIssuedQty.Text + ") where STO_No='" + hdfSTO_No.Value + "' and MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='12'; ";

                                instr += "Update RecAgainst_StateMovementOrder set IssuedQty=(IssuedQty+ " + txtIssuedQty.Text + ") where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + hdfSMO.Value + "' and ModeofDispatch='12' and Godown='" + hdfToGodown.Value + "'; ";

                                instr += "Insert Into DeliveryChallan_MO_Trans_Logs(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,OperatorID,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,Recd_Qty,Recd_Bags,Recd_Date,RecIC,RecBranch,RecGodown,RSDRecd_Qty,RSDRecd_Bags,RSDRecd_Date,RSDCreatedDate,RRDRecd_Qty,RRDRecd_Bags,RRDRecd_Date,RRDCreated,Distance,Consinment_No,Book_No,Page_No,SubSMO,RMO,DeletedDate,DeletedIP,Operation) select MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,OperatorID,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,Recd_Qty,Recd_Bags,Recd_Date,RecIC,RecBranch,RecGodown,RSDRecd_Qty,RSDRecd_Bags,RSDRecd_Date,RSDCreatedDate,RRDRecd_Qty,RRDRecd_Bags,RRDRecd_Date,RRDCreated,Distance,Consinment_No,Book_No,Page_No,SubSMO,RMO,GETDATE(),'" + GetIp + "','D' From DeliveryChallan_MO where STO_No='" + hdfSTO_No.Value + "' and DC_MO='" + ddlDC.SelectedItem.ToString() + "' and CreatedDate='" + ddlDC.SelectedValue.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='12' and Issue_Center='" + hdfFrmIC.Value + "' and Issued_Qty='" + txtIssuedQty.Text + "' and Truck_No='" + txtTCNo.Text + "' and RecIC='" + hdfToIC.Value + "' and IsReceived='N'; ";

                                //instr += "Delete From DeliveryChallan_MO where STO_No='" + hdfSTO_No.Value + "' and DC_MO='" + ddlDC.SelectedItem.ToString() + "' and CreatedDate='" + ddlDC.SelectedValue.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='12' and Issue_Center='" + hdfFrmIC.Value + "' and Issued_Qty='" + txtIssuedQty.Text + "' and Truck_No='" + txtTCNo.Text + "' and RecIC='" + hdfToIC.Value + "' and IsReceived='N'; ";

                                instr += "Delete From DeliveryChallan_MO where STO_No='" + hdfSTO_No.Value + "' and DC_MO='" + ddlDC.SelectedItem.ToString() + "' and CreatedDate='" + ddlDC.SelectedValue.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='12' and Issue_Center='" + hdfFrmIC.Value + "' and Truck_No='" + txtTCNo.Text + "' and IsReceived='N'; ";

                                instr += "insert into dbo.SCSC_Truck_challan_Trans_Log(State_Id,Dist_ID,Depot_Id,Challan_Date,Dispatch_Godown,Sendto_District,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,Month,Year,Source,TO_Number,Sendto_IC,Operation,Updated_Date,IP_Address) Select State_Id,Dist_ID,Depot_Id,Challan_Date,Dispatch_Godown,Sendto_District,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,Month,Year,Source,TO_Number,Sendto_IC,'D',GETDATE(),'" + GetIp + "' From SCSC_Truck_challan where Challan_No='" + ddlDC.SelectedItem.ToString() + "' and Dist_ID='" + hdfFrmDist.Value + "' and Sendto_District='" + hdfToDist.Value + "' and Truck_no='" + txtTCNo.Text + "' and Bags='" + hdfIssued_Bags.Value + "'; ";

                                instr += "Delete From SCSC_Truck_challan where Challan_No='" + ddlDC.SelectedItem.ToString() + "' and Dist_ID='" + hdfFrmDist.Value + "' and Sendto_District='" + hdfToDist.Value + "' and Truck_no='" + txtTCNo.Text + "' and Bags='" + hdfIssued_Bags.Value + "'; ";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                        }
                        else if (DCsubstring == "MORK") //By Rail Rack
                        {
                            if (hdfIsRecd.Value == "Y")
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('रेल हेड पर कमोडिटी प्राप्त कर ली गयी है, इसलिए आप इस डिलेवरी चालान को डिलीट नहीं कर सकते |'); </script> ");
                                return;
                            }
                            else
                            {
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Insert Into TO_AgainstHO_MO_Trans_Log(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,Month,Year,IsSend,State_Id,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist,DeletedDate,DeletedIP,Operation) select MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,Month,Year,IsSend,State_Id,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist,GETDATE(),'" + GetIp + "','U' From TO_AgainstHO_MO where STO_No='" + hdfSTO_No.Value + "' and MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='13' ;";

                                instr += "Update TO_AgainstHO_MO Set RemQty=(RemQty+" + txtIssuedQty.Text + ") where STO_No='" + hdfSTO_No.Value + "' and MoveOrdernum='" + txtMONo.Text + "'and ModeofDispatch='13' ;";

                                instr += "Insert Into DeliveryChallan_MO_Trans_Logs(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,OperatorID,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,Recd_Qty,Recd_Bags,Recd_Date,RecIC,RecBranch,RecGodown,RSDRecd_Qty,RSDRecd_Bags,RSDRecd_Date,RSDCreatedDate,RRDRecd_Qty,RRDRecd_Bags,RRDRecd_Date,RRDCreated,Distance,Consinment_No,Book_No,Page_No,SubSMO,RMO,DeletedDate,DeletedIP,Operation) select MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,OperatorID,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,Recd_Qty,Recd_Bags,Recd_Date,RecIC,RecBranch,RecGodown,RSDRecd_Qty,RSDRecd_Bags,RSDRecd_Date,RSDCreatedDate,RRDRecd_Qty,RRDRecd_Bags,RRDRecd_Date,RRDCreated,Distance,Consinment_No,Book_No,Page_No,SubSMO,RMO,GETDATE(),'" + GetIp + "','D' From DeliveryChallan_MO where STO_No='" + hdfSTO_No.Value + "' and DC_MO='" + ddlDC.SelectedItem.ToString() + "' and CreatedDate='" + ddlDC.SelectedValue.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='13' and Issue_Center='" + hdfFrmIC.Value + "' and Issued_Qty='" + txtIssuedQty.Text + "' and Truck_No='" + txtTCNo.Text + "' and FrmRailHaid='" + hdfFrmRailHead.Value + "' and ToRailHaid='" + hdfToRailHead.Value + "' and Rack_No='" + txtRackNo.Text + "' and IsReceived='N'; ";

                                instr += "Delete From DeliveryChallan_MO where STO_No='" + hdfSTO_No.Value + "' and DC_MO='" + ddlDC.SelectedItem.ToString() + "' and CreatedDate='" + ddlDC.SelectedValue.ToString() + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='13' and Issue_Center='" + hdfFrmIC.Value + "' and Truck_No='" + txtTCNo.Text + "' and FrmRailHaid='" + hdfFrmRailHead.Value + "' and ToRailHaid='" + hdfToRailHead.Value + "' and IsReceived='N'; ";

                                instr += "Insert Into SCSC_RailHead_TC_Log Select * From SCSC_RailHead_TC where Depot_Id='" + hdfFrmIC.Value + "' and Challan_Date='" + hdfIssued_Date.Value + "' and Dispatch_Godown='" + ActualFrmGodown + "' and Commodity='" + hdfCommodityVal.Value + "' and Rack_NO='" + txtRackNo.Text + "' and Bags='" + hdfIssued_Bags.Value + "' and Challan_No='" + ddlDC.SelectedItem.ToString() + "' and Truck_no='" + txtTCNo.Text + "' and Dist_ID='" + hdfFrmDist.Value + "' and Sendto_District='" + hdfToDist.Value + "';";

                                instr += "Delete From SCSC_RailHead_TC where Depot_Id='" + hdfFrmIC.Value + "' and Challan_Date='" + hdfIssued_Date.Value + "' and Commodity='" + hdfCommodityVal.Value + "' and Rack_NO='" + txtRackNo.Text + "' and Bags='" + hdfIssued_Bags.Value + "' and Challan_No='" + ddlDC.SelectedItem.ToString() + "' and Truck_no='" + txtTCNo.Text + "' and Dist_ID='" + hdfFrmDist.Value + "' and Sendto_District='" + hdfToDist.Value + "';";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }
                        }
                        else
                        {
                            return;
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnDelete.Enabled = false;

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('डिलेवरी चालान सफलता पूर्वक डिलीट हो चूका है|'); </script> ");
                            txtIssuedQty.Text = "";

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
        Response.Redirect("~/District/MovementOrderHome.aspx");
    }
    protected void rdbRoad_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        ddlIssueCentre.SelectedIndex = 0;
        ddlDC.Items.Clear();
        txtMONo.Text = txtRackNo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtFrmRailHead.Text = txtToRailHead.Text = txtIC.Text = txtGodown.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = "";
    }
    protected void rdbRack_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        ddlIssueCentre.SelectedIndex = 0;
        ddlDC.Items.Clear();
        txtMONo.Text = txtRackNo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtFrmRailHead.Text = txtToRailHead.Text = txtIC.Text = txtGodown.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = "";
    }
    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        ddlDC.Items.Clear();
        txtMONo.Text = txtRackNo.Text = txtCommodity.Text = txtTranspNO.Text = txtFrmDist.Text = txtToDist.Text = txtFrmRailHead.Text = txtToRailHead.Text = txtIC.Text = txtGodown.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = "";
    }
}