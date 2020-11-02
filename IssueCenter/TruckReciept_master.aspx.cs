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
public partial class IssueCenter_Godown_master : System.Web.UI.Page
{
    Transporter tobj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string issueid = "";
    DataTable dt = new DataTable();
    protected Common ComObj = null, cmn = null;
    public string challan = "";
    public string truckno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

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
    void Fillgrid()
    {
        tobj = new Transporter(ComObj);
        string query = "select Truck_Challan,Truck_No,Created_Date from dbo.Reciept_Truck_Details where Depot_Id='" + issueid + "'";
        DataSet ds = tobj.selectAny(query);

         if (ds.Tables[0].Rows.Count==0)
        {
            Label1.Text = "Currently no record is present";
            Label1.Visible = true;
            btnupdate.Visible = false;

        }
        else
        {
            Label1.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            btnupdate.Visible = false;
            
        }


    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string challanno = txtchallanno.Text;
        string mtruckno = txttruckno.Text;
        string mcdate = DateTime.Today.Date.ToString();
        string mudate = "";
        string mcby = issueid;
       
        string mgfdate = DateTime.Today.Date.ToString();
       
        string mddate = "";
       
       
       

        string qyr = "insert into dbo.Reciept_Truck_Details(Distt_Id,Depot_Id,Truck_Challan,Truck_No,Created_Date,Updated_Date,Deleted_Date)values('"+ distid + "','" + issueid + "','" + challanno + "','" + mtruckno +"','" + mcdate +  "','" + mudate +"','" + mddate + "'" + ")";
        
        cmd.Connection = con;
        cmd.CommandText = qyr;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Succesfully ......'); </script> ");
            txtchallanno.Text = "";
            txttruckno.Text = "";
            txtchallanno.Focus();
            Fillgrid();
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
        con.Open();

        btnaddnew.Visible = true;
        lbltname.Visible = false;
        lbltadd.Visible = false;
        txtchallanno.Visible = false;
        txttruckno.Visible = false;
        btnadd.Visible = false;
        btnclose.Visible = false;
        btnupdate.Visible = false;

    }
    protected void btnaddnew_Click1(object sender, EventArgs e)
    {
        Label1.Visible = false;
        lbltname.Visible = true;
        lbltadd.Visible = true;
        txtchallanno.Visible = true;
        txttruckno.Visible = true;
        btnadd.Visible = true;
        btnclose.Visible = true;
        txtchallanno.Focus();
        btnaddnew.Visible = false;
        btnupdate.Visible = false;
        
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {

    }

   
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Wheat;
        challan = GridView1.SelectedRow.Cells[1].Text;
        truckno = GridView1.SelectedRow.Cells[2].Text;

        txttruckno.BackColor = System.Drawing.Color.Wheat;
        txtchallanno.Text = challan;
        txttruckno.Text = truckno;
        btnaddnew.Visible = false;
        btnupdate.Visible = true;
        txtchallanno.Visible = true;
        txttruckno.Visible = true;
        lbltname.Visible = true;
        lbltadd.Visible = true;
        txtchallanno.Enabled = false;
        txttruckno.Focus();
        //btnadd.Visible = false;
        //btnclose.Visible = false;
        //Button1.Visible = true;
        //txttadd.Visible = false;
        //lbltadd.Visible = false;

    }
}
