<%@ Page Title="Home Page" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CacheAccessWebSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Cache WebService Client
    </h2>
    <p>
        Input name:
        <asp:TextBox ID="txtName" runat="server" />
    </p>
    <p>
        <asp:Button ID="btnCall" Text="Call Function" OnClick="BtnCall_Click" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnClear" Text="Clear Cache" OnClick="BtnClear_Click" runat="server" />
    </p>
    <p>
        <asp:Label ID="lbResult" runat="server" />
    </p>
</asp:Content>
