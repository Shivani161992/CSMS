<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Delete_Opening_Balances.aspx.cs" Inherits="Admin_Delete_Opening_Balances" Title="Opening Balance Of Stock " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> 
    <asp:Label ID="lblopendetails" runat="server" Text="Delete Opening Balance" Width="184px"></asp:Label>&nbsp;</div>
<div id ="ronewmargin">
<div>
<table cellpadding ="0" cellspacing ="0" border ="0" >
    <tr>
        <td>
        <table style="width: 649px">
        <tr>
        <td style="width: 110px">
            Issue Center</td>
        <td style="width: 161px">
            <asp:DropDownList ID="ddlissueCenter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissueCenter_SelectedIndexChanged" Width="159px">
                <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
            </asp:DropDownList></td>
            <td style="width: 138px">
                Godown</td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Width ="161px" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="01">--Select--</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
         <tr>
        <td style="width: 110px">
        
        </td>
        <td style="width: 161px">
        
        </td>
             <td style="width: 138px">
             </td>
             <td>
             </td>
        </tr>
        </table>
        </td>
    </tr>
  
  <tr>
  <td>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="15" PagerSettings-Visible ="True" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnPageIndexChanged="GridView1_PageIndexChanged" Width="652px" Height="143px"  >
                 <Columns>
                     <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                         <HeaderStyle Font-Size="11px" />
                         <ItemStyle Font-Size="11px" />
                     </asp:CommandField>
                     <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Source_Name" HeaderText="Source Of Arrival" SortExpression="Source_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quantity" HeaderText="Opening Qty." SortExpression="Quantity" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Bags" HeaderText="Opening Bags" SortExpression="Bags">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Balance" HeaderText="Current Balance" SortExpression="Current_Balance">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Current_Bags" HeaderText="Current Bags" SortExpression="Current_Bags">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Source" HeaderText="SrcID" SortExpression="Source" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Commodity_ID" HeaderText="CmID" SortExpression="Commodity_ID" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Scheme_ID" HeaderText="ScID" SortExpression="Scheme_ID" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                      <asp:BoundField DataField="Godown" HeaderText="GdID" SortExpression="Godown" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="Tan" />
                 <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                 <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="Tan" Font-Bold="True" />
                 <AlternatingRowStyle BackColor="PaleGoldenrod" />
             </asp:GridView>
      <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="137px" /></td> 
  </tr> 
  </table>
</div>
<center>
 <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Firstbutton_Click"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Prevbutton_Click" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Nextbutton_Click" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Lastbutton_Click" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
        </center>
 <div>
 
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

