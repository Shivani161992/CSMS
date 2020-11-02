<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Scheme_Master.aspx.cs" Inherits="Scheme_Master " Title="Scheme  Master" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="font-weight: bold; color: green; font-style: italic"> Scheme &nbsp;Master</div>
<div id ="ronewmargin">

 <table cellpadding ="0" cellspacing ="0" border ="0" >
     <tr>
         <td style="font-weight: bold; color: navy; font-style: italic">
             All Scheme</td>
         <td align="left" style="font-weight: bold; color: navy; font-style: italic; width: 139px;">
         </td>
         <td style="font-weight: bold; color: navy; font-style: italic">
             Already Added Scheme
         </td>
     </tr>
     <tr>
         <td rowspan ="5" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid" valign="top">
              <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  Width="250px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="12">
            <Columns>
                <asp:TemplateField>
                                
                     <AlternatingItemTemplate>
                        <asp:CheckBox  ID="cbSelectAll"  runat="server" AutoPostBack ="false" />
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server"  AutoPostBack ="false" />
                    </ItemTemplate>
                    <ItemStyle Font-Size="6pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:TemplateField>
                <asp:BoundField DataField="Scheme_ID" HeaderText="Scheme ID" SortExpression="Scheme_ID" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme  Name" SortExpression="Scheme_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
            </Columns>
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td style="width: 117px">
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
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; font-weight: bold;
             border-left: 1px solid; width: 139px; color: #003333; border-bottom: 1px solid;
             font-style: italic">
             <asp:Button ID="btnsubmit" runat="server" Text="Add Scheme" OnClick="btnsubmit_Click" ValidationGroup="1" Width="124px" /><br />
             <asp:Button ID="btnremove" runat="server" Text="Remove Scheme" OnClick="btnremove_Click" Width="125px" /><br />
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="127px" /></td>
         <td valign="top" rowspan="2">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="194px" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="15" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Scheme_Name" HeaderText="SchemeName" SortExpression="Scheme_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Scheme_ID" HeaderText="Comdty_ID" SortExpression="Scheme_ID" Visible="False" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
            </Columns>
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td style="width: 117px">
                            <div >
                                <asp:LinkButton ID="LinkButton1" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClickCS" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="LinkButton2" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClickCS" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="LinkButton3" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClickCS" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="LinkButton4" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClickCS" CausesValidation ="false" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 139px; border-bottom: 1px solid">
             &nbsp;</td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 139px; border-bottom: 1px solid">
         </td>
         <td rowspan="1" valign="top">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 139px; border-bottom: 1px solid">
         </td>
         <td rowspan="1" valign="top">
         </td>
     </tr>
 </table>
 

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


