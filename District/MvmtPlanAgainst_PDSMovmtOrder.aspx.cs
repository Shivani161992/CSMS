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

public partial class District_MvmtPlanAgainst_PDSMovmtOrder_HO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    double QtyTotal = 0;
    string districtid = "", MONumber = "", SMONumber = "";

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
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
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

                string select = "select MoveOrdernum,SMO from StateMovementOrder where ToDist= '" + districtid + "' and (RecAgainstHO='N' and ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and IsAccepted='Y' " +
                                 " Union All " +
                                 "Select MoveOrdernum,SubSMO From StateSubMovementOrder where ToOtherDist='" + districtid + "' and (IsMvmtPlan='N' and ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and IsAccepted='Y' and ToDist!=ToOtherDist order by MoveOrdernum";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMONumber.DataSource = ds.Tables[0];
                    ddlMONumber.DataTextField = "MoveOrdernum";
                    ddlMONumber.DataValueField = "SMO";
                    ddlMONumber.DataBind();
                    ddlMONumber.Items.Insert(0, "--Select--");

                    GetMPIssueCentre();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('MO Number Is Not Available'); </script> ");
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
        districtid = Session["dist_id"].ToString();
        string MONumber = ddlMONumber.SelectedItem.ToString();
        string SMONumber = ddlMONumber.SelectedValue.ToString();
        string IsCancelled = "";
        hdfModeofDist.Value = hdfSMOSMO.Value = "";
        txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtQuantity.Text = txtQty.Text =  "";
        txtQty.Enabled = false;
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();

        if (ddlMONumber.SelectedIndex > 0)
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string CheckModeofDist = "Select ModeofDist,Commodity,IsCancelled From StateMovementOrder Where MoveOrdernum='" + MONumber + "' and IsAccepted='Y'";
                    da1 = new SqlDataAdapter(CheckModeofDist, con);

                    ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        hdfModeofDist.Value = ds1.Tables[0].Rows[0]["ModeofDist"].ToString();
                        hdfCommodity25.Value = ds1.Tables[0].Rows[0]["Commodity"].ToString();
                        IsCancelled = ds1.Tables[0].Rows[0]["IsCancelled"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Found For Mode of District'); </script> ");
                        return;
                    }

                    if (IsCancelled == "Y")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Movement Order को मुख्यालय द्वारा निरस्त कर दिया गया हैं| आप इस Movement Order के विरुद्ध Movement Plan नहीं बना सकते|'); </script> ");
                        return;
                    }
                    else
                    {
                        if (hdfModeofDist.Value == "Other" || hdfModeofDist.Value == "Both")
                        {
                            string select = "";

                            if (hdfCommodity25.Value == "25")
                            {
                                select = string.Format("select SMO,(select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= SubMO.Commodity) ComdtyName, Commodity,CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= SubMO.ModeofDispatch) ModeofDispatchName, ModeofDispatch,MOCreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= SubMO.ToDist) ToDistName, ToDist,(QtyByDist) As QuantityMT,QtyByDist from StateSubMovementOrder SubMO where MoveOrdernum='" + MONumber + "' and ToOtherDist='" + districtid + "' and SubSMO='" + SMONumber + "'");
                            }
                            else
                            {
                                select = string.Format("select SMO,(select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= SubMO.Commodity) ComdtyName, Commodity,CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= SubMO.ModeofDispatch) ModeofDispatchName, ModeofDispatch,MOCreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= SubMO.ToDist) ToDistName, ToDist,(QtyByDist/10) As QuantityMT,QtyByDist from StateSubMovementOrder SubMO where MoveOrdernum='" + MONumber + "' and ToOtherDist='" + districtid + "' and SubSMO='" + SMONumber + "'");
                            }

                            da = new SqlDataAdapter(select, con);

                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                txtComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                                hdfComdtyValue.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();

                                if (hdfComdtyValue.Value == "25")
                                {
                                    lblMT.Text = lblMT0.Text = "(Bales)";
                                }
                                else
                                {
                                    lblMT.Text = lblMT0.Text = "(MT)";
                                }

                                txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                                txtDispatch.Text = ds.Tables[0].Rows[0]["ModeofDispatchName"].ToString();
                                hdfDispatchModeValue.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();

                                DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["MOCreatedDate"].ToString());
                                txtDateMo.Text = CreatedDate.ToString("dd/MMM/yyyy");

                                DateTime EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReachDate"].ToString());
                                txtEndDate.Text = EndDate.ToString("dd/MMM/yyyy");

                                txtFrmDist.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                                hdfFrmDistValue.Value = ds.Tables[0].Rows[0]["ToDist"].ToString();

                                txtQuantity.Text = ds.Tables[0].Rows[0]["QuantityMT"].ToString();

                                hdfQuantityValue.Value = ds.Tables[0].Rows[0]["QtyByDist"].ToString();
                                hdfSMOSMO.Value = ds.Tables[0].Rows[0]["SMO"].ToString();

                                if (txtQuantity.Text != "" && double.Parse(txtQuantity.Text) > 0)
                                {
                                    txtQty.Enabled = true;
                                    txtQty.Text = txtQuantity.Text;
                                }
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Found For Sub Movement Order'); </script> ");
                                txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtQty.Text = txtQuantity.Text = "";
                                txtQty.Enabled = false;
                            }
                        }
                        else
                        {
                            string select = "";
                            if (hdfCommodity25.Value == "25")
                            {
                                select = string.Format("select (select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= Commodity) ComdtyName, Commodity,CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= ModeofDispatch) ModeofDispatchName, ModeofDispatch,CreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= FrmDist) FrmDistName, FrmDist,(Quantity)QuantityMT,Quantity from StateMovementOrder where MoveOrdernum='" + MONumber + "' and ToDist='" + districtid + "' and SMO='" + SMONumber + "'");
                            }
                            else
                            {
                                select = string.Format("select (select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= Commodity) ComdtyName, Commodity,CropYear,(select Source_Name From Source_Arrival_Type where Source_ID= ModeofDispatch) ModeofDispatchName, ModeofDispatch,CreatedDate,ReachDate,(select district_name From pds.districtsmp where district_code= FrmDist) FrmDistName, FrmDist,(Quantity/10)QuantityMT,Quantity from StateMovementOrder where MoveOrdernum='" + MONumber + "' and ToDist='" + districtid + "' and SMO='" + SMONumber + "'");
                            }
                            da = new SqlDataAdapter(select, con);

                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                txtComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                                hdfComdtyValue.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();

                                txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                                txtDispatch.Text = ds.Tables[0].Rows[0]["ModeofDispatchName"].ToString();
                                hdfDispatchModeValue.Value = ds.Tables[0].Rows[0]["ModeofDispatch"].ToString();

                                DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                                txtDateMo.Text = CreatedDate.ToString("dd/MMM/yyyy");

                                DateTime EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReachDate"].ToString());
                                txtEndDate.Text = EndDate.ToString("dd/MMM/yyyy");

                                txtFrmDist.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                                hdfFrmDistValue.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();

                                txtQuantity.Text = ds.Tables[0].Rows[0]["QuantityMT"].ToString();

                                hdfQuantityValue.Value = ds.Tables[0].Rows[0]["Quantity"].ToString();

                                if (txtQuantity.Text != "" && double.Parse(txtQuantity.Text) > 0)
                                {
                                    txtQty.Enabled = true;
                                    txtQty.Text = txtQuantity.Text;
                                }
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Found'); </script> ");
                                txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtQty.Text = txtQuantity.Text = "";
                                txtQty.Enabled = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtQty.Text = txtQuantity.Text = "";
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
            txtComdty.Text = txtCropYear.Text = txtDispatch.Text = txtDateMo.Text = txtEndDate.Text = txtFrmDist.Text = txtQty.Text = txtQuantity.Text = "";
            txtQty.Enabled = false;
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
        double TotalQty = double.Parse(txtQuantity.Text);
        double AllotedQty = double.Parse(txtQty.Text);

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
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Allow Because Alloted Quantity Is Greater Than Quantity'); </script> ");
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
                ddlMONumber.Enabled = false;
                fillgrid();

                txtQuantity.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();

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
            e.Row.Cells[4].Text = "Total Qty = " + QtyTotal.ToString("0.00");
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
        double TotalQty = double.Parse(txtQuantity.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (txtCropYear.Text != "" || txtDispatch.Text != "")
        {
            if (TotalQty == 0 && AllotedQty == 0)
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertMODate = ServerDate.getDate_MDY(txtDateMo.Text);
                string ConvertEndDate = ServerDate.getDate_MDY(txtEndDate.Text);

                MONumber = ddlMONumber.SelectedItem.ToString();
                SMONumber = ddlMONumber.SelectedValue.ToString();

                districtid = Session["dist_id"].ToString();
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string instr = "";

                            if (hdfModeofDist.Value == "Other" || hdfModeofDist.Value == "Both")
                            {
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                    "Update StateSubMovementOrder Set IsMvmtPlan='Y' where MoveOrdernum='" + MONumber + "' and SubSMO='" + SMONumber + "' and ToOtherDist='" + districtid + "';";

                                DataTable dt = adddetails();
                                if (dt != null)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        string RMO = SMONumber + districtid + (i + 1);
                                        instr += "Insert into RecAgainst_StateMovementOrder(MoveOrdernum,SMO,SubSMO,RMO,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,ModeofDispatch,DispatchAgainstMO,ReceivedAgainstMO,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,IssuedQty,ModeofDist) values('" + MONumber + "','" + hdfSMOSMO.Value + "','" + SMONumber + "','" + RMO + "','" + hdfFrmDistValue.Value + "','" + districtid + "','" + hdfComdtyValue.Value + "','" + hdfQuantityValue.Value + "','" + txtCropYear.Text + "','" + ConvertEndDate + "','" + ConvertMODate + "','" + hdfDispatchModeValue.Value + "','N','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + hdfModeofDist.Value + "');";
                                    }
                                }
                            }
                            else
                            {
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                    "Update StateMovementOrder Set RecAgainstHO='Y' where MoveOrdernum='" + MONumber + "' and SMO='" + SMONumber + "' and ToDist='" + districtid + "';";

                                DataTable dt = adddetails();
                                if (dt != null)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        string RMO = SMONumber + districtid + (i + 1);
                                        instr += "Insert into RecAgainst_StateMovementOrder(MoveOrdernum,SMO,RMO,FrmDist,ToDist,Commodity,Quantity,CropYear,ReachDate,MODate,ModeofDispatch,DispatchAgainstMO,ReceivedAgainstMO,CreatedDate,IP,Issue_Center,Branch,Godown,RequiredQuantity,RemQty,IssuedQty) values('" + MONumber + "','" + SMONumber + "','" + RMO + "','" + hdfFrmDistValue.Value + "','" + districtid + "','" + hdfComdtyValue.Value + "','" + hdfQuantityValue.Value + "','" + txtCropYear.Text + "','" + ConvertEndDate + "','" + ConvertMODate + "','" + hdfDispatchModeValue.Value + "','N','N',GETDATE(),'" + GetIp + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "','" + dt.Rows[i]["QuantityQtls"] + "');";
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

                                if (hdfModeofDist.Value == "Other" || hdfModeofDist.Value == "Both")
                                {
                                    Session["SubMovmtOrderNo"] = hdfSMOSMO.Value;
                                }
                                else
                                {
                                    Session["SubMovmtOrderNo"] = SMONumber;
                                }

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print_MvmtPlanAgainst_PDSMO.aspx";
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
        txtQty.Focus();
    }
}