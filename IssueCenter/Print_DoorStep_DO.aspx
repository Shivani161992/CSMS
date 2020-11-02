<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_DoorStep_DO.aspx.cs" Inherits="IssueCenter_Print_DoorStep_DO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print DO for Door Step</title>
    
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
                <table border="2" style="width: 712px">
                    <tr>
                        <td>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="0" width="550px">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Door Step Delivery Order" ForeColor="maroon" Font-Size="20px"
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
                                        <td rowspan="3" style="text-align: left; height: 95px;">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 219px">
                                                <tr>
                                                    <td align="left" style="height: 19px">
                                                        Book No</td>
                                                    <td align="left" style="width: 79px; height: 19px;">
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
                                            Issue Center 
                                            <asp:Label ID="ICName" runat="server" ForeColor="#0000C0" Width="126px"></asp:Label>
                                            &nbsp;</td>
                                        <td rowspan="3" style="width: 12px; height: 95px;">
                                            &nbsp; &nbsp; &nbsp;</td>
                                        <td rowspan="3" style="width: 4px; height: 95px;">
                                            &nbsp;</td>
                                        <td style="height: 95px">
                                            <table cellpadding="0" cellspacing="0" style="width: 170px">
                                                <tr>
                                                    <td align="left" style="height: 19px; width: 96px;">
                                                        District</td>
                                                    <td style="height: 19px" align="center">
                                                        <asp:Label ID="lbldist" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 96px; height: 19px">
                                                        DO Date</td>
                                                    <td align="center" style="height: 19px">
                                                        <asp:Label ID="lblissudt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 96px">
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
                                <table style="width: 670px">
                                    <tr>
                                        <td align="left" colspan="5" rowspan="2">
                                            Please deliver to M/s.<asp:Label ID="lblisname" runat="server" Font-Bold="False" Width="400px" ForeColor="#000099"></asp:Label></td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5" rowspan="1">
                                            Transporter Name
                                            <asp:Label ID="transName" runat="server" Font-Bold="False" ForeColor="#000099"></asp:Label>
                                            F.P. Shop dealer&nbsp; Bulk consumer No</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px; text-align: left;" colspan="5">
                                            holding
                                            permit No.<asp:Label ID="lblper" runat="server"></asp:Label>
                                            &nbsp;Date:-<asp:Label ID="lblrodate" runat="server" Font-Bold="True" Width="95px"></asp:Label>&nbsp;
                                            the following quantity&nbsp; of&nbsp; food grain in accordance with</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="5">
                                            &nbsp;the usual term and condition governing&nbsp; the sale of food grain from the
                                            corporation's&nbsp; Stock.</td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                <table cellpadding="0" cellspacing="0" border="1" style="width: 583px">
                                    <tr>
                                        <td style="width: 570px;">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="670px"
                                                FooterStyle-ForeColor="green" ShowFooter="True" >
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="fps_name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfps" runat="server" Text='<%# Eval("fps_name").ToString()%>' Font-Size="10pt">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <HeaderStyle CssClass="gridtotsize"  Font-Size="10pt"/>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                   
                                                    
                                                    <asp:BoundField DataField="commodity_name" HeaderText="Discription of Food grains/Sugar/Commidity"
                                                        ReadOnly="True" SortExpression="commodity_name">
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
                                <table style="width: 670px">
                                    <tr>
                                        <td colspan="4" style="height: 21px" align="left">
                                            TheQuantity in respect of this Delivery Order has been Issued vide Payemnt Option
                                            <asp:Label ID="lblpayoption" runat="server" ForeColor="#0000C0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="height: 21px">
                                            for Amount of INR&nbsp; <asp:Label ID="lbltotamount" runat="server" ForeColor="#0000C0"></asp:Label>
                                            DD Number&nbsp;
                                            <asp:Label ID="ddNum" runat="server" ForeColor="#0000C0" Width="130px"></asp:Label>
                                            DD Amount
                                            <asp:Label ID="ddamt" runat="server" ForeColor="#0000C0" Width="130px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px" align="left" colspan="4">
                                            District &nbsp;
                                            <asp:Label ID="lbldd_district" runat="server" Text="Label" Width="100px"></asp:Label>
                                            &nbsp; Dated.&nbsp;
                                            <asp:Label ID="lbldt" runat="server" Width="66px"></asp:Label>&nbsp;
                                            Issued by Bank &nbsp;<asp:Label ID="lblbname" runat="server"></asp:Label></td>
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
