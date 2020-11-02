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
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Transporter tobj = null;
    Transporter tobj1 = null;
    MoveChallan mobj1 = null;
    DistributionCenters distobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public string sid = "";
    public static string tid = "";
    public string tname = "";
    public string gatepass = "";
    public int  getnum ;
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["dist_id"] != null)
        {
            sid = Session["dist_id"].ToString();
             //string dbname = "Warehouse";
           txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
           txtmobile.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
           txtrate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

           txtqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
           txtqty.Attributes.Add("onchange", "return chksqltxt(this)");

           txtmobile.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
           txtmobile.Attributes.Add("onchange", "return chksqltxt(this)");

           txtrate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
           txtrate.Attributes.Add("onchange", "return chksqltxt(this)");

           txttname.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
           txttname.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
           txttname.Attributes.Add("onchange", "return chksqltxt(this)");

           txttadd.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
           txttadd.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
           txttadd.Attributes.Add("onchange", "return chksqltxt(this)");

           DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
           DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
           DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");


           txtmobile.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           txtrate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


            distobj = new DistributionCenters(ComObj);
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtqty.Text);
            ctrllist.Add(txtmobile.Text);
            ctrllist.Add(txtrate.Text);
            ctrllist.Add(txttname.Text);
            ctrllist.Add(txttadd.Text);
            ctrllist.Add(DaintyDate1.Text);
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
                Fillgrid();
                GetTransportType();
                GetDistance();
                get_block();
                lbl_block.Visible = false;
                ddlBlock.Visible = false;
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
        string query = "select Transporter_Table.*,Lead_Distance.Lead_Name as Lead_Name,m_Transport_Type.Transport_Type as T_Type  from dbo.Transporter_Table left join dbo.m_Transport_Type on Transporter_Table.Transport_ID=m_Transport_Type.Transport_ID left join dbo.Lead_Distance on Transporter_Table.Lead=Lead_Distance.Lead_ID where Transporter_Table.Distt_ID='" + sid + "' and Valid_Upto is not null ";
        DataSet ds = tobj.selectAny(query);
        if (ds == null)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            Button1.Visible = false;
            GridView1.Columns[1].Visible = false;
        }


    }

    void GetTransportType()
    {


        mobj1 = new MoveChallan(ComObj);
        string type = "Select * from dbo.m_Transport_Type order by Transport_ID";
        DataSet ds = mobj1.selectAny(type);
        ddltranstype.DataSource = ds.Tables[0];
        ddltranstype.DataTextField = "Transport_Type";
        ddltranstype.DataValueField = "Transport_ID";
        ddltranstype.DataBind();
        ddltranstype.Items.Insert(0, "--Select--");

    }
    void GetDistance()
    {


        mobj1 = new MoveChallan(ComObj);
        string lead = "Select * from dbo.Lead_Distance";
        DataSet ds = mobj1.selectAny(lead);

        ddllead.DataSource = ds.Tables[0];
        ddllead.DataTextField = "Lead_Name";
        ddllead.DataValueField = "Lead_ID";
        ddllead.DataBind();
        ddllead.Items.Insert(0, "--Select--");

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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (DaintyDate1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date'); </script> ");
        }
        else
        {
            string state = Session["State_Id"].ToString();

            ddltranstype.SelectedItem.Text = "0";   // Change here because this is visible false at runtime and default value goes to 0
            ddllead.SelectedItem.Text = "0";

            if (ddltranstype.SelectedItem.Text == "--Select--" || ddllead.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Transporter Type'); </script> ");
            }
            else
            {
                //tobj1 = new Transporter(ComObj);
                //string queryt = "select Transporter_Name from dbo.Transporter_Table where Distt_ID='" + sid + "'and Transporter_Name='" + txttname.Text + "'";
                //DataSet dst = tobj1.selectAny(queryt);
                //if (dst.Tables[0].Rows.Count == 0)
                //{


                mobj1 = new MoveChallan(ComObj);
                string qrey = "select isnull(max(Transporter_ID),0) as Transporter_ID  from dbo.Transporter_Table where  Distt_ID='" + sid + "' and LEN(Transporter_ID)<8 ";
                DataSet ds = mobj1.selectAny(qrey);
                DataRow dr = ds.Tables[0].Rows[0];
                gatepass = dr["Transporter_ID"].ToString();
                if (gatepass == "")
                {
                    gatepass = state + sid + "01";
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
                //string mttype = ddltranstype.SelectedValue;  // if necessary then do uncomment and comment below string (anurag 28-10-2013)
                //string mlead = ddllead.SelectedValue;        // if necessary then do uncomment and comment below string (anurag 28-10-2013)

                string mttype = ddltranstype.SelectedValue.ToString();    // Change after edit, previously dropdown of Transporter type value inserted (anurag 28-10-2013)
                string mlead = "0";          // Change after edit, previously dropdown of Lead type value inserted (anurag 28-10-2013)


                float qty = CheckNull(txtqty.Text);
                float rate = CheckNull(txtrate.Text);
                string isactive = "Y";
                string validuoto = getDate_MDY(DaintyDate1.Text);
                string block = ddlBlock.SelectedValue.ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string opid = Session["OperatorIDDM"].ToString();

                string qry = "insert into dbo.Transporter_Table (State_Id,Distt_ID,Transporter_ID,Transporter_Name,Address,MobileNo,Transport_ID,Lead,Quantity,Rate,Valid_Upto,Created_Date,IsActive,fps_Block,IP,OperatorID) values('" + state + "','" + sid + "','" + gatepass + "','" + mtname + "','" + madd + "','" + mobile + "','" + mttype + "','" + mlead + "'," + qty + "," + rate + ",'" + validuoto + "',getdate(),'" + isactive + "','" + block + "','" + ip + "','" + opid + "')";
                cmd.CommandText = qry;
                cmd.Connection = con;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlTransaction trns;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;
                try
                {
                    cmd.ExecuteNonQuery();
                    trns.Commit();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted  successfully...'); </script> ");
                    btnadd.Enabled = false;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    Label1.Visible = true;
                    Label1.Text = ex.Message;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    ComObj.CloseConnection();
                }


                Fillgrid();

            }
            //else
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transporter Already Exist...'); </script> ");
            //    Panel1.Visible = false;

            //}
        }
    }

    
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
       
  
    }
    public string getdateddmmyyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

                string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Valid_Upto"));
                if (griddate != "" || griddate != string.Empty)
                {
                    getdatef = getdateddmmyyy(griddate);
                    Label lbl = (Label)e.Row.FindControl("lblChallan");
                    lbl.Text = getdatef;
                }
                else
                {
                    Label lbl = (Label)e.Row.FindControl("lblChallan");
                    lbl.Text = "";
                }


                //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
                //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

                //grdTotal = grdTotal + rowTotal;
                //grdTotalQty = grdTotalQty + rowTotalQty;

            }


        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('"+ ex.Message.ToString() +"'); </script> ");
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getdateg(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd-MM-yyyy");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.Columns[1].Visible = true;
        Label2.Text = "Update Existing Transporter";
        GridView1.SelectedRow.BackColor = System.Drawing.Color.CadetBlue;
        //GridView1.SelectedRow.BackColor = System.Drawing.Color.CadetBlue ;
        ddltranstype.SelectedValue = GridView1.DataKeys[GridView1.SelectedIndex].Values["Transport_ID"].ToString();
        txttname.BackColor = System.Drawing.Color.Wheat;
        tname=GridView1.SelectedRow.Cells [3].Text.Trim();
        tid = GridView1.SelectedRow.Cells[1].Text;
        
        txttadd.Text = "";
        txtmobile.Text = "";
        txttname.Text ="";
        //ddllead.SelectedValue = GridView1.SelectedRow.Cells[4].Text.Trim();
        txtqty.Text= GridView1.SelectedRow.Cells[5].Text.Trim ();

        if (GridView1.SelectedRow.Cells[6].Text.Trim() == "&nbsp;")
        {
            txtrate.Text = "0";
        }
        else
        {
            txtrate.Text = GridView1.SelectedRow.Cells[6].Text.Trim();
        }
        txtmobile.Text = GridView1.SelectedRow.Cells[7].Text.Trim ();
        txttname.Text = tname;

        string gettrans = "Select * from dbo.Transporter_Table where Distt_ID='" + sid+"' and Transporter_ID='"+tid +"'";

        mobj1 = new MoveChallan(ComObj);
        DataSet ds = mobj1.selectAny(gettrans);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
           // GetDistance();
            DataRow dr = ds.Tables[0].Rows[0];

            //ddltranstype.SelectedValue = dr["Transport_ID"].ToString(); 

            if (dr["Lead"].ToString() != "")
            {
                ddllead.SelectedValue = dr["Lead"].ToString();
            }
            else
            {
                ddllead.SelectedIndex = 0;
            }
            txttadd.Text = dr["Address"].ToString();
            if (dr["Valid_Upto"].ToString() != "" || dr["Valid_Upto"].ToString() != string.Empty)
            {
                DaintyDate1.Text = getdateg(dr["Valid_Upto"].ToString());
            }
            else
            {
                DaintyDate1.Text = "";
            }
           
        }
        



        //txttadd.Text = tid;
        btnadd.Visible = false;
        btnclose.Visible = true;
        Button1.Visible = true;
        //txttadd.Visible = true;
        //lbltadd.Visible = true;
        Panel1.Visible = true;
        GridView1.Columns[1].Visible = false;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        if ((Convert.ToInt16(ddltranstype.SelectedValue) != 7) )
        {

            GridView1.Columns[1].Visible = true;

            //ddltranstype.SelectedItem.Text = "0";   // Change here because this is visible false at runtime and default value goes to 0
            ddllead.SelectedItem.Text = "0";

            if (ddltranstype.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Transporter Type'); </script> ");
            }
            else
            {


                string madd = txttadd.Text;
                string mobile = txtmobile.Text;
                string mtname = txttname.Text;

                string mttype = tid;    // Change after edit, previously dropdown of Transporter type value inserted (anurag 28-10-2013)
                string mlead = "0";

                //string mttype = ddltranstype.SelectedValue;
                //string mlead = ddllead.SelectedValue;
                float qty = CheckNull(txtqty.Text);
                float rate = CheckNull(txtrate.Text);
                string validity = getDate_MDY(DaintyDate1.Text);
                string qry = "Update  dbo.Transporter_Table set fps_Block='" + ddlBlock.SelectedValue.ToString() + "', Transporter_Name='" + mtname + "',Quantity=" + qty + ",Rate= " + rate + ",MobileNo='" + mobile + " ',Transport_ID='" + mttype + "',Lead='" + mlead + "',Address='" + madd + "',Valid_Upto='" + validity + "'  where Distt_ID='" + sid + "'and Transporter_ID='" + GridView1.SelectedRow.Cells[1].Text + "'";

                cmd.CommandText = qry;
                cmd.Connection = con;



                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated successfully...'); </script> ");

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
                Panel1.Visible = false;
            }
            GridView1.Columns[1].Visible = false;

        }
        else {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please update transporter Details in Dwar praday Trasporter link...'); </script> ");
        
        }
    }
    


    protected void btnaddnew_Click1(object sender, EventArgs e)
    {
        Label2.Text = "Add New Transporter";
        Panel1.Visible = true;
        btnadd.Visible = true;
        btnclose.Visible = true;
        txttadd.Visible = true;
        lbltadd.Visible = true;
        txttname.Text = "";
        txttname.Text = "";

        ddltranstype.Focus();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/mpscsc_transporter.aspx");
    }
    protected void btnCloseh_Click(object sender, EventArgs e)
    {

        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void ddltranstype_SelectedIndexChanged(object sender, EventArgs e)
    {
   
    }
    protected void get_block()
    {
        try
        {
            string dist = sid;
            ddlBlock.Items.Clear();

            string qryblock = "select * from  pds.block_master where District_code=" + dist + " order by Block_name";


            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }


            SqlDataAdapter da = new SqlDataAdapter(qryblock, con_opdms);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBlock.DataSource = ds.Tables[0];
                    ddlBlock.DataTextField = "Block_Uname";
                    ddlBlock.DataValueField = "block_code";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, "--Select--");

                }
            }


            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }
        }
        catch (Exception)
        {

        }
    }
}
