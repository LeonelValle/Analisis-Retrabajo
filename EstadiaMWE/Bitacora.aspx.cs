using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class Bitacora : System.Web.UI.Page
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
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {
                lblUsuario.Text = Session["Nombre_Empleado"].ToString();
                LlenarGridBitacora(Conexion.consultaBitacora());
                if (!IsPostBack)
                {

                    LlenarDdlBD(ddlStatus, "CAT_STATUS", "Nombre_status", "Id_Status");
                    LlenarDdlBD(ddlPartNumber, "CAT_NUMPARTE", "Num_Parte", "Id_NumParte");
                }
            }
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void LlenarGridBitacora(DataSet ds)
        {
            gvBitacora.DataSource = ds;

            try
            {
                gvBitacora.DataBind();
            }
            catch (Exception)
            {
            }

        }

        protected void LlenarDdlBD(AjaxControlToolkit.ComboBox ddl, string tabla, string nombre, string id)
        {

            ddl.DataSource = Conexion.ConsultaGeneral(tabla);
            ddl.DataTextField = nombre;
            ddl.DataValueField = id;
            ddl.DataBind();

            ListItem item = new ListItem { Text = "Select All", Value = "-1" };
            ddl.Items.Add(item);

            ddl.SelectedValue = (ddl.Items.FindByText("Select All")).Value;

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarEntrada(txtEmpleado);
            limpiarEntrada(txtSerialNum);
            limpiarEntrada(txtWo);

            RefrescarGrid();

        }

        public void RefrescarGrid()
        {
            if (txtEmpleado.Text == "" && ddlStatus.SelectedValue == "-1" && ddlTurno.SelectedValue == "-1"
                && ddlPartNumber.SelectedValue == "-1" && txtWo.Text == "" && txtSerialNum.Text == "" && txtFecha.Text == "")
            {

                //CALENDARIO
                LlenarGridBitacora(Conexion.consultaBitacora());
            }
            else
            {
                LlenarGridBitacora(Conexion.consultaBitacora(txtEmpleado.Text, ddlStatus.SelectedItem.Text, ddlTurno.SelectedItem.Text, txtWo.Text,
                    Convert.ToInt32(ddlPartNumber.SelectedItem.Value), txtSerialNum.Text, pageControl.fecha_entrada));
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtEmpleado.Text = "";
            txtFecha.Text = "";
            txtSerialNum.Text = "";
            txtWo.Text = "";
            pageControl.fecha_entrada = "";
            pageControl.fecha_salida = "";
            ddlPartNumber.SelectedValue = "-1";
            ddlStatus.SelectedValue = "-1";
            ddlTurno.SelectedValue = "-1";
            LlenarGridBitacora(Conexion.consultaBitacora());
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        protected void gvReportes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            pageControl.fecha_entrada = txtFecha.Text;
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

        private void ExportGridToExcel()
        {
            Response.Clear();

            Response.AddHeader("content-disposition", "attachment; filename = Bitacora.xls");

            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite =
            new HtmlTextWriter(stringWrite);

            RefrescarGrid();

            gvBitacora.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
    }

}