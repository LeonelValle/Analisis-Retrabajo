<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="EstadiaMWE.Reportes" %>

<!DOCTYPE html>
<style type="text/css">
    body {
        text-align: center;
    }

    .grid {
        font-family: Calibri;
    }


    form {
        background-color: white;
        background-repeat: round;
        background-attachment: fixed;
        text-align: left;
        width: 100%;
        display: inline-block;
    }

    /*Botones*/

    .blueButton {
        box-shadow: inset 0px 1px 0px 0px #bee2f9;
        background-color: #63b8ee;
        border-radius: 6px;
        border: 1px solid #3866a3;
        display: inline-block;
        cursor: pointer;
        color: white;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #7cacde;
        margin-left: 0px;
    }

        .blueButton:disabled {
            background-color: #808080;
            color: black;
        }

        .blueButton:hover {
            background: linear-gradient(to bottom, #468ccf 5%, #63b8ee 100%);
            background-color: #468ccf;
        }

        .blueButton:active {
            position: relative;
            top: 1px;
        }



    .redButton {
        box-shadow: inset 0px 1px 0px 0px #f7c5c0;
        background: linear-gradient(to bottom, #fc8d83 5%, #e4685d 100%);
        background-color: #fc8d83;
        border-radius: 6px;
        border: 1px solid #d83526;
        display: inline-block;
        cursor: pointer;
        color: #ffffff;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #b23e35;
    }

        .redButton:disabled {
            background-color: #808080;
            color: black;
        }

        .redButton:hover {
            background: linear-gradient(to bottom, #e4685d 5%, #fc8d83 100%);
            background-color: #e4685d;
        }

        .redButton:active {
            position: relative;
            top: 1px;
        }

    .greenButton {
        box-shadow: inset 0px 1px 0px 0px #a4e271;
        background: linear-gradient(to bottom, #89c403 5%, #77a809 100%);
        background-color: #89c403;
        border-radius: 6px;
        border: 1px solid #74b807;
        display: inline-block;
        cursor: pointer;
        color: #ffffff;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #528009;
    }

        .greenButton:disabled {
            background-color: #808080;
            color: black;
        }

        .greenButton:hover {
            background: linear-gradient(to bottom, #77a809 5%, #89c403 100%);
            background-color: #77a809;
        }

        .greenButton:active {
            position: relative;
            top: 1px;
        }

    .orangeButton {
        box-shadow: inset 0px 1px 0px 0px #fff6af;
        background: linear-gradient(to bottom, #ffcc66 5%, #d18717 100%);
        background-color: #ffcc66;
        border-radius: 6px;
        border: 1px solid #ffaa22;
        display: inline-block;
        cursor: pointer;
        color: #333333;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #ffee66;
    }

        .orangeButton:disabled {
            background-color: #808080;
            color: black;
        }

        .orangeButton:hover {
            background: linear-gradient(to bottom, #d18717 5%, #ffcc66 100%);
            background-color: #d18717;
        }

        .orangeButton:active {
            position: relative;
            top: 1px;
        }

    .purpleButton {
        box-shadow: inset 0px 1px 0px 0px #e184f3;
        background: linear-gradient(to bottom, #ca77db 5%, #6b3875 100%);
        background-color: #ca77db;
        border-radius: 6px;
        border: 1px solid #a511c0;
        display: inline-block;
        cursor: pointer;
        color: #ffffff;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #9120a8;
    }

        .purpleButton:disabled {
            background-color: #808080;
            color: black;
        }

        .purpleButton:hover {
            background: linear-gradient(to bottom, #6b3875 5%, #ca77db 100%);
            background-color: #6b3875;
        }

        .purpleButton:active {
            position: relative;
            top: 1px;
        }

    .labelStyle {
        font-family: Calibri;
        font-size: medium;
    }

    .css-input {
        padding: 5px;
        font-size: medium;
        border-width: 0px;
        border-color: #CCCCCC;
        background-color: #FFFFFF;
        color: #000000;
        border-style: solid;
        border-radius: 7px;
        font-family: Calibri;
        box-shadow: 0px 0px 5px rgba(66,66,66,.75);
        text-shadow: -50px 0px 0px rgba(66,66,66,.0);
    }

        .css-input:focus {
            outline: none;
        }

    .largeCard {
        width: 80px;
        height: 80px;
        border-color: white;
        border-radius: 10px;
        border: 1px solid #ccc;
        cursor: pointer;
        display: inline-block;
    }

        .largeCard .largecard_image {
            width: inherit;
            height: inherit;
            text-align: center;
            border-radius: 40px;
        }

            .largeCard .largecard_image img {
                width: 50px;
                height: 50px;
                border-radius: 40px;
                object-fit: cover;
            }

        .largeCard .largecard_title {
            text-align: center;
            border-radius: 0px 0px 40px 40px;
            font-family: Calibri;
            font-size: 15px;
            margin-top: -50px;
            height: 40px;
        }


</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="left">
                    <div style="text-align: left; margin: 1px">
                        <a href="Menu.aspx" style="color: black;">
                            <div class="largeCard">
                                <div class="largecard_image">
                                    <img src="https://img.icons8.com/color/48/000000/logout-rounded-left--v1.png" />
                                </div>
                                <div class="largecard_title">
                                    <p>Volver</p>
                                </div>
                            </div>
                        </a>
                        <asp:Label Style="font-family: Calibri; position: absolute; top: 10px; left: 47%;" runat="server" Text="Reportes" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label Style="font-family: Calibri" runat="server" Text="Usuario actual:" Font-Bold="true" Font-Size="Larger"></asp:Label>
                        <asp:Label Style="font-family: Calibri" ID="lblUsuario" runat="server" Text="Label" Font-Size="Larger"></asp:Label>
                    </div>
                </div>
                <br />
                <br />
                <div>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Fecha entrada:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtFechaEntrada" runat="server" AutoCompleteType="Disabled" CssClass="css-input" TextMode="Date" onkeypress="return false;" onpaste="return false"  OnTextChanged="txtFechaEntrada_TextChanged"></asp:TextBox>

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label8" runat="server" Text="Fecha salida:"></asp:Label>
                    &nbsp;&nbsp;
        <asp:TextBox ID="txtFechaSalida" runat="server" AutoCompleteType="Disabled" CssClass="css-input" TextMode="Date" onkeypress="return false;" onpaste="return false"  OnTextChanged="txtFechaSalida_TextChanged"></asp:TextBox>

                    &nbsp;<br /> <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label1" CssClass="labelStyle" runat="server" Text="Estatus actual"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
         <ajaxToolkit:ComboBox ID="ddlStatus" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
             AutoCompleteMode="Append">
         </ajaxToolkit:ComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" CssClass="labelStyle" runat="server" Text="Part Number"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
  <ajaxToolkit:ComboBox ID="ddlPartNumber" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
      AutoCompleteMode="Append">
  </ajaxToolkit:ComboBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" CssClass="labelStyle" runat="server" Text="Work order"></asp:Label>
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWo" AutoCompleteType="Disabled" CssClass="css-input" runat="server" Height="16px" Width="129px" MaxLength="20"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" CssClass="labelStyle" runat="server" Text="Serial Number"></asp:Label>
                    &nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="txtSerialNum" CssClass="css-input" runat="server" Height="16px" Width="129px" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="label" CssClass="labelStyle" runat="server" Text="Turno"></asp:Label>
                    &nbsp;
        <asp:DropDownList ID="ddlTurno" CssClass="css-input" runat="server">
            <asp:ListItem Value="-1">Select All</asp:ListItem>
            <asp:ListItem Value="1">Primero</asp:ListItem>
            <asp:ListItem Value="2">Segundo</asp:ListItem>
        </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label3" CssClass="labelStyle" runat="server" Text="Numero de empleado"></asp:Label>
                    &nbsp;&nbsp;
            <asp:TextBox ID="txtEmpleado" CssClass="css-input" runat="server" Height="16px" Width="129px" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnBuscar" CssClass="greenButton" runat="server" BackColor="#33CC33" Height="42px" OnClick="btnBuscar_Click" Text="Buscar" Width="112px" />
                    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnLimpiar" CssClass="blueButton" runat="server" BackColor="#33CCFF" Height="42px" OnClick="btnLimpiar_Click" Text="Limpiar" Width="112px" />
                    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExportar" CssClass="orangeButton" runat="server" BackColor="#FFCC66" Height="42px" Text="Exportar" Width="112px" OnClick="btnExportar_Click" />
                    <br />
                    &nbsp;
                </div>
                <div id="divScroll" style="display: inline-block; overflow-y: scroll; height: 500px; width: 100%">
                    <asp:GridView runat="server" ID="gvBitacora" OnRowCreated="gvReportes_RowCreated" CssClass="grid"></asp:GridView>
                </div>

                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnExportar" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
