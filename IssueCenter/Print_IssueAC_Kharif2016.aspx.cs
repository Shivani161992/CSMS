using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenter_Print_IssueAC_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string sid = "", issueid = "", dist = "", QRGridDetails = "";
    int senbags = 0, accbags = 0, stichbags = 0, stencilbags = 0;
    decimal senqty = 0, accqty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["Markfed"].ToString() == "Y")
                {
                    Image1.ImageUrl = "~/Images/markfedPDY.jpg";
                    lblMFD.Text = "मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड";
                }
                else
                {
                    Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
                    lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
                }

                sid = Session["Acceptance_NO"].ToString();
                issueid = Session["issue_id"].ToString();
                dist = Session["dist_id"].ToString();

                GetData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "";
                select = " SELECT GETDATE() As CurrentDatetime,Acceptance_Note_Kharif2016.CropYear,convert(nvarchar,Acceptance_Note_Kharif2016.Acceptance_Date,103)Acceptance_Date , convert(nvarchar,SCSC_Procurement_Kharif2016.Recd_Date,103)Recd_Date , SCSC_Procurement_Kharif2016.Receipt_Id,SCSC_Procurement_Kharif2016.TC_Number ,SCSC_Procurement_Kharif2016.Truck_Number ,SCSC_Procurement_Kharif2016.No_of_Bags as sendbags , SCSC_Procurement_Kharif2016.Quantity as sendqty ,SCSC_Procurement_Kharif2016.TaulParchi,SCSC_Procurement_Kharif2016.Moisture , SCSC_Procurement_Kharif2016.category ,CONVERT(varchar,SCSC_Procurement_Kharif2016.Dispatch_Date,103)as DateOfIssue1,Acceptance_Note_Kharif2016.Bags as Acc_Bag,Acceptance_Note_Kharif2016.Accept_Qty,Acceptance_Note_Kharif2016.Acceptance_No, SCSC_Procurement_Kharif2016.Stencile_bags,SCSC_Procurement_Kharif2016.Stiching_bags ,Acceptance_Note_Kharif2016.Acceptance_Date,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,pds.districtsmp.district_name,(Society.Society_Name+','+Society.SocPlace)as Society_Name ,tbl_MetaData_DEPOT.DepotName FROM SCSC_Procurement_Kharif2016 left join tbl_MetaData_STORAGE_COMMODITY  on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Procurement_Kharif2016.Commodity_Id  left join Acceptance_Note_Kharif2016 on Acceptance_Note_Kharif2016.TC_Number =  SCSC_Procurement_Kharif2016.TC_Number and Acceptance_Note_Kharif2016.IssueID = SCSC_Procurement_Kharif2016.Receipt_Id and Acceptance_Note_Kharif2016.godown = SCSC_Procurement_Kharif2016.Recd_Godown left join pds.districtsmp  on pds.districtsmp.district_code = SCSC_Procurement_Kharif2016.Distt_ID left join SocietyKharif2016 As Society on Society.Society_Id= SCSC_Procurement_Kharif2016.Purchase_Center inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement_Kharif2016.IssueCenter_ID where SCSC_Procurement_Kharif2016.Distt_ID='" + dist + "' and SCSC_Procurement_Kharif2016.IssueCenter_ID='" + issueid + "'  and Acceptance_Note_Kharif2016.Acceptance_No='" + sid + "' and  Acceptance_Note_Kharif2016.godown= '" + Session["Godown"].ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    lblpurchaseCent.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    lblrecddate.Text = ds.Tables[0].Rows[0]["Recd_Date"].ToString();
                    lblcomm.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lblissCen.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblacceptnum.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    lblacceptdate.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    lblacceptdate.Text = ds.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDatetime"].ToString();
                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblgodown.Text = Session["Godown"].ToString();

                    grd_viewDepot.DataSource = ds.Tables[0];
                    grd_viewDepot.DataBind();

                    GetGodownName();

                    QRGridDetails = "District=" + lbldist.Text + ", Acceptance No.=" + lblacceptnum.Text + ", Acceptance Date='" + lblacceptdate.Text + "', CropYear=" + lblCropYear.Text + ", Recd. Godown='" + lblgodown.Text + "', Commodity='" + lblcomm.Text + "'";
                    ImgQRCode.ImageUrl = ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;
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

    public void GetGodownName()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblgodown.Text + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblgodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    }
                }
                else
                {

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