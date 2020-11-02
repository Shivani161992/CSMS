using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

public partial class PaddyMilling_Inspector_MasterDist : System.Web.UI.Page
{

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;

                txtDistrict.Text = Session["dist_name"].ToString();
                hdfDist.Value = Session["dist_id"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetMPIssueCentre();
                Fillgrid();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtFrmdate.Text = Request.Form[txtFrmdate.UniqueID];
        txtToDate.Text = Request.Form[txtToDate.UniqueID];
    }

    public void GetMPIssueCentre()
    {




        // string districtid = Session["district_code"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + hdfDist.Value + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIC.DataSource = ds.Tables[0];
                    ddlIC.DataTextField = "DepotName";
                    ddlIC.DataValueField = "DepotID";
                    ddlIC.DataBind();
                    ddlIC.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    protected void bttsub_Click(object sender, EventArgs e)
    {
        if (ddlIC.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
            return;
        }
        using (con = new SqlConnection(strcon))
            try
            {
                con.Open();
                string qrey = "select max(Inspector_ID) as Inspector_ID  from Inspector_Master_02017 where  LEN(Inspector_ID)<8 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);

                DataRow dr = ds.Tables[0].Rows[0];

                gatepass = ds.Tables[0].Rows[0]["Inspector_ID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "17" + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);

                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
            try
            {
                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(txtFrmdate.Text);
                string ConvertToDate = ServerDate.getDate_MDY(txtToDate.Text);

                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "insert into Inspector_Master_02017( district, Inspector_ID, Inspector_Name, Inspector_Desig, IssueCenter_code, Created_Date, IP, MobileNum, Frmdate, ToDate, SpecialStatus, Useragent ) values ('" + hdfDist.Value + "','" + gatepass + "','" + txtInspectorname.Text + "','" + txtDesignation.Text + "','" + ddlIC.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtMobNum.Text + "', '" + ConvertFromDate + "','" + ConvertToDate + "','NO', 'DM')";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }



        txtInspectorname.Text = "";
        txtDesignation.Text = "";
        txtMobNum.Text = "";
        txtMobtwo.Text = "";
        txtToDate.Text = "";
        txtFrmdate.Text = "";
        ddlIC.ClearSelection();
        Fillgrid();

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bttsub.Visible = false;
        bttupdate.Visible = true;
        //txt_dist.Text = GridView1.SelectedRow.Cells[2].Text.Trim();

        // ddlIC.SelectedValue = GridView1.SelectedRow.Cells[1].Text;
        ddlIC.SelectedItem.Text = GridView1.SelectedRow.Cells[1].Text;
        txtInspectorname.Text = GridView1.SelectedRow.Cells[3].Text;
        txtDesignation.Text = GridView1.SelectedRow.Cells[4].Text;


        if (GridView1.SelectedRow.Cells[5].Text == "&nbsp;")
        {
            txtMobNum.Text = "";
        }
        else
        {
            txtMobNum.Text = GridView1.SelectedRow.Cells[5].Text;
        }

        //decimal bdec = Convert.ToDecimal(GridView1.SelectedRow.Cells[5].Text);

        //int whole = (int)bdec;

        //int precision =Convert.ToInt32( (bdec - whole) * 100000);
        //txtMobNum.Text = Convert.ToString(whole);
        //txtMobtwo.Text=Convert.ToString(precision);

        txtFrmdate.Text = GridView1.SelectedRow.Cells[6].Text;
        txtToDate.Text = GridView1.SelectedRow.Cells[7].Text;


        hdfInspID.Value = GridView1.SelectedRow.Cells[2].Text;

        ddlIC.Enabled = false;
        txtInspectorname.Enabled = false;
        txtDesignation.Enabled = false;
        txtMobNum.Enabled = false;

    }

    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select Inspector_ID, Inspector_Name, Inspector_desig, DepotName, IssueCenter_code, district_name, MobileNum, convert(varchar(10),Frmdate, 103) as Frmdate , convert(varchar(10),ToDate, 103) ToDate from Inspector_Master_02017 as IM inner join tbl_MetaData_DEPOT as IC on IC.DepotID=IM.IssueCenter_code inner join pds.districtsmp as D on D.district_code=IM.district where IM.district='" + hdfDist.Value + "' and SpecialStatus='NO' order by DepotName, Inspector_Name, Inspector_desig  ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }

    protected void bttupdate_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
            try
            {
                ConvertServerDate ServerDate = new ConvertServerDate();
                string ConvertFromDate = ServerDate.getDate_MDY(txtFrmdate.Text);
                string ConvertToDate = ServerDate.getDate_MDY(txtToDate.Text);

                con.Open();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strselect = "Update Inspector_Master_02017 set MobileNum='" + txtMobNum.Text + "', Frmdate='" + ConvertFromDate + "',ToDate='" + ConvertToDate + "' where Inspector_ID='" + hdfInspID.Value + "'";
                cmd = new SqlCommand(strselect, con);
                string check = (string)cmd.ExecuteScalar();

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated successfully'); </script> ");

            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }



        txtInspectorname.Text = "";
        txtDesignation.Text = "";
        txtMobNum.Text = "";
        txtMobtwo.Text = "";
        txtToDate.Text = "";
        txtFrmdate.Text = "";
        ddlIC.ClearSelection();
        Fillgrid();

    }
}