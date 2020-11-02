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

public partial class DistrictFood_Rack_master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    Commodity_MP comdtobj =null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    public string hname = "";
    chksql chk = null;
    public string transid = "";
    public int railnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            //txtrackno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            //txtrackno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            //txtrackno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            //txtrackno.Attributes.Add("onchange", "return chksqltxt(this)");
            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

            //txtrackno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            chk = new chksql();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            ArrayList ctrllist = new ArrayList();
            //ctrllist.Add(txtrackno.Text);
            ctrllist.Add(DaintyDate3.Text);
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
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                GetName();
                fillgrid();
                GetCommodity();
                GetDist();
                GetRailHead();
                getstates();
                GetDispatchType();
                get_scheme();
                //GetRackAllert();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    void GetDispatchType()
    {
        mobj = new MoveChallan(ComObj);
        string qryd = "SELECT * FROM DispatchType";
        DataSet dsd = mobj.selectAny(qryd);
        if (dsd.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            ddldispatchtype.DataSource = dsd.Tables[0];

            ddldispatchtype.DataTextField = "DispatchName";
            ddldispatchtype.DataValueField = "DispatchId";
            ddldispatchtype.DataBind();
        }

    }

    protected void ddldispatchtype_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl != null)
        {
            string qry = "SELECT * FROM DispatchType";

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int d = 0; d < ds.Tables[0].Rows.Count; d++)
            {
                ddl.Items[d].Attributes.Add("title", ds.Tables[0].Rows[d]["DispatchHindi"].ToString());
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qryd = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet dsd = mobj.selectAny(qryd);
        if (dsd.Tables[0].Rows.Count==0)
        {
        }
        else
        {
            DataRow drd = dsd.Tables[0].Rows[0];

            txtdistrict.Text = drd["district_name"].ToString();
            txtdistrict.ReadOnly = true;

        }

    }

    void GetRailHead()
    {
        mobj = new MoveChallan(ComObj);
        string qrygrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + distid + "'";
        DataSet dsd = mobj.selectAny(qrygrh);
        ddlsengrailhead.DataSource = dsd.Tables[0];
        ddlsengrailhead.DataTextField ="RailHead_Name";
        ddlsengrailhead.DataValueField ="RailHead_Code";
        ddlsengrailhead.DataBind();
        ddlsengrailhead.Items.Insert(0, "--Select--");
        
      

    }

    void GetDestRH()
    {

        mobj = new MoveChallan(ComObj);
        string qrydrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + ddldesdistt .SelectedValue  + "'";
        DataSet dsdh = mobj.selectAny(qrydrh);

        ddldesrailhead.DataSource = dsdh.Tables[0];
        ddldesrailhead.DataTextField = "RailHead_Name";
        ddldesrailhead.DataValueField = "RailHead_Code";
        ddldesrailhead.DataBind();
        ddldesrailhead.Items.Insert(0, "--Select--");
    }

    void GetDist()
    {
        mobj = new MoveChallan(ComObj);
        string qrydist = "select district_name,district_code  from pds.districtsmp  order by district_name ";
        DataSet dsd = mobj.selectAny(qrydist);

        ddldesdistt.DataSource = dsd.Tables[0];
        ddldesdistt.DataTextField = "district_name";
        ddldesdistt.DataValueField = "district_code";
        ddldesdistt.DataBind();
        ddldesdistt.Items.Insert(0, "--Select--");

    }

    void GetCommodity()
    {

        ddlcommodity.Items.Clear();
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");

        ddlcommodity.DataSource = ds.Tables[0];
        ddlcommodity.DataTextField = "Commodity_Name";
        ddlcommodity.DataValueField = "Commodity_Id";
        ddlcommodity.DataBind();
        ddlcommodity.Items.Insert(0, "--Select--");


    }

    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select  tbl_RackMaster.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name ,districtsmp.district_Name,tbl_Rail_Head.RailHead_Name ,RailHead_Dest.RailHead_Name as DestRName from dbo.tbl_RackMaster left join pds.districtsmp on tbl_RackMaster.Dest_District=districtsmp.district_code left join dbo.tbl_MetaData_STORAGE_COMMODITY  on tbl_RackMaster.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID  left join dbo.tbl_Rail_Head on tbl_RackMaster.Dest_RailHead=tbl_Rail_Head.RailHead_Code left join  dbo.tbl_Rail_Head as RailHead_Dest on tbl_RackMaster.RailHead=RailHead_Dest.RailHead_Code   where tbl_RackMaster.district_code='" + distid + "'";
        DataSet ds = mobj.selectAny(qrey);
         if (ds.Tables[0].Rows.Count==0)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Rack_DispDate"));
           

            getdatef = getdate(griddate);



            Label lbl = (Label)e.Row.FindControl("lbldisdate");
            lbl.Text = getdatef;
                   


        }



    }

    //public void FooterPagerClick(object sender, CommandEventArgs e)
    //{
    //    if (GridView1.PageCount == 0)
    //    {
    //    }
    //    else
    //    {
    //        //Used by external paging
    //        string arg;
    //        arg = e.CommandArgument.ToString();

    //        switch (arg)
    //        {
    //            case "next":
    //                //The next Button was Clicked
    //                if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
    //                {
    //                    GridView1.PageIndex += 1;
    //                }

    //                break;

    //            case "prev":
    //                //The prev button was clicked
    //                if ((GridView1.PageIndex > 0))
    //                {
    //                    GridView1.PageIndex -= 1;
    //                }

    //                break;

    //            case "last":
    //                //The Last Page button was clicked
    //                GridView1.PageIndex = (GridView1.PageCount - 1);
    //                break;

    //            default:
    //                //The First Page button was clicked
    //                GridView1.PageIndex = Convert.ToInt32(arg);
    //                break;
    //        }
    //        fillgrid();
    //    }
    //}

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridView1.SelectedRow.BackColor = System.Drawing.Color.Wheat;
        //txtrailhead.BackColor = System.Drawing.Color.Wheat;
        //hname = GridView1.SelectedRow.Cells[1].Text.Trim();
        //txtrailhead.Text = GridView1.SelectedRow.Cells[2].Text.Trim();
        //txtrailhead.Focus();
        //btnadd.Visible = false;
        //btnupdate.Visible = true;





    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }

    //void GetRackAllert()
    //{
    //    string qryr = "select Rack_Receipt_Bulk.Rack_No,Rack_Receipt_Bulk.district_code,districtsmp.district_name,Recd_RailHead,tbl_Rail_Head.RailHead_Name from Rack_Receipt_Bulk left join pds.districtsmp on districtsmp.district_code=Rack_Receipt_Bulk.district_code left join dbo.tbl_Rail_Head on Rack_Receipt_Bulk.Recd_RailHead=tbl_Rail_Head.RailHead_Code  where Sending_District='" + distid + "' and  Rack_No not in(select Rack_No from tbl_RackMaster where district_code='" + distid + "')";
    //    mobj = new MoveChallan(ComObj);
    //    DataSet dsrac = mobj.selectAny(qryr);
    //    if (dsrac == null)
    //    {
    //        ddlrackno.Visible = false; ;
    //        lblrackno.Visible =false ;
    //        lbltctrackno.Visible = true;
    //        txtrackno.Visible = true;
    //    }
    //    else
    //    {
    //        if (dsrac.Tables[0].Rows.Count == 0)
    //        {
    //            //Label2.Visible = false;

    //        }
    //        else
    //        {
               
    //            ddlrackno.Visible = true;
    //            lblrackno.Visible = true;
    //            lbltctrackno.Visible = false;
    //            txtrackno.Visible = false;
    //            ddlrackno.DataSource = dsrac;
    //            ddlrackno.DataTextField = "Rack_No";
    //            ddlrackno.DataValueField = "Rack_No";
    //            ddlrackno.DataBind();
    //            ddlrackno.Items.Insert(0, "--Select--");
    //            ddlrackno.Items.Insert(1, "Not Indicated");

    //        }
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ddlsengrailhead.SelectedItem.Text == "--Select--")
        {
            ddlsengrailhead.Focus();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Sending Rail Head.....'); </script> ");
            return;
        }

        if (ddldesdistt.SelectedItem.Text == "--Select--")
        {
            ddldesdistt.Focus();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Destination District.....'); </script> ");
            return;
        }

        if (ddlcommodity.SelectedItem.Text == "--Select--")
        {
            ddlcommodity.Focus();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Commodity.....'); </script> ");
            return;
        }

        if (ddl_scheme.SelectedItem.Text == "--Select--")
        {
            ddl_scheme.Focus();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Scheme.....'); </script> ");
            return;
        }
        if (ddldesrailhead.Visible == false && txtrailhead.Text == "")
        {
            txtrailhead.Focus();
            txtrailhead.BackColor = System.Drawing.Color.Red;
          Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Insert Sending Rail Head Name.....'); </script> ");
          return;         
        }
        if (ddldesrailhead.Visible == true && ddldesrailhead.SelectedItem.Text == "--Select--")
        {
            ddldesrailhead.Focus();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Sending Rail Head Name.....'); </script> ");
            return;
        }

        else
        {
            string sendist = txtdistrict.Text;
            string recdist = ddldesdistt.SelectedItem.Text;
            string senddate = Convert.ToString(DaintyDate3.Text);

            string racknumber = sendist + "-" + recdist + "-" + senddate;

            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());

            mobj = new MoveChallan(ComObj);
            string qrey = "select max(Trans_ID) as Trans_ID  from dbo.tbl_RackMaster where district_code='" + distid + "' and Month =" + month;
            DataSet ds = mobj.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            transid = dr["Trans_ID"].ToString();
            string mmonth = month.ToString();

            if (transid == "")
            {
                transid = distid + mmonth + "001";
            }
            else
            {
                railnum = Convert.ToInt32(transid);
                railnum = railnum + 1;
                transid = railnum.ToString();
            }

            mobj = new MoveChallan(ComObj);
            string rackchk = racknumber;

            //string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + distid + "' and Rack_No='" + rackchk + "'";
            //DataSet dsrac = mobj.selectAny(qreyrac);

            //if (dsrac.Tables[0].Rows.Count == 0)
            //{
                string mracno = racknumber;
                string msendrh = ddlsengrailhead.SelectedValue;
                string mdesrh = "";
                if (ddldesrailhead.Visible == false)
                {
                    mdesrh = txtrailhead.Text.Trim();
                }
                else
                {
                     mdesrh = ddldesrailhead.SelectedValue;
                }
               
                string mddistt = ddldesdistt.SelectedValue;
                string crdate = DateTime.Today.Date.ToString();
                string udate = "";
                string mdisdate = getDate_MDY(DaintyDate3.Text);
                string mcommdty = ddlcommodity.SelectedValue;

            string state = "";
                if (ddlStates.Visible == true)
                {
                    state = ddlStates.SelectedValue;
                }
                else
                {
                    state = Session["State_Id"].ToString();
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string opid = Session["OperatorIDDM"].ToString();

                string scheme = ddl_scheme.SelectedValue;
                string dispatchtype = ddldispatchtype.SelectedValue;

                string st = "N";
                string qryinsert = "Insert into  dbo.tbl_RackMaster(State_Id,district_code,Rack_No,RailHead,Dest_District,Dest_RailHead,Rack_DispDate,Commodity_ID,Trans_ID,Month,Year,Created_Date,Updated_Date,IP_Address,IsSend,OperatorID,Scheme_Id,DispatchID)values('" + state + "','" + distid + "','" + mracno + "','" + msendrh + "','" + mddistt + "','" + mdesrh + "','" + mdisdate + "','" + mcommdty + "','" + transid + "'," + month + "," + year + ",getdate(),'" + udate + "','" + ip + "','" + st + "','" + opid + "','" + scheme + "','" + dispatchtype + "')";
                cmd.Connection = con;
                cmd.CommandText = qryinsert;
                SqlTransaction trns;
                con.Open();
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;
                try
                {
                    cmd.ExecuteNonQuery();
                    trns.Commit();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved, Rack Number Is -- "+racknumber+"'); </script> ");
                    btnSubmit.Enabled = false;
                    fillgrid();
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
                }

            //}
            //else
            //{
            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rack Number Already Exist.....'); </script> ");

            //}

        }
     
    }
    //void GetRackDetails()

    //{
       
    //    string qryr = "select Rack_Receipt_Bulk.Rack_No,Rack_Receipt_Bulk.district_code,districtsmp.district_name,Recd_RailHead,tbl_Rail_Head.RailHead_Name from Rack_Receipt_Bulk left join pds.districtsmp on districtsmp.district_code=Rack_Receipt_Bulk.district_code left join dbo.tbl_Rail_Head on Rack_Receipt_Bulk.Recd_RailHead=tbl_Rail_Head.RailHead_Code  where Sending_District='" + distid + "' and Rack_No='" + txtrackno.Text + "'";
    //    mobj = new MoveChallan(ComObj);
    //    DataSet dsrac = mobj.selectAny(qryr);
    //    if (dsrac == null)
    //    {
    //    }
    //    else
    //    {
    //        if (dsrac.Tables[0].Rows.Count == 0)
    //        {
    //            lbltctrackno.Visible = true;
    //            txtrackno.Visible = true;
    //            ddldesrailhead.Enabled = true;
    //            ddldesdistt.Enabled = true;
    //            GetDist();
    //            GetDestRH();

    //        }
    //        else
    //        {
    //            lbltctrackno.Visible = false ;
    //            txtrackno.Visible =false ;
    //            DataRow drrack = dsrac.Tables[0].Rows[0];
    //            ddldesdistt.SelectedValue = drrack["district_code"].ToString();
    //            ddldesdistt.Enabled = false;

    //            mobj = new MoveChallan(ComObj);
    //            string qrydrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + drrack["district_code"].ToString() + "'";
    //            DataSet dsdh = mobj.selectAny(qrydrh);

    //            ddldesrailhead.DataSource = dsdh.Tables[0];
    //            ddldesrailhead.DataTextField = "RailHead_Name";
    //            ddldesrailhead.DataValueField = "RailHead_Code";
    //            ddldesrailhead.DataBind();
    //            ddldesrailhead.Items.Insert(0, "--Select--");

    //            ddldesrailhead.SelectedValue = drrack["Recd_RailHead"].ToString();
    //            ddldesrailhead.Enabled = false;


    //        }
    //    }
    //}
    protected void ddldesdistt_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDestRH();
    }
    protected void ddlsengrailhead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    //protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlrackno.SelectedItem.Text == "Not Indicated" || ddlrackno.SelectedItem.Text == "--Select--")
    //    {
    //        txtrackno.Text = "";
    //        txtrackno.Focus();
    //    }
    //    else
    //    {
    //        txtrackno.Text = ddlrackno.SelectedValue;
    //    }
    //    GetRackDetails();
    //}
    //protected void txtrackno_TextChanged(object sender, EventArgs e)
    //{
    //    GetRackDetails();
    //}

    void getstates()
    {
        mobj = new MoveChallan(ComObj);
        string qrygrh = "SELECT State_Code ,State_Name  FROM State_Master";
        DataSet dsd = mobj.selectAny(qrygrh);
        ddlStates.DataSource = dsd.Tables[0];
        ddlStates.DataTextField = "State_Name";
        ddlStates.DataValueField = "State_Code";
        ddlStates.DataBind();
        ddlStates.Items.Insert(0, "--Select--");
    }
    protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStates.SelectedItem.Text != "Madhya Pradesh")
        {
            ddldesdistt.DataSource = "";
            ddldesdistt.DataBind();

            mobj = new MoveChallan(ComObj);
            string qrydist = "SELECT district_code,district_name FROM OtherState_DistrictCode where State_Id = '" + ddlStates.SelectedValue + "'";
            DataSet dsd = mobj.selectAny(qrydist);

            ddldesdistt.DataSource = dsd.Tables[0];
            ddldesdistt.DataTextField = "district_name";
            ddldesdistt.DataValueField = "district_code";
            ddldesdistt.DataBind();
            ddldesdistt.Items.Insert(0, "--Select--");

            lblrailhead.Visible = true;
            txtrailhead.Visible = true;

            lblsenddist.Visible = false;
            ddldesrailhead.Visible = false;
        }
        else
        {
            ddldesdistt.DataSource = "";
            ddldesdistt.DataBind();

            mobj = new MoveChallan(ComObj);
            string qrydist = "select district_name,district_code  from pds.districtsmp  order by district_name ";
            DataSet dsd = mobj.selectAny(qrydist);

            ddldesdistt.DataSource = dsd.Tables[0];
            ddldesdistt.DataTextField = "district_name";
            ddldesdistt.DataValueField = "district_code";
            ddldesdistt.DataBind();
            ddldesdistt.Items.Insert(0, "--Select--");

            lblrailhead.Visible = false;
            txtrailhead.Visible = false;

            lblsenddist.Visible = true;
            ddldesrailhead.Visible = true;

        }    
    }

    protected void ddldispatchtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldispatchtype.SelectedItem.Text == "Own District")
        {
            lblstate.Visible = false;
            ddlStates.Visible = false;

            lblsenddist.Visible = true;
            ddldesrailhead.Visible = true;

            ddldesdistt.DataSource = "";
            ddldesdistt.DataBind();

            txtrailhead.Visible = false;

            lblrailhead.Visible = false;

            GetDist();
        }

        else
        {
            lblstate.Visible = true;
            ddlStates.Visible = true;

            lblsenddist.Visible = false;
            ddldesrailhead.Visible = false;

            txtrailhead.Visible = true;

            lblrailhead.Visible = true;
        }
        
    }

    protected void get_scheme()
    {
        string query = "select * from dbo.tbl_MetaData_SCHEME where status='Y' order by displayorder";
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdqry = new SqlCommand(query, con);

        SqlDataAdapter da = new SqlDataAdapter(cmdqry);
        DataSet dsqry = new DataSet();
        da.Fill(dsqry);

        ddl_scheme.DataSource = dsqry.Tables[0];
        ddl_scheme.DataTextField = "scheme_name";
        ddl_scheme.DataValueField = "scheme_id";
        ddl_scheme.DataBind();
        ddl_scheme.Items.Insert(0, "--Select--");
           
    }

}
