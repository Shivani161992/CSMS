<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Reject_Procurement.aspx.cs" Inherits="IssueCenter_Print_Reject_Procurement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Print Reject Truck</title>
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0" style="font-size:13px;" >
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                    <td colspan="2" style="height: 10px">
                        <strong><span style="font-size: 12pt">Truck Rejection Receipt</span></strong></td>
                </tr>
                <tr>
                <td style="height: 19px"> </td>
                <td style="height: 19px"> </td>
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
    <table  cellspacing="2" style="font-size:13px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 560px; height: 630px;">
    <tr>
    <td style="border-right: black thin groove"> </td>
    <td style="border-right: black thin groove"> </td>
        <td align="left" colspan="2" style="border-right: black thin groove;">
            <strong><span style="font-size: 11pt">Rejected &nbsp;Acknowledgement</span></strong></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px;"> Sr.No:</td>
    <td align="left" style="border-right: black thin groove; height: 17px"> <asp:Label ID="lblgno" runat="server" Text="" Font-Size="12px"> </asp:Label></td>
    <td style="border-right: black thin groove; height: 17px; width: 99px;"> </td>
    <td align ="left" style="border-right: black thin groove; height: 17px">
        Date/Time</td>
    <td align="left" style="height: 17px">
        :<asp:Label ID="lblgdtae" runat="server" Text="" Font-Size="12px"> </asp:Label> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove"> 1.</td>
    <td align ="left" style="border-right: black thin groove"> IssueCenter
    </td>
    <td align ="left" style="border-right: black thin groove; width: 99px;" > :<asp:Label ID="lbldepon" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove"> 
                Sending District</td>
    <td align ="left"> :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            2.</td>
            <td align="left" style="border-right: black thin groove">
                Purchase Center</td>
            <td align="left" colspan="3" style="border-right: black thin groove">
                :<asp:Label ID="lblpccenter" runat="server" Font-Size="12px"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
     <tr> 
    <td style="border-right: black thin groove"> 3.</td>
    <td align ="left" style="border-right: black thin groove">Challan No. </td>
    <td align ="left" style="border-right: black thin groove; width: 99px;"> :<asp:Label ID="lblchallanno" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove"> Challan Date</td>
    <td align ="left">
        :<asp:Label ID="lblchallandt" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove"> 4.</td>
    <td align ="left" style="border-right: black thin groove"> Commodity Name </td>
    <td align ="left" style="border-right: black thin groove; width: 99px;"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove"> Crop Year</td>
    <td align ="left">
        :<asp:Label ID="lblcrop" runat="server"></asp:Label> </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove"> 5.</td>
    <td align ="left" style="border-right: black thin groove"> Transporter Name </td>
    <td align ="left" style="border-right: black thin groove; width: 99px;"> :<asp:Label ID="lbltransp" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove"> Truck&nbsp; No.</td>
    <td align ="left">
        :<asp:Label ID="lblvicln" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove"> </td>
    <td align ="left" style="border-right: black thin groove"> 
        <asp:Label ID="lblsendBags" runat="server" Text="Sending Bags"></asp:Label></td>
    <td align ="left" style="border-right: black thin groove; width: 99px;"> 
        <asp:Label ID="lblsend_bagsNum" runat="server"></asp:Label></td>
    <td style="border-right: black thin groove"> 
        <asp:Label ID="lblsendQty" runat="server" Text="Sending Quantity"></asp:Label></td>
    <td> 
        <asp:Label ID="lblSend_Qtydisplay" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
            </td>
            <td style="border-right: black thin groove">
            </td>
            <td>
                <asp:Label ID="lblmeasqty" runat="server" Text="Qty in Qtls"></asp:Label></td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove"> 6</td>
        <td align="left" colspan="4">
            <span lang="HI" style="font-size: 12pt; line-height: 115%; font-family: 'Mangal','serif';
                mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                mso-bidi-language: HI">दिनांक </span>
            <asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label>
            <span lang="HI" style="font-size: 12pt; line-height: 115%; font-family: 'Mangal','serif';
                mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                mso-bidi-language: HI"><span style="font-size: 11pt">को केन्द्र पर प्राप्त</span>
                <asp:Label ID="commodity" runat="server"></asp:Label>
                <span style="font-size: 11pt">
                निर्धारित गुणवत्ता का नहीं</span> </span>
        </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" colspan="4">
                <span style="font-size: 11pt">
                <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                    mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                    mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                    mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                    mso-bidi-language: HI">पाया गया| अत: स्कंध निम्न कारण से </span><span lang="HI" style="line-height: 115%; font-family: 'Calibri','sans-serif'; mso-ascii-theme-font: minor-latin;
                        mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin;
                        mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                        mso-bidi-language: HI; mso-bidi-font-family: Mangal"><span style="mso-spacerun: yes">
                            &nbsp;</span></span><span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                                mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                                mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                                mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                                mso-bidi-language: HI">अमान्य किया जाता है |</span></span></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" colspan="4">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove"> </td>
    <td align ="left" style="border-right: black thin groove"> 
        <span style="font-size: 9pt; font-family: Mangal">स्कंध एफ०ए०क्यू० नहीं है</span></td>
    <td align ="left" style="border-right: black thin groove; width: 99px;"> 
        <asp:CheckBox ID="chk_faq" runat="server" Enabled="False" Font-Bold="True" ForeColor="Black"/></td>
    <td align ="left" style="border-right: black thin groove"> 
        <span style="font-size: 9pt; font-family: Mangal">बाह्य पदार्थ अधिक है</span></td>
    <td align ="left">
        &nbsp;<asp:CheckBox ID="chk_extra" runat="server" Enabled="False" Font-Bold="True" /></td>
    </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px">
                <asp:Label ID="lbl_faq_per" runat="server" Text=""></asp:Label></td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left">
                <asp:Label ID="lbl_extra_per" runat="server" Text=""> </asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-size: 9pt; font-family: Mangal">दाने क्षतिग्रस्त है</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
                <asp:CheckBox ID="chk_damaged" runat="server" Enabled="False" Font-Bold="True" /></td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-size: 9pt; font-family: Mangal">स्कंध चमक विहीन है</span></td>
            <td align="left">
                <asp:CheckBox ID="chk_brightness" runat="server" Enabled="False" Font-Bold="True" /></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px">
                <asp:Label ID="lbl_damage_per" runat="server" Text=""> </asp:Label></td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left">
                <asp:Label ID="lbl_bright_per" runat="server" Text=""> </asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-size: 9pt; font-family: Mangal">आंशिक क्षतिग्रस्त है</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
                <asp:CheckBox ID="chk_partially" runat="server" Enabled="False" Font-Bold="True" /></td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-size: 12pt; font-family: Mangal"><span style="font-size: 9pt">टूटन
                    व् सिकुड़े दाने है</span> </span>
            </td>
            <td align="left">
                <asp:CheckBox ID="chk_splited" runat="server" Enabled="False" Font-Bold="True" /></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px">
                <asp:Label ID="lbl_partial_per" runat="server" Text=""> </asp:Label></td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left">
                <asp:Label ID="Lbl_Split_Per" runat="server" Text=""> </asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-size: 9pt; font-family: Mangal">नमी का प्रतिशत अधिक है</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px;">
                <asp:CheckBox ID="chk_moist" runat="server" Enabled="False" Font-Bold="True" /></td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove">
                <span style="font-family: Mangal">प्रतिशत</span></td>
            <td align="left" style="border-right: black thin groove; width: 99px">
                <asp:Label ID="Lbl_Moist_Per" runat="server" Text=""> </asp:Label></td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove">
        7.</td>
    <td align="left" style="border-right: black thin groove">
        Discription</td>
    <td colspan="3" style="border-right: black thin groove"> 
        <asp:Label ID="lblreason" runat="server"></asp:Label></td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove; width: 99px;"> &nbsp; &nbsp; &nbsp;
    </td>
    <td style="border-right: black thin groove"></td>
    <td></td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove;"></td>
    <td style="border-right: black thin groove;">
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label></td>
    <td style="border-right: black thin groove; width: 99px;"> &nbsp;&nbsp;
    </td>
    <td style="border-right: black thin groove;"></td>
    <td style="height: 10px"> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove; width: 99px;"> &nbsp;
    </td>
    <td style="border-right: black thin groove"></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove">
        Signature Of
    </td>
    <td style="border-right: black thin groove; width: 99px;"> Signature Of&nbsp;</td>
    <td style="border-right: black thin groove"> </td>
    <td> Signature Of
    </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove"> </td>
    <td style="border-right: black thin groove"> Truck Driver</td>
    <td style="border-right: black thin groove; width: 99px;">Incharge </td>
    <td style="border-right: black thin groove"> </td>
    <td> Branch Manager</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
