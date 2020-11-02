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

public partial class PaddyMilling_CMRRejection_Challan : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string IC_Id = "", Dist_Id = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                txtTCNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTCNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTCNo.Attributes.Add("onchange", "return chksqltxt(this)");

                Session["RejCMR_Challan"] = null;

                //ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                //ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
                //ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));

                GetCropYearValues();

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                // GetBranch();
                GetCommodity();

                //GetBagsType();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtIssuedDate.Text = Request.Form[txtIssuedDate.UniqueID];
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id IN('3','4') order by Commodity_Id";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCommodity.DataSource = ds.Tables[0];
                    ddlCommodity.DataTextField = "Commodity_Name";
                    ddlCommodity.DataValueField = "Commodity_Id";
                    ddlCommodity.DataBind();
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

    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropYear.DataSource = ds.Tables[0];
                    ddlCropYear.DataTextField = "CropYear";
                    ddlCropYear.DataValueField = "CropYear";
                    ddlCropYear.DataBind();
                    ddlCropYear.Items.Insert(0, "--Select--");
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

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        ddlBranch.Items.Clear ();
        ddlGodown.Items.Clear();
        ddlstackNumber.Items.Clear();
        ddlRejectionNo.Items.Clear();
        ddlmillname.Items.Clear();
        ddlAgree.Items.Clear();
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";

        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";
        txtbagsType.Text = "";


        //ddlBranch.SelectedIndex = 0;
       
        if (ddlCropYear.SelectedIndex > 0)
        {
            GetBranch();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Crop Year चुने|'); </script> ");
        }
    }

    public void GetBranch()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
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



    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbagsType.Text = "";
        ddlGodown.Items.Clear();
        ddlstackNumber.Items.Clear();
        ddlRejectionNo.Items.Clear();
        ddlmillname.Items.Clear();
        ddlAgree.Items.Clear();
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";

        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";


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
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name");
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
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Godown Not Available'); </script> ");
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
        ddlstackNumber.Items.Clear();
        ddlRejectionNo.Items.Clear();
        ddlmillname.Items.Clear();
        ddlAgree.Items.Clear();
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";
        txtbagsType.Text = "";
        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";


        if (ddlGodown.SelectedIndex > 0)
        {
            GetStackNumber();


        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
        }
    }

    public void GetStackNumber()
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


    protected void ddlStackNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRejectionNo.Items.Clear();
        ddlmillname.Items.Clear();
        ddlAgree.Items.Clear();
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";
        txtbagsType.Text = "";
        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";

        if (ddlstackNumber.SelectedIndex > 0)
        {


            GetRejectionNumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Stack Number'); </script> ");
        }
    }

    public void GetRejectionNumber()
    {

        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select  distinct MSRejection from  tblStackrejection  where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and District_ID='" + Dist_Id + "' and ICenter_ID='" + IC_Id + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and Stack_ID='" + ddlstackNumber.SelectedValue.ToString() + "' and Status='Rejected'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRejectionNo.DataSource = ds.Tables[0];
                    ddlRejectionNo.DataTextField = "MSRejection";
                    ddlRejectionNo.DataValueField = "MSRejection";
                    ddlRejectionNo.DataBind();
                    ddlRejectionNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rejection Number is not available'); </script> ");
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

    protected void ddlRejectionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlmillname.Items.Clear();
        ddlAgree.Items.Clear();
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";
        txtbagsType.Text = "";
        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";
        if (ddlRejectionNo.SelectedIndex > 0)
        {
            GetMillerName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Rejection Number चुनें |'); </script> ");
        }
    }

    public void GetMillerName()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select distinct millname, MR.Mill_Name from tblStackrejection as SR inner join Miller_Registration_2017 as MR on MR.Registration_ID=SR.millname and MR.CropYear=SR.CropYear where MSRejection='" + ddlRejectionNo.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlmillname.DataSource = ds.Tables[0];
                    ddlmillname.DataTextField = "Mill_Name";
                    ddlmillname.DataValueField = "millname";
                    ddlmillname.DataBind();
                    ddlmillname.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Miller Name is not available'); </script> ");
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

    protected void ddlmillname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAgree.Items.Clear();
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";
        txtbagsType.Text = "";
        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";
        if (ddlmillname.SelectedIndex > 0)
        {
            GetAgreementNum();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Miller Name चुनें |'); </script> ");
        }
    }

    public void GetAgreementNum()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select distinct Agreement_ID from tblStackrejection as SR  where MSRejection='"+ddlRejectionNo.SelectedValue.ToString()+"' and SR.millname='"+ddlmillname.SelectedValue.ToString()+"'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAgree.DataSource = ds.Tables[0];
                    ddlAgree.DataTextField = "Agreement_ID";
                    ddlAgree.DataValueField = "Agreement_ID";
                    ddlAgree.DataBind();
                    ddlAgree.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Agreement Number is not available'); </script> ");
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
    protected void ddlAgree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlLot.Items.Clear();
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";
        txtbagsType.Text = "";
        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";
        if (ddlAgree.SelectedIndex > 0)
        {
            GetLotNum();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Lot Number चुनें |'); </script> ");
        }
    }
    public void GetLotNum()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select distinct LotNumber from tblStackrejection as SR  where MSRejection='"+ddlRejectionNo.SelectedValue.ToString()+"' and SR.millname='"+ddlmillname.SelectedValue.ToString()+"' and Agreement_ID='"+ddlAgree.SelectedValue.ToString()+"'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlLot.DataSource = ds.Tables[0];
                    ddlLot.DataTextField = "LotNumber";
                    ddlLot.DataValueField = "LotNumber";
                    ddlLot.DataBind();
                    ddlLot.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Lot Number is not available'); </script> ");
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

    protected void ddlLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtpaddyDO.Text = "";
        txtCMRDO.Text = "";
        txtCMRAccep.Text = "";
        txtRejQuant.Text = "";
        txtRejeBags.Text = "";
        txtrejecBagsType.Text = "";
        txtInspecHo.Text = "";
        txtMobNum.Text = "";
        txtDOR.Text = "";
        txtbagsType.Text = "";
        txtRemQty.Text = "";
        txtRemBags.Text = "";


        txtIssuedQty.Text = "";
        txtIssuedBags.Text = "";
        txtIssuedQty.Text = "";
        txtTCNo.Text = "";
        txtIssuedDate.Text = "";
        hdfMillId.Value = "";
        hdfDist.Value = "";
        hdfInspID.Value = "";
        hdfbagsType.Value = "";
        if (ddlLot.SelectedIndex > 0)
        {
            btnRecptSubmit.Enabled = true;
            GetData();
            GetReMData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Rejection Number चुनें |'); </script> ");
        }
    }

    public void GetData()
    {
        IC_Id = Session["issue_id"].ToString();
        Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                select = " select mill_phone, PaddyDO, CMRDO, CMR_Acceptance_No, Accept_CommonRice, firstBags, BT.BagType, SR.BagType as BagsCode, INSP.Inspector_Name, SR.Inspector_Name as InspHO, OtherMobileNum ,CONVERT(varchar(10), D_O_Inspection, 103) as D_O_Inspection   from tblStackrejection as SR  inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=SR.BagType inner join Inspector_Master_02017 as INSP on INSP.Inspector_ID=SR.Inspector_Name where MSRejection='" + ddlRejectionNo.SelectedValue.ToString() + "' and SR.millname='" + ddlmillname.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgree.SelectedValue.ToString() + "' and LotNumber='" + ddlLot.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtpaddyDO.Text = ds.Tables[0].Rows[0]["PaddyDO"].ToString();
                    txtCMRDO.Text = ds.Tables[0].Rows[0]["CMRDO"].ToString();
                    txtCMRAccep.Text = ds.Tables[0].Rows[0]["CMR_Acceptance_No"].ToString();
                    txtRejQuant.Text = ds.Tables[0].Rows[0]["Accept_CommonRice"].ToString();
                    txtRejeBags.Text = ds.Tables[0].Rows[0]["firstBags"].ToString();
                    txtrejecBagsType.Text = ds.Tables[0].Rows[0]["BagType"].ToString();
                    txtbagsType.Text = ds.Tables[0].Rows[0]["BagType"].ToString();
                    hdfbagsType.Value = ds.Tables[0].Rows[0]["BagsCode"].ToString();
                    txtInspecHo.Text = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                    hdfInspID.Value = ds.Tables[0].Rows[0]["InspHO"].ToString();
                    txtMobNum.Text = ds.Tables[0].Rows[0]["mill_phone"].ToString();
                    txtDOR.Text = ds.Tables[0].Rows[0]["D_O_Inspection"].ToString();

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is not available'); </script> ");
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

    public void GetReMData()
    {


        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select isnull(sum(isnull(Issued_Qty,0)),0) as Issued_Qty, isnull(sum(isnull(Issued_Bags,0)),0) as Issued_Bags  from StackRejection_DeliveryChallan where CMRAcceptance='" + txtCMRAccep.Text + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double issueQty = Convert.ToDouble(ds.Tables[0].Rows[0]["Issued_Qty"].ToString());
                    double RemQty = Convert.ToDouble(txtRejQuant.Text) - issueQty;

                    int issueQtyBags = Convert.ToInt16(ds.Tables[0].Rows[0]["Issued_Bags"].ToString());
                    int RemQtyBags = Convert.ToInt16(txtRejeBags.Text) - issueQtyBags;

                    txtRemQty.Text = Convert.ToString(RemQty);
                    txtRemBags.Text = Convert.ToString(RemQtyBags);


                   

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Rejection Number पर कोई भी डाटा उपलभ नहीं है|'); </script> ");
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
    protected void txtIssuedQty_TextChanged(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select isnull(sum(isnull(Issued_Qty,0)),0) as Issued_Qty from StackRejection_DeliveryChallan where CMRAcceptance='" + txtCMRAccep.Text + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double issueQty = Convert.ToDouble(ds.Tables[0].Rows[0]["Issued_Qty"].ToString());
                    double RemQty = Convert.ToDouble(txtRejQuant.Text) - issueQty;

                    if (Convert.ToDouble(txtIssuedQty.Text) <= Convert.ToDouble(RemQty))
                    {

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी की गयी मात्रा रिजेक्टेड मात्रा से जायदा नहीं हो सकती'); </script> ");
                        txtIssuedQty.Text = "";
                        txtIssuedQty.Focus();
                        return;
                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Rejection Number पर कोई भी डाटा उपलभ नहीं है|'); </script> ");
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
    protected void txtIssuedBags_TextChanged(object sender, EventArgs e)
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist
                select = "select isnull(sum(isnull(Issued_Bags,0)),0) as Issued_Bags from StackRejection_DeliveryChallan where CMRAcceptance='" + txtCMRAccep.Text + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int issueQtyBags = Convert.ToInt16(ds.Tables[0].Rows[0]["Issued_Bags"].ToString());
                    int RemQtyBags = Convert.ToInt16(txtRejeBags.Text) - issueQtyBags;


                    if (Convert.ToDouble(txtIssuedBags.Text) <= Convert.ToDouble(RemQtyBags))
                    {

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('जारी किये गये बग्स रिजेक्टेड बग्स से जायदा नहीं होंगे|'); </script> ");
                        txtIssuedBags.Text = "";
                        txtIssuedBags.Focus();
                        return;

                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस Rejection Number पर कोई भी डाटा उपलभ नहीं है|'); </script> ");
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
        if (ddlCommodity.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Type of CMR'); </script> ");
            return;
        }

        else if (txtIssuedDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया दिनांक चुने|'); </script> ");
            return;
        }
        else if (txtIssuedQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Qty की मात्रा इंटर करे|'); </script> ");
            return;
        }
        else if (txtIssuedBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Issued Bags की मात्रा इंटर करे|'); </script> ");
            return;
        }
        else if (txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया Truck Number इंटर करे|'); </script> ");
            return;
        }
        else if (ddlRejectionNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Rejection Number'); </script> ");
            return;
        }
        //else if (ddlbagstype.SelectedIndex <= 0)
        //{
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
        //    return;
        //}

        else if (ddlstackNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select stack Number'); </script> ");
            return;
        }
        else if (ddlBranch.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }

        else if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        else if (ddlmillname.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select miller name'); </script> ");
            return;
        }
        else if (ddlAgree.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement number'); </script> ");
            return;
        }
        else if (ddlLot.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please SelectLot Number'); </script> ");
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
                        Dist_Id = Session["dist_id"].ToString();
                        IC_Id = Session["issue_id"].ToString();
                        string opid = Session["OperatorId"].ToString();

                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string IssuedDate = ServerDate.getDate_MDY(txtIssuedDate.Text);
                        
                        ConvertServerDate RejDate = new ConvertServerDate();
                        string RejDateQ = RejDate.getDate_MDY(txtDOR.Text);

                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        string year_do = System.DateTime.Now.Date.ToString("yy");

                        string instr = "", SubDCDate = "", DCDate = "", Paddy_Challan = "", Bagstype = "", DeliveryChallan = "";



                        int month = int.Parse(DateTime.Today.Date.Month.ToString());
                        int year = int.Parse(DateTime.Today.Year.ToString());

                        con.Open();

                        string selectmax = "";

                        selectmax = "select max(Dispatch_id) as MaxDeliveryChallan,CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate from dbo.SCSC_Truck_challan where Depot_Id='" + IC_Id + "' and Dist_ID='" + Dist_Id + "'";
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string whr_ID = ds.Tables[0].Rows[0]["MaxDeliveryChallan"].ToString();
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

                        if (SubDCDate != "" && DeliveryChallan != "")
                        {
                            Paddy_Challan = "CRC" + SubDCDate;

                            //Return_CommonRice is RemCommon Dhan in DO
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +


                            "Insert Into StackRejection_DeliveryChallan(CMR_Rej_ChallanNo,CMR_RejectionNo,CropYear,CMR_RecdDist,Mill_ID,IssueCenter,Branch,Godown_id,Issued_Qty,Issued_Bags,Bags_Type,Truck_No,Issue_date,CreatedDate,IP_Address,OperatorID,Commodity_Id, CMRAcceptance, CMRDO, AgreementID, LotNumber, RejectedQuantity, RejectedBags, InspectorName, DoFInsp, StackNumber, StackName, PaddyDO, InspHo_MobNum ) Values('" + Paddy_Challan + "','" + ddlRejectionNo.SelectedValue.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "','" + Dist_Id + "','" + ddlmillname.SelectedValue.ToString() + "','" + IC_Id + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "','" + txtIssuedQty.Text + "','" + txtIssuedBags.Text + "','" + hdfbagsType.Value + "','" + txtTCNo.Text + "','" + IssuedDate + "',GETDATE(),'" + GetIp + "','" + opid + "','" + ddlCommodity.SelectedValue.ToString() + "', '" + txtCMRAccep.Text + "', '" + txtCMRDO.Text + "', '" + ddlAgree.SelectedValue.ToString() + "', '" + ddlLot.SelectedValue.ToString() + "','" + txtRejQuant.Text + "', '" + txtRejeBags.Text + "','" + hdfInspID.Value + "','" + RejDateQ + "','" + ddlstackNumber.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedItem.Text + "', '" + txtpaddyDO.Text + "', '" + txtMobNum.Text + "');";

                         
                            instr += "Insert Into SCSC_Truck_challan(Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Remarks,Dispatch_id,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,State_Id,OperatorID,NoTransaction,DispatchID,Branchid,Cropyear) Values('" + Dist_Id + "','" + IC_Id + "','" + ddlRejectionNo.SelectedValue.ToString() + "','" + IssuedDate + "','" + ddlGodown.SelectedValue.ToString() + "','" + ddlCommodity.SelectedValue.ToString() + "','0','" + txtIssuedBags.Text + "','" + txtIssuedQty.Text + "','" + Paddy_Challan + "','" + txtTCNo.Text + "','CMR To Miller','" + DeliveryChallan + "','N','" + month + "','" + year + "',GETDATE(),'','" + GetIp + "','17','23','" + opid + "','N','1','" + ddlBranch.SelectedValue.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "');";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            btnPrint.Enabled = true;

                            Label2.Visible = true;
                            Label2.Text = "Your CMR Challan Number Is : " + Paddy_Challan;
                            trNumber.Visible = true;

                            ddlCropYear.Enabled = ddlRejectionNo.Enabled = ddlBranch.Enabled = ddlGodown.Enabled = false;
                            ddlstackNumber.Enabled = false;
                            ddlmillname.Enabled = false;
                            ddlAgree.Enabled = false;
                            ddlLot.Enabled = false;
                            ddlCommodity.Enabled = false;
                            txtIssuedDate.Enabled = false;
                            txtIssuedQty.Enabled = txtIssuedBags.Enabled = txtTCNo.Enabled = false;

                            Session["RejCMR_Challan"] = Paddy_Challan.ToString();

                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your CMR Challan Number Is " + Paddy_Challan + "'); </script> ");
                            // txtInspectionDate.Text = txtStackNo.Text = "";
                          

                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                        return;
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PrintCMRInsp_Challan.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

   


   
   
    
}