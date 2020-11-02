<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PM_Inspector_Approval.aspx.cs" Inherits="PaddyMilling_Inspection_Rice_wheat_PM_Inspector_Approval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" lang="javascript" src="../Scripts/jquery-2.1.1.js"></script>
    <link href="../calendar/calendar_blue.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../calendar/calendar.js"></script>


    <style>
        body
        {
            background-color: #5D6D7E;
        }

        .column
        {
            height: 60px;
            color: #6B8E23;
            font-family: 'Lucida Fax';
            text-align: center;
            font-size: 35px;
        }

        .tcolumn
        {
            width: 400px;
            text-align: center;
            border-radius: 32px;
            height: 35px;
            font-size: 15px;
            font-family: 'Lucida Fax';
        }

            .tcolumn:focus
            {
                box-shadow: 0 0 25px rgb(107,142,35);
                padding: 3px 0px 3px 3px;
                margin: 5px 1px 3px 0px;
                border: 1px solid rgba(81, 203, 238, 1);
            }

        .tddl
        {
            width: 400px;
            border-radius: 32px;
            height: 35px;
            text-align: center;
        }

        .bttn
        {
            width: 400px;
            height: 50px;
            border-radius: 25px;
            color: #fff!important;
            background-color: #000!important;
            border-radius: 32px;
        }

        .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
        {
            color: #1B2631!important;
            background-color: #6B8E23!important;
            font-family: 'Lucida Fax';
            font-size: 25px;
        }

        .bttn:active
        {
            background-color: yellow;
        }

        .txt
        {
        }

            .txt:focus
            {
            }

        .table
        {
            background-color: #1B2631;
            width: 1267px;
            border-radius: 32px;
            margin-right: 35px;
            text-align: center;
        }

        .select
        {
            background-color: white;
            color: green;
            font-size: 15px;
        }
    </style>
</head>
<body>

    <div style="width: 100%; height: 45px; background-color: #1B2631; border-radius: 32px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    <asp:HyperLink ID="HyperLink3" runat="server" ForeColor="#6B8E23" NavigateUrl="~/PaddyMilling/Inspection(Rice_wheat)/Inspection_Registration.aspx">Register</asp:HyperLink>

                </td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;"></td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;"></td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    <asp:HyperLink ID="HyperLink5" runat="server" ForeColor="#6B8E23" NavigateUrl="~/PaddyMilling/Inspection(Rice_wheat)/Inspector_Login.aspx">Approve Inspector</asp:HyperLink>
                </td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="#6B8E23" NavigateUrl="~/PaddyMilling/Inspection(Rice_wheat)/Inspector_Login.aspx">Login</asp:HyperLink>
                </td>
            </tr>
        </table>

    </div>

    <form id="form2" runat="server" style="width: 100%;">
        <div style="margin-top: 25px; float: right; width: 100%">


            <table class="table">
                <tr>
                    <td colspan="4" class="column">Approve Inspectors
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                        <table style="width: 1267px; background-color:#5D6D7E;">
                            <tr>
                                <td style="width: 1267px">


                                    <asp:GridView ID="GridView2" Width="100%" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" ShowFooter="false" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                        OnRowDataBound="GridView2_RowDataBound" Style="font-size: small">
                                        <AlternatingRowStyle BackColor="White" />

                                        <Columns>
                                            <asp:CommandField HeaderText="Action" ShowSelectButton="True" HeaderStyle-CssClass="select" />


                                            <asp:BoundField DataField="district_name" HeaderText="Name">
                                                <HeaderStyle Font-Names="Arial" BackColor="white" Font-Size="15px" ForeColor="Green" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Max_distance" HeaderText="Designation">
                                                <HeaderStyle Font-Names="Arial" BackColor="white" Font-Size="15px" ForeColor="Green" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="district_code" HeaderText="Posting Place">
                                                <HeaderStyle Font-Names="Arial" BackColor="white" Font-Size="15px" ForeColor="Green" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="district_code" HeaderText="Date Of Joining">
                                                <HeaderStyle Font-Names="Arial" BackColor="white" Font-Size="15px" ForeColor="Green" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="district_code" HeaderText="Email Id">
                                                <HeaderStyle Font-Names="Arial" BackColor="white" Font-Size="15px" ForeColor="Green" />
                                            </asp:BoundField>
                                        </Columns>


                                    </asp:GridView>
                                </td>
                </tr>
                </table>
                    </td>
                </tr>
                 
    <tr>
        <td style="text-align: center; height: 60px; font-size: large;">

            <asp:Button ID="bttlogin" runat="server" Text="Approve" CssClass="bttn w3-grey" OnClick="bttlogin_Click" /></td>
    </tr>
                <tr>
                    <td colspan="4" class="column"></td>
                </tr>
            </table>
        </div>
        <table style="height: 175px;">
            <tr>
                <td></td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" width="780" align="center" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; height: 66px;">
            <tr>
                <td style="height: 10px; font-weight: bold; font-size: 12px; color: White;" align="center">Site Designed and Hosted By:</td>
                <td style="height: 10px; font-weight: bold; font-size: 12px; color: White;" align="center">Contents Provided By:</td>
            </tr>
            <tr>
                <td style="height: 82px; text-align: center">
                    <p>
                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
                            ForeColor="White" Height="47px" Width="320px" Font-Bold="True">National Informatics Centre, Madhya Pradesh
Ministry of Communications and Information Technology
Department of Information Technology</asp:HyperLink>&nbsp;
                    </p>
                </td>
                <td style="height: 82px; text-align: center;">
                    <p class="bot">
                        <b><a href="">
                            <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
                                ForeColor="White" Height="35px" Width="338px">Madhya Pradesh State Civil Supplies Corporation Ltd. Block-1, 3rd Floor, Paryavas Bhavan, Jail Road, Bhopal- 462011 India  Phones : 91-755-2768590, Fax :91-755-2677847 Email : mpscsc@bsnl.in </asp:HyperLink></a></b>
                    </p>
                </td>
            </tr>
        </table>

    </form>

</body>
</html>
