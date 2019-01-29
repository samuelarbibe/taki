<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <tr>
        <td>
    <Table width="30%" align="center">
        <tr><td colspan="2">login</td></tr>
        <tr><td>username </td><td>
            <asp:TextBox ID="Username" runat="server"></asp:TextBox></td></tr>
        <tr><td>password </td><td>
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox></td></tr>
        <tr><td colspan="2">
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" /></td></tr>
        <tr><td colspan="2">
            <asp:Label ID="noUserError" runat="server" Text="Label"></asp:Label></td></tr>
    </Table>

        </td>
    </tr>

</asp:Content>

