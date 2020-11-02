using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class District_TO_against_MOfrmHO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    string districtid;
    DataTable Dt1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            districtid = Session["dist_id"].ToString();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (!IsPostBack)
            {
                Session["dt1"] = null;

                GridView1.DataSource = (DataTable)Session["dt1"];
                GridView1.DataBind();

                txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                Recdate.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                //Recdate.Text = DateTime.Today.Date.ToString("dd-MM-yyyy");

                GetMO();

                GetDist();

                GetCommodity();

                GetTransport();

                GetDepot();

                getbranch();
             
                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 2).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 3).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 2).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 4).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 3).ToString());

                HyperLink1.Attributes.Add("onclick", "window.open('Print_Gatepass_Procurement.aspx',null,'left=50, top=10, height=570, width= 690, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');");
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public void GetDist()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "SELECT district_code ,district_name FROM pds.districtsmp order by district_name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlrecdist.DataSource = ds.Tables[0];
                ddlrecdist.DataTextField = "district_name";
                ddlrecdist.DataValueField = "district_code";
                ddlrecdist.DataBind();
                ddlrecdist.Items.Insert(0, "--Select--");

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void GetCommodity()
    {

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string cropname = "select Commodity_Id, Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (3,4,12,13,14,19,22,23)";
        SqlCommand cmd = new SqlCommand(cropname, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlcommodity.DataSource = ds.Tables[0];


        ddlcommodity.DataTextField = "Commodity_Name";
        ddlcommodity.DataValueField = "Commodity_Id";
        ddlcommodity.DataBind();
        ddlcommodity.Items.Insert(0, "--Select--");


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    private void Getgodown()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlbranch.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgodown.DataSource = ds.Tables[0];
                        ddlgodown.DataTextField = "Godown_Name";
                        ddlgodown.DataValueField = "Godown_ID";
                        ddlgodown.DataBind();
                        ddlgodown.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }

    }

    public void GetDepot()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ord = "Select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = '23" + districtid + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(ord, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            ddlissuecenter.DataSource = ds.Tables[0];
            ddlissuecenter.DataTextField = "DepotName";
            ddlissuecenter.DataValueField = "DepotId";

            ddlissuecenter.DataBind();

            ddlissuecenter.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
    
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getgodown();
    }

    public void GetMO()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qry = "SELECT MoveOrdernum FROM StateMovementOrder where FrmDist = '" + districtid + "' and ModeofDispatch = 12 and IsIssued = 'N'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlmo.DataSource = ds.Tables[0];
                ddlmo.DataTextField = "MoveOrdernum";
                ddlmo.DataValueField = "MoveOrdernum";
                ddlmo.DataBind();
                ddlmo.Items.Insert(0, "--Select--");

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddlmo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string getdetail = "SELECT pds.districtsmp.district_name,pds.districtsmp.district_code , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , tbl_MetaData_STORAGE_COMMODITY.Commodity_Id , StateMovementOrder.Quantity , convert(nvarchar,StateMovementOrder.ReachDate,105)ReachDate FROM StateMovementOrder inner join pds.districtsmp on pds.districtsmp.district_code = StateMovementOrder.ToDist inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id =  StateMovementOrder.Commodity where MoveOrdernum = '"+ddlmo.SelectedValue+"'";
        SqlCommand cmd = new SqlCommand(getdetail,con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string todist = ds.Tables[0].Rows[0]["district_code"].ToString();

            string commid = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();

            string Tdate = ds.Tables[0].Rows[0]["ReachDate"].ToString();

            string qty = ds.Tables[0].Rows[0]["Quantity"].ToString();

            ddlrecdist.SelectedValue = todist;

            ddlcommodity.SelectedValue = commid;

            Recdate.Text = Tdate;

            txtqty.Text = qty;

            lbltdate.Text = Tdate;

            lblTQty.Text = qty;

            GetRDepot(todist);

            getRbranch(todist);


        }

        else
        {
            GetMO();

            GetDist();

            GetCommodity();

            GetTransport();

            GetDepot();
        }
    }

    void GetTransport()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qry = "SELECT Transporter_Name,Transporter_ID FROM dbo.Transporter_Table where Distt_ID='" + districtid + "'and IsActive='Y'";
        
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddltransporter.DataSource = ds.Tables[0];
                ddltransporter.DataTextField = "Transporter_Name";
                ddltransporter.DataValueField = "Transporter_ID";
                ddltransporter.DataBind();
                ddltransporter.Items.Insert(0, "--Select--");

            }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    private DataTable CreateTable()
    {

        DataTable dt = new DataTable();//DataTable is created
        DataColumn Godown = new DataColumn("Godown", Type.GetType("System.String"));
        DataColumn MO = new DataColumn("MO", Type.GetType("System.String"));

        DataColumn distname = new DataColumn("distname", Type.GetType("System.String"));

        DataColumn cropyear = new DataColumn("cropyear", Type.GetType("System.String"));
        DataColumn qty = new DataColumn("qty", Type.GetType("System.Decimal"));
        DataColumn transname = new DataColumn("transname", Type.GetType("System.String"));

        DataColumn commodity = new DataColumn("commodity", Type.GetType("System.String"));
        DataColumn transp = new DataColumn("transp", Type.GetType("System.String"));

        DataColumn commodityid = new DataColumn("commodityid", Type.GetType("System.Int32"));
        DataColumn isscen = new DataColumn("isscen", Type.GetType("System.Int64"));

        DataColumn branchid = new DataColumn("branchid", Type.GetType("System.Int64"));

        DataColumn godownid = new DataColumn("godownid", Type.GetType("System.Int64"));



        DataColumn RGdn = new DataColumn("RGdn", Type.GetType("System.Int64"));

        DataColumn RIC = new DataColumn("RIC", Type.GetType("System.Int64"));

       
            
        
        dt.Columns.Add(Godown);//Column is added to the DataTable
        dt.Columns.Add(MO);//Column is added to the DataTable

        dt.Columns.Add(distname);//Column is added to the DataTable

        dt.Columns.Add(cropyear);//Column is added to the DataTable

        dt.Columns.Add(qty);//Column is added to the DataTable
        
        dt.Columns.Add(transname);//Column is added to the DataTable

        dt.Columns.Add(commodity);//Column is added to the DataTable

        dt.Columns.Add(commodityid);//Column is added to the DataTable

        dt.Columns.Add(transp);

        dt.Columns.Add(isscen);

        dt.Columns.Add(branchid);

        dt.Columns.Add(godownid);

        dt.Columns.Add(RIC);

        dt.Columns.Add(RGdn);

             
        dt.AcceptChanges();
        return dt;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (ddlrgodown.SelectedValue == "" || ddlrgodown.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('प्राप्ति गोदाम चुने'); </script> ");
            return;
        }
        if (ddlrIC.SelectedValue == "" || ddlrIC.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('प्राप्ति प्रदाय केंद्र चुने'); </script> ");
            return;
        }
        if (ddlrbranch.SelectedValue == "" || ddlrbranch.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('प्राप्ति ब्रांच का नाम चुने'); </script> ");
            return;
        }
        
        bool checkstatus = false;

        if (ddlmo.SelectedValue == "" || ddlmo.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Movement Order'); </script> ");
            return;
        }

        if (ddlrecdist.SelectedValue == "" || ddlrecdist.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भेजे जाने वाले जिला चुने !'); </script> ");
            return;
        }

        if (ddlcommodity.SelectedValue == "" || ddlcommodity.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Commodity !'); </script> ");
            return;
        }

        if (ddltransporter.SelectedValue == "" || ddltransporter.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('परिवहनकर्ता का नाम चुने !'); </script> ");
            return;
        }

        if (Recdate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('अंतिम परिवहन कि दिनांक चुने !'); </script> ");
            return;
        }

        if (txtqty.Text == "" || txtqty.Text== "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भेजने वाली मात्रा भरें!'); </script> ");
            return;
        }

        if (ddlissuecenter.SelectedValue == "" || ddlissuecenter.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भेजे जाने वाले प्रदाय केंद्र का नाम चुने !'); </script> ");
            return;
        }
                    
        else if (ddlbranch.SelectedValue == "" || ddlbranch.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('भेजे जाने वाले ब्रांच का नाम चुने'); </script> ");
            return;
        }

        else if (ddlgodown.SelectedValue == "" || ddlgodown.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert(' भेजे जाने वालेगोदाम में नाम चुने !'); </script> ");
            return;
        }
            decimal issqty = Convert.ToDecimal(txtqty.Text);

        decimal hoqty = Convert.ToDecimal(lblTQty.Text);

         if(issqty > hoqty)
         {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('जारी कि जाने वाली मात्रा , मुख्यालय द्वारा भरी गयी मात्रा से ज्यादा नहीं हो सकती'); </script> ");
            return;
         }
               
        DateTime issuedt = Convert.ToDateTime(DateTime.ParseExact(Recdate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));

     
        DateTime hodate = Convert.ToDateTime(DateTime.ParseExact(lbltdate.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));

        int result = DateTime.Compare(issuedt, hodate);
 
         if (result > 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('अंतिम परिवहन दिनांक , मुख्यालय से दी गयी दिनांक से अधिक नहीं हो सकती'); </script> ");
            return;
        }

        else
        {

            string moorder = ddlmo.SelectedValue;
         
            string todisname = ddlrecdist.SelectedItem.Text;
            string com = ddlcommodity.SelectedValue;
            string comname = ddlcommodity.SelectedItem.Text;

            string cropyear = ddlcropyear.SelectedItem.Text;

            string tranpid = ddltransporter.SelectedValue;

            string transpname = ddltransporter.SelectedItem.Text;

            string qty = txtqty.Text.Trim();

            string isscen = ddlissuecenter.SelectedValue;
            string branchid = ddlbranch.SelectedValue;

            string godownid = ddlgodown.SelectedValue;

            string gdnname = ddlgodown.SelectedItem.Text;

            string Rgodownid = ddlrgodown.SelectedValue;

            string RIssCent = ddlrIC.SelectedValue;

            if (ddlgodown.Items.Count > 0)
            {
                btnsave.Enabled = true;
                if (Session["dt1"] == null)
                {
                    Dt1 = CreateTable();
                    Session["dt1"] = Dt1;
                }

                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["dt1"]).NewRow();

                ((DataTable)Session["dt1"]).AcceptChanges();

                dr["godownid"] = godownid;
                dr["Godown"] = gdnname;

                dr["MO"] = moorder;

             
                dr["distname"] = todisname;

                dr["cropyear"] = cropyear;

                dr["transp"] = tranpid;

                dr["transname"] = transpname;
                
                dr["commodity"] = comname;

                dr["commodityid"] = com;
                
                dr["qty"] = qty;

                dr["isscen"] = isscen;

                dr["RIC"] = RIssCent;

                dr["RGdn"] = Rgodownid;

                dr["branchid"] = branchid;
              

                dr["qty"] = qty;

                if (GridView1.Rows.Count > 0)
                {
                    int i;

                    // checking whether or not the godown is already added to the grid view
                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        string commodityid = GridView1.DataKeys[i].Values[0].ToString();
                        string selectcomm = ddlcommodity.SelectedValue.ToString();

                        string godownidd = GridView1.DataKeys[i].Values[1].ToString();

                        string selectgodown = ddlgodown.SelectedValue.ToString();

                        if (godownidd == selectgodown && commodityid == selectcomm)
                        {
                            checkstatus = true;
                        }
                    }
                    if (checkstatus == false)
                    {
                        ((DataTable)Session["dt1"]).Rows.Add(dr);
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        GridView1.DataSource = (DataTable)Session["dt1"];
                        GridView1.DataBind();

                    
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('इस गोदाम के लिए प्रविष्टि हो चुकी है |'); </script> ");
                    }
                }
                else
                {
                    ((DataTable)Session["dt1"]).Rows.Add(dr);
                    ((DataTable)Session["dt1"]).AcceptChanges();
                    GridView1.DataSource = (DataTable)Session["dt1"];
                    GridView1.DataBind();

                    
                }

            }

        }

    }

    protected void getbranch()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }


                string qrysel = "select DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + districtid + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranch.DataSource = ds.Tables[0];
                        ddlbranch.DataTextField = "DepotName";
                        ddlbranch.DataValueField = "DepotID";
                        ddlbranch.DataBind();
                        ddlbranch.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        # region commentedcode_frmware
        if (cons != null)
        {
            if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }

            //string qrysel = "select tbl_MetaData_GODOWN.Godown_ID,Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,isnull(SUM(ReceiptWts),0)as depositmsp,ISNULL(Godown_Capacity - SUM(ReceiptWts),0)as vacientcap from tbl_MetaData_STACK left join DailyStacking_TransactionStatus on DailyStacking_TransactionStatus.Stackid = tbl_MetaData_STACK.Stack_ID   inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = tbl_MetaData_STACK.Godown_ID where tbl_MetaData_GODOWN.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'  group by Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,tbl_MetaData_GODOWN.Godown_ID order by tbl_MetaData_GODOWN.Godown_ID";
            string qrysel = "select tbl_MetaData_GODOWN.Godown_ID,Godown_Name,Godown_Capacity,isnull(SUM(ReceiptWts),0)as depositmsp,ISNULL(Godown_Capacity - SUM(ReceiptWts),0)as vacientcap from tbl_MetaData_GODOWN  left join tbl_MetaData_STACK on tbl_MetaData_GODOWN.Godown_ID = tbl_MetaData_STACK.Godown_ID  left join DailyStacking_TransactionStatus on DailyStacking_TransactionStatus.Stackid = tbl_MetaData_STACK.Stack_ID  where tbl_MetaData_GODOWN.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'   group by Godown_Name,tbl_MetaData_GODOWN.Hired_Type,tbl_MetaData_GODOWN.Storage_Type,Godown_Capacity,tbl_MetaData_GODOWN.Godown_ID order by tbl_MetaData_GODOWN.Godown_ID";
            SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblmaxcap.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString();

                }
            }

        }
        else
        {
        }

        # endregion

        string pqry = "available_space_godown";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmdpqty = new SqlCommand(pqry, con);
        cmdpqty.CommandType = CommandType.StoredProcedure;
        
        cmdpqty.Parameters.Add("@district_code", SqlDbType.NVarChar).Value = districtid;
        cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = ddlissuecenter.SelectedValue;
        cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = ddlgodown.SelectedValue.ToString();

        DataSet ds1 = new DataSet();
        SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

        dr.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            double stock = Math.Round(Convert.ToDouble(ds1.Tables[0].Rows[0]["Total"].ToString()), 5);

            lblcurr.Text = Convert.ToString(stock);

            double Max_Cap = Math.Round(Convert.ToDouble(CheckNull(lblmaxcap.Text)), 5);

            double availble = Max_Cap - stock;

            lblstockbal.Text = Convert.ToString(availble);
        }
    }

    decimal CheckNull(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;


    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/TO_against_MOfrmHO.aspx");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; GridView1.Rows.Count > i; i++)  // Multiple Godown Insert.
            {
                string MOrder = GridView1.Rows[i].Cells[0].Text.ToString();

                string recdist = ddlrecdist.SelectedValue;

                string Cropyear = GridView1.Rows[i].Cells[3].Text.ToString();

                string Transpoter = GridView1.DataKeys[i].Values[2].ToString();

                string Commodity = GridView1.DataKeys[i].Values[0].ToString();

                string Qty = GridView1.Rows[i].Cells[6].Text.ToString();

                string isscent = GridView1.DataKeys[i].Values[3].ToString();

                string branch = GridView1.DataKeys[i].Values[4].ToString();

                string godownid = GridView1.DataKeys[i].Values[1].ToString();

                string Rgodownid = GridView1.DataKeys[i].Values[6].ToString();

                string RIC = GridView1.DataKeys[i].Values[5].ToString();


                decimal qtyy = CheckNull(Qty.ToString());

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string maxtransdte = getDate_MDY(Recdate.Text);

                string insrt = "Insert into TO_against_MO (MO ,FrmDist ,FrmIsscen ,FrmBranch ,FrmGodown ,EndTrans_Date,Transporter ,TODist,ToIC,ToGdn ,Cropyear ,Commodity ,Quantity ,ByRoad, ByRack, CreatedDate ,IP ) Values ('" + MOrder + "','" + districtid + "','" + isscent + "','" + branch + "' ,'" + godownid + "','" + maxtransdte + "','" + Transpoter + "','" + recdist + "','"+RIC+"','"+Rgodownid+"','" + Cropyear + "','" + Commodity + "'," + qtyy + ",'Y','',getdate(),'" + ip + "')";
            
          
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

            try
            {

                SqlCommand cmd = new SqlCommand(insrt, con);

                int x = cmd.ExecuteNonQuery();

                if (x > 0)
                {

                    string upste = "Update StateMovementOrder set IsIssued = 'Y' where FrmDist = '"+districtid+"' and MoveOrdernum = '"+MOrder+"' and Commodity = '"+ddlcommodity.SelectedValue+"' and ModeofDispatch = '12'";

                    SqlCommand upcmd = new SqlCommand(upste,con);

                    upcmd.ExecuteNonQuery();

                    btnsave.Enabled = false;

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted'); </script> ");
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Insert '); </script> ");
                }
            }

            catch
            {
                Session["dt1"] = null;

                GridView1.DataSource = (DataTable)Session["dt1"];
                GridView1.DataBind();

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error on Insertion'); </script> ");
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

                    
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Before Insert , please click add this '); </script> ");
        }

        Session["dt1"] = null;

        GridView1.DataSource = (DataTable)Session["dt1"];
        GridView1.DataBind();
        
    }

    
    Int32 CheckNullInt(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

    private void RGetgodown()
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlrbranch.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlrgodown.DataSource = ds.Tables[0];
                        ddlrgodown.DataTextField = "Godown_Name";
                        ddlrgodown.DataValueField = "Godown_ID";
                        ddlrgodown.DataBind();
                        ddlrgodown.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }

    }

    public void GetRDepot(string rdist)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string ord = "Select DepotID , DepotName from tbl_MetaData_DEPOT where DistrictId = '23" + rdist + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(ord, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
        }
        else
        {
            ddlrIC.DataSource = ds.Tables[0];
            ddlrIC.DataTextField = "DepotName";
            ddlrIC.DataValueField = "DepotId";

            ddlrIC.DataBind();

            ddlrIC.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void getRbranch(string rdist)
    {
        try
        {
            if (cons != null)
            {
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }


                string qrysel = "select DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='23" + rdist + "'";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlrbranch.DataSource = ds.Tables[0];
                        ddlrbranch.DataTextField = "DepotName";
                        ddlrbranch.DataValueField = "DepotID";
                        ddlrbranch.DataBind();
                        ddlrbranch.Items.Insert(0, "--select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            cons.Close();
        }
        finally
        {
            cons.Close();
        }
    }


    protected void ddlrbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        RGetgodown();
    }

    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
