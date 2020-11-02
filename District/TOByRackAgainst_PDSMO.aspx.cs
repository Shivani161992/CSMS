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

public partial class District_TOByRackAgainst_PDSMO : System.Web.UI.Page
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
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMONumber()
    {
        ddlTransporterName.Items.Clear();
        hdfModeofDist.Value = hdfSubMocementOrderNo.Value = hdfMovmtOrderNo.Value = hdfDistID.Value = hdfModeofDist.Value = hdfComdtyValue.Value = hdfDispatchModeValue.Value = hdfFrmDistValue.Value = hdfQuantityValue.Value = hdfServerDate.Value = hdfToDate.Value = "";
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select distinct MoveOrdernum from StateMovementOrder where ToDist= '" + districtid + "' and (SMS_todist='N' and ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and ModeofDispatch='13' and IsAccepted='Y' and ModeofDist IN('Both','Other','Self') order by MoveOrdernum");
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


                    // GetTransport();
                    //GetAllDist();

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('MO Number Is Not Available'); </script> ");
                    ddlTransporterName.Items.Clear();
                    ddlIssueCentre.Items.Clear();
                    ddlBranch.Items.Clear();
                    ddlGodown.Items.Clear();
                    txtQty.Enabled = false;
                    txtCropYear.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtDate.Text = "";
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

    public void GetGunnyTransport()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select distinct Transporter_ID,Transporter_Name+ ' { '+MobileNo+' }' as TMobile from Transporter_Table where Transporter_ID in(select Max(Transporter_ID) from Transporter_Table where Distt_ID='" + districtid + "' and IsActive='Y' and MobileNo!='' and Transport_ID='1' and (Valid_Upto+1)>=GetDATE() group by MobileNo) and Distt_ID='" + districtid + "' and IsActive='Y' order by TMobile";
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

    public void GetTransport()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select distinct Transporter_ID,Transporter_Name+ ' { '+MobileNo+' }' as TMobile from Transporter_Table where Transporter_ID in(select Max(Transporter_ID) from Transporter_Table where Distt_ID='" + districtid + "' and IsActive='Y' and MobileNo!='' and Transport_ID='10' and (Valid_Upto+1)>=GetDATE() group by MobileNo) and Distt_ID='" + districtid + "' and IsActive='Y' order by TMobile";
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

    protected void ddlMONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFrmRack.Items.Clear();
        trIDReceived.Visible = false;
        Label3.Visible = false;
        ddlSendingDist.Items.Clear();
        hdfModeofDist.Value = hdfSubMocementOrderNo.Value = hdfMovmtOrderNo.Value = hdfDistID.Value = hdfModeofDist.Value = hdfComdtyValue.Value = hdfDispatchModeValue.Value = hdfFrmDistValue.Value = hdfQuantityValue.Value = hdfServerDate.Value = hdfToDate.Value = "";

        GridView2.DataSource = "";
        GridView2.DataBind();

        trIDAdd.Visible = trIDGrid.Visible = false;
        trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = true;

        if (ddlMONumber.SelectedIndex > 0)
        {
            txtQty.Enabled = false;
            txtCropYear.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtEndDate.Text = "";
            GetToDist();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mo Number'); </script> ");
            txtQty.Enabled = false;
            txtCropYear.Text = txtDateMo.Text = txtDateMo.Text = txtFrmDist.Text = txtToDist.Text = txtComdty.Text = txtQuantity.Text = txtQty.Text = txtEndDate.Text = txtDate.Text = "";
        }
    }

    public void GetToDist()
    {
        districtid = Session["dist_id"].ToString();
        string MONumber = ddlMONumber.SelectedItem.ToString();
        hdfMovmtOrderNo.Value = ddlMONumber.SelectedItem.ToString();
        string IsCancelled = "";

        ddlFrmRack.Items.Clear();

        trIDReceived.Visible = false;

        if (ddlMONumber.SelectedIndex > 0)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = string.Format("select IsCancelled,ModeofDist,(select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= Commodity) ComdtyName, Commodity,CropYear, ModeofDispatch,CreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= FrmDist) FrmDistName, FrmDist,(select district_name From pds.districtsmp where district_code= ToDist) ToDistName, ToDist,Quantity,(Case When Commodity='25' Then Quantity Else (Quantity/10) End) As QuantityMT,GETDATE() ServerDate, SMO from StateMovementOrder where MoveOrdernum='" + MONumber + "' and ToDist='" + districtid + "' and ModeofDispatch='13' and SMS_todist='N'");
                    da = new SqlDataAdapter(select, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        IsCancelled = ds.Tables[0].Rows[0]["IsCancelled"].ToString();

                        if (IsCancelled == "Y")
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Movement Order को मुख्यालय द्वारा निरस्त कर दिया गया हैं| आप इस Movement Order के विरुद्ध Transport Order नहीं बना सकते|'); </script> ");
                            return;
                        }
                        else
                        {
                            txtComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                            txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

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
                            ViewState["TotalDate"] = NoOfDays.ToString();

                            if (txtQuantity.Text != "" && double.Parse(txtQuantity.Text) > 0)
                            {
                                txtQty.Enabled = true;
                                txtQty.Text = txtQuantity.Text;
                            }

                            Label3.Visible = true;
                            string ModeofDist = hdfModeofDist.Value = ds.Tables[0].Rows[0]["ModeofDist"].ToString();


                            if (ModeofDist == "Self")
                            {
                                trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = true;
                                Label3.Text = "रैक प्राप्तकर्ता जिले द्वारा स्कंध का परिवहन केवल स्वयं के जिलों में किया जाये|";
                                GetSelfDist();
                            }
                            else if (ModeofDist == "Other")
                            {
                                trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = false;
                                Label3.Text = "रैक प्राप्तकर्ता जिले द्वारा स्कंध का परिवहन केवल अन्य जिलों में किया जाये|";
                                GetOtherDist();
                                txtQuantity.Text = txtQty.Text = "";
                            }
                            else if (ModeofDist == "Both")
                            {
                                trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = false;
                                Label3.Text = "रैक प्राप्तकर्ता जिले द्वारा स्कंध का परिवहन स्वयं तथा अन्य जिलों में किया जाये|";
                                txtQuantity.Text = txtQty.Text = "";
                                GetBothDist();
                            }
                            else
                            {
                                Label3.Visible = false;
                            }

                            if (hdfComdtyValue.Value == "25")
                            {
                                lblMT.Text = lblMT0.Text = "(Bales)";
                                GetGunnyTransport();
                            }
                            else
                            {
                                lblMT.Text = lblMT0.Text = "(MT)";
                                GetTransport();
                            }

                            GetFrmRackPoint();
                        }
                    }
                    else
                    {
                        txtComdty.Text = txtCropYear.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtToDist.Text = txtQty.Text = txtQuantity.Text = txtDate.Text = "";
                        txtQty.Enabled = false;
                        Label3.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    txtComdty.Text = txtCropYear.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtToDist.Text = txtQty.Text = txtQuantity.Text = txtDate.Text = "";
                    txtQty.Enabled = false;
                    Label3.Visible = false;
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
            txtComdty.Text = txtCropYear.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtToDist.Text = txtQty.Text = txtQuantity.Text = txtDate.Text = "";
            txtQty.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Movement Order Number'); </script> ");
        }
    }

    public void GetBothDist()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select (select district_name from pds.districtsmp where district_code=SubMO.ToOtherDist) As DistName, SubMO.ToOtherDist As DistCode from StateSubMovementOrder SubMO where MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and ToDist='" + districtid + "' and ModeofDist='Both' and (IsIssued='N' and ((DATEADD(DAY,90,CreatedDate))>=Getdate()))";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSendingDist.DataSource = ds.Tables[0];
                    ddlSendingDist.DataTextField = "DistName";
                    ddlSendingDist.DataValueField = "DistCode";
                    ddlSendingDist.DataBind();
                    ddlSendingDist.Items.Insert(0, "--Select--");
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

    public void GetOtherDist()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select (select district_name from pds.districtsmp where district_code=SubMO.ToOtherDist) As DistName, SubMO.ToOtherDist As DistCode from StateSubMovementOrder SubMO where MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and ToDist='" + districtid + "' and ModeofDist='Other' and (IsIssued='N' and ((DATEADD(DAY,90,CreatedDate))>=Getdate()))";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSendingDist.DataSource = ds.Tables[0];
                    ddlSendingDist.DataTextField = "DistName";
                    ddlSendingDist.DataValueField = "DistCode";
                    ddlSendingDist.DataBind();
                    ddlSendingDist.Items.Insert(0, "--Select--");
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



    public void GetSelfDist()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select district_name,district_code from pds.districtsmp where district_code='" + districtid + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSendingDist.DataSource = ds.Tables[0];
                    ddlSendingDist.DataTextField = "district_name";
                    ddlSendingDist.DataValueField = "district_code";
                    ddlSendingDist.DataBind();
                    ddlSendingDist.Items.Insert(0, "--Select--");
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

    public void GetAllDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select district_name,district_code from pds.districtsmp order by district_name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSendingDist.DataSource = ds.Tables[0];
                    ddlSendingDist.DataTextField = "district_name";
                    ddlSendingDist.DataValueField = "district_code";
                    ddlSendingDist.DataBind();
                    ddlSendingDist.Items.Insert(0, "--Select--");
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

    protected void ddlSendingDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfSubSMO.Value = "";

        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        GridView2.DataSource = "";
        GridView2.DataBind();

        GridView1.DataSource = "";
        GridView1.DataBind();

        trIDAdd.Visible = trIDGrid.Visible = false;
        btnRecptSubmit.Enabled = false;

        if (ddlSendingDist.SelectedIndex > 0)
        {
            hdfOnlyOne.Value = "1";
            hdfGetTotalTOQty.Value = "0";

            if (hdfComdtyValue.Value == "25")
            {
                districtid = Session["dist_id"].ToString();
                trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = true;

                lblMvmtPlan.Text = ddlSendingDist.SelectedItem.ToString();

                if (hdfModeofDist.Value == "Other")
                {
                    txtQuantity.Text = "";
                    CheckSubMOMvmtPlan();
                    GetOtherDistIssueCentre();
                }
                else if (hdfModeofDist.Value == "Both")
                {
                    districtid = Session["dist_id"].ToString();

                    txtQuantity.Text = "";
                    hdfQuantityValue.Value = "";
                    //btnRecptSubmit.Enabled = false;

                    if (districtid == ddlSendingDist.SelectedValue.ToString())
                    {
                        GetMPIssueCentre();
                        GetQtyOwnDist();
                    }
                    else
                    {
                        CheckSubMOMvmtPlan();
                        GetOtherDistIssueCentre();
                    }
                }
                else if (hdfModeofDist.Value == "Self")
                {
                    if (districtid == ddlSendingDist.SelectedValue.ToString())
                    {
                        GetMPIssueCentre();
                        GetQtyOwnSelfDist();
                    }
                    else
                    {
                        txtQty.Text = "";
                        //GetOtherDistIssueCentre();
                    }
                }
            }
            else
            {
                GetMPIssueCentre();

                lblMvmtPlan.Text = ddlSendingDist.SelectedItem.ToString();

                if (hdfModeofDist.Value == "Other")
                {
                    txtQuantity.Text = "";
                    CheckSubMOMvmtPlan();
                }
                else if (hdfModeofDist.Value == "Both")
                {
                    districtid = Session["dist_id"].ToString();

                    txtQuantity.Text = "";
                    hdfQuantityValue.Value = "";

                    if (districtid == ddlSendingDist.SelectedValue.ToString())
                    {
                        trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = true;
                        GetQtyOwnDist();
                    }
                    else
                    {
                        trHideIC.Visible = trHideGodown.Visible = trIDGrid1.Visible = false;
                        CheckSubMOMvmtPlan();
                    }
                }
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District Name'); </script> ");
        }
    }

    public void GetQtyOwnSelfDist()
    {
        txtQuantity.Text = txtQty.Text = "";
        hdfQuantityValue.Value = "";

        districtid = Session["dist_id"].ToString();

        trIDAdd.Visible = trIDGrid.Visible = false;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select (ABC.QtyByDist - ISNULL(SUM(ABC.TOMOQuantity),0)) As QtyByDist From (Select MAX(MO.Quantity) As QtyByDist,MAX(TOMO.Quantity) As TOMOQuantity From StateMovementOrder MO left Join TO_AgainstHO_MO TOMO ON(TOMO.SMO=MO.SMO) where MO.MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and MO.SMO='" + hdfSubMocementOrderNo.Value + "' and MO.FrmDist='" + hdfFrmDistValue.Value + "' and MO.ToDist='" + hdfDistID.Value + "' and (MO.IsIssued='N' and ((DATEADD(DAY,90,MO.CreatedDate))>=Getdate())) Group BY TOMO.TO_No ) As ABC Group BY ABC.QtyByDist";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtQuantity.Text = txtQty.Text = ds.Tables[0].Rows[0]["QtyByDist"].ToString();
                    hdfQuantityValue.Value = ds.Tables[0].Rows[0]["QtyByDist"].ToString();
                }
                else
                {
                    txtQuantity.Text = "";
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

    public void GetQtyOwnDist()
    {
        districtid = Session["dist_id"].ToString();

        trIDAdd.Visible = trIDGrid.Visible = false;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                if (hdfComdtyValue.Value == "25")
                {
                    //select = "Select distinct MO.SubSMO,(MO.QtyByDist-ISNULL(TOMO.Quantity,0)) As Qty,(MO.QtyByDist - ISNULL(TOMO.Quantity,0)) As QtyByDist From StateSubMovementOrder MO left Join TO_AgainstHO_MO TOMO ON(TOMO.SubSMO=MO.SubSMO) where MO.MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and MO.SMO='" + hdfSubMocementOrderNo.Value + "' and MO.FrmDist='" + hdfFrmDistValue.Value + "' and MO.ToDist='" + hdfDistID.Value + "' and MO.ToOtherDist='" + districtid + "' and (MO.IsIssued='N' and ((DATEADD(DAY,90,MO.CreatedDate))>=Getdate()))";

                    select = "Select (ABC.Qty-ISNULL(SUM(ABC.TOMOQuantity),0)) As Qty,(ABC.QtyByDist - ISNULL(SUM(ABC.TOMOQuantity),0)) As QtyByDist,ABC.SubSMO  From (Select MAX(MO.SubSMO) As SubSMO,MAX(MO.QtyByDist) As Qty,MAX(MO.QtyByDist) As QtyByDist,MAX(TOMO.Quantity) As TOMOQuantity From StateSubMovementOrder MO left Join TO_AgainstHO_MO TOMO ON(TOMO.SubSMO=MO.SubSMO) where MO.MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and MO.SMO='" + hdfSubMocementOrderNo.Value + "' and MO.FrmDist='" + hdfFrmDistValue.Value + "' and MO.ToDist='" + hdfDistID.Value + "' and MO.ToOtherDist='" + districtid + "' and (MO.IsIssued='N' and ((DATEADD(DAY,90,MO.CreatedDate))>=Getdate())) Group BY TOMO.TO_No ) As ABC Group BY ABC.Qty,ABC.QtyByDist,ABC.SubSMO";
                }
                else
                {
                    select = "Select SubSMO,(Case When Commodity='25' Then QtyByDist Else (QtyByDist/10) End) As Qty,QtyByDist From StateSubMovementOrder where MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + hdfDistID.Value + "' and ToOtherDist='" + districtid + "' and IsIssued='N'";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtQuantity.Text = txtQty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
                    hdfSubSMO.Value = ds.Tables[0].Rows[0]["SubSMO"].ToString();
                    hdfQuantityValue.Value = ds.Tables[0].Rows[0]["QtyByDist"].ToString();
                }
                else
                {
                    txtQuantity.Text = "";
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

    public void CheckSubMOMvmtPlan()
    {
        hdfMOPlanIC.Value = hdfMOPlanBranch.Value = hdfMOPlanGodown.Value = "";
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select SubSMO From StateSubMovementOrder where MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + hdfDistID.Value + "' and ToOtherDist='" + ddlSendingDist.SelectedValue.ToString() + "' and IsMvmtPlan='Y' and IsIssued='N'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfSubSMO.Value = ds.Tables[0].Rows[0]["SubSMO"].ToString();
                    GetSubMOMvmtPlanData();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्तकर्ता जिले द्वारा मूवमेंट प्लान नहीं बनाया गया हैं, कृपया प्राप्तकर्ता जिला कार्यालय " + ddlSendingDist.SelectedItem.ToString() + " से संपर्क करें|'); </script> ");
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

    public void GetSubMOMvmtPlanData()
    {
        districtid = Session["dist_id"].ToString();

        trIDAdd.Visible = trIDGrid.Visible = true;

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                if (hdfComdtyValue.Value == "25")
                {
                    select = string.Format("Select (select district_name from pds.districtsmp where district_code=ToDist) As DistName,(RequiredQuantity) As RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID=Issue_Center) ICName,Issue_Center,Branch,Godown From RecAgainst_StateMovementOrder where MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and FrmDist='" + districtid + "' and ToDist='" + ddlSendingDist.SelectedValue.ToString() + "' order by RMO");
                }
                else
                {
                    select = string.Format("Select (select district_name from pds.districtsmp where district_code=ToDist) As DistName,(RequiredQuantity/10)RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID=Issue_Center) ICName,Issue_Center,Branch,Godown From RecAgainst_StateMovementOrder where MoveOrdernum='" + ddlMONumber.SelectedItem.ToString() + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and FrmDist='" + districtid + "' and ToDist='" + ddlSendingDist.SelectedValue.ToString() + "' order by RMO");
                }
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (hdfComdtyValue.Value == "25")
                    {
                        hdfMOPlanIC.Value = hdfMOPlanBranch.Value = hdfMOPlanGodown.Value = "";

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            hdfMOPlanIC.Value += ((hdfMOPlanIC.Value == "") ? "" : ",") + "'" + ds.Tables[0].Rows[i][3].ToString() + "'";
                            hdfMOPlanBranch.Value += ((hdfMOPlanBranch.Value == "") ? "" : ",") + "'" + ds.Tables[0].Rows[i][4].ToString() + "'";
                            hdfMOPlanGodown.Value += ((hdfMOPlanGodown.Value == "") ? "" : ",") + "'" + ds.Tables[0].Rows[i][5].ToString() + "'";
                        }
                    }

                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();

                    if (hdfComdtyValue.Value == "25")
                    {
                        btnRecptSubmit.Enabled = false;
                    }
                    else
                    {
                        btnRecptSubmit.Enabled = true;
                    }

                }
                else
                {
                    GridView2.DataSource = "";
                    GridView2.DataBind();
                    btnRecptSubmit.Enabled = false;
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

            if (hdfComdtyValue.Value == "25")
            {
                e.Row.Cells[5].Text = "Quantity (Bales)";
            }
            else
            {
                e.Row.Cells[5].Text = "Quantity (MT)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BranchName = GodownName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            BranchName = e.Row.Cells[3].Text;
            GodownName = e.Row.Cells[4].Text;

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
                            e.Row.Cells[3].Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                            e.Row.Cells[4].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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

            QtyTotal += (double.Parse(e.Row.Cells[5].Text));
            double QtyRow = (double.Parse(e.Row.Cells[5].Text));
            e.Row.Cells[5].Text = QtyRow.ToString("0.00");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total Qty = ";
            e.Row.Cells[5].Text = txtQuantity.Text = QtyTotal.ToString("0.00");
        }
    }

    public void GetOtherDistIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                if (hdfMOPlanIC.Value != "")
                {
                    select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DepotID  IN (" + hdfMOPlanIC.Value + ") and DistrictId= '23" + ddlSendingDist.SelectedValue.ToString() + "' order by DepotName";
                }

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

    public void GetOtherDistBranch()
    {
        districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where tbl_MetaData_DEPOT.BranchID IN (" + hdfMOPlanBranch.Value + ") and IssueCenterId='" + ddlIssueCentre.SelectedValue.ToString() + "'";
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

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlSendingDist.SelectedValue.ToString() + "' order by DepotName");
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
        districtid = Session["dist_id"].ToString();

        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            if (hdfComdtyValue.Value == "25" && districtid != ddlSendingDist.SelectedValue.ToString())
            {
                GetOtherDistBranch();
            }
            else
            {
                GetBranch();
            }
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
        districtid = Session["dist_id"].ToString();
        ddlGodown.Items.Clear();

        if (ddlBranch.SelectedIndex > 0)
        {
            if (hdfComdtyValue.Value == "25" && districtid != ddlSendingDist.SelectedValue.ToString())
            {
                GetOtherDistGodown();
            }
            else
            {
                GetGodown();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
        }
    }

    public void GetOtherDistGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Godown_ID IN (" + hdfMOPlanGodown.Value + ") and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
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
        districtid = Session["dist_id"].ToString();

        double TotalQty = double.Parse(txtQuantity.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (ddlTransporterName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Transporter Name'); </script> ");
            return;
        }

        if (ddlSendingDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select District Name'); </script> ");
            return;
        }

        if (hdfDispatchModeValue.Value == "13")
        {
            if (ddlFrmRack.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rail Head'); </script> ");
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
            if (hdfComdtyValue.Value == "25" && districtid != ddlSendingDist.SelectedValue.ToString())
            {
                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("DistName");
                    dt.Columns.Add("DistVal");
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
                    dr["DistName"] = ddlSendingDist.SelectedItem.Text;
                    dr["DistVal"] = ddlSendingDist.SelectedValue;
                    dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                    dr["ICVal"] = ddlIssueCentre.SelectedValue;
                    dr["BranchName"] = ddlBranch.SelectedItem.Text;
                    dr["BranchVal"] = ddlBranch.SelectedValue;
                    dr["GodownName"] = ddlGodown.SelectedItem.Text;
                    dr["GodownVal"] = ddlGodown.SelectedValue;
                    dr["Quantity"] = txtQty.Text;

                    dr["QuantityQtls"] = ((double.Parse(txtQty.Text)));

                    dr["quantity"] = (AllotedQty).ToString("0.00");
                    dt.Rows.Add(dr);
                    Session["ICGBQ"] = dt;
                    ddlMONumber.Enabled = ddlTransporterName.Enabled = ddlFrmRack.Enabled = ddlSendingDist.Enabled = false;
                    trIDReceived.Visible = true;

                    fillgrid();

                    txtQuantity.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();

                    btnRecptSubmit.Enabled = true;

                    if (txtQuantity.Text == "0")
                    {
                        btnAdd.Enabled = false;
                        txtQty.Enabled = false;
                    }

                    txtQty.Focus();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Godown Is Not Allowed'); </script> ");
                    return;
                }
            }
            else
            {
                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("DistName");
                    dt.Columns.Add("DistVal");
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
                    dr["DistName"] = ddlSendingDist.SelectedItem.Text;
                    dr["DistVal"] = ddlSendingDist.SelectedValue;
                    dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                    dr["ICVal"] = ddlIssueCentre.SelectedValue;
                    dr["BranchName"] = ddlBranch.SelectedItem.Text;
                    dr["BranchVal"] = ddlBranch.SelectedValue;
                    dr["GodownName"] = ddlGodown.SelectedItem.Text;
                    dr["GodownVal"] = ddlGodown.SelectedValue;
                    dr["Quantity"] = txtQty.Text;

                    if (hdfComdtyValue.Value == "25")
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
                    ddlMONumber.Enabled = ddlTransporterName.Enabled = ddlFrmRack.Enabled = ddlSendingDist.Enabled = false;
                    trIDReceived.Visible = true;

                    fillgrid();

                    txtQuantity.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();

                    if (hdfComdtyValue.Value == "25")
                    {
                        btnRecptSubmit.Enabled = true;

                        if (txtQuantity.Text == "0")
                        {
                            btnAdd.Enabled = false;
                            txtQty.Enabled = false;
                        }
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
                e.Row.Cells[5].Text = "Quantity (Bales)";
            }
            else
            {
                e.Row.Cells[5].Text = "Quantity (MT)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[5].Text));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total Qty = ";
            e.Row.Cells[5].Text = QtyTotal.ToString("0.00");

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        districtid = Session["dist_id"].ToString();

        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("DistName");
            dt.Columns.Add("DistVal");
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

            if (hdfComdtyValue.Value == "25")
            {
                if (districtid != ddlSendingDist.SelectedValue.ToString())
                {
                    ddlGodown.SelectedIndex = 0;
                }

                btnRecptSubmit.Enabled = true;

                if (dt.Rows.Count == 0)
                {
                    btnRecptSubmit.Enabled = false;
                }
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
        }
        Session["ICGBQ"] = dt;
        fillgrid();
        txtQty.Focus();
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddlTransporterName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Transpoter Name'); </script> ");
            return;
        }
        else if (ddlFrmRack.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rail Head'); </script> ");
            return;
        }
        else
        {
            if (txtDate.Text != "")
            {
                double TotalQty = double.Parse(txtQuantity.Text);

                if (txtCropYear.Text != "")
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

                                string selectmax = "select max(cast(TO_No as bigint)) as TransportOrder from TO_AgainstHO_MO where FrmDist='" + districtid + "' and CropYear='" + txtCropYear.Text + "' and SMO='" + SMONumber + "'";
                                cmd = new SqlCommand(selectmax, con);
                                da = new SqlDataAdapter(cmd);
                                ds = new DataSet();
                                da.Fill(ds);

                                string whr_ID = ds.Tables[0].Rows[0]["TransportOrder"].ToString();

                                if (whr_ID == "")
                                {
                                    strTransportNumber = SMONumber + "1000";
                                }
                                else
                                {
                                    string wid = whr_ID.Substring(whr_ID.Length - 4);

                                    Int64 wid_ID_new = Convert.ToInt64(wid);

                                    wid_ID_new = wid_ID_new + 1;

                                    string combine = wid_ID_new.ToString();

                                    strTransportNumber = SMONumber + combine;
                                }

                                if (hdfDispatchModeValue.Value == "13")  //Dispatch By Rack
                                {
                                    if (hdfModeofDist.Value == "Self" || (hdfModeofDist.Value == "Both" && districtid == ddlSendingDist.SelectedValue.ToString()))
                                    {
                                        double AllotedQty = double.Parse(txtQty.Text);

                                        if (hdfComdtyValue.Value == "25")
                                        {
                                            if ((TotalQty == 0 && AllotedQty == 0))
                                            {
                                                if (hdfModeofDist.Value == "Self")
                                                {
                                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                            "Update StateMovementOrder Set SMS_todist='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "';";

                                                }
                                                else if (hdfModeofDist.Value == "Both" && districtid == ddlSendingDist.SelectedValue.ToString())
                                                {
                                                    int countDropDown = ddlSendingDist.Items.Count;

                                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                            "Update StateSubMovementOrder Set IsIssued='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "' and ToOtherDist='" + districtid + "' and SubSMO='" + hdfSubSMO.Value + "';";

                                                    if (countDropDown == 2)
                                                    {
                                                        instr += "Update StateMovementOrder Set SMS_todist='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "';";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";
                                            }

                                            DataTable dt = adddetails();
                                            if (dt != null)
                                            {
                                                decimal SumQty = 0;
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    SumQty += decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString());
                                                }
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    string TMO = strTransportNumber + (i + 1);
                                                    instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,State_Id,Month,Year,IsSend,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + districtid + "','00','" + hdfComdtyValue.Value + "','" + SumQty + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + ddlFrmRack.SelectedValue.ToString() + "','00','00','','00','00','00','00','00','00','00','00','00','" + dt.Rows[i]["DistVal"] + "','" + hdfSubSMO.Value + "','" + hdfModeofDist.Value + "');";
                                                }
                                            }

                                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                        }
                                        else
                                        {
                                            if ((TotalQty == 0 && AllotedQty == 0))
                                            {
                                                if (hdfModeofDist.Value == "Self")
                                                {
                                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                            "Update StateMovementOrder Set SMS_todist='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "';";

                                                }
                                                else if (hdfModeofDist.Value == "Both" && districtid == ddlSendingDist.SelectedValue.ToString())
                                                {
                                                    int countDropDown = ddlSendingDist.Items.Count;

                                                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                            "Update StateSubMovementOrder Set IsIssued='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "' and ToOtherDist='" + districtid + "' and SubSMO='" + hdfSubSMO.Value + "';";

                                                    if (countDropDown == 2)
                                                    {
                                                        instr += "Update StateMovementOrder Set SMS_todist='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "';";

                                                    }
                                                }

                                                DataTable dt = adddetails();
                                                if (dt != null)
                                                {
                                                    for (int i = 0; i < dt.Rows.Count; i++)
                                                    {
                                                        string TMO = strTransportNumber + (i + 1);
                                                        instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,State_Id,Month,Year,IsSend,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + districtid + "','00','" + hdfComdtyValue.Value + "','" + hdfQuantityValue.Value + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + ddlFrmRack.SelectedValue.ToString() + "','00','00','','00','00','00','00','00','00','00','00','00','" + dt.Rows[i]["DistVal"] + "','" + hdfSubSMO.Value + "','" + hdfModeofDist.Value + "');";
                                                    }
                                                }

                                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                            }
                                            else
                                            {
                                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Alloted All Quantity'); </script> ");
                                            }
                                        }
                                    }
                                    else if (hdfModeofDist.Value == "Other" || (hdfModeofDist.Value == "Both" && districtid != ddlSendingDist.SelectedValue.ToString()))
                                    {
                                        double ConvertMTtoQtls = 0;

                                        if (hdfComdtyValue.Value == "25")
                                        {
                                            ConvertMTtoQtls = ((double.Parse(txtQuantity.Text)));

                                            int countDropDown = ddlSendingDist.Items.Count;

                                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                                            string Grid1FooterRow = (decimal.Parse(hdfGetTotalTOQty.Value) + decimal.Parse(GridView1.FooterRow.Cells[5].Text)).ToString();
                                            string Grid2FooterRow = decimal.Parse(GridView2.FooterRow.Cells[5].Text).ToString();

                                            if (Grid1FooterRow.ToString() == Grid2FooterRow.ToString())
                                            {
                                                instr += "Update StateSubMovementOrder Set IsIssued='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "' and ToOtherDist='" + ddlSendingDist.SelectedValue.ToString() + "' and SubSMO='" + hdfSubSMO.Value + "';";
                                            }

                                            if ((countDropDown == 2) && (Grid1FooterRow.ToString() == Grid2FooterRow.ToString()))
                                            {
                                                instr += "Update StateMovementOrder Set SMS_todist='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "';";
                                            }

                                            DataTable dt = adddetails();
                                            if (dt != null)
                                            {
                                                decimal SumQty = 0;
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    SumQty += decimal.Parse(dt.Rows[i]["QuantityQtls"].ToString());
                                                }
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    string TMO = strTransportNumber + (i + 1);
                                                    instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,State_Id,Month,Year,IsSend,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + districtid + "','00','" + hdfComdtyValue.Value + "','" + SumQty + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + ddlFrmRack.SelectedValue.ToString() + "','00','00','','00','00','00','00','00','00','00','00','00','" + dt.Rows[i]["DistVal"] + "','" + hdfSubSMO.Value + "','" + hdfModeofDist.Value + "');";
                                                }
                                            }

                                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                        }
                                        else
                                        {
                                            ConvertMTtoQtls = ((double.Parse(txtQuantity.Text)) * 10);

                                            int countDropDown = ddlSendingDist.Items.Count;

                                            string TMO = strTransportNumber + 1;

                                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                                            "Update StateSubMovementOrder Set IsIssued='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "' and ToOtherDist='" + ddlSendingDist.SelectedValue.ToString() + "' and SubSMO='" + hdfSubSMO.Value + "';";

                                            if (countDropDown == 2)
                                            {
                                                instr += "Update StateMovementOrder Set SMS_todist='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and FrmDist='" + hdfFrmDistValue.Value + "' and ToDist='" + districtid + "';";
                                            }

                                            instr += "Insert into TO_AgainstHO_MO(MoveOrdernum,SMO,TO_No,STO_No,Transporter_ID,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,TOEndDate,ModeofDispatch,IsIssued,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,FrmRailHaid,ToRailHaid,Rack_No,Rack_DispDate,Trans_ID,State_Id,Month,Year,IsSend,OperatorID,Scheme_Id,DispatchID,STrans_ID,To_MultiDist,SubSMO,ModeofDist)values('" + MONumber + "','" + SMONumber + "','" + strTransportNumber + "','" + TMO + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + districtid + "','00','" + hdfComdtyValue.Value + "','" + ConvertMTtoQtls + "','" + txtCropYear.Text + "','" + ConvertMOEndDate + "','" + ConvertMODate + "','" + ConvertTOEndDate + "','" + hdfDispatchModeValue.Value + "','N',GETDATE(),'" + GetIp + "','00','00','00','" + ConvertMTtoQtls + "','" + ConvertMTtoQtls + "','" + ddlFrmRack.SelectedValue.ToString() + "','00','00','','00','00','00','00','00','00','00','00','00','" + ddlSendingDist.SelectedValue.ToString() + "','" + hdfSubSMO.Value + "','" + hdfModeofDist.Value + "');";

                                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                                        }
                                    }


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


                                        Label2.Visible = true;
                                        Label2.Text = "Your TransportOrder Number Is : " + strTransportNumber;
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your TransportOrder Number Is " + strTransportNumber + "'); </script> ");


                                        txtCropYear.Text = "";
                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                    else
                                    {
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
                    else
                    {
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }

                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter End Date of Transporation'); </script> ");
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print_TOAgainst_MO_RecByRack.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
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
        if (ddlGodown.SelectedIndex > 0)
        {
            districtid = Session["dist_id"].ToString();

            if (hdfComdtyValue.Value == "25" && districtid != ddlSendingDist.SelectedValue.ToString())
            {
                txtQty.Text = "";
                txtQty.Enabled = btnAdd.Enabled = true;
                GetSubMOPlanDataForGunny();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
        }

        txtQty.Focus();
    }

    public void GetSubMOPlanDataForGunny()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select (ABC.RMORequiredQuantity - ISNULL(ABC.TOMORequiredQuantity,0)) As RMORequiredQuantity  From (Select Max(RMO.RequiredQuantity) As RMORequiredQuantity, SUM(TOMO.RequiredQuantity) As TOMORequiredQuantity From RecAgainst_StateMovementOrder RMO left join TO_AgainstHO_MO TOMO ON(TOMO.SubSMO=RMO.SubSMO and TOMO.FrmDist = RMO.FrmDist and TOMO.To_MultiDist=RMO.ToDist and TOMO.Issue_Center=RMO.Issue_Center and TOMO.Branch=RMO.Branch and TOMO.Godown=RMO.Godown) where RMO.SubSMO='" + hdfSubSMO.Value + "' and RMO.Commodity='25' and RMO.Issue_Center='" + ddlIssueCentre.SelectedValue.ToString() + "' and RMO.Branch='" + ddlBranch.SelectedValue.ToString() + "' and RMO.Godown='" + ddlGodown.SelectedValue.ToString() + "') As ABC";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtQuantity.Text = txtQty.Text = ds.Tables[0].Rows[0]["RMORequiredQuantity"].ToString();

                    if (txtQty.Text == "0.00")
                    {
                        txtQty.Enabled = btnAdd.Enabled = false;
                    }


                    if (hdfOnlyOne.Value == "1")
                    {
                        GetTotalQtyForTO();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्तकर्ता जिले द्वारा मूवमेंट प्लान नहीं बनाया गया हैं, कृपया प्राप्तकर्ता जिला कार्यालय " + ddlSendingDist.SelectedItem.ToString() + " से संपर्क करें|'); </script> ");
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

    public void GetTotalQtyForTO()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select ISNULL(Sum(RequiredQuantity),0) As GetTotalTOQty From TO_AgainstHO_MO Where SubSMO='" + hdfSubSMO.Value + "' and Commodity='25'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfGetTotalTOQty.Value = ds.Tables[0].Rows[0]["GetTotalTOQty"].ToString();
                    hdfOnlyOne.Value = "0";
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

}