Option Explicit On
Option Strict On

Partial Class ssc1900016_wait
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    Me.Title = CStr(Session("Title"))
    Me.lblTitle.Text = CStr(Session("Title"))

    If Not IsNothing(Request.Params("ID")) Then
      Dim strProcessState As String = CStr(Session(Request.Params("ID")))
      If Not IsNothing(strProcessState) Then
        If strProcessState = "Busy" Then
          Me.lblStatus.Text = "Your request is processing."
          Response.AddHeader("refresh", "3")
          Exit Sub
        ElseIf Microsoft.VisualBasic.Left(strProcessState, 7) = "TimeOut" Then
          Response.Write("The SQL Server is too busy.  Please wait 30 minutes and try again.")
          Response.End()
        ElseIf Microsoft.VisualBasic.Left(strProcessState, "Failure<br/>Status=NoRows".Length) = "Failure<br/>Status=NoRows" Then
          Me.Image1.ImageUrl = "~/images/109_AllAnnotations_Info_256.png"
          Me.lblStatus.Text = "This combination of choices did not return any rows."
          Exit Sub
        ElseIf Microsoft.VisualBasic.Left(strProcessState, 7) <> "Failure" Then
          Me.lblStatus.Text = "All done!"
          Me.Image1.ImageUrl = "~/images/109_AllAnnotations_Info_256.png"
          Me.btnGetExcel.Visible = True
          Exit Sub
        Else
          Response.Write(strProcessState)
          Response.End()
        End If
      End If
    End If
        Response.Redirect("ssc1900016_default.aspx")
  End Sub

  Protected Sub btnGetExcel_Click(sender As Object, e As System.EventArgs) Handles btnGetExcel.Click
    Me.btnGetExcel.Visible = False
    Me.lblStatus.Text = "File has already been downloaded.  Run again"
    subGetFile()
  End Sub

  Public Sub subGetFile()
    Try
      Dim strGuid As String = Guid.NewGuid.ToString
      For Each objFile As String In System.IO.Directory.EnumerateFiles(Request.PhysicalApplicationPath, "*.xlsZ")
        System.IO.File.Delete(objFile)
      Next
      System.IO.File.Copy(CStr(Session(Request.Params("ID"))), Request.PhysicalApplicationPath & "\" & strGuid & ".xlsZ", True)
      System.IO.File.Delete(CStr(Session(Request.Params("ID"))))
            Response.Redirect("ssc1900016_default.aspx?Status=DwnLd&Message=" & Request.PhysicalApplicationPath & "\" & strGuid & ".xlsZ", True)
    Catch ex As Exception
      'Bummer
    End Try
  End Sub

End Class
