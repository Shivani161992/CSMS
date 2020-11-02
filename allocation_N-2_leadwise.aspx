<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allocation_N-2_leadwise.aspx.cs" Inherits="allocation_N_2_leadwise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lead Society Allocation</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid; width: 784px; border-bottom: teal 1px solid">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 18pt; color: white; background-color: #99cccc; width: 706px;">
                    <strong>Allocation Of Lead Society (
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        )</strong></td>
            </tr>
            <tr>
                <td align="center" style="width: 706px">
                    <table style="border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid;
                        border-bottom: teal 1px solid; width: 808px;">
                        <tr>
                            <td align="left" style="font-weight: bold; background-color: #cccccc">
                                <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" Enabled="False">Print Report</asp:LinkButton></td>
                            <td align="left" style="width: 537px; font-weight: bold; background-color: #cccccc;">
                    Month</td>
                            <td align="left" style="width: 70px; font-weight: bold; background-color: #cccccc;">
                    <asp:DropDownList ID="ddl_allot_month" runat="server" Width="105px">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList></td>
                            <td align="right" style="width: 91px; font-weight: bold; background-color: #cccccc;">
                                &nbsp; &nbsp;&nbsp;
                    Year</td>
                            <td align="left" style="font-weight: bold; width: 91px; background-color: #cccccc">
                    <asp:DropDownList ID="ddd_allot_year" runat="server" Width="88px">
                    </asp:DropDownList></td>
                            <td align="right" style="font-weight: bold; width: 91px; background-color: #cccccc">
                            </td>
                            <td align="right" style="font-weight: bold; background-color: #cccccc" colspan="2">
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Medium"
                                    NavigateUrl="~/District/frmReports.aspx">Back</asp:HyperLink>
                                &nbsp;
                                <asp:LinkButton ID="LinkButton9" runat="server" Font-Bold="False" Font-Size="Medium"
                                    OnClick="LinkButton9_Click">LogOut</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold; height: 19px; background-color: #cccccc">
                            </td>
                            <td align="left" style="width: 537px; font-weight: bold; background-color: #cccccc; height: 19px;">
                                Lead Society</td>
                            <td align="left" colspan="6" style="font-weight: bold; width: 91px; background-color: #cccccc; height: 19px;">
                    <asp:DropDownList ID="ddl_lead" runat="server" AutoPostBack="True" Font-Names="Kruti Dev 010"
                        Font-Size="Medium" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged" Width="296px">
                    </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold; font-size: 11pt; background-color: #cccccc">
                            </td>
                            <td align="left" style="width: 537px; font-weight: bold; background-color: #cccccc; font-size: 11pt;">
                                Commodity</td>
                            <td align="center" style="width: 70px; font-weight: bold; background-color: #cccccc; font-size: 11pt;">
                                Allotment (Qtls.)<br />
                                (1)</td>
                            <td align="center" style="font-weight: bold; font-size: 11pt; width: 91px; background-color: #cccccc">
                                Opening Balance<br />
                                (2)</td>
                            <td align="center" style="font-weight: bold; font-size: 11pt; width: 91px; background-color: #cccccc">
                                Received Quantity<br />
                                (3)</td>
                            <td align="center" style="font-weight: bold; font-size: 11pt; width: 91px; background-color: #cccccc">
                                Distributed Quantity<br />
                                (4)</td>
                            <td align="center" style="font-weight: bold; font-size: 11pt; width: 91px; background-color: #cccccc">
                                Stock Balance<br />
                                (2+3-4)<br />
                                (5)</td>
                            <td align="center" style="width: 91px; font-weight: bold; background-color: #cccccc; font-size: 11pt;">
                                Total Balance<br />
                                (1-5)</td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px">
                                Rice APL</td>
                            <td align="left" style="width: 70px">
                                <asp:TextBox ID="rice_apl_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="rice_apl_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="rice_apl_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="rice_apl_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="rice_apl_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px">
                                <asp:TextBox ID="rice_apl_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px; height: 9px">
                                Rice BPL</td>
                            <td align="left" style="width: 70px; height: 9px">
                                <asp:TextBox ID="rice_bpl_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 9px">
                                <asp:TextBox ID="rice_bpl_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 9px">
                                <asp:TextBox ID="rice_bpl_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 9px">
                                <asp:TextBox ID="rice_bpl_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 9px">
                                <asp:TextBox ID="rice_bpl_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px; height: 9px">
                                <asp:TextBox ID="rice_bpl_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 21px">
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px; height: 21px">
                                Rice AAY</td>
                            <td align="left" style="width: 70px; height: 21px">
                                <asp:TextBox ID="rice_aay_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 21px">
                                <asp:TextBox ID="rice_aay_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 21px">
                                <asp:TextBox ID="rice_aay_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 21px">
                                <asp:TextBox ID="rice_aay_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 21px">
                                <asp:TextBox ID="rice_aay_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px; height: 21px">
                                <asp:TextBox ID="rice_aay_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px">
                                Wheat APL</td>
                            <td align="left" style="width: 70px">
                                <asp:TextBox ID="wheat_apl_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_apl_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_apl_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_apl_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_apl_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px">
                                <asp:TextBox ID="wheat_apl_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px">
                                Wheat BPL</td>
                            <td align="left" style="width: 70px">
                                <asp:TextBox ID="wheat_bpl_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_bpl_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_bpl_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_bpl_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="wheat_bpl_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px">
                                <asp:TextBox ID="wheat_bpl_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 26px">
                                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px; height: 26px;">
                                Wheat AAY</td>
                            <td align="left" style="width: 70px; height: 26px;">
                                <asp:TextBox ID="wheat_aay_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px;">
                                <asp:TextBox ID="wheat_aay_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px">
                                <asp:TextBox ID="wheat_aay_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px">
                                <asp:TextBox ID="wheat_aay_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px">
                                <asp:TextBox ID="wheat_aay_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px; height: 26px;">
                                <asp:TextBox ID="wheat_aay_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 26px">
                                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px; height: 26px;">
                                Sugar</td>
                            <td align="left" style="width: 70px; height: 26px;">
                                <asp:TextBox ID="sugar_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px;">
                                <asp:TextBox ID="sugar_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px">
                                <asp:TextBox ID="sugar_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px">
                                <asp:TextBox ID="sugar_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px; height: 26px">
                                <asp:TextBox ID="sugar_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px; height: 26px;">
                                <asp:TextBox ID="sugar_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click" Width="96px" Enabled="False">Click to Details</asp:LinkButton></td>
                            <td align="left" style="width: 537px">
                                Kerosene</td>
                            <td align="left" style="width: 70px">
                                <asp:TextBox ID="kerosene_allot" runat="server" Width="99px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="kerosene_ope" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="kerosene_rec" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="kerosene_distr" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 113px">
                                <asp:TextBox ID="kerosene_st" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="width: 100px">
                                <asp:TextBox ID="kerosene_bal" runat="server" Width="88px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 537px">
                            </td>
                            <td align="left" style="width: 70px">
                            </td>
                            <td align="left" style="width: 113px">
                            </td>
                            <td align="left" style="width: 113px">
                            </td>
                            <td align="left" style="width: 113px">
                            </td>
                            <td align="left" style="width: 113px">
                            </td>
                            <td align="left" style="width: 100px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        </form>
</body>
</html>
