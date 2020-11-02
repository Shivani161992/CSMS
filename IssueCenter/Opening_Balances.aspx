<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Opening_Balances.aspx.cs" Inherits="IssueCenter_Opening_Balances" Title="Opening Balance Of Stock " %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id ="ronewmargin">
<center>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: lightslategray 3px double; border-top: lightslategray 3px double; border-left: lightslategray 3px double; border-bottom: lightslategray 3px double; width: 569px;">
     <tr>
         <td colspan="4" 
             style="border-right: 1px solid; border-top: 1px solid; font-size: 10pt;
             border-left: 1px solid; border-bottom: 1px solid; background-color: #cccccc">
    <asp:Label ID="lblopendetails" runat="server" Text="Opening Balance Of Stock " Font-Bold="True" Font-Size="14px"></asp:Label></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
             &nbsp;<asp:TextBox ID="txtspos" runat="server" Width="108px" Visible="False"></asp:TextBox></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblopendate" runat="server" Text="Opening Stock Date" Font-Size="10pt"></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
             colspan="2">
         <asp:TextBox ID="effective_from" runat="server" ></asp:TextBox>
       <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_effective_from'
	    });
	     </script>
         
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label5" runat="server" Text="Opening Balance Of the Month" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
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
             style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
             colspan="2">
         </td>
     </tr>
 <tr>
 <td class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="159px" ondatabound="ddlsarrival_DataBound" >
        
     </asp:DropDownList>
 </td>
     <td align="left" 
         style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
         colspan="2">
     </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="161px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td align="left" 
             style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
             colspan="2">
         </td>
     </tr>
 
 <tr>
     <td class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;">
         <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;">
   <asp:DropDownList ID="ddlscheme" runat="server"  Width="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" >
     
     </asp:DropDownList>
             </td>
     <td align="left" 
         style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 25px;" 
         colspan="2">
     </td>
   
                                       
  </tr>
  <tr>
 <td align="left" class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >  
     <asp:Label ID="lblCategory" runat="server" Text="Category" Visible="False"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">  
      <asp:DropDownList ID="ddlcategory" runat="server" Width="161px" Visible="False" >
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
      <td align="left" 
          style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
          colspan="2">
      </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
     <asp:Label ID="lblGodown" runat="server" Text="Godown"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
     <asp:DropDownList ID="ddlissue" runat="server" Width ="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged">
                        <asp:ListItem Value ="01" Selected ="True" >--Select--</asp:ListItem>
                        </asp:DropDownList></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: #cfdcdc" 
             colspan="2">
         </td>
     </tr>
 <tr>
 <td align="left" class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="Label3" runat="server" Text="Crop Year" Visible="true"></asp:Label></td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="161px" Visible="true" >
                                              
                                              <%-- <asp:ListItem Value="01">2015-2016</asp:ListItem>--%>
                                                <asp:ListItem Value="02">2014-2015</asp:ListItem>
                                                <asp:ListItem Value="03">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="04">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="05">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="06">2010-2011</asp:ListItem>
                                               
                                              
                                            </asp:DropDownList></td>
     <td align="left" 
         style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
         colspan="2">
         <asp:Label ID="lblcap" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="12px"
             ForeColor="Navy" Text="Godown Capacity" Visible="False" Width="114px"></asp:Label>
         <asp:TextBox ID="txtcap" runat="server" ForeColor="#004000" Visible="False" Width="116px"></asp:TextBox></td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblBagNumber" runat="server" Text="No.Bags" Width="62px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtbags" runat="server" Width="110px" MaxLength="10"></asp:TextBox></td>
         <td align="left" 
             style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
             colspan="2">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbags"
                 ErrorMessage="No. of Bags  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
      <asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="65px"></asp:Label></td>
 <td  align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtqty" runat="server" Width="110px" MaxLength="13" ></asp:TextBox>
     <asp:Label ID="lblKgs" runat="server" Text="(Qtls.)" Font-Size="10pt"></asp:Label></td>
      <td align="left" 
          style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
          colspan="2">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtqty"
              ErrorMessage=" Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>

  </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
      Quality</td>
 <td  align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlQuality" runat="server" Width="161px" 
         AutoPostBack="True" onselectedindexchanged="ddlQuality_SelectedIndexChanged" >
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
          <asp:ListItem>Sound</asp:ListItem>
          <asp:ListItem>Damaged</asp:ListItem>
          <asp:ListItem>Sweepage</asp:ListItem>
      </asp:DropDownList></td>
      <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
          <asp:Label ID="lbl_cate" runat="server" Text="Category" Visible="False"></asp:Label>
      </td>

      <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                <asp:DropDownList ID="ddldamagecataegory" runat="server" Width="120px" Visible="False" 
                  >
                    <asp:ListItem Selected="True" Value="01">--Select--</asp:ListItem>
                </asp:DropDownList></td>

  </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" 
             style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
             colspan="2">
         </td>
     </tr>
                                
                       
                                   
                             
   
       <tr>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" > </td>
       <td colspan ="1" align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"><asp:Button ID="btnsubmit" runat="server" Text="Save " OnClick="btnsubmit_Click" ValidationGroup="1" Width="82px" /> 
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="74px" /> </td>
           <td align="left" colspan="2" 
               style="background-color: #cfdcdc; font-size: 10pt; position: static;">
           </td>
       </tr>
     <tr>
         <td align="left" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            
         </td>
         <td align="left" colspan="2" 
             style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td align="left" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label></td>
         <td align="left" colspan="2" 
             style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="2" rowspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
       </td>
           <td align="left" colspan="2" rowspan="2" 
               style="background-color: #cfdcdc; font-size: 10pt; position: static;">
           </td>
       </tr>
 </table>
 <div>
 <table cellpadding ="0" cellspacing ="0" border ="0" >
  
  <tr>
  <td>
      &nbsp;</td> 
  </tr> 
  </table>
 </div>
  <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="6" PagerSettings-Visible ="True" 
    ShowFooter = "True" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
    OnPageIndexChanging="GridView1_PageIndexChanging" Width="568px" EnableModelValidation="True" 
                                    style="font-size: small"  >
                                    <Columns>
                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" 
                         SortExpression="Godown_Name">
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Source_Name" HeaderText="Source Of Arrival" 
                         SortExpression="Source_Name" >
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" 
                         SortExpression="Commodity_Name" >
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" 
                         SortExpression="Scheme_Name" >
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Opening Balance" 
                         SortExpression="Quantity" >
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Crop_year" HeaderText="Crop_year" 
                         SortExpression="Crop_year">
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" 
                         Visible = "false" >
                                        <ItemStyle Font-Size="11px" />
                                        <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Stock_Date" HeaderText="Stock Date"  
                                            DataFormatString="{0:dd-MM-yyyy}" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                </asp:GridView>
                            </asp:Panel>
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
 
 <script type="text/javascript">
function CheckIsnondecimal(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 47) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}
}



    </script>
 
</asp:Content>

