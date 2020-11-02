<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Dist_Welcome.aspx.cs" Inherits="Dist_Welcome" Title="Welcome To District Office (MPSCSC)"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server">
<div  style="width: 592px">
<center>
<table style="border-right: gray thin groove; border-top: gray thin groove; border-left: gray thin groove; border-bottom: gray thin groove" >
    <tr>
        <td align="center" colspan="2" 
            style="border-bottom: blue thin groove; text-align: center">
            द्वार <span style="font-family: Verdana"><span style="font-size: 11pt">प्रदाय के 
            सम्बन्ध में कार्य के दिशा एवं निर्देश के लिए कृपया लोगिन पेज से यूजर मैनुअल 
            (User Manual) <span style="color: #000099"><b>द्वार प्रदाय योजना दिशा एवं 
            निर्देश</b></span> को डाउनलोड करें |
            <asp:Image ID="Image2" runat="server" Height="19px" 
                ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
            &nbsp;</span></span></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 21px">
            <asp:LinkButton ID="btnLink_AddOperator" runat="server" OnClick="btnLink_AddOperator_Click"
                Visible="False" Width="115px" Font-Bold="True" Font-Italic="True" Font-Size="13px">Add Operator</asp:LinkButton></td>
    </tr>
    <tr>
        <td style="height: 14px">
            &nbsp;</td>
        <td align="center">
                <table width="100%" border="2" style="border: thin solid #000000">
                    <tr>
                        <td >
                        <asp:TextBox ID="txt_Date" runat="server" BackColor="#FFFF80" BorderColor="Black"></asp:TextBox>
                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txt_Date'
	    });
	     </script>
                <asp:Button ID="btnSelect" runat="server" Text="Check Date for No Transaction" 
                                 Width="195px" OnClick="btnSelect_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                <asp:CheckBox ID="chkRecieptDetails" runat="server" Font-Bold="True" Font-Size="8pt" 
                                Text="&lt;----Ckeck Here If no Reciept Details" Enabled="False" Width="273px" AutoPostBack="True" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 14px;">
                        <asp:CheckBox ID="chkDispatchDetails" runat="server" Font-Bold="True" 
                                Font-Size="8pt" Text="&lt;----Ckeck Here If no Dispatch Details" 
                                Enabled="False" Width="279px" AutoPostBack="True" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        <asp:CheckBox ID="chkDistributionDetails" runat="server" Font-Bold="True" 
                                Font-Size="8pt" Text="&lt;----Ckeck Here If no Distribution Details" 
                                Enabled="False" Width="302px" AutoPostBack="True" /></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                <asp:Button ID="btnFinalize" runat="server"  Text="Submit" onclick="btnFinalize_Click" /></td>
                    </tr>
                </table>
            </td>
    </tr>
<tr>
<td style="height: 14px">
</td>
<td style="width: 96px; height: 14px;" align="left">
    &nbsp;<asp:Label ID="lblrack" runat="server" Text="Details of Rack  to be Entered..." Font-Bold="True" Font-Italic="True" ForeColor="Navy" Width="245px" Font-Size="10px"></asp:Label></td>
</tr>
<tr>
<td align="center" colspan="2">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        ShowFooter="True" Width="337px">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No" SortExpression="Rack_No" />
            <asp:BoundField DataField="district_name" HeaderText="District Name" SortExpression="district_name" />
            <asp:BoundField DataField="RailHead_Name" HeaderText="Rail Head" SortExpression="RailHead_Name" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
   
   
    </td>
</tr>
    <tr>
        <td align="left">
            </td>
        <td align="left" style="width: 96px">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C04000"
        Text="Label" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td align="left">
        </td>
        <td align="left" style="width: 96px">
            <asp:Label ID="lblto" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="10px"
                ForeColor="Navy" Text="Pending Transport Order (Transfer to Other Godown)" Width="325px" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td align="left">
        </td>
        <td align="left" style="width: 96px">
        <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" 
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="true"  CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="541px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
            <asp:BoundField DataField="TO_Number" HeaderText="T.O. No." ReadOnly="True" SortExpression="TO_Number" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
                       
            <asp:BoundField DataField="SDepot" HeaderText="Src. Depot" ReadOnly="True" SortExpression="SDepot" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="Dest.Depot" SortExpression="DepotName" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Qty_send" HeaderText="Qty." SortExpression="Qty_send">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
           
        </Columns>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
    </asp:GridView>
        </td>
    </tr>
</table>
</center>
</div>
</asp:Panel>


  </asp:Content>
