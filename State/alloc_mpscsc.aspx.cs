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
public partial class dist_alloc_mpscsc : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    chksql chk = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["st_id"] == null)
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        distid = Session["st_id"].ToString();
        if (Page.IsPostBack == false)
        {
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;
            //ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
            get_comm();            
            get_scheme();
            getDistrict();
        }
        TextBox1.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");


        TextBox1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
        TextBox1.Attributes.Add("onchange", "return chksqltxt(this)");
        TextBox1.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
        ctrllist.Add(TextBox1.Text);
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


    }
    protected void get_comm()
    {
        cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where Status='Y' order by Commodity_Name Desc";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["commodity_name"].ToString();
            lstitem.Value = dr["commodity_id"].ToString();
            ddl_commodity.Items.Add(lstitem);
        }
        ListItem lstitem1 = new ListItem();
        lstitem1.Text = "Select";
        lstitem1.Value = "N";
        ddl_commodity.Items.Insert(0, lstitem1);

        dr.Close();
        con.Close();

    }
    protected void get_scheme()
    {
        Label1.Text = "";
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME where Scheme_Id in ('34','35','103') and Status='Y'  order by Scheme_Id";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["scheme_name"].ToString();
            lstitem.Value = dr["scheme_id"].ToString();
            ddl_scheme.Items.Add(lstitem);
        }
        ddl_scheme.Items.Insert(0, "Select");
        ListItem lstitem1 = new ListItem();
        dr.Close();
        con.Close();
    }

    protected void getDistrict()
    {
        string dist = "SELECT district_code ,Dist_name  FROM pds.districtsmp where district_code not in (99)";

        SqlCommand cmddist = new SqlCommand(dist, con);
        SqlDataAdapter dadist = new SqlDataAdapter(cmddist);
        DataSet ds = new DataSet();
        dadist.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldist.DataSource = ds.Tables[0];
            ddldist.DataTextField = "Dist_name";
            ddldist.DataValueField = "district_code";
            ddldist.DataBind();
            ddldist.Items.Insert(0, "Select");
        }
    }

    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select" || ddldist.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme/District...');</script>");
            ddl_scheme.SelectedIndex = 0;
        }
        else
        {
            distid = ddldist.SelectedValue.ToString();

            TextBox1.Text = "";
            TextBox1.Focus();
            string str2 = "select alloc_qty from dbo.mpscsc_State_Qtr_allocation where district_code='" + distid + "' and Qtr_allocation=" + ddl_allot_month.SelectedItem.Value + " and alloc_year=" + ddd_allot_year.SelectedItem.Text + " and commodity_id='" + ddl_commodity.SelectedItem.Value + "' and scheme_id='" + ddl_scheme.SelectedItem.Value + "'";
            cmd.Connection = con;
            cmd.CommandText = str2;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TextBox1.Text = dr["alloc_qty"].ToString();
            }
            dr.Close();
            con.Close();
            save.Enabled = true;

            
        }
    }
    protected void save_Click(object sender, EventArgs e)
    {
        Label1.Text = "";


        if (ddl_scheme.SelectedItem.Text == "Select" || ddl_commodity.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme...');</script>");
        }        
        else
        {
            distid = ddldist.SelectedValue.ToString();
            string temp = "NNN";
            string str2 = "select alloc_qty from dbo.mpscsc_State_Qtr_allocation where district_code='" + distid + "' and Qtr_allocation=" + ddl_allot_month.SelectedItem.Value + " and alloc_year=" + ddd_allot_year.SelectedItem.Text + " and commodity_id=" + ddl_commodity.SelectedItem.Value + " and scheme_id='" + ddl_scheme.SelectedItem.Value + "'";
            cmd.Connection = con;
            cmd.CommandText = str2;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                temp = "YYY";
            }
            dr.Close();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
           
            string state = Session["State_Id"].ToString();
            if (temp == "YYY")
            {
                str2 = "update dbo.mpscsc_State_Qtr_allocation set alloc_qty=" + TextBox1.Text + ",updated_date=getdate(),ip_address='" + ip + "' where district_code='" + distid + "' and Qtr_allocation=" + ddl_allot_month.SelectedItem.Value + " and alloc_year=" + ddd_allot_year.SelectedItem.Text + " and commodity_id=" + ddl_commodity.SelectedItem.Value + " and scheme_id='" + ddl_scheme.SelectedItem.Value + "'";
            }
            else
            {
                str2 = "INSERT INTO dbo.mpscsc_State_Qtr_allocation(district_code,Qtr_allocation,alloc_year,commodity_id,scheme_id,alloc_qty,created_date,updated_date,ip_address)VALUES ('" + distid + "'," + ddl_allot_month.SelectedItem.Value + "," + ddd_allot_year.SelectedItem.Text + "," + ddl_commodity.SelectedItem.Value + ",'" + ddl_scheme.SelectedItem.Value + "'," + TextBox1.Text + ",getdate(),getdate(),'" + ip + "')";
            }
                
                cmd.CommandText = str2;
                cmd.Connection = con;
                try
                {                   
                    cmd.ExecuteNonQuery();                    
                    save.Enabled = false;
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                    Label1.ForeColor = System.Drawing.Color.Blue ;
                    Label1.Text = "Data Saved Successfully ...";              
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    con.Close();
                }        

            }
            
        }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        save.Enabled = true;
        ddl_scheme.SelectedIndex = 0;
        ddl_commodity.SelectedIndex = 0;
    }
    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/State_Welcome.aspx");
    }
}
