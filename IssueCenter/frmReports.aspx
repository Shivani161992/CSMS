<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="frmReports.aspx.cs" Inherits="frmReports" Title="Report Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        function TABLE1_onclick() {

        }

        // ]]>
    </script>

    <div style="width: 500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: #cfdcc8; border-right-width: 1pt; border-right-color: black;">
        <table style="border-right: navy 1pt solid; border-top: navy 1pt solid; border-left: navy 1pt solid; border-bottom: navy 1pt solid;" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="center" style="background-color: #cccccc; width: 15px;"></td>
                <td align="center" style="background-color: #cccccc;">Reports</td>
                <td style="background-color: #cccccc"></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">1.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="form13" runat="server" PostBackUrl="~/Report_IssueCenter/delivery_order_rpt.aspx" ForeColor="Navy">Delivery Order Report </asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">2.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Report_IssueCenter/issueagainst_do_rpt.aspx" ForeColor="Navy">Issue Against DO  Report</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">3.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Report_IssueCenter/Form13A.aspx" ForeColor="Navy">Daily Stock Transfer Information(Form-13(A))</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">4.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Report_IssueCenter/AllReceipt_Register.aspx" ForeColor="Navy">Daily Receipt Register</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">5.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Report_IssueCenter/Daily_Dispatched.aspx" ForeColor="Navy">Daily Dispatch Details</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">6.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/Report_IssueCenter/multiple_DO_issueCentrewise_rpt.aspx" ForeColor="Navy">Delivery Order Details Commodity/Scheme-Wise </asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">7</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/Report_IssueCenter/Scheme_Transfer_rpt.aspx" ForeColor="Navy">Scheme Transfer Report</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">8.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/DailyEntryIssueCenterWise.aspx">Daily Entry IssuecenterWise</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">9.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnktotsummary" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/TotalSummary.aspx">Total Summary</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">10.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkrack_commodity" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/IC_RackReport_commwise.aspx">By Rack Dispatch Detail Commodity Wise</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">11.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkrpaddispatch" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/IC_RoadDispatch_commwise.aspx">By Road Dispatch Detail Commodity Wise</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">12.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/IC_SupplyOrder.aspx">Total Summary for Tender Purchase by Road-Sugar/Salt</asp:LinkButton></td>
                <td></td>
            </tr>


            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">&nbsp;</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left"
                    style="font-size: 10pt; position: static; background-color: #cfdcdc; text-align: center;"
                    colspan="2">
                    <b>Procurement Reports</b></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">a.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                    <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="~/Report_IssueCenter/DailyReceipt_Register.aspx" ForeColor="Navy">Receipt Procurement 2016</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/Report_IssueCenter/ACNO_details.aspx" ForeColor="Navy">Accepence Note Details</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">2</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/rpt_Societywisestorage.aspx">Society wise Deposit Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">3</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkDelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/RptIC_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">4</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnktruck_rejected" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/Rpt_RejectedTruck.aspx">Rejected Truck Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">5</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkstencile" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/Stencile_Acceptance.aspx">Stencile & Steching Bags</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">6</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkstencile0" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/DetailProc.aspx">Detail between two dates</asp:LinkButton></td>
                <td>&nbsp;</td>

            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">7</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/Gdnwise_ReceiptProc.aspx">Godown Wise Receipt</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">8</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnlfaq" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/Faq_NonFaq.aspx">Wheat FAQ and URS Receiving details</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">9</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                    <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/PaddyProcHome2016_RptIC.aspx">Paddy Procurement Reports 2016</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>


            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">&nbsp;</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left"
                    style="font-size: 10pt; position: static; background-color: #cfdcdc; font-style: italic; text-align: center; font-weight: 700;"
                    colspan="2">DoorStep Reports</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc; height: 21px;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 21px;">
                    <asp:LinkButton ID="lnkDpy" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/DPY_DOWise.aspx">Door Step Do Wise</asp:LinkButton></td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc"></td>
                <td style="height: 21px"></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; height: 21px; background-color: #cfdcdc">2</td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkdpy_cons" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/DPY_Consolidated.aspx">Door Step Details</asp:LinkButton></td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc"></td>
                <td style="height: 21px"></td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; height: 21px; background-color: #cfdcdc">3</td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnkdpy_salesheet" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/frm_DailySalesheet.aspx">Daily Sale sheet Report</asp:LinkButton></td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">&nbsp;</td>
                <td style="height: 21px">&nbsp;</td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; height: 21px; background-color: #cfdcdc">4</td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnktransportorder" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/frm_transportorderwise_detail.aspx">Transport Order Detail FPSwise Report </asp:LinkButton></td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">&nbsp;</td>
                <td style="height: 21px">&nbsp;</td>
            </tr>



            <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; height: 21px; background-color: #cfdcdc">5.</td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">
                    <asp:LinkButton ID="lnktransportorder0" runat="server" ForeColor="Navy"
                        PostBackUrl="~/Report_IssueCenter/DPY_DCandIssuedDO_Detail.aspx">DC and Issued DO fpswise Detail</asp:LinkButton></td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc">&nbsp;</td>
                <td style="height: 21px">&nbsp;</td>
            </tr>

             <tr>
                <td align="left"
                    style="font-size: 10pt; position: static; background-color: #cfdcdc; font-style: italic; text-align: center; font-weight: 700;"
                    colspan="2">Paddy Milling Reports </td>
                <td>&nbsp;</td>
            </tr>

             <tr>
                <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc; height: 21px;">1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 21px;">
                    <asp:LinkButton ID="LinkButton14" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Report_By_One_Member_IC.aspx">Paddy Milling Inspection report By One Member</asp:LinkButton></td>
                <td align="left" style="font-size: 10pt; position: static; height: 21px; background-color: #cfdcdc"></td>
                <td style="height: 21px"></td>
            </tr>

        </table>
    </div>
</asp:Content>

