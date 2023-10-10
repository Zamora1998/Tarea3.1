$(document).ready(function () {
    // Agregar un controlador de eventos a los botones de eliminar
    $(".btnEliminar").click(function () {
        var categoriaID = $(this).data("id"); // Obtener el ID de la categoría
        eliminarCategoria(categoriaID);
    });
});

function eliminarCategoria(categoriaID) {
    // Realizar una solicitud HTTP POST a la API para eliminar la categoría
    $.ajax({
        url: "http://localhost:50912/api/Categorias/EliminarCategoria/" + categoriaID,
        type: "POST",
        success: function (data) {
            if (data === 200) {
                // Éxito: recargar la página actual
                location.reload();
            } else {
                // Fallo: Mostrar un mensaje de error
                alert("El elemento no pudo ser eliminado.");
            }
        },
        error: function () {
            // Error de solicitud: Mostrar un mensaje de error
            alert("Hubo un problema al comunicarse con la API.");
        }
    });
}
