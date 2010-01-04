using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Web;

using Cuyahoga.Core; //Contains: INHibernateModule
using Cuyahoga.Core.Domain; //Contains: ModuleBase
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Service.SiteStructure;
using Cuyahoga.Core.Search;
using Cuyahoga.Core.Util;
using Cuyahoga.Core.Communication;

using Cuyahoga.Web.Util;
using Cuyahoga.Core.DataAccess; //Contains: ICommonDao
using Castle.Services.Transaction;
using Castle.Core;

using Cuyahoga.Modules.CuyAccordion.DataAccess; //Needed or: 'as it has dependencies to be satisfied'
using Cuyahoga.Modules.CuyAccordion.Domain;

namespace Cuyahoga.Modules.CuyAccordion
{
    [Transactional]
    public class CuyAccordionModule : ModuleBase, INHibernateModule//, IActionConsumer, IActionProvider
    {
        private ICommonDao _commonDao;
        public ICuyAccordionDao _cuyAccordionDao;
        public ISectionService _sectionService;
        //private ActionCollection _inboundActions;
        //private ActionCollection _outboundActions;

        public CuyAccordionModule(ICommonDao commonDao, ICuyAccordionDao cuyAccordionDao, ISectionService sectionService)
        {
            this._commonDao = commonDao;
            this._cuyAccordionDao = cuyAccordionDao;
            this._sectionService = sectionService;

            //InitInboundActions();
            //InitOutboundActions();
        }

        #region Actions
            //private void InitInboundActions()
            //{
            //    this._inboundActions = new ActionCollection();
            //    this._inboundActions.Add(new Cuyahoga.Core.Communication.Action("EditRecruiterProfile", null)); //Replace null with passed values
            //}
            //public ActionCollection GetInboundActions()
            //{
            //    return this._inboundActions;
            //}

            //private void InitOutboundActions()
            //{
            //    this._outboundActions = new ActionCollection();
            //    this._outboundActions.Add(new Cuyahoga.Core.Communication.Action("EditRecruiterProfile", null));//Replace null with passed values
            //}
            //public ActionCollection GetOutboundActions()
            //{
            //    return this._outboundActions;
            //}
        #endregion Actions

        #region Data Access

        #endregion Data Access

    }
}
