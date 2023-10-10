<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Tweb.Views.Categorias" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Categorias</title>
    <link rel="stylesheet" type="text/css" href="../CSS/style.css" />
    <script src="../Scripts/AgregarCategoria.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Categorías</h2>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btnAgregar" data-toggle="modal" data-target="#miModal" OnClientClick="return false;" />

            <div id="miModal" class="modal fade" runat="server">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Categorias</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <!-- Contenido del modal aquí -->
                            <p>Agregar Categoria</p>
                            <asp:TextBox ID="txtInputregistrar" runat="server" CssClass="form-control" placeholder="Ingrese el nombre de Categoria" />

                            <span id="errorMensaje" style="color: red;"></span>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" id="btnAceptar" onclick="validarInput();">Aceptar</button>
                            <button type="button" class="btn btn-secondary" id="btnCerrarModal" data-dismiss="modal">Cerrar</button>
                            <!-- Otros botones del modal si es necesario -->
                        </div>
                    </div>
                </div>
            </div>

            <asp:Table ID="tablaCategorias" runat="server" BorderWidth="1">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Nombre</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Acciones</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>

        </div>
    </form>



</body>
</html>
