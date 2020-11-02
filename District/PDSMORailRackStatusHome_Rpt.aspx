<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PDSMORailRackStatusHome_Rpt.aspx.cs" Inherits="District_PDSMORailRackStatusHome_Rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="800px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="3">Reports</td>
                </tr>
                <tr>
                    <td align="left" colspan="2"
                        style="font-weight: 700; text-align: center; font-style: italic">PDS Movement (By Rail Rack)</td>
                            <td align="left" style="text-align: center">
                                <asp:LinkButton ID="bakbtn" runat="server" ForeColor="Navy" PostBackUrl="~/District/FrmReports_Districts.aspx">Back</asp:LinkButton>
                            </td>
                </tr>
                <tr>
                    <td align="left" style="text-align: center">1</td>
                    <td align="left">
                         <asp:LinkButton ID="btnSendDist" runat="server"  ForeColor="Navy" OnClick="btnSendDist_Click" >Sending Rail Rack District</asp:LinkButton></td>
                    <td></td>
                </tr>

                <tr>
                    <td align="left" style="text-align: center">2</td>
                    <td align="left">
                        <asp:LinkButton ID="btnRecdDist" runat="server" ForeColor="Navy" OnClick="btnRecdDist_Click" >Receiving Rail Rack District</asp:LinkButton></td>
                    <td></td>
                </tr>
                


            </table>

        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

