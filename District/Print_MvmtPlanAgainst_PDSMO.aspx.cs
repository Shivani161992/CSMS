using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data; 

public partial class District_Print_MvmtPlanAgainst_PDSMO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0;
    string QRGridDetails, districtid = "",BranchName="",GodownName="";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                hdfMovmtOrderNo.Value = lblMovmtNo.Text = Session["MovmtOrderNo"].ToString();
                hdfSubMocementOrderNo.Value = Session["SubMovmtOrderNo"].ToString();

                districtid = Session["dist_id"].ToString();
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
                string select = string.Format("SELECT SMO.GunnyType,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=Rec.Commodity) ComdtyName,Rec.Commodity,Rec.ReachDate,(select Source_Name from  Source_Arrival_Type where Source_ID=Rec.ModeofDispatch) DispatchModeName,Rec.ModeofDispatch,(SELECT district_name FROM pds.districtsmp where district_code=Rec.FrmDist) FromDistName,Rec.FrmDist,(SELECT district_name FROM pds.districtsmp where district_code=Rec.ToDist) ToDistName,Rec.ToDist,Rec.CropYear,GETDATE() CurrentDate,Rec.CreatedDate,MODate,(Case When Rec.Commodity='25' Then Rec.Quantity Else (Rec.Quantity/10) End) As Quantity FROM RecAgainst_StateMovementOrder As Rec Left Join StateMovementOrder As SMO ON(SMO.MoveOrdernum=Rec.MoveOrdernum) where Rec.MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and Rec.SMO='" + hdfSubMocementOrderNo.Value + "' and Rec.ToDist='" + districtid + "'");

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        lblDate.Text = CreatedDate.ToString("dd/MMM/yyyy");

                        DateTime MODate = DateTime.Parse(ds.Tables[0].Rows[0]["MODate"].ToString());
                        lblMODate.Text = MODate.ToString("dd/MMM/yyyy");

                        DateTime EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReachDate"].ToString());
                        lblEndDate.Text = EndDate.ToString("dd/MMM/yyyy");

                        lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDate"].ToString();

                        lblComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();

                        if (lblComdty.Text == "Gunny")
                        {
                            lblPDS.Text = "GUNNY";
                            lblMT.Text = "(Bales)";

                            string strGunnyType = ds.Tables[0].Rows[0]["GunnyType"].ToString();
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
                            lblMT.Text = "(MT)";
                        }
                        lblTransMode.Text = ds.Tables[0].Rows[0]["DispatchModeName"].ToString();

                        lblFrmDist.Text = ds.Tables[0].Rows[0]["FromDistName"].ToString();
                        lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                        double TotalQty = double.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                        lblTotalQty.Text = TotalQty.ToString("0.00");
                        lblDistName.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();

                        QRGridDetails = "Movement Order No=" + lblMovmtNo.Text + ",SMO=" + hdfSubMocementOrderNo.Value + " ,Commodity=" + lblComdty.Text + ", Total Qty(Qtls)=" + lblTotalQty.Text + ", Sending Dist=" + lblFrmDist.Text + ", Dist Office=" + lblDistName.Text + ",'Issue Center,Branch,Godown,Qty'";
                    }

                    //select = string.Format("Select RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID=Issue_Center) ICName,Issue_Center,BranchName,GodownName From RecAgainst_StateMovementOrder where MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and ToDist='" + districtid + "' order by RMO");

                    if (lblComdty.Text == "Gunny")
                    {
                        select = string.Format("Select (RequiredQuantity)RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID=Rec.Issue_Center) ICName,Issue_Center,Branch,Godown From RecAgainst_StateMovementOrder As Rec where MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and ToDist='" + districtid + "' order by RMO");
                    }
                    else
                    {
                        select = string.Format("Select (RequiredQuantity/10)RequiredQuantity,(select DepotName from tbl_MetaData_DEPOT where DepotID=Rec.Issue_Center) ICName,Issue_Center,Branch,Godown From RecAgainst_StateMovementOrder As Rec where MoveOrdernum='" + hdfMovmtOrderNo.Value + "' and SMO='" + hdfSubMocementOrderNo.Value + "' and ToDist='" + districtid + "' order by RMO");
                    }

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        string qrdata = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            qrdata += ((qrdata == "") ? "" : ",") + "{" + ds.Tables[0].Rows[i]["ICName"] + "," + ds.Tables[0].Rows[i]["Branch"] + "," + ds.Tables[0].Rows[i]["Godown"] + "," + ds.Tables[0].Rows[i]["RequiredQuantity"] + "}";
                        }
                        QRGridDetails += qrdata + " MT";
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
                e.Row.Cells[4].Text = "Qty To Be Deposited (Bales)";
            }
            else
            {
                e.Row.Cells[4].Text = "Qty To Be Deposited (MT)";
            }
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
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "कुल मात्रा";
            e.Row.Cells[4].Text = QtyTotal.ToString("0.00");
        }
    }


}