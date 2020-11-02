<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Scheme_Transfer.aspx.cs" Inherits="District_Scheme_Transfer" Title="Scheme Transfer" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




 <table cellpadding ="0" cellspacing ="0" border ="0" >
     <tr>
         <td colspan="4" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; font-weight: bold; color: black; background-color: #cccccc;" align="center">
             Scheme Transfer</td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: lightslategray; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; color: white;">
             &nbsp;&nbsp;
         </td>
         <td align="center" style="font-size: 10pt; position: static; background-color: lightslategray; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; color: white;" colspan="2">
             Source Information&nbsp; &nbsp;
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: lightslategray; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; color: white;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label6" runat="server" Text="District Name"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddldistrict" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistric_SelectedIndexChanged" Enabled="False" Font-Bold="False" Font-Italic="True" ForeColor="Navy"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label7" runat="server" Text="Issue Center "></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlissue" runat="server"  Width="152px" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label3" runat="server" Text="Source of Arrival"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label5" runat="server" Text="Godown No."></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label8" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack="True"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp;<asp:Label ID="Label9" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp;<asp:DropDownList ID="ddlscheme" runat="server"  Width="146px" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="True" >
          <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
      </asp:DropDownList></td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"  >
      &nbsp;
      </td>
  <td  align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
      &nbsp;<asp:Label ID="lblbalqty" runat="server" Text="Balance Qty. " Visible="False" Width="117px"></asp:Label>&nbsp;
  </td>
     <td  style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
         &nbsp;<asp:TextBox ID="txtbalqty" runat="server" Width="93px" Visible="False"></asp:TextBox>
         <asp:Label ID="Label2" runat="server" Text="Qtls." Visible="False"></asp:Label>&nbsp;</td>
     <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
         &nbsp;&nbsp;</td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: lightslategray; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; color: white;">
         </td>
         <td align="center" style="font-size: 10pt; position: static; background-color: lightslategray; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; color: white;" colspan="2">
             <strong>
             Destination Information</strong></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: lightslategray; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; color: white;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             District Name</td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddldistrictd" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrictd_SelectedIndexChanged" Enabled="False"  >
                </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Issue Center
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlissued" runat="server"  Width="152px" OnSelectedIndexChanged="ddlissued_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label10" runat="server" Text="Source of Arrival" Width="116px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlarrivalsource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlarrivalsource_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Commodity</td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlcommodityd" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcommodityd_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Scheme</td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlschemetrs" runat="server"  Width="156px" AutoPostBack="True" OnSelectedIndexChanged="ddlschemetrs_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         &nbsp;&nbsp;
     </td>
  <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
      <asp:Label ID="lblbalqtyd" runat="server" Text="Balance Qty." Visible="False"></asp:Label>
      &nbsp;
  </td>
  <td class ="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
      &nbsp;<asp:TextBox ID="txtbalqtyd" runat="server" Visible="False" Width="93px"></asp:TextBox>
     <asp:Label ID="Label4" runat="server" Text="Qtls." Visible="False"></asp:Label>&nbsp;
  </td>
 <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
     &nbsp; &nbsp;&nbsp;
 </td>
   
                                       
  </tr>
  <tr>
  <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left" >
      &nbsp;No of Bags.</td>
 <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
     <asp:TextBox ID="txtbags" runat="server" Width="107px"></asp:TextBox></td>
     <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         Quantity&nbsp;</td>
  <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"> &nbsp;<asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="13" ></asp:TextBox></td>

  </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label11" runat="server" Text="Date"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="tx_stdate" runat="server" Width="107px"></asp:TextBox>
              <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_tx_stdate'
	                      });
	          </script>
             </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp;&nbsp;
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Button ID="btnsubmit" runat="server" Text="Transfer " OnClick="btnsubmit_Click" ValidationGroup="1" Width="156px" /></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="147px" />&nbsp;</td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="4" rowspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
     </td>
       <td style="font-size: 10pt; position: static; background-color: #cfdcc8"> </td>
       <td style="font-size: 10pt; position: static; background-color: #cfdcc8"> </td>
       </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
     </tr>
 </table>

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


