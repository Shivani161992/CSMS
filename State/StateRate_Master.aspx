<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="StateRate_Master.aspx.cs" Inherits="State_StateRate_Master" Title="Rate Master Page" %>
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
 <script type="text/javascript" language ="javascript "> 
    
    

function Highlight(chk) {

if (chk.checked) {

 $("#" + chk.id).parent("td").parent("tr").css("background-color", "Red");

}else

{

$("#" + chk.id).parent("td").parent("tr").css("background-color", "white");

}

}

</script>

<script type="text/javascript">
function ConfirmationBox(DepotID) {

var result = confirm('क्या आप सम्बंधित जानकारी निरस्त करना चाहते है?' );
if (result)
 {

return true;
}
else {
return false;
}
}
</script>


<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="500">
    <tr valign="top">
        <td align="center" colspan="2" style="color: black; height: 24px; background-color: #cccccc">
            <span style="color: maroon"><strong>Issue Rate Master (To be Entered by Authorised Person of HO)</strong></span></td>
    </tr>
    <tr valign="top" >
        <td align="right" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="Label4" runat="server">(Rate per Quantal)</asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lbltadd" runat="server" Text="Commodity Name"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
   <asp:DropDownList ID="ddlcomodity" runat="server" Width="140px" >
    </asp:DropDownList>
            <asp:Label ID="lblcomdty" runat="server" Width="72px"></asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lblsch" runat="server" Text="Scheme Name"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            &nbsp;<asp:DropDownList ID="ddlscheme" runat="server" Width="140px" >
            </asp:DropDownList>
            <asp:Label ID="lblscheme" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="lblcropyear" runat="server" Text="Crop Year"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
        <asp:DropDownList ID="ddlcropyear" runat="server"  Width="140px" >
                                               
                                                  <asp:ListItem Value="02">2014-2015</asp:ListItem>
                                                <asp:ListItem Value="03">2015-2016</asp:ListItem>
                                     
                                            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="lblrate" runat="server" Text="Rate Type"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:DropDownList ID="ddlrate" runat="server"  Width="100px" >
                <asp:ListItem Value="U">Urban</asp:ListItem>
                <asp:ListItem Value="R">Rural</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
<tr>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:Label ID="Label6" runat="server" Text="Purchase Rate"></asp:Label></td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:TextBox ID="txtmsprate" runat="server" Width="100px" MaxLength="13"></asp:TextBox>per Qtls.</td>
</tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 27px;">
            <asp:Label ID="Label7" runat="server" Text="Effective From:(dd/mm/yyyy)"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 27px;">
       <asp:TextBox ID="effective_from" runat="server"></asp:TextBox>
             <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_effective_from'
	                      });
	          </script>
            </td>
    </tr>
<tr>
<td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="114px" />&nbsp;
</td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    &nbsp;<asp:Button ID="btnclose" runat="server" Text="Close" Width="115px" OnClick="btnclose_Click" />&nbsp;<asp:Button
        ID="btnnew" runat="server" OnClick="btnnew_Click" Text="New" Width="70px" /></td>
</tr>
</table>
    <asp:GridView ID="gvDetails" DataKeyNames="Commodity_ID,Scheme_ID,Crop_Year" runat="server" 
        AutoGenerateColumns="False" HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" OnRowDataBound = "gvDetails_RowDataBound" Width="688px">
     
<Columns>
<asp:TemplateField>
<EditItemTemplate>
    <asp:LinkButton ID="LinkButton1" CommandName ="Update"  runat="server" ToolTip="Update" Height="20px"  >Update</asp:LinkButton>

<asp:LinkButton ID="LinkButton2" CommandName ="Cancel"  runat="server" ToolTip="Cancel" Height="20px"  >Cancel</asp:LinkButton>


</EditItemTemplate>
<ItemTemplate>

<asp:LinkButton ID="LinkButton2" CommandName ="Edit"  runat="server" ToolTip="Cancel" Height="20px"  >Edit</asp:LinkButton>

<asp:LinkButton ID="LinkButton3" CommandName ="Delete"  runat="server" ToolTip="Cancel" Height="20px"  >Delete</asp:LinkButton>


</ItemTemplate>
 </asp:TemplateField>
 
 <asp:TemplateField HeaderText="Commodity Name">
<EditItemTemplate>
<asp:Label ID="lbleditcom" runat="server" Text='<%#Eval("Commodity_Name") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemcom" runat="server" Text='<%#Eval("Commodity_Name") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Scheme Name">
<EditItemTemplate>
<asp:Label ID="lbleditsch" runat="server" Text='<%#Eval("Scheme_Name") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemsch" runat="server" Text='<%#Eval("Scheme_Name") %>'/>
</ItemTemplate>
</asp:TemplateField>
 
 
 <asp:TemplateField HeaderText="Date of Effect">
<EditItemTemplate>
<asp:Label ID="lbleditdate" runat="server" Text='<%#Eval("Effective_From") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemdate" runat="server" Text='<%#Eval("Effective_From") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Crop Year">
<EditItemTemplate>
<asp:Label ID="lbledityear" runat="server" Text='<%#Eval("Crop_Year") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemyear" runat="server" Text='<%#Eval("Crop_Year") %>'/>
</ItemTemplate>
</asp:TemplateField>
 
 <asp:TemplateField HeaderText="Rate per Qntls">
<EditItemTemplate>

<asp:TextBox ID="Rate" runat="server" Text='<%#Eval("Rate") %>' />


</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemrate" runat="server" Text='<%#Eval("Rate") %>'/>
</ItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Rate Type">
<EditItemTemplate>
<asp:Label ID="lbleditratetype" runat="server" Text='<%#Eval("RateType") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemRatetype" runat="server" Text='<%#Eval("RateType") %>'/>
</ItemTemplate>
</asp:TemplateField>

 
 </Columns>
    <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
</asp:GridView>


</asp:Content>

