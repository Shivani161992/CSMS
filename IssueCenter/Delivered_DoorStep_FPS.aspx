<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Delivered_DoorStep_FPS.aspx.cs" Inherits="IssueCenter_Delivered_DoorStep_FPS" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
        
  function onlyNumbers(evt) 
  {
  var AsciiCode = event.keyCode;
var txt=evt.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=evt.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);

 if(AsciiCode==46)
    {
         if (num.split(".").length>1)
         {    
            alert('दशमलव एक ही बार आ सकता है |');
            return false;
         }
    }

if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}

 
 
}
  
 } 
  
//    var e = event || evt; // for trans-browser compatibility
//    var charCode = e.which || e.keyCode;
//    if (charCode > 31 && (charCode < 46 || charCode > 57))
//        return false;
//    return true;
//}      
function CheckCalDate(tx){
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>

 

<table style="width: 950px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">

<tr>
<td colspan="3"> 
<asp:Label ID = "Label1" runat = "server" Text = "जिस डिलीवरी आर्डर को जारी किया जा चुका है ,केवल वही यहाँ दिखेंगे "></asp:Label>
</td>
</tr>
    <tr>
        <td colspan="3" style="height: 53px">
      <table style="width: 950px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">

        <tr>
        <td>
        <asp:Label ID = "Label2" runat = "server" Text = "Select DO Month "></asp:Label>
        </td>
        
       <td>
       <asp:DropDownList ID = "ddlmonth" runat = "server" Width="138px">
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
              
       </asp:DropDownList>
        </td>
        
        <td>
        <asp:Label ID = "Label4" runat = "server" Text = "Select DO Year "></asp:Label>
        </td>
        
        <td>
         <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" Width="133px">
         </asp:DropDownList>
        </td>
        
        </tr>
        </table>
        
        
        
        
        </td>
    </tr>

<tr>
<td>
<asp:Label ID = "lbl1" runat = "server" Text = "Select Delivery Order Number"></asp:Label>
 </td>

<td>
<asp:DropDownList ID = "ddlDO" runat = "server" Width="220px" AutoPostBack="True" OnSelectedIndexChanged="ddlDO_SelectedIndexChanged"></asp:DropDownList>



</td>

<td> </td>

</tr>

<tr>
<td colspan = "3" valign = "top" > 
<asp:Panel ID = "pnl" runat = "server" ScrollBars="Auto" Height = "300px" Width = "950px">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="940px"  >
                    
                     <Columns>
                <asp:TemplateField HeaderText = "चुने ">
                    <ItemTemplate >
                        <asp:CheckBox ID="cbSelectAll" runat="server" Width = "25px" AutoPostBack ="true" OnCheckedChanged="cbSelectAll_CheckedChanged"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fps_code" HeaderText="एफ. पी. एस. कोड" SortExpression="fps_code">
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="80px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="fps_name" HeaderText="एफ. पी. एस. नाम" SortExpression="fps_name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="390px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" SortExpression="Commodity_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="90px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme Name" SortExpression="Scheme_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="90px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="issue_date" HeaderText="Issue Date" SortExpression="issue_date" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="80px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="lift_qty" HeaderText="Lifted Quantity" SortExpression="lift_qty" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="80px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                               
                
                <asp:TemplateField HeaderText="Delivered Quantity ">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                   <asp:TextBox ID="txtdelivered" runat="server" Text='<%# Bind("DeliveredQty") %>' ReadOnly="true"   BorderColor="Black" Width = "50px" onkeypress="return onlyNumbers(this);" > </asp:TextBox>
                     
                     
                 </ItemTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Delivery Date" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChallanDate"  AutoComplete="off" runat="server"  MaxLength="10" onkeypress="return CheckCalDate(this)" Text='<%# Bind("DelivDate") %>' ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChallanDate" ErrorMessage="सही दिनांक प्रविष्ट करे (dd/MM/yyyy फोर्मेट मे) " ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ValidationGroup="1">*</asp:RegularExpressionValidator>
                                 </ItemTemplate>
                                <HeaderStyle Font-Size="10pt" HorizontalAlign="Center" Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                <ControlStyle Width="70px" />                               
                            </asp:TemplateField>
                            <asp:TemplateField>
                                 <ItemTemplate>                                              
                                       <script type  ="text/javascript">                                        
                                       var grid=document.getElementById("<%=GridView1.ClientID %>");
                                       
                                       var indx=grid.rows.length;  
                                                                      
                                       var ctrl= 'ctl00_ContentPlaceHolder1_GridView1_ctl0'+indx+'_txtChallanDate' 
                                       
                                       if (indx >9)
                                       {
                                       var ctrl= 'ctl00_ContentPlaceHolder1_GridView1_ctl'+indx+'_txtChallanDate' ;
                                       }                                                                                                  
                                       
                 new tcal ({
                            'formname': '0',
                            'controlname': ctrl
                          });
              </script>
                                 </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="1px" Font-Size="Small" />
                            </asp:TemplateField> 
                
                     <asp:BoundField DataField="commodity" HeaderText="Commodity " SortExpression="commodity" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="5pt" ForeColor = "White" BackColor="Transparent" BorderColor="Transparent" />
                    <HeaderStyle CssClass="gridlarohead" BackColor="Transparent" ForeColor="White" BorderColor="Transparent" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="scheme_id" HeaderText="Scheme " SortExpression="scheme_id" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="5pt" ForeColor = "White" BackColor="Transparent" BorderColor="Transparent" />
                    <HeaderStyle CssClass="gridlarohead" BackColor="Transparent" ForeColor="White" BorderColor="Transparent" />
                </asp:BoundField>
                             
                
                
                </Columns>
                    
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
 
 </asp:Panel>

</td>



</tr>
<tr>
<td>
<asp:Button ID="btnClose" runat="server" Text="Close" Width="125px" OnClick="btnClose_Click"/>

 </td>

<td>

<asp:Button ID="btnSave" runat="server" Text="Save" Width="125px" OnClick="btnSave_Click"  />

</td>

<td> 

<asp:Button ID="btnNew" runat="server" Text="New" Width="125px" OnClick="btnNew_Click" />

</td>
</tr>


</table>



</asp:Content>

