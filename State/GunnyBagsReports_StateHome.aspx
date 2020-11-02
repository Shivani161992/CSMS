<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="GunnyBagsReports_StateHome.aspx.cs" Inherits="State_GunnyBagsReports_StateHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="2">Gunny Bags Reports</td>
                </tr>

                <%--     <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton39" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy Milling Status Report</asp:LinkButton></td>
                    <td align="left" style="font-weight: bold;">

                        <asp:LinkButton ID="linkbtn1" runat="server" PostBackUrl="~/State/NewFrmReport_State.aspx">Back</asp:LinkButton>
                    </td>

                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="Linkmilleragree" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/rpt_miller_info.aspx">Miller Agreement Report</asp:LinkButton></td>
                </tr>--%>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/GunnyBags_IndentCreation_Report.aspx">Gunny Bags Indent Report</asp:LinkButton>(CropYear Wise)</td>
                </tr>
               
                
            </table>
        </asp:Panel>
    </div>
</asp:Content>

