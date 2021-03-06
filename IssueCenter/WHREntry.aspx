<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="WHREntry.aspx.cs" Inherits="IssueCenter_WHREntry" Title="WHR Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
    function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            return false;
        }                
       
        
    }
    </script>


<table style="width: 600px; height: 373px">
<tr>
<td colspan="3" style="height: 1px">

    Depositer Form&nbsp; के विरुद्ध WHR की प्रविष्टि 
    हेतु</td>

</tr>

<tr>
<td style="height: 2px; text-align: left; color: #CC3300;" colspan="3">

                <b>केवल एफ सी आई एवं अन्य गोदाम के सन्दर्भ में मैनुअल एंट्री करे , वेयरहाउस 
                सॉफ्टवेयर की WHR जानकरी स्वतः CSMS में आ जायेगी , अतः इसकी एंट्री ना करें |</b></td>

</tr>

<tr>
<td style="height: 2px; width: 253px;">

                <asp:Label ID="Label3" runat="server" ForeColor="#003399" 
                    Text="Select Commodity"></asp:Label>

</td>
<td colspan="2" style="height: 2px">

                <asp:DropDownList ID="ddlcommodtiy" runat="server" Height="22px" Width="120px" 
                    AutoPostBack="True" onselectedindexchanged="ddlcommodtiy_SelectedIndexChanged">
                </asp:DropDownList>

</td>

</tr>

<tr>
<td style="height: 1px; width: 253px;">

                <asp:Label ID="Label5" runat="server" ForeColor="#003399" 
                    Text="Depositer Form Numnber"></asp:Label>

</td>
<td colspan="2" style="height: 1px">

                <asp:DropDownList ID="ddldepositer" runat="server" Height="19px" Width="250px" 
                    AutoPostBack="True" onselectedindexchanged="ddldepositer_SelectedIndexChanged">
                </asp:DropDownList>

</td>

</tr>

<tr>
<td style="height: 3px; width: 253px;">

                <asp:RadioButton ID="rdmanul" runat="server" Text="Manual WHR" GroupName="1" />

</td>
<td colspan="2" style="height: 3px">

                <asp:RadioButton ID="rdcomp" runat="server" Text="Computerized WHR" 
                    GroupName="1" />

</td>

</tr>

<tr>
<td colspan="3" style="height: 32px" valign = "top">

                <table style="width: 89%">
                    <tr>
                        <td style="width: 297px">
                            <asp:Label ID="Label6" runat="server" Text="Number of Trucks" 
                                ForeColor="#993366"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbltruck" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 297px">
                            <asp:Label ID="Label7" runat="server" Text="Total Bags" ForeColor="#993366"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblbags" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 297px">
                            <asp:Label ID="Label8" runat="server" Text="Total Quantity" ForeColor="#993366"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblqty" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>

</td>

</tr>
  <tr>
            <td  style="height: 25px; width: 253px;">
                            <asp:Label ID="Label9" runat="server" Text="Enter WHR Number" 
                    Font-Bold="True" ForeColor="#000099"></asp:Label>
            </td>
            <td style="width: 177px; height: 25px;">
                <asp:TextBox ID="txtwhr" runat="server" Height="23px" Width="200px"></asp:TextBox>
            </td>
            <td style="height: 25px">
                </td>
        </tr>

  <tr>
            <td  style="height: 26px; width: 253px;">
                <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" 
                    onclick="btnNew_Click"  />
            </td>
            <td style="width: 177px; height: 26px;">
                <asp:Button ID="btnsave" runat="server" Text="Submit" Width="100px" 
                    onclick="btnsave_Click"  />
            </td>
            <td style="height: 26px">
                <asp:Button ID="btnclose" runat="server" Text="Close" Width="75px" 
                    onclick="btnclose_Click"  />
            </td>
        </tr>

  <tr>
            <td  style="height: 26px; text-align: left;" colspan="3">
                            <asp:Label ID="Label10" runat="server" 
                    Font-Bold="False" Visible="False"></asp:Label>
            </td>
        </tr>

</table>

</asp:Content>

