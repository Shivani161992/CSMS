using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class mpproc_State_allocationEstimatedToAgency : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    SqlString SqlObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["StateLog_AgId"] != null)
        {

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            if (!IsPostBack)
            {
                GetAgencyName();
                GetMarkSeas();
                GetCrpyear();


                GetCommodity();
                DDL_Agency.Enabled = false;
                DDL_CropYear.Enabled = false;
                DDL_MarkSeas.Enabled = false;


                btn_save.Visible = false;
                
                btn_save.Attributes.Add("onclick", "fnChkCont();");
                btn_Update.Attributes.Add("onclick", "fnChkCont();");

            }
        }
        else
        {


            Response.Redirect("../../frmLogin.aspx");

        }
    }
    private void GetCrpyear()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string str = "SELECT CropId,CropYear FROM CropYearMaster  where CropId in('8') ";
            DataSet ds = SqlObj.selectAny(str);
            if (ds == null)
            {
            }
            else
            {

                DDL_CropYear.DataSource = ds.Tables[0];
                DDL_CropYear.DataTextField = "CropYear";
                DDL_CropYear.DataValueField = "CropId";
                DDL_CropYear.DataBind();
                DDL_CropYear.SelectedValue = "8";
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }




    }

    private void GetMarkSeas()
    {
        SqlObj = new SqlString(ComObj);
        DataSet ds = new DataSet();
        try
        {

            string Mid = Session["StateLog_MarkID"].ToString();
            string strSql = "SELECT MarkSeasId,MarkSeaon FROM MarketingSeasonMaster where MarkSeasId ='" + Mid + "'";

            ds = SqlObj.selectAny(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {

                DDL_MarkSeas.DataSource = ds.Tables[0];
                DDL_MarkSeas.DataTextField = "MarkSeaon";
                DDL_MarkSeas.DataValueField = "MarkSeasId";
                DDL_MarkSeas.DataBind();

            }


        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

        finally
        {
            SqlObj = null;
            ds = null;

        }

    }

    private void fillGridDetail(string ComID)
    {
        if (Session["StateLog_AgId"] != null)
        {
            ComID = DDL_Commodity.SelectedValue;
            string MrakId = Session["StateLog_MarkID"].ToString();
            string Cropy = Session["StateLog_CropY"].ToString();
            string PcType = Session["StateLog_AgId"].ToString();
            int value = 0;
            string strSql = "";
            strSql = "select count(*) as value from  EstimatedAllocationToAgency where  EstimatedAllocationToAgency.MarketingSeasonID='" + MrakId + "' and EstimatedAllocationToAgency.CropYear='" + Cropy + "' and EstimatedAllocationToAgency.CommodityID='" + ComID + "' and EstimatedAllocationToAgency.PCType_Id='" + DDL_Agency.SelectedValue + "'";
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandText = strSql;
                value = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (value == 0)
                {

                    string str = "SELECT District_Authorization.AgencyName as AgencyName,DistrictMaster.DistrictName as DistrictName,District_Authorization.Autho_Dist as Autho_Dist FROM District_Authorization,DistrictMaster where District_Authorization.Autho_Dist=DistrictMaster.DistrictId and District_Authorization.AgencyName='" + Session["StateLog_Agency"].ToString() + "' order by DistrictMaster.DistrictName";

                    cmd = new SqlCommand(str, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds2 = new DataSet();
                    da.Fill(ds2, "EAToAgency");
                    if (ds2 != null)
                    {

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            GridView2.Visible = false;
                            GridView2.DataSource = null;

                            GridView1.DataSource = ds2.Tables["EAToAgency"];
                            GridView1.DataBind();

                            GridView1.Visible = true;
                            btn_save.Visible = true;
                            btn_Update.Visible = false;
                            txt_total.Visible = false;
                            lbl_total.Visible = false;

                        }
                    }


                }

                else
                {
                    string str = "SELECT District_Authorization.AgencyName as AgencyName,DistrictMaster.DistrictName as DistrictName,District_Authorization.Autho_Dist as Autho_Dist, EstimatedAllocationToAgency.Quantity as Quantity  , EstimatedAllocationToAgency.AllocationTargetId  as AllocationTargetId FROM District_Authorization,DistrictMaster, EstimatedAllocationToAgency where District_Authorization.Autho_Dist=DistrictMaster.DistrictId and EstimatedAllocationToAgency.DistrictId=District_Authorization.Autho_Dist and EstimatedAllocationToAgency.MarketingSeasonID='" + MrakId + "' and EstimatedAllocationToAgency.CropYear='" + Cropy + "' and EstimatedAllocationToAgency.CommodityID='" + ComID + "' and EstimatedAllocationToAgency.PCType_Id='" + DDL_Agency.SelectedValue + "' order by DistrictMaster.DistrictName ";

                    cmd = new SqlCommand(str, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds2 = new DataSet();
                    da.Fill(ds2, "EstAllct");
                    if (ds2 != null)
                    {

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            GridView1.Visible = false;
                            GridView1.DataSource = null;

                            GridView2.Visible = true;
                            GridView2.DataSource = ds2.Tables["EstAllct"];
                            GridView2.DataBind();

                            GetTotal(DDL_Commodity.SelectedValue);
                            btn_save.Visible = false;
                            btn_Update.Visible = true;
                            txt_total.Visible = true;
                            lbl_total.Visible = true;

                        }
                    }


                }


            }
            catch (Exception ex)
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

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

    private void GetTotal(String Comid)
    {
        try
        {
            string MrakId = Session["StateLog_MarkID"].ToString();
            string Cropy = Session["StateLog_CropY"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string strTot = "SELECT Sum(EstimatedAllocationToAgency.Quantity) as TotalQuant  FROM EstimatedAllocationToAgency  where EstimatedAllocationToAgency.MarketingSeasonID='" + MrakId + "' and EstimatedAllocationToAgency.CropYear='" + Cropy + "' and EstimatedAllocationToAgency.CommodityID='" + Comid + "' and EstimatedAllocationToAgency.PCType_Id='" + DDL_Agency.SelectedValue + "'  ";
            cmd = new SqlCommand(strTot, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2, "EstTot");
            if (ds2 != null)
            {

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    String GTotal = (ds2.Tables[0].Rows[0]["TotalQuant"].ToString());
                    txt_total.Text = GTotal;

                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
        finally {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        
        }

    }
    private void GetAgencyName()
    {
        try
        {
            string agid = Session["StateLog_AgId"].ToString();
            SqlObj = new SqlString(ComObj);
            string qry = "select * from PurchaseAgencyMaster   where PCType_ID='" + agid + "'";
            DataSet ds = SqlObj.selectAny(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDL_Agency.DataSource = ds.Tables[0];
                    DDL_Agency.DataTextField = "PurchaseAgencyName";
                    DDL_Agency.DataValueField = "PCType_ID";
                    DDL_Agency.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");


        }
    }




    private void GetCommodity()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string strcom = Session["StateLog_MarkID"].ToString();
            string strsql = "SELECT * FROM CommodityMaster where MarkSeasId='" + strcom + "'";
            DataSet ds = SqlObj.selectAny(strsql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Commodity.DataSource = ds.Tables[0];
                DDL_Commodity.DataTextField = "CommodityName";
                DDL_Commodity.DataValueField = "CommodityId";
                DDL_Commodity.DataBind();
                DDL_Commodity.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

    }
    protected void DDL_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DDL_Commodity.SelectedItem.Text!="--Select--")
         {
        fillGridDetail(DDL_Commodity.SelectedValue);
         }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["StateLog_AgId"] != null && DDL_Commodity.SelectedItem.Text != "--Select--")
            {
                string PCType = Session["StateLog_Agency"].ToString();
                string PCTID = Session["StateLog_AgId"].ToString();
                string MS = Session["StateLog_MarkSeas"].ToString();
                string MSID = Session["StateLog_MarkID"].ToString();
                string CY = Session["StateLog_CropY"].ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                String[] strDistIDs = Request.Form["txt_GDid"].Split(Convert.ToChar(","));
                String[] strQuantity = Request.Form["txt_Quantity"].Split(Convert.ToChar(","));

                if (strDistIDs.Length > 0)
                {

                    for (Int32 icount = 0; icount <= Convert.ToInt32(strDistIDs.Length - 1); icount++)
                    {


                        string did = strDistIDs[icount];
                        if (strQuantity[icount] == "" || strQuantity[icount].Equals(null))
                        {
                            strQuantity[icount] = "00.00";

                        }
                        string aid = DDL_CropYear.SelectedItem.Text + DDL_MarkSeas.SelectedValue.ToString() + DDL_Commodity.SelectedValue.ToString() + did;
                        String strSql = String.Concat("INSERT INTO EstimatedAllocationToAgency(AllocationTargetId,MarketingSeasonID,CropYear,CommodityID,DistrictId,Quantity,CreatedDate,PCType,PCType_Id,ip)VALUES ('" + aid + "','" + MSID + "','" + CY + "','" + DDL_Commodity.SelectedValue + "','", strDistIDs[icount], "','", strQuantity[icount], "',getDate(),'" + PCType + "','" + PCTID + "','" + ip + "')");

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd = new SqlCommand(strSql, con);

                        cmd.ExecuteNonQuery();
                     

                    }

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved successfully.............'); </script> ");
                    btn_save.Enabled = false;
                  
                    fillGridDetail(DDL_Commodity.SelectedValue);
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
        finally {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        fillGridDetail(DDL_Commodity.SelectedValue);

    }

    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        fillGridDetail(DDL_Commodity.SelectedValue);
    }
    //protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    if (Session["StateLog_AgId"] != null && DDL_Commodity.SelectedItem.Text != "--Select--")
    //    {
    //        string MrakId = Session["StateLog_MarkID"].ToString();
    //        string Cropy = Session["StateLog_CropY"].ToString();
    //        string PcType = Session["StateLog_AgId"].ToString();
    //         string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

    //        TextBox txtQuant = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_EditQuantity");
    //        TextBox txtallid = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_ALLId");

    //        if (txtQuant.Text == "" || txtQuant.Text.Equals(null))
    //        {
    //            txtQuant.Text = "0.00";

    //        }

    //        string sqlUP = "";
    //        sqlUP = "update   EstimatedAllocationToAgency set Quantity='" + Convert.ToDecimal(txtQuant.Text) + "',UpdatedBy='" + ip + "',UpdatedDate=getDate()  where EstimatedAllocationToAgency.MarketingSeasonID='" + MrakId + "' and EstimatedAllocationToAgency.CropYear='" + Cropy + "' and EstimatedAllocationToAgency.CommodityID='" + DDL_Commodity.SelectedValue + "' and EstimatedAllocationToAgency.PCType_Id='" + DDL_Agency.SelectedValue + "' and   AllocationTargetId='" + txtallid.Text + "'";

    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }
    //        cmd = new SqlCommand(sqlUP, con);
    //        cmd.ExecuteNonQuery();

    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        } 
    //        GridView2.EditIndex = -1;
    //        fillGridDetail();
    //    }
    //}

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["StateLog_AgId"] != null && DDL_Commodity.SelectedItem.Text != "--Select--")
            {
                string MrakId = Session["StateLog_MarkID"].ToString();
                string Cropy = Session["StateLog_CropY"].ToString();
                string PcType = Session["StateLog_AgId"].ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                String[] strDistIDs = Request.Form["txt_GDid"].Split(Convert.ToChar(","));
                String[] strQuantity = Request.Form["txt_Quantity"].Split(Convert.ToChar(","));

                if (strDistIDs.Length > 0)
                {

                    for (Int32 icount = 0; icount <= Convert.ToInt32(strDistIDs.Length - 1); icount++)
                    {

                        if (strQuantity[icount] == "" || strQuantity[icount].Equals(null))
                        {
                            strQuantity[icount] = "00.00";

                        }

                        string sqlUP = "";
                        //sqlUP = "update   EstimatedAllocationToAgency set Quantity='" + Convert.ToDecimal(strQuantity[icount]) + "',UpdatedBy='" + ip + "',UpdatedDate=getDate()  where EstimatedAllocationToAgency.MarketingSeasonID='" + MrakId + "' and EstimatedAllocationToAgency.CropYear='" + Cropy + "' and EstimatedAllocationToAgency.CommodityID='" + DDL_Commodity.SelectedValue + "' and EstimatedAllocationToAgency.PCType_Id='" + DDL_Agency.SelectedValue + "' and   AllocationTargetId='" + txtallid.Text + "'";
                        sqlUP = String.Concat("update  EstimatedAllocationToAgency set Quantity='" + Convert.ToDecimal(strQuantity[icount]) + "',UpdatedBy='" + ip + "',UpdatedDate=getDate()  where EstimatedAllocationToAgency.MarketingSeasonID='" + MrakId + "' and EstimatedAllocationToAgency.CropYear='" + Cropy + "' and EstimatedAllocationToAgency.CommodityID='" + DDL_Commodity.SelectedValue + "' and EstimatedAllocationToAgency.PCType_Id='" + DDL_Agency.SelectedValue + "' and EstimatedAllocationToAgency.DistrictId='", strDistIDs[icount], "'");


                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd = new SqlCommand(sqlUP, con);

                        cmd.ExecuteNonQuery();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Updated successfully.............'); </script> ");
                        btn_Update.Enabled = false;
                      
                        fillGridDetail(DDL_Commodity.SelectedValue);


                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
        finally
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("allocationEstimatedToAgency.aspx");
    }
}
