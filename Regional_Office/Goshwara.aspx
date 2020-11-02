<%@ Page Language="C#" MasterPageFile="~/MasterPage/Regional_Office.master" AutoEventWireup="true" CodeFile="Goshwara.aspx.cs" Inherits="Regional_Office_Goshwara" Title="Society Entry Goshwara Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
<script type="text/javascript">
    function CheckIsNumeric(e, tx)
    
     {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57) || (AsciiCode == 47))
        
         {
            alert('Please enter only numbers.');
            return false;
        }

       
       
    }
    </script>
  
    <table style="width: 623px; height: 550px">
        <tr>
            <td colspan="2" style="color: #000099">
                <b>वर्ष 2015-16&nbsp; के अंतर्गत जिलो की प्रारंभिक स्थति का गोषवारा</b></td>
        </tr>
        <tr>
            <td colspan="2" style="color: #993300; font-size: small;">
                <b>&nbsp;जिनमे निम्न सुविधा उपलब्ध नहीं है उनकी केवल Yes / No&nbsp; 
                में भरे</b></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                बैनर</td>
            <td>
                <asp:DropDownList ID="ddlbanner" runat="server" Height="25px" Width="150px" >
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                कृषको की बैठने की छायादार व्यवस्था</td>
            <td>
                <asp:DropDownList ID="ddlshadow" runat="server" Height="25px" Width="150px" >
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                साफ पीने के पानी की व्यवस्था</td>
            <td>
                <asp:DropDownList ID="ddlwater" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                शौचालय व्यवस्था</td>
            <td>
                <asp:DropDownList ID="ddltoiler" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                फर्स्ट एड बॉक्स</td>
            <td>
                <asp:DropDownList ID="ddlfirstaid" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                लेपटॉप</td>
            <td>
                <asp:DropDownList ID="ddllaptop" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                प्रिंटर</td>
            <td>
                <asp:DropDownList ID="ddlprinter" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                बटेरी</td>
            <td>
                <asp:DropDownList ID="ddlbattery" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                डाटा कनेक्टिविटी</td>
            <td>
                <asp:DropDownList ID="ddldataconnect" runat="server" Height="25px" Width="150px" >
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                सिलाई मशीन की संख्या</td>
            <td>
                <asp:DropDownList ID="ddlstiching" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                                                </td>
        </tr>
        <tr>
            <td>
                तौल मशीन की संख्या</td>
            <td>
                <asp:DropDownList ID="ddltaulmachine" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                छन्ना संख्या</td>
            <td>
                <asp:DropDownList ID="ddlfilter" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                पंखा संख्या</td>
            <td>
                <asp:DropDownList ID="ddlfan" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                मॉइस्चर मीटर</td>
            <td>
                <asp:DropDownList ID="ddlmoit" runat="server" Height="25px" Width="150px" >
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                एनेमल प्लेट</td>
            <td>
                <asp:DropDownList ID="ddlenamel" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                डिजिटल बैलेंस</td>
            <td>
                <asp:DropDownList ID="ddldigital" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                तिरपाल</td>
            <td>
                <asp:DropDownList ID="ddltirpal" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                अन्य तकनिकी उपकरण</td>
            <td>
                <asp:DropDownList ID="ddlothers" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                केंद्र पर उपलब्ध वरदाना गठान</td>
            <td>
                <asp:DropDownList ID="ddlvardana" runat="server" Height="25px" Width="150px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                मानव संसाधन की उपलब्धता</td>
            <td>
                <asp:DropDownList ID="ddlhr" runat="server" Height="25px" Width="180px">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnClose" runat="server" Height="27px" Text="Close" 
                    Width="102px" onclick="btnClose_Click" />
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Height="27px" Text="Save" 
                    Width="103px" onclick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

