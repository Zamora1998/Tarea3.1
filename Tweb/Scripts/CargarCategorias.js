// menuDesplegable.js

// Función para mostrar u ocultar el menú desplegable
function mostrarMenuDesplegable(categoriaID) {
    var dropdown = document.getElementById("dropdown_" + categoriaID);
    dropdown.classList.toggle("show");
}

// Cierra todos los menús desplegables al hacer clic en cualquier lugar de la página
window.onclick = function (event) {
    if (!event.target.matches('.acciones-btn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

