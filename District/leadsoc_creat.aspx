<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" CodeFile="leadsoc_creat.aspx.vb" Inherits="leadsoc_creat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lead Society</title>

<script type="text/javascript">
    function CheckIsNumeric(tx)
    {
        var AsciiCode = event.keyCode;
        var txt=tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3=txt2*1;
        if ((AsciiCode < 46) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
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
            if ((AsciiCode == 46 ))
            {
                alert('Point must be apear only one time.');
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

</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <table style="vertical-align: top; width: 792px; text-align: left">
            <tr>
                <td style="font-size: 16pt; color: #ffffff; background-color: #99cccc; width: 779px; height: 25px;">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<strong><span style="font-size: 15pt; color: #ffffff">Lead Society Master &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 
                        <asp:HyperLink ID="HyperLink1" runat="server"
                            Font-Bold="False" Font-Size="Medium" NavigateUrl="~/District/Dist_welcome.aspx">Back</asp:HyperLink>
                        &nbsp; &nbsp;&nbsp; <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" Font-Size="Medium">LogOut</asp:LinkButton></span></strong></td>
            </tr>
            <tr>
                <td style="width: 779px">
    <table id="TABLE1" border ="1" style="width: 784px; height: 520px; border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid; border-bottom: teal 1px solid;" >
        <tr>
            <td align="left" colspan="4" style="font-size: medium; height: 14px">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="Red" Font-Names="DVBW-TTYogeshEN" Font-Bold="False"></asp:Label></td>
        </tr>
    <tr style="font-weight: bold; color: #000000; font-family: Times New Roman;">
    <td style="width: 1586px; height: 4px; font-size: medium;" align="left">District </td>
    <td style="width: 238px; height: 4px; font-size: 12pt;">
        <asp:Label ID="distname" runat="server" Font-Names="DVBW-TTYogeshEN" Width="224px" Font-Size="Large" Height="24px" Font-Bold="True" ForeColor="#804000"></asp:Label></td>
    <td style="height: 4px; font-size: medium; width: 106px;" align="left">
        Lead Society</td>
    <td style="color: red;">
        <asp:TextBox ID="leadname" runat="server" Height="24px" Width="280px" TextMode="MultiLine" Enabled="False"></asp:TextBox>*</td>
    </tr>
    <tr style="color: #000000; font-family: Times New Roman;">
    <td style="width: 1586px; height: 7px; font-size: medium;" align="left">
        <strong>Block </strong>
    </td>
    <td style="width: 238px; height: 7px">
        <asp:DropDownList ID="blockname" runat="server" Width="256px" AutoPostBack="True" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium">
        </asp:DropDownList></td>
    <td style="height: 7px; font-size: medium; width: 106px;" align="left"><strong> (hindi)</strong></td>
    <td style="width: 359px; height: 7px; vertical-align: top; font-family: 'Kruti Dev 010'; text-align: left;">
        <asp:TextBox ID="leadname_h" runat="server" Height="24px" Width="280px" Font-Names="Kruti Dev 010" Font-Size="Medium" TextMode="MultiLine" ></asp:TextBox></td>
    </tr>
    <tr>
    <td style="width: 1586px; font-size: medium; height: 32px;" align="left"><strong>Lead Society</strong></td>
    <td style="width: 238px; height: 32px;">
        <asp:DropDownList ID="lead" runat="server" AutoPostBack="True" Width="256px" Font-Names="Arial">
        </asp:DropDownList></td>
    <td style="font-size: medium; width: 106px; height: 32px;" align="left"><strong> Address</strong></td>
    <td style="width: 359px; height: 32px;">
        <asp:TextBox ID="leadadd" runat="server" Height="24px" Width="280px" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
    <td style="width: 1586px; height: 5px; font-size: medium;" align="left"><strong>Issue Centre </strong></td>
    <td style="width: 238px; height: 5px">
        <asp:DropDownList ID="issuename" runat="server" Width="256px" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" AutoPostBack="True" Enabled="False">
        </asp:DropDownList></td>
    <td style="height: 5px; font-size: medium; width: 106px;" align="left"><strong> Telephone</strong></td>
    <td style="width: 359px; height: 5px">
        <asp:TextBox ID="leadtele" runat="server" Height="16px" MaxLength="14" Width="112px"></asp:TextBox></td>
    </tr>
    <tr>
    <td style="width: 1586px; font-size: medium;" align="left"><strong>Add More FPS</strong></td>
    <td style="width: 238px;">
        <asp:DropDownList ID="fpsname" runat="server" Width="256px" AutoPostBack="True" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium">
        </asp:DropDownList></td>
    <td style="font-size: medium; width: 106px;" align="left"><strong> Mobile</strong></td>
    <td style="width: 359px;">
        <asp:TextBox ID="leadmob" runat="server" MaxLength="10" Width="112px" Height="16px"></asp:TextBox></td>
    </tr>
    <tr>
    <td style="width: 1586px; font-size: medium; vertical-align: top; height: 28px;" align="left">
        <strong>
            <br />
            Selected
            Fair Price Shops Under&nbsp;<br />
            Lead
            Society</strong></td>
    <td style="font-size: 10pt; vertical-align: top; height: 28px;" colspan="2">
        <asp:ListBox ID="fpslist" runat="server" Height="280px" Width="360px" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium"></asp:ListBox><br />
        <asp:Button ID="Button2" runat="server" Text="Remove FPS" Width="96px" /><strong style="vertical-align: top"></strong></td>
    <td align="left" style="vertical-align: top">
        <strong>Selected IssueCentre Under Lead Society<br />
            &nbsp;</strong><asp:ListBox ID="lst_issue" runat="server" Height="150px" Width="271px" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium"></asp:ListBox><br /><asp:Button ID="removeIC" runat="server" Text="Remove IssueCentre" Width="147px" /><br />
        <br />
        <br />
        <br />
        <asp:Button ID="update" runat="server" Text="Save" Height="32px" Width="80px" ValidationGroup="1" Visible="False" /><asp:Button ID="Button1" runat="server" Text="Save" Height="32px" Width="80px" ValidationGroup="1" /></td>
    </tr>
    
    
    </table>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Nic-Logo_blue1.bmp" /></td>
            </tr>
            <tr>
                <td style="width: 779px">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp; &nbsp;
        &nbsp; &nbsp;
        <br />
        <br />
        <br />
        <br />
                    </div>
    </form>
</body>
</html>
>
