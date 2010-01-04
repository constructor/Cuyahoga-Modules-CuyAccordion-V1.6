-- 'CuyAccordion' Module
SET DATEFORMAT ymd

-- Module database entries table creation script
CREATE TABLE [dbo].[cm_Accordion](
	[AccordionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NULL,
	[Description] [varchar](2048) NULL,
	[Created] [datetime] NULL,
	[Active] [bit] NULL,
	[NodeId] [int] NULL,
 CONSTRAINT [PK_cm_Accordion] PRIMARY KEY CLUSTERED 
(
	[AccordionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cm_Accordion]  WITH CHECK ADD  CONSTRAINT [FK_cm_Accordion_cuyahoga_node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[cuyahoga_node] ([nodeid])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cm_Accordion] CHECK CONSTRAINT [FK_cm_Accordion_cuyahoga_node]
GO


CREATE TABLE [dbo].[cm_AccordionItem](
	[AccordionItemId] [int] IDENTITY(1,1) NOT NULL,
	[ParentAccordion] [int] NOT NULL,
	[Title] [varchar](64) NULL,
	[HTML] [text] NULL,
	[Created] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_cm_AccordionItem] PRIMARY KEY CLUSTERED 
(
	[AccordionItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cm_AccordionItem]  WITH CHECK ADD  CONSTRAINT [FK_cm_AccordionItem_cm_Accordion] FOREIGN KEY([ParentAccordion])
REFERENCES [dbo].[cm_Accordion] ([AccordionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cm_AccordionItem] CHECK CONSTRAINT [FK_cm_AccordionItem_cm_Accordion]
GO


-- Module database entries and settings
DECLARE @moduletypeid int

INSERT INTO cuyahoga_moduletype ([name],assemblyname,classname,path,editpath,inserttimestamp,updatetimestamp) VALUES ('CuyAccordion','Cuyahoga.Modules.CuyAccordion','Cuyahoga.Modules.CuyAccordion.CuyAccordionModule','Modules/CuyAccordion/CuyAccordion.ascx', 'Modules/CuyAccordion/Admin/AdminCuyAccordion.aspx', '2008-11-29 19:59:59.998', '2008-11-29 19:59:59.998')
SELECT @moduletypeid = Scope_Identity()

INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, 'ACCORDION_ID', 'Current Accordion Id (set to 0 (zero) as default)', 'System.Int32', 0, 1)
--INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, 'SETTING_NAME', 'Setting description text...', 'System.Boolean', 0, 1)

--NOTE: The 'servicekey' must be lower case (ToLower must be used in the code so any upper case entries do not work)
INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'cuyaccordion.cuyaccordiondao', 'Cuyahoga.Modules.CuyAccordion.DataAccess.ICuyAccordionDao, Cuyahoga.Modules.CuyAccordion', 'Cuyahoga.Modules.CuyAccordion.DataAccess.CuyAccordionDao, Cuyahoga.Modules.CuyAccordion')
GO

INSERT INTO cuyahoga_version (assembly, major, minor, patch) VALUES ('Cuyahoga.Modules.CuyAccordion', 1, 5, 2)
GO
