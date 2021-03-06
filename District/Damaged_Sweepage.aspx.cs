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

public partial class District_Damaged_Sweepage : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    Districts DObj = null;
    chksql chk = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;

    MoveChallan mobj = null;
    string distid = "";
    static string issueid = "";
    protected Common ComObj = null, cmn = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
         
            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtqty.Attributes.Add("onchange", "return chksqltxt(this)");

            
            txtdamquantity.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtdamquantity.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdamquantity.Attributes.Add("onchange", "return chksqltxt(this)");

            txtsweepqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtsweepqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtsweepqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtdamagebag.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtdamagebag.Attributes.Add("onchange", "return chksqltxt(this)");
            txtdamagebag.Attributes.Add("onkeypress", "return CheckIsnondecimal(this)");
                        
            effective_from.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            effective_from.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            effective_from.Attributes.Add("onchange", "return chksqltxt(this)");

            if (!IsPostBack)
            {
              
                GetScheme();
                GetCommodity();

                getsweepageCategory();
                            
                GetSource();
               
                GetICenter();
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }
    }

    void GetCommodity()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];
        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetScheme()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetSource()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

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

    void GetGodown()
    {
        if (con.State == ConnectionState.Closed)
        {
          con.Open();
        }

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + ddlissuecenter.SelectedValue + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetICenter()
    {
        if (con.State == ConnectionState.Closed)
        {
          con.Open();
        }
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_DEPOT where DistrictId = '23" + distid + "' order by DepotID "; 
        DataSet ds = mobj.selectAny(qry);

        ddlissuecenter.DataSource = ds.Tables[0];
        ddlissuecenter.DataTextField = "DepotName";
        ddlissuecenter.DataValueField = "DepotID";
        ddlissuecenter.DataBind();
        ddlissuecenter.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
    void getsweepageCategory()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_sweepage_CATEGORY ";
        DataSet ds = mobj.selectAny(qry);

        ddlcataegory.DataSource = ds.Tables[0];
        ddlcataegory.DataTextField = "Category_Name";
        ddlcataegory.DataValueField = "Category_Id";
        ddlcataegory.DataBind();
        ddlcataegory.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected String getDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        return dat;
    }

    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlissuecenter.SelectedValue == "0")
        {

        }
        else
        {
            issueid = ddlissuecenter.SelectedValue;
            GetGodown();
        }
    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        if (ddlsarrival.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Source of Arrival....'); </script> ");
            return;
        }

        if (ddlcomdty.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity....'); </script> ");
            return;
        }
                
        if (ddlissuecenter.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Issue Center....'); </script> ");
            return;
        }

        if (ddlgodown.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Godown....'); </script> ");
            return;
        }

        string Commodity_Id = ddlcomdty.SelectedValue;
        Int16 comid = Convert.ToInt16(Commodity_Id);

        string pqry = "stock_depotwise";
        SqlCommand cmdpqty = new SqlCommand(pqry, con);
        cmdpqty.CommandType = CommandType.StoredProcedure;


        cmdpqty.Parameters.Add("@Commodity_Id", SqlDbType.Int).Value = comid;
        cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = distid;


        //DateTime fdate = Convert.ToDateTime(getDate_MDY(effective_from.Text));

       // DateTime dt = Convert.ToDateTime(fdate);


        //cmdpqty.Parameters.Add("@todate", System.Data.SqlDbType.DateTime).Value = fdate;
        cmdpqty.Parameters.Add("@Depotid", SqlDbType.Int).Value = issueid;
        cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = ddlgodown.SelectedValue;


        DataSet ds = new DataSet();
        SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

        dr.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Sound"].ToString()),2);
           
            txtqty.Text = Convert.ToString(stock);
        }
           
            BindGrid();      
                            
        if(con.State == ConnectionState.Open)
        {
            con.Close();
        }
}
   
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedValue == "0")
        {

        }
        else
        {
            Label1.Text = ddlgodown.SelectedItem.Text;
         
            ddlcropyear.SelectedIndex = 00;
            txtqty.Text = "0";

        }       
    }

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;
    }

    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string category;
        if (Convert.ToDecimal(txtdamagebag.Text) + Convert.ToDecimal(txtdamquantity.Text) != 0)
        {

            if (ddlcataegory.SelectedValue == "0")
            {
                ddlgodown.Focus();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Category....'); </script> ");
                return;
            }

            category = ddlcataegory.SelectedValue.ToString();

        }
        else
        {
            category = "NA";
        }
   
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
       
        decimal damQty = CheckNull(txtdamquantity.Text);
        decimal SweeQty = CheckNull(txtsweepqty.Text);
        int dambags = CheckNullInt(txtdamagebag.Text);

        decimal AvilQty = CheckNull(txtqty.Text);

       

        if (effective_from.Text == "")
        {
            effective_from.Focus();
            effective_from.BackColor = System.Drawing.Color.Blue;
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Date....'); </script> ");
            return;
        }

        if (ddlsarrival.SelectedValue == "0")
        {
            ddlsarrival.Focus();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Source of Arrival....'); </script> ");
            return;
        }

        if (ddlcomdty.SelectedValue == "0")
        {
            ddlcomdty.Focus();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity....'); </script> ");
            return;
        }

        if (ddlissuecenter.SelectedValue == "0")
        {
            ddlissuecenter.Focus();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Issue Center....'); </script> ");
            return;
        }

        if (ddlgodown.SelectedValue == "0")
        {
            ddlgodown.Focus();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select Godown....'); </script> ");
            return;
        }


         if(txtsweepqty.Text == "" || txtdamquantity.Text == "" || txtdamagebag.Text == "" || txtqty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Value will not be Empty'); </script> ");
            return;
        }

        if (damQty > AvilQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Damage Quantity will Not More than Available Quantity....'); </script> ");
            return;
        }

        if (SweeQty > AvilQty)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Sweepage Quantity will Not More than Available Quantity....'); </script> ");
            return;
        }

        string Insddate = getDate_MDY(effective_from.Text);

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();

        string Browser = Request.Browser.Browser;
        string strSession = HttpContext.Current.Session.SessionID;

        string seldupli = "SELECT  count(TransId) FROM Tbl_Damage_Sweepage where Depot_ID = '"+ddlissuecenter.SelectedValue+"' and Godown = '"+ddlgodown.SelectedValue+"' and S_of_arrival = '"+ddlsarrival.SelectedValue+"' and Commodity = '"+ddlcomdty.SelectedValue+"' and Scheme = '"+ddlscheme.SelectedValue+"' and Crop_year = '"+ddlcropyear.SelectedItem.Text+"' and OperatedDate = convert(date,'"+Insddate+"')";
        SqlCommand cmdduplicate = new SqlCommand(seldupli, con);
        Int16 check = Convert.ToInt16(cmdduplicate.ExecuteScalar());
        if (check > 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('इस दिनांक की सम्बंधित जानकारी उपलब्ध है , कृपया नीचे जानकारी में संसोधन करें'); </script> ");

        }

        else
        {

       try
        {
            int month = System.DateTime.Now.Month;

            string insert = "Insert into Tbl_Damage_Sweepage (Dist_Id ,Depot_ID ,Godown ,S_of_arrival ,Commodity ,Scheme ,Crop_year ,Damage_Bags ,Damage_Quantity ,SweepageQty, Month ,OperatedDate ,IP_Address ,Created_date ,OperatorID,Category_Id) values ('" + distid + "' ,'" + issueid + "','" + ddlgodown.SelectedValue + "','" + ddlsarrival.SelectedValue + "' ,'" + ddlcomdty.SelectedValue + "','" + ddlscheme.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "','" + dambags + "','" + damQty + "','" + SweeQty + "'," + month + ",'" + Insddate + "','" + ip + "',getdate(),'" + opid + "','" + category + "')";
            SqlCommand cmd = new SqlCommand(insert, con);

            int y = cmd.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved....'); </script> ");

            btnSave.Enabled = false;

            BindGrid();

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        catch
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Thing Might Going Wrong....'); </script> ");
        }

        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;

        GetScheme();
      
        GetICenter();

        GetGodown();
       
        txtqty.Text = "0";

        ddlcropyear.SelectedIndex = 00;

        txtdamagebag.Text = "";
        txtdamquantity.Text = "";

        txtsweepqty.Text = "";
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }


    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDetails.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int TransId = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
      
        TextBox txtdamBag = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtdamBag");
        TextBox txtdamQty = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtdamQty");
        TextBox txtqty = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtqty");
        con.Open();
        SqlCommand cmd = new SqlCommand("update Tbl_Damage_Sweepage set Category_Id='"+ddlcataegory.SelectedValue.ToString()+"', Damage_Bags='" + txtdamBag.Text + "',Damage_Quantity='" + txtdamQty.Text + "',SweepageQty='" + txtqty.Text + "' where TransId=" + TransId, con);
        cmd.ExecuteNonQuery();
        con.Close();

        gvDetails.EditIndex = -1;
        BindGrid();
    }

    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDetails.EditIndex = -1;
        BindGrid();
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int TransId = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["TransId"].ToString());
        string DepotID = gvDetails.DataKeys[e.RowIndex].Values["Depot_ID"].ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from Tbl_Damage_Sweepage where TransId=" + TransId, con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        if (result == 1)
        {
            BindGrid();
        }
    }

    protected void BindGrid()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string qry = "select tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,tbl_MetaData_SCHEME.Scheme_Name,Tbl_Damage_Sweepage.Depot_ID,Tbl_Damage_Sweepage.TransId,Tbl_Damage_Sweepage.Crop_year,convert(nvarchar,Tbl_Damage_Sweepage.OperatedDate,103)Date,Tbl_Damage_Sweepage.Damage_Bags,Tbl_Damage_Sweepage.Damage_Quantity,Tbl_Damage_Sweepage.SweepageQty from Tbl_Damage_Sweepage inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = Tbl_Damage_Sweepage.Depot_ID inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Tbl_Damage_Sweepage.Godown inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = Tbl_Damage_Sweepage.Commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = Tbl_Damage_Sweepage.Scheme where Tbl_Damage_Sweepage.Dist_Id = '" + distid + "' and Tbl_Damage_Sweepage.Depot_ID = '" + ddlissuecenter.SelectedValue + "' and Tbl_Damage_Sweepage.Godown = '" + ddlgodown.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = ds;
            gvDetails.DataBind();

            gvDetails.Visible = true;

            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;           
        }
        else
        {
            gvDetails.Visible = false;

            Label1.Visible = false;
            Label2.Visible = false;
           Label3.Visible = false;           
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
            string DepotID = Label1.Text;
            //identifying the control in gridview
            LinkButton lnkbtnresult = (LinkButton)e.Row.FindControl("LinkButton3");
            //raising javascript confirmationbox whenver user clicks on link button
            if (lnkbtnresult != null)
            {
                lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + DepotID + "')");
            }

        }
    }

 
}

