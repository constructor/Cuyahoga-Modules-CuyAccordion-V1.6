using System;
using System.Collections;
using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.CuyAccordion.Domain;
using System.Data.SqlClient;

namespace Cuyahoga.Modules.CuyAccordion.DataAccess
{
    public interface ICuyAccordionDao
    {
        #region Accordion Data Access
            IList GetAccordions();

            Accordion GetAccordion(int accordionID);

            void SaveAccordion(Accordion accordionToSave);

            void SaveOrUpdateAccordion(Accordion accordionToSave);

            void DeleteAccordion(Accordion accordionToDelete);
        #endregion Accordion Data Access

        #region AccordionItem Data Access
            IList GetAccordionItems();

            IList GetActiveAccordionSpecificItems(Accordion accordion);

            IList GetAllAccordionSpecificItems(Accordion accordion);

            int GetActiveAccordionItemsCount(Accordion accordion);

            IList GetNodesAccordions(Node node);

            Boolean AccordionExists(int accordionId);

            AccordionItem GetAccordionItem(int accordionItemID);

            void SaveAccordionItem(AccordionItem accordionItemToSave);

            void SaveOrUpdateAccordionItem(AccordionItem accordionItemToSave);

            void DeleteAccordionItem(AccordionItem accordionItemToDelete);
        #endregion AccordionItem Data Access
    }
}
