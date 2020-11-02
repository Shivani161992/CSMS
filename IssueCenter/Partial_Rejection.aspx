<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Partial_Rejection.aspx.cs" Inherits="IssueCenter_Partial_Rejection" Title="Partial Rejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">


 function CheckCalDate(tx) 
 {
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>

    <table style="width: 768px" >
        <tr>
            <td style="height: 24px">
                </td>
            <td colspan="2" style="font-size: large; color: #000099; height: 24px;">
                <b>Partial Rejection</b></td>
            <td style="width: 128px; height: 24px;">
                                            </td>
        </tr>
        
        <tr>
            <td style="height: 24px; color: #CC0000; font-size: medium;" colspan="4">
                <blink>
                स्वीकृत की गयी मात्रा का पहले स्वीकृत पत्र जारी करें , फिर अस्वीकृत मात्रा का 
                अस्वीकृत पत्र जारी करें</blink></td>
        </tr>
        
        <tr>
            <td bgcolor="#9999FF">
                <asp:Label ID="Label3" runat="server" Text="Date of Deposit" Font-Bold="True"></asp:Label>
            </td>
           <td bgcolor="#9999FF">
                <asp:TextBox ID="DaintyDate3" runat="server" Height="17px" Width="121px"></asp:TextBox>
                
                <script type  ="text/javascript">
                    new tcal({
                        'formname': '0',
                        'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
                    });
	     </script>
                
            </td>
            <td bgcolor="#9999FF">
                <asp:Label ID="Label7" runat="server" 
                    Text="Select Commodity" Font-Bold="True"></asp:Label>
            </td>
            <td bgcolor="#9999FF">
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Height="19px" Width="120px" 
                    AutoPostBack="True" onselectedindexchanged="ddlcommodtiy_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
           <td bgcolor="#9999FF">
                <asp:Label ID="Label6" runat="server" Text="Receiving Godown" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="2" style="text-align: left; height: 26px;" bgcolor="#9999FF">
                <asp:DropDownList ID="ddlgodown" runat="server" Height="21px" Width="295px" 
                    onselectedindexchanged="ddlgodown_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
           <td bgcolor="#9999FF">
                </td>
        </tr>
        <tr>
           <td bgcolor="#9999FF">
                <asp:Label ID="Label8" runat="server" Text="Select Issue Id / TC Number" 
                    Font-Bold="True"></asp:Label>
            </td>
            <td colspan="2" style="text-align: left" bgcolor="#9999FF">
                <asp:DropDownList ID="ddlissueId" runat="server" Height="21px" Width="295px" 
                    onselectedindexchanged="ddlissueId_SelectedIndexChanged" 
                    AutoPostBack="True" >
                </asp:DropDownList>
            </td>
           <td bgcolor="#9999FF">
                &nbsp;</td>
        </tr>
       
        <tr>
            <td bgcolor="#FFCCFF">
                समिति का नाम                समिति का नाम</td>
            <td colspan="3" style="text-align: left"  bgcolor="#FFCCFF">
                <asp:TextBox ID="txtSocName" runat="server" BackColor="#CCCCCC" Height="21px" 
                    ReadOnly="True" Width="330px"></asp:TextBox>
                                            <asp:Label ID="lblSocId" runat="server" 
                    Visible="False"></asp:Label>
                                            </td>
        </tr>
        <tr>
            <td bgcolor="#FFCCFF">
                ट्रक नंबर</td>
             <td bgcolor="#FFCCFF">
                <asp:TextBox ID="TxtTruckNumber" runat="server" BackColor="#CCCCCC" 
                    Height="20px" ReadOnly="True" Width="121px"></asp:TextBox>
            </td>
           <td bgcolor="#FFCCFF">
                टी सी नंबर</td>
            <td bgcolor="#FFCCFF" >
                <asp:TextBox ID="txtTcNumber" runat="server" BackColor="#CCCCCC" Height="20px" 
                    ReadOnly="True" Width="121px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td bgcolor="#FFCCFF">
                भेजे गए बोरे</td>
             <td bgcolor="#FFCCFF">
                <asp:TextBox ID="txtsendbags" runat="server" BackColor="#CCCCCC" 
                    Height="20px" ReadOnly="True" Width="121px"></asp:TextBox>
            </td>
           <td bgcolor="#FFCCFF">
                प्राप्त बोरे</td>
            <td bgcolor="#FFCCFF" >
                <asp:TextBox ID="txtrecbags" runat="server" BackColor="#CCCCCC" 
                    Height="20px" ReadOnly="True" Width="121px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
             <td bgcolor="#FFCCFF">
                भेजी गयी मात्रा</td>
             <td bgcolor="#FFCCFF">
                <asp:TextBox ID="txtsendQty" runat="server" BackColor="#CCCCCC" Height="20px" 
                    ReadOnly="True" Width="121px"></asp:TextBox>
                                            </td>
             <td bgcolor="#FFCCFF">
                प्राप्त मात्रा</td>
            <td bgcolor="#FFCCFF" >
                <asp:TextBox ID="txtRecdQty" runat="server" BackColor="#CCCCCC" Height="20px" 
                    ReadOnly="True" Width="121px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
             <td bgcolor="#FFCCFF">
                 बोरो में अंतर</td>
             <td bgcolor="#FFCCFF">
                <asp:TextBox ID="txtdiffBags" runat="server" BackColor="#CCCCCC" Height="20px" 
                    ReadOnly="True" Width="121px"></asp:TextBox>
                                            </td>
             <td bgcolor="#FFCCFF">
                 मात्रा में अंतर</td>
            <td bgcolor="#FFCCFF" >
                <asp:TextBox ID="txtqtyDiff" runat="server" BackColor="#CCCCCC" Height="20px" 
                    ReadOnly="True" Width="121px"></asp:TextBox>
                                            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 109%">
                    <tr>
                        <td bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small; text-align: left;">स्कंध एफ०ए०क्यू० नहीं है</span></td>
                        <td text-align: left; height: 29px;" bgcolor="#CFDCC8">
             <asp:CheckBox ID="chk_faq" runat="server" OnCheckedChanged="chk_faq_CheckedChanged" AutoPostBack="True" />
                        </td>
                        <td style= height: 29px;" bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small;">बाह्य पदार्थ अधिक है</span></td>
                        <td style=" text-align: left; height: 29px;" bgcolor="#CFDCC8">
             <asp:CheckBox ID="chk_extra" runat="server" OnCheckedChanged="chk_extra_CheckedChanged" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td style=" height: 32px;" bgcolor="#CFDCC8">
             <span style="font-family: Mangal; font-size: small;">कितने प्रतिशत एफ०ए०क्यू० नहीं है</span></td>
                        <td style="height: 32px;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_faq_per" runat="server" MaxLength="13" ReadOnly="True" Width="110px">0</asp:TextBox>
                        </td>
                        <td style="height: 32px;" bgcolor="#CFDCC8">
                            <span style="font-size: small">कितने प्रतिशत </span> 
                            <span style="font-family: Mangal; font-size: small;">बाह्य पदार्थ अधिक है</span></td>
                        <td style="text-align: left; height: 32px;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_extra_per" runat="server" MaxLength="13" ReadOnly="True" Width="99px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 31px;" bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small;">दाने क्षतिग्रस्त है</span></td>
                        <td style="height: 31px;" bgcolor="#CFDCC8">
                            <asp:CheckBox ID="chk_damaged" runat="server" OnCheckedChanged="chk_damaged_CheckedChanged" AutoPostBack="True" />
                        </td>
                        <td style="height: 31px;" bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small;">स्कंध चमक विहीन है</span></td>
                        <td style="text-align: left; height: 31px;" bgcolor="#CFDCC8">
             <asp:CheckBox ID="chk_brightness" runat="server" OnCheckedChanged="chk_brightness_CheckedChanged" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px;" bgcolor="#CFDCC8" >
                            <span style="font-size: small">कितने प्रतिशत </span> 
                            <span style="font-family: Mangal; font-size: small;">दाने क्षतिग्रस्त है</span></td>
                        <td style="height: 35px;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_damage_per" runat="server" MaxLength="13" ReadOnly="True" Width="110px">0</asp:TextBox>
                        </td>
                        <td style="height: 35px;" bgcolor="#CFDCC8">
                            <span style="font-size: small">कितने प्रतिशत </span> 
                            <span style="font-family: Mangal; font-size: small;">स्कंध चमक विहीन है</span></td>
                        <td style=" text-align: left; height: 35px;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_bright_per" runat="server" MaxLength="13" ReadOnly="True" Width="110px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 27px;" bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small;">आंशिक क्षतिग्रस्त है</span></td>
                        <td style="width: 130px; text-align: left; height: 27px;" bgcolor="#CFDCC8">
             <asp:CheckBox ID="chk_partially" runat="server" OnCheckedChanged="chk_partially_CheckedChanged" AutoPostBack="True" />
                        </td>
                        <td style="height: 27px;" bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small;">टूटन व् सिकुड़े दाने है</span></td>
                        <td style="text-align: left; height: 27px;" bgcolor="#CFDCC8">
             <asp:CheckBox ID="chk_splited" runat="server" OnCheckedChanged="chk_splited_CheckedChanged" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                       <td style="height: 27px;" bgcolor="#CFDCC8">
                            <span style="font-size: small">कितने प्रतिशत </span> 
                            <span style="font-family: Mangal; font-size: small;">आंशिक क्षतिग्रस्त है</span></td>
                      <td style="height: 27px;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_partial_per" runat="server" MaxLength="13" ReadOnly="True" Width="110px">0</asp:TextBox>
                        </td>
                       <td style="height: 27px;" bgcolor="#CFDCC8">
                            <span style="font-size: small">कितने प्रतिशत </span> 
                            <span style="font-family: Mangal; font-size: small;">टूटन व् सिकुड़े दाने है</span></td>
                        <td style=" text-align: left;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_split_per" runat="server" MaxLength="13" ReadOnly="True" Width="110px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px;" bgcolor="#CFDCC8">
             <span lang="HI" style="line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; font-size: small;">नमी का प्रतिशत अधिक है</span></td>
                        <td style=" height: 30px;" bgcolor="#CFDCC8">
             <asp:CheckBox ID="chk_moist" runat="server" OnCheckedChanged="chk_moist_CheckedChanged" AutoPostBack="True" />
                        </td>
                        <td style=" height: 30px;" bgcolor="#CFDCC8">
                            </td>
                        <td style=" height: 30px;" bgcolor="#CFDCC8">
                            </td>
                    </tr>
                    <tr>
                        <td style=" height: 31px;" bgcolor="#CFDCC8">
                            <span style="font-size: small">कितने प्रतिशत </span> 
                            <span style="font-family: Mangal; font-size: small;">नमी अधिक है</span></td>
                        <td style="height: 31px;" bgcolor="#CFDCC8">
             <asp:TextBox ID="txt_moist_per" runat="server" MaxLength="13" ReadOnly="True" Width="110px">0</asp:TextBox>
                        </td>
                        <td style="height: 31px;" bgcolor="#CFDCC8" colspan="2">
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="139px">Print Detail</asp:HyperLink>
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 27px;" bgcolor="#CFDCC8">
             <span lang="HI" style="font-size: small; line-height: 115%; font-family: 'Mangal','serif';
                 mso-ascii-font-family: Calibri; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri;
                 mso-fareast-theme-font: minor-latin; mso-hansi-font-family: Calibri; mso-hansi-theme-font: minor-latin;
                 mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                 mso-bidi-language: HI; text-align: left;">अन्य कारण </span>
                        </td>
                        <td colspan="3" style="text-align: left" bgcolor="#CFDCC8">
             <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Width="530px" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
             <asp:Button ID="btnclose" runat="server" Text="Close" Width="91px" 
                    onclick="btnclose_Click"/></td>
            <td>
         <asp:Button ID="btnsubmit" runat="server" Text="Submit"  Visible="false"  
                    Width="103px" onclick="btnsubmit_Click"/></td>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

