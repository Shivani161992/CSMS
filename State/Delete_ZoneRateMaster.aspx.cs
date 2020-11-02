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

public partial class State_Delete_ZoneRateMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;


    protected Common ComObj = null, cmn = null;
    chksql chk = null;
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



            //GetDist();
            //Getzone();
            GetSupplier();
            fillgirdforTenderRate();
         

        }
        
    }

    private void fillgirdforTenderRate()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select distinct ZoneMaster.ZoneCode,ZoneMaster.Zone,tblZoneMasterNew.Tender_Rate,Financial_Year from tblZoneMasterNew inner join ZoneMaster on ZoneMaster.ZoneCode = tblZoneMasterNew.ZoneCode where tblZoneMasterNew.ZoneCode='" + ddlzonename.SelectedValue + "'";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null )
        {
            
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

    private void GetSupplier()
    {
        SqlCommand cmd = new SqlCommand("select Distinct Name  from tblZoneMasterNew", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlsuppliername.DataSource = ds;
        ddlsuppliername.DataTextField = "Name";
        //ddlsupplier.DataValueField = "id";
        ddlsuppliername.DataBind();
        ddlsuppliername.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlzonename_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgirdforTenderRate();
    }
    protected void ddlsuppliername_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetZone();
    }

    private void GetZone()
    {
        SqlCommand cmd = new SqlCommand("select Distinct ZoneMaster.ZoneCode from ZoneMaster inner join tblZoneMasterNew on tblZoneMasterNew.Zonecode = ZoneMaster.Zonecode where tblZoneMasterNew.Name='" + ddlsuppliername.SelectedItem.Text.ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlzonename.DataSource = ds;
        //ddlzonename.DataTextField = "Zone";
        ddlzonename.DataValueField = "ZoneCode";
        ddlzonename.DataBind();
        ddlzonename.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        int count = 0;
        string ZoneCode = ddlzonename.SelectedValue;
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = null;
            chk = (CheckBox)row.FindControl("chkDelete");
            if (chk.Checked == true)
            {

                //string godown = chk.Text.Trim().ToString();

                string mmzonecode = row.Cells[1].Text;

                string mzone = row.Cells[2].Text;

                string tenderrate = row.Cells[3].Text;
                string mfinancialyear = row.Cells[4].Text;



                string delLogqry = "Insert into tblZoneMasterNew_Log select * from tblZoneMasterNew where ZoneCode='" + mmzonecode + "'and Tender_Rate='" + tenderrate + "'";
                SqlCommand delcmd = new SqlCommand(delLogqry, con);
                int log = delcmd.ExecuteNonQuery();


                string query = "delete  from tblZoneMasterNew where ZoneCode='" + mmzonecode + "'and Tender_Rate='" + tenderrate + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int req = cmd.ExecuteNonQuery();

               

                count = count + 1;

            }
            fillgirdforTenderRate();

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
}
