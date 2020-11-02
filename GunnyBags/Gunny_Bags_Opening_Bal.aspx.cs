using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_Gunny_Bags_Opening_Bal : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;


    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string New;
    string CutTorn;
    string Old;
    string Crop_Year;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";

                txtDist.Text = Session["dist_name"].ToString();
                string DistCode = Session["dist_id"].ToString();
                GetCropYear();
                GetSociety();
                GetBundletype();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }


    public void GetSociety()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //string DistCode = Session["dist_id"].ToString();

                string select = string.Format("select Society_Id, '('+ Society_Id +')'+',' + Society_Name_Eng as Society_Name_Eng from Society2016 where IsPaddy='Y' and DistrictId='23" + DistCode + "' order by Society_Name_Eng ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //ddllead.DataTextField = "Lead_Name";
                        //ddllead.DataValueField = "Lead_ID";
                        //ddllead.DataBind();
                        //ddllead.Items.Insert(0, "--Select--");

                        ddlsociety.DataSource = ds.Tables[0];
                        ddlsociety.DataTextField = "Society_Name_Eng";
                        ddlsociety.DataValueField = "Society_Id";
                        ddlsociety.DataBind();
                        ddlsociety.Items.Insert(0, "--Select--");


                    }
                }

                else 
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Society Is not available'); </script> ");
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

    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    public void GetBundletype()
    {
        if (ddlBagsType.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bag Type'); </script> ");
            return;
        }

        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string select = string.Format("select BundleID, BundleType from GunnyBags_BundleType");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlBundleType.DataSource = ds.Tables[0];
                            ddlBundleType.DataTextField = "BundleType";
                            ddlBundleType.DataValueField = "BundleID";
                            ddlBundleType.DataBind();
                            ddlBundleType.Items.Insert(0, "--Select--");
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
    }

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlsociety.SelectedIndex <0)
        {
            
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Society Name'); </script> ");
            return;
        }
        else if (ddlBagsType.SelectedIndex < 0)
        {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bag Type'); </script> ");
            return;
        }
        else if (rdbNew.Checked == true && ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
       
        else
        {
            string DistCode = Session["dist_id"].ToString();
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();
                    string qrey = "select max(GBpenID) as GBpenID from GunnyBags_OpeningBalance where DistrictID='" + DistCode + "' and LEN(GBpenID)<8 ";
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
                    gatepass = ds.Tables[0].Rows[0]["GBpenID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "17" + DistCode + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                    if (rdbNew.Checked == true)
                    {
                        New = "Y";
                        CutTorn = "N";
                        Old = "N";

                    }
                    else if (rdbCuttorn.Checked == true)
                    {
                        New = "N";
                        CutTorn = "Y";
                        Old = "N";
                    }
                    else if (rdbOld.Checked == true)
                    {
                        New = "N";
                        CutTorn = "N";
                        Old = "Y";
                    }

                    if (rdbNew.Checked == true)
                    {
                        Crop_Year = ddlCropYear.SelectedValue.ToString();
                    }
                    else
                    {
                        Crop_Year = "";
                    }
                    string strinsert = "";
                     DataTable dt = adddetails();
                     if (dt != null)
                     {
                         for (int i = 0; i < dt.Rows.Count; i++)
                         {

                             string GBID_Multiple = gatepass + (i + 1);

                             string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                             strinsert = "insert into GunnyBags_OpeningBalance(GBpenID, DistrictID, SocietyID, BagsType, New, Cut_Torn, Old, CropYear, BundleType, No_Of_Bundle, Quantity, CreatedDate, IP, GBID_MultipleID) values ('" + gatepass + "','" + DistCode + "','" + ddlsociety.SelectedValue.ToString() + "','" + ddlBagsType.SelectedValue.ToString() + "','" + New + "','" + CutTorn + "','" + Old + "','" + dt.Rows[i]["fromdisttext"] + "','" + dt.Rows[i]["fromdistval"] + "','" + dt.Rows[i]["todisttext"] + "','" + dt.Rows[i]["todistval"] + "',getdate(),'" + ip + "','" + GBID_Multiple + "')";
                             cmd = new SqlCommand(strinsert, con);
                             string check = (string)cmd.ExecuteScalar();
                         }
                     }
                    
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    ddlBagsType.Enabled = false;
                    ddlBundleType.Enabled = false;
                    ddlCropYear.Enabled = false;
                    ddlsociety.Enabled = false;
                    bttSubmit.Enabled = false;
                    txtNoOfBundle.Enabled = false;
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

    protected void rdbNew_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbNew.Checked == true)
        {
            trCropYear.Visible = true;
        
        }
    }
    protected void rdbCuttorn_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCuttorn.Checked == true)
        {
            trCropYear.Visible = false;
        }

    }
    protected void rdbOld_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbOld.Checked == true)
        {
            trCropYear.Visible = false;
        }
    }
    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void ddlBagsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsociety.SelectedIndex==0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Society name'); </script> ");
            ddlBagsType.ClearSelection();
            return;
        }
        if (ddlBagsType.SelectedValue.ToString() == "Jute")
        {
            ddlBundleType.ClearSelection();
            trbagtype.Visible = true;
            rdbCuttorn.Visible = true;
            rdbOld.Visible = true;

            rdbCuttorn.Checked = false;
            rdbOld.Checked = false;
            rdbNew.Checked = false;
            trCropYear.Visible = false;

        }
        else 
        {
            trbagtype.Visible = true;
            rdbCuttorn.Visible = false;
            rdbOld.Visible = false;
            rdbNew.Checked = true;
            trCropYear.Visible = true;
        }
    }
    protected void ddlBundleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rdbNew.Checked!=true && rdbCuttorn.Checked!=true && rdbOld.Checked!=true)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bags type from New, Cut& Torn and Old'); </script> ");
            ddlBundleType.ClearSelection();
            return;
        }
                
          
        if (ddlBundleType.SelectedItem.Text == "Loose Bags")
        {
            txtNoOfBundle.Text = "0";
            txtNoOfBundle.Enabled = false;
        }
        else
        {
            txtNoOfBundle.Text = "";
            txtNoOfBundle.Enabled = true;
        }
    }


    protected void bttAdd_Click(object sender, EventArgs e)
    {
        trGrid.Visible = true;
        ddlsociety.Enabled = false;
        ddlBagsType.Enabled = false;
        txtQuantity.Enabled = true;
        if (rdbNew.Checked == true)
        {
            rdbNew.Enabled = false;
            rdbCuttorn.Enabled = false;
            rdbOld.Enabled = false;
        }

        else if (rdbCuttorn.Checked == true)
        {
            rdbNew.Enabled = false;
            rdbCuttorn.Enabled = false;
            rdbOld.Enabled = false;
        }

        else if (rdbOld.Checked == true)
        {
            rdbNew.Enabled = false;
            rdbCuttorn.Enabled = false;
            rdbOld.Enabled = false;
        }


          if (ddlsociety.SelectedIndex <0)
        {
            
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Society Name'); </script> ");
            return;
        }
        else if (ddlBagsType.SelectedIndex < 0)
        {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bag Type'); </script> ");
            return;
        }
        else if (rdbNew.Checked == true && ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (ddlBundleType.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Bundle Type'); </script> ");
            return;
        }
        else if (txtNoOfBundle.Text == "")
        {
           
        }
         else if (txtQuantity.Text == "")
         {
             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity'); </script> ");
             return;
         }
         else
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
             DataRow dr = dt.NewRow();
             dr["fromdisttext"] = ddlCropYear.SelectedItem.Text;
             dr["fromdistval"] = ddlBundleType.SelectedItem.Text;
             dr["todisttext"] = txtNoOfBundle.Text;
             dr["todistval"] = txtQuantity.Text;
             //ddlToDist.Items.FindByValue(ddlToDist.SelectedValue.ToString()).Enabled = ddlToDist.Items.FindByValue(ddlFrmDist.SelectedValue.ToString()).Enabled = false;

             //dr["quantity"] = (float.Parse(txtQuantity.Text)).ToString("0.00");
             dt.Rows.Add(dr);
             Session["fdjfhxncdfh"] = dt;
             fillgrid();
             ddlCropYear.SelectedIndex = 0;
             ddlBundleType.SelectedIndex = 0;
             txtQuantity.Text = "";
             txtNoOfBundle.Enabled = true;
             txtNoOfBundle.Text = "";

             //ddlToDist.SelectedIndex = 0;
             //txtQty.Text = "";
             //btnRecptSubmit.Enabled = true;
         }
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
           // QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            //QtyTotal += (double.Parse(e.Row.Cells[3].Text));

            if (e.Row.Cells[0].Text == "15")
            {
                bttAdd.Enabled = false;
                ViewState["Row"] = "15";
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('You Have Alloted All 15 Dist. & You Can Not Add Another Dist. In Same MO'); </script> ");
            }

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

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }


    protected void txtNoOfBundle_TextChanged(object sender, EventArgs e)
    {
        int BundleType = Convert.ToInt32(ddlBundleType.SelectedItem.ToString());
        int NoOfBundle = Convert.ToInt32(txtNoOfBundle.Text);
        txtQuantity.Text = Convert.ToString(BundleType * NoOfBundle);
        txtQuantity.Enabled = false;

    }
}