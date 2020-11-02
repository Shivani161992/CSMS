<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Godown_Add.aspx.cs" Inherits="IssueCenter_Godown_Add" Title="Godown Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
  function IsNumericWithoutDecimal(tx)
    {
        var AsciiCode = event.keyCode;
        var txt=tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3=txt2*1;
        if ((AsciiCode < 46) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please insert numeric values.');
            event.cancelBubble = true;
            event.returnValue = false;
        }
         var num=tx.value;
         if ((AsciiCode == 46 ))
            {
                alert('Please insert numeric values.');
                event.cancelBubble = true;
                event.returnValue = false;
            }
        
    }
    
    function Spc_characteralpha(abc)
   {
		var checkOK = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,\ ";;
		var checkStr = abc.value;
		var allValid = true;
		var allChr = "";
		for (i = 0;  i < checkStr.length;  i++)
		{
			ch = checkStr.charAt(i);
			for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
			break;
			if (j == checkOK.length)
			{
				allValid = false;
				break;
			}
			if (ch != ",")
				allChr += ch;
		}
			if (!allValid)
			{
				alert("This Character is Not Allowed");
				abc.value=""
				abc.focus();
				return (false);
			}  
			} 
  </script>
  
  <div>
        <div>
            <table class="yelloCell" style="width: 500px; margin-left: 10px; background-image: url(../images/images[26].jpg);">
          
               <tr>
               <td colspan="2" align="right">
                   <asp:LinkButton ID="lnkback" runat="server" PostBackUrl ="~/IssueCenter/Godown_Master_New.aspx" CausesValidation="false">>>Back>></asp:LinkButton>
               </td>
               </tr>
                <tr>
                    <td colspan="2" style="width: 600px; height: 285px;" valign="top">
                        <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                            CellPadding="4" Width="600px" BackColor="White" BorderColor="#eeeeee" BorderStyle="None"
                            BorderWidth="1px" AllowPaging="True" AllowSorting="True" OnRowDataBound="godown_GridView_RowDataBound"
                            OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged" OnRowDeleting="godown_GridView_RowDeleting"
                            OnPageIndexChanging="godown_GridView_PageIndexChanging" Font-Size="8pt">
                            <FooterStyle BackColor="#eeeeee" ForeColor="#330099" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" HeaderText="Action" ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="S.N.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Gowdown No." />
                                <asp:BoundField DataField="Godown_Capacity" HeaderText="Capacity" />
                                <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="ScientificCapacity" />
                                <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                                <asp:BoundField DataField="Godown_ID">
                                    <HeaderStyle Font-Size="0pt" />
                                    <ItemStyle Font-Size="0pt" ForeColor="White" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#6b6868" Font-Bold="True" ForeColor="#FFFFCC" HorizontalAlign="Left" />
                        </asp:GridView>
                        <asp:Label ID="Label2" runat="server" Font-Size="15pt" ForeColor="#400040" ></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" >
                        &nbsp;<asp:Panel ID="PanelGodown" runat="server" Visible="False">
                            <table>
                                <tr>
                                    <td style="width: 100px; height: 26px;">
                                        <asp:Label ID="Label3" runat="server" Text="Godown No" Width="99px"></asp:Label></td>
                                    <td style="width: 100px; height: 26px;">
                                        <asp:TextBox ID="txtGodownName" runat="server" Width="150px"></asp:TextBox></td>
                                    <td style="width: 100px; height: 26px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ControlToValidate="txtGodownName"
                                            Display="Dynamic" ErrorMessage="Godown Name field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label ID="Label4" runat="server" Text="Maximum Capacity" Width="127px"></asp:Label></td>
                                    <td style="width: 100px;">
                                        <asp:TextBox ID="txtCapacity" runat="server"></asp:TextBox></td>
                                    <td style="width: 100px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCapacity"
                                            Display="Dynamic" ErrorMessage="Capacity field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label1" runat="server" Text="Scientific Capacity" Width="127px"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtScientificCapacity" runat="server"></asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScientificCapacity"
                                            Display="Dynamic" ErrorMessage="Capacity field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label5" runat="server" Text="Hired Type"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="ddllst_hired" runat="server" Width="154px">
                                            <asp:ListItem Text="Owned" Value="Owned"></asp:ListItem>
                                            <asp:ListItem Text="Hired" Value="Hired"></asp:ListItem>
                                            <asp:ListItem Text="Joint Venture(JV)" Value="Joint Venture(JV)"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label6" runat="server" Text="Storage Type"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="ddllst_storage" runat="server" Width="153px">
                                            <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                            <asp:ListItem Text="Open(CAP)" Value="Open(CAP)"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnUpdate" runat="server" Height="22px" Text="Update" Width="95px"
                                            OnClick="btnUpdate_Click" /></td>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnCan" runat="server" Height="21px" Text="Cancel" Width="70px" OnClick="btnCan_Click" CausesValidation ="false" /></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="btnaddnew" runat="server" Font-Size="X-Small" OnClick="btnaddnew_Click"
                            Text="Add New" CausesValidation="False" />
                        <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label></td>
                </tr>
             
            </table>
        </div>
        <asp:ValidationSummary ID="godown_Validationerror" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </div>
</asp:Content>

