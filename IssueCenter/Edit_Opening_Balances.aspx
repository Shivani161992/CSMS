<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Edit_Opening_Balances.aspx.cs" Inherits="IssueCenter_Edit_Opening_Balances" Title="Opening Balance Of Stock " %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> &nbsp;</div>
<div id ="ronewmargin">
<div>
<table cellpadding ="0" cellspacing ="0" border ="0" >
    <tr>
        <td align="center" style="background-color: #cccccc">
    <asp:Label ID="lblopendetails" runat="server" Text="Edit Opening Balance" Width="255px" Font-Bold="True"></asp:Label></td>
    </tr>
  
  <tr>
  <td>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
          OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="15" PagerSettings-Visible ="True" 
          ShowFooter = "True" CellPadding="4" ForeColor="#333333" GridLines="None" 
          OnPageIndexChanging="GridView1_PageIndexChanging" 
          OnPageIndexChanged="GridView1_PageIndexChanged" Width="652px" Height="143px" 
          EnableModelValidation="True" style="font-size: small"  >
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
                     <asp:BoundField DataField="Current_Balance" HeaderText="Current_Balance" SortExpression="Current_Balance" Visible = "false">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Bags" HeaderText="Current_Bags" SortExpression="Current_Bags" Visible = "false">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Source" HeaderText="SrcID" SortExpression="Source" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Commodity_ID" HeaderText="CmID" SortExpression="Commodity_ID" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Scheme_ID" HeaderText="ScID" SortExpression="Scheme_ID" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Godown" HeaderText="GdID" SortExpression="Godown" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Crop_year" HeaderText="Crop Year" SortExpression="Crop_year">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" Visible = "false">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Stock_Date" HeaderText="Stock_Date" SortExpression="Stock_Date">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quality" HeaderText="Quality" />
                 </Columns>
                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
       <EditRowStyle BackColor="#999999" />
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
    
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 641px;">
     <tr>
         <td style="font-size: 10pt; background-color: #cfdcc8; position: static;" >
             &nbsp;<asp:TextBox ID="txtspos" runat="server" Width="108px" Visible="False"></asp:TextBox></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblopendate" runat="server" Text="Opening Stock Date" Font-Size="10pt" Visible="False"></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
             colspan="2">
             &nbsp;<asp:Label ID="Label3" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="Label5" runat="server" Text="Opening Balance Of the Month" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
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
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
             colspan="2">
     <asp:Label ID="lblCategory" runat="server" Text="Category" Visible="False"></asp:Label></td>
     </tr>
 <tr>
 <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival"></asp:Label></td>
 <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="159px" >
        
     </asp:DropDownList>
 </td>
     <td align="left" 
         style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
         colspan="2">
      <asp:DropDownList ID="ddlcategory" runat="server" Width="161px" Visible="False" >
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="161px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
             colspan="2">
                                            </td>
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
  <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> <asp:DropDownList ID="ddlscheme" runat="server"  Width="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList>
             </td>
     <td align="left" 
         style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
         colspan="2">
     </td>
   
                                       
  </tr>
 <tr>
 <td align="left" class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:Label ID="lblGodown" runat="server" Text="Godown"></asp:Label></td>
 <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddlissue" runat="server" Width ="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList></td>
     <td align="left" 
         style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
         colspan="2">
         <asp:Label ID="lblcap" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="12px"
             ForeColor="Navy" Text="Godown Capacity" Visible="False" Width="114px"></asp:Label>
         <asp:TextBox ID="txtcap" runat="server" ForeColor="#004000" Visible="False" Width="116px"></asp:TextBox></td>
 </tr>
     <tr>
         <td align="left" class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 25px;">
             <asp:Label ID="lblyear" runat="server" Text="Crop Year"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 25px;">
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="161px" >
                                                <asp:ListItem Value="01" Selected="True">Crop year not indicated</asp:ListItem>
                                                <asp:ListItem Value="02">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="03">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="04">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="05">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="06">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="07">2008-2009</asp:ListItem>
                                            </asp:DropDownList></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 25px;" 
             colspan="2">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblBagNumber" runat="server" Text="No.Bags"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtbags" runat="server" Width="110px" MaxLength="10"></asp:TextBox></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
             colspan="2">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbags"
                 ErrorMessage="No. of Bags  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;" >
      <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
 <td  align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="15" ></asp:TextBox>
     <asp:Label ID="lblKgs" runat="server" Text="(Qtls.)" Font-Size="10pt"></asp:Label></td>
      <td align="left" 
          style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
          colspan="2">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtqty"
              ErrorMessage=" Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>

  </tr>
                                
                       
                                   
                             
   
       <tr>
           <td class="tdmarginro" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;">
               <asp:Label ID="qunt" runat="server" Text="Quality"></asp:Label>
           </td>
           <td align="left" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;">
               <asp:Label ID="lbl_quality" runat="server"></asp:Label>
           </td>
           <td align="left" colspan="2" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;">
               <asp:CheckBox ID="chk_check" runat="server" AutoPostBack="True" 
                   oncheckedchanged="chk_check_CheckedChanged" Text="Change Quality" />
           </td>
     </tr>
     <tr>
         <td class="tdmarginro" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lbl_lablel" runat="server" Text="Quality" Visible="False"></asp:Label>
         </td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlQuality" runat="server" AutoPostBack="True" 
                 onselectedindexchanged="ddlQuality_SelectedIndexChanged" Visible="False" 
                 Width="161px">
                 <asp:ListItem Value="0"> --Select--</asp:ListItem>
                 <asp:ListItem>Sound</asp:ListItem>
                 <asp:ListItem>Damaged</asp:ListItem>
                 <asp:ListItem>Sweepage</asp:ListItem>
             </asp:DropDownList>
         </td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lbl_cate" runat="server" Text="Category" Visible="False"></asp:Label>
         </td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldamagecataegory" runat="server" Visible="False" 
                 Width="120px">
                 <asp:ListItem Selected="True" Value="01">--Select--</asp:ListItem>
             </asp:DropDownList>
         </td>
     </tr>
                                
                       
                                   
                             
   
       <tr>
       <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" > </td>
       <td colspan ="1" align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;"><asp:Button ID="btnsubmit" runat="server" Text="Update" OnClick="btnsubmit_Click" ValidationGroup="1" Width="80px" /> 
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="74px" /> </td>
           <td align="left" colspan="2" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;">
           </td>
       </tr>
     <tr>
         <td align="left" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label></td>
         <td align="left" colspan="2" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="2" rowspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
       </td>
           <td align="left" colspan="2" rowspan="2" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         
        
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
var coda = event.keyCode
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
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

