function mostrarModal() {
    $('#<%= nuevoPlatilloModal %>').modal('show');
    llenarCategorias();
    llenarEstados();
}

async function llenarECategorias() {
    try {
        var ddlCategorias = document.getElementById('<%= ddleditarcategoria.ClientID %>');
        ddlCategorias.innerHTML = '';

        const categoriasResponse = await fetch('http://localhost:50912/api/Categorias/ObtenerCategorias');
        const categoriasData = await categoriasResponse.json();
        categoriasData.forEach(categoria => {
            var option = document.createElement('option');
            option.value = categoria.CategoriaID;
            option.text = categoria.Nombre;
            ddlCategorias.appendChild(option);
        });
    } catch (error) {
        mostrarError(errorMensaje, "Error al cargar los datos de categorías y estados", false);
    }
}
async function llenarEEstados() {
    try {
        var ddlEstado = document.getElementById('<%= ddleditarestado.ClientID %>');
        ddlEstado.innerHTML = '';

        const response = await fetch('http://localhost:50912/api/Estados/ObtenerEstados');
        const data = await response.json();

        data.forEach(estado => {
            var option = document.createElement('option');
            option.value = estado.EstadoID;
            option.text = estado.Descripcion;
            ddlEstado.appendChild(option);
        });
    } catch (error) {
        mostrarError(errorMensaje, "Error al cargar los datos de estados", false);
    }
}

async function llenarCategorias() {
    try {
        var ddlCategorias = document.getElementById('<%= ddlcategoria.ClientID %>');
        ddlCategorias.innerHTML = '';

        const categoriasResponse = await fetch('http://localhost:50912/api/Categorias/ObtenerCategorias');
        const categoriasData = await categoriasResponse.json();
        categoriasData.forEach(categoria => {
            var option = document.createElement('option');
            option.value = categoria.CategoriaID;
            option.text = categoria.Nombre;
            ddlCategorias.appendChild(option);
        });
    } catch (error) {
        mostrarError(errorMensaje, "Error al cargar los datos de categorías y estados", false);
    }
}
async function llenarEstados() {
    try {
        var ddlEstado = document.getElementById('<%= ddlestado.ClientID %>');
        ddlEstado.innerHTML = '';

        const response = await fetch('http://localhost:50912/api/Estados/ObtenerEstados');
        const data = await response.json();

        data.forEach(estado => {
            var option = document.createElement('option');
            option.value = estado.EstadoID;
            option.text = estado.Descripcion;
            ddlEstado.appendChild(option);
        });
    } catch (error) {
        mostrarError(errorMensaje, "Error al cargar los datos de estados", false);
    }
}

function validarYGuardarPlatillo() {
    if (validarPlatillo()) {
        const nombre = document.getElementById('<%= nombre.ClientID %>').value;
        const costo = parseFloat(document.getElementById('<%= costo.ClientID %>').value);
        const categoria = parseInt(document.getElementById('<%= ddlcategoria.ClientID %>').value);
        const estado = parseInt(document.getElementById('<%= ddlestado.ClientID %>').value);

        fetch('http://localhost:50912/api/Platillos/RegistrarPlatillo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Nombre: nombre,
                Costo: costo,
                CategoriaID: categoria,
                IDESTADO: estado
            })
        })
            .then(response => {
                if (response.status === 201) {
                    mostrarError(errorMensaje, "Platillo creado con éxito.", true);
                } else if (response.status === 409) {
                    mostrarError(errorMensaje, "El platillo ya existe.", false);
                } else {
                    mostrarError(errorMensaje, "El platillo no se envio correctamente.", false);
                }
            })
            .catch(error => {
                mostrarError(errorMensaje, error.message, true);
            })
    }
}

