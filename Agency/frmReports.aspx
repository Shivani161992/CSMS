<%@ Page Language="C#" MasterPageFile="~/MasterPage/AgencyMaster.master" AutoEventWireup="true" CodeFile="frmReports.aspx.cs" Inherits="frmReports" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: lavender; border-right-width: 1pt; border-right-color: black;" >
     <table style="border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black">
        <tr>
            <td align="center" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <strong>Reports</strong></td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 17px;">
                1.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton21" runat="server" PostBackUrl="~/Agency/District_RO_TO_Details.aspx" Font-Size="14px" ForeColor="Navy">Summary Report(District wise) </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 17px;">
                2.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton22" runat="server" PostBackUrl="~/Agency/Latest_Date_Receipt_Disp.aspx" Font-Size="14px" ForeColor="Navy">Summary Report(Issue Center Wise)</asp:LinkButton></td>
        </tr>
        <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 17px;">
                3.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton29" runat="server" PostBackUrl="~/Agency/LoginReportofOperators_District.aspx" Font-Size="14px" ForeColor="Navy">Opeartors Last Login Details of DMMPSCSC</asp:LinkButton></td>
                </tr>
                <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px; width: 17px;">
                4.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                <asp:LinkButton ID="LinkButton28" runat="server" PostBackUrl="~/Agency/LoginReportofOperators_Issue.aspx" Font-Size="14px" ForeColor="Navy">Opeartors Last Login Details of IssueCenters</asp:LinkButton></td>
                </tr>
                 <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 17px;">
                5.</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton30" runat="server" PostBackUrl="~/Agency/RegisteredOperator_rpt.aspx" Font-Size="14px" ForeColor="Navy">Registered Operator Details(Issue Centre)</asp:LinkButton></td>
                </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 17px;">
               6.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton31" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Agency/RegisteredOperator_rpt_DM.aspx">Registered Operator Details(DMMPSCSC)</asp:LinkButton></td>
        </tr>
    </table>
    </div>
</asp:Content>

