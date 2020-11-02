<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="AcceptRegistration_OtherState.aspx.cs" Inherits="PaddyMilling_AcceptRegistration_OtherState" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .subtablesub
        {
            width: 960px;
            text-align: center;
            background-color: #fff;
            border-radius: 8px;
            color: black;
            box-shadow: 10px 10px 5px rgba(0,0,0,0.5);
            letter-spacing: 2px;
            font-family: Georgia;
            border-collapse: collapse;
            border-color: black;
        }


        .table
        {
            width: 960px;
            text-align: center;
            background-color: rgba(0, 0, 0, 0.83);
            border-radius: 8px;
            color: #fff;
            font-size: 20px;
            letter-spacing: 4px;
            font-family: Georgia;
        }

            .table:hover
            {
                background-color: rgba(0, 0, 0, 0.83);
            }

        .colomn
        {
            width: 240px;
            text-align: left;
        }


        .ddl
        {
            border-radius: 8px;
            height: 20px;
            width: 240px;
            letter-spacing: 2px;
            font-family: Georgia;
            text-align: center;
            font-weight: bolder;
        }

        .txt
        {
            border-radius: 8px;
            height: 20px;
            width: 240px;
            letter-spacing: 2px;
            font-family: Georgia;
            text-align: center;
            font-weight: bolder;
        }

        .button
        {
            border-radius: 8px;
            background-color: rgb(127, 158, 221);
            width: 240px;
            color: Black;
            letter-spacing: 2px;
            font-family: Georgia;
            font-weight: 400;
        }

            .button:hover
            {
                background-color: rgba(10, 10, 10, 0.81);
                color: #fff;
            }
    </style>

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.GridView1.Rows.Count %>');

                        //Get total no. of checked CheckBoxes in side the GridView.
                        Counter = 0;
                    }

                    function HeaderClick(CheckBox) {
                        //Get target base & child control.
                        var TargetBaseControl =
                            document.getElementById('<%= this.GridView1.ClientID %>');
                        var TargetChildControl = "chk_select";

                        //Get all the control of the type INPUT in the base control.
                        var Inputs = TargetBaseControl.getElementsByTagName("input");

                        //Checked/Unchecked all the checkBoxes in side the GridView.
                        for (var n = 0; n < Inputs.length; ++n)
                            if (Inputs[n].type == 'checkbox' &&
                                      Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                                Inputs[n].checked = CheckBox.checked;

                        //Reset Counter
                        Counter = CheckBox.checked ? TotalChkBx : 0;
                    }

                    function ChildClick(CheckBox, HCheckBox) {
                        //get target control.
                        var HeaderCheckBox = document.getElementById(HCheckBox);

                        //Modifiy Counter; 
                        if (CheckBox.checked && Counter < TotalChkBx)
                            Counter++;
                        else if (Counter > 0)
                            Counter--;

                        //Change state of the header CheckBox.
                        if (Counter < TotalChkBx)
                            HeaderCheckBox.checked = false;
                        else if (Counter == TotalChkBx)
                            HeaderCheckBox.checked = true;
                    }
    </script>


    <table style="width: 1000px; text-align: center;">
        <tr>
            <td style="width: 20px"></td>
            <td>
                <table style="width: 960px; text-align: center;">
                    <tr>
                        <td>
                            <table style="width: 960px; text-align: center;">
                                <tr>
                                    <td class="table">Approve Miller Registration (Other State)
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px; background-color: #fff;"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="subtablesub" border="1">
                                            <tr>
                                                <%-- <td colspan="2" style="height: 10px; text-align: left; font-size: 20px; color: rgb(127, 158, 221);"><a href="../District/PaddyMillingHome.aspx" style="color: black; font-size: 25px; text-decoration: none;">
                                                    
                                                    &#9754 

                                                </a></td>--%>

                                                <td colspan="4" style="height: 10px; text-align: right; font-size: 20px; color: rgb(127, 158, 221);"><a href="AcceptRegistration_OtherState.aspx" style="color: rgb(127, 158, 221); font-size: 25px; text-decoration: none;">&#8635 
                                                     

                                                </a></td>

                                            </tr>

                                            <tr>
                                                <td class="colomn" colspan="2" style="text-align: left; padding-left: 40px">Crop Year
                                                </td>
                                                <td class="colomn" colspan="2" style="text-align: left; padding-left: 40px">
                                                    <asp:DropDownList ID="ddlCropyear" CssClass="ddl" runat="server" Height="25px" Width="240px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="colomn" colspan="2" style="text-align: left; padding-left: 40px">Miller State
                                                </td>
                                                <td class="colomn" colspan="2" style="text-align: left; padding-left: 40px">
                                                    <asp:DropDownList ID="ddlOtherState" CssClass="ddl" runat="server" Height="25px" Width="240px" AutoPostBack="true" OnSelectedIndexChanged="ddlOtherState_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </td>



                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                            <tr>

                                           
                                            <td colspan="4">
                                                <asp:Button ID="bttnSearch" runat="server" Height="30px" Text="Search" CssClass="button" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="bttnSearch_Click" />

                                            </td>
                                                 </tr>

                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>

                                            <tr runat="server" id="tr1" visible="false">
                                                <td colspan="4">
                                                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" EnableModelValidation="True" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderWidth="1px" OnRowCreated="GridView1_RowCreated">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_select" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkBxHeader"
                                                                        onclick="javascript:HeaderClick(this);" runat="server" />
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>



                                                            <asp:BoundField DataField="Mill_Name" HeaderText="मिल का नाम" SortExpression="Mill_Name">
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="operator_name" HeaderText="मिल संचालक का नाम" SortExpression="operator_name">
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>


                                                            <asp:BoundField DataField="District" HeaderText="जिला" SortExpression="Mill_Name">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Tehsil" HeaderText="तहसील" SortExpression="operator_name">

                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="milling_capacity_arwa" HeaderText="मिलिंग क्षमता (अरवा)" SortExpression="operator_name">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="milling_capacity_usna" HeaderText="मिलिंग क्षमता (उसना)" SortExpression="Mill_Name">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mill_phone" HeaderText="टेलीफ़ोन न." SortExpression="operator_name">
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundField>

                                                            <asp:TemplateField HeaderText="मोबाइल नंबर" SortExpression="Miller_MobileNo">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Miller_MobileNo") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Miller_MobileNo" runat="server" Text='<%# Bind("Miller_MobileNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="Created_date" HeaderText="मिल पंजीकरण का दिनांक" SortExpression="operator_name" DataFormatString="{0:d}">


                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>


                                                            <asp:TemplateField HeaderText="Registration" ItemStyle-CssClass="hideGridColumn" HeaderStyle-CssClass="hideGridColumn">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Registration_ID") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Registration_ID") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                                                                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                                            </asp:TemplateField>


                                                        </Columns>


                                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                        <HeaderStyle BackColor="black" Font-Bold="true" ForeColor="#FFFFCC" Font-Size="Small" />
                                                        <PagerStyle BackColor="#FFFFCC" ForeColor="black" HorizontalAlign="Center" Font-Size="Small" />
                                                        <RowStyle BackColor="White" ForeColor="black" Font-Size="Small" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" Font-Size="Small" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                            <tr runat="server" id="tr2" visible="false">
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                                                                        <tr runat="server" id="tr3" visible="false">

                                           
                                            <td colspan="4">
                                              

                                                 <asp:Button ID="btnAccept" Visible="false" runat="server" Height="30px"  Text="Approval" CssClass="button" Style="margin-left: 10px" OnClick="btnAccept_Click" OnClientClick="return confirm('Are You Sure To Accept Records?');" Enabled="False" />

                                            </td>
                                                 </tr>
                                            <tr runat="server" id="tr4" visible="false">
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>

</asp:Content>

