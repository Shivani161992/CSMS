<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="RR_Entry.aspx.cs" Inherits="IssueCenter_RR_Entry" Title="R.R. Entry" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
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
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 594px;" >
     <tr>
         <td  style="height: 14px; background-color: #cccccc;" colspan="4" align="center">
             
                 <asp:Label ID="lbldepositstock" runat="server" Text="Deposition Of Stock" Width="139px"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: lightslategray; width: 101px;" align="left">
         </td>
         <td  colspan="2" style="color: white; background-color: lightslategray;" align="center">
             <strong>Receipt Details (R.R.)</strong></td>
         <td class="tdmarginro" style="background-color: lightslategray" align="left">
         </td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" colspan="4" align="left">
             <asp:Label ID="Label1" runat="server" Visible="False" Width="80px"></asp:Label></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; height: 33px; background-color: #ffff99; width: 101px;" align="left">
             <asp:Label ID="lblChallanNumber" runat="server" Text="Select Challan No." Width="98px"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; height: 33px; background-color: #ffff99; width: 91px;" align="left">
             <asp:DropDownList ID="ddlchallan" runat="server"  Width ="173px" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td  style="font-size: 10pt; position: static; height: 33px; background-color: #ffff99; width: 91px;" align="left">
             <asp:Label ID="lblrackno1" runat="server" Text="Rack No."></asp:Label></td>
         <td  style="font-size: 10pt; position: static; height: 33px; background-color: #ffff99; width: 91px;" align="left">
             <asp:DropDownList ID="ddlrackno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged"
                 Width="227px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #ffff99; width: 101px;" align="left">
             <asp:Label ID="Lblsendingdistrict" runat="server" Text="Sending District" Width="94px"></asp:Label></td>
         <td style="font-size: 10pt; position: static; background-color: #ffff99; width: 91px;" align="left"><asp:DropDownList ID="ddlDistrict" runat="server"  Width ="173px" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged" Enabled="False">
         </asp:DropDownList></td>
         <td  style="font-size: 10pt; position: static; background-color: #ffff99; width: 91px;" align="left">
       <asp:Label ID="lblrailhead" runat="server" Text="Sending Rail Head" Width="103px"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #ffff99; width: 91px;" align="left">
         <asp:DropDownList ID="ddlsenrailhead" runat="server"  Width ="227px" AutoPostBack="True" OnSelectedIndexChanged="ddlsenrailhead_SelectedIndexChanged" Enabled="False">
                            
                        </asp:DropDownList></td>
     </tr>
   <tr>
   <td  style="font-size: 10pt; position: static; background-color: #ffff99; width: 101px;" align="left"> 
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
     <td  style="font-size: 10pt; position: static; background-color: #ffff99;" align="left">
             <asp:DropDownList ID="ddlcomdty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"
                 Width="153px" Enabled="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
       <td  style="font-size: 10pt; position: static; background-color: #ffff99;" align="left">
             <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #ffff99" align="left"> &nbsp;<asp:DropDownList ID="ddlscheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged"
                 Width="176px" Enabled="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
   </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; width: 101px;" align="left">
             </td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99;" align="left">
             </td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99;" align="left">
           <asp:Label ID="lblChallanNumber1" runat="server" Text="TruckChallan No." Width="95px"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99" align="left">
             <asp:TextBox ID="txtchallan" runat="server" Width="146px" ValidationGroup="1"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtchallan"
                 ErrorMessage="Challan No. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; width: 101px;" align="left">
         </td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99;" align="left">
             <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99;" align="left">
             <asp:Label ID="lblsch" runat="server" Visible="False" Width="2px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #66cc99" align="left">
         </td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; width: 101px;" align="left">
             <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No." Width="64px"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99;" align="left">
             <asp:TextBox ID="txttruckno" runat="server" Width="146px" OnTextChanged="txttruckno_TextChanged"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttruckno"
                 ErrorMessage="Truck No. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99;" align="left">
             <asp:Label ID="lblchallandate" runat="server" Text="TC Date"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99" align="left">
                  
         <asp:TextBox ID="DaintyDate3" runat="server" Width="122px"></asp:TextBox>
                      <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script>
             <br />
             <asp:TextBox ID="txtchalldt" runat="server" Width="121px" Visible="False"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; height: 25px; width: 101px;" align="left">
             <asp:Label ID="lblTrans" runat="server" Text="Transporter"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; height: 25px; width: 91px;" align="left">
             <asp:DropDownList ID="ddltransporter" runat="server" Width="186px">
             </asp:DropDownList></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; width: 91px; height: 25px;" align="left">
             <asp:Label ID="lblcropyear" runat="server" Text="Crop Year"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #66cc99; height: 25px; width: 91px;" align="left">
         
         <asp:DropDownList ID="ddlcropyear" runat="server" Width="147px" Height="20px">
         
             <asp:ListItem Value="0">--select--</asp:ListItem>
             <asp:ListItem Value="1">2014-2015</asp:ListItem>
             <asp:ListItem Value="2">2013-2014</asp:ListItem>
             <asp:ListItem Value="3">2012-2013</asp:ListItem>
             <asp:ListItem Value="4">2011-2012</asp:ListItem>
         </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 101px;" align="left">
             <asp:Label ID="lblIssuedBags" runat="server" Text="Dispatched Bags"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:TextBox ID="txtdisbag" runat="server" Width="146px"></asp:TextBox></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 91px;" align="left">
             <asp:Label ID="lblDispatchQty" runat="server" Text="Dispatched Qty."></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc" align="left">
             <asp:TextBox ID="txtdisqty" runat="server" Width="146px"></asp:TextBox>Qtls.</td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 101px;" align="left">
             <asp:Label ID="lbltotalReceivedBags" runat="server" Text="Received Bags"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:TextBox ID="txtrecdbaf" runat="server" Width="146px"></asp:TextBox></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 91px;" align="left">
             <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Received Qty."></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:TextBox ID="txtrecdqty" runat="server" Width="146px"></asp:TextBox>Qtls.</td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 101px;" align="left">
             <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 91px;" align="left">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="144px"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 101px;" align="left">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap."></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="145px"></asp:TextBox></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 91px;" align="left">
             <asp:Label ID="lblAvailable" runat="server" Text="Available"></asp:Label></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="143px"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cc99cc;" align="left">
         </td>
         <td  style="font-size: 10pt; position: static; background-color: #cc99cc;" align="left">
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cc99cc;" align="left">
         </td>
         <td  style="font-size: 10pt; position: static; background-color: #cc99cc;" align="left">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1"
                 Width="190px" ShowMessageBox="True" ShowSummary="False" />
         </td>
     </tr>
 </table>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 602px;" id="Table1" >
      <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td align="right" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
         <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="128px" OnClick="btnsubmit_Click" ValidationGroup="1" /></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcc8;">
         <asp:Button ID="btnclose" runat="server" Text="Close" Width="127px" OnClick="btnclose_Click" /></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
     </tr>
     </table> 
</asp:Content>