function validarPlatillo() {
    var nombre = document.getElementById('<%= nombre.ClientID %>').value;
    var costo = document.getElementById('<%= costo.ClientID %>').value;
    var categoria = document.getElementById('<%= ddlcategoria.ClientID %>').value;
    var estado = document.getElementById('<%= ddlestado.ClientID %>').value;
    var errorMensaje = document.getElementById('<%= errorMensaje.ClientID %>');

    // Validar que los campos no estén vacíos
    if (nombre.trim() === "" || costo.trim() === "" || categoria.trim() === "" || estado.trim() === "") {
        mostrarError(errorMensaje, "Todos los campos son obligatorios.", false);
        return false;
    }

    // Validar que el nombre solo contenga letras y tenga como máximo 100 caracteres
    if (!/^[A-Za-z]+$/.test(nombre) || nombre.length > 100) {
        mostrarError(errorMensaje, "El campo Nombre debe contener solo letras", false);
        return false;
    }

    // Validar que el costo sea numérico con hasta dos decimales
    if (!/^\d+(\.\d{1,2})?$/.test(costo)) {
        mostrarError(errorMensaje, "El campo Costo debe ser un número válido con hasta dos decimales.", false);
        return false;
    }

    // Validar que la categoría esté seleccionada
    if (estado === "0") {
        mostrarError(errorMensaje, "Por favor, seleccione un estado.", false);
        return false;
    }

    // Validar que la categoría esté seleccionada
    if (categoria === "0") {
        mostrarError(errorMensaje, "Por favor, seleccione una categoría.", false);
        return false;
    }
    // Si todas las validaciones pasan, retorna true
    return true;
}

function mostrarError(elemento, mensaje, recargarPagina) {
    elemento.textContent = mensaje;
    setTimeout(function () {
        elemento.textContent = "";
        if (recargarPagina) {
            location.reload();
        }
    }, 4000);
}

