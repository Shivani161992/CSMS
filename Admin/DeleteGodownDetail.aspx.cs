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

public partial class State_DeleteGodownDetail : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string adminid = "";
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());


           

            if (!IsPostBack)
            {

                GetDist();

            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }

    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddl_dist.DataSource = ds.Tables[0];
        ddl_dist.DataTextField = "district_name";
        ddl_dist.DataValueField = "district_Code";
        ddl_dist.DataBind();
        ddl_dist.Items.Insert(0, "--Select--");

    }

    void GetIssCName()
    {

        ddl_IC.Items.Clear();
        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddl_dist.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);
        if (ds == null)
        {
        }
        else
        {
            ddl_IC.DataSource = ds.Tables[0];
            ddl_IC.DataTextField = "DepotName";
            ddl_IC.DataValueField = "DepotId";
            ddl_IC.DataBind();
            ddl_IC.Items.Insert(0, "--Select--");
        }


    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIssCName();
    }

    protected void ddl_IC_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetGodown();
       
    }

   
    void GetGodown()
    {


        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + ddl_dist.SelectedValue + "' and DepotId='" + ddl_IC.SelectedValue + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlgodown.DataSource = ds.Tables[0];
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.Items.Clear();
            ddlgodown.DataValueField = null;
            ddlgodown.DataBind();

        }

    }

    void fillgridProc()
    {


        string did = ddl_dist.SelectedValue;
        string sid = ddl_IC.SelectedValue;
        string gid = ddlgodown.SelectedValue;

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT SCSC_Procurement.*, tbl_MetaData_GODOWN.Godown_Name as Godown_Name, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,tbl_MetaData_Purchase_Center.PurchaseCenterName FROM dbo.SCSC_Procurement  left join tbl_MetaData_GODOWN  on  SCSC_Procurement.Recd_Godown=tbl_MetaData_GODOWN.Godown_ID left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_Purchase_Center on SCSC_Procurement.Purchase_Center=tbl_MetaData_Purchase_Center.PcId where  Distt_ID='" + did + "' and IssueCenter_ID='" + sid + "'  and Recd_Godown='" + gid + "' order by SCSC_Procurement.Recd_date desc";
        DataSet ds = mobj.selectAny(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {


            dgrid_Proc.DataSource = ds.Tables[0];
            dgrid_Proc.DataBind();


       
        }
        else
        {



            dgrid_Proc.DataSource = null;
            dgrid_Proc.DataBind();

         
        }
    }

    private void fillgridOther()
    {

        string did = ddl_dist.SelectedValue;
        string depid = ddl_IC.SelectedValue;
        string gid = ddlgodown.SelectedValue;

        mobj = new MoveChallan(ComObj);
        string qry = "SELECT  tbl_Receipt_Details.Godown as Godown, tbl_Receipt_Details.challan_no,  tbl_Receipt_Details.challan_date,tbl_Receipt_Details.Vehile_no,tbl_Receipt_Details.Receipt_Id,tbl_Receipt_Details.S_of_arrival,Source_Arrival_Type.Source_Name as Source_Name   FROM dbo.tbl_Receipt_Details  left join dbo.Source_Arrival_Type on tbl_Receipt_Details.S_of_arrival= Source_Arrival_Type.Source_ID  where  Dist_Id='" + did + "' and Depot_ID='" + depid + "'  and Godown='" + gid + "' order by Order by  challan_date desc";
        DataSet ds = mobj.selectAny(qry);

        if (ds != null)
        {

            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView_other.DataSource = ds.Tables[0];
                GridView_other.DataBind();

            }

            else
            {



                GridView_other.DataSource = null;
                GridView_other.DataBind();

            }


        }
        else
        {

            GridView_other.DataSource = null;
            GridView_other.DataBind();
        }
    }

    protected void dgrid_Proc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgrid_Proc.PageIndex = e.NewPageIndex;
        fillgridProc();
    }
    protected void GridView_other_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_other.PageIndex = e.NewPageIndex;
        fillgridOther();

    }
    protected void dgrid_Proc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView_other_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillGrid_opening();
        fillgridProc();
        fillgridOther();
        fillGrid_iado();
        fillGrid_tch();


        if (ddlgodown.SelectedItem.Text != "--Select--")
        {
            Panel1.Visible = true;
            HyperLink1.Visible = true;
            btn_del.Visible = true;
           
            ddl_dist.Enabled = false;
            ddl_IC.Enabled = false;
            ddlgodown.Enabled = false;
        }
        else 
        {

            ddl_dist.Enabled = true;
            ddl_IC.Enabled = true;
            ddlgodown.Enabled = true;
            HyperLink1.Visible = false;
        
        }


    }

    void fillGrid_opening()
    {

        string distid = ddl_dist.SelectedValue;
        string issueid = ddl_IC.SelectedValue;
        string gdwnId=ddlgodown.SelectedValue;

        string qrychk = "select issue_opening_balance.District_Id as DistiD,issue_opening_balance.Depotid as depid,issue_opening_balance.Godown,issue_opening_balance.Source,issue_opening_balance.Scheme_Id,issue_opening_balance.Commodity_Id,convert(decimal(18,5),issue_opening_balance.Current_Balance) as Current_Balance ,issue_opening_balance.Current_Bags,convert(decimal(18,5),issue_opening_balance.Quantity) as Quantity ,issue_opening_balance.Bags,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name ,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name,Source_Arrival_Type.Source_Name as Source_Name ,Current_Godown_Position.Month, Current_Godown_Position.Year from dbo.issue_opening_balance left join dbo.tbl_MetaData_STORAGE_COMMODITY on issue_opening_balance.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on issue_opening_balance.Scheme_Id =tbl_MetaData_SCHEME.Scheme_Id left join dbo.Source_Arrival_Type on issue_opening_balance.Source=Source_Arrival_Type.Source_ID left join dbo.tbl_MetaData_GODOWN on issue_opening_balance.Godown=tbl_MetaData_GODOWN.Godown_ID  left join Current_Godown_Position on issue_opening_balance.Godown=Current_Godown_Position.Godown  where issue_opening_balance.District_Id='" + distid + "'and issue_opening_balance.Depotid='" + issueid + "'  and Godown='" + gdwnId + "'";
        mobj = new MoveChallan(ComObj);
        DataSet dschk = mobj.selectAny(qrychk);
        if (dschk == null)
        {

        }
        else
        {
            if (dschk.Tables[0].Rows.Count > 0)
            {
                GridView_opn.DataSource = dschk.Tables[0];
                GridView_opn.DataBind();


            }
            else
            {

                GridView_opn.DataSource = null;
                GridView_opn.DataBind();


            }

        }
    }

    void fillGrid_iado()
    {

        string did = ddl_dist.SelectedValue;
        string depid = ddl_IC.SelectedValue;
        string gid = ddlgodown.SelectedValue;

        string qrychk = "select * from  issue_against_do  where  district_code='" + did + "' and issueCentre_code='" + depid + "'  and Godown='" + gid + "' ";
        mobj = new MoveChallan(ComObj);
        DataSet dschk = mobj.selectAny(qrychk);
        if (dschk == null)
        {

        }
        else
        {
            if (dschk.Tables[0].Rows.Count > 0)
            {
                GridView_Iado.DataSource = dschk.Tables[0];
                GridView_Iado.DataBind();


            }
            else
            {

                GridView_Iado.DataSource = null;
                GridView_Iado.DataBind();


            }

        }
    }

    void fillGrid_tch()
    {

        string did = ddl_dist.SelectedValue;
        string depid = ddl_IC.SelectedValue;
        string gid = ddlgodown.SelectedValue;

        string qrychk = "select * from   SCSC_Truck_challan  where  Dist_ID='" + did + "' and Depot_Id='" + depid + "'  and Dispatch_Godown='" + gid + "' ";
        mobj = new MoveChallan(ComObj);
        DataSet dschk = mobj.selectAny(qrychk);
        if (dschk == null)
        {

        }
        else
        {
            if (dschk.Tables[0].Rows.Count > 0)
            {
                GridView_trck_chn.DataSource = dschk.Tables[0];
                GridView_trck_chn.DataBind();


            }
            else
            {

                GridView_trck_chn.DataSource = null;
                GridView_trck_chn.DataBind();


            }

        }
    }

    protected void btn_del_Click(object sender, EventArgs e)
    {
        SqlTransaction trans = null;
        string did = ddl_dist.SelectedValue;
        string depid = ddl_IC.SelectedValue;
        string gid = ddlgodown.SelectedValue;


        con.Open();
        trans = con.BeginTransaction();
        cmd.Transaction = trans;
        cmd.Connection = con;
        if (con != null)
        { //
            //string strop = "INSERT INTO issue_opening_balance_dellog select * from issue_opening_balance where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            string strop = "INSERT INTO  issue_opening_balance_dellog([State_Id],[District_Id],[Depotid] ,[Commodity_Id],[Scheme_Id] ,[Category_Id] ,[Godown]  ,[Crop_year] ,[Bags],[Quantity]  ,[Source] ,[Current_Balance] ,[Current_Bags] ,[Month]  ,[Year] ,[IP_Address] ,[Stock_Date]  ,[CreatedDate]  ,[UpdatedDate] ,[DeletedDate] ,[OperatorID] ) select State_Id,District_Id,Depotid ,Commodity_Id,Scheme_Id ,Category_Id ,Godown  ,Crop_year ,Bags,Quantity  ,Source ,Current_Balance ,Current_Bags ,Month  ,Year ,IP_Address ,Stock_Date  ,CreatedDate  ,UpdatedDate ,DeletedDate ,OperatorID from issue_opening_balance where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            string strdel = "delete from issue_opening_balance where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            cmd.CommandText = strop;
            cmd.ExecuteNonQuery();

            cmd.CommandText = strdel;
            cmd.ExecuteNonQuery();
            //

            string strcgp = "INSERT INTO Current_Godown_Position_dellog(District_Id,Depotid,Godown,Current_Balance,Current_Bags ,Month ,Year ,IP_Address,CreatedDate ,UpdatedDate ,DeletedDate,Godown_Capacity,Current_Capacity,State_Id) Select District_Id,Depotid,Godown,Current_Balance,Current_Bags ,Month ,Year ,IP_Address,CreatedDate ,UpdatedDate ,DeletedDate,Godown_Capacity,Current_Capacity,State_Id from Current_Godown_Position where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            string strcgpdel = "Delete from Current_Godown_Position where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            cmd.CommandText = strcgp;
            cmd.ExecuteNonQuery();

            cmd.CommandText = strcgpdel;
            cmd.ExecuteNonQuery();
            //


            string strlg = "INSERT INTO  Loss_gain_log  Select * from Loss_gain where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            string strlgdel = "delete from Loss_gain where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            cmd.CommandText = strlg;
            cmd.ExecuteNonQuery();

            cmd.CommandText = strlgdel;
            cmd.ExecuteNonQuery();
            //


            string strrr = "INSERT INTO RR_receipt_Depot_log Select * from RR_receipt_Depot where district_code='" + did + "'  and DepotID='" + depid + "' and  Godown='" + gid + "'";
            string strrrdel = "delete from RR_receipt_Depot where district_code='" + did + "'  and DepotID='" + depid + "' and  Godown='" + gid + "'";

            cmd.CommandText = strrr;
            cmd.ExecuteNonQuery();

            cmd.CommandText = strrrdel;
            cmd.ExecuteNonQuery();
            //


            string strscscp = "insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Distt_ID='" + did + "'  and IssueCenter_ID='" + depid + "' and  Recd_Godown='" + gid + "'";
            string strscscpdel = "delete from SCSC_Procurement where Distt_ID='" + did + "'  and IssueCenter_ID='" + depid + "' and  Recd_Godown='" + gid + "'";
            cmd.CommandText = strscscp;
            cmd.ExecuteNonQuery();


            cmd.CommandText = strscscpdel;
            cmd.ExecuteNonQuery();
            //

            string strrd = "insert into tbl_Receipt_Details_log select * from tbl_Receipt_Details where Dist_Id='" + did + "'  and Depot_ID='" + depid + "' and  Godown='" + gid + "'";
            string strrddel = "delete from tbl_Receipt_Details where Dist_Id='" + did + "'  and Depot_ID='" + depid + "' and  Godown='" + gid + "'";

            cmd.CommandText = strrd;
            cmd.ExecuteNonQuery();


            cmd.CommandText = strrddel;
            cmd.ExecuteNonQuery();


            //
            string striad = "insert into issue_against_do_log select * from issue_against_do where district_code='" + did + "'  and issueCentre_code='" + depid + "' and  Godown='" + gid + "'";
            string striaddel = "delete from issue_against_do where district_code='" + did + "'  and issueCentre_code='" + depid + "' and  Godown='" + gid + "'";
            cmd.CommandText = striad;
            cmd.ExecuteNonQuery();


            cmd.CommandText = striaddel;
            cmd.ExecuteNonQuery();

            //
            string strtc = "insert into SCSC_Truck_challan_dellog select * from SCSC_Truck_challan where Dist_ID='" + did + "'  and Depot_Id='" + depid + "' and  Dispatch_Godown='" + gid + "'";
            string strtcddel = "delete from SCSC_Truck_challan where Dist_ID='" + did + "'  and Depot_Id='" + depid + "' and  Dispatch_Godown='" + gid + "'";
            cmd.CommandText = strtc;
            cmd.ExecuteNonQuery();


            cmd.CommandText = strtcddel;
            cmd.ExecuteNonQuery();
            //
            string sttrn = "insert into State_Scheme_Transfer_dellog select * from State_Scheme_Transfer where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            string sttrnddel = "delete from State_Scheme_Transfer where District_Id='" + did + "'  and Depotid='" + depid + "' and  Godown='" + gid + "'";
            cmd.CommandText = sttrn;
            cmd.ExecuteNonQuery();


            cmd.CommandText = sttrnddel;
            cmd.ExecuteNonQuery();


            //7
            string strgodn = "insert into tbl_MetaData_GODOWN_Dellog select * from tbl_MetaData_GODOWN where DistrictId='" + did + "'  and DepotId='" + depid + "' and  Godown_ID='" + gid + "'";
            string strgodnddel = "delete from tbl_MetaData_GODOWN where DistrictId='" + did + "'  and DepotId='" + depid + "' and  Godown_ID='" + gid + "'";

            string ipd=Request.ServerVariables["REMOTE_ADDR"].ToString();
            string strupgd = "update tbl_MetaData_GODOWN_Dellog set DeletedBy='" + ipd + "' , DeletedDate=getDate()";
          
            cmd.CommandText = strgodn;
            cmd.ExecuteNonQuery();


            cmd.CommandText = strgodnddel;
            cmd.ExecuteNonQuery();

            cmd.CommandText = strupgd;
            cmd.ExecuteNonQuery();

            trans.Commit();
            con.Close();

      
          
            string datasave = " Godown Details are deleted Successfully......";

           Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + datasave + "'); </script> ");

            clearData();
        }

    }

    private void clearData()
    {

        btn_del.Enabled = false;
        Panel1.Visible = false;


       // cleargrid

        dgrid_Proc.DataSource = null;
        dgrid_Proc.DataBind();


        GridView_other.DataSource = null;
        GridView_other.DataBind();

        GridView_opn.DataSource = null;
        GridView_opn.DataBind();


        GridView_Iado.DataSource = null;
        GridView_Iado.DataBind();

        GridView_trck_chn.DataSource = null;
        GridView_trck_chn.DataBind();

    }
    protected void GridView_opn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_opn.PageIndex = e.NewPageIndex;
        fillGrid_opening();

    }
    protected void GridView_Iado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Iado.PageIndex = e.NewPageIndex;
        fillGrid_iado();
    }
    protected void GridView_trck_chn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_trck_chn.PageIndex = e.NewPageIndex;
        fillGrid_tch();
    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteGodown.aspx");

    }
}
