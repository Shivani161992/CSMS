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

public partial class State_StateRate_Master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    State_StateRate_Master rmobj = null;
    //MoveChallan mobj = null;
    //MoveChallan mobj1 = null;

    chksql chk = null;

    protected Common ComObj = null, cmn = null;
    public string dstid = "";
    public string fps_code = "";
    public string getdatef = "";
    public string distid = "";
    public string schemeid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
          

            txtmsprate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
           
           
            txtmsprate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtmsprate.Attributes.Add("onchange", "return chksqltxt(this)");

                   
            effective_from.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            effective_from.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            effective_from.Attributes.Add("onchange", "return chksqltxt(this)");

            
            txtmsprate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
          
                    
            if (chk == null)
            {
            }
            else
            {
                
            }

            if (!IsPostBack)
            {

                effective_from.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

                GetScheme();
                GetCommodity();

                BindGrid();
             
//GetSource();
               
            }

        }
    }

    void GetScheme()
    {

       // mobj = new MoveChallan(ComObj);
        string qrysch = "SELECT * from tbl_MetaData_SCHEME where Scheme_Id in ('34','35','103','0','112','13','3','116')";

        SqlCommand cmd = new SqlCommand(qrysch, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlscheme.DataSource = ds.Tables[0];
            ddlscheme.DataTextField = "Scheme_Name";
            ddlscheme.DataValueField = "Scheme_Id";
            ddlscheme.DataBind();
            ddlscheme.Items.Insert(0, "--Select--");
        }

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string state = Session["State_Id"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = "H";


        if (ddlcomodity.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity..'); </script> ");

        }

        else
             
                if (ddlscheme.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Scheme..'); </script> ");

        }
        else
        {

            string RateType;

            if (ddlrate.SelectedItem.Text == "Urban")
            {
                RateType = "U";
            }

            else
            {
                RateType = "R";
            }

            lblscheme.Text = ddlscheme.SelectedValue;
           
            string mcomdty = ddlcomodity.SelectedValue;
            string scheme = lblscheme.Text;
            string myear = DateTime.Today.Year.ToString();
            string effectivedate = getDate_MDY(effective_from.Text);

            string qrey = "Select * from dbo.State_rateMaster where Commodity_ID='" + mcomdty + "'and Scheme_ID='" + scheme + "' and Year='" + myear + "' and Effective_From='" + effectivedate + "' and RateType = '"+RateType+"'";
           // mobj1 = new MoveChallan(ComObj);

            SqlCommand cmd = new SqlCommand(qrey,con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

           // DataSet ds = mobj1.selectAny(qrey);
            if (ds.Tables[0].Rows.Count == 0)
            {
              
                string year = DateTime.Today.Year.ToString();
                string date = getDate_MDY(effective_from.Text);
               
                string mscheme = lblscheme.Text;
                string mcommdty = ddlcomodity.SelectedValue;
              
                //  string medate = getDate_MDY(DateTime.Today.Date.ToString());
                string mcrop = ddlcropyear.SelectedItem.ToString();
             
                float mmsp = CheckNull(txtmsprate.Text);

                


                string qryInsert = "insert into dbo.State_rateMaster(Scheme_ID,Commodity_ID,Crop_Year,RateType,Rate,Effective_From,Created_Date,year,IP,OperatorID)values('" + mscheme + "','" + mcommdty + "','" + mcrop + "','" + RateType + "'," + mmsp + ",'" + effectivedate + "',getdate(),'" + year + "','" + ip + "','" + opid + "')";

                cmd.Connection = con;
                cmd.CommandText = qryInsert;


                try
                {
                    if (mmsp == 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rate Should Not Be Zero...'); </script> ");
                    }
                    else
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted  successfully...'); </script> ");
                        btnAdd.Enabled = false;
                        txtmsprate.Text = "";

                        BindGrid();
                     
                    }

                }
                catch (Exception ex)
                {
                    Label4.Text = ex.Message;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + Label4.Text + "'); </script> ");



                }
                finally
                {
                    con.Close();
                   
                }
                

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rate Already Exist ...'); </script> ");

            }
        }
    }

    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }

    void GetCommodity()
    {
      //  comdtobj = new Commodity_MP(ComObj);

        string qrry = "SELECT * from  tbl_MetaData_STORAGE_COMMODITY where Status = 'Y'";


        SqlCommand cmd = new SqlCommand(qrry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        //DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        if (ds == null)
        {
        }
        else
        {
            ddlcomodity.DataSource = ds.Tables[0];
            ddlcomodity.DataTextField = "Commodity_Name";
            ddlcomodity.DataValueField = "Commodity_Id";
            ddlcomodity.DataBind();
            ddlcomodity.Items.Insert(0, "--Select--");

        }
    }

    protected void BindGrid()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string qry = "select tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , tbl_MetaData_SCHEME.Scheme_Name , CONVERT(nvarchar, State_rateMaster.Effective_From,103)Effective_From ,State_rateMaster.Commodity_ID,State_rateMaster.Scheme_ID,State_rateMaster.RateType, State_rateMaster.Crop_Year , State_rateMaster.Rate from State_rateMaster inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = State_rateMaster.Commodity_ID inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = State_rateMaster.Scheme_ID ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (con.State == ConnectionState.Open)
        {

            con.Close();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = ds;
            gvDetails.DataBind();

            gvDetails.Visible = true;

            
        }
        else
        {
            gvDetails.Visible = false;

           
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //getting username from particular row
            
            //identifying the control in gridview
            LinkButton lnkbtnresult = (LinkButton)e.Row.FindControl("LinkButton3");
            //raising javascript confirmationbox whenver user clicks on link button
            if (lnkbtnresult != null)
            {
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('')");
            }

        }
    }

    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDetails.EditIndex = -1;
        BindGrid();
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Scheme_ID = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["Scheme_ID"].ToString());

        string Commodity_ID = gvDetails.DataKeys[e.RowIndex].Values["Commodity_ID"].ToString();

        string Crop_Year = gvDetails.DataKeys[e.RowIndex].Values["Crop_Year"].ToString();

        //Crop_Year

        con.Open();
        SqlCommand cmd = new SqlCommand("Delete from State_rateMaster where Commodity_ID = '" + Commodity_ID + "' and Scheme_ID = '" + Scheme_ID + "' and Crop_Year = '" + Crop_Year + "' ", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();

        if (result == 1)
        {
            BindGrid();
        }
    }

    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDetails.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int Scheme_ID = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["Scheme_ID"].ToString());

        string Commodity_ID = gvDetails.DataKeys[e.RowIndex].Values["Commodity_ID"].ToString();

        string Crop_Year = gvDetails.DataKeys[e.RowIndex].Values["Crop_Year"].ToString();

        TextBox Rate = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("Rate");
        
        if(con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        //SqlCommand cmd = new SqlCommand("update State_rateMaster set Rate='" + Rate.Text + "' where Commodity_ID =" + Commodity_ID "and Scheme_ID" + Scheme_ID "and Crop_Year" = +Crop_Year , con);

         SqlCommand cmd = new SqlCommand("update State_rateMaster set Rate='" + Rate.Text + "' where Commodity_ID = '" + Commodity_ID +"' and Scheme_ID ='" + Scheme_ID +"' and Crop_Year = '"+Crop_Year+"'" , con);

        cmd.ExecuteNonQuery();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
       

        gvDetails.EditIndex = -1;

        BindGrid();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/State_Welcome.aspx");
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        btnAdd.Enabled = true;
    }
}
