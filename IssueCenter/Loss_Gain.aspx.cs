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
public partial class IssueCenter_Loss_Gain : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;    
    string sid = "";
    string distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            distid = Session["dist_id"].ToString();
            txtrqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtrecbags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            tx_lgdate.Attributes.Add("onkeypress", "return CheckIndate(event);");
            txtrqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();           
            txtrecbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtmaxcap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcurntcap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtavalcap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();         
            if (!IsPostBack)
            {
                tx_lgdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                GetScheme();           
                GetCommodity();       
                GetSource();       
                GetGodown();
                Session["qry"] = "0";
            }
        }
        else
        {
            Response.Redirect("~/Session_Expire_Dist.aspx");
        }             
    }
    void GetGodown()
    {
        ddlgodown.Items.Clear();
        cmd.CommandText = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + sid + "' order by Godown_ID";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["Godown_Name"].ToString();
            lstitem.Value = dr["Godown_ID"].ToString();
            ddlgodown.Items.Add(lstitem);
        }
        ddlgodown.Items.Insert(0, "Select");
        dr.Close();
        con.Close();
    }
    void GetCommodity()
    {
        ddlcomdty.Items.Clear();
        cmd.CommandText = "select commodity_id,commodity_name from dbo.tbl_MetaData_STORAGE_COMMODITY where status='Y'";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["commodity_name"].ToString();
            lstitem.Value = dr["commodity_id"].ToString();
            ddlcomdty.Items.Add(lstitem);
        }
        ddlcomdty.Items.Insert(0, "Select");
        dr.Close();
        con.Close();
    }
    void GetScheme()
    {
        ddlscheme.Items.Clear();
        cmd.CommandText = "select * from dbo.tbl_MetaData_SCHEME   where status='Y' order by displayorder";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["scheme_name"].ToString();
            lstitem.Value = dr["scheme_id"].ToString();
            ddlscheme.Items.Add(lstitem);
        }
        ddlscheme.Items.Insert(0, "Select");
        dr.Close();
        con.Close();
    }
    void GetSource()
    {
        ddlsarrival.Items.Clear();
        cmd.CommandText = "SELECT * FROM dbo.Source_Arrival_Type where Source_ID !='08'  order by Source_ID";
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lstitem = new ListItem();
            lstitem.Text = dr["Source_Name"].ToString();
            lstitem.Value = dr["Source_ID"].ToString();
            ddlsarrival.Items.Add(lstitem);
        }
        ddlsarrival.Items.Insert(0, "Select");
        dr.Close();
        con.Close();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrqty.Text = "";
        txtrecbags.Text = "";             
        get_godowncap();
        get_Data_gwn();       
    }
    protected decimal CheckNull(string Val)
    {
        decimal rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Convert.ToDecimal(Val);
        }
        return rval;
    }
    protected int CheckNullInt(string Val)
    {
        int rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = int.Parse(Val);
        }
        return rval;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string opid = Session["OperatorId"].ToString();
        string state = Session["State_Id"].ToString();
        string notrans = "N";
        if (ddlcomdty.SelectedItem.Text == "Select" || ddlscheme.SelectedItem.Text == "Select" || ddlsarrival.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Source of Arrival/Commodity/Scheme...');</script>");
        }
        else if (ddlgodown.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Godown...');</script>");
        }
        else if (CheckNull(txtrqty.Text) < 0 )
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter Loss or Gain Quantity and Bags...');</script>");
        }
        else
        {
            string temp = "NNN";
            string temp1 = "NNN";

            string strqr = "select * from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + ddlcomdty.SelectedItem.Value + "'and Scheme_Id='" + ddlscheme.SelectedItem.Value + "' and Godown='" + ddlgodown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
            cmd.CommandText = strqr;
            cmd.Connection = con;
            
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                temp = "YYY";
            }
            dr.Close(); 

            

           string strqr1 = "select * from dbo.Current_Godown_Position where District_Id='" + distid + "' and Depotid='" + sid + "' and Godown='" + ddlgodown.SelectedItem.Value + "'";
            cmd.CommandText = strqr1;
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                temp1 = "YYY";
            }
            dr.Close();
           
            if (temp1 == "NNN" && temp == "NNN")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Record not Found...');</script>");
            }
            else 
            {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int trnscnt = 0;
            string rowcount = "";
            strqr = "select count(Depotid) as rwcount  from dbo.Loss_gain  where Depotid='" + sid + "' and District_Id='" + distid + "'";
            cmd.CommandText = strqr;
            cmd.Connection = con;
           
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rowcount = dr["rwcount"].ToString();
            }
            dr.Close();
           
            if (rowcount != "")
            {
                trnscnt = CheckNullInt(rowcount);
            }
            trnscnt = trnscnt + 1;
            string transid = sid.ToString() + (trnscnt).ToString();
            string str1 = "";
            if (Session["qry"].ToString() == "1")
            {
                string trnsid = Session["transid"].ToString();
                strqr = "select round(convert(decimal(18,5),Quantity),5) as Quantity ,Bags from dbo.Loss_gain  where District_Id='" + distid + "' and Depotid='" + sid + "' and Source='" + ddlsarrival.SelectedItem.Value + "' and Godown='" + ddlgodown.SelectedItem.Value + "' and Commodity_Id='" + ddlcomdty.SelectedItem.Value + "' and Scheme_Id='" + ddlscheme.SelectedItem.Value + "' and stock_type='" + ddl_lossgain.SelectedItem.Value + "' and trans_id='" + trnsid + "' ";
                cmd.CommandText = strqr;
                string rqty = "", rbags = "";
                cmd.Connection = con;
               
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    rqty = dr["Quantity"].ToString();
                    rbags = dr["Bags"].ToString();
                }
                dr.Close();
                
                Session["cqty"] = (CheckNull(txtrqty.Text) - CheckNull(rqty)).ToString();
                Session["cbags"] = (CheckNullInt(txtrecbags.Text) - CheckNullInt(rbags)).ToString();
                str1 = "update dbo.Loss_gain set Bags=" + txtrecbags.Text + ",Quantity=" + txtrqty.Text + ",CreatedDate='" + getDate_MDY(tx_lgdate.Text) + "',UpdatedDate=getdate()  where District_Id='" + distid + "' and Depotid='" + sid + "' and Source='" + ddlsarrival.SelectedItem.Value + "' and Godown='" + ddlgodown.SelectedItem.Value + "' and Commodity_Id='" + ddlcomdty.SelectedItem.Value + "' and Scheme_Id='" + ddlscheme.SelectedItem.Value + "' and stock_type='" + ddl_lossgain.SelectedItem.Value + "' and trans_id='" + trnsid + "' ";
            }
            else
            {
                str1 = "INSERT INTO Loss_gain( State_Id,trans_id, District_Id , Depotid, Commodity_Id, Scheme_Id, Godown, Source, stock_type, Bags, Quantity, IP_Address, CreatedDate , UpdatedDate,OperatorID,NoTransaction) VALUES ('" + state + "','" + transid + "','" + distid + "','" + sid + "','" + ddlcomdty.SelectedItem.Value + "','" + ddlscheme.SelectedItem.Value + "','" + ddlgodown.SelectedItem.Value + "','" + ddlsarrival.SelectedItem.Value + "','" + ddl_lossgain.SelectedItem.Value + "','" + txtrecbags.Text + "','" + txtrqty.Text + "','" + ip + "','" + getDate_MDY(tx_lgdate.Text) + "',getdate(),'" + opid + "','" + notrans + "')";
                Session["cqty"] = txtrqty.Text;
                Session["cbags"] = txtrecbags.Text;
            }
            cmd.CommandText = str1;
            cmd.Connection = con;
            try
            {
                
                cmd.ExecuteNonQuery();
                btnsubmit.Enabled = false;
                update_bal();
                get_Data_gwn();
                Session["qry"] = "0";
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully...');</script>");
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            }
        }
    }
    protected void btnclose_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Loss_Gain.aspx");
    }
    protected void get_Data_src()
    {
        if (ddlsarrival.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Source of Arrival...');</script>");
        }
        else
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Loss_gain.trans_id, Loss_gain.District_Id, Loss_gain.Depotid, Loss_gain.Commodity_Id, Loss_gain.Scheme_Id,Loss_gain.Godown, Loss_gain.Source, Loss_gain.stock_type,Loss_gain.Bags,round(convert(decimal(18,5),Loss_gain.Quantity),5) as Quantity, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, tbl_MetaData_SCHEME.Scheme_Name, tbl_MetaData_GODOWN.Godown_Name, Source_Arrival_Type.Source_Name,CAST(DAY(Loss_gain.createddate) AS nvarchar(2))+'/'+CAST(MONTH(Loss_gain.createddate) AS nvarchar(2)) +'/'+CAST(YEAR(Loss_gain.createddate) AS nvarchar(4)) as CreatedDate FROM   dbo.Loss_gain LEFT JOIN  tbl_MetaData_STORAGE_COMMODITY ON Loss_gain.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN   dbo.tbl_MetaData_SCHEME ON Loss_gain.Scheme_Id = tbl_MetaData_SCHEME.Scheme_Id LEFT JOIN  dbo.Source_Arrival_Type ON Loss_gain.Source = Source_Arrival_Type.Source_ID LEFT JOIN dbo. tbl_MetaData_GODOWN ON Loss_gain.Depotid = tbl_MetaData_GODOWN.DepotId AND Loss_gain.Godown = tbl_MetaData_GODOWN.Godown_ID where  Loss_gain.District_Id='" + distid + "' and Loss_gain.Depotid='" + sid + "' and Loss_gain.Source='" + ddlsarrival.SelectedItem.Value + "' order by Loss_gain.Godown,Loss_gain.Commodity_Id,Loss_gain.Scheme_Id,Loss_gain.stock_type,Loss_gain.CreatedDate", con);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
        }
    }
    protected void update_bal()
    {
        string strope = "";
        string strgwn = "";
        if (ddl_lossgain.SelectedItem.Value == "L")
        {
            strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + CheckNull(Session["cqty"].ToString()) + ",5),Current_Bags=Current_Bags-" + CheckNullInt(Session["cbags"].ToString()) + " where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + ddlcomdty.SelectedItem.Value + "'and Scheme_Id='" + ddlscheme.SelectedItem.Value + "' and Godown='" + ddlgodown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
            strgwn = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + CheckNullInt(Session["cbags"].ToString()) + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + CheckNull(Session["cqty"].ToString()) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + CheckNull(Session["cqty"].ToString()) + ",5) where District_Id='" + distid + "' and Depotid='" + sid + "' and Godown='" + ddlgodown.SelectedItem.Value + "'";
        }
        else
        {
            strope = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance) +" + CheckNull(Session["cqty"].ToString()) + ",5),Current_Bags=Current_Bags+" + CheckNullInt(Session["cbags"].ToString()) + " where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + ddlcomdty.SelectedItem.Value + "'and Scheme_Id='" + ddlscheme.SelectedItem.Value + "' and Godown='" + ddlgodown.SelectedItem.Value + "' and Source='" + ddlsarrival.SelectedItem.Value + "'";
            strgwn = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags+" + CheckNullInt(Session["cbags"].ToString()) + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)+" + CheckNull(Session["cqty"].ToString()) + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)-" + CheckNull(Session["cqty"].ToString()) + ",5) where District_Id='" + distid + "' and Depotid='" + sid + "' and Godown='" + ddlgodown.SelectedItem.Value + "'";
        }
        //str1 = "update dbo.tbl_Stock_Registor set Sale_Do=Sale_Do+" + CheckNull(txtrqty.Text) + " where Commodity_Id ='" + ddlcomdty.SelectedItem.Value + "' and Scheme_Id ='" + ddlscheme.SelectedItem.Value + "' and DistrictId='" + distid + "'and DepotID='" + sid + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
        //cmd.CommandText = str1;
        //cmd.Connection = con;      
        //cmd.ExecuteNonQuery();        
        cmd.CommandText = strope;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        //        
        cmd.CommandText = strgwn;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
    }
    protected void get_Data_gwn()
    {
        if (ddlcomdty.SelectedItem.Text == "Select" || ddlscheme.SelectedItem.Text == "Select" || ddlsarrival.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Source of Arrival/Commodity/Scheme...');</script>");
        }
        else if (ddlgodown.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Godown...');</script>");
        }
        else
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Loss_gain.trans_id, Loss_gain.District_Id, Loss_gain.Depotid, Loss_gain.Commodity_Id, Loss_gain.Scheme_Id,Loss_gain.Godown, Loss_gain.Source, Loss_gain.stock_type,Loss_gain.Bags,round(convert(decimal(18,5),Loss_gain.Quantity),5) as Quantity, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, tbl_MetaData_SCHEME.Scheme_Name, tbl_MetaData_GODOWN.Godown_Name, Source_Arrival_Type.Source_Name,CAST(DAY(Loss_gain.createddate) AS nvarchar(2))+'/'+CAST(MONTH(Loss_gain.createddate) AS nvarchar(2)) +'/'+CAST(YEAR(Loss_gain.createddate) AS nvarchar(4)) as CreatedDate FROM   dbo.Loss_gain LEFT JOIN  tbl_MetaData_STORAGE_COMMODITY ON Loss_gain.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id LEFT JOIN   dbo.tbl_MetaData_SCHEME ON Loss_gain.Scheme_Id = tbl_MetaData_SCHEME.Scheme_Id LEFT JOIN  dbo.Source_Arrival_Type ON Loss_gain.Source = Source_Arrival_Type.Source_ID LEFT JOIN dbo. tbl_MetaData_GODOWN ON Loss_gain.Depotid = tbl_MetaData_GODOWN.DepotId AND Loss_gain.Godown = tbl_MetaData_GODOWN.Godown_ID where  Loss_gain.District_Id='" + distid + "' and Loss_gain.Depotid='" + sid + "' and Loss_gain.Source='" + ddlsarrival.SelectedItem.Value + "' and Loss_gain.Godown='" + ddlgodown.SelectedItem.Value + "' and Loss_gain.Commodity_Id='" + ddlcomdty.SelectedItem.Value + "' and Loss_gain.Scheme_Id='" + ddlscheme.SelectedItem.Value + "' and Loss_gain.stock_type='" + ddl_lossgain.SelectedItem.Value + "' order by Loss_gain.CreatedDate", con);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
        }
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_Data_src();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrqty.Text = "";
        txtrecbags.Text = "";
        DataTable dt = new DataTable();
        Session["qry"] = "1";
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        if (idx != -1)
        {
            ddlsarrival.Enabled = false;
            ddl_lossgain.SelectedValue = dt.Rows[idx][7].ToString();
            ddl_lossgain.Enabled = false;
            ddlcomdty.SelectedValue = dt.Rows[idx][3].ToString();
            ddlcomdty.Enabled = false;
            ddlscheme.SelectedValue = dt.Rows[idx][4].ToString();
            ddlscheme.Enabled = false;
            ddlgodown.SelectedValue = dt.Rows[idx][5].ToString();
            ddlgodown.Enabled = false;
            txtrqty.Text = dt.Rows[idx][9].ToString();
            txtrecbags.Text = dt.Rows[idx][8].ToString();            
            tx_lgdate.Text   =dt.Rows[idx][14].ToString();            
            Session["transid"] = dt.Rows[idx][0].ToString();
            get_godowncap();            
        }
    }
    protected void get_godowncap()
    {
        txtmaxcap.Text = "";
        txtcurntcap.Text = "";
        txtavalcap.Text = "";        
        if (ddlcomdty.SelectedItem.Text == "Select" || ddlscheme.SelectedItem.Text == "Select" || ddlsarrival.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Source of Arrival/Commodity/Scheme');</script>");
            ddlgodown.SelectedIndex = 0;
        }
        else if (ddlgodown.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Godown');</script>");
            ddlgodown.SelectedIndex = 0;            
        }
        else
        {
            string maxcap = "", currcap = "";
            string str1 = "SELECT convert(decimal(18,5),Godown_Capacity) as Godown_Capacity,convert(decimal(18,5),Current_Balance) as Current_Balance FROM dbo.Current_Godown_Position where District_Id='" + distid + "' and Depotid='" + sid + "' and Godown='" + ddlgodown.SelectedItem.Value + "'";
            cmd.CommandText = str1;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                maxcap = dr["Godown_Capacity"].ToString();
                currcap = dr["Current_Balance"].ToString();
            }
            dr.Close();
            con.Close();
            txtmaxcap.Text = System.Math.Round(CheckNull(maxcap), 5).ToString();
            txtcurntcap.Text = System.Math.Round(CheckNull(currcap), 5).ToString();
            txtavalcap.Text = System.Math.Round(CheckNull(maxcap) - CheckNull(currcap), 5).ToString();            
        }
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}
