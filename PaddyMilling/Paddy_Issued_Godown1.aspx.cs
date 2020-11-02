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

public partial class PaddyMilling_Paddy_Issued_Godown : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd, cmd1, cmd2;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    double QtyTotal = 0;

    public string districtid = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;

                txtDistManager.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();

                GetMillName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear,ArvaChawal,UshnaChawal FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                hdfDistanceDist.Value = "";
                //string select = string.Format("select distinct IC.DepotName,IC.DepotID from tbl_MetaData_DEPOT As IC Left Join Distance_Master_Godown As Dist ON(Dist.IssueCenter=IC.DepotID and '23'+Dist.DistrictId=IC.DistrictId) where Dist.DistrictId='" + districtid + "' and Dist.PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "' and Dist.Distance_For='11'");

                select = "Select distinct IssueCenter As DepotID  From Distance_Master_Godown where DistrictId='" + districtid + "' and PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "' and Distance_For='11'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfDistanceDist.Value = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfDistanceDist.Value += ((hdfDistanceDist.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["DepotID"].ToString() + "'";
                    }

                    GetWareHouseIC();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने Paddy Mill से Godown के Distance की Mapping नहीं की है,इसलिए कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    public void GetWareHouseIC()
    {
        districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                //string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' and StateId='23' and BranchId IN(" + hdfDistanceDist.Value + ") order by DepotName";
                string select = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + districtid + "' and StateId='23' and BranchId IN(" + hdfDistanceDist.Value + ") order by DepotName";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlIssueCentre.DataSource = ds.Tables[0];
                        ddlIssueCentre.DataTextField = "DepotName";
                        ddlIssueCentre.DataValueField = "BranchId";
                        ddlIssueCentre.DataBind();
                        ddlIssueCentre.Items.Insert(0, "--Select--");
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

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                {
                    select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement As PM Left Join Miller_Registration MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' and PM.User_Agent!='DDMO' order by MillName Asc";
                }
                else if (Session["DistrictManager"].ToString() == "DDMO")
                {
                    select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement As PM Left Join Miller_Registration MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' and PM.User_Agent='DDMO' order by MillName Asc";
                }

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtQty.Text = txtGodownRemDhan.Text = "";
        txtQty.Enabled = false;

        trGrid2.Visible = false;

        ddlIssueCentre.Items.Clear();
        ddlGodown.Items.Clear();
        ddlAgtmtNumber.Items.Clear();

        GridView2.DataSource = "";
        GridView2.DataBind();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    //select = "Select Agreement_ID From PaddyMilling_Agreement where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

                    if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                    {
                        select = "Select Agreement_ID From PaddyMilling_Agreement where Agreement_ID Not IN (Select distinct Agreement_ID From PaddyMapping_Godown where District='" + DistCode + "' and CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "') and District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and User_Agent!='DDMO'";
                    }
                    else if (Session["DistrictManager"].ToString() == "DDMO")
                    {
                        select = "Select Agreement_ID From PaddyMilling_Agreement where Agreement_ID Not IN (Select distinct Agreement_ID From PaddyMapping_Godown where District='" + DistCode + "' and CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "') and District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and User_Agent='DDMO'";
                    }
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");

                        GetMPIssueCentre();

                        GetGridDataForDistance();

                        trGrid2.Visible = true;
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अनुबंध नंबर उपलब्ध नहीं है|'); </script> ");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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

    public void GetGridDataForDistance()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Dist.IssueCenter As ICName,Dist.IssueCenter,Dist.Godown_id,Dist.distance From Distance_Master_Godown As Dist  where Dist.DistrictId='" + districtid + "' and Dist.PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "' and Dist.Distance_For='11' order by  CONVERT(float, Replace(Dist.distance, 'Group','')) Asc";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();

                    GetGodownBalanceForGrid();
                    GetGodownMappedForGrid();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल से गोदाम की दुरी की Mapping करे|'); </script> ");
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

    public void GetGodownMappedForGrid()
    {
        string IC_Id = "", strGodownVal = "";
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    IC_Id = GridView2.Rows[i].Cells[7].Text;
                    strGodownVal = GridView2.Rows[i].Cells[8].Text;

                    string select = "";

                    select = "Select Sum(Alloted_CommonPaddy) As Alloted_CommonPaddy From PaddyMapping_Godown where District='" + DistCode + "' and CropYear='" + txtYear.Text + "' and IssueCenter='" + IC_Id + "' and Godown_id='" + strGodownVal + "'";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string Alloted_CommonPaddy = "";
                        Alloted_CommonPaddy = ds.Tables[0].Rows[0]["Alloted_CommonPaddy"].ToString();
                        GridView2.Rows[i].Cells[5].Text = Alloted_CommonPaddy;

                        if (Alloted_CommonPaddy != "")
                        {
                            decimal OriginalPaddy = 0, PaddyMapped = 0;

                            OriginalPaddy = decimal.Parse(GridView2.Rows[i].Cells[4].Text);
                            PaddyMapped = decimal.Parse(GridView2.Rows[i].Cells[5].Text);

                            GridView2.Rows[i].Cells[6].Text = (OriginalPaddy - PaddyMapped).ToString();
                        }
                        else
                        {
                            GridView2.Rows[i].Cells[6].Text = GridView2.Rows[i].Cells[4].Text;
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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
            {
                e.Row.Style.Add("height", "89px");
            }

            string GodownName = "",ICName= "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            GodownName = e.Row.Cells[2].Text;
            ICName = e.Row.Cells[1].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchID='" + ICName + "') As BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') As Godown_Name");
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                            e.Row.Cells[1].Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
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

        }
    }

    public void GetGodownBalanceForGrid()
    {
        string IC_Id = "", strGodownVal = "";

        string pqry = "space_forcommodity_ingodown";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                SqlCommand cmdpqty = new SqlCommand(pqry, con);
                cmdpqty.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    IC_Id = GridView2.Rows[i].Cells[7].Text;
                    strGodownVal = GridView2.Rows[i].Cells[8].Text;

                    cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = Session["dist_id"].ToString();
                    cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = IC_Id;
                    cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
                    cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = "13";  //Paddy-Common
                    cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = "01/01/2015";

                    DataSet ds = new DataSet();
                    SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);
                    dr.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

                        GridView2.Rows[i].Cells[4].Text = Convert.ToString(stock);
                    }
                    cmdpqty.Parameters.Clear();
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

    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRemCommonDhan.Text = txtRemGradeADhan.Text = txtTotalCDhan.Text = txtTotalGDhan.Text = txtQty.Text = txtGodownRemDhan.Text = "";
        txtQty.Enabled = false;
        ddlGodown.Items.Clear();

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("Select Common_Dhan,GradeA_Dhan From PaddyMilling_Agreement where District='" + districtid + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtTotalCDhan.Text = txtRemCommonDhan.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();
                    txtTotalGDhan.Text = txtRemGradeADhan.Text = ds.Tables[0].Rows[0]["GradeA_Dhan"].ToString();

                    btnAdd.Enabled = true;
                    ddlIssueCentre.SelectedIndex = 0;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        double TotalQty = double.Parse(txtRemCommonDhan.Text);
        double AllotedQty = double.Parse(txtQty.Text);
        double GodownRemQty = double.Parse(txtGodownRemDhan.Text);

        if (ddlAgtmtNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया अनुबंध नंबर चुने|'); </script> ");
            return;
        }
        if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया गोदाम चुने|'); </script> ");
            return;
        }
        else if (txtQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया Quantity इंटर करे|'); </script> ");
            return;
        }
        else if (TotalQty < AllotedQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('अनुबंध के अनुसार बची हुई मात्रा के आधार पर ही धान का आबंटन करे|'); </script> ");
            return;
        }
        //else if (GodownRemQty < AllotedQty)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('गोदाम में बची हुई मात्रा के आधार पर ही धान का आबंटन करे|'); </script> ");
        //    return;
        //}
        else
        {
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("ICName");
                dt.Columns.Add("ICVal");
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
                dr["GodownName"] = ddlGodown.SelectedItem.Text;
                dr["GodownVal"] = ddlGodown.SelectedValue;
                dr["Quantity"] = dr["QuantityQtls"] = txtQty.Text;

                dr["quantity"] = (AllotedQty).ToString("0.00");
                dt.Rows.Add(dr);
                Session["ICGBQ"] = dt;
                ddlMillName.Enabled = ddlAgtmtNumber.Enabled = false;
                fillgrid();

                txtRemCommonDhan.Text = txtQty.Text = (TotalQty - AllotedQty).ToString();
                txtGodownRemDhan.Text = (GodownRemQty - AllotedQty).ToString();

                if (txtQty.Text == "0" && txtRemCommonDhan.Text == "0")
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
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[3].Text));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total Qty = " + QtyTotal.ToString("0.00");
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (ddlGodown.SelectedValue.ToString() != dt.Rows[e.RowIndex]["GodownVal"].ToString() || ddlIssueCentre.SelectedValue.ToString() != dt.Rows[e.RowIndex]["ICVal"].ToString())
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया आप वही गोदाम का नाम चुने, जिस गोदाम से आप Quantity को Remove करना चाहते हो|'); </script> ");
            return;
        }
        else
        {
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("ICName");
                dt.Columns.Add("ICVal");
                dt.Columns.Add("GodownName");
                dt.Columns.Add("GodownVal");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("QuantityQtls");
            }
            else
            {
                string QtyRowValue = dt.Rows[e.RowIndex]["Quantity"].ToString();

                double TotalQty = double.Parse(txtRemCommonDhan.Text);
                double AllotedQty = double.Parse(QtyRowValue);
                double GodownRemQty = double.Parse(txtGodownRemDhan.Text);

                txtRemCommonDhan.Text = txtQty.Text = (TotalQty + AllotedQty).ToString();
                txtGodownRemDhan.Text = (GodownRemQty + AllotedQty).ToString();

                dt.Rows.RemoveAt(e.RowIndex);

                if (txtQty.Text == "0" || txtRemCommonDhan.Text == "0")
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
    }


    protected void ddlIssueCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        txtGodownRemDhan.Text = txtQty.Text = "";
        txtQty.Enabled = false;

        if (ddlIssueCentre.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्रदाय केंद्र चुने|'); </script> ");
        }
    }

    public void GetGodown()
    {
        districtid = Session["dist_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select distinct Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId='23" + districtid + "' and BranchId='" + ddlIssueCentre.SelectedValue.ToString() + "'");
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

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string checkpage = ds.Tables[0].Rows[i]["Godown_ID"].ToString();

                            if (ddlGodown.Items.FindByValue(checkpage) != null)
                            {
                                ddlGodown.Items.FindByValue(checkpage).Enabled = false;
                            }
                        }
                        GetDistanceGodown();
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

    public void GetDistanceGodown()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Godown_id From Distance_Master_Godown Where DistrictId='" + districtid + "' and PCCodeOrRailheadcode='" + ddlMillName.SelectedValue.ToString() + "' and IssueCenter='" + ddlIssueCentre.SelectedValue.ToString() + "' and Distance_For='11' order by  CONVERT(float, Replace(distance, 'Group','')) Asc";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string checkpage = ds.Tables[0].Rows[i]["Godown_id"].ToString();

                        if (ddlGodown.Items.FindByValue(checkpage) != null)
                        {
                            ddlGodown.Items.FindByValue(checkpage).Enabled = true;
                        }
                    }
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


    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        districtid = Session["dist_id"].ToString();

        double TotalQty = double.Parse(txtRemCommonDhan.Text);
        double AllotedQty = double.Parse(txtQty.Text);

        if (txtYear.Text != "" || txtDistManager.Text != "")
        {
            if (TotalQty == 0 && AllotedQty == 0)
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                districtid = Session["dist_id"].ToString();
                string AgrmtNumber = ddlAgtmtNumber.SelectedValue.ToString();

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string instr = "";

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            DataTable dt = adddetails();
                            if (dt != null)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string Mapping_No = AgrmtNumber + (i + 1);
                                    instr += "Insert into PaddyMapping_Godown(District,CropYear,Mill_ID,Agreement_ID,Mapping_No,IssueCenter,Godown_id,Alloted_CommonPaddy,Alloted_GradePaddy,Rem_CommonPaddy,Rem_GradePaddy,CreatedDate,IP_Address) values('" + districtid + "','" + txtYear.Text + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedValue.ToString() + "','" + Mapping_No + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["GodownVal"] + "','" + dt.Rows[i]["QuantityQtls"] + "','0','" + dt.Rows[i]["QuantityQtls"] + "','0',GETDATE(),'" + GetIp + "');";
                                }
                            }

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = btnAdd.Enabled = false;
                                Session["ICGBQ"] = null;

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
                                txtYear.Text = txtDistManager.Text = "";
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGodownRemDhan.Text = txtQty.Text = "";
        txtQty.Enabled = false;

        if (ddlGodown.SelectedIndex > 0)
        {
            GetGodownBalance();
            txtQty.Enabled = true;
            txtQty.Text = txtRemCommonDhan.Text;
            GetRemGodownBalance();
            txtQty.Focus();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया गोदाम चुने|'); </script> ");
            return;
        }
    }

    public void GetRemGodownBalance()
    {
        double GodownRemQty = double.Parse(txtGodownRemDhan.Text);

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string ddlGodownName = ddlGodown.SelectedItem.ToString();
            string GridGodownName = GridView1.Rows[i].Cells[2].Text;

            string ddlICName = ddlIssueCentre.SelectedItem.ToString();
            string GridICName = GridView1.Rows[i].Cells[1].Text;

            if (ddlGodownName == GridGodownName && ddlICName == GridICName)
            {
                txtGodownRemDhan.Text = (GodownRemQty - (double.Parse(GridView1.Rows[i].Cells[3].Text))).ToString();
                break;
            }
        }
    }

    public void GetGodownBalance()
    {
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            string ddlGodownName = ddlGodown.SelectedValue.ToString();
            string GridGodownName = GridView2.Rows[i].Cells[8].Text;

            string ddlICName = ddlIssueCentre.SelectedValue.ToString();
            string GridICName = GridView2.Rows[i].Cells[7].Text;

            if (ddlGodownName == GridGodownName && ddlICName == GridICName)
            {
                txtGodownRemDhan.Text = GridView2.Rows[i].Cells[6].Text;
                break;
            }
        }
    }

    //public void GetGodownBalance()
    //{

    //}

    //public void GetGodownBalance()
    //{
    //    string IC_Id = "", strGodownVal = "", strCommodity = "";

    //    districtid = Session["dist_id"].ToString();
    //    IC_Id = ddlIssueCentre.SelectedValue.ToString();

    //    strGodownVal = ddlGodown.SelectedValue.ToString();
    //    Int64 comid = Convert.ToInt64(strGodownVal);

    //    strCommodity = "13"; //Paddy-Common

    //    //DateTime dodate = Convert.ToDateTime(DateTime.ParseExact(txtIssuedDate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));

    //    string openingdate = "01/01/2015";

    //    //if (dodate >= Convert.ToDateTime(DateTime.ParseExact("01/01/2015", "dd/MM/yyyy", null).ToString("MM/dd/yyyy")))
    //    //{
    //    //    openingdate = "01/01/2015";
    //    //}
    //    //else
    //    //{
    //    //    openingdate = "01/04/2014";
    //    //}

    //    string pqry = "space_forcommodity_ingodown";

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();

    //            SqlCommand cmdpqty = new SqlCommand(pqry, con);
    //            cmdpqty.CommandType = CommandType.StoredProcedure;

    //            cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = districtid;
    //            cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = IC_Id;
    //            cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = strGodownVal;
    //            cmdpqty.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = strCommodity;
    //            //cmdpqty.Parameters.Add("@source", SqlDbType.NVarChar).Value = ddlsarrival.SelectedValue.ToString();
    //            cmdpqty.Parameters.Add("@Openingdate", SqlDbType.NVarChar).Value = openingdate;

    //            DataSet ds = new DataSet();
    //            SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

    //            dr.Fill(ds);

    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

    //                //double bags = Convert.ToDouble(ds.Tables[0].Rows[0]["Totalbags"].ToString());

    //                txtGodownRemDhan.Text = Convert.ToString(stock);
    //                //txtBalBagInGodown.Text = Convert.ToString(bags);

    //                txtQty.Enabled = true;
    //                //txtcurntcap.Text = Convert.ToString(stock);
    //                //txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

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
}