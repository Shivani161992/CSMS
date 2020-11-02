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

public partial class District_Print_TO : System.Web.UI.Page
{
     public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

     public string distid = "";

     public string IssuecenterID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            if (!IsPostBack)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

               get_IssueCenter();

               if (con.State == ConnectionState.Open)
               {
                   con.Close();
               }
            }

        }
        hlinkpdo.Attributes.Add("onclick", "window.open('Print_DPY_TransportOrder.aspx',null,'left=800, top=800, height=900, width= 800, status=n o, resizable= yes, scrollbars= yes, toolbar= no,location= no, menubar= no');");

    }

    protected void get_IssueCenter()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qry = "select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = 23" + distid + "";

        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlissueCenter.DataSource = ds.Tables[0];
            ddlissueCenter.DataTextField = "DepotName";
            ddlissueCenter.DataValueField = "DepotID";
            ddlissueCenter.DataBind();
            ddlissueCenter.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlissueCenter.DataSource = "";

            ddlissueCenter.DataBind();

            ddlissueCenter.Items.Insert(0, "--Select--");

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlissueCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlissueCenter.SelectedItem.Text == "--Select--")
        {
            ddlto.DataSource = "";

            ddlto.DataBind();

            ddlto.Items.Insert(0, "--Select--");
        }

        else
        {
            Session["sid"] = ddlissueCenter.SelectedValue;

            string qry = "select distinct TransportOrder from DPY_TranportOrder where IssueCenter = '"+ddlissueCenter.SelectedValue+"'";

            SqlCommand cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlto.DataSource = ds.Tables[0];
                ddlto.DataTextField = "TransportOrder";
                ddlto.DataValueField = "TransportOrder";
                ddlto.DataBind();
                ddlto.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlto.DataSource = "";

                ddlto.DataBind();

                ddlto.Items.Insert(0, "--Select--");

            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlto.SelectedValue == "0" || ddlto.SelectedItem.Text == "--Select--")
        {
            hlinkpdo.Visible = false;
        }

        else
        {
            Session["TO"] = ddlto.SelectedValue;

           hlinkpdo.Visible = true;
        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
}
