<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DelAcceptance1.aspx.cs" Inherits="District_DelAcceptance" Title="Delete Acceptance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function CheckCalDate(tx){
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

<center>
    <table style="border: thin groove black; width: 676px; height: 388px; ">
        <tr>
            <td colspan="2" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 600px; border-bottom: black thin groove; text-align: left;">
                <span style="font-size: 11pt; color: #cc0033; text-align: center;">अगर वेयर हाउस 
                में WHR एंट्री नहीं हुई है तो ही डिलीट होगा|</span></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 38px; border-bottom: black thin groove; text-align: left">
                <asp:Label ID="Label3" runat="server" Text="Select Commodity" Width="173px"></asp:Label></td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 339px; border-bottom: black thin groove; text-align: left">
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Width="221px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblAcdate" runat="server" Text="Select Date of Acceptance" Width="173px"></asp:Label></td>
            <td style="width: 339px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            
            <asp:TextBox ID="txtAccDate" runat="server" Width="119px" ></asp:TextBox>
       
	     
	     <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtAccDate'
	    });
	     </script>
	          
            
            </td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblisscen" runat="server" Text="Select Issue Center" Width="193px"></asp:Label></td>
            <td style="width: 339px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:DropDownList ID="ddlIC" runat="server" Width="209px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblTC" runat="server" Text="Select TC Number" Width="179px"></asp:Label></td>
            <td style="width: 339px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:DropDownList ID="ddlTCNum" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTCNum_SelectedIndexChanged" Width="203px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblAcceptance" runat="server" Text="Select Acceptance Number" Width="181px"></asp:Label></td>
            <td style="width: 339px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:DropDownList ID="ddlAcceptanceNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAcceptanceNumber_SelectedIndexChanged" Width="227px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" colspan="2">
                 
            </td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="Label1" runat="server" Text="Sending Society" Width="117px"></asp:Label></td>
            <td style="width: 339px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblsociety" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 38px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 28px;">
                <asp:Label ID="labeltruck" runat="server" Text="Truck Number" Width="128px"></asp:Label></td>
            <td style="width: 339px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 28px;">
                <asp:Label ID="lbltruck" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 38px; border-bottom: black thin groove; text-align: left">
                <asp:Label ID="Label2" runat="server" Text="Issue ID" Width="109px"></asp:Label></td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 339px; border-bottom: black thin groove; text-align: left">
                <asp:Label ID="lblissueId" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 38px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
            </td>
            <td style="width: 339px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
            </td>
        </tr>
        <tr>
            <td style="width: 38px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" /></td>
            <td style="width: 339px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
        </tr>
    </table>
    </center>


</asp:Content>

