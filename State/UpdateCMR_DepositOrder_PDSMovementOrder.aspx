<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="UpdateCMR_DepositOrder_PDSMovementOrder.aspx.cs" Inherits="State_UpdateCMR_DepositOrder_PDSMovementOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table
        {
            width: 1000px;
            background-color: white;
            color: blue;
        }

        .row
        {
            color: white;
            background-color: black;
            font-size: 20px;
        }

        .column
        {
            width: 250px;
            color: black;
            text-align: left;
        }

        .button
        {
            border-radius: 10px;
            background-color: black;
            color: #FFF;
        }

            .button:hover
            {
                border-radius: 50%;
                background-color: black;
                color: #FFF;
            }

        .buttonSubmit
        {
            background-color: Black;
            border-radius: 15px;
            width: 250px;
            color: #FFF;
            height: 25px;
            font-size:18px;
            font-family:sans-serif;
        }

            .buttonSubmit:hover
            {
                background-color: white;
                border-radius: 15px;
                width: 250px;
                color: black;
                font-family: sans-serif;
                 font-size:18px;
            font-family:sans-serif;
            }

        }
    </style>
    <table class="table" border="1">
        <tr class="row">
            <td colspan="4">
                <strong>Update CMR Deposit Order (Movement Order) </strong>
            </td>
        </tr>
        <tr style="height:18px;">
            <td colspan="4"></td>
        </tr>
        <tr>
            <td class="column">
                <strong>Crop Year</strong>
            </td>
            <td class="column">
                <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server">
                </asp:DropDownList>
            </td>
            <td class="column">
                <strong>Agreement District</strong>
            </td>
            <td class="column">
                <asp:DropDownList ID="ddlAgreeDistrict" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlAgreeDistrict_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>



        </tr>
        <tr>
            <td class="column">
                <strong>Miller District</strong>
            </td>
            <td class="column">
                <asp:DropDownList ID="ddlMillerDistrict" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlMillerDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="column">
                <strong>Receiving District</strong>
            </td>
            <td class="column">
                <asp:DropDownList ID="ddlReceivingDistrict" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlReceivingDistrict_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="column" colspan="1">
                <strong>Miller Name</strong>
            </td>
            <td class="column" colspan="3">
                <asp:DropDownList ID="ddl_MillerName" AutoPostBack="true" Width="750px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddl_MillerName_SelectedIndexChanged">
                </asp:DropDownList></td>



        </tr>
        <tr>
            <td class="column">
                <strong>Agreement Number</strong>
            </td>
            <td class="column">
                <asp:DropDownList ID="ddl_AgreementNumber" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddl_AgreementNumber_SelectedIndexChanged1">
                </asp:DropDownList>
            </td>
            <td class="column">
                <strong>CMR Deposit Order</strong>
            </td>
            <td class="column">
                <asp:DropDownList ID="ddlCMRDO" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" OnSelectedIndexChanged="ddlCMRDO_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="height:18px;">
            <td colspan="4"></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:RadioButton ID="rdbPDSMO" runat="server" Text="PDS Movement Order" ForeColor="Black" Font-Bold="true" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbPDSMO_CheckedChanged" />
            </td>
            <td colspan="2">
                <asp:RadioButton ID="rdbMillMO" runat="server" Text="Mill Movement Order" ForeColor="Black" Font-Bold="true" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbMillMO_CheckedChanged" />
            </td>
        </tr>
        <tr style="height:18px;">
            <td colspan="4"></td>
        </tr>
        <tr id="trPDSMO" visible="false" runat="server">
            <td class="column" colspan="1">
                <strong> <asp:Label ID="lblMovementOrder" runat="server" ></asp:Label></strong>
            </td>
            <td class="column" colspan="3">
                <asp:DropDownList ID="ddlSMO" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" Visible="false" >
                </asp:DropDownList>
                 <asp:DropDownList ID="ddlMMO" AutoPostBack="true" Width="250px" CssClass="tddl" runat="server" Visible="false" >
                </asp:DropDownList>
                <asp:Button ID="bttAdd" runat="server" CssClass="button" Text="Add" OnClick="bttAdd_Click" />
            </td>

          </tr>
       

            <tr>
                <td colspan="4"></td>
            </tr>
            <tr id="trGrid" runat="server" visible="true">
                <td colspan="4">

                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="S. No." />

                            <asp:BoundField DataField="AgreeDist" HeaderText="Agreement District " />
                            <asp:BoundField DataField="MillerDistrict" HeaderText="Miller District" />
                            <asp:BoundField DataField="ReceivedDistrict" HeaderText="Receiving District" />
                            <asp:BoundField DataField="MillerName" HeaderText="Miller Name" />
                            <asp:BoundField DataField="Agreement" HeaderText="Agreement " />
                            <asp:BoundField DataField="CMRDO" HeaderText="CMR DO " />
                            <asp:BoundField DataField="MovementOrder" HeaderText="Movement Order" />



                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4" style="width: 1000px; height: 15px; text-align: center;">
                    <asp:Button ID="bttUpdate" runat="server" Text="Update" CssClass="buttonSubmit" Enabled="false" OnClick="bttUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 1000px; height: 15px; text-align: left;">
                    <asp:Button ID="bttNew" runat="server" Text="New" CssClass="button" OnClick="bttNew_Click" />
                </td>
                <td colspan="2" style="width: 1000px; height: 15px; text-align: right;">
                    <asp:Button ID="bttClose" runat="server" Text="Close" CssClass="button" OnClick="bttClose_Click" />
                </td>


            </tr>
    </table>

</asp:Content>

