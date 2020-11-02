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
using System.Data.SqlClient;
using System.Text;

public partial class State_Delete_SugarSupplyOrder : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {

           ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");
        }
        if (!IsPostBack)
        {

            GetSupplier();
            //GetOrderno(); 
            fillgrid();
        }
        

    }

    private void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select distinct Orderno,CONVERT(nvarchar, tbl_SupplyOrder_Master.Dispatch_Date, 103) AS Dispatch_Date,distirctsmp_Sugar.district_name,tbl_MetaData_DEPOT.DepotName,ZoneMaster.Zone,Qty from tbl_SupplyOrder_Master inner join distirctsmp_Sugar on tbl_SupplyOrder_Master.district_code = distirctsmp_Sugar.district_code inner join ZoneMaster on tbl_SupplyOrder_Master.Zonecode = ZoneMaster.Zonecode inner join tbl_MetaData_DEPOT on tbl_SupplyOrder_Master.Depot_ID = tbl_MetaData_DEPOT.DepotID where Orderno='" + ddlorderno.SelectedValue + "'";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null)
        {

        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    private void GetOrderno()
    {
        SqlCommand cmd = new SqlCommand("select distinct Orderno from tbl_SupplyOrder_Master where S_name='" + ddlsuppliername.SelectedItem.Text.ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlorderno.DataSource = ds;
        ddlorderno.DataValueField = "Orderno";
        ddlorderno.DataBind();
        ddlorderno.Items.Insert(0, new ListItem("--Select--", "0"));
    }

   

    private void GetSupplier()
    {
        SqlCommand cmd = new SqlCommand("select Distinct S_name  from tbl_SupplyOrder_Master", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlsuppliername.DataSource = ds;
        ddlsuppliername.DataTextField = "S_name";
        ddlsuppliername.DataBind();
        ddlsuppliername.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlsuppliername_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOrderno();
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        int count = 0;
        string Orderno = ddlorderno.SelectedValue;
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = null;
            chk = (CheckBox)row.FindControl("chkDelete");
            if (chk.Checked == true)
            {


                string mOrderno = row.Cells[1].Text;
                string morderdate = row.Cells[2].Text;
                string mzone = row.Cells[3].Text;
                string mdistid = row.Cells[4].Text;
                string mdeoptid = row.Cells[5].Text;
                string mqty = row.Cells[6].Text;



                string delLogqry = "Insert into tbl_SupplyOrder_Master_Log select * from tbl_SupplyOrder_Master where Orderno='" + mOrderno + "' and Qty='" + mqty + "' ";
                SqlCommand delcmd = new SqlCommand(delLogqry, con);
                int log = delcmd.ExecuteNonQuery();


                string query = "delete  from tbl_SupplyOrder_Master where Orderno='" + mOrderno + "' and Qty='" + mqty + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int req = cmd.ExecuteNonQuery();



                count = count + 1;

            }
            fillgrid();
            if (count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please click on check box before delete'); </script> ");
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");
            }
        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
    protected void Btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/State_Welcome.aspx");
    }
    protected void ddlorderno_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
