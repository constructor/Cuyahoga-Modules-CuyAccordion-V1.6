<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCuyAccordion.aspx.cs" Inherits="Cuyahoga.Modules.CuyAccordion.Web.Admin.AdminCuyAccordion" MaintainScrollPositionOnPostback="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>CuyAccordion Admin</title>
    </head>
    <body>
        <form id="form1" runat="server">
        <div class="AdminPanel">
        
            <h2><asp:Label ID="lblModuleSetting" runat="server" Text=" "></asp:Label></h2>
            
            <asp:Label ID="lblMessages" runat="server" Text=" "></asp:Label>
            
            <div id="accordioninfo" class="Panel60">
                <fieldset>
                    <legend>Accordion</legend>
                    <asp:Label ID="lblName" AssociatedControlID="tbName" Text="Name" runat="server"></asp:Label>
                    <asp:TextBox ID="tbName" runat="Server"></asp:TextBox><br/>
                    <asp:Label ID="lblDescription" AssociatedControlID="tbDescription" Text="Description" runat="server"></asp:Label>
                    <asp:TextBox ID="tbDescription" runat="Server"></asp:TextBox><br/>
                    <asp:Label ID="lblActive" AssociatedControlID="cbActive" Text="Is Active" runat="server"/>
                    <asp:CheckBox ID="cbActive" runat="server" /><br/>
                    <asp:Button ID="btnSelect" runat="server" CssClass="SubmitButton" Text="Add Accordion" onclick="btnSubmit_Click" />
                </fieldset>
                <hr />
                <asp:GridView ID="gvAccordion" runat="server" 
                        DataKeyNames="AccordionId" 
                        AutoGenerateColumns="False" 
                        onselectedindexchanged="gvAccordion_SelectedIndexChanged" 
                        onrowediting="gvAccordion_RowEditing"
                        onrowcancelingedit="gvAccordion_RowCancelingEdit" 
                        onrowupdating="gvAccordion_RowUpdating" 
                        onrowdeleting="gvAccordion_RowDeleting" >
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Accordion Name" />
                        
                        <asp:TemplateField HeaderText="Date Created">
                            <ItemTemplate>
                                <asp:Label ID="lblCreated" runat="server" Text='<%# Bind("Created") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCreatedEdit" runat="server" Text='<%# Bind("Created") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:CheckBoxField DataField="Active" HeaderText="Is Active" />

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="False" CommandName="Select" Text="Make Current"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Accordion item?');" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
            </div>
                
            <div id="accordioniteminfo" class="Panel60">
            
                <fieldset>
                    <legend>Accordion Item</legend>
                    <asp:Label ID="lblTitle" AssociatedControlID="tbTitle" Text="Title" runat="server"></asp:Label>
                    <asp:TextBox ID="tbTitle" runat="Server"></asp:TextBox><br/>
                    <hr />
                    <fckeditorv2:fckeditor id="fckEditor" runat="server" height="220px" width="100%"></fckeditorv2:fckeditor>
                    <hr />
                    <asp:Label ID="lblItemActive" AssociatedControlID="cbItemActive" Text="Is Active" runat="server"/>
                    <asp:CheckBox ID="cbItemActive" runat="server" /><br/>
                    <asp:Button ID="btnSelectItem" runat="server" CssClass="SubmitButton" Text="Add Accordion Item" onclick="btnSubmitItem_Click" />
                </fieldset>
                <hr />
                <asp:GridView ID="gvAccordionItems" runat="server" DataKeyNames="AccordionItemId" AutoGenerateColumns="False" onrowcancelingedit="gvAccordionItems_RowCancelingEdit" onrowdeleting="gvAccordionItems_RowDeleting" onrowediting="gvAccordionItems_RowEditing" onrowupdating="gvAccordionItems_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HTML">
                            <EditItemTemplate>
                                <a href="#" class="jqModal">Edit HTML</a>
                                <div class="jqmWindow" id="gvfck">
                                    <a href="#" class="jqmClose">Close</a>
                                    <hr/>
                                    <fckeditorv2:fckeditor id="fckEditorGV" runat="server" BasePath="~/Support/FCKeditor/" height="440px" Value='<%# Bind("HTML") %>'></fckeditorv2:fckeditor>
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbActive" runat="server" Checked='<%# Bind("Active") %>' Enabled="false" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cbActiveEdit" runat="server" Checked='<%# Bind("Active") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Accordion item?');" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
            </div>
            
            </div>
        </form>
    </body>
</html>

