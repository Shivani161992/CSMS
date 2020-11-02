<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reprint_RejectionNote.aspx.cs" Inherits="IssueCenter_Reprint_RejectionNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Print Reject Note Number</title>
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" id="IMG1" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2" style="height: 22px"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
                <tr>
                <td> </td>
                <td> </td>
                </tr>
        <tr>
        <td> 
            <asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label></td>
                <td> 
            <asp:Label ID="Label1" runat="server" Text="District:">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
     
    </table>
    </center>
    <center >
    <table cellspacing="3" style="font-size:14px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 70%;">
        <tr>
            <td colspan="5" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove; height: 1px">
                <table style="width: 631px">
                    <tr>
                        <td style="width: 210px; height: 18px">
                            Select Crop</td>
                        <td style="width: 91px; height: 18px; text-align: left">
                            <asp:DropDownList ID="ddlcrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged"
                                Width="150px">
                            </asp:DropDownList></td>
                        <td style="width: 91px; height: 18px">
                            <asp:HyperLink ID="hypback" runat="server" Font-Bold="True" NavigateUrl="~/IssueCenter/issue_welcome.aspx" Width="109px">वापस जायें</asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td style="width: 210px; height: 18px">
                            Select Rejection Note Number</td>
                        <td style="width: 91px; height: 18px; text-align: left;">
                            <asp:DropDownList ID="ddlAccptNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccptNumber_SelectedIndexChanged"
                                Width="218px">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px;" colspan="5"> 
        <strong>Reprint Reject Acknowledgement (Rejection Note)</strong></td>
    
    </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Sr.No:</td>
    <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> <asp:Label ID="lblgno" runat="server" Text=""> </asp:Label></td>
    <td style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    <td align ="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        Date/Time</td>
    <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
        :<asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 1.</td>
    <td align ="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">Name Of Depot </td>
    <td align ="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" > :<asp:Label ID="lbldepon" runat="server" Text=""></asp:Label></td>
    <td align ="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Crop Year</td>
    <td align ="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcrop" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            2.</td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                Sending District</td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
            <td align="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblsessiondist" runat="server" Visible="False"></asp:Label></td>
            <td align="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblSessionDepot" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                3.</td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                Purchase Center</td>
            <td align="left" colspan="3" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove">
                :<asp:Label ID="lblpccenter" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 147px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
            </td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
            </td>
        </tr>
     
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 4.</td>
    <td align ="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Commodity Name </td>
    <td align ="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="width: 147px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Rejection Date</td>
    <td align ="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
        :
        <asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 147px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
            </td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
            </td>
        </tr>
    
   
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left; height: 24px;"> 5.</td>
    <td align ="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 24px;"> 
        <strong>Rejection</strong> No.</td>
    <td align ="left" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove; height: 24px;" colspan="2"> :<asp:Label ID="lblwcmno" runat="server" Text="" Font-Bold="true" Font-Size="14px"></asp:Label></td>
    <td align ="left" style="width: 184px; height: 24px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    </tr>
        <tr>
            <td colspan="5" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove; height: 24px;
                text-align: left">
                
                 <asp:GridView Width="100%" ID="grd_viewDepot" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                      
                        <Columns>
                           
                            <asp:BoundField DataField="TruckChalanNo" HeaderText="Challan No"> </asp:BoundField>
                            <asp:BoundField DataField="DateOfIssue1" HeaderText="Challan Date"></asp:BoundField>
                            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter Name"></asp:BoundField>
                            <asp:BoundField DataField="TruckNo" HeaderText="Truck No."></asp:BoundField>
                            <asp:BoundField DataField="Bags" HeaderText="Send Bags."></asp:BoundField>
                            <asp:BoundField DataField="QtyTransffer" HeaderText="Send Quantity"></asp:BoundField>
                            
                            
                        </Columns>
                        
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    </asp:GridView>
                
            </td>
        </tr>
    </table>
    <table style="font-size:14px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 70%; text-align: left;">
    <tr> 
    <td style="height: 18px; text-align: left; width: 20px;">
        6.</td>
    <td align="left" style="height: 18px; width: 170px;">
        Discription</td>
        <td colspan="3" style="height: 18px; text-align: left">
            <span style="font-size: 12pt; font-family: Mangal"><span style="font-size: 11pt">दिनांक</span>
                <asp:Label ID="Label2" runat="server" Font-Size="Small"></asp:Label>
                <span style="font-size: 11pt"><span style="font-size: 10pt">को केन्द्र पर प्राप्त</span>
                    <asp:Label ID="commodity" runat="server" Font-Size="Small"></asp:Label>
                    <span style="font-size: 10pt">निर्धारित गुणवत्ता का नहीं पाया गया| अत: स्कंध
                <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif'; mso-ascii-font-family: Calibri;
                    mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin;
                    mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin; mso-bidi-theme-font: minor-bidi;
                    mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">अमान्य
                    किया जाता है</span></span></span></span></td>
    </tr>
    <tr> 
    <td style="height: 21px; width: 20px;"></td>
    <td style="height: 21px; width: 170px;"></td>
    <td style="width: 90px" > &nbsp; 
        
        &nbsp;&nbsp; &nbsp;
    </td>
        <td colspan="2" style="height: 21px">
        </td>
    </tr>
    <tr> 
    <td style="width: 20px"></td>
    <td style="width: 170px">
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label></td>
    <td style="width: 90px"> &nbsp;&nbsp;
    </td>
        <td colspan="2">
        </td>
    </tr>
    <tr> 
    <td style="width: 20px"></td>
    <td style="width: 170px"></td>
    <td style="width: 90px"> &nbsp;
    </td>
        <td colspan="2">
        </td>
    </tr>
    <tr> 
    <td style="width: 20px"></td>
    <td style="width: 170px">
        Signature Of
    </td>
    <td style="width: 90px"> Signature Of&nbsp;</td>
        <td colspan="2">
            Signature Of
        </td>
    </tr>
    <tr> 
    <td style="width: 20px"> </td>
    <td style="width: 170px"> Truck Driver</td>
    <td style="width: 90px">Incharge (Society)</td>
        <td colspan="2">
            Incharge (NAN)</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
