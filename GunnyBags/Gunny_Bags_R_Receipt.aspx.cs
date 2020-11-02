using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class GunnyBags_Gunny_Bags_R_Receipt : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;

    double RRQtyTotal = 0, WQtyTotal = 0, WRQtyTotal=0;
    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string All_RR_Wagon_Qty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";

                txtdistrict.Text = Session["dist_name"].ToString();
                string DistCode = Session["dist_id"].ToString();
                GetCropYear();
                //GetSociety();
                //GetBundletype();
                GetIndentNumber();
                GetSupplierName();
                string fromdate = Request.Form[txtDateofReceipt.UniqueID];
                txtDateofReceipt.Text = fromdate;

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //DateTime _date;
                //string day = "";
                //_date = DateTime.Parse("5/MAY/2012");
                //day = _date.ToString("dd-mm-yyyy");
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    public void GetIndentNumber()
    {
        if (ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }

        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    string DistCode = Session["dist_id"].ToString();
                    con.Open();
                    string select = string.Format("select distinct IndentNumber from Gunny_Bags_Indent_Creation where District='" + DistCode + "' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"' order by IndentNumber");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlIndentorNum.DataSource = ds.Tables[0];
                            ddlIndentorNum.DataTextField = "IndentNumber";
                            ddlIndentorNum.DataValueField = "IndentNumber";
                            ddlIndentorNum.DataBind();
                            ddlIndentorNum.Items.Insert(0, "--Select--");
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Indent Number is not available'); </script> ");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                }

                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
        }
    }

    protected void ddlIndentorNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRailHead.Items.Clear();
        txtQty.Text = "";
        txtDateOfDelivery.Text = "";
        if (ddlIndentorNum.SelectedIndex>0)
        {
            GetRailHead();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Indent Number'); </script> ");
            return;
        
        }
    }


    public void GetRailHead()
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("select RailHead_Destination, RH.RailHead_Name as RailHead_Name  from Gunny_Bags_Indent_Creation as IC INNER JOIN tbl_Rail_Head AS RH on RH.RailHead_Code=IC.RailHead_Destination and RH.district_code=IC.District WHERE IndentNumber='" + ddlIndentorNum.SelectedValue.ToString() + "' AND District='" + DistCode + "'  AND CropYear='" + ddlCropYear.SelectedValue.ToString() + "'  Order By RailHead_Name ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRailHead.DataSource = ds.Tables[0];
                        ddlRailHead.DataTextField = "RailHead_Name";
                        ddlRailHead.DataValueField = "RailHead_Destination";
                        ddlRailHead.DataBind();
                        ddlRailHead.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Indent Number is not available'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    
    }

    protected void ddlRailHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIndentorNum.SelectedIndex > 0)
        {
            GetRailHeadData();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is not available'); </script> ");
            return;

        }
    }
   
    public void GetRailHeadData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("select Quantity, CONVERT(varchar(10), DeliveryDate, 103 ) as DeliveryDate from Gunny_Bags_Indent_Creation where IndentNumber='"+ddlIndentorNum.SelectedValue.ToString()+"' and CropYear='"+ddlCropYear.SelectedValue.ToString()+"' and RailHead_Destination='"+ddlRailHead.SelectedValue.ToString()+"' and District='"+DistCode+"'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                        txtDateOfDelivery.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Quantity is not available'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    
    
    }
    public void GetSupplierName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistCode = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("select SupplierID, Supplier_Name from GunnyBags_SupplierMaster order by Supplier_Name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSupplierName.DataSource = ds.Tables[0];
                        ddlSupplierName.DataTextField = "Supplier_Name";
                        ddlSupplierName.DataValueField = "SupplierID";
                        ddlSupplierName.DataBind();
                        ddlSupplierName.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Supplier name is not available'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

    }
    protected void txtRRQuantity_TextChanged(object sender, EventArgs e)
    {
        if(Convert.ToInt64(txtRRQuantity.Text)>Convert.ToInt64(txtQty.Text))
        {
        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('RR Quantity will always be less than Indent Quantity'); </script> ");

            txtRRQuantity.Text="";
            txtRRQuantity.Focus();
                    return;
        }
        else if (Convert.ToInt64(txtRRQuantity.Text)<Convert.ToInt64(txtQty.Text))
        {
        
        }
    }
    protected void txtWagonQuantity_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt64(txtRRQuantity.Text) < Convert.ToInt64(txtWagonQuantity.Text))
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Wagon quantity will always be less than RR Qunatity '); </script> ");

            txtWagonQuantity.Text = "";
            txtWagonQuantity.Focus();
            return;
        }
        else if (Convert.ToInt64(txtRRQuantity.Text) > Convert.ToInt64(txtQty.Text))
        {

        }
    }
    protected void txtRcdWagQty_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt64(txtRcdWagQty.Text) > Convert.ToInt64(txtWagonQuantity.Text))
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Received Wagon quantity will always be less than or equal to Wagon quantity'); </script> ");

            txtRcdWagQty.Text = "";
            txtRcdWagQty.Focus();
            return;
        }
        else if (Convert.ToInt64(txtRRQuantity.Text) <= Convert.ToInt64(txtQty.Text))
        {

        }
    }

    protected void bttAddRR_Click(object sender, EventArgs e)
    {
        txtRailwayReceipt.Enabled = true;
        txtRailwayReceipt.Text = "";

        txtRRQuantity.Enabled = true;
        txtRRQuantity.Text = "";
        ddlSupplierName.ClearSelection();
        ddlSupplierName.Enabled = true;
    }
    protected void bttAdd_Click(object sender, EventArgs e)
    {

        if (ddlCropYear.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (ddlIndentorNum.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Indent Number'); </script> ");
            return;
        }
        else if (ddlRailHead.SelectedIndex<0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Head'); </script> ");
            return;
        }
        else if (ddlBagsType.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bags Type'); </script> ");
            return;
        }
        else if (txtDateofReceipt.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Date of Receipt'); </script> ");
            return;
        }
        else if (txtQty.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rail Head Quantity is not available'); </script> ");
            return;
        }
        else if (txtDateOfDelivery.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Date of delivery is not available'); </script> ");
            return;
        }
        else if (txtRailwayReceipt.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter RR number'); </script> ");
            return;
        }
        else if (txtRRQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter RR Qty(Bales)'); </script> ");
            return;
        }
        else if (txtWagonNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Wagon Number'); </script> ");
            return;
        }
        else if (txtRcdWagQty.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Received Wagon Quantity'); </script> ");
            return;
        }
        else if (txtWagonQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Wagon Qty(Bales)'); </script> ");
            return;
        }
        else
        {
            trGrid.Visible = true;
            DataTable dt = adddetails();
            if (dt == null)
            {
                dt = new DataTable("aadqty");
                dt.Columns.Add("fromdisttext");
                dt.Columns.Add("fromdistval");
                dt.Columns.Add("todistval");

                dt.Columns.Add("todisttext");
                // dt.Columns.Add("todistval");
                dt.Columns.Add("quantity");
                dt.Columns.Add("SupplierName");
                 dt.Columns.Add("SupplierID");
                 dt.Columns.Add("RCDquantity");
            }
            DataRow dr = dt.NewRow();
            dr["SupplierName"] = ddlSupplierName.SelectedItem.ToString();
            dr["SupplierID"] = ddlSupplierName.SelectedValue;
            dr["fromdisttext"] = txtRailwayReceipt.Text;
            dr["todisttext"] = txtRRQuantity.Text;
            dr["fromdistval"] = txtWagonNo.Text;
            dr["quantity"] = txtWagonQuantity.Text;
            dr["RCDquantity"] = txtRcdWagQty.Text;

            //dr["todisttext"] = txtNoOfBundle.Text;
            //dr["todistval"] = txtQuantity.Text;
            //ddlToDist.Items.FindByValue(ddlToDist.SelectedValue.ToString()).Enabled = ddlToDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;

            //dr["quantity"] = (float.Parse(txtQuantity.Text)).ToString("0.00");
            dt.Rows.Add(dr);
            Session["fdjfhxncdfh"] = dt;
            fillgrid();
            txtRailwayReceipt.Enabled = false;
            txtRRQuantity.Enabled = false;
            ddlSupplierName.Enabled = false;
            ddlBagsType.Enabled = false;
            txtWagonNo.Text = "";
            txtWagonQuantity.Text = "";
            txtRcdWagQty.Text = "";
            bttSubmit.Enabled = true;

            ddlCropYear.Enabled = false;
            ddlIndentorNum.Enabled = false;
            ddlSupplierName.Enabled = false;
            txtDateofReceipt.Enabled = false;
            ddlRailHead.Enabled = false;
            //Ddldist.SelectedIndex = 0;
            //ddlDestination.SelectedIndex = 0;
            ////txtQuantity.Text = "";
            //txtQuantity.Enabled = true;
            //txtQuantity.Text = "";



            ////ddlToDist.SelectedIndex = 0;
            ////txtQty.Text = "";
            ////btnRecptSubmit.Enabled = true;
            //txtIndNumber.Enabled = false;
            //txtIndDate.Enabled = false;
            //ddlMarSeason.Enabled = false;
            //txtColorCode.Enabled = false;
            //txtDelyDate.Enabled = false;
            //ddlCropYear.Enabled = false;
            //txtFundReq.Enabled = true;
            //txtConQty.Enabled = true;

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            WRQtyTotal = 0;
            WQtyTotal = 0;
           // RRQtyTotal = 0;

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            WRQtyTotal += (double.Parse(e.Row.Cells[6].Text));
            WQtyTotal += (double.Parse(e.Row.Cells[5].Text));
           // RRQtyTotal += (double.Parse(e.Row.Cells[3].Text));

            //if (e.Row.Cells[0].Text == "15")
            //{
            //    bttAdd.Enabled = false;
            //    ViewState["Row"] = "15";
            //   // Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            //}

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            e.Row.Cells[6].Text = WRQtyTotal.ToString();
            e.Row.Cells[5].Text = WQtyTotal.ToString();
            //e.Row.Cells[3].Text = RRQtyTotal.ToString();
            //All_RR_Wagon_Qty = e.Row.Cells[3].Text;
            txttotalquantity.Text = e.Row.Cells[5].Text;
            txtTotalRec_Qty.Text = e.Row.Cells[6].Text;

        }


    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("fromdisttext");
            dt.Columns.Add("fromdistval");
            dt.Columns.Add("todisttext");
            dt.Columns.Add("todistval");
            dt.Columns.Add("quantity");
        }
        else
        {
            string DistRowValue = dt.Rows[e.RowIndex]["fromdistval"].ToString();
            //ddlToDist.Items.FindByValue(DistRowValue).Enabled = true;
            dt.Rows.RemoveAt(e.RowIndex);

            if (ViewState["Row"].ToString() == "15")
            {
                bttAdd.Enabled = true;
                ViewState["Row"] = "0";
            }

        }
        Session["fdjfhxncdfh"] = dt;
        fillgrid();
    }
    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["fdjfhxncdfh"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            //GridView1.Columns[3].HeaderText = ddlComdtyMode.SelectedItem.ToString();
            GridView1.DataBind();
        }
    }

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (txtTotalRec_Qty.Text == "")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Total Received Quantity'); </script> ");
            return;
        }


        else
        {
            string DistCode = Session["dist_id"].ToString();
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(RR_Receive_ID) as RR_Receive_ID from GunnyBags_Receiving_RR_Dist where District='" + DistCode + "' and LEN(RR_Receive_ID)<15 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    //mobj1 = new MoveChallan(ComObj);
                    //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                    //DataSet ds = new DataSet();
                    // dmax.Fill(ds);
                    // DataTable dt = ds.Tables[""];
                    DataRow dr = ds.Tables[0].Rows[0];
                    //gatepass = dr["Inspector_ID"].ToString();
                    gatepass = ds.Tables[0].Rows[0]["RR_Receive_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "17170" + DistCode + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }


                    string strinsert = "";
                    DataTable dt = adddetails();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            string GBID_Multiple = gatepass + (i + 1);
                            ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                            string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDateofReceipt.Text);

                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            strinsert = "insert into GunnyBags_Receiving_RR_Dist(RR_Receive_ID, District, CropYear, Indent_Number, DateOf_Receipt, SupplierName, RR_No, RR_Qty, Wagon_No, Wagon_Qty, Total_Received_QTY, CreatedDate, All_RR_Wagon_Qty, IP, RR_Receive_ID_Multiple, BagsType, RailHead, WagonRCDQty) values ('" + gatepass + "','" + DistCode + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlIndentorNum.SelectedValue.ToString() + "','" + IssuedDeliveryDate + "','" + dt.Rows[i]["SupplierID"] + "','" + dt.Rows[i]["fromdisttext"] + "','" + dt.Rows[i]["todisttext"] + "','" + dt.Rows[i]["fromdistval"] + "','" + dt.Rows[i]["quantity"] + "','" + txtTotalRec_Qty.Text + "',getdate(),'" + txttotalquantity.Text + "','" + ip + "','" + GBID_Multiple + "','" + ddlBagsType.SelectedValue.ToString() + "','" + ddlRailHead.SelectedValue.ToString() + "','" + dt.Rows[i]["RCDquantity"] + "')";
                            cmd = new SqlCommand(strinsert, con);
                            string check = (string)cmd.ExecuteScalar();
                        }
                    }

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    trID.Visible = true;
                    Label1.Text = "Receiving ID is" + gatepass;
                    txtTotalRec_Qty.Enabled = false;
                    bttSubmit.Enabled = false;
                    // Fillgrid();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                    //ddl_Depart.Enabled = false;


                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
        }
    }
    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Gunny_Bags_Home.aspx");
    }


    
   
}