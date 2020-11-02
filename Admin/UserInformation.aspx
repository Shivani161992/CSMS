<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="UserInformation.aspx.cs" Inherits="Admin_UserInformation" Title="User Information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="hedl"> User Login/Logout Information</div>

    <table>
        <tr>
            <td align="left">
            <table>
            <tr>
            <td> <asp:Label id="Label3" runat="server" Text="Login Type"></asp:Label> </td>
            <td><asp:DropDownList id="ddllogintype" runat="server" Width="240px">
                </asp:DropDownList> </td>
            
            </tr>
            
            </table>
            
            
            </td>
        </tr>
        <tr>
            <td align="left">
            
            
            <table>
            <tr>
            
            <td> 
        <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label></td>
             <td> 
    
        <asp:TextBox ID="DaintyDate1" runat="server" Width="128px"></asp:TextBox>
        <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
        </td>
              <td> 
     <asp:Label ID="Label2" runat="server" Text="To Date " Width="54px"></asp:Label></td>
               <td> 
     <asp:TextBox ID="DaintyDate2" runat="server" Width="105px"></asp:TextBox>
     <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });
	     </script>
     </td>
            </tr>
            
            </table>
            
            
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnGet" runat="server" Text="Get Data" Width="143px" OnClick="btnGet_Click" />
                <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="137px" /></td>
        </tr>
    <tr>
    <td>
        <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="#C00000" Width="304px"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
           PageSize ="15"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="516px"  >
         
        <Columns>
            <asp:BoundField DataField="DepotName" HeaderText="Depot Name" ReadOnly="True"
                SortExpression="DepotName" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="IP_Address" HeaderText="IP Address" SortExpression="IP_Address" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
          <asp:TemplateField HeaderText="Login Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("Login_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
              <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Login_Time" HeaderText="Login Time" SortExpression="Login_Time" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="Logout_Time" HeaderText="Logout Time" SortExpression="Logout_Time" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
           <asp:TemplateField HeaderText="Status">
     
                <ItemTemplate>
                <asp:Label ID="lblstatus" runat="server" 
                Text='<%# Eval("offline").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
               <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    
    </asp:GridView></td>
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
      
</asp:Content>

