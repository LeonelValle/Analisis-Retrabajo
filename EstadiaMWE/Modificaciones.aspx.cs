using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class Modificaciones : System.Web.UI.Page
    {
        private Globales pageControl;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["pageControl"] = pageControl;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pageControl = ViewState["pageControl"] as Globales ?? new Globales();

            if (Session["Nombre_Empleado"] == null || Convert.ToInt32(Session["FK_TipoUsuario"]) != Conexion.obtenerTipoUsuario("Admin"))
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {

                lblUsuario.Text = Session["Nombre_Empleado"].ToString();

                if (!IsPostBack)
                {
                    lblError.Visible = false;
                    lbSerialNumbers.Visible = false;
                    lblSerialN.Visible = false;
                    btnSelecTodos.Enabled = false;
                    pageControl.esMultiseleccion = false;
                    lblErrorDefecto.Visible = false;
                    LlenarGrid(Conexion.ConsultaUnidadEstatus(new Modelos.Status { Nombre_Status = "All" }));

                    LlenarDdlBD(ddlPartNumber, "CAT_NUMPARTE", "Num_Parte", "Id_NumParte");
                    LlenarDdlBD(ddlDefecto, "CAT_DEFECTO", "FullDefecto", "Id_Defecto");
                    LlenarDdlBD(ddlStatus, "CAT_STATUS", "Nombre_status", "Id_Status");
                    LlenarDdlBD(ddlArea, "CAT_AREA", "Area", "Id_Area");

                    ddlPartNumber.SelectedValue = "-1";
                    ddlDefecto.SelectedValue = "-1";
                    ddlArea.SelectedValue = "-1";
                    ddlStatus.SelectedValue = "-1";


                    ModificarInfoUnidad(false);

                }

            }

        }

        #region Eventos

        //GRIDVIEW PRINCIPAL
        protected void gvAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {



            lblError.Visible = false; //No mostrar mensaje de error
            //RefrescarGridDefectos();
            RefrescarGrid();
            if (gvAnalisis.Rows.Count > 1)
            {
                btnSelecTodos.Enabled = true; //seleccionar todos de la misma orden
            }
            ModificarUnidadesSelec(gvAnalisis.SelectedRow);

            CambiarColorRenglones();

            ModificarInfoUnidad(true);

        }

        protected void gvAnalisis_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        //GRIDVIEW DEFECTOS
        protected void gvDefectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            EliminarDefecto();
        }

        protected void gvDefectos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 3)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
        }

        //DDL DEFECTOS
        protected void btnAgregarDefecto_Click(object sender, EventArgs e)
        {
            AgregarDefecto();
        }


        #endregion

        #region Eventos_Botones
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CamposValidos())
            {
                if (pageControl.esMultiseleccion)
                {
                    ActualizarUnidadesSelec();

                    foreach (Modelos.Unidad unidad in pageControl.unidades_seleccionadas)
                    {
                        pageControl.unidad_seleccionada = unidad;
                        Conexion.Modificar_Unidad(Conexion.ConsultaEstatus(ddlStatus.SelectedItem.Text), Session["Num_Empleado"].ToString(),
                           pageControl.unidad_seleccionada, pageControl.defectos_seleccionados);
                    }
                }
                else
                {
                    ActualizarUnidadSelec();
                    Conexion.Modificar_Unidad(Conexion.ConsultaEstatus(ddlStatus.SelectedItem.Text), Session["Num_Empleado"].ToString(),
                        pageControl.unidad_seleccionada, pageControl.defectos_seleccionados);
                }

                RefrescarGrid();
                CambiarColorRenglones();
                MostrarInfoUnidad();
                mostrarMensaje(lblError, Color.Green, "Cambio exitoso!");
            }
            else
            {
                mostrarMensaje(lblError, Color.Red, "Informacion incompleta");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarInfoUnidadSelec();
        }

        protected void btnSelecTodos_Click(object sender, EventArgs e)
        {
            pageControl.esMultiseleccion = false;
            // pageControl.unidades_seleccionadas.Clear();
            PermitirMultiseleccion();

            ModificarUnidadesSelec(gvAnalisis.SelectedRow);

            foreach (GridViewRow row in gvAnalisis.Rows)
            {
                if (row != gvAnalisis.SelectedRow)
                {
                    ModificarUnidadesSelec(row);
                }
            }

            RefrescarGrid();
            CambiarColorRenglones();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pageControl.esMultiseleccion = true; //lo dejamos en true para que el metodo lo cambie a false
            PermitirMultiseleccion();
            RefrescarGrid();
            LimpiarInfoUnidadSelec();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            pageControl.esMultiseleccion = true;
            PermitirMultiseleccion();
            RefrescarGrid();
            LimpiarBusqueda();
            LimpiarInfoUnidadSelec();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            PermitirMultiseleccion();

            LimpiarInfoUnidadSelec();
        }

        #endregion

        #region Metodos

        //GRIDVIEW DEFECTOS

        protected void LlenarGridDefectos(bool refresh)
        {
            if (pageControl.defectos_seleccionados.Count != 0 || refresh)
            {
                gvDefectos.DataSource = pageControl.defectos_seleccionados;
                gvDefectos.DataBind();
            }
        }
        protected void RefrescarGridDefectos()
        {
            pageControl.defectos_seleccionados.Clear();
            pageControl.defectos_seleccionados = Conexion.ConsultaDefectosLista(Convert.ToInt32(gvAnalisis.SelectedRow.Cells[1].Text));

            LlenarGridDefectos(true);
        }


        //DROPDOWNLIST
        protected void LlenarDdlBD(AjaxControlToolkit.ComboBox ddl, string tabla, string nombre, string id)
        {
            ddl.DataSource = Conexion.ConsultaGeneral(tabla);
            ddl.DataTextField = nombre;
            ddl.DataValueField = id;
            ddl.DataBind();

            ListItem item = new ListItem { Text = "No seleccionada", Value = "-1" };
            ddl.Items.Add(item);
        }

        //GRIDVIEW PRINCIPAL
        protected void LlenarGrid(DataSet ds)
        {
            gvAnalisis.DataSource = null;
            gvAnalisis.DataSource = ds;
            gvAnalisis.DataBind();
            try
            {
                gvAnalisis.DataBind();
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void PermitirMultiseleccion()
        {
            pageControl.unidades_seleccionadas.Clear();

            if (pageControl.esMultiseleccion)
            {
                btnSeleccionar.Text = "Seleccionas varios";
                pageControl.esMultiseleccion = false;
            }
            else
            {
                btnSeleccionar.Text = "Seleccionando...";
                pageControl.esMultiseleccion = true;
            }
        }

        public void RefrescarGrid()
        {
            if (esBusqueda())
            {
                LlenarGrid(Conexion.ConsultaUnidadFiltro(new Modelos.Status { Nombre_Status = "All" }, txtBuscarWO.Text, txtBuscarSN.Text));
            }
            else
            {
                LlenarGrid(Conexion.ConsultaUnidadEstatus(new Modelos.Status { Nombre_Status = "All" }));
            }

            //  ModificarInfoUnidad(false);
        }

        protected void CambiarColorRenglones()
        {
            if (pageControl.esMultiseleccion)
            {
                foreach (GridViewRow row in gvAnalisis.Rows)
                {
                    if (pageControl.unidades_seleccionadas.Exists(r => r.Id_Unidad == Convert.ToInt32(row.Cells[1].Text)))
                    {
                        row.BackColor = Color.Pink;
                    }
                    else
                    {
                        //row.BackColor = Color.White;
                        //row.ForeColor = Color.Black;
                        //row.Font.Bold = false;
                    }
                }
            }
        }

        //UNIDADES
        public void ModificarUnidadesSelec(GridViewRow row)
        {
            if (pageControl.esMultiseleccion)
            {

                if (!pageControl.unidades_seleccionadas.Exists(r => r.Id_Unidad == Convert.ToInt32(row.Cells[1].Text)))
                {
                    if (UnidadesIguales(row))
                    {

                        //pageControl.unidades_seleccionadas.Add(new Modelos.Unidad
                        //{
                        //    Id_Unidad = Convert.ToInt32(row.Cells[1].Text),
                        //    Work_Order = HttpUtility.HtmlDecode(row.Cells[2].Text),
                        //    FK_PartNumber = Convert.ToInt32((ddlPartNumber.Items.FindByText(row.Cells[4].Text)).Value),
                        //    Serial_Number = HttpUtility.HtmlDecode(row.Cells[5].Text),
                        //    FK_Status = Convert.ToInt32((ddlStatus.Items.FindByText(row.Cells[6].Text)).Value),
                        //    Falla = HttpUtility.HtmlDecode(row.Cells[7].Text)
                        //});

                        Modelos.Unidad nueva_unidad = new Modelos.Unidad();
                        nueva_unidad.Id_Unidad = Convert.ToInt32(row.Cells[1].Text);
                        nueva_unidad.Work_Order = HttpUtility.HtmlDecode(row.Cells[2].Text);
                        nueva_unidad.FK_PartNumber = Convert.ToInt32((ddlPartNumber.Items.FindByText(row.Cells[4].Text)).Value);
                        nueva_unidad.Serial_Number = HttpUtility.HtmlDecode(row.Cells[5].Text);
                        nueva_unidad.FK_Status = Convert.ToInt32((ddlStatus.Items.FindByText(row.Cells[6].Text)).Value);
                        nueva_unidad.Falla = HttpUtility.HtmlDecode(row.Cells[7].Text);
                        if (row.Cells[3].Text != "N/A")
                        {
                            nueva_unidad.FK_Area = Convert.ToInt32((ddlArea.Items.FindByText(row.Cells[3].Text)).Value);
                        }
                        else
                        {
                            nueva_unidad.FK_Area = -2;
                        }

                        pageControl.unidades_seleccionadas.Add(nueva_unidad);

                        MostrarInfoUnidad();
                        RefrescarGridDefectos();
                    }
                }
                else
                {
                    var itemToRemove = pageControl.unidades_seleccionadas.Single(r => r.Id_Unidad == Convert.ToInt32(row.Cells[1].Text));
                    pageControl.unidades_seleccionadas.Remove(itemToRemove);

                    lblSerialN.Text = "Serial Numbers: " + pageControl.unidades_seleccionadas.Count;
                    lbSerialNumbers.DataSource = pageControl.unidades_seleccionadas;
                    lbSerialNumbers.DataTextField = "Serial_Number";
                    lbSerialNumbers.DataBind();
                }


                //if (UnidadesIguales(row))
                //{
                //    MostrarInfoUnidad();
                //}
                //else
                //{
                //    //gvAnalisis.SelectedIndex = -1;
                //    // btnSelecTodos.Enabled = false;
                //}
            }
            else
            {
                //RefrescarGridDefectos();
                MostrarInfoUnidad();
            }

        }


        public void ActualizarUnidadSelec()
        {
            pageControl.unidad_seleccionada.Id_Unidad = Convert.ToInt32(gvAnalisis.SelectedRow.Cells[1].Text);
            pageControl.unidad_seleccionada.FK_PartNumber = Convert.ToInt32(ddlPartNumber.SelectedValue);
            pageControl.unidad_seleccionada.Work_Order = txtWorkOrder.Text;
            pageControl.unidad_seleccionada.FK_Area = Convert.ToInt32(ddlArea.SelectedValue);
            pageControl.unidad_seleccionada.Serial_Number = txtSerialNumber.Text;
            pageControl.unidad_seleccionada.FK_Status = Convert.ToInt32(ddlStatus.SelectedValue);
            pageControl.unidad_seleccionada.Falla = txtFalla.Text;
        }

        public void ActualizarUnidadesSelec()
        {
            foreach (Modelos.Unidad unidad in pageControl.unidades_seleccionadas)
            {
                unidad.FK_PartNumber = Convert.ToInt32(ddlPartNumber.SelectedValue);
                unidad.Work_Order = txtWorkOrder.Text;
                unidad.FK_Area = Convert.ToInt32(ddlArea.SelectedValue);
                unidad.FK_Status = Convert.ToInt32(ddlStatus.SelectedValue);
                unidad.Falla = txtFalla.Text;
            }
        }

        protected void LimpiarInfoUnidad()
        {
            txtWorkOrder.Text = "";
            txtSerialNumber.Text = "";
            txtFechaEntrada.Text = "";
            txtReferencia.Text = "";
            txtFalla.Text = "";
            ddlPartNumber.SelectedValue = "-1";
            ddlDefecto.SelectedValue = "-1";
            ddlArea.SelectedValue = "-1";
            ddlStatus.SelectedValue = "-1";

            if (!pageControl.esMultiseleccion)
            {
                lbSerialNumbers.Visible = false;
                lblSerialN.Visible = false;
                txtSerialNumber.Visible = true;
                Label3.Visible = true;
            }
            else
            {
                // lbSerialNumbers.Items.Clear();
                txtSerialNumber.Visible = false;
                Label3.Visible = false;
            }

        }
        protected void LimpiarInfoUnidadSelec()
        {
            gvAnalisis.SelectedIndex = -1;
            btnSelecTodos.Enabled = false;

            gvDefectos.DataSource = null;
            gvDefectos.DataBind();

            // lbSerialNumbers.Items.Clear();

            LimpiarInfoUnidad();
            RefrescarGrid();
        }

        protected void MostrarInfoUnidad()
        {
            LimpiarInfoUnidad();
            RefrescarGridDefectos();
            txtFechaEntrada.Text = HttpUtility.HtmlDecode(Conexion.consultaFechaAlta(gvAnalisis.SelectedRow.Cells[1].Text));
            txtWorkOrder.Text = HttpUtility.HtmlDecode(gvAnalisis.SelectedRow.Cells[2].Text);

            ddlPartNumber.SelectedValue = (ddlPartNumber.Items.FindByText(gvAnalisis.SelectedRow.Cells[4].Text)).Value;
            ddlDefecto.SelectedValue = (ddlDefecto.Items.FindByText("No seleccionada")).Value;

            if (!pageControl.esMultiseleccion)
            {
                txtSerialNumber.Text = HttpUtility.HtmlDecode(gvAnalisis.SelectedRow.Cells[5].Text);
                lbSerialNumbers.Visible = false;
                lblSerialN.Visible = false;
                txtSerialNumber.Visible = true;
                Label3.Visible = true;
            }
            else
            {
                txtSerialNumber.Visible = false;
                Label3.Visible = false;
                lbSerialNumbers.DataSource = pageControl.unidades_seleccionadas;
                lbSerialNumbers.Visible = true;
                lblSerialN.Visible = true;
                lblSerialN.Text = "Serial Numbers: " + pageControl.unidades_seleccionadas.Count;
                lbSerialNumbers.DataTextField = "Serial_Number";
                lbSerialNumbers.DataBind();
            }

            if (gvAnalisis.SelectedRow.Cells[3].Text != "N/A")
            {
                ddlArea.SelectedValue = (ddlArea.Items.FindByText(gvAnalisis.SelectedRow.Cells[3].Text)).Value;
            }
            else
            {
                ddlArea.SelectedValue = "-1";
            }


            ddlStatus.SelectedValue = HttpUtility.HtmlDecode((ddlStatus.Items.FindByText(gvAnalisis.SelectedRow.Cells[6].Text)).Value);
            txtFalla.Text = HttpUtility.HtmlDecode(gvAnalisis.SelectedRow.Cells[7].Text);

        }

        protected void ModificarInfoUnidad(bool modificable)
        {

            if (ddlArea.SelectedValue != "-1")
            {
                ddlArea.Enabled = modificable;
            }
            else
            {
                ddlArea.Enabled = false;
            }

            if (gvDefectos.Rows.Count > 0)
            {
                paneldefectos.Enabled = modificable;
            }
            else
            {
                paneldefectos.Enabled = false;
            }

            if (txtFalla.Text != "N/A")
            {
                txtFalla.Enabled = modificable;
            }
            else
            {
                txtFalla.Enabled = false;
            }

            txtFechaEntrada.Enabled = false;
            txtReferencia.Enabled = modificable;
            txtWorkOrder.Enabled = modificable;
            ddlPartNumber.Enabled = modificable;
            lbSerialNumbers.Enabled = false;
            txtSerialNumber.Enabled = modificable;
            //  ddlDefecto.Enabled = modificable;
            ddlStatus.Enabled = false;
            //  txtFalla.Enabled = modificable;

            //lbSerialNumbers.Visible = false;
        }

        //VALIDACIONES
        protected bool CamposValidos()
        {

            limpiarEntrada(txtReferencia);
            limpiarEntrada(txtWorkOrder);
            limpiarEntrada(txtFalla);
            limpiarEntrada(txtSerialNumber);

            if (ddlStatus.SelectedItem.Text == "Analisis")
            {
                if (txtWorkOrder.Text != "" && txtFalla.Text != ""
                    && ddlPartNumber.SelectedValue != "-1")
                {
                    if (pageControl.esMultiseleccion)
                    {
                        if (lbSerialNumbers.Items.Count != 0)
                        {
                            return true;
                        }
                    }
                    else if (txtSerialNumber.Text == "")
                    {
                        return false;
                    }

                    return true;
                }
                return false;
            }

            else
            {
                if (gvDefectos.Rows.Count != 0 && ddlPartNumber.SelectedValue != "-1"
               && txtWorkOrder.Text != "")
                {
                    if (pageControl.esMultiseleccion)
                    {
                        if (lbSerialNumbers.Items.Count != 0)
                        {
                            return true;
                        }
                    }
                    else if (txtSerialNumber.Text == "")
                    {
                        return false;
                    }

                    return true;
                }
                return false;
            }
        }

        public void mostrarMensaje(Label lblmensaje, Color colormensaje, string mensaje)
        {
            lblmensaje.Visible = true;
            lblmensaje.ForeColor = colormensaje;
            lblmensaje.Text = mensaje;
        }

        protected bool UnidadesIguales(GridViewRow row)
        {

            int area = -2;

            if (row.Cells[3].Text != "N/A")
            {
                area = Convert.ToInt32(ddlArea.Items.FindByText(row.Cells[3].Text).Value);
            }

            if (pageControl.unidades_seleccionadas.Count > 0)
            {
                if (pageControl.unidades_seleccionadas[0].Work_Order == HttpUtility.HtmlDecode(row.Cells[2].Text) &&
                  pageControl.unidades_seleccionadas[0].Falla == HttpUtility.HtmlDecode(row.Cells[7].Text) &&
                  pageControl.unidades_seleccionadas[0].FK_Area == area)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            //Es la primera entrada
            return true;
        }

        //DEFECTOS
        protected void AgregarDefecto()
        {
            lblErrorDefecto.Visible = false;
            limpiarEntrada(txtReferencia);

            if (txtReferencia.Text != "" && ddlDefecto.SelectedValue != "-1")
            {
                if (!pageControl.defectos_seleccionados.Any(x => x.Id_Defecto == Convert.ToInt32(ddlDefecto.SelectedValue)
                     && x.Referencia == txtReferencia.Text))
                {
                    pageControl.defectos_seleccionados.Add(new Modelos.Defecto {
                        Id_Defecto = Convert.ToInt32(ddlDefecto.SelectedValue),
                        defecto = ddlDefecto.SelectedItem.Text,
                        Referencia = txtReferencia.Text,
                        Part_Number = txtpartnumber.Text
                    });
                    LlenarGridDefectos(false);
                }
                else
                {
                    mostrarMensaje(lblErrorDefecto, Color.Red, "Defecto duplicado");
                }
            }
            else
            {
                mostrarMensaje(lblErrorDefecto, Color.Red, "Defecto incorrecto");
            }

        }

        protected void EliminarDefecto()
        {
            lblErrorDefecto.Visible = false;
            var itemToRemove = pageControl.defectos_seleccionados.Single(x => x.Id_Defecto == Convert.ToInt32(gvDefectos.SelectedRow.Cells[1].Text)
                        && x.Referencia == gvDefectos.SelectedRow.Cells[5].Text);
            pageControl.defectos_seleccionados.Remove(itemToRemove);
            LlenarGridDefectos(true);
        }

        //BUSQUEDA
        protected bool esBusqueda()
        {
            limpiarEntrada(txtBuscarWO);
            limpiarEntrada(txtBuscarSN);

            if (txtBuscarWO.Text != "" || txtBuscarSN.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void LimpiarBusqueda()
        {
            txtBuscarSN.Text = "";
            txtBuscarWO.Text = "";
        }

        public void limpiarEntrada(TextBox textbox)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "\n", "%" };

            foreach (var c in charsToRemove)
            {
                textbox.Text = textbox.Text.Replace(c, string.Empty);
            }

            textbox.Text = textbox.Text.Replace("\"", string.Empty);
        }


        #endregion


    }
}