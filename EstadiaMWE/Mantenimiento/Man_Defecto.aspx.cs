using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE.Mantenimiento
{
    public partial class Man_Defecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Nombre_Empleado"] == null || Convert.ToInt32(Session["FK_TipoUsuario"]) != Conexion.obtenerTipoUsuario("Admin"))
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {
                lblerror.Visible = false;
                lblUsuario.Text = Session["Nombre_Empleado"].ToString();
                if (!IsPostBack)
                {
                    // LlenarGrid(Conexion.ConsultaGeneral("CAT_DEFECTO"));
                    btnModificar.Enabled = false;
                }
            }
        }

        #region eventos
        protected void gvConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarInfoUnidad();
            esModificacion();
        }

        protected void gvConsulta_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
        }

        #endregion

        #region botones
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (camposValidos())
            {
                if (!Conexion.Existe("select count(*) from CAT_DEFECTO where Codigo = '" + txtCodigo.Text.Trim() + "'"))
                {
                    Conexion.Alta_Defecto(new Modelos.Defecto { Codigo = txtCodigo.Text, Descripcion = txtDesc.Text });
                    mostrarMensaje(lblerror, Color.DarkSeaGreen, "Registro exitoso");
                    LlenarGrid(null);
                    LimpiarInfoUnidad();
                    txtBuscarCodigo.Text = "";
                }
                else
                {
                    mostrarMensaje(lblerror, Color.Red, "Ya existe el registro");
                }
            }
            else
            {
                mostrarMensaje(lblerror, Color.Red, "Informacion incompleta");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarInfoUnidad();
            lblerror.Visible = false;

            gvConsulta.SelectedIndex = -1;
            esModificacion();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (camposValidos() && gvConsulta.SelectedIndex != -1)
            {
                Conexion.Modificar_Defecto(new Modelos.Defecto { Id_Defecto = Convert.ToInt32(gvConsulta.SelectedRow.Cells[1].Text), Codigo = txtCodigo.Text, Descripcion = txtDesc.Text });
                mostrarMensaje(lblerror, Color.DarkSeaGreen, "Modificacion exitosa");
                LlenarGrid(null);
                LimpiarInfoUnidad();
                txtBuscarCodigo.Text = "";

                gvConsulta.SelectedIndex = -1;
                esModificacion();
            }
            else
            {
                mostrarMensaje(lblerror, Color.Red, "Informacion incompleta");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LimpiarInfoUnidad();

            gvConsulta.SelectedIndex = -1;
            esModificacion();

            if (txtBuscarCodigo.Text != "")
            {
                LlenarGrid(Conexion.ConsultaGeneral("CAT_DEFECTO", "Codigo", "'" + txtBuscarCodigo.Text + "'"));
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarCodigo.Text = "";

            gvConsulta.SelectedIndex = -1;
            esModificacion();

            LimpiarInfoUnidad();
            LlenarGrid(null);
        }

        #endregion

        #region metodos
        protected void LlenarGrid(DataSet ds)
        {
            gvConsulta.DataSource = null;
            gvConsulta.DataSource = ds;
            gvConsulta.DataBind();
            try
            {
                gvConsulta.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void limpiarEntrada(TextBox textbox)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "\n", "%", "-" };

            foreach (var c in charsToRemove)
            {
                textbox.Text = textbox.Text.Replace(c, string.Empty);
            }

            textbox.Text = textbox.Text.Replace("\"", string.Empty);
        }

        protected void MostrarInfoUnidad()
        {
            LimpiarInfoUnidad();
            txtCodigo.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[2].Text);
            txtDesc.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[3].Text);

        }

        private void LimpiarInfoUnidad()
        {
            txtCodigo.Text = "";
            txtDesc.Text = "";
        }

        protected bool camposValidos()
        {
            limpiarEntrada(txtCodigo);
            limpiarEntrada(txtDesc);

            if (txtCodigo.Text != "" && txtDesc.Text != "")
            {
                return true;
            }

            return false;
        }
        public void mostrarMensaje(Label lblmensaje, Color colormensaje, string mensaje)
        {
            lblmensaje.Visible = true;
            lblmensaje.ForeColor = colormensaje;
            lblmensaje.Text = mensaje;
        }

        public bool esModificacion()
        {
            if (gvConsulta.SelectedIndex != -1)
            {
                btnModificar.Enabled = true;
                btnAlta.Enabled = false;
                return true;
            }

            btnModificar.Enabled = false;
            btnAlta.Enabled = true;
            return false;
        }

        #endregion


    }
}