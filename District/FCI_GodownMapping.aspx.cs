using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class District_FCI_GodownMapping : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                string getdis = "select district_code, district_name from pds.districtsmp";

                SqlCommand cmd = new SqlCommand(getdis, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddldist.DataSource = ds.Tables[0];

                    ddldist.DataTextField = "district_name";

                    ddldist.DataValueField = "district_code";

                    ddldist.DataBind();

                    ddldist.Items.Insert(0, "-Select-");
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();

                }

                bindgrid();
            }


        }
    }
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {

        string getdis = "select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = 23 '"+ddldist.SelectedValue+"'";

        SqlCommand cmd = new SqlCommand(getdis, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlissue.DataSource = ds.Tables[0];

            ddlissue.DataTextField = "DepotName";

            ddlissue.DataValueField = "DepotID";

            ddlissue.DataBind();

            ddlissue.Items.Insert(0, "-Select-");
        }

        else
        {
            ddlissue.DataSource = "";

            ddlissue.DataBind();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
    }

    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }


    void GetGodown()
    {
      
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + ddldist.SelectedValue + "' and DepotId='" + ddlissue.SelectedValue + "' order by Godown_ID";
        SqlCommand cmd = new SqlCommand(qry, cons);

        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");

        }

        else
        {
            ddlgodown.DataSource = "";

            ddlgodown.DataBind();
        }
    

        if (cons.State == ConnectionState.Open)
        {
            cons.Close();

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (ddldist.SelectedValue == "0" || ddldist.SelectedItem.Text == "-Select-")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select District'); </script> ");
            return;
        }


        if (ddlissue.SelectedValue == "0" || ddlissue.SelectedItem.Text == "-Select-")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Issue Center'); </script> ");
            return;
        }


        if (ddlgodown.SelectedValue == "0" || ddlgodown.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Plz Select Godown'); </script> ");
            return;
        }
        
        string distid = ddldist.SelectedValue;

        string issueid = ddlissue.SelectedValue;

        string godownName = ddlgodown.SelectedItem.Text;

        string godown = ddlgodown.SelectedValue;

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string ins = "Insert into MappedFCIGodown DistrictID ,IssueId ,GodownName ,Godownid  ,Ip ,CreatedDate) Values ('" + distid + "', '" + issueid + "', '"+godownName+"' , '" + godown + "' , '" + ip + "', getdate())";

        SqlCommand cmd = new SqlCommand(ins,con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            int x = cmd.ExecuteNonQuery();

            if (x > 0)
            {
                bindgrid();

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Submited'); </script> ");
                return;
            }

            else
            {

            }

        }

        catch(Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }

        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        

    }

    void bindgrid()
    {
        string bind = "select pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , GodownName ,Godownid from MappedFCIGodown inner join pds.districtsmp on pds.districtsmp.district_code = MappedFCIGodown.DistrictID inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = MappedFCIGodown.IssueId ";

        SqlCommand cmd = new SqlCommand(bind, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = ds.Tables[0];

            GridView2.DataBind();
        }

        else
        {
            GridView2.DataSource = "";

            GridView2.DataBind();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }
}
