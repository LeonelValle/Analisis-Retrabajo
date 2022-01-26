using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE.Mantenimiento
{
    public partial class Man_Usuario : System.Web.UI.Page
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
                   // LlenarGrid(Conexion.ConsultaGeneral("USUARIO"));
                    LlenarDdlBD(ddlAreaInt, "CAT_AREAINTERNA", "AreaInt", "Id_AreaInterna");
                    LlenarDdlBD(ddlTipoUsuario, "CAT_TIPOUSUARIO", "TipoUsuario", "Id_TipoUsuario");
                    ddlTipoUsuario.SelectedValue = "-1";
                    ddlAreaInt.SelectedValue = "-1";
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
                Conexion.Alta_Usuario(new Modelos.Usuario
                {
                    Nombre_Empleado = txtNombre.Text,
                    Username = txtusername.Text,
                    Password = txtpassword.Text,
                    FK_AreaInt = Convert.ToInt32(ddlAreaInt.SelectedValue),
                    FK_TipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue),
                    Num_Empleado = txtNumEmpleado.Text
                });

                mostrarMensaje(lblerror, Color.DarkSeaGreen, "Registro exitoso");
                LlenarGrid(null);
                //LlenarGrid(Conexion.ConsultaGeneral("USUARIO"));
                LimpiarInfoUnidad();
                txtBuscarNumEmpleado.Text = "";
            }
            else
            {
                mostrarMensaje(lblerror, Color.Red, "Informacion incorrecta");
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
                Conexion.Modificar_Usuario(new Modelos.Usuario
                {
                    Id_Usuario = Convert.ToInt32(gvConsulta.SelectedRow.Cells[1].Text),
                    Nombre_Empleado = txtNombre.Text,
                    Username = txtusername.Text,
                    Password = txtpassword.Text,
                    FK_AreaInt = Convert.ToInt32(ddlAreaInt.SelectedValue),
                    FK_TipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue),
                    Num_Empleado = txtNumEmpleado.Text
                });

                mostrarMensaje(lblerror, Color.DarkSeaGreen, "Modificacion exitosa");
                LlenarGrid(null);
                //LlenarGrid(Conexion.ConsultaGeneral("USUARIO"));
                gvConsulta.SelectedIndex = -1;
                LimpiarInfoUnidad();
                txtBuscarNumEmpleado.Text = "";

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
            if (txtBuscarNumEmpleado.Text != "")
            {
                LlenarGrid(Conexion.ConsultaGeneral("USUARIO", "Num_Empleado", "'" + txtBuscarNumEmpleado.Text + "'"));
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNumEmpleado.Text = "";

            gvConsulta.SelectedIndex = -1;
            esModificacion();

            LimpiarInfoUnidad();
            LlenarGrid(null);
            //LlenarGrid(Conexion.ConsultaGeneral("USUARIO"));
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

            if (textbox == txtpassword)
            {
                charsToRemove = new string[] { "'" };
            }

            foreach (var c in charsToRemove)
            {
                textbox.Text = textbox.Text.Replace(c, string.Empty);
            }

            textbox.Text = textbox.Text.Replace("\"", string.Empty);
        }

        protected void MostrarInfoUnidad()
        {
            LimpiarInfoUnidad();
            txtNombre.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[2].Text);
            txtusername.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[3].Text);
            txtpassword.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[4].Text);
            ddlAreaInt.SelectedValue = (ddlAreaInt.Items.FindByText(gvConsulta.SelectedRow.Cells[5].Text)).Value;
            ddlTipoUsuario.SelectedValue = (ddlTipoUsuario.Items.FindByText(gvConsulta.SelectedRow.Cells[6].Text)).Value;
            txtNumEmpleado.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[7].Text);
        }

        private void LimpiarInfoUnidad()
        {
            txtNombre.Text = "";
            txtusername.Text = "";
            txtpassword.Text = "";
            ddlAreaInt.SelectedValue = "-1";
            ddlTipoUsuario.SelectedValue = "-1";
            txtNumEmpleado.Text = "";
        }

        protected bool camposValidos()
        {
            limpiarEntrada(txtNombre);
            limpiarEntrada(txtusername);
            limpiarEntrada(txtpassword);
            limpiarEntrada(txtNumEmpleado);

            if (txtNombre.Text != "" && txtusername.Text != "" && txtNumEmpleado.Text != "" && txtpassword.Text != ""
                    && ddlAreaInt.SelectedValue != "-1" && ddlTipoUsuario.SelectedValue != "-1")
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

        protected void LlenarDdlBD(DropDownList ddl, string tabla, string nombre, string id)
        {
            ddl.DataSource = Conexion.ConsultaGeneral(tabla);
            ddl.DataTextField = nombre;
            ddl.DataValueField = id;
            ddl.DataBind();

            ListItem item = new ListItem { Text = "No seleccionada", Value = "-1" };
            ddl.Items.Add(item);
        }

        #endregion

    }
}
