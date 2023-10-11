<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Tweb.Views.Categorias" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Categorias</title>
    <link rel="stylesheet" type="text/css" href="../CSS/style.css" />
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script> 
    <script src="../Scripts/AgregarCategoria.js"></script>
    <script src="../Scripts/CargarCategorias.js"></script>
    <script src="../Scripts/CargarCategoriasnuevas.js"></script>

</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <a class="navbar-brand mr-auto" href="#">Administrador de Categorias</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="Platillos.aspx">Platillos</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Categorias.aspx">Categorias</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Listado.aspx">Listado Platillos</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div>
            <h2> </h2>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary" data-toggle="modal" data-target="#miModal" OnClientClick="return false;" />

            <div id="miModal" class="modal fade" runat="server">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Categorias</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <p>Agregar Categoria</p>
                            <asp:TextBox ID="txtInputregistrar" runat="server" CssClass="form-control" placeholder="Ingrese el nombre de Categoria" />

                            <span id="errorMensaje" style="color: red;"></span>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" id="btnAceptar" onclick="validarInput();">Aceptar</button>
                            <button type="button" class="btn btn-secondary" id="btnCerrarModal" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalAdvertencia" class="modal fade" runat="server">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Advertencia</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <p>Debe seleccionar un registro para poder eliminar.</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">Aceptar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalConfirmacion" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="modalConfirmacionLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalConfirmacionLabel">Confirmar Eliminación</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        ¿Estás seguro de que deseas eliminar la categoría con ID: <span id="categoriaId"></span>?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        <button type="button" id="btnConfirmarEliminar" class="btn btn-danger">Sí, Eliminar</button>
                    </div>
                </div>
            </div>
        </div>

            <asp:Table ID="tablaCategorias" runat="server" BorderWidth="1" CssClass="table table-bordered table-striped">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell CssClass="text-center">Check</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="thID" CssClass="text-center">ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="thNombre" CssClass="text-center">Nombre</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="thAcciones" CssClass="text-center">Acciones</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </form>
<script>
    // Función para cargar datos desde la API y actualizar la tabla
    async function cargarDatosDesdeAPI() {
        try {
            const response = await fetch('http://localhost:50912/api/Categorias/ObtenerCategorias');
            if (response.ok) {
                const data = await response.json();
                const tabla = document.getElementById('tablaCategorias');
                const tbody = tabla.createTBody();

                // Ordenar los datos por Nombre alfabéticamente
                data.sort((a, b) => a.Nombre.localeCompare(b.Nombre));

                data.forEach(categoria => {
                    const row = tbody.insertRow();

                    // Agregar una casilla de verificación
                    const cellCheck = row.insertCell();
                    cellCheck.classList.add('text-center');
                    const checkbox = document.createElement('input');
                    checkbox.type = 'checkbox';
                    cellCheck.appendChild(checkbox);

                    // Agregar la ID
                    const cellId = row.insertCell();
                    cellId.classList.add('text-center');
                    cellId.textContent = categoria.CategoriaID;

                    // Agregar el Nombre
                    const cellNombre = row.insertCell();
                    cellNombre.classList.add('text-center');
                    cellNombre.textContent = categoria.Nombre;

                    // Agregar el botón "Acciones"
                    const cellAcciones = row.insertCell();
                    cellAcciones.classList.add('text-center');
                    const btnAcciones = document.createElement('button');
                    btnAcciones.textContent = 'Acciones';
                    btnAcciones.classList.add('btn', 'btn-primary');
                    btnAcciones.type = 'button'; // Evitar recarga de la página

                    // Crear un div contenedor para los botones de "Eliminar" y "Modificar"
                    const divBotones = document.createElement('div');
                    divBotones.style.display = 'none'; // Cambiado a 'none' para que estén ocultos inicialmente
                    divBotones.classList.add('dropdown'); // Agregar la clase para el menú desplegable

                    // Agregar botón "Acciones" como el botón principal del menú desplegable
                    const btnAccionesDropdown = document.createElement('button');
                    btnAccionesDropdown.textContent = 'Acciones';
                    btnAccionesDropdown.classList.add('btn', 'btn-primary', 'dropdown-toggle');
                    btnAccionesDropdown.type = 'button';
                    btnAccionesDropdown.setAttribute('data-toggle', 'dropdown'); // Habilitar el menú desplegable

                    // Crear un div para el menú desplegable
                    const divDropdown = document.createElement('div');
                    divDropdown.classList.add('dropdown-menu');

                    // Agregar botón de "Eliminar" al menú desplegable
                    const btnEliminar = document.createElement('button');
                    btnEliminar.textContent = 'Eliminar';
                    btnEliminar.classList.add('btn', 'btn-danger');
                    btnEliminar.type = 'button'; // Evitar recarga de la página
                    btnEliminar.onclick = (event) => {
                        // Lógica para eliminar
                        event.stopPropagation(); // Detener la propagación del evento
                        const id = categoria.CategoriaID;
                        // Agrega aquí tu lógica para eliminar el registro con la ID 'id'
                        return false; // Evitar recarga de la página
                    };

                    // Agregar botón de "Modificar" al menú desplegable
                    const btnModificar = document.createElement('button');
                    btnModificar.textContent = 'Modificar';
                    btnModificar.classList.add('btn', 'btn-warning');
                    btnModificar.type = 'button'; // Evitar recarga de la página
                    btnModificar.onclick = (event) => {
                        // Lógica para modificar
                        event.stopPropagation(); // Detener la propagación del evento
                        const id = categoria.CategoriaID;
                        // Agrega aquí tu lógica para modificar el registro con la ID 'id'
                        return false; // Evitar recarga de la página
                    };

                    // Agregar botones al menú desplegable
                    divDropdown.appendChild(btnEliminar);
                    divDropdown.appendChild(btnModificar);

                    // Agregar el botón "Acciones" al div de botones
                    divBotones.appendChild(btnAccionesDropdown);
                    divBotones.appendChild(divDropdown);

                    // Agregar el div de botones a la celda de Acciones
                    cellAcciones.appendChild(divBotones);

                    // Evento click del botón "Acciones" para mostrar/ocultar el menú desplegable
                    btnAccionesDropdown.onclick = (event) => {
                        event.stopPropagation(); // Detener la propagación del evento
                        if (divDropdown.style.display === 'none') {
                            divDropdown.style.display = 'block';
                        } else {
                            divDropdown.style.display = 'none';
                        }
                        return false; // Evitar recarga de la página
                    };
                });
            } else {
                console.error('Error al cargar datos desde la API');
            }
        } catch (error) {
            console.error('Error inesperado: ' + error.message);
        }
    }

    // Llamar a la función para cargar datos al cargar la página
    cargarDatosDesdeAPI();

</script>


</body>
</html>
