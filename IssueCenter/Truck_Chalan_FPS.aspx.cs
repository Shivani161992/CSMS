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

public partial class IssueCenter_Truck_Chalan_FPS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    chksql chk = null;
    SqlDataReader dr;
    Transporter tobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    DistributionCenters distobj = null;
    Districts DObj = null;
    string distid = "";
    string sid = "23";
    string issueid = "";
    string version = "";
    int i = 0;
    string transid;
    string Godown_From;
    string Stock_Source;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            version = Session["hindi"].ToString();

            txtbagno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtquant.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txt_Driver.Attributes.Add("onkeypress", "return CheckIsChar(this)");

            txtremark.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtremark.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtremark.Attributes.Add("onchange", "return chksqltxt(this)");

            txttruckno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttruckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttruckno.Attributes.Add("onchange", "return chksqltxt(this)");

            txttrukcno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttrukcno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttrukcno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbagno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbagno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtquant.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtquant.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbuiltydate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            txtbuiltydate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbuiltydate.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbagno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtquant.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttrukcno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txttrukcno.Text);
            ctrllist.Add(txtbagno.Text);
            ctrllist.Add(txtquant.Text);
            ctrllist.Add(DaintyDate1.Text);
            ctrllist.Add(txttruckno.Text);
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
                GetScheme();
                GetCommodity();
                GetTransport();
                GetGodown();
                GetDist();
                GetICname();
                GetName();
               
                txtbuiltydate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                DaintyDate1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                ddlissuecenter.Items.Insert(0, "--Select--");
                ddl_fps_name.Items.Insert(0, "--Select--");
                ddlcomdty.Items.Insert(0, "--Select--");
                ddlscheme.Items.Insert(0, "--Select--");
                ddl_block.Items.Insert(0, "--Select--");
                ddlallot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlallot_year.Items.Add(DateTime.Today.Year.ToString());
                ddlallot_year.SelectedIndex = 1;
                ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
               
                GetSource();
                GET_Table_Rows();
                Session["dt"] = dt;
                if (version == "H")
                {
                    lblbqty.Text = Resources.Hindi_New.lblbqty;
                    lbl_cur_bags.Text = Resources.Hindi_New.lbl_cur_bags;

                    lbl_Bookno.Text = Resources.Hindi_New.lbl_Bookno;
                    lblbuilty.Text = Resources.Hindi_New.lblbuilty;
                    lblbuiltydate.Text = Resources.Hindi_New.lblbuiltydate;
                    lbl_Saralno.Text = Resources.Hindi_New.lbl_Saralno;

                    btnAdd_FPS.Text = Resources.Hindi_New.btnAdd_FPS;
                    lbl_fpsname.Text = Resources.Hindi_New.lbl_fpsname;
                    lbl_Driver_Name.Text = Resources.Hindi_New.lbl_Driver_Name;
                    lbl_Block.Text = Resources.Hindi_New.lbl_Block;

                    lbl_Head_Transport.Text = Resources.Hindi_New.lbl_Head_Transport;
                    lbl_FPS_Details.Text = Resources.Hindi_New.lbl_FPS_Details;
                    lbl_Receive_Details.Text = Resources.Hindi_New.lbl_Receive_Details;
                    lbl_comm_scheme.Text = Resources.Hindi_New.lbl_comm_scheme;
                    lbl_Dispatch_Details.Text = Resources.Hindi_New.lbl_Dispatch_Details;
                    lblmonth.Text = Resources.Hindi_New.lblmonth;
                    lblyear.Text = Resources.Hindi_New.lblyear;
                    lblCommodity.Text = Resources.Hindi_New.lblCommodity;
                    lblGodownNo.Text = Resources.Hindi_New.lblGodownNo;
                    lblScheme.Text = Resources.Hindi_New.lblScheme;
                    lblQuantity.Text = Resources.Hindi_New.lblQuantity;
                    lblBagNumber.Text = Resources.Hindi_New.lblBagNumber;
                    lbltransfer.Text = Resources.Hindi_New.lbltransfer;
                    lbltransferdepot.Text = Resources.Hindi_New.lbltransferdepot;
                    lbldispsource.Text = Resources.Hindi_New.lbldispsource;
                    lblchallanNumber.Text = Resources.Hindi_New.lblChallanNumber;
                    lblTrans.Text = Resources.Hindi_New.lblTrans;
                    lblTruckNumber.Text = Resources.Hindi_New.lblTruckNumber;
                    lbldisdate.Text = Resources.Hindi_New.lbldisdate;
                    lblrecddist.Text = Resources.Hindi_New.lblrecddist;
                    lblrecdepo.Text = Resources.Hindi_New.lblrecdepo;
                    lbldistime.Text = Resources.Hindi_New.lbldistime;
                    lblNameDepot.Text = Resources.Hindi_New.lblNameDepot;
                    lblDistrictName.Text = Resources.Hindi_New.lblDistrictName;
                    btnclose.Text = Resources.Hindi_New.btnclose;
                    lblRemark.Text = Resources.Hindi_New.lblRemark;
                    btnsave.Text = Resources.Hindi_New.btnsave;
                    btnaddnew.Text = Resources.Hindi_New.btnaddnew;
                }
            }
            lnkPrintPage.Attributes.Add("onclick", "window.open('Print_Truckchalan_FPS.aspx',null,'left=0, top=0, height=620, width=580, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    public void GetCommodity()
    {
        try
        {
            cmd.CommandText = "select commodity_id,Commodity_Name_Hindi,Commodity_Name_Eng from dbo.tbl_Commodity_Hindi order by commodity_id";
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (version == "H")
                {
                    ListItem lstitem = new ListItem();
                    lstitem.Text = dr["Commodity_Name_Hindi"].ToString();
                    lstitem.Value = dr["commodity_id"].ToString();
                    ddlcomdty.Items.Add(lstitem);
                }
                else
                {
                    ListItem lstitem = new ListItem();
                    lstitem.Text = dr["Commodity_Name_Eng"].ToString();
                    lstitem.Value = dr["commodity_id"].ToString();
                    ddlcomdty.Items.Add(lstitem);
                }

            }

            dr.Close();
            con.Close();
        }
        catch (Exception)
        {
 
        }
       
    }
    public void GetSource()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
            DataSet ds = mobj.selectAny(qry);
            ddlsarrival.DataSource = ds.Tables[0];
            ddlsarrival.DataTextField = "Source_Name";
            ddlsarrival.DataValueField = "Source_ID";
            ddlsarrival.DataBind();
            ddlsarrival.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {

        }
       
       
    }
    public void GetScheme()
    {
        try 
        {
            cmd.CommandText = "select Scheme_Id,Scheme_Name from dbo.tbl_MetaData_SCHEME  where Scheme_Id in (0,1,2,3,4) and status='Y' order by Scheme_Id";
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["scheme_name"].ToString();
                lstitem.Value = dr["scheme_id"].ToString();
                ddlscheme.Items.Add(lstitem);
            }
            // ddlscheme.Items.Insert(0, "Select");
            dr.Close();
            con.Close();
        }
        catch (Exception)
        {

        }
        
    }
    public void GetGodown()
    {
        try
        {
            mobj = new MoveChallan(ComObj);
            string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid + "' order by Godown_ID";
            DataSet ds = mobj.selectAny(qry);
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {

        }
       
    }
    public void GetDist()
    {
        try
        {
            DObj = new Districts(ComObj);
            DataSet ds = DObj.selectAll(" order by district_name");
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "district_name";
            ddldistrict.DataValueField = "District_Code";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {

        }
       
    }
    public void GetDCName()
    {
        try
        {
            distobj = new DistributionCenters(ComObj);
            string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
            DataSet ds = distobj.select(ord);
            ddlissuecenter.DataSource = ds.Tables[0];
            ddlissuecenter.DataTextField = "DepotName";
            ddlissuecenter.DataValueField = "DepotId";
            ddlissuecenter.DataBind();
            ddlissuecenter.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {

        }
       
    }
    public void GetTransport()
    {
        try
        {
            tobj = new Transporter(ComObj);
            string qry = "SELECT Transporter_Name,Transporter_ID FROM dbo.Transporter_Table where Distt_ID='" + distid + "'and IsActive='Y'";
            DataSet ds = tobj.selectAny(qry);
            ddltransporter.DataSource = ds.Tables[0];
            ddltransporter.DataTextField = "Transporter_Name";
            ddltransporter.DataValueField = "Transporter_ID";
            ddltransporter.DataBind();
            ddltransporter.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {

        }

    }
    public void GetICname()
    {
        try
        {
            tobj = new Transporter(ComObj);
            string qry = "Select * from dbo.tbl_MetaData_DEPOT where DistrictId='" + "23" + distid + "' and DepotID='" + issueid + "'";

            DataSet ds = tobj.selectAny(qry);
            DataRow dr = ds.Tables[0].Rows[0];
        }
        catch (Exception)
        {

        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
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
    public void GetName()
    {
        try 
        {
            mobj = new MoveChallan(ComObj);
            string qry1dt = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
            DataSet ds1dt = mobj.selectAny(qry1dt);
            DataRow dr1dt = ds1dt.Tables[0].Rows[0];
            lbldist.Text = dr1dt["district_name"].ToString();


            mobj = new MoveChallan(ComObj);
            string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + issueid + "'";
            DataSet dsic = mobj.selectAny(qryissue);
            DataRow dric = dsic.Tables[0].Rows[0];
            lbldepo.Text = dric["DepotName"].ToString();
        }
        catch (Exception)
        {

        }
       
    }
    public void UpdateReceipt()
    {
        try
        {
            string challan = txttrukcno.Text.Trim().ToString();
            string upd = "Update dbo.tbl_Receipt_Details set Challan_Status='I' where A_dist='" + distid + "'and A_Depo='" + issueid + "'and challan_no='" + challan + "'";
            cmd.CommandText = upd;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = ex.Message;
            }
            finally
            {

                con.Close();
            }
        }
        catch (Exception)
        {

        }
        

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime my1 = Convert.ToDateTime(getDate_MDY(DaintyDate1.Text));
            string month = my1.Month.ToString();
            string year = my1.Year.ToString();
            string Chalan_no = txttrukcno.Text.Trim().ToString();
            Session["doforprint"] = Chalan_no;
            string opid = Session["OperatorId"].ToString();
            string state = Session["State_Id"].ToString();
          
            mobj1 = new MoveChallan(ComObj);
            string Bookno = txtbookno.Text.Trim().ToString();
            string saral_no = txt_Saral.Text.Trim().ToString();
            string BuiltyNo = txtBuiltyno.Text.Trim().ToString();
            string BuiltyDate = getDate_MDY(txtbuiltydate.Text);
            string dispatchdate = getDate_MDY(DaintyDate1.Text);

            string commodity_id = ddlcomdty.SelectedValue;
            string Scheme_id = ddlscheme.SelectedValue;

            Stock_Source = ddlsarrival.SelectedValue;
            string Issued_Godown = ddlgodown.SelectedValue;

            Godown_From = ddlgodown.SelectedValue;
            Session["Dispatch_godown"] = Godown_From;
            string District_From = lbldist.Text;
            string Depot_From = lbldepo.Text;

            string Issued_Dist = ddldistrict.SelectedValue;
            string Issued_Depot = ddlissuecenter.SelectedValue;
            string Issued_Block = ddl_block.SelectedValue;
            string Issued_FPS = ddl_fps_name.SelectedValue;

            int Issued_Bags = CheckNullInt(txtbagno.Text);
            float Issued_Quant = CheckNull(txtquant.Text);
            float Bal_Quant = CheckNull(txtbqty.Text);

            string Transporter = ddltransporter.SelectedValue;
            string Drivername = txt_Driver.Text.Trim().ToString();
            string mudate = "";
            string mdstatus = "N";
            string remark = txtremark.Text.ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            string notrans = "N";
            string qreychal = "select Challan_No from dbo.tbl_TruckChalan_FPS where Depot_Id='" + issueid + "' and Dist_ID='" + distid + "' and Challan_No='" + Chalan_no + "'";
            cmd = new SqlCommand(qreychal, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_TruckChalan_FPS");
            if (ds.Tables[0].Rows.Count == 0)
            {
                dt = (DataTable)Session["dt"];
                int countrw = dt.Rows.Count;
                if (countrw > 0)
                {

                    if (Bal_Quant < Issued_Quant)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Have Insufficieant Balance ....'); </script> ");
                    }
                    else
                    {
                        while (i < countrw)
                        {
                            int trnscnt = 0;
                            string docount = "";
                            string strqr = "select max(convert(int,right(Dispatch_id,len(Dispatch_id)-len(Year)-len(Dist_ID)-len(Challan_No)-3))) as rwcount  from dbo.tbl_TruckChalan_FPS where Challan_No='" + Chalan_no + "' and Dist_ID='" + distid + "'";
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            cmd.CommandText = strqr;
                            cmd.Connection = con;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                docount = dr["rwcount"].ToString();
                            }
                            dr.Close();
                            if (CheckNullInt(docount) < 0)
                            {
                                docount = "0";
                            }
                            if (docount != "")
                            {
                                trnscnt = CheckNullInt(docount);
                            }
                            trnscnt = trnscnt + 1;
                            transid = year + "-" + distid.ToString() + "-" + Chalan_no.ToString() + "-" + (trnscnt).ToString();

                            string time = (ddlhour.SelectedItem.Text.ToString() + ":" + ddlminute.SelectedItem.Text.ToString() + ":" + ddlampm.SelectedItem.Text.ToString());
                            string qry = "insert into dbo.tbl_TruckChalan_FPS(Dispatch_id,State_Id,Dist_ID,Depot_Id,Book_No,Saral_NO,Challan_No,Challan_Date,Builty_No,Builty_Date,Dispatch_Godown,Sendto_District,Sendto_Depot,sendto_Block,sendto_FPS,Commodity,Scheme,Bags,Qty_send,Truck_no,Transporter,Driver_Name,Dispatch_Time,Remarks,IsDeposit,Month,Year,Created_date,Updated_Date,IP_Address,Source,OperatorID,NoTransaction) values('" + transid + "','" + state + "','" + distid + "','" + issueid + "','" + Bookno + "','" + saral_no + "','" + Chalan_no + "','" + dispatchdate + "','" + BuiltyNo + "','" + dispatchdate + "','" + Godown_From + "','" + Issued_Dist + "','" + Issued_Depot + "'," + Issued_Block + "," + dt.Rows[i][0] + ",'" + dt.Rows[i][2] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "','" + txttruckno.Text.Trim() + "','" + Transporter + "','" + Drivername + "','" + time + "','" + remark + "','" + mdstatus + "'," + month + "," + year + ",getdate(),'" + mudate + "','" + ip + "','" + Stock_Source + "','" + opid + "','" + notrans + "')";

                            cmd = new SqlCommand(qry, con);
                            int req = cmd.ExecuteNonQuery();
                            //i = i + 1;
                            con.Close();
                            try
                            {
                                if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--" || ddlsarrival.SelectedItem.Text == "--Select--" || ddltransporter.SelectedItem.Text == "--Select--" || ddldistrict.SelectedItem.Text == "--Select--" || ddlissuecenter.SelectedItem.Text == "--Select--")
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Scheme/Dispatch Godown/Source/Transporter/Issue Center....'); </script> ");
                                }
                                else
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    float buqty = CheckNull(txtbqty.Text);
                                    string msaletype = "Other Depot";
                                    string mstate = "23";
                                    string mupdate = "";
                                    string mddate = "";
                                    string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance-" + dt.Rows[i][7] + ",Current_Bags=Current_Bags-" + dt.Rows[i][6] + " where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + dt.Rows[i][2] + "'and Scheme_Id='" + dt.Rows[i][4] + "' and Godown='" + Godown_From + "'and Source='" + Stock_Source + "'";
                                    cmd.CommandText = query;
                                    cmd.Connection = con;
                                    string qreySale = "insert into dbo.SCSC_Sale_Details(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Sale_Type,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + issueid + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][4] + "','" + msaletype + "'," + dt.Rows[i][7] + "," + month + "," + year + ",getdate(),'" + mupdate + "','" + mddate + "'" + ")";
                                    try
                                    {
                                        cmd.ExecuteNonQuery();
                                        cmd.CommandText = qreySale;
                                        int count = cmd.ExecuteNonQuery();
                                        con.Close();
                                        if (count >= 1)
                                        {
                                            string qrystock = "select Sum(Qty_send) as Qty from dbo.tbl_TruckChalan_FPS where Commodity ='" + dt.Rows[i][2] + "' and Scheme='" + dt.Rows[i][4] + "' and Dist_ID='" + distid + "'and Depot_Id='" + issueid + "'and Month=" + month + "and Year=" + year;
                                            mobj = new MoveChallan(ComObj);
                                            DataSet dsstock = mobj.selectAny(qrystock);

                                            if (dsstock.Tables[0].Rows.Count == 0)
                                            {

                                            }
                                            else
                                            {
                                                DataRow drop = dsstock.Tables[0].Rows[0];
                                                float msod = CheckNull(drop["Qty"].ToString());
                                                string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + dt.Rows[i][2] + "' and Scheme_Id='" + dt.Rows[i][4] + "' and DistrictId ='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
                                                mobj = new MoveChallan(ComObj);
                                                DataSet dsopen = mobj.selectAny(qryinsopen);

                                                if (dsopen.Tables[0].Rows.Count == 0)
                                                {

                                                    string qrysr = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + dt.Rows[i][2] + "' and Scheme_Id='" + dt.Rows[i][4] + "' and DistrictId ='" + distid + "'and DepotID='" + sid + "'and Month=" + month + "and Year=" + year;
                                                    mobj = new MoveChallan(ComObj);
                                                    DataSet dssr = mobj.selectAny(qrysr);

                                                    if (dssr.Tables[0].Rows.Count == 0)
                                                    {
                                                        string chkopenss = "Select Sum(Current_Balance) as Current_Balance   from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + dt.Rows[i][2] + "' and Scheme_Id='" + dt.Rows[i][4] + "'";
                                                        mobj = new MoveChallan(ComObj);
                                                        DataSet dsqry = mobj.selectAny(chkopenss);
                                                        if (dsqry == null)
                                                        {

                                                        }

                                                        else
                                                        {
                                                            DataRow drss = dsqry.Tables[0].Rows[0];
                                                            float sropen = CheckNull(drss["Current_Balance"].ToString());
                                                            string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][4] + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtbqty.Text) + "," + 0 + "," + 0 + "," + month + "," + year + ",'')";
                                                            cmd.CommandText = qryinsr;
                                                            if (con.State == ConnectionState.Closed)
                                                            {
                                                                con.Open();
                                                            }
                                                            cmd.ExecuteNonQuery();
                                                            con.Close();

                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    string qryinsU = "update dbo.tbl_Stock_Registor set Sale_otherg=" + msod + " where Commodity_Id ='" + dt.Rows[i][2] + "'and Scheme_Id='" + dt.Rows[i][4] + "' and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
                                                    cmd.CommandText = qryinsU;
                                                    if (con.State == ConnectionState.Closed)
                                                    {
                                                        con.Open();
                                                    }
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                        }
                                        try
                                        {
                                            //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                            //string state = Session["State_Id"].ToString();
                                            DateTime datetime = Convert.ToDateTime(getDate_MDY(DaintyDate1.Text));
                                            //string time = (ddlhour.SelectedItem.Text.ToString() + ":" + ddlminute.SelectedItem.Text.ToString() + ":" + ddlampm.SelectedItem.Text.ToString());
                                            string qryupdate = "insert into dbo.SCSC_Truck_challan_Trans_Log(State_Id,Dist_ID,Depot_Id,TO_Number,Challan_Date,Dispatch_Godown,Sendto_District,Sendto_IC,Commodity,Scheme,Bags,Qty_send,Challan_No,Truck_no,Transporter,Dispatch_Time,Remarks,Dispatch_id,Month,Year,Updated_Date,IP_Address,Source,Operation) values('" + state + "','" + distid + "','" + issueid + "','" + txt_Saral.Text.Trim() + "','" + getDate_MDY(DaintyDate1.Text) + "','" + ddlgodown.SelectedValue + "','" + ddldistrict.SelectedValue + "','" + ddlissuecenter.SelectedValue + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][4] + "'," + dt.Rows[i][6] + "," + dt.Rows[i][7] + ",'" + txttrukcno.Text.Trim() + "','" + txttruckno.Text + "','" + ddltransporter.SelectedValue + "','" + time + "','" + txtremark.Text + "','" + Issued_Godown + "'," + datetime.Month.ToString() + "," + datetime.Year.ToString() + ",getdate(),'" + ip + "','" + Stock_Source + "','I')";
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                            cmd.CommandText = qryupdate;
                                            cmd.Connection = con;
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception)
                                        {
 
                                        }
                                       
                                        UpdateReceipt();

                                    }
                                    catch (Exception ex)
                                    {
                                        Label1.Text = ex.Message;

                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                               
                            }
                            i = i + 1;
                        }
                       
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                        btnsave.Enabled = false;
                        lnkPrintPage.Visible = true;

                        txt_Driver.Text = "";
                        txt_Saral.Text = "";
                        txtBuiltyno.Text = "";
                        Gv_FPS_Details.DataSource = null;
                        Gv_FPS_Details.DataBind();
                        tr1.Visible = false;
                        pnl_Grid.Visible = false;
                        txttrukcno.Text = "";
                        txtremark.Text = "";
                        txttruckno.Text = "";
                        txtbookno.Text = "";
                        txtbqty.Text = "";
                        txtcurbags.Text = "";
                        ddlcomdty.SelectedIndex = -1;
                        ddlscheme.SelectedIndex = -1;
                        ddlissuecenter.SelectedIndex = -1;
                        ddlgodown.SelectedIndex = -1;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter FPS details');</script>");
            
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist....'); </script> ");

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("myscript", "<script language=javascript > alert('Please Try Again...');</script>");
        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();
        }

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
        get_block();
    }
    public void GetBalance()
    {
        try
        {
            string mcomdtyid = ddlcomdty.SelectedValue;
            string mscheme = ddlscheme.SelectedValue;
            string mgdown = ddlgodown.SelectedValue;
            string msource = ddlsarrival.SelectedValue;
            mobj1 = new MoveChallan(ComObj);
            string qry = "Select Round(Current_Balance,5) as Current_Balance,Current_Bags from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomdtyid + "'and Scheme_Id='" + mscheme + "' and Godown='" + mgdown + "'and Source='" + msource + "'";
            DataSet ds = mobj1.selectAny(qry);

            if (ds.Tables[0].Rows.Count == 0)
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  Comodity/Scheme....'); </script> ");
                txtbqty.Text = "0";
                txtcurbags.Text = "0";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtbqty.Text = dr["Current_Balance"].ToString();
                txtcurbags.Text = dr["Current_Bags"].ToString();
            }
        }
        catch (Exception)
        {

        }
       
    }
    protected void get_block()
    {
        try
        {
            string dist_code = ddldistrict.SelectedValue;
            ddl_block.Items.Clear();
            cmd.CommandText = "select Block_Uname,block_code from  pds.block_master where District_code=" + dist_code + " order by Block_Uname";
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["Block_Uname"].ToString();
                lstitem.Value = dr["block_code"].ToString();
                ddl_block.Items.Add(lstitem);
            }
            ddl_block.Items.Insert(0, "--Select--");
            dr.Close();
            con.Close();
        }
        catch (Exception)
        {

        }
       
    }
    protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_blkFPS();
    }
    protected void get_blkFPS()
    {
        try
        {

            string dist_code = ddldistrict.SelectedValue;
            ddl_fps_name.Items.Clear();
            string blk = ddl_block.SelectedItem.Value;
            //cmd.CommandText = "SELECT fps_Uname,fps_code FROM pds.fps_master where district_code='" + dist_code + "' and block_code='" + blk + "' and del_status='False' order by fps_Uname ";
            cmd.CommandText = "SELECT fps_Uname,fps_code FROM pds.fps_master_New where district_code='" + dist_code + "' and block_code='" + blk + "' and del_status='False' order by fps_Uname ";
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_Uname"].ToString() + "  (" + dr["fps_code"].ToString() + ")";
               // lstitem.Text = dr["fps_Uname"].ToString();
                lstitem.Value = dr["fps_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }
            ddl_fps_name.Items.Insert(0, "--Select--");
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddldispdipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Stock Issued From....'); </script> ");
        }
        else
        {
            GetBalance();
        }
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Truck_Chalan_FPS.aspx");
    }
    protected void btnAdd_FPS_Click(object sender, EventArgs e)
    {
        try
        {
            float Issued_Quant = CheckNull(txtquant.Text);
            if (ddlcomdty.SelectedItem.Text == "-Select-" || ddlscheme.SelectedItem.Text == "-Select-" || ddl_fps_name.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Commodity/Scheme');</script>");
            }
            else if (Issued_Quant == 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be Zero ....'); </script> ");
            }
            else if (ddlsarrival.SelectedItem.Text == "--Select--" || ddlgodown.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("myscript", "<script language=javascript > alert('Select Stock Issued from or Dispatch Godown...');</script>");
            }
            else if (txtbqty.Text == "" || txtcurbags.Text == "")
            {
                Page.RegisterClientScriptBlock("myscript", "<script language=javascript > alert('Balance Quantity is Less than Issued Stock of Commodity...');</script>");
            }
            else
            {
                string temp = "NNN";
                dt = (DataTable)Session["dt"];
                int row = 0;
                if (dt.Rows.Count > 0)
                {
                    while (row < dt.Rows.Count)
                    {
                        if (dt.Rows[row][0].ToString() == ddl_fps_name.SelectedItem.Value && dt.Rows[row][2].ToString() == ddlcomdty.SelectedItem.Value && dt.Rows[row][4].ToString() == ddlscheme.SelectedItem.Value)
                        {
                            temp = "YYY";
                            break;
                        }
                        row = row + 1;
                    }
                }
                if (temp == "YYY")
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Quantity to be selected FPS/Commodity/Scheme Already Issued ...');</script>");
                    Label1.Text = "Quantity to be selected FPS/Commodity/Scheme Already Issued";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (Convert.ToDecimal(txtquant.Text) > Convert.ToDecimal(txtbqty.Text) && Convert.ToInt32(txtbagno.Text) > Convert.ToInt32(txtcurbags.Text))
                    {
                        Page.RegisterClientScriptBlock("myscript", "<script language=javascript > alert('Quantity to Issue can not be greater than Balance Stock of Commodity...');</script>");
                        txtquant.Text = "";
                        txtquant.Focus();
                    }
                    
                    else
                    {
                        pnl_Grid.Visible = true;
                        tr1.Visible = true;
                        Gv_FPS_Details.Columns[1].Visible = false;
                        dt = (DataTable)Session["dt"];
                        dt.Rows.Add(ddl_fps_name.SelectedItem.Value, ddl_fps_name.SelectedItem.Text, ddlcomdty.SelectedItem.Value, ddlcomdty.SelectedItem.Text, ddlscheme.SelectedItem.Value, ddlscheme.SelectedItem.Text, txtbagno.Text, txtquant.Text);
                        Gv_FPS_Details.DataSource = dt;
                        Gv_FPS_Details.DataBind();
                        Session["dt"] = dt;
                        txtbagno.Text = "";
                        txtquant.Text = "";
                        txtbagno.Focus();
                        int rcount = 0;
                        while (rcount < dt.Rows.Count)
                        {
                            rcount = rcount + 1;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("myscript", "<script language=javascript > alert('Please Try Again...');</script>");
        }
    }
    public void GET_Table_Rows()
    {
        dt.Columns.Add("fps_code");
        dt.Columns.Add("fps_name");
        dt.Columns.Add("commodity_id");
        dt.Columns.Add("commodity_name");
        dt.Columns.Add("scheme_id");
        dt.Columns.Add("scheme_name");
        dt.Columns.Add("qtyBags");
        dt.Columns.Add("allot_qty");
        Session["dt"] = dt;
    }
}
