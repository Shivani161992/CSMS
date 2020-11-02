<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feb.aspx.cs" Inherits="salaryslip_Feb" %>

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
            font-size: medium;
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
            height:1000px;
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
         .image
         {}
    </style>
</head>
<body  onload="window.print()"
    <form id="form1" runat="server">
         <br />
        <div class="A4">
            <table align="center" class="auto-style1">
                <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                         <asp:Image ID="Image1" ImageUrl="~/SalImg/2F.png" Width="750px" Height="" CssClass="image" runat="server" />
                    </td>
                </tr>
                  <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                  <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                 <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                 <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                 <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                <tr>
                    <td style="width:100%; height:40px; text-align:center;">
                           
                    </td>
                </tr>
                </table>
    
    </div>
    </form>
</body>
</html>
