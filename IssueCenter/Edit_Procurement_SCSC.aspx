<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Edit_Procurement_SCSC.aspx.cs" Inherits="IssueCenter_Edit_Procurement_SCSC" Title="Edit Receipt Details (Procurement)" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> &nbsp;</div>
<div>
<center>

<table style="width: 686px" >

<tr>
 <td style="width: 830px" align="left">
 <table style="width: 583px">
     <tr>
         <td align="center" colspan="4" style="background-color: #cccccc">
    <asp:Label ID="lbldispprocure" runat="server" Text="Dispatch from Purchase Center (Procurement) " Width="327px" Font-Bold="True"></asp:Label></td>
     </tr>
     <tr>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:Label ID="lblDistrictName" runat="server" Text="District(Logged in)" Width="116px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtdist" runat="server" Width ="142px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Label ID="lblNameDepot" runat="server" Text="IssueCenter" Width="93px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtissue" runat="server"  Width ="136px" Font-Bold="True" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblRecFromDist" runat="server" Text="Sending District" Width="114px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="146px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td   style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblpcname" runat="server" Text="Purchase Center" Width="114px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:DropDownList ID="ddlissuecenter" runat="server" Width="216px" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" Font-Size="11px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td   style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblChallanNumber" runat="server" Text="Truck Challan No." Width="118px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttrukcno" runat="server"  Width ="122px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No." Width="118px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttruckno" runat="server" Width="140px" MaxLength="12"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txttruckno"
         ErrorMessage="Truck No.  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblTrans" runat="server" Text="Transport Name" Width="108px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddltransporter" runat="server" Width="148px">
     </asp:DropDownList></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblDateOfChallan" runat="server" Text="Date Of Dispatch" Width="117px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
          <asp:TextBox ID="DaintyDate1" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
         </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="146px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcropyear" runat="server"  Width="160px" >
                                                <asp:ListItem Value="01" Selected="True">Crop year not indicated</asp:ListItem>
                                                <asp:ListItem Value="02">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="03">2012-2013</asp:ListItem>
                                                 <asp:ListItem Value="04">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="05">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="06">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="07">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="08">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="09">2006-2007</asp:ListItem>
                                                <asp:ListItem Value="10">2005-2006</asp:ListItem>
                                                <asp:ListItem Value="11">2004-2005</asp:ListItem>
                                                <asp:ListItem Value="12">2003-2004</asp:ListItem>
                                                <asp:ListItem Value="13">2002-2003</asp:ListItem>
                                               
                                            </asp:DropDownList></td>
         
     </tr>
  <tr>
  <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:Label ID="lblIssuedBags" runat="server" Text="NO.Of Bags" Font-Size="12px"></asp:Label></td>
 <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtbagno" runat="server" Width="127px" MaxLength="5"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbagno"
         ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Label ID="lblQuantity" runat="server" Text="Net Qty. Dispatched"
             Width="117px" Font-Size="12px"></asp:Label></td>
  <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;"> <asp:TextBox ID="txtquant" runat="server" Width="130px" MaxLength="13"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtquant"
          ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>(Qtls.)</td>
 
  
  </tr>
     <tr>
         <td  style="background-color: #cccccc;">
             <asp:Label ID="Label14" runat="server" Text="Book No." Visible="False" Font-Size="12px"></asp:Label>
             &nbsp;
         </td>
         <td align="left" style="background-color: #cccccc;">
     <asp:TextBox ID="txtbookno" runat="server" Width="128px" Visible="False"></asp:TextBox>&nbsp;
         </td>
         <td  style="background-color: #cccccc;">
             <asp:Label ID="lblRecepDetail" runat="server" Text="Receipt Details" Width="124px" Font-Bold="True"></asp:Label></td>
         <td align="left" style="background-color: #cccccc;">
             &nbsp;&nbsp;
             <asp:DropDownList ID="ddlscheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged"
                 Width="153px" Visible="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblReceiptDate" runat="server" Text="Recd. Date" Width="73px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script>
         </td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             &nbsp;
             <asp:Label ID="lblrqty" runat="server" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             &nbsp;&nbsp;
             <asp:Label ID="lblrecbag" runat="server" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lbltotalReceivedBags" runat="server" Text="Recd Bags." Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrecdbags" runat="server" MaxLength="5" Width="146px"></asp:TextBox></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Recd. Qty." Width="109px" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrecdqty" runat="server" MaxLength="13" Width="146px"></asp:TextBox></td>
     </tr>
      <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="Label2" runat="server" Text="Branch" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlbranchwlc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchwlc_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblGodownNo" runat="server" Font-Size="12px" Text="Godown/Own N."></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblhirty" runat="server" Text="Hired_Type" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txthhty" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"
                 Width="140px"></asp:TextBox></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="144px"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap." Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="145px"></asp:TextBox></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblAvailable" runat="server" Text="Available" Font-Size="12px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="143px"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblbalanceqty" runat="server" Text="Current Stock Position"></asp:Label></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtbalqty" runat="server" Width="93px"></asp:TextBox></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td     style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label15" runat="server" Text="Acceptance Note Number" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtaccptno" runat="server" Width="125px" MaxLength="13" Visible="False"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtaccptno"
                 ErrorMessage="AN Required" ValidationGroup="1" Visible="False">*</asp:RequiredFieldValidator></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="DaintyDate2" runat="server" Visible="false"></asp:TextBox>
                      <script type  ="text/javascript" visible="false">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });
	     </script>
         </td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             &nbsp;&nbsp;
             <asp:Label ID="lblgid" runat="server" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Button ID="btnsave" runat="server" Text="Update" OnClick="btnsave_Click" ValidationGroup="1" Width="185px"/></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="115px"/></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="false" Width="211px">Print Receipt Detail</asp:HyperLink>
             &nbsp;
         </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="153px" ShowSummary="False" />
         </td>
     </tr>
     <tr>
         <td  colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td  colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label></td>
     </tr>
    
 </table>
 
 </td>
</tr>
</table>
<table >
 <tr>
         <td align="left" colspan="6">
         <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="false" ShowFooter = "True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanged="dgridchallan_PageIndexChanged" OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="676px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="TC_Number" HeaderText="TC No." ReadOnly="True" SortExpression="TC_Number" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." ReadOnly="True"
                SortExpression="Truck_Number" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
              <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No." ReadOnly="True"
                SortExpression="Acceptance_No" >
                <ItemStyle CssClass="griditem" />
                  <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="A N Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("Acceptance_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
            
            <asp:BoundField DataField="No_of_Bags" HeaderText="Bags" ReadOnly="True" SortExpression="No_of_Bags" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Quantity" HeaderText="Qty" ReadOnly="True"
                SortExpression="Quantity" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ReadOnly="True" SortExpression="Commodity_Name" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Status_Deposit" HeaderText="Status of Deposit" ReadOnly="True" SortExpression="Status_Deposit" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
        </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             <PagerSettings Visible="False" />
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
             <EditRowStyle BackColor="#999999" />
    </asp:GridView></td>
     </tr>
</table>
 <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;&nbsp;
                             
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

