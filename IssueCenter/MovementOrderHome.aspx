<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="MovementOrderHome.aspx.cs" Inherits="IssueCenter_MovementOrderHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../PaddyMilling/Scripts/jquery-ui.js"></script>

    <%--Script For Help Option Only Set Background--%>
    <link href="../PaddyMilling/Scripts/jquery-ui.css" rel="stylesheet" />

    <script>
        $(function () {
            $(document).tooltip({
                track: true
            });
        });
    </script>



    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To PDS Movement Order</td>
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
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/IssueCenter/DC_MO_DispatchByRack.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delivery Challan</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/IssueCenter/DeliveryChallan_IssuedCentre.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delivery Challan(Issue Centre)</asp:HyperLink>
                    </li>
                     <li>
                        <asp:HyperLink ID="hyplReceiptEntry" runat="server" NavigateUrl="~/IssueCenter/Receipt_Entry_MO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Receipt Entry</asp:HyperLink>
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
                        <asp:HyperLink ID="HyplMvmtPlan0" runat="server" NavigateUrl="~/IssueCenter/RePrint_DO_PDSMO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Delivery Challan</asp:HyperLink>
                    </li>
                     <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/IssueCenter/RePrintReceiptEntry_MO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Receipt Entry</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>


        <tr>
            <td style="background-color: #CCCCCC">&nbsp;</td>
        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>

</asp:Content>

