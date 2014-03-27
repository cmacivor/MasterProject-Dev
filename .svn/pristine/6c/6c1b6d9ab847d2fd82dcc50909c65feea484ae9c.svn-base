Option Explicit On
Option Strict On

'*****************************************************************************
'* Project:  P34502
'* Activity: ssc1200026
'* Created:  7/31/2012
'* Author:   Mark Alford
'* Descrip:  CR Quarterly Coop A/R Review of Past Due Accounts
'*****************************************************************************


Imports vb = Microsoft.VisualBasic
Imports eXl = SoftArtisans.OfficeWriter.ExcelWriter
Imports System.Data.Objects
Imports System.Linq
Imports System.Data
Imports retail_receivablesModel

Partial Class ssc1900016_default
  Inherits System.Web.UI.Page

  'Setup Page Variables
  '---------------------------------------------------------------------------------------------------------
  Protected ReadOnly strTitle As String = "Quarterly Coop A/R Review of Past Due Accounts"

  Protected ReadOnly bolStoredProcedure As Boolean = True
  Protected ReadOnly strSQLSelect As String = "[retail_receivables].[dbo].[web_credit_past_due_sp]"
  Protected ReadOnly strSQLDataSource As String = "Poseidon2"
  Protected ReadOnly strSQLInitialCatalog As String = "dw_work"
  Protected ReadOnly strSQLUserID As String = "dwbatch"
  Protected ReadOnly strSQLPassword As String = "fEznEXY59Nc="
  Protected ReadOnly intSQLTimeOutSeconds As Integer = 30

  Protected ReadOnly strExcelName As String = "Quarterly Coop AR Review of Past Due Accounts"

  'Automatic Excel Formating
  Protected ReadOnly stylDecimal As String = "$#,##0.00;[Red]($#,##0.00)"
  Protected ReadOnly stylDouble As String = "$#,##0.00;[Red]($#,##0.00)"
  Protected ReadOnly stylInteger As String = "#,##0;[Red](#,##0)"
  Protected ReadOnly stylDateTime As String = "mm/dd/yyyy"
  '---------------------------------------------------------------------------------------------------------

  Protected gid As Guid
  Public strPath As String

  Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
    Dim cookie As HttpCookie = New HttpCookie("OpenSS")
    Try
      cookie = Request.Cookies("OpenSS")

      If cookie.Value Is vbNullString Or cookie.Values("UserID") Is vbNullString Or cookie.Values("UserID") = "" Or cookie.Values("LGRP") Is vbNullString Or cookie.Values("LGRP") = "" Then
        Response.Write("#1 OpenStream session could not be validated.  Please log back into OpenStream.")
        Response.End()
      End If

    Catch ex As Exception
      Response.Write("#2 OpenStream session could not be validated.  Please log back into OpenStream.")
      'Response.End()
    End Try
  End Sub

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    Me.Title = strTitle
    Me.lblTitle.Text = strTitle
    Session("Title") = strTitle

    strPath = Request.PhysicalApplicationPath

    If Request.Params("Status") = "DwnLd" Then
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=" & strExcelName & ".xls")
      Response.ContentType = "application/vnd.ms-excel"
      Response.WriteFile(Request.Params("Message"))
      Response.End()
    End If

    'bind the gridview
    BindGrid()

  End Sub

  Private Sub BindGrid()
    Dim retail As New ssc1900016_retail_receivablesEntities2()
    Dim parameters As ObjectSet(Of web_credit_past_due_rates) = retail.web_credit_past_due_rates
        Dim paramQuery = From p In parameters
                         Order By p.start_date
                         Select p

    gdvConfiguration.DataSource = paramQuery
    gdvConfiguration.DataBind()
    retail.Dispose()
  End Sub

  Protected Sub btnExcel_Click(sender As Object, e As System.EventArgs) Handles btnExcel.Click
    gid = Guid.NewGuid
    Dim ts As New Threading.ThreadStart(AddressOf subDoSearch)
    Dim th As New Threading.Thread(ts)
    th.Start()
    Session(gid.ToString) = "Busy"
    Response.Redirect("ssc1900016_wait.aspx?ID=" + gid.ToString())
  End Sub

  Protected Sub subDoSearch()
    Dim sqlConnStrBuild As New System.Data.SqlClient.SqlConnectionStringBuilder
    sqlConnStrBuild.DataSource = strSQLDataSource
    sqlConnStrBuild.InitialCatalog = strSQLInitialCatalog
    sqlConnStrBuild.UserID = strSQLUserID
    sqlConnStrBuild.Password = funcGetPassWord()
    Me.sql_SDS.ConnectionString = sqlConnStrBuild.ConnectionString

    If bolStoredProcedure Then
      Me.sql_SDS.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
    Else
      Me.sql_SDS.SelectCommandType = SqlDataSourceCommandType.Text
    End If
    'This is where the SPROC gets called- could implement logic here.
    Me.sql_SDS.SelectCommand = strSQLSelect

    Dim sqlDV As System.Data.DataView
    Try
      sqlDV = CType(sql_SDS.Select(System.Web.UI.DataSourceSelectArguments.Empty), Data.DataView)
      If sqlDV.Table.Rows.Count > 0 Then
        subCreateExcel(sqlDV)
      Else
        Session(gid.ToString) = "Failure<br/>Status=NoRows<br/>Message=This combination of choices did not return any rows."
      End If
    Catch exSQL As System.Data.SqlClient.SqlException
      If exSQL.Number = -2 Then
        Session(gid.ToString) = "TimeOut"
      Else
        Session(gid.ToString) = "Failure<br/>Status=Error<br/>Message=SQL Error.  " & exSQL.Message & "<br/><br/>" & exSQL.StackTrace
      End If
    Catch ex As Exception
      Session(gid.ToString) = "Failure<br/>Status=Error<br/>Message=" & ex.Message & "<br/><br/>" & ex.StackTrace
    End Try
  End Sub

  Protected Sub subCreateExcel(ByVal dvExcel As System.Data.DataView)

    Try

      Dim sqlDV As System.Data.DataView = dvExcel

      Dim eApp As New eXl.ExcelApplication
      Dim eWB As eXl.Workbook = eApp.Create
      Dim eWS As eXl.Worksheet = eWB(0)

      'Set Colors
      Dim p As eXl.Palette = eWB.Palette()
      Dim clr_orange As eXl.Color = p.GetClosestColor(System.Drawing.Color.Orange)
      Dim clr_yellow As eXl.Color = p.GetClosestColor(System.Drawing.Color.Yellow)

      Dim intStartRow As Integer = 4

      'Create Headers and Format Columns
      For x = 0 To sqlDV.Table.Columns.Count - 1
        If Left(sqlDV.Table.Columns(x).ColumnName, "Increase/ (Decrease)".Length) = "Increase/ (Decrease)" Then
          eWS.Cells(intStartRow, x).Value = "Increase/ (Decrease)"
        Else
          eWS.Cells(intStartRow, x).Value = sqlDV.Table.Columns(x).ColumnName
        End If
        eWS.Cells(intStartRow, x).Style.Font.Bold = True
        eWS.Cells(intStartRow, x).Style.WrapText = True
        eWS.Cells(intStartRow, x).Style.HorizontalAlignment = eXl.Style.HAlign.Center
        Dim wsColumn As eXl.ColumnProperties = eWS.GetColumnProperties(x)
        Select Case sqlDV.Table.Columns(x).DataType.ToString
          Case "System.String"
          Case "System.Double"
            wsColumn.Style.NumberFormat = stylDouble
          Case "System.DateTime"
            wsColumn.Style.NumberFormat = stylDateTime
          Case "System.Decimal"
            wsColumn.Style.NumberFormat = stylDecimal
          Case "System.Int32"
            wsColumn.Style.NumberFormat = stylInteger
        End Select
      Next

      intStartRow += 1

      'Put Data in Cells
      For intRow = 0 To sqlDV.Table.Rows.Count - 1
        For intCol = 0 To sqlDV.Table.Columns.Count - 1
          eWS.Cells(intStartRow + intRow, intCol).Value = sqlDV(intRow)(intCol)
        Next intCol
      Next intRow


      'Create Subtotals and Groups
      Dim strDistrict As String = CStr(eWS.Cells(intStartRow, 2).Value)
      Dim intStartDistrict As Integer = intStartRow + 1
      Dim intGroupAdjust As Integer = 0
      Dim intWorkSheetRows As Integer = eWS.PopulatedCells.RowCount - 1

      '  First Inner Group - District
      For intRow = intStartRow To intWorkSheetRows + intStartRow
        If intRow = intWorkSheetRows + intStartRow OrElse CStr(eWS.Cells(intRow + intGroupAdjust, 2).Value) <> strDistrict Then
          'New District
          If intRow <> intWorkSheetRows + intStartRow Then
            strDistrict = eWS.Cells(intRow + intGroupAdjust, 2).Value.ToString
          End If
          eWS.InsertRow(intRow + intGroupAdjust)
          For intCol = 5 To eWS.PopulatedCells.ColumnCount - 1
            eWS.Cells(intRow + intGroupAdjust, intCol).Formula = "SUBTOTAL(9," & eWS.Cells(intStartDistrict - 1, intCol).Name & ":" & eWS.Cells(intRow + intGroupAdjust - 1, intCol).Name & ")"
            eWS.Cells(intRow + intGroupAdjust, intCol).Style.Font.Bold = True
          Next
          eWS.Cells(intRow + intGroupAdjust, 2).Value = eWS.Cells(intRow + intGroupAdjust - 1, 2).Value.ToString
          eWS.Cells(intRow + intGroupAdjust, 2).Style.Font.Bold = True
          eWS.Cells(intRow + intGroupAdjust, 3).Value = eWS.Cells(intRow + intGroupAdjust - 1, 3).Value.ToString
          eWS.Cells(intRow + intGroupAdjust, 3).Style.Font.Bold = True
          eWS.GroupRows(intStartDistrict - 1, intRow + intGroupAdjust - intStartDistrict + 1, False)
          intGroupAdjust += 1
          intStartDistrict = intRow + intGroupAdjust + 1
        End If
      Next intRow

      '  Second Outer Group - Region
      Dim strRegion As String = CStr(eWS.Cells(intStartRow, 0).Value)
      Dim intStartRegion As Integer = intStartRow + 1
      intGroupAdjust = 0
      intWorkSheetRows = eWS.PopulatedCells.RowCount - 1
      For intRow = intStartRow To intWorkSheetRows + intStartRow
        If intRow = intWorkSheetRows + intStartRow OrElse (CStr(eWS.Cells(intRow + intGroupAdjust, 0).Value) <> strRegion And CStr(eWS.Cells(intRow + intGroupAdjust, 0).Value) <> "") Then
          'New Region
          If intRow <> intWorkSheetRows + intStartRow Then
            strRegion = eWS.Cells(intRow + intGroupAdjust, 0).Value.ToString
          End If
          eWS.InsertRow(intRow + intGroupAdjust)
          For intCol = 5 To eWS.PopulatedCells.ColumnCount - 1
            eWS.Cells(intRow + intGroupAdjust, intCol).Formula = "SUBTOTAL(9," & eWS.Cells(intStartRegion - 1, intCol).Name & ":" & eWS.Cells(intRow + intGroupAdjust - 1, intCol).Name & ")"
            eWS.Cells(intRow + intGroupAdjust, intCol).Style.Font.Bold = True
          Next
          eWS.Cells(intRow + intGroupAdjust, 0).Value = eWS.Cells(intRow + intGroupAdjust - 2, 0).Value.ToString
          eWS.Cells(intRow + intGroupAdjust, 0).Style.Font.Bold = True
          eWS.Cells(intRow + intGroupAdjust, 1).Value = eWS.Cells(intRow + intGroupAdjust - 2, 1).Value.ToString
          eWS.Cells(intRow + intGroupAdjust, 1).Style.Font.Bold = True
          eWS.GroupRows(intStartRegion - 1, intRow + intGroupAdjust - intStartRegion + 1, False)
          intGroupAdjust += 1
          intStartRegion = intRow + intGroupAdjust + 1
        End If
      Next intRow

      '  Third Final Group - Grand Total
      Dim intTotalRow As Integer = eWS.PopulatedCells.RowCount + intStartRow - 1
      eWS.Cells(intTotalRow, 0).Value() = "Grand Total"
      eWS.Cells(intTotalRow, 0).Style.Font.Bold = True
      For intCol = 5 To eWS.PopulatedCells.ColumnCount - 1
        eWS.Cells(intTotalRow, intCol).Formula = "SUBTOTAL(9," & eWS.Cells(intStartRow, intCol).Name & ":" & eWS.Cells(intTotalRow - 1, intCol).Name & ")"
        eWS.Cells(intTotalRow, intCol).Style.Font.Bold = True
      Next
      eWS.GroupRows(intStartRow, eWS.PopulatedCells.RowCount - 2, False)

      'Format WorkSheet
      eWS.FreezePanes = eWS.Cells(intStartRow, 0)
      eWS.CreateArea(0, 0, sqlDV.Table.Rows.Count, sqlDV.Table.Columns.Count).AutoFitWidth()

      eWS.CreateArea(0, 0, 1, 7).MergeCells()
      eWS.Cells(0, 0).Style.HorizontalAlignment = eXl.Style.HAlign.Left
      eWS.Cells(0, 0).Value = "CONFIDENTIAL / SOUTHERN STATES"
      eWS.CreateArea(1, 0, 1, 7).MergeCells()
      eWS.Cells(1, 0).Style.HorizontalAlignment = eXl.Style.HAlign.Left
      eWS.Cells(1, 0).Value = strTitle & " | Run on " & Format(Now, "MM/dd/yyyy")
      eWS.Cells(1, 0).Style.Font.Bold = True
      eWS.CreateArea(2, 0, 1, 7).MergeCells()
      eWS.Cells(2, 0).Style.HorizontalAlignment = eXl.Style.HAlign.Left
      eWS.Cells(2, 0).Value = "(PROPERTY OF SSC, INC - DO NOT DUPLICATE - DO NOT DISTRIBUTE)"

      eApp.Save(eWB, strPath & "\__" & gid.ToString & ".xls")

      Session(gid.ToString) = strPath & "__" & gid.ToString & ".xls"
      sqlDV.Dispose()

    Catch ex As Exception
      Session(gid.ToString) = "Failure<br/>Status=Error<br/>Message=Could not connect to database.  " & ex.Message & "<br/><br/>" & ex.StackTrace
    End Try
  End Sub

  Protected Sub sql_SDS_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles sql_SDS.Selecting
    e.Command.CommandTimeout = intSQLTimeOutSeconds
  End Sub

  Public Function funcGetPassWord() As String
    Dim strPassWord As String = strSQLPassword

    'UnEncrypt
    Try
      Dim byKey() As Byte = {}
      Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
      Dim inputByteArray(strPassWord.Length) As Byte
      byKey = System.Text.Encoding.UTF8.GetBytes("12345678")
      Dim des As New System.Security.Cryptography.DESCryptoServiceProvider
      inputByteArray = Convert.FromBase64String(strPassWord)
      Dim ms As New System.IO.MemoryStream
      Dim cs As New System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write)
      cs.Write(inputByteArray, 0, inputByteArray.Length)
      cs.FlushFinalBlock()
      Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
      strPassWord = encoding.GetString(ms.ToArray())
    Catch ex As Exception
      strPassWord = Nothing
    End Try

    Return strPassWord
  End Function


  Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
    Try
      Dim context As New ssc1900016_retail_receivablesEntities2()
            Dim parameters As New web_credit_past_due_rates
            If String.IsNullOrWhiteSpace(txtRate.Text) Or String.IsNullOrWhiteSpace(txtStartDate.Text) Then
                lblError.Text = "Both fields are required. Please select a record or create a new one, and then click Add/Save."
                lblError.Visible = True
            Else
                Dim startDate As DateTime = DateTime.Parse(txtStartDate.Text)
                Dim rate As Decimal = Decimal.Parse(txtRate.Text)
                If Session("RowID") Is Nothing Then

                    Dim record = (From r In context.web_credit_past_due_rates
                                  Where r.start_date = startDate
                                  Select r).FirstOrDefault()
                    If record IsNot Nothing Then
                        lblError.Text = "This date already exists in the database"
                        lblError.Visible = True
                    Else
                        
                        parameters.start_date = Convert.ToDateTime(txtStartDate.Text)
                        'parameters.end_date = Convert.ToDateTime(txtEndDate.Text)
                        parameters.rate = Convert.ToDecimal(txtRate.Text)
                        parameters.chgstamp = DateTime.Now
                        context.AddToweb_credit_past_due_rates(parameters)
                        context.SaveChanges()
                        BindGrid()
                        lblError.Visible = False
                        context.Dispose()
                        'txtEndDate.Text = ""
                        txtStartDate.Text = ""
                        txtRate.Text = ""
                        gdvConfiguration.SelectedRowStyle.Reset()
                    End If
                Else
                    Dim currentRecords As ObjectSet(Of web_credit_past_due_rates) = context.web_credit_past_due_rates
                    'Dim startDate As DateTime = DateTime.Parse(txtStartDate.Text)
                    'Dim endDate As DateTime = DateTime.Parse(txtEndDate.Text)
                    Dim rowID As Integer = Convert.ToInt32(Session("RowID"))

                    Dim selectedRecord = (From r In context.web_credit_past_due_rates
                                          Where r.ROWID = rowID
                                          Select r).FirstOrDefault()
                    If selectedRecord IsNot Nothing Then
                        Dim record = (From r In context.web_credit_past_due_rates
                                      Where r.start_date = startDate And
                                      r.rate = rate
                                      Select r).FirstOrDefault()
                        If record IsNot Nothing Then
                            lblError.Text = "A record matching these parameters already exists in the database"
                            lblError.Visible = True
                        Else
                            selectedRecord.start_date = startDate
                            'record.end_date = endDate
                            selectedRecord.rate = rate
                            selectedRecord.chgstamp = DateTime.Now
                            context.SaveChanges()
                            BindGrid()
                            lblError.Visible = False
                            Session("RowID") = Nothing
                            gdvConfiguration.SelectedRowStyle.Reset()
                            txtRate.Text = ""
                            txtStartDate.Text = ""
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "A system error has occurred: " + ex.Message
            lblError.Visible = True
        End Try
  End Sub

  Private Function CheckForUniqueDate(txtdate As String) As Boolean
    Dim context As New ssc1900016_retail_receivablesEntities2()
    Dim currentRecords As ObjectSet(Of web_credit_past_due_rates) = context.web_credit_past_due_rates
    Dim isPresent As Boolean = False
    Dim enteredDate As DateTime = Convert.ToDateTime(txtdate)
    Dim record = (From r In context.web_credit_past_due_rates
                  Where r.start_date = enteredDate
                  Select r).FirstOrDefault()
    If record IsNot Nothing Then
      isPresent = True
      Return isPresent
    End If
    Return isPresent
  End Function


  Private Function CheckForOverlap(startdate As DateTime, enddate As DateTime) As Boolean
    Dim isOverlap As Boolean = False
    Dim context As New ssc1900016_retail_receivablesEntities2()
    Dim currentRecords As ObjectSet(Of web_credit_past_due_rates) = context.web_credit_past_due_rates

    Dim minStartDate = (From c In currentRecords
                       Select c.start_date).Min()

    Dim maxStartDate = (From c In currentRecords
                        Select c.start_date).Max()

    Dim minEndDate = (From c In currentRecords
                      Select c.end_date).Min()

    Dim maxEndDate = (From c In currentRecords
                      Select c.end_date).Max()

    If (startdate >= minStartDate) And (startdate <= maxStartDate) Then
      isOverlap = True
      Return isOverlap
    End If

    If (enddate >= minEndDate) And (enddate <= minEndDate) Then
      isOverlap = True
      Return isOverlap
    End If
    Return isOverlap
  End Function

  Private Sub ShowOverlapMessage()
    lblError.Text = "The dates you have entered overlap with dates that already exist in the database."
    lblError.Visible = True
  End Sub

  Private Sub ShowDuplicateMessage()
    lblError.Text = "You have already entered a record with these parameters."
    lblError.Visible = True
  End Sub

  Private Sub ShowDateMessage()
    lblError.Text = "The end date must come after the start date."
    lblError.Visible = True
  End Sub

    Sub gdvConfiguration_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Dim rowID As Integer = Convert.ToInt32(gdvConfiguration.DataKeys(e.NewSelectedIndex).Value)
        Session("RowID") = rowID
        Dim retail As New ssc1900016_retail_receivablesEntities2()
        Dim parameters As ObjectSet(Of web_credit_past_due_rates) = retail.web_credit_past_due_rates
        Dim paramQuery = (From p In parameters
                         Where p.ROWID = rowID
                         Select p).SingleOrDefault()

        txtStartDate.Text = paramQuery.start_date.ToString()
        'txtEndDate.Text = paramQuery.end_date.ToString()
        txtRate.Text = paramQuery.rate.ToString()
        lblError.Visible = False
    End Sub

  
    
End Class
