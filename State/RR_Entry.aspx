<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="RR_Entry.aspx.cs" Inherits="State_RR_Entry" Title="R.R. Details" %>
 
       
   
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
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
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;"  width="640">
     <tr>
         <td class="tdmarginro" style="width: 124px; color: white; background-color: lightslategray;">
         </td>
         <td class="tdmarginro" style="width: 165px; color: white; background-color: lightslategray;">
             <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="15px" ForeColor="White"
                 Text="Details OF R.R." Width="123px"></asp:Label></td>
         <td align="center"  colspan="2" style="color: white; width: 110px; background-color: lightslategray;">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             <asp:Label ID="Label1" runat="server" ForeColor="#C00000" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; width: 124px; position: static; background-color: #cfdcdc">
             <asp:Label ID="Label8" runat="server" Text="Sending District" Width="108px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 165px;">
             <asp:DropDownList ID="ddlsourcedist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsourcedist_SelectedIndexChanged"
                 Width="144px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             &nbsp;<asp:Label ID="Label2" runat="server" Text="Rack No." Width="66px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             <asp:DropDownList ID="ddlrackno" runat="server"  Width ="126px" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged">
                            
                        </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; width: 124px; position: static; background-color: #cfdcdc">
           <asp:Label ID="Label3" runat="server" Text="Source Rail Head " Width="119px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 165px;">
             <asp:TextBox ID="txtsrailh" runat="server" Width="121px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
             <asp:Label ID="Label6" runat="server" Text="RR. No."></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 165px;">
             <asp:TextBox ID="txtrrno" runat="server" Width="120px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrrno"
                 ErrorMessage="RR No. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             <asp:Label ID="Label5" runat="server" Text="RR Qty."></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
             <asp:TextBox ID="txtrrqty" runat="server" Width="104px" MaxLength="12"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 124px;">
             <asp:Label ID="Label7" runat="server" Text="No. of Wagon in RR" Width="143px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 165px;">
             <asp:TextBox ID="txtwcount" runat="server" Width="119px" MaxLength="5"></asp:TextBox></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px; font-size: 10pt; position: static; background-color: #cfdcdc;">
               <asp:Label ID="Label4" runat="server" Text="Destination  Rail Head " Visible="False" Width="149px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 165px;">
             <asp:TextBox ID="txtdesrailh" runat="server" Width="120px" Font-Italic="True" ForeColor="Navy" Visible="False"></asp:TextBox></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
             <asp:Button ID="Button1" runat="server" Text="Add More RR" Width="95px" OnClick="Button1_Click" ValidationGroup="1" /></td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
           PageSize ="5"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Width="564px" OnRowDeleting="GridView1_RowDeleting"  >
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." SortExpression="Rack_No">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="RR_No" HeaderText="RR Number" SortExpression="RR_No">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="RR_qty" HeaderText="RR Quantity" SortExpression="RR_qty">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Wagon_Count" HeaderText="No. of Wagons" SortExpression="Wagon_Count">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" CssClass="gridheader" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
             <EditRowStyle BackColor="#999999" />
    </asp:GridView>
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 124px; font-size: 10pt; position: static; background-color: #cfdcdc;">
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
             &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="79px" OnClick="btnSubmit_Click" />
             <asp:Button ID="btnClose" runat="server" Text="Close" Width="84px" OnClick="btnClose_Click" /></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; width: 124px; position: static; background-color: #cfdcdc">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="186px" ShowSummary="False" />
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 165px;">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
     </tr>
 </table>
 

</asp:Content>

