<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printRpt_N-2_fpswise_alloc.aspx.cs" Inherits="printRpt_N_2_fpswise_alloc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report N_2 Allocation FPS-Wise</title>
    <script language="javascript"  type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();		
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('print_alloc')"> <img src="Images/Printer-50x50.png"  alt ="print" /><strong >&nbsp;</strong></a>
 
    <div id ="print_alloc">
    <div  style="width: 7in; height: 9.69in; text-align: center; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black;">
        <table style="width: 7in">
            <tr>
                <td align="center" colspan="5">
                    <strong style="font-size: 14pt">N-2 Allocation Report FPS-Wise&nbsp; District -
                        <asp:Label ID="lbl_dist" runat="server" Font-Bold="True"></asp:Label></strong></td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 98px; height: 21px;">
                    Current
                    Month</td>
                <td align="left" colspan="2" style="height: 21px">
                    <asp:Label ID="lbl_cmonth" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left" style="width: 116px; height: 21px;">
                    N-2 Alloc Month</td>
                <td align="left" style="width: 222px; height: 21px;">
                                <asp:Label ID="lbl_month" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="width: 98px">
                    Current Year</td>
                <td align="left" style="width: 129px">
                    <asp:Label ID="lbl_cyear" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left" style="width: 91px">
                </td>
                <td align="left" style="width: 116px">
                    N-2 Alloc Year</td>
                <td align="left" style="width: 222px">
                                <asp:Label ID="lbl_year" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="width: 98px">
                                Commodity</td>
                <td align="left" style="width: 129px">
                    <asp:Label ID="tx_comm" runat="server" Font-Bold="True"></asp:Label></td>
                <td align="left" style="width: 91px">
                                Lead Society</td>
                <td align="left" colspan="2">
                                <asp:Label ID="lbl_lead" runat="server" Font-Bold="True" Font-Names="Kruti Dev 010" Font-Size="Medium"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="font-size: 10pt;" colspan="5">
                    Stock Balance = Opening Stock + Received Quantity - Distributed Quantity</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Panel ID="Panel1" runat="server" Width="6.95in">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            Font-Names="Arial Narrow" Font-Size="Smaller"
                            PageSize="15" ShowFooter="True" Width="6.95in" Font-Bold="False">
                            <Columns>
                                <asp:BoundField DataField="fps_name" FooterText="Total                            ------&gt;" HeaderText="Fair Price Shop Name"
                                    SortExpression="fps_name">
                                    <FooterStyle Font-Bold="True" Font-Size="Small" ForeColor="#404040" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" ForeColor="Black" Height="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="open_st" HeaderText="Opening Stock" SortExpression="open_st">
                                    <FooterStyle Font-Bold="True" Font-Size="Small" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Height="10px" Width="65px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qty_recev" HeaderText="Received Qty" SortExpression="qty_recev">
                                    <FooterStyle Font-Bold="True" Font-Size="Small" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Height="10px" Width="65px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qty_distr" HeaderText="Distributed Qty" SortExpression="qty_distr">
                                    <FooterStyle Font-Bold="True" Font-Size="Small" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Height="10px" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Balance" HeaderText="Stock Balance" SortExpression="Balance">
                                    <FooterStyle Font-Bold="True" Font-Size="Small" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Height="10px" Width="65px" />
                                </asp:BoundField>
                            </Columns>
                            <PagerSettings FirstPageText="" LastPageText="" NextPageText=""
                                PreviousPageText="" />
                            <FooterStyle Height="16px" Font-Bold="False" Font-Size="Small" />
                            <HeaderStyle Font-Bold="False" Font-Size="Small" Font-Strikeout="False" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
