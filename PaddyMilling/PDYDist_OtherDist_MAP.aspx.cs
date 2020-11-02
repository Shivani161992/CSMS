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
using System.Drawing;

public partial class PaddyMilling_PDYDist_OtherDist_MAP : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                hdfYear.Value = "";
                Session["ICGBQ"] = null;
                GridView1.DataSource = "";
                GridView1.DataBind();

                GetCropYearValues();
                GetDistName();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (Session["st_id"].ToString() == "10")
            {
                this.MasterPageFile = "~/MasterPage/Markfed_PDY.master";
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
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfYear.Value = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                //if (Session["st_id"].ToString() == "10")
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
                //}
                //else
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed IS NULL Order By district_name";
                //}

                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist.DataSource = ds.Tables[0];
                        ddlFrmDist.DataTextField = "district_name";
                        ddlFrmDist.DataValueField = "district_code";
                        ddlFrmDist.DataBind();
                        ddlFrmDist.Items.Insert(0, "--Select--");
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

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlToDist.Items.Clear();
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        btnSubmit.Enabled = false;

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetToDistName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select From District'); </script> ");
            return;
        }
    }

    public void GetToDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlToDist.DataSource = ds.Tables[0];
                        ddlToDist.DataTextField = "district_name";
                        ddlToDist.DataValueField = "district_code";
                        ddlToDist.DataBind();
                        ddlToDist.Items.Insert(0, "--Select--");
                        ddlToDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;
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


    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        if (Session["st_id"].ToString() == "10")
        {
            Response.Redirect("~/State/PaddyMillingHome_MFD.aspx");
        }
        else
        {
            Response.Redirect("~/State/PaddyMillingHome.aspx");
        }
    }

    protected void ddlToDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIssueCentre.Items.Clear();
        ddlBranch.Items.Clear();
        ddlGodown.Items.Clear();
        btnAdd.Enabled = false;

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlFrmDist.SelectedValue.ToString() == ddlToDist.SelectedValue.ToString())
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Same District Is Not Allow'); </script> ");
            return;
        }
        else if (ddlToDist.SelectedIndex > 0)
        {
            GetMappingData();
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select To District'); </script> ");
            return;
        }
    }

    public void GetMappingData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Depot.DepotName,PM.IssueCenter,PM.Godown_id,PM.Mapping_No From PM_DistToGodown As PM LEft Join tbl_MetaData_DEPOT As Depot ON(Depot.DistrictId='23'+PM.ToDistrict and Depot.DepotID = PM.IssueCenter) Where PM.FrmDistrict='" + ddlFrmDist.SelectedValue.ToString() + "' and PM.ToDistrict='" + ddlToDist.SelectedValue.ToString() + "' and PM.CropYear='" + hdfYear.Value + "' Order By Depot.DepotName";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string GodownName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            GodownName = e.Row.Cells[2].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = "";
                    select = "select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "' ";
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
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

        }
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddlToDist.SelectedValue.ToString() + "' order by DepotName";
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
        ddlGodown.Items.Clear();
        ddlBranch.Items.Clear();
        btnAdd.Enabled = false;

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
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ddlIssueCentre.SelectedValue.ToString() + "'");
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
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + ddlToDist.SelectedValue.ToString() + "' order by DepotName");
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
        btnAdd.Enabled = false;

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
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Enabled = false;
        if (ddlGodown.SelectedIndex > 0)
        {
            btnAdd.Enabled = true;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
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
                int checkDupGodown = 0;

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    string GodownCode = GridView2.Rows[i].Cells[3].Text;
                    if (GodownCode == ddlGodown.SelectedValue.ToString())
                    {
                        checkDupGodown = 1;
                        break;
                    }
                }

                if (checkDupGodown == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ICName"] = ddlIssueCentre.SelectedItem.Text;
                    dr["ICVal"] = ddlIssueCentre.SelectedValue.ToString();
                    dr["BranchName"] = ddlBranch.SelectedItem.Text;
                    dr["BranchVal"] = ddlBranch.SelectedValue.ToString();
                    dr["GodownName"] = ddlGodown.SelectedItem.Text;
                    dr["GodownVal"] = ddlGodown.SelectedValue.ToString();

                    dt.Rows.Add(dr);
                    Session["ICGBQ"] = dt;
                    ddlFrmDist.Enabled = ddlToDist.Enabled = false;
                    fillgrid();

                    btnSubmit.Enabled = true;
                    ddlGodown.Focus();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप उस गोदाम का चुनाव नहीं कर सकते, जिस गोदाम की आपने पहले से Mapping की हैं|'); </script> ");
                    return;
                }
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
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
        }
        else
        {
            dt.Rows.RemoveAt(e.RowIndex);
            if (e.RowIndex <= 0)
            {
                btnSubmit.Enabled = false;
            }
        }

        Session["ICGBQ"] = dt;
        fillgrid();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = adddetails();
        if (dt.Rows.Count <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Add At least One Godown'); </script> ");
            return;
        }
        else if (ddlGodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select At least One Godown'); </script> ");
            return;
        }
        else
        {
            if (hdfYear.Value != "")
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string ChechMax = "";
                            decimal MaxMappingNo = 0;
                            ChechMax = "Select Max(Mapping_No) As MaxMap From PM_DistToGodown Where FrmDistrict='" + ddlFrmDist.SelectedValue.ToString() + "' and ToDistrict='" + ddlToDist.SelectedValue.ToString() + "' ";
                            da = new SqlDataAdapter(ChechMax, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                string Data = "";
                                Data = ds.Tables[0].Rows[0]["MaxMap"].ToString();
                                if (Data == "")
                                {
                                    MaxMappingNo = decimal.Parse(ddlFrmDist.SelectedValue.ToString() + ddlToDist.SelectedValue.ToString() + "100000");
                                }
                                else
                                {
                                    string wid = Data.Substring(Data.Length - 6);
                                    Int64 wid_ID_new = Convert.ToInt64(wid);
                                    wid_ID_new = wid_ID_new + 1;
                                    string combine = wid_ID_new.ToString();
                                    MaxMappingNo = decimal.Parse(ddlFrmDist.SelectedValue.ToString() + ddlToDist.SelectedValue.ToString() + combine);
                                }
                            }

                            if (MaxMappingNo != 0)
                            {
                                string instr = "";

                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                                if (dt != null)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (i == 0)
                                        {

                                        }
                                        else
                                        {
                                            MaxMappingNo = MaxMappingNo + 1;
                                        }

                                        instr += "Insert Into PM_DistToGodown(FrmDistrict,ToDistrict,Mapping_No,CropYear,IssueCenter,Branch,Godown_id,CreatedDate,IP_Address) Values('" + ddlFrmDist.SelectedValue.ToString() + "','" + ddlToDist.SelectedValue.ToString() + "','" + MaxMappingNo + "','" + hdfYear.Value + "','" + dt.Rows[i]["ICVal"] + "','" + dt.Rows[i]["BranchVal"] + "','" + dt.Rows[i]["GodownVal"] + "',GETDATE(),'" + GetIp + "');";
                                    }
                                }

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                                cmd = new SqlCommand(instr, con);
                                int count = cmd.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    btnSubmit.Enabled = btnAdd.Enabled = false;
                                    ddlFrmDist.Enabled = ddlToDist.Enabled = ddlIssueCentre.Enabled = ddlBranch.Enabled = ddlGodown.Enabled = false;
                                    hdfYear.Value = "";
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
                                    Session["ICGBQ"] = null;
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                                }
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Changes Not Allow'); </script> ");
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
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Click On New Button'); </script> ");
                return;
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }
}