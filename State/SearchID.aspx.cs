using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class State_SearchID : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void search_Click(object sender, EventArgs e)
    {
        if (ddlcroptype.SelectedIndex == 0)   // For Wheat
        {
            # region frm WPMS

            if (con_WPMS.State == ConnectionState.Closed)
            {
                con_WPMS.Open();
            }

            string qry = "select Society.Society_Name + ','+ Society.SocPlace + ',' + Society.Society_Id as Society , convert(nvarchar,IssueToSangrahanaKendra.DateOfIssue,103)IssueDate ,IssueToSangrahanaKendra.TruckChalanNo ,IssueToSangrahanaKendra.Bags , IssueToSangrahanaKendra.QtyTransffer ,ISNULL(IssueCenterReceipt_Online.IssueID ,'')inProcuremnet , Acceptance_Note_Detail.Acceptance_No , Acceptance_Note_Detail.Proc_Flag from Society inner join IssueToSangrahanaKendra on Society.Society_Id = IssueToSangrahanaKendra.PCID left join IssueCenterReceipt_Online on IssueCenterReceipt_Online.IssueID = IssueToSangrahanaKendra.IssueID left join Acceptance_Note_Detail on Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID and Acceptance_Note_Detail.godown = IssueCenterReceipt_Online.Recd_Godown where IssueToSangrahanaKendra.IssueID = '" + txtId.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(qry, con_WPMS);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dispdate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();

                senproc.Text = ds.Tables[0].Rows[0]["Society"].ToString();

                inwpms.Text = ds.Tables[0].Rows[0]["inProcuremnet"].ToString();
                
                challan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();

                sendbags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                sendQty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

                wpmaccept.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();

                lblpymnt.Text = ds.Tables[0].Rows[0]["Proc_Flag"].ToString();

                if (inwpms.Text == "")
                {
                    inwpms.Text = "NO";
                }

                else
                {
                    inwpms.Text = "YES";
                }

            }

            else
            {
                dispdate.Text = "";

                senproc.Text = "";

                inwpms.Text = "";

                challan.Text = "";

                sendbags.Text = "";

                sendQty.Text = "";

                Whrnum.Text = "";

                Recdate.Text = "";

                Recdist.Text = "";

                RecIssuecenter.Text = "";

                Accepnum.Text = "";

                recbags.Text = "";

                RecQty.Text = "";

                incsms.Text = "";

                WhrDate.Text = "";

                AccepDate.Text = "";

                lblpymnt.Text = "";

                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Invalid Issue ID.....'); </script> ");

                return;
            }


            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }

            # endregion


            # region frm CSMS

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qrycsms = "Select CONVERT(nvarchar,SCSC_Procurement.Recd_Date,103)RecdDate, pds.districtsmp.district_name,tbl_MetaData_DEPOT.DepotName , SCSC_Procurement.Recd_Bags,SCSC_Procurement.Recd_Qty , Acceptance_Note_Detail.Acceptance_No , ISNULL(convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103),'') Acceptance_Date from SCSC_Procurement inner join pds.districtsmp on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID left join Acceptance_Note_Detail on Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id where SCSC_Procurement.Receipt_Id = '" + txtId.Text.Trim() + "'";
            
            SqlCommand cmdcmms = new SqlCommand(qrycsms, con);

            SqlDataAdapter dacsms = new SqlDataAdapter(cmdcmms);

            DataSet dscsms = new DataSet();

            dacsms.Fill(dscsms);

            if (dscsms.Tables[0].Rows.Count > 0)
            {
                Recdate.Text = dscsms.Tables[0].Rows[0]["RecdDate"].ToString();

                Recdist.Text = dscsms.Tables[0].Rows[0]["district_name"].ToString();

                RecIssuecenter.Text = dscsms.Tables[0].Rows[0]["DepotName"].ToString();

                Accepnum.Text = dscsms.Tables[0].Rows[0]["Acceptance_No"].ToString();

                recbags.Text = dscsms.Tables[0].Rows[0]["Recd_Bags"].ToString();

                RecQty.Text = dscsms.Tables[0].Rows[0]["Recd_Qty"].ToString();

                AccepDate.Text = dscsms.Tables[0].Rows[0]["Acceptance_Date"].ToString();

                incsms.Text = "Yes";
            }

            else
            {
                Recdate.Text = "";

                Recdist.Text = "";

                RecIssuecenter.Text = "";

                Accepnum.Text = "";

                recbags.Text = "";

                RecQty.Text = "";

                incsms.Text = "NO";

                AccepDate.Text = "";
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            # endregion


            # region frm Warehouse

            if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }

            string qrywhr = "Select WHR_Id , isnull(convert(nvarchar,Receipt_Date,103),'')Receipt_Date from tbl_Storage_Receipt_Details where issueid = '" + txtId.Text.Trim() + "'";
            SqlCommand cmdwhr = new SqlCommand(qrywhr, cons);

             SqlDataAdapter dawhr = new SqlDataAdapter(cmdwhr);

            DataSet dswhr = new DataSet();

            dawhr.Fill(dswhr);

            if (dswhr.Tables[0].Rows.Count > 0)
            {
                Whrnum.Text = dswhr.Tables[0].Rows[0]["WHR_Id"].ToString();

                WhrDate.Text = dswhr.Tables[0].Rows[0]["Receipt_Date"].ToString();
            }

            else
            {
                Whrnum.Text = "";

                WhrDate.Text = "";
            }

            if (cons.State == ConnectionState.Open)
            {
                cons.Close();
            }

            # endregion

        }

        else
            if (ddlcroptype.SelectedIndex == 1)  // For Paddy
            {
                # region frm Paddy

                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string qry = "select Society.Society_Name + ','+ Society.SocPlace + ',' + Society.Society_Id as Society , convert(nvarchar,IssueToSangrahanaKendra.DateOfIssue,103)IssueDate ,IssueToSangrahanaKendra.TruckChalanNo ,IssueToSangrahanaKendra.Bags , IssueToSangrahanaKendra.QtyTransffer ,ISNULL(IssueCenterReceipt_Online.IssueID ,'')inProcuremnet from Society inner join IssueToSangrahanaKendra on Society.Society_Id = IssueToSangrahanaKendra.PCID left join IssueCenterReceipt_Online on IssueCenterReceipt_Online.IssueID = IssueToSangrahanaKendra.IssueID where IssueToSangrahanaKendra.IssueID = '" + txtId.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(qry, con_paddy);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dispdate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();

                    senproc.Text = ds.Tables[0].Rows[0]["Society"].ToString();

                    inwpms.Text = ds.Tables[0].Rows[0]["inProcuremnet"].ToString();

                    challan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();

                    sendbags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                    sendQty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

                    if (inwpms.Text == "")
                    {
                        inwpms.Text = "NO";
                    }

                    else
                    {
                        inwpms.Text = "YES";
                    }

                }

                else
                {
                    dispdate.Text = "";

                    senproc.Text = "";

                    inwpms.Text = "";

                    challan.Text = "";

                    sendbags.Text = "";

                    sendQty.Text = "";

                    Whrnum.Text = "";

                    Recdate.Text = "";

                    Recdist.Text = "";

                    RecIssuecenter.Text = "";

                    Accepnum.Text = "";

                    recbags.Text = "";

                    RecQty.Text = "";

                    incsms.Text = "";

                    WhrDate.Text = "";

                    AccepDate.Text = "";

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Invalid Issue ID.....'); </script> ");

                    return;
                }


                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }

                # endregion


                # region frm CSMS

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string qrycsms = "Select CONVERT(nvarchar,SCSC_Procurement.Recd_Date,103)RecdDate, pds.districtsmp.district_name,tbl_MetaData_DEPOT.DepotName , SCSC_Procurement.Recd_Bags,SCSC_Procurement.Recd_Qty , Acceptance_Note_Detail.Acceptance_No , ISNULL(convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103),'') Acceptance_Date from SCSC_Procurement inner join pds.districtsmp on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID left join Acceptance_Note_Detail on Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id where SCSC_Procurement.Receipt_Id = '" + txtId.Text.Trim() + "'";

                SqlCommand cmdcmms = new SqlCommand(qrycsms, con);

                SqlDataAdapter dacsms = new SqlDataAdapter(cmdcmms);

                DataSet dscsms = new DataSet();

                dacsms.Fill(dscsms);

                if (dscsms.Tables[0].Rows.Count > 0)
                {
                    Recdate.Text = dscsms.Tables[0].Rows[0]["RecdDate"].ToString();

                    Recdist.Text = dscsms.Tables[0].Rows[0]["district_name"].ToString();

                    RecIssuecenter.Text = dscsms.Tables[0].Rows[0]["DepotName"].ToString();

                    Accepnum.Text = dscsms.Tables[0].Rows[0]["Acceptance_No"].ToString();

                    recbags.Text = dscsms.Tables[0].Rows[0]["Recd_Bags"].ToString();

                    RecQty.Text = dscsms.Tables[0].Rows[0]["Recd_Qty"].ToString();

                    AccepDate.Text = dscsms.Tables[0].Rows[0]["Acceptance_Date"].ToString();

                    incsms.Text = "Yes";
                }

                else
                {
                    Recdate.Text = "";

                    Recdist.Text = "";

                    RecIssuecenter.Text = "";

                    Accepnum.Text = "";

                    recbags.Text = "";

                    RecQty.Text = "";

                    incsms.Text = "NO";

                    AccepDate.Text = "";
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                # endregion


                # region frm Warehouse

                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }

                string qrywhr = "Select WHR_Id , isnull(convert(nvarchar,Receipt_Date,103),'')Receipt_Date from tbl_Storage_Receipt_Details where issueid = '" + txtId.Text.Trim() + "'";
                SqlCommand cmdwhr = new SqlCommand(qrywhr, cons);

                SqlDataAdapter dawhr = new SqlDataAdapter(cmdwhr);

                DataSet dswhr = new DataSet();

                dawhr.Fill(dswhr);

                if (dswhr.Tables[0].Rows.Count > 0)
                {
                    Whrnum.Text = dswhr.Tables[0].Rows[0]["WHR_Id"].ToString();

                    WhrDate.Text = dswhr.Tables[0].Rows[0]["Receipt_Date"].ToString();
                }

                else
                {
                    Whrnum.Text = "";

                    WhrDate.Text = "";
                }

                if (cons.State == ConnectionState.Open)
                {
                    cons.Close();
                }

                # endregion


            }

            else
                if (ddlcroptype.SelectedIndex == 2) // For Coarse Grain
                {
                    # region frm Maize

                    if (con_Maze.State == ConnectionState.Closed)
                    {
                        con_Maze.Open();
                    }

                    string qry = "select Society.Society_Name + ','+ Society.SocPlace + ',' + Society.Society_Id as Society , convert(nvarchar,IssueToSangrahanaKendra.DateOfIssue,103)IssueDate ,IssueToSangrahanaKendra.TruckChalanNo ,IssueToSangrahanaKendra.Bags , IssueToSangrahanaKendra.QtyTransffer ,ISNULL(IssueCenterReceipt_Online.IssueID ,'')inProcuremnet from Society inner join IssueToSangrahanaKendra on Society.Society_Id = IssueToSangrahanaKendra.PCID left join IssueCenterReceipt_Online on IssueCenterReceipt_Online.IssueID = IssueToSangrahanaKendra.IssueID where IssueToSangrahanaKendra.IssueID = '" + txtId.Text.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(qry, con_Maze);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dispdate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();

                        senproc.Text = ds.Tables[0].Rows[0]["Society"].ToString();

                        inwpms.Text = ds.Tables[0].Rows[0]["inProcuremnet"].ToString();

                        challan.Text = ds.Tables[0].Rows[0]["TruckChalanNo"].ToString();

                        sendbags.Text = ds.Tables[0].Rows[0]["Bags"].ToString();

                        sendQty.Text = ds.Tables[0].Rows[0]["QtyTransffer"].ToString();

                        if (inwpms.Text == "")
                        {
                            inwpms.Text = "NO";
                        }

                        else
                        {
                            inwpms.Text = "YES";
                        }

                    }

                    else
                    {
                        dispdate.Text = "";

                        senproc.Text = "";

                        inwpms.Text = "";

                        challan.Text = "";

                        sendbags.Text = "";

                        sendQty.Text = "";

                        Whrnum.Text = "";

                        Recdate.Text = "";

                        Recdist.Text = "";

                        RecIssuecenter.Text = "";

                        Accepnum.Text = "";

                        recbags.Text = "";

                        RecQty.Text = "";

                        incsms.Text = "";

                        WhrDate.Text = "";

                        AccepDate.Text = "";

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Invalid Issue ID.....'); </script> ");

                        return;
                    }


                    if (con_Maze.State == ConnectionState.Open)
                    {
                        con_Maze.Close();
                    }

                    # endregion


                    # region frm CSMS

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string qrycsms = "Select CONVERT(nvarchar,SCSC_Procurement.Recd_Date,103)RecdDate, pds.districtsmp.district_name,tbl_MetaData_DEPOT.DepotName , SCSC_Procurement.Recd_Bags,SCSC_Procurement.Recd_Qty , Acceptance_Note_Detail.Acceptance_No , ISNULL(convert(nvarchar,Acceptance_Note_Detail.Acceptance_Date,103),'') Acceptance_Date from SCSC_Procurement inner join pds.districtsmp on pds.districtsmp.district_code = SCSC_Procurement.Distt_ID inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Procurement.IssueCenter_ID left join Acceptance_Note_Detail on Acceptance_Note_Detail.IssueID = SCSC_Procurement.Receipt_Id where SCSC_Procurement.Receipt_Id = '" + txtId.Text.Trim() + "'";

                    SqlCommand cmdcmms = new SqlCommand(qrycsms, con);

                    SqlDataAdapter dacsms = new SqlDataAdapter(cmdcmms);

                    DataSet dscsms = new DataSet();

                    dacsms.Fill(dscsms);

                    if (dscsms.Tables[0].Rows.Count > 0)
                    {
                        Recdate.Text = dscsms.Tables[0].Rows[0]["RecdDate"].ToString();

                        Recdist.Text = dscsms.Tables[0].Rows[0]["district_name"].ToString();

                        RecIssuecenter.Text = dscsms.Tables[0].Rows[0]["DepotName"].ToString();

                        Accepnum.Text = dscsms.Tables[0].Rows[0]["Acceptance_No"].ToString();

                        recbags.Text = dscsms.Tables[0].Rows[0]["Recd_Bags"].ToString();

                        RecQty.Text = dscsms.Tables[0].Rows[0]["Recd_Qty"].ToString();

                        AccepDate.Text = dscsms.Tables[0].Rows[0]["Acceptance_Date"].ToString();

                        incsms.Text = "Yes";
                    }

                    else
                    {
                        Recdate.Text = "";

                        Recdist.Text = "";

                        RecIssuecenter.Text = "";

                        Accepnum.Text = "";

                        recbags.Text = "";

                        RecQty.Text = "";

                        incsms.Text = "NO";

                        AccepDate.Text = "";
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    # endregion


                    # region frm Warehouse

                    if (cons.State == ConnectionState.Closed)
                    {
                        cons.Open();
                    }

                    string qrywhr = "Select WHR_Id , isnull(convert(nvarchar,Receipt_Date,103),'')Receipt_Date from tbl_Storage_Receipt_Details where issueid = '" + txtId.Text.Trim() + "'";
                    SqlCommand cmdwhr = new SqlCommand(qrywhr, cons);

                    SqlDataAdapter dawhr = new SqlDataAdapter(cmdwhr);

                    DataSet dswhr = new DataSet();

                    dawhr.Fill(dswhr);

                    if (dswhr.Tables[0].Rows.Count > 0)
                    {
                        Whrnum.Text = dswhr.Tables[0].Rows[0]["WHR_Id"].ToString();

                        WhrDate.Text = dswhr.Tables[0].Rows[0]["Receipt_Date"].ToString();
                    }

                    else
                    {
                        Whrnum.Text = "";

                        WhrDate.Text = "";
                    }

                    if (cons.State == ConnectionState.Open)
                    {
                        cons.Close();
                    }

                    # endregion
                }
    }
}
