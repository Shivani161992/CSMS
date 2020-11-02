using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Data.SqlClient;
using Data;
using DataAccess;

public partial class State_SectorMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    MoveChallan mobj1 = null;
    DistributionCenters distobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDistrict();
            GetTransportType();

        }
    }
    void GetTransportType()
    {



        string type = "Select * from dbo.m_Transport_Type order by Transport_ID";

        SqlCommand cmd = new SqlCommand(type, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddltranstype.DataSource = ds.Tables[0];
                    ddltranstype.DataTextField = "Transport_Type";
                    ddltranstype.DataValueField = "Transport_ID";
                    ddltranstype.DataBind();
                    ddltranstype.Items.Insert(0, "--Select--");
                }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue == "0" || ddlissuecenter.SelectedValue == "0" || ddldistrict.SelectedItem.Text == "--Select--" || ddlissuecenter.SelectedItem.Text == "--Select--"|| ddltranstype.SelectedIndex==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Issue Center or District Name'); </script> ");
            return; 
        }

        if (txtsector.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Insert Name of Sector'); </script> ");
            return; 
        }

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        

        string sectorname = txtsector.Text.Trim();

        string distid = ddldistrict.SelectedValue;

        string issuecenter = ddlissuecenter.SelectedValue;

        string qrey = "select max(SectorId) as SectorId from dbo.District_SectorMaster where DistrictId='" + distid + "'";

        SqlCommand cmd_secid = new SqlCommand(qrey, con);
        
        SqlDataReader sqldr = cmd_secid.ExecuteReader();
        sqldr.Read();

        string seccode = sqldr["SectorId"].ToString();

        if (seccode == "")
        {
            seccode = 23 + distid + "201";

        }
        else
        {
            Int32 secnum = Convert.ToInt32(seccode);
            secnum = secnum + 1;
            seccode = secnum.ToString();

        }

        sqldr.Close();

        string insqry = "Insert into District_SectorMaster (DistrictId,IssueCenter,SectorId,SectorName,CreatedDate,IP_Address,trans_Type) Values ('" + distid + "','" + issuecenter + "','" + seccode + "',N'" + sectorname + "',getdate(),'" + ip + "','" + ddltranstype.SelectedValue.ToString() + "')";

        SqlCommand cmd = new SqlCommand(insqry, con);

        try
        {
            int x = cmd.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved....'); </script> ");

            txtsector.Text = "";


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Thing Going wrong'); </script> ");
        }
    }

    public void GetDistrict()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        {
            try
            {
                string qry = "SELECT district_code ,district_name FROM pds.districtsmp order by district_name ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddldistrict.DataSource = ds.Tables[0];
                    ddldistrict.DataTextField = "district_name";
                    ddldistrict.DataValueField = "district_code";
                    ddldistrict.DataBind();
                    ddldistrict.Items.Insert(0, "--Select--");

                }
            }
            catch (Exception)
            {
                //////
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepot();
    }

    public void GetDepot()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ord = "Select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = '23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(ord, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            ddlissuecenter.DataSource = ds.Tables[0];
            ddlissuecenter.DataTextField = "DepotName";
            ddlissuecenter.DataValueField = "DepotId";

            ddlissuecenter.DataBind();

            ddlissuecenter.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/State/SectorMaster.aspx");
    }
}
