USE [retail_receivables]
GO

/****** Object:  StoredProcedure [dbo].[web_credit_past_due_sp]    Script Date: 07/31/2012 17:23:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[web_credit_past_due_sp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[web_credit_past_due_sp]
GO

USE [retail_receivables]
GO

/****** Object:  StoredProcedure [dbo].[web_credit_past_due_sp]    Script Date: 07/31/2012 17:23:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Mark Alford
-- Create date: 7/12/2012
-- Description:	CR Quarterly Coop A/R Review of
--              Past Due Accounts
-- Project:     P-34502
-- Activity:    ssc1900016
-- =============================================
CREATE PROCEDURE [dbo].[web_credit_past_due_sp]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 DECLARE Accounting_Quarters CURSOR FOR
  SELECT
DISTINCT [eff_date]
       , CONVERT(CHAR(10),[eff_date],101) AS [AcctPeriod]
    FROM [retail_receivables].[dbo].[rss_ar_balance_eom]
   WHERE [eff_date] >= '12/31/2010'
     AND CAST(MONTH([eff_date]) AS DECIMAL)/3 = FLOOR(CAST(MONTH([eff_date]) AS DECIMAL)/3)
   ORDER
      BY [eff_date];

 DECLARE @eff_date DATETIME;
 DECLARE @AcctPeriod CHAR(10);
 DECLARE @LastProvision CHAR(10);
     SET @LastProvision = '';
 DECLARE @strSQL NVARCHAR(MAX);
     SET @strSQL = 'SELECT [Region]           = RTRIM(LEFT([lv3_desc],CHARINDEX(''-'',[lv3_desc]) - 1))
     , [Regional Manager] = LTRIM(SUBSTRING([lv3_desc],CHARINDEX(''-'',[lv3_desc]) + 1,64))
     , [District]         = RTRIM(LEFT([lv4_desc],CHARINDEX(''-'',[lv4_desc]) - 1))
     , [District Manager] = LTRIM(SUBSTRING([lv4_desc],CHARINDEX(''-'',[lv4_desc]) + 1,64))
     , CASE 
         WHEN LEFT([lv5_rollup],1) = ''C'' THEN '' + '' + [lv5_desc]
         ELSE [entry_point] + '' '' + [lv5_desc]
         END AS [Location]';

    OPEN Accounting_Quarters;

   FETCH NEXT FROM Accounting_Quarters INTO @eff_date, @AcctPeriod;

   WHILE @@FETCH_STATUS = 0
   BEGIN
	 SET @strSQL = @strSQL + 
	               CHAR(13) +
	               '     , SUM(CASE WHEN [eff_date] = ''' + 
	               @AcctPeriod + 
	               ''' THEN [PD_180]       ELSE 0 END) AS [' +
	               RTRIM(CAST(MONTH(@eff_date) AS CHAR(2))) +
	               '/' +
	               CAST(YEAR(@AcctPeriod) AS CHAR(4)) +
	               ' PD 180] ';
	 SET @strSQL = @strSQL + 
	               CHAR(13) +
	               '     , SUM(CASE WHEN [eff_date] = ''' + 
	               @AcctPeriod + 
	               ''' THEN [PD_180] * .25 ELSE 0 END) AS [' +
	               RTRIM(CAST(MONTH(DATEADD(MONTH,1,@eff_date)) AS CHAR(2))) +
	               '/' +
	               CAST(YEAR(DATEADD(MONTH,1,@eff_date)) AS CHAR(4)) +
	               ' Provision] ';
	  IF @LastProvision <> '          '
	 SET @strSQL = @strSQL + 
	               CHAR(13) +
	               '     , SUM(CASE WHEN [eff_date] = ''' + 
	               @LastProvision + 
	               ''' THEN [PD_180] * .25 ELSE 0 END) - ' +
	               CHAR(13) +
	               '       SUM(CASE WHEN [eff_date] = ''' + 
	               @AcctPeriod + 
	               ''' THEN [PD_180] * .25 ELSE 0 END) AS [Increase/ (Decrease)] ';
	 SET @LastProvision = @AcctPeriod;
   FETCH NEXT FROM Accounting_Quarters INTO @eff_date, @AcctPeriod;
     END;

   CLOSE Accounting_Quarters;
DEALLOCATE Accounting_Quarters;

     SET @strSQL = @strSQL + '
  FROM [retail_receivables].[dbo].[rss_ar_balance_eom]
  LEFT
  JOIN [corp_global].[dbo].[cg_report_structures]
    ON [store_nbr] = [entry_point]
 WHERE [eff_date] >= ''12/31/2010''
   AND [store_nbr] < ''30000''
   AND [group_id]     = 4
   AND [structure_id] = 238
   AND [lv2_name]     = ''RCOOP''
 GROUP
    BY LEFT([lv3_desc],CHARINDEX(''-'',[lv3_desc]) - 1)
     , LEFT([lv4_desc],CHARINDEX(''-'',[lv4_desc]) - 1)
     , LTRIM(SUBSTRING([lv3_desc],CHARINDEX(''-'',[lv3_desc]) + 1,64))
     , LTRIM(SUBSTRING([lv4_desc],CHARINDEX(''-'',[lv4_desc]) + 1,64))
     , CASE 
         WHEN LEFT([lv5_rollup],1) = ''C'' THEN '' + '' + [lv5_desc]
         ELSE [entry_point] + '' '' + [lv5_desc]
         END
 ORDER
    BY [Region]
     , [District]';
     
   PRINT @strSQL;

    EXEC(@strSQL);
END

GO