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
using System.Data.SqlClient;
using Data;
using DataAccess;

public partial class District_Delete_DoOpenSale : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] == null)
        {
            Response.Redirect("~/Session_Expire_Dist.aspx");
        }

        else
        {
            if (!IsPostBack)
            {
                getsaletype();
            }       
        }
    }

    protected void getsaletype()
    {
        string qry = "SELECT SaleId ,SaleType FROM DO_SaleType";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdsale = new SqlCommand(qry, con);
        SqlDataAdapter dasale = new SqlDataAdapter(cmdsale);
        DataSet dsSale = new DataSet();

        dasale.Fill(dsSale);

        if (dsSale.Tables[0].Rows.Count == 0)
        {

        }

        else
        {
            ddlIssueType.DataSource = dsSale.Tables[0];
            ddlIssueType.DataTextField = "SaleType";
            ddlIssueType.DataValueField = "SaleId";

            ddlIssueType.DataBind();

            ddlIssueType.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void ddlIssueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlIssueType.SelectedValue == "0" || ddlIssueType.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Sale Type');</script>");
            return;
        }


        else
        {
            string qry = "SELECT delivery_order_no FROM OpenSale_DO where district_code = '" + Session["dist_id"].ToString() + "' and issue_type = '" + ddlIssueType.SelectedValue + "' and  OpenSale_DO.delivery_order_no not in (select issue_against_OpenSale_do.delivery_order_no from issue_against_OpenSale_do where district_code = '" + Session["dist_id"].ToString() + "' and issue_to = '" + ddlIssueType.SelectedValue + "')";
            
            SqlCommand cmdsale = new SqlCommand(qry, con);
            SqlDataAdapter dasale = new SqlDataAdapter(cmdsale);
            DataSet dsSale = new DataSet();

            dasale.Fill(dsSale);

            if (dsSale.Tables[0].Rows.Count == 0)
            {
                lblcommodity.Text = "";

                lbldate.Text = "";

                lblQty.Text = "";

                btndelete.Enabled = false;

                ddlDONumber.DataSource = "";
                ddlDONumber.DataBind();
            }

            else
            {
                btndelete.Enabled = true;

                ddlDONumber.DataSource = dsSale.Tables[0];
                ddlDONumber.DataTextField = "delivery_order_no";


                ddlDONumber.DataBind();

                ddlDONumber.Items.Insert(0, "--Select--");
            }
        }
       

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void ddlDONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDONumber.SelectedValue == "0" || ddlDONumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Do Number');</script>");
            return;
        }

        else
        {

        string qry = "select  quantity , CONVERT(nvarchar,do_date,103) do_date , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name   from OpenSale_DO inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = OpenSale_DO.commodity_id where district_code = '" + Session["dist_id"].ToString() + "'   and issue_type = '" + ddlIssueType.SelectedValue + "' ";

        SqlCommand cmd = new SqlCommand(qry,con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            lblcommodity.Text = "";

            lbldate.Text = "";

            lblQty.Text = "";

            btndelete.Enabled = false;
        }

        else
        {
            btndelete.Enabled = true;

            lblcommodity.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();

            lbldate.Text = ds.Tables[0].Rows[0]["do_date"].ToString();

            lblQty.Text = ds.Tables[0].Rows[0]["quantity"].ToString();
        }

    }

    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (ddlIssueType.SelectedItem.Text == "" || ddlIssueType.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Sale Type and DO Number');</script>");
            return;
        }
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string inslog = "Insert into OpenSale_DO_Log select * from OpenSale_DO where district_code = '" + Session["dist_id"].ToString() + "'   and issue_type = '" + ddlIssueType.SelectedValue + "' and OpenSale_DO.delivery_order_no = '"+ddlDONumber.SelectedItem.Text+"'";

        SqlCommand cmd = new SqlCommand(inslog, con);      
        
        string Delqry = "delete from OpenSale_DO where district_code = '" + Session["dist_id"].ToString() + "'   and issue_type = '" + ddlIssueType.SelectedValue + "' and OpenSale_DO.delivery_order_no = '" + ddlDONumber.SelectedItem.Text + "'";

        SqlCommand cmd1 = new SqlCommand(Delqry, con);    

        try
        {
            cmd.ExecuteNonQuery();

            cmd1.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Delete Sucessfully');</script>");
        }

        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Error!!');</script>");
            return;
        }

        finally
        {
            btndelete.Enabled = false;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }       
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        btndelete.Enabled = true;

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}
