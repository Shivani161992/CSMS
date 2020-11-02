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

public partial class IssueCenter_DC_MO_DispatchByRack : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    MoveChallan mobj = null;
    protected Common ComObj = null;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string IC_Id = "", Dist_Id = "", strBranchval = "", strGodownVal = "", strUpdateGodownVal = "", strUpdateBranchVal = "", strCommodity = "", DeliveryChallan = "", DeliveryChallan_MO = "", TypeofBags = "", whr_ID = "", DCDate = "", SubDCDate = "";
    double QtyTotal = 0, QtyRemTotal = 0, strBalBagInGodown = 0, strBalQtyInGodown = 0, StrBalQtyInSendIC = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                Session["DC_MO"] = "";
                Session["CreatedDate"] = "";

                trMsg.Visible = trBG.Visible = false;

                GetTransporterDetails();
                GetBranch();
                GetSource();

                //rdbSBT.Checked = true;
                chkChange.Checked = false;

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }

    public void GetBranch()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + IC_Id + "'");
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

    public void GetSource()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "SELECT Source_Name,Source_ID,GETDATE() As ServerDate FROM dbo.Source_Arrival_Type where Source_ID IN('01','02','05') order by Source_ID";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSarrival.DataSource = ds.Tables[0];
                    ddlSarrival.DataTextField = "Source_Name";
                    ddlSarrival.DataValueField = "Source_ID";
                    ddlSarrival.DataBind();
                    ddlSarrival.Items.Insert(0, "--Select--");

                    //DateTime IssuedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ServerDate"].ToString());
                    //txtIssuedDate.Text = IssuedDate.ToString("dd-MM-yyyy");
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

    public void GetTransporterDetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                //Display Only Transport Order Issue Centre
                //string select = "select distinct TO_No from TO_AgainstHO_MO where FrmDist='" + Dist_Id + "' and ToDist!='00' and Issue_Center='" + IC_Id + "' and ((TOEndDate+1>=Getdate()) or ((DATEADD(DAY,120,CreatedDate))>=Getdate())) and (ToRailHaid!='00' or ToRailHaid is Null) and (Rack_No!='00' or Rack_No is Null)";

                string select = "select distinct TO_No from TO_AgainstHO_MO where FrmDist='" + Dist_Id + "' and ToDist!='00' and ((TOEndDate+1>=Getdate()) or ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and (ToRailHaid!='00' or ToRailHaid is Null) and (Rack_No!='00' or Rack_No is Null)";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTONo.DataSource = ds.Tables[0];
                    ddlTONo.DataTextField = "TO_No";
                    ddlTONo.DataValueField = "TO_No";
                    ddlTONo.DataBind();
                    ddlTONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('परिवहन आदेश क्रमांक उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlTONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGunnyType.Text = txtMONo.Text = txtRackNo.Text = txtTransporterName.Text = txtTranspEndDate.Text = txtCommodity.Text = txtFrmRailHead.Text = txtToRailHead.Text = txtIC.Text = txtBranch.Text = txtGodown.Text = txtQtyTotal.Text = txtQtyRemTotal.Text = txtBalBagInGodown.Text = txtBalQtyInGodown.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = txtIssuedDate.Text = txtBalQtyInSendIC.Text = txtFrmDist.Text = txtToDist.Text = "";
        GridView1.DataSource = "";
        GridView1.DataBind();

        trMsg.Visible = trBG.Visible = false;
        chkChange.Checked = false;

        ddlSendIC.Items.Clear();
        ddlSendBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlTONo.SelectedIndex > 0)
        {
            GetBagsType();
            GetAllData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया परिवहन आदेश क्रमांक चुने|'); </script> ");
            trMsg.Visible = false;
        }
    }

    public void GetAllData()
    {
        Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();
        string IsCancelled = "";

        ViewState["hdfSMO"] = ViewState["hdfTransporter_ID"] = ViewState["hdfFrmDist"] = ViewState["hdfToDist"] = ViewState["hdfCommodity"] = ViewState["hdfCropYear"] = ViewState["hdfTOEndDate"] = ViewState["hdfFrmRailHaid"] = ViewState["hdfToRailHaid"] = ViewState["hdfRack_DispDate"] = ViewState["hdfTrans_ID"] = ViewState["hdfDispatchMode"] = ViewState["hdfToIC"] = ViewState["hdfRMO"] = "";
        hdfGunnyType.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                //Display Only Transport Order Issue Centre
                //string select = "select distinct SMO.GunnyType,TOMO.MoveOrdernum,TOMO.SMO,TOMO.STO_No,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID =TOMO.Transporter_ID and Distt_ID='" + Dist_Id + "' and Transport_ID in ('10','11','6') order by Transporter_Name desc) TransporterName, TOMO.Transporter_ID,(select district_name From pds.districtsmp where district_code= TOMO.FrmDist) FrmDistName,TOMO.FrmDist,(select district_name From pds.districtsmp where district_code= TOMO.ToDist) ToDistName,TOMO.ToDist,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=TOMO.Commodity) ComdtyName,TOMO.Commodity,TOMO.RequiredQuantity,TOMO.RemQty,TOMO.CropYear,TOEndDate,TOMO.ModeofDispatch,(select DepotName from tbl_MetaData_DEPOT where DepotID=TOMO.Issue_Center) ICName,Issue_Center,Branch,Godown,(select RailHead_Name from tbl_Rail_Head where RailHead_Code=TOMO.FrmRailHaid) FrmRailName,FrmRailHaid,(select RailHead_Name from tbl_Rail_Head where RailHead_Code=TOMO.ToRailHaid) ToRailName,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID from TO_AgainstHO_MO As TOMO Left Join StateMovementOrder SMO ON(SMO.MoveOrdernum=TOMO.MoveOrdernum) where TO_No='" + ddlTONo.SelectedValue.ToString() + "' and TOMO.FrmDist='" + Dist_Id + "' and TOMO.ToDist!='00' and Issue_Center='" + IC_Id + "' and TOMO.RemQty!=0 and (ToRailHaid!='00' or ToRailHaid is Null) and (Rack_No!='00' or Rack_No is Null)";

                string select = "select distinct SMO.IsCancelled,SMO.GunnyType,TOMO.MoveOrdernum,TOMO.SMO,TOMO.STO_No,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID =TOMO.Transporter_ID and Distt_ID='" + Dist_Id + "' and Transport_ID in ('10','11','6') order by Transporter_Name desc) TransporterName, TOMO.Transporter_ID,(select district_name From pds.districtsmp where district_code= TOMO.FrmDist) FrmDistName,TOMO.FrmDist,(select district_name From pds.districtsmp where district_code= TOMO.ToDist) ToDistName,TOMO.ToDist,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=TOMO.Commodity) ComdtyName,TOMO.Commodity,TOMO.RequiredQuantity,TOMO.RemQty,TOMO.CropYear,TOEndDate,TOMO.ModeofDispatch,(select DepotName from tbl_MetaData_DEPOT where DepotID=TOMO.Issue_Center) ICName,Issue_Center,Branch,Godown,(select RailHead_Name from tbl_Rail_Head where RailHead_Code=TOMO.FrmRailHaid) FrmRailName,FrmRailHaid,(select RailHead_Name from tbl_Rail_Head where RailHead_Code=TOMO.ToRailHaid) ToRailName,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID from TO_AgainstHO_MO As TOMO Left Join StateMovementOrder SMO ON(SMO.MoveOrdernum=TOMO.MoveOrdernum) where TO_No='" + ddlTONo.SelectedValue.ToString() + "' and TOMO.FrmDist='" + Dist_Id + "' and TOMO.ToDist!='00' and TOMO.RemQty!=0 and (ToRailHaid!='00' or ToRailHaid is Null) and (Rack_No!='00' or Rack_No is Null)";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IsCancelled = ds.Tables[0].Rows[0]["IsCancelled"].ToString();

                    if (IsCancelled == "Y")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Movement Order को मुख्यालय द्वारा निरस्त कर दिया गया हैं| आप इस Movement Order के विरुद्ध Delivery Challan नहीं बना सकते|'); </script> ");
                        return;
                    }
                    else
                    {
                        txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                        txtRackNo.Text = ds.Tables[0].Rows[0]["Rack_No"].ToString();
                        txtTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();

                        DateTime TOEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["TOEndDate"].ToString());
                        txtTranspEndDate.Text = TOEndDate.ToString("dd/MMM/yyyy");

                        txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                        txtIC.Text = ds.Tables[0].Rows[0]["ICName"].ToString();
                        txtFrmRailHead.Text = ds.Tables[0].Rows[0]["FrmRailName"].ToString();
                        txtToRailHead.Text = ds.Tables[0].Rows[0]["ToRailName"].ToString();
                        txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                        txtToDist.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();

                        ViewState["hdfSMO"] = ds.Tables[0].Rows[0]["SMO"].ToString();
                        ViewState["hdfTransporter_ID"] = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
                        ViewState["hdfFrmDist"] = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                        ViewState["hdfToDist"] = ds.Tables[0].Rows[0]["ToDist"].ToString();
                        ViewState["hdfCommodity"] = ds.Tables[0].Rows[0]["Commodity"].ToString();
                        ViewState["hdfCropYear"] = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        ViewState["hdfTOEndDate"] = ds.Tables[0].Rows[0]["TOEndDate"].ToString();
                        ViewState["hdfFrmRailHaid"] = ds.Tables[0].Rows[0]["FrmRailHaid"].ToString();
                        ViewState["hdfToRailHaid"] = ds.Tables[0].Rows[0]["ToRailHaid"].ToString();
                        ViewState["hdfRack_DispDate"] = ds.Tables[0].Rows[0]["Rack_DispDate"].ToString();
                        ViewState["hdfTrans_ID"] = ds.Tables[0].Rows[0]["Trans_ID"].ToString();
                        ViewState["hdfDispatchMode"] = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();

                        hdfGunnyType.Value = ds.Tables[0].Rows[0]["GunnyType"].ToString();

                        //if (ViewState["hdfCommodity"].ToString() == "25")
                        //{
                        //    lblQtls.Text = lblQtls0.Text = "(Bales)";
                        //   // rdbHDPE.Visible = rdbSBT.Visible = false;
                        //    txtGunnyType.Visible = true;

                        //    if (hdfGunnyType.Value == "JUTE")
                        //    {
                        //        txtGunnyType.Text = "Jute(SBT)";
                        //    }
                        //    else
                        //    {
                        //        txtGunnyType.Text = "PP(HDPE)";
                        //    }
                        //}
                        //else
                        //{
                        //    lblQtls.Text = lblQtls0.Text = "(Qtls)";
                        //   // rdbHDPE.Visible = rdbSBT.Visible = true;
                        //    txtGunnyType.Visible = false;
                        //    txtGunnyType.Text = "";
                        //}

                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();

                        // Only Limited Issue Centre that are available in Movement Plan
                        //if (ViewState["hdfDispatchMode"].ToString() == "12") //Dispatch By Road
                        //{
                        //    string select1 = "select distinct (select DepotName from tbl_MetaData_DEPOT where DepotID=Rec.Issue_Center) ICName, Issue_Center From RecAgainst_StateMovementOrder As Rec where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString()+ "' and ModeofDispatch='12' and (IssuedQty>0 or IssuedQty Is Null)";
                        //    da1 = new SqlDataAdapter(select1, con);

                        //    ds1 = new DataSet();
                        //    da1.Fill(ds1);
                        //    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        //    {
                        //        ViewState["hdfToIC"] = ds1.Tables[0].Rows[0]["Issue_Center"].ToString();

                        //        ddlSendIC.DataSource = ds1.Tables[0];
                        //        ddlSendIC.DataTextField = "ICName";
                        //        ddlSendIC.DataValueField = "Issue_Center";
                        //        ddlSendIC.DataBind();
                        //        ddlSendIC.Items.Insert(0, "--Select--");
                        //    }
                        //}

                        // All Issue Centre that are not available in Movement Plan
                        if (ViewState["hdfDispatchMode"].ToString() == "12") //Dispatch By Road
                        {
                            string select1 = "Select DepotName As ICName,DepotID As Issue_Center from tbl_MetaData_DEPOT where DistrictId='23" + ViewState["hdfToDist"].ToString() + "'";
                            da1 = new SqlDataAdapter(select1, con);

                            ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                //ViewState["hdfToIC"] = ds1.Tables[0].Rows[0]["Issue_Center"].ToString();

                                ddlSendIC.DataSource = ds1.Tables[0];
                                ddlSendIC.DataTextField = "ICName";
                                ddlSendIC.DataValueField = "Issue_Center";
                                ddlSendIC.DataBind();
                                ddlSendIC.Items.Insert(0, "--Select--");
                            }
                        }
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अन्य जानकारी उपलब्ध नहीं हैं|'); </script> ");
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


    protected void ddlSendIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSendBranch.Items.Clear();
        ddlSendGodown.Items.Clear();
        txtBalQtyInSendIC.Text = "";
        ViewState["hdfRMO"] = "";

        if (ddlSendIC.SelectedIndex > 0)
        {
            GetSendBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Issue Centre'); </script> ");
        }
    }

    // Only Limited Branch that are available in Movement Plan
    //public void GetSendBranch()
    //{
    //    string name = string.Empty;
    //    string id = string.Empty;

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            string select = "select distinct Branch From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString() + "' and ModeofDispatch='12' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and (IssuedQty>0 or IssuedQty Is Null)";
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    using (con_MPStorage = new SqlConnection(strcon_MPStorage))
    //                    {
    //                        try
    //                        {
    //                            con_MPStorage.Open();
    //                            string GetBranch = "select DepotName from tbl_MetaData_DEPOT where BranchId='" + ds.Tables[0].Rows[i]["Branch"].ToString() + "'";
    //                            da1 = new SqlDataAdapter(GetBranch, con_MPStorage);

    //                            ds1 = new DataSet();
    //                            da1.Fill(ds1);
    //                            if (ds1 != null)
    //                            {
    //                                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
    //                                {
    //                                    id = ds.Tables[0].Rows[i]["Branch"].ToString();
    //                                    name = ds1.Tables[0].Rows[0]["DepotName"].ToString();
    //                                    ddlSendBranch.Items.Add(new ListItem(name, id));
    //                                }
    //                            }
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //                        }

    //                        finally
    //                        {
    //                            if (con_MPStorage.State != ConnectionState.Closed)
    //                            {
    //                                con_MPStorage.Close();
    //                            }
    //                        }
    //                    }
    //                }

    //                ddlSendBranch.Items.Insert(0, "--Select--");
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Branch Is Not Available'); </script> ");
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

    public void GetSendBranch()
    {
        ViewState["hdfToIC"] = ddlSendIC.SelectedValue.ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ddlSendIC.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSendBranch.DataSource = ds.Tables[0];
                        ddlSendBranch.DataTextField = "DepotName";
                        ddlSendBranch.DataValueField = "BranchID";
                        ddlSendBranch.DataBind();
                        ddlSendBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ViewState["hdfToDist"].ToString() + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSendBranch.DataSource = ds.Tables[0];
                        ddlSendBranch.DataTextField = "DepotName";
                        ddlSendBranch.DataValueField = "BranchId";
                        ddlSendBranch.DataBind();
                        ddlSendBranch.Items.Insert(0, "--Select--");
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

    protected void ddlSendBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSendGodown.Items.Clear();
        txtBalQtyInSendIC.Text = "";
        ViewState["hdfRMO"] = "";

        if (ddlSendBranch.SelectedIndex > 0)
        {
            GetSendGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Branch'); </script> ");
        }
    }

    //public void GetSendGodown()
    //{
    //    string name = string.Empty;
    //    string id = string.Empty;

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            string select = "select distinct Godown From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString() + "' and ModeofDispatch='12' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and (IssuedQty>0 or IssuedQty Is Null)";
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    using (con_MPStorage = new SqlConnection(strcon_MPStorage))
    //                    {
    //                        try
    //                        {
    //                            con_MPStorage.Open();
    //                            string GetGodown = "select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + ds.Tables[0].Rows[i]["Godown"].ToString() + "'";
    //                            da1 = new SqlDataAdapter(GetGodown, con_MPStorage);

    //                            ds1 = new DataSet();
    //                            da1.Fill(ds1);
    //                            if (ds1 != null)
    //                            {
    //                                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
    //                                {
    //                                    id = ds.Tables[0].Rows[i]["Godown"].ToString();
    //                                    name = ds1.Tables[0].Rows[0]["Godown_Name"].ToString();
    //                                    ddlSendGodown.Items.Add(new ListItem(name, id));
    //                                }
    //                            }
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //                        }

    //                        finally
    //                        {
    //                            if (con_MPStorage.State != ConnectionState.Closed)
    //                            {
    //                                con_MPStorage.Close();
    //                            }
    //                        }
    //                    }
    //                }

    //                ddlSendGodown.Items.Insert(0, "--Select--");
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Godown Is Not Available'); </script> ");
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

    public void GetSendGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='{0}'", ddlSendBranch.SelectedValue.ToString());
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSendGodown.DataSource = ds.Tables[0];
                        ddlSendGodown.DataTextField = "Godown_Name";
                        ddlSendGodown.DataValueField = "Godown_ID";
                        ddlSendGodown.DataBind();
                        ddlSendGodown.Items.Insert(0, "--Select--");
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

    protected void ddlSendGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBalQtyInSendIC.Text = "";
        ViewState["hdfRMO"] = "";
        if (ddlSendGodown.SelectedIndex > 0)
        {
            GetDataRec();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
        }
    }

    public void GetDataRec()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                //For Movement Plan Qty is Greater Than 0.
                //string select = "Select IssuedQty,RMO From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString() + "' and ModeofDispatch='12' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "' and IssuedQty>0";

                string select = "Select IssuedQty,RMO From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and ToDist='" + ViewState["hdfToDist"].ToString() + "' and ModeofDispatch='12' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtBalQtyInSendIC.Text = ds.Tables[0].Rows[0]["IssuedQty"].ToString();
                    ViewState["hdfRMO"] = ds.Tables[0].Rows[0]["RMO"].ToString();
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


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
            QtyRemTotal = 0;

            if (ViewState["hdfCommodity"].ToString() == "25")
            {
                e.Row.Cells[3].Text = "कुल मात्रा(Bales)";
                e.Row.Cells[4].Text = "बची हुई मात्रा(Bales)";
            }
            else
            {
                e.Row.Cells[3].Text = "कुल मात्रा(Qtls)";
                e.Row.Cells[4].Text = "बची हुई मात्रा(Qtls)";
            }
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
            //e.Row.Cells[3].Text = QtyTotal.ToString();

            QtyRemTotal += (double.Parse(e.Row.Cells[4].Text));
            //e.Row.Cells[4].Text = QtyRemTotal.ToString();

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (ViewState["hdfCommodity"].ToString() == "25")
            {
                e.Row.Cells[2].Text = "महायोग (Bales)";
            }
            else
            {
                e.Row.Cells[2].Text = "महायोग (Qtls)";
            }

            e.Row.Cells[3].Text = QtyTotal.ToString("0.00");
            e.Row.Cells[4].Text = QtyRemTotal.ToString("0.00");
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkChange.Checked = false;
        trBG.Visible = false;
        ddlBranch.SelectedIndex = 0;
        ddlGodown.Items.Clear();
        ddlSarrival.SelectedIndex = 0;
        txtIssuedDate.Text = "";
        txtBalBagInGodown.Text = txtBalQtyInGodown.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = "";

        ViewState["hdfLoginBranch"] = ViewState["hdfLoginGodown"] = ViewState["hdfSTO_No"] = ViewState["hdfSTrans_ID"] = "";
        hdfGridIC_ID.Value = "";

        txtBranch.Text = GridView1.SelectedRow.Cells[1].Text;
        txtGodown.Text = GridView1.SelectedRow.Cells[2].Text;
        txtQtyTotal.Text = GridView1.SelectedRow.Cells[3].Text;
        txtQtyRemTotal.Text = GridView1.SelectedRow.Cells[4].Text;

        ViewState["hdfLoginBranch"] = GridView1.SelectedRow.Cells[5].Text;
        ViewState["hdfLoginGodown"] = GridView1.SelectedRow.Cells[6].Text;
        ViewState["hdfSTO_No"] = GridView1.SelectedRow.Cells[7].Text; ;
        ViewState["hdfSTrans_ID"] = GridView1.SelectedRow.Cells[8].Text;
        hdfGridIC_ID.Value = GridView1.SelectedRow.Cells[9].Text;
        GetStackNumber();
        trMsg.Visible = true;

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
                    string select = string.Format("select Stack_ID, Stack_Name  from tbl_MetaData_STACK where Stack_Killed='N' and Godown_ID='" + ViewState["hdfLoginGodown"] + "' order by Stack_Name");
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


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlSarrival.SelectedIndex = 0;
        txtIssuedDate.Text = "";
        txtBalBagInGodown.Text = txtBalQtyInGodown.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = "";
        ddlstackNumber.Items.Clear();
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

    protected void ddlSarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBalBagInGodown.Text = txtBalQtyInGodown.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = "";

        if (hdfGridIC_ID.Value == Session["issue_id"].ToString())
        {
            if (chkChange.Checked)
            {
                if (ddlBranch.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
                    ddlSarrival.SelectedIndex = 0;
                    return;
                }

                if (ddlGodown.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें |'); </script> ");
                    ddlSarrival.SelectedIndex = 0;
                    return;
                }

                if (ddlstackNumber.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया स्टैक नंबर चुनें |'); </script> ");
                    ddlSarrival.SelectedIndex = 0;
                    return;
                }
                if (ddlbagstype.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
                    return;
                }
            }
        }
        else
        {
            if (chkChange.Checked == false)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया आप स्वयं के प्रदाय केंद्र के ब्रांच तथा गोदाम का चुनाव करें |'); </script> ");
                ddlSarrival.SelectedIndex = 0;
                return;
            }
            else
            {
                if (ddlBranch.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
                    ddlSarrival.SelectedIndex = 0;
                    return;
                }

                if (ddlGodown.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें |'); </script> ");
                    ddlSarrival.SelectedIndex = 0;
                    return;
                }
                if (ddlstackNumber.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया स्टैक नंबर चुनें |'); </script> ");
                    ddlSarrival.SelectedIndex = 0;
                    return;
                }
                if (ddlbagstype.SelectedIndex <= 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
                    return;
                }
            }
        }

        if (ddlSarrival.SelectedIndex > 0)
        {
            if (txtIssuedDate.Text != "" && ddlstackNumber.SelectedIndex > 0 && ddlbagstype.SelectedIndex>0)
            {
                GetGodownBalance();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issued Date, Stack number and Bags Type'); </script> ");
                ddlSarrival.SelectedIndex = 0;
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mode of Stock'); </script> ");
        }
    }

    public void GetGodownBalance()
    {
        if (ddlbagstype.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        strBalBagInGodown = strBalQtyInGodown = 0;
        strBranchval = strGodownVal = "";

        if (chkChange.Checked)
        {
            strBranchval = ddlBranch.SelectedValue.ToString();
            Dist_Id = Session["dist_id"].ToString();
            IC_Id = Session["issue_id"].ToString();
            strCommodity = ViewState["hdfCommodity"].ToString();

            DateTime dodate = Convert.ToDateTime(DateTime.ParseExact(txtIssuedDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));

            strGodownVal = ddlGodown.SelectedValue.ToString();
            Int64 comid = Convert.ToInt64(strGodownVal);

            string openingdate;

            if (dodate >= Convert.ToDateTime(DateTime.ParseExact("01/01/2015", "dd/MM/yyyy", null).ToString("MM/dd/yyyy")))
            {
                openingdate = "01/01/2015";
            }
            else
            {
                openingdate = "01/04/2014";
            }

            string pqry = "balanceDetails";

            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    SqlCommand cmdpqty = new SqlCommand(pqry, con);
                    cmdpqty.CommandType = CommandType.StoredProcedure;

                    //cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = Dist_Id;
                    //cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = IC_Id;
                    //cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
                    //cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = strCommodity;
                    ////cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
                    //cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;


                    cmdpqty.Parameters.Add("@cropyear", SqlDbType.VarChar).Value = ViewState["hdfCropYear"].ToString();
                    cmdpqty.Parameters.Add("@godown", SqlDbType.VarChar).Value = strGodownVal;
                    cmdpqty.Parameters.Add("@cmd", SqlDbType.VarChar).Value = strCommodity;
                    cmdpqty.Parameters.Add("@BagsType", SqlDbType.Int).Value = ddlbagstype.SelectedValue.ToString();

                    DataSet ds = new DataSet();
                    SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

                    dr.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Qtybalance"].ToString()), 5);

                        double bags = Convert.ToDouble(ds.Tables[0].Rows[0]["BalanceBags"].ToString());

                        txtBalQtyInGodown.Text = Convert.ToString(stock);
                        txtBalBagInGodown.Text = Convert.ToString(bags);

                        if (stock == 0 || stock < 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस गोदाम में स्टॉक उपलभ नहीं है इसलिए इस गोदाम से चालान जारी नहीं होंगे |'); </script> ");
                            btnRecptSubmit.Enabled = false;
                            return;

                        }
                        if (bags == 0 || bags < 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस गोदाम में Bags उपलभ नहीं है इसलिए इस गोदाम से चालान जारी नहीं होंगे |'); </script> ");
                            btnRecptSubmit.Enabled = false;
                            return;
                        }


                        //txtcurntcap.Text = Convert.ToString(stock);
                        //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

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
            if (txtBranch.Text != "" || txtGodown.Text != "" || txtQtyTotal.Text != "" || txtQtyRemTotal.Text != "")
            {
                strBranchval = ViewState["hdfLoginBranch"].ToString();

                Dist_Id = Session["dist_id"].ToString();
                IC_Id = Session["issue_id"].ToString();
                strCommodity = ViewState["hdfCommodity"].ToString();

                DateTime dodate = Convert.ToDateTime(DateTime.ParseExact(txtIssuedDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));

                strGodownVal = ViewState["hdfLoginGodown"].ToString();
                Int64 comid = Convert.ToInt64(strGodownVal);

                string openingdate;

                if (dodate >= Convert.ToDateTime(DateTime.ParseExact("01/01/2015", "dd/MM/yyyy", null).ToString("MM/dd/yyyy")))
                {
                    openingdate = "01/01/2015";
                }
                else
                {
                    openingdate = "01/04/2014";
                }

                string pqry = "balanceDetails";

                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        SqlCommand cmdpqty = new SqlCommand(pqry, con);
                        cmdpqty.CommandType = CommandType.StoredProcedure;

                        //cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = Dist_Id;
                        //cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = IC_Id;
                        //cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
                        //cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = strCommodity;
                        ////cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
                        //cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;

                        cmdpqty.Parameters.Add("@cropyear", SqlDbType.VarChar).Value = ViewState["hdfCropYear"].ToString();
                        cmdpqty.Parameters.Add("@godown", SqlDbType.VarChar).Value = strGodownVal;
                        cmdpqty.Parameters.Add("@cmd", SqlDbType.VarChar).Value = strCommodity;
                        cmdpqty.Parameters.Add("@BagsType", SqlDbType.Int).Value = ddlbagstype.SelectedValue.ToString();

                        DataSet ds = new DataSet();
                        SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

                        dr.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Qtybalance"].ToString()), 5);

                            double bags = Convert.ToDouble(ds.Tables[0].Rows[0]["BalanceBags"].ToString());

                            txtBalQtyInGodown.Text = Convert.ToString(stock);
                            txtBalBagInGodown.Text = Convert.ToString(bags);

                            if (stock == 0 || stock < 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस गोदाम में स्टॉक उपलभ नहीं है इसलिए इस गोदाम से चालान जारी नहीं होंगे |'); </script> ");
                                btnRecptSubmit.Enabled = false;
                                return;

                            }
                            if (bags == 0 || bags < 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस गोदाम में Bags उपलभ नहीं है इसलिए इस गोदाम से चालान जारी नहीं होंगे |'); </script> ");
                                btnRecptSubmit.Enabled = false;
                                return;
                            }
                            //txtcurntcap.Text = Convert.ToString(stock);
                            //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

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
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('उपरोक्त ब्रांच में से किसी भी एक ब्रांच का चुनाव करें |'); </script> ");
            }
        }

        if (txtBalBagInGodown.Text != "" && txtBalQtyInGodown.Text != "")
        {
            strBalBagInGodown = double.Parse(txtBalBagInGodown.Text);
            strBalQtyInGodown = double.Parse(txtBalQtyInGodown.Text);

            //if (strBalBagInGodown <= 0 || strBalQtyInGodown <= 0)
            //{
            //    txtIssuedBags.Enabled = txtIssuedQty.Enabled = txtTCNo.Enabled = false;
            //}
            //else
            //{
            txtIssuedBags.Enabled = txtIssuedQty.Enabled = txtTCNo.Enabled = true;
            //}
        }

    }

    protected void chkChange_CheckedChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlGodown.Items.Clear();
        ddlSarrival.SelectedIndex = 0;
        txtIssuedDate.Text = "";
        txtBalBagInGodown.Text = txtBalQtyInGodown.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = "";

        if (chkChange.Checked)
        {
            trBG.Visible = true;
            ddlstackNumber.Items.Clear();
        }
        else
        {
            trBG.Visible = false;
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSarrival.SelectedIndex = 0;
        txtIssuedDate.Text = "";
        txtBalBagInGodown.Text = txtBalQtyInGodown.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = "";

        if (ddlGodown.SelectedIndex > 0)
        {
            ddlstackNumber.Items.Clear();
            GetStackNumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुनें |'); </script> ");
        }
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    public void GetBagsType()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format(" select Bag_Type_ID, BagType from FIN_Bag_Type where Bag_Type_ID!='4' order by BagType desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlbagstype.DataSource = ds.Tables[0];
                    ddlbagstype.DataTextField = "BagType";
                    ddlbagstype.DataValueField = "Bag_Type_ID";
                    ddlbagstype.DataBind();
                    ddlbagstype.Items.Insert(0, "--Select--");
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

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (txtCommodity.Text != "" && txtIC.Text != "")
        {
            strBalBagInGodown = 0;
            strBalQtyInGodown = 0;

            double strIssuedQty = 0, strIssuedBags = 0, strQtyRemTotal = 0, strQtyRemMinus = 0;

            strIssuedQty = double.Parse(txtIssuedQty.Text);
            strBalQtyInGodown = double.Parse(txtBalQtyInGodown.Text);

            strIssuedBags = double.Parse(txtIssuedBags.Text);
            strBalBagInGodown = double.Parse(txtBalBagInGodown.Text);

            strQtyRemTotal = double.Parse(txtQtyRemTotal.Text);

            if (txtIssuedDate.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया दिनांक चुने|'); </script> ");
                return;
            }
            else if (ddlbagstype.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
                return;
            }
            else if (ddlstackNumber.SelectedIndex<=0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Stack Number'); </script> ");
                return;
            }
            //else if (strBalQtyInGodown <= 0)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम में पर्याप्त Balance नहीं है|'); </script> ");
            //    return;
            //}
            //else if (strBalBagInGodown <= 0)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम में पर्याप्त बोरे नहीं है|'); </script> ");
            //    return;
            //}
            //else if (strIssuedQty > strBalQtyInGodown)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Qty की मात्रा कम करें |'); </script> ");
            //    return;
            //}
            //else if (strIssuedQty > strQtyRemTotal)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया बची हुई मात्रा के अनुसार ही आबंटन करें |'); </script> ");
            //    return;
            //}
            //else if (strIssuedBags > strBalBagInGodown)
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया बोरों की मात्रा कम करें |'); </script> ");
            //    return;
            //}
            else
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            if (chkChange.Checked)
                            {
                                strBranchval = strUpdateBranchVal = ddlBranch.SelectedValue.ToString();
                                strGodownVal = strUpdateGodownVal = ddlGodown.SelectedValue.ToString();
                            }
                            else
                            {
                                strBranchval = "00";
                                strGodownVal = "00";
                                strUpdateGodownVal = ViewState["hdfLoginGodown"].ToString();
                                strUpdateBranchVal = ViewState["hdfLoginBranch"].ToString();
                            }

                            //if (ViewState["hdfCommodity"].ToString() == "25")
                            //{
                            //    if (hdfGunnyType.Value == "JUTE")
                            //    {
                            //        TypeofBags = "Jute(SBT)";
                            //    }
                            //    else
                            //    {
                            //        TypeofBags = "PP(HDPE)";
                            //    }
                            //}
                            //else
                            //{
                            //    if (rdbSBT.Checked)
                            //    {
                            //        TypeofBags = "SBT";
                            //    }
                            //    else
                            //    {
                            //        TypeofBags = "HDPE";
                            //    }
                            //}

                            strQtyRemMinus = strQtyRemTotal - strIssuedQty;

                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);

                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();
                            string opid = Session["OperatorId"].ToString();

                            string msaletype = "Other Depot";

                            int month = int.Parse(DateTime.Today.Date.Month.ToString());
                            int year = int.Parse(DateTime.Today.Year.ToString());

                            string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());

                            con.Open();

                            string instr = "";

                            if (ViewState["hdfDispatchMode"].ToString() == "13") //Dispatch By Rack
                            {
                                whr_ID = instr = DCDate = SubDCDate = "";
                                // string selectmax = "select max(DC_No) as MaxDeliveryChallan from DeliveryChallan_MO where Issue_Center='" + IC_Id + "' and CropYear='" + hdfCropYear.Value + "'";

                                string selectmax = "select max(Dispatch_id) as MaxDeliveryChallan,CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate from dbo.SCSC_RailHead_TC where Depot_Id='" + IC_Id + "' and Dist_ID='" + Dist_Id + "'";
                                da = new SqlDataAdapter(selectmax, con);
                                ds = new DataSet();
                                da.Fill(ds);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    whr_ID = ds.Tables[0].Rows[0]["MaxDeliveryChallan"].ToString();
                                    DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                    SubDCDate = DCDate.Substring(2);

                                    if (whr_ID == "")
                                    {
                                        DeliveryChallan = IC_Id + "01";
                                    }
                                    else
                                    {
                                        DeliveryChallan = ((double.Parse(whr_ID)) + 1).ToString();
                                    }
                                }

                                if (DeliveryChallan != "" && SubDCDate != "")
                                {
                                    string Diverted = "N";
                                    DeliveryChallan_MO = "MORK" + SubDCDate;

                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                               "Update TO_AgainstHO_MO Set RemQty='" + strQtyRemMinus + "' where STO_No='" + ViewState["hdfSTO_No"].ToString() + "' ;";

                                    //instr += "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + strIssuedQty + ",Current_Bags=Current_Bags-" + strIssuedBags + " where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + ViewState["hdfCommodity"].ToString() + "'and Scheme_Id='0' and Godown='" + strUpdateGodownVal + "' and Source='" + ddlSarrival.SelectedValue.ToString() + "';";

                                    instr += "Insert Into DeliveryChallan_MO(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,STrans_ID,OperatorID,Distance, IsDiverted, StackNum, StackName) values('" + txtMONo.Text + "','" + ViewState["hdfSMO"].ToString() + "','" + ddlTONo.SelectedItem.ToString() + "','" + ViewState["hdfSTO_No"].ToString() + "','" + DeliveryChallan + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + Dist_Id + "','" + ViewState["hdfToDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','" + ViewState["hdfCropYear"].ToString() + "','" + ViewState["hdfTOEndDate"].ToString() + "','13','N','" + DeliveryChallan_MO + "',GETDATE(),'" + GetIp + "','" + IC_Id + "','" + ViewState["hdfLoginBranch"].ToString() + "','" + ViewState["hdfLoginGodown"].ToString() + "','" + strBranchval + "','" + strGodownVal + "','" + strIssuedQty + "','" + IssuedDate + "','" + strIssuedBags + "','" + ddlbagstype.SelectedValue.ToString() + "','" + txtTCNo.Text + "','" + ddlSarrival.SelectedValue.ToString() + "','" + ViewState["hdfFrmRailHaid"].ToString() + "','" + ViewState["hdfToRailHaid"].ToString() + "','" + txtRackNo.Text + "','" + ViewState["hdfRack_DispDate"].ToString() + "','" + ViewState["hdfTrans_ID"].ToString() + "','" + ViewState["hdfSTrans_ID"].ToString() + "','" + opid + "','" + txtDistance.Text.Trim() + "','" + Diverted + "','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";

                                    instr += "insert into dbo.SCSC_RailHead_TC(State_Id,Dist_ID,Depot_Id,Challan_Date,Dispatch_Godown,Sendto_District,RailHead,Commodity,Scheme,Rack_NO,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,OperatorID,NoTransaction,DispatchID,Cropyear,Branchid, BagsType) values('23','" + Dist_Id + "','" + IC_Id + "','" + IssuedDate + "','" + strUpdateGodownVal + "','" + ViewState["hdfToDist"].ToString() + "','" + ViewState["hdfToRailHaid"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','0','" + txtRackNo.Text + "','" + strIssuedBags + "','" + strIssuedQty + "','" + DeliveryChallan_MO + "','" + txtTCNo.Text + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + time + "','','" + DeliveryChallan + "','N','" + month + "','" + year + "',GETDATE(),'','" + GetIp + "','" + ddlSarrival.SelectedValue.ToString() + "','" + opid + "','N','1','" + ViewState["hdfCropYear"].ToString() + "','" + strUpdateBranchVal + "','"+ddlbagstype.SelectedValue.ToString()+"');";

                                    instr += "insert into dbo.SCSC_Sale_Details(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Sale_Type,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate,DispatchType) values('23','" + Dist_Id + "','" + IC_Id + "','" + ViewState["hdfCommodity"].ToString() + "',0,'" + msaletype + "','" + strIssuedQty + "','" + month + "','" + year + "',GETDATE(),'','','1');";

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                            }

                            if (ViewState["hdfDispatchMode"].ToString() == "12") //Dispatch By Road
                            {
                                whr_ID = instr = DCDate = SubDCDate = "";
                                if (ddlSendGodown.SelectedIndex <= 0)
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
                                    return;
                                }

                                if (txtBalQtyInSendIC.Text != "")
                                {
                                    StrBalQtyInSendIC = double.Parse(txtBalQtyInSendIC.Text);
                                }

                                //if (txtBalQtyInSendIC.Text == "" || StrBalQtyInSendIC > 0)
                                //{
                                //if (strIssuedQty <= StrBalQtyInSendIC || txtBalQtyInSendIC.Text == "")
                                //{

                                string selectmax = "select max(Dispatch_id) as MaxDeliveryChallan,CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate from dbo.SCSC_Truck_challan where Depot_Id='" + IC_Id + "' and Dist_ID='" + Dist_Id + "'";
                                da = new SqlDataAdapter(selectmax, con);
                                ds = new DataSet();
                                da.Fill(ds);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    whr_ID = ds.Tables[0].Rows[0]["MaxDeliveryChallan"].ToString();
                                    DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                    SubDCDate = DCDate.Substring(2);

                                    if (whr_ID == "")
                                    {
                                        DeliveryChallan = IC_Id + "01";
                                    }
                                    else
                                    {
                                        DeliveryChallan = ((double.Parse(whr_ID)) + 1).ToString();
                                    }
                                }

                                if (DeliveryChallan != "" && SubDCDate != "")
                                {
                                    string Diverted = "N";
                                    DeliveryChallan_MO = "MORD" + SubDCDate;

                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                               "Update TO_AgainstHO_MO Set RemQty='" + strQtyRemMinus + "' where STO_No='" + ViewState["hdfSTO_No"].ToString() + "' ";

                                    // instr += "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + strIssuedQty + ",Current_Bags=Current_Bags-" + strIssuedBags + " where District_Id='" + Dist_Id + "'and Depotid='" + IC_Id + "'and Commodity_Id='" + ViewState["hdfCommodity"].ToString() + "'and Scheme_Id='0' and Godown='" + strUpdateGodownVal + "' and Source='" + ddlSarrival.SelectedValue.ToString() + "';";

                                    if (txtBalQtyInSendIC.Text.Trim() != "")
                                    {
                                        instr += "Update RecAgainst_StateMovementOrder set IssuedQty=IssuedQty-" + strIssuedQty + " where RMO='" + ViewState["hdfRMO"].ToString() + "';";
                                    }

                                    instr += "Insert Into DeliveryChallan_MO(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,OperatorID,RecIC,RecBranch,RecGodown, IsDiverted, StackNum, StackName) values('" + txtMONo.Text + "','" + ViewState["hdfSMO"].ToString() + "','" + ddlTONo.SelectedItem.ToString() + "','" + ViewState["hdfSTO_No"].ToString() + "','" + DeliveryChallan + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + Dist_Id + "','" + ViewState["hdfToDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','" + ViewState["hdfCropYear"].ToString() + "','" + ViewState["hdfTOEndDate"].ToString() + "','12','N','" + DeliveryChallan_MO + "',GETDATE(),'" + GetIp + "','" + IC_Id + "','" + ViewState["hdfLoginBranch"].ToString() + "','" + ViewState["hdfLoginGodown"].ToString() + "','" + strBranchval + "','" + strGodownVal + "','" + strIssuedQty + "','" + IssuedDate + "','" + strIssuedBags + "','" + ddlbagstype.SelectedValue.ToString() + "','" + txtTCNo.Text + "','" + ddlSarrival.SelectedValue.ToString() + "','" + opid + "','" + ddlSendIC.SelectedValue.ToString() + "','" + ddlSendBranch.SelectedValue.ToString() + "','" + ddlSendGodown.SelectedValue.ToString() + "','" + Diverted + "','"+ddlstackNumber.SelectedValue.ToString()+"','"+ddlstackNumber.SelectedItem.Text+"');";

                                    instr += "Insert Into DeliveryChallan_MO_WODiv(MoveOrdernum,SMO,TO_No,STO_No,DC_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_MO,CreatedDate,IP,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,Stock_Issued_From,OperatorID,RecIC,RecBranch,RecGodown, IsDiverted,  StackNum, StackName) values('" + txtMONo.Text + "','" + ViewState["hdfSMO"].ToString() + "','" + ddlTONo.SelectedItem.ToString() + "','" + ViewState["hdfSTO_No"].ToString() + "','" + DeliveryChallan + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + Dist_Id + "','" + ViewState["hdfToDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','" + ViewState["hdfCropYear"].ToString() + "','" + ViewState["hdfTOEndDate"].ToString() + "','12','N','" + DeliveryChallan_MO + "',GETDATE(),'" + GetIp + "','" + IC_Id + "','" + ViewState["hdfLoginBranch"].ToString() + "','" + ViewState["hdfLoginGodown"].ToString() + "','" + strBranchval + "','" + strGodownVal + "','" + strIssuedQty + "','" + IssuedDate + "','" + strIssuedBags + "','" + ddlbagstype.SelectedValue.ToString() + "','" + txtTCNo.Text + "','" + ddlSarrival.SelectedValue.ToString() + "','" + opid + "','" + ddlSendIC.SelectedValue.ToString() + "','" + ddlSendBranch.SelectedValue.ToString() + "','" + ddlSendGodown.SelectedValue.ToString() + "','" + Diverted + "','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "');";


                                    instr += "insert into dbo.SCSC_Truck_challan(State_Id,Dist_ID,Depot_Id,Challan_Date,Dispatch_Godown,Sendto_District,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,OperatorID,NoTransaction,DispatchID,Cropyear,Branchid,TO_Number,Sendto_IC, BagsType, StackNum, StackName) values('23','" + Dist_Id + "','" + IC_Id + "','" + IssuedDate + "','" + strUpdateGodownVal + "','" + ViewState["hdfToDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','0','" + strIssuedBags + "','" + strIssuedQty + "','" + DeliveryChallan_MO + "','" + txtTCNo.Text + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + time + "','','" + DeliveryChallan + "','N','" + month + "','" + year + "',GETDATE(),'','" + GetIp + "','" + ddlSarrival.SelectedValue.ToString() + "','" + opid + "','N','1','" + ViewState["hdfCropYear"].ToString() + "','" + strUpdateBranchVal + "','" + ddlTONo.SelectedItem.ToString() + "','" + ViewState["hdfToIC"].ToString() + "','"+ddlbagstype.SelectedValue.ToString()+"', '"+ddlstackNumber.SelectedValue.ToString()+"','"+ddlstackNumber.SelectedItem.Text+"');";

                                    //instr += "insert into dbo.SCSC_Truck_challan_Trans_Log(State_Id,Dist_ID,Depot_Id,Challan_Date,Dispatch_Godown,Sendto_District,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,Month,Year,Updated_Date,IP_Address,Source,Operation,TO_Number,Sendto_IC) values('23','" + Dist_Id + "','" + IC_Id + "','" + IssuedDate + "','" + strUpdateGodownVal + "','" + ViewState["hdfToDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','0','" + strIssuedBags + "','" + strIssuedQty + "','" + DeliveryChallan_MO + "','" + txtTCNo.Text + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + time + "','','" + DeliveryChallan + "','" + month + "','" + year + "',GETDATE(),'" + GetIp + "','" + ddlSarrival.SelectedValue.ToString() + "','I','" + ddlTONo.SelectedItem.ToString() + "','" + ViewState["hdfToIC"].ToString() + "');";

                                    instr += "insert into dbo.SCSC_Sale_Details(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Sale_Type,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate,DispatchType) values('23','" + Dist_Id + "','" + IC_Id + "','" + ViewState["hdfCommodity"].ToString() + "',0,'" + msaletype + "','" + strIssuedQty + "','" + month + "','" + year + "',GETDATE(),'','','1');";

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                    //}
                                    //else
                                    //{
                                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Qty की मात्रा कम करे |'); </script> ");
                                    //    return;
                                    //}
                                    //}
                                    //else
                                    //{
                                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके Receiving Godown का बैलेंस शुन्य होने के कारण आप इस Godown में कमोडिटी नहीं भेज सकते |'); </script> ");
                                    //    return;
                                    //}
                                }
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = false;
                                btnPrint.Enabled = true;

                                Label2.Visible = true;
                                Label2.Text = "Your Challan Number Is : " + DeliveryChallan_MO;

                                Session["DC_MO"] = DeliveryChallan_MO.ToString();
                                Session["CreatedDate"] = "NOTRequired";

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Delivery Challan Number Is " + DeliveryChallan_MO + "'); </script> ");
                                txtCommodity.Text = txtIC.Text = "";

                                GridView1.DataSource = "";
                                GridView1.DataBind();

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                // For tbl_Stock_Registor
                                if (ViewState["hdfDispatchMode"].ToString() == "12") //Dispatch By Road
                                {
                                    string qrystock = "select Sum(Qty_send) as Qty from dbo.SCSC_Truck_challan where Commodity ='" + ViewState["hdfCommodity"].ToString() + "' and Scheme='0' and Dist_ID='" + Dist_Id + "'and Depot_Id='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dsstock = mobj.selectAny(qrystock);

                                    if (dsstock.Tables[0].Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        DataRow drop = dsstock.Tables[0].Rows[0];
                                        float msod = CheckNull(drop["Qty"].ToString());
                                        string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ViewState["hdfCommodity"].ToString() + "' and Scheme_Id='0' and DistrictId ='" + Dist_Id + "'and DepotID='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsopen = mobj.selectAny(qryinsopen);

                                        if (dsopen.Tables[0].Rows.Count == 0)
                                        {

                                            string qrysr = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + ViewState["hdfCommodity"].ToString() + "' and Scheme_Id='0' and DistrictId ='" + Dist_Id + "'and DepotID='23' and Month=" + month + "and Year=" + year;
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dssr = mobj.selectAny(qrysr);

                                            if (dssr.Tables[0].Rows.Count == 0)
                                            {
                                                string chkopenss = "Select Sum(Current_Balance) as Current_Balance from dbo.issue_opening_balance where District_Id='" + Dist_Id + "'and Depotid='23' and Commodity_Id='" + ViewState["hdfCommodity"].ToString() + "' and Scheme_Id='0'";
                                                mobj = new MoveChallan(ComObj);
                                                DataSet dsqry = mobj.selectAny(chkopenss);
                                                if (dsqry == null)
                                                {

                                                }

                                                else
                                                {

                                                }
                                                {
                                                    DataRow drss = dsqry.Tables[0].Rows[0];
                                                    float sropen = CheckNull(drss["Current_Balance"].ToString());
                                                    string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks,DispatchType) Values('" + Dist_Id + "','23','" + ViewState["hdfCommodity"].ToString() + "','0'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtBalQtyInGodown.Text) + "," + 0 + "," + 0 + "," + month + "," + year + ",'','1')";
                                                    cmd.CommandText = qryinsr;
                                                    if (con.State != ConnectionState.Open)
                                                    {
                                                        con.Open();
                                                    }
                                                    cmd.ExecuteNonQuery();
                                                    if (con.State != ConnectionState.Closed)
                                                    {
                                                        con.Close();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string qryinsU = "update dbo.tbl_Stock_Registor set Sale_otherg=" + msod + " where Commodity_Id ='" + ViewState["hdfCommodity"].ToString() + "'and Scheme_Id='0' and DistrictId='" + Dist_Id + "'and DepotID='" + IC_Id + "'and Month=" + month + "and Year=" + year;
                                            cmd.CommandText = qryinsU;

                                            if (con.State != ConnectionState.Open)
                                            {
                                                con.Open();
                                            }

                                            cmd.ExecuteNonQuery();

                                            if (con.State != ConnectionState.Closed)
                                            {
                                                con.Close();
                                            }
                                        }
                                    }
                                }
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print_DO_PDSMO.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/MovementOrderHome.aspx");
    }

    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;
    }
    protected void txtIssuedQty_TextChanged(object sender, EventArgs e)
    {
        if ((Convert.ToDouble(txtBalQtyInGodown.Text) >= Convert.ToDouble(txtIssuedQty.Text)) && (Convert.ToDouble(txtIssuedQty.Text)<= Convert.ToDouble(txtQtyRemTotal.Text)))
        {
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी की गयी मात्रा गोदाम में उपलभ मात्रा से जायदा नहीं हो सकती |'); </script> ");
            txtIssuedQty.Text = "";
            txtIssuedQty.Focus();
            return;
            
        }
    }
    protected void txtIssuedBags_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDouble(txtBalBagInGodown.Text) >= Convert.ToDouble(txtIssuedBags.Text))
        {
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी किये गये बोरे गोदाम में उपलभ बोरों से जायदा नहीं हो सकते |'); </script> ");
            txtIssuedBags.Text = "";
            txtIssuedBags.Focus();
            return;
            
        }
    }
}