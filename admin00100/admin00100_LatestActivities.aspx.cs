using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSwebModel;

public partial class admin00100_admin00100_LatestActivities : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      admin00100_reconciliation rec = new admin00100_reconciliation();
      rec.GetNewlyReconciledRecords(gdvLatestActivities);
      //rec.GetIncompleteRecords(gdvIncompleteRecords);
      rec.GetDeletedRecords(gdvDeleted);
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  protected void btnGetLatestActivities_Click(object sender, EventArgs e)
  {
    try
    {
      admin00100_reconciliation rec = new admin00100_reconciliation();
      rec.GetLatestActivities(gdvLatestActivities);
      rec.GetActivitiesDeletedFromOpenSS();
      rec.GetDeletedRecords(gdvDeleted);
      lblInformation.Text = "The latest Activity ID's from OpenStream are now in the grid below. Please click on the Select button to complete the record.";
      lblInformation.Visible = true;
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  protected void gdvLatestActivities_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
    try
    {
      gdvLatestActivities.PageIndex = e.NewPageIndex;
      admin00100_reconciliation rec = new admin00100_reconciliation();
      rec.GetNewlyReconciledRecords(gdvLatestActivities);
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  protected void gdvIncompleteRecords_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
  {
    try
    {
      string newActivityID = Convert.ToString(gdvLatestActivities.DataKeys[e.NewSelectedIndex].Value);
      Response.Redirect("~/admin00100/admin00100_details.aspx?activityID=" + newActivityID);
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  //protected void gdvIncompleteRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
  //{
  //    gdvIncompleteRecords.PageIndex = e.NewPageIndex;
  //    Reconciliation rec = new Reconciliation();
  //    rec.GetIncompleteRecords(gdvIncompleteRecords);
  //}

  protected void gdvLatestActivities_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
  {
    try
    {
      string newActivityID = Convert.ToString(gdvLatestActivities.DataKeys[e.NewSelectedIndex].Value);
      Response.Redirect("~/admin00100/admin00100_details.aspx?latestActivityID=" + newActivityID);
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  //for deleted records
  protected void gdvDeleted_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
  {
    string activityID = Convert.ToString(gdvDeleted.DataKeys[e.NewSelectedIndex].Value);
  }

  protected void gdvDeleted_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    try
    {
      string currentCommand = e.CommandName;
      int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
      string ActivityID = gdvDeleted.DataKeys[currentRowIndex].Value.ToString();
      admin00100_reconciliation rec = new admin00100_reconciliation();
      using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
      {
        web_tracker_Activity activity = context.web_tracker_Activity.Single(record => record.OldActivityID == ActivityID);
        activity.Deleted = null;
        context.SaveChanges();
      }
      rec.GetDeletedRecords(gdvDeleted);
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  protected void gdvDeleted_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
    try
    {
      gdvDeleted.PageIndex = e.NewPageIndex;
      admin00100_reconciliation rec = new admin00100_reconciliation();
      rec.GetDeletedRecords(gdvDeleted);
    }
    catch (Exception ex)
    {
      Label1.Text = "A system error has occurred." + " " + ex.Message;
      Label1.Visible = true;
    }
  }

  //protected void btnShowIncompleteRecords_Click(object sender, EventArgs e)
  //{
  //    Reconciliation rec = new Reconciliation();
  //    rec.GetIncompleteRecords(gdvIncompleteRecords);
  //}
}