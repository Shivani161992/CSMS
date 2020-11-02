<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PaddyMillingHome.aspx.cs" Inherits="State_PaddyMillingHome" %>

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

    <table align="center" style="width: 620px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Paddy Milling</td>
        </tr>


        <tr>
            <td rowspan="9" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC"></td>
            <td rowspan="9" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PaddyMilling/PM_DO_AddGodown.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Paddy Godown</asp:HyperLink>
                        &nbsp;<strong>(Mapping)</strong></li>

                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PaddyMilling/PM_DOUpdate_Shortage.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Paddy Milling Delivery Order</asp:HyperLink>
                       
                        
                         &nbsp;<strong>(Shortage)</strong></li>

                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/PM_CMR_DO_Godown_Update.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update CMR Deposit Order</asp:HyperLink>
                       
                        
                         &nbsp;</li>







                     <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/State/PM_BlackListedMiller.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">BlackListed Miller </asp:HyperLink>
                       
                        
                         &nbsp;</li>
                    <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/State/Delete_PM_Insp_ByOneMember.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete PM Inspection By One Member </asp:HyperLink>
                       
                        
                         &nbsp;</li>
                                          <li>
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/State/PM_Ratio_Master.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Milling Ratio Master</asp:HyperLink>
                       
                        
                         &nbsp;</li>
                      <li>
                                <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/PaddyMilling/Update_MillerRegistration_OtherState.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Miller Registration (Other State)</asp:HyperLink>
                                &nbsp;</li>
                                         


                        <li>
                                <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/PaddyMilling/AcceptRegistration_OtherState.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Approve Miller Registration (Other State)</asp:HyperLink>
                                &nbsp;</li>
                                         

                                           <%-- <li> <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PaddyMilling/PaddyMillingCropYear.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Master</asp:HyperLink></li>%>


                </ul>
            </td>
        </tr>

<%--        <tr runat="server" id="DRCMRDO">
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Delete</td>
        </tr>
        <tr runat="server" id="DCMRDO">
            <td>
                <ul style="color: blue;">--%>

                    <%-- <li>
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/PaddyMilling/PM_Agrmt_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Active/ Deactive/ Delete Miller Agreement</asp:HyperLink>
                    </li>--%>
                    <%--<li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/PM_DO_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Paddy Milling Delivery Order</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/PaddyMilling/PM_Challan_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Paddy Challan</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/PaddyMilling/CMR_DO_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete CMR Deposit Order</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PaddyMilling/ReceiptCMR_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Receipt Entry CMR</asp:HyperLink>
                    </li>

                </ul>
            </td>
        </tr>--%>


        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Mapping</td>
        </tr>

        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/PaddyMilling/PDYDist_OtherDist_MAP.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy District To Paddy Other District</asp:HyperLink>
                        &nbsp;<strong>(Godown Mapping)</strong>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/PaddyMilling/CMRDO_OtherDist.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">CMR Deposit Order</asp:HyperLink>
                        &nbsp;<strong>(Other District Mapping)</strong>
                    </li>
                </ul>
            </td>
        </tr>

        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Bill Generate</td>
        </tr>
<tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/State/Master_StandingCommitee_TransRates.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Standard Committee Transporter Rates </asp:HyperLink>
                       
                    </li>
                   
                </ul>
            </td>
        </tr>
                    <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">FDR Update</td>
        </tr>
<tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/State/ExpiredFDR_Updation.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Updating Expired FDR </asp:HyperLink>
                       
                    </li>
                   
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC"></td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">

            </td>
        </tr>
                    </td>
            </tr>
        
    </table>
</asp:Content>

