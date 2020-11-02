using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
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

public partial class State_New_Supplier_master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    public string stid = "";
  
    protected Common ComObj = null, cmn = null;
    public string getdate = "";
    public Common cm = null;
   
    chksql chk = null;
  
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["st_id"] != null)
        {

            txtsuppliername.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtsuppliername.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtsuppliername.Attributes.Add("onchange", "return chksqltxt(this)");

            txtplace.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtplace.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtplace.Attributes.Add("onchange", "return chksqltxt(this)");

          
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtsuppliername.Text);
            ctrllist.Add(txtplace.Text);
            if (chk == null)
            {
            }
            else
            {
                bool chkstr = chk.chksql_server(ctrllist);
                if (chkstr == true)
                {
                    Page.Server.Transfer(HttpContext.Current.Request.Path);
                }
            }


            if (Page.IsPostBack == false)
            {
                fillgrid();

            }


        }
        else
        {
           
        }


    }
   
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select  * from dbo.Supplier_master";
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
   
    protected void btnadd_Click(object sender, EventArgs e)
    {


        string name = txtsuppliername.Text;
        string place = txtplace.Text;
        string commodity = RadioButtonList1.SelectedValue;
        string status = RadioButtonList2.SelectedValue;
        string crdate = DateTime.Today.Date.ToString();
        string IPAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string insert = "insert into dbo.Supplier_master(Name,Place,Commodity,Status,Ip_Address,Created_Date)values('" + name + "','" + place + "','" + commodity + "','" + status + "','" + IPAddr + "',getdate())";
        cmd.Connection = con;
        cmd.CommandText = insert;
        con.Open();
        
        try
        {

          
            cmd.ExecuteNonQuery();
         
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
            btnadd.Enabled = false;

        }
        catch (Exception ex)
        {
          
            Label1.Visible = true;
            Label1.Text = ex.Message;
        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();
        }

        txtsuppliername.Text = "";
        txtplace.Text = "";



       


    }
        protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
        //txtsuppliername.Text = GridView1.SelectedRow.Cells[1].Text;
        //txtplace.Text = GridView1.SelectedRow.Cells[2].Text;

        //btnadd.Visible = false;
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
