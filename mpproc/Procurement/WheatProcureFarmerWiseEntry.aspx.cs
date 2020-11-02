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
public partial class Procurement_WheatProcureFarmerWiseEntry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";  
    comodity CdObj = null;
    SqlString SqlObj = null;
    DataReader objDr = null;
    Districts distObj = null;
    string dist = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] != null)
        {

            dist = Session["dist_id"].ToString();

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());

            txt_paidAmount.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_paidAmount.Attributes.Add("onkeyup", "extractNumber(this,2,false)");

            btn_update.Attributes.Add("onclick", "chkFields();");
            btn_AddNew.Attributes.Add("onclick", "chkFields();");

            txtQuan.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txt_Remark.Attributes.Add("onkeypress", "return CheckIsChar(event,this)");

            txt_Remark.Attributes.Add("onkeypress", "return CheckIsChar(event,this)");


            DaintyDate2.Attributes.Add("onkeypress", "return validateDatenew(this)");
            DaintyDate1.Attributes.Add("onkeypress", "return validateDatenew(this)");

            if (!IsPostBack)
            {

                DaintyDate1.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                DaintyDate2.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                GetCommodity();
                GetPc();
                GetDist();
                getTah();
                GetInfo();
            }
        }
        else
        {

            Response.Redirect("../sessionexpired.aspx");


        }
    }

    private void getTah()
    {
        try
        {
            if (DDL_Dist.SelectedItem.Text != "--Select--")
            {
                SqlObj = new SqlString(ComObj);
                string strSql = "select * from LR_TehsilMaster tm,DistrictMaster dm where dm.DistNo_LRMP=tm.Distno and dm.DistrictCode='" + DDL_Dist.SelectedValue + "'";
                DataSet ds = SqlObj.selectAny(strSql);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DDL_Tah.DataSource = ds.Tables[0];
                        DDL_Tah.DataTextField = "Tehsilname";
                        DDL_Tah.DataValueField = "Tehsilno";
                        DDL_Tah.DataBind();
                        DDL_Tah.Items.Insert(0, "--Select--");
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");


        }

    }

    private void GetPc()
    {
        if (Session["pcId"] != null)
        {
            DDL_PC.Items.Insert(0, Session["pc_name"].ToString());
        }

    }

    private void GetDist()
    {
        try
        {
            distObj = new Districts(ComObj);
            DataSet ds = distObj.selectmp(dist);

            if(ds!=null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDL_Dist.DataSource = ds.Tables[0];
                    DDL_Dist.DataTextField = "DistrictName";
                    DDL_Dist.DataValueField = "DistrictCode";
                    DDL_Dist.DataBind();

                    DDL_Dist.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

    }

    private void GetInfo()
    {
        if (Session["pcId"] != null)
        {
            lbl_DistRes.Text = Session["dist_name"].ToString();
            lbl_AgencyRes.Text = Session["Ag_Name"].ToString();
            lbl_MarSeasRes.Text = Session["Mark_Seas"].ToString();
            lbl_CropYearRes.Text = Session["cropyear"].ToString();
        }
    }

    private void GetCommodity()
    {
        try
        {
            if (Session["pcId"] != null)
            {
                CdObj = new comodity(ComObj);
                string strcom = Session["Markseas_id"].ToString();
                string strsql = "SELECT * FROM CommodityMaster where MarkSeasId='" + strcom + "'";
                DataSet ds = CdObj.selectAny(strsql);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DDL_Commodity.DataSource = ds.Tables[0];
                        DDL_Commodity.DataTextField = "CommodityName";
                        DDL_Commodity.DataValueField = "CommodityId";
                        DDL_Commodity.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

    }
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDL_Tah_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL_Tah.SelectedItem.Text != "--Select--")
            {


                SqlObj = new SqlString(ComObj);
                string strsql = "  select * from LR_VillageMaster vm,DistrictMaster dm,LR_TehsilMaster tm  where dm.DistNo_LRMP=vm.Distno and tm.Tehsilno=vm.Tehsilno and tm.Distno=dm.DistNo_LRMP and dm.DistrictCode='" + DDL_Dist.SelectedValue + "' and vm.Tehsilno='" + DDL_Tah.SelectedValue + "'";

                DataSet ds = SqlObj.selectAny(strsql);
                if(ds!=null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DDL_Village.DataSource = ds.Tables[0];

                        DDL_Village.DataTextField = "Villagename";
                        DDL_Village.DataValueField = "Villageno";
                        DDL_Village.DataBind();
                        DDL_Village.Items.Insert(0, "--Select--");
                    }
                }

            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }



    private void FarmerDetail()
    {
        try
        {
            if (DDL_Dist.SelectedItem.Text != "--Select--" && DDL_Tah.SelectedItem.Text != "--Select--" && DDL_Village.SelectedItem.Text != "--Select--")
            {
                SqlObj = new SqlString(ComObj);
                string strSql = "select FarmerId,FarmerName,FatherName,FarmerName + '(' + FatherName + ')' as FarrmName  from FarmerDetails where DistrictId='" + DDL_Dist.SelectedValue + "' and Taluk_Code='" + DDL_Tah.SelectedValue + "'and Village_Code='" + DDL_Village.SelectedValue + "' order by FarmerName";

                DataSet ds = SqlObj.selectAny(strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DDL_Farmer.DataSource = ds.Tables[0];
                        DDL_Farmer.DataTextField = "FarrmName";
                        DDL_Farmer.DataValueField = "FarmerId";
                        DDL_Farmer.DataBind();
                        DDL_Farmer.Items.Insert(0, "--Select--");

                    }
                }
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }

    protected void DDL_Farmer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL_Dist.SelectedItem.Text != "--Select--" && DDL_Tah.SelectedItem.Text != "--Select--" && DDL_Village.SelectedItem.Text != "--Select--")
            {
                SqlObj = new SqlString(ComObj);
                string strSql = "select * from FarmerDetails where FarmerId='" + DDL_Farmer.SelectedValue + "'";

                DataSet ds = SqlObj.selectAny(strSql);
                if(ds!=null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        lbl_rcNoRes.Text = ds.Tables[0].Rows[0]["RationCardNo"].ToString();
                        lbl_RcType.Text = ds.Tables[0].Rows[0]["RationCardType"].ToString();
                        lbl_KhasaraRes.Text = ds.Tables[0].Rows[0]["KhasaraNo"].ToString();
                        lbl_khRes.Text = ds.Tables[0].Rows[0]["PatwariHalkaNo"].ToString();
                        lbl_B1Res.Text = ds.Tables[0].Rows[0]["B1_No"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

    }
    protected void txtQuan_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["pcId"] != null)
            {
                if (txtQuan.Text != "")
                {
                    string crpy = Session["cropyear"].ToString();
                    string strSql = "select Bonus ,Rate from  CommodityRate where CommodityId='" + DDL_Commodity.SelectedValue + "' and  CropYear='" + crpy + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(strSql, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        decimal rat = Convert.ToDecimal(dr[0].ToString()) + Convert.ToDecimal(dr[1].ToString());
                        decimal pay2farmer = Convert.ToDecimal(txtQuan.Text) * rat;
                        txtAmount.Text = pay2farmer.ToString();
                    }

                }
                else
                {
                    txtAmount.Text = "";

                }
            }
            else
            {

                Response.Redirect("../sessionexpired.aspx");


            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }
    }
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        string sta = "";
        string PayMod = "";
        string paidAm = "";
        string ChDate = "";
        string ChNo = "";

        if (Session["pcId"] != null)
        {

            try
            {
                string ProcDate = getDate_MDY(DaintyDate1.Text);
                string pcid = Session["Ag_id"].ToString();

                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                Int64 gdnid = Convert.ToInt64(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());


                if (RD_Btn_Yes.Checked == true)
                {
                    PayMod = "Cheque";
                    paidAm = txt_paidAmount.Text;
                    ChDate = getDate_MDY(DaintyDate2.Text);
                    ChNo = txt_chno.Text;
                    sta = "Active";

                }
                else
                {

                    PayMod = "";
                    paidAm = "0.0";
                    ChDate = "";
                    ChNo = "00";
                    sta = "InActive";

                }

                if (DDL_Farmer.SelectedIndex > 0)
                {
                    if (txtQuan.Text != "" || txtAmount.Text != "")
                    {
                        SqlObj = new SqlString(ComObj);
                        string strqsl = "INSERT INTO CommodityProcurementByAgencyFromFarmer(ProcAgentFarmerID,PCType_ID_Agency,DistrictId,CropYear,MarketingSeasonId,PCID,ProcurementDate,FarmerId,CommodityId,QtyProcured,Amt_Payable_to_farmer,Amt_Paid ,Mode_Of_Payment ,Cheque_No,Cheque_date,Date_Of_Creation,Date_Of_Updation ,Status,ip,RakbaNo,Remark) values ( '" + gdnid + "','" + Session["Ag_id"].ToString() + "','" + DDL_Dist.SelectedValue + "','" + lbl_CropYearRes.Text + "','" + Session["Markseas_id"].ToString() + "','" + Session["pcId"].ToString() + "','" + ProcDate + "','" + DDL_Farmer.SelectedValue + "','" + DDL_Commodity.SelectedValue + "','" + txtQuan.Text + "','" + txtAmount.Text + "','" + paidAm + "','" + PayMod + "','" + ChNo + "','" + ChDate + "',getDate(),'','" + sta + "','" + ip + "','" + txt_rkbNo.Text + "','" + txt_Remark.Text + "')";

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd = new SqlCommand(strqsl, con);
                        cmd.ExecuteNonQuery();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved successfully.............'); </script> ");
                        btn_AddNew.Enabled = false;
                        if (con.State == ConnectionState.Open)
                        {

                            con.Close();

                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Enter Quantity..'); </script> ");

                    }
                }

                else
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Farmer Name..'); </script> ");

                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

            }
        }

        else
        {

            Response.Redirect("../sessionexpired.aspx");


        }

    }
    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }

    void GetFarmerDetails(string farmerid)
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string qry = "SELECT *  FROM FarmerDetails where FarmerId='" + farmerid + "'";
            DataSet ds = SqlObj.selectAny(qry);

            if(ds !=null) 
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    string tehsfil = dr["Taluk_Code"].ToString();
                    string village = dr["Village_Code"].ToString();
                    string farmername = dr["FarmerName"].ToString();

                    string khasara = dr["KhasaraNo"].ToString();
                    string b1 = dr["B1_No"].ToString();
                    string rationno = dr["RationCardNo"].ToString();
                    string rationtype = dr["RationCardType"].ToString();
                    string halkano = dr["PatwariHalkaNo"].ToString();


                    lbl_rcNoRes.Text = rationno;
                    lbl_RcType.Text = rationtype;
                    lbl_KhasaraRes.Text = khasara;
                    lbl_khRes.Text = halkano;
                    lbl_B1Res.Text = b1;

                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }

    }
    protected void RD_Btn_No_CheckedChanged(object sender, EventArgs e)
    {

        lbl_paidAm.Visible = false;
        lbl_chNo.Visible = false;
        lbl_Cheque.Visible = false;


        txt_paidAmount.Visible = false;
        txt_chno.Visible = false;
        DaintyDate2.Visible = false;
        txt_paidAmount.Text = "";
        txt_chno.Text = "";
      



    }


    protected void RD_Btn_Yes_CheckedChanged(object sender, EventArgs e)
    {

        lbl_paidAm.Visible = true;
        lbl_chNo.Visible = true;
        lbl_Cheque.Visible = true;

        txt_paidAmount.Visible = true;
        txt_chno.Visible = true;
        DaintyDate2.Visible = true;
  
        txt_paidAmount.Focus();



    }
    private void fillgrid()
    {
        try
        {
            string prdate = getDate_MDY(DaintyDate1.Text);
            if (Session["pcId"] != null)
            {
                SqlObj = new SqlString(ComObj);

                string str = "select  distinct fd.FarmerName,cf.ProcAgentFarmerID,cf.Remark, cf.RakbaNo,fd.FarmerId,cm.CommodityName,cf.QtyProcured,cf.Amt_Payable_to_farmer as AmtPayable ,cf.Amt_Paid as AmtPaid,cf.Cheque_No as ChequeNo ,cf.Cheque_date as Chequedate,cf.Status from FarmerDetails fd,CommodityProcurementByAgencyFromFarmer cf,PurchaseCenterMaster,CommodityMaster cm where   cf.CommodityId=cm.CommodityId and fd.FarmerId=cf.FarmerId and cf.CommodityId='" + DDL_Commodity.SelectedValue + "'  and cf.DistrictId='" + DDL_Dist.SelectedValue + "' and cf.PCID='" + Session["pcId"].ToString() + "'  and cf.ProcurementDate='" + prdate + "' order by fd.FarmerName";

                DataSet ds = SqlObj.selectAny(str);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                }
                else
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not Found'); </script> ");

                }

            }

            else
            {

                Response.Redirect("../sessionexpired.aspx");


            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

    }
    protected void btn_fecth_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void DDL_Village_SelectedIndexChanged(object sender, EventArgs e)
    {
        FarmerDetail();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string st = GridView1.SelectedRow.Cells[9].Text.ToString();

        if (st == "Active")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Can not be Updated the payement is alrady made for the selected transaction'); </script> ");

        }
        else
        {

            txtQuan.Text = GridView1.SelectedRow.Cells[4].Text.ToString();
            txtAmount.Text = GridView1.SelectedRow.Cells[5].Text.ToString();


            txt_rkbNo.Text = "";

            txt_chno.Text = "";
            lblfid.Text = GridView1.SelectedRow.Cells[2].Text.ToString();
            GetFarmerDetails(lblfid.Text);


            btn_AddNew.Visible = false;
            btn_update.Visible = true;

            lbl_Tahsil.Visible = false;
            lbl_village.Visible = false;

            DDL_Tah.Visible = false;
            DDL_Village.Visible = false;
            lbl_FarSelection.Visible = false;
            lblfdetail.Visible = true;
            lblfrname.Visible = true;
            txtFarmer.Visible = true;
            lbl_Farmer.Visible = false;
            DDL_Farmer.Visible = false;
            txtFarmer.Text = GridView1.SelectedRow.Cells[1].Text.ToString();

            if (GridView1.SelectedRow.Cells[10].Text == null || GridView1.SelectedRow.Cells[10].Text == "&nbsp;")
            {
                txt_rkbNo.Text = "";

            }
            else
            {

                txt_rkbNo.Text = GridView1.SelectedRow.Cells[10].Text.ToString();
            }
            if (GridView1.SelectedRow.Cells[11].Text == null || GridView1.SelectedRow.Cells[11].Text == "&nbsp;")
            {
                txt_Remark.Text = "";

            }
            else
            {

                txt_Remark.Text = GridView1.SelectedRow.Cells[11].Text.ToString();
            }



        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("WheatProcureFarmerWiseEntry.aspx");
    }
    protected void DDL_PC_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string getdatef = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Chequedate"));

            getdatef = getdate(griddate);

            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (Session["pcId"] != null)
        {
            try
            {

                string sta = "";
                string PayMod = "";
                string paidAm = "";
                string ChDate = "";
                string ChNo = "";
                if (RD_Btn_Yes.Checked == true)
                {
                    PayMod = "Cheque";
                    paidAm = txt_paidAmount.Text;
                    ChDate = getDate_MDY(DaintyDate2.Text);
                    ChNo = txt_chno.Text;
                    sta = "Active";

                }
                else
                {

                    PayMod = "";
                    paidAm = "0.0";
                    ChDate = "";
                    ChNo = "00";
                    sta = "InActive";

                }



                if (txtQuan.Text != "")
                {
                    SqlObj = new SqlString(ComObj);
                    string strqsl = "update  CommodityProcurementByAgencyFromFarmer set QtyProcured ='" + txtQuan.Text + "',Remark='" + txt_Remark.Text + "',  Amt_Payable_to_farmer='" + txtAmount.Text + "',Amt_Paid ='" + txt_paidAmount.Text + "',Mode_Of_Payment ='" + PayMod + "',Cheque_No='" + txt_chno.Text + "',Cheque_date='" + ChDate + "',Status='" + sta + "' where ProcAgentFarmerID='" + GridView1.SelectedRow.Cells[12].Text.ToString() + "' and PCID='" + Session["pcId"].ToString() + "'";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd = new SqlCommand(strqsl, con);
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Updeted successfully.............'); </script> ");

                    btn_update.Visible = false;
                    btn_AddNew.Enabled = true;
                    btn_AddNew.Visible = true;
                    btn_AddNew.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {

                        con.Close();

                    }
                    fillgrid();
                }

            }
            catch (Exception ex)
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }
        }

        else
        {

            Response.Redirect("../sessionexpired.aspx");


        }
    }
}
