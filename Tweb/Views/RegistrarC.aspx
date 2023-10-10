<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarC.aspx.cs" Inherits="Tweb.Views.RegistrarC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="txtCategoria">Nombre de Categoría:</label>
            <asp:TextBox ID="txtCategoria" runat="server" />
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
        </div>
        <asp:Label ID="lblMensaje" runat="server" Visible="true" ForeColor="Green" Text=""></asp:Label>
    </form>
</body>
</html>
