<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="FIN_DistrictRiceAllotment.aspx.cs" Inherits="District_FIN_DistrictRiceAllotment" Title="Untitled Page" %>
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


    <table style="border: medium groove #996633; width: 779px; height: 601px">
        <tr>
            <td colspan="2" 
                
                style="height: 1px; color: #0033CC; font-family: Verdana; font-size: large; background-color: #999966;">
                <b>जिले की कुल चावल की आवंटन की जानकारी भरें</b></td>
        </tr>
        <tr>
            <td colspan="2" 
                style="color: #003366; font-size: medium; background-color: #FFCC66; height: 1px;">
                <b>Entry of RICE Allotment DCP RMS</b></td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #cccc66; height: 2px;">
             
                <span style="font-size: 11pt; color: midnightblue"><strong>Note:- एक बार प्रविष्टि हो जाने
                    पर संसोधन संभव नहीं हो सकेगा, कृपया ध्यान पूर्वक प्रविष्टि करें|</strong></span></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="11pt" OnClick="LinkButton1_Click">स्कीम अनुसार माह के  आवंटन कि जानकारी के लिए यहाँ क्लिक करें</asp:LinkButton></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="10pt" OnClick="LinkButton2_Click">BPL एवं APL कि जानकारी के लिए यहाँ क्लिक करें</asp:LinkButton></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                <asp:Label ID = "Label10" runat = "server" Text = "वर्ष चुनें (Select Year)"></asp:Label>
                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
            <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlyear" runat="server" Height="25px" Width="154px">
                    <asp:ListItem Value="0">2013-14</asp:ListItem>
                    <asp:ListItem Value="0">2012-13</asp:ListItem>
                    <asp:ListItem Value="0">2011-12</asp:ListItem>
                    <asp:ListItem Value="0">2010-11</asp:ListItem>
                   
                </asp:DropDownList></td>
        </tr>
        <tr style="font-size: 12pt">
           <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
              
               <asp:Label ID = "Label9" runat = "server" Text = "माह चुनें (Select Month)"></asp:Label></td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                 <asp:DropDownList ID="ddlmonth" runat="server" Height="25px" Width="152px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
     
        <tr style="font-size: 12pt">
         <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
             
           <asp:Label ID = "Label8" runat = "server" Text = "स्कीम/योजना चुनें (Select Scheme)" Visible = "false"> </asp:Label></td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlhead" runat="server" Height="25px" Width="214px" Visible = "false" >
                    
                </asp:DropDownList></td>
        </tr>
        
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                <asp:Label ID = "lbl1" runat = "server" Text = "माह का आवंटन(Monthly Allotment)" Visible = "false"> </asp:Label></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:TextBox ID="txtMonthAllot" runat="server" Width="200px" Visible = "false"></asp:TextBox></td>
        </tr>
               
                
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                 <asp:Label ID = "lbl2" runat = "server" Text = "एफ सी आई स्टॉक से वितरण(Sales Out of FCI Stock)" Visible = "false" > </asp:Label></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:TextBox ID="txtsalesofFCI" runat="server"  Width="200px" Visible = "false"></asp:TextBox></td>
        </tr>
        
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                 <asp:Label ID = "lbl3" runat = "server" Text = "Allotment for BPL out of APL" Visible = "false" > </asp:Label></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:TextBox ID="txtBPLAllot" runat="server" Width="200px" Visible = "false"></asp:TextBox></td>
        </tr>
        
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                 <asp:Label ID = "lbl4" runat = "server" Text = "Actual Distribution in BPL against APL" Width="240px" Visible = "false" ></asp:Label></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:TextBox ID="txtActualBPLallot" runat="server" Width="200px" Visible = "false"></asp:TextBox></td>
        </tr>
        
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px; text-align: center">
                 <asp:Label ID = "lbl5" runat = "server" Text = "Allotment for Senior Citizens (Out of BPL Allotment)" Visible = "false"> </asp:Label></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:TextBox ID="txtSeniourallot" runat="server"  Width="200px" Visible = "false"></asp:TextBox></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="border-top-width: thin; border-right: #999966 thin groove; border-left-width: thin;
                border-left-color: #999966; width: 458px; border-top-color: #999966; border-bottom: #999966 thin groove;
                height: 1px">
                 <asp:Label ID = "lbl6" runat = "server" Text = "Actual Distribution to Senior Citizens" Visible = "false" > </asp:Label></td>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: #999966;
                border-top-color: #999966; border-bottom: #999966 thin groove; height: 1px; border-right-width: thin;
                border-right-color: #999966">
                <asp:TextBox ID="txtActualSeniourAllot" runat="server"  Width="200px" Visible = "false"></asp:TextBox></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="width: 458px; background-color: #999966; height: 1px;">
                &nbsp;</td>
            <td style="background-color: #999966; height: 1px;">
                &nbsp;</td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="width: 458px; height: 1px;">
                <asp:Button ID="btnclose" runat="server" Text="बंद करें" OnClick="btnclose_Click" /></td>
            <td style="height: 1px">
                <asp:Button ID="btnsave" runat="server" Text="सुरक्षित करें" OnClick="btnsave_Click" />
                &nbsp; &nbsp;&nbsp; </td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="width: 458px">
            </td>
            <td>
            </td>
        </tr>
    </table>


</td>

</tr>

</table>


</center>

</asp:Content>

