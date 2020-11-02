<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allocation_N-2_fpswise.aspx.cs" Inherits="allocation_N_2_fpswise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lead Society Allocation FPS-Wise </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid; width: 776px; border-bottom: teal 1px solid">
            <tr>
                <td align="center" style="background-color: #99cccc; height: 27px;">
                    <strong style="font-weight: bold; font-size: 18pt; color: white; background-color: #99cccc">Allocation Of Lead Society FPS-Wise Details</strong></td>
            </tr>
            <tr>
                <td align="center">
        <table style="width: 600px;">
            <tr>
                <td style="width: 19px; background-color: #cccccc;" align="left">
                    Month</td>
                <td style="background-color: #cccccc;" align="left">
                    <asp:DropDownList ID="ddl_allot_month" runat="server" Width="104px" Enabled="False" >
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
                <td style="width: 131px; background-color: #cccccc;" align="left">
                    Year</td>
                <td style="background-color: #cccccc;" align="left">
                    <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="left" style="width: 19px; background-color: #cccccc">
                    Commodity</td>
                <td align="left" style="background-color: #cccccc">
                    <asp:TextBox ID="tx_comm" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Small" Width="96px" ReadOnly="True"></asp:TextBox></td>
                <td align="left" style="width: 131px; background-color: #cccccc">
                    Lead Society</td>
                <td align="left" style="background-color: #cccccc">
                    <asp:TextBox ID="tx_lead" runat="server" Font-Bold="True" Font-Names="Kruti Dev 010"
                        Font-Size="Medium" Width="320px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 237px">
                    <asp:Panel ID="Panel1" runat="server" Width="600px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" GridLines="None" PageSize="15" Width="600px" ForeColor="#404040" ShowFooter="True" >
                            <Columns>
                                <asp:BoundField DataField="fps_code" HeaderText="FPS Code" SortExpression="fps_code" FooterText="Total" >
                                    <FooterStyle Font-Bold="True" Font-Size="Large" ForeColor="#404040" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fps_name" HeaderText="FPS Name" SortExpression="fps_name" FooterText="------&gt;" >
                                    <FooterStyle Font-Bold="True" Font-Size="Large" ForeColor="#404040" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="open_st" HeaderText="Opening Stock" SortExpression="open_st" >
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qty_recev" HeaderText="Received Qty" SortExpression="qty_recev" >
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qty_distr" HeaderText="Distributed Qty" SortExpression="qty_distr" >
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Balance" HeaderText="Stock Balance" SortExpression="Balance" >
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="" LastPageText="" NextPageText="" PreviousPageText="" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" Height="16px" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                        </asp:GridView>
                         
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#804000"
                            Visible="False"></asp:Label></asp:Panel>
                    <asp:Panel ID="Panel2" runat="server">
                    <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../MPSCSC_MODULE/Images/Firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="goto_page" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../MPSCSC_MODULE/images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="goto_page" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../MPSCSC_MODULE/images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="goto_page" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../MPSCSC_MODULE/images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="goto_page" CausesValidation ="false" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Size="Medium"
                        NavigateUrl="~/allocation_N-2_leadwise.aspx" Style="z-index: 100; left: 576px;
                        position: absolute; top: 56px">Back</asp:HyperLink>
                    <asp:LinkButton ID="LinkButton1" runat="server"  Style="z-index: 102;
                        left: 624px; position: absolute; top: 56px" Enabled="False">Print Report</asp:LinkButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
