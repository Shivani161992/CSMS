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

public partial class District_TOAgainst_PDSMovmtOrder : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0;
    string districtid = "", MONumber = "", SMONumber = "", BranchName = "", GodownName = "", ToDistCode = "", strTransportNumber = "", strPRackNo = "";
    public int railnum;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                districtid = Session["dist_id"].ToString();

                GetMONumber();

                Session["ICGBQ"] = null;
                Session["MovmtOrderNo"] = null;
                Session["SubMovmtOrderNo"] = null;
                Session["ToDostCode"] = null;
                Session["TransportNumber"] = null;
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

            txtDate.Text = Request.Form[txtDate.UniqueID];
            txtRackDispatchDate.Text = Request.Form[txtRackDispatchDate.UniqueID];
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMONumber()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select distinct MoveOrdernum from StateMovementOrder where FrmDist= '" + districtid + "' and (IsIssued='N' and ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and (IsAccepted='Y' or IsAccepted Is Null) and Commodity!='25' order by MoveOrdernum");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMONumber.DataSource = ds.Tables[0];
                    ddlMONumber.DataTextField = "MoveOrdernum";
                    ddlMONumber.DataValueField = "MoveOrdernum";
                    ddlMONumber.DataBind();
                    ddlMONumber.Items.Insert(0, "--Select--");

                    GetMPIssueCentre();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('MO Number Is Not Available'); </script> ");
                    ddlSMONumber.Items.Clear();

                    ddlIssueCentre.Items.Clear();
                    ddlBranch.Items.Clear();
                    ddlGodown.Items.Clear();
                    txtQty.Enabled = false;
                    txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtDate.Text = "";
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

    protected void ddlMONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfRailHeadQty.Value = "";
        GridView2.DataSource = "";
        GridView2.DataBind();

        ddlFrmRack.Items.Clear();
        ddlToRack.Items.Clear();
        ddlTransporterName.Items.Clear();
        ChkRailHead.Checked = false;

        trIDAdd.Visible = trIDGrid.Visible = trIDReceived.Visible = trRail.Visible = trRail1.Visible = trMultipleRail.Visible = false;

        if (ddlMONumber.SelectedIndex > 0)
        {
            ddlSMONumber.Items.Clear();
            txtQty.Enabled = false;
            txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtEndDate.Text = "";
            GetToDist();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mo Number'); </script> ");
            ddlSMONumber.Items.Clear();
            txtQty.Enabled = false;
            txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtEndDate.Text = txtDate.Text = "";
        }
    }

    public void GetToDist()
    {
        districtid = Session["dist_id"].ToString();
        MONumber = ddlMONumber.SelectedItem.ToString();
        trIDAdd.Visible = trIDGrid.Visible = trIDReceived.Visible = trRail.Visible = trRail1.Visible = false;
        string IsCancelled = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select (select district_name From pds.districtsmp where district_code= ToDist) DistName, ToDist,GunnyType,IsCancelled from StateMovementOrder where FrmDist= '" + districtid + "' and MoveOrdernum='" + MONumber + "' and IsIssued='N' and (IsAccepted='Y' or IsAccepted Is Null) order by SMO");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string GunnyType = ds.Tables[0].Rows[0]["GunnyType"].ToString();
                    IsCancelled = ds.Tables[0].Rows[0]["IsCancelled"].ToString();

                    if (IsCancelled == "Y")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Movement Order को मुख्यालय द्वारा निरस्त कर दिया गया हैं| आप इस Movement Order के विरुद्ध Transport Order नहीं बना सकते|'); </script> ");
                        return;
                    }
                    else
                    {
                        if (GunnyType == "JUTE")
                        {
                            txtGunnyType.Text = "Jute(SBT)";
                            lblQty.Text = "(Bales)";
                            lblQty1.Text = "(Bales)";
                        }
                        else if (GunnyType == "PP")
                        {
                            txtGunnyType.Text = "PP";
                            lblQty.Text = "(Bales)";
                            lblQty1.Text = "(Bales)";
                        }
                        else
                        {
                            txtGunnyType.Text = "";
                            lblQty.Text = "(MT)";
                            lblQty1.Text = "(MT)";
                        }


                        ddlSMONumber.DataSource = ds.Tables[0];
                        ddlSMONumber.DataTextField = "DistName";
                        ddlSMONumber.DataValueField = "ToDist";
                        ddlSMONumber.DataBind();
                        ddlSMONumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Dist Is Not Available'); </script> ");
                    txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtDate.Text = "";
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

    protected void ddlSMONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfRailHeadQty.Value = "";
        ChkRailHead.Checked = false;
        ddlTransporterName.Items.Clear();
        districtid = Session["dist_id"].ToString();
        string MONumber = ddlMONumber.SelectedItem.ToString();
        hdfMovmtOrderNo.Value = ddlMONumber.SelectedItem.ToString();
        string ToDistCode = ddlSMONumber.SelectedValue.ToString();
        string ToDistName = ddlSMONumber.SelectedItem.ToString();

        ddlFrmRack.Items.Clear();
        ddlToRack.Items.Clear();

        GridView2.DataSource = "";
        GridView2.DataBind();

        trIDAdd.Visible = trIDGrid.Visible = trIDReceived.Visible = trRail.Visible = trRail1.Visible = trMultipleRail.Visible = false;

        if (ddlSMONumber.SelectedIndex > 0)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = "";

                    if (txtGunnyType.Text != "")
                    {
                        select = "select (select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= Commodity) ComdtyName, Commodity,CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= ModeofDispatch) ModeofDispatchName, ModeofDispatch,CreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= FrmDist) FrmDistName, FrmDist,(select district_name From pds.districtsmp where district_code= ToDist) ToDistName, ToDist,Quantity,(Quantity)QuantityMT,GETDATE() ServerDate, SMO from StateMovementOrder where MoveOrdernum='" + MONumber + "' and FrmDist='" + districtid + "' and ToDist='" + ToDistCode + "' and RecAgainstHO='Y'";
                    }
                    else
                    {
                        //select = "select (select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= Commodity) ComdtyName, Commodity,CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= ModeofDispatch) ModeofDispatchName, ModeofDispatch,CreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= FrmDist) FrmDistName, FrmDist,(select district_name From pds.districtsmp where district_code= ToDist) ToDistName, ToDist,Quantity,(Quantity/10)QuantityMT,GETDATE() ServerDate, SMO from StateMovementOrder where MoveOrdernum='" + MONumber + "' and FrmDist='" + districtid + "' and ToDist='" + ToDistCode + "' and RecAgainstHO='Y'";
                        select = "Select  ABC.ComdtyName,ABC.Commodity,ABC.CreatedDate,ABC.CropYear,ABC.FrmDist,ABC.FrmDistName,ABC.ModeofDispatch,ABC.ModeofDispatchName,ABC.Quantity,ABC.ReachDate,ABC.SMO,ABC.ServerDate,((ABC.MOQty-(SUM(ABC.TOQty)))/10) As QuantityMT,ABC.ToDist,ABC.ToDistName From (select distinct (select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= MO.Commodity) ComdtyName, MO.Commodity,MO.CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= MO.ModeofDispatch) ModeofDispatchName, MO.ModeofDispatch,MO.CreatedDate,MO.ReachDate,(select district_name From pds.districtsmp where district_code= MO.FrmDist) FrmDistName, MO.FrmDist,(select district_name From pds.districtsmp where district_code= MO.ToDist) ToDistName, MO.ToDist,MO.Quantity,MO.Quantity AS MOQty, (ISNULL(TOMO.Quantity,0)) As TOQty ,GETDATE() ServerDate, MO.SMO from StateMovementOrder As MO Left Join TO_AgainstHO_MO As TOMO ON(MO.MoveOrdernum=TOMO.MoveOrdernum and MO.FrmDist = TOMO.FrmDist and MO.ToDist=TOMO.ToDist) where MO.MoveOrdernum='" + MONumber + "' and MO.FrmDist='" + districtid + "' and MO.ToDist='" + ToDistCode + "' and RecAgainstHO='Y' ) As ABC Group BY ABC.ComdtyName,ABC.Commodity,ABC.CreatedDate,ABC.CropYear,ABC.FrmDist,ABC.FrmDistName,ABC.MOQty,ABC.ModeofDispatch,ABC.ModeofDispatchName,ABC.Quantity,ABC.ReachDate,ABC.SMO,ABC.ServerDate,ABC.ToDist,ABC.ToDistName";
                    }

                    da = new SqlDataAdapter(select, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                        txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        txtDispatch.Text = ds.Tables[0].Rows[0]["ModeofDispatchName"].ToString();

                        DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtDateMo.Text = CreatedDate.ToString("dd/MMM/yyyy");

                        DateTime EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReachDate"].ToString());
                        txtEndDate.Text = EndDate.ToString("dd/MMM/yyyy");

                        txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                        txtToDist.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                        hdfQuantityValue.Value = ds.Tables[0].Rows[0]["Quantity"].ToString();

                        txtQuantity.Text = ds.Tables[0].Rows[0]["QuantityMT"].ToString();

                        hdfSubMocementOrderNo.Value = ds.Tables[0].Rows[0]["SMO"].ToString();
                        hdfDistID.Value = ds.Tables[0].Rows[0]["ToDist"].ToString();
                        hdfFrmDistValue.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                        hdfDispatchModeValue.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();
                        hdfComdtyValue.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();

                        //For Calendar Date

                        hdfToDate.Value = ds.Tables[0].Rows[0]["ReachDate"].ToString();
                        hdfServerDate.Value = ds.Tables[0].Rows[0]["ServerDate"].ToString();

                        DateTime dt1 = Convert.ToDateTime(hdfServerDate.Value);
                        DateTime dt2 = Convert.ToDateTime(hdfToDate.Value);

                        TimeSpan ts = dt2.Subtract(dt1);
                        int NoOfDays = ts.Days;
                        ViewState["TotalDate"] = (NoOfDays + 1).ToString();

                        if (txtQuantity.Text != "" && double.Parse(txtQuantity.Text) > 0)
                        {
                            txtQty.Enabled = true;
                            txtQty.Text = txtQuantity.Text;
                        }

                        if (hdfDispatchModeValue.Value == "12")
                        {
                            GetData();
                            trIDAdd.Visible = trIDGrid.Visible = true;
                            lblMvmtPlan.Text = txtToDist.Text;
                            trRail.Visible = trRail1.Visible = false;
                        }
                        else
                        {
                            GetFrmRackPoint();
                            GetToRackPoint();
                            GridView2.DataSource = "";
                            GridView2.DataBind();
                            trIDAdd.Visible = trIDGrid.Visible = false;
                            trRail.Visible = trRail1.Visible = trMultipleRail.Visible = true;
                        }

                        GetTransport();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्तकर्ता जिले द्वारा मूवमेंट प्लान नहीं बनाया गया हैं, कृपया प्राप्तकर्ता जिला कार्यालय " + ToDistName + " से संपर्क करें|'); </script> ");
                        txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtToDist.Text = txtQty.Text = txtQuantity.Text = txtDate.Text = "";
                        txtQty.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtToDist.Text = txtQty.Text = txtQuantity.Text = txtDate.Text = "";
                    txtQty.Enabled = false;
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
            txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtToDist.Text = txtQty.Text = txtQuantity.Text = txtDate.Text = "";
            txtQty.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
        }
    }

    public void GetTransport()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (hdfDispatchModeValue.Value == "12")
                {
                    select = "select distinct Transporter_ID,Transporter_Name+ ' { '+MobileNo+' }' as TMobile from Transporter_Table where Transporter_ID in(select Max(Transporter_ID) from Transporter_Table where Distt_ID='" + districtid + "' and IsActive='Y' and MobileNo!='' and Transport_ID In ('6','11')  and (Valid_Upto+1)>=GetDATE() group by MobileNo) and Distt_ID='" + districtid + "' and IsActive='Y' order by TMobile";
                }
                else
                {
                    select = "select distinct Transporter_ID,Transporter_Name+ ' { '+MobileNo+' }' as TMobile from Transporter_Table where Transporter_ID in(select Max(Transporter_ID) from Transporter_Table where Distt_ID='" + districtid + "' and IsActive='Y' and MobileNo!='' and Transport_ID In ('10')  and (Valid_Upto+1)>=GetDATE() group by MobileNo) and Distt_ID='" + districtid + "' and IsActive='Y' order by TMobile";
                }

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTransporterName.DataSource = ds.Tables[0];
                    ddlTransporterName.DataTextField = "TMobile";
                    ddlTransporterName.DataValueField = "Transporter_ID";
                    ddlTransporterName.DataBind();
                    ddlTransporterName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transporters Is Not Available'); </script> ");
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

    public void GetToRackPoint()
    {
        ddlToRack.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select RailHead_Name,RailHead_Code From tbl_Rail_Head where district_code='" + ddlSMONumber.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlToRack.DataSource = ds.Tables[0];
                    ddlToRack.DataTextField = "RailHead_Name";
                    ddlToRack.DataValueField = "RailHead_Code";
                    ddlToRack.DataBind();
                    ddlToRack.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी रेल हेड उपलब्ध नहीं है|'); </script> ");
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

    public void GetFrmRackPoint()
    {
        districtid = Session["dist_id"].ToString();
        ddlFrmRack.Items.Clear();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select RailHead_Name,RailHead_Code From tbl_Rail_Head where district_code='" + districtid + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFrmRack.DataSource = ds.Tables[0];
                    ddlFrmRack.DataTextField = "RailHead_Name";
                    ddlFrmRack.DataValueField = "RailHead_Code";
                    ddlFrmRack.DataBind();
                    ddlFrmRack.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी रेल हेड उपलब्ध नहीं है|'); </script> ");
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

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
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
        districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddlIssueCentre.SelectedValue.ToString());
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
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
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


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();

        if (ddlBranch.SelectedIndex > 0)
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        trMultipleRail.Visible = false;
        double TotalQty = double.Parse(txtQuantity.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (ddlTransporterName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Transporter Name'); </script> ");
            return;
        }

        if (hdfDispatchModeValue.Value == "13")
        {
            if (ddlFrmRack.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select From District Rail'); </script> ");
                return;
            }

            if (ddlToRack.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select To District Rail'); </script> ");
                return;
            }

            if (txtRackDispatchDate.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Rack Dispatch Date'); </script> ");
                return;
            }

        }

        if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
        else if (txtQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Quantity'); </script> ");
            return;
        }
        else if (TotalQty < AllotedQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Allow Because Dispatch Quantity Is Greater Than Quantity'); </script> ");
            return;
        }
        else
        {
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("ICName");
                dt.Columns.Add("ICVal");
                dt.Columns.Add("BranchName");
                dt.Columns.Add("BranchVal");
                dt.Columns.Add("GodownName");
                dt.Columns.Add("GodownVal");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("QuantityQtls");
            }

            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ddlGodown.SelectedValue.ToString() == dt.Rows[i]["GodownVal"].ToString())
                {
                    count = 1;
                    break;
                }
            }

            if (count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                dr["ICVal"] = ddlIssueCentre.SelectedValue;
                dr["BranchName"] = ddlBranch.SelectedItem.Text;
                dr["BranchVal"] = ddlBranch.SelectedValue;
                dr["GodownName"] = ddlGodown.SelectedItem.Text;
                dr["GodownVal"] = ddlGodown.SelectedValue;
                dr["Quantity"] = txtQty.Text;

                if (txtGunnyType.Text != "")
                {
                    dr["QuantityQtls"] = ((double.Parse(txtQty.Text)));
                }
                else
                {
                    dr["QuantityQtls"] = ((double.Parse(txtQty.Text)) * 10);
                }

                dr["quantity"] = (AllotedQty).ToString("0.00");
                dt.Rows.Add(dr);
                Session["ICGBQ"] = dt;
                ddlMONumber.Enabled = ddlSMONumber.Enabled = ddlTransporterName.Enabled = ddlFrmRack.Enabled = ddlToRack.Enabled = false;
                trIDReceived.Visible = true;

                fillgrid();

                txtQuantity.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();

                if (ChkRailHead.Checked == true)
                {
                    btnRecptSubmit.Enabled = true;
                }
                else
                {
                    if (txtQty.Text == "0" || txtQuantity.Text == "0")
                    {
                        btnAdd.Enabled = false;
                        txtQty.Enabled = false;
                        btnRecptSubmit.Enabled = true;
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        txtQty.Enabled = true;
                        btnRecptSubmit.Enabled = false;
                    }
                }
                txtQty.Focus();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Godown Is Not Allowed'); </script> ");
                return;
            }
        }
    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["ICGBQ"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;

            if (hdfComdtyValue.Value == "25")
            {
                e.Row.Cells[4].Text = "Quantity (Bales)";
            }
            else
            {
                e.Row.Cells[4].Text = "Quantity (MT)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[4].Text));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total Qty = ";
            e.Row.Cells[4].Text = QtyTotal.ToString("0.00");
            hdfRailHeadQty.Value = QtyTotal.ToString("0.00");
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("ICName");
            dt.Columns.Add("ICVal");
            dt.Columns.Add("BranchName");
            dt.Columns.Add("BranchVal");
            dt.Columns.Add("GodownName");
            dt.Columns.Add("GodownVal");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("QuantityQtls");
        }
        else
        {
            string QtyRowValue = dt.Rows[e.RowIndex]["Quantity"].ToString();

            double TotalQty = double.Parse(txtQuantity.Text);
            double AllotedQty = double.Parse(QtyRowValue);

            txtQuantity.Text = txtQty.Text = (TotalQty + AllotedQty).ToString();

            dt.Rows.RemoveAt(e.RowIndex);

            if (txtQty.Text == "0" || txtQuantity.Text == "0")
            {
                btnAdd.Enabled = false;
                txtQty.Enabled = false;
                btnRecptSubmit.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = true;
                txtQty.Enabled = true;
                btnRecptSubmit.Enabled = false;
            }
        }
        Session["ICGBQ"] = dt;
        fillgrid();
        txtQty.Focus();
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (txtDate.Text != "")
        {
            if (hdfDispatchModeValue.Value == "13") //Dispatch By Rack
            {
                if (txtRackDispatchDate.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Rack Dispatch Date'); </script> ");
                    return;
                }
            }

            double TotalQty = double.Parse(txtQuantity.Text);
            double AllotedQty = double.Parse(txtQty.Text);

            if (txtCropYear.Text != "" || txtDispatch.Text != "")
            {
                if (ChkRailHead.Checked == false)
                {
                    if (TotalQty == 0 && AllotedQty == 0)
                    {
                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string ConvertMOEndDate = ServerDate.getDate_MDY(txtEndDate.Text);
                        string ConvertMODate = ServerDate.getDate_MDY(txtDateMo.Text);
                        string ConvertTOEndDate = ServerDate.getDate_MDY(txtDate.Text);

                        MONumber = ddlMONumber.SelectedItem.ToString();
                        SMONumber = hdfSubMocementOrderNo.Value;
                        ToDistCode = hdfDistID.Value;

                        strTransportNumber = "";

                        districtid = Session["dist_id"].ToString();

                        if (Session["update"].ToString() == ViewState["update"].ToString())
                        {
                            using (con = new SqlConnection(strcon))
                            {
                                try
                                {
                                    con.Open();

                                    string instr = "";

                                    string DCDate = "";

                                    string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                                    da = new SqlDataAdapter(selectmax, con);
                                    ds = new DataSet();
                                    da.Fill(ds);

                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    {
                                        DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                        strTransportNumber = DCDate.Substring(2);
                                    }

                                    if (strTransportNumber != "")
                                    {
                                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                "Update StateMovementOrder Set IsIssued='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + districtid + "' and ToDist='" + ToDistCode + "';";

                                        DataTable dt = adddetails();
                                        if (dt != null)
                                        {
                                            if (hdfDispatchModeValue.Value == "12") //Dispatch By Road
                                            {
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    string TMO = strTransportNumber + (i + 1);
                                                    instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + hdfFrmDistValue.Value + "','" + hdfDistID.Value + "','" + hdfComdtyValue.Value + "','" + hdfQuantityValue.Value + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "');";
                                                }
                                            }
                                            else if (hdfDispatchModeValue.Value == "13")  //Dispatch By Rack
                                            {
                                                string sendist = txtFrmDist.Text;
                                                string recdist = txtToDist.Text;
                                                string senddate = Convert.ToString(txtRackDispatchDate.Text);

                                                string racknumber = sendist + "-" + recdist + "-" + senddate;

                                                int month = int.Parse(DateTime.Today.Month.ToString());
                                                int year = int.Parse(DateTime.Today.Year.ToString());

                                                //string selectmaxRackNo = "select max(Trans_ID) as Trans_ID  from dbo.tbl_RackMaster where district_code='" + districtid + "' and Month =" + month;
                                                string selectmaxRackNo = "select max(Trans_ID) as Trans_ID  from dbo.TO_AgainstHO_MO where FrmDist='" + districtid + "' and Month =" + month;
                                                cmd = new SqlCommand(selectmaxRackNo, con);
                                                da = new SqlDataAdapter(cmd);
                                                ds = new DataSet();
                                                da.Fill(ds);

                                                string transid = ds.Tables[0].Rows[0]["Trans_ID"].ToString();

                                                string mmonth = month.ToString();

                                                if (transid == "")
                                                {
                                                    transid = districtid + mmonth + "001";
                                                }
                                                else
                                                {
                                                    railnum = Convert.ToInt32(transid);
                                                    railnum = railnum + 1;
                                                    transid = railnum.ToString();
                                                }

                                                string rackchk = racknumber;

                                                string mracno = strPRackNo = racknumber + "-" + transid;
                                                string msendrh = ddlFrmRack.SelectedValue.ToString();
                                                string mdesrh = "";

                                                mdesrh = ddlToRack.SelectedValue.ToString();
                                                string mddistt = hdfDistID.Value;
                                                string crdate = DateTime.Today.Date.ToString();
                                                string udate = "";

                                                string ConvertRackEndDate = ServerDate.getDate_MDY(txtRackDispatchDate.Text);

                                                string mdisdate = ConvertRackEndDate;
                                                string mcommdty = hdfComdtyValue.Value;

                                                string state = "";
                                                state = Session["State_Id"].ToString();

                                                string opid = Session["OperatorIDDM"].ToString();
                                                string scheme = "0";
                                                string dispatchtype = "1";
                                                string st = "N";

                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    string TMO = strTransportNumber + (i + 1);
                                                    string Stransid = transid + (i + 1);
                                                    instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,State_Id,Month,Year,IsSend,OperatorID,Scheme_Id,DispatchID,STrans_ID)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + hdfFrmDistValue.Value + "','" + hdfDistID.Value + "','" + hdfComdtyValue.Value + "','" + hdfQuantityValue.Value + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + ddlFrmRack.SelectedValue.ToString() + "','" + ddlToRack.SelectedValue.ToString() + "','" + mracno + "','" + ConvertRackEndDate + "','" + transid + "','" + state + "','" + month + "','" + year + "','" + st + "','" + opid + "','" + scheme + "','" + dispatchtype + "','" + Stransid + "');";
                                                }
                                            }
                                        }
                                    }

                                    instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                                    cmd = new SqlCommand(instr, con);
                                    int count = cmd.ExecuteNonQuery();

                                    if (count > 0)
                                    {
                                        btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                                        btnPrint.Enabled = true;
                                        Session["ICGBQ"] = null;
                                        Session["MovmtOrderNo"] = MONumber;
                                        Session["SubMovmtOrderNo"] = SMONumber;
                                        Session["ToDostCode"] = hdfDistID.Value;
                                        Session["TransportNumber"] = strTransportNumber;

                                        if (hdfDispatchModeValue.Value == "12")
                                        {
                                            Label2.Visible = true;
                                            Label2.Text = "Your TransportOrder Number Is => " + strTransportNumber;
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your TransportOrder Number Is " + strTransportNumber + "'); </script> ");
                                        }

                                        else if (hdfDispatchModeValue.Value == "13")
                                        {
                                            Label2.Visible = true;
                                            Label2.Text = "Your TransportOrder Number Is: " + strTransportNumber + " and Rack No: " + strPRackNo;
                                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your TransportOrder Number Is " + strTransportNumber + " and Rack Number Is " + strPRackNo + "'); </script> ");
                                        }

                                        txtCropYear.Text = txtDispatch.Text = "";
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
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Alloted All Quantity'); </script> ");
                    }
                }
                else //For Multiple Rail Head
                {
                    ClientIP objClientIP = new ClientIP();
                    string GetIp = (objClientIP.GETIP());

                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string ConvertMOEndDate = ServerDate.getDate_MDY(txtEndDate.Text);
                    string ConvertMODate = ServerDate.getDate_MDY(txtDateMo.Text);
                    string ConvertTOEndDate = ServerDate.getDate_MDY(txtDate.Text);

                    MONumber = ddlMONumber.SelectedItem.ToString();
                    SMONumber = hdfSubMocementOrderNo.Value;
                    ToDistCode = hdfDistID.Value;

                    strTransportNumber = "";

                    districtid = Session["dist_id"].ToString();

                    double StrMultiRailHead = ((double.Parse(hdfRailHeadQty.Value)) * 10);

                    if (Session["update"].ToString() == ViewState["update"].ToString())
                    {
                        using (con = new SqlConnection(strcon))
                        {
                            try
                            {
                                con.Open();
                                string instr = "", DCDate = "";

                                string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                                da = new SqlDataAdapter(selectmax, con);
                                ds = new DataSet();
                                da.Fill(ds);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                    strTransportNumber = DCDate.Substring(2);
                                }

                                if (strTransportNumber != "")
                                {
                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                                    if (TotalQty == 0 && AllotedQty == 0)
                                    {
                                        instr += "Update StateMovementOrder Set IsIssued='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + districtid + "' and ToDist='" + ToDistCode + "';";
                                    }

                                    DataTable dt = adddetails();
                                    if (dt != null)
                                    {
                                        if (hdfDispatchModeValue.Value == "12") //Dispatch By Road
                                        {
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                string TMO = strTransportNumber + (i + 1);
                                                instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + hdfFrmDistValue.Value + "','" + hdfDistID.Value + "','" + hdfComdtyValue.Value + "','" + hdfQuantityValue.Value + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "');";
                                            }
                                        }
                                        else if (hdfDispatchModeValue.Value == "13")  //Dispatch By Rack
                                        {
                                            string sendist = txtFrmDist.Text;
                                            string recdist = txtToDist.Text;
                                            string senddate = Convert.ToString(txtRackDispatchDate.Text);

                                            string racknumber = sendist + "-" + recdist + "-" + senddate;

                                            int month = int.Parse(DateTime.Today.Month.ToString());
                                            int year = int.Parse(DateTime.Today.Year.ToString());

                                            //string selectmaxRackNo = "select max(Trans_ID) as Trans_ID  from dbo.tbl_RackMaster where district_code='" + districtid + "' and Month =" + month;
                                            string selectmaxRackNo = "select max(Trans_ID) as Trans_ID  from dbo.TO_AgainstHO_MO where FrmDist='" + districtid + "' and Month =" + month;
                                            cmd = new SqlCommand(selectmaxRackNo, con);
                                            da = new SqlDataAdapter(cmd);
                                            ds = new DataSet();
                                            da.Fill(ds);

                                            string transid = ds.Tables[0].Rows[0]["Trans_ID"].ToString();

                                            string mmonth = month.ToString();

                                            if (transid == "")
                                            {
                                                transid = districtid + mmonth + "001";
                                            }
                                            else
                                            {
                                                railnum = Convert.ToInt32(transid);
                                                railnum = railnum + 1;
                                                transid = railnum.ToString();
                                            }

                                            string rackchk = racknumber;

                                            string mracno = strPRackNo = racknumber + "-" + transid;
                                            string msendrh = ddlFrmRack.SelectedValue.ToString();
                                            string mdesrh = "";

                                            mdesrh = ddlToRack.SelectedValue.ToString();
                                            string mddistt = hdfDistID.Value;
                                            string crdate = DateTime.Today.Date.ToString();
                                            string udate = "";

                                            string ConvertRackEndDate = ServerDate.getDate_MDY(txtRackDispatchDate.Text);

                                            string mdisdate = ConvertRackEndDate;
                                            string mcommdty = hdfComdtyValue.Value;

                                            string state = "";
                                            state = Session["State_Id"].ToString();

                                            string opid = Session["OperatorIDDM"].ToString();
                                            string scheme = "0";
                                            string dispatchtype = "1";
                                            string st = "N";

                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                string TMO = strTransportNumber + (i + 1);
                                                string Stransid = transid + (i + 1);
                                                instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,State_Id,Month,Year,IsSend,OperatorID,Scheme_Id,DispatchID,STrans_ID)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + hdfFrmDistValue.Value + "','" + hdfDistID.Value + "','" + hdfComdtyValue.Value + "','" + StrMultiRailHead + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + ddlFrmRack.SelectedValue.ToString() + "','" + ddlToRack.SelectedValue.ToString() + "','" + mracno + "','" + ConvertRackEndDate + "','" + transid + "','" + state + "','" + month + "','" + year + "','" + st + "','" + opid + "','" + scheme + "','" + dispatchtype + "','" + Stransid + "');";
                                            }
                                        }
                                    }
                                }

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                                cmd = new SqlCommand(instr, con);
                                int count = cmd.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                                    btnPrint.Enabled = true;
                                    Session["ICGBQ"] = null;
                                    Session["MovmtOrderNo"] = MONumber;
                                    Session["SubMovmtOrderNo"] = SMONumber;
                                    Session["ToDostCode"] = hdfDistID.Value;
                                    Session["TransportNumber"] = strTransportNumber;

                                    if (hdfDispatchModeValue.Value == "12")
                                    {
                                        Label2.Visible = true;
                                        Label2.Text = "Your TransportOrder Number Is => " + strTransportNumber;
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your TransportOrder Number Is " + strTransportNumber + "'); </script> ");
                                    }

                                    else if (hdfDispatchModeValue.Value == "13")
                                    {
                                        Label2.Visible = true;
                                        Label2.Text = "Your TransportOrder Number Is: " + strTransportNumber + " and Rack No: " + strPRackNo;
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your TransportOrder Number Is " + strTransportNumber + " and Rack Number Is " + strPRackNo + "'); </script> ");
                                    }

                                    txtCropYear.Text = txtDispatch.Text = "";
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
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter End Date of Transporation'); </script> ");
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (hdfDispatchModeValue.Value == "12")
        {
            string url = "Print_TOAgainst_PDSMovmtOrder.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else if (hdfDispatchModeValue.Value == "13")
        {
            string url = "Print_TOAgainst_MO_ByRack.aspx";
            string s = "window.open('" + url + "', 'popup_window');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/MovementOrderHome.aspx");
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQty.Focus();
    }

    public string DateLimit
    {
        get
        {

            if (ViewState["TotalDate"] == null)
            {
                return "0";
            }
            else
            {
                return ViewState["TotalDate"].ToString();
            }
        }
    }


    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("Select (RequiredQuantity/10)RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID=Issue_Center) ICName,Issue_Center,Branch,Godown From RecAgainst_StateMovementOrder where MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and ToDist='" + hdfDistID.Value + "' order by RMO");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = "";
                    GridView2.DataBind();
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BranchName = GodownName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();


            BranchName = e.Row.Cells[2].Text;
            GodownName = e.Row.Cells[3].Text;

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
                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                            e.Row.Cells[3].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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

            QtyTotal += (double.Parse(e.Row.Cells[4].Text));
            double QtyRow = (double.Parse(e.Row.Cells[4].Text));
            e.Row.Cells[4].Text = QtyRow.ToString("0.00");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total Qty = ";
            e.Row.Cells[4].Text = QtyTotal.ToString("0.00");
        }
    }
}