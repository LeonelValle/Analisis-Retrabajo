using EstadiaMWE.Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class AltaUnidad : System.Web.UI.Page
    {
        private Globales pageControl;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["pageControl"] = pageControl;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pageControl = ViewState["pageControl"] as Globales ?? new Globales();
            //ARREGLAR LOS DEFECTOS PARA QUE PUEDAN SER VARIOS


            if (Session["Id_Usuario"] == null)
            {
                Modelos.Usuario usuario_actual = Conexion.obtenerUsuarioGuest();
                Session["Nombre_Empleado"] = usuario_actual.Nombre_Empleado;
                Session["Num_Empleado"] = usuario_actual.Num_Empleado;
                //pageControl.usuario_actual = Conexion.obtenerUsuarioGuest();
            }

            lblUsuario.Text = Session["Nombre_Empleado"].ToString();
            lblerror.Visible = false;
            lblerrorSN.Visible = false;
            lblErrorDefecto.Visible = false;

            if (!IsPostBack)
            {
                pageControl.num_serie.Clear();
                pageControl.defectos_seleccionados.Clear();

                LlenarDdlBD(ddlpartnumber, "CAT_NUMPARTE", "Num_Parte", "Id_NumParte");
                LlenarDdlBD(ddlDefecto, "CAT_DEFECTO", "FullDefecto", "Id_Defecto");
                LlenarDdlBD(ddlArea, "CAT_AREA", "Area", "Id_Area");


                ddlpartnumber.SelectedValue = "-1";
                ddlDefecto.SelectedValue = "-1";
                ddlArea.SelectedValue = "-1";
            }
        }


        #region eventos
        protected void gvNumSerie_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Eliminar";
                e.Row.Cells[1].Text = "Serial_Number";
            }
        }

        protected void gvDefectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Eliminar";
            }
        }

        protected void gvNumSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemToRemove = pageControl.num_serie.Single(r => r == gvNumSerie.SelectedRow.Cells[1].Text);
            pageControl.num_serie.Remove(itemToRemove);
            lblNumSerie.Text = "Numeros de serie: " + pageControl.num_serie.Count();
            LlenarGrid(true);
        }

        protected void gvDefectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemToRemove = pageControl.defectos_seleccionados.Single(x => x.Id_Defecto == Convert.ToInt32(gvDefectos.SelectedRow.Cells[1].Text)
                     && x.Referencia == gvDefectos.SelectedRow.Cells[5].Text);

            pageControl.defectos_seleccionados.Remove(itemToRemove);

            LlenarGridDefectos(true);
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
        #endregion

        #region botones

        protected void btnLimpiargv_Click(object sender, EventArgs e)
        {
            pageControl.num_serie.Clear();
            LlenarGrid(true);
        }
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {

                Conexion.Alta_Unidad(new Modelos.Unidad
                {
                    FK_PartNumber = Convert.ToInt32(ddlpartnumber.SelectedValue),
                    Work_Order = txtNumOrden.Text,
                    FK_Area = Convert.ToInt32(ddlArea.SelectedValue),
                    FK_Status = Conexion.ConsultaEstatus("RWK").Id_Status,
                }, "RWK", Session["Num_Empleado"].ToString(),
                pageControl.num_serie, pageControl.esNuevaUnidad, pageControl.defectos_seleccionados);

                LimpiarInfoUnidad();
                mostrarMensaje(lblerror, Color.Green, "Registro exitoso");
            }
            else
            {
                mostrarMensaje(lblerror, Color.Red, "Informacion incompleta");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarInfoUnidad();
        }
        protected void btnAgregarNumSerie_Click(object sender, EventArgs e)
        {
            if (validarNumSerie())
            {
                if (txtNumSerie.Text != "" && !pageControl.num_serie.Exists(r => r == txtNumSerie.Text))
                {
                    pageControl.num_serie.Add(txtNumSerie.Text);
                }
                else
                {
                    mostrarMensaje(lblerrorSN, Color.Red, "Numero de serie duplicado");

                }
                txtNumSerie.Text = "";
            }
            LlenarGrid(false);
        }
        protected void btnAgregarDefecto_Click(object sender, EventArgs e)
        {
            lblErrorDefecto.Visible = false;
            limpiarEntrada(txtReferencia);
            limpiarEntrada(txtpartnumber);
            if (txtReferencia.Text != "" && ddlDefecto.SelectedValue != "-1")
            {
                if (!pageControl.defectos_seleccionados.Any(x => x.Id_Defecto == Convert.ToInt32(ddlDefecto.SelectedValue)
                     && x.Referencia == txtReferencia.Text))
                {
                    pageControl.defectos_seleccionados.Add(new Modelos.Defecto
                    {
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
        #endregion

        #region metodos
        protected void LlenarDdlBD(AjaxControlToolkit.ComboBox ddl, string tabla, string nombre, string id)
        {
            ddl.DataSource = Conexion.ConsultaGeneral(tabla);
            ddl.DataTextField = nombre;
            ddl.DataValueField = id;
            ddl.DataBind();

            ListItem item = new ListItem { Text = "No seleccionada", Value = "-1" };
            ddl.Items.Add(item);

        }
        protected void LlenarDdlBD(DropDownList ddl, string tabla, string nombre, string id)
        {
            ddl.DataSource = Conexion.ConsultaGeneral(tabla);
            ddl.DataTextField = nombre;
            ddl.DataValueField = id;
            ddl.DataBind();

            ListItem item = new ListItem { Text = "No seleccionada", Value = "-1" };
            ddl.Items.Add(item);

        }
        protected bool validarCampos()
        {
            if (txtNumOrden.Text != "" && gvNumSerie.Rows.Count > 0 && ddlpartnumber.SelectedValue != "-1"
                && ddlArea.SelectedValue != "-1" && gvDefectos.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        protected void LlenarGrid(bool refresh)
        {
            if (pageControl.num_serie.Count != 0 || refresh)
            {
                gvNumSerie.DataSource = pageControl.num_serie;
                gvNumSerie.DataBind();
            }


            lblNumSerie.Text = "Numeros de serie: " + pageControl.num_serie.Count();

        }
        public bool validarNumSerie()
        {

            if (pageControl.num_serie.Count < 100)
            {

                return true;
            }
            else
            {
                mostrarMensaje(lblerrorSN, Color.Red, "Excede las 100 unidades por transaccion");
                return false;
            }
        }
        protected void LimpiarInfoUnidad()
        {

            lblNumSerie.Text = "Numeros de serie: 0";
            txtNumOrden.Text = "";
            txtNumSerie.Text = "";
            txtReferencia.Text = "";
            txtpartnumber.Text = "";
            ddlpartnumber.SelectedValue = "-1";
            ddlDefecto.SelectedValue = "-1";
            ddlArea.SelectedValue = "-1";

            pageControl.num_serie.Clear();
            pageControl.defectos_seleccionados.Clear();

            LlenarGrid(true);
            LlenarGridDefectos(true);
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
        public void mostrarMensaje(Label lblmensaje, Color colormensaje, string mensaje)
        {
            lblmensaje.Visible = true;
            lblmensaje.ForeColor = colormensaje;
            lblmensaje.Text = mensaje;
        }

        protected void LlenarGridDefectos(bool refresh)
        {
            if (pageControl.defectos_seleccionados.Count != 0 || refresh)
            {
                gvDefectos.DataSource = pageControl.defectos_seleccionados;
                gvDefectos.DataBind();
            }
        }

        #endregion


    }
}