<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Fin_DistRice_StockEntry.aspx.cs" Inherits="District_RiceDCP_RMS_entry" Title="Untitled Page" %>
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


    <table style="border: medium groove #996633; width: 779px; height: 391px">
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
            <td colspan="2" style="background-color: #cccc66; height: 1px;">
             
                <span style="font-size: 11pt; color: midnightblue"><strong>Note:- एक बार प्रविष्टि हो जाने
                    पर संसोधन संभव नहीं हो सकेगा, कृपया ध्यान पूर्वक प्रविष्टि करें|</strong></span></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                <asp:Label ID = "Label10" runat = "server" Text = "वर्ष चुनें (Select Year)"> </asp:Label></td>
            <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlyear" runat="server" Height="25px" Width="154px">
                    <asp:ListItem Value="0">2013-14</asp:ListItem>
                    <asp:ListItem Value="1">2012-13</asp:ListItem>
                    <asp:ListItem Value="2">2011-12</asp:ListItem>
                    <asp:ListItem Value="2">2010-11</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr style="font-size: 12pt">
           <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
              
               <asp:Label ID = "Label9" runat = "server" Text = "माह चुनें (Select Month)"> </asp:Label></td>
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
             
           <asp:Label ID = "Label8" runat = "server" Text = "स्कीम/योजना चुनें (Select Scheme)"> </asp:Label></td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlhead" runat="server" Height="25px" Width="220px" OnSelectedIndexChanged="ddlhead_SelectedIndexChanged" AutoPostBack = "true" >
                    
                </asp:DropDownList></td>
        </tr>
               
        <tr style="font-size: 12pt">
         <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
              <asp:Label ID = "Label7" runat = "server" Text = "प्रकार चुनें (select Type)"> </asp:Label></td>
           <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:DropDownList ID="ddlTYPE" runat="server" Height="25px" Width="200px" >
                    <asp:ListItem Value="0">--चुनें---</asp:ListItem>
                    <asp:ListItem Value="1">CMR</asp:ListItem>
                    <asp:ListItem Value="2">LEVY</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        
        
               
        <tr style="font-size: 12pt">
             <td style="border-color: #999966; border-width: thin; width: 458px; border-right-style: groove; border-bottom-style: groove; height: 1px;">
                <asp:Label ID = "Label6" runat = "server" Text = "मात्रा भरें MT में (Quantity in MT)" > </asp:Label></td>
            <td style="border-color: #999966; border-width: thin; border-bottom-style: groove; height: 1px;">
                <asp:TextBox ID="txtqty" runat="server" Height="19px" Width="200px"></asp:TextBox></td>
        </tr>
        
        
        
        
        
        
        
        
        
        <tr style="font-size: 12pt">
            <td colspan="2" style="height: 1px" >
                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Blue" Width="242px" NavigateUrl="~/District/FIN_DistrictRiceAllotment.aspx">Click here to Enter allotment of Month</asp:HyperLink></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="width: 458px; height: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Button ID="btnclose" runat="server" Text="बंद करें" OnClick="btnclose_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Text="NEW" OnClick="Button1_Click" /></td>
            <td style="height: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Button ID="btnsave" runat="server" Text="सुरक्षित करें" OnClick="btnsave_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnprint" runat="server" Text="Print"  Width="73px" OnClick="btnprint_Click" /></td>
        </tr>
        <tr style="font-size: 12pt">
            <td style="width: 458px; height: 1px;">
            </td>
            <td style="height: 1px">
            </td>
        </tr>
    </table>


</td>

</tr>

</table>


</center>

</asp:Content>

