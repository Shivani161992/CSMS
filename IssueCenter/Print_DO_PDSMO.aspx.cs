using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IssueCenter_Print_DO_PDSMO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    string Dist_Id = "",QRDetails="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                lblDONo.Text = hdfDC_MO.Value = Session["DC_MO"].ToString();

                if (Session["CreatedDate"].ToString() == "NOTRequired".ToString())
                {
                    GetDataNotRequired();
                }
                else
                {
                    hdfCreatedDate.Value = Session["CreatedDate"].ToString();
                    GetData();
                }

                Dist_Id = Session["dist_id"].ToString();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetDataNotRequired()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Truck_No,Issued_Qty,Bags_Type, BT.BagType  as BagsTypeNew ,Issued_Bags,(select Quantity From TO_AgainstHO_MO where STO_No=DCMO.STO_No) TotalQty, (Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=DCMO.Commodity) ComdtyName,Commodity,(select RailHead_Name from tbl_Rail_Head where RailHead_Code=DCMO.FrmRailHaid) FrmRailName,FrmRailHaid,Rack_No,RecBranch,RecGodown,(select DepotName from tbl_MetaData_DEPOT where DepotID=DCMO.RecIC) ToICName,RecIC,(select district_name from pds.districtsmp where district_code=DCMO.ToDist) ToDistName,ToDist,Default_Branch,Default_Godown,Change_Branch,Change_Godown,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID = DCMO.Transporter_ID and Distt_ID='" + Dist_Id + "' and Transport_ID In ('6','11','10') order by Transporter_Name desc) TransporterName,Transporter_ID,(Select CreatedDate From TO_AgainstHO_MO where STO_No=DCMO.STO_No) TODate,STO_No,TO_No,(Select CreatedDate From StateMovementOrder Where SMO = DCMO.SMO) MODate,SMO, MoveOrdernum,(select district_name from pds.districtsmp where district_code=DCMO.FrmDist) FrmDistName,FrmDist,(select DepotName from tbl_MetaData_DEPOT where DepotID=DCMO.Issue_Center) ICName,Issue_Center,Issued_Date From DeliveryChallan_MO As DCMO inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=DCMO.Bags_Type where DC_MO='" + hdfDC_MO.Value + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblICName.Text = ds.Tables[0].Rows[0]["ICName"].ToString();
                    lblDistName.Text = lblDistName1.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    lblComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    lblTotalQty.Text = ds.Tables[0].Rows[0]["TotalQty"].ToString();
                    lblBags.Text = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();
                    lblBagType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();
                    lblIssuedQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    lblTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();

                    DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Issued_Date"].ToString());
                    lblDate.Text = CreatedDate.ToString("dd/MMM/yyyy");

                    lblMovmtNo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();

                    DateTime MODate = DateTime.Parse(ds.Tables[0].Rows[0]["MODate"].ToString());
                    lblMODate.Text = MODate.ToString("dd/MMM/yyyy");

                    lblTOno.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();

                    DateTime TODate = DateTime.Parse(ds.Tables[0].Rows[0]["TODate"].ToString());
                    lblTOEndDate.Text = TODate.ToString("dd/MMM/yyyy");

                    lblTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();


                    hdfDefault_Branch.Value = ds.Tables[0].Rows[0]["Default_Branch"].ToString();
                    hdfDefault_Godown.Value = ds.Tables[0].Rows[0]["Default_Godown"].ToString();
                    hdfChange_Branch.Value = ds.Tables[0].Rows[0]["Change_Branch"].ToString();
                    hdfChange_Godown.Value = ds.Tables[0].Rows[0]["Change_Godown"].ToString();

                    if (hdfChange_Branch.Value == "00" || hdfChange_Godown.Value == "00")
                    {
                        lblBranch.Text = hdfDefault_Branch.Value;
                        lblGodown.Text = hdfDefault_Godown.Value;
                    }
                    else
                    {
                        lblBranch.Text = hdfChange_Branch.Value;
                        lblGodown.Text = hdfChange_Godown.Value;
                    }

                    lblToBranch.Text = ds.Tables[0].Rows[0]["RecBranch"].ToString();
                    lblToGodown.Text = ds.Tables[0].Rows[0]["RecGodown"].ToString();

                    GetBGName();

                    lblToDistName.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                    lblToIC.Text = ds.Tables[0].Rows[0]["ToICName"].ToString();

                    hdfRack_No.Value = ds.Tables[0].Rows[0]["Rack_No"].ToString();

                    if (hdfRack_No.Value == "")
                    {
                        lblTo.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                    }
                    else
                    {
                        lblTo.Text = ds.Tables[0].Rows[0]["FrmRailName"].ToString();
                        RowRoad.Visible = false;
                    }

                    QRDetails = "Dist=" + lblDistName.Text + ", IC=" + lblICName.Text + ", DCNo=" + lblDONo.Text + ", MONo=" + lblMovmtNo.Text + ", TONo=" + lblTOno.Text + ", IssuedQty(Qtls)=" + lblIssuedQty.Text + ", Bags=" + lblBags.Text + ",Commodity=" + lblComdty.Text + ",BageType=" + lblBagType.Text;
                    ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRDetails;
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

    public void GetData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select BT.BagType  as BagsTypeNew, Truck_No,Issued_Qty,Bags_Type,Issued_Bags,(select Quantity From TO_AgainstHO_MO where STO_No=DCMO.STO_No) TotalQty, (Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=DCMO.Commodity) ComdtyName,Commodity,(select RailHead_Name from tbl_Rail_Head where RailHead_Code=DCMO.FrmRailHaid) FrmRailName,FrmRailHaid,Rack_No,RecBranch,RecGodown,(select DepotName from tbl_MetaData_DEPOT where DepotID=DCMO.RecIC) ToICName,RecIC,(select district_name from pds.districtsmp where district_code=DCMO.ToDist) ToDistName,ToDist,Default_Branch,Default_Godown,Change_Branch,Change_Godown,(select Top 1 (Transporter_Name) from Transporter_Table where Transporter_ID = DCMO.Transporter_ID and Distt_ID='" + Dist_Id + "' and Transport_ID In ('6','11','10') order by Transporter_Name desc) TransporterName,Transporter_ID,(Select CreatedDate From TO_AgainstHO_MO where STO_No=DCMO.STO_No) TODate,STO_No,TO_No,(Select CreatedDate From StateMovementOrder Where SMO = DCMO.SMO) MODate,SMO, MoveOrdernum,(select district_name from pds.districtsmp where district_code=DCMO.FrmDist) FrmDistName,FrmDist,(select DepotName from tbl_MetaData_DEPOT where DepotID=DCMO.Issue_Center) ICName,Issue_Center,Issued_Date From DeliveryChallan_MO As DCMO inner join FIN_Bag_Type as BT on BT.Bag_Type_ID=DCMO.Bags_Type where DC_MO='" + hdfDC_MO.Value + "' and CreatedDate='" + hdfCreatedDate.Value + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblICName.Text = ds.Tables[0].Rows[0]["ICName"].ToString();
                    lblDistName.Text = lblDistName1.Text = ds.Tables[0].Rows[0]["FrmDistName"].ToString();
                    lblComdty.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();
                    lblTotalQty.Text = ds.Tables[0].Rows[0]["TotalQty"].ToString();
                    lblBags.Text = ds.Tables[0].Rows[0]["Issued_Bags"].ToString();
                    lblBagType.Text = ds.Tables[0].Rows[0]["BagsTypeNew"].ToString();
                    lblIssuedQty.Text = ds.Tables[0].Rows[0]["Issued_Qty"].ToString();
                    lblTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();

                    DateTime CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Issued_Date"].ToString());
                    lblDate.Text = CreatedDate.ToString("dd/MMM/yyyy");

                    lblMovmtNo.Text = ds.Tables[0].Rows[0]["MoveOrdernum"].ToString();

                    DateTime MODate = DateTime.Parse(ds.Tables[0].Rows[0]["MODate"].ToString());
                    lblMODate.Text = MODate.ToString("dd/MMM/yyyy");

                    lblTOno.Text = ds.Tables[0].Rows[0]["TO_No"].ToString();

                    DateTime TODate = DateTime.Parse(ds.Tables[0].Rows[0]["TODate"].ToString());
                    lblTOEndDate.Text = TODate.ToString("dd/MMM/yyyy");

                    lblTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();


                    hdfDefault_Branch.Value = ds.Tables[0].Rows[0]["Default_Branch"].ToString();
                    hdfDefault_Godown.Value = ds.Tables[0].Rows[0]["Default_Godown"].ToString();
                    hdfChange_Branch.Value = ds.Tables[0].Rows[0]["Change_Branch"].ToString();
                    hdfChange_Godown.Value = ds.Tables[0].Rows[0]["Change_Godown"].ToString();

                    if (hdfChange_Branch.Value == "00" || hdfChange_Godown.Value == "00")
                    {
                        lblBranch.Text = hdfDefault_Branch.Value;
                        lblGodown.Text = hdfDefault_Godown.Value;
                    }
                    else
                    {
                        lblBranch.Text = hdfChange_Branch.Value;
                        lblGodown.Text = hdfChange_Godown.Value;
                    }

                    lblToBranch.Text = ds.Tables[0].Rows[0]["RecBranch"].ToString(); 
                    lblToGodown.Text = ds.Tables[0].Rows[0]["RecGodown"].ToString(); 

                    GetBGName();

                    lblToDistName.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                    lblToIC.Text = ds.Tables[0].Rows[0]["ToICName"].ToString();

                    hdfRack_No.Value = ds.Tables[0].Rows[0]["Rack_No"].ToString();

                    if (hdfRack_No.Value == "")
                    {
                        lblTo.Text = ds.Tables[0].Rows[0]["ToDistName"].ToString();
                    }
                    else
                    {
                        lblTo.Text = ds.Tables[0].Rows[0]["FrmRailName"].ToString();
                        RowRoad.Visible = false;
                    }

                    QRDetails = "Dist=" + lblDistName.Text + ", IC=" + lblICName.Text + ", DCNo=" + lblDONo.Text + ", MONo=" + lblMovmtNo.Text + ", TONo=" + lblTOno.Text + ", IssuedQty(Qtls)=" + lblIssuedQty.Text + ", Bags=" + lblBags.Text + ",Commodity=" + lblComdty.Text + ",BageType=" + lblBagType.Text;
                    ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRDetails;
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

    public void GetBGName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select1 = string.Format("select (select DepotName from tbl_MetaData_DEPOT where BranchId='" + lblBranch.Text + "') BranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblGodown.Text + "') Godown_Name,(select DepotName from tbl_MetaData_DEPOT where BranchId='" + lblToBranch.Text + "') ToBranchName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + lblToGodown.Text + "') ToGodown_Name");
                da1 = new SqlDataAdapter(select1, con_MPStorage);

                ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        lblBranch.Text = ds1.Tables[0].Rows[0]["BranchName"].ToString();
                        lblGodown.Text = ds1.Tables[0].Rows[0]["Godown_Name"].ToString();

                        lblToBranch.Text = ds1.Tables[0].Rows[0]["ToBranchName"].ToString();
                        lblToGodown.Text = ds1.Tables[0].Rows[0]["ToGodown_Name"].ToString();
                    }
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
    }
}