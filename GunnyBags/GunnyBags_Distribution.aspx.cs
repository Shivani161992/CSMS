using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class GunnyBags_GunnyBags_Distribution : System.Web.UI.Page
{
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;
    string transporter, RDist, RailHead, Rgodown="";

    double QtyTotal = 0;
    public string sid = "";
    string OwnDist, OtherDist;

    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int RemainingQty;
    string TotalQty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {

                txtdistrict.Text = Session["dist_name"].ToString();
                string DistCode = Session["dist_id"].ToString();
                string fromdate = Request.Form[txtDateofReceipt.UniqueID];
                txtDateofReceipt.Text = fromdate;

                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Insert(0, "--Select--");
                

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //DateTime _date;
                //string day = "";
                //_date = DateTime.Parse("5/MAY/2012");
                //day = _date.ToString("dd-mm-yyyy");
               // GetReceivingID();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }


    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCropYear.SelectedIndex > 0)
        {
            GetReceivingID();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CropYear'); </script> ");
            return;

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
                string select = string.Format("select TransportID from GunnyBaggs_AllocationTransporter where UserID='"+DistCode+"' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"' order By TransportID ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTO.DataSource = ds.Tables[0];
                        ddlTO.DataTextField = "TransportID";
                        ddlTO.DataValueField = "TransportID";
                        ddlTO.DataBind();
                        ddlTO.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Number is Not Available'); </script> ");
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
        if (ddlTO.SelectedIndex > 0)
        {
            GetData();
            //GetTransporterName();
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
                string select = string.Format("select Indent, RailHead, R.RailHead_Name as RailHead_Name, RRID, TransportedQuantity, BagType, Transporter, TT.Transporter_Name as Transporter_Name,   D.district_name, G.Godown_Name  from GunnyBaggs_AllocationTransporter  inner join tbl_Rail_Head as R on R.RailHead_Code=GunnyBaggs_AllocationTransporter.RailHead   inner join Transporter_Table as TT on TT.Transporter_ID=GunnyBaggs_AllocationTransporter.Transporter    inner join PDS.districtsmp as D on D.district_code=GunnyBaggs_AllocationTransporter.GodownDistrict inner join tbl_MetaData_GODOWN as G on G.Godown_ID=GunnyBaggs_AllocationTransporter.Godown where UserID='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and TransportID='" + ddlTO.SelectedValue.ToString() + "'  and TT.Transport_ID='1'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TotalQty = txtReceivedQty.Text = ds.Tables[0].Rows[0]["TransportedQuantity"].ToString();
                        //txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                       txtBagsType.Text = ds.Tables[0].Rows[0]["BagType"].ToString();

                       txtIN.Text = ds.Tables[0].Rows[0]["Indent"].ToString();

                       txtRH.Text = ds.Tables[0].Rows[0]["RailHead_Name"].ToString();

                       txtRRNum.Text = ds.Tables[0].Rows[0]["RRID"].ToString();
                       txtTransporterName.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                       txtGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                       txtDelidist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is not available'); </script> ");
                    return;
                }
                string qry = string.Format("select isnull(sum(isnull(IssueQty,0)),0) as IssueQty from GunnyBags_DeliveryChallan_Dist where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and Transport_Order='" + ddlTO.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(qry, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        string IssuedQuantity = ds.Tables[0].Rows[0]["IssueQty"].ToString();

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

    //public void GetTransporterName()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            string DistCode = Session["dist_id"].ToString();
    //            con.Open();
    //            string select = string.Format("select distinct Transporter_ID, Transporter_Name from Transporter_Table where Transport_ID='1' and Distt_ID='" + DistCode + "' order by Transporter_Name ");
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    //ddlTransporterName.DataSource = ds.Tables[0];
    //                    //ddlTransporterName.DataTextField = "Transporter_Name";
    //                    //ddlTransporterName.DataValueField = "Transporter_ID";
    //                    //ddlTransporterName.DataBind();
    //                    //ddlTransporterName.Items.Insert(0, "--Select--");
    //                }
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transporter Name is Not Available'); </script> ");
    //                return;
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

    //protected void rdbOwnDist_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbOwnDist.Checked == true)
    //    {
    //        trDistrictIC.Visible = true;
    //        trBranchGodown.Visible = true;
    //        txtDelidist.Visible = true;
    //        ddlDelidist.Visible = false;

    //        txtDelidist.Text = Session["dist_name"].ToString();
    //        GetIssueCenter();
    //    }
    //}
    //protected void rdbOtherDist_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbOtherDist.Checked == true)
    //    {
    //        trDistrictIC.Visible = true;
    //        trBranchGodown.Visible = true;
    //        ddlDelidist.Visible = true;
    //        txtDelidist.Visible = false;
    //        GetDistrict();

    //    }

    //}

    //public void GetDistrict()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string DistCode = Session["dist_id"].ToString();
    //            string select = "";
    //            select = "SELECT district_name,district_code FROM pds.districtsmp where district_code!='" + DistCode + "'  Order By district_name";
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlDelidist.DataSource = ds.Tables[0];
    //                    ddlDelidist.DataTextField = "district_name";
    //                    ddlDelidist.DataValueField = "district_code";
    //                    ddlDelidist.DataBind();
    //                    ddlDelidist.Items.Insert(0, "--Select--");
    //                    //Ddldist.SelectedValue = Session["dist_id"].ToString();
    //                    // GetMPIssueCentre();
    //                }
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

    //protected void ddlDelidist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDelidist.SelectedIndex > 0)
    //    {
    //        GetIssueCenter();
    //    }
    //    else
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
    //        return;
    //    }
    //}

    //public void GetIssueCenter()
    //{
    //    if (rdbOwnDist.Checked == true)
    //    {
    //        string districtid = Session["dist_id"].ToString();
    //        using (con = new SqlConnection(strcon))
    //        {
    //            try
    //            {
    //                con.Open();

    //                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' order by DepotName");
    //                da = new SqlDataAdapter(select, con);

    //                ds = new DataSet();
    //                da.Fill(ds);
    //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlIC.DataSource = ds.Tables[0];
    //                    ddlIC.DataTextField = "DepotName";
    //                    ddlIC.DataValueField = "DepotID";
    //                    ddlIC.DataBind();
    //                    ddlIC.Items.Insert(0, "--Select--");
    //                }
    //                else
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //            }

    //            finally
    //            {
    //                if (con.State != ConnectionState.Closed)
    //                {
    //                    con.Close();
    //                }
    //            }
    //        }
    //    }
    //    else if (rdbOtherDist.Checked == true)
    //    {
    //        string districtid = Session["dist_id"].ToString();
    //        using (con = new SqlConnection(strcon))
    //        {
    //            try
    //            {
    //                con.Open();

    //                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDelidist.SelectedValue.ToString() + "' order by DepotName");
    //                da = new SqlDataAdapter(select, con);

    //                ds = new DataSet();
    //                da.Fill(ds);
    //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlIC.DataSource = ds.Tables[0];
    //                    ddlIC.DataTextField = "DepotName";
    //                    ddlIC.DataValueField = "DepotID";
    //                    ddlIC.DataBind();
    //                    ddlIC.Items.Insert(0, "--Select--");
    //                }
    //                else
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //            }

    //            finally
    //            {
    //                if (con.State != ConnectionState.Closed)
    //                {
    //                    con.Close();
    //                }
    //            }
    //        }
    //    }

    //}

    //protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlIC.SelectedIndex > 0)
    //    {
    //        GetBranch();
    //    }
    //    else
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select IssueCenter'); </script> ");
    //        return;
    //    }
    //}

    //public void GetBranch()
    //{
    //    string districtid = Session["dist_id"].ToString();

    //    using (con_MPStorage = new SqlConnection(strcon_MPStorage))
    //    {
    //        try
    //        {
    //            con_MPStorage.Open();
    //            string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='{0}'", ddlIC.SelectedValue.ToString());
    //            da = new SqlDataAdapter(select, con_MPStorage);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds != null)
    //            {
    //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlBranch.DataSource = ds.Tables[0];
    //                    ddlBranch.DataTextField = "DepotName";
    //                    ddlBranch.DataValueField = "BranchID";
    //                    ddlBranch.DataBind();
    //                    ddlBranch.Items.Insert(0, "--Select--");
    //                }
    //                else
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
    //                }
    //            }
    //            else
    //            {
    //                if (rdbOwnDist.Checked == true)
    //                {
    //                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + txtDelidist.Text + "' order by DepotName");
    //                    da = new SqlDataAdapter(select1, con_MPStorage);

    //                    ds = new DataSet();
    //                    da.Fill(ds);
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        ddlBranch.DataSource = ds.Tables[0];
    //                        ddlBranch.DataTextField = "DepotName";
    //                        ddlBranch.DataValueField = "BranchId";
    //                        ddlBranch.DataBind();
    //                        ddlBranch.Items.Insert(0, "--Select--");
    //                    }
    //                }
    //                else if (rdbOtherDist.Checked == true)
    //                {
    //                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddlDelidist.SelectedValue.ToString() + "' order by DepotName");
    //                    da = new SqlDataAdapter(select1, con_MPStorage);

    //                    ds = new DataSet();
    //                    da.Fill(ds);
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        ddlBranch.DataSource = ds.Tables[0];
    //                        ddlBranch.DataTextField = "DepotName";
    //                        ddlBranch.DataValueField = "BranchId";
    //                        ddlBranch.DataBind();
    //                        ddlBranch.Items.Insert(0, "--Select--");
    //                    }
    //                }

                    
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con_MPStorage.State != ConnectionState.Closed)
    //            {
    //                con_MPStorage.Close();
    //            }
    //        }
    //    }
    //}
    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlBranch.SelectedIndex > 0)
    //    {
    //        GetGodown();
    //    }
    //    else
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
    //        return;
    //    }
    //}
    //public void GetGodown()
    //{
    //    using (con_MPStorage = new SqlConnection(strcon_MPStorage))
    //    {
    //        try
    //        {
    //            con_MPStorage.Open();
    //            string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name";
    //            da = new SqlDataAdapter(select, con_MPStorage);

    //            ds = new DataSet();
    //            da.Fill(ds);
    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlGodown.DataSource = ds.Tables[0];
    //                    ddlGodown.DataTextField = "Godown_Name";
    //                    ddlGodown.DataValueField = "Godown_ID";
    //                    ddlGodown.DataBind();
    //                    ddlGodown.Items.Insert(0, "--Select--");
    //                }
    //                else
    //                {
    //                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con_MPStorage.State != ConnectionState.Closed)
    //            {
    //                con_MPStorage.Close();
    //            }
    //        }
    //    }
    //}
    //protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlGodown.SelectedIndex > 0)
    //    {
    //        bttSubmit.Enabled = true;
    //    }
    //    else
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
    //        return;
    //    }
    //}

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlTO.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving ID'); </script> ");
            return;
        }
        else if (txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter TC Number'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
        else if (txtDateofReceipt.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Date of Quantity Issued '); </script> ");
            return;
        }
        //else if (ddlTransporterName.SelectedIndex < 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Transporter Name '); </script> ");
        //    return;
        //}
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
        //else if ((rdbOtherDist.Checked == false) && (rdbOwnDist.Checked == false))
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया चुने की जारी की गयी मात्रा स्वयं के जिले में जमा होगी या अन्य जिले में जमा होगी|'); </script> ");
        //    return;
        //}
        //else if (rdbOtherDist.Checked == true && ddlDelidist.SelectedIndex < 0)
        //{


        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Destination District'); </script> ");
        //    return;

        //}
        //else if (ddlIC.SelectedIndex < 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Issue Center '); </script> ");
        //    return;
        //}
        //else if (ddlBranch.SelectedIndex < 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Branch '); </script> ");
        //    return;
        //}
        //else if (ddlGodown.SelectedIndex < 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Godown '); </script> ");
        //    return;
        //}

        else
        {
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                   
                    string DistCode = Session["dist_id"].ToString();
                    string qrey = "select max(DeliveryChallan_ID) as DeliveryChallan_ID from GunnyBags_DeliveryChallan_Dist where LEN(DeliveryChallan_ID)<20 ";
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
                    gatepass = ds.Tables[0].Rows[0]["DeliveryChallan_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "17891800";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    string select = string.Format("select Godown,  Indent, RailHead, R.RailHead_Name as RailHead_Name, RRID, TransportedQuantity, BagType, Transporter, TT.Transporter_Name as Transporter_Name,GodownDistrict,   D.district_name, G.Godown_Name  from GunnyBaggs_AllocationTransporter  inner join tbl_Rail_Head as R on R.RailHead_Code=GunnyBaggs_AllocationTransporter.RailHead   inner join Transporter_Table as TT on TT.Transporter_ID=GunnyBaggs_AllocationTransporter.Transporter    inner join PDS.districtsmp as D on D.district_code=GunnyBaggs_AllocationTransporter.GodownDistrict inner join tbl_MetaData_GODOWN as G on G.Godown_ID=GunnyBaggs_AllocationTransporter.Godown where UserID='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and TransportID='" + ddlTO.SelectedValue.ToString() + "'  and TT.Transport_ID='1'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       
                        transporter = ds.Tables[0].Rows[0]["Transporter"].ToString();
                        RDist = ds.Tables[0].Rows[0]["GodownDistrict"].ToString();
                        RailHead = ds.Tables[0].Rows[0]["RailHead"].ToString();
                        Rgodown = ds.Tables[0].Rows[0]["Godown"].ToString();
                    }
                }

                string BookNumber = "DC" + DistCode + gatepass;

                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                    string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDateofReceipt.Text);
                    string strinsert = "insert into GunnyBags_DeliveryChallan_Dist(DeliveryChallan_ID, Dist, CropYear, Transport_Order,BagsType, TotalQty,  IndentNumber, RailHead, RR_Number, Transporter_Number, TruckNumber, Toul_Receipt, TruckChallan, IssueDate, IssueQty ,RcdDist, RcdGodown,Createddate , IP, BookNumber, IsReceived) values ('" + gatepass + "','" + DistCode + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtIN.Text + "','" + RailHead + "','" + txtRRNum.Text + "','" + transporter + "','" + txtTruckNo.Text + "','" + txtReceipt.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtQuantity.Text + "','" + RDist + "','" + Rgodown + "',getdate(),'" + ip + "','" + BookNumber + "','N')";
                     cmd = new SqlCommand(strinsert, con);
                    string check = (string)cmd.ExecuteScalar();

                    //if (rdbOwnDist.Checked == true)
                    //{
                    //    OwnDist = "Y";
                    //    OtherDist = "N";
                    //    string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtTransporterName.Text + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + DistCode + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "')";
                    //    cmd = new SqlCommand(strinsert, con);
                    //    string check = (string)cmd.ExecuteScalar();
                    //}
                    //else if (rdbOtherDist.Checked == true)
                    //{
                    //    OwnDist = "N";
                    //    OtherDist = "Y";
                    //    string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtTransporterName.Text + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + ddlDelidist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "')";
                    //    cmd = new SqlCommand(strinsert, con);
                    //    string check = (string)cmd.ExecuteScalar();
                    //}


                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                    bttSubmit.Enabled = false;
                    bttSubmit.Visible = false ;
                    bttPrint.Visible = true;
                    bttPrint.Enabled = false;
                       
                    ddlTO.Enabled = false;
                    txtTruckNo.Enabled = false;
                    txtTCNo.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtDateofReceipt.Enabled = false;
                    txtReceipt.Enabled = false;

                    txtTransporterName.Enabled = false;
                    //ddlDelidist.Enabled = false;
                    //ddlIC.Enabled = false;
                    //ddlBranch.Enabled = false;
                    //ddlGodown.Enabled = false;
                    trID.Visible = true;
                    Label1.Text = "Delivery ID is" + BookNumber;


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
    

    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Gunny_Bags_Home.aspx");
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(txtRemainingQty.Text) < Convert.ToInt32(txtQuantity.Text))
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी की जाने वाली  मात्रा उपलभ मात्रा से अधिक है|'); </script> ");
            txtQuantity.Focus();

            return;
        }
        else 
        
        {
            bttSubmit.Enabled = true;

        }
        
    }

    protected void bttPrint_Click(object sender, EventArgs e)
    {

    }
}