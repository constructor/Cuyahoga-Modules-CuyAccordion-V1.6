using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Cuyahoga.Web.UI;
using Cuyahoga.Web.Util;
using Cuyahoga.Core.Service.SiteStructure;

using Cuyahoga.Core.Domain;

using Cuyahoga.Modules.CuyAccordion;
using Cuyahoga.Modules.CuyAccordion.Domain;


namespace Cuyahoga.Modules.CuyAccordion.Web.Admin
{

    public partial class AdminCuyAccordion : ModuleAdminBasePage
    {
	//Create an instance of this module...
        private CuyAccordionModule _Module;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get THIS module class and put it in _Module
            //ALWAYS GET THE MODULE AND PUT IT IN A PRIVATE VARIABLE
            this._Module = this.Module as CuyAccordionModule;

            //Set fckEditor Base Path
            this.fckEditor.BasePath = this.Page.ResolveUrl("~/Support/FCKeditor/");

            //To DISPLAY template styles in editor (Only use if you have template specific editor css) 
            //fckEditor.EditorAreaCSS = UrlHelper.GetApplicationPath() + this.Node.Template.BasePath + "/css/editor_" + this.Node.Template.Css;
            //To ADD template styles in editor DropDown List
            //fckEditor.StylesXmlPath = UrlHelper.GetApplicationPath() + this.Node.Template.BasePath + "/css/fckstyles.xml";

