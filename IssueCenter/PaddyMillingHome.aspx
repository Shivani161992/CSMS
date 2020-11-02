<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="PaddyMillingHome.aspx.cs" Inherits="IssueCenter_PaddyMillingHome" %>

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
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Paddy Milling</td>
        </tr>

        <tr>
            <td rowspan="7" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="7" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/PaddyMilling/Paddy_Challan.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Challan</asp:HyperLink>
                    </li>

                    <li runat="server" id="li_Recpt">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/IssueCenter/Receipt_Entry_CMR.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Receipt Entry CMR</asp:HyperLink>
                    </li>
                      <li runat="server" id="li4">
                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/PaddyMilling/CMR_Stacknumber_Update.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Receipt Entry CMR Stack Update</asp:HyperLink>
                    </li>

                     <li runat="server" id="li3">
                        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/IssueCenter/PM_Rem_CMRDetails_Receipt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Remaining CMR Receipt Entry </asp:HyperLink>
                    </li>
                    <li runat="server" id="li_Ins">
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/IssueCenter/PM_Inspection_Team.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Quality Inspection By Team (Rice/Wheat)</asp:HyperLink>
                    </li>

                  
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">CMR Stack Operations</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">

                     <%-- <li runat="server" id="li_Ins_ByOne">
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/IssueCenter/PM_Inspection_ByOneMember.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Quality Inspection (Rice/Wheat)</asp:HyperLink>
                    </li>--%>
                    <li runat="server" id="li1">
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/IssueCenter/Stack_Rejection.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> CMR Stack <a style="font-size:18px; color:green;">(Accept/Reject) </a></asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PaddyMilling/CMRRejection_Challan.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> CMR Rejection Challan</asp:HyperLink><span style="font-size: large;"></span>
                    </li>

<%--                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PaddyMilling/RcptEntry_RejCMR.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Receipt Entry For Rejected CMR</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>
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
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/IssueCenter/Reprint_Dhan_Challan.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Paddy Challan</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                    <li runat="server" id="li_ReRecpt">
                        <asp:HyperLink ID="HyplMvmtPlan0" runat="server" NavigateUrl="~/IssueCenter/Reprint_CMR_Acpt_Reject.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Receipt Entry CMR</asp:HyperLink>
                    </li>

                     <li runat="server" id="li5">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/IssueCenter/RePrint_StackRejection.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint CMR Stack <a style="font-size:18px; color:green;">(Accept/Reject)</asp:HyperLink>
                    </li>
                  
                      <li runat="server" id="li6">
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/IssueCenter/Reprint_CMRStackRejection.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint CMR Rejection Delivery Challan</asp:HyperLink>
                    </li>
                  



                    <li runat="server" id="RCMRDO">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/PaddyMilling/Reprint_CMRDO_IC.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint CMR Deposit Order</asp:HyperLink><span style="font-size: large;"><strong>&nbsp;(Mapping)</strong></span>
                    </li>
                     <li runat="server" id="Li2">
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/IssueCenter/Reprint_Print_Insp_ByOneMember_paddy.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Inspection By One Member(Paddy)</asp:HyperLink><span style="font-size: large;"><strong>&nbsp;</strong></span>
                    </li>
                </ul>
            </td>
        </tr>

        <tr>
            <td style="background-color: #CCCCCC"></td>
        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>

</asp:Content>

