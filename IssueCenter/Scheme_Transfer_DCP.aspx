<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Scheme_Transfer_DCP.aspx.cs" Inherits="IssueCenter_Scheme_Transfer" Title="Scheme Transfer " %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> 
    <asp:Label ID="Label10" runat="server" Text="Transfer DCP Stock to Scheme(APL/BPL/AAY)"></asp:Label>&nbsp;</div>
<div id="transcheme">
<div id ="ronewmargin">

 <table cellpadding ="0" cellspacing ="0" border ="0" >
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp; &nbsp;
         </td>
         <td align="center" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; color: midnightblue; border-bottom: 1px solid; font-style: italic">
             From DCP</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             &nbsp; &nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="Label9" runat="server" Text="Godown No."></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlgodowndcp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodowndcp_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
             <asp:Label ID="Label3" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      <asp:DropDownList ID="ddlcomdty_dcp" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcomdty_dcp_SelectedIndexChanged" AutoPostBack="True"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginro" style="width: 107px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
             &nbsp;<asp:Label ID="Label4" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid">
             &nbsp;
             <asp:DropDownList ID="ddldcpscheme" runat="server"  Width="143px" OnSelectedIndexChanged="ddldcpscheme_SelectedIndexChanged" AutoPostBack="True" >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="lbldcp" runat="server" Text="Current Balance" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtdcpqty" runat="server" Visible="False" Width="93px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             <asp:Label ID="dcpunit" runat="server" Text="Qtls." Visible="False"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td align="center" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; color: midnightblue; font-style: italic;">
             To (APL/BPL/AAY)</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             &nbsp; &nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp; &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="center" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; color: midnightblue; border-bottom: 1px solid; font-style: italic">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
         <td align="center" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; color: midnightblue; border-bottom: 1px solid; font-style: italic">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="Label5" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlcomdty" runat="server"  Width="152px"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             <asp:Label ID="Label6" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlscheme" runat="server"  Width="155px" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="True" >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;"  >
      </td>
  <td  align="left" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      &nbsp;<asp:Label ID="lblbalqty" runat="server" Text="Current Balance" Visible="False"></asp:Label></td>
     <td class ="tdmarginro" style="width: 107px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" >
         &nbsp;<asp:TextBox ID="txtbalqty" runat="server" Visible="False" Width="93px"></asp:TextBox></td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid" >
         &nbsp;<asp:Label ID="Label2" runat="server" Text="Qtls." Visible="False"></asp:Label>&nbsp;</td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="Label7" runat="server" Text="No. of Bags"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtbags" runat="server" Width="93px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
             <asp:Label ID="Label8" runat="server" Text="Quantity"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtqty" runat="server" Width="103px" ></asp:TextBox>Qtls.</td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" ></td>
 <td style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
     &nbsp;</td>
     <td class ="tdmarginro" style="width: 107px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
         &nbsp;</td>
  <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid"> &nbsp;</td>

  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Button ID="btnsubmit" runat="server" Text="Transfer" OnClick="btnsubmit_Click" ValidationGroup="1" Width="149px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 107px; border-bottom: 1px solid">
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="83px" />&nbsp;</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;</td>
     </tr>
     <tr>
         <td align="left" colspan="4" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label></td>
         <td style="height: 16px">
         </td>
         <td style="height: 16px">
         </td>
         <td style="height: 16px">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="4" rowspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
     </td>
       <td> </td>
       <td> </td>
       </tr>
     <tr>
         <td>
         </td>
         <td>
         </td>
     </tr>
 </table>

</div>
</div>
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
</asp:Content>

