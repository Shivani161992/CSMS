<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Scheme_Transfer.aspx.cs" Inherits="State_Scheme_Transfer" Title="Scheme Transfer" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> Scheme Transfer</div>
<div id="transcheme">
<div id ="ronewmargin">

 <table cellpadding ="0" cellspacing ="0" border ="0" >
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; font-weight: bold;
             border-left: 1px solid; width: 154px; color: #003333; border-bottom: 1px solid;
             font-style: italic">
             Source District</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
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
             District Name</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddldistrict" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistric_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Issue Center
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlissue" runat="server"  Width="152px" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
             Commodity</td>
         <td align="left" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="152px"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
             &nbsp;Scheme</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid">
             &nbsp;<asp:DropDownList ID="ddlscheme" runat="server"  Width="146px" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="True" >
          <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
      </asp:DropDownList></td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;"  >
      &nbsp;
      </td>
  <td  align="left" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      &nbsp;<asp:Label ID="lblbalqty" runat="server" Text="Balance Qty. " Visible="False" Width="117px"></asp:Label>&nbsp;
  </td>
     <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" >
         &nbsp;<asp:TextBox ID="txtbalqty" runat="server" Width="93px" Visible="False"></asp:TextBox>
         <asp:Label ID="Label2" runat="server" Text="Qtls." Visible="False"></asp:Label>&nbsp;</td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid" >
         &nbsp;&nbsp;</td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; background-color: #006699">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; background-color: #006699">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; background-color: #006699">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; background-color: #006699">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; font-weight: bold;
             border-left: 1px solid; width: 154px; color: #003333; border-bottom: 1px solid;
             font-style: italic">
             Destination District</td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             District Name</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddldistrictd" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrictd_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Issue Center
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlissued" runat="server"  Width="152px"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Commodity</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlcommodityd" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcommodityd_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Scheme</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlschemetrs" runat="server"  Width="156px" AutoPostBack="True" OnSelectedIndexChanged="ddlschemetrs_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
         &nbsp;&nbsp;
     </td>
  <td style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
      <asp:Label ID="lblbalqtyd" runat="server" Text="Balance Qty." Visible="False"></asp:Label>
      &nbsp;
  </td>
  <td class ="tdmarginddl" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      &nbsp;<asp:TextBox ID="txtbalqtyd" runat="server" Visible="False" Width="93px"></asp:TextBox>
     <asp:Label ID="Label4" runat="server" Text="Qtls." Visible="False"></asp:Label>&nbsp;
  </td>
 <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid;">
     &nbsp; &nbsp;&nbsp;
 </td>
   
                                       
  </tr>
  <tr>
  <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" >Quantity&nbsp;
  </td>
 <td style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
     &nbsp;<asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="13" ></asp:TextBox>Qtls.</td>
     <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
         &nbsp; &nbsp;
     </td>
  <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid"> &nbsp; &nbsp;
  </td>

  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Button ID="btnsubmit" runat="server" Text="Transfer " OnClick="btnsubmit_Click" ValidationGroup="1" Width="143px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="147px" />&nbsp;</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td align="left" colspan="4" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
         </td>
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


