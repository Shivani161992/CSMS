using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_GunnyBags_IndentCreation : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    //  string strconuparjan = ConfigurationManager.ConnectionStrings["uparjan"].ConnectionString;      //uparjan
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;

    double QtyTotal = 0;
    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";

                //txtDist.Text = Session["dist_name"].ToString();
                //string DistCode = Session["dist_id"].ToString();
                GetCropYear();
                GetDist();
                // GetSociety();
                // GetBundletype();
                txtIndName.Text = "Madhya Pradesh";
                txtAgeName.Text = "MP State Civil Supplies Corporation Limited";
                txtStatus.Text = "New";
                txtConsigneeName.Text = "District Manager";

                string fromdate = Request.Form[txtIndDate.UniqueID];
                txtIndDate.Text = fromdate;
                string toDate = Request.Form[txtDelyDate.UniqueID];
                txtDelyDate.Text = toDate;


                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Ddldist.DataSource = ds.Tables[0];
                        Ddldist.DataTextField = "district_name";
                        Ddldist.DataValueField = "district_code";
                        Ddldist.DataBind();
                        Ddldist.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
                    }
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

    public void GetRailHead()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select  RailHead_Code , RailHead_Name from dbo.tbl_Rail_Head where district_code='" + Ddldist.SelectedValue.ToString() + "' order by RailHead_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDestination.DataSource = ds.Tables[0];
                        ddlDestination.DataTextField = "RailHead_Name";
                        ddlDestination.DataValueField = "RailHead_Code";
                        ddlDestination.DataBind();
                        ddlDestination.Items.Insert(0, "--Select--");
                        //Ddldist.SelectedValue = Session["dist_id"].ToString();
                        // GetMPIssueCentre();
                    }
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

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDestination.ClearSelection();
        if (Ddldist.SelectedIndex > 0)
        {
            GetRailHead();
        }
       
    }

    protected void bttAdd_Click(object sender, EventArgs e)
    {

        if (Ddldist.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District Name'); </script> ");
            return;
        }
        else if (ddlDestination.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Head Destination'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Head Destination'); </script> ");
            return;
        }

        else if (txtDelyDate.Text =="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Head Destination'); </script> ");
            return;
        }

        else if (ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (txtQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity'); </script> ");
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
            }
            DataRow dr = dt.NewRow();
            dr["fromdisttext"] = Ddldist.SelectedItem.Text;
            dr["todisttext"] = Ddldist.SelectedValue.ToString();
            dr["fromdistval"] = ddlDestination.SelectedItem.Text;
            dr["quantity"] = ddlDestination.SelectedValue.ToString();

            //dr["todisttext"] = txtNoOfBundle.Text;
            dr["todistval"] = txtQuantity.Text;
            //ddlToDist.Items.FindByValue(ddlToDist.SelectedValue.ToString()).Enabled = ddlToDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;

            //dr["quantity"] = (float.Parse(txtQuantity.Text)).ToString("0.00");
            dt.Rows.Add(dr);
            Session["fdjfhxncdfh"] = dt;
            fillgrid();
            Ddldist.SelectedIndex = 0;
            ddlDestination.SelectedIndex = 0;
            //txtQuantity.Text = "";
            txtQuantity.Enabled = true;
            txtQuantity.Text = "";

           

            //ddlToDist.SelectedIndex = 0;
            //txtQty.Text = "";
            //btnRecptSubmit.Enabled = true;
            txtIndNumber.Enabled = false;
            txtIndDate.Enabled = false;
            ddlMarSeason.Enabled = false;
            txtColorCode.Enabled = false;
            txtDelyDate.Enabled = false;
            ddlCropYear.Enabled = false;
            txtFundReq.Enabled = true;
            txtConQty.Enabled = true;

        }
    }
    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (txtConQty.Text=="")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Confirm Quantity'); </script> ");
            return;
        }
        else if (txtFundReq.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Fund Required'); </script> ");
            return;
        }
        else if (txtConQty.Text != txtqty.Text)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Indented Quantity and Confirm Quantity does not match. Please check and enter again.'); </script> ");
            return;
        }

        else
        {
           // string DistCode = Session["dist_id"].ToString();
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(IndentID) as IndentID from Gunny_Bags_Indent_Creation where  LEN(IndentID)<8 ";
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
                    gatepass = ds.Tables[0].Rows[0]["IndentID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "17" + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string IssuedDate = ServerDate.getDate_MDY(txtIndDate.Text);

                    ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                    string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDelyDate.Text);

                  
                    string strinsert = "";
                    DataTable dt = adddetails();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            string GBID_Multiple = gatepass + (i + 1);

                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            strinsert = "insert into Gunny_Bags_Indent_Creation(IndentID, IndentorName, IndentNumber, IndentDate, Marketing_Season, Color_Code, Indented_Quantity, Confirm_Quantity, Fund_Required, DeliveryDate, CropYear, Status, District, RailHead_Destination, Quantity, CreatedDate, IP, IndentIDMultiple) values ('" + gatepass + "','MP','" + txtIndNumber.Text + "','" + IssuedDate + "','" + ddlMarSeason.SelectedValue.ToString() + "','" + txtColorCode.Text + "','" + txtqty.Text + "','" + txtConQty.Text + "','" + txtFundReq.Text + "','" + IssuedDeliveryDate + "','" + ddlCropYear.SelectedValue.ToString() + "','" + txtStatus.Text + "','" + dt.Rows[i]["todisttext"] + "','" + dt.Rows[i]["quantity"] + "','" + dt.Rows[i]["todistval"] + "',getdate(),'" + ip + "','" + GBID_Multiple + "')";
                            cmd = new SqlCommand(strinsert, con);
                            string check = (string)cmd.ExecuteScalar();
                        }
                    }

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    Ddldist.Enabled = false;
                    ddlDestination.Enabled = false;
                    ddlCropYear.Enabled = false;
                    txtFundReq.Enabled = false;
                    bttSubmit.Enabled = false;
                    txtConQty.Enabled = false;
                    txtQuantity.Enabled = false;
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[3].Text));

            if (e.Row.Cells[0].Text == "15")
            {
                bttAdd.Enabled = false;
                ViewState["Row"] = "15";
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            e.Row.Cells[3].Text = QtyTotal.ToString();
            txtqty.Text = e.Row.Cells[3].Text;
           
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
    protected void ddlMarSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtIndNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Indent Number'); </script> ");
            ddlMarSeason.ClearSelection();
            return;
        }

        else if (txtIndDate.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Indent Date'); </script> ");
            ddlMarSeason.ClearSelection();
            return;
        }
        else if (ddlMarSeason.SelectedIndex>0)
        {
            //Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Indent Date'); </script> ");
            //return;
        }

    }
    protected void txtFundReq_TextChanged(object sender, EventArgs e)
    {
        if (txtColorCode.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Color Code'); </script> ");
            // ddlMarSeason.ClearSelection();
            ddlCropYear.ClearSelection();
            return;
        }
    }
}