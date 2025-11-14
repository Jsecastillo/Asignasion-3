<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantMascotas.aspx.cs" Inherits="FamiliaWebApp.Pages.MantMascotas" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Mantenimiento de Mascotas</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container py-4">

        <h2 class="mb-4 text-center text-primary fw-bold">🐶 Mantenimiento de Mascotas</h2>

        <!-- FORMULARIO -->
        <div class="card p-4 shadow-sm mb-4">
            <h5 class="text-secondary mb-3">Datos de la Mascota</h5>

            <div class="row g-3">
                <!-- Dueño -->
                <div class="col-md-4">
                    <label class="form-label fw-bold">Dueño</label>
                    <asp:DropDownList ID="ddlDueno" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <!-- Nombre Mascota -->
                <div class="col-md-4">
                    <label class="form-label fw-bold">Nombre</label>
                    <asp:TextBox ID="txtNombreMascota" runat="server" CssClass="form-control" Placeholder="Ej: Rocky"></asp:TextBox>
                </div>

                <!-- Especie -->
                <div class="col-md-4">
                    <label class="form-label fw-bold">Especie</label>
                    <asp:TextBox ID="txtEspecie" runat="server" CssClass="form-control" Placeholder="Ej: Perro"></asp:TextBox>
                </div>

                <!-- Raza -->
                <div class="col-md-4">
                    <label class="form-label fw-bold">Raza</label>
                    <asp:TextBox ID="txtRaza" runat="server" CssClass="form-control" Placeholder="Ej: Labrador"></asp:TextBox>
                </div>

                <!-- Fecha Nacimiento -->
                <div class="col-md-4">
                    <label class="form-label fw-bold">Fecha de Nacimiento</label>
                    <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <!-- Peso -->
                <div class="col-md-4">
                    <label class="form-label fw-bold">Peso (kg)</label>
                    <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <!-- Alergias -->
                <div class="col-md-12">
                    <label class="form-label fw-bold">Alergias</label>
                    <asp:TextBox ID="txtAlergias" runat="server" CssClass="form-control" Placeholder="Ej: Ninguna"></asp:TextBox>
                </div>

                <!-- Botón Guardar -->
                <div class="col-md-12 d-grid mt-3">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Mascota" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>
            </div>

            <div class="mt-3">
                <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold"></asp:Label>
            </div>
        </div>

        <!-- GRID -->
        <div class="card p-4 shadow-sm">
            <h5 class="text-secondary mb-3">Listado de Mascotas</h5>

            <asp:GridView ID="gvMascotas" runat="server" CssClass="table table-bordered table-striped text-center"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="MascotaId" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Especie" HeaderText="Especie" />
                    <asp:BoundField DataField="Raza" HeaderText="Raza" />
                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." />
                    <asp:BoundField DataField="Peso" HeaderText="Peso (kg)" />
                    <asp:BoundField DataField="Alergias" HeaderText="Alergias" />
                    <asp:BoundField DataField="NombreDueno" HeaderText="Dueño" />
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
