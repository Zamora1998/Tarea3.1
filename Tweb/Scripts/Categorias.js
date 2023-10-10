function mostrarMenuDesplegable(categoriaId) {
    // Oculta todos los menús desplegables de acciones
    var dropdowns = document.getElementsByClassName("dropdown-content");
    for (var i = 0; i < dropdowns.length; i++) {
        dropdowns[i].style.display = "none";
    }

    // Muestra el menú desplegable de acciones de la categoría seleccionada
    var dropdown = document.getElementById("dropdown-" + categoriaId);
    dropdown.style.display = "block";
}
