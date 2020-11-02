using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class District_Delete_PDSMODist_DC : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    SqlCommand cmd;

    string districtid = "";
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlBookNo.Items.Clear();
        ddlPageNo.Items.Clear();
        btnDelete.Enabled = false;
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtToDist.Text = txtFrmRailHead.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = txtConsNo.Text = "";
        hdfFrmDist.Value = hdfToDist.Value = hdfFrmRailHead.Value = hdfToIC.Value = hdfToGodown.Value = hdfIsRecd.Value = hdfSTO_No.Value = hdfSMO.Value = hdfIssued_Bags.Value = hdfCommodityVal.Value = hdfIssued_Date.Value = hdfRMO.Value = "";
        
        if (txtFromDate.Text == "")
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
            GetBookNumber();
        }
    }

    public void GetBookNumber()
    {
        // RequiredFieldValidator5.Visible = RequiredFieldValidator6.Visible = false;
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

                select = string.Format("SELECT distinct Book_No FROM DeliveryChallan_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and FrmDist='" + districtid + "' and ModeofDispatch='13' and Issue_Center='0000' and DC_MO like'MORTR%' order by Book_No");
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस दिनांक में कोई भी Book Number उपलब्ध नहीं है|'); </script> ");
                    ddlBookNo.DataSource = "";
                    ddlBookNo.DataBind();
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
        btnDelete.Enabled = false;
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtToDist.Text = txtFrmRailHead.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = txtConsNo.Text = "";
        hdfFrmDist.Value = hdfToDist.Value = hdfFrmRailHead.Value = hdfToIC.Value = hdfToGodown.Value = hdfIsRecd.Value = hdfSTO_No.Value = hdfSMO.Value = hdfIssued_Bags.Value = hdfCommodityVal.Value = hdfIssued_Date.Value = hdfRMO.Value = "";

        if (ddlBookNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Book Number'); </script> ");
            return;
        }
        else
        {
            GetPageNumber();
        }

    }

    public void GetPageNumber()
    {
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

                select = string.Format("SELECT Page_No,DC_MO FROM DeliveryChallan_MO where CreatedDate between '" + ConvertFromDate + " 00:00:00' and '" + ConvertToDate + " 23:59:59' and FrmDist='" + districtid + "' and ModeofDispatch='13' and Issue_Center='0000' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and DC_MO like'MORTR%' order by Page_No");
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस दिनांक में कोई भी Page Number उपलब्ध नहीं है|'); </script> ");
                    ddlPageNo.DataSource = "";
                    ddlPageNo.DataBind();
                    btnDelete.Enabled = false;
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
        btnDelete.Enabled = false;
        txtMONo.Text = txtCommodity.Text = txtTranspNO.Text = txtToDist.Text = txtFrmRailHead.Text = txtTCNo.Text = txtIssuedQty.Text = txtRecdIC.Text = txtRecdGodown.Text = txtConsNo.Text = "";
        hdfFrmDist.Value = hdfToDist.Value = hdfFrmRailHead.Value = hdfToIC.Value = hdfToGodown.Value = hdfIsRecd.Value = hdfSTO_No.Value = hdfSMO.Value = hdfIssued_Bags.Value = hdfCommodityVal.Value = hdfIssued_Date.Value = hdfRMO.Value = "";

        if (ddlPageNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Page Number'); </script> ");
            return;
        }
        else
        {
            GetPageDetails();
        }
    }

    public void GetPageDetails()
    {
        districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select DC.Consinment_No,DC.MoveOrdernum, DC.TO_No,DC.Issued_Qty,DC.Truck_No,TOIC.DepotName As TOIC, COM.Commodity_Name,RH.RailHead_Name,ToDist.district_name As ToDist, " +
                                "DC.RecIC As ToICVal,DC.RecGodown As RecGodown,DC.FrmDist As FrmDistVal,DC.ToDist As ToDistVal,DC.FrmRailHaid As FrmRailHaidVal,DC.IsReceived,DC.STO_No,DC.SMO,DC.Issued_Bags,DC.Commodity As CommodityVal,DC.Stock_Issued_From,DC.Issued_Date,DC.RMO  " +
                                "From DeliveryChallan_MO DC Left Join tbl_MetaData_STORAGE_COMMODITY COM ON(DC.Commodity=COM.Commodity_Id) " +
                                "Left join tbl_Rail_Head RH ON(RH.RailHead_Code=DC.FrmRailHaid and RH.district_code=DC.FrmDist) " +
                                "left Join pds.districtsmp ToDist ON(DC.ToDist=ToDist.district_code) " +
                                "Left Join tbl_MetaData_DEPOT ToIC ON(DC.RecIC=ToIC.DepotID and '23' + DC.ToDist=ToIC.DistrictId ) " +
                                "where DC.Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and DC.Page_No='" + ddlPageNo.SelectedItem.ToString() + "' and DC.DC_MO='" + ddlPageNo.SelectedValue.ToString() + "' and DC.FrmDist='" + districtid + "'  and DC.ModeofDispatch='13' and DC.DC_MO like'MORTR%' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtConsNo.Text = ds.Tables[0].Rows[0]["Consinment_No"].ToString();
                    txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    txtCommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    txtTranspNO.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();
                    txtFrmRailHead.Text = ds.Tables[0].Rows[0]["RailHead_Name"].ToString();
                    txtToDist.Text = ds.Tables[0].Rows[0]["ToDist"].ToString();
                    txtIssuedQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    txtTCNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtRecdIC.Text = ds.Tables[0].Rows[0]["TOIC"].ToString();

                    hdfFrmDist.Value = ds.Tables[0].Rows[0]["FrmDistVal"].ToString();
                    hdfToDist.Value = ds.Tables[0].Rows[0]["ToDistVal"].ToString();
                    hdfFrmRailHead.Value = ds.Tables[0].Rows[0]["FrmRailHaidVal"].ToString();
                    hdfToIC.Value = ds.Tables[0].Rows[0]["ToICVal"].ToString();
                    hdfToGodown.Value = ds.Tables[0].Rows[0]["RecGodown"].ToString();
                    hdfIsRecd.Value = ds.Tables[0].Rows[0]["IsReceived"].ToString();

                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
                    hdfSMO.Value = ds.Tables[0].Rows[0]["SMO"].ToString();
                    hdfIssued_Bags.Value = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();
                    hdfCommodityVal.Value = ds.Tables[0].Rows[0]["CommodityVal"].ToString();
                    hdfIssued_Date.Value = ds.Tables[0].Rows[0]["Issued_Date"].ToString();
                    hdfRMO.Value = ds.Tables[0].Rows[0]["RMO"].ToString();

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

                select = string.Format("select (Select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + hdfToGodown.Value + "') As RecddGodown_Name");
                da1 = new SqlDataAdapter(select, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
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
        if (ddlPageNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Page Number चुने |'); </script> ");
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
                        string instr = "";

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        con.Open();

                        if (hdfIsRecd.Value == "Y")
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्तकर्ता जिले द्वारा कमोडिटी प्राप्त कर ली गयी है, इसलिए आप इस डिलेवरी चालान को डिलीट नहीं कर सकते |'); </script> ");
                            return;
                        }
                        else
                        {
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                        "Update TruckChallan_Book set Rem_Page=Rem_Page+1 where Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and District='" + hdfFrmDist.Value + "';";

                            instr += "Update RackReceived set Rem_Qty=(Rem_Qty+'" + txtIssuedQty.Text + "'),Rem_Bags=(Rem_Bags+'" + hdfIssued_Bags.Value + "') where Consinment_No='" + txtConsNo.Text + "';";

                            if (hdfRMO.Value != "0")
                            {
                                instr += "Update RecAgainst_StateMovementOrder set IssuedQty=(IssuedQty+ " + txtIssuedQty.Text + ") where RMO='" + hdfRMO.Value + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and Godown='" + hdfToGodown.Value + "' and ModeofDispatch='13' ; ";
                            }

                            instr += "Insert Into TO_AgainstHO_MO_Trans_Log(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,Month,Year,IsSend,State_Id,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist,DeletedDate,DeletedIP,Operation) select MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,Month,Year,IsSend,State_Id,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist,GETDATE(),'" + GetIp + "','U' From TO_AgainstHO_MO where STO_No='" + hdfSTO_No.Value + "' and MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='13'; ";

                            instr += "Update TO_AgainstHO_MO Set RemQty=(RemQty+" + txtIssuedQty.Text + ") where STO_No='" + hdfSTO_No.Value + "' and MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='13'; ";

                            instr += "Insert Into DeliveryChallan_MO_Trans_Logs (MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,OperatorID,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,Recd_Qty,Recd_Bags,Recd_Date,RecIC,RecBranch,RecGodown,RSDRecd_Qty,RSDRecd_Bags,RSDRecd_Date,RSDCreatedDate,RRDRecd_Qty,RRDRecd_Bags,RRDRecd_Date,RRDCreated,Distance,Consinment_No,Book_No,Page_No,SubSMO,RMO,DeletedDate,DeletedIP,Operation) select MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,OperatorID,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,Recd_Qty,Recd_Bags,Recd_Date,RecIC,RecBranch,RecGodown,RSDRecd_Qty,RSDRecd_Bags,RSDRecd_Date,RSDCreatedDate,RRDRecd_Qty,RRDRecd_Bags,RRDRecd_Date,RRDCreated,Distance,Consinment_No,Book_No,Page_No,SubSMO,RMO,GETDATE(),'" + GetIp + "','D' From DeliveryChallan_MO where DC_MO='" + ddlPageNo.SelectedValue.ToString() + "' and SMO='" + hdfSMO.Value + "' and STO_No='" + hdfSTO_No.Value + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='13' and Issue_Center='0000' and Truck_No='" + txtTCNo.Text + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and Page_No='" + ddlPageNo.SelectedItem.ToString() + "' and Consinment_No='" + txtConsNo.Text + "' and IsReceived='N'; ";

                            instr += "Delete From DeliveryChallan_MO where DC_MO='" + ddlPageNo.SelectedValue.ToString() + "' and SMO='" + hdfSMO.Value + "' and STO_No='" + hdfSTO_No.Value + "' and FrmDist='" + hdfFrmDist.Value + "' and ToDist='" + hdfToDist.Value + "' and ModeofDispatch='13' and Issue_Center='0000' and Truck_No='" + txtTCNo.Text + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and Page_No='" + ddlPageNo.SelectedItem.ToString() + "' and Consinment_No='" + txtConsNo.Text + "' and IsReceived='N'; ";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
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

}