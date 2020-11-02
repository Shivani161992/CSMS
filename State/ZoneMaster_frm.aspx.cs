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
using System.Text;

public partial class State_ZoneMaster_frm : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    
    protected Common ComObj = null, cmn = null;
    chksql chk = null;
   
    protected void Page_Load(object sender, EventArgs e)
    {

        Label3.Text = "";
        
        if (Session["st_id"] != null)
        {





          

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtsupplier.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtsupplier.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtsupplier.Attributes.Add("onchange", "return chksqltxt(this)");

            txttenderrate.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");


            txttenderrate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttenderrate.Attributes.Add("onchange", "return chksqltxt(this)");

            txtmobileno.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");


            txtmobileno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtmobileno.Attributes.Add("onchange", "return chksqltxt(this)");


            txtaddress.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtaddress.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtaddress.Attributes.Add("onchange", "return chksqltxt(this)");

            txtpincode.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");


            txtpincode.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtpincode.Attributes.Add("onchange", "return chksqltxt(this)");
            
            
            txttenderrate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            txtbruitno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
           
            
            chk = new chksql();
            ArrayList ctrllist = new ArrayList();


            ctrllist.Add(txtaddress.Text);
            ctrllist.Add(txtmobileno.Text);
            ctrllist.Add(txtsupplier.Text);

            ctrllist.Add(txttenderrate.Text);

            ctrllist.Add(txtpincode.Text);
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



                //GetDist();
                Getzone();
                Getsupplier();
                fillgird();
                fillgirdforselectedZone();
                fillgirdforTenderRate();
              
                GetState();
               
            }


        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");




        }
    }

    

    private void fillgirdforTenderRate()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        mobj = new MoveChallan(ComObj);
        string qrey = "select distinct ZoneMaster.ZoneCode,ZoneMaster.Zone,Name,tblZoneMasterNew.Tender_Rate,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name from tblZoneMasterNew inner join ZoneMaster on ZoneMaster.ZoneCode = tblZoneMasterNew.ZoneCode inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = tblZoneMasterNew.Commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = tblZoneMasterNew.Scheme";
        DataSet ds = mobj.selectAny(qrey);
        if (ds==null)
        {
           
        }
        else
        {
            GridView3.DataSource = ds.Tables[0];
            GridView3.DataBind();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    
    }

    private void fillgirdforselectedZone()
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        
        mobj = new MoveChallan(ComObj);
        string qrey = "select distinct ZoneMaster.Zone,pds.districtsmp.district_name,Bruit_No,Tender_Rate from tblZoneMasterNew inner join ZoneMaster on tblZoneMasterNew.ZoneCode = ZoneMaster.ZoneCode inner join pds.districtsmp on tblZoneMasterNew.district_code = pds.districtsmp.district_code";
        DataSet ds = mobj.selectAny(qrey);
        if (ds==null)
        {
            
        }
        else
        {
           
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    
    
    }

    private void GetState()
    {
        mobj = new MoveChallan(ComObj);
        string qrygrh = "SELECT State_Code ,State_Name  FROM State_Master";
        DataSet dsd = mobj.selectAny(qrygrh);
        ddlstate.DataSource = dsd.Tables[0];
        ddlstate.DataTextField = "State_Name";
        ddlstate.DataValueField = "State_Code";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "--Select--");
    }

   

   

    private void fillgird()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        
        mobj = new MoveChallan(ComObj);
        string qrey = "SELECT district_code,district_name,Division_code FROM distirctsmp_Sugar where ZoneCode in (SELECT [ZoneCode] FROM [dbo].[ZoneMaster] where Zone = '" + ddlzonename.SelectedItem.ToString() + "')";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    
    
    }

    private void Getzone()
    {
        SqlCommand cmd = new SqlCommand("select Distinct Zone,ZoneCode from ZoneMaster order by ZoneCode ASC ",con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlzonename.DataSource = ds;
        ddlzonename.DataTextField = "Zone";
        ddlzonename.DataValueField = "ZoneCode";
        ddlzonename.DataBind();
        ddlzonename.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       


    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


        GridView1.PageIndex = e.NewPageIndex;
      
    }
   
    protected void Button1_Click1(object sender, EventArgs e)
    {

       
              try
               {

                if (GridView1.Rows.Count == 0)
                {
                }
                else
                {

                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        CheckBox GchkBx = (CheckBox)gr.FindControl("CheckBox1");

                        if (GchkBx.Checked == true)
                        {

                            string district_code = gr.Cells[1].Text;
                            string district_name = gr.Cells[2].Text;
                            string Address = txtaddress.Text;
                            string Pincode = txtpincode.Text;

                            string State_Code = ddlstate.SelectedValue;
                            string district_code_others = ddldistrict.SelectedValue;
                            string Bruit_No = txtbruitno.Text;
                            string Tender_Rate = txttenderrate.Text;
                            string State_Id = Session["State_Id"].ToString();
                            string Zonecode = ddlzonename.SelectedValue;
                            string MobileNo = txtmobileno.Text;

                            string Nividha_frmdate = getDate_MDY(txtnfromdate.Text);
                            string Nividha_todate = getDate_MDY(txtntodate.Text);
                            string Financial_Year = ddlfinancialyear.SelectedItem.ToString();
                            string Name = txtsupplier.Text;
                            string crdate = DateTime.Today.Date.ToString();
                            string IP_Address = Request.ServerVariables["REMOTE_ADDR"].ToString();
                           
                            

                            string insert = "insert into dbo.tblZoneMasterNew(Zonecode,Name,Address,Pincode,MobileNo,State_Code,district_code_others,district_code,Financial_Year,Tender_Rate,Bruit_No,Nividha_frmdate,Nividha_todate,CreatedDate,IP_Address,State_Id)values('" + Zonecode + "',N'" + Name + "',N'" + Address + "','" + Pincode + "','" + MobileNo + "','" + State_Code + "','" + district_code_others + "','" + district_code + "','" + Financial_Year + "','" + Tender_Rate + "','" + Bruit_No + "','" + Nividha_frmdate + "','" + Nividha_todate + "',getdate(),'" + IP_Address + "','" + State_Id + "')";
                            try
                            {
                                cmd.Connection = con;
                                cmd.CommandText = insert;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
                                GchkBx.Checked = false;


                            }
                            catch (Exception ex)
                            {
                                Label3.Visible = true;
                                Label3.Text = ex.Message;
                            }
                            finally
                            {
                                con.Close();
                            }

                            fillgirdforselectedZone();
                            fillgirdforTenderRate();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Label3.Visible = true;
                Label3.Text = ex.Message;
            }


           
    
    
    }

    private void Getsupplier()
    {
        SqlCommand cmd = new SqlCommand("select Distinct Name  from tblZoneMasterNew", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();

        ddl_filledsuplyer.DataSource = ds;
        ddl_filledsuplyer.DataTextField = "Name";
        //ddlsupplier.DataValueField = "id";
        ddl_filledsuplyer.DataBind();
        ddl_filledsuplyer.Items.Insert(0, new ListItem("--Select--", "0"));
    }

   
        protected void Button2_Click1(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            
           
            Response.Redirect("~/State/State_Welcome.aspx");
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkTest = (CheckBox)sender;
            GridViewRow grdRow = (GridViewRow)chkTest.NamingContainer;
          
            TextBox txtamount = (TextBox)grdRow.FindControl("txtamount");
            if (chkTest.Checked)
            {
              
                txtamount.ReadOnly = false;
             
                txtamount.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
             
                txtamount.ReadOnly = true;
              
                txtamount.ForeColor = System.Drawing.Color.Blue;
            }
        }

    protected void txtzonename_TextChanged(object sender, EventArgs e)
    {
       

    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
       

    }
    
   
    protected void GridView1_SelectedIndexChanged3(object sender, EventArgs e)
    {
        
    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
       
    }
    protected void CheckBox1_CheckedChanged2(object sender, EventArgs e)
    {

    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        
        if (ddlstate.SelectedItem.Text != "Madhya Pradesh")
        {
            ddldistrict.DataSource = "";
            ddldistrict.DataBind();

            mobj = new MoveChallan(ComObj);
            string qrydist = "SELECT district_code,district_name FROM OtherState_DistrictCode where State_Id = '" + ddlstate.SelectedValue + "'";
            DataSet dsd = mobj.selectAny(qrydist);

            ddldistrict.DataSource = dsd.Tables[0];
            ddldistrict.DataTextField = "district_name";
            ddldistrict.DataValueField = "district_code";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");

        }
        else
        {
            ddldistrict.DataSource = "";
            ddldistrict.DataBind();

            mobj = new MoveChallan(ComObj);
            string qrydist = "select district_name,district_code  from pds.districtsmp  order by district_name ";
            DataSet dsd = mobj.selectAny(qrydist);

            ddldistrict.DataSource = dsd.Tables[0];
            ddldistrict.DataTextField = "district_name";
            ddldistrict.DataValueField = "district_code";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");



        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    
    }
    
    protected void ddlzonename_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        
        
        fillgird();
        try
        {


            string query = "select ZoneMaster.ZoneCode from ZoneMaster where ZoneMaster.Zone = '" + ddlzonename.SelectedItem.ToString() + "'";
            SqlCommand cmdnew = new SqlCommand(query, con);
            SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
            DataSet dsnew = new DataSet();
            danew.Fill(dsnew);
            if (dsnew == null)
            {

            }
            else
            {
                string ZoneCode = dsnew.Tables[0].Rows[0]["ZoneCode"].ToString();




                txtzonecode.Text = ZoneCode;
                txtzonecode.Enabled = false;



            }
        }
        catch (Exception ex)
        {

            Label9.Visible = true;
            Label9.Text = ex.Message;


        }
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
    
    
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/ZoneMaster_frm.aspx");
    }



    protected void ddl_filledsuplyer_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsupplier.Text = ddl_filledsuplyer.SelectedItem.Text.Trim();

         con.Open();
    
            string mystr = "select* from tblZoneMasterNew Where Name='" + ddl_filledsuplyer.SelectedValue.ToString() + "' ";

            cmd = new SqlCommand(mystr, con);
            SqlDataReader sqldr = cmd.ExecuteReader();
            sqldr.Read();

            if (sqldr.HasRows)
            {
                
               txtaddress.Text=sqldr["Address"].ToString();
                    txtpincode.Text=sqldr["Pincode"].ToString();
                    txtmobileno.Text=sqldr["MobileNo"].ToString();
             
          
            
            }
            else
            {

               


            }
        }
       
    }



