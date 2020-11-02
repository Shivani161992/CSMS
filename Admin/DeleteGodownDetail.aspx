<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteGodownDetail.aspx.cs" Inherits="State_DeleteGodownDetail" MasterPageFile="~/MasterPage/AdminLogin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 623px"> 
        <tr>
            <td colspan="2" style="background-color: #cccccc; width: 748px;">
    <asp:Label ID="lblreceipt" runat="server" Text="Delete Godown Details" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" style="width: 748px; background-color: #cccccc; height: 90px;">
            <table>
                                 <tr>
                                            <td colspan="2" style="background-color: #ccccff; color: #ffffff; height: 71px; width: 333px;" align="left">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="width: 100px; height: 24px;">
                                                            <asp:Label ID="lbl_dist" runat="server" Font-Bold="True" Text="District :-" ForeColor="Navy"></asp:Label></td>
                                                        <td style="width: 210px; height: 24px;">
                                                            <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100px">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Issue Center :-" ForeColor="Navy"></asp:Label></td>
                                                        <td style="width: 210px">
                                                            <asp:DropDownList ID="ddl_IC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_IC_SelectedIndexChanged">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100px; height: 26px;">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Navy" Text="Godown Name"></asp:Label></td>
                                                        <td style="width: 210px; height: 26px;">
                                                <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" >
                                                </asp:DropDownList>&nbsp;
                                                            <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Maroon" NavigateUrl="~/Admin/DeleteGodownDetail.aspx"
                                                                Visible="False">New Search</asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                                &nbsp;
                                            </td>
                                        </tr>

                                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 748px; height: 11px; background-color: #cccccc">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 748px; height: 11px; background-color: #cccccc">
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <table style="width: 454px; height: 186px">
                        <tr>
                            <td colspan="3">
                            
                            <asp:Label id="lable" runat="server" Text="Opening Balance" ForeColor="#004000"></asp:Label><asp:Label id="lbl_opn" runat="server" ForeColor="Maroon"></asp:Label>
                <asp:GridView ID="GridView_opn" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnPageIndexChanging="GridView_opn_PageIndexChanging"
                    PagerSettings-Visible="True" Width="652px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Source_Name" HeaderText="Source Of Arrival" SortExpression="Source_Name">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Opening Qty." SortExpression="Quantity">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bags" HeaderText="Opening Bags" SortExpression="Bags">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Current_Balance" HeaderText="Current Balance" SortExpression="Current_Balance">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Current_Bags" HeaderText="Current Bags" SortExpression="Current_Bags">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Godown" HeaderText="GdID" SortExpression="Godown">
                            <HeaderStyle Font-Size="1px" />
                            <ItemStyle Font-Size="1px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DistiD" HeaderText="distid">
                            <HeaderStyle Font-Size="0px" />
                            <ItemStyle Font-Size="0px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="depid" HeaderText="depoid">
                            <HeaderStyle Font-Size="0px" />
                            <ItemStyle Font-Size="0px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 21px">
                            </td>
                            <td style="width: 100px; height: 21px">
                            </td>
                            <td style="width: 100px; height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table style="width: 512px; height: 60px">
                                    <tr>
                                        <td colspan="2" style="height: 21px">
                <asp:Label ID="Label4" runat="server" Text="Reciept Details" ForeColor="#004000"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                <asp:Label
                    ID="lblfrom_proc" runat="server" Text="From Procurement" Width="179px" ForeColor="#004000"></asp:Label>
                                            <asp:Label ID="lblprcgrid" runat="server" BorderColor="White" ForeColor="Maroon"
                                                Width="60px"></asp:Label></td>
                                        <td style="width: 100px">
                            <asp:Label ID="Label3" runat="server" Text="From other Source" Width="138px" ForeColor="#004000"></asp:Label>
                                            <asp:Label ID="lbl_otrgrid" runat="server" ForeColor="#C00000"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 295px">
                <asp:GridView ID="dgrid_Proc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Height="96px" OnPageIndexChanging="dgrid_Proc_PageIndexChanging"
                    PagerSettings-Visible="True" Width="273px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TC_Number" HeaderText="TC No" SortExpression="TC_Number">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Recd_Bags" HeaderText="Recd_Bags">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Recd_Qty" HeaderText="Recd_Qty">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                            <HeaderStyle Font-Size="11px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                                        </td>
                                        <td style="width: 100px; height: 295px">
                            <asp:GridView ID="GridView_other" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Height="96px" OnPageIndexChanging="GridView_other_PageIndexChanging"
                    PagerSettings-Visible="True" Width="358px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown">
                                        <HeaderStyle Font-Size="11px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="challan_no" HeaderText="challan_no">
                                        <HeaderStyle Font-Size="11px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Source_Name" HeaderText="Source ">
                                        <HeaderStyle Font-Size="11px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 378px">
                                <table style="width: 476px; height: 117px">
                                    <tr>
                                        <td style="width: 100px; height: 40px;">
                            <asp:Label ID="Label5" runat="server" Text="Iissue against D.O. details" Width="176px" ForeColor="#004000"></asp:Label>
                                            <asp:Label ID="lbl_IssagDo" runat="server" ForeColor="Maroon"></asp:Label></td>
                                        <td style="width: 100px; height: 40px;">
                            <asp:Label ID="Label6" runat="server" Text="Truck chalan  details" Width="144px" ForeColor="#004000"></asp:Label>
                                            <asp:Label ID="lbl_tcd" runat="server" ForeColor="Maroon"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                            <asp:GridView ID="GridView_Iado" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" Height="168px" OnPageIndexChanging="GridView_Iado_PageIndexChanging" PagerSettings-Visible="true " Width="319px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown">
                                        <ItemStyle Font-Size="Small" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="challan_no" HeaderText="Challan No." ReadOnly="True" SortExpression="challan_no">
                                        <HeaderStyle CssClass="gridlarohead" />
                                        <ItemStyle CssClass="griditemlaro" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Source_Name" HeaderText="Source " SortExpression="Source_Name">
                                        <HeaderStyle CssClass="gridlarohead" />
                                        <ItemStyle CssClass="griditemlaro" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" CssClass="gridheader" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                                        </td>
                                        <td style="width: 100px">
                            <asp:GridView ID="GridView_trck_chn" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView_trck_chn_PageIndexChanging" PagerSettings-Visible="true " Width="354px">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown">
                                        <HeaderStyle Font-Size="X-Small" />
                                        <ItemStyle Font-Size="Small" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="challan_no" HeaderText="Challan No." ReadOnly="True" SortExpression="challan_no">
                                        <HeaderStyle CssClass="gridlarohead" />
                                        <ItemStyle CssClass="griditemlaro" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown">
                                        <HeaderStyle Font-Size="X-Small" />
                                        <ItemStyle Font-Size="Small" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" CssClass="gridheader" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                        </td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </asp:Panel>
                <table style="width: 462px">
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                <asp:Button ID="btn_del" runat="server" OnClick="btn_del_Click" OnClientClick="javascript:return confirm('Are you sure to delete all godown records?');" 
 Text="Delete Record" Visible="False" /></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                  
                </table>
            </td>
        </tr>
       
      
       
     
     
    </table>
    
    
</asp:Content>
