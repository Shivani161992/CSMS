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

public partial class Admin_Update_Stock_Register : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
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
    public string adminid = "";
    DataTable dt = new DataTable();
    float disqty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
            //txtbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            //txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            //txtbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            //txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         

            if (!IsPostBack)
            {

                
                GetCommodity();
                         
                GetDist();
                GetScheme();
                GetSource();
                ddldistrict.SelectedValue = distid;
               
                               ddd_allot_year.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddd_allot_year.Items.Add(DateTime.Today.Year.ToString());
                ddd_allot_year.SelectedIndex = 1;
                ddl_allot_month.SelectedIndex = DateTime.Today.Month - 1;
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
   
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue  + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
   
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];

        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string mmcom = ddlcomdty.SelectedValue;
        string mmdist = ddldistrict.SelectedValue;
        string mmscheme = ddlscheme.SelectedValue;
        string mmissie = ddlissue.SelectedValue;
        string mmonth = ddl_allot_month.SelectedValue;
        string mmyear = ddd_allot_year.SelectedValue;
        string qrypos = "select Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Received_RailHead,Received_CMR,Received_Levy,Recieved_Other_Src from dbo.tbl_Stock_Registor where DistrictId='" + mmdist + "'and DepotID='" + mmissie + "'and Commodity_ID='" + mmcom + "' and Scheme_ID='" + mmscheme  + "' and Month=" + mmonth + " and Year=" + mmyear; //and Stock_Date='" + ddd +"'";
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrypos);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {
                string qryinsr = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + mmdist + "','" + mmissie + "','" + mmcom + "','" + mmscheme + "'," + CheckNull(txtopenbal.Text) + "," + CheckNull(txtrecdproc.Text) + "," + CheckNull(txtrecdother.Text) + "," + CheckNull(txtrecdfci.Text) + "," + CheckNull(txtrecdosch.Text) + "," + CheckNull(txtrecdOS.Text) + "," + CheckNull(txtrecdRH.Text) + "," + CheckNull(txtrecdCMR.Text) + "," + CheckNull(txtrecdLR.Text) + "," + CheckNull(txtdistribution.Text) + "," + CheckNull(txtsaltoother.Text) + "," + CheckNull(txttranssch.Text) + "," + mmonth + "," + mmyear + ",'')";

                cmd.Connection = con;
                cmd.CommandText = qryinsr;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted  Successfully......'); </script> ");
                    //btnsubmit.Enabled = false;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    con.Close();
                }
               
               


            }

            else
            {
                string qryUpdate = "Update tbl_Stock_Registor set Opening_Balance=" + CheckNull(txtopenbal.Text) + ",Recieved_Procure=" + CheckNull(txtrecdproc.Text) + ",Recieved_Otherg=" + CheckNull(txtrecdother.Text) + ",Recieved_FCI=" + CheckNull(txtrecdfci.Text) + ",Received_OtherSch=" + CheckNull(txtrecdosch.Text) + ",Sale_Do=" + CheckNull(txtdistribution.Text) + ",Sale_otherg=" + CheckNull(txtsaltoother.Text) + ",Transfer_OtherSch=" + CheckNull(txttranssch.Text) + ",Recieved_Other_Src=" + CheckNull(txtrecdOS.Text) + ",Received_RailHead=" + CheckNull(txtrecdRH.Text) + ",Received_CMR=" + CheckNull(txtrecdCMR.Text) + ",Received_Levy=" + CheckNull(txtrecdLR.Text) + "  where DistrictId='" + mmdist + "'and DepotID='" + mmissie + "'and Commodity_ID='" + mmcom + "' and Scheme_ID='"+mmscheme+"' and Month=" + mmonth + " and Year=" + mmyear; ;

                cmd.Connection = con;
                cmd.CommandText = qryUpdate;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully......'); </script> ");
                    //btnsubmit.Enabled = false;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    con.Close();
                }

            }


        }
       

    }
    protected void ddldistric_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
  
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {
         
    }
   
  
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/Admin/AdminWelcome.aspx");
    }
    protected void ddlcommodityd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {
      
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
    protected void ddd_allot_year_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_allot_month_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mcom=ddlcomdty.SelectedValue;
        string mscheme =ddlscheme.SelectedValue ;
        string mdist=ddldistrict.SelectedValue;
        string missie=ddlissue.SelectedValue;
        string month=ddl_allot_month.SelectedValue;
        string myear= ddd_allot_year.SelectedValue;
        string qrypos = "select Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Received_RailHead,Received_CMR,Received_Levy,Recieved_Other_Src from dbo.tbl_Stock_Registor where DistrictId='" + mdist + "'and DepotID='" + missie + "'and Commodity_ID='" + mcom + "' and Scheme_ID='"+mscheme +"' and Month=" + month + " and Year=" + myear; //and Stock_Date='" + ddd +"'";
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrypos);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {
                txtopenbal.Text = "0";
                txtrecdproc.Text = "0";
                txtrecdother.Text = "0";
                txtrecdfci.Text = "0";
                txtrecdosch.Text = "0";
                txtdistribution.Text = "0";
                txtsaltoother.Text = "0";
                txttranssch.Text = "0";
                txtrecdCMR.Text = "0";
                txtrecdLR.Text = "0";
                txtrecdOS.Text = "0";
                txtrecdRH.Text = "0";


            }

            else
            {
                DataRow drpos = dspos.Tables[0].Rows[0];
                txtopenbal.Text = drpos["Opening_Balance"].ToString();
                txtrecdproc.Text = drpos["Recieved_Procure"].ToString();
                txtrecdother.Text = drpos["Recieved_Otherg"].ToString();
                txtrecdfci.Text = drpos["Recieved_FCI"].ToString();
                txtrecdosch.Text = drpos["Received_OtherSch"].ToString();
                txtdistribution.Text = drpos["Sale_Do"].ToString();
                txtsaltoother.Text = drpos["Sale_otherg"].ToString();
                txttranssch.Text = drpos["Transfer_OtherSch"].ToString();
                txtopenbal.Text = drpos["Opening_Balance"].ToString();
                txtrecdCMR.Text = drpos["Received_CMR"].ToString();
                txtrecdLR.Text = drpos["Received_Levy"].ToString();
                txtrecdOS.Text = drpos["Recieved_Other_Src"].ToString();
                txtrecdRH.Text = drpos["Received_RailHead"].ToString();


            }


        }
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string comdty = ddlcomdty.SelectedValue;
        string scheme = ddlscheme.SelectedValue;
        string distid = ddldistrict.SelectedValue;
        string issueid = ddlissue.SelectedValue;
        string qrypos = "select Sum(Quantity) as Oqty from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + comdty + "' and Scheme_Id ='" + scheme + "'";
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrypos);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {



            }

            else
            {
                DataRow drpos = dspos.Tables[0].Rows[0];
                txtgetopening.Text = drpos["Oqty"].ToString();


            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string comdty = ddlcomdty.SelectedValue;
        string scheme = ddlscheme.SelectedValue;
        string distid = ddldistrict.SelectedValue;
        string issueid = ddlissue.SelectedValue;
        string source = ddlsarrival.SelectedValue;
        int month = int.Parse(ddl_allot_month.SelectedValue);
        int year = int.Parse(ddd_allot_year.SelectedValue);
        string qrystock = "select Sum(Recd_Qty) as Qty from dbo.tbl_Receipt_Details where Commodity='" + comdty  + "' and Scheme ='" + scheme  + "' and Dist_Id='" + distid + "'and Depot_ID='" + issueid  + "' and S_of_arrival='" + ddlsarrival.SelectedValue + "' and Month=" + month + "and Year=" + year;
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrystock);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {



            }

            else
            {
                DataRow drpos = dspos.Tables[0].Rows[0];
                txtgreceipt.Text = drpos["Qty"].ToString();


            }

        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string comdty = ddlcomdty.SelectedValue;
        string scheme = ddlscheme.SelectedValue;
        string distid = ddldistrict.SelectedValue;
        string issueid = ddlissue.SelectedValue;
        string source = ddlsarrival.SelectedValue;
        int month = int.Parse(ddl_allot_month.SelectedValue);
        int year = int.Parse(ddd_allot_year.SelectedValue);
        string qrytc = "select Sum(Qty_send) as Qty from dbo.SCSC_Truck_challan where Commodity ='" + comdty + "' and Scheme='"+ scheme +"'  and Dist_ID='" + distid + "'and Depot_Id='" + issueid + "'and Month=" + month + "and Year=" + year;
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrytc);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {



            }

            else
            {
                DataRow drpos = dspos.Tables[0].Rows[0];
                txtgettruck.Text = drpos["Qty"].ToString();


            }

        }

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
         string mmcom = ddlcomdty.SelectedValue;
        string mmdist = ddldistrict.SelectedValue;
        string mmscheme = ddlscheme.SelectedValue;
        string mmissie = ddlissue.SelectedValue;
        string mmonth = ddl_allot_month.SelectedValue;
        string mmyear = ddd_allot_year.SelectedValue;
        string qrypos = "SELECT Sum(issue_against_do.qty_issue) as IssueQty FROM delivery_order_mpscsc Inner JOIN issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND  delivery_order_mpscsc.district_code = issue_against_do.district_code AND  delivery_order_mpscsc.issueCentre_code = issue_against_do.issueCentre_code AND  delivery_order_mpscsc.allotment_month = issue_against_do.allotment_month AND  delivery_order_mpscsc.allotment_year = issue_against_do.allotment_year where  delivery_order_mpscsc.district_code='" + mmdist + "' and  delivery_order_mpscsc.issueCentre_code='" + mmissie + "' and delivery_order_mpscsc.commodity_id='" + mmcom + "' and delivery_order_mpscsc.scheme_id='" + mmscheme + "' and delivery_order_mpscsc.allotment_month='" + mmonth + "' and delivery_order_mpscsc.allotment_year='"+ mmyear +"'"; 
        mobj = new MoveChallan(ComObj);

        DataSet dspos = mobj.selectAny(qrypos);
        if (dspos == null)
        {
        }
        else
        {
            if (dspos.Tables[0].Rows.Count == 0)
            {
                txtgettc.Text="0";
            }
            else

            {
                DataRow drdo=dspos.Tables[0].Rows[0];

                txtgettc.Text = drdo["IssueQty"].ToString();


            }




            }
        
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string comdty = ddlcomdty.SelectedValue;
        string scheme = ddlscheme.SelectedValue;
        string distid = ddldistrict.SelectedValue;
        string issueid = ddlissue.SelectedValue;
        string source = ddlsarrival.SelectedValue;
        int mmonth = int.Parse(ddl_allot_month.SelectedValue);
        int myear = int.Parse(ddd_allot_year.SelectedValue);
        string qrystock = "select Sum(Quantity) as Qty from dbo.State_Scheme_Transfer where Commodity_Id='" + comdty + "'and S_Scheme_Id='"+ scheme +"' and District_Id='" + distid + "'and Depotid='" + issueid  + "' and Month=" + mmonth  + "and Year=" + myear;
        mobj = new MoveChallan(ComObj);
        DataSet dspro = mobj.selectAny(qrystock);
        if (dspro.Tables[0].Rows.Count == 0)
        {
            txtgetscheme.Text = "0";

        }
        else
        {
            DataRow drop = dspro.Tables[0].Rows[0];

            txtgetscheme .Text= CheckNull(drop["Qty"].ToString()).ToString ();

        }


        ////

        string qrystockdest = "select Sum(Quantity) as Qty from dbo.State_Scheme_Transfer where Commodity_Id='" + comdty + "'and D_Scheme_Id='" + scheme + "' and District_Id='" + distid + "'and Depotid='" + issueid + "' and Month=" + mmonth + "and Year=" + myear;
        mobj = new MoveChallan(ComObj);
        DataSet dsprod = mobj.selectAny(qrystockdest);
        if (dspro.Tables[0].Rows.Count == 0)
        {
            txtrecdScheme.Text = "0";

        }
        else
        {
            DataRow drop = dsprod.Tables[0].Rows[0];

            txtrecdScheme.Text = CheckNull(drop["Qty"].ToString()).ToString ();

        }



    }
}
