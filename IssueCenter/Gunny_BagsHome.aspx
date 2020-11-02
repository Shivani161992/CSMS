<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Gunny_BagsHome.aspx.cs" Inherits="IssueCenter_Gunny_BagsHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
        .ButtonClass
        {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Gunny Bags Home</td>
        </tr>

        <tr>
            <td rowspan="15" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="15" style="width: 2px">&nbsp;</td>
        </tr>

        <tr>
            <td>
                <ul style="color: blue;">
                 <%--    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/GunnyBags/GunnyBags_DeliveryChallan.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Delivery Challan</asp:HyperLink>
                    </li>--%>


                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/GunnyBags/Gunny_Bags_Receipt_Entry.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Receipt Entry (Godown)</asp:HyperLink>
                    </li>
                      <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/GunnyBags/GunnyBags_DeliveryChallan_Society.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Delivery Challan (Society)</asp:HyperLink>
                    </li>

                   
                </ul>
            </td>
        </tr>



        <%--
        <tr runat="server" id="DRCMRDO">
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Delete Request</td>
        </tr>
        <tr runat="server" id="DCMRDO">
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/DelReq_CMR_DO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Request For CMR Deposit Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                </ul>
            </td>
        </tr>--%>

        <%--        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Milling Bill</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/PaddyMilling/PM_TranspRs_Distance.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Transportation Lead Distance</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                </ul>

            </td>
        </tr>--%>



        <tr>
            <td style="background-color: #CCCCCC"></td>
        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>
</asp:Content>

