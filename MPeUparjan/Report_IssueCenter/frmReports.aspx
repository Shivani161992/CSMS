<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="frmReports.aspx.cs" Inherits="frmReports" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: #cfdcc8; border-right-width: 1pt; border-right-color: black;" >
    <table style="border-right: navy 1pt solid; border-top: navy 1pt solid; border-left: navy 1pt solid; border-bottom: navy 1pt solid;">
        <tr>
            <td align="center" style="background-color: #cccccc; width: 15px;">
            </td>
            <td align="center" style="background-color: #cccccc;">
                Reports</td>
            <td style="background-color: #cccccc">
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
                1.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="form13" runat="server" PostBackUrl="~/Report_IssueCenter/delivery_order_rpt.aspx" ForeColor="Navy" >Delivery Order Report </asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
                2.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Report_IssueCenter/issueagainst_do_rpt.aspx" ForeColor="Navy" >Issue Against DO  Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
                3.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Report_IssueCenter/Form13A.aspx" ForeColor="Navy">Daily Stock Transfer Information(Form-13(A))</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
               4.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Report_IssueCenter/Daily_Receipt_Register.aspx" ForeColor="Navy">Daily Receipt Register</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
               5.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Report_IssueCenter/Daily_Dispatched.aspx" ForeColor="Navy">Daily Dispatch Details</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
               6.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Report_IssueCenter/Current_Balance.aspx" ForeColor="Navy">Current Balance of Stock</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
                7.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/Report_IssueCenter/multiple_DO_issueCentrewise_rpt.aspx" ForeColor="Navy">Delivery Order Details Commodity/Scheme-Wise </asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
                8.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/Report_IssueCenter/ACNO_details.aspx" ForeColor="Navy">Accepence Note Details</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 15px;">
                9.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/Report_IssueCenter/Scheme_Transfer_rpt.aspx" ForeColor="Navy">Scheme Transfer Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">
                10.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/DailyEntryIssueCenterWise.aspx">Daily Entry IssuecenterWise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
     <tr>
            <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">
                11.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnktotsummary" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/TotalSummary.aspx">Total Summary</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">
                12.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkrack_commodity" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/IC_RackReport_commwise.aspx">By Rack Dispatch Detail Commodity Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">
                13.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkrpaddispatch" runat="server" ForeColor="Navy" PostBackUrl="~/Report_IssueCenter/IC_RoadDispatch_commwise.aspx">By Road Dispatch Detail Commodity Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 15px; position: static; background-color: #cfdcdc">
                14.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton10" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Report_IssueCenter/IC_SupplyOrder.aspx">Total Summary for Tender Purchase by Road(Sugar Supply Order)</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
    </table>
    </div>
</asp:Content>