async function cargarPlatillosDesdeAPI() {
    try {
        const response = await fetch('http://localhost:50912/api/Platillos/ListarPlatillos');
        if (response.ok) {
            const data = await response.json();
            const tabla = document.getElementById('tablaPlatillos').getElementsByTagName('tbody')[0];
            data.forEach(platillo => {
                const row = tabla.insertRow();
                const checkboxCell = row.insertCell();
                const idCell = row.insertCell();
                const nombreCell = row.insertCell();
                const costoCell = row.insertCell();
                const categoriaCell = row.insertCell();
                const estadoCell = row.insertCell();
                const cellAcciones = row.insertCell();
                cellAcciones.classList.add('text-center');

                const checkbox = document.createElement('input');
                checkbox.type = 'checkbox';
                checkboxCell.appendChild(checkbox);

                idCell.textContent = platillo.PlatilloID;
                nombreCell.textContent = platillo.Nombre;
                costoCell.textContent = platillo.Costo.toFixed(2);
                categoriaCell.textContent = platillo.CategoriaNombre;
                estadoCell.textContent = platillo.EstadoDescripcion;

                const btnEliminar = document.createElement('button');
                btnEliminar.textContent = 'Eliminar';
                btnEliminar.classList.add('btn', 'btn-danger');
                btnEliminar.type = 'button';
                btnEliminar.onclick = (event) => {
                    event.stopPropagation();

                    // Verificar si se ha seleccionado alguna casilla de verificación
                    const row = event.target.closest('tr');
                    const checkbox = row.querySelector('input[type="checkbox"]');

                    if (checkbox && checkbox.checked) {
                        // Mostrar el modal de confirmación si se ha seleccionado una casilla
                        const nombre = platillo.Nombre;
                        $('#confirmacionModal').modal('show');

                        // Configurar el manejador del botón "Sí, Eliminar" en el modal de confirmación
                        document.getElementById('botonEliminar').onclick = async () => {
                            // Realizar la solicitud para eliminar la categoría
                            const response = await fetch(`http://localhost:50912/api/Platillos/EliminarPlatillo/${nombre}`, {
                                method: 'DELETE'
                            });

                            if (response.status === 200) {
                                // Si la solicitud es exitosa, cierra el modal de confirmación suavemente
                                $('#confirmacionModal').modal('hide');
                                $('#confirmacionModal').on('hidden.bs.modal', function () {
                                    // Puedes recargar la página o realizar otras acciones después de eliminar
                                    window.location.reload(); // Recargar la página
                                });
                            } else {
                                mostrarError(errorMensaje, "El platillo no se envio pudo eliminar correctamente.", false);
                            }
                        };
                    } else {
                        $('#modalAdvertencia').modal('show');
                    }
                    return false;
                };

                cellAcciones.appendChild(btnEliminar);

                const btnModificar = document.createElement('button');
                btnModificar.textContent = 'Modificar';
                btnModificar.classList.add('btn', 'btn-warning', 'ml-2');
                btnModificar.type = 'button';
                btnModificar.onclick = (event) => {
                    event.stopPropagation();
                    const selectedRow = obtenerFilaSeleccionada();

                    if (selectedRow) {
                        // Obtener datos de la fila seleccionada
                        const idPlatillo = selectedRow.cells[1].textContent;
                        const nombreActual = selectedRow.cells[2].textContent;
                        const nuevoNombre = document.getElementById('txtmodalNombre').value;
                        const nuevoCosto = parseFloat(document.getElementById('txtmodalCosto').value);

                        // Llenar modal con datos de la fila seleccionada
                        document.getElementById('modalPlatilloID').textContent = idPlatillo;
                        document.getElementById('txtmodalNombre2').textContent = nombreActual;
                        document.getElementById('txtmodalNombre').value = nuevoNombre;
                        document.getElementById('txtmodalCosto').value = nuevoCosto;

                        // Mostrar modal de modificación
                        $('#modalModificar').modal('show');
                        llenarECategorias();
                        llenarEEstados();

                        document.getElementById('btnAceptarModificar').onclick = async () => {
                            const nuevoNombre = document.getElementById('txtmodalNombre').value;
                            const nuevoCosto = parseInt(document.getElementById('txtmodalCosto').value);
                            const categoria = parseInt(document.getElementById('<%= ddleditarcategoria.ClientID %>').value);
                            const estado = parseInt(document.getElementById('<%= ddleditarestado.ClientID %>').value);

                            const requestBody = {
                                Nombre: nuevoNombre,
                                Costo: nuevoCosto,
                                CategoriaID: categoria,
                                IDESTADO: estado
                            };

                            const response = await fetch(`http://localhost:50912/api/Platillos/EditarPlatillo/?nombreActual=${nombreActual}`, {
                                method: 'PUT',
                                headers: {
                                    'Content-Type': 'application/json'
                                },
                                body: JSON.stringify(requestBody)
                            });

                            if (response.status === 200) {
                                $('#modalModificar').modal('hide');
                                $('#modalModificar').on('hidden.bs.modal', function () {
                                    window.location.reload();
                                });
                            } else if (response.status === 409) {
                                document.getElementById('errorEditar').textContent = 'El nombre ya se encuentra en uso.';
                            } else {
                                mostrarError(errorMensaje, "Error al editar el platillo.", true);
                            }
                        };
                    } else {
                        $('#modalAdvertencia').modal('show');
                    }
                    return false;
                };
                cellAcciones.appendChild(btnModificar);
            });
        } else {
            mostrarError(errorMensaje, "Error al cargar datos desde la api.", false);
        }
    } catch (error) {
        mostrarError(errorMensaje, "Error inesperado.", true);
    }
}

function obtenerFilaSeleccionada() {
    const checkboxes = document.querySelectorAll('#tablaPlatillos tbody input[type="checkbox"]');
    for (const checkbox of checkboxes) {
        if (checkbox.checked) {
            const row = checkbox.closest('tr');
            return row;
        }
    }
    return null;
}
cargarPlatillosDesdeAPI();