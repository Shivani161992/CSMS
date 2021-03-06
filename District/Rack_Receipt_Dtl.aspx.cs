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
public partial class DistrictFood_Rack_Receipt_Dtl : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    MoveChallan mobj = null;
    Commodity_MP comdtobj = null;
    DistributionCenters distobj = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string getdatef = "";
    public string hname = "";
    chksql chk = null;
    public string transid = "";
    public int railnum;
    DataTable dt = new DataTable();
    float disqty = 0;
    float chkqty = 0;
    public string rhcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            txtdisbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtdisqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecdqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrackqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtdisbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdisbags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtdisqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdisqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecdqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrackqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrackqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrackno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrackno.Attributes.Add("onchange", "return chksqltxt(this)");

            tctchallanno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            tctchallanno.Attributes.Add("onchange", "return chksqltxt(this)");

            txttruckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttruckno.Attributes.Add("onchange", "return chksqltxt(this)");


            DaintyDate2.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate2.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate2.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate3.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");



            txtrecdqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtdisqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttruckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrackqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtrecdqty.Text);
            ctrllist.Add(txtdisbags.Text);
            ctrllist.Add(txtdisqty.Text);
            ctrllist.Add(txttruckno.Text);
            ctrllist.Add(txtrackqty.Text);
            ctrllist.Add(txtrackno.Text);
            ctrllist.Add(tctchallanno.Text);
            ctrllist.Add(txttruckno.Text);
            ctrllist.Add(DaintyDate1.Text);
            ctrllist.Add(DaintyDate2.Text);
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

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


            if (!IsPostBack)
            {
                
                txtrackddate.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                DaintyDate2.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                DaintyDate3.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

                GetDist();
                GetTransport();
                ddlrecddistrict.SelectedValue = distid;
               
                GetDCName();
                GetRecddist();
                GetDestRH();
                GetDestDist();
                dt.Columns.Add("district_code");
                dt.Columns.Add("Rack_No");
                dt.Columns.Add("IC_ID");
                dt.Columns.Add("IC_Name");
                dt.Columns.Add("Challan_No");
                dt.Columns.Add("Challan_date");
                dt.Columns.Add("Transporter_ID");
                dt.Columns.Add("Transporter_Name");
                dt.Columns.Add("Truck_No");
                dt.Columns.Add("Disp_Bags");
                dt.Columns.Add("Disp_Qty");
             

                Session["dt"] = dt;
                GetChallan();

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }

    void GetRack()
    {
        ddlrackno.Items.Clear();
        string sdist = ddldistrict.SelectedValue;

        //int month = int.Parse(DateTime.Today.Month.ToString());
        //int year = int.Parse(DateTime.Today.Year.ToString());

        //ddlrackno.Items.Insert(0, "--Select--");
        //string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + sdist + "' and Dest_District='" + distid + "'";
        //cmd.Connection = con;
        //cmd.CommandText = qreyrac;
        //con.Open();
        //dr = cmd.ExecuteReader();
        //while (dr.Read())
        //{
        //    ddlrackno.Items.Add(dr["Rack_No"].ToString());

        //}
        //dr.Close();
        //con.Close();

        string mrack =  txtrackno.Text;
        mobj = new MoveChallan(ComObj);

        string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + sdist + "'";// and Dest_District='" + distid + "'";
        DataSet ds = mobj.selectAny(qreyrac);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ddlchallan.Items.Clear();
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";
            ddlrackno.Items.Insert(0, "--Select--");
            ddlrackno.Items.Insert(1, lst);


        }
        else
        {
            ddlrackno.Items.Clear();
            ddlrackno.DataSource = ds.Tables[0];
            ddlrackno.DataTextField = "Rack_No";
            ddlrackno.DataValueField = "Rack_No";
            ddlrackno.DataBind();
            ddlrackno.Items.Insert(0, "--Select--");
            ddlrackno.Items.Insert(1, "Not Indicated");

        }


    }
    void GetRackDetails()
    {
        //ddlrackno.Items.Clear();
        string sdist = ddldistrict.SelectedValue;
        string mrack = txtrackno.Text;
        mobj = new MoveChallan(ComObj);

        string qreyrac = "select districtsmp.district_name,tbl_RackMaster.Rack_No,tbl_RackMaster.Dest_District  from dbo.tbl_RackMaster left join pds.districtsmp on tbl_RackMaster.Dest_District=districtsmp.district_code  where tbl_RackMaster.district_code='" + sdist + "' and tbl_RackMaster.Rack_No='"+mrack+"'";// and Dest_District='" + distid + "'";
        DataSet ds = mobj.selectAny(qreyrac);
        if (ds.Tables[0].Rows.Count == 0)
        {
            //ddlrackno.Items.Clear();
            //ListItem lst = new ListItem();
            //lst.Text = "Not Indicated";
            //lst.Value = "0";
            //ddlrackno.Items.Insert(0, "--Select--");
            //ddlrackno.Items.Insert(1, lst);
            //lblra.Visible = false;

        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            string dname = dr["Dest_District"].ToString();
            if (dname == distid)
            {
                lblra.Visible = false;
                //ddlrackno.SelectedValue = mrack;
                txtrecdqty.ReadOnly = false;
                tctchallanno.ReadOnly = false;
                ddltransporter.Enabled = true ;
                ddldestdistrict.Enabled = true;
                ddlissuename.Enabled = true;
                txttruckno.ReadOnly =false ;
                txtdisbags.ReadOnly = false;
                txtdisqty.ReadOnly = false;
            }
            else
            {
                lblra.Visible = true;
                lblra.Text = "Sorry This Rack No.( "+ddlrackno.SelectedValue+" )  Is for ***  "+ dr["district_name"].ToString () +" ***";
                GetRack();
                txtrecdqty.ReadOnly = true;
                tctchallanno.ReadOnly = true;
                ddltransporter.Enabled = false;
                ddldestdistrict.Enabled = false;
                ddlissuename.Enabled = false;
                txttruckno.ReadOnly = true;
                txtdisbags.ReadOnly = true;
                txtdisqty.ReadOnly = true;
            }

           
        }


    }
    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        ddldistrict.DataSource = ds.Tables[0];
        ddldistrict.DataTextField = "district_name";
        ddldistrict.DataValueField = "District_Code";

        ddldistrict.DataBind();
        ddldistrict.Items.Insert(0, "--Select--");
       }
    void GetDestDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddldestdistrict.DataSource = ds.Tables[0];

        ddldestdistrict.DataTextField = "district_name";
        ddldestdistrict.DataValueField = "District_Code";

        ddldestdistrict.DataBind();
        ddldestdistrict.Items.Insert(0, "--Select--");
    }
    void GetRecddist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddlrecddistrict.DataSource = ds.Tables[0];
        ddlrecddistrict.DataTextField = "district_name";
        ddlrecddistrict.DataValueField = "District_Code";

        ddlrecddistrict.DataBind();
        ddlrecddistrict.Items.Insert(0, "--Select--");
    }
    void GetDestRH()
    {

        mobj = new MoveChallan(ComObj);
        string qrydrh = "select RailHead_Code,RailHead_Name from dbo.tbl_Rail_Head where district_code='" + distid  + "'";
        DataSet dsdh = mobj.selectAny(qrydrh);

        ddldesrailhead.DataSource = dsdh.Tables[0];
        ddldesrailhead.DataTextField = "RailHead_Name";
        ddldesrailhead.DataValueField = "RailHead_Code";
        ddldesrailhead.DataBind();
        ddldesrailhead.Items.Insert(0, "--Select--");
    }
    void GetTransport()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid + "'and IsActive='Y'";

        DataSet ds = mobj.selectAny(qry);

        ddltransporter.DataSource = ds.Tables[0];
        ddltransporter.DataTextField = "Transporter_Name";
        ddltransporter.DataValueField = "Transporter_ID";
        ddltransporter.DataBind();
        ddltransporter.Items.Insert(0, "--Select--");

    }

    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldestdistrict.SelectedValue  + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissuename.DataSource = ds.Tables[0];
        ddlissuename.DataTextField = "DepotName";
        ddlissuename.DataValueField = "DepotId";

        ddlissuename.DataBind();
        ddlissuename.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string state = Session["State_Id"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();


        if (ddlrackno.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" || ddlissuename.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number  And Receipt Details...');</script>");
        }
        else
        {
            dt = (DataTable)Session["dt"];
           
            int cont = dt.Rows.Count;
            if (cont > 0)
            {
                int i = 0;
                while (i < cont)
                {
                    disqty = disqty + CheckNull(dt.Rows[i][10].ToString());
                    i = i + 1;
                }

            }
            if ((disqty + CheckNull(txtissqty.Text)) > CheckNull(txtrecdqty.Text))
            {
                Label2.Visible = true;
                Label2.Text = "Sorry You Can't Issue More Then Receipt Qty.";
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You Can't Issue More Then Receipt Qty');</script>");
            }
            else
            {
                Label2.Visible = false;
                float ccq = CheckNull(txtissqty.Text);
                float ddq = CheckNull(txtrecdqty.Text);

                string senddist = ddldistrict.SelectedValue;
                string recieverailhead = ddldesrailhead.SelectedValue.ToString();
                string  rackno =txtrackno.Text;
                float rckqty = CheckNull(txtrackqty.Text);
                float recdqty = CheckNull(txtrecdqty.Text);
                string rackddate ="";
                string rackrecdate = getDate_MDY(DaintyDate1.Text);
                string notrans = "N";
                if (ddlrackno.SelectedItem.Text == "Not Indicated")
                {
                    rackddate = getDate_MDY(DaintyDate2.Text);
                }
                else
                {
                    rackddate = getDate_MDY(txtrackddate.Text);

                }

                int month = int.Parse(DateTime.Today.Month.ToString());
                int year = int.Parse(DateTime.Today.Year.ToString());
                //string crdate = getDate_MDY(DateTime.Today.Date.ToString());
                string udate = "";
                string rachnoq = txtrackno.Text;
                int count = 0;
                string st = "N";
                string sdist = ddldistrict.SelectedValue;
                mobj = new MoveChallan(ComObj);
                string qrqtyeb= "select * from dbo.Rack_Receipt_Bulk where Sending_District='" + sdist + "' and Month=" + month + "and Year=" + year + "and district_code=" + distid + " and Rack_No='" + rachnoq+"'";
                DataSet dsrb = mobj.selectAny(qrqtyeb);
                 if (dsrb.Tables[0].Rows.Count==0)
                {
                   // string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string qryinsertr = "insert into dbo.Rack_Receipt_Bulk(State_Id,district_code,Sending_District,Recd_RailHead,Commodity_ID,Rack_No,Rack_Qty,Rack_RecdQty,Rack_DispDate,Rack_RecdDate,Month,Year,Created_Date,Updated_Date,IP_Address,RR_Status,OperatorID,NoTransaction)values('" + state + "','" + distid + "','" + senddist + "','" + recieverailhead + "','" + lblcmdty.Text + "','" + rackno + "'," + rckqty + "," + recdqty + ",'" + rackddate + "','" + rackrecdate + "'," + month + "," + year + ",getdate(),'" + udate + "','" + ip + "','O','" + opid + "','" + notrans + "')";

                    cmd.Connection = con;
                    cmd.CommandText = qryinsertr;
                    SqlTransaction trns;
                    con.Open();
                    trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    cmd.Transaction = trns;
                    try
                    {
                       
                        count = cmd.ExecuteNonQuery();

                        if (count == 1)
                        {

                            dt = (DataTable)Session["dt"];
                                                    
                            int countc = dt.Rows.Count;
                            if (countc > 0)
                            {
                                int i = 0;
                                while (i < countc)
                                {
                                    string mmmrackno =  txtrackno.Text;
                                    string qry = "insert into dbo.Rack_Receipt_Details(district_code,Sending_District,Sending_RH,Recd_RailHead,Rack_No,IssueCenter,Challan_No,Challan_date,Transporter_ID,Truck_No,Disp_Bags,Disp_Qty,Month,Year,Created_date,Updated_date,IsReceived,RR_Status,State_Id,OperatorID,NoTransaction)values('" + dt.Rows[i][0] + "','" + ddldistrict.SelectedValue + "','" + lblrhc.Text + "','" + recieverailhead + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][8] + "'," + dt.Rows[i][9] + "," + dt.Rows[i][10] + "," + month + "," + year + ",getDate(),'" + udate + "','" + st + "','O','" + state + "','" + opid + "','" + notrans + "')";
                                    cmd.CommandText = qry;
                                    cmd.Connection = con;
                                    cmd.Transaction = trns;
                                    cmd.ExecuteNonQuery();

                                    string qryuch = "Update dbo.RR_receipt_Depot set Challan_st='I' where district_code='" + dt.Rows[i][0] + "'and  DepotID='" + dt.Rows[i][2] + "'and TC_Number='" + dt.Rows[i][4] + "'and Rack_No='" + mmmrackno + "'";
                                    cmd.CommandText = qryuch;
                                    cmd.ExecuteNonQuery();

                                    i = i + 1;
                                }
                            }
                          
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                            btnSubmit.Enabled = false;
                        }
                        else
                        {

                        }



                        trns.Commit();

                    }
                    catch (Exception ex)
                    {
                        trns.Rollback();
                        Label1.Visible = true;
                        //Label1.Text = ex.Message;

                    }
                    finally
                    {
                        con.Close();
                        ComObj.CloseConnection();
                    }

                  

                }
                else
                {

                   

                        dt = (DataTable)Session["dt"];
                        cmd.Connection = con;
                        con.Open();
                        int countc = dt.Rows.Count;
                        if (countc > 0)
                        {
                            int i = 0;
                            while (i < countc)
                            {
                                string mmrackno = txtrackno.Text ;
                                string qry = "insert into dbo.Rack_Receipt_Details(district_code,Sending_District,Sending_RH,Recd_RailHead,Rack_No,IssueCenter,Challan_No,Challan_date,Transporter_ID,Truck_No,Disp_Bags,Disp_Qty,Month,Year,Created_date,Updated_date,IsReceived,NoTransaction)values('" + dt.Rows[i][0] + "','" + ddldistrict.SelectedValue + "','" + lblrhc.Text + "','" + Label1.Text + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][8] + "'," + dt.Rows[i][9] + "," + dt.Rows[i][10] + "," + month + "," + year + ",getdate(),'" + udate + "','" + st + "','" + notrans + "')";
                                cmd.CommandText = qry;
                                cmd.ExecuteNonQuery();
                                string qryuch = "Update dbo.RR_receipt_Depot set Challan_st='I' where district_code='" + dt.Rows[i][0] + "'and  DepotID='" + dt.Rows[i][2] + "'and TC_Number='" + dt.Rows[i][4] + "'and Rack_No='" + mmrackno + "'";
                                cmd.CommandText = qryuch;
                                cmd.ExecuteNonQuery();
                                i = i + 1;
                            }
                        }
                        con.Close();
                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                        btnSubmit.Enabled = false;
                    }
                    

                   

                }



               
            }

        }
        

    


    void GetData()
    {
        txtrackno.Text = ddlrackno.SelectedValue;
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
            txtrecdqty.Text = "";
            txtrecdist.Text = "";
            ddlrackno.Focus();
        }
        else
        {
            string rackno =  txtrackno.Text;
            string sdist = ddldistrict.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qrey = "select tbl_RackMaster.RailHead,tbl_RackMaster.Commodity_ID,tbl_RackMaster.Rack_DispDate,districtsmp.district_name,tbl_Rail_Head.RailHead_Name ,RailHead_Dest.RailHead_Name as DestRName,tbl_RackMaster.RailHead,tbl_RackMaster.Dest_RailHead from dbo.tbl_RackMaster left join dbo.tbl_Rail_Head on tbl_RackMaster.RailHead=tbl_Rail_Head.RailHead_Code left join  dbo.tbl_Rail_Head as RailHead_Dest on tbl_RackMaster.Dest_RailHead=RailHead_Dest.RailHead_Code left join pds.districtsmp on tbl_RackMaster.Dest_District=districtsmp.district_code  where tbl_RackMaster.district_code='" + sdist + "' and tbl_RackMaster.Rack_No='" + rackno+"'";
            DataSet ds = mobj.selectAny(qrey);
             if (ds.Tables[0].Rows.Count==0)
            {
                
      
            }
            else
            {
                int month = int.Parse(DateTime.Today.Month.ToString());
                int year = int.Parse(DateTime.Today.Year.ToString());
                string rachnoq =  txtrackno.Text;
                string qrqty = "select Rack_RecdQty from dbo.Rack_Receipt_Bulk where Sending_District='" + sdist + "' and Month=" + month + "and Year=" + year + "and district_code=" + distid + " and Rack_No='" + rachnoq+"'";
                DataSet dsqr = mobj.selectAny(qrqty);
                 
                if (dsqr.Tables[0].Rows.Count==0)
                {
                    DataRow drs = ds.Tables[0].Rows[0];
                    txtrecrailh.Text = drs["DestRName"].ToString();
                    txtrackddate.Text = getdate(drs["Rack_DispDate"].ToString());
                    txtrecdist.Text = drs["district_name"].ToString();
                    rhcode = drs["Dest_RailHead"].ToString();
                    Label1.Text = rhcode;
                    lblrhc.Text = drs["RailHead"].ToString();
                    lblcmdty.Text = drs["Commodity_ID"].ToString();
                    txtrecdist.ReadOnly = true;
                    txtrackddate.ReadOnly = true;
                    txtrecrailh.ReadOnly = true;
                    txtrecdqty.Focus();
                   
                }
                else
                {
                    DataRow drqr = dsqr.Tables[0].Rows[0];
                    txtrecdqty.Text = drqr["Rack_RecdQty"].ToString();
                    txtrecdqty.ReadOnly = true;

                    DataRow drs = ds.Tables[0].Rows[0];
                    txtrecrailh.Text = drs["DestRName"].ToString();
                    txtrackddate.Text = getdate(drs["Rack_DispDate"].ToString());
                    txtrecdist.Text = drs["district_name"].ToString();
                    rhcode = drs["Dest_RailHead"].ToString();
                    Label1.Text = rhcode;
                    lblrhc.Text = drs["RailHead"].ToString();
                    lblcmdty.Text = drs["Commodity_ID"].ToString();
                    txtrecdist.ReadOnly = true;
                    txtrackddate.ReadOnly = true;
                    txtrecrailh.ReadOnly = true;
                    tctchallanno.Focus();
                 
                }

               
            }
        }
    }
   
    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {
       
             GetData();
        GetRackQty();
        GetBalRQty();
        GetChallan();
       
        if (ddlrackno.SelectedItem.Text == "Not Indicated")
        {
            lblrecddist.Visible = true;
            ddlrecddistrict.Visible = true;
            lblrecdrh.Visible = true;
            ddldesrailhead.Visible = true;
            DaintyDate2.Visible = true;
            lblrecddistrict.Visible = false;
            txtrecdist.Visible = false;
            lblrecdrailhed.Visible = false;
            txtrackddate.Visible = false;
            txtrecrailh.Visible = false;
            txtrackqty.ReadOnly = false;
            lblrackno.Visible = true;
            txtrackno.Visible = true;
            txtrackno.Text = "";
            txtrackno.Focus();
            lblra.Visible = false;
        }
        else
        {
            lblrecddist.Visible = false ;
            ddlrecddistrict.Visible = false;
            lblrecdrh.Visible = false;
            ddldesrailhead.Visible = false;
            DaintyDate2.Visible = false;
            lblrecddistrict.Visible = true;
            txtrecdist.Visible = true;
            lblrecdrailhed.Visible = true;
            txtrackddate.Visible = true;
            txtrecrailh.Visible = true;
            txtrackqty.ReadOnly = true;
            lblrackno.Visible = false;
            txtrackno.Visible = false;
            txtrackno.Text = ddlrackno.SelectedValue;
        }

        GetRackDetails();
    }
   
    void GetRackQty()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
        }
        else
        {
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string rackno =  txtrackno.Text;
            string ddidt = ddldistrict.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qreyrq = "select Rack_Qty as Rack_Qty,Rack_RecdQty  from dbo.Rack_Receipt_Bulk where district_code='" + distid + "' and Rack_No='" + rackno + "'";
            DataSet ds = mobj.selectAny(qreyrq);
             if (ds.Tables[0].Rows.Count==0)
            {
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                string flag = "";
                if (flag == drs["Rack_Qty"].ToString())
                {

                    txtrackqty.Text = "0";
                    txtrackqty.ReadOnly = false;

                }
                else
                {
                    txtrackqty.Text = drs["Rack_Qty"].ToString();
                    txtrackqty.ReadOnly = true;
                    txtrecdqty.Text = drs["Rack_RecdQty"].ToString();
                }

            }

        }
    }
     void GetBalRQty()
    {
        if (ddlrackno.SelectedItem.Text == "--Select--")
        {
        }
        else
        {
            int month = int.Parse(DateTime.Today.Month.ToString());
            int year = int.Parse(DateTime.Today.Year.ToString());
            string rackno =  txtrackno.Text;
            string ddidt = ddldistrict.SelectedValue;
            mobj = new MoveChallan(ComObj);
            string qreyrq = "select Sum(Disp_Qty) as Disp_Qty from dbo.Rack_Receipt_Details where district_code='" + distid  + "' and Rack_No='" + rackno + "'";
            DataSet ds = mobj.selectAny(qreyrq);
             if (ds.Tables[0].Rows.Count==0)
            {
            }
            else
            {
                DataRow drs = ds.Tables[0].Rows[0];
                string flag = "";
                if (flag == drs["Disp_Qty"].ToString())
                {

                    txtissqty.Text = "0";
                    txtissqty.ReadOnly = true;

                }
                else
                {
                    txtissqty.Text = drs["Disp_Qty"].ToString();
                    txtissqty.ReadOnly = true;
                }

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
    protected void btnaddmore_Click(object sender, EventArgs e)
    {
        if (ddltransporter.SelectedItem.Text == "--Select--" || ddlissuename.SelectedItem.Text == "--Select--" || ddldistrict.SelectedItem.Text=="--Select--" || ddlrackno.SelectedItem.Text=="--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Rack Number/Prucurement Center/ IssueCenter/Transporter Name  ...');</script>");
            ddlrackno.Focus();
        }

        else
        {
            dt = (DataTable)Session["dt"];
            int count = dt.Rows.Count;
            if (count > 0)
            {
                int i = 0;
                while (i < count)
                {
                    chkqty = chkqty + CheckNull(dt.Rows[i][10].ToString());
                    i = i + 1;
                }
                float recdqt =CheckNull(txtrecdqty.Text);
                float cksq = CheckNull(txtissqty.Text) + chkqty;
                if (cksq > recdqt)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Sorry You Can't Issue More Then Received Quantity  ...');</script>");
                }
                else
                {
                    string icid = ddlissuename.SelectedValue;
                    string icname = ddlissuename.SelectedItem.Text;
                    string challan = tctchallanno.Text;
                    string tcdate = getDate_MDY(DaintyDate3.Text);
                    string mtransid = ddltransporter.SelectedValue;
                    string mtrannm = ddltransporter.SelectedItem.Text ;
                    string truckno = txttruckno.Text;
                    int dispbags = CheckNullInt(txtdisbags.Text);
                    float dispqty = CheckNull(txtdisqty.Text);
                    string rackno = txtrackno.Text;
                    dt.Rows.Add(distid,rackno, icid, icname, challan, tcdate, mtransid, mtrannm, truckno, dispbags, dispqty);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                    Session["dt"] = dt;


                }
            }

            else
            {

                string icid = ddlissuename.SelectedValue;
                string icname = ddlissuename.SelectedItem.Text;
                string mtrannm = ddltransporter.SelectedItem.Text;
                string challan = tctchallanno.Text;
                string tcdate = getDate_MDY(DaintyDate3.Text);
                string mtransid = ddltransporter.SelectedValue;
                string truckno = txttruckno.Text;
                int dispbags = CheckNullInt(txtdisbags.Text);
                float dispqty = CheckNull(txtdisqty.Text);
                string rackno = txtrackno.Text;
                dt.Rows.Add(distid, rackno, icid, icname, challan, tcdate, mtransid, mtrannm, truckno, dispbags, dispqty);

                GridView1.DataSource = dt;
                GridView1.DataBind();


                Session["dt"] = dt;


            }
           
           
        }
        txttruckno.Text = "";
        tctchallanno.Text = "";
        txtdisbags.Text = "";
        txtdisqty.Text = "";
        tctchallanno.Focus();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = GridView1.SelectedIndex;
        dt = (DataTable)Session["dt"];
        dt.Rows[idx].Delete();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRack();
        

    }
    protected void txtrackddate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetChallanData();
        tctchallanno.Text = ddlchallan.SelectedValue;
        
    }


    void GetChallanData()
    {
        string mrack =  txtrackno.Text;
        string challan = ddlchallan.SelectedValue;
        mobj = new MoveChallan(ComObj);

        string qrydata = "SELECT * FROM dbo.RR_receipt_Depot where district_code='" + distid + "'and Rack_No='" + mrack + "' and TC_Number='"+challan +"'";
        DataSet ds = mobj.selectAny(qrydata);
        if (ds.Tables[0].Rows.Count == 0)
        {
            


        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txttruckno.Text = dr["Truck_No"].ToString();
            ddltransporter.SelectedValue  = dr["Transporter_ID"].ToString();
            txtdisbags.Text  = dr["Disp_Bags"].ToString();
           txtdisqty.Text  = dr["Disp_Qty"].ToString();
            ddlissuename.SelectedValue  = dr["DepotID"].ToString();
           //DaintyDate3.SelectedDate.Date  = Convert.ToDateTime ( dr["TC_date"].ToString());


        }

    }
    void GetChallan()
    {
        string mrack= txtrackno.Text;
        mobj = new MoveChallan(ComObj);

        string qry = "SELECT * FROM dbo.RR_receipt_Depot where district_code='" + distid + "'and Rack_No='" + mrack + "' and Challan_st='N'";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ddlchallan.Items.Clear();
            ListItem lst = new ListItem();
            lst.Text = "Not Indicated";
            lst.Value = "0";
            ddlchallan.Items.Insert(0, "--Select--");
            ddlchallan.Items.Insert(1, lst);


        }
        else
        {
            ddlchallan.Items.Clear();
            ddlchallan.DataSource = ds.Tables[0];
            ddlchallan.DataTextField = "TC_Number";
            ddlchallan.DataValueField = "TC_Number";
            ddlchallan.DataBind();
            ddlchallan.Items.Insert(0, "--Select--");
            ddlchallan.Items.Insert(1, "Not Indicated");
        }
    }
    protected void ddldesrailhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = ddldesrailhead.SelectedValue;
        if (ddlrackno.SelectedItem.Text == "Not Indicated")
        {
            lblrhc.Text = ddldesrailhead.SelectedValue;

        }
    }
    protected void txtrackno_TextChanged(object sender, EventArgs e)
    {
        GetBalRQty();
        GetRackQty();
        GetChallan();
        mobj = new MoveChallan(ComObj);
        string sdist = ddldistrict.SelectedValue;
        string qreyrac = "select districtsmp.district_name,Rack_Receipt_Bulk.Rack_No,Rack_Receipt_Bulk.district_code  from dbo.Rack_Receipt_Bulk left join pds.districtsmp on Rack_Receipt_Bulk.district_code=districtsmp.district_code  where Rack_Receipt_Bulk.Sending_District='" + sdist + "' and Rack_Receipt_Bulk.Rack_No='" + txtrackno.Text + "'";// and Dest_District='" + distid + "'";
        DataSet ds = mobj.selectAny(qreyrac);
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblra.Visible = false;
            txtrecdqty.ReadOnly = false;
            tctchallanno.ReadOnly = false;
            ddltransporter.Enabled = true;
            ddldestdistrict.Enabled = true;
            ddlissuename.Enabled = true;
            txttruckno.ReadOnly = false;
            txtdisbags.ReadOnly = false;
            txtdisqty.ReadOnly = false;

        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            string dname = dr["district_code"].ToString();
            if (dname == distid)
            {
                lblra.Visible = false;
                txtrecdqty.ReadOnly = false;
                tctchallanno.ReadOnly = false;
                ddltransporter.Enabled = true;
                ddldestdistrict.Enabled = true;
                ddlissuename.Enabled = true;
                txttruckno.ReadOnly = false;
                txtdisbags.ReadOnly = false;
                txtdisqty.ReadOnly = false;
            }
            else
            {
                lblra.Visible = true;
                lblra.Text = "Sorry This Rack No.( " + txtrackno.Text + " )  Is for ***  " + dr["district_name"].ToString() + " ***";
                GetRack();
                txtrecdqty.ReadOnly = true;
                tctchallanno.ReadOnly = true;
                ddltransporter.Enabled = false;
                ddldestdistrict.Enabled = false;
                ddlissuename.Enabled = false;
                txttruckno.ReadOnly = true;
                txtdisbags.ReadOnly = true;
                txtdisqty.ReadOnly = true;
            }


        }
    }
    protected void ddldestdistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
}
