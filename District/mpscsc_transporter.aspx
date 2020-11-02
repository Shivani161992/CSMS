<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="mpscsc_transporter.aspx.cs" Inherits="mpscsc_transporter" Title="Transporter Master " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function CheckCalDate(tx)
{
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
<div id="transT">
<div>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="300">
    <tr>
        <td style="width: 576px; background-color: #cccccc">
                    Transporter Master<asp:GridView ID="GridView1" runat="server" 
                        AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="621px" CssClass="gridheader" 
                        OnRowDataBound ="GridView1_RowDataBound" DataKeyNames="Transport_ID" 
                        EnableModelValidation="True">
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
            <asp:BoundField DataField="Transporter_ID" HeaderText="Transporter ID" SortExpression="Transporter_ID" >
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="T_Type" HeaderText="Transport Type" ReadOnly="True" >
                <HeaderStyle Font-Names="Arial" Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter Name" ReadOnly="True"
                SortExpression="Transporter_Name" >
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Lead_Name" HeaderText="Lead upto(Kms.)" SortExpression="Lead_Name" >
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" Visible="False" >
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" >
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" SortExpression="MobileNo" >
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
             <asp:TemplateField HeaderText="Valid Upto">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("Valid_Upto").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Transport_ID" HeaderText="type" Visible="False" />
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
<div>
<table>
<tr>
<td><asp:Button ID="btnaddnew" runat="server" Text="AddNew" OnClick="btnaddnew_Click1" />
</td>
<td><asp:Button ID="btnCloseh" runat="server" Text="Close" OnClick="btnCloseh_Click" />
</td>
</tr>

</table>
</div>
<div id = "detail1" class ="detail1">


<table >
<tr>
<td style="width: 633px">
<asp:Panel ID="Panel1" runat="server" Height="250px" Width="630px" Visible="False">
    
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 400px; height: 237px;" id="Table1" >
    <tr>
        <td colspan="4" align="right" 
            style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#004000"
                Text="Label" Width="416px" style="text-align: center"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="Label1" runat="server" Visible="False" Width="96px"></asp:Label></td>
        <td colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
        </td>
        <td colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
        </td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 25px;">

            <asp:Label ID = "lbl" runat = "server" Text = "Transporter Type"></asp:Label>
            </td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 25px; width: 167px;">
            <asp:DropDownList ID="ddltranstype" runat="server" Width="153px" 
                AutoPostBack="True" onselectedindexchanged="ddltranstype_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 25px;">
            &nbsp;<asp:Label ID="Label3" runat="server" Text="Valid Upto"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 25px; width: 187px;">
            .<asp:TextBox ID="DaintyDate1" runat="server" Width="97px"></asp:TextBox>
            
            <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	                      });
	          </script>
            
            </td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px;">
            <asp:Label ID="lbltname" runat="server" Text="Transporter Name" Width="117px"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px; width: 167px;">
    <asp:TextBox ID="txttname" runat="server" Width="146px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttname"
        ErrorMessage="Transporter Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px;">
            <asp:Label ID="lbltadd" runat="server" Text="Transporter Address" Width="123px"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px; width: 187px;">
    <asp:TextBox ID="txttadd" runat="server" Width="146px"></asp:TextBox></td>
    </tr>
<tr>
<td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
    
    <asp:Label ID = "Label6" runat = "server" Text = "Mobile Num"></asp:Label>
    </td>
<td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 167px;">
    <asp:TextBox ID="txtmobile" runat="server" MaxLength="12" Width="146px"></asp:TextBox></td>
    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
        
        <asp:Label ID = "Label7" runat = "server" Text = "Rate per qts"></asp:Label>
        </td>
    <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
        <asp:TextBox ID="txtrate" runat="server" MaxLength="12" Width="146px"></asp:TextBox></td>
</tr>
<tr>
<td align="left" 
        style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
    <asp:Label ID="lbl_block" runat="server" Text="Block" Visible="False"></asp:Label>
    </td>
<td align="left" 
        style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 167px;">
    <asp:DropDownList ID="ddlBlock" runat="server" 
         Visible="False" 
        Width="194px">
    </asp:DropDownList>
    </td>
    <td align="left" 
        style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
            
            <asp:Label ID="Label5" runat="server" Text="Lead/Distance"></asp:Label>
    </td>
    <td align="left" 
        style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
        <asp:DropDownList ID="ddllead" runat="server" Width="158px">
        </asp:DropDownList>
    </td>
</tr>
    <tr>
        <td align="left" 
            style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px;">
            <asp:Label ID="Label4" runat="server" Text="Quantity" Visible="False"></asp:Label>
        </td>
        <td align="left" 
            style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px; width: 167px;">
            <asp:TextBox ID="txtqty" runat="server" MaxLength="12" Visible="False" 
                Width="146px"></asp:TextBox>
        </td>
        <td align="left" 
            style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px;">
            &nbsp;</td>
        <td align="left" 
            style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 27px; width: 187px;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        </td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 167px;">
            </td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
        </td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="263px" />
        </td>
        <td colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
        </td>
        <td colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 187px;">
        </td>
    </tr>
<tr>
<td align="right" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    </td>
<td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 167px;">
    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Width="178px" ValidationGroup="1" />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update" Width="177px" /></td>
    <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    <asp:Button ID="btnclose" runat="server" Text="Close" Width="131px" OnClick="btnclose_Click" /></td>
    <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 187px;">
    </td>
</tr>
    <tr>
        <td align="right" colspan="4" 
            style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: center;">
            &nbsp;</td>
    </tr>
</table>
</asp:Panel>
</td>
</tr>
</table>
    <table>
    <tr>
<td style="width: 576px"> 

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

