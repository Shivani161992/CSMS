using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_Print_TOAgainst_PDSMovmtOrder : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0;
    string QRGridDetails, districtid = "", BranchName = "", GodownName = "",strMixIC = "";
    int ro = 0, RowSpan = 0;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                hdfMovmtOrderNo.Value = lblMovmtNo.Text = lblMovmtNo1.Text = Session["MovmtOrderNo"].ToString();
                hdfSubMocementOrderNo.Value = Session["SubMovmtOrderNo"].ToString();
                hdfToDistCode.Value = Session["ToDostCode"].ToString();
                hdfTransportNumber.Value = lblToOrderNO.Text = lblToOrderNO1.Text = lblToOrderNO2.Text = Session["TransportNumber"].ToString();

                ViewState["PreviousRow"] = "0000";
                districtid = Session["dist_id"].ToString();

                if (Session["MovmtOrderNo"] != "" && Session["SubMovmtOrderNo"] != "")
                {
                    GetDataTO();
                }
                else
                {
                    GetDataTO1();
                }

                GetData();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetDataTO()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select (select district_name from pds.districtsmp where district_code=TOMO.FrmDist) FrmDistName,FrmDist,CropYear,CreatedDate,(select Top 1 (Transporter_Name) + ' { '+MobileNo+' }'  from Transporter_Table where Transporter_ID = TOMO.Transporter_ID and Distt_ID='" + districtid + "' and Transport_ID In ('6','11','10') order by Transporter_Name desc) TransporterName, Transporter_ID, (Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=TOMO.Commodity) ComdtyName,Commodity,MODate,TOEndDate,(Quantity/10)Quantity,GETDATE() CurrentDateTime,(select DepotName from tbl_MetaData_DEPOT where DepotID=TOMO.Issue_Center) ICName,Issue_Center,Branch,Godown,(RequiredQuantity/10)RequiredQuantity From TO_AgainstHO_MO TOMO where MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and TO_No='" + hdfTransportNumber.Value + "' and FrmDist='" + districtid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDistName.Text = lblDistName1.Text  = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    lblCropYear.Text = lblCropYear1.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    lblDate.Text = lblDate1.Text = lblDate2.Text = CreatedDate.ToString("dd/MMM/yyyy");

                    lblTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();
                    lblComdty.Text = lblComdty1.Text = lblComdty2.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();

                    DateTime MODate =  DateTime.Parse(ds.Tables[0].Rows[0]["MODate"].ToString());
                    lblMODate.Text = lblMODate1.Text = MODate.ToString("dd/MMM/yyyy");

                    DateTime TOEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["TOEndDate"].ToString());
                    lblTOEndDate.Text = TOEndDate.ToString("dd/MMM/yyyy");

                    double TotalQty = double.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                    lblTotalQty.Text = TotalQty.ToString("0.00");

                    lblCurrentDateTime.Text = lblCurrentDateTime1.Text = ds.Tables[0].Rows[0]["CurrentDateTime"].ToString();

                    QRGridDetails = "MO_No=" + lblMovmtNo.Text + ", SMO_No=" + hdfSubMocementOrderNo.Value + ", TO_No=" + lblToOrderNO.Text + ", TO_Date=" + lblDate.Text + ", TO_EndDate=" + lblTOEndDate.Text + ", Transporter_Name=" + lblTransporterName.Text + ", Sending_Dist=" + lblDistName.Text + ", Commodity=" + lblComdty.Text + ", Quantity(MT)=" + lblTotalQty.Text +", Receiving_Dist=";

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = "";
                    GridView1.DataBind();
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

    public void GetDataTO1()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select MoveOrdernum,SMO, (select district_name from pds.districtsmp where district_code=TOMO.FrmDist) FrmDistName,FrmDist,CropYear,CreatedDate,(select Top 1 (Transporter_Name) + ' { '+MobileNo+' }'  from Transporter_Table where Transporter_ID = TOMO.Transporter_ID and Distt_ID='" + districtid + "' and Transport_ID In ('6','11','10') order by Transporter_Name desc) TransporterName, Transporter_ID, (Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=TOMO.Commodity) ComdtyName,Commodity,MODate,TOEndDate,(Quantity/10)Quantity,GETDATE() CurrentDateTime,(select DepotName from tbl_MetaData_DEPOT where DepotID=TOMO.Issue_Center) ICName,Issue_Center,Branch,Godown,(RequiredQuantity/10)RequiredQuantity From TO_AgainstHO_MO TOMO where TO_No='" + hdfTransportNumber.Value + "' and FrmDist='" + districtid + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDistName.Text = lblDistName1.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    lblCropYear.Text = lblCropYear1.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    lblDate.Text = lblDate1.Text = lblDate2.Text = CreatedDate.ToString("dd/MMM/yyyy");

                    lblTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();
                    lblComdty.Text = lblComdty1.Text = lblComdty2.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();

                    hdfMovmtOrderNo.Value = lblMovmtNo.Text = lblMovmtNo1.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();
                    hdfSubMocementOrderNo.Value = ds.Tables[0].Rows[0]["SMO"].ToString();

                    DateTime MODate = DateTime.Parse(ds.Tables[0].Rows[0]["MODate"].ToString());
                    lblMODate.Text = lblMODate1.Text = MODate.ToString("dd/MMM/yyyy");

                    DateTime TOEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["TOEndDate"].ToString());
                    lblTOEndDate.Text = TOEndDate.ToString("dd/MMM/yyyy");

                    double TotalQty = double.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                    lblTotalQty.Text = TotalQty.ToString("0.00");

                    lblCurrentDateTime.Text = lblCurrentDateTime1.Text = ds.Tables[0].Rows[0]["CurrentDateTime"].ToString();

                    QRGridDetails = "MO_No=" + lblMovmtNo.Text + ", SMO_No=" + hdfSubMocementOrderNo.Value + ", TO_No=" + lblToOrderNO.Text + ", TO_Date=" + lblDate.Text + ", TO_EndDate=" + lblTOEndDate.Text + ", Transporter_Name=" + lblTransporterName.Text + ", Sending_Dist=" + lblDistName.Text + ", Commodity=" + lblComdty.Text + ", Quantity(MT)=" + lblTotalQty.Text + ", Receiving_Dist=";

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = "";
                    GridView1.DataBind();
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

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BranchName = GodownName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            BranchName = e.Row.Cells[2].Text;
            GodownName = e.Row.Cells[3].Text;

            
            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + BranchName + "') BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') Godown_Name");
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string a = e.Row.Cells[1].Text;

                            if (a == ViewState["PreviousRow"].ToString())
                            {

                            }
                            else
                            {
                                strMixIC += ((strMixIC == "") ? "" : ", ") + e.Row.Cells[1].Text;
                            }

                            ViewState["PreviousRow"] = e.Row.Cells[1].Text;

                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                            e.Row.Cells[3].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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
                    if (con_MPStorage.State != ConnectionState.Closed)
                    {
                        con_MPStorage.Close();
                    }
                }
            }

            QtyTotal += (double.Parse(e.Row.Cells[4].Text));

            double QtyRow = (double.Parse(e.Row.Cells[4].Text));
            e.Row.Cells[4].Text = QtyRow.ToString("0.00");
            lblMixIC.Text = strMixIC;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total Qty = ";
            e.Row.Cells[4].Text = QtyTotal.ToString("0.00");
        }
    }

    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("Select (RequiredQuantity/10)RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID= Rec.Issue_Center) ICName,Issue_Center,Branch,Godown,(select district_name from pds.districtsmp where district_code='" + hdfToDistCode.Value + "') ToDistName From RecAgainst_StateMovementOrder As Rec where MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and ToDist='" + hdfToDistCode.Value + "' order by RMO");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblDistName2.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();

                    QRGridDetails += GridView2.Rows[0].Cells[1].Text;
                    ImgQRCode.ImageUrl = ImgQRCode1.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;

                }
                else
                {
                    GridView2.DataSource = "";
                    GridView2.DataBind();
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BranchName = GodownName = "";
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            BranchName = e.Row.Cells[3].Text;
            GodownName = e.Row.Cells[4].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + BranchName + "') BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') Godown_Name");
                    da = new SqlDataAdapter(select, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            e.Row.Cells[3].Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                            e.Row.Cells[4].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
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
                    if (con_MPStorage.State != ConnectionState.Closed)
                    {
                        con_MPStorage.Close();
                    }
                }
            }

            QtyTotal += (double.Parse(e.Row.Cells[5].Text));
            double QtyRow = (double.Parse(e.Row.Cells[5].Text));
            e.Row.Cells[5].Text = QtyRow.ToString("0.00");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total Qty = ";
            e.Row.Cells[5].Text = QtyTotal.ToString("0.00");

            RowSpan = 1;
            ro = 0;
            int j = 0;
            for (int i = 0; i < GridView2.Rows.Count - 1; i++)
            {
                GridViewRow currrow = GridView2.Rows[j];
                GridViewRow nextrow = GridView2.Rows[i + 1];
                if (currrow.Cells[1].Text == nextrow.Cells[1].Text)
                {
                    nextrow.Cells[1].Visible = false;
                    RowSpan += 1;
                    ro++;
                }
                else
                {
                    currrow.Cells[1].RowSpan = RowSpan;
                    RowSpan = 1;
                    j = i + 1;
                }
            }

            GridViewRow currrow1 = GridView2.Rows[j];
            currrow1.Cells[1].RowSpan = RowSpan;

            
        }
        
    }


}