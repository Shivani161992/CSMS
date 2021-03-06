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

public partial class IssueCenter_Scheme_Transfer : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    chksql chk = null;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    public string distid = "";
    public string stid = "";
    string roqty = null;
    MoveChallan mobj = null;
    public string getdatef = "";
    DataTable dt = new DataTable();
    float disqty = 0;
    public string sid  = "";
    public string version = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            version = Session["hindi"].ToString();
            sid = Session["issue_id"].ToString();

            txtbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtqty.Attributes.Add("onchange", "return chksqltxt(this)");

            tx_stdate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            tx_stdate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            tx_stdate.Attributes.Add("onchange", "return chksqltxt(this)");
            txtbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtbags.Text);
            ctrllist.Add(txtqty.Text);
            ctrllist.Add(tx_stdate.Text);

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
                GetGodown();
                GetScheme();
                GetCommodity();
                GetSource();
                GetSourceD();
                //GetCommodityT();
                GetSchemeT();
                GetDist();
                GetDistDest();
                ddldistrict.SelectedValue = distid;
                ddldistrictd.SelectedValue = distid;
                GetDCName();
                GetDCNameDest();
                ddlissue.SelectedValue = sid;
                ddlissued.SelectedValue = sid;
                if (version == "H")
                {
                    lblsourcefrom.Text = Resources.LocalizedText.lblsourcefrom;
                    lblsourceto.Text = Resources.LocalizedText.lblsourceto;
                    lblCommodity.Text = Resources.LocalizedText.lblCommodity;
                    lblCommodity1.Text = Resources.LocalizedText.lblCommodity1;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblScheme.Text = Resources.LocalizedText.lblScheme;
                    lblScheme1.Text = Resources.LocalizedText.lblScheme1;
                    lblQuantity.Text = Resources.LocalizedText.lblQuantity;
                    lblBagNumber.Text = Resources.LocalizedText.lblBagNumber;
                    lblGodownNo.Text = Resources.LocalizedText.lblGodownNo;
                    lblschemet.Text = Resources.LocalizedText.lblschemet;
                    lblDistrictName.Text = Resources.LocalizedText.lblDistrictName;
                    lblDistrictName1.Text = Resources.LocalizedText.lblDistrictName1;
                    lblqtls.Text = Resources.LocalizedText.lblqtls;
                    lblqtls1.Text = Resources.LocalizedText.lblqtls1;
                    lblNameDepot.Text = Resources.LocalizedText.lblNameDepot;
                    lblNameDepot1.Text = Resources.LocalizedText.lblNameDepot1;
                    btnsubmit.Text = Resources.LocalizedText.btnsubmit;
                    btnclose.Text = Resources.LocalizedText.btnclose;
                    lblbalqty.Text = Resources.LocalizedText.lblbalqty;
                    lblbalqtyd.Text = Resources.LocalizedText.lblbalqtyd;
                }

            }

        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");
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
    void GetSource()
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
    void GetSourceD()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlarrivalsource.DataSource = ds.Tables[0];
        ddlarrivalsource.DataTextField = "Source_Name";
        ddlarrivalsource.DataValueField = "Source_ID";
        ddlarrivalsource.DataBind();
        ddlarrivalsource.Items.Insert(0, "--Select--");
    }
    void GetGodown()
    {
        string issueid = ddlissue.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + sid+ "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");


    }
    void GetDistDest()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddldistrictd.DataSource = ds.Tables[0];
        ddldistrictd.DataTextField = "district_name";
        ddldistrictd.DataValueField = "District_Code";

        ddldistrictd.DataBind();
        ddldistrictd.Items.Insert(0, "--Select--");
    }
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + distid + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetDCNameDest()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + distid + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissued.DataSource = ds.Tables[0];
        ddlissued.DataTextField = "DepotName";
        ddlissued.DataValueField = "DepotId";

        ddlissued.DataBind();
        ddlissued.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll("order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];

        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
    void GetCommodityT()
    {
        
        string com = ddlcomdty.SelectedItem.Text;

        string com1 = com.Substring(0, 4);
        string qr = "select * from tbl_MetaData_STORAGE_COMMODITY where Commodity_Name like '%" + com1 + "%' and Status='Y' order by Commodity_Name";
        comdtobj = new Commodity_MP(ComObj);
        DataSet dst = comdtobj.selectAny(qr);
        ddlcommodityd.DataSource = dst.Tables[0];
        ddlcommodityd.DataTextField = "Commodity_Name";
        ddlcommodityd.DataValueField = "Commodity_Id";
        ddlcommodityd.DataBind();
        ddlcommodityd.Items.Insert(0, "--Select--");


    }
    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("order by displayorder");      
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetSchemeT()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");

        ddlschemetrs.DataSource = ds.Tables[0];
        ddlschemetrs.DataTextField = "Scheme_Name";
        ddlschemetrs.DataValueField = "Scheme_Id";
        ddlschemetrs.DataBind();
        ddlschemetrs.Items.Insert(0, "--Select--");

    }
    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    decimal  CheckNullDec(string Val)
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
    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        
        
        if (CheckNull(txtbalqty.Text) == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You have insufficient balance to transfer......'); </script> ");
        }
        else
        {

            if (ddlissue.SelectedValue == ddlissued.SelectedValue && ddlscheme.SelectedValue == ddlschemetrs.SelectedValue && ddlsarrival.SelectedValue == ddlarrivalsource.SelectedValue)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Source and Destination Information Should not be same......'); </script> ");

            }
            else
            {
                string mstate = "23";
                string mcomid = ddlcomdty.SelectedValue;

                string mscheme = ddlscheme.SelectedValue;
                string mdscheme = ddlschemetrs.SelectedValue;
                string msdist = ddldistrict.SelectedValue;
                string msissue = ddlissue.SelectedValue;
                string mddist = ddldistrictd.SelectedValue;
                string mdissue = ddlissued.SelectedValue;
                string issueid = ddlissue.SelectedValue;
                string fromsrs = ddlsarrival.SelectedValue;
                string tosrs = ddlarrivalsource.SelectedValue;
                int mbagst = CheckNullInt(txtbags.Text);
                string mgdn = ddlgodown.SelectedValue;
                string ddate = "";
                string udate = "";
                int openqty = 0;
                int openbag = 0;
                int mmonth = int.Parse(DateTime.Today.Month.ToString());
                int myear = int.Parse(DateTime.Today.Year.ToString());
                float qty = CheckNull(txtqty.Text);
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string stdate = getDate_MDY(tx_stdate.Text);
                string cdate = DateTime.Today.Date.ToString();
                DateTime std = DateTime.Parse(getDate_MDY(tx_stdate.Text));
                string opid = Session["OperatorId"].ToString();
                string stm = std.Month.ToString();
                string sty = std.Year.ToString();
                if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlschemetrs.SelectedItem.Text == "--Select--" || ddlarrivalsource.SelectedItem.Text == "--Select--" || ddlsarrival.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please  Select Commodity/Scheme/Source of Arrival......'); </script> ");
                }
                else
                {


                    string qrey = "insert into dbo.State_Scheme_Transfer(State_Id,District_Id,Depotid,Commodity_Id,S_Scheme_Id,D_District,D_Depot,D_Scheme_Id,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate,IP_Address,Bags,From_Source,To_Source,Godown,ST_Date,OperatorID) values('" + mstate + "','" + distid + "','" + sid + "','" + mcomid + "','" + mscheme + "','" + mddist + "','" + sid + "','" + mdscheme + "'," + qty + "," + stm + "," + sty + ",getdate(),'" + udate + "','" + ddate + "','" + ip + "'," + mbagst + ",'" + fromsrs + "','" + tosrs + "','" + mgdn + "','" + stdate + "','"+ opid+"')";
                    cmd.CommandText = qrey;
                    cmd.Connection = con;
                    con.Open();
                    //SqlTransaction trns;
                    //trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    //cmd.Transaction = trns;
                    try
                    {
                        if (qty == 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be 0 ......'); </script> ");

                        }
                        else
                        {
                            //if (ddlscheme.SelectedValue == ddlschemetrs.SelectedValue)
                            //{
                            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Source and Destination Scheme Should not be Same ......'); </script> ");
                            //}
                            //else
                            //{

                                cmd.ExecuteNonQuery();
                                con.Close();

                                string qrystock = "select Sum(Quantity) as Qty from dbo.State_Scheme_Transfer where Commodity_Id='" + mcomid + "'and S_Scheme_Id='"+ mscheme +"' and District_Id='" + distid + "'and Depotid='" + issueid + "' and Month=" + stm+ "and Year=" + sty;
                                mobj = new MoveChallan(ComObj);
                                DataSet dspro = mobj.selectAny(qrystock);
                                if (dspro.Tables[0].Rows.Count == 0)
                                {

                                }
                                else
                                {
                                    DataRow drop = dspro.Tables[0].Rows[0];

                                    float mrothersch = CheckNull(drop["Qty"].ToString());


                                    string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomid + "' and Scheme_Id='" + mscheme + "' and DistrictId ='" + distid  + "'and DepotID='" + sid + "'and Month=" + stm  + "and Year=" + sty ;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dsopen = mobj.selectAny(qryinsopen);

                                    if (dsopen.Tables[0].Rows.Count == 0)
                                    {
                                        string chkopenss = "Select Round(convert(decimal(18,5),Sum(Current_Balance)),5)  as Current_Balance   from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id ='" + mscheme + "'";
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsqry = mobj.selectAny(chkopenss);
                                        if (dsqry == null)
                                        {

                                        }

                                        else
                                        {
                                             
                                            DataRow drss = dsqry.Tables[0].Rows[0];
                                            decimal sropen = CheckNullDec(drss["Current_Balance"].ToString());
                                            string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + mcomid + "','" + mscheme + "'," + sropen + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtqty.Text) + "," + stm + "," + sty + ",'')";
                                            cmd.CommandText = qryinsr;
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }      
                                                                               

                                    }
                                    else
                                    {
                                    string qryinsU = "update dbo.tbl_Stock_Registor set Transfer_OtherSch=" + mrothersch + " where Commodity_Id ='" + mcomid + "'and  Scheme_Id ='" +mscheme +"' and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Month=" + stm + "and Year=" +sty;
                                    cmd.CommandText = qryinsU;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }


                                string qrystockd = "select Sum(Quantity) as Qty from dbo.State_Scheme_Transfer where Commodity_Id='" + mcomid + "'and D_Scheme_Id='" + mdscheme + "' and District_Id='" + distid + "'and Depotid='" + issueid + "' and Month=" + stm + "and Year=" + sty;
                                mobj = new MoveChallan(ComObj);
                                DataSet dsprod = mobj.selectAny(qrystockd);
                                if (dspro.Tables[0].Rows.Count == 0)
                                {

                                }
                                else
                                {
                                    DataRow dropd = dspro.Tables[0].Rows[0];

                                    float mrotherschd = CheckNull(drop["Qty"].ToString());
                                    string qryinsopendes = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" +mcomid  + "'and Scheme_ID='"+ mdscheme +"' and DistrictId ='" + distid  + "'and DepotID='" + sid + "'and Month=" +stm + "and Year=" +sty ;
                                    mobj = new MoveChallan(ComObj);
                                    DataSet dsopends = mobj.selectAny(qryinsopendes);

                                    if (dsopends.Tables[0].Rows.Count == 0)
                                    {

                                        string chkopenss = "Select Round(convert(decimal(18,5),Sum(Current_Balance)),5)  as Current_Balance   from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id ='" + mdscheme  + "'";
                                        mobj = new MoveChallan(ComObj);
                                        DataSet dsqry = mobj.selectAny(chkopenss);
                                        if (dsqry == null)
                                        {

                                        }

                                        else
                                        {
                                            
                                            DataRow drss = dsqry.Tables[0].Rows[0];
                                            decimal sropen = CheckNullDec(drss["Current_Balance"].ToString());
                                            string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + sid + "','" + mcomid + "','" + mdscheme + "'," + sropen  + "," + 0 + "," + 0 + "," + 0 + "," + CheckNull(txtqty.Text) + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + stm + "," + sty + ",'')";
                                            cmd.CommandText = qryinsr;
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }                                                                                
                                                                              

                                    }
                                    else
                                    {
                                    string qryinsUd = "update dbo.tbl_Stock_Registor set Received_OtherSch=" + mrothersch + " where Commodity_Id ='" + mcomid + "' and Scheme_Id='" +mdscheme +"'and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Month=" + stm + "and Year=" + sty;
                                    cmd.CommandText = qryinsUd;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }

                                }



                                }


                                float tqty = CheckNull(txtqty.Text);
                                int mtbags = CheckNullInt(txtbags.Text);
                                string msource = ddlsarrival.SelectedValue;
                                string mgodown = ddlgodown.SelectedValue;

                                string uqry = "Update dbo.issue_opening_balance  set Current_Balance=Round(convert(decimal(18,5),Current_Balance),5)-" + tqty + ",Current_Bags=Current_Bags-" + mtbags + " where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Source ='" + msource + "' and Godown='" + mgodown + "'";
                                cmd.CommandText = uqry;
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                string mmsource = ddlarrivalsource.SelectedValue;
                                string chkopenbal = "Select * from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + ddlcommodityd.SelectedValue.ToString () + "'and Scheme_Id='" + mdscheme + "' and Godown='" + mgodown + "' and Source='" + mmsource + "'";
                                distobj = new DistributionCenters(ComObj);
                                DataSet dsbal = distobj.selectAny(chkopenbal);
                                if (dsbal == null)
                                {

                                }

                                else
                                {
                                    if (dsbal.Tables[0].Rows.Count == 0)
                                    {
                                        string qreyins = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Current_Bags,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + sid + "','" + ddlcommodityd.SelectedValue.ToString() + "','" + mdscheme + "','','" + mgodown + "',''," + openbag + "," + openqty + ",'" + mmsource + "'," + tqty + "," + mtbags + "," + mmonth + "," + myear + ",'" + ip + "',getdate(),getdate(),'" + udate + "','" + ddate + "'" + ")";
                                        cmd.CommandText = qreyins;
                                        cmd.Connection = con;
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();

                                    }
                                    else
                                    {
                                        float tqtyd = CheckNull(txtqty.Text);
                                        int tbags = CheckNullInt(txtbags.Text);

                                        string uqryd = "Update dbo.issue_opening_balance  set Current_Balance=Round(convert(decimal(18,5),Current_Balance),5)+" + tqtyd + ",Current_Bags=Current_Bags+" + tbags + " where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + ddlcommodityd.SelectedValue.ToString() + "'and Scheme_Id='" + mdscheme + "' and Godown='" + mgodown + "' and Source='" + mmsource + "'";
                                        cmd.CommandText = uqryd;
                                        cmd.Connection = con;
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                               





                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Transfer Successfully ......'); </script> ");
                                txtbalqty.ReadOnly = false;
                                //txtbalqty.Text = uqty.ToString();
                                //txtbalqtyd.ReadOnly = false;
                                //txtbalqtyd.Text = uqtyd.ToString();



                                btnsubmit.Enabled = false;




                            
                        }
                        //trns.Commit();
                       
                    }
                    catch (Exception ex)
                    {
                        //trns.Rollback();
                        Label1.Visible = true;
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
    protected void ddldistric_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void ddldistrictd_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCNameDest();
    }
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBalance();
        //ddldistrictd.SelectedItem.Text = ddldistrict.SelectedItem.Text;
        
    }
    protected void ddlschemetrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string distid = ddldistrictd.SelectedValue; ;
        string issueid = sid;
       string mcomid = ddlcomdty.SelectedValue;
        string mscheme = ddlschemetrs.SelectedValue;
        string source = ddlarrivalsource.SelectedValue;
        string godown = ddlgodown.SelectedValue;
        comdtobj = new Commodity_MP(ComObj);
        string qry = "Select Round(Sum(Current_Balance),5) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Godown='"+godown +"' and Source='"+source +"'";
        DataSet ds = comdtobj.selectAny(qry);

        if (ds.Tables[0].Rows.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  comodity....'); </script> ");
            lblbalqtyd.Visible = false;
            txtbalqtyd.Visible = false;
            lblqtls.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbalqtyd.Text = dr["Current_Balance"].ToString();
            lblbalqtyd.Visible = true;
            txtbalqtyd.Visible = true;
            lblqtls1.Visible = true;
            txtbalqtyd.BackColor = System.Drawing.Color.Wheat;
            txtbalqtyd.ReadOnly = true;
        }
    }
    void GetBalance()
    {
        string distid = ddldistrict.SelectedValue;
        string issueid = ddlissue.SelectedValue;
        string mcomid = ddlcomdty.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        string source = ddlsarrival.SelectedValue;
        string godown = ddlgodown.SelectedValue;
        comdtobj = new Commodity_MP(ComObj);
         string qry = "Select Round(Sum(Current_Balance),5) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + sid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "' and Source='"+source +"' and Godown='"+ godown +"'";
         DataSet ds = comdtobj.selectAny(qry);

         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  comodity....'); </script> ");
            lblbalqty.Visible = false;
            txtbalqty.Visible = false;
            lblqtls.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbalqty.Text = dr["Current_Balance"].ToString();
            lblbalqty . Visible = true;
            txtbalqty.Visible = true;
            lblqtls.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.ReadOnly = true;
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    protected void ddlcommodityd_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlcommodityd.SelectedItem.Text = ddlcomdty.SelectedItem.Text;
        GetCommodityT();



    }
    protected void ddlissued_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlarrivalsource_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
