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
public partial class Rate_master : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Rate_master rmobj = null;
    MoveChallan mobj = null;
    MoveChallan mobj1 = null;

    chksql chk = null;

    protected Common ComObj = null, cmn = null;
    public string dstid = "";
    public string  fps_code="";
    public string getdatef = "";
    public string distid = "";
    public string schemeid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            txtmsprate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtincidental.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtbonus.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtgunnycap.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");



            txtmsprate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtmsprate.Attributes.Add("onchange", "return chksqltxt(this)");

            txtincidental.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtincidental.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbonus.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbonus.Attributes.Add("onchange", "return chksqltxt(this)");

            txtgunnycap.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtgunnycap.Attributes.Add("onchange", "return chksqltxt(this)");

            effective_from.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            effective_from.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            effective_from.Attributes.Add("onchange", "return chksqltxt(this)");



            txtmsprate.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtincidental.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
            txtbonus.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           txtgunnycap.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
           chk = new chksql();
           ArrayList ctrllist = new ArrayList();
           ctrllist.Add(txtmsprate.Text);
           ctrllist.Add(txtincidental.Text);
           ctrllist.Add(txtbonus.Text);
           ctrllist.Add(txtgunnycap.Text);
           ctrllist.Add(effective_from.Text);
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

                effective_from.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");
               
                GetScheme();
                GetCommodity();
                GetSeason();
                GetSource();
                GetSchemeFCI();

                int myear = int.Parse(DateTime.Now.Year.ToString());
                ddlcropyear.Items.Add("Crop Year Not Indicated");
                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 2).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 3).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 2).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 4).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 3).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 5).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 4).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 6).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 5).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 7).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 6).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 8).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 7).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 9).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 8).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 10).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 9).ToString());

               
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
    void GetSeason()
    {
        mobj = new MoveChallan(ComObj);
        string qryms = "select *  from dbo.Marketing_Season";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {

            ddlmseason.DataSource = dsms.Tables[0];
            ddlmseason.DataTextField = "Season_Name";
            ddlmseason.DataValueField = "Season_Id";
            ddlmseason.DataBind();
            ddlmseason.Items.Insert(0, "--Select--");
        }

    }
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qrygs = "SELECT * FROM dbo.Source_Arrival_Type where Source_Id in (01,03) order by Source_ID";
        DataSet ds = mobj.selectAny(qrygs);
        if (ds == null)
        {
        }
        else
        {
            ddlsource.DataSource = ds.Tables[0];
            ddlsource.DataTextField = "Source_Name";
            ddlsource.DataValueField = "Source_ID";
            ddlsource.DataBind();
            ddlsource.Items.Insert(0, "--Select--");
        }
    }

    void GetScheme()
    {

        mobj = new MoveChallan(ComObj);
        string qrysch = "SELECT * FROM dbo.Purchase_Pool Order By Scheme_ID";
        DataSet ds = mobj.selectAny(qrysch);
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
    void GetSchemeFCI()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlschemefci.DataSource = ds.Tables[0];
        ddlschemefci.DataTextField = "Scheme_Name";
        ddlschemefci.DataValueField = "Scheme_Id";
        ddlschemefci.DataBind();
        ddlschemefci.Items.Insert(0, "--Select--");

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
        string opid = Session["OperatorIDDM"].ToString();


        if (ddlcomodity.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity..'); </script> ");

        }
        else
        {

            if (ddlsource.SelectedItem.Text == "From FCI")
            {
                lblscheme.Text = ddlschemefci.SelectedValue;

            }
            else
            {
                lblscheme.Text = ddlscheme.SelectedValue;
            }


            string mcomdty = ddlcomodity.SelectedValue;
            string scheme = lblscheme.Text;
            string myear = DateTime.Today.Year.ToString();
            string effectivedate = getDate_MDY(effective_from.Text);
            string qrey = "Select * from dbo.SCSC_MSP_rate where Commodity_ID='" + mcomdty + "'and Scheme_ID='" + scheme + "' and Year='" + myear + "' and Purchase_From='" + ddlsource.SelectedValue + "' and District_code='" + distid + "' and Effective_From='" + effectivedate + "'";
            mobj1 = new MoveChallan(ComObj);

            DataSet ds = mobj1.selectAny(qrey);
             if (ds.Tables[0].Rows.Count==0)
            {
                string qtl = "Qtls.";
                string op = "";
                string year = DateTime.Today.Year.ToString();
                string date = getDate_MDY(effective_from.Text);
                string mpurfrom = ddlsource.SelectedValue;
                string mscheme = lblscheme.Text;
                string mcommdty = ddlcomodity.SelectedValue;
                float rate = CheckNull(txtmsprate.Text) + CheckNull(txtincidental.Text) + CheckNull(txtbonus.Text);
              //  string medate = getDate_MDY(DateTime.Today.Date.ToString());
                string mcrop = ddlcropyear.SelectedItem.ToString();
                string mseason = ddlmseason.SelectedValue;
                float mbonous = CheckNull(txtbonus.Text);
                float minsd = CheckNull(txtincidental.Text);
                float mmsp = CheckNull(txtmsprate.Text);
                float mgcap = CheckNull(txtgunnycap.Text);
                string qryInsert = "insert into dbo.SCSC_MSP_rate(State_Id,District_code,Purchase_From,Scheme_ID,Commodity_ID,Crop_Year,M_Season,MSP_rate,Incidental,Bonus,Rate,Gunny_Capacity,Effective_From,Created_By,Created_Date,year,IP,OperatorID)values('" + state + "','" + distid + "','" + mpurfrom + "','" + mscheme + "','" + mcommdty + "','" + mcrop + "','" + mseason + "'," + mmsp + "," + minsd + "," + mbonous + "," + rate + "," + mgcap + ",getdate(),'" + dstid + "',getdate(),'" + year + "','" +ip +"','" + opid + "')";

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
                        txtincidental.Text = "";
                        txtbonus.Text = "";
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
                    ComObj.CloseConnection();
                }
                if (ddlsource.SelectedItem.Text == "From FCI")
                {
                    FillgridFCI();

                }
                else
                {
                    Fillgrid();
                }

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
        string qrypr = "SELECT SCSC_MSP_rate.* ,Marketing_Season.Season_Name,Purchase_Pool.Scheme_Name,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name FROM dbo.SCSC_MSP_rate  left join dbo.Marketing_Season on SCSC_MSP_rate.M_Season=Marketing_Season.Season_ID left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_MSP_rate.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID left join dbo.Purchase_Pool on SCSC_MSP_rate.Scheme_Id=Purchase_Pool.Scheme_Id  where  SCSC_MSP_rate.Purchase_From='01' and District_code='"+distid +"'";
        DataSet dspr = mobj.selectAny(qrypr);
        if (dspr == null)
        {
        }
        else
        {
            GridView1.DataSource = dspr.Tables[0];
            GridView1.DataBind();
        }
    }
    void FillgridFCI()

    {

        mobj = new MoveChallan(ComObj);
        string qryfc = "SELECT SCSC_MSP_rate.* ,Marketing_Season.Season_Name,tbl_MetaData_Scheme.Scheme_Name,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name FROM dbo.SCSC_MSP_rate  left join dbo.Marketing_Season on SCSC_MSP_rate.M_Season=Marketing_Season.Season_ID left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_MSP_rate.Commodity_ID=tbl_MetaData_STORAGE_COMMODITY .Commodity_ID left join dbo.tbl_MetaData_Scheme on SCSC_MSP_rate.Scheme_Id=tbl_MetaData_Scheme.Scheme_Id  where  SCSC_MSP_rate.Purchase_From='03' and District_code='"+distid +"'";
        DataSet dsfc = mobj.selectAny(qryfc);
        if (dsfc == null)
        {
        }
        else
        {
            GridView1.DataSource = dsfc.Tables[0];
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
        if (ddlsource.SelectedItem.Text == "From FCI")
        {
          FillgridFCI();

        }
        else
        {
            Fillgrid();
       }
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
        if (ddlsource.SelectedItem.Text == "From FCI")
        {

            string comdty = GridView1.SelectedRow.Cells[11].Text;
            string scheme = GridView1.SelectedRow.Cells[12].Text;
            string Source = GridView1.SelectedRow.Cells[10].Text;
            txtmsprate.Text = GridView1.SelectedRow.Cells[5].Text;
            txtgunnycap.Text = GridView1.SelectedRow.Cells[9].Text;

            ddlcomodity.SelectedValue = comdty;
            ddlschemefci.SelectedValue = scheme;
            btnAdd.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            btnAdd.Visible = false;
            Button1.Visible = true;
            string pcomdty = GridView1.SelectedRow.Cells[11].Text;
            string pscheme = GridView1.SelectedRow.Cells[12].Text;
            string pSource = GridView1.SelectedRow.Cells[10].Text;
            string pmcrop = GridView1.SelectedRow.Cells[4].Text;
            string pmsesion = GridView1.SelectedRow.Cells[13].Text;
            ddlcomodity.SelectedValue = pcomdty;
            ddlscheme.SelectedValue = pscheme;
            ddlcropyear.SelectedItem.Text = pmcrop;
            ddlmseason.SelectedValue = pmsesion;
            txtmsprate.Text = GridView1.SelectedRow.Cells[5].Text;
            txtgunnycap.Text = GridView1.SelectedRow.Cells[9].Text;
            txtincidental.Text = GridView1.SelectedRow.Cells[8].Text;
            txtbonus.Text = GridView1.SelectedRow.Cells[7].Text;


        }

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mcomodity=ddlcomodity.SelectedItem.Text ;
        if (mcomodity.Contains("Wheat") || mcomodity.Contains("Rice") || mcomodity.Contains("Salt"))
        {
            txtgunnycap.Text = "50";
            txtgunnycap.ReadOnly = true ;
            txtgunnycap.BackColor = System.Drawing.Color.Wheat;
        }
        else if (mcomodity.Contains("Sugar"))
        {
            txtgunnycap.Text = "100";
            txtgunnycap.ReadOnly  = true ;
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
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void ddlsource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsource.SelectedItem.Text =="From FCI")
        {
            lblinsid.Visible = false;
            lblbonus.Visible = false;
            txtbonus.Visible = false;
            txtincidental.Visible = false;
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[3].Visible = false;
            GridView1.Columns[4].Visible = false;
            lblsch.Visible = true;
            ddlschemefci.Visible = true;
            lbltname.Visible = false;
            ddlscheme.Visible = false;
            lblcropyear.Visible = false;
            lblmarktse.Visible = false;
            ddlcropyear.Visible = false;
            ddlmseason.Visible = false;
            FillgridFCI();

        }
        else
        {
            Fillgrid();
            GridView1.Columns[7].Visible =true ;
            GridView1.Columns[8].Visible = true;
            lblinsid.Visible = true ;
            lblbonus.Visible = true;
            txtbonus.Visible = true;
            txtincidental.Visible = true;
            lblsch.Visible = false ;
            ddlschemefci.Visible = false;
            lbltname.Visible = true;
            ddlscheme.Visible = true;
            lblcropyear.Visible = true ;
            lblmarktse.Visible = true;
            ddlcropyear.Visible = true;
            ddlmseason.Visible = true;
        }


    }
    protected void ddlmseason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void ddlschemefci_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        lblscheme.Text = ddlschemefci.SelectedValue;
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        lblscheme.Text = ddlscheme.SelectedValue;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mcomdty = GridView1.SelectedRow.Cells[11].Text;
        string mscheme = GridView1.SelectedRow.Cells[12].Text;
        string mSource = GridView1.SelectedRow.Cells[10].Text;
        float rate=CheckNull (txtmsprate.Text);
        float bonus = CheckNull(txtbonus.Text);
        float incident = CheckNull(txtincidental.Text);
        int gcaps = CheckNullInt(txtgunnycap.Text);
        string mcropy = ddlcropyear.SelectedItem.Text;
        string msession = ddlmseason.SelectedValue;
        string mpscheme = ddlscheme.SelectedValue;
        string updatedate = getDate_MDY(effective_from.Text);
        if (ddlsource.SelectedItem.Text == "From FCI")
        {
            string update = "Update dbo.SCSC_MSP_rate set MSP_rate=" + rate + ",Rate=" + rate + ",Gunny_Capacity=" + gcaps + ",Effective_From='"+updatedate+"' where Purchase_From='" + mSource + "' and Commodity_ID='" + mcomdty + "' and Scheme_ID='" + mscheme + "' and  District_code='" + distid + "'";

            cmd.CommandText = update;
            try
            {
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully ...'); </script> ");
                FillgridFCI();
                btnAdd.Visible = true;
                Button1.Visible = false;
                txtgunnycap.Text = "";
                txtmsprate.Text = "";
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
        else
        {
            float mprate = rate + incident + bonus;
            string updatePr = "Update dbo.SCSC_MSP_rate set MSP_rate=" + rate + ",Gunny_Capacity=" + gcaps + ",Incidental=" + incident + ",Bonus=" + bonus + ",Rate=" + mprate + ",Effective_From='" + updatedate + "' where Purchase_From='" + mSource + "' and Commodity_ID='" + mcomdty + "' and Scheme_ID='" + mscheme + "' and District_code='" + distid + "'";

            cmd.CommandText = updatePr;
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
                txtmsprate.Text = "";
                GetCommodity();
                GetScheme();
                txtmsprate.Text = "";
                txtincidental.Text = "";
                txtbonus.Text = "";
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
}
   


