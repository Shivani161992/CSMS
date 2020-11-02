<%@ Page Language="C#" MasterPageFile="~/Master_Hindi/IssueMaster.master" AutoEventWireup="true" CodeFile="Opening_Balances_Hindi.aspx.cs" Inherits="IssueCenter_Opening_Balances" Title="Opening Balance Of Stock " %>
 
<%@ Register Assembly="CustomControlFreak" Namespace="CustomControlFreak" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> 
    <span style="font-family: Mangal">प्ररंभिक अवशेष</span>
</div>
<div id ="ronewmargin">
<center>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 569px;">
     <tr>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; font-size: 10pt; background-color: white; width: 141px;" >
             &nbsp;<asp:TextBox ID="txtspos" runat="server" Width="108px" Visible="False"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
             <span lang="HI" style="font-size: 12pt; font-family: Mangal; mso-ascii-font-family: 'Times New Roman';
                 mso-fareast-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman';
                 mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">प्ररंभिक
                 अवशेष <span lang="HI" style="font-size: 12pt; font-family: Mangal; mso-ascii-font-family: 'Times New Roman';
                     mso-fareast-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman';
                     mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">दिनाँक</span></span></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
         <cc1:DaintyDate ID="effective_from" runat="server" FormatType="DDMMYYYY" />
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; height: 25px; background-color: white">
             <asp:Label ID="Label2" runat="server" Text="प्ररंभिक अवशेष का माह "></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; height: 25px; background-color: white">
            <asp:DropDownList ID="ddlopeningmonth" runat="server"  Width ="161px">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; height: 25px; background-color: white">
         </td>
     </tr>
 <tr>
 <td class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; height: 25px; width: 141px;">
     <asp:Label ID="Label3" runat="server" Text="आगमन का स्त्रोत"></asp:Label></td>
 <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; height: 25px; width: 161px;">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="159px" >
        
     </asp:DropDownList>
 </td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; height: 25px; background-color: white">
     </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;">
             <asp:Label ID="Label4" runat="server" Text="वस्तु"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="161px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;">
         <asp:Label ID="Label5" runat="server" Text="स्कीम"></asp:Label></td>
  <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;"> <asp:DropDownList ID="ddlscheme" runat="server"  Width="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList>
             </td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; background-color: white">
     </td>
   
                                       
  </tr>
  <tr>
 <td align="left" class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;" >  
     <asp:Label ID="Label6" runat="server" Text="श्रेणी"></asp:Label></td>
 <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">  
      <asp:DropDownList ID="ddlcategory" runat="server" Width="161px" >
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
      <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
          border-bottom: 1px solid; background-color: white">
      </td>
 </tr>
 <tr>
  <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;" >
      <asp:Label ID="Label7" runat="server" Text="फसल वर्ष "></asp:Label></td>
  <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="161px" >
                                                <asp:ListItem Value="01" Selected="True">Crop year not indicated</asp:ListItem>
                                                <asp:ListItem Value="02">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="03">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="04">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="05">2006-2007</asp:ListItem>
                                                <asp:ListItem Value="06">2005-2006</asp:ListItem>
                                                <asp:ListItem Value="07">2004-2005</asp:ListItem>
                                                <asp:ListItem Value="08">2003-2004</asp:ListItem>
                                                <asp:ListItem Value="09">2002-2003</asp:ListItem>
                                                <asp:ListItem Value="10">2001-2002</asp:ListItem>
                                                <asp:ListItem Value="11">2000-2001</asp:ListItem>
                                            </asp:DropDownList></td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; background-color: white">
     </td>
      
 </tr>
 <tr>
 <td align="left" class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;">
     <asp:Label ID="Label8" runat="server" Text="गोदाम"></asp:Label></td>
 <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
     <asp:DropDownList ID="ddlissue" runat="server" Width ="161px">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList></td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; background-color: white">
     </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; background-color: white">
             <asp:Label ID="Label9" runat="server" Text="बोरो की संख्या"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; background-color: white">
             <asp:TextBox ID="txtbags" runat="server" Width="110px" MaxLength="5"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbags"
                 ErrorMessage="No. of Bags  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;" >
      <asp:Label ID="Label10" runat="server" Text="वजन"></asp:Label></td>
 <td  align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
     <asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="13" ></asp:TextBox>(Qtls)</td>
      <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
          border-bottom: 1px solid; background-color: white">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtqty"
              ErrorMessage=" Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>

  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; background-color: white">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; background-color: white">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
                                
                       
                                   
                             
   
       <tr>
       <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;" > </td>
       <td colspan ="1" align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;"><asp:Button ID="btnsubmit" runat="server" Text="सुरक्षित करें " OnClick="btnsubmit_Click" ValidationGroup="1" Width="79px" /> 
              
       <asp:Button ID="btnclose" runat="server" Text="बंद करे" OnClick="btnclose_Click" Width="74px" /> </td>
           <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
               border-left: 1px solid; border-bottom: 1px solid; background-color: white">
           </td>
       </tr>
     <tr>
         <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white;">
            
         </td>
         <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
     <tr>
         <td align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label></td>
         <td align="left" colspan="1" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="2" rowspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
       </td>
           <td align="left" colspan="1" rowspan="2" style="border-right: 1px solid; border-top: 1px solid;
               border-left: 1px solid; border-bottom: 1px solid; background-color: white">
           </td>
       </tr>
 </table>
 <div>
 <table cellpadding ="0" cellspacing ="0" border ="0" >
  
  <tr>
  <td>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="6" PagerSettings-Visible ="True" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnPageIndexChanged="GridView1_PageIndexChanged" Width="568px"  >
                 <Columns>
                     <asp:BoundField DataField="Source_Name" HeaderText="आगमन का स्त्रोत " SortExpression="Source_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Commodity_Name" HeaderText="वस्तु" SortExpression="Commodity_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Scheme_Name" HeaderText="स्कीम" SortExpression="Scheme_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quantity" HeaderText="प्ररंभिक अवशेष " SortExpression="Quantity" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Balance" HeaderText="कॅरंट अवशेष" SortExpression="Current_Balance">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Month" HeaderText="माह" SortExpression="Month" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="Tan" />
                 <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                 <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="Tan" Font-Bold="True" />
                 <AlternatingRowStyle BackColor="PaleGoldenrod" />
             </asp:GridView>
  
  </td> 
  </tr> 
  </table>
 </div>
  <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Firstbutton_Click"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Prevbutton_Click" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Nextbutton_Click" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Lastbutton_Click" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
</center>
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

