using System;
using System.Drawing;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace EstadiaMWE.Mantenimiento
{
    public partial class Man_NumParte : System.Web.UI.Page
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
                //LlenarGrid(Conexion.ConsultaGeneral("CAT_NUMPARTE"));
                if (!IsPostBack)
                {
                    LlenarDdlBD(ddlCliente, "CAT_CLIENTE", "Cliente", "Id_Cliente");
                    ddlCliente.SelectedValue = "-1";
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
            }
        }

        #endregion

        #region botones
        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (camposValidos())
            {
                if (!Conexion.Existe("select count(*) from CAT_NUMPARTE where Num_Parte = '" + txtPartNumber.Text.Trim() + "'"))
                {
                    Conexion.Alta_NumParte(new Modelos.NumParte { Num_Parte = txtPartNumber.Text, Precio_Unitario = float.Parse(txtPrecio.Text), FK_Cliente = Convert.ToInt32(ddlCliente.SelectedValue) });
                    mostrarMensaje(lblerror, Color.DarkSeaGreen, "Registro exitoso");
                    LlenarGrid(null);
                    LimpiarInfoUnidad();
                    txtBuscarPartNumber.Text = "";
                }
                else
                {
                    mostrarMensaje(lblerror, Color.Red, "Ya existe el registro");
                }
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
                Conexion.Modificar_NumParte(new Modelos.NumParte
                {
                    Id_NumParte = Convert.ToInt32(gvConsulta.SelectedRow.Cells[1].Text),
                    Num_Parte = txtPartNumber.Text,
                    Precio_Unitario = float.Parse(txtPrecio.Text),
                    FK_Cliente = Convert.ToInt32(ddlCliente.SelectedValue)
                });

                mostrarMensaje(lblerror, Color.DarkSeaGreen, "Modificacion exitosa");
                LlenarGrid(null);
                gvConsulta.SelectedIndex = -1;
                LimpiarInfoUnidad();
                txtBuscarPartNumber.Text = "";

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
            if (txtBuscarPartNumber.Text != "")
            {
                LlenarGrid(Conexion.ConsultaGeneral("CAT_NUMPARTE", "Num_Parte", "'" + txtBuscarPartNumber.Text + "'"));
            }
            //gvConsulta.Visible = true;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarPartNumber.Text = "";

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
            var charsToRemove = new string[] { "@", ",", ";", "'", "\n", "%" };

            foreach (var c in charsToRemove)
            {
                textbox.Text = textbox.Text.Replace(c, string.Empty);
            }

            textbox.Text = textbox.Text.Replace("\"", string.Empty);
        }

        public void MostrarInfoUnidad()
        {
            LimpiarInfoUnidad();
            txtPartNumber.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[2].Text);
            txtPrecio.Text = HttpUtility.HtmlDecode(gvConsulta.SelectedRow.Cells[3].Text);
            ddlCliente.SelectedValue = (ddlCliente.Items.FindByText(gvConsulta.SelectedRow.Cells[4].Text)).Value;
        }

        private void LimpiarInfoUnidad()
        {
            txtPartNumber.Text = "";
            txtPrecio.Text = "";
            ddlCliente.SelectedValue = "-1";
        }

        protected bool camposValidos()
        {
            limpiarEntrada(txtPartNumber);

            if (txtPrecio.Text != "" && txtPartNumber.Text != "" && ddlCliente.SelectedValue != "-1")
            {
                if (float.TryParse(txtPrecio.Text, out float result))
                {
                    return true;
                }
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

        protected void LlenarDdlBD(AjaxControlToolkit.ComboBox ddl, string tabla, string nombre, string id)
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
