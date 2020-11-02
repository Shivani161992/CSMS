using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_DeliveryChallanNew_ByRack : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlDataAdapter da, da1, da2;
    DataSet ds, ds1, ds2;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string Dist_Id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Dist_Id = Session["dist_id"].ToString();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                rdbSBT.Checked = true;
                GetTransporterDetails();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }

    public void GetTransporterDetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select distinct TO_No from TO_AgainstHO_MO where FrmDist='" + Dist_Id + "' and ToDist='00' and ModeofDispatch='13' and (DATEADD(DAY,60,CreatedDate))>=Getdate() and ToRailHaid='00' and Rack_No='00' and ModeofDist IN('Self','Other','Both') ";

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
        txtMONo.Text = txtTransporterName.Text = txtCommodity.Text = txtRailHead.Text = txtFrmDist.Text = txtToDist.Text = txtBalBagInRack.Text = txtBalQtyInRack.Text = txtIssuedBags.Text = txtIssuedQty.Text = txtTCNo.Text = txtIssuedDate.Text = txtBalQtyInSendIC.Text = "";

        ddlSendIC.Items.Clear();
        ddlSendBranch.Items.Clear();
        ddlSendGodown.Items.Clear();
        ddlConsinmentNo.Items.Clear();

        if (ddlTONo.SelectedIndex > 0)
        {
            GetAllData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया परिवहन आदेश क्रमांक चुने|'); </script> ");
        }
    }

    public void GetAllData()
    {
        Dist_Id = Session["dist_id"].ToString();

        ViewState["hdfSMO"] = ViewState["hdfTransporter_ID"] = ViewState["hdfFrmDist"] = ViewState["hdfTo_MultiDist"] = ViewState["hdfCommodity"] = ViewState["hdfCropYear"] = ViewState["hdfTOEndDate"] = ViewState["hdfDispatchMode"] = ViewState["hdfModeofDist"] = ViewState["hdfIssue_Center"] = ViewState["hdfRMO"] = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select (select DepotName from tbl_MetaData_DEPOT where DepotID=TOMO.Issue_Center) ICName,Issue_Center,ModeofDist,MoveOrdernum,SMO,STO_No,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID =TOMO.Transporter_ID and Distt_ID='" + Dist_Id + "' and Transport_ID in ('10','11','6') order by Transporter_Name desc) TransporterName, Transporter_ID,(select district_name From pds.districtsmp where district_code= TOMO.FrmDist) FrmDistName,FrmDist,(select district_name From pds.districtsmp where district_code= TOMO.To_MultiDist) To_MultiDistName,To_MultiDist,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=TOMO.Commodity) ComdtyName,Commodity,RequiredQuantity,RemQty,CropYear,TOEndDate,ModeofDispatch,Branch,Godown from TO_AgainstHO_MO As TOMO where TO_No='" + ddlTONo.SelectedValue.ToString() + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and ModeofDispatch='13' and RemQty>0 and ToRailHaid='00'  and Rack_No='00' and ModeofDist IN('Self','Other','Both')  order by Issue_Center";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    txtTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();

                    txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    txtToDist.Text = ds.Tables[0].Rows[0]["To_MultiDistName"].ToString();

                    ViewState["hdfSMO"] = ds.Tables[0].Rows[0]["SMO"].ToString();
                    ViewState["hdfTransporter_ID"] = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
                    ViewState["hdfFrmDist"] = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    ViewState["hdfTo_MultiDist"] = ds.Tables[0].Rows[0]["To_MultiDist"].ToString();
                    ViewState["hdfCommodity"] = ds.Tables[0].Rows[0]["Commodity"].ToString();
                    ViewState["hdfCropYear"] = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    ViewState["hdfTOEndDate"] = ds.Tables[0].Rows[0]["TOEndDate"].ToString();
                    ViewState["hdfDispatchMode"] = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
                    ViewState["hdfModeofDist"] = ds.Tables[0].Rows[0]["ModeofDist"].ToString();
                    ViewState["hdfIssue_Center"] = ds.Tables[0].Rows[0]["Issue_Center"].ToString();

                    //Code For Limited Issue Centre According To Movement Plan
                    /* string checkIC = "";

                    if ((ViewState["hdfModeofDist"].ToString() == "Self" && ViewState["hdfIssue_Center"].ToString() != "00") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfIssue_Center"].ToString() != "00")) //Own Dist
                    {
                        ddlSendIC.DataSource = ds.Tables[0];

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = string.Empty;
                            string id = string.Empty;

                            if (ds.Tables[0].Rows[i]["Issue_Center"].ToString() != checkIC)
                            {
                                id = ds.Tables[0].Rows[i]["Issue_Center"].ToString();
                                name = ds.Tables[0].Rows[i]["ICName"].ToString();
                                ddlSendIC.Items.Add(new ListItem(name, id));
                                checkIC = ds.Tables[0].Rows[i]["Issue_Center"].ToString();
                            }
                        }
                        ddlSendIC.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        string select1 = "select distinct (select DepotName from tbl_MetaData_DEPOT where DepotID=Rec.Issue_Center) ICName, Issue_Center From RecAgainst_StateMovementOrder As Rec where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and FrmDist='" + ViewState["hdfFrmDist"].ToString() + "' and  ToDist='" + ViewState["hdfTo_MultiDist"] + "' and ModeofDispatch='13' and IssuedQty>0";
                        da1 = new SqlDataAdapter(select1, con);

                        ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            ddlSendIC.DataSource = ds1.Tables[0];
                            ddlSendIC.DataTextField = "ICName";
                            ddlSendIC.DataValueField = "Issue_Center";
                            ddlSendIC.DataBind();
                            ddlSendIC.Items.Insert(0, "--Select--");
                        }
                    } */

                    //Code For All Issue Centre
                    string select1 = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ViewState["hdfTo_MultiDist"].ToString() + "' order by DepotName";
                    da1 = new SqlDataAdapter(select1, con);

                    ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlSendIC.DataSource = ds1.Tables[0];
                        ddlSendIC.DataTextField = "DepotName";
                        ddlSendIC.DataValueField = "DepotID";
                        ddlSendIC.DataBind();
                        ddlSendIC.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
                    }

                    GetConsinmentNo();
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

    public void GetConsinmentNo()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "Select Consinment_No From RackReceived where MoveOrdernum='" + txtMONo.Text + "' and To_RackDist='" + Dist_Id + "' and Rem_Qty>0 and Commodity='" + ViewState["hdfCommodity"].ToString() + "'";
                da2 = new SqlDataAdapter(select, con);
                ds2 = new DataSet();
                da2.Fill(ds2);

                if (ds2.Tables[0].Rows.Count > 0 && ds2.Tables.Count > 0)
                {
                    ddlConsinmentNo.DataSource = ds2.Tables[0];
                    ddlConsinmentNo.DataTextField = "Consinment_No";
                    ddlConsinmentNo.DataValueField = "Consinment_No";
                    ddlConsinmentNo.DataBind();
                    ddlConsinmentNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Consinment Number Is Not Available'); </script> ");
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

    protected void ddlConsinmentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRailHead.Text = txtBalQtyInRack.Text = txtBalBagInRack.Text = "";
        if (ddlConsinmentNo.SelectedIndex > 0)
        {
            GetConsinmentData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Consinment Number'); </script> ");
        }
    }

    public void GetConsinmentData()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                select = "Select Rem_Qty,Rem_Bags,(Select RailHead_Name From tbl_Rail_Head where RailHead_Code=ToRailHaid) As ToRailHaidName From RackReceived where MoveOrdernum='" + txtMONo.Text + "' and To_RackDist='" + Dist_Id + "' and Rem_Qty>0 and Commodity='" + ViewState["hdfCommodity"].ToString() + "' and Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    txtBalQtyInRack.Text = ds.Tables[0].Rows[0]["Rem_Qty"].ToString();
                    txtBalBagInRack.Text = ds.Tables[0].Rows[0]["Rem_Bags"].ToString();
                    txtRailHead.Text = ds.Tables[0].Rows[0]["ToRailHaidName"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Consinment Data Is Not Available'); </script> ");
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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sending Issue Centre'); </script> ");
        }
    }

    public void GetSendBranch()
    {
        Dist_Id = Session["dist_id"].ToString();

        //Code For Limited Branch According To Movement Plan
        /*
        string name = string.Empty;
        string id = string.Empty;

        
         using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select distinct Branch From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and ToDist='" + Dist_Id + "' and ModeofDispatch='12' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and (IssuedQty>0 or IssuedQty Is Null)";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
                        {
                            try
                            {
                                con_MPStorage.Open();
                                string GetBranch = "select DepotName from tbl_MetaData_DEPOT where BranchId='" + ds.Tables[0].Rows[i]["Branch"].ToString() + "'";
                                da1 = new SqlDataAdapter(GetBranch, con_MPStorage);

                                ds1 = new DataSet();
                                da1.Fill(ds1);
                                if (ds1 != null)
                                {
                                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                                    {
                                        id = ds.Tables[0].Rows[i]["Branch"].ToString();
                                        name = ds1.Tables[0].Rows[0]["DepotName"].ToString();
                                        ddlSendBranch.Items.Add(new ListItem(name, id));
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

                    ddlSendBranch.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Branch Is Not Available'); </script> ");
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
            }  */

        //Code For All Branch
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddlSendIC.SelectedValue.ToString());
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
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
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

        if (ddlSendBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
        }
    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='{0}'", ddlSendBranch.SelectedValue.ToString());
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
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
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
        hdfSTO_No.Value = "";

        if ((ViewState["hdfModeofDist"].ToString() == "Other") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfIssue_Center"].ToString() == "00")) //Other + Both With Other Dist
        {
            if (ddlSendGodown.SelectedIndex > 0)
            {
                GetDataMovementPlan();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            }
        }
        else if ((ViewState["hdfModeofDist"].ToString() == "Self") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfIssue_Center"].ToString() != "00"))
        {
            if (ddlSendGodown.SelectedIndex > 0)
            {
                GetDataTO();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            }
        }
    }

    public void GetDataMovementPlan()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select IssuedQty,RMO From RecAgainst_StateMovementOrder where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and FrmDist='" + ViewState["hdfFrmDist"].ToString() + "' and ToDist='" + ViewState["hdfTo_MultiDist"].ToString() + "' and ModeofDispatch='13' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "'";
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

    public void GetDataTO()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select RemQty,STO_No From TO_AgainstHO_MO where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + ddlTONo.SelectedItem.ToString() + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and ModeofDispatch='13' and Issue_Center!='00' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "' and To_MultiDist='" + Dist_Id + "' and ModeofDist IN ('Both','Self')";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtBalQtyInSendIC.Text = ds.Tables[0].Rows[0]["RemQty"].ToString();
                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
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
        double dblBalQtyInRack = 0, dblBalBagInRack = 0, dblIssuedQty = 0, dblIssuedBags=0, dblTotalRemQty = 0, dblTotalRemBags = 0;

        dblBalQtyInRack = double.Parse(txtBalQtyInRack.Text.Trim());
        dblBalBagInRack = double.Parse(txtBalBagInRack.Text.Trim());
        dblIssuedQty = double.Parse(txtIssuedQty.Text.Trim());
        dblIssuedBags = double.Parse(txtIssuedBags.Text.Trim());

        dblTotalRemQty = dblBalQtyInRack-dblIssuedQty;
        dblTotalRemBags = dblBalBagInRack - dblIssuedBags;

        if (ddlSendGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            return;
        }
        if (dblIssuedQty > dblBalQtyInRack)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Qty की मात्रा कम करे|'); </script> ");
            return;
        }
        if (dblIssuedBags > dblBalBagInRack)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Bags की मात्रा कम करे|'); </script> ");
            return;
        }
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print_DO_PDSMO.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }


}