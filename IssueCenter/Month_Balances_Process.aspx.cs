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
public partial class Month_Balances_Process : System.Web.UI.Page
  {

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    DistributionCenters distobj = null;
  
    Districts DObj = null;
   
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
  
    MoveChallan mobj = null;
    string distid = "";
    string issueid = "";
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());


          

            if (!IsPostBack)
            {
                ddlopeningmonth.SelectedIndex = DateTime.Today.Month - 1;

                ddlprevyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlprevyear.Items.Add(DateTime.Today.Year.ToString());
                ddlprevyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) +1).ToString());
                ddlprevyear.SelectedIndex = 1;

                ddlnextyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) - 1).ToString());
                ddlnextyear.Items.Add(DateTime.Today.Year.ToString());
                ddlnextyear.Items.Add((int.Parse(DateTime.Today.Year.ToString()) + 1).ToString());
                ddlnextyear.SelectedIndex = 1;


               
             
            }
            

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }



       

    }
  

   
   
    //void GetGodown()
    //{
    //    mobj = new MoveChallan(ComObj);
    //    string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='"+issueid +"'";
    //    DataSet ds = mobj.selectAny(qry);

    //    ddlgodown.DataSource = ds.Tables[0];
    //    ddlgodown.DataTextField = "Godown_Name";
    //    ddlgodown.DataValueField = "Godown_ID";
    //    ddlgodown.DataBind();
    //    ddlgodown.Items.Insert(0, "--Select--");


    //}
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    //void fillGrid()
    //{
    //    string mcomid = ddlcomdty.SelectedValue;
    //    string mcatid = ddlcategory.SelectedValue;
    //    string mscheme = ddlscheme.SelectedValue;
    //    int year = int.Parse(DateTime.Today.Date.Year.ToString());
    //    int month = int.Parse(DateTime.Today.Date.Month.ToString());
    //    string qrychk = "select issue_opening_balance.* ,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name ,tbl_MetaData_DCP_S.Scheme_Name as Scheme_Name,Source_Arrival_Type.Source_Name as Source_Name  from dbo.issue_opening_balance left join dbo.tbl_MetaData_STORAGE_COMMODITY on issue_opening_balance.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_DCP_S on issue_opening_balance.Scheme_Id =tbl_MetaData_DCP_S.Scheme_Id left join dbo.Source_Arrival_Type on issue_opening_balance.Source=Source_Arrival_Type.Source_ID  where District_Id='" + distid + "'and Depotid='" + issueid + "'and Month=" + month + "and Year=" + year;
    //    mobj = new MoveChallan(ComObj);
    //    DataSet dschk = mobj.selectAny(qrychk);
    //    if (dschk==null)
    //    {
    //    }
    //    else
    //    {
    //        GridView1.DataSource = dschk.Tables[0];
    //        GridView1.DataBind();
    //    }
    //}
    void GetBalance()
    {
        int prevmonth = int.Parse (ddlopeningmonth.SelectedValue);
        int nextmonth = int.Parse (ddlnextmonth.SelectedValue);
        int prevyear = int.Parse(ddlprevyear.SelectedValue);
        int nextyear = int.Parse(ddlnextyear.SelectedValue);



        int year = int.Parse(DateTime.Today.Date.Year.ToString());
        int month = int.Parse(DateTime.Today.Date.Month.ToString());

        if (prevyear < nextyear)
        {
            if (prevmonth > nextmonth)
            {
                string qrychk = "select issue_opening_balance.* from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Month=" + prevmonth;

                mobj = new MoveChallan(ComObj);
                DataSet ds = mobj.selectAny(qrychk);
                if (ds == null)
                {
                }
                else
                {
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count;i++ )
                    {
                        DataRow drch = ds.Tables[0].Rows[i];
                        string comdty = drch["Commodity_Id"].ToString();
                        string scheme = drch["Scheme_Id"].ToString();
                        string cat = drch["Category_Id"].ToString();
                        string crop = drch["Crop_year"].ToString();
                        string bags = drch["Bags"].ToString();
                        string qty = drch["Quantity"].ToString();
                        string source = drch["Source"].ToString();
                        string cbalance = drch["Current_Balance"].ToString();
                        string stldt = drch["Stock_Date"].ToString();
                        i = i + 1;
                    }

                }
            }



        }
        else
        {
            if (prevmonth < nextmonth)
            {
                
                    string qrychk = "select issue_opening_balance.* from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Month=" + prevmonth;

                    mobj = new MoveChallan(ComObj);
                    DataSet ds = mobj.selectAny(qrychk);
                    if (ds == null)
                    {
                    }
                    else
                    {

                    }

               

            }
        }

        
      
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
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
       
    }
   
    protected string changeDateDesc(DateTime inDate, int inDays)
    {
        int noofdays = DateTime.DaysInMonth(inDate.Year, inDate.Month);
        int count = 1;
        int xday = inDate.Day;
        int xmonth = inDate.Month;
        int xyear = inDate.Year;
        while (count <= inDays)
        {
            xday = xday - 1;
            if (xday < 1)
            {
                xmonth = xmonth - 1;
                if (xmonth < 1)
                {
                    xyear = xyear - 1;
                    xmonth = 12;
                }
                noofdays = DateTime.DaysInMonth(xyear, xmonth);
                xday = DateTime.DaysInMonth(xyear, xmonth);
               
            }
            
            count = count + 1;
        }
        return (xday + "/" + xmonth + "/" + xyear);
    }

     
   
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        GetBalance();
    //    string mstate = "23";
    //    string mcomid = ddlcomdty.SelectedValue;
    //    string mcatid = ddlcategory.SelectedValue;
    //    string mscheme = ddlscheme.SelectedValue;
    //    string msource = ddlsarrival.SelectedValue;

    //    string cdate = DateTime.Today.Date.ToString();
    //    float qty = float .Parse (txtqty.Text);
    //    string udate = "";
    //    string ddate = "";
    //    string mcrop = ddlcropyear.SelectedItem.ToString();
    //    string opnmonth = ddlopeningmonth.SelectedValue;
    //    string mgodown = ddlissue.SelectedValue;
    //    string opnyear = DateTime.Today.Year.ToString ();
    //    int year = int.Parse(opnyear);
    //    int omonth = int.Parse(opnmonth);
    //    int mbags = CheckNullInt(txtbags.Text);
    //    float cbalance = float.Parse(txtqty.Text);
    //    string pdate = getDate_MDY(effective_from.Text);
    //    int month = int.Parse(DateTime.Today.Date.Month.ToString());
    //    string source = ddlsarrival.SelectedValue;
    //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //    if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlsarrival.SelectedItem.Text =="--Select--" )
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please  Select Source of Arrival/Commodity/Scheme......'); </script> ");
    //    }
    //    else
    //    {
        

    //           string qrychk = "select Quantity  from dbo.issue_opening_balance where Commodity_Id ='" + mcomid + "'and Scheme_Id='"+ mscheme +"'and District_Id='" + distid + "'and Depotid='" + issueid + "'and Month=" + month + "and Year=" + year+" and Source='"+source +"'";
    //            mobj = new MoveChallan(ComObj);
    //            DataSet dschk = mobj.selectAny(qrychk);
    //            if (dschk.Tables[0].Rows.Count == 0)
    //            {
    //                string qrey = "insert into dbo.issue_opening_balance(State_Id,District_Id,Depotid,Commodity_Id,Scheme_Id,Category_Id,Godown,Crop_year,Bags,Quantity,Source,Current_Balance,Month,Year,IP_Address,Stock_Date,CreatedDate,UpdatedDate,DeletedDate) values('" + mstate + "','" + distid + "','" + issueid + "','" + mcomid + "','" + mscheme + "','" + mcatid + "','" + mgodown + "','" + mcrop + "'," + mbags + "," + qty + ",'" + msource + "'," + cbalance + "," + omonth + "," + year + ",'" + ip + "','" + pdate + "',getdate(),'" + udate + "','" + ddate + "'" + ")";
    //                cmd.CommandText = qrey;
    //                cmd.Connection = con;
    //                try
    //                {

    //                    con.Open();
    //                    int count = cmd.ExecuteNonQuery();
    //                    con.Close();
    //                    if (count >= 1)
    //                    {
    //                        string qrystock = "select Sum(Quantity) as Qty from dbo.issue_opening_balance where Commodity_Id ='" + mcomid + "'and District_Id='" + distid + "'and Depotid='" + issueid + "'and Month=" + month + "and Year=" + year;
    //                        mobj = new MoveChallan(ComObj);
    //                        DataSet ds = mobj.selectAny(qrystock);
    //                         if (ds.Tables[0].Rows.Count==0)
    //                        {

    //                        }
    //                        else
    //                        {
    //                            DataRow drop = ds.Tables[0].Rows[0];
    //                            float mobal = CheckNull(drop["Qty"].ToString());
    //                            float mrp = 0;
    //                            float mrod = 0;
    //                            float msod = 0;
    //                            float msdelo = 0;
    //                            float mrfci = 0;
    //                            float mros = 0;
    //                            float msos = 0;
    //                            string mremark = "";
    //                            string qryinsopen = "select * from dbo.tbl_Stock_Registor where Commodity_Id ='" + mcomid + "'and DistrictId ='" + distid  + "'and DepotID='" + issueid  + "'and Month=" + month + "and Year=" + year;
    //                            mobj = new MoveChallan(ComObj);
    //                            DataSet dsopen = mobj.selectAny(qryinsopen);

    //                            if (dsopen.Tables[0].Rows.Count == 0)
    //                            {
    //                                string qryins = "insert into dbo.tbl_Stock_Registor(DistrictId,DepotID,Commodity_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Sale_Do,Sale_otherg,Transfer_OtherSch,Month,Year,Remarks) Values('" + distid + "','" + issueid  + "','" + mcomid  + "'," + mobal + "," + mrp + "," + mrod + "," + mrfci + "," + mros + "," + msdelo + "," + msod + "," + msos + "," + month  + "," + year + ",'" + mremark + "')";
    //                                cmd.CommandText = qryins;
    //                                con.Open();
    //                                cmd.ExecuteNonQuery();
    //                                con.Close();

    //                            }
    //                            else
    //                            {
    //                                string qryinsU = "update dbo.tbl_Stock_Registor set Opening_Balance=" + mobal + " where Commodity_Id ='" + mcomid + "'and DistrictId='" + distid + "'and DepotID='" + issueid + "'and Month=" + month + "and Year=" + year;
    //                                cmd.CommandText = qryinsU;
    //                                con.Open();
    //                                cmd.ExecuteNonQuery();
    //                                con.Close();
    //                            }


    //                        }
    //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully......'); </script> ");
    //                        btnsubmit.Enabled = false;
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Label1.Visible = true;
    //                    Label1.Text = ex.Message;
    //                }
    //                finally
    //                {
    //                    ComObj.CloseConnection();
    //                }







    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Opening Already Exist For Selected Commodity/Scheme......'); </script> ");

    //            }





    //            fillGrid();
    //}
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
    //protected string getDate_MDY(string inDate)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
    //    return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    //}
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
      
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

        //    string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acceptance_Date"));

        //    getdatef = getdate(griddate);


        //    Label lbl = (Label)e.Row.FindControl("lblChallan");
        //    lbl.Text = getdatef;

        //    //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
        //    //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

        //    //grdTotal = grdTotal + rowTotal;
        //    //grdTotalQty = grdTotalQty + rowTotalQty;

        //}
    }
    protected void Lastbutton_Click(object sender, EventArgs e)
    {

    }
    protected void Nextbutton_Click(object sender, EventArgs e)
    {

    }
    protected void Prevbutton_Click(object sender, EventArgs e)
    {

    }
    protected void Firstbutton_Click(object sender, EventArgs e)
    {

    }
   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
}
