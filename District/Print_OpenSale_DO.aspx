<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_OpenSale_DO.aspx.cs" Inherits="District_Print_OpenSale_DO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Open Sale DO</title>
    
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
                                                        </td>
                                                    <td style="width: 79px" align="left">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                    </td>
                                                    <td style="width: 79px" align="left">
                                                        &nbsp;</td>
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
                                                    <td style="height: 14px">
                                                        Date Of Issue</td>
                                                    <td align="center" style="height: 14px">
                                                        <asp:Label ID="lblissudt" runat="server" Text=" " Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="DODate" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        DO Validity</td>
                                                    <td align="center">
                                                        <asp:Label ID="lblvaliddt" runat="server" Text=" " Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="DoValid" runat="server" Font-Bold="True"></asp:Label></td>
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
                                        <td align="left" colspan="5" style="height: 21px">
                                            Date:-<asp:Label ID="lblrodate" runat="server" Text="" Font-Bold="true"> </asp:Label>&nbsp;
                                            the following quantity&nbsp; of&nbsp; food grain in accordance with
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="left">
                                            the usual term and condition governing the sale of food grain from the</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: left">
                                            corporation's&nbsp; Stock for Amount of Rs. &nbsp; &nbsp;
                                            <asp:Label ID="lbltotamount" runat="server"></asp:Label></td>
                                        <td colspan="1">
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <center>
                                &nbsp;</center>
                            <center>
                                <table style="width: 642px">
                                    <tr>
                                        <td colspan="4" style="height: 21px" align="left">
                                            The price in respect of this Delivery Order has been received vide Demand Draft
                                            No.<asp:Label ID="lbldraft" runat="server" Text="......" Width="53px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px" align="left">
                                            District &nbsp;
                                            <asp:Label ID="lbldd_district" runat="server" Text="Label"></asp:Label>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;Dated. &nbsp;&nbsp;
                                            <asp:Label ID="lbldt" runat="server" Text="......" Width="66px"></asp:Label>
                                            &nbsp; &nbsp; &nbsp; DD Amount
                                            <asp:Label ID="DD_Amt" runat="server" Text="......" Width="66px"></asp:Label></td>
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
