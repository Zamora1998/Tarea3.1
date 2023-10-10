<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Tweb.Views.Categorias" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Categorias</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../CSS/style.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script> 
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script> 
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script> 
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script> 


    <script src="../Scripts/AgregarCategoria.js"></script>
    <script src="../Scripts/CargarCategorias.js"></script>

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
</body>
</html>
