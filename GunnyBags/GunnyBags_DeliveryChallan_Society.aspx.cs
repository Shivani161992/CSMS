using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class GunnyBags_GunnyBags_DeliveryChallan_Society : System.Web.UI.Page
{
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;
    string transporter, RDist, RailHead, Rgodown = "";

    double QtyTotal = 0;
    public string sid = "";
    string OwnDist, OtherDist;
    public string ICID, DistId;
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int RemainingQty;
    string TotalQty;

    string MS, Bagstype, Quanty, EDOTDate, TransporterID, SGodown, RSDist, Society = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["issue_id"] != null)
        {


            if (!IsPostBack)
            {
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                //txtdistrict.Text = Session["dist_id"].ToString();
                //txtIC.Text = Session["issue_id"].ToString();

                GodownGetICDIST();

                //GodownGet();
            }
        }
    }

    public void GodownGetICDIST()
    {
        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {

                con.Open();
                string select = string.Format("select district_name from pds.districtsmp where district_code='" + DistId + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtdistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();


                        string selectIC = string.Format("select DepotName from tbl_MetaData_DEPOT where DepotID='" + ICID + "'");
                        da = new SqlDataAdapter(selectIC, con);

                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txtIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                GodownGet();
                            }
                        }

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
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


    public void GodownGet()
    {
        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and DepotId='" + ICID + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
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


    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Insert(0, "--Select--");

    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetTransportOrder();
    }

    public void GetTransportOrder()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {

                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("select BookNumber from GunnyBags_TransportOrder_Society where SDist='" + DistId + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and SGodown='" + ddlGodown.SelectedValue.ToString() + "' order by BookNumber");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTO.DataSource = ds.Tables[0];
                        ddlTO.DataTextField = "BookNumber";
                        ddlTO.DataValueField = "BookNumber";
                        ddlTO.DataBind();
                        ddlTO.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Number is Not Available'); </script> ");
                    return;
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
    protected void ddlTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTransportOrderDta();


    }
    public void GetTransportOrderDta()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {

                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                con.Open();
                string select = string.Format("select G.Godown_Name, MarSea, BagsType, Issue_Qty, convert(varchar(10),End_DOT,103) as End_DOT,TransporterID, TT.Transporter_Name, SGodown, Society_Dist, D.district_name, Society, S.Society_Name_Eng+ + Society as Society_Name_Eng from GunnyBags_TransportOrder_Society as TOS inner join pds.districtsmp as D on D.district_code=TOS.Society_Dist inner join Society2016 as S on S.Society_Id=TOS.Society inner join Transporter_Table as TT on TT.Transporter_ID=TOS.TransporterID   inner join tbl_MetaData_GODOWN as G on G.Godown_ID=TOS.SGodown where BookNumber='" + ddlTO.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        string MS = "";
                        MS = ds.Tables[0].Rows[0]["MarSea"].ToString();
                        if (MS == "R")
                        {
                            txtMS.Text = "Rabi";
                        }
                        else if (MS == "K")
                        {
                            txtMS.Text = "Khariff";
                        }

                        //txtMS.Text = ds.Tables[0].Rows[0]["MarSea"].ToString();
                        txtBagstype.Text = ds.Tables[0].Rows[0]["BagsType"].ToString();
                        txtQuantity.Text = ds.Tables[0].Rows[0]["Issue_Qty"].ToString();
                        txtEnd_DOT.Text = ds.Tables[0].Rows[0]["End_DOT"].ToString();

                        txtTransporter.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                        txtSGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        txtSocDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        txtSociety.Text = ds.Tables[0].Rows[0]["Society_Name_Eng"].ToString();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Transport Order Number is Not Available'); </script> ");
                    return;
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
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

        bttSubmit.Enabled = true;
    }

    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlTO.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving ID'); </script> ");
            return;
        }
        else if (txtTCNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter TC Number'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year'); </script> ");
            return;
        }
        else if (txtDateofReceipt.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Date of Quantity Issued '); </script> ");
            return;
        }

        else if (txtTruckNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Truck Number '); </script> ");
            return;
        }

        else if (txtQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Quantity that is to be issued '); </script> ");
            return;
        }

        else
        {
            using (con = new SqlConnection(strcon))
                try
                {
                    con.Open();

                    string DistCode = Session["dist_id"].ToString();
                    string qrey = "select max(DC_Society) as DC_Society from GunnyBags_DeliveryChallan_Society where LEN(DC_Society)<20 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);

                    DataRow dr = ds.Tables[0].Rows[0];

                    gatepass = ds.Tables[0].Rows[0]["DC_Society"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "1819800";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }



                    string select = string.Format("select G.Godown_Name, MarSea, BagsType, Issue_Qty, convert(varchar(10),End_DOT,103) as End_DOT,TransporterID, TT.Transporter_Name, SGodown, Society_Dist, D.district_name, Society, S.Society_Name_Eng+ + Society as Society_Name_Eng from GunnyBags_TransportOrder_Society as TOS inner join pds.districtsmp as D on D.district_code=TOS.Society_Dist inner join Society2016 as S on S.Society_Id=TOS.Society inner join Transporter_Table as TT on TT.Transporter_ID=TOS.TransporterID   inner join tbl_MetaData_GODOWN as G on G.Godown_ID=TOS.SGodown where BookNumber='" + ddlTO.SelectedValue.ToString() + "'");
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {


                            MS = ds.Tables[0].Rows[0]["MarSea"].ToString();




                            Bagstype = ds.Tables[0].Rows[0]["BagsType"].ToString();
                            Quanty = ds.Tables[0].Rows[0]["Issue_Qty"].ToString();
                            EDOTDate = ds.Tables[0].Rows[0]["End_DOT"].ToString();

                            TransporterID = ds.Tables[0].Rows[0]["TransporterID"].ToString();
                            SGodown = ds.Tables[0].Rows[0]["SGodown"].ToString();
                            RSDist = ds.Tables[0].Rows[0]["Society_Dist"].ToString();
                            Society = ds.Tables[0].Rows[0]["Society"].ToString();
                        }
                    }



                    ICID = Session["issue_id"].ToString();
                    DistId = Session["dist_id"].ToString();
                    string BookNumber = "DC" + DistCode + gatepass;

                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    ConvertServerDate ServerDeliveryDate = new ConvertServerDate();
                    string IssuedDeliveryDate = ServerDeliveryDate.getDate_MDY(txtDateofReceipt.Text);

                    ConvertServerDate EDOTDa = new ConvertServerDate();
                    string EDOT = EDOTDa.getDate_MDY(EDOTDate);

                    string strinsert = "insert into GunnyBags_DeliveryChallan_Society([DC_Society],[SDist]  ,[SIC]   ,[SGodown]  ,[CropYear] ,[Trasnport_Order]  ,[MarSea] ,[BagsType] ,[Quantity]  ,[End_DOT] ,[TransporterID] ,[SenGodown] ,[RecdDist]  ,[RecdSociety]  ,[TruckNum] ,[ToulReceipt]     ,[Truckchallan],[DateOFIssue]  ,[Quantity_Issue],[CreatedDate] ,[IP],[BookNumber]) values ('" + gatepass + "','" + DistCode + "','" + ICID + "','" + ddlGodown.SelectedValue.ToString() + "','" + ddlCropYear.SelectedValue.ToString() + "','" + ddlTO.SelectedValue.ToString() + "','" + MS + "','" + Bagstype + "','" + txtQuantity.Text + "','" + EDOT + "','" + TransporterID + "','" + SGodown + "','" + RSDist + "','" + Society + "','" + txtTruckNo.Text + "','" + txtReceipt.Text + "','" + txtTCNo.Text + "','"+IssuedDeliveryDate+"','" + TextBox1.Text + "',getdate(),'"+ip+"','"+BookNumber+"')";
                    cmd = new SqlCommand(strinsert, con);
                    string check = (string)cmd.ExecuteScalar();

                    //if (rdbOwnDist.Checked == true)
                    //{
                    //    OwnDist = "Y";
                    //    OtherDist = "N";
                    //    string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtTransporterName.Text + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + DistCode + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "')";
                    //    cmd = new SqlCommand(strinsert, con);
                    //    string check = (string)cmd.ExecuteScalar();
                    //}
                    //else if (rdbOtherDist.Checked == true)
                    //{
                    //    OwnDist = "N";
                    //    OtherDist = "Y";
                    //    string strinsert = "insert into GunnyBags_DeliveryChallan( DC_Number_ID, Sending_District, CropYear, RR_Receive_ID, BagsType, Total_QTY, TC_Number, Date_of_Issue, Transporter_Name_HLRT, Truck_Number, Own_District,  Other_District, Receiving_District, Receiving_IC, Receiving_Branch, Receiving_Godown, CreatedDate, IP, Qty_Issued) values ('" + gatepass + "','" + DistCode + "','" + txtCropYear.Text + "','" + ddlTO.SelectedValue.ToString() + "','" + txtBagsType.Text + "','" + txtReceivedQty.Text + "','" + txtTCNo.Text + "','" + IssuedDeliveryDate + "','" + txtTransporterName.Text + "','" + txtTruckNo.Text + "','" + OwnDist + "','" + OtherDist + "','" + ddlDelidist.SelectedValue.ToString() + "','" + ddlIC.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + txtQuantity.Text + "')";
                    //    cmd = new SqlCommand(strinsert, con);
                    //    string check = (string)cmd.ExecuteScalar();
                    //}


                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                    bttSubmit.Enabled = false;
                    bttSubmit.Visible = false;
                    bttPrint.Visible = true;
                    bttPrint.Enabled = false;

                    ddlTO.Enabled = false;
                    txtTruckNo.Enabled = false;
                    txtTCNo.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtDateofReceipt.Enabled = false;
                    txtReceipt.Enabled = false;
                    TextBox1.Enabled = false;


                   
                    //ddlDelidist.Enabled = false;
                    //ddlIC.Enabled = false;
                    //ddlBranch.Enabled = false;
                    //ddlGodown.Enabled = false;
                    trID.Visible = true;
                    Label1.Text = "Delivery Challan ID is:- " + BookNumber;


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


    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Gunny_BagsHome.aspx");
    }
    protected void bttPrint_Click(object sender, EventArgs e)
    {

    }

}