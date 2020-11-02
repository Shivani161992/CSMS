<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RackDispatch_IC.aspx.cs" Inherits="District_RackDispatch_IC" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
function CheckIsnonDecimal(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 47) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}
}
   </script>

<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var coda = event.keyCode;
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>

    <table style="width: 676px; height: 378px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="4" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <span style="color: #cc0033"><strong>
                रैक से भेजने हेतु , केन्द्र द्वारा भेजे गए खाद्यान्न की प्राप्ति हेतु प्रविष्टि
                करें</strong></span></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Select Rake Number</span></td>
            <td colspan="3" style="height: 1px; width: 128px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:DropDownList ID="ddlracknumber" runat="server" Width="232px" AutoPostBack="True" OnSelectedIndexChanged="ddlracknumber_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Sending District</span></td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Label ID="lbldistrict" runat="server" Width="148px" Font-Bold="True" ForeColor="#0000C0"></asp:Label></td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Select Issue Center</span></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
            <asp:DropDownList ID="ddlIssuecenter" runat="server" Width="152px" OnSelectedIndexChanged="ddlIssuecenter_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Select T.C. Number</span></td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:DropDownList ID="ddlchallan" runat="server" Width="152px" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
             <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Truck Number</span></td>
             <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
                <asp:Label ID="lbltruck" runat="server" Width="156px" Font-Bold="True" ForeColor="#0000C0"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">T.C. Date</span></td>
             <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Label ID="lblchallanDate" runat="server" Width="153px" Font-Bold="True" ForeColor="#0000C0"></asp:Label></td>
           <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Crop Year</span></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
                <asp:Label ID="lblcropyear" runat="server" Width="148px" Font-Bold="True" ForeColor="#0000C0"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Send Bags in Number</span></td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:TextBox ID="txtsendbags" runat="server" Enabled="False" Font-Bold="True" ForeColor="#0000C0"></asp:TextBox></td>
             <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Send Qty in Qts</span></td>
             <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
                <asp:TextBox ID="txtsendQty" runat="server" Enabled="False" Font-Bold="True" ForeColor="#0000C0"></asp:TextBox></td>
        </tr>
        <tr>
             <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Recd Bags in Number</span></td>
             <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:TextBox ID="txtRecdBags" runat="server" AutoComplete="off"></asp:TextBox></td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Recd Qty in Qts</span></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
                <asp:TextBox ID="txtRecdQty" runat="server" AutoComplete="off"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Received Date</span></td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove; text-align: left;">
            
             <asp:TextBox ID="DaintyDate1" runat="server" Width="115px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
            
            </td>
             <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">
                <asp:HiddenField ID="hdfComdty" runat="server" />
                 </span></td>
           <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
                <asp:HiddenField ID="hdfRecDist" runat="server" />
                </td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; background-color: #ffff99;">
            </td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; background-color: #ffff99;">
            </td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; background-color: #ffff99;">
            </td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove; background-color: #ffff99;">
            </td>
        </tr>
        <tr>
            <td style="width: 160px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:HiddenField ID="hdfChallanDate" runat="server" />
            </td>
            <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="54px" OnClick="btnSave_Click" /></td>
             <td style="width: 128px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Button ID="btnnew" runat="server" Text="New" Width="67px" OnClick="btnnew_Click" /></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
                <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
        </tr>
    </table>
</asp:Content>

