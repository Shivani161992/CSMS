<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DistRice_StockEntry_DM.aspx.cs" Inherits="District_RiceDCP_RMS_entry" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type ="text/javascript" language ="javascript" >
    function IsNumericMobile(tx)
    {
        var AsciiCode = event.keyCode;
        var txt=tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3=txt2*1;
              
        if ((AsciiCode < 47) || (AsciiCode > 57 ))
        {
            alert('कृपया संख्या अंको में भरें.');
            event.cancelBubble = true;
            event.returnValue = false;
        }
    }
    
 </script>

<center>

<table>
<tr>
<td>


    <table style="border: medium groove #996633; width: 750px; height: 509px">
        <tr>
            <td colspan="2" 
                
                style="height: 1px; color: #0033CC; font-family: Verdana; font-size: large; background-color: #999966;">
                <b>जिले की कुल चावल की स्टॉक की जानकारी भरें</b></td>
        </tr>
        <tr>
            <td colspan="2" 
                style="color: #003366; font-size: medium; background-color: #FFCC66; height: 1px;">
                <b>Entry of RICE DCP RMS</b></td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #cccc66; height: 2px;">
                <span style="font-size: 11pt; color: midnightblue"><strong>Note:- एक बार प्रविष्टि हो जाने
                    पर संसोधन संभव नहीं हो सकेगा, कृपया ध्यान पूर्वक प्रविष्टि करें|</strong></span></td>
        </tr>
        <tr>
            <td style="border-color: #999966; border-width: thin; width: 415px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                वर्ष चुनें (Select Year)</td>
            <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlyear" runat="server" Height="25px" Width="200px">
                    <asp:ListItem Value="0">2013-14</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
           <td style="border-color: #999966; border-width: thin; width: 415px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                माह चुनें Select Month)</td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlmonth" runat="server" Height="25px" Width="200px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
         <td style="border-color: #999966; border-width: thin; width: 415px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                हेड चुनें (Select Head)</td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlhead" runat="server" Height="25px" Width="220px">
                    
                </asp:DropDownList></td>
        </tr>
               
        <tr>
         <td style="border-color: #999966; border-width: thin; width: 415px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                प्रकार चुनें</td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlTYPE" runat="server" Height="25px" Width="200px">
                    <asp:ListItem Value="0">--चुनें---</asp:ListItem>
                    <asp:ListItem Value="1">CMR</asp:ListItem>
                    <asp:ListItem Value="2">LEVY</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
               
        <tr>
             <td style="border-color: #999966; border-width: thin; width: 415px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                मात्रा भरें MT में (Quantity in MT)</td>
            <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:TextBox ID="txtqty" runat="server" Height="19px" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 415px; background-color: #999966; height: 1px;">
                &nbsp;</td>
            <td style="background-color: #999966; height: 1px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 415px; height: 1px;">
                <asp:Button ID="btnclose" runat="server" Text="बंद करें" OnClick="btnclose_Click" /></td>
            <td style="height: 1px">
                <asp:Button ID="btnsave" runat="server" Text="सुरक्षित करें" OnClick="btnsave_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnprint" runat="server" Text="Print"  Width="73px" OnClick="btnprint_Click" /></td>
        </tr>
        <tr>
            <td style="width: 415px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 415px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 415px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 415px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 415px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>


</td>

</tr>

</table>


</center>

</asp:Content>

