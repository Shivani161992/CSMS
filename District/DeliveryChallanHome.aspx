<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DeliveryChallanHome.aspx.cs" Inherits="District_DeliveryChallanHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Delivery Challan</td>
        </tr>

        <tr>
            <td rowspan="5" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="5" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/District/DeliveryChallanNew_ByRack.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> New Delivery Challan</asp:HyperLink>
                    </li>
                        <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Update Delivery Challan</asp:HyperLink>
                    </li>  
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Reprint</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan0" runat="server" NavigateUrl="" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Delivery Challan</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC">&nbsp;</td>
        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

