<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="CMR_Movement_FromMill.aspx.cs" Inherits="State_CMR_Movement_FromMill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table
        {
            width: 1000px;
            color: black;
            background-color: rgb(219, 216, 216);
        }

        .tablerow
        {
            color: #fff;
            background-color: #004E4E;
            font-family: sans-serif;
            font-size: 23px;
        }
            .tablerow:hover
            {
                color: #fff;
            background-color: black;
            font-family: sans-serif;
            font-size: 23px;
            }

        .tablecolumn
        {
            width: 250px;
            text-align: left;
            font-size: 17px;
            font-family: sans-serif;
        }
        .button
        {
            border-radius:10px;
            background-color: #004E4E;

            color:#FFF;
        }
            .button:hover
            {
                border-radius:50%;
            background-color: black;

            color:#FFF;
            }
        .buttonSubmit
        {
            background-color: #004E4E;
            border-radius: 15px;
            width:250px;
            color:#FFF;
            height:25px;

        }
            .buttonSubmit:hover
            {
                background-color: black;
            border-radius: 15px;
            width:250px;
            color:#FFF;
            font-family:sans-serif;
            }
    </style>
      <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
        <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>
    <center>
        <table class="table" border="1">
            <tr class="tablerow">
                <td colspan="4" style="text-align:center;">
                    <strong>Inter District CMR Movement From Mill </strong>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:10px;"></td>
            </tr>
            <tr>
               
                 <td  class="tablecolumn" > <strong>Crop Year </strong></td>
                 <td   class="tablecolumn" > 
                      <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged" >
                    </asp:DropDownList></td>
                 <td  class="tablecolumn" > <strong>Commodity </strong></td>
                 <td   class="tablecolumn" > 
                      <asp:TextBox ID="txtCommodity" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
            </tr>
            <tr>
                <td class="tablecolumn">
                    <strong>Miller Order Number </strong>
                </td>
                <td class="tablecolumn">
                    <asp:TextBox ID="txt_OrderNumber" CssClass="tcolumn" Width="250px" Enabled="true" runat="server"></asp:TextBox>
                </td>
                <td class="tablecolumn"><strong>Date </strong>
                </td>
                <td class="tablecolumn">
                      <asp:TextBox ID="txtDateofReceipt" CssClass="tcolumn" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDateofReceipt' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                </td>
            </tr>
            <tr>
                <td colspan="4"  style="height:10px;"></td>
            </tr>
            <tr>
                <td class="tablecolumn">
                    <strong>Paddy District </strong>
                </td>
                <td class="tablecolumn">
                    <asp:DropDownList ID="ddlPaddyDistrict" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlPaddyDistrict_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
                <td class="tablecolumn"><strong>Miller District </strong>
                </td>
                <td class="tablecolumn">
                    <asp:DropDownList ID="ddlMillerDistrict" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlMillerDistrict_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tablecolumn">
                    <strong>Miller Name </strong>
                </td>
                <td colspan="3" class="tablecolumn">
                    <asp:DropDownList ID="ddl_MillerName" AutoPostBack="true" Width="750px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddl_MillerName_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tablecolumn">
                    <strong>Agreement Number </strong>
                </td>
                <td class="tablecolumn">
                    <asp:DropDownList ID="ddl_AgreementNumber" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddl_AgreementNumber_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
                <td class="tablecolumn"><strong>Balance CMR Qty (M.T) </strong>
                </td>
                <td class="tablecolumn">
                    <asp:TextBox ID="txt_BalCMR" CssClass="tcolumn" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tablecolumn">
                    <strong>Receiving District</strong>
                </td>
                <td class="tablecolumn">
                     <asp:DropDownList ID="ddlReceivingDistrict" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" >
                    </asp:DropDownList>
                     </td>
                <td class="tablecolumn"><strong>Transfer CMR Qty(M.T) </strong>
                </td>
                <td class="tablecolumn">
                   <asp:TextBox ID="txt_TransferQty" CssClass="tcolumn" Width="200px" Enabled="true" runat="server" OnTextChanged="txt_TransferQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:Button ID="bttAdd" runat="server" CssClass="button" Text="Add" OnClick="bttAdd_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4"  style="height:10px;"></td>
            </tr>
            <tr runat="server" visible="false" id="trGrid">
                <td colspan="4">
                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                            
                             <asp:BoundField DataField="PaddyDistrict" HeaderText="Paddy District " />
                            <asp:BoundField DataField="MillerDistrict" HeaderText="Miller District" />
                            <asp:BoundField DataField="MillerName" HeaderText="Miller Name" />
                             <asp:BoundField DataField="Agreement" HeaderText="Agreement " />
                            <asp:BoundField DataField="BalQty" HeaderText="Balance Quantity" />
                            <asp:BoundField DataField="ReceivingDistrict" HeaderText="Receiving District" />
                             <asp:BoundField DataField="TransferQuantity" HeaderText="Transfer CMR Quantity" />
                         
                            

                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>
                </td>
            </tr>
               <tr>
                <td colspan="4"  style="height:10px;"></td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center;" >
                    <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="buttonSubmit" Enabled="false" OnClick="bttSubmit_Click" />
                    <asp:Button ID="bttprint" runat="server" Text="Print" CssClass="buttonSubmit" Enabled="false" OnClick="bttprint_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 1000px; height: 15px; text-align: left;" >
                     <asp:Button ID="bttNew" runat="server" Text="New" CssClass="button"  OnClick="bttNew_Click" />
                </td>
                 <td colspan="2" style="width: 1000px; height: 15px; text-align: right; ">
                     <asp:Button ID="bttClose" runat="server" Text="Close" CssClass="button"  OnClick="bttClose_Click" />
                </td>


            </tr>
        </table>
    </center>
</asp:Content>

