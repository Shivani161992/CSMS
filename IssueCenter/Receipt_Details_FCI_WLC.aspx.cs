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

public partial class IssueCenter_Receipt_Details_FCI_WLC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    
    Commodity_MP comdtobj = null;
  
    MoveChallan mobj = null;
      protected Common ComObj = null, cmn = null;
    protected Common ComObjWlC = null, cmnWLC = null;
    public string sid = "";
    public string did = "";
    public string getdatef = "";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            did = Session["dist_id"].ToString();

            txtmoisture.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtwcm.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbags.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtweight.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtchallan.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttruckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtmoisture.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtwcm.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbags.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtweight.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();


          
            ComObjWlC = new Common(ConfigurationManager.AppSettings["ConnectionWlc"].ToString());
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
                     

            if (!IsPostBack)
            {

                GetScheme();
               
                GetCommodity();
                GetCategory();
                GetGodown();
                fillGrid();
                GetName();

                dt.Columns.Add("G_Name");
                dt.Columns.Add("Stack");
                dt.Columns.Add("Bags");
                dt.Columns.Add("Weight");


                Session["dt"] = dt;

                
               
                

            }


        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");




        }

    }
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string qrySelect = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_ID";
        DataSet ds = comdtobj.selectAny(qrySelect);
     
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
    void GetScheme()
    {

        comdtobj = new Commodity_MP(ComObjWlC);
        string qrysch = "Select * from dbo.tbl_MetaData_SCHEME order by Scheme_Id ";
        DataSet ds = comdtobj.selectAny(qrysch);
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetCategory()
    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string qrycat = "SELECT * FROM dbo.tbl_MetaData_STORAGE_CATEGORY";
        DataSet ds = comdtobj.selectAny(qrycat);
        ddlcategory.DataSource = ds.Tables[0];
        ddlcategory.DataTextField = "Category_Name";
        ddlcategory.DataValueField = "Category_Id";
        ddlcategory.DataBind();


    }
    void GetGodown()
    {
        comdtobj = new Commodity_MP(ComObjWlC);
        string qrygodn = "SELECT Godown_ID,Godown_Name FROM dbo.tbl_MetaData_GODOWN where DistrictId='"+did + "' and DepotId='"+sid +"'";
        DataSet ds = comdtobj.selectAny(qrygodn);
        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");

    }
    void GetStack()

    {
        string gid = ddlgodown.SelectedValue;
        comdtobj = new Commodity_MP(ComObjWlC);
        string qrystack = "SELECT Stack_ID,Stack_Name FROM dbo.tbl_MetaData_STACK where District_Id='" + did + "' and DepotId='" + sid + "'and Godown_ID='" + gid + "'";
        DataSet ds = comdtobj.selectAny(qrystack);
        ddlstack.DataSource = ds.Tables[0];
        ddlstack.DataTextField = "Stack_Name";
        ddlstack.DataValueField = "Stack_ID";
        ddlstack.DataBind();
        ddlstack.Items.Insert(0, "--Select--");

    }
    void GetStackPos()
    {
        if (ddlstack.SelectedItem.Text == "--Select--")
        {
            txtmaxcap.Text = "00.00000";
            txtcurcap.Text = "00.00000";
        }
        else
        {
            string gid = ddlgodown.SelectedValue;
            string stackid = ddlstack.SelectedValue;
            comdtobj = new Commodity_MP(ComObjWlC);
            string qryspos = "SELECT tbl_MetaData_STACK.Stack_capacity,tbl_storage_Stacking_Details.Weight FROM dbo.tbl_MetaData_STACK left join dbo.tbl_storage_Stacking_Details on tbl_MetaData_STACK.Godown_ID=tbl_storage_Stacking_Details.Godown_ID and tbl_MetaData_STACK.Stack_ID=tbl_storage_Stacking_Details.Stack_ID where tbl_MetaData_STACK.District_Id='" + did + "' and tbl_MetaData_STACK.DepotId='" + sid + "'and tbl_MetaData_STACK.Godown_ID='" + gid + "'and tbl_MetaData_STACK.Stack_ID='" + stackid + "'";
            DataSet ds = comdtobj.selectAny(qryspos);
            if (ds == null)
            {
                txtmaxcap.Text = "00.00000";
                txtcurcap.Text = "00.00000";
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtmaxcap.Text = dr["Stack_capacity"].ToString();
                txtcurcap.Text = dr["Weight"].ToString();
                decimal avcap=decimal.Parse(txtmaxcap.Text) - decimal.Parse(txtcurcap.Text);
                txtavlcap.Text = avcap.ToString();

            }
        }
    }
    void GetData()
    {
        mobj = new MoveChallan(ComObj);

        string qrychallan = "SELECT challan_no,challan_date,Commodity,Recieved_Bags,Recd_Qty,Moisture,WCM_no,Vehile_no,Category,Crop_year FROM dbo.tbl_Receipt_Details where Dist_Id='" + did + "'and Depot_ID='" + sid + "'and IsRecieved='N' and Challan_No='";
        DataSet ds = mobj.selectAny(qrychallan);

    }
    void fillGrid()
    {
        
            mobj = new MoveChallan(ComObj);

            string qrychallan = "SELECT tbl_Receipt_Details.*,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name FROM dbo.tbl_Receipt_Details left join dbo.tbl_MetaData_STORAGE_COMMODITY on tbl_Receipt_Details.Commodity=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID where tbl_Receipt_Details.Dist_Id='" + did + "'and tbl_Receipt_Details.Depot_ID='" + sid + "'and tbl_Receipt_Details.IsDeposit='N' and tbl_Receipt_Details.S_of_arrival='03'";
           DataSet ds = mobj.selectAny(qrychallan);
            if (ds == null)
            {
            }
            else
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }

       


    }
   
    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qry1dt = "select district_name  from pds.districtsmp where district_code='" + did + "'";
        DataSet ds1dt = mobj.selectAny(qry1dt);
        DataRow dr1dt = ds1dt.Tables[0].Rows[0];
        txtdist.Text = dr1dt["district_name"].ToString();
 

        mobj = new MoveChallan(ComObj);
        string qryissue = "select DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + sid + "'";
        DataSet dsic = mobj.selectAny(qryissue);
        DataRow dric = dsic.Tables[0].Rows[0];
        txtissue.Text = dric["DepotName"].ToString();
         

      



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
    protected void btnadstack_Click(object sender, EventArgs e)
    {
        int bags = CheckNullInt(txtbags.Text);
        float weight = CheckNull(txtweight.Text);
        if (bags == 0 || weight == 0 )
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No. of Bags and Weight Required....'); </script> ");

        }
        else 
        {
            dt = (DataTable)Session["dt"];
            string gname = ddlgodown.SelectedValue;
            string stack = ddlstack.SelectedValue;

            dt.Rows.Add(gname, stack, bags, weight);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["dt"] = dt;
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetStack();
    }
    protected void ddlstack_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetStackPos();
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string challan = GridView2.SelectedRow.Cells[1].Text;
        string rono = GridView2.SelectedRow.Cells[3].Text;

        mobj = new MoveChallan(ComObj);

        string qrychallan = "SELECT challan_no,challan_date,Commodity,Recieved_Bags,Recd_Qty,Moisture,WCM_no,Vehile_no,Category,Crop_year FROM dbo.tbl_Receipt_Details where Dist_Id='" + did + "'and Depot_ID='" + sid + "'and IsDeposit='N' and Challan_No='" + challan + "' and RO_No='" + rono + "'";
        DataSet ds = mobj.selectAny(qrychallan);
        if (ds == null)
        {
        }

        else
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtchallan.Text = dr["challan_no"].ToString();
            txtwcm.Text = dr["WCM_no"].ToString();
            txtmoisture.Text = dr["Moisture"].ToString();
            txtqty.Text = dr["Recd_Qty"].ToString();
            txtbags.Text = dr["Recieved_Bags"].ToString();
            txttruckno.Text = dr["Vehile_no"].ToString();

        }


    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
}
