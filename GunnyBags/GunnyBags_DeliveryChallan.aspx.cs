﻿using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_GunnyBags_DeliveryChallan : System.Web.UI.Page
{
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;

    double QtyTotal = 0;
    public string sid = "";
    string OwnDist, OtherDist;

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int RemainingQty;
    string TotalQty;
    public string ICID, DistId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                string fromdate = Request.Form[txtDateofReceipt.UniqueID];
                txtDateofReceipt.Text = fromdate;
                txtSenIC.Text = Session["issue_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //DateTime _date;
                //string day = "";
                //_date = DateTime.Parse("5/MAY/2012");
                //day = _date.ToString("dd-mm-yyyy");
                GetReceivingID();
                GetSenDist();
               
                
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetSenDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string DistCode = Session["dist_id"].ToString();
                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistId + "'  Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["OwnDist"]=txtdistrict.Text = txtSenDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
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
    public void GetSenBranch()
    {
        string Dist_Id = Session["dist_id"].ToString();
        ICID = Session["issue_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ICID + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSenBranch.DataSource = ds.Tables[0];
                        ddlSenBranch.DataTextField = "DepotName";
                        ddlSenBranch.DataValueField = "BranchID";
                        ddlSenBranch.DataBind();
                        ddlSenBranch.Items.Insert(0, "--Select--");
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
                        ddlSenBranch.DataSource = ds.Tables[0];
                        ddlSenBranch.DataTextField = "DepotName";
                        ddlSenBranch.DataValueField = "BranchId";
                        ddlSenBranch.DataBind();
                        ddlSenBranch.Items.Insert(0, "--Select--");
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
    protected void ddlRecBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSenGodown.Items.Clear();
        //ddlCMRDONo.Items.Clear();
       // ddlDeliveryChallan.Items.Clear();

        //hdfAdjustCMRDO.Value = "";

        if (ddlSenBranch.SelectedIndex > 0)
        {
            GetSenGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
        }

    }

    public void GetSenGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlSenBranch.SelectedValue.ToString() + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSenGodown.DataSource = ds.Tables[0];
                        ddlSenGodown.DataTextField = "Godown_Name";
                        ddlSenGodown.DataValueField = "Godown_ID";
                        ddlSenGodown.DataBind();
                        ddlSenGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
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

    public void GetReceivingID()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("Select distinct RR_Receive_ID from GunnyBags_Receiving_RR_Dist where District='" + DistCode + "' order By RR_Receive_ID ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlReceiID.DataSource = ds.Tables[0];
                        ddlReceiID.DataTextField = "RR_Receive_ID";
                        ddlReceiID.DataValueField = "RR_Receive_ID";
                        ddlReceiID.DataBind();
                        ddlReceiID.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving ID is Not Available'); </script> ");
                    return;
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
    protected void ddlReceiID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReceiID.SelectedIndex > 0)
        {
            GetData();
            GetTransporterName();
            GetSenBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving ID'); </script> ");
            return;

        }
    }
    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {

                string DistCode = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("Select distinct Total_Received_QTY, CropYear, BagsType  from GunnyBags_Receiving_RR_Dist where RR_Receive_ID='" + ddlReceiID.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TotalQty = txtReceivedQty.Text = ds.Tables[0].Rows[0]["Total_Received_QTY"].ToString();
                        txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        txtBagsType.Text = ds.Tables[0].Rows[0]["BagsType"].ToString();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Total Received Quantity, Cropyear and BagsType are not available'); </script> ");
                    return;
                }
                string qry = string.Format("select isnull(sum(Qty_Issued),0) as Qty from GunnyBags_DeliveryChallan where RR_Receive_ID='" + ddlReceiID.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(qry, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        string IssuedQuantity = ds.Tables[0].Rows[0]["Qty"].ToString();
                        int IssuedQty = Convert.ToInt32(IssuedQuantity);
                        RemainingQty = Convert.ToInt32(TotalQty) - IssuedQty;
                        txtRemainingQty.Text = Convert.ToString(RemainingQty);


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

    public void GetTransporterName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("select distinct Transporter_ID, Transporter_Name from Transporter_Table where Transport_ID='1' and Distt_ID='" + DistCode + "' order by Transporter_Name ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTransporterName.DataSource = ds.Tables[0];
                        ddlTransporterName.DataTextField = "Transporter_Name";
                        ddlTransporterName.DataValueField = "Transporter_ID";
                        ddlTransporterName.DataBind();
                        ddlTransporterName.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transporter Name is Not Available'); </script> ");
                    return;
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

    protected void rdbOwnDist_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbOwnDist.Checked == true)
        {
            trDistrictIC.Visible = true;
            trBranchGodown.Visible = true;
            txtDelidist.Visible = true;
            ddlDelidist.Visible = false;

            txtDelidist.Text = Session["OwnDist"].ToString();
            GetIssueCenter();
        }
    }
    protected void rdbOtherDist_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbOtherDist.Checked == true)
        {
            trDistrictIC.Visible = true;
            trBranchGodown.Visible = true;
            ddlDelidist.Visible = true;
            txtDelidist.Visible = false;
            GetDistrict();

        }

    }

    public void GetDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string DistCode = Session["dist_id"].ToString();
                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code!='" + DistCode + "'  Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDelidist.DataSource = ds.Tables[0];
                        ddlDelidist.DataTextField = "district_name";
                        ddlDelidist.DataValueField = "district_code";
                        ddlDelidist.DataBind();
                        ddlDelidist.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
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

    protected void ddlDelidist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDelidist.SelectedIndex > 0)
        {
            GetIssueCenter();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetIssueCenter()
    {
        if (rdbOwnDist.Checked == true)
        {
            string districtid = Session["dist_id"].ToString();
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
                        ddlIC.DataSource = ds.Tables[0];
                        ddlIC.DataTextField = "DepotName";
                        ddlIC.DataValueField = "DepotID";
                        ddlIC.DataBind();
                        ddlIC.Items.Insert(0, "--Select--");
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
        else if (rdbOtherDist.Checked == true)
        {
            string districtid = Session["dist_id"].ToString();
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDelidist.SelectedValue.ToString() + "' order by DepotName");
                    da = new SqlDataAdapter(select, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlIC.DataSource = ds.Tables[0];
                        ddlIC.DataTextField = "DepotName";
                        ddlIC.DataValueField = "DepotID";
                        ddlIC.DataBind();
                        ddlIC.Items.Insert(0, "--Select--");
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

    }

    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIC.SelectedIndex > 0)
        {
            GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select IssueCenter'); </script> ");
            return;
        }
    }

    public void GetBranch()
    {
        string districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddlIC.SelectedValue.ToString());
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
                    if (rdbOwnDist.Checked == true)
                    {
                        string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + txtDelidist.Text + "' order by DepotName");
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
                    else if (rdbOtherDist.Checked == true)
                    {
                        string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDelidist.SelectedValue.ToString() + "' order by DepotName");
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
        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
            return;
        }
    }
    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name";
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
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodown.SelectedIndex > 0)
        {
            bttSubmit.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
    }

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlReceiID.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving ID'); </script> ");
            return;
        }
        else if (txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter TC Number'); </script> ");
            return;
        }
        else if (txtDateofReceipt.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Date of Quantity Issued '); </script> ");
            return;
        }
        else if (ddlTransporterName.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Transporter Name '); </script> ");
            return;
        }
        else if (txtTruckNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Truck Number '); </script> ");
            return;
        }

        else if (txtQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity that is to be issued '); </script> ");
            return;
        }
        else if ((rdbOtherDist.Checked == false) && (rdbOwnDist.Checked == false))
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया चुने की जारी की गयी मात्रा स्वयं के जिले में जमा होगी या अन्य जिले में जमा होगी|'); </script> ");
            return;
        }
        else if (rdbOtherDist.Checked == true && ddlDelidist.SelectedIndex < 0)
        {


            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination District'); </script> ");
            return;

        }
        else if (ddlIC.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Issue Center '); </script> ");
            return;
        }
        else if (ddlBranch.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Branch '); </script> ");
            return;
        }
        else if (ddlGodown.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Godown '); </script> ");
            return;
        }

        else
        {
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string DistCode = Session["dist_id"].ToString();
                    string IC_Id = Session["issue_id"].ToString();
                    string qrey = "select max(DC_Number_ID) as DC_Number_ID from GunnyBags_DeliveryChallan where LEN(DC_Number_ID)<15 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    //mobj1 = new MoveChallan(ComObj);
                    //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                    //DataSet ds = new DataSet();
                    // dmax.Fill(ds);
                    // DataTable dt = ds.Tables[""];
                    DataRow dr = ds.Tables[0].Rows[0];
                    //gatepass = dr["Inspector_ID"].ToString();
                    gatepass = ds.Tables[0].Rows[0]["DC_Number_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "1718" + DistCode + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                    string BookNumber = "DC" + gatepass;

                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                    string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDateofReceipt.Text);


                    if (rdbOwnDist.Checked == true)
                    {
                        OwnDist = "Y";
                        OtherDist = "N";
                        string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued, SenIC, SenBranch, SenGodown, IsReceived, DC_BookNumber) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlReceiID.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + DistCode + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "','" + IC_Id + "','" + ddlSenBranch.SelectedValue.ToString() + "','" + ddlSenGodown.SelectedValue.ToString() + "','N','" + BookNumber + "')";
                        cmd = new SqlCommand(strinsert, con);
                        string check = (string)cmd.ExecuteScalar();
                    }
                    else if (rdbOtherDist.Checked == true)
                    {
                        OwnDist = "N";
                        OtherDist = "Y";
                        string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued, SenIC, SenBranch, SenGodown, IsReceived, DC_BookNumber) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlReceiID.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + ddlTransporterName.SelectedValue.ToString() + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + ddlDelidist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "','" + IC_Id + "','" + ddlSenBranch.SelectedValue.ToString() + "','" + ddlSenGodown.SelectedValue.ToString() + "','N','" + BookNumber + "')";
                        cmd = new SqlCommand(strinsert, con);
                        string check = (string)cmd.ExecuteScalar();
                    }


                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    ddlSenBranch.Enabled = false;
                    ddlSenGodown.Enabled = false;
                    bttSubmit.Enabled = false;
                    ddlReceiID.Enabled = false;
                    txtTruckNo.Enabled = false;
                    txtTCNo.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtDateofReceipt.Enabled = false;
                    ddlTransporterName.Enabled = false;
                    ddlDelidist.Enabled = false;
                    ddlIC.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlGodown.Enabled = false;
                    trID.Visible = true;
                    bttSubmit.Visible = false;
                    bttprint.Enabled = true;
                    bttprint.Visible = true;

                    Label1.Text = "Delivery ID is:-" + BookNumber;


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

    protected void bttprint_Click(object sender, EventArgs e)
    {

    }
    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Gunny_BagsHome.aspx");
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtRemainingQty.Text) < Convert.ToInt32(txtQuantity.Text))
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी की जाने वाली उपलभ मात्रा से अधिक है|'); </script> ");
            txtQuantity.Text = "";
            return;
        }

    }
}