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
public partial class District_Truckchallan_lifting_Ro : System.Web.UI.Page
{

    Transporter tobj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string issueid = "";
    DataTable dt = new DataTable();
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string challan = "";
    public string truckno = "";
    public string gatepass = "";
    public int getnum;
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            txtchallanno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
           
            txtchallanno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txttruckno.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           

            if (Page.IsPostBack == false)
            {
                Fillgrid();

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }



    }
    void Fillgrid()
    {
        tobj = new Transporter(ComObj);
        string query = "select * from dbo.Truckwise_Ro_Detail  where Distt_Id='" + distid + "'";
        //       string qrychallan = "SELECT Lift_A_RO.Challan_No, Lift_A_RO.Vehicle_No, Lift_A_RO.RO_No, Lift_A_RO.RO_Date, Lift_A_RO.RO_Qty,Lift_A_RO.Commodity, Lift_A_RO.Scheme, Lift_A_RO.Qty_send, Lift_A_RO.Balance_Qty, Lift_A_RO.Transporter,Truckwise_Ro_Detail.Truck_No, Truckwise_Ro_Detail.Truck_Challan FROM Lift_A_RO Left JOIN Truckwise_Ro_Detail ON Lift_A_RO.Dist_Id=Truckwise_Ro_Detail.Distt_Id";

        DataSet ds = tobj.selectAny(query);

         if (ds.Tables[0].Rows.Count==0)
        {
            Label1.Text = "Currently no record is present";
            Label1.Visible = true;
            btnupdate.Visible = false;

        }
        else
        {
            Label1.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            btnupdate.Visible = false;

        }


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "challan_date"));
            string comdty = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Commodity"));
            getdatef = getdate(griddate);
            string comname = Commodity_Name(comdty);


            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
            Label lblcomdty = (Label)e.Row.FindControl("lblcomdty");
            lblcomdty.Text = comname;

            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));
            //decimal rowTotalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "quantity"));

            //grdTotal = grdTotal + rowTotal;
            //grdTotalQty = grdTotalQty + rowTotalQty;

        }
    }

    public string Commodity_Name(string comid)
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT Commodity_Name FROM dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + comid + "'";
        DataSet ds = mobj.selectAny(qry);
        DataRow dr = ds.Tables[0].Rows[0];
        return dr["Commodity_Name"].ToString();
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
            Fillgrid();
        }
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string getmmddyy(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("MM/d/yyyy");
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string mchalan = txtchallanno.Text;
        string mtruckmo = txttruckno.Text;

       

        mobj1 = new MoveChallan(ComObj);
        string laroqry = "select * from dbo.Lift_A_RO where Dist_Id='" + distid + "' and Challan_No='" + mchalan + "'and Vehicle_No='" + mtruckmo + "'";
        DataSet dsl = mobj1.selectAny(laroqry);
       if (dsl.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No Entry Made by lifting against RO ......'); </script> ");

        }
        else
        {

            DataRow drl = dsl.Tables[0].Rows[0];

           
            string mtcdate = drl["Challan_Date"].ToString();
            string mtransporter = drl["Transporter"].ToString();
            string mrono = drl["RO_No"].ToString();
            int  mbags = int.Parse (drl["No_of_Bags"].ToString());
            float mroqty = float.Parse(drl["Qty_send"].ToString());
            string mcmdty = drl["Commodity"].ToString();


            string state = "23";
            mobj1 = new MoveChallan(ComObj);
            string qrey = "select max(Transuction_Id) as Transuction_Id  from dbo.Truckwise_Ro_Detail where  Distt_Id='" + distid + "'";
            DataSet ds = mobj1.selectAny(qrey);
            DataRow dr = ds.Tables[0].Rows[0];
            gatepass = dr["Transuction_Id"].ToString();
            if (gatepass == "")
            {
                gatepass = state + distid + "1";

            }
            else
            {
                getnum = Convert.ToInt32(gatepass);
                getnum = getnum + 1;
                gatepass = getnum.ToString();


            }

            string challanno = txtchallanno.Text;
            string mtruckno = txttruckno.Text;
            string Created_Date = DateTime.Today.Date.ToString();
            string mudate = "";
            string mtransutid = gatepass;
            string mcby = issueid;

            //string mgfdate = DateTime.Today.Date.ToString();

            string mddate = "";




            string qyr = "insert into dbo.Truckwise_Ro_Detail(Distt_Id,Truck_Challan,Truck_No,RO_No,Challan_Date,Transporter,No_of_Bags,Qty_send,Commodity,Transuction_Id,Created_Date,Updated_Date,Deleted_Date)values('" + distid + "','" + challanno + "','" + mtruckno + "','" + mrono + "','" + mtcdate + "','" + mtransporter + "'," + mbags + "," + mroqty + ",'" + mcmdty + "','" + gatepass + "','" + Created_Date + "',getdate(),'" + mddate + "'" + ")";

            cmd.Connection = con;
            cmd.CommandText = qyr;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully......'); </script> ");
                txtchallanno.Text = "";
                txttruckno.Text = "";
                txtchallanno.Focus();
                Fillgrid();
            }
            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = ex.Message;

            }
            finally
            {
                con.Close();
                ComObj.CloseConnection();

            }
            con.Open();

            btnaddnew.Visible = true;
            lbltname.Visible = false;
            lbltadd.Visible = false;
            txtchallanno.Visible = false;
            txttruckno.Visible = false;
            btnadd.Visible = false;
            btnclose.Visible = false;
            btnupdate.Visible = false;





        }





        
    }
    protected void btnaddnew_Click1(object sender, EventArgs e)
    {
        Label1.Visible = false;
        lbltname.Visible = true;
        lbltadd.Visible = true;
        txtchallanno.Visible = true;
        txttruckno.Visible = true;
        btnadd.Visible = true;
        btnclose.Visible = true;
        txtchallanno.Focus();
        btnaddnew.Visible = false;
        btnupdate.Visible = false;

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }


    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Wheat;
        challan = GridView1.SelectedRow.Cells[1].Text;
        truckno = GridView1.SelectedRow.Cells[2].Text;

        txttruckno.BackColor = System.Drawing.Color.Wheat;
        txtchallanno.Text = challan;
        txttruckno.Text = truckno;
        btnaddnew.Visible = false;
        btnupdate.Visible = true;
        txtchallanno.Visible = true;
        txttruckno.Visible = true;
        lbltname.Visible = true;
        lbltadd.Visible = true;
        txtchallanno.Enabled = false;
        txttruckno.Focus();
        //btnadd.Visible = false;
        //btnclose.Visible = false;
        //Button1.Visible = true;
        //txttadd.Visible = false;
        //lbltadd.Visible = false;

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
