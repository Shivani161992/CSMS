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
public partial class Admin_UpdateValidity : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    Districts DObj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string rono = "";
    public string distn = "";
    public string adminid = "";
    public string gatepass = "";
    public string getdatef;
    public string validdate;
    public string valid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
            distn = Session["distname"].ToString();
            distid  = Session["disid"].ToString();
            rono  = Session["rono"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
                lbldistrict.Text = distn;
                fillgrid();
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT RO_of_FCI.*,districtsmp.district_name from dbo.RO_of_FCI left join pds.districtsmp on RO_of_FCI.Distt_Id=districtsmp.district_code   where RO_of_FCI.IsExpire='Y' and  RO_of_FCI.Distt_Id='"+distid +"' and RO_of_FCI.RO_No='"+rono +"'" ;
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No Record Present Currently";

        }
        else
        {
            lblmsg.Visible = false;
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GridView1.Columns[0].Visible = false;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalamt"));

            string rodate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RO_date"));
            string validity = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RO_Validity"));


            getdatef = getdate(rodate);
            validdate = getdate(validity);

            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
            Label lblst = (Label)e.Row.FindControl("lbrovalidity");
            lblst.Text = validdate;

        }

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string get_days(DateTime fromDate, DateTime toDate)
    {

        int y1 = 0, m1 = 0, d1 = 0, y2 = 0, m2 = 0, d2 = 0;
        y1 = fromDate.Year;
        m1 = fromDate.Month;
        d1 = fromDate.Day;
        y2 = toDate.Year;
        m2 = toDate.Month;
        d2 = toDate.Day;

        int y = (y2 - y1) * 12;
        int m = (y + m2) - m1;
        int d = (m * 30) + d2;
        int day = d - d1;
        return day.ToString();
    }


   
    protected void btnGet_Click(object sender, EventArgs e)
    {


        string mrodate = "";
        string  mroqty="" ;
        string mvdate = "";
        string mrodist = "";
        string mcomdty = "";

        string mscheme = "";
        string mrate = "";
        string mamt = "";
        string mallotm = "";
        string myear="";
        string mddno = "";
        string mdddate = "";
        string mddamt = "";
        string mbank = "";
        string mremark = "";

        string mcrdate = DateTime.Now.Date.ToString();

        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());
        mobj = new MoveChallan(ComObj);
        string qryv = "SELECT * from dbo.RO_of_FCI where IsExpire='Y' and  Distt_Id='" + distid + "' and RO_No='" + rono + "'and Allot_month='" + month + "' and Allot_year='" + year + "'";
        DataSet ds = mobj.selectAny(qryv);
        if (ds.Tables[0].Rows.Count == 0)
        {
           
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
           valid=dr["RO_Validity"].ToString ();
           mvdate = dr["RO_Validity"].ToString();
           mrodate = dr["RO_date"].ToString();
          mroqty = dr["RO_qty"].ToString();
          mrodist  = dr["RO_district"].ToString();
          mcomdty  = dr["Commodity"].ToString();
          mscheme = dr["Scheme"].ToString();
          mrate  = dr["Rate"].ToString();
          mamt  = dr["Amount"].ToString();
          mallotm  = dr["Allot_month"].ToString();
          myear  = dr["Allot_year"].ToString();
          mddno = dr["DD_chk_no"].ToString();
           mdddate  = dr["DD_chk_date"].ToString();
          mddamt = dr["DD_chk_Amount"].ToString();
         mbank  = dr["Bank_ID"].ToString();
          mremark = dr["Remarks"].ToString();
          

        }
      
        DateTime fdate = new DateTime();
        DateTime tdate = new DateTime();
        fdate = Convert.ToDateTime(DaintyDate2.Text);
        tdate = Convert.ToDateTime (valid);

        string validity = get_days(tdate,fdate);
        string vdate = getDate_MDY(DaintyDate2.Text);
        if (int.Parse(validity) > 2)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Only 2 Days Allowed....'); </script> ");

        }
        else
        {

            string update = "Update RO_of_FCI set RO_Validity ='" + vdate + "',IsExpire='N'  where Distt_Id='" + distid + "'and RO_No='" + rono + "' and Allot_month='" + month + "' and Allot_year='" + year+"'";
            cmd.Connection = con;
            cmd.CommandText = update;
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;
            try
            {
                cmd.ExecuteNonQuery();

                string trans = "Update";
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string qryTrans = "insert into dbo.tbl_ROFCI_Transuction(Distt_Id,RO_No,RO_date,RO_qty,RO_Validity,RO_district,Commodity,Scheme,Rate,Amount,Allot_month,Allot_year,DD_chk_no,DD_chk_date,DD_chk_Amount,Bank_ID,Remarks,Operation,Trans_Date,IP_Address,User_ID)values('" + distid + "','" + rono + "','" + mrodate + "'," + mroqty + ",'" + mvdate + "','" + mrodist + "','" + mcomdty + "','" + mscheme + "'," + mrate + "," + mamt + ",'" + mallotm + "','" + myear + "','" + mddno + "','" + mdddate + "'," + mddamt + ",'" + mbank + "','" + mremark + "','" + trans + "',getdate(),'" + ip + "','" + adminid + "')";
                cmd.CommandText = qryTrans;
                cmd.Transaction = trns;
                cmd.ExecuteNonQuery();


                string uto = "update dbo.Transport_Order_againstRo set RO_Validity='" + validdate + "' where Distt_Id='" + distid + "' and RO_No='" + rono + "'and Month="+month +" and Year="+year ;
                cmd.CommandText = uto;
                cmd.Transaction = trns;
                cmd.ExecuteNonQuery();

                string utoal = "update dbo.TO_Allot_Lift set RO_Validity='" + validdate + "' where Distt_Id='" + distid + "' and RO_No='" + rono + "'and Month="+month +" and Year="+year ;
                cmd.CommandText = utoal;
                cmd.Transaction = trns;
                cmd.ExecuteNonQuery();

                trns.Commit();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Validity Updated Successfully.....'); </script> ");
                btnGet.Enabled = false;




            }
            catch (Exception ex)
            {
                trns.Rollback();
                lblmsg.Visible = true;
                lblmsg.Text = ex.Message;


            }
            finally
            {
                con.Close();
                ComObj.CloseConnection();
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/Admin/AdminWelcome.aspx");
    }
}
