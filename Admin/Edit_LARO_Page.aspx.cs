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
public partial class Admin_Edit_LARO_Page : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    LARO obj = null;
    LARO objo = null;
    public string adminid = "";
    string roqty = null;
    MoveChallan mobj = null;
    MoveChallan mobjro = null;
    public string getdatef = "";
    public string Ro_No = "";
    public string TO_No = "";
    public string TransID = "";
    public string IC_No = "";
    public string distid = "";

    public string challan = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
           
            Ro_No = Session["RO_No"].ToString();
            TO_No = Session["TO_No"].ToString();
            distid = Session["Dist"].ToString();
            TransID = Session["Trans"].ToString();
            IC_No = Session["TIC"].ToString();
            challan = Session["Challan"].ToString();
            txtqtysend.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtnobags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");



            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
           
            if (Session["dc_id"] != null)
            {
                //Session.Abandon();
                //Session.RemoveAll();
            }


            if (!IsPostBack)
            {
                //GetRO();
                Transport();
                GetGunny();
                GetDist();
                GetCategory();
                GetData();
                GetFCIdist();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    //void GetRO()
    //{
    //    //ddlrono.Items.Insert(0, "--Select--");
    //    //obj = new LARO(ComObj);
    //    //string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'";
    //    //DataSet ds = obj.selectAny(qry);
    //    //ddlrono.DataSource = ds.Tables[0];
    //    //ddlrono.DataTextField = "RO_No";
    //    //ddlrono.DataValueField = "Allot_month";
    //    //ddlrono.DataBind();
    //    ddlrono.Items.Insert(0, "--Select--");
    //    string qry = "SELECT RO_No,Allot_month FROM dbo.RO_of_FCI where Distt_Id='" + distid + "'";
    //    cmd.Connection = con;
    //    cmd.CommandText = qry;
    //    con.Open();
    //    dr = cmd.ExecuteReader();
    //    while (dr.Read())
    //    {
    //        ddlrono.Items.Add(dr["RO_No"].ToString());



    //    }
    //    dr.Close();
    //    con.Close();

    //}
    void GetCategory()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        DataSet ds = mobj.selectAny(qry);

        ddlcategory.DataSource = ds.Tables[0];
        ddlcategory.DataTextField = "Category_Name";
        ddlcategory.DataValueField = "Category_Id";
        ddlcategory.DataBind();
        ddlcategory.Items.Insert(0, "--Select--");

    }

    void GetData()
    {
        
            obj = new LARO(ComObj);
            string qryall = "SELECT RO_of_FCI.Commodity AS Expr1, RO_of_FCI.Distt_Id,RO_of_FCI.RO_Validity, RO_of_FCI.RO_No, RO_of_FCI.RO_date, RO_of_FCI.RO_qty,RO_of_FCI.RO_district, RO_of_FCI.Scheme as Scheme, RO_of_FCI.Rate, RO_of_FCI.Amount, RO_of_FCI.Allot_month,RO_of_FCI.Allot_year, RO_of_FCI.DD_chk_no, RO_of_FCI.DD_chk_date, RO_of_FCI.Remarks, RO_of_FCI.Created_date,RO_of_FCI.updated_date, RO_of_FCI.deleted_date, RO_of_FCI.Balance_Qty,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,dbo.tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name  FROM dbo.RO_of_FCI Left JOIN dbo.tbl_MetaData_STORAGE_COMMODITY    ON RO_of_FCI.Commodity = dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on RO_of_FCI.Scheme=dbo.tbl_MetaData_SCHEME.Scheme_Id  where  RO_of_FCI.RO_No='" + Ro_No + "' and RO_of_FCI.Distt_Id='" + distid + "'";
            DataSet ds = obj.selectAny(qryall);
            DataRow dr = ds.Tables[0].Rows[0];
            txtrono.Text = Ro_No;
            txtrono.Enabled = false;
            txtrono.BackColor = System.Drawing.Color.Wheat;
            string rdate = dr["RO_date"].ToString();
            string rodate = getdate(rdate);
            txtrodate.Text = rodate;
            txtrodate.ReadOnly = true;
            txtrodate.BackColor = System.Drawing.Color.Wheat;

            roqty = dr["RO_qty"].ToString();

            txtroqty.Text = dr["RO_qty"].ToString();

            txtroqty.ReadOnly = true;
            txtroqty.BackColor = System.Drawing.Color.Wheat;

            txtcomdty.Text = dr["Commodity_Name"].ToString();
            txtcomdty.ReadOnly = true;
            txtcomdty.BackColor = System.Drawing.Color.Wheat;

            txtscheme.Text = dr["Scheme_Name"].ToString();
            txtscheme.ReadOnly = true;
            txtscheme.BackColor = System.Drawing.Color.Wheat;

            txtbalqty.Text = System.Math.Round(CheckNull(dr["Balance_Qty"].ToString()), 5).ToString();
            txtbalqty.ReadOnly = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;


            obj = new LARO(ComObj);
            string qrylaro = "SELECT Lift_A_RO.*,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,dbo.tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name ,Transporter_Table.Transporter_Name as Transporter_Name,districtsmp.district_name as district_name ,tbl_MetaData_DEPOT.DepotName as DepotName   FROM dbo.Lift_A_RO left join dbo.tbl_MetaData_STORAGE_COMMODITY  on Lift_A_RO.Commodity=dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on Lift_A_RO.Scheme=dbo.tbl_MetaData_SCHEME .Scheme_Id left join dbo.Transporter_Table on Lift_A_RO.Transporter=Transporter_Table.Transporter_ID left join pds.districtsmp on Lift_A_RO.Send_District= districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on Lift_A_RO.Issue_center=dbo.tbl_MetaData_DEPOT.DepotID where Dist_Id='" + distid + "'and Ro_No='" + Ro_No + "'and Challan_No='" + challan + "'";
            DataSet dsl = obj.selectAny(qrylaro);
            DataRow drl = dsl.Tables[0].Rows[0];

            ddldistrict.SelectedItem.Text = drl["district_name"].ToString();
            ddldepottype.SelectedItem.Text = drl["FCIdepotype"].ToString();
            ddldistrict.SelectedValue = drl["Send_District"].ToString();
            ddlfcidist.SelectedItem.Text = drl["FCIdistrict"].ToString();
            //ddlfcidist.SelectedValue = drl["FCIdistrict"].ToString();
            ddlfcidepo.SelectedItem.Text = drl["FCIdepo"].ToString();
            //ddlfcidepo.SelectedValue = drl["FCIdepo"].ToString();
            ddlissue.SelectedItem.Text = drl["DepotName"].ToString();
             ddlissue.SelectedValue  = drl["Issue_center"].ToString();


            txttrans.SelectedItem.Text = drl["Transporter_Name"].ToString();
             txttrans.SelectedValue = drl["Transporter"].ToString();

            txtvehno.Text = drl["Vehicle_No"].ToString();

              txtchallan.Text  = drl["Challan_No"].ToString();
              txtchallan.ReadOnly = true;
              txtchallan.BackColor = System.Drawing.Color.Wheat;
              string chdat = drl["Challan_Date"].ToString();
              DaintyDate2.Text = chdat.ToString();
            txtqtysend.Text = System.Math.Round(CheckNull(drl["Qty_send"].ToString()), 5).ToString();
            lblfqty.Text= drl["Qty_send"].ToString();
            txtnobags.Text = drl["No_of_Bags"].ToString();
            ddlcropyear.SelectedItem .Text  = drl["Crop_year"].ToString();
              ddlcategory.SelectedItem.Text  = drl["Category"].ToString();
           txtmoisture.Text  = drl["Moisture"].ToString();
            string time = drl["Dispatch_Time"].ToString();
            string hh = time.Substring(0, 2);
            string mm = time.Substring(3, 2);
            string ampm = time.Substring(6, 2);
            ddlhour.SelectedItem.Text = hh;
            ddlminute.SelectedItem.Text = mm;
           ddlampm.SelectedItem.Text = ampm;
     

    }
    void GetDataLARO()
    {
        obj = new LARO(ComObj);
        string qryall = "SELECT Lift_A_RO.Commodity AS Expr1, Lift_A_RO.Distt_Id,Lift_A_RO.RO_Validity, Lift_A_RO.RO_No, Lift_A_RO.RO_date, Lift_A_RO.RO_qty,Lift_A_RO.RO_district, Lift_A_RO.Scheme as Scheme, Lift_A_RO.Rate, Lift_A_RO.Amount, Lift_A_RO.Allot_month,Lift_A_RO.Allot_year, Lift_A_RO.DD_chk_no, Lift_A_RO.DD_chk_date, Lift_A_RO.Remarks, Lift_A_RO.Created_date,Lift_A_RO.updated_date, Lift_A_RO.deleted_date, Lift_A_RO.Balance_Qty,dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name From dbo.Lift_A_RO Left JOIN dbo.tbl_MetaData_STORAGE_COMMODITY  ON Lift_A_RO.Commodity = dbo.tbl_MetaData_STORAGE_COMMODITY .Commodity_Id where Lift_A_RO.RO_No='" + Ro_No + "' and Lift_A_RO.Distt_Id='" + distid + "'";
        DataSet ds = obj.selectAny(qryall);
        DataRow dr = ds.Tables[0].Rows[0];
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    void GetGunny()
    {
        obj = new LARO(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GunnyBags_Type";
        DataSet ds = obj.selectAny(qry);
        ddlgtype.DataSource = ds.Tables[0];
        ddlgtype.DataTextField = "Gunny_Bags_Type";
        ddlgtype.DataValueField = "Gunny_Bags_Type_Id";
        ddlgtype.DataBind();
        ddlgtype.Items.Insert(0, "--Select--");

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
        //ddlfcidist.Items.Insert(0, "--Select--");

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
    }
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void Transport()
    {

        tobj = new Transporter(ComObj);
        string qry = "Select * from dbo.Transporter_Table where Distt_ID='" + distid +"'";
        DataSet ds = tobj.selectAny(qry);

        txttrans.DataSource = ds.Tables[0];
        txttrans.DataTextField = "Transporter_Name";
        txttrans.DataValueField = "Transporter_ID";
        txttrans.DataBind();
        txttrans.Items.Insert(0, "--Select--");

    }

    

    protected void ddlrono_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
       

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetGunny();

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void ddlgtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetDist();
    }

    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
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
        int  ValF = int .Parse(ValS);
        return ValF;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        

       
        //int roqty = int.Parse(txtbalqty.Text);
        //int sndqty = int.Parse(txtqtysend.Text);
        

            if (Page.IsValid)
            {
              
                string dist_id = distid;
                string RO_No = txtrono.Text;
                string RO_date = getmmddyy(txtrodate.Text);
                float RO_qty = CheckNull(txtroqty.Text);



                float balamt = CheckNull((txtbalqty.Text)) - CheckNull((txtqtysend.Text));

                string Balance_Qty = balamt.ToString();
                float balqty = CheckNull(Balance_Qty);
                string Transporter = txttrans.SelectedValue;
                string Vehicle_No = txtvehno.Text;
                string  Challan_No = txtchallan.Text;
                string Challan_Date = getDate_MDY(DaintyDate2.Text);
                float Qty_send = CheckNull(txtqtysend.Text);
                string Category = ddlcategory.SelectedItem.ToString();
                string Crop_year = ddlcropyear.SelectedItem.ToString();
                string Godown = txtgowdn.Text;
                string Gunny_type = ddlgtype.SelectedItem.Text;
                int No_of_Bags = CheckNullInt(txtnobags.Text);
                string Send_District = ddldistrict.SelectedValue;
                string Issue_center = ddlissue.SelectedValue;
                string Created_Date = getDate_MDY(DateTime.Today.Date.ToString());
               
                //string  mmoist=((txtmoisture.Text==null)? (txtmoisture.Text): "0" );
                //float mmoisture = float.Parse(mmoist);

                float mmoisture = CheckNull(txtmoisture.Text);
                string udate = getDate_MDY(DateTime.Today.Date.ToString()); 
                string time = (ddlhour.SelectedItem.ToString() + ":" + ddlminute.SelectedItem.ToString() + ":" + ddlampm.SelectedItem.ToString());


                string schemen = txtscheme.Text;
                string qryUpdate = "Update dbo.Lift_A_RO set Transporter='" + Transporter + "',Vehicle_No='" + Vehicle_No + "',Challan_No='" + Challan_No + "',Challan_Date='" + Created_Date + "',Qty_send=" + Qty_send + ",Category='" + Category + "',Crop_year='" + Crop_year + "',No_of_Bags=" + No_of_Bags + ",Dispatch_Time='" + time + "',Moisture=" + mmoisture + ",updated_date='" + udate + "' where Dist_Id='" + distid  + "' and RO_No='" + Ro_No + "'and Challan_No='"+challan  +"'";
                
                cmd.Connection = con;
                cmd.CommandText = qryUpdate;
               

                try
                {
                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    con.Close();
                    if (count == 1)

                    {
                        float chksendq = CheckNull(lblfqty .Text);
                        float chksendqchange=CheckNull(txtqtysend.Text);
                       decimal  uq =System.Math.Round (decimal.Parse (chksendq.ToString ()) - decimal.Parse (txtqtysend.Text),5);
                        string uquery = "update  dbo.RO_of_FCI set Balance_Qty=Round(convert(decimal(18,5),Balance_Qty)+(" + uq + "),5),IsLifted='Y' where RO_No='" + RO_No + "' and Distt_Id='" + distid + "'";
                    //string uquery = "update  dbo.RO_of_FCI set IsLifted='Y' where RO_No='" + RO_No + "' and Distt_Id='" + distid + "'";

                       try
                       {
                           if (chksendq == chksendqchange)
                           {
                           }
                           else
                           {
                               cmd.CommandText = uquery;
                               con.Open();
                               cmd.ExecuteNonQuery();
                               con.Close();

                               string qryFCI = "Update Transport_Order_againstRo set Cumulative_Qty=Round(convert(decimal(18,5),Cumulative_Qty)-(" + uq + "),5),Pending_Qty=Round(convert(decimal(18,5),Pending_Qty)+" + uq + ",5) where Trunsuction_Id='" + TransID + "' and Distt_Id='" + distid + "' and RO_NO='" + Ro_No + "' and TO_Number='" + TO_No + "' and toIssueCenter='" + IC_No + "'";
                               cmd.CommandText = qryFCI;
                               con.Open();
                               cmd.ExecuteNonQuery();
                               con.Close();

                           }
                       }
                       catch (Exception ex)
                       {
                           Label1.Text = ex.Message;
                       }
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully...'); </script> ");

                    }


                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;


                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
                }
                btnsubmit.Enabled = false;


            
        }

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFCIdepot();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}
