using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class IssueCenter_Godown_Add : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con_csms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            txtGodownName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
            txtCapacity.Attributes.Add("onkeypress", "return IsNumericWithoutDecimal(event,this);");
            txtScientificCapacity.Attributes.Add("onkeypress", "return IsNumericWithoutDecimal(event,this);");
            //ComObj = new Common(ConfigurationManager.AppSettings["connect_warehouse"].ToString());


            if (Session["hindi"].ToString() == "H")
            {


                btnaddnew.Text = Resources.hindi.btnaddnew;
                //btninsert.Text =Resources.hindi.btninsert;
                Label3.Text = Resources.hindi.godawonname;
               // lblGodownMaster.Text = Resources.hindi.lblGodownMaster;
                Label4.Text = Resources.hindi.godawoncapcity;
                Label5.Text = Resources.hindi.godawontype;
                Label6.Text = Resources.hindi.storagetype;
                //btncancel.Text = Resources.hindi.btncancel;

            }
            if (!IsPostBack)
            {
                if (Session["Depot_DepotID"].ToString() != null)
                {
                    string Depotid = Session["Depot_DepotID"].ToString();
                    GetGodown(Depotid);
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();

        }
    }
    private void GetGodown(string depotId)
    {
        try
        {
            string query = "select * from dbo.tbl_MetaData_GODOWN where DepotId='" + depotId + "' order by tbl_MetaData_GODOWN.Godown_Name ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsGodown"] = ds;
                fillGrid(ds);
                Label2.Visible = false;
            }
            else
            {
                godown_GridView.DataSource = null;
                godown_GridView.DataBind();
                Label2.Visible = true;
                Label2.Text = "There is No Godown Entry in this Depot";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();

        }

    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        PanelGodown.Visible = true;
        btnUpdate.Text = "Insert";
        txtGodownName.ReadOnly = false;

    }
    protected void godown_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblSerial = (Label)e.Row.FindControl("Label1");
        //    int i = e.Row.RowIndex + 1;
        //    lblSerial.Text = i.ToString();            
        //}
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = new LinkButton();
                lb = (LinkButton)e.Row.Cells[0].Controls[0];
                lb.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this row?');");
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
    }
    protected void godown_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate.Text = "Update";
        PanelGodown.Visible = true;


        string gid = godown_GridView.SelectedRow.Cells[7].Text;
        if (gid != "")
        {
            txtGodownName.Text = godown_GridView.SelectedRow.Cells[2].Text.Trim();
            txtGodownName.ReadOnly = true;
            ViewState["GodownName"] = godown_GridView.SelectedRow.Cells[2].Text.Trim();
            txtCapacity.Text = godown_GridView.SelectedRow.Cells[3].Text.Trim();

            if (godown_GridView.SelectedRow.Cells[4].Text.Trim() == "" || godown_GridView.SelectedRow.Cells[4].Text.Trim() == "&nbsp;")
            {

                txtScientificCapacity.Text = "";


            }

            else
            {

                txtScientificCapacity.Text = godown_GridView.SelectedRow.Cells[4].Text.Trim();

            }
            string hired = godown_GridView.SelectedRow.Cells[5].Text.Trim();
            ddllst_hired.SelectedItem.Selected = false;
            foreach (ListItem lst1 in ddllst_hired.Items)
            {
                if (lst1.Value == hired)
                {
                    lst1.Selected = true;
                }
            }
            string storage = godown_GridView.SelectedRow.Cells[6].Text.Trim();
            ddllst_storage.SelectedItem.Selected = false;
            foreach (ListItem lst1 in ddllst_storage.Items)
            {
                if (lst1.Value == storage)
                {
                    lst1.Selected = true;
                }
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string godown = txtGodownName.Text.Trim();
            float capacity = CheckFloat(txtCapacity.Text.Trim());
            float scapacity = CheckFloat(txtScientificCapacity.Text.Trim());
            string hired = ddllst_hired.SelectedValue;
            string storage = ddllst_storage.SelectedValue;
            string distid = "23" + Session["dist_id"].ToString();
            // string distid = Session["Depot_DistID"].ToString();
            string depotId = Session["Depot_DepotID"].ToString();
            try
            {
                if (con != null)
                {
                    con.Open();
                    con_csms.Open();
                    if (btnUpdate.Text == "Insert")
                    {
                        #region Insert Godown Master
                        SqlCommand cmd_go = new SqlCommand("sp_insertGodownMaster", con);
                        cmd_go.CommandType = CommandType.StoredProcedure;
                        cmd_go.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                        cmd_go.Parameters["@DepotId"].Value = depotId;

                        cmd_go.Parameters.Add("@DistrictId", SqlDbType.NVarChar, 20);
                        cmd_go.Parameters["@DistrictId"].Value = distid;

                        cmd_go.Parameters.Add("@GodownName", SqlDbType.NVarChar, 100);
                        cmd_go.Parameters["@GodownName"].Value = godown;

                        cmd_go.Parameters.Add("@Capacity", SqlDbType.Decimal);
                        cmd_go.Parameters["@Capacity"].Value = capacity;

                        cmd_go.Parameters.Add("@SCapacity", SqlDbType.Decimal);
                        cmd_go.Parameters["@SCapacity"].Value = scapacity;

                        cmd_go.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 20);
                        cmd_go.Parameters["@CreatedBy"].Value = ip;

                        cmd_go.Parameters.Add("@Hired_Type", SqlDbType.NChar, 50);
                        cmd_go.Parameters["@Hired_Type"].Value = hired;

                        cmd_go.Parameters.Add("@Storage_Type", SqlDbType.NChar, 50);
                        cmd_go.Parameters["@Storage_Type"].Value = storage;

                        int _sts = cmd_go.ExecuteNonQuery();

                        #region Insert Godown Master csms database

                        string distid_2_digit = Session["dist_id"].ToString();

                        SqlCommand cmd_go_csms = new SqlCommand("sp_insertGodownMaster_csms", con_csms);
                        cmd_go_csms.CommandType = CommandType.StoredProcedure;
                        cmd_go_csms.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                        cmd_go_csms.Parameters["@DepotId"].Value = depotId;

                        cmd_go_csms.Parameters.Add("@DistrictId", SqlDbType.NVarChar, 20);
                        cmd_go_csms.Parameters["@DistrictId"].Value = distid_2_digit;

                        cmd_go_csms.Parameters.Add("@GodownName", SqlDbType.NVarChar, 100);
                        cmd_go_csms.Parameters["@GodownName"].Value = godown;

                        cmd_go_csms.Parameters.Add("@Capacity", SqlDbType.Decimal);
                        cmd_go_csms.Parameters["@Capacity"].Value = capacity;

                        cmd_go_csms.Parameters.Add("@SCapacity", SqlDbType.Decimal);
                        cmd_go_csms.Parameters["@SCapacity"].Value = scapacity;

                        cmd_go_csms.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 20);
                        cmd_go_csms.Parameters["@CreatedBy"].Value = ip;

                        cmd_go_csms.Parameters.Add("@Hired_Type", SqlDbType.NChar, 50);
                        cmd_go_csms.Parameters["@Hired_Type"].Value = hired;

                        cmd_go_csms.Parameters.Add("@Storage_Type", SqlDbType.NChar, 50);
                        cmd_go_csms.Parameters["@Storage_Type"].Value = storage;

                        int _sts_csms = cmd_go_csms.ExecuteNonQuery();

                        #endregion

                        if (_sts == 1 && _sts_csms == 1)
                        {
                            GetGodown(depotId);
                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "Record saved Successfully" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        }
                        else if (_sts == -1)
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "This godown name already exist!" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        }
                        else
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "Some error has occurred" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());

                        }
                        #endregion

                    }
                    else if (btnUpdate.Text == "Update")
                    {
                        string godownid = godown_GridView.SelectedRow.Cells[7].Text.Trim();

                        int status = 0;
                        if (ViewState["GodownName"].ToString() == txtGodownName.Text.Trim())
                        {
                            status = 0;
                        }
                        else
                        {
                            status = 1;
                        }

                        #region Update Godown Master
                        SqlCommand cmd_go = new SqlCommand("sp_godownupdate", con);
                        cmd_go.CommandType = CommandType.StoredProcedure;
                        cmd_go.Parameters.Add("@Godown_Name", SqlDbType.NVarChar, 100);
                        cmd_go.Parameters["@Godown_Name"].Value = godown;

                        cmd_go.Parameters.Add("@Godown_Id", SqlDbType.NVarChar, 20);
                        cmd_go.Parameters["@Godown_Id"].Value = godownid;

                        cmd_go.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                        cmd_go.Parameters["@DepotId"].Value = depotId;

                        cmd_go.Parameters.Add("@Godown_Capacity", SqlDbType.Decimal);
                        cmd_go.Parameters["@Godown_Capacity"].Value = capacity;

                        cmd_go.Parameters.Add("@SCapacity", SqlDbType.Decimal);
                        cmd_go.Parameters["@SCapacity"].Value = scapacity;

                        cmd_go.Parameters.Add("@status", SqlDbType.Int);
                        cmd_go.Parameters["@status"].Value = status;

                        cmd_go.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
                        cmd_go.Parameters["@UpdatedBy"].Value = ip;

                        cmd_go.Parameters.Add("@Hired_Type", SqlDbType.NChar, 50);
                        cmd_go.Parameters["@Hired_Type"].Value = hired;

                        cmd_go.Parameters.Add("@Storage_Type", SqlDbType.NChar, 50);
                        cmd_go.Parameters["@Storage_Type"].Value = storage;

                        int _sts = cmd_go.ExecuteNonQuery();

                        #region Update Godown Master in csms database

                        SqlCommand cmd_go_csms = new SqlCommand("sp_Godownupdate_csms", con_csms);
                        cmd_go_csms.CommandType = CommandType.StoredProcedure;
                        cmd_go_csms.Parameters.Add("@Godown_Name", SqlDbType.NVarChar, 100);
                        cmd_go_csms.Parameters["@Godown_Name"].Value = godown;

                        cmd_go_csms.Parameters.Add("@Godown_Id", SqlDbType.NVarChar, 20);
                        cmd_go_csms.Parameters["@Godown_Id"].Value = godownid;

                        cmd_go_csms.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                        cmd_go_csms.Parameters["@DepotId"].Value = depotId;

                        cmd_go_csms.Parameters.Add("@Godown_Capacity", SqlDbType.Decimal);
                        cmd_go_csms.Parameters["@Godown_Capacity"].Value = capacity;

                        cmd_go_csms.Parameters.Add("@SCapacity", SqlDbType.Decimal);
                        cmd_go_csms.Parameters["@SCapacity"].Value = scapacity;

                        cmd_go_csms.Parameters.Add("@status", SqlDbType.Int);
                        cmd_go_csms.Parameters["@status"].Value = status;

                        cmd_go_csms.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
                        cmd_go_csms.Parameters["@UpdatedBy"].Value = ip;

                        cmd_go_csms.Parameters.Add("@Hired_Type", SqlDbType.NChar, 50);
                        cmd_go_csms.Parameters["@Hired_Type"].Value = hired;

                        cmd_go_csms.Parameters.Add("@Storage_Type", SqlDbType.NChar, 50);
                        cmd_go_csms.Parameters["@Storage_Type"].Value = storage;

                        int _sts_update = cmd_go_csms.ExecuteNonQuery();

                        #endregion
                        if (_sts == 1 && _sts_update == 1)
                        {
                            GetGodown(depotId);
                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "Record updated successfully" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        }
                        else  if (_sts == 1 && _sts_update == -1)
                        {
                            GetGodown(depotId);
                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "Record updated successfully" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        }
                        else
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "Some error has occurred" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());

                        }
                        #endregion

                    }
                    txtGodownName.Text = "";
                    txtCapacity.Text = "";
                    ddllst_hired.SelectedItem.Selected = false;
                    ddllst_storage.SelectedItem.Selected = false;
                    PanelGodown.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    private void fillGrid(DataSet ds)
    {
        godown_GridView.DataSource = ds.Tables[0];
        godown_GridView.DataBind();
    }
    protected void godown_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dsGodown"];
        godown_GridView.PageIndex = e.NewPageIndex;
        fillGrid(ds);
    }
    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int64 CheckInt(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int64 ValF = Int64.Parse(ValS);
        return ValF;

    }
    protected void godown_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            string depotId = Session["Depot_DepotID"].ToString();
            int _rowindex = e.RowIndex;
            string gid = godown_GridView.DataKeys[_rowindex].Value.ToString();

      
            string stcCnt = "Select count(Stack_ID) as count from tbl_MetaData_STACK where Godown_ID='" + gid + "'";
            SqlCommand cmd = new SqlCommand(stcCnt, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"]) > 0)
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Delete Stack of this GODOWN first.'); </script> ");

            }
            else
            {

                string query = "delete from dbo.tbl_MetaData_GODOWN where Godown_ID='" + gid + "'";
                if (con != null)
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data deleted Successfully......'); </script> ");
                    GetGodown(depotId);
                    btnUpdate.Text = "";
                    PanelGodown.Visible = false;

                    if (con_csms != null)
                    {
                        cmd = new SqlCommand();
                        con_csms.Open();
                        cmd.Connection = con_csms;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnCan_Click(object sender, EventArgs e)
    {
        txtCapacity.Text = "";
        txtGodownName.Text = "";
        ddllst_hired.SelectedItem.Selected = false;
        ddllst_storage.SelectedItem.Selected = false;
        PanelGodown.Visible = false;

    }

}
