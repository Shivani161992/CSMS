<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Edit_Opening_Balances.aspx.cs" Inherits="Admin_Edit_Opening_Balances" Title="Opening Balance Of Stock " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> 
    <asp:Label ID="lblopendetails" runat="server" Text="Edit Opening Balance" Width="184px"></asp:Label>&nbsp;</div>
<div id ="ronewmargin">
<div>
<table cellpadding ="0" cellspacing ="0" border ="0" >
    <tr>
        <td>
        <table style="width: 649px">
        <tr>
        <td style="width: 110px">
            Issue Center</td>
        <td style="width: 161px">
            <asp:DropDownList ID="ddlissueCenter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissueCenter_SelectedIndexChanged" Width="159px">
                <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
            </asp:DropDownList></td>
            <td style="width: 138px">
                Godown</td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Width ="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="01">--Select--</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
         <tr>
        <td style="width: 110px">
        
        </td>
        <td style="width: 161px">
        
        </td>
             <td style="width: 138px">
             </td>
             <td>
             </td>
        </tr>
        </table>
        </td>
    </tr>
  
  <tr>
  <td>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="15" PagerSettings-Visible ="True" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnPageIndexChanged="GridView1_PageIndexChanged" Width="652px" Height="143px"  >
                 <Columns>
                     <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                         <HeaderStyle Font-Size="11px" />
                         <ItemStyle Font-Size="11px" />
                     </asp:CommandField>
                     <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Source_Name" HeaderText="Source Of Arrival" SortExpression="Source_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quantity" HeaderText="Opening Qty." SortExpression="Quantity" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Bags" HeaderText="Opening Bags" SortExpression="Bags">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Balance" HeaderText="Current Balance" SortExpression="Current_Balance">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Bags" HeaderText="Current Bags" SortExpression="Current_Bags">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Source" HeaderText="SrcID" SortExpression="Source" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Commodity_ID" HeaderText="CmID" SortExpression="Commodity_ID" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Scheme_ID" HeaderText="ScID" SortExpression="Scheme_ID" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Godown" HeaderText="GdID" SortExpression="Godown" >
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
<center>
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
    <asp:Panel ID="Panel1" runat="server" Height="400px" Width="125px" Visible="False">
    
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 569px;">
     <tr>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; font-size: 10pt; background-color: white; width: 141px;" >
             &nbsp;<asp:TextBox ID="txtspos" runat="server" Width="108px" Visible="False"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
             <asp:Label ID="lblopendate" runat="server" Text="Opening Stock Date" Font-Size="10pt" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
             &nbsp;<asp:Label ID="Label3" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white">
             <asp:Label ID="Label5" runat="server" Text="Opening Balance Of the Month" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white">
            <asp:DropDownList ID="ddlopeningmonth" runat="server"  Width ="161px" Visible="False">
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
             border-bottom: 1px solid; background-color: white">
     <asp:Label ID="lblCategory" runat="server" Text="Category" Visible="False"></asp:Label></td>
     </tr>
 <tr>
 <td class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; height: 25px; width: 141px;">
     <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival"></asp:Label></td>
 <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; height: 25px; width: 161px;">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="159px" >
        
     </asp:DropDownList>
 </td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; height: 25px; background-color: white">
      <asp:DropDownList ID="ddlcategory" runat="server" Width="161px" Visible="False" >
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="161px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="161px" Visible="False" >
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
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;">
         <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
  <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;"> <asp:DropDownList ID="ddlscheme" runat="server"  Width="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList>
             </td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; background-color: white">
     </td>
   
                                       
  </tr>
 <tr>
 <td align="left" class ="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;">
     <asp:Label ID="lblGodown" runat="server" Text="Godown"></asp:Label></td>
 <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
     <asp:DropDownList ID="ddlissue" runat="server" Width ="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList></td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
         border-bottom: 1px solid; background-color: white">
         <asp:Label ID="lblcap" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="12px"
             ForeColor="Navy" Text="Godown Capacity" Visible="False" Width="114px"></asp:Label>
         <asp:TextBox ID="txtcap" runat="server" ForeColor="#004000" Visible="False" Width="116px"></asp:TextBox></td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; background-color: white">
             <asp:Label ID="lblBagNumber" runat="server" Text="No.Bags"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; background-color: white">
             <asp:TextBox ID="txtbags" runat="server" Width="110px" MaxLength="10"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbags"
                 ErrorMessage="No. of Bags  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;" >
      <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
 <td  align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;">
     <asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="13" ></asp:TextBox>
     <asp:Label ID="lblKgs" runat="server" Text="(Qtls.)" Font-Size="10pt"></asp:Label></td>
      <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
          border-bottom: 1px solid; background-color: white">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtqty"
              ErrorMessage=" Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>

  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; background-color: white">
             <asp:Label ID="Label2" runat="server" Text="Current Bags"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; background-color: white">
             <asp:TextBox ID="txtcurbags" runat="server" MaxLength="10" Width="110px"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; background-color: white">
             <asp:Label ID="Label4" runat="server" Text="Current Balance"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 161px; border-bottom: 1px solid; background-color: white">
             <asp:TextBox ID="txtcurbalance" runat="server" MaxLength="10" Width="110px"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: white">
         </td>
     </tr>
                                
                       
                                   
                             
   
       <tr>
       <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 141px;" > </td>
       <td colspan ="1" align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: white; width: 161px;"><asp:Button ID="btnsubmit" runat="server" Text="Update" OnClick="btnsubmit_Click" ValidationGroup="1" Width="80px" /> 
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="74px" /> </td>
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
               <asp:TextBox ID="effective_from" runat="server"></asp:TextBox>
         
          <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_effective_from'
	    });
	     </script>
           </td>
       </tr>
 </table>
 </asp:Panel>
 <div>
 
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

