<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="RePrint_StackRejection.aspx.cs" Inherits="IssueCenter_RePrint_StackRejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .auto-styleNSC
        {
            width: 622px;
        }

        .InspecTable
        {
            width: 1100px;
            border-top: 6px groove #D9D9D9;
            border-bottom: 6px groove #D9D9D9;
            border-left: 6px groove #D9D9D9;
            border-right: 6px groove #D9D9D9;
            border-radius: 8px;
            /*box-shadow: 0 5px 10px rgba(0,0,0,.5);
           
            background-color:rgb(229, 230, 228);*/
        }

        .InspHead
        {
        }

        .InspColumn
        {
            width: 33%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: Almendra;
            font-size: 13px;
            font-weight: bold;
        }

        .insptxt
        {
            width: 300px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insfixtext
        {
            width: 310px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insptxt:focus
        {
            border: none;
        }

        .inspddl
        {
            width: 310px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            padding-left: 10px;
            border-bottom-style: groove;
        }

            .inspddl::selection
            {
                border-radius: 8px;
            }

        /*.inspddl:focus
            {
                border-radius: 8px;
            }*/

        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: Almendra;
            height: 30px;
            width: 400px;
        }

            .bttsub:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .bttsubother
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #00AAA0;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family:Almendra;
            height: 30px;
            width: 150px;
        }

            .bttsubother:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }
    </style>

    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../IssueCenter/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy;"> Reprint CMR Stack Acceptance/Rejection</h2>
                    <input type="hidden" runat="server" id="hdfDist" />
                    <input type="hidden" runat="server" id="hdfInspID" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="Stack_Rejection.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>

        </table>
        <table class="InspecTable">

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px;">CropYear
                    <br />

                    <asp:DropDownList ID="ddlCropyear" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">District
                    <br />

                    <asp:DropDownList ID="ddldist" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddldist_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddldist" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Issue Center
                    <br />

                    <asp:DropDownList ID="ddlIC" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlIC" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="InspColumn" style="padding-left: 20px;">Godown
                    <br />

                    <asp:DropDownList ID="ddlgodown" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlgodown" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Stack Number
                    <br />

                    <asp:DropDownList ID="ddlstack" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlstack_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlstack" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>

                <td class="InspColumn" style="padding-left: 20px;">Acceptance/Rejection No.
                    <br />

                    <asp:DropDownList ID="ddlaccepReject" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlaccepReject_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlaccepReject" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;">
                    <center>
                        <asp:Button ID="bttaccept" runat="server" Text="Print" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="bttaccept_Click" />
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

        </table>
    </center>
</asp:Content>

