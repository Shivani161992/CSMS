using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_DeliveryChallan_ByRailRack : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlDataAdapter da, da1, da2;
    DataSet ds, ds1, ds2;
    SqlCommand cmd;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string Dist_Id = "", TypeofBags = "";

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
                GetBookNo();
                //GetBagsType();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }


    public void GetBookNo()
    {
        ddlBookNo.Items.Clear();
        ddlPageNo.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Book_No From TruckChallan_Book where District='" + Dist_Id + "' and Rem_Page>0";
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Book Number Is Not Available On Your District'); </script> ");
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
        if (ddlBookNo.SelectedIndex > 0)
        {
            GetPageNumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Book Number'); </script> ");
        }
    }

    //public void GetPageNumber()
    //{
    //    Dist_Id = Session["dist_id"].ToString();

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            string select = "Select Rem_Page,Upto_Page From TruckChallan_Book where District='" + Dist_Id + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and Rem_Page!=Upto_Page";
    //            da = new SqlDataAdapter(select, con);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                int UsedPage = int.Parse(ds.Tables[0].Rows[0]["Rem_Page"].ToString());
    //                int UptoPage = int.Parse(ds.Tables[0].Rows[0]["Upto_Page"].ToString());

    //                for (int i = UsedPage; i < UptoPage; i++)
    //                {
    //                    int DisplayUsedPage = i + 1;
    //                    ddlPageNo.Items.Add(new ListItem(DisplayUsedPage.ToString(), DisplayUsedPage.ToString()));
    //                }

    //                ddlPageNo.Items.Insert(0, "--Select--");
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Page Number Is Not Available'); </script> ");
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

    public void GetPageNumber()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Upto_Page From TruckChallan_Book where District='" + Dist_Id + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and Rem_Page>0";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int UptoPage = int.Parse(ds.Tables[0].Rows[0]["Upto_Page"].ToString());

                    for (int i = 0; i < UptoPage; i++)
                    {
                        int DisplayUsedPage = i + 1;
                        ddlPageNo.Items.Add(new ListItem(DisplayUsedPage.ToString(), DisplayUsedPage.ToString()));
                    }


                    string checkExistPage = "Select Page_No From DeliveryChallan_MO where FrmDist='" + Dist_Id + "' and Book_No='" + ddlBookNo.SelectedItem.ToString() + "'";
                    da1 = new SqlDataAdapter(checkExistPage, con);

                    ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            string checkpage = ds1.Tables[0].Rows[i][0].ToString();

                            if (ddlPageNo.Items.FindByValue(checkpage) != null)
                            {
                                ddlPageNo.Items.FindByValue(checkpage).Enabled = false;
                            }
                        }
                    }


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

    public void GetTransporterDetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select distinct TO_No from TO_AgainstHO_MO where FrmDist='" + Dist_Id + "' and ToDist='00' and ModeofDispatch='13' and ((TOEndDate+1>=Getdate()) or ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and ToRailHaid='00' and Rack_No='00' and ModeofDist IN('Self','Other','Both') ";

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
        txtMONo.Text = txtTransporterName.Text = txtCommodity.Text = txtFrmDist.Text = txtToDist.Text = txtBalQtyInSendIC.Text = txtDistance.Text = txtRailHead.Text = txtBalQtyInRack.Text = txtBalBagInRack.Text = txtIssuedQty.Text = txtIssuedBags.Text = txtTCNo.Text = txtIssuedDate.Text = "";

        ddlSendIC.Items.Clear();
        ddlSendBranch.Items.Clear();
        ddlSendGodown.Items.Clear();
        ddlPageNo.Items.Clear();
        ddlConsinmentNo.Items.Clear();

        if (ddlBookNo.Items.Count > 0)
        {
            ddlBookNo.SelectedIndex = 0;
        }

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
        hdfGunnyType.Value = "";
        string IsCancelled = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select distinct SMO.IsCancelled,SMO.GunnyType,(select DepotName from tbl_MetaData_DEPOT where DepotID=TOMO.Issue_Center) ICName,TOMO.Issue_Center,TOMO.ModeofDist,TOMO.MoveOrdernum,TOMO.SMO,TOMO.STO_No,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID =TOMO.Transporter_ID and Distt_ID='" + Dist_Id + "' and Transport_ID in ('10','1') order by Transporter_Name desc) TransporterName, TOMO.Transporter_ID,(select district_name From pds.districtsmp where district_code= TOMO.FrmDist) FrmDistName,TOMO.FrmDist,(select district_name From pds.districtsmp where district_code= TOMO.To_MultiDist) To_MultiDistName,TOMO.To_MultiDist,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=TOMO.Commodity) ComdtyName,TOMO.Commodity,TOMO.RequiredQuantity,TOMO.RemQty,TOMO.CropYear,TOMO.TOEndDate,TOMO.ModeofDispatch,TOMO.Branch,TOMO.Godown from TO_AgainstHO_MO As TOMO Left Join StateMovementOrder SMO ON(SMO.MoveOrdernum=TOMO.MoveOrdernum) where TOMO.TO_No='" + ddlTONo.SelectedValue.ToString() + "' and TOMO.FrmDist='" + Dist_Id + "' and TOMO.ToDist='00' and TOMO.ModeofDispatch='13' and TOMO.ToRailHaid='00'  and TOMO.Rack_No='00' and TOMO.ModeofDist IN('Self','Other','Both')  order by Issue_Center";
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

                        hdfGunnyType.Value = ds.Tables[0].Rows[0]["GunnyType"].ToString();

                        if (ViewState["hdfCommodity"].ToString() == "25")
                        {
                            lblQtls.Text = "(Bales)";
                            lblQtls0.Text = "(Bales)";
                            lblQtls1.Text = "(Bales)";
                            txtIssuedBags.Text = "0";
                            txtIssuedBags.Enabled = false;

                          rdbHDPE.Visible = rdbSBT.Visible = false;
                            txtGunnyType.Visible = true;

                            if (hdfGunnyType.Value == "JUTE")
                            {
                                txtGunnyType.Text = "Jute(SBT)";
                            }
                            else
                            {
                                txtGunnyType.Text = "PP(HDPE)";
                            }
                        }
                        else
                        {
                            lblQtls.Text = "(Qtls)";
                            lblQtls0.Text = "(Qtls)";
                            lblQtls1.Text = "(Qtls)";
                            txtIssuedBags.Text = "";
                            txtIssuedBags.Enabled = true;

                          rdbHDPE.Visible = rdbSBT.Visible = true;
                            txtGunnyType.Visible = false;
                            txtGunnyType.Text = "";
                        }

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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination Issue Centre'); </script> ");
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
        txtBalQtyInSendIC.Text = "";

        if (ddlSendBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination Branch'); </script> ");
        }
    }

    public void GetGodown()
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
        hdfSTO_No.Value = hdfSubSMO.Value = "";

        if (ViewState["hdfCommodity"].ToString() == "25")
        {
            if ((ViewState["hdfModeofDist"].ToString() == "Other") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfFrmDist"].ToString() != ViewState["hdfTo_MultiDist"].ToString()))
            {
                if (ddlSendGodown.SelectedIndex > 0)
                {
                    GetGunnyDataMovementPlan();
                    //GetDataTOByRack();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination Godown'); </script> ");
                }
            }
            else if ((ViewState["hdfModeofDist"].ToString() == "Self") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfFrmDist"].ToString() == ViewState["hdfTo_MultiDist"].ToString()))
            {
                if (ddlSendGodown.SelectedIndex > 0)
                {
                    GetGunnyDataTO();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination Godown'); </script> ");
                }
            }

        }
        else
        {
            if ((ViewState["hdfModeofDist"].ToString() == "Other") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfIssue_Center"].ToString() == "00")) //Other + Both With Other Dist
            {
                if (ddlSendGodown.SelectedIndex > 0)
                {
                    GetDataMovementPlan();
                    GetDataTOByRack();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination Godown'); </script> ");
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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination Godown'); </script> ");
                }
            }
        }
    }

    public void GetGunnyDataMovementPlan()
    {
        Dist_Id = Session["dist_id"].ToString();

        txtBalQtyInSendIC.Text = "";
        ViewState["hdfRMO"] = "";
        hdfSTO_No.Value = hdfSubSMO.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                select = "Select TOMO.RemQty,RMO.RMO,TOMO.STO_No,TOMO.SubSMO From TO_AgainstHO_MO As TOMO left join RecAgainst_StateMovementOrder As RMO ON(TOMO.SMO=RMO.SMO and TOMO.Issue_Center=RMO.Issue_Center and TOMO.Branch=RMO.Branch and TOMO.Godown=RMO.Godown) where TOMO.TO_No='" + ddlTONo.SelectedItem.ToString() + "' and TOMO.FrmDist='" + Dist_Id + "' and TOMO.To_MultiDist='" + ViewState["hdfTo_MultiDist"].ToString() + "' and TOMO.Commodity='25' and TOMO.CropYear='" + ViewState["hdfCropYear"].ToString() + "' and TOMO.ModeofDist IN('Other','Both') and TOMO.Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and TOMO.Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and TOMO.Godown='" + ddlSendGodown.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtBalQtyInSendIC.Text = ds.Tables[0].Rows[0]["RemQty"].ToString();
                    ViewState["hdfRMO"] = ds.Tables[0].Rows[0]["RMO"].ToString();

                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
                    hdfSubSMO.Value = ds.Tables[0].Rows[0]["SubSMO"].ToString();
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

    public void GetGunnyDataTO()
    {
        txtBalQtyInSendIC.Text = "";
        hdfSTO_No.Value = "";

        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select RemQty,STO_No From TO_AgainstHO_MO where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + ddlTONo.SelectedItem.ToString() + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and Commodity='25' and CropYear='" + ViewState["hdfCropYear"].ToString() + "' and ModeofDispatch='13' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "' and To_MultiDist='" + Dist_Id + "' and ModeofDist IN ('Both','Self') order by RemQty desc";
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

    public void GetDataTO()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select RemQty,STO_No From TO_AgainstHO_MO where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + ddlTONo.SelectedItem.ToString() + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and ModeofDispatch='13' and Issue_Center!='00' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "' and To_MultiDist='" + Dist_Id + "' and ModeofDist IN ('Both','Self') order by RemQty desc";
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

    public void GetDataTOByRack()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select STO_No,SubSMO From TO_AgainstHO_MO where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + ddlTONo.SelectedItem.ToString() + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and ModeofDispatch='13' and Issue_Center='00' and Branch='00' and Godown='00' and To_MultiDist='" + ViewState["hdfTo_MultiDist"].ToString() + "' and ModeofDist IN ('Both','Other')";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
                    hdfSubSMO.Value = ds.Tables[0].Rows[0]["SubSMO"].ToString();
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

    //public void GetBagsType()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = string.Format(" select Bag_Type_ID, BagType from FIN_Bag_Type where Bag_Type_ID!='4' order by BagType desc");
    //            da = new SqlDataAdapter(select, con);

    //            ds = new DataSet();
    //            da.Fill(ds, "PaddyMilling_CropYear");
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

    //                ddlbagstype.DataSource = ds.Tables[0];
    //                ddlbagstype.DataTextField = "BagType";
    //                ddlbagstype.DataValueField = "Bag_Type_ID";
    //                ddlbagstype.DataBind();
    //                ddlbagstype.Items.Insert(0, "--Select--");
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

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        
        double dblBalQtyInRack = 0, dblBalBagInRack = 0, dblIssuedQty = 0, dblIssuedBags = 0, dblTotalRemQty = 0, dblTotalRemBags = 0;

        dblBalQtyInRack = double.Parse(txtBalQtyInRack.Text.Trim());
        dblBalBagInRack = double.Parse(txtBalBagInRack.Text.Trim());
        dblIssuedQty = double.Parse(txtIssuedQty.Text.Trim());
        dblIssuedBags = double.Parse(txtIssuedBags.Text.Trim());

        dblTotalRemQty = dblBalQtyInRack - dblIssuedQty;
        dblTotalRemBags = dblBalBagInRack - dblIssuedBags;

        if (ddlSendGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            return;
        }
        else if (ddlPageNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Page Number'); </script> ");
            return;
        }
        else if (dblIssuedQty <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issue Qty Is Not Allow 0 or Less Than 0'); </script> ");
            return;
        }
        else if (dblIssuedBags <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Issue Bags Is Not Allow 0 or Less Than 0'); </script> ");
            return;
        }
        //else if (ddlbagstype.SelectedIndex <= 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
        //    return;
        //}
        //if (dblIssuedQty > dblBalQtyInRack)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Qty की मात्रा कम करे|'); </script> ");
        //    return;
        //}
        //if (dblIssuedBags > dblBalBagInRack)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Bags की मात्रा कम करे|'); </script> ");
        //    return;
        //}
        else
        {
            Dist_Id = Session["dist_id"].ToString();

            ClientIP objClientIP = new ClientIP();
            string GetIp = (objClientIP.GETIP());

            ConvertServerDate ServerDate = new ConvertServerDate();
            string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);

            if (ViewState["hdfCommodity"].ToString() == "25")
            {
                if (hdfGunnyType.Value == "JUTE")
                {
                    TypeofBags = "3"; //Jute(SBT)
                }
                else
                {
                    TypeofBags = "1";//PP(HDPE)
                }
            }
            else
            {
                if (rdbSBT.Checked)
                {
                    TypeofBags = "2";//SBT
                }
                else
                {
                    TypeofBags = "1";//HDPE
                }
            }

            if (txtFrmDist.Text.Trim() != "" && txtToDist.Text.Trim() != "")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string selectmax = "", DeliveryChallan_MO = "", instr = "", DCDate = "", SubDCDate = "";

                            selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                            da = new SqlDataAdapter(selectmax, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                SubDCDate = DCDate.Substring(2);
                            }

                            if (SubDCDate != "")
                            {
                                //selectmax = "select Max(DC_No) as MaxDC_No From DeliveryChallan_MO where FrmDist='" + Dist_Id + "' and ToDist='" + ViewState["hdfTo_MultiDist"].ToString() + "' and ModeofDispatch='13' and Issue_Center='0000' and Default_Branch='0000' and Default_Godown='0000' ";
                                //cmd = new SqlCommand(selectmax, con);
                                //da = new SqlDataAdapter(cmd);
                                //ds = new DataSet();
                                //da.Fill(ds);

                                //whr_ID = ds.Tables[0].Rows[0]["MaxDC_No"].ToString();
                                //if (whr_ID == "")
                                //{
                                //    DeliveryChallan = "23" + Dist_Id + ViewState["hdfTo_MultiDist"].ToString() + "1";
                                //}
                                //else
                                //{
                                //    string getlastdigit = whr_ID.Substring(6);
                                //    string getfirstdigit = whr_ID.Substring(0, 6);
                                //    DeliveryChallan = getfirstdigit + ((double.Parse(getlastdigit)) + 1).ToString();
                                //} 

                                if (ViewState["hdfCommodity"].ToString() == "25")
                                {
                                    DeliveryChallan_MO = "MGRTR" + SubDCDate;

                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                       "Update TruckChallan_Book set Rem_Page=Rem_Page-1 where Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and District='" + Dist_Id + "';";

                                    instr += "Update RackReceived set Rem_Qty='" + dblTotalRemQty + "',Rem_Bags='" + dblTotalRemBags + "' where Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "';";

                                    if (txtBalQtyInSendIC.Text.Trim() != "")
                                    {
                                        instr += "Update TO_AgainstHO_MO Set RemQty=RemQty-'" + dblIssuedQty + "' where STO_No='" + hdfSTO_No.Value + "' ;";
                                    }

                                    // Code For Other Dist & Both with Other Dist
                                    if ((ViewState["hdfModeofDist"].ToString() == "Other") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfFrmDist"].ToString() != ViewState["hdfTo_MultiDist"].ToString()))
                                    {
                                        if (txtBalQtyInSendIC.Text.Trim() != "")
                                        {
                                            instr += "Update RecAgainst_StateMovementOrder set IssuedQty=IssuedQty-" + dblIssuedQty + " where RMO='" + ViewState["hdfRMO"].ToString() + "' ;";
                                        }
                                    }

                                    instr += "Insert Into DeliveryChallan_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_No,DC_MO,CreatedDate,IP,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Distance,SubSMO,RMO,Book_No,Page_No,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,FrmRailHaid,RecIC,RecBranch,RecGodown,Consinment_No) values('" + txtMONo.Text + "','" + ViewState["hdfSMO"].ToString() + "','" + ddlTONo.SelectedItem.ToString() + "','" + hdfSTO_No.Value + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + Dist_Id + "','" + ViewState["hdfTo_MultiDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','" + ViewState["hdfCropYear"].ToString() + "','" + ViewState["hdfTOEndDate"].ToString() + "','" + ViewState["hdfDispatchMode"].ToString() + "','N','0000','" + DeliveryChallan_MO + "',GETDATE(),'" + GetIp + "','0000','0000','0000','0000','0000','" + txtDistance.Text.Trim() + "','" + hdfSubSMO.Value + "','" + ViewState["hdfRMO"].ToString() + "','" + ddlBookNo.SelectedItem.ToString() + "','" + ddlPageNo.SelectedItem.ToString() + "','" + dblIssuedQty + "','" + IssuedDate + "','" + dblIssuedBags + "','" + TypeofBags+ "','" + txtTCNo.Text.Trim() + "','" + hdfFrmRailHaid.Value + "','" + ddlSendIC.SelectedValue.ToString() + "','" + ddlSendBranch.SelectedValue.ToString() + "','" + ddlSendGodown.SelectedValue.ToString() + "','" + ddlConsinmentNo.SelectedItem.ToString() + "');";

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                                else
                                {
                                    DeliveryChallan_MO = "MORTR" + SubDCDate;

                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                       "Update TruckChallan_Book set Rem_Page=Rem_Page-1 where Book_No='" + ddlBookNo.SelectedItem.ToString() + "' and District='" + Dist_Id + "';";

                                    instr += "Update RackReceived set Rem_Qty='" + dblTotalRemQty + "',Rem_Bags='" + dblTotalRemBags + "' where Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "';";


                                    // Code For Other Dist & Both with Other Dist
                                    if ((ViewState["hdfModeofDist"].ToString() == "Other") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfIssue_Center"].ToString() == "00"))
                                    {
                                        instr += "Update TO_AgainstHO_MO Set RemQty=RemQty-'" + dblIssuedQty + "' where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + ddlTONo.SelectedItem.ToString() + "' and STO_No='" + hdfSTO_No.Value + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and CropYear='" + ViewState["hdfCropYear"].ToString() + "' and ModeofDispatch='13' and Issue_Center='00' and Branch='00' and Godown='00' and SubSMO='" + hdfSubSMO.Value + "' and ModeofDist IN('Both','Other');";

                                        if (txtBalQtyInSendIC.Text.Trim() != "")
                                        {
                                            instr += "Update RecAgainst_StateMovementOrder set IssuedQty=IssuedQty-" + dblIssuedQty + " where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and RMO='" + ViewState["hdfRMO"].ToString() + "' and Issue_Center='" + ddlSendIC.SelectedValue.ToString() + "' and Branch='" + ddlSendBranch.SelectedValue.ToString() + "' and Godown='" + ddlSendGodown.SelectedValue.ToString() + "';";
                                        }
                                    }

                                    // Code For Self Dist & Both with Self 
                                    else if ((ViewState["hdfModeofDist"].ToString() == "Self") || (ViewState["hdfModeofDist"].ToString() == "Both" && ViewState["hdfIssue_Center"].ToString() != "00"))
                                    {
                                        if (txtBalQtyInSendIC.Text.Trim() != "")
                                        {
                                            instr += "Update TO_AgainstHO_MO Set RemQty=RemQty-'" + dblIssuedQty + "' where MoveOrdernum='" + txtMONo.Text + "' and SMO='" + ViewState["hdfSMO"].ToString() + "' and TO_No='" + ddlTONo.SelectedItem.ToString() + "' and STO_No='" + hdfSTO_No.Value + "' and FrmDist='" + Dist_Id + "' and ToDist='00' and CropYear='" + ViewState["hdfCropYear"].ToString() + "' and ModeofDispatch='13' and Issue_Center!='00' and Branch!='00' and Godown!='00' and To_MultiDist ='" + Dist_Id + "' and ModeofDist IN('Both','Self');";
                                        }
                                    }

                                    instr += "Insert Into DeliveryChallan_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,CropYear,TOEndDate,ModeofDispatch,IsReceived,DC_No,DC_MO,CreatedDate,IP,Issue_Center,Default_Branch,Default_Godown,Change_Branch,Change_Godown,Distance,SubSMO,RMO,Book_No,Page_No,Issued_Qty,Issued_Date,Issued_Bags,Bags_Type,Truck_No,FrmRailHaid,RecIC,RecBranch,RecGodown,Consinment_No) values('" + txtMONo.Text + "','" + ViewState["hdfSMO"].ToString() + "','" + ddlTONo.SelectedItem.ToString() + "','" + hdfSTO_No.Value + "','" + ViewState["hdfTransporter_ID"].ToString() + "','" + Dist_Id + "','" + ViewState["hdfTo_MultiDist"].ToString() + "','" + ViewState["hdfCommodity"].ToString() + "','" + ViewState["hdfCropYear"].ToString() + "','" + ViewState["hdfTOEndDate"].ToString() + "','" + ViewState["hdfDispatchMode"].ToString() + "','N','0000','" + DeliveryChallan_MO + "',GETDATE(),'" + GetIp + "','0000','0000','0000','0000','0000','" + txtDistance.Text.Trim() + "','" + hdfSubSMO.Value + "','" + ViewState["hdfRMO"].ToString() + "','" + ddlBookNo.SelectedItem.ToString() + "','" + ddlPageNo.SelectedItem.ToString() + "','" + dblIssuedQty + "','" + IssuedDate + "','" + dblIssuedBags + "','" + TypeofBags + "','" + txtTCNo.Text.Trim() + "','" + hdfFrmRailHaid.Value + "','" + ddlSendIC.SelectedValue.ToString() + "','" + ddlSendBranch.SelectedValue.ToString() + "','" + ddlSendGodown.SelectedValue.ToString() + "','" + ddlConsinmentNo.SelectedItem.ToString() + "');";

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                }
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = false;

                                ddlBookNo.Enabled = ddlPageNo.Enabled = ddlConsinmentNo.Enabled = ddlSendIC.Enabled = ddlSendBranch.Enabled = ddlSendGodown.Enabled = false;
                                txtIssuedQty.Enabled = txtIssuedBags.Enabled = txtTCNo.Enabled = false;
                                //btnPrint.Enabled = true;

                                Label2.Visible = true;
                                Label2.Text = "Your Challan Number Is : " + DeliveryChallan_MO;

                                Session["DC_MO"] = DeliveryChallan_MO.ToString();
                                Session["ModeofDispatch"] = ViewState["hdfDispatchMode"].ToString();

                                txtFrmDist.Text = txtToDist.Text = "";
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Delivery Challan Number Is " + DeliveryChallan_MO + "'); </script> ");

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select All Field'); </script> ");
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

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    string url = "Print_DO_PDSMO.aspx";
    //    string s = "window.open('" + url + "', 'popup_window');";
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    //}

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/MovementOrderHome.aspx");
    }


    protected void ddlConsinmentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRailHead.Text = txtBalQtyInRack.Text = txtBalBagInRack.Text = txtIssuedQty.Text = txtIssuedBags.Text = txtTCNo.Text = "";
        hdfFrmRailHaid.Value = "";

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

        if (ViewState["hdfCommodity"].ToString() == "25")
        {
            txtIssuedBags.Text = "0";
        }

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                //select = "Select Rem_Qty,Rem_Bags,ToRailHaid,(Select RailHead_Name From tbl_Rail_Head where RailHead_Code=ToRailHaid) As ToRailHaidName From RackReceived where MoveOrdernum='" + txtMONo.Text + "' and To_RackDist='" + Dist_Id + "' and Rem_Qty>0 and Commodity='" + ViewState["hdfCommodity"].ToString() + "' and Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "'";

                select = "Select ABC.Rec_Qty As RackQty,ABC.Rec_Bags As RackBags,ABC.ToRailHaid,ABC.ToRailHaidName,(ABC.Rec_Qty-ABC.Send_Qty) As Rem_Qty, (ABC.Rec_Bags-ABC.Send_Bags) As Rem_Bags From (Select Rec_Qty,Rec_Bags,ToRailHaid,(Select RailHead_Name From tbl_Rail_Head where RailHead_Code=ToRailHaid) As ToRailHaidName ,(Select SUM(ISNULL(Issued_Qty,0)) As qty From DeliveryChallan_MO where MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='13' and Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "') As Send_Qty,(Select SUM(CAST(ISNULL(Issued_Bags,0) AS INT)) As qty From DeliveryChallan_MO where MoveOrdernum='" + txtMONo.Text + "' and ModeofDispatch='13' and Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "') As Send_Bags From RackReceived where MoveOrdernum='" + txtMONo.Text + "' and To_RackDist='" + Dist_Id + "' and Commodity='" + ViewState["hdfCommodity"].ToString() + "' and Consinment_No='" + ddlConsinmentNo.SelectedItem.ToString() + "') As ABC";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    double RemQty = 0; double Rem_Bags = 0;
                    string RQty = "", RBags = "";

                    RQty = ds.Tables[0].Rows[0]["Rem_Qty"].ToString();
                    RBags = ds.Tables[0].Rows[0]["Rem_Bags"].ToString();

                    if (RQty != "" && RBags != "")
                    {
                        RemQty = double.Parse(ds.Tables[0].Rows[0]["Rem_Qty"].ToString());
                        Rem_Bags = double.Parse(ds.Tables[0].Rows[0]["Rem_Bags"].ToString());
                    }
                    else
                    {
                        RemQty = double.Parse(ds.Tables[0].Rows[0]["RackQty"].ToString());
                        Rem_Bags = double.Parse(ds.Tables[0].Rows[0]["RackBags"].ToString());
                    }

                    txtBalQtyInRack.Text = RemQty.ToString();
                    txtBalBagInRack.Text = Rem_Bags.ToString();

                    txtRailHead.Text = ds.Tables[0].Rows[0]["ToRailHaidName"].ToString();
                    hdfFrmRailHaid.Value = ds.Tables[0].Rows[0]["ToRailHaid"].ToString();

                    //if (RemQty > 0 && Rem_Bags > 0)
                    //{
                    //    txtRailHead.Text = ds.Tables[0].Rows[0]["ToRailHaidName"].ToString();
                    //    hdfFrmRailHaid.Value = ds.Tables[0].Rows[0]["ToRailHaid"].ToString();
                    //}
                    //else
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Consinment No के विरुद्ध Qty या Bags की मात्रा कम होने के कारण आप Delivery Challan जारी नहीं कर सकते|'); </script> ");
                    //    return;
                    //}
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

    public void GetConsinmentNo()
    {
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                //select = "Select Consinment_No From RackReceived where MoveOrdernum='" + txtMONo.Text + "' and To_RackDist='" + Dist_Id + "' and Rem_Qty>0 and Commodity='" + ViewState["hdfCommodity"].ToString() + "'";
                select = "Select Consinment_No From RackReceived where MoveOrdernum='" + txtMONo.Text + "' and To_RackDist='" + Dist_Id + "' and Commodity='" + ViewState["hdfCommodity"].ToString() + "' and ((DATEADD(DAY,180,CreatedDate))>=Getdate())";
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
}