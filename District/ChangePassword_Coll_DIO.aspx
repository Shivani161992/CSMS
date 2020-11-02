<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Collector_DIO.master" AutoEventWireup="true" CodeFile="ChangePassword_Coll_DIO.aspx.cs" Inherits="District_ChangePassword_Coll_DIO" %>
<%@ MasterType VirtualPath="~/MasterPage/Collector_DIO.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function CheckPassword(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;

            if (AsciiCode == 59 || AsciiCode == 32) {
                alert('Sorry ! Semicolon(;)/Blank Space Not Allowed.');
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }
</script>
<script language = "Javascript">
    /**
     * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
     */

    var maxL = 10;
    var bName = navigator.appName;
    function taLimit(taObj) {
        if (taObj.value.length == maxL) return false;
        return true;
    }

    function taCount(taObj, Cnt) {
        objCnt = createObject(Cnt);
        objVal = taObj.value;
        if (objVal.length > maxL) objVal = objVal.substring(0, maxL);
        if (objCnt) {
            if (bName == "Netscape") {
                objCnt.textContent = maxL - objVal.length;
            }
            else { objCnt.innerText = maxL - objVal.length; }
        }
        return true;
    }
    function createObject(objId) {
        if (document.getElementById) return document.getElementById(objId);
        else if (document.layers) return eval("document." + objId);
        else if (document.all) return eval("document.all." + objId);
        else return eval("document." + objId);
    }
</script>
<div id="transCP" align="center">
<center >
<div id="transCPm"> 
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 302px;" id="">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(../Images/imgg2.jpg);" align="center">
                    <asp:Label ID="Label5" runat="server" Text="Change Password"></asp:Label></td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;"  width="300" >
    <tr>
        <td colspan="2" style="height: 21px">
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="color: red">
            You have <b><span id="myCounter">10</span></b> characters remaining
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label2" runat="server" Text="Old Password"></asp:Label></td>
        <td align="left">
    <asp:TextBox ID="txtoldpwd" runat="server" Width="123px" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left" >
            <asp:Label ID="Label3" runat="server" Text="New Password"></asp:Label></td>
        <td align="left" >
            <asp:TextBox ID="txtnewpwd" runat="server" Width="120px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnewpwd"
                ErrorMessage="New Password Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
    </tr>
<tr>
<td align="left">
    <asp:Label ID="Label4" runat="server" Text="Confirm Password"></asp:Label></td>
<td align="left">
    <asp:TextBox ID="txtconfpwd" runat="server" Width="119px" TextMode="Password"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpwd"
        ControlToValidate="txtconfpwd" ErrorMessage="Password not Match" ValidationGroup="1">*</asp:CompareValidator></td>
</tr>
<tr>
<td></td>
<td align="left">
    </td>
</tr>
    <tr>
        <td>
            &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnadd" runat="server" Text="Save" OnClick="btnadd_Click" Width="74px" ValidationGroup="1" /></td>
        <td align="left">
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="135px" OnClick="btnclose_Click" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="263px" />
        </td>
    </tr>
<tr>
<td>
    </td>
<td align="left">
    &nbsp;</td>
</tr>
</table>
</div> 
</center>
</div>
</asp:Content>