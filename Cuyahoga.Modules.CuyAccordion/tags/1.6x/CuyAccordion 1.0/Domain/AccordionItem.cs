namespace  Cuyahoga.Modules.CuyAccordion.Domain
{
    // data object class AccordionItem for table cm_AccordionItem
    // By John [2009-05-15] 

    using System;
    using System.Collections;
    using Cuyahoga.Core.Domain;


    public  class AccordionItem 
    {
        #region Private_Variables
            private int _accordionitemid;
            private Accordion _parentAccordion;
            private string _title;
            private string _hTML;
            private DateTime _created;
            private Boolean _active;
        #endregion

        #region Constructors
            public AccordionItem(){ 	
		        this._accordionitemid = -1;
            }
        #endregion

        #region Properties
            public virtual int AccordionItemId
            {
                get { return _accordionitemid; }
	            set { _accordionitemid = value; }
            }
            public virtual Accordion ParentAccordion
            {
                get { return _parentAccordion; }
	            set { _parentAccordion = value; }
            }
            public virtual string Title
            {
                get { return _title; }
	            set { _title = value; }
            }
            public virtual string HTML
            {
                get { return _hTML; }
	            set { _hTML = value; }
            }
            public virtual DateTime Created
            {
                get { return _created; }
	            set { _created = value; }
            }
            public virtual Boolean Active
            {
                get { return _active; }
	            set { _active = value; }
            }
        #endregion

	} 
        // AccordionItem
}
