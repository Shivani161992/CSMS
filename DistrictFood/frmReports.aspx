<%@ Page Language="C#" MasterPageFile="~/MasterPage/DF_MPSCSC.master" AutoEventWireup="true" CodeFile="frmReports.aspx.cs" Inherits="frmReports" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: lavender; border-right-width: 1pt; border-right-color: black;" >
    <table style="border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black">
        <tr>
            <td align="center" style="font-weight: bold; font-size: 15pt; color: teal">
            </td>
            <td align="center" style="font-weight: bold; font-size: 15pt; color: teal">
                Reports</td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 21px">
                1.</td>
            <td align="left" style="height: 21px">
                <asp:LinkButton ID="btn_multi_do" runat="server" PostBackUrl="~/District_food_rpt/No_of_DO_rpt.aspx">Multiple Delivery Order Report District-Wise</asp:LinkButton></td>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 20px">
                2.</td>
            <td align="left" style="height: 20px">
                <asp:LinkButton ID="btn_multi_issue_do" runat="server" PostBackUrl="~/District_food_rpt/No_of_IssedDO_rpt.aspx">Multiple Issue Against Delivery Order Report District-Wise</asp:LinkButton></td>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td align="left">
                3.</td>
            <td align="left">
                <asp:LinkButton ID="multiDo_issueCentrewise" runat="server" PostBackUrl="~/District_food_rpt/multiple_DO_issueCentrewise_rpt.aspx">Multiple Delivery Order Report IssueCentre-Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                4.</td>
            <td align="left">
                <asp:LinkButton ID="multiple_issuedDo_issuewise" runat="server" PostBackUrl="~/District_food_rpt/multiple_IssuedDO_issueCentrewise_rpt.aspx">Multiple Issue Against Delivery Order Report IssueCentre-Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left">
                5.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/District_food_rpt/liftingDetails_DO_rpt.aspx">Lifted Do Details Against Delivery Order Report District-Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                6.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/District_food_rpt/Details_DO_comm_rpt.aspx">Delivery Order Report FPS/Commodity/Scheme-Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
      
        <tr>
            <td align="left">
                7.</td>
            <td align="left">
                <asp:LinkButton ID="n_2_alloc" runat="server" PostBackUrl="~/District_food_rpt/allocation_N-2_leadwise.aspx">N-2 Allocation of Lead Society Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                8.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/District_food_rpt/TOrpt.aspx">Details of Transport Order</asp:LinkButton></td>
            <td>
            </td>
        </tr>
          <tr>
              <td align="left">
                9.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/District_food_rpt/Truck_Move_rpt.aspx">Truck Movement Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
               10.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/District_food_rpt/Release_Order_rpt.aspx">Release Order Register</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                11.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/District_food_rpt/Stock_Register_rpt.aspx">Stock Register</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                  12.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/District_food_rpt/Current_Balance_rpt.aspx">Current Balance Of Stock</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                13.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/District_food_rpt/Statement_Transport.aspx">Statement Of Transportation </asp:LinkButton></td>
            <td>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

