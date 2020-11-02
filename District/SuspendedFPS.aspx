<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="SuspendedFPS.aspx.cs" Inherits="District_SuspendedFPS" Title="Suspend FPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="border: thin groove #000000; height: 385px; width: 618px; background-color: #DFDFFF;">
        <tr>
            <td colspan="2">
                FPS Suspension Entry</td>
        </tr>
        <tr>
            <td style="text-align: left; color: #000099;" colspan="2">
                नोट: किसी दूकान का निलंबन केवल उसी आवंटन माह के लिए होगा | यदि दूकान अगले</td>
        </tr>
        <tr>
            <td style="text-align: left; color: #000099;" colspan="2">
                माह भी निलंबित रहती है तो उस माह के लिए पुनः एंट्री की जाना आवश्यक होगा |</td>
        </tr>
        <tr>
            <td style="width: 317px">
                <asp:Label ID="lblmonth" runat="server" Text="Allotment Month" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_allot_month" runat="server" AutoPostBack="True" 
                  OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Width="153px">
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
        </tr>
        <tr>
            <td style="width: 317px; height: 36px;">
                <asp:Label ID="lblyear" runat="server" Text="Year" style="font-weight: 700"></asp:Label>
            </td>
            <td style="height: 36px">
                <asp:DropDownList ID="ddd_allot_year" runat="server" Height="16px"  Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 317px; height: 30px;">
                <asp:Label ID="lblordernum" runat="server" Text="DSO Order Number" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td style="height: 30px">
                
                <asp:TextBox ID="txt_orderNum" runat="server" Height="25px" Width="180px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 317px; height: 27px;">
                <asp:Label ID="lblorderdate" runat="server" Text="Order Date" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td style="height: 27px">
                <asp:TextBox ID="tx_order_date" runat="server" Height="21px" Width="180px"></asp:TextBox>
                
         <script type="text/javascript">
                    new tcal({
                        'formname': '0',
                        'controlname': 'ctl00_ContentPlaceHolder1_tx_order_date'
                    });
	     </script>
            </td>
        </tr>
        <tr>
            <td style="width: 317px; height: 36px;">
                <asp:Label ID="lblsuspend" runat="server" Text="Suspended Shop Name" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td style="height: 36px">
                <asp:DropDownList ID="ddlsusFPS" runat="server" Height="25px" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 317px">
                <asp:Label ID="lblcontfps" runat="server" Text="Connected to Shop Number" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlcoctFPS" runat="server" Height="25px" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="background-color: #FFFFFF">
            <td style="width: 317px; height: 8px;">
                </td>
            <td style="height: 8px">
                </td>
        </tr>
        <tr>
            <td style="width: 317px">
                <asp:Button ID = "btnNew" runat = "server" Text = "New" Width="100px" 
                    onclick="btnNew_Click" />
                </td>
            <td>
               <asp:Button ID = "btnsave" runat = "server" Text = "Save" Height="26px" 
                    Width="100px" onclick="btnsave_Click" />
               </td>
        </tr>
        <tr>
            <td style="width: 317px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

