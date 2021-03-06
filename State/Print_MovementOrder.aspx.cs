﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class State_Print_MovementOrder : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string NoToWords;
    double QtyTotal = 0, ConvertQtlsToMT = 0;
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
                        //lblCancel.Text = "[Pending]";
                        lblCancel.Text = "";
                    }
                    else
                    {
                        lblCancel.Text = "[Approved]";
                    }
                }
                ViewState["hdfMovmtOrderNo"] = lblMovmtNo.Text = Session["MovmtOrderNo"].ToString();
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
                string select = string.Format("SELECT (Select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=Commodity) ComdtyName,Commodity,ReachDate,(select Source_Name from  Source_Arrival_Type where Source_ID=ModeofDispatch) DispatchModeName,ModeofDispatch,(SELECT district_name FROM pds.districtsmp where district_code=FrmDist) FromDistName,FrmDist,CropYear,GETDATE() CurrentDate,CreatedDate,GunnyType FROM StateMovementOrder where MoveOrdernum='" + ViewState["hdfMovmtOrderNo"].ToString() + "'");
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
                            //lblSig.Text = "परिवहन";
                            lblSig.Text = "वित० एवं निराकरण";
                        }

                        lblTransMode.Text = ds.Tables[0].Rows[0]["DispatchModeName"].ToString();

                        //hdfMovmtIdentID.Value = ds.Tables[0].Rows[0]["Id"].ToString();
                        ViewState["hdfFromDist"] = ds.Tables[0].Rows[0]["FromDistName"].ToString();
                        ViewState["hdfCropYear"] = ds.Tables[0].Rows[0]["CropYear"].ToString();

                        QRGridDetails = "Movement Order No=" + ViewState["hdfMovmtOrderNo"].ToString() + " , Movement Order Date=" + lblDate.Text + " , Movement Order End Date=" + ViewState["hdfEndDate"].ToString() + ", Commodity=" + lblComdty.Text + ", Mode of Dispatch=" + lblTransMode.Text + ", Sending Dist=" + ViewState["hdfFromDist"].ToString() + " , Receiving Dist ";

                    }

                    select = string.Format("Select Quantity,(SELECT district_name FROM pds.districtsmp where district_code=ToDist) ReceiveDistName,ToDist From StateMovementOrder where MoveOrdernum='" + ViewState["hdfMovmtOrderNo"].ToString() + "'");
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
           lblnotowords.Text= NumbersToWords(QtyTotal.ToString());
            
            
           

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