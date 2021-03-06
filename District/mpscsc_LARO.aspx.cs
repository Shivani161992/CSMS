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
public partial class mpscsc_LARO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    chksql chk = null;
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    LARO obj = null;
    LARO objt = null;
    LARO objo = null;
    public string distid = "";
    string roqty = null;
    MoveChallan mobj = null;
    MoveChallan mobjro = null;
    public string getdatef = "";
    public string gateno = "";
    public string amonth = "";
    public string ayear = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();


            txtqtysend.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");


           
            txtqtysend.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtqtysend.Attributes.Add("onchange", "return chksqltxt(this)");

            //txtsendqty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtnobags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtnobags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtchallan.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtchallan.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtchallan.Attributes.Add("onchange", "return chksqltxt(this)");

            txtvehno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtvehno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtvehno.Attributes.Add("onchange", "return chksqltxt(this)");

            challandate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            challandate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            challandate.Attributes.Add("onchange", "return chksqltxt(this)");



            txtchallan.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtqtysend.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtnobags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtrodate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtroqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbalqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtmoisture.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtcomdty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtscheme.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            HyperLink1.Attributes.Add("onclick", "window.open('Print_TruckChallan.aspx',null,'left=200, top=10, height=620, width=580, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
           

            if (Session["dc_id"] != null)
            {
                //Session.Abandon();
                //Session.RemoveAll();
            }
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtqtysend.Text);
            ctrllist.Add(txtnobags.Text);
            ctrllist.Add(txtchallan.Text);
            ctrllist.Add(txtvehno.Text);
            ctrllist.Add(challandate.Text);

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
                challandate.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
                GetRO();
                Transport();
                //GetGunny();
                GetDist();
                GetCategory();
                GetFCIdist();
                GetDCName();
                ddlallot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlallot_year.Items.Add(DateTime.Today.Year.ToString());
                ddlallot_year.SelectedIndex = 1;
                ddlalotmm.SelectedIndex = DateTime.Today.Month - 1;
                //GetdepotType();

            }
            //GetDetails();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    void GetRO()
    {       
        ddlrono.Items.Insert(0, "--Select--");
        string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'and  Balance_Qty >0";
        cmd.Connection = con;
        cmd.CommandText = qry;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrono.Items.Add(dr["RO_No"].ToString());



        }
        dr.Close();
        con.Close();

    }
    void GetCategory()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        DataSet ds = mobj.selectAny(qry);
        
        ddlcategory.DataSource = ds.Tables[0];
        ddlcategory.DataTextField = "Category_Name";
        ddlcategory.DataValueField = "Category_Id";
        ddlcategory.DataBind();
       
    }

    void GetData()
    {
        if (ddlrono.SelectedItem.Text != "--Select--")
        {
            obj = new LARO(ComObj);
            string qryall = "SELECT RO_of_FCI.Commodity , RO_of_FCI.Distt_Id,RO_of_FCI.RO_Validity, RO_of_FCI.RO_No, RO_of_FCI.RO_date, round(convert(decimal(18,5),RO_of_FCI.RO_qty),5) as RO_qty ,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date,round(convert(decimal(18,5),RO_of_FCI.Balance_Qty),5) as Balance_Qty,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,dbo.tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  From dbo.RO_of_FCI Left JOIN dbo.tbl_MetaData_STORAGE_COMMODITY  ON RO_of_FCI.Commodity = dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=dbo.tbl_MetaData_SCHEME.Scheme_id  where RO_of_FCI.RO_No='" + ddlrono.SelectedItem + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
            DataRow dr = ds.Tables[0].Rows[0];

            string rdate = dr["RO_date"].ToString();
            string rodate = getdate(rdate);
            txtrodate.Text = rodate;
            txtrodate.ReadOnly = true;
           // txtrodate.BackColor = System.Drawing.Color.Wheat;

            roqty = dr["RO_qty"].ToString();
            txtroqty.Text = System.Math.Round (CheckNull(dr["RO_qty"].ToString()),5).ToString();
           
            txtroqty.ReadOnly = true;
            

            txtcomdty.Text = dr["Commodity_Name"].ToString();
            txtcomdty.ReadOnly = true;
            //txtcomdty.BackColor = System.Drawing.Color.Wheat;

            txtscheme.Text = dr["Scheme_Name"].ToString();
            lblscheme.Text = dr["Scheme"].ToString();
            lblcomdty.Text = dr["Commodity"].ToString();
            txtscheme.ReadOnly = true;
            //txtscheme.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.Text = System.Math.Round(CheckNull (dr["Balance_Qty"].ToString()), 5).ToString();
            txtbalqty.ReadOnly = true;
            //txtbalqty.BackColor = System.Drawing.Color.Wheat;
            lblmonth.Text = dr["Allot_month"].ToString();
            lblyear.Text=dr["Allot_year"].ToString();
       

        }
        else
        {
            txtrodate.Text = "";
            txtroqty.Text = "";
            txtcomdty.Text = "";
            txtbalqty.Text = "";
        }

    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
      
    void GetFCIdist()
    {
        obj = new LARO(ComObj);
        string qry = "select districtsmp.district_name as dist_name,DepoCode.district_code as dist_code From dbo.DepoCode left join pds.districtsmp   on upper(DepoCode.district)=upper( districtsmp.district_name) group by districtsmp.district_name, DepoCode.district_code";
        DataSet ds = obj.selectAny(qry);

        ddlfcidist.DataSource = ds.Tables[0];
        ddlfcidist.DataTextField = "dist_name";
        ddlfcidist.DataValueField = "dist_code";
        ddlfcidist.DataBind();
        ddlfcidist.Items.Insert(0, "--Select--");

    }
    void GetFCIdepot()
    {
        string dtype = ddldepottype.SelectedItem.ToString();
        string dcode = ddlfcidist.SelectedValue;
        obj = new LARO(ComObj);
        string qry = "select distinct(DepoName) as depo_name  ,DepoCode as depo_code,type From dbo.DepoCode where district_code='" + dcode + "'and type='" + dtype + "'";
        DataSet ds = obj.selectAny(qry);

        ddlfcidepo.DataSource = ds.Tables[0];
        ddlfcidepo.DataTextField = "depo_name";
        ddlfcidepo.DataValueField = "depo_code";
        ddlfcidepo.DataBind();
        ddlfcidepo.Items.Insert(0, "--Select--");

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
        ddlissue.Focus();
    }
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        //string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        string ord = "Select DepotName,DepotId from dbo.tbl_MetaData_DEPOT order by DepotName ";
        DataSet ds = distobj.selectAny(ord);
       
        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");
        ddlissue.Focus();
        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void Transport()
    {

        tobj = new Transporter(ComObj);
        string qry = "Select Lead,Transporter_ID,Transporter_Name+'/'+'('+Lead_Distance.Lead_Name +')'as Transporter_Name  from dbo.Transporter_Table left join Lead_Distance on Transporter_Table.Lead=Lead_Distance.Lead_ID where Distt_ID='" + distid + "' and IsActive='Y'";// and Lead='"+ddllead.SelectedValue+"'";
        DataSet ds = tobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
        }
        else
        {
            txttrans.DataSource = ds.Tables[0];
            txttrans.DataTextField = "Transporter_Name";
            txttrans.DataValueField = "Transporter_ID";
            txttrans.DataBind();
            txttrans.Items.Insert(0, "--Select--");
        }

    }

    void fillgrid()
    {
        mobjro = new MoveChallan(ComObj);
        string qryro = "SELECT Transport_Order_againstRo.Trunsuction_Id,Lift_A_RO.* ,dbo.tbl_MetaData_DEPOT.DepotName as Depot_Name,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name  from dbo.Lift_A_RO left join  dbo.tbl_MetaData_DEPOT on Lift_A_RO.Issue_center=dbo.tbl_MetaData_DEPOT.DepotID left join dbo.tbl_MetaData_STORAGE_COMMODITY  on Lift_A_RO.Commodity=dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id  left join   dbo.Transport_Order_againstRo ON Lift_A_RO.Dist_Id = Transport_Order_againstRo.Distt_Id AND Lift_A_RO.RO_No = Transport_Order_againstRo.RO_No AND Lift_A_RO.TO_Number = Transport_Order_againstRo.TO_Number AND Lift_A_RO.Send_District = Transport_Order_againstRo.toDistrict AND Lift_A_RO.Issue_center = Transport_Order_againstRo.toIssueCenter  where Lift_A_Ro.RO_No='" + ddlrono.SelectedItem + "' and Lift_A_Ro.Dist_Id='" + distid + "'";
        DataSet dsro = mobjro.selectAny(qryro);
        GridView1.DataSource = dsro.Tables[0];
        GridView1.DataBind();
    }

    protected void ddlrono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        fillgrid();
        GetTO();
        Label2.Visible = false;
    }
    void GetTO()
    {
        ddltono.Items.Clear();
        string mro = ddlrono.SelectedValue;
       ddltono.Items.Insert(0, "--Select--");
        string qryro = "SELECT distinct(TO_Number) From dbo.Transport_Order_againstRo where Distt_Id='" + distid + "' and RO_No='" + mro + "' and IsLifted='N'";
        cmd.Connection = con;
        cmd.CommandText = qryro;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddltono.Items.Add(dr["TO_Number"].ToString());



        }
        dr.Close();
        con.Close();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetGunny();

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetDCName();
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        //string ord = "Select DepotName,DepotId from dbo.tbl_MetaData_DEPOT order by DepotName ";
        DataSet ds = distobj.select(ord);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");
    }
    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetDist();
    }
   
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
    //void GetTotal()
    //{
    //    string mscheme = lblscheme.Text;
    //    string mcomdty = lblcomdty.Text;
    //    int month = int.Parse(lblmonth.Text);
    //    int year = int.Parse(lblyear.Text);
    //    string qryGD = "Select Sum(Qty_send) as Qty_send   from dbo.Lift_A_RO where Scheme='" + mscheme + "'and Commodity='" + mcomdty + "' and Dist_Id='" + distid + "' and Month=" + month + "and Year=" + year;

    //    DObj = new Districts(ComObj);
    //    DataSet dsGD = DObj.selectAny(qryGD);
    //    if (dsGD.Tables[0].Rows.Count==0)
    //    {           


    //    }
    //    else
    //    {
    //        DataRow drGD = dsGD.Tables[0].Rows[0];
    //        float liftq = CheckNull(drGD["Qty_send"].ToString());

    //        string qrydallocU = "Update dbo.District_Alloc set Lifted_Qty =" + liftq + " where Scheme_ID='" + mscheme + "'and Commodity_ID='" + mcomdty + "' and district_code='" + distid + "'and Month=" + month + "and Year=" + year;
    //        cmd.Connection = con;
    //        cmd.CommandText = qrydallocU;
    //        try
    //        {

                
    //            cmd.ExecuteNonQuery();
               
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //            con.Close();
    //            ComObj.CloseConnection();
               

    //        }

    //    }

    //}

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Challan_Date"));

            getdatef = getdate(griddate);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        mobj = new MoveChallan(ComObj);
        string mmrono=ddlrono.SelectedValue;
        string challanno=txtchallan.Text;
        string transid = lbltid.Text;
        string getch = "Select Challan_No from dbo.Lift_A_RO where Dist_Id='" + distid + "' and Challan_No='" + challanno + "'";
        DataSet dsch = mobj.selectAny(getch);
        if (dsch == null)
        {

        }
        else
        {
            if (dsch.Tables[0].Rows.Count == 0)
            {
                objo = new LARO(ComObj);
                string ssqlro = "Select Commodity from dbo.RO_of_FCI where Distt_Id='" + distid + "'and RO_No='" + ddlrono.SelectedItem.ToString() + "'";
                DataSet dso = objo.selectAny(ssqlro);
                if (dso.Tables[0].Rows.Count == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select The Commodity); </script> ");

                }
                else
                {
                    DataRow drr = dso.Tables[0].Rows[0];

                    string Comodty = drr["Commodity"].ToString();
                    decimal roqty = CheckNull(txtbalqty.Text);
                    decimal sndqty = CheckNull(txtqtysend.Text.ToString());

                    string dist_id = distid;
                    string RO_No = ddlrono.SelectedItem.ToString();
                    string RO_date = getmmddyy(txtrodate.Text);
                    decimal RO_qty = CheckNull(txtroqty.Text);
                    string Commodity = Comodty;


                    decimal balamt = CheckNull((txtbalqty.Text)) - CheckNull((txtqtysend.Text.ToString()));

                    string Balance_Qty = balamt.ToString();
                    decimal balqty = CheckNull(Balance_Qty);

                    string Transporter = txttrans.SelectedValue;
                    string Vehicle_No = txtvehno.Text;
                    string Challan_No = txtchallan.Text;
                    string Challan_Date = getDate_MDY(challandate.Text);
                    decimal Qty_send = CheckNull(txtqtysend.Text.ToString());
                    string Category = ddlcategory.SelectedItem.ToString();
                    string Crop_year = ddlcropyear.SelectedItem.ToString();
                    string Godown = "";
                    string Gunny_type = "";
                    int No_of_Bags = CheckNullInt(txtnobags.Text);
                    string Send_District = ddldistrict.SelectedValue;
                    string Issue_center = ddlissue.SelectedValue;
                    //string Created_Date = getDate_MDY(DateTime.Today.Date.ToString());
                    decimal mmoisture = CheckNull(txtmoisture.Text);
                    string rstatus = lblstatus.Text;
                    string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());

                    string fcidist = lblfdepo.Text;
                    string fcidepo = lblfdist.Text;

                    string fcidtype = ddldepottype.SelectedItem.ToString();
                    string scheme = lblscheme.Text;

                    string mtono = ddltono.SelectedValue;
                    int month = int.Parse(lblmonth.Text.ToString ());
                    int year = int.Parse(lblyear.Text.ToString ());
                    string mTONO = ddltono.SelectedValue;
                    string mrrono = ddlrono.SelectedValue;
                    string notrans = "N";
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string opid = Session["OperatorIDDM"].ToString();
                    string state = Session["State_Id"].ToString();

                    if (ddltono.SelectedItem.Text == "--Select--")
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select The Transport Order No...'); </script> ");

                    }
                    else
                    {

                        if (ddldepottype.SelectedItem.Text == "--Select--" || ddlfcidist.SelectedItem.Text == "--Select--" || ddlfcidepo.SelectedItem.Text == "--Select--" || ddldistrict.SelectedItem.Text == "--Select--" || ddlissue.SelectedItem.Text == "--Select--")
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Proper FCI Depot and Destination Depot...'); </script> ");
                        }
                        else
                        {


                            string qryInsert = "insert into dbo.Lift_A_RO(State_Id,dist_id,RO_No,RO_date,RO_qty,Commodity,Scheme,Balance_Qty,FCIdistrict,FCIdepo,FCIdepotype,TO_Number,Transporter,Vehicle_No,Challan_No,Challan_Date,Qty_send,Category,Crop_year,Godown,Gunny_type,No_of_Bags,Send_District,Issue_center,Dispatch_Time,Moisture,Month,Year,Created_Date,updated_date,deleted_date,IsRecieved,IP_Address,OperatorID,NoTransaction)values('" + state + "','" + dist_id + "','" + RO_No + "','" + RO_date + "'," + RO_qty + ",'" + Commodity + "','" + scheme + "'," + Balance_Qty + ",'" + fcidist + "','" + fcidepo + "','" + fcidtype + "','" + mtono + "','" + Transporter + "','" + Vehicle_No + "','" + Challan_No + "','" + Challan_Date + "'," + Qty_send + ",'" + Category + "','" + Crop_year + "','" + Godown + "','" + Gunny_type + "'," + No_of_Bags + ",'" + Send_District + "','" + Issue_center + "','" + time + "'," + mmoisture + "," + month + "," + year + ",getdate(),'','','" + rstatus + "','" + ip + "','" + opid + "','" + notrans + "')";
                            cmd.Connection = con;
                            cmd.CommandText = qryInsert;
                            decimal mrosend = CheckNull(txtqtysend.Text);
                            string uquery = "update  dbo.RO_of_FCI set Balance_Qty=Round(convert(decimal(18,5),Balance_Qty)-(" + mrosend + "),5),IsLifted='Y' where RO_No='" + RO_No + "' and Distt_Id='" + distid + "'";
                            //string uquery = "update  dbo.RO_of_FCI set IsLifted='Y' where RO_No='" + RO_No + "' and Distt_Id='" + distid + "'";
                           
                            //SqlTransaction trns;
                            //trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                            //cmd.Transaction = trns;
                            try
                            {
                                decimal pnding = System.Math.Round(CheckNull(lblpendqty.Text),5);
                                decimal sending = CheckNull(txtqtysend.Text);
                                if (pnding < sending)
                                {
                                    lbldisply.Visible = true;
                                    lbldisply.Text = "Sorry You Can't Issue More Than Pending Quantity...";
                                    //Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry You Can't Issue More Than Pending Quantity...'); </script> ");

                                }
                                else
                                {
                                    lbldisply.Visible = false;

                                    if (No_of_Bags == 0 || sending == 0)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity/No.Of Bags...'); </script> ");

                                    }
                                    else
                                    {
                                        con.Open();
                                        int count = cmd.ExecuteNonQuery();
                                        con.Close();
                                        if (count == 1)
                                        {
                                            
                                            cmd.CommandText = uquery;
                                           
                                            //cmd.Transaction = trns;
                                            con.Open();
                                            int countut = cmd.ExecuteNonQuery();
                                            con.Close();
                                            if (balqty == 0)
                                            {
                                                string uqrylift = "update  dbo.RO_of_FCI set IsLifted='Y' where RO_No='" + RO_No + "' and Distt_Id='" + distid + "'";
                                                cmd.CommandText = uqrylift;
                                                //cmd.Transaction = trns;
                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }

                                            if (countut == 1)
                                            {
                                                string mchkto = ddltono.SelectedValue;
                                                string mtodist = ddldistrict.SelectedValue;
                                                string mtoissue = ddlissue.SelectedValue;

                                                decimal mmsendqty = CheckNull(txtqtysend.Text);
                                                string Update = "Update dbo.Transport_Order_againstRo set Cumulative_Qty=Round(convert(decimal(18,5),Cumulative_Qty)+(" + mmsendqty + "),5),Pending_Qty =Round(convert(decimal(18,5),Pending_Qty)-" + mmsendqty + ",5) where Distt_Id='" + distid + "' and RO_No='" + mrrono + "' and TO_Number='" + mTONO + "'and Trunsuction_Id='" + transid + "'";
                                                cmd.CommandText = Update;
                                                //cmd.Transaction = trns;
                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();

                                                objo = new LARO(ComObj);
                                                string qrycum = "Select Round(convert(decimal(18,5),Cumulative_Qty),5) as Cumulative_Qty ,Round(convert(decimal(18,5),Pending_Qty),5) as Pending_Qty ,Round(convert(decimal(18,5),Quantity),5) as Quantity  from dbo.Transport_Order_againstRo where Distt_Id='" + distid + "'and RO_No='" + ddlrono.SelectedItem.ToString() + "'and TO_Number='" + mchkto + "'and toDistrict='" + mtodist + "'and toIssueCenter='" + mtoissue + "'and Trunsuction_Id='" + transid + "'";
                                                DataSet dscum = objo.selectAny(qrycum);
                                                if (dscum.Tables[0].Rows.Count == 0)
                                                {

                                                }
                                                else
                                                {
                                                    DataRow drcum = dscum.Tables[0].Rows[0];
                                                    //decimal mcumulateq = CheckNull(drcum["Cumulative_Qty"].ToString());
                                                    decimal pendqty = CheckNull(drcum["Pending_Qty"].ToString());
                                                    //decimal actqty = CheckNull(drcum["Quantity"].ToString());
                                                    //decimal msendqty = CheckNull(txtqtysend.Text.ToString());
                                                    //decimal mcumquant = mcumulateq + msendqty;
                                                    //decimal mpendqty = actqty - mcumquant;

                                                    if (pendqty == 0)
                                                    {
                                                        string UpdateTO = "Update dbo.Transport_Order_againstRo set IsLifted='Y' where Distt_Id='" + distid + "' and RO_No='" + mrrono + "' and TO_Number='" + mTONO + "'and toIssueCenter='" + mtoissue + "'and Trunsuction_Id='" + transid + "'";
                                                        cmd.CommandText = UpdateTO;
                                                        //cmd.Transaction = trns;
                                                        con.Open();
                                                        cmd.ExecuteNonQuery();
                                                        con.Close();


                                                    }


                                                    objo = new LARO(ComObj);
                                                    string qrylift = "Select Round(convert(decimal(18,5),Lifted_Qty),5) as Lifted_Qty  from dbo.TO_Allot_Lift where Distt_Id='" + distid + "'and RO_No='" + ddlrono.SelectedItem.ToString() + "'";
                                                    DataSet dslift = objo.selectAny(qrylift);
                                                    if (dslift.Tables[0].Rows.Count == 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        DataRow drlift = dslift.Tables[0].Rows[0];

                                                        decimal liftedqty = CheckNullDecimal(drlift["Lifted_Qty"].ToString());
                                                        decimal msndqty = System.Math.Round(CheckNull(txtqtysend.Text.ToString()), 5);
                                                        decimal  aliftqty = liftedqty + msndqty;
                                                        string UpdateLift = "Update TO_Allot_Lift set Lifted_Qty=" + aliftqty + " where Distt_Id='" + distid + "' and RO_No='" + ddlrono.SelectedItem.ToString() + "'";
                                                        cmd.CommandText = UpdateLift;
                                                        //cmd.Transaction = trns;
                                                        con.Open();
                                                        cmd.ExecuteNonQuery();
                                                        con.Close();

                                                    }



                                                }



                                            }
                                        }
                                    }
                                    //trns.Commit();
                                   
                                    UpdateReceipt();
                                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                                    Session["gatepass"] = Challan_No;
                                    Session["ToNo"] = mTONO;
                                    //HyperLink1.Visible = true;
                                    //btnsubmit.Enabled = false;
                                    btnsubmit.Visible = false;
                                    btnaddmore.Visible = true;
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                //trns.Rollback();
                                Label1.Text = ex.Message;


                            }
                            finally
                            {
                                con.Close();
                                ComObj.CloseConnection();
                            }



                        }

                    }
                }

            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Already Exist..'); </script> ");

            }

        }


       
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gateno = GridView1.SelectedRow.Cells[3].Text;
        Session["RO_No"] = gateno;
        Session["TO_No"] = GridView1.SelectedRow.Cells[4].Text;
        Session["Trans"] = GridView1.SelectedRow.Cells[11].Text;
        Session["TIC"] = GridView1.SelectedRow.Cells[12].Text;
        Session["Challan"] = GridView1.SelectedRow.Cells[1].Text;
        string tag = "Y";
        mobj = new MoveChallan(ComObj);
        string tono = GridView1.SelectedRow.Cells[4].Text;
        string mchallan = GridView1.SelectedRow.Cells[1].Text;
        string qry = "SELECT IsRecieved FROM dbo.Lift_A_RO where Dist_Id='" + distid + "'and RO_No='" + gateno + "'and Challan_No='" + mchallan + "'and TO_Number='" + tono + "'";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            string st = dr["IsRecieved"].ToString().Trim();
            if (st == tag)
            {
                Label2.Visible = true;
                Label2.Text = "Sorry You Can't Edit This Details ,It has been Deposited";
            }
            else
            {
                Label2.Visible = false;
                Response.Redirect("~/District/Edit_LARO_Page.aspx");
            }
        }

       
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFCIdepot();
        ddlfcidepo.Focus();
    }
    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    decimal CheckNullDecimal(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");

        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void ddltono_SelectedIndexChanged(object sender, EventArgs e)
    {
       // GetToDetail();


    }
   
    void GetToDetail()
    {
        lblchk.Visible = true;
        string mtono = ddltono.SelectedValue;
        string mro = ddlrono.SelectedValue;
        mobjro = new MoveChallan(ComObj);
        string qryro = "SELECT DepoCode.DepoName,DepoCode.District , Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.Trunsuction_Id,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Round(convert(decimal(18,5),Transport_Order_againstRo.Cumulative_Qty),5) as Cumulative_Qty ,Round(convert(decimal(18,5),Transport_Order_againstRo.Pending_Qty),5) as Pending_Qty ,Round(convert(decimal(18,5),Transport_Order_againstRo.Quantity),5) as Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID  left join dbo.DepoCode on Transport_Order_againstRo.FCI_district=DepoCode.District_Code  and Transport_Order_againstRo.FCI_depot=DepoCode.DepoCode    where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "' and Transport_Order_againstRo.IsLifted='N'";
        //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

        DataSet dstod = mobjro.selectAny(qryro);

       this.GridView2.DataSource = dstod;
       this.GridView2.DataBind();
       //GridView2.Columns[6].Visible = false;
       //GridView2.Columns[8].Visible = false;
       //GridView2.Columns[9].Visible = false;
       //GridView2.Columns[10].Visible = false;
       //GridView2.Columns[11].Visible = false;
       //GridView2.Columns[12].Visible = false;


       

    }
    protected void ddltono_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetToDetail();
        ddldepottype.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void ddlfcidepo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldepottype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        //GridView2.Columns[9].Visible = true;
        //GridView2.Columns[10].Visible = true;
        //GridView2.Columns[11].Visible = true;
        //GridView2.Columns[12].Visible = true;
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow gr  = (GridViewRow)checkbox.NamingContainer ;
    
        if(checkbox.Checked==true)
        {
     
        //if (GridView2.Rows.Count == 0)
        //{
        //}
        //else
        //{

        //    foreach (GridViewRow gr in GridView2.Rows)
        //    {
                
        //        CheckBox GchkBx = (CheckBox)gr.FindControl("CheckBox1");
              
        //        if (GchkBx.Checked == true)
        //        {
        //            GchkBx.Enabled = false;
                    //GridView2.Columns[6].Visible = true;
                    //GridView2.Columns[8].Visible = true;
                    string todist = gr.Cells[9].Text;
                    string tdepot = gr.Cells[10].Text;
                    //txtqtysend.Text = gr.Cells[11].Text;
                    lblpendqty.Text = gr.Cells[8].Text;
                    lbltid.Text = gr.Cells[12].Text;
                    //txtqtysend.Text = gr.Cells[11].Text;
                    string mtono = ddltono.SelectedValue;
                    string mro = ddlrono.SelectedValue;
                    mobjro = new MoveChallan(ComObj);
                    string qryro = "SELECT DepoCode.DepoName as FDepo,DepoCode.District,Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID left join dbo.DepoCode on DepoCode.District_Code=Transport_Order_againstRo.FCI_district and DepoCode.DepoCode=Transport_Order_againstRo.FCI_depot   where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "' and Transport_Order_againstRo.toDistrict='" + todist + "'and Transport_Order_againstRo.toIssueCenter='" + tdepot + "'and Transport_Order_againstRo.IsLifted='N'";
                    //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

                    DataSet dstod = mobjro.selectAny(qryro);

                    DataRow dr = dstod.Tables[0].Rows[0];
                    txttrans.SelectedItem.Text = dr["Tname"].ToString();
                    txttrans.SelectedValue = dr["Transporter_Name"].ToString();
                    //txtqtysend.Text = dr["Quantity"].ToString();
                    ddlfcidist.SelectedItem.Text = dr["District"].ToString();
                    ddlfcidepo.SelectedItem.Text = dr["FDepo"].ToString();
                    lblfdepo.Text = dr["FCI_district"].ToString();
                    lblfdist.Text = dr["FCI_depot"].ToString();
                    ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
                    ddldistrict.SelectedValue = dr["toDistrict"].ToString();
                    ddlissue.SelectedItem.Text = dr["DepotName"].ToString();
                    ddlissue.SelectedValue = dr["toIssueCenter"].ToString();

                    //ddldistrict.BackColor = System.Drawing.Color.CadetBlue;

                    //ddlissue.BackColor = System.Drawing.Color.CadetBlue;
                    //ddlfcidist.BackColor = System.Drawing.Color.CadetBlue;
                    //ddlfcidepo.BackColor = System.Drawing.Color.CadetBlue;
                    //txtqtysend.ReadOnly = true;

                    //txttrans.BackColor = System.Drawing.Color.CadetBlue;
                    ddldistrict.Enabled = false;
                    ddlissue.Enabled = false;
                    ddlfcidist.Enabled = false;
                    ddlfcidepo.Enabled = false;
                    txtchallan.Focus();
                    GetChallan();
                    //Label5.Visible = true;
                    Label6.Visible = false;
                    Label7.Visible = false;
                    Label8.Visible = false;
                    Label9.Visible = false;
                    ddldistrict.Visible = false;
                    ddlissue.Visible = false;
                    ddlfcidist.Visible = false;
                    ddlfcidepo.Visible = false;
        }
        else
        {
              Transport();
             GetDist();
            //GetCategory();
            GetFCIdist();
            GetDCName();
        }

        //        }
        //        else
        //        {
        //            if (GchkBx.Checked == false)
        //            {
        //                GchkBx.Enabled = true;
        //                Transport();
        //                GetDist();
        //                //GetCategory();
        //                GetFCIdist();
        //                GetDCName();
        //            }
        //            else
        //            {
        //                Transport();
        //                GetDist();
        //                GetCategory();
        //                GetFCIdist();
        //                GetDCName();
        //                ddldistrict.BackColor = System.Drawing.Color.White;
        //                ddlissue.BackColor = System.Drawing.Color.White;
        //                ddlfcidist.BackColor = System.Drawing.Color.White;
        //                ddlfcidepo.BackColor = System.Drawing.Color.White;
        //                //txtqtysend.ReadOnly = true;

        //                txttrans.BackColor = System.Drawing.Color.White;
        //                ddldistrict.Enabled = true;
        //                ddlissue.Enabled = true;
        //                ddlfcidist.Enabled = true;
        //                ddlfcidepo.Enabled = true;
        //                lblfdepo.Text = "";
        //                lblfdist.Text = "";
        //            }
        //        }


        //    }
        //}
        ////GridView2.Columns[6].Visible =false ;
        ////GridView2.Columns[8].Visible =false;
        ////GridView2.Columns[12].Visible = false;

        //GridView2.Columns[9].Visible = false;
        //GridView2.Columns[10].Visible = false;
        //GridView2.Columns[11].Visible = false;
        //GridView2.Columns[12].Visible = false;


        
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetDetails();
    }
    void GetDetails()
    {

        if (GridView2.Rows.Count == 0)
        {
        }
        else
        {

            foreach (GridViewRow gr in GridView2.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

                if (GchkBx.Checked == true)
                {
                    string todist = gr.Cells[6].Text;
                    string tdepot = gr.Cells[8].Text;
                    //txtqtysend.Text = gr.Cells[11].Text;
                    lblpendqty.Text = gr.Cells[11].Text;
                    lbltid.Text = gr.Cells[12].Text;
                    //txtqtysend.Text = gr.Cells[11].Text;
                    string mtono = ddltono.SelectedValue;
                    string mro = ddlrono.SelectedValue;
                    mobjro = new MoveChallan(ComObj);
                    string qryro = "SELECT DepoCode.DepoName as FDepo,DepoCode.District,Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID left join dbo.DepoCode on DepoCode.District_Code=Transport_Order_againstRo.FCI_district and DepoCode.DepoCode=Transport_Order_againstRo.FCI_depot   where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "' and Transport_Order_againstRo.toDistrict='" + todist + "'and Transport_Order_againstRo.toIssueCenter='" + tdepot + "'and Transport_Order_againstRo.IsLifted='N'";
                    //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

                    DataSet dstod = mobjro.selectAny(qryro);
                    if (dstod.Tables[0].Rows.Count == 0)
                    {
                    }
                    else
                    {
                        DataRow dr = dstod.Tables[0].Rows[0];
                        txttrans.SelectedItem.Text = dr["Tname"].ToString();
                        txttrans.SelectedValue = dr["Transporter_Name"].ToString();
                        //txtqtysend.Text = dr["Quantity"].ToString();
                        ddlfcidist.SelectedItem.Text = dr["District"].ToString();
                        ddlfcidepo.SelectedItem.Text = dr["FDepo"].ToString();
                        lblfdepo.Text = dr["FCI_district"].ToString();
                        lblfdist.Text = dr["FCI_depot"].ToString();
                        ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
                        ddldistrict.SelectedValue = dr["toDistrict"].ToString();
                        ddlissue.SelectedItem.Text = dr["DepotName"].ToString();
                        ddlissue.SelectedValue = dr["toIssueCenter"].ToString();

                        ddldistrict.BackColor = System.Drawing.Color.Wheat;
                        ddlissue.BackColor = System.Drawing.Color.Wheat;
                        ddlfcidist.BackColor = System.Drawing.Color.Wheat;
                        ddlfcidepo.BackColor = System.Drawing.Color.Wheat;
                        //txtqtysend.ReadOnly = true;
                        txttrans.BackColor = System.Drawing.Color.Wheat;
                        ddldistrict.Enabled = false;
                        ddlissue.Enabled = false;
                        ddlfcidist.Enabled = false;
                        ddlfcidepo.Enabled = false;
                        txtvehno.Focus();
                        GetChallan();


                    }



                }
                else
                {
                    if (GchkBx.Checked == false)
                    {
                        GetChallan();
                    }
                    else
                    {

                        //Transport();
                        //GetDist();
                        //GetCategory();
                        //GetFCIdist();
                        //GetDCName();
                        //ddldistrict.BackColor = System.Drawing.Color.White;
                        //ddlissue.BackColor = System.Drawing.Color.White;
                        //ddlfcidist.BackColor = System.Drawing.Color.White;
                        //ddlfcidepo.BackColor = System.Drawing.Color.White;
                        ////txtqtysend.ReadOnly = true;
                        //txtqtysend.BackColor = System.Drawing.Color.White;
                        //txttrans.BackColor = System.Drawing.Color.White;
                        //ddldistrict.Enabled = true;
                        //ddlissue.Enabled = true;
                        //ddlfcidist.Enabled = true;
                        //ddlfcidepo.Enabled = true;
                        //lblfdepo.Text = "";
                        //lblfdist.Text = "";
                    }
                }



            }

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)

    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    //Find the checkbox control in header and add an attribute
        //    ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        //}
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        //GridView2.Columns[6].Visible =true ;
        //GridView2.Columns[8].Visible =true ;
        //GridView2.Columns[12].Visible = true;

        if (GridView2.Rows.Count == 0)
        {
        }
        else
        {
            
            foreach (GridViewRow gr in GridView2.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

            if (GchkBx.Checked == true)
            {
                //GridView2.Columns[6].Visible = true;
                //GridView2.Columns[8].Visible = true;
                string todist = gr.Cells[9].Text;
                string tdepot = gr.Cells[10].Text;
                //txtqtysend.Text = gr.Cells[11].Text;
                lblpendqty.Text = gr.Cells[8].Text;
                lbltid.Text = gr.Cells[12].Text;
                //txtqtysend.Text = gr.Cells[11].Text;
                string mtono = ddltono.SelectedValue;
                string mro = ddlrono.SelectedValue;
                mobjro = new MoveChallan(ComObj);
                string qryro = "SELECT DepoCode.DepoName as FDepo,DepoCode.District,Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID left join dbo.DepoCode on DepoCode.District_Code=Transport_Order_againstRo.FCI_district and DepoCode.DepoCode=Transport_Order_againstRo.FCI_depot   where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "' and Transport_Order_againstRo.toDistrict='" + todist + "'and Transport_Order_againstRo.toIssueCenter='" + tdepot + "'and Transport_Order_againstRo.IsLifted='N'";
                //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

                DataSet dstod = mobjro.selectAny(qryro);

                DataRow dr = dstod.Tables[0].Rows[0];
                txttrans.SelectedItem.Text = dr["Tname"].ToString();
                txttrans.SelectedValue = dr["Transporter_Name"].ToString();
                //txtqtysend.Text = dr["Quantity"].ToString();
                ddlfcidist.SelectedItem.Text = dr["District"].ToString();
                ddlfcidepo.SelectedItem.Text = dr["FDepo"].ToString();
                lblfdepo.Text = dr["FCI_district"].ToString();
                lblfdist.Text = dr["FCI_depot"].ToString();
                ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
                ddldistrict.SelectedValue = dr["toDistrict"].ToString();
                ddlissue.SelectedItem.Text = dr["DepotName"].ToString();
                ddlissue.SelectedValue = dr["toIssueCenter"].ToString();

                //ddldistrict.BackColor = System.Drawing.Color.CadetBlue;

                //ddlissue.BackColor = System.Drawing.Color.CadetBlue;
                //ddlfcidist.BackColor = System.Drawing.Color.CadetBlue;
                //ddlfcidepo.BackColor = System.Drawing.Color.CadetBlue;
                //txtqtysend.ReadOnly = true;

                //txttrans.BackColor = System.Drawing.Color.CadetBlue;
                ddldistrict.Enabled = false;
                ddlissue.Enabled = false;
                ddlfcidist.Enabled = false;
                ddlfcidepo.Enabled = false;
                txtchallan.Focus();
                GetChallan();
                //Label5.Visible = true;
                Label6.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                Label9.Visible = false;
                ddldistrict.Visible = false;
                ddlissue.Visible = false;
                ddlfcidist.Visible = false;
                ddlfcidepo.Visible = false;


            }
            else
            {
                if (GchkBx.Checked == false)
                {
                }
                else
                {
                    Transport();
                    GetDist();
                    GetCategory();
                    GetFCIdist();
                    GetDCName();
                    ddldistrict.BackColor = System.Drawing.Color.White;
                    ddlissue.BackColor = System.Drawing.Color.White;
                    ddlfcidist.BackColor = System.Drawing.Color.White;
                    ddlfcidepo.BackColor = System.Drawing.Color.White;
                    //txtqtysend.ReadOnly = true;
                    
                    txttrans.BackColor = System.Drawing.Color.White;
                    ddldistrict.Enabled = true;
                    ddlissue.Enabled = true;
                    ddlfcidist.Enabled = true;
                    ddlfcidepo.Enabled = true;
                    lblfdepo.Text = "";
                    lblfdist.Text = "";
                }
            }

                
            }
        }
        //GridView2.Columns[6].Visible =false ;
        //GridView2.Columns[8].Visible =false;
        //GridView2.Columns[12].Visible = false;


        
    }

    void GetNew()
    {
        //GridView2.Columns[6].Visible =true ;
        //GridView2.Columns[8].Visible =true ;
        //GridView2.Columns[12].Visible = true;

        if (GridView2.Rows.Count == 0)
        {
        }
        else
        {

            foreach (GridViewRow gr in GridView2.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

                if (GchkBx.Checked == true)
                {
                    //GridView2.Columns[6].Visible = true;
                    //GridView2.Columns[8].Visible = true;
                    string todist = gr.Cells[9].Text;
                    string tdepot = gr.Cells[10].Text;
                    //txtqtysend.Text = gr.Cells[11].Text;
                    lblpendqty.Text = gr.Cells[8].Text;
                    lbltid.Text = gr.Cells[12].Text;
                    //txtqtysend.Text = gr.Cells[11].Text;
                    string mtono = ddltono.SelectedValue;
                    string mro = ddlrono.SelectedValue;
                    mobjro = new MoveChallan(ComObj);
                    string qryro = "SELECT DepoCode.DepoName as FDepo,DepoCode.District,Transport_Order_againstRo.toDistrict,Transport_Order_againstRo.toIssueCenter,Transport_Order_againstRo.FCI_district,Transport_Order_againstRo.FCI_Depot,Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Cumulative_Qty,Transport_Order_againstRo.Pending_Qty,Transport_Order_againstRo.Quantity,toDistrict,toIssueCenter,Transporter_Table.Transporter_Name as Tname,tbl_MetaData_DEPOT.DepotName as DepotName ,districtsmp.district_name as district_name From dbo.Transport_Order_againstRo left join dbo.Transporter_Table on Transport_Order_againstRo.Transporter_Name=Transporter_Table.Transporter_ID  left join pds.districtsmp on Transport_Order_againstRo.toDistrict=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Transport_Order_againstRo.toIssueCenter=tbl_MetaData_DEPOT.DepotID left join dbo.DepoCode on DepoCode.District_Code=Transport_Order_againstRo.FCI_district and DepoCode.DepoCode=Transport_Order_againstRo.FCI_depot   where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "' and Transport_Order_againstRo.toDistrict='" + todist + "'and Transport_Order_againstRo.toIssueCenter='" + tdepot + "'and Transport_Order_againstRo.IsLifted='N'";
                    //string todata = "Select Transport_Order_againstRo.TO_Number,Transport_Order_againstRo.Transporter_Name,Transport_Order_againstRo.Quantity,FCI_district,FCI_Depot,toDistrict,toIssueCenter from dbo.Transport_Order_againstRo  where Transport_Order_againstRo.Distt_Id='" + distid + "' and Transport_Order_againstRo.RO_No='" + mro + "'and Transport_Order_againstRo.TO_Number='" + mtono + "'";

                    DataSet dstod = mobjro.selectAny(qryro);

                    DataRow dr = dstod.Tables[0].Rows[0];
                    txttrans.SelectedItem.Text = dr["Tname"].ToString();
                    txttrans.SelectedValue = dr["Transporter_Name"].ToString();
                    //txtqtysend.Text = dr["Quantity"].ToString();
                    ddlfcidist.SelectedItem.Text = dr["District"].ToString();
                    ddlfcidepo.SelectedItem.Text = dr["FDepo"].ToString();
                    lblfdepo.Text = dr["FCI_district"].ToString();
                    lblfdist.Text = dr["FCI_depot"].ToString();
                    ddldistrict.SelectedItem.Text = dr["district_name"].ToString();
                    ddldistrict.SelectedValue = dr["toDistrict"].ToString();
                    ddlissue.SelectedItem.Text = dr["DepotName"].ToString();
                    ddlissue.SelectedValue = dr["toIssueCenter"].ToString();

                    //ddldistrict.BackColor = System.Drawing.Color.CadetBlue;

                    //ddlissue.BackColor = System.Drawing.Color.CadetBlue;
                    //ddlfcidist.BackColor = System.Drawing.Color.CadetBlue;
                    //ddlfcidepo.BackColor = System.Drawing.Color.CadetBlue;
                    //txtqtysend.ReadOnly = true;

                    //txttrans.BackColor = System.Drawing.Color.CadetBlue;
                    ddldistrict.Enabled = false;
                    ddlissue.Enabled = false;
                    ddlfcidist.Enabled = false;
                    ddlfcidepo.Enabled = false;
                    txtchallan.Focus();
                    GetChallan();
                    //Label5.Visible = true;
                    Label6.Visible = true;
                    Label7.Visible = true;
                    Label8.Visible = true;
                    Label9.Visible = true;
                    ddldistrict.Visible = true;
                    ddlissue.Visible = true;
                    ddlfcidist.Visible = true;
                    ddlfcidepo.Visible = true;


                }
                else
                {
                    if (GchkBx.Checked == false)
                    {
                    }
                    else
                    {
                        Transport();
                        GetDist();
                        GetCategory();
                        GetFCIdist();
                        GetDCName();
                        ddldistrict.BackColor = System.Drawing.Color.White;
                        ddlissue.BackColor = System.Drawing.Color.White;
                        ddlfcidist.BackColor = System.Drawing.Color.White;
                        ddlfcidepo.BackColor = System.Drawing.Color.White;
                        //txtqtysend.ReadOnly = true;

                        txttrans.BackColor = System.Drawing.Color.White;
                        ddldistrict.Enabled = true;
                        ddlissue.Enabled = true;
                        ddlfcidist.Enabled = true;
                        ddlfcidepo.Enabled = true;
                        lblfdepo.Text = "";
                        lblfdist.Text = "";
                    }
                }


            }
        }
        //GridView2.Columns[6].Visible =false ;
        //GridView2.Columns[8].Visible =false;
        //GridView2.Columns[12].Visible = false;

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (GridView1.PageCount == 0)
        {
        }
        else
        {
            //Used by external paging
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                    {
                        GridView1.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView1.PageIndex > 0))
                    {
                        GridView1.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView1.PageIndex = (GridView1.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView1.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            fillgrid();
        }
    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlchallan.SelectedItem.Text == "--Select--" || ddlchallan.SelectedItem.Text == "Not Indicated")
        {
            txtchallan.Visible = true;
            txtchallan.Focus();
            txtchallan.Text = "";
        }
        else
        {
            txtchallan.Text = ddlchallan.SelectedValue;
            txtchallan.Visible = false;
            txtvehno.Focus();
        }
        GetChallanData();
    }
    void GetChallan()
    {
        string ro_no = ddlrono.SelectedValue;
        string todist = ddldistrict.SelectedValue;
        string toissue = ddlissue.SelectedValue;
        string to_no = ddltono.SelectedValue;
        string gchalan = "Select challan_no from dbo.tbl_Receipt_Details where Dist_Id='" + todist + "'and Depot_ID='" + toissue + "'and RO_No='" + ro_no + "'and TO_Number='" + to_no + "'and Challan_Status='N'";
         mobjro = new MoveChallan(ComObj);
         DataSet dsch = mobjro.selectAny(gchalan);
         if (dsch==null)
         {
             ddlchallan.Visible = false;
             txtchallan.Visible = true;
             lblstatus.Text  = "N";

         }
         else
         {
             if (dsch.Tables[0].Rows.Count ==0)
             {
             }
             else
             {
                 lblstatus.Text = "Y";
                 ddlchallan.Visible = true;
                 txtchallan.Visible = false;
                 DataRow drch = dsch.Tables[0].Rows[0];
                 ddlchallan.DataSource = dsch;
                 ddlchallan.DataTextField = "challan_no";
                 ddlchallan.DataValueField = "challan_no";
                 ddlchallan.DataBind();
                 ddlchallan.Items.Insert(0, "--Select--");
                 ddlchallan.Items.Insert(1, "Not Indicated");
             }

         }
    }
    void UpdateReceipt()
    {
        string ro_no = ddlrono.SelectedValue;
        string todist = ddldistrict.SelectedValue;
        string toissue = ddlissue.SelectedValue;
        string to_no = ddltono.SelectedValue;
        string challan = txtchallan.Text;
        string upd = "Update dbo.tbl_Receipt_Details set Challan_Status='I' where Dist_Id='" + todist + "'and Depot_ID='" + toissue + "'and RO_No='" + ro_no + "'and TO_Number='" + to_no + "'and challan_no='"+challan +"'";
        cmd.CommandText = upd;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ddlchallan.Visible = false;
            txtchallan.Text = "";
            txtchallan.Visible = true;
        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = ex.Message;
        }
        finally
        {
           
           
        }

    }
    void GetChallanData()
    {
        string ro_no = ddlrono.SelectedValue;
        string todist = ddldistrict.SelectedValue;
        string toissue = ddlissue.SelectedValue;
        string to_no = ddltono.SelectedValue;
        string challan = txtchallan.Text;
        string qrychalan = "Select challan_no,Dispatch_Date,challan_date,Qty,Crop_year,Category,Vehile_no,No_of_Bags,Moisture from dbo.tbl_Receipt_Details where Dist_Id='" + todist + "'and Depot_ID='" + toissue + "'and RO_No='" + ro_no + "'and TO_Number='" + to_no + "'and Challan_Status='N' and challan_no='"+challan +"'";
        mobjro = new MoveChallan(ComObj);
        DataSet dschdt = mobjro.selectAny(qrychalan);
        if (dschdt == null)
        {
           
        }
        else
        {
            if (dschdt.Tables[0].Rows.Count == 0)
            {
            }
            else
            {               
                DataRow drchdt = dschdt.Tables[0].Rows[0];
                txtvehno.Text = drchdt["Vehile_no"].ToString();
                txtnobags.Text = drchdt["No_of_Bags"].ToString();
                txtqtysend.Text = drchdt["Qty"].ToString();
                txtmoisture.Text = drchdt["Moisture"].ToString(); 
            }

        }


    }
    protected void Lastbutton_Click(object sender, EventArgs e)
    {

    }
    protected void btnaddmore_Click(object sender, EventArgs e)
    {
        lblchk.Visible = true;
        btnsubmit.Visible = true ;
        btnaddmore.Visible = false;
        txtvehno.Text = "";
        txtqtysend.Text = "";
        txtnobags.Text = "";
        txtchallan.Text = "";
        txtmoisture.Text = "";
        lbltid.Text = "";
       
        GetData();
        GetDist();
        GetFCIdist();
        GetDCName();
        GetToDetail();
        Label6.Visible = false;
        Label7.Visible = false;
        Label8.Visible = false;
        Label9.Visible = false;
        ddldistrict.Visible = false;
        ddlissue.Visible = false;
        ddlfcidist.Visible = false;
        ddlfcidepo.Visible = false;
        if (GridView2.Rows.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('There is no pending quantity for lift..'); </script> ");
            btnsubmit.Enabled = false;

        }
        else
        {

        }


    }
    protected void ddlalotmm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlallot_year_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
