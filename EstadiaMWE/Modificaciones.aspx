<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modificaciones.aspx.cs" Inherits="EstadiaMWE.Modificaciones" %>

<!DOCTYPE html>

<style type="text/css">
    body {
        background-attachment: fixed;
        text-align: center;
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

    a {
        color: red;
        text-decoration: none;
        font-family: Calibri;
    }

    .overlay {
        position: relative;
    }

    form {
        background-color: white;
        background-repeat: round;
        background-attachment: fixed;
        text-align: left;
        /*width: 80%;*/
        display: inline-block;
    }

    .css-input {
        padding: 5px;
        font-size: medium;
        font-family: Calibri;
        border-width: 0px;
        border-color: #CCCCCC;
        background-color: #FFFFFF;
        color: #000000;
        border-style: solid;
        border-radius: 7px;
        box-shadow: 0px 0px 5px rgba(66,66,66,.75);
        text-shadow: -50px 0px 0px rgba(66,66,66,.0);
    }

        .css-input:focus {
            outline: none;
        }

        .css-input:disabled {
            background-color: rgba(209, 204, 204, 0.76);
        }


    .left, .right {
        height: 100%;
        position: fixed;
        z-index: 1;
        top: 0;
    }

    .left {
        overflow-x: hidden;
        left: 0;
        width: 40%;
    }

    .right {
        width: 60%;
        right: 0;
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
        margin-bottom: 0px;
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

        .ajax__combobox_itemlist {
        position: fixed !important;
    }

</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Modificaciones</title>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>

</head>
<body>
  <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <br />
        <%-- LADO IZQUIERDO --%>
        <div class="left">
            <div style="text-align: left; margin: 8px">

                <a href="Menu.aspx" style="color: black">
                    <div class="largeCard">
                        <div class="largecard_image">
                            <img src="https://img.icons8.com/color/48/000000/logout-rounded-left--v1.png" />
                        </div>
                        <div class="largecard_title">
                            <p>Volver</p>
                        </div>
                    </div>
                </a>
                <asp:Label Style="font-family: Calibri; position: absolute; top: 10px; left: 43%;" runat="server" Text="Modificaciones" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label Style="font-family: Calibri" runat="server" Text="Usuario actual:" Font-Bold="true" Font-Size="Larger"></asp:Label>
                <asp:Label Style="font-family: Calibri" ID="lblUsuario" runat="server" Text="Label" Font-Size="Larger"></asp:Label>
            </div>
            <br />
            <%-- Primer Panel --%>
            <asp:UpdatePanel ID="PanelInfoUnidad" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <fieldset>
                        <legend class="labelStyle">Informacion de la(s) unidad(es):</legend>
                        &nbsp;
                        <br />
                        <asp:Label ID="Label11" CssClass="labelStyle" runat="server" Text="Fecha entrada"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtFechaEntrada" CssClass="css-input" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label10" CssClass="labelStyle" runat="server" Text="Status actual"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
             <ajaxToolkit:ComboBox ID="ddlStatus" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
                 AutoCompleteMode="Append">
             </ajaxToolkit:ComboBox>
                        <br />
                        <br />
                        <asp:Label ID="Label6" CssClass="labelStyle" runat="server" Text="Part Number"></asp:Label>
                        &nbsp;&nbsp;
      <ajaxToolkit:ComboBox ID="ddlPartNumber" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
          AutoCompleteMode="Append">
      </ajaxToolkit:ComboBox>
                        <br />
                        <br />
                        <asp:Label ID="Label2" CssClass="labelStyle" runat="server" Text="Work order"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtWorkOrder" AutoCompleteType="Disabled" CssClass="css-input" runat="server" MaxLength="20"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label1" CssClass="labelStyle" runat="server" Text="Falla"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtFalla" Style="resize: none;" AutoCompleteType="Disabled" CssClass="css-input" runat="server" Height="35px" TextMode="MultiLine" Width="197px" MaxLength="30"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label3" CssClass="labelStyle" runat="server" Text="Serial Number"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtSerialNumber" CssClass="css-input" AutoCompleteType="Disabled" runat="server" MaxLength="30"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblSerialN" CssClass="labelStyle" runat="server" Text="Serial Numbers:"></asp:Label>
                        <br />
                        <br />
                        &nbsp;&nbsp;
                        <asp:ListBox CssClass="css-input" ID="lbSerialNumbers" runat="server" Height="84px" Width="207px"></asp:ListBox>
                        <br />
                        <br />
                        <asp:Label ID="Label5" CssClass="labelStyle" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        <asp:Label runat="server" CssClass="labelStyle" Text="Area" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <ajaxToolkit:ComboBox ID="ddlArea" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
       AutoCompleteMode="Append">
   </ajaxToolkit:ComboBox>
                        <br />
                        <br />
                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvAnalisis" EventName="SelectedIndexChanged" />

                    <asp:AsyncPostBackTrigger ControlID="btnSeleccionar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSelecTodos" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                </Triggers>

            </asp:UpdatePanel>

            <%-- Segundo Panel --%>
            
             <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">

                <ContentTemplate>
                    <fieldset>
                        <asp:Panel runat="server" ID="paneldefectos">
                            <legend class="labelStyle">Defecto(s):</legend>
                            <br />
                            <br />
                            <asp:Label runat="server" CssClass="labelStyle" Text="Defecto" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <ajaxToolkit:ComboBox ID="ddlDefecto" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
             AutoCompleteMode="Append">
         </ajaxToolkit:ComboBox>
                            <br />
                            <br />
                            <asp:Label ID="Label15" runat="server" CssClass="labelStyle" ForeColor="Red" Text="*" />
                            <asp:Label ID="Label19" runat="server" CssClass="labelStyle" Text="Referencia" />
                            &nbsp; &nbsp;&nbsp;
                            <asp:TextBox ID="txtReferencia" runat="server" AutoCompleteType="Disabled" CssClass="css-input" MaxLength="20"></asp:TextBox>
                            <br />
                            <br />
                            &nbsp;
                            <asp:Label ID="Label18" CssClass="labelStyle" runat="server" Text="Numero de Parte del Componente"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtpartnumber" runat="server" AutoCompleteType="Disabled" CssClass="css-input" MaxLength="20"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="btnAgregarDefecto" runat="server" class="blueButton" OnClick="btnAgregarDefecto_Click" Text="Agregar" Width="138px" />
                            <br />
                            <asp:Label ID="lblErrorDefecto" CssClass="labelStyle" runat="server" ForeColor="#FF3300" Text="Defecto incorrecto o duplicado"></asp:Label>
                            <br />
                            <br />
                            <asp:Label runat="server" CssClass="labelStyle" Text="Defectos de la unidad:" />
                            <br />
                            <br />
                            <asp:GridView ID="gvDefectos" runat="server" AutoGenerateSelectButton="True" Style="font-family: Calibri"
                                OnSelectedIndexChanged="gvDefectos_SelectedIndexChanged" OnRowCreated="gvDefectos_RowCreated">
                            </asp:GridView>
                            <br />
                        </asp:Panel>
                    </fieldset>
                    <asp:Label ID="lblError" CssClass="labelStyle" runat="server" Text="Informacion incompleta" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGuardar" CssClass="blueButton" runat="server" Text="Guardar cambios" Height="45" Width="182px" OnClick="btnGuardar_Click" />
                    &nbsp;&nbsp;&nbsp
                    <asp:Button ID="btnCancelar" CssClass="redButton" runat="server" Text="Cancelar" Height="45" Width="182px" BackColor="#FF6666" OnClick="btnCancelar_Click" />
                    <br />
                    <br />
                    <br />

                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAgregarDefecto" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="gvDefectos" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="gvAnalisis" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSeleccionar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSelecTodos" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />



                </Triggers>
            </asp:UpdatePanel>
        </div>
        <%-- FIN LADO IZQUIERDO --%>
        <%-- LADO DERECHO --%>
        <div class="right">
            <%-- Tercer panel --%>
            <asp:UpdatePanel ID="PanelUnidades" runat="server" UpdateMode="Conditional">

                <ContentTemplate>
                    <fieldset>
                        <legend class="labelStyle">Buscar unidad(es):</legend>
                        <br />
                        &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label12" CssClass="labelStyle" runat="server" Text="Work order"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtBuscarWO" AutoCompleteType="Disabled" runat="server" CssClass="css-input" MaxLength="20"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label13" CssClass="labelStyle" runat="server" Text="Serial Number"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtBuscarSN" AutoCompleteType="Disabled" runat="server" CssClass="css-input" MaxLength="30"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnBuscar" CssClass="greenButton" runat="server" Height="42px" Text="Buscar" Width="112px" OnClick="btnBuscar_Click" />
                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnLimpiar" CssClass="blueButton" runat="server" Height="42px" Text="Limpiar" Width="112px" OnClick="btnLimpiar_Click" />
                        <br />
                        <br />
                    </fieldset>
                    <div runat="server" id="divAdmin">
                        <fieldset>
                            <legend class="labelStyle">Seleccionar unidad(es):</legend>
                            <asp:Button ID="btnSeleccionar" CssClass="purpleButton" runat="server" Height="37px" Text="Seleccionar varias" Width="234px" OnClick="btnSeleccionar_Click" />
                            &nbsp;&nbsp;
                        <asp:Button ID="btnSelecTodos" runat="server" CssClass="blueButton" Height="32px" OnClick="btnSelecTodos_Click" Text="Seleccionar toda la orden" Width="233px" />
                            <br />
                        </fieldset>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvAnalisis" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSeleccionar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSelecTodos" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div id="divScroll" style="display: inline-block; overflow-y: scroll; height: 500px; width: 100%">
                <asp:UpdatePanel ID="panelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvAnalisis" runat="server" AutoGenerateSelectButton="True" AutoGenerateColumns="false"
                            BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" Style="font-family: Calibri"
                            ForeColor="Black" OnSelectedIndexChanged="gvAnalisis_SelectedIndexChanged" Width="100%" OnRowCreated="gvAnalisis_RowCreated">

                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#9999FF" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="Gray" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                            <Columns>
                                <asp:BoundField HeaderText="Id_Unidad" DataField="Id_Unidad" />
                                <asp:BoundField HeaderText="Work Order" DataField="Work Order" />
                                <asp:BoundField HeaderText="Area" DataField="Area" />
                                <asp:BoundField HeaderText="Part Number" DataField="Part Number" />
                                <asp:BoundField HeaderText="Serial Number" DataField="Serial Number" />
                                <asp:BoundField HeaderText="Estatus" DataField="Estatus" />

                                <asp:BoundField HeaderText="Falla" DataField="Falla">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvAnalisis" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSeleccionar" EventName="Click" />
                        
                        <asp:AsyncPostBackTrigger ControlID="btnSelecTodos" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>

</body>
</html>

