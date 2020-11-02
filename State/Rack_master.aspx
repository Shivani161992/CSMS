<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Rack_master.aspx.cs" Inherits="DistrictFood_Rack_master" Title="Rack Master" %>
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

<center >
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 449px;" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()" >
     <tr>
         <td colspan="4" style="color: white; background-color: lightslategray">
             <strong>
                   Rake Master</strong></td>
     </tr>
     <tr>
         <td  colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Font-Size="10pt" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
         <td align="right"  style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label11" runat="server" Text="State"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                 Width="154px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label9" runat="server" Text="Place of Sugar Factory" Width="156px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:DropDownList ID="ddlplace" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlplace_SelectedIndexChanged"
                 Width="144px">
                 <asp:ListItem Value="01">--Select--</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label10" runat="server" Text="Name of Sugar Factory" Width="155px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:DropDownList ID="ddlfactory" runat="server" OnSelectedIndexChanged="ddlfactory_SelectedIndexChanged"
                 Width="153px">
                 <asp:ListItem Value="01">--Select--</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label3" runat="server" Text="For District" Width="116px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:DropDownList ID="ddlsourcedist" runat="server"  Width ="144px" AutoPostBack="True" OnSelectedIndexChanged="ddlsourcedist_SelectedIndexChanged">
         </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label4" runat="server" Text="Rail Head " Width="69px"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
         <asp:DropDownList ID="ddlsengrailhead" runat="server"  Width ="156px" OnSelectedIndexChanged="ddlsengrailhead_SelectedIndexChanged">
         </asp:DropDownList></td>
     </tr>
   <tr>
   <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> 
       <asp:Label ID="lbltctrackno" runat="server" Text="Rake No."></asp:Label></td>
     <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8"> <asp:TextBox ID="txtrackno" runat="server" Width="122px" AutoPostBack="True" OnTextChanged="txtrackno_TextChanged"></asp:TextBox>
         </td>
       <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> </td>
         <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> </td>
   </tr>
    <tr>
   <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> 
       <asp:Label ID="Label5" runat="server" Text="Destination District" Width="133px" Visible="False"></asp:Label></td>
     <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">   &nbsp;<asp:DropDownList ID="ddldesdistt" runat="server"  Width ="143px" AutoPostBack="True" OnSelectedIndexChanged="ddldesdistt_SelectedIndexChanged" Visible="False">
         </asp:DropDownList></td>
       <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> 
           <asp:Label ID="Label6" runat="server" Text="Destination Rail Head" Width="139px" Visible="False"></asp:Label></td>
         <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"> <asp:DropDownList ID="ddldesrailhead" runat="server"  Width ="154px" Visible="False">
         <asp:ListItem Text ="--Select--" Value ="01" Selected ="True" ></asp:ListItem>
         </asp:DropDownList></td>
   </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="Label8" runat="server" Text="Commodity"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:DropDownList ID="ddlcommodity" runat="server"  Width ="142px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="Label7" runat="server" Text="Rake Dispatch Date" Width="141px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8" >
             <asp:TextBox ID="DaintyDate3" runat="server" Width="121px"></asp:TextBox>
             <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	                      });
function TABLE1_onclick() {

}

	          </script>
             
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
             </td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8"><asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="139px" OnClick="btnSubmit_Click" /></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;"><asp:Button ID="btnClose" runat="server" Text="Close" Width="139px" OnClick="btnClose_Click" /></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
             </td>
     </tr>
     <tr>
         <td  colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8">
             <asp:Label ID="Label1" runat="server"></asp:Label></td>
     </tr>
 </table>
 
 </center>
 <table>
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
           PageSize ="5"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Width="561px" Height="134px"  >
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                <HeaderStyle Font-Size="12px" />
                <ItemStyle Font-Size="11px" />
            </asp:CommandField>
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." SortExpression="Rack_No">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="DestRName" HeaderText="Rail Head" SortExpression="DestRName">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="district_name" HeaderText="Dest. District" SortExpression="district_name">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="RailHead_Name" HeaderText="Dest. RailHead" SortExpression="RailHead_Name">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Dispatch Date">
     
                <ItemTemplate>
                <asp:Label ID="lbldisdate" runat="server" 
                Text='<%# Eval("Rack_DispDate").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle Font-Size="10pt" />
                <ItemStyle Font-Size="8pt" />
                 </asp:TemplateField>
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
</table> 
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
 

</asp:Content>