            //Add Admin Page Javascript to page
            RegisterJavascript("jQuery", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/javascript/jquery-1.3.2.min.js");
            RegisterJavascript("js1", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/javascript/jqModal.js");
            
            //Add Admin Page Specific CSS
            RegisterAdminStylesheet("CSS", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/CSS/Editor.css");
            RegisterAdminStylesheet("CSS2", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/CSS/jqModal.css");

            //Modal js
            string csb = "<script type=\"text/javascript\">" + System.Environment.NewLine +
                "$(document).ready(function() {" + System.Environment.NewLine +
                "$('#gvfck').jqm({toTop: true});" + System.Environment.NewLine +
                "});" + System.Environment.NewLine +
                "</script>" + System.Environment.NewLine;

            Page.RegisterClientScriptBlock("modal", csb);

            ShowSelectedAccordion();
            CheckSelected();

            if (!Page.IsPostBack)
            {
                DatabindAccordions();
                DatabindAccordionItems(Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"]));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Accordion a = new Accordion();
            a.Name = tbName.Text;
            a.Description = tbDescription.Text;
            a.Created = DateTime.Now;
            a.Active = cbActive.Checked;

            a.Node = this._Module.Section.Node;

            this._Module._cuyAccordionDao.SaveAccordion(a);

            lblMessages.Text = "Accordion " + a.Name + " added.";

            DatabindAccordions();

            tbName.Text = "";
            tbDescription.Text = "";
        }

        protected void btnSubmitItem_Click(object sender, EventArgs e)
        {
            AccordionItem i = new AccordionItem();
            i.Title = tbTitle.Text;

            int aId = Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"]);
            Accordion a = this._Module._cuyAccordionDao.GetAccordion(aId);
            i.ParentAccordion = a;

            i.HTML = fckEditor.Value;

            i.Active = cbItemActive.Checked;
            i.Created = DateTime.Now;

            this._Module._cuyAccordionDao.SaveAccordionItem(i);

            lblMessages.Text = "Accordion Item " + i.Title + " added.";
            DatabindAccordionItems(Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"]));

            //Reset inputs
            tbTitle.Text = "";
            fckEditor.Value = "";
            cbItemActive.Checked = false;
        }


        private void CheckSelected()
        {
            if ((Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"]) < 1))
            {
                btnSelectItem.Enabled = false;
            }
            else
            {
                btnSelectItem.Enabled = true;
            }
        }

        private void DatabindAccordions()
        {
            gvAccordion.DataSource = this._Module._cuyAccordionDao.GetNodesAccordions(this._Module.Section.Node);
            gvAccordion.DataBind();
        }

        private void DatabindAccordionItems(int accordionId)
        {
            if (this._Module._cuyAccordionDao.AccordionExists(accordionId))
            {
                Accordion a = this._Module._cuyAccordionDao.GetAccordion(accordionId);
                gvAccordionItems.DataSource = a.AccordionItems;
                gvAccordionItems.DataBind();
            }
            else
            {
                lblMessages.Text = "There is no accordion assigned to this section.";
            }
        }

        private void ShowSelectedAccordion()
        {
            lblModuleSetting.Text = "No Accordion Seleted";
            int accordionId = Convert.ToInt32(_Module.Section.Settings["ACCORDION_ID"]);
            if (this._Module._cuyAccordionDao.AccordionExists(accordionId))
            {
                Accordion l = this._Module._cuyAccordionDao.GetAccordion(accordionId);
                lblModuleSetting.Text = "Current Accordion: " + l.Name + " (Created " + l.Created.ToShortDateString() + ")";
            }
        }

        #region gvAccordion
            protected void gvAccordion_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (this._Module.Section.Settings.Contains("ACCORDION_ID"))
                {
                    this._Module.Section.Settings.Remove("ACCORDION_ID");
                    this._Module.Section.Settings.Add("ACCORDION_ID", gvAccordion.SelectedValue.ToString());
                    this._Module._sectionService.SaveSection(this._Module.Section);

                    ShowSelectedAccordion();

                    DatabindAccordions();
                    DatabindAccordionItems(Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"].ToString()));
                }
                CheckSelected();
            }

            protected void gvAccordion_RowEditing(object sender, GridViewEditEventArgs e)
            {
                gvAccordion.EditIndex = e.NewEditIndex;
                DatabindAccordions();
            }

            protected void gvAccordion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                gvAccordion.EditIndex = -1;
                DatabindAccordions();
            }

            protected void gvAccordion_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                int accordionid = Convert.ToInt32(gvAccordion.DataKeys[e.RowIndex].Value);
                Accordion a = this._Module._cuyAccordionDao.GetAccordion(accordionid);

                TextBox tbName = gvAccordion.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox;
                a.Name = tbName.Text;

                CheckBox cbActive = gvAccordion.Rows[e.RowIndex].Cells[2].Controls[0] as CheckBox;
                a.Active = cbActive.Checked;

                this._Module._cuyAccordionDao.SaveAccordion(a);

                gvAccordion.EditIndex = -1;
                DatabindAccordions();
            }
            
            protected void gvAccordion_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                int delaccordionid = (int)gvAccordion.DataKeys[e.RowIndex].Value;
                int accordionid = Convert.ToInt32(_Module.Section.Settings["ACCORDION_ID"]);
                Accordion a = this._Module._cuyAccordionDao.GetAccordion(delaccordionid);
                this._Module._cuyAccordionDao.DeleteAccordion(a);
                if (delaccordionid == accordionid)
                {
                    this._Module.Section.Settings.Remove("ACCORDION_ID");
                    this._Module.Section.Settings.Add("ACCORDION_ID", "0");
                    this._Module._sectionService.SaveSection(this._Module.Section);
                    gvAccordionItems.DataBind();
                }
                ShowSelectedAccordion();
                CheckSelected();
                DatabindAccordions();
            }
        #endregion


        #region gvAccordionItems
            protected void gvAccordionItems_RowEditing(object sender, GridViewEditEventArgs e)
            {
                gvAccordionItems.EditIndex = e.NewEditIndex;
                DatabindAccordionItems(Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"]));
            }
        #endregion

            protected void gvAccordionItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                gvAccordionItems.EditIndex = -1;
                DatabindAccordionItems(Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"]));
            }

            protected void gvAccordionItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                TextBox tbTitle = gvAccordionItems.Rows[e.RowIndex].FindControl("tbTitle") as TextBox;
                
                FredCK.FCKeditorV2.FCKeditor fckEditorGV = gvAccordionItems.Rows[e.RowIndex].FindControl("fckEditorGV") as FredCK.FCKeditorV2.FCKeditor;
                
                CheckBox cbActiveEdit = gvAccordionItems.Rows[e.RowIndex].FindControl("cbActiveEdit") as CheckBox;

                int accordionId = Convert.ToInt32(_Module.Section.Settings["ACCORDION_ID"]);
                int accordionItemId = (int)gvAccordionItems.DataKeys[e.RowIndex].Value;
                AccordionItem i = this._Module._cuyAccordionDao.GetAccordionItem(accordionItemId);

                i.Title = tbTitle.Text;
                i.HTML = fckEditorGV.Value;
                i.Active = cbActiveEdit.Checked;

                this._Module._cuyAccordionDao.SaveAccordionItem(i);

                gvAccordionItems.EditIndex = -1;
                DatabindAccordionItems(accordionId); 
            }

            protected void gvAccordionItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                int accordionId = Convert.ToInt32(_Module.Section.Settings["ACCORDION_ID"]);

                int accordionItemId = (int)gvAccordionItems.DataKeys[e.RowIndex].Value;
                AccordionItem a = this._Module._cuyAccordionDao.GetAccordionItem(accordionItemId);
                this._Module._cuyAccordionDao.DeleteAccordionItem(a);

                DatabindAccordionItems(accordionId);
            }

    }
}
