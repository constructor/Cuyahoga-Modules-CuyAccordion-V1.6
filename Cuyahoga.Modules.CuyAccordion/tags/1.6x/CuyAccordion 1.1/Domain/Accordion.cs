namespace  Cuyahoga.Modules.CuyAccordion.Domain
{
    // data object class Accordion for table cm_Accordion
    // By John [2009-05-16] 

    using System;
    using System.Collections;
    using Cuyahoga.Core.Domain;


    public  class Accordion 
    {
        #region Private_Variables
            private int _accordionid;
            private IList _accordionItems;
            private string _name;
            private string _description;
            private DateTime _created;
            private Boolean _active;
            private Node _node;
        #endregion

        #region Constructors
            public Accordion(){ 	
		        this._accordionid = -1;
            }
        #endregion

        #region Properties
            public virtual IList AccordionItems
            {
                get { return _accordionItems; }
                set { _accordionItems = value; }
            }
            public virtual int AccordionId
            {
                get { return _accordionid; }
	            set { _accordionid = value; }
            }
            public virtual string Name
            {
                get { return _name; }
	            set { _name = value; }
            }
            public virtual string Description
            {
                get { return _description; }
	            set { _description = value; }
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
            public virtual Node Node
            {
                get { return _node; }
	            set { _node = value; }
            }
        #endregion

	} 
        // Accordion
}
