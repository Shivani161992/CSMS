<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_DeleveryOrder.aspx.cs"
    Inherits="print_DeleveryOrder" Title=" Print Delivery Order " %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delevery Order</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href="../MyCss/menu.css" type="text/css" />

    <script language="javascript" type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=600px,height=800px,toolbar=0,scrollbars=0,status=0');
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
        <a style="cursor: pointer" onclick="javascript:CallPrint('printdiv')">
            <img src="../Images/print.jpg" alt="print" style="width: 47px; height: 34px" /><strong>&nbsp;</strong></a>
        <div id="printdiv">
            <center>
                <table border="2">
                    <tr>
                        <td>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0" width="500px">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="Label2" runat="server" Text="DELIVERY ORDER" ForeColor="maroon" Font-Size="20px"
                                                Font-Underline="TRUE"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="title" runat="server" Text="MADHYA PRADESH STATE CIVIL SUPPLIES CORPORATION LIMITED"
                                                ForeColor="Maroon" Font-Size="17px" Width="600px" Font-Bold="true" Height="15px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td rowspan="3">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 219px">
                                                <tr>
                                                    <td align="left">
                                                        Book No</td>
                                                    <td align="left" style="width: 79px">
                                                        2014/10 B</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Allotment Month</td>
                                                    <td style="width: 79px" align="left">
                                                        <asp:Label ID="lblmonth" runat="server" Text="September"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Allotment Year
                                                    </td>
                                                    <td style="width: 79px" align="left">
                                                        <asp:Label ID="lblyear" runat="server" Text="2009"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        D. O. No.</td>
                                                    <td style="width: 79px" align="left">
                                                        <asp:Label ID="do_no" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                            &nbsp;</td>
                                        <td rowspan="3">
                                            &nbsp; &nbsp; &nbsp;</td>
                                        <td rowspan="3">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/main1_03.jpg" />
                                        </td>
                                        <td rowspan="3" style="color: white">
                                            &nbsp; &nbsp; &nbsp;** &nbsp;&nbsp; &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" style="width: 170px">
                                                <tr>
                                                    <td align="left" style="height: 19px">
                                                        District</td>
                                                    <td style="height: 19px" align="center">
                                                        <asp:Label ID="lbldist" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Date Of Issue</td>
                                                    <td align="center">
                                                        <asp:Label ID="lblissudt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Validity Date</td>
                                                    <td align="center">
                                                        <asp:Label ID="lblvaliddt" runat="server" Text=" " Font-Bold="true"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                &nbsp;</center>
                            <center>
                                <table style="width: 640px">
                                    <tr>
                                        <td align="left" style="width: 295px">
                                            Please deliver to M/s.</td>
                                        <td colspan="4" style="text-align: left" rowspan="2">
                                            <asp:Label ID="lblisname" runat="server" Font-Bold="True" Width="200px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 295px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px; width: 295px;">
                                            F.P. Shop dealer /Bulk consumer No.</td>
                                        <td style="height: 21px; width: 362px; text-align: left;" colspan="3">
                                            holding
                                            permit No.</td>
                                        <td colspan="1" style="height: 21px; text-align: left;">
                                            <asp:Label ID="lblper" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5">
                                            Date:-<asp:Label ID="lblrodate" runat="server" Text="" Font-Bold="true"> </asp:Label>&nbsp;
                                            the following quantity&nbsp; of&nbsp; food grain in accordance with
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="left">
                                            the usual term and condition governing the sale of food grain from the</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: left; height: 21px;">
                                            corporation's&nbsp; Stock for Amount of INR&nbsp; &nbsp;<asp:Label ID="lbltotamount" runat="server" Width="126px"></asp:Label></td>
                                        <td colspan="1" style="height: 21px">
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="1" style="width: 583px">
                                    <tr>
                                        <td style="width: 570px;">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="640px"
                                                FooterStyle-ForeColor="green" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="fps_name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfps" runat="server" Text='<%# Eval("fps_name").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <HeaderStyle CssClass="gridtotsize"  Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    
                                                              <%--<asp:BoundField DataField="fps_name" HeaderText="fps_name"
                                                        ReadOnly="True" SortExpression="fps_name" >
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                  
                                                    </asp:BoundField>  --%>                             
                                                   
                                                    
                                                    <asp:BoundField DataField="commodity_name" HeaderText="Discription of Food grains/Sugar/Commidity"
                                                        ReadOnly="True" SortExpression="commodity_name" FooterText="Total">
                                                        <HeaderStyle CssClass="gridheader" Font-Size="10pt" />
                                                  
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="scheme_name" HeaderText="Sheme" ReadOnly="True" SortExpression="scheme_name">
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Quntity  to be Issued (in Qtls)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("quantity").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                           <%-- <asp:Label ID="lblTotalQty" runat="server" Font-Size="10pt"></asp:Label>--%>
                                                        
                                                        </FooterTemplate>
                                                        <HeaderStyle CssClass="gridtotsize"  Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="rate_per_qtls" HeaderText="Rate per Quantal(Rs.)" ReadOnly="True"
                                                        SortExpression="rate_per_qtls">
                                                        <HeaderStyle CssClass="gridtotsize" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Amount(Rs.)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Totalamt").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                           <%-- <asp:Label ID="lblTotal" runat="server" Font-Size="10pt"></asp:Label>--%>
                                                        </FooterTemplate>
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    
                                             <%--        <asp:TemplateField HeaderText="Paid Amount(in Rs)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Eval("amount").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPaidTotal" runat="server" Font-Size="10pt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle CssClass="gridtotsize" Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>--%>
                                                    
                                                </Columns>
                                                <FooterStyle ForeColor="Green" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 570px; height: 21px">
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table style="width: 642px">
                                    <tr>
                                        <td colspan="4" style="height: 21px" align="left">
                                            The price in respect of this Delivery Order has been received vide Demand Draft
                                            No.<asp:Label ID="lbldraft" runat="server" Text="......" Width="53px"></asp:Label>
                                            Amount INR
                                            <asp:Label ID="lbldepositamt" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px" align="left">
                                            District &nbsp;
                                            <asp:Label ID="lbldd_district" runat="server" Text="Label"></asp:Label>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;Dated. &nbsp;&nbsp;
                                            <asp:Label ID="lbldt" runat="server" Text="......" Width="66px"></asp:Label></td>
                                        <td style="height: 21px" colspan="3">
                                            Issued by
                                            <asp:Label ID="lblbname" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                &nbsp;</center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="height: 92px">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td align="left" style="height: 21px">
                                                            To,</td>
                                                        <td align="left" style="height: 21px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 21px">
                                                            The Godown Incharge</td>
                                                        <td style="color: white; height: 21px; background-color: white">
                                                            *********************</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblgodn" runat="server" Text=".........."></asp:Label>
                                                            Godown</td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td style="height: 92px">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            District Manager/Authorised Signatory</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Madhya Pradesh State Civil Supplies</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            Corporation Limited</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: white">
                                            ********************</td>
                                        <td style="color: white">
                                            ********************</td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table>
                                    <tr>
                                        <td>
                                            ................FOR USE AT THE GODOWN</td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="height: 92px">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td align="left" style="height: 21px">
                                                            Stock issued on.......</td>
                                                        <td align="left" style="height: 21px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 21px">
                                                            ................</td>
                                                        <td style="color: white; height: 21px; background-color: white">
                                                            ******************</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            Signature Of Godown Incharge
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td style="height: 92px">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        Received the stocks desribed in the
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            released order in good condition</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            (..............................)</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            Signature Of Purchaser</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: white">
                                            *****************</td>
                                        <td style="color: white">
                                            *****************</td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                &nbsp;</center>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
