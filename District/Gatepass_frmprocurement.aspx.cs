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

public partial class District_Gatepass_frmprocurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    MoveChallan mobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public static string Dist = "";
    public string gateno = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            
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
                Dist = Session["dist_id"].ToString();
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
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }


        string qry = "SELECT * FROM dbo.Source_Arrival_Type where source_id = '01' order by Source_ID ";
        SqlCommand cmdsource = new SqlCommand(qry, con);
        SqlDataAdapter dasource = new SqlDataAdapter(cmdsource);
        DataSet ds = new DataSet();
        dasource.Fill(ds);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }


    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.Text == "-Select-")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Source of Arrival'); </script> ");

        }

        else
        {
            if (ddlsarrival.SelectedItem.ToString() == "Procurement")
            {

                fillgridP();
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    void fillgridP()
    {
        con.Open();
        string fromdae = getDate_MDY(DaintyDate1.Text);
        string todate = getDate_MDY(DaintyDate2.Text);
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Receipt_Id FROM dbo.SCSC_Procurement where IssueCenter_ID = 'BYRack' and Dispatch_Date >='" + fromdae + "'and Dispatch_Date <='" + todate + "' and Distt_ID = '"+Dist+"'  order by Receipt_Id";
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

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gateno = GridView1.SelectedRow.Cells[1].Text;
        Session["Receipt_ID"] = gateno;
        if (ddlsarrival.SelectedItem.ToString() == "Procurement")
        {
            GridView1.SelectedRow.Attributes.Add("onclick", "window.open('Print_Receipt_Procurement.aspx',null,'left=200, top=10, height=800, width= 650, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
        }
        
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        mobj = new MoveChallan(ComObj);
        mobj.Depot_ID = "BYRack";
        mobj.GatePass_id = gateno;
        //mobj.delete();
    }
}
