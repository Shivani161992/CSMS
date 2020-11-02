using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class State_Print_SubMovementOrder : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double QtyTotal = 0, ConvertQtlsToMT = 0, QtyTotalSubMO = 0;
    string QRGridDetails;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    int ro = 0;
    int RowSpan = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                //lblCancel.Visible = false;
                if (Session["Acpt/Rjct"] != null)
                {
                    if (Session["Acpt/Rjct"] == "Reject")
                    {
                        lblCancel.Text = "[Cancelled]";
                    }
                    else if (Session["Acpt/Rjct"] == "Pending")
                    {
                        lblCancel.Text = "[Pending]";
                    }
                    else
                    {
                        lblCancel.Text = "[Approved]";
                    }
                }
                hdfMovmtOrderNo.Value = lblMovmtNo.Text = Session["MovmtOrderNo"].ToString();
                GetData();
                GetSubData();
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
                string select = string.Format("SELECT ModeofDist,(Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=Commodity) ComdtyName,Commodity,ReachDate,(select Source_Name from  Source_Arrival_Type where Source_ID=ModeofDispatch) DispatchModeName,ModeofDispatch,(SELECT district_name FROM pds.districtsmp where district_code=FrmDist) FromDistName,FrmDist,CropYear,GETDATE() CurrentDate,CreatedDate,GunnyType FROM StateMovementOrder where MoveOrdernum='" + hdfMovmtOrderNo.Value + "'");
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
                        hdfEndDate.Value = frmDate.ToString("dd/MMM/yyyy");

                        lblCurrentDateTime.Text = ds.Tables[0].Rows[0]["CurrentDate"].ToString();

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
                            lblSig.Text = "वित० एवं निराकरण";
                        }

                        lblTransMode.Text = ds.Tables[0].Rows[0]["DispatchModeName"].ToString();

                        string ModeofDist = ds.Tables[0].Rows[0]["ModeofDist"].ToString();

                        if (ModeofDist == "Other")
                        {
                            lblDistMode.Text = "रैक प्राप्तकर्ता जिले द्वारा स्कंध का परिवहन केवल अन्य जिलों में निम्नानुसार किया जाये :";
                        }
                        else
                        {
                            lblDistMode.Text = "रैक प्राप्तकर्ता जिले द्वारा स्कंध का परिवहन स्वयं तथा अन्य के जिलों में निम्नानुसार किया जाये :";
                        }

                        hdfFromDist.Value = ds.Tables[0].Rows[0]["FromDistName"].ToString();
                        hdfCropYear.Value = ds.Tables[0].Rows[0]["CropYear"].ToString();

                        QRGridDetails = "Movement Order No=" + hdfMovmtOrderNo.Value + " , Movement Order Date=" + lblDate.Text + " , Movement Order End Date=" + hdfEndDate.Value + ", Commodity=" + lblComdty.Text + ", Mode of Dispatch=" + lblTransMode.Text + ", Sending Dist=" + hdfFromDist.Value + " , Receiving Dist ";
                    }

                    select = string.Format("Select Quantity,(SELECT district_name FROM pds.districtsmp where district_code=ToDist) ReceiveDistName,ToDist From StateMovementOrder where MoveOrdernum='" + hdfMovmtOrderNo.Value + "'");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        string qrdata = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            qrdata += ((qrdata == "") ? "" : " , ") + "{" + ds.Tables[0].Rows[i]["ReceiveDistName"] + "=";

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

            e.Row.Cells[4].Text = hdfCropYear.Value;
            e.Row.Cells[1].Text = hdfFromDist.Value;
            e.Row.Cells[3].Text = hdfEndDate.Value;

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
            lblnotowords.Text = NumbersToWords(QtyTotal.ToString());

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

    public void GetSubData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select1 = "";

                if (lblComdty.Text == "Gunny")
                {
                    select1 = string.Format("Select (SubMO.QtyByDist) SubQtyByDist ,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToOtherDist) RecdDist,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToDist) RackRecdDist From StateSubMovementOrder SubMO where MoveOrdernum='" + hdfMovmtOrderNo.Value + "'");
                }
                else
                {
                    select1 = string.Format("Select (SubMO.QtyByDist/10) SubQtyByDist ,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToOtherDist) RecdDist,(SELECT district_name FROM pds.districtsmp where district_code=SubMO.ToDist) RackRecdDist From StateSubMovementOrder SubMO where MoveOrdernum='" + hdfMovmtOrderNo.Value + "'");
                }

                da = new SqlDataAdapter(select1, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
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

            if (lblComdty.Text == "Gunny")
            {
                e.Row.Cells[3].Text = "परिवहन की मात्रा (Bales)";
            }
            else
            {
                e.Row.Cells[3].Text = "परिवहन की मात्रा (मै० टन)";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            ConvertQtlsToMT = 0;
            ConvertQtlsToMT = ((double.Parse(e.Row.Cells[3].Text)));
            QtyTotalSubMO += ConvertQtlsToMT;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "महायोग ";
            e.Row.Cells[3].Text = QtyTotalSubMO.ToString("0.00");

            RowSpan = 1;
            ro = 0;
            int j = 0;
            int z = 1;
            double kulyog = 0;
            for (int i = 0; i < GridView2.Rows.Count - 1; i++)
            {

                GridViewRow currrow = GridView2.Rows[j];
                GridViewRow nextrow = GridView2.Rows[i + 1];
                if (currrow.Cells[1].Text == nextrow.Cells[1].Text)
                {

                    nextrow.Cells[1].Visible = false;
                    RowSpan += 1;
                    ro++;
                    GridView2.Rows[j].Cells[0].Text = "1";
                    GridView2.Rows[i + 1].Cells[0].Text = RowSpan.ToString();
                    kulyog += (double.Parse(GridView2.Rows[i].Cells[3].Text));
                }
                else
                {
                    RowSpan++;
                    currrow.Cells[1].RowSpan = RowSpan;
                    z++;
                    kulyog += (double.Parse(GridView2.Rows[i].Cells[3].Text));
                    GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableHeaderCell oTableCell = new TableHeaderCell();
                    oGridViewRow.Cells.Add(oTableCell);
                    oTableCell = new TableHeaderCell();
                    oTableCell.Text = "कुल योग";
                    oTableCell.ForeColor = System.Drawing.Color.Red;
                    oTableCell.HorizontalAlign = HorizontalAlign.Right;
                    oGridViewRow.Cells.Add(oTableCell);
                    oTableCell = new TableHeaderCell();
                    oTableCell.Text = kulyog.ToString("0.00");
                    oTableCell.ForeColor = System.Drawing.Color.Red;
                    oTableCell.HorizontalAlign = HorizontalAlign.Right;
                    oGridViewRow.Cells.Add(oTableCell);
                    int x = (i + z);
                    GridView2.Controls[0].Controls.AddAt(x, oGridViewRow);
                    RowSpan = 1;
                    j = i + 1;
                    kulyog = 0;
                }
            }

            kulyog += (double.Parse(GridView2.Rows[GridView2.Rows.Count - 1].Cells[3].Text));
            GridViewRow currrow1 = GridView2.Rows[j];
            currrow1.Cells[1].RowSpan = RowSpan + 1;
            GridViewRow oGridViewRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableHeaderCell oTableCell1 = new TableHeaderCell();
            oGridViewRow1.Cells.Add(oTableCell1);
            oTableCell1 = new TableHeaderCell();
            oTableCell1.Text = "कुल योग";
            oTableCell1.ForeColor = System.Drawing.Color.Red;
            oTableCell1.HorizontalAlign = HorizontalAlign.Right;
            oGridViewRow1.Cells.Add(oTableCell1);
            oTableCell1 = new TableHeaderCell();
            oTableCell1.Text = kulyog.ToString("0.00");
            oTableCell1.ForeColor = System.Drawing.Color.Red;
            oTableCell1.HorizontalAlign = HorizontalAlign.Right;
            oGridViewRow1.Cells.Add(oTableCell1);
            int x1 = (GridView2.Rows.Count + z);
            GridView2.Controls[0].Controls.AddAt(x1, oGridViewRow1);

        }
    }

    private static string NumbersToWords(string inputNumber)
    {

        int inputNo = Convert.ToInt16(inputNumber);

        if (inputNo == 0)
            return "Zero";

        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }

        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        //lblnotowords.Text = sb.ToString();
        return sb.ToString().TrimEnd();


    }
}