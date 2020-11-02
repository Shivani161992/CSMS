<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PDS_MovPlan_Delete.aspx.cs" Inherits="District_PDS_MovPlan_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
       <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            return confirm('क्या आप Movement Plan Delete करना चाहते हो? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .hiddencol {
            display: none;
        }
    </style>

    <table align="center" style="width: 630px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: blue"><strong><span class="auto-style2">Delete Movement Plan </span></strong>
                <input id="hdfAcpt" type="hidden" runat="server" />
                <input id="hdfReject" type="hidden" runat="server" />
                 <input id="hdfLotNo" type="hidden" runat="server" />
                <input id="hdfDONo" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="7">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                
            </td>
            <td rowspan="7"></td>
        </tr>

        <tr>
            <td> <Strong style="font-size:medium"> District</Strong></td>
            <td>
               <asp:TextBox ID="txt_toDist" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>

            <td> <Strong style="font-size:medium">Crop Year </Strong></td>
            <td>

               <asp:DropDownList ID="ddlCropYear" runat="server" Width="137px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged" >
                </asp:DropDownList>
                 </td>
            </tr>
        <tr>
           
       
            <td colspan="2"> <Strong style="font-size:medium">Movement Order No. </Strong></td>
            <td colspan="2" >
                <asp:DropDownList ID="ddlMoveOrdNo" runat="server" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="ddlMoveOrdNo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        

        <tr>
            <td> <Strong style="font-size:medium">From District</Strong></td>
            <td>

                <asp:TextBox ID="txt_FrmDist" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td> <Strong style="font-size:medium">To District</Strong></td>
            <td>
                <asp:TextBox ID="txt_todistF" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><Strong style="font-size:medium">Quantity </Strong></td>
            <td>

                <asp:TextBox ID="txt_Quan" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td> <Strong style="font-size:medium">Date of Movement Order </Strong></td>
            <td>

                <asp:TextBox ID="txt_DOMP" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnDelete" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Delete" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnDelete_Click" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
