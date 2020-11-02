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
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class IssueCenter_Transporter_Order_Payment_detail : System.Web.UI.Page
{
    chksql chk = null;
    Districts DObj = null;
    
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());

    string distid = "";
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/Session_Expire_Dist.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
 
      

        if (!IsPostBack)
        {
           

            tx_dd_date.Text = DateTime.Today.Date.Date.ToString("dd/MM/yyyy");

           

            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
            ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
            ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
            ddd_allot_year.SelectedIndex = 1;

           
            GetBank();

            Get_LinkCooperative();

            get_fps_Cash();

            ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;


            

        }

        tx_dd_date.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

        tx_dd_amount.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");

       // hlinkpdo.Attributes.Add("onclick", "window.open('Print_DoorStep_DO.aspx',null,'left=400, top=100, height=900, width= 700, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
                
        chk = new chksql();
        ArrayList ctrllist = new ArrayList();
       

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

    protected void save_Click(object sender, EventArgs e)
    {
        save.Enabled = false;

        if (txttotalamt.Text == "" || tx_dd_amount.Text == "")
        {
            save.Enabled = true;

            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('भुगतान एवं प्राप्ति योग्य राशि नहीं भरी गयी  | ');</script>");
            return;
        }

        //decimal chk = Convert.ToDecimal(txttotalamt.Text); // suppose 100

        //decimal chkdd = Convert.ToDecimal(tx_dd_amount.Text); // suppose 100

        //decimal lessamt = chk - 10;  // 100 - 10 = 90

        
        //if (chkdd >= chk || chkdd >= lessamt)  // 90 <= 100
        //{
            
        //}

        //else
        //{
        //    save.Enabled = true;

        //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('डी डी की राशि, कुल प्राप्त योग्य राशि से केवल 10 रु तक कम हो सकती | ');</script>");
        //    return;
        //}

       try
        {
            string opid = Session["OperatorId"].ToString();

            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            string dist = distid;
            string issue_centre_code = sid;

                    
            if (txtwheat.Text == "" || txtrice.Text == "" || txtsugar.Text == "" || txtsalt.Text == "")
              {
                save.Enabled = true;

                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('आवंटन मात्रा सही नहीं है ');</script>");
                return;
               }


             if (txtwRate.Text == "" || txtrRate.Text == "" || txtsugRate.Text == "" || txtsaltRate.Text == "")
               {
                 save.Enabled = true;
                 
                 Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('रेट(दर)की मात्रा सही नहीं है | ');</script>");
                 return;
                }

                if (ddlfpsname.SelectedValue == "0")
                 {
                   save.Enabled = true;

                   Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS Name.');</script>");
                   return;
                 }

                    if (tx_dd_no.Text == "" || tx_dd_no.Text == "0")
                    {
                        save.Enabled = true;

                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter DD Number..');</script>");
                        return;
                    }

                    if (ddl_pmode.SelectedItem.Value == "0")
                    {
                        save.Enabled = true;

                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Payment Type..');</script>");
                        return;
                    }

                    if (ddl_bank.SelectedValue == "0")
                    {
                        save.Enabled = true;

                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select Bank Name..');</script>");
                        return;
                    }

                    if (tx_dd_date.Text == "")
                    {
                        save.Enabled = true;

                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select DD Date from Calender.');</script>");
                        return;
                    }

                    if (tx_dd_amount.Text == "")
                    {
                        save.Enabled = true;

                        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter DD Amount.');</script>");
                        return;
                    }

                    string linkname = ddlCooperative.SelectedIndex.ToString();
                    string fps = ddlfpsname.SelectedValue;

                    string allotmonth = ddl_allot_month.SelectedItem.Value;

                    string allotYear = ddd_allot_year.SelectedItem.Text;

                    string whtallot = txtwheat.Text;

                    string riceallot = txtrice.Text;

                    string sugallot = txtsugar.Text;

                    string sltallot = txtsalt.Text;

                    string Maizeallot = txtmaize.Text;

                    string wrate = txtwRate.Text;

                    string ricerate = txtrRate.Text;

                    string sugrte = txtsugRate.Text;

                    string sltrate = txtsaltRate.Text;
           
                    string maizrate = txtMaizeRate.Text;
           
                    string payamt = txttotalamt.Text;

                    string banknme = ddl_bank.SelectedValue;

                    string dd_date = getDate_MDY(tx_dd_date.Text);

                    string ddamount = tx_dd_amount.Text;

                    string ddnum = tx_dd_no.Text;

                    string fpscode = fps;

                    string year_do = System.DateTime.Now.Date.ToString("yy");    // For DO generation year wise (29/03/14)

                    string selectmax = "select max(cast(TransID as bigint)) as Transid from DPY_FPS_Payment where DistrictId='" + dist + "' and IssueCenter = '" + issue_centre_code + "' ";

                    SqlCommand cmdmax = new SqlCommand(selectmax, con);
                    SqlDataAdapter damax = new SqlDataAdapter(cmdmax);

                    DataSet dsmax = new DataSet();

                    damax.Fill(dsmax);
           
                    string Tran_ID = dsmax.Tables[0].Rows[0]["Transid"].ToString();

                    if (Tran_ID == "")
                    {
                        Tran_ID = fpscode + year_do + allotmonth  + "1000";
                    }
                    else
                    {
                        string fordo = Tran_ID.Substring(Tran_ID.Length - 4);

                        Int64 Tran_ID_new = Convert.ToInt64(fordo);

                        Tran_ID_new = Tran_ID_new + 1;

                        string combine = Tran_ID_new.ToString();

                        Tran_ID = fpscode + year_do + allotmonth + combine;
                    }


                    string insqry = "Insert into DPY_FPS_Payment (TransID,DistrictId ,IssueCenter ,allotMonth ,Allotyear ,FpsCode ,WhatAllot ,RiceAllot ,SugarAllot ,SaltAllot,MaizeAllot ,WRate ,RRate ,SugarRate ,SaltRate ,MaizeRate,TotalAmount ,LinkCooperative , PaymentType ,DDNum ,BankName ,DD_Date ,DDAmount ,CreatedDate ,IP ,UserDetail) values ('" + Tran_ID + "','" + distid + "','" + sid + "','" + allotmonth + "','" + allotYear + "','" + fps + "','" + whtallot + "','" + riceallot + "','" + sugallot + "','" + sltallot + "','"+Maizeallot+"','" + wrate + "','" + ricerate + "','" + sugrte + "','" + sltrate + "','"+maizrate+"','" + payamt + "','" + linkname + "','" + ddl_pmode.SelectedItem.Value + "','" + ddnum + "','" + banknme + "','" + dd_date + "','" + ddamount + "',getdate(),'" + ip + "','" + opid + "')";

                    try
                    {
                        SqlCommand cmdins = new SqlCommand(insqry, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        int y = cmdins.ExecuteNonQuery();

                        if (y > 0)
                        {
                            Session["TransID"] = Tran_ID;
                            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Record Saved Sucessfully.');</script>");
                        }
                    }

                    catch (Exception exx)
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        save.Enabled = true;

                        Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('" + exx + "'); </script> ");
                    }

                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
        }
        catch (Exception)
        {
            save.Enabled = true;
        }
    }

    protected void get_fps_Cash()
    {
        try
        {
            string dist = distid;
            ddlfpsname.Items.Clear();


           // string fpsget = "SELECT DPY_TranportOrder.FPSCode,tbl_rootchart_master.fps_name FROM DPY_TranportOrder join tbl_rootchart_master on tbl_rootchart_master.DistrictId=DPY_TranportOrder.DistrictId and tbl_rootchart_master.IssueCenter=DPY_TranportOrder.IssueCenter and tbl_rootchart_master.fps_code=DPY_TranportOrder.FPSCode where tbl_rootchart_master.DistrictId='" + dist + "' and tbl_rootchart_master.IssueCenter='" + sid + "' and AllotMonth='" + ddl_allot_month.SelectedValue.ToString() + "' and AllotYear='" + ddd_allot_year.SelectedValue.ToString() + "' and DPY_TranportOrder.FPSCode in (select fps_code from tbl_rootchart_master where Payment_mode = 'Cash' and fps_code not in (select FpsCode from DPY_FPS_Payment where IssueCenter = '" + sid + "' and allotMonth = '" + ddl_allot_month.SelectedValue.ToString() + "' and Allotyear = '" + ddd_allot_year.SelectedValue.ToString() + "')) order by fps_code ";

            string fpsget = "select fps_code , tbl_rootchart_master.fps_name + '(' + tbl_rootchart_master.fps_code + ')'  as fps_name from tbl_rootchart_master where Payment_mode = 'Cash' and fps_code not in (select FpsCode from DPY_FPS_Payment where IssueCenter = '" + sid + "' and allotMonth = '" + ddl_allot_month.SelectedValue.ToString() + "' and AllotYear='" + ddd_allot_year.SelectedValue.ToString() + "') and  DistrictId = '" + dist + "' and IssueCenter = '" + sid + "' order by fps_code";
            
            if (con.State == ConnectionState.Closed)
            {
              con.Open();
            }

            SqlCommand cmdfps = new SqlCommand(fpsget, con);

            SqlDataAdapter dafps = new SqlDataAdapter(cmdfps);

            DataSet dsfps = new DataSet();

            dafps.Fill(dsfps);

            if (dsfps.Tables[0].Rows.Count > 0)
            {
                ddlfpsname.DataSource = dsfps.Tables[0];

                ddlfpsname.DataTextField = "fps_name";
                ddlfpsname.DataValueField = "fps_code";
                ddlfpsname.DataBind();
                ddlfpsname.Items.Insert(0, "--Select--");


                txtWAmount.Text = "";

                txtRAmount.Text = "";

                txtsugAmount.Text = "";

                txtsaltAmount.Text = "";

                txttotalamt.Text = "";

            }

            else
            {
                ddlfpsname.DataSource = "";
               
                ddlfpsname.DataBind();
                ddlfpsname.Items.Insert(0, "--Select--");

                txtwheat.Text = "";

                txtrice.Text = "";

                txtsugar.Text = "";

                txtsalt.Text = "";

                txtWAmount.Text = "";

                txtRAmount.Text = "";

                txtsugAmount.Text = "";

                txtsaltAmount.Text = "";

                txttotalamt.Text = "";

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception)
        {

        }
    }

    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void GetRate()
    {
        if (ddlfpsname.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please select FPS...');</script>");
        }

        else
        {
            try
            {
                if (ddl_rate_type.SelectedItem.Text == "Rural")
                {
                    # region WheatRate
                    string getrate = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '22' and State_rateMaster.RateType = 'R' order by Effective_From ";

                    SqlCommand cmd = new SqlCommand(getrate, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtwRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    }

                    # endregion

                    # region RiceRate
                    string getrateR = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '3' and State_rateMaster.RateType = 'R' order by Effective_From ";

                    SqlCommand cmdR = new SqlCommand(getrateR, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                    }

                    SqlDataAdapter daR = new SqlDataAdapter(cmdR);

                    DataSet dsR = new DataSet();

                    daR.Fill(dsR);

                    if (dsR.Tables[0].Rows.Count > 0)
                    {
                        txtrRate.Text = dsR.Tables[0].Rows[0]["Rate"].ToString();
                    }

                    # endregion

                    # region SuagrRate
                    string getrateSu = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '23' and State_rateMaster.RateType = 'R' ";

                    SqlCommand cmdSu = new SqlCommand(getrateSu, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                    }

                    SqlDataAdapter daSu = new SqlDataAdapter(cmdSu);

                    DataSet dsSu = new DataSet();

                    daSu.Fill(dsSu);

                    if (dsSu.Tables[0].Rows.Count > 0)
                    {
                        txtsugRate.Text = dsSu.Tables[0].Rows[0]["Rate"].ToString();
                    }

                    # endregion

                    # region SaltRate
                    string getrateSa = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '19' and State_rateMaster.RateType = 'R' ";

                    SqlCommand cmdSa = new SqlCommand(getrateSa, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                    }

                    SqlDataAdapter daSa = new SqlDataAdapter(cmdSa);

                    DataSet dsSa = new DataSet();

                    daSa.Fill(dsSa);

                    if (dsSa.Tables[0].Rows.Count > 0)
                    {
                        txtsaltRate.Text = dsSa.Tables[0].Rows[0]["Rate"].ToString();
                    }

                    # endregion

                    # region MaizeRate
                    string getrateMaize = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '12' and State_rateMaster.RateType = 'R'";

                    SqlCommand cmdMai = new SqlCommand(getrateMaize, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlDataAdapter daMai = new SqlDataAdapter(cmdMai);

                    DataSet dsMai = new DataSet();

                    daMai.Fill(dsMai);

                    if (dsMai.Tables[0].Rows.Count > 0)
                    {
                        txtMaizeRate.Text = dsMai.Tables[0].Rows[0]["Rate"].ToString();
                    }

                    # endregion

                }

                else
                    if (ddl_rate_type.SelectedItem.Text == "Urban")
                    {
                        # region WheatRate
                        string getrate = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '22' and State_rateMaster.RateType = 'U' order by Effective_From ";

                        SqlCommand cmd = new SqlCommand(getrate, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtwRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                        }

                        # endregion

                        # region RiceRate
                        string getrateR = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '3' and State_rateMaster.RateType = 'U' order by Effective_From ";

                        SqlCommand cmdR = new SqlCommand(getrateR, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                        }

                        SqlDataAdapter daR = new SqlDataAdapter(cmdR);

                        DataSet dsR = new DataSet();

                        daR.Fill(dsR);

                        if (dsR.Tables[0].Rows.Count > 0)
                        {
                            txtrRate.Text = dsR.Tables[0].Rows[0]["Rate"].ToString();
                        }

                        # endregion

                        # region SuagrRate
                        string getrateSu = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '23' and State_rateMaster.RateType = 'U' ";

                        SqlCommand cmdSu = new SqlCommand(getrateSu, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                        }

                        SqlDataAdapter daSu = new SqlDataAdapter(cmdSu);

                        DataSet dsSu = new DataSet();

                        daSu.Fill(dsSu);

                        if (dsSu.Tables[0].Rows.Count > 0)
                        {
                            txtsugRate.Text = dsSu.Tables[0].Rows[0]["Rate"].ToString();
                        }

                        # endregion

                        # region SaltRate
                        string getrateSa = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '19' and State_rateMaster.RateType = 'U' ";

                        SqlCommand cmdSa = new SqlCommand(getrateSa, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();//9260/4710 , I230602920140501084605  ,  30053 17/04 guna
                        }

                        SqlDataAdapter daSa = new SqlDataAdapter(cmdSa);

                        DataSet dsSa = new DataSet();

                        daSa.Fill(dsSa);

                        if (dsSa.Tables[0].Rows.Count > 0)
                        {
                            txtsaltRate.Text = dsSa.Tables[0].Rows[0]["Rate"].ToString();
                        }

                        # endregion

                        # region MaizeRate
                        string getrateMaize = "select ISNULL(Rate,1)Rate from State_rateMaster where State_rateMaster.Commodity_ID = '12' and State_rateMaster.RateType = 'U'";

                        SqlCommand cmdMai = new SqlCommand(getrateMaize, con);

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlDataAdapter daMai = new SqlDataAdapter(cmdMai);

                        DataSet dsMai = new DataSet();

                        daMai.Fill(dsMai);

                        if (dsMai.Tables[0].Rows.Count > 0)
                        {
                            txtMaizeRate.Text = dsMai.Tables[0].Rows[0]["Rate"].ToString();
                        }

                        # endregion

                    }

                decimal wht = Convert.ToDecimal(CheckNull(txtwheat.Text));

                decimal rice = Convert.ToDecimal(CheckNull(txtrice.Text));

                decimal sugar = Convert.ToDecimal(CheckNull(txtsugar.Text));

                decimal salt = Convert.ToDecimal(CheckNull(txtsalt.Text));

                decimal Maize = Convert.ToDecimal(CheckNull(txtmaize.Text));

                decimal whtrte = Convert.ToDecimal(CheckNull(txtwRate.Text));

                decimal ricerte = Convert.ToDecimal(CheckNull(txtrRate.Text));

                decimal sugarrte = Convert.ToDecimal(CheckNull(txtsugRate.Text));

                decimal saltrte = Convert.ToDecimal(CheckNull(txtsaltRate.Text));

                decimal Maizerte = Convert.ToDecimal(CheckNull(txtMaizeRate.Text));


                decimal calW = wht * whtrte;

                txtWAmount.Text = Convert.ToString(calW);

                decimal calR = rice * ricerte;

                txtRAmount.Text = Convert.ToString(calR);

                decimal calsug = sugar * sugarrte;

                txtsugAmount.Text = Convert.ToString(calsug);

                decimal calsalt = salt * saltrte;

                txtsaltAmount.Text = Convert.ToString(calsalt);

                decimal calMaize = Maize * Maizerte;

                txtMaizeAmount.Text = Convert.ToString(calMaize);
                
                decimal totamt = calW + calR + calsug + calsalt + calMaize;

                txttotalamt.Text = Convert.ToString(totamt);

                tx_dd_amount.Text = Convert.ToString(totamt);

                
                if(con.State == ConnectionState.Open)
                {

                    con.Close();
                }

            }
            catch (Exception)
            {

            }
        }
        
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
            
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
    
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }
    
    protected void ddl_rate_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_rate_type.SelectedItem.Text == "Select")
        {
            txtwheat.Text = "";

            txtrice.Text = "";

            txtsugar.Text = "";

            txtsalt.Text = "";

            txtWAmount.Text = "";

            txtRAmount.Text = "";

            txtsugAmount.Text = "";

            txtsaltAmount.Text = "";

            txttotalamt.Text = "";

            tx_dd_amount.Text = "";

            txtwRate.Text = "";

            txtrRate.Text = "";

            txtsugRate.Text = "";

            txtsaltRate.Text = "";

        }

        else
        {
            GetRate();
        }
       
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/IssueCenter/Transporter_Order_Payment_detail.aspx");
    }

   
    public void Get_LinkCooperative()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }



        string qrysociety = "Select distinct CoperativeSocietyName , CooperativeSoc_Code from Link_CooperativeSociety where DistrictId = '" + distid + "' and IssueCenter = '" + sid + "' ";

        SqlCommand cmdsoc = new SqlCommand(qrysociety, con);

        SqlDataAdapter dasoc = new SqlDataAdapter(cmdsoc);

        DataSet dssoc = new DataSet();

        dasoc.Fill(dssoc);

        if (dssoc.Tables[0].Rows.Count > 0)
        {
            ddlCooperative.DataSource = dssoc.Tables[0];
            ddlCooperative.DataTextField = "CoperativeSocietyName";
            ddlCooperative.DataValueField = "CooperativeSoc_Code";
            ddlCooperative.DataBind();
            ddlCooperative.Items.Insert(0, "--Select--");
        }

        else
        {
            ddlCooperative.DataSource = "";

            ddlCooperative.DataBind();

            ddlCooperative.Items.Insert(0, "-Not Indicated-");

            ////ddl_fps_name.DataSource = "";           // Change on 13-10-14 Discussion

            ////ddl_fps_name.DataBind();
        }

    }

    protected void ddl_pmode_SelectedIndexChanged(object sender, EventArgs e)
    {


       
        ddl_bank.Enabled = true;
        if (ddl_pmode.SelectedItem.Value == "D")
        {
            tx_dd_no.Enabled = true;
           
            lbl_ddno.Visible = true;
            lbl_amt.Visible = true;
            tx_dd_date.ReadOnly = false;
         
            lblddchekno.Visible = true;
            lblddchekno.Text = "DD/Chq.No.";
            tx_dd_no.Visible = true;
            lblddchekdate.Visible = true;
          
            tx_dd_amount.Visible = true;
            lblamount.Visible = true;


        }
        else
        {
            tx_dd_no.Enabled = false;
            
            lbl_ddno.Visible = false;
            lbl_amt.Visible = true;

            if (ddl_pmode.SelectedItem.Value == "R")
            {
               
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
            
                tx_dd_date.ReadOnly = true;
                lblddchekno.Visible = false;
                tx_dd_no.Visible = false;
                lblddchekdate.Visible = false;

              

                tx_dd_amount.Visible = true;
                lblamount.Visible = true;


            }
            
            if (ddl_pmode.SelectedItem.Value == "OP")
            {
                
                lbl_amt.Visible = false;
                tx_dd_amount.Text = "0";
                ddl_bank.Enabled = false;
                //tx_dd_date.Text = "1/1/1";
                tx_dd_date.ReadOnly = true;
                lblddchekno.Visible = true;
                lblddchekno.Text = "Neft/RTGS Number";
                tx_dd_no.Visible = true;
                tx_dd_no.Enabled = true;
                tx_dd_no.Text = "0";
                lblddchekdate.Visible = false;
                //tx_do_date.Visible = false;
                tx_dd_amount.Visible = true;
                lblamount.Visible = true;
            }
        }
    }


    protected void GetBank()
    {
        try
        {
            string dist = distid;
            ddl_bank.Items.Clear();

            string qrybank = "select * from  Bank_Master_New order by Bank_Name";

            //string qrybank = "select * from  Bank_Master where District_code=" + dist + " order by Bank_Name";

            SqlCommand cmd = new SqlCommand(qrybank, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlDataAdapter da = new SqlDataAdapter(qrybank, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_bank.DataSource = ds.Tables[0];
                    ddl_bank.DataTextField = "Bank_Name";
                    ddl_bank.DataValueField = "Bank_ID";
                    ddl_bank.DataBind();
                    ddl_bank.Items.Insert(0, "--Select--");

                }
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {

        }

    }

    protected void ddlfpsname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfpsname.SelectedItem.Text == "Select")
        {

        }

        else
        {
           // string getdata = "select WheatAllot ,RiceAllot ,SugarAllot ,SaltAllot from DPY_TranportOrder where FPSCode = '"+ddlfpsname.SelectedValue+"' and AllotMonth = '"+ddl_allot_month.SelectedItem.Value+"' and AllotYear = '"+ddd_allot_year.SelectedItem.Text+"' and DistrictId = '"+distid+"' and IssueCenter = '"+sid+"'";

            string getdata = "select isnull((Net_Rice_alloc)/100,0) as RiceAllot , isnull((Net_Wheat_alloc)/100,0) as WheatAllot , isnull(sugar_alloc/100,0) as SugarAllot , isnull(salt_alloc/100,0) as SaltAllot , isnull((Net_Maize_alloc)/100,0) as MaizeAllot  from pds.fps_allot where month = '" + ddl_allot_month.SelectedItem.Value + "' and YEAR = '" + ddd_allot_year.SelectedItem.Text + "' and pds.fps_allot.fps_code = '" + ddlfpsname.SelectedValue + "' ";


            SqlCommand cmd = new SqlCommand(getdata, con_opdms);

            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtwheat.Text = ds.Tables[0].Rows[0]["WheatAllot"].ToString();

                txtrice.Text = ds.Tables[0].Rows[0]["RiceAllot"].ToString();

                txtsugar.Text = ds.Tables[0].Rows[0]["SugarAllot"].ToString();

                txtsalt.Text = ds.Tables[0].Rows[0]["SaltAllot"].ToString();

                txtmaize.Text = ds.Tables[0].Rows[0]["MaizeAllot"].ToString();

                ddl_rate_type.SelectedValue = "0";

                txtWAmount.Text = "";

                txtRAmount.Text = "";

                txtsugAmount.Text = "";

                txtsaltAmount.Text = "";

                txtMaizeAmount.Text = "";
            }

            else
            {
                txtwheat.Text = "";

                txtrice.Text = "";

                txtsugar.Text = "";

                txtsalt.Text = "";

                txtmaize.Text = "";

                txtWAmount.Text = "";

                txtRAmount.Text = "";

                txtsugAmount.Text = "";

                txtsaltAmount.Text = "";

                txtMaizeAmount.Text = "";

                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Details of Allotment Not Found'); </script> ");

            }

            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }
        }

        
    }


    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_fps_Cash();
    }
}