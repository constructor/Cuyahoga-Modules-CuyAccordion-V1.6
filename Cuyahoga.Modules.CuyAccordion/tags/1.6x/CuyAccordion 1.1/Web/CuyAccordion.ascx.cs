using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Cuyahoga.Core.Util;
using Cuyahoga.Core.Domain;
using Cuyahoga.Web.UI;
using Cuyahoga.Web.Util;

using Cuyahoga.Modules.CuyAccordion;
using Cuyahoga.Modules.CuyAccordion.Domain;

namespace Cuyahoga.Modules.CuyAccordion.Web
{
    public partial class CuyAccordion : BaseModuleControl
    {
        //Create an instance of this module...
        private CuyAccordionModule _Module;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get THIS module class and put it in _Module
            //ALWAYS GET THE MODULE AND PUT IT IN A PRIVATE VARIABLE
            this._Module = this.Module as CuyAccordionModule;

            //Register Javascripts
            RegisterJavaScript("CuyAccordion_js", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/JavaScript/jquery-1.3.2.min.js");
            RegisterJavaScript("CuyAccordion_js2", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/JavaScript/ui.core.js");
            RegisterJavaScript("CuyAccordion_js3", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/JavaScript/ui.accordion.js");

            //Register Css
            RegisterStyleSheet("CuyAccordion_css", UrlHelper.GetApplicationPath() + "Modules/CuyAccordion/css/accordion.css");

            //Get Settings for js
            string COLLAPSIBLE = _Module.Section.Settings["COLLAPSIBLE"].ToString().ToLower();
            string AUTO_HEIGHT = _Module.Section.Settings["AUTO_HEIGHT"].ToString().ToLower();
            string CLEAR_STYLE = _Module.Section.Settings["CLEAR_STYLE"].ToString().ToLower();
            string ACTIVE_INDEX = _Module.Section.Settings["ACTIVE_INDEX"] != null ? ", active: " + _Module.Section.Settings["ACTIVE_INDEX"].ToString().ToLower() : string.Empty;

            //RegisterClientScriptBlock
            string lightboxjs = "<script type=\"text/javascript\">" + System.Environment.NewLine
                            + "$(document).ready(function() {" + System.Environment.NewLine
                            + "$(\"#" + pnlAccordion.ClientID + "\").accordion({ collapsible: " + COLLAPSIBLE + ", autoHeight: " + AUTO_HEIGHT + ", clearStyle: " + CLEAR_STYLE + ACTIVE_INDEX + " });" + System.Environment.NewLine
                            + "});" + System.Environment.NewLine
                            + "</script>" + System.Environment.NewLine;

            Page.RegisterClientScriptBlock(this._Module.Section.Id.ToString(), lightboxjs);

            int accordionid = Convert.ToInt32(this._Module.Section.Settings["ACCORDION_ID"].ToString());
            if (this._Module._cuyAccordionDao.AccordionExists(accordionid))
            {
                Accordion a = this._Module._cuyAccordionDao.GetAccordion(accordionid);

                rptAccordion.DataSource = this._Module._cuyAccordionDao.GetActiveAccordionSpecificItems(a);
                rptAccordion.DataBind();
            }
            else
            {
                lblMessages.Text = "There is no accordion assigned to this section.";
            }

        }
    }
}
