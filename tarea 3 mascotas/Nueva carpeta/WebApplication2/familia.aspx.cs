using FamiliaWebApp.Data;
using FamiliaWebApp.Models;
using System;
using System.Linq;
using System.Web.UI;

namespace FamiliaWebApp.Pages
{
    public partial class MantMascotas : Page
    {
        private readonly MascotaRepositorio repo = new MascotaRepositorio();
        private readonly DuenoRepositorio repoDueno = new DuenoRepositorio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDuenos();
                CargarGrid();
            }
        }

        private void CargarDuenos()
        {
            var duenos = repoDueno.ObtenerTodos().ToList();
            ddlDueno.DataSource = duenos;
            ddlDueno.DataTextField = "Nombre";
            ddlDueno.DataValueField = "DuenoId";
            ddlDueno.DataBind();
            ddlDueno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione", ""));
        }

        private void CargarGrid()
        {
            var mascotas = repo.ObtenerTodas()
                .Select(m => new
                {
                    m.MascotaId,
                    m.Nombre,
                    m.Especie,
                    m.Raza,
                    FechaNacimiento = m.FechaNacimiento.HasValue ? m.FechaNacimiento.Value.ToString("yyyy-MM-dd") : "",
                    m.Peso,
                    m.Alergias,
                    NombreDueno = repoDueno.ObtenerPorId(m.DuenoId)?.Nombre
                })
                .ToList();

            gvMascotas.DataSource = mascotas;
            gvMascotas.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!int.TryParse(ddlDueno.SelectedValue, out int duenoId))
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Seleccione un dueño válido.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreMascota.Text))
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Ingrese el nombre de la mascota.";
                return;
            }

            var mascota = new Mascota
            {
                DuenoId = duenoId,
                Nombre = txtNombreMascota.Text.Trim(),
                Especie = string.IsNullOrWhiteSpace(txtEspecie.Text) ? null : txtEspecie.Text.Trim(),
                Raza = string.IsNullOrWhiteSpace(txtRaza.Text) ? null : txtRaza.Text.Trim(),
                FechaNacimiento = string.IsNullOrWhiteSpace(txtFechaNac.Text) ? (DateTime?)null : DateTime.Parse(txtFechaNac.Text),
                Peso = string.IsNullOrWhiteSpace(txtPeso.Text) ? (decimal?)null : decimal.Parse(txtPeso.Text),
                Alergias = string.IsNullOrWhiteSpace(txtAlergias.Text) ? null : txtAlergias.Text.Trim()
            };

            try
            {
                repo.Insertar(mascota);
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Mascota guardada correctamente.";

                CargarGrid();

                txtNombreMascota.Text = "";
                txtEspecie.Text = "";
                txtRaza.Text = "";
                txtFechaNac.Text = "";
                txtPeso.Text = "";
                txtAlergias.Text = "";
                ddlDueno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error: " + ex.Message;
            }
        }
    }
}
