using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class Alta_Analisis : System.Web.UI.Page
    {
        private Globales pageControl;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["pageControl"] = pageControl;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pageControl = ViewState["pageControl"] as Globales ?? new Globales();
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

            if (!IsPostBack)
            {
                pageControl.num_serie.Clear();
                pageControl.defectos_seleccionados.Clear();
                ddlpartnumber.DataSource = Conexion.ConsultaGeneral("CAT_NUMPARTE");
                ddlpartnumber.DataTextField = "Num_Parte";
                ddlpartnumber.DataValueField = "Id_NumParte";
                ddlpartnumber.DataBind();
                ListItem item = new ListItem { Text = "No seleccionada", Value = "-1" };
                ddlpartnumber.Items.Add(item);
                ddlpartnumber.SelectedValue = "-1";
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

        protected bool validarCampos()
        {
            if (txtNumOrden.Text != "" && txtFalla.Text != "" && gvNumSerie.Rows.Count > 0 && ddlpartnumber.SelectedValue != "-1")
            {
                return true;
            }

            return false;
        }

        protected void gvNumSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemToRemove = pageControl.num_serie.Single(r => r == gvNumSerie.SelectedRow.Cells[1].Text);
            pageControl.num_serie.Remove(itemToRemove);
            lblNumSerie.Text = "Numeros de serie: " + pageControl.num_serie.Count();
            LlenarGrid(true);
        }
        #endregion

        #region botones
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            limpiarEntrada(txtFalla);

            if (validarCampos())
            {
                Conexion.Alta_Unidad(new Modelos.Unidad
                {
                    FK_PartNumber = Convert.ToInt32(ddlpartnumber.SelectedValue),
                    Work_Order = txtNumOrden.Text,
                    FK_Status = Conexion.ConsultaEstatus("Analisis").Id_Status,
                    Falla = txtFalla.Text
                }, "Analisis", Session["Num_Empleado"].ToString(),
                pageControl.num_serie, pageControl.esNuevaUnidad, pageControl.defectos_seleccionados);

                LimpiarInfoUnidad();
                mostrarMensaje(lblerror, Color.DarkSeaGreen, "Registro exitoso");
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
            if (txtNumSerie.Text != "" && !pageControl.num_serie.Exists(r => r == txtNumSerie.Text))
            {
                pageControl.num_serie.Add(txtNumSerie.Text);
            }
            else
            {
                mostrarMensaje(lblerrorSN, Color.Red, "Numero de serie incorrecto");
            }
            txtNumSerie.Text = "";

            LlenarGrid(false);
        }

        protected void btnLimpiargv_Click(object sender, EventArgs e)
        {
            pageControl.num_serie.Clear();
            LlenarGrid(true);
        }
        #endregion

        #region metodos
        //protected void LlenarDdlBD(DropDownList ddl, string tabla, string nombre, string id)
        //{
        //    ddl.DataSource = Conexion.ConsultaGeneral(tabla);
        //    ddl.DataTextField = nombre;
        //    ddl.DataValueField = id;
        //    ddl.DataBind();

        //    ListItem item = new ListItem { Text = "No seleccionada", Value = "-1" };
        //    ddl.Items.Add(item);  
        //}

        protected void LlenarGrid(bool refresh)
        {
            if (pageControl.num_serie.Count != 0 || refresh)
            {
                gvNumSerie.DataSource = pageControl.num_serie;
                gvNumSerie.DataBind();
            }
            lblNumSerie.Text = "Numeros de serie: " + pageControl.num_serie.Count();
        }

        //public bool validarNumSerie()
        //{
        //    if (pageControl.num_serie.Count < 100){
        //        return true;
        //    }
        //    else{
        //        mostrarMensaje(lblerrorSN, Color.Red, "Excede las 100 unidades por transaccion");
        //        return false;
        //    }
        //}

        protected void LimpiarInfoUnidad()
        {

            lblNumSerie.Text = "Numeros de serie: 0";
            txtNumOrden.Text = "";
            txtNumSerie.Text = "";
            txtFalla.Text = "";

            ddlpartnumber.SelectedValue = "-1";

            pageControl.num_serie.Clear();
            LlenarGrid(true);
        }

        public void limpiarEntrada(TextBox textbox)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "\n" };

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

        #endregion

    }
}