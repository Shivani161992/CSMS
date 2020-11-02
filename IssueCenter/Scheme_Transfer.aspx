<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Scheme_Transfer.aspx.cs" Inherits="IssueCenter_Scheme_Transfer" Title="Scheme Transfer" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="transcheme">
<div id ="ronewmargin">
 <table cellpadding ="0" cellspacing ="0" border ="0" >
     <tr>
         <td align="center"  colspan="4" style="border-right: darkseagreen 1px solid;
             border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: #cccccc;">
    <asp:Label ID="lblschemet" runat="server" Text="Scheme Transfer" Width="216px" Font-Bold="True"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: lightslategray;">
             &nbsp;&nbsp;
         </td>
         <td colspan="2" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: lightslategray" align="center">
             <asp:Label ID="Label12" runat="server" Text="Source Information" Width="138px" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;
             &nbsp;
         </td>
         <td align="left" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: lightslategray;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblDistrictName" runat="server" Text="District Name"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldistrict" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistric_SelectedIndexChanged" Enabled="False" Font-Bold="False" Font-Italic="True" ForeColor="Navy"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 166px;">
             <asp:Label ID="lblNameDepot" runat="server" Text="Issue Center "></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlissue" runat="server"  Width="152px" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged" Enabled="False"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblsourcefrom" runat="server" Text="Source of Arrival" Width="114px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 166px;">
             <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack="True"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 166px;">
             &nbsp;<asp:Label ID="lblScheme" runat="server" Text="Scheme" Width="70px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             &nbsp;<asp:DropDownList ID="ddlscheme" runat="server"  Width="146px" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="True" >
          <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
      </asp:DropDownList></td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;"  >
      &nbsp;
      </td>
  <td  class="tdmarginro"  align="right" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="lblbalqty" runat="server" Text="Balance Qty. " Visible="False" Width="90px"></asp:Label>&nbsp;
  </td>
     <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 166px;" >
         &nbsp;<asp:TextBox ID="txtbalqty" runat="server" Width="113px" Visible="False"></asp:TextBox>
         &nbsp;</td>
     <td class="tdmarginro" align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
         &nbsp;<asp:Label ID="lblqtls" runat="server" Text="Qtls." Visible="False"></asp:Label></td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 166px;">
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: lightslategray;">
             &nbsp;&nbsp;
         </td>
         <td align="center" colspan="2" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid;
             border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: lightslategray">
             &nbsp; &nbsp; &nbsp; &nbsp;
             <asp:Label ID="Label13" runat="server" Text="Destination Information" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;
             &nbsp;
         </td>
         <td align="left" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid; background-color: lightslategray;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="lblDistrictName1" runat="server" Text="District Name"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddldistrictd" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrictd_SelectedIndexChanged" Enabled="False"  >
                </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
             <asp:Label ID="lblNameDepot1" runat="server" Text="Issue Center "></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlissued" runat="server"  Width="152px" OnSelectedIndexChanged="ddlissued_SelectedIndexChanged" Enabled="False"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="lblsourceto" runat="server" Text="Source of Arrival" Width="111px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlarrivalsource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlarrivalsource_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
         </td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="lblCommodity1" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlcommodityd" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcommodityd_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
             <asp:Label ID="lblScheme1" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlschemetrs" runat="server"  Width="156px" AutoPostBack="True" OnSelectedIndexChanged="ddlschemetrs_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         &nbsp;&nbsp;
     </td>
  <td  class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="right">
      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
      <asp:Label ID="lblbalqtyd" runat="server" Text="Balance Qty." Visible="False"></asp:Label>
      &nbsp;
  </td>
  <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
      &nbsp;<asp:TextBox ID="txtbalqtyd" runat="server" Visible="False" Width="114px"></asp:TextBox>
      &nbsp;
  </td>
 <td  class="tdmarginro" align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
     <asp:Label ID="lblqtls1" runat="server" Text="Qtls." Visible="False"></asp:Label></td>
   
                                       
  </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
      <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags."></asp:Label></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
     <asp:TextBox ID="txtbags" runat="server" Width="107px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtbags"
         ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
         <asp:Label ID="lblQuantity" runat="server" Text="Quantity "></asp:Label></td>
  <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"> &nbsp;<asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="13" ></asp:TextBox></td>

  </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label2" runat="server" Text="Transfer Date" Width="107px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="tx_stdate" runat="server" Width="107px"></asp:TextBox>
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_tx_stdate'
	                      });
	          </script>

             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_stdate"
                 ErrorMessage="Scheme Transfer Date" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
             
             </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp;&nbsp;
         </td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Button ID="btnsubmit" runat="server" Text="Transfer " OnClick="btnsubmit_Click" ValidationGroup="1" Width="152px" /></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 166px;">
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="158px" />&nbsp;</td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td align="left" colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="4" rowspan="2" style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
     </td>
       <td style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid"> </td>
       <td style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid"> </td>
       </tr>
     <tr>
         <td style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid">
         </td>
         <td style="border-right: darkseagreen 1px solid; border-top: darkseagreen 1px solid; border-left: darkseagreen 1px solid; border-bottom: darkseagreen 1px solid">
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


