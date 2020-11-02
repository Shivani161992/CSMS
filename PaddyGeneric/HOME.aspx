<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HOME.aspx.cs" Inherits="PaddyGeneric_HOME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Base + Vendors CSS -->
    <link href="css/Font.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/fonts/font-awesome/css/font-awesome.css">
    <!-- Theme CSS-->
    <link rel="stylesheet" href="css/theme.css">

    <style>
        #serviceBox {
            width: 100%;
            margin: 0 auto;
            margin-top: 75px;
            height: 250px;
            border: 1px solid black;
        }

        .serviceBox {
            margin-right: 4%;
            float: left;
            width: 20%;
            height: 550px;
            background-color: white;
            border: 1px solid #bdbdbd;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            -moz-box-shadow: 0 0 10px #bdbdbd;
            -webkit-box-shadow: 0 0 10px #bdbdbd;
            box-shadow: 0 0 10px #bdbdbd;
            box-sizing: border-box;
        }

            .serviceBox:first-child {
                margin-left: 4%;
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;">
                <tr>
                    <td>States of India</td>
                </tr>

            </table>
        </div>
        <div id="Div1">
            <div class="serviceBox">
                <div class="widget_categories widget widget__footer">
                    <div class="widget-content">
                        <ul>
                            <li><a href="#">Web Design</a> (3)</li>
                            <li><a href="#">Illustration</a> (10)</li>
                            <li><a href="#">Logo Design</a> (12)</li>
                            <li><a href="#">Branding &amp; Identity</a> (8)</li>
                            <li><a href="#">WordPress</a> (3)</li>
                            <li><a href="#">HTML5 &amp; CSS3</a> (5)</li>
                            <li><a href="#">PHP/MySQl</a> (3)</li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="serviceBox">
                <h2>Heading 2</h2>
                <div class="widget_categories widget widget__footer">
                    <div class="widget-content">
                            <li><a href="#">Web Design</a> (3)</li>
                            <li><a href="#">Illustration</a> (10)</li>
                            <li><a href="#">Logo Design</a> (12)</li>
                            <li><a href="#">Branding &amp; Identity</a> (8)</li>
                            <li><a href="#">WordPress</a> (3)</li>
                            <li><a href="#">HTML5 &amp; CSS3</a> (5)</li>
                            <li><a href="#">PHP/MySQl</a> (3)</li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="serviceBox">
                <h2>Heading 3</h2>
                <p>Information</p>
            </div>
            <div class="serviceBox">
                <h2>Heading 4</h2>
                <p>Information</p>
            </div>
        </div>
    </form>
</body>
</html>







