<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Transport_Order_Other.aspx.cs" Inherits="District_Transport_Order_Other" Title="Transport Order" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table cellpadding ="0" cellspacing ="0" border ="0" style="width: 618px">
                    <tr>
                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 21px; color: black; background-color: #cccccc;" align="left">
                            &nbsp;&nbsp;
                        </td>
                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; color: black; height: 21px; background-color: #cccccc;" align="center" colspan="3">
                            <strong>Transport Order Details</strong>(
                            <asp:Label ID="Label3" runat="server" Width="131px"></asp:Label>) &nbsp;&nbsp; &nbsp;
                        </td>
                    </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             District Logged in</td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:TextBox ID="txtdistrict" runat="server" Width="148px"></asp:TextBox></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
           
         </td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             &nbsp; &nbsp;
         </td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="lblmonth" runat="server" Text="Allotment Month" Width="109px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlalotmm" runat="server" OnSelectedIndexChanged="ddlalotmm_SelectedIndexChanged"
                 Width="153px">
                 <asp:ListItem Value="1">January</asp:ListItem>
                 <asp:ListItem Value="2">February</asp:ListItem>
                 <asp:ListItem Value="3">March</asp:ListItem>
                 <asp:ListItem Value="4">April</asp:ListItem>
                 <asp:ListItem Value="5">May</asp:ListItem>
                 <asp:ListItem Value="6">June</asp:ListItem>
                 <asp:ListItem Value="7">July</asp:ListItem>
                 <asp:ListItem Value="8">August</asp:ListItem>
                 <asp:ListItem Value="9">September</asp:ListItem>
                 <asp:ListItem Value="10">October</asp:ListItem>
                 <asp:ListItem Value="11">November</asp:ListItem>
                 <asp:ListItem Value="12">December</asp:ListItem>
             </asp:DropDownList></td>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="lblyear" runat="server" Text="Allotment Year" Width="103px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlallotyear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged" Width="153px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
                    <tr>
                       
                          
                                
                                
                                        <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            Transport Order&nbsp; No.</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            <asp:TextBox ID="txttorderno" runat="server" Width="148px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttorderno"
                                                ErrorMessage="Transport Order No Required " Font-Bold="True" ValidationGroup="1"
                                                Width="1px">*</asp:RequiredFieldValidator>&nbsp;</td>
                                        <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            Transport Order Date(dd/mm/yyyy)</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            <asp:TextBox ID="DaintyDate1" runat="server"></asp:TextBox>                                          
                                          <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
                                            
                                            </td>
                                    </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             Commodity</td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left"><asp:DropDownList ID="ddlcomdty" runat="server" Width="153px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             Scheme</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"><asp:DropDownList ID="ddlscheme" runat="server" Width="153px">
                                                <asp:ListItem Text ="Non Scheme" Value ="4" ></asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             Quantity (Qtl.kgGms)
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:TextBox ID="txtsendqty" runat="server" Width="148px" MaxLength="13"></asp:TextBox>
             &nbsp;&nbsp;
         </td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             No of Bags</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="txtbags" runat="server" Width="148px"></asp:TextBox></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                                Transporter</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                                <asp:DropDownList ID="ddltransport" runat="server" OnSelectedIndexChanged="ddltransport_SelectedIndexChanged" Width="153px">
                                                </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            Validity Date</td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
           
             <asp:TextBox ID="DaintyDate2" runat="server"></asp:TextBox>
              <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });
	     </script>
         </td>
     </tr>
                                    <tr>
                                        <td style="background-color: lightslategray; font-size: 10pt; position: static; color: white; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="center" colspan="4">
                                            <strong>Source Information</strong></td>
                                    </tr>
     <tr>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Source District</td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlsrcdist" runat="server" OnSelectedIndexChanged="ddlsrcdist_SelectedIndexChanged"
                 Width="153px" AutoPostBack="True">
             </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             Source Depot</td>
         <td align="left" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddlsrcdepot" runat="server" Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="background-color: lightslategray; font-size: 10pt; position: static; color: white; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="center" colspan="4">
             <strong>Destination Information </strong>
             </td>
     </tr>
     <tr>
         <td align="left"  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Destination District</td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"><asp:DropDownList ID="ddldistrict" runat="server" Width="153px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                                                AutoPostBack="True">
             </asp:DropDownList></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             Destination Depot</td>
         <td align="left" class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"><asp:DropDownList ID="ddlissue" runat="server" Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             &nbsp;
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" colspan="2" align="right">
             &nbsp;
             <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" Width="125px" ValidationGroup="1" /></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             &nbsp;<asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close"
                 Width="125px" /></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="250px" ShowSummary="False" />
         </td>
     </tr>
     <tr>
         <td colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="center">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Details Of Transport Order which is not Entered by District Office "></asp:Label></td>
     </tr>
     <tr>
         <td colspan="4" style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="center">
             <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="12px"
                 Font-Underline="False" ForeColor="Navy" Text="Please Fill These Information First"></asp:Label></td>
     </tr>
     <tr>
         <td colspan="4" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue" align="left">
              <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" 
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="true"  CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgridchallan_PageIndexChanging"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
            <asp:BoundField DataField="TO_Number" HeaderText="T.O. No." ReadOnly="True" SortExpression="TO_Number" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
                       
            <asp:BoundField DataField="SDepot" HeaderText="Src. Depot" ReadOnly="True" SortExpression="SDepot" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="Dest.Depot" SortExpression="DepotName" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Qty_send" HeaderText="Qty." SortExpression="Qty_send">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
           
        </Columns>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                  <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                  <EditRowStyle BackColor="#999999" />
    </asp:GridView>
             
             
             
         </td>
     </tr>
                                    <tr>
                                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue;" colspan="4" align="left">
                                            
                                            <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left" colspan="4">
                                            &nbsp;</td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
        
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



