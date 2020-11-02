<%@ Page Language="C#" MasterPageFile="~/MasterPage/HOFinance_master.master" AutoEventWireup="true" CodeFile="Finance_RiceDCP_Correction.aspx.cs" Inherits="State_Finance_RiceDCP_Correction" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type ="text/javascript" language ="javascript" >
    function CheckIsNumeric(tx)
    {
        var AsciiCode = event.keyCode;
        var txt=tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3=txt2*1;
       
        if ((AsciiCode < 46) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('कृपया संख्या अंको में भरें.');
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
                alert('दशमलव एक ही बार आ सकता है');
                event.cancelBubble = true;
                event.returnValue = false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 2)  
            {
                alert("दशमलव के बाद 2 अंक ही भरे");
                event.cancelBubble = true;
                event.returnValue = false;
            }
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
                <b>जिले की कुल चावल की स्टॉक की जानकारी संशोधित करे</b></td>
        </tr>
        <tr>
            <td colspan="2" 
                style="color: #003366; font-size: medium; background-color: #FFCC66; height: 1px;">
                <b>Correction of RICE DCP RMS</b></td>
        </tr>
        <tr>
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 415px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                जिला चुनें (Select District)</td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="200px">
                </asp:DropDownList></td>
        </tr>
        
        <tr>
            <td style="border-color: #999966; border-width: thin; width: 415px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                वर्ष चुनें (Select Year)</td>
            <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlyear" runat="server" Height="25px" Width="200px">
                    <asp:ListItem Value="0">2013-14</asp:ListItem>
                    <asp:ListItem Value="1">2012-13</asp:ListItem>
                    <asp:ListItem Value="2">2011-12</asp:ListItem>
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
                <asp:DropDownList ID="ddlTYPE" runat="server" Height="25px" Width="200px" OnSelectedIndexChanged="ddlTYPE_SelectedIndexChanged" AutoPostBack="True">
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
                <asp:Button ID="btnsave" runat="server" Text="सुरक्षित करें" OnClick="btnsave_Click" /></td>
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

