<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rack_reconciliation.aspx.cs" Inherits="District_food_rpt_rack_reconciliation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Rack Reconciliation Report</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    
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
   
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/Printer.png"  alt ="print" width="70" /><strong >&nbsp;</strong></a>
     <div >
    <center >
    <table >
    <tr>
    <td>Rack Number
    </td>
    <td>
        <asp:DropDownList ID="ddlrackno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged1">
        </asp:DropDownList>
    </td>
    </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="79px" /></td>
        </tr>
    </table>
    </center>
    </div>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" Text="Rack Reconciliation Report" ForeColor ="Maroon" Font-Size ="20px"></asp:Label></td>
     </tr> 
                <tr>
                <td> </td>
                <td> </td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table >
    <tr> 
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> </td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;"> </td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> </td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> </td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> </td>
        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
        </td>
    
    </tr>
    <tr>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> </td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;"> </td>
        <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
            </td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> </td>
        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
        </td>
    </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                Rack No.</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                :<asp:Label ID="lblrackno" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                &nbsp; &nbsp;&nbsp;
                Sending Rail Head</td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                :<asp:Label ID="lblsrh" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                &nbsp;
                Sending District</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid" align="left">
                :<asp:Label ID="lblsdist" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
        </tr>
    <tr> 
    <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> Date Of Dispatch</td>
    <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left"> :<asp:Label ID="lbldisdate" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
    <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> &nbsp; &nbsp; &nbsp; Rack Qty</td>
    <td align ="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
        :<asp:Label ID="lblrqty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
    <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
        &nbsp;
        Bags In Rack
    </td>
        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid" align="left">
            :<asp:Label ID="lblbags" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
    </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                Commodity</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                :<asp:Label ID="lblcmdty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
            </td>
        </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                Receiving Rail Head</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                :<asp:Label ID="lblrrh" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                &nbsp; &nbsp;&nbsp;
                Receiving District</td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                :<asp:Label ID="lblrecdist" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                &nbsp;
                Sent Bags</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid" align="left">
                :<asp:Label ID="lblsntbags" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                Sent Qty</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                :<asp:Label ID="lblsntqty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                &nbsp; &nbsp;&nbsp;
                Rack Recd. On
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                :<asp:Label ID="lblrrecddate" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                &nbsp;
                Recd&nbsp; Bags</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                :<asp:Label ID="lblrecdbags" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                Recd Qty
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                :<asp:Label ID="lblrecdqty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                &nbsp; &nbsp;
                Unit Sortage</td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                :<asp:Label ID="lblusort" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                &nbsp;
                Qty. Sortage</td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid" align="left">
                :<asp:Label ID="lblqsort" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
        </tr>
        <tr>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                border-bottom: 1px solid">
            </td>
        </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
