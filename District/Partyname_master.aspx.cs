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

public partial class District_Partyname_master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    string distid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] == null)
        {
            Response.Redirect("~/Session_Expire_Dist.aspx");
        }

        else
        {
            distid = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                getsaletype();

                BindGrid();
                
                txtmobile.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
            }
            
        }
    }

    protected void getsaletype()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qry = "SELECT SaleId ,SaleType FROM DO_SaleType where SaleId not in (1)";
        SqlCommand cmdsale = new SqlCommand(qry, con);
        SqlDataAdapter dasale = new SqlDataAdapter(cmdsale);
        DataSet dsSale = new DataSet();

        dasale.Fill(dsSale);

        if (dsSale.Tables[0].Rows.Count == 0)
        {

        }

        else
        {
            ddltype.DataSource = dsSale.Tables[0];
            ddltype.DataTextField = "SaleType";
            ddltype.DataValueField = "SaleId";

            ddltype.DataBind();
            ddltype.Items.Insert(0, "--Select--");
           
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void BindGrid()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qry = "SELECT  OpenSaleParty.PartyId ,OpenSaleParty.PartyName ,OpenSaleParty.Mobile ,OpenSaleParty.SaleTypeID ,OpenSaleParty.Address,DO_SaleType.SaleType FROM OpenSaleParty inner join DO_SaleType on DO_SaleType.SaleId = OpenSaleParty.SaleTypeID where OpenSaleParty.District = '" + distid + "'";
        SqlCommand cmd = new SqlCommand(qry,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = ds;
            gvDetails.DataBind();
        }

        else
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvDetails.DataSource = ds;
            gvDetails.DataBind();
            int columncount = gvDetails.Rows[0].Cells.Count;
            gvDetails.Rows[0].Cells.Clear();
            gvDetails.Rows[0].Cells.Add(new TableCell());
            gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
            gvDetails.Rows[0].Cells[0].Text = "No Records Found";
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //getting username from particular row
           
            //identifying the control in gridview
            LinkButton lnkbtnresult = (LinkButton)e.Row.FindControl("LinkButton3");
            //raising javascript confirmationbox whenver user clicks on link button
            if (lnkbtnresult != null)
            {
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox()");
            }

        }
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int PartyId = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["PartyId"].ToString());

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("delete from OpenSaleParty where PartyId=" + PartyId, con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        if (result == 1)
        {
            BindGrid();
        }
    }

    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int PartyId = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
        //string SaleTypeID = gvDetails.DataKeys[e.RowIndex].Values["SaleTypeID"].ToString();

        TextBox txtName = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtName");
        TextBox txtMobile = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMobile");
        TextBox txtadd = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtadd");

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand("update OpenSaleParty set PartyName='" + txtName.Text + "',Mobile='" + txtMobile.Text + "',Address='" + txtadd.Text + "' where PartyId=" + PartyId, con);
        cmd.ExecuteNonQuery();
        con.Close();

        gvDetails.EditIndex = -1;
        BindGrid();
    }

    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDetails.EditIndex = -1;
        BindGrid();
    }

    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDetails.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddltype.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Sale Type of Party'); </script> ");
            return;
        }

        if (txtname.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Name Must be Filled'); </script> ");
            return;
        }

        if (txtmobile.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Mobile Must be Filled'); </script> ");
            return;
        }

        if (txtaddress.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Address'); </script> ");
            return;
        }


        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();

        string partyname = txtname.Text.Trim();

        string address = txtaddress.Text.Trim();

        string mobile = txtmobile.Text.Trim();

        string saletype = ddltype.SelectedValue;

        string duplicatecheck = "Select * from OpenSaleParty where District = '" + distid + "' and PartyName = '" + partyname + "' and Mobile = '" + mobile + "' and SaleTypeID = '" + saletype + "' ";
        SqlCommand cmdduplicate = new SqlCommand(duplicatecheck, con);

        SqlDataReader dr;

        dr = cmdduplicate.ExecuteReader();

        if (dr.Read())
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Party Name Exist....'); </script> ");
            dr.Close();
            return;
        }
        else
        {
           
            dr.Close();
            string insqry = "Insert into OpenSaleParty (District,PartyName,Address,Mobile,SaleTypeID,IP,Createddate,usertype) Values ('" + distid + "','" + partyname + "','" + address + "','" + mobile + "','" + saletype + "','" + ip + "',getdate(),'" + opid + "')";

            SqlCommand cmd = new SqlCommand(insqry, con);

            try
            {
                int x = cmd.ExecuteNonQuery();

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved....'); </script> ");

                txtname.Text = "";
                txtmobile.Text = "";
                txtaddress.Text = "";

                BindGrid();
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Thing Going wrong'); </script> ");
            }

        }     

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}
