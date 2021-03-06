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
using System.Globalization;

public partial class State_SupplyOrder_frm : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    
    DistributionCenters distobj = null;

    MoveChallan mobj = null;
  
    
    chksql chk = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
   
    public string sid = "";
    public string did = "";
    public string snid = "";
    public string S_name = "";
    public string Qty = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (Session["st_id"] != null)
               {
                  

                   
                   


                   txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                  

                   txtqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                   txtqty.Attributes.Add("onchange", "return chksqltxt(this)");


                   txtorderno.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                   
                 
                   txtdate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                   txtdate.Attributes.Add("onchange", "return chksqltxt(this)");

                   txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
                   txtrate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();


               
                   ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                  
                   

                   distobj = new DistributionCenters(ComObj);
                   chk = new chksql();
                   ArrayList ctrllist = new ArrayList();
                  
                   ctrllist.Add(txtqty.Text);
                   ctrllist.Add(txtorderno.Text);
                  
                   ctrllist.Add(txtdate.Text);
                  
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
                       
                     
                      
                    
                       GetDCName();
                       //GetZone();
                       //GetDist();
                       Getsupplier();
                       fillgrid();

                   }


               }
               else
               {

                   Response.Redirect("~/MainLogin.aspx");




               }

           }

    private void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select distinct tbl_SupplyOrder_Master.TenderRate, tbl_SupplyOrder_Master.Financial_Year, tbl_SupplyOrder_Master.ZoneCode, tbl_SupplyOrder_Master.district_code,Depot_ID, Orderno,CONVERT(nvarchar, tbl_SupplyOrder_Master.Dispatch_Date, 103) AS Dispatch_Date,distirctsmp_Sugar.district_name,tbl_MetaData_DEPOT.DepotName,ZoneMaster.Zone,Qty,S_name from tbl_SupplyOrder_Master inner join distirctsmp_Sugar on tbl_SupplyOrder_Master.district_code = distirctsmp_Sugar.district_code inner join ZoneMaster on tbl_SupplyOrder_Master.Zonecode = ZoneMaster.Zonecode inner join tbl_MetaData_DEPOT on tbl_SupplyOrder_Master.Depot_ID = tbl_MetaData_DEPOT.DepotID order by Dispatch_Date asc ";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }

   

  

    
    
    private void GetZone()
    {
       // SqlCommand cmd = new SqlCommand("select DISTINCT division.Division_code,division.Division from Division  join tblZoneMasterNew  on Division.Division_code = tblZoneMasterNew.Zonecode", con);
        SqlCommand cmd = new SqlCommand("select Distinct ZoneMaster.Zone,ZoneMaster.ZoneCode from ZoneMaster inner join tblZoneMasterNew on tblZoneMasterNew.Zonecode = ZoneMaster.Zonecode where tblZoneMasterNew.Name='" + ddlsupplier.SelectedItem.Text.ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlzonecode.DataSource = ds;
        ddlzonecode.DataTextField = "Zone";
        ddlzonecode.DataValueField = "ZoneCode";
        ddlzonecode.DataBind();
        ddlzonecode.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void Getsupplier()
    {
        SqlCommand cmd = new SqlCommand("select Distinct Name  from tblZoneMasterNew", con);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataSet ds = new DataSet();
               da.Fill(ds);
               con.Close();
               ddlsupplier.DataSource = ds;
               ddlsupplier.DataTextField = "Name";
               //ddlsupplier.DataValueField = "id";
               ddlsupplier.DataBind();
               ddlsupplier.Items.Insert(0, new ListItem("--Select--", "0"));
           }
           void GetDist()
           {


               //SqlCommand cmd = new SqlCommand("select DISTINCT [pds].[districtsmp].district_code,district_name from [pds].[districtsmp] join tblZoneMasterNew on pds.districtsmp.Division_code = tblZoneMasterNew.Zonecode where Zonecode='" + ddlzonecode.SelectedValue + "'", con);
               SqlCommand cmd = new SqlCommand("select DISTINCT distirctsmp_Sugar.district_name ,district_code from distirctsmp_Sugar join ZoneMaster on distirctsmp_Sugar.ZoneCode = ZoneMaster.ZoneCode where Zone='" + ddlzonecode.SelectedItem.ToString() + "';", con);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataSet ds = new DataSet();
               da.Fill(ds);
               con.Close();
               ddldistricts.DataSource = ds;
               ddldistricts.DataSource = ds.Tables[0];
               ddldistricts.DataTextField = "district_name";
               ddldistricts.DataValueField = "district_code";

               ddldistricts.DataBind();
               ddldistricts.Items.Insert(0, "--Select--");
               
               
               
               
               
           }
    void GetDCName()
    {


        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistricts.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        ddlissuecenters.DataSource = ds.Tables[0];
        ddlissuecenters.DataTextField = "DepotName";
        ddlissuecenters.DataValueField = "DepotID";
        ddlissuecenters.DataBind();
        ddlissuecenters.Items.Insert(0, "--Select--");
        // ddDistId.Items.Insert(0, "--चुनिये--");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    protected void ddldistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
        //GetDist();
        try
        {


            string query = "select tblZoneMasterNew.Tender_Rate from tblZoneMasterNew where tblZoneMasterNew.district_code = '" + ddldistricts.SelectedValue + "'";
            SqlCommand cmdnew = new SqlCommand(query, con);
            SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
            DataSet dsnew = new DataSet();
            danew.Fill(dsnew);
            if (dsnew == null)
            {

            }
            else
            {
                string Tender_Rate = dsnew.Tables[0].Rows[0]["Tender_Rate"].ToString();




                txtrate.Text = Tender_Rate;
                txtrate.Enabled = false;



            }
        }
        catch (Exception ex)
        {

            Label9.Visible = true;
            Label9.Text = ex.Message;


        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (lbl_Check.Text != "Sugar Supply Order Entry is Already Saved|")
            
        {


            if (ddlsupplier.SelectedItem.Text == "--Select--")
            {
                ddlsupplier.Focus();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Supplier Name.....'); </script> ");
                return;
            }

            if (ddlzonecode.SelectedItem.Text == "--Select--")
            {
                ddlzonecode.Focus();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Zone Code.....'); </script> ");
                return;
            }

            if (ddldistricts.SelectedItem.Text == "--Select--")
            {
                ddldistricts.Focus();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Districts Name.....'); </script> ");
                return;
            }

            if (ddlissuecenters.SelectedItem.Text == "--Select--")
            {
                ddlissuecenters.Focus();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select IssueCenters Name.....'); </script> ");
                return;
            }

            //  string qury = "select count(Depot_ID) from tbl_SupplyOrder_Master where Depot_ID= '" + ddlissuecenters.SelectedValue + "'";

            // SqlCommand cmd = new SqlCommand(qury, con);

            // if (con.State == ConnectionState.Closed)
            //{
            //  con.Open();
            //}

            // int  X = Convert.ToInt32(cmd.ExecuteScalar());

            //if (X == 0)

            // {

            string state = Session["State_Id"].ToString();

            string district_code = ddldistricts.SelectedValue;
            string depoid = ddlissuecenters.SelectedValue;
            string ZoneCode = ddlzonecode.SelectedValue;
            string Financial_Year = ddlcropyear.SelectedItem.ToString();
            string Created_Date = DateTime.Today.Date.ToString();
            string Orderno = txtorderno.Text;

            string Dispatch_Date = getDate_MDY(txtdate.Text);
            decimal Qty = CheckNull(txtqty.Text);
            string TenderRate = txtrate.Text;
            string S_name = ddlsupplier.SelectedItem.ToString();
            string IP_Address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string insert = "insert into dbo.tbl_SupplyOrder_Master(district_code,Depot_ID,S_name,Dispatch_Date,Orderno,ZoneCode,Financial_Year,IP_Address,State_Id,Qty,TenderRate,Created_Date)values('" + district_code + "','" + depoid + "',N'" + S_name + "','" + Dispatch_Date + "','" + Orderno + "','" + ZoneCode + "','" + Financial_Year + "','" + IP_Address + "','" + state + "','" + Qty + "','" + TenderRate + "',getdate())";


            try
            {

                cmd.Connection = con;
                cmd.CommandText = insert;
                con.Open();
                cmd.ExecuteNonQuery();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
                btnsave.Enabled = true;
                fillgrid();
            }
            catch (Exception ex)
            {

                Label9.Visible = true;
                Label9.Text = ex.Message;


            }
            finally
            {
                con.Close();
                ComObj.CloseConnection();
            }

            txtqty.Text = "";
            fillgrid();


            //else
            //{

            //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('IssueCenters :-" + ddlissuecenters.SelectedItem.Text.ToString() + ",Already Exist.. !!!'); </script> ");

            //}


        }
        else
        {
        
        }
         

        }

    

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/SupplyOrder_frm.aspx");
    }
   
    protected void ddlzonecode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetDist();
        try
        {

            string query = "select distinct  ZoneMaster.ZoneCode from tblZoneMasterNew inner join ZoneMaster on tblZoneMasterNew.ZoneCode = ZoneMaster.ZoneCode where ZoneMaster.Zone = '" + ddlzonecode.SelectedItem.ToString() + "'";
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
         
                txtzonecodes.Text = ZoneCode;
                txtzonecodes.Enabled = false;



            }
        }
        catch (Exception ex)
        {

            Label9.Visible = true;
            Label9.Text = ex.Message;


        }
      
    }

    protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlsupplier_SelectedIndexChanged1(object sender, EventArgs e)
    {
       
    }
    protected void ddlsupplier_SelectedIndexChanged2(object sender, EventArgs e)
    {
        GetZone();
    }
    protected void ddlissuecenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkEntry();



    }
    private void checkEntry()
    {

        if (!string.IsNullOrEmpty(ddlissuecenters.SelectedValue.ToString()))
        {
            con.Open();
            string d = getDate_MDY(txtdate.Text);



            string mystr = "select* from tbl_SupplyOrder_Master Where district_code='" + ddldistricts.SelectedValue.ToString() + "' AND Depot_ID='" + ddlissuecenters.SelectedValue.ToString() + "' AND S_name='" + ddlsupplier.SelectedValue + "' AND Dispatch_Date='" + d + "' AND Orderno='" + txtorderno.Text + "' AND ZoneCode='" + txtzonecodes.Text + "' AND Financial_Year='" + ddlcropyear.SelectedItem.Text + "' ";

            cmd = new SqlCommand(mystr, con);
            SqlDataReader sqldr = cmd.ExecuteReader();
            sqldr.Read();

            if (sqldr.HasRows)
            {

                lbl_Check.Visible = true;
                lbl_Check.ForeColor = System.Drawing.Color.Red;
                lbl_Check.Text = "Sugar Supply Order Entry is Already Saved|";
          
            
            }
            else
            {

               


            }
        }
        else
        {
            lbl_Check.Visible = false;
        }
    
    }

 




    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        string d = GridView1.DataKeys[GridView1.SelectedIndex].Values["Dispatch_Date"].ToString();
        DateTime dt = DateTime.ParseExact(d, "d/M/yyyy", CultureInfo.InvariantCulture);
        // for both "1/1/2000" or "25/1/2000" formats
        string newString = dt.ToString("MM/dd/yyyy");

        string mystr = "select distinct tbl_SupplyOrder_Master.TenderRate, tbl_SupplyOrder_Master.Financial_Year, tbl_SupplyOrder_Master.ZoneCode, tbl_SupplyOrder_Master.district_code,Depot_ID, Orderno,CONVERT(nvarchar, tbl_SupplyOrder_Master.Dispatch_Date, 103) AS Dispatch_Date,distirctsmp_Sugar.district_name,tbl_MetaData_DEPOT.DepotName,ZoneMaster.Zone,Qty,S_name from tbl_SupplyOrder_Master inner join distirctsmp_Sugar on tbl_SupplyOrder_Master.district_code = distirctsmp_Sugar.district_code inner join ZoneMaster on tbl_SupplyOrder_Master.Zonecode = ZoneMaster.Zonecode inner join tbl_MetaData_DEPOT on tbl_SupplyOrder_Master.Depot_ID = tbl_MetaData_DEPOT.DepotID where distirctsmp_Sugar.district_code='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["district_code"].ToString() + "' and Depot_ID='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["Depot_ID"].ToString() + "' and S_name='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["S_name"].ToString() + "' and Dispatch_Date='" + newString + "' and Orderno='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["Orderno"].ToString() + "' and ZoneMaster.ZoneCode='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["ZoneCode"].ToString() + "' and Financial_Year='" + GridView1.DataKeys[GridView1.SelectedIndex].Values["Financial_Year"].ToString() + "' ";
        SqlCommand sqlcmd = new SqlCommand(mystr, con);
        SqlDataReader sqldr = sqlcmd.ExecuteReader();
        sqldr.Read();
        if (sqldr.HasRows)
        {
            btnsave.Visible = false;
            btn_update.Visible = true;
            lbl_district.Visible = true;
            lbl_issuecentre.Visible = true;
            lbl_zonename.Visible = true;

            ddldistricts.Visible = false;
            ddlissuecenters.Visible = false;
            ddlzonecode.Visible = false;
           


            ddlsupplier.SelectedValue = sqldr["S_name"].ToString();
            ddlsupplier.Enabled = false;
            txtorderno.Text = sqldr["Orderno"].ToString();
            txtorderno.Enabled = false;
            ddlcropyear.SelectedValue = sqldr["Financial_Year"].ToString();
            ddlcropyear.Enabled = false;
            txtdate.Text = sqldr["Dispatch_Date"].ToString();
            txtdate.Enabled = false;
            
     
            lbl_zonename.Text = sqldr["Zone"].ToString();
            hd_district.Value = sqldr["district_code"].ToString();
            hd_issuecentre.Value = sqldr["Depot_ID"].ToString();
           
            txtzonecodes.Text = sqldr["ZoneCode"].ToString();
            txtzonecodes.Enabled = false;
            lbl_district.Text = sqldr["district_name"].ToString();

          
            lbl_issuecentre.Text = sqldr["DepotName"].ToString();
            
            txtrate.Text = sqldr["TenderRate"].ToString();
            txtrate.Enabled = false;
            txtqty.Text = sqldr["Qty"].ToString();

        }
    
    }


    protected void btn_update_Click(object sender, EventArgs e)
    {
        string state = Session["State_Id"].ToString();

        string district_code = ddldistricts.SelectedValue;
        string depoid = ddlissuecenters.SelectedValue;
        string ZoneCode = ddlzonecode.SelectedValue;
        string Financial_Year = ddlcropyear.SelectedItem.ToString();
        string Created_Date = DateTime.Today.Date.ToString();
        string Orderno = txtorderno.Text;

        string Dispatch_Date = getDate_MDY(txtdate.Text);
        decimal Qty = CheckNull(txtqty.Text);
        string TenderRate = txtrate.Text;
        string S_name = ddlsupplier.SelectedItem.ToString();
        string IP_Address = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string d = txtdate.Text;
        DateTime dt = DateTime.ParseExact(d, "d/M/yyyy", CultureInfo.InvariantCulture);
        // for both "1/1/2000" or "25/1/2000" formats
        string newString = dt.ToString("MM/dd/yyyy");


        string instr = "insert into dbo.tbl_SupplyOrder_Master_Editlog(district_code,Depot_ID,S_name,Dispatch_Date,Orderno,ZoneCode,Financial_Year,IP_Address,State_Id,Qty,TenderRate,Update_Date)values('" + hd_district.Value + "','" + hd_issuecentre.Value + "',N'" + S_name + "','" + Dispatch_Date + "','" + Orderno + "','" + txtzonecodes.Text + "','" + Financial_Year + "','" + IP_Address + "','" + state + "','" + Qty + "','" + TenderRate + "',getdate())";
        cmd.Connection = con;
        cmd.CommandText = instr;
        con.Open();
        cmd.ExecuteNonQuery();


        string update = "UPDATE tbl_SupplyOrder_Master SET Qty='" + txtqty.Text + "' Where district_code='" + hd_district.Value + "' AND Depot_ID='" + hd_issuecentre.Value + "' AND S_name='" + ddlsupplier.SelectedValue + "' AND Dispatch_Date='" + newString + "' AND Orderno='" + txtorderno.Text + "' AND ZoneCode='" + txtzonecodes.Text + "' AND Financial_Year='" + ddlcropyear.SelectedItem.Text + "' ";


        try
        {

            cmd.Connection = con;
            cmd.CommandText = update;
       
            cmd.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Update Successfully.....'); </script> ");
            btnsave.Enabled = true;
            fillgrid();

        }
        catch (Exception ex)
        {

            Label9.Visible = true;
            Label9.Text = ex.Message;


        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();
        }

        txtqty.Text = "";
        fillgrid();
        btnsave.Visible = true;
        btn_update.Visible = false;
        lbl_district.Visible = false;
        lbl_issuecentre.Visible = false;
        lbl_zonename.Visible = false;

        ddldistricts.Visible = true;
        ddlissuecenters.Visible = true;
        ddlzonecode.Visible = true;



        ddlsupplier.Enabled = true;

        txtorderno.Enabled = true;
        ddlcropyear.DataBind();
        ddlcropyear.Enabled = true;
       
        txtdate.Enabled = true;
         
    }
}
  