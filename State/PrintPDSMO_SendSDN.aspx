<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPDSMO_SendSDN.aspx.cs" Inherits="State_PrintPDSMO_SendSDN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .A4 {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: large;
        }

        .LineBreak {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
        }

        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

        p {
            margin-right: 0in;
            margin-left: 0in;
            font-size: 12.0pt;
            font-family: "Times New Roman","serif";
        }

        @page {
            size: auto; /* auto is the current printer page size */
            margin: 25px 25px 0px 30px; /* this affects the margin in the printer settings */
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style3 {
            width: 100%;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <div class="A4">
            <br />
            <br />

            <table align="center" class="auto-style1">
                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0"> 
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" /></td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">मुख्यालय भोपाल</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt; <span class="auto-style2">PDS MOVEMENT ORDER</span> &gt;&gt; </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">एम०ओ० क्रमांक : ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>....<asp:Label ID="lblCancel" runat="server" Text="[Cancelled]" Style="font-weight: 700" Visible="False"></asp:Label></td>
                    <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate" runat="server"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">कमोडिटी : ....<asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">परिवहन का माध्यम : ....<asp:Label ID="lblTransMode" runat="server" Text=""></asp:Label>....</td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
