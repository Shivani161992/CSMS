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
using System.Data.SqlClient;

public partial class IssueCenter_Print_AcceptanceNo_New : System.Web.UI.Page
{
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());


    public string distname = "";
    public string dist = "";
    public string sid = "";
    public string ssid = "";
    public string dname = "";
    public string snid = "";
    public string distid = "";
    public string dipotid = "";
    public string cdate = "";
    public string issueid = "";

    public string commodity = "";


    int senbags = 0;
    decimal senqty = 0;

    int accbags = 0;

    decimal accqty = 0;

    int stichbags = 0;

    int stencilbags = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (Session["Acceptance_NO"] != null)
            {
                sid = Session["Acceptance_NO"].ToString();
                issueid = Session["issue_id"].ToString();
                dist = Session["dist_id"].ToString();

                commodity = Session["Commodity_ID"].ToString();

                if (!IsPostBack)
                {
                    string qry = "SELECT  *  FROM [pds].[districtsmp] where (paddy_markfed = 'Y' or Wheat_Markfed = 'Y') and district_code = '" + dist + "'";
                SqlCommand cmd = new SqlCommand(qry,con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblmarkfed.Visible = true;

                    lblscsc.Visible = false;
                }

                else
                {
                    lblscsc.Visible = true;

                    lblmarkfed.Visible = false;
                }

                lblmarkfed.Visible = false;

                lblscsc.Visible = true;

                dr.Close();
                    

                ssid = sid.Substring(0, 7);
                GetData();

                }
            }
            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }

        
    }

    void GetData()
    {
        string qry = "";

        if (commodity == "1")
        {
            qry = "SELECT convert(nvarchar,Acceptance_Note_Detail2016.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement2016.Recd_Date,103)Recd_Date , SCSC_Procurement2016.Receipt_Id,SCSC_Procurement2016.TC_Number ,SCSC_Procurement2016.Truck_Number ,SCSC_Procurement2016.No_of_Bags as sendbags , SCSC_Procurement2016.Quantity as sendqty ,SCSC_Procurement2016.TaulParchi,SCSC_Procurement2016.Moisture , SCSC_Procurement2016.category ,CONVERT(varchar,SCSC_Procurement2016.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Detail2016.Bags as Acc_Bag,Acceptance_Note_Detail2016.Accept_Qty,Acceptance_Note_Detail2016.Acceptance_No, SCSC_Procurement2016.Stencile_bags,SCSC_Procurement2016.Stiching_bags ,Acceptance_Note_Detail2016.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement2016 left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement2016.Commodity_Id  left join Acceptance_Note_Detail2016 on Acceptance_Note_Detail2016.TC_Number =  SCSC_Procurement2016.TC_Number and Acceptance_Note_Detail2016.IssueID = SCSC_Procurement2016.Receipt_Id and Acceptance_Note_Detail2016.godown = SCSC_Procurement2016.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement2016.Distt_ID left join Society on Society.Society_Id= SCSC_Procurement2016.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement2016.IssueCenter_ID where SCSC_Procurement2016.Distt_ID='" + dist + "' and SCSC_Procurement2016.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Detail2016.Acceptance_No='" + sid + "' and  Acceptance_Note_Detail2016.godown= '" + Session["Godown"].ToString() + "'";

        }

        else if (commodity == "2" || commodity == "3")
        {
             qry = "SELECT convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement.Recd_Date,103)Recd_Date , SCSC_Procurement.Receipt_Id,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number ,SCSC_Procurement.No_of_Bags as sendbags , SCSC_Procurement.Quantity as sendqty ,SCSC_Procurement.TaulParchi,SCSC_Procurement.Moisture , SCSC_Procurement.category ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Acceptance_No, SCSC_Procurement.Stencile_bags,SCSC_Procurement.Stiching_bags ,Acceptance_Note_Detail.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement.Commodity_Id  left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number =  SCSC_Procurement.TC_Number and Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id and Acceptance_Note_Detail.godown = SCSC_Procurement.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID left join Society on Society.Society_Id= SCSC_Procurement.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID where SCSC_Procurement.Distt_ID='" + dist + "' and SCSC_Procurement.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Detail.Acceptance_No='" + sid + "' and  Acceptance_Note_Detail.godown= '" + Session["Godown"].ToString() + "'";

        }

        else if (commodity == "4")
        {
             qry = "SELECT convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement.Recd_Date,103)Recd_Date , SCSC_Procurement.Receipt_Id,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number ,SCSC_Procurement.No_of_Bags as sendbags , SCSC_Procurement.Quantity as sendqty ,SCSC_Procurement.TaulParchi,SCSC_Procurement.Moisture , SCSC_Procurement.category ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Acceptance_No, SCSC_Procurement.Stencile_bags,SCSC_Procurement.Stiching_bags ,Acceptance_Note_Detail.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement.Commodity_Id  left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number =  SCSC_Procurement.TC_Number and Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id and Acceptance_Note_Detail.godown = SCSC_Procurement.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID left join Society on Society.Society_Id= SCSC_Procurement.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID where SCSC_Procurement.Distt_ID='" + dist + "' and SCSC_Procurement.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Detail.Acceptance_No='" + sid + "' and  Acceptance_Note_Detail.godown= '" + Session["Godown"].ToString() + "'";

        }

        else if (commodity == "5" )
        {
             qry = "SELECT convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement.Recd_Date,103)Recd_Date , SCSC_Procurement.Receipt_Id,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number ,SCSC_Procurement.No_of_Bags as sendbags , SCSC_Procurement.Quantity as sendqty ,SCSC_Procurement.TaulParchi,SCSC_Procurement.Moisture , SCSC_Procurement.category ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Acceptance_No, SCSC_Procurement.Stencile_bags,SCSC_Procurement.Stiching_bags ,Acceptance_Note_Detail.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement.Commodity_Id  left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number =  SCSC_Procurement.TC_Number and Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id and Acceptance_Note_Detail.godown = SCSC_Procurement.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID left join Society on Society.Society_Id= SCSC_Procurement.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID where SCSC_Procurement.Distt_ID='" + dist + "' and SCSC_Procurement.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Detail.Acceptance_No='" + sid + "' and  Acceptance_Note_Detail.godown= '" + Session["Godown"].ToString() + "'";


        }

        else if (commodity == "6")
        {
             qry = "SELECT convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement.Recd_Date,103)Recd_Date , SCSC_Procurement.Receipt_Id,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number ,SCSC_Procurement.No_of_Bags as sendbags , SCSC_Procurement.Quantity as sendqty ,SCSC_Procurement.TaulParchi,SCSC_Procurement.Moisture , SCSC_Procurement.category ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Acceptance_No, SCSC_Procurement.Stencile_bags,SCSC_Procurement.Stiching_bags ,Acceptance_Note_Detail.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement.Commodity_Id  left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number =  SCSC_Procurement.TC_Number and Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id and Acceptance_Note_Detail.godown = SCSC_Procurement.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID left join Society on Society.Society_Id= SCSC_Procurement.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID where SCSC_Procurement.Distt_ID='" + dist + "' and SCSC_Procurement.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Detail.Acceptance_No='" + sid + "' and  Acceptance_Note_Detail.godown= '" + Session["Godown"].ToString() + "'";

        }

        else if (commodity == "7")
        {
             qry = "SELECT convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement.Recd_Date,103)Recd_Date , SCSC_Procurement.Receipt_Id,SCSC_Procurement.TC_Number ,SCSC_Procurement.Truck_Number ,SCSC_Procurement.No_of_Bags as sendbags , SCSC_Procurement.Quantity as sendqty ,SCSC_Procurement.TaulParchi,SCSC_Procurement.Moisture , SCSC_Procurement.category ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Acceptance_No, SCSC_Procurement.Stencile_bags,SCSC_Procurement.Stiching_bags ,Acceptance_Note_Detail.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement.Commodity_Id  left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number =  SCSC_Procurement.TC_Number and Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id and Acceptance_Note_Detail.godown = SCSC_Procurement.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID left join Society on Society.Society_Id= SCSC_Procurement.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID where SCSC_Procurement.Distt_ID='" + dist + "' and SCSC_Procurement.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Detail.Acceptance_No='" + sid + "' and  Acceptance_Note_Detail.godown= '" + Session["Godown"].ToString() + "'";

        }

        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        cmd.CommandTimeout = 0;

        SqlDataAdapter daP = new SqlDataAdapter(qry, con);
        DataSet dsP = new DataSet();
        daP.Fill(dsP);

        if (dsP.Tables[0].Rows.Count > 0)
        {
            grd_viewDepot.DataSource = dsP.Tables[0];
            grd_viewDepot.DataBind();


            lbldist.Text = dsP.Tables[0].Rows[0]["district_name"].ToString();
            lblpurchaseCent.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();

            lblrecddate.Text = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();

            lblcomm.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();


            lblissCen.Text = dsP.Tables[0].Rows[0]["DepotName"].ToString();

            lblacceptnum.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString();

            lblacceptdate.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();


            lblacceptdate.Text = dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString();
        }

        else
        {
            Response.Write("Data Not Found");
        }

        string gid = Session["Godown"].ToString();
        string gdnname = "SELECT  Godown_ID  ,Godown_Name FROM tbl_MetaData_GODOWN where Godown_ID = '" + gid + "'";

        SqlDataAdapter dagid = new SqlDataAdapter(gdnname, con);

        if (cons.State == ConnectionState.Closed)
        {
            cons.Open();
        }


        DataSet dsgid = new DataSet();
        dagid.Fill(dsgid);

        if (dsgid.Tables[0].Rows.Count > 0)
        {

            lblgodown.Text = dsgid.Tables[0].Rows[0]["Godown_Name"].ToString();
        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        if (cons.State == ConnectionState.Open)
        {
            cons.Close();
        }

    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            senbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "sendbags"));

            senqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "sendqty"));

            accbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Acc_Bag"));

            accqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Accept_Qty"));

            stichbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stiching_bags"));

            stencilbags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stencile_bags"));
        }
       
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount1 = (Label)e.Row.FindControl("lbl_totsenbag");

            lblAmount1.Text = senbags.ToString();

            Label lblqty = (Label)e.Row.FindControl("lbl_totsenqty");

            lblqty.Text = senqty.ToString();


            Label lbljutnewbag = (Label)e.Row.FindControl("lbl_totrecbag");

            lbljutnewbag.Text = accbags.ToString();

            Label lblppbags = (Label)e.Row.FindControl("lbl_totrecqty");

            lblppbags.Text = accqty.ToString();


            Label lbljutold = (Label)e.Row.FindControl("lbl_totstichbag");

            lbljutold.Text = stichbags.ToString();


            Label stencil = (Label)e.Row.FindControl("lbl_totstencilebag");

            stencil.Text = stencilbags.ToString();

        }
    }

   
}
