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
public partial class Procurement_allocationEstimatedToPurchaseCenter : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    //Agency Aobj = null;
    //MarketingSeas Mobj = null;
    //cropYear cobj = null;
    //comodity CdObj = null;
    SqlString SqlObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ag_Name"] != null && Session["Ag_id"] != null && Session["Mark_Seas"] != null && Session["Markseas_id"] != null && Session["dist_id"] != null && Session["dist_name"] != null && Session["cropyear"] != null && Session["pc_name"] != null && Session["pcId"] != null)
        {
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());

            txt_pan_quant.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_pan_quant.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txt_pan_quant.Attributes.Add("onkeypress", "return blockNonNumbers(this, event, true, false)");
            txt_pan_quant.Style["text-align"] = "right";

          

            if (!IsPostBack)
            {
                GetAgencyName();
                GetMakSeas();
                GetCropYear();

                GetAgencyFromLog();
                GetMakseasfromLog();
                GetCropyearfromlog();

                GetCommodity();
                DDL_Agency.Enabled = false;
                DDL_CropYear.Enabled = false;
                DDL_MarkSeas.Enabled = false;

               fillGridDetail();
            }
        }
        else 
        {


            Response.Redirect("../sessionexpired.aspx");
        
        }
    }

    private void GetCropyearfromlog()
    {
        DDL_CropYear.Items.Insert(0, Session["cropyear"].ToString());
    }

    private void GetMakseasfromLog()
    {

        DDL_MarkSeas.Items.Insert(0, Session["Mark_Seas"].ToString());

    }

    private void GetAgencyFromLog()
    {
   
        DDL_Agency.Items.Insert(0, Session["Ag_Name"].ToString());

    }

    private void GetCommodity()
    {
        
        SqlObj=new SqlString (ComObj);
        string strcom = Session["Markseas_id"].ToString();
        string strsql = "SELECT * FROM CommodityMaster where MarkSeasId='" + strcom + "'";
        DataSet ds = SqlObj.selectAny(strsql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDL_Commodity.DataSource = ds.Tables[0];
            DDL_Commodity.DataTextField = "CommodityName";
            DDL_Commodity.DataValueField = "CommodityId";
            DDL_Commodity.DataBind();
        }
        else
        {

            // nothing

        }

    }

    private void GetCropYear()
    {
        SqlObj = new SqlString(ComObj);
        string str = "SELECT * FROM CropYearMaster";
        DataSet ds = SqlObj.selectAny(str);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_CropYear.DataSource = ds.Tables[0];
                DDL_CropYear.DataTextField = "CropYear";
                DDL_CropYear.DataValueField = "CropId";
                DDL_CropYear.DataBind();
            }

        }
     

    }

    private void GetMakSeas()
    {
        SqlObj = new SqlString(ComObj);
        string str = "SELECT * FROM MarketingSeasonMaster";
        DataSet ds = SqlObj.selectAny(str);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_MarkSeas.DataSource = ds.Tables[0];
                DDL_MarkSeas.DataValueField = "MarkSeasId";
                DDL_MarkSeas.DataTextField = "MarkSeaon";
                DDL_MarkSeas.DataBind();
            }


        }
      

    }

    private void GetAgencyName()
    {
        SqlObj = new SqlString(ComObj);
        string qry = "select * from PurchaseAgencyMaster  order by PurchaseAgencyName";
        DataSet ds = SqlObj.selectAny(qry);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Agency.DataSource = ds.Tables[0];
                DDL_Agency.DataTextField = "PurchaseAgencyName";
                DDL_Agency.DataValueField = "PurchaseAgencyId";
                DDL_Agency.DataBind();
            }
        }
       

    }

    private void fillGridDetail()
    {
        if (Session["pcId"] != null)
        {
            string Did = Session["dist_id"].ToString();
            string MrakId = Session["Markseas_id"].ToString();
            string Cropy = Session["cropyear"].ToString();
            string pcid = Session["pcId"].ToString();
            string PcType = Session["Ag_id"].ToString();
            SqlObj = new SqlString(ComObj);
            string strSql = "";
            strSql = "select EstimatedAllocationToPurchaseCenter.AllocationTargetId as AllocationTargetId, EstimatedAllocationToPurchaseCenter.Quantity as Quantity, PurchaseCenterMaster.PurchaseCenterName as PurchaseCenterName FROM EstimatedAllocationToPurchaseCenter left outer JOIN PurchaseCenterMaster ON EstimatedAllocationToPurchaseCenter.PcID = PurchaseCenterMaster.PcId where EstimatedAllocationToPurchaseCenter.DistrictID='" + Did + "' and EstimatedAllocationToPurchaseCenter.MarkSaesonId='" + MrakId + "'and EstimatedAllocationToPurchaseCenter.PcID='" + pcid + "' and EstimatedAllocationToPurchaseCenter.CropYear='" + Cropy + "' and EstimatedAllocationToPurchaseCenter.CommodityID='" + DDL_Commodity.SelectedValue + "'";
            //strSql = "select AllocationTargetId,Quantity ,PurchaseCenterMaster.PurchaseCenterName from EstimatedAllocationToPurchaseCenter left outer join PurchaseCenterMaster on EstimatedAllocationToPurchaseCenter.PcID=PurchaseCenterMaster.PcId where DistrictID='" + Did + "' and MarkSaesonId='" + MrakId + "'and PcID='" + pcid + "' and CropYear='" + Cropy + "'";
            DataSet ds = SqlObj.selectAny(strSql);

            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    txt_pan_quant.Text = dr["Quantity"].ToString();
                    lbl_PurGrid.Text = Session["pc_name"].ToString();
                    divPur.Visible = true;
                    //GridView1.Visible = true;
                    //GridView1.DataSource = ds;
                    // GridView1.DataBind();
                    lbl_u.Text = "U";
                    btn_save.Visible = true;
                }
                else
                {
                    //GridView1.Visible = false;
                    divPur.Visible = true;
                    lbl_PurGrid.Text = Session["pc_name"].ToString();
                    txt_pan_quant.Text = "";
                    txt_pan_quant.Focus();
                    btn_save.Visible = true;
                    lbl_u.Text = "I";

                }


            }


        }
        else 
        {

            Response.Redirect("../sessionexpired.aspx");
        
        
        }
       
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (Session["pcId"] != null)
        {
            string Did = Session["dist_id"].ToString();
            string aid = Session["cropyear"].ToString() + Session["Markseas_id"].ToString() + Session["dist_id"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string strsql = "";


            if (lbl_u.Text == "I")
            {
                if (txt_pan_quant.Text != "")
                {
                    strsql = "Insert into EstimatedAllocationToPurchaseCenter(AllocationTargetId,DistrictID,MarkSaesonId,PcID,CropYear,CommodityID,Quantity, PC_CategoryID, PCType,CreatedDate,ipAddress) values ('" + aid + "','" + Did + "','" + Session["Markseas_id"].ToString() + "','" + Session["pcId"].ToString() + "','" + Session["cropyear"].ToString() + "','" + DDL_Commodity.SelectedValue + "','" + Convert.ToDecimal(txt_pan_quant.Text) + "','" + Session["Ag_id"].ToString() + "','" + Session["pcId"].ToString() + "',getDate(),'" + ip + "')";
                    con.Open();
                    cmd = new SqlCommand(strsql, con);
                    cmd.ExecuteNonQuery();
                    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saves successfully.............'); </script> ");

                    btn_save.Enabled = false;
                    con.Close();
                }
                else
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity should not be blnak..'); </script> ");

                }
            }

            else
            {
                if (txt_pan_quant.Text != "")
                {
                    strsql = "Update EstimatedAllocationToPurchaseCenter set Quantity=" + txt_pan_quant.Text + "  where DistrictID='" + Session["dist_id"].ToString() + "' and MarkSaesonId='" + Session["Markseas_id"].ToString() + "' and PcID='" + Session["pcId"].ToString() + "' and CropYear='" + Session["cropyear"].ToString() + "' ";
                    con.Open();
                    cmd = new SqlCommand(strsql, con);
                    cmd.ExecuteNonQuery();
                    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saves successfully.............'); </script> ");
                    btn_save.Enabled = false;
                    con.Close();
                }
                else
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity should not be blnak..'); </script> ");

                }

            }

        }

        else
        {

            Response.Redirect("../sessionexpired.aspx");


        }
        //fillGridDetail();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillGridDetail();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGridDetail();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        if (Session["pcId"] != null)
        {
            TextBox txtQuant = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_EditQuantity");
            //txtQuant.Attributes.Add("onchange", "return CheckIsNumeric(event,this);");
            TextBox txtallid = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_ALLId");

            string sqlUP = "";
            sqlUP = "update  EstimatedAllocationToPurchaseCenter set Quantity='" + Convert.ToDecimal(txtQuant.Text) + "'  where AllocationTargetId='" + txtallid.Text + "'";
            con.Open();
            cmd = new SqlCommand(sqlUP, con);
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Upadted successfully.............'); </script> ");
            con.Close();
            GridView1.EditIndex = -1;
            fillGridDetail();
        }
        else
        {

            Response.Redirect("../sessionexpired.aspx");


        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
        }
        else
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox qtytxt = (TextBox)e.Row.Cells[2].FindControl("txt_EditQuantity");

                //TextBox txtQuant = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_EditQuantity");
                qtytxt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
                // qtytxt.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");

            }
        }

       
    }
    protected void DDL_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGridDetail();
    }
    protected void Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("allocationEstimatedToPurchaseCenter.aspx");
    }
}
