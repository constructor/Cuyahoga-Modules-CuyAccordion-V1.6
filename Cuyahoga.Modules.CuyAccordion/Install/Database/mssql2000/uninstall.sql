-- 'CuyAccordion' Module Uninstall

-- Delete Module Tables

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cm_AccordionItem_cm_Accordion]') AND parent_object_id = OBJECT_ID(N'[dbo].[cm_AccordionItem]'))
ALTER TABLE [dbo].[cm_AccordionItem] DROP CONSTRAINT [FK_cm_AccordionItem_cm_Accordion]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cm_AccordionItem]') AND type in (N'U'))
DROP TABLE [dbo].[cm_AccordionItem]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cm_Accordion_cuyahoga_node]') AND parent_object_id = OBJECT_ID(N'[dbo].[cm_Accordion]'))
ALTER TABLE [dbo].[cm_Accordion] DROP CONSTRAINT [FK_cm_Accordion_cuyahoga_node]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cm_Accordion]') AND type in (N'U'))
DROP TABLE [dbo].[cm_Accordion]
GO


-- Delete System Database Entries

DELETE FROM cuyahoga_version WHERE assembly = 'Cuyahoga.Modules.CuyAccordion'
GO

DELETE cuyahoga_modulesetting
FROM cuyahoga_modulesetting ms
INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid
AND mt.assemblyname = 'Cuyahoga.Modules.CuyAccordion'
GO

DELETE cuyahoga_moduleservice
FROM cuyahoga_moduleservice ms
INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.CuyAccordion'
GO

DELETE FROM cuyahoga_moduletype
WHERE assemblyname = 'Cuyahoga.Modules.CuyAccordion'
GO



--UNINSTALL STORED PROCEDURES

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROCEDURENAME]') AND type in (N'P'))
--BEGIN    
--	DROP PROCEDURE [dbo].[PROCEDURE_NAME]
--END
--GO



--UNINSTALL FUNCTIONS

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FUNCTIONNAME]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
--DROP FUNCTION [dbo].[FUNCTIONNAME]
--GO
