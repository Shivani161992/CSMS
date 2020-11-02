<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Ratemaster_Purchase.aspx.cs" Inherits="State_Ratemaster_Purchase" Title="Rate Master (Issue)" %>

 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
<div id="trans">
<div id = "detail1">
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;" id="" width="500">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(../Images/imgg2.jpg);" align="center">
                    Issue
                   Rate Master</td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;"  width="500">
    <tr valign="top" >
        <td align="right" colspan="2" style="background-color: lightblue">
            <asp:Label ID="Label4" runat="server">(Rate per Quantal)</asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="background-color: lightblue">
            <asp:Label ID="lbltadd" runat="server" Text="Commodity Name"></asp:Label></td>
        <td align="left" style="background-color: lightblue">
   <asp:DropDownList ID="ddlcomodity" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
    </asp:DropDownList></td>
    </tr>
<tr>
<td align="left" style="background-color: lightblue"><asp:Label ID="lbltname" runat="server" Text="Scheme Name" Width="113px"></asp:Label></td>
<td align="left" style="background-color: lightblue">
    <asp:DropDownList ID="ddlscheme" runat="server" Width="140px" >
    </asp:DropDownList>
    <asp:Label ID="lblscheme" runat="server"></asp:Label></td>
</tr>
     <tr>
        <td align="left" style="height: 24px; background-color: lightblue">
            <asp:Label ID="Label8" runat="server" Text="Crop Year" Width="112px"></asp:Label></td>
        <td align="left" style="height: 24px; background-color: lightblue">
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
<td align="left" style="background-color: lightblue;">
    <asp:Label ID="Label5" runat="server" Text="Rural  Rate" Width="112px"></asp:Label></td>
<td align="left" style="background-color: lightblue;">
    <asp:TextBox ID="txtrrate" runat="server" Width="100px" MaxLength="13" ></asp:TextBox>per Qtls.</td>
</tr>
    <tr>
        <td align="left" style="background-color: lightblue;">
            <asp:Label ID="Label6" runat="server" Text="Urban Rate" Width="110px"></asp:Label></td>
        <td align="left" style="background-color: lightblue;">
            <asp:TextBox ID="txturate" runat="server" Width="100px" MaxLength="13"></asp:TextBox>per Qtls.</td>
    </tr>
<tr>
<td align="left" style="background-color: lightblue;"><asp:Label ID="Label3" runat="server" Text="Incidental" Visible="False" Width="110px"></asp:Label></td>
<td style="background-color: lightblue;" align="left">
    <asp:TextBox ID="txtincidental" runat="server" Width="100px" Visible="False" MaxLength="13"></asp:TextBox></td> 
</tr>
<tr>
<td align="left" style="background-color: lightblue"><asp:Label ID="Label1" runat="server" Text="Bonus" Visible="False" Width="110px"></asp:Label></td>
<td align="left" style="background-color: lightblue">
    <asp:TextBox ID="txtbonus" runat="server" Width="100px" Visible="False" MaxLength="13"></asp:TextBox></td> 
</tr>
<tr>
<td align="left" style="background-color: lightblue"><asp:Label ID="Label2" runat="server" Text="Gunny Filling Capacity: (In Kg)"></asp:Label></td>
<td align="left" style="background-color: lightblue">
    <asp:TextBox ID="txtgunnycap" runat="server" Width="100px" MaxLength="4"></asp:TextBox></td> 
</tr>
    <tr>
        <td align="left" style="background-color: lightblue">
            <asp:Label ID="Label7" runat="server" Text="Effective From:(dd/mm/yyyy)" Width="186px"></asp:Label></td>
        <td align="left" style="background-color: lightblue">
         
          <asp:TextBox ID="effective_from" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_effective_from'
	    });
	     </script>
        </td>
        
    </tr>
<tr>
<td style="background-color: lightblue">
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="114px" />
    <asp:Button ID="btnclose" runat="server" Text="Close" Width="121px" OnClick="btnclose_Click"  /></td>
<td align="left" style="background-color: lightblue">
    &nbsp;<asp:Button ID="Button1" runat="server"  Text="Update" Visible="False" Width="137px" OnClick="Button1_Click"  /></td>
</tr>
</table>

</div>
</div>
<div id="rate">
<table class="laromargin">
<tr>
    <td colspan="9">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="6" PagerSettings-Visible ="true" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" Width="603px"  >
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="Update" >
                <ItemStyle CssClass="griditemlaro" />
            </asp:CommandField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" SortExpression="Crop_Year" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Rural_rate" HeaderText="Rural Rate" SortExpression="Rural_rate" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Uraban_rate" HeaderText="Urban Rate" SortExpression="Uraban_rate" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
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
           <asp:BoundField DataField="Bonus" HeaderText="Bonus" SortExpression="Bonus" Visible="False" >
               <HeaderStyle CssClass="gridlarohead" />
               <ItemStyle CssClass="griditemlaro" />
           </asp:BoundField>
            <asp:BoundField DataField="Incidental" HeaderText="Incidental" SortExpression="Incidental" Visible="False" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Gunny_Capacity" HeaderText="Gunny Capacity" SortExpression="Gunny_Capacity" >
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_ID" HeaderText="ComID" SortExpression="Commodity_ID">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Scheme_ID" HeaderText="SchID" SortExpression="Scheme_ID">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" CssClass="gridheader" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    </td>

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


