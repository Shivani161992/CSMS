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
public partial class State_Ratemaster_Purchase : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    //Rate_master rmobj = null;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public string dstid = "";
    public string fps_code = "";
    public string getdatef = "";
    public string stid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["st_id"] != null)
        {
            stid = Session["st_id"].ToString();

            txtrrate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtincidental.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbonus.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtgunnycap.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txturate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtrrate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txturate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtincidental.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbonus.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtgunnycap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

            if (!IsPostBack)
            {

                GetScheme();
                GetCommodity();
                Fillgrid();
            }

        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomodity.DataSource = ds.Tables[0];
        ddlcomodity.DataTextField = "Commodity_Name";
        ddlcomodity.DataValueField = "Commodity_Id";
        ddlcomodity.DataBind();
        ddlcomodity.Items.Insert(0, "--Select--");


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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlcomodity.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity /Scheme...'); </script> ");

        }
        else
        {



            string mcomdty = ddlcomodity.SelectedValue;
            string mscheme = ddlscheme.SelectedValue;
            string myear = DateTime.Today.Year.ToString();

            string qrey = "Select * from dbo.SCSC_IssueRate where Commodity_ID='" + mcomdty + "'and Scheme_ID='" + mscheme  + "' and Year='" + myear + "'";
            mobj1 = new MoveChallan(ComObj);

            DataSet ds = mobj1.selectAny(qrey);
             if (ds.Tables[0].Rows.Count==0)
            {
                string qtl = "Qtls.";
                string op = "";
                string year = DateTime.Today.Year.ToString();
                string date = getDate_MDY(effective_from.Text);
               
                string medate = getDate_MDY(DateTime.Today.Date.ToString());

                float mbonous = 0;// CheckNull(txtbonus.Text);
                float minsd = 0;//CheckNull(txtincidental.Text);
                float murate = CheckNull(txturate .Text );
                float mrrate = CheckNull(txtrrate.Text);
                float mgcap = CheckNull(txtgunnycap.Text);
                string mcrop = ddlcropyear.SelectedItem.Text;
                string qryInsert = "insert into dbo.SCSC_IssueRate(Scheme_ID,Commodity_ID,Crop_Year,Rural_rate,Uraban_rate,Incidental,Bonus,Gunny_Capacity,Effective_From,Created_By,Created_Date,year)values('" + mscheme + "','" + mcomdty +"','"+ mcrop + "'," + mrrate + "," + murate + "," + minsd + "," + mbonous + "," + mgcap + ",'" + date + "','" + dstid + "','" + medate + "','" + year + "'" + ")";

                cmd.Connection = con;
                cmd.CommandText = qryInsert;


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted  successfully...'); </script> ");
                    btnAdd.Enabled = false;


                }
                catch (Exception ex)
                {
                    Label4.Text = ex.Message;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + Label4.Text + "'); </script> ");



                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
                }
                Fillgrid();

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rate Already Exist ...'); </script> ");

            }



        }
    }

    void Fillgrid()
    {

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_IssueRate.*,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as Commodity_Name  ,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name    FROM dbo.SCSC_IssueRate left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_IssueRate.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY.Commodity_ID left join tbl_MetaData_SCHEME on SCSC_IssueRate.Scheme_ID =tbl_MetaData_SCHEME.Scheme_ID  order by Commodity_Name  desc";
        DataSet ds = mobj.selectAny(qry);
         if (ds==null)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Effective_From"));

            getdatef = getdate(griddate);

            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;


        }
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
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
        Fillgrid();
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mcomdty = GridView1.SelectedRow.Cells[1].Text;
        string mscheme = GridView1.SelectedRow.Cells[2].Text;
        string mcrop = GridView1.SelectedRow.Cells[3].Text;
        ddlcropyear.SelectedItem.Text = mcrop;
        txtrrate.Text = GridView1.SelectedRow.Cells[4].Text;
        txturate.Text = GridView1.SelectedRow.Cells[5].Text;
        string comid = GridView1.SelectedRow.Cells[10].Text;
        string schid = GridView1.SelectedRow.Cells[11].Text;
        ddlcomodity.SelectedValue = comid;
        ddlscheme.SelectedValue = schid;
        ddlcropyear.SelectedItem.Text = GridView1.SelectedRow.Cells[3].Text;
        txtgunnycap.Text = GridView1.SelectedRow.Cells[9].Text;
        btnAdd.Visible = false;
        Button1.Visible = true;
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mcomodity = ddlcomodity.SelectedItem.Text;
        if (mcomodity.Contains("Wheat") || mcomodity.Contains("Rice") || mcomodity.Contains("Salt"))
        {
            txtgunnycap.Text = "50";
            txtgunnycap.ReadOnly = true;
            txtgunnycap.BackColor = System.Drawing.Color.Wheat;
        }
        else if (mcomodity.Contains("Sugar"))
        {
            txtgunnycap.Text = "100";
            txtgunnycap.ReadOnly = true;
            txtgunnycap.BackColor = System.Drawing.Color.Wheat;
        }
        else if (mcomodity == "Kerosene")
        {
            txtgunnycap.Text = "";
            txtgunnycap.ReadOnly = false;
            txtgunnycap.BackColor = System.Drawing.Color.White;
        }
        else
        {
            txtgunnycap.Text = "";
            txtgunnycap.ReadOnly = false;
            txtgunnycap.BackColor = System.Drawing.Color.White;
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mcomdty = GridView1.SelectedRow.Cells[10].Text;
        string mscheme = GridView1.SelectedRow.Cells[11].Text;
        int gcaps = CheckNullInt(txtgunnycap.Text);
        float rrate = CheckNull(txtrrate.Text);
        float urate = CheckNull(txturate.Text);
        string crop = ddlcropyear.SelectedItem.Text;
        string update = "Update dbo.SCSC_IssueRate set Rural_rate=" + rrate + ",Uraban_rate=" + urate + ",Gunny_Capacity=" + gcaps + ",Crop_Year='"+crop +"' where Commodity_ID='" + mcomdty + "' and Scheme_ID='" + mscheme + "'";

            cmd.CommandText = update;
            try
            {
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully ...'); </script> ");
                Fillgrid();
                btnAdd.Visible = true;
                Button1.Visible = false;
                txtgunnycap.Text = "";
                GetCommodity();
                GetScheme();
               
            }
            catch (Exception ex)
            {
                Label4.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
    }
}
