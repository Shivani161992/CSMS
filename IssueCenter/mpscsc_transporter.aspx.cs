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
public partial class mpscsc_transporter : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Transporter tobj = null;
    Transporter tobj1 = null;
    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public string sid = "";
    public string tid = "";
    public string distid= "";
    public string tname = "";
    public string gatepass = "";
    public int getnum;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            sid = Session["issue_id"].ToString();
            distid = Session["dist_id"].ToString();
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtmobile.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");


            txtmobile.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            if (Page.IsPostBack == false)
            {
                Fillgrid();
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {


        string state = "23";
        mobj1 = new MoveChallan(ComObj);
        string qrey = "select max(Transporter_ID) as Transporter_ID  from dbo.Transporter_Table where Depot_ID='" + sid + "' and Distt_ID='" + distid  + "'";
        DataSet ds = mobj1.selectAny(qrey);
        DataRow dr = ds.Tables[0].Rows[0];
        gatepass = dr["Transporter_ID"].ToString();
        if (gatepass == "")
        {
            gatepass = sid + "1";


        }
        else
        {
            getnum = Convert.ToInt32(gatepass);
            getnum = getnum + 1;
            gatepass = getnum.ToString();


        }
        string mtname = txttname.Text;


        string madd = txttadd.Text;
        string mobile = txtmobile.Text;
        string qry = "insert into dbo.Transporter_Table (Distt_ID,Depot_ID,Transporter_ID,Transporter_Name,Address,MobileNo) values('" + sid + "','" + sid + "','" + gatepass + "','" + mtname + "','" + madd + "','" + mobile + "')";
        cmd.CommandText = qry;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted  successfully...'); </script> ");

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


        Fillgrid();

        

    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
       
  
    }
    void Fillgrid()
    {
        tobj = new Transporter(ComObj);
        string query = "select * from dbo.Transporter_Table where Depot_ID='" + sid + "' and Distt_ID='" + distid + "'";
        DataSet ds = tobj.selectAny(query);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        Button1.Visible = false;


    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Wheat;
        txttname.BackColor = System.Drawing.Color.Wheat;
        tname=GridView1.SelectedRow.Cells [2].Text;
        tid = GridView1.SelectedRow.Cells[1].Text;
        
        txttname.Text = tname;
        txttadd.Text = tid;
        btnadd.Visible = false;
        btnclose.Visible = false;
        Button1.Visible = true;
        txttadd.Visible = false;
        lbltadd.Visible = false;
    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        tobj = new Transporter(ComObj);
        tobj.DepotID = sid;
        tobj.Transporter_Name = txttname.Text;
        tobj.Transporter_ID = txttadd.Text;
        int count = tobj.update();
        if (count == 1)
        {
           
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Updated successfully...'); </script> ");
            


        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Some Error Occured...'); </script> ");

        }
    }



    protected void btnaddnew_Click1(object sender, EventArgs e)
    {
        btnadd.Visible = true;
        btnclose.Visible = true;
        txttadd.Visible = true;
        lbltadd.Visible = true;
        txttname.Text = "";
        txttname.Text = "";
        txttname.BackColor = System.Drawing.Color.Bisque;
        txttname.Focus();
    }
}
