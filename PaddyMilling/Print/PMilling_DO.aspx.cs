using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_Print_PMilling_DO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string Society, Godown, Societydist, societyname;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    double QtyTotal = 0;
    string QRGridDetails = "", districtid = "", BranchName = "", strMixIC = "";
    int ro = 0, RowSpan = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["Markfed"].ToString() == "Y")
                {
                    Image1.ImageUrl = "~/Images/markfedPDY.jpg";
                    lblMFD.Text = "मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड";
                    lblDM.Text = lblDM0.Text = "जिला विपणन अधिकारी";
                }
                else
                {
                    Image1.ImageUrl = "~/Images/mpscsc_logo.jpg";
                    lblMFD.Text = "मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड";
                    lblDM.Text = lblDM0.Text = "जिला प्रबंधक";
                }

                ViewState["PreviousRow"] = "0000";
                districtid = Session["dist_id"].ToString();
                lblDoNumber.Text = Session["AgrmtNo"].ToString();
                GetDataTO();
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

                string select = "Select GETDATE() As CurrentDateTime ,SocietyDist, SocietyID,PMDO.Agreement_ID,PMDO.CropYear,PMDO.From_Date,PMDO.To_Date,PMDO.Milling_Type,PMDO.Alloted_CommonDhan,PMDO.Alloted_GradeADhan,PMDO.Branch_Code,PMDO.Godown_Code,PMDO.Rem_Alloted_CommonDhan,PMDO.Rem_Alloted_GradeADhan,PMDO.LotNo,MP.district_name,PMDO.Issue_Centre As DepotName,MR.Mill_Name,MA.Common_Dhan,MA.GradeA_Dhan, IsSociety, IsGodown From PaddyMilling_DO_2017 As PMDO Left Join pds.districtsmp As MP ON(MP.district_code=PMDO.District) Left join Miller_Registration_2017 As MR ON(PMDO.Mill_Code=MR.Registration_ID and PMDO.CropYear=MR.CropYear) Left Join PaddyMilling_Agreement_2017 MA ON(MA.District=PMDO.District and MA.Mill_Name=PMDO.Mill_Code and MA.CropYear=PMDO.CropYear and MA.Agreement_ID=PMDO.Agreement_ID) where PMDO.District='" + districtid + "' and PMDO.Check_DO='" + lblDoNumber.Text + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Society = ds.Tables[0].Rows[0]["IsSociety"].ToString();
                    Godown = ds.Tables[0].Rows[0]["IsGodown"].ToString();

                    lblDistManagerName.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

                    DateTime FrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["From_Date"].ToString());
                    lblDate.Text = lblDate1.Text = FrmDate.ToString("dd/MMM/yyyy");
                    
                    lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDateTime"].ToString();

                    if (Society == "Y")
                    {
                        lblIssueCentreName.Text = ds.Tables[0].Rows[0]["SocietyDist"].ToString();
                        lblGS.Text="उपार्जन केंद्र";

                    }
                    else if (Godown == "Y")
                    {
                        lblIssueCentreName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                        lblGS.Text = "गोदाम";


                    }


                    lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    lblMillName.Text = lblMillName1.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    lblAgrmtNo.Text = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();
                    lblTotalCDhan.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();
                    lblTotalGDhan.Text = ds.Tables[0].Rows[0]["GradeA_Dhan"].ToString();

                    lblAllotedGDhan.Text = "0";

                    lblMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();

                  
                    Societydist = ds.Tables[0].Rows[0]["SocietyDist"].ToString();
                    societyname = ds.Tables[0].Rows[0]["SocietyID"].ToString();


                    if (lblMillingType.Text == "अरवा")
                    {
                        lblAllotedCDhan.Text = "403";
                    }
                    else
                    {
                        lblAllotedCDhan.Text = "397";
                    }

                    DateTime EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["To_Date"].ToString());
                    lblEndDate.Text = lblEndDate1.Text = EndDate.ToString("dd/MMM/yyyy");

                    lblRemCDhan.Text = ds.Tables[0].Rows[0]["Rem_Alloted_CommonDhan"].ToString();
                    lblRemGDhan.Text = ds.Tables[0].Rows[0]["Rem_Alloted_GradeADhan"].ToString();

                    QRGridDetails = "Dist=" + lblDistManagerName.Text + ", DO_No=" + lblDoNumber.Text + ", CropYear=" + lblCropYear.Text + ", Agrmt_No=" + lblAgrmtNo.Text + ", DO_Date=" + lblDate.Text + ", DO_EndDate=" + lblEndDate.Text + ", Rem_CommonDhan=" + lblRemCDhan.Text + "(Qtls), Rem_GradeADhan=" + lblRemGDhan.Text + " (Qtls)";
                    ImgQRCode.ImageUrl = ImgQRCode.ImageUrl = "https://chart.googleapis.com/chart?chs=500x500&cht=qr&chl=" + QRGridDetails;

                    if (Society == "Y")
                    {
                        trsociety.Visible = true;
                        trgodown.Visible = false;
                        GridView2.DataSource = ds.Tables[0];
                        GridView2.DataBind();
                       
                    }
                    else if (Godown == "Y")
                    {
                        trsociety.Visible = false;
                        trgodown.Visible = true;
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    
                    }
                    //GridView1.DataSource = ds.Tables[0];
                    //GridView1.DataBind();
                }
                else
                {
                    if (Society == "Y")
                    {
                        trsociety.Visible = true;
                        trgodown.Visible = false;
                        GridView2.DataSource = "";
                        GridView2.DataBind();
                    }
                    else if (Godown == "Y")
                    {
                        trsociety.Visible = false; ;
                        trgodown.Visible = true;
                        GridView1.DataSource = "";
                        GridView1.DataBind();

                    }
                  
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
        string GodownName = "", ICName = "";

        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            GodownName = e.Row.Cells[2].Text;
            ICName = e.Row.Cells[1].Text;

            using (con_MPStorage = new SqlConnection(strcon_MPStorage))
            {
                try
                {
                    con_MPStorage.Open();
                    string select = "";
                    //select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "'");

                    select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where DepotId='" + ICName + "') ICName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') Godown_Name");
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
                                strMixIC += ((strMixIC == "") ? "" : ", ") + ds.Tables[0].Rows[0]["ICName"].ToString();
                            }

                            ViewState["PreviousRow"] = e.Row.Cells[1].Text;

                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                            e.Row.Cells[1].Text = ds.Tables[0].Rows[0]["ICName"].ToString();

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

            QtyTotal += (double.Parse(e.Row.Cells[3].Text));

            double QtyRow = (double.Parse(e.Row.Cells[3].Text));
            e.Row.Cells[3].Text = QtyRow.ToString("0.00");
            lblIssueCentreName.Text = strMixIC;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total Qty = ";
            e.Row.Cells[3].Text = QtyTotal.ToString("0.00");

            RowSpan = 1;
            ro = 0;
            int j = 0;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                GridViewRow currrow = GridView1.Rows[j];
                GridViewRow nextrow = GridView1.Rows[i + 1];
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

            GridViewRow currrow1 = GridView1.Rows[j];
            currrow1.Cells[1].RowSpan = RowSpan;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string SocietyDist = "", SocietyName = "";

        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            SocietyName = e.Row.Cells[2].Text;
            SocietyDist = e.Row.Cells[1].Text;

            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string select = "";
                    //select = string.Format("select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "'");

                    select = string.Format("select (select district_name from pds.districtsmp where district_code='" + SocietyDist + "') SocietyDist,(select Society_Name + '('+Society_Id + ')' as Society_Name  from Society_Kharif17 where Society_Id='" + SocietyName + "') SocietyName");
                    da = new SqlDataAdapter(select, con);

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
                                strMixIC += ((strMixIC == "") ? "" : ", ") + ds.Tables[0].Rows[0]["SocietyDist"].ToString();
                            }

                            ViewState["PreviousRow"] = e.Row.Cells[1].Text;

                            e.Row.Cells[2].Text = ds.Tables[0].Rows[0]["SocietyName"].ToString();
                            e.Row.Cells[1].Text = ds.Tables[0].Rows[0]["SocietyDist"].ToString();

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

            QtyTotal += (double.Parse(e.Row.Cells[3].Text));

            double QtyRow = (double.Parse(e.Row.Cells[3].Text));
            e.Row.Cells[3].Text = QtyRow.ToString("0.00");
            lblIssueCentreName.Text = strMixIC;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total Qty = ";
            e.Row.Cells[3].Text = QtyTotal.ToString("0.00");

            RowSpan = 1;
            ro = 0;
            int j = 0;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
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