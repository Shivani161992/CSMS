<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true"  CodeFile="Rate_master.aspx.cs" Inherits="Rate_master" Title="Rate Master(Purchase)" %>
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

<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double;"  width="500">
    <tr valign="top">
        <td align="center" colspan="2" style="color: black; height: 24px; background-color: #cccccc">
            Purchase Rate Master</td>
    </tr>
    <tr valign="top" >
        <td align="right" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="Label4" runat="server">(Rate per Quantal)</asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            Purchase From</td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:DropDownList ID="ddlsource" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlsource_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lbltadd" runat="server" Text="Commodity Name"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
   <asp:DropDownList ID="ddlcomodity" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
    </asp:DropDownList>
            <asp:Label ID="lblcomdty" runat="server" Width="72px"></asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lblmarktse" runat="server" Text="Marketing Season"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:DropDownList ID="ddlmseason" runat="server" Width="140px" OnSelectedIndexChanged="ddlmseason_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="lblsource" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lblcropyear" runat="server" Text="Crop Year"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
        <asp:DropDownList ID="ddlcropyear" runat="server"  Width="140px" >
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
                                            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="lblsch" runat="server" Text="Scheme Name"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:DropDownList ID="ddlschemefci" runat="server" OnSelectedIndexChanged="ddlschemefci_SelectedIndexChanged"
                Width="140px" AutoPostBack="True">
            </asp:DropDownList>
            <asp:Label ID="lblscheme" runat="server" Visible="False"></asp:Label></td>
    </tr>
<tr>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"><asp:Label ID="lbltname" runat="server" Text="Pool" Width="58px"></asp:Label></td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:DropDownList ID="ddlscheme" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged">
    </asp:DropDownList></td>
</tr>
<tr>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:Label ID="Label6" runat="server" Text="Purchase Rate"></asp:Label></td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:TextBox ID="txtmsprate" runat="server" Width="100px" MaxLength="13"></asp:TextBox>per Qtls.</td>
</tr>
<tr>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"><asp:Label ID="lblinsid" runat="server" Text="Incidental"></asp:Label></td>
<td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
    <asp:TextBox ID="txtincidental" runat="server" Width="100px" MaxLength="13"></asp:TextBox></td> 
</tr>
<tr>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"><asp:Label ID="lblbonus" runat="server" Text="Bonus"></asp:Label></td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:TextBox ID="txtbonus" runat="server" Width="100px" MaxLength="13"></asp:TextBox></td> 
</tr>
<tr>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"><asp:Label ID="Label2" runat="server" Text="Gunny Filling Capacity: (In Kg)"></asp:Label></td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    <asp:TextBox ID="txtgunnycap" runat="server" Width="100px" MaxLength="4"></asp:TextBox></td> 
</tr>
    <tr>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:Label ID="Label7" runat="server" Text="Effective From:(dd/mm/yyyy)"></asp:Label></td>
        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
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
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="114px" />
    <asp:Button ID="Button1" runat="server"  Text="Update" Visible="False" OnClick="Button1_Click" Width="107px" />
</td>
<td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
    &nbsp;<asp:Button ID="btnclose" runat="server" Text="Close" Width="115px" OnClick="btnclose_Click"  /></td>
</tr>
</table>

<div id="rate">
<table class="laromargin" style="width: 599px">
<tr>
    <td colspan="9" style="width: 663px">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="6" PagerSettings-Visible ="true" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" Width="571px" CellPadding="4" ForeColor="#333333" GridLines="None"  >
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="Update" >
                <ItemStyle CssClass="griditemlaro" />
            </asp:CommandField>
            <asp:BoundField DataField="Commodity_name" HeaderText="Commodity" SortExpression="Commodity_name" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Scheme_name" HeaderText="Scheme" SortExpression="Scheme_name" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Season_Name" HeaderText="Marketing Season" SortExpression="Season_Name" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" SortExpression="Crop_Year" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
             <asp:TemplateField HeaderText="Effective From">
             <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("Effective_From").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gridlarohead" />
                 <ItemStyle CssClass="griditemlaro" />
                 </asp:TemplateField>
           <asp:BoundField DataField="Bonus" HeaderText="Bonus" SortExpression="Bonus" >
               <ItemStyle CssClass="griditemlaro" />
               <HeaderStyle CssClass="gridlarohead" />
           </asp:BoundField>
            <asp:BoundField DataField="Incidental" HeaderText="Incidental" SortExpression="Incidental" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Gunny_Capacity" HeaderText="Gunny Capacity" SortExpression="Gunny_Capacity" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Purchase_From" HeaderText="Source" SortExpression="Purchase_From">
                <ItemStyle CssClass="griditemlaro" Font-Size="0px" />
                <HeaderStyle CssClass="gridlarohead" Font-Size="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_ID" HeaderText="Comdty" SortExpression="Commodity_ID">
                <ItemStyle CssClass="griditemlaro" Font-Size="0px" />
                <HeaderStyle CssClass="gridlarohead" Font-Size="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="Scheme_ID" HeaderText="Sch" SortExpression="Scheme_ID">
                <ItemStyle CssClass="griditemlaro" Font-Size="0px" />
                <HeaderStyle CssClass="gridlarohead" Font-Size="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="M_Season" HeaderText="Mcode" SortExpression="M_Season">
                <ItemStyle CssClass="griditemlaro" Font-Size="0px" />
                <HeaderStyle CssClass="gridlarohead" Font-Size="0px" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" CssClass="gridheader" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    </td>

</tr>
</table>
 <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../Images/firstpage.gif"=align="absmiddle">'
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
if (count > 3)  
{
 alert("Only 3 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>

</asp:Content>

