using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;

using NHibernate;

using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.CuyAccordion.Domain;

namespace Cuyahoga.Modules.CuyAccordion.DataAccess
{
    /// <summary>
    /// Specific Data Access functionality for CuyAccordion module.
    /// NOTE: Set the 'Delete Rule' and 'Update Rule' on the relationships between tables (in the Database Diagrams): Database Designer: Update and Delete Specification
    /// </summary>
    [Transactional]
    public class CuyAccordionDao : Cuyahoga.Modules.CuyAccordion.DataAccess.ICuyAccordionDao
    {

        private ISessionManager _sessionManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sessionManager"></param>
        public CuyAccordionDao(ISessionManager sessionManager)
	    {
	        this._sessionManager = sessionManager;
        }

        #region Accordion Data Access
            public IList GetAccordions()
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.Accordion c";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    //q.SetInt32("siteId", site.Id);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'Accordion' object list: " + x.Message);
                }
            }

            public IList GetNodesAccordions(Node node)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.Accordion c where c.Node = :Node";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetEntity("Node", node);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'Accordion' object list: " + x.Message);
                }
            }

            public Boolean AccordionExists(int accordionId)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.Accordion c where c.AccordionId = :AccordionId";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetInt32("AccordionId", accordionId);
                    return q.List().Count > 0;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not check the 'Accordion object: " + x.Message + "exists.");
                }
            }

            public Accordion GetAccordion(int accordionId)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.Accordion c where c.AccordionId = :accordionId";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetInt32("accordionId", accordionId);
                    return q.List()[0] as Accordion;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'Accordion object: " + x.Message);
                }
            }
            public void SaveAccordion(Accordion accordionToSave)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.Save(accordionToSave);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not save the 'Accordion object: " + x.Message);
                }
            }

            public void SaveOrUpdateAccordion(Accordion accordionToSave)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.SaveOrUpdate(accordionToSave);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not save the 'Accordion object: " + x.Message);
                }
            }

            public void DeleteAccordion(Accordion accordionToDelete)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.Delete(accordionToDelete);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not delete the 'Accordion object: " + x.Message);
                }
            }
        #endregion Accordion Data Access

        #region AccordionItem Data Access
            public IList GetAccordionItems()
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.AccordionItem c order by c.ZIndex";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    //q.SetInt32("siteId", site.Id);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'AccordionItem' object list: " + x.Message);
                }
            }
            public IList GetActiveAccordionSpecificItems(Accordion accordion)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.AccordionItem c where c.Active = :active and c.Accordion = :accordion order by c.ZIndex";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetBoolean("active", true);
                    q.SetEntity("accordion", accordion);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'AccordionItem' object list: " + x.Message);
                }
            }
            public IList GetAllAccordionSpecificItems(Accordion accordion)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.AccordionItem c where c.Accordion = :accordion order by c.ZIndex";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetEntity("accordion", accordion);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'AccordionItem' object list: " + x.Message);
                }
            }
            public int GetActiveAccordionItemsCount(Accordion accordion)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.AccordionItem c where c.Active = :active and c.Accordion = :accordion";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetBoolean("active", true);
                    q.SetEntity("accordion", accordion);
                    return q.List().Count;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'AccordionItem' count: " + x.Message);
                }
            }
            public AccordionItem GetAccordionItem(int accordionItemId)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyAccordion.Domain.AccordionItem c where c.AccordionItemId = :accordionItemId";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetInt32("accordionItemId", accordionItemId);
                    return q.List()[0] as AccordionItem;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'AccordionItem object: " + x.Message);
                }
            }
            public void SaveAccordionItem(AccordionItem accordionItemToSave)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.Save(accordionItemToSave);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not save the 'AccordionItem object: " + x.Message);
                }
            }

            public void SaveOrUpdateAccordionItem(AccordionItem accordionItemToSave)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.SaveOrUpdate(accordionItemToSave);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not save the 'AccordionItem object: " + x.Message);
                }
            }

            public void DeleteAccordionItem(AccordionItem accordionItemToDelete)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.Delete(accordionItemToDelete);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not delete the 'AccordionItem object: " + x.Message);
                }
            }
        #endregion AccordionItem Data Access

    }
}
