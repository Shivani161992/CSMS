<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Reprint_Print_Insp_ByOneMember_paddy.aspx.cs" Inherits="IssueCenter_Reprint_Print_Insp_ByOneMember_paddy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <style>
            .tddl
            {
                width: 400px;
                border-radius: 32px;
                height: 35px;
                text-align: center;
                font-family: 'Lucida Fax';
                text-align: center;
            }

            .bttn
            {
                width: 600px;
                height: 40px;
                border-radius: 25px;
                color: #fff!important;
                background-color: #D4AC0D!important;
                border-radius: 32px;
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
            {
                color: #1B2631!important;
                background-color: #D4AC0D!important;
                font-family: 'Lucida Fax';
                font-size: 25px;
            }

            .bttn:active
            {
                background-color: yellow;
            }
        </style>
    </head>
    <body>
        <table style="width: 700px; border-color: black; background-color: #AEB6BF;">
            <tr>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <table border="1" style="width: 700px; border-color: black; background-color: #1B2631;">
                        <tr>
                            <td colspan="2" style="text-align: center; font-size: 25px; color: #D4AC0D; background-color: #1B2631;">
                                <b>Reprint  </b>
                                <br />
                                <b>Inspection Acceptance/Rejection Note </b>

                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Inspector Name</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_Insp" runat="server" class="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddl_Insp_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Godown </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlgd" runat="server" class="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlgd_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Stack No. </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSK" runat="server" class="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlSK_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Acceptance/Rejection No. </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlacptRej" runat="server" class="tddl" AutoPostBack="True"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 50px; text-align: center;">
                                <asp:Button ID="bttRegister" runat="server" Text="Print" CssClass="bttn w3-grey" OnClick="bttprint_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3"></td>
            </tr>
        </table>

    </body>
    </html>




</asp:Content>

