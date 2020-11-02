<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Markfed_PDY.master" AutoEventWireup="true" CodeFile="FrmReport_State_mfd.aspx.cs" Inherits="State_FrmReport_State_mfd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="3">Reports</td>
                </tr>



                <tr>
                    <td align="left">1</td>
                    <td align="left">
                        <asp:LinkButton ID="lnkaccepnote" runat="server" ForeColor="Navy"
                            PostBackUrl="~/State/FrmPaddyMillingRpt_mfd.aspx">धान मिलिंग रिपोर्टस</asp:LinkButton>

                    </td>
                    <td>&nbsp;</td>
                </tr>


            </table>

        </asp:Panel>
    </div>
</asp:Content>

