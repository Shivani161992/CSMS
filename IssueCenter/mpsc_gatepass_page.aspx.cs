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
public partial class mpsc_gatepass_page : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    MoveChallan mobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public string sid = "";
    public string gateno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            sid = Session["issue_id"].ToString();
            //string dbname = "Warehouse";
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            //mobj = new MoveChallan(ComObj);
            //string qry = "SELECT GatePass_idFROM dbo.tbl_Receipt_Details where Depot_Id='"+ sid +"'";
            //DataSet ds = mobj.selectAny(qry);
            // GridView1.DataSource = ds.Tables[0];
            // GridView1.DataBind();
            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate2.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate2.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate2.Attributes.Add("onchange", "return chksqltxt(this)");


            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(DaintyDate1.Text);
            ctrllist.Add(DaintyDate2.Text);

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

                //fillgrid();
                GetSource();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    void GetSource()
    {
        con.Open();
      
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
        con.Close();
    }
    void fillgrid()
    {
        string fromdae = getDate_MDY(DaintyDate1.Text );
        string todate = getDate_MDY(DaintyDate2.Text);
        con.Open();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Receipt_Id FROM dbo.tbl_Receipt_Details where Depot_Id='" + sid + "'and S_of_arrival='03' and arrival_date >='" + fromdae + "'and arrival_date <='" + todate + "' order by Receipt_Id";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            lblmsg.Text = "Currently No Records Found.";
        }
        else
        {
            lblmsg.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
         con.Close();
    }
    void fillgridG()

    {
        string fromdae = getDate_MDY(DaintyDate1.Text);
        string todate = getDate_MDY(DaintyDate2.Text);
        string source=ddlsarrival.SelectedValue;
        con.Open();
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Receipt_Id FROM dbo.tbl_Receipt_Details where Depot_Id='" + sid + "'and S_of_arrival='02'and arrival_date >='" + fromdae + "'and arrival_date <='" + todate + "' order by Receipt_Id";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Text = "Currently No Records Found.";
        }
        else
        {
            lblmsg.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        con.Close();
    }
    void filltenderpurchaseby_sugar()
    {
        con.Open();
        string fromdae = getDate_MDY(DaintyDate1.Text);
        string todate = getDate_MDY(DaintyDate2.Text);
        string source = ddlsarrival.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Receipt_Id FROM dbo.tbl_Receipt_Details where Depot_Id='" + sid + "'and S_of_arrival='10'and arrival_date >='" + fromdae + "'and arrival_date <='" + todate + "' order by Receipt_Id";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Text = "Currently No Records Found.";
        }
        else
        {
            lblmsg.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        con.Close();
    }
    void fillgridP()
    {
        con.Open();
        string fromdae = getDate_MDY(DaintyDate1.Text);
        string todate = getDate_MDY(DaintyDate2.Text);
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Receipt_Id FROM dbo.SCSC_Procurement where IssueCenter_ID='" + sid + "' and Dispatch_Date >='" + fromdae + "'and Dispatch_Date <='" + todate + "' order by Receipt_Id";
        DataSet ds = mobj.selectAny(qry);
         if (ds.Tables[0].Rows.Count==0)
        {
            lblmsg.Text = "Currently No Records Found.";
        }
        else
        {
            lblmsg.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
         con.Close();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gateno = GridView1.SelectedRow.Cells[1].Text;
        Session["Receipt_ID"] = gateno;
        if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {

            GridView1.SelectedRow.Attributes.Add("onclick", "window.open('Print_Gatepass_Procurement.aspx',null,'left=200, top=10, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        else if (ddlsarrival.SelectedItem.ToString() == "From FCI")
        {
            GridView1.SelectedRow.Attributes.Add("onclick", "window.open('print_Gatepass_FCI.aspx',null,'left=250, top=0, height=550, width= 600, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        else if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
        {
            GridView1.SelectedRow.Attributes.Add("onclick", "window.open('print_Gatepass.aspx',null,'left=250, top=0, height=550, width=600, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        if (ddlsarrival.SelectedItem.ToString() == "Tender Purchase(by Road)-Sugar/Salt")
        {

            GridView1.SelectedRow.Attributes.Add("onclick", "window.open('Print_Tender_PurchasebyRoad-(Sugar).aspx',null,'left=200, top=10, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        //Response.Redirect("print_Gatepa ss.aspx");
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        

            mobj = new MoveChallan(ComObj);
            mobj.Depot_ID = sid;
            mobj.GatePass_id= gateno;
           //mobj.delete();
    
        

      
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        //Used by external paging
        if (GridView1.PageCount == 0)
        {
        }
        else
        {
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
            if (ddlsarrival.SelectedItem.ToString() == "Procurement")
            {

                fillgridP();
            }
            else
            {
                fillgrid();
            }
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void Firstbutton_Click(object sender, EventArgs e)
    {
       

    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.Text == "-Select-")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Source of Arrival'); </script> ");

        }
        else
        {
            if (ddlsarrival.SelectedItem.ToString()== "Procurement")
            {

                fillgridP();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "From FCI")
            {
                fillgrid();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "Other Depot")
            {
                fillgridG();
            }
            else if (ddlsarrival.SelectedItem.ToString() == "Tender Purchase(by Road)-Sugar/Salt")
            {

                filltenderpurchaseby_sugar();
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }
}
