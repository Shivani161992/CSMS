using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_Print_DistTransportOrder : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0, ConvertQtlsToMT = 0;
    string QRGridDetails;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    int ro = 0;
    int RowSpan = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
               
                ViewState["DistTransportOrdernum"] = lblMovmtNo.Text = Session["DistTransportOrdernum"].ToString();
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
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select (SELECT district_name FROM pds.districtsmp where district_code=district) as District,(SELECT DepotName FROM tbl_MetaData_DEPOT where DepotID=FrmIC) as FrmIC, (Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=Commodity) ComdtyName, (select Source_Name from  Source_Arrival_Type where Source_ID=ModeofDispatch) DispatchModeName, ReachDate, CropYear, Types_of_Gunny as GunnyType, CreatedDate  from PDS_Dist_TransportOrder_Intra_IC where DistTransportOrdernum='" + ViewState["DistTransportOrdernum"].ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        lblDate.Text = CreatedDate.ToString("dd/MMM/yyyy");

                        DateTime frmDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReachDate"].ToString());
                        ViewState["hdfEndDate"] = frmDate.ToString("dd/MMM/yyyy");

                       // lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDate"].ToString();
                        lbldist.Text = ds.Tables[0].Rows[0]["District"].ToString();

                        lblComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                        string strGunnyType = ds.Tables[0].Rows[0]["GunnyType"].ToString();

                        if (lblComdty.Text == "Gunny")
                        {
                            lblGunnyTypes.Visible = true;
                            lblPDS.Text = "GUNNY";
                            lblSig.Text = "उपार्जन";
                            if (strGunnyType == "JUTE")
                            {
                                lblGunnyTypes.Text = "Jute(SBT)....";
                            }
                            else
                            {
                                lblGunnyTypes.Text = "PP....";
                            }
                        }
                        else
                         {
                            lblPDS.Text = "PDS";
                            //lblSig.Text = "परिवहन";
                            lblSig.Text = "सा०वि०प्र०";
                        }

                        lblTransMode.Text = ds.Tables[0].Rows[0]["DispatchModeName"].ToString();

                        //hdfMovmtIdentID.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                        ViewState["hdfFromDist"] = ds.Tables[0].Rows[0]["FrmIC"].ToString();
                        ViewState["hdfCropYear"] = ds.Tables[0].Rows[0]["CropYear"].ToString();

                        QRGridDetails = "Transport Order No=" + ViewState["DistTransportOrdernum"].ToString() + " , Transport Order Date=" + lblDate.Text + " , Transport Order End Date=" + ViewState["hdfEndDate"].ToString() + ", Commodity=" + lblComdty.Text + ", Mode of Dispatch=" + lblTransMode.Text + ", Sending Issue Center=" + ViewState["hdfFromDist"].ToString() + " , Receiving Dist ";

                    }

                    select = string.Format("Select Quantity,(SELECT DepotName FROM tbl_MetaData_DEPOT where DepotID=ToIC) ReceiveICName,ToIC From PDS_Dist_TransportOrder_Intra_IC where DistTransportOrdernum='" + ViewState["DistTransportOrdernum"].ToString() + "'");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        string qrdata = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            qrdata += ((qrdata == "") ? "" : " , ") + "{" + ds.Tables[0].Rows[i]["ReceiveICName"] + "=";

                            ConvertQtlsToMT = 0;
                            if (lblComdty.Text == "Gunny")
                            {
                                ConvertQtlsToMT = ((double.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString())));
                            }
                            else
                            {
                                ConvertQtlsToMT = ((double.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString())) / 10);
                            }
                            qrdata += ConvertQtlsToMT + "}";
                        }

                        if (lblComdty.Text == "Gunny")
                        {
                            QRGridDetails += qrdata + " Bales";
                        }
                        else
                        {
                            QRGridDetails += qrdata + " MT";
                        }

                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = "";
                        GridView1.DataBind();
                    }
                    ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;

            if (lblComdty.Text == "Gunny")
            {
                e.Row.Cells[5].Text = "परिवहन की मात्रा (Bales)";
            }
            else
            {
                e.Row.Cells[5].Text = "परिवहन की मात्रा (मै० टन)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            e.Row.Cells[4].Text = ViewState["hdfCropYear"].ToString();

            e.Row.Cells[1].Text = ViewState["hdfFromDist"].ToString();
            e.Row.Cells[3].Text = ViewState["hdfEndDate"].ToString();

            ConvertQtlsToMT = 0;

            if (lblComdty.Text == "Gunny")
            {
                ConvertQtlsToMT = (((double.Parse(e.Row.Cells[5].Text))));
            }
            else
            {
                ConvertQtlsToMT = (((double.Parse(e.Row.Cells[5].Text)) / 10));
            }

            e.Row.Cells[5].Text = ConvertQtlsToMT.ToString("0.00");
            QtyTotal += ConvertQtlsToMT;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "कुल मात्रा";
            e.Row.Cells[5].Text = QtyTotal.ToString("0.00");

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            RowSpan = 1;
            ro = 0;
            int j = 0;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                GridViewRow currrow = GridView1.Rows[j];
                GridViewRow nextrow = GridView1.Rows[i + 1];
                if (currrow.Cells[1].Text == nextrow.Cells[1].Text && currrow.Cells[4].Text == nextrow.Cells[4].Text && currrow.Cells[3].Text == nextrow.Cells[3].Text)
                {
                    nextrow.Cells[1].Visible = false;
                    nextrow.Cells[4].Visible = false;
                    nextrow.Cells[3].Visible = false;
                    RowSpan += 1;
                    ro++;
                }
                else
                {
                    currrow.Cells[1].RowSpan = RowSpan;
                    currrow.Cells[4].RowSpan = RowSpan;
                    currrow.Cells[3].RowSpan = RowSpan;
                    RowSpan = 1;
                    j = i + 1;
                }
            }

            GridViewRow currrow1 = GridView1.Rows[j];
            currrow1.Cells[1].RowSpan = RowSpan;
            currrow1.Cells[4].RowSpan = RowSpan;
            currrow1.Cells[3].RowSpan = RowSpan;
        }
    }


}