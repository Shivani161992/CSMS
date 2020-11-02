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
public partial class State_RR_Entry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj = null;
    Commodity_MP comdtobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string stid = "";
    public string getdatef = "";
    public string hname = "";
    chksql chk = null;
    public string transid = "";
    public int railnum;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            stid = Session["st_id"].ToString();           
           


            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtrrqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtwcount.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");


            txtrrqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrrqty.Attributes.Add("onchange", "return chksqltxt(this)");


            txtwcount.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtwcount.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrrno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtrrno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrrno.Attributes.Add("onchange", "return chksqltxt(this)");



            txtrrqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtwcount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrrno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtrrqty.Text);
            ctrllist.Add(txtwcount.Text);
            ctrllist.Add(txtrrno.Text);
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

            if (!IsPostBack)
            {
                GetSourceDist();
                //GetRack();
                dt.Columns.Add("district_code");
                dt.Columns.Add("Rack_No");
                dt.Columns.Add("RR_No");
                dt.Columns.Add("RR_qty");
                dt.Columns.Add("Wagon_Count");

                Session["dt"] = dt;
              
               
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }

    void GetSourceDist()
    {

        mobj = new MoveChallan(ComObj);
        string qrydist = "select district_name,district_code  from pds.districtsmp  order by district_name ";
        DataSet dsd = mobj.selectAny(qrydist);

        ddlsourcedist.DataSource = dsd.Tables[0];
        ddlsourcedist.DataTextField = "district_name";
        ddlsourcedist.DataValueField = "district_code";
        ddlsourcedist.DataBind();
        ddlsourcedist.Items.Insert(0, "--Select--");

    }
    void GetRack()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        ddlrackno.Items.Clear();
        string qreyrac = "select Rack_No  from dbo.Rake_Master_Sugar where district_code='" + ddlsourcedist.SelectedValue  + "' and Month =" + month + " and Year=" + year;
        cmd.Connection = con;
        cmd.CommandText =qreyrac;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrackno.Items.Add(dr["Rack_No"].ToString());
            
        }
        ddlrackno.Items.Insert(0, "--Select--");
        dr.Close();
        con.Close();

    }
    void GetData()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
            txtsrailh.Text = "";
            txtdesrailh.Text ="";
            ddlrackno.Focus();
        }
        else
        {
            string rackno = ddlrackno.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qrey = "select tbl_Rail_Head.RailHead_Name as RailHead_Name from dbo.Rake_Master_Sugar left join dbo.tbl_Rail_Head on Rake_Master_Sugar.RailHead=tbl_Rail_Head.RailHead_Code   where Rake_Master_Sugar.district_code='" + ddlsourcedist.SelectedValue + "' and Rake_Master_Sugar.Rack_No='" + rackno +"'";
            DataSet ds = mobj.selectAny(qrey);
             if (ds.Tables[0].Rows.Count==0)
            {
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                txtsrailh.Text = drs["RailHead_Name"].ToString();           

                txtsrailh.ReadOnly = true;
                
            }
        }
    }
    //void fillgrid()
    //{
    //    mobj = new MoveChallan(ComObj);
    //    string qrey = "select  tbl_RackMaster.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name ,districtsmp.district_Name,tbl_Rail_Head.RailHead_Name ,RailHead_Dest.RailHead_Name as DestRName from dbo.tbl_RackMaster left join pds.districtsmp on tbl_RackMaster.Dest_District=districtsmp.district_code left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_RackMaster.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID  left join dbo.tbl_Rail_Head on tbl_RackMaster.RailHead=tbl_Rail_Head.RailHead_Code left join  dbo.tbl_Rail_Head as RailHead_Dest on tbl_RackMaster.RailHead=RailHead_Dest.RailHead_Code   where tbl_RackMaster.district_code='" + distid + "'";
    //    DataSet ds = mobj.selectAny(qrey);
    //     if (ds.Tables[0].Rows.Count==0)
    //    {
    //    }
    //    else
    //    {
    //        GridView1.DataSource = ds.Tables[0];
    //        GridView1.DataBind();
    //    }


    //}
   
   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;
    }
    
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
  
  
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");     
    }
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string state = Session["State_Id"].ToString();
        string opid = "H";
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number ...');</script>");
        }
        else
        {
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Connection = con;
            cmd.Transaction = trns;
            
            dt = (DataTable)Session["dt"];           
            
            int count = dt.Rows.Count;
            if (count > 0)
            {
                try
                {
                    int month = int.Parse(DateTime.Today.Month.ToString());
                    int year = int.Parse(DateTime.Today.Year.ToString());
                    string crdate = DateTime.Today.Date.ToString();
                    string udate = "";
                    int i = 0;

                    while (i < count)
                    {
                        string qry = "insert into dbo.Rail_Receipt_Details(State_Id,district_code,Rack_No,RR_No,RR_Qty,Wagon,Month,Year,Created_By,Created_date,Updated_date,RR_status,IP,OperatorID)values('" + state + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][2] + "'," + dt.Rows[i][3] + ",'" + dt.Rows[i][4] + "'," + month + "," + year + ",'" + ip + "',getdate(),'" + udate + "','S','"+ ip+"','"+ opid +"')";
                        cmd.CommandText = qry;
                        cmd.ExecuteNonQuery();
                        i = i + 1;
                    }
                    trns.Commit();
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                    btnSubmit.Enabled = false;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    Label1.Visible = true;
                    Label1.Text = ex.Message;

                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();

                }
            }
            else
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Enter RR Number/Quantity/No of Wagons in RR ...');</script>");

            }
        }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number ...');</script>");
        }
        else
        {

            float rrqty = CheckNull(txtrrqty.Text);
            string rackno = ddlrackno.SelectedValue;
            string rrno = txtrrno.Text;
            int mwcount = CheckNullInt(txtwcount.Text);
            if (rrqty == 0 || mwcount == 0)
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Enter R.R.Qty/No.of Wagon in RR ...');</script>");
            }
            else
            {
                dt = (DataTable)Session["dt"];
                dt.Rows.Add(ddlsourcedist.SelectedValue , rackno, rrno, rrqty, mwcount);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                txtrrno.Text = "";
                txtrrqty.Text = "";
                txtwcount.Text = "";
                txtrrno.Focus();
                Session["dt"] = dt;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlsourcedist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRack();
    }
}
