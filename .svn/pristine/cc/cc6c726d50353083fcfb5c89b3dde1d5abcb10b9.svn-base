using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSwebModel;

public partial class admin00100_admin00100_details : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
    if (!IsPostBack)
    {
      admin00100_search search = new admin00100_search();
      admin00100_details details = new admin00100_details();
      //this is for when they clicked on a latest activity record
      if (Request.QueryString["latestActivityID"] != null && Request.QueryString["latestActivityID"] != "")
      {
        string oldActivityID = Request.QueryString["latestActivityID"];
        details.LoadSelectedDetails(oldActivityID, txtActivityGroupName, txtActivityName, ddlProgrammer, lblOriginalProgrammer, txtActivityID, ddlLastModifiedBy, rblCSharp, rblNET, rblVB, rblASP, rblAJAX, rblJavaScript, txtModifiedDate, HyperLink1, txtDocLink, txtNotes, lblURL);
        details.LoadSelectedDetails(oldActivityID, txtProject, rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblActivityDiscontinued, rblCodeLocation1, rblPopUpWindow, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4);
        //have all radiobuttons default to "No" for these records, except for Hathor and Classic ASP, which should be "Yes"
        rblASP.Items.FindByValue("Yes").Selected = true;
        rblCodeLocation1.Items.FindByValue("Yes").Selected = true;
        rblVB.Items.FindByValue("No").Selected = true;
        rblNET.Items.FindByValue("No").Selected = true;
        rblCSharp.Items.FindByValue("No").Selected = true;
        rblJavaScript.Items.FindByValue("No").Selected = true;
        rblAJAX.Items.FindByValue("No").Selected = true;
        rblCodeLocation2.Items.FindByValue("No").Selected = true;
        rblCodeLocation3.Items.FindByValue("No").Selected = true;
        rblCodeLocation4.Items.FindByValue("No").Selected = true;
        rblDataControls.Items.FindByValue("No").Selected = true;
        rblOSSecurityDoc.Items.FindByValue("No").Selected = true;
        rblOSSecurityGroup.Items.FindByValue("No").Selected = true;
        rblIpadEnabled.Items.FindByValue("No").Selected = true;
        rblActivityDiscontinued.Items.FindByValue("No").Selected = true;
        rblPopUpWindow.Items.FindByValue("No").Selected = true;
      }
      //for when they're coming from the Search page
      else if ((Request.QueryString["activityID"] != null && Request.QueryString["activityID"] != ""))
      {
        string oldActivityID = Request.QueryString["activityID"];
        //details.LoadSelectedDetails(oldActivityID, txtActivityGroupName, txtActivityName, ddlProgrammer, rblIpadEnabled, rblActivityDiscontinued, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4);
        details.LoadSelectedDetails(oldActivityID, txtActivityGroupName, txtActivityName, ddlProgrammer, lblOriginalProgrammer, txtActivityID, ddlLastModifiedBy, rblCSharp, rblNET, rblVB, rblASP, rblAJAX, rblJavaScript, txtModifiedDate, HyperLink1, txtDocLink, txtNotes, lblURL);
        details.LoadSelectedDetails(oldActivityID, txtProject, rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblActivityDiscontinued, rblCodeLocation1, rblPopUpWindow, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4);
      }
      else
      {
        EnableDetailControls();
      }
    }
  }

  private void DisableDetailControls()
  {
    ddlProgrammer.Visible = false;
    txtActivityGroupName.Enabled = false;
    txtActivityName.Enabled = false;
    txtActivityID.Enabled = false;
    rblVB.Enabled = false;
    rblCSharp.Enabled = false;
    rblNET.Enabled = false;
    rblJavaScript.Enabled = false;
    rblASP.Enabled = false;
    rblAJAX.Enabled = false;
    rblCodeLocation1.Enabled = false;
    rblCodeLocation2.Enabled = false;
    rblCodeLocation3.Enabled = false;
    rblCodeLocation4.Enabled = false;
    ddlLastModifiedBy.Enabled = false;
    rblDataControls.Enabled = false;
    rblOSSecurityDoc.Enabled = false;
    rblOSSecurityGroup.Enabled = false;
    rblIpadEnabled.Enabled = false;
    rblActivityDiscontinued.Enabled = false;
    txtModifiedDate.Enabled = false;
    txtProject.Enabled = false;
    btnSave.Enabled = false;
    rblPopUpWindow.Enabled = false;
    txtDocLink.Enabled = false;
    txtDocLink.Visible = false;
    txtNotes.Enabled = false;
  }

  private void EnableDetailControls()
  {
    //4/8/13- the user should never have the ability to edit the Activity Name, Activity Group Name, or Activity ID
    //ddlActivityGroupName.Visible = true;
    //ddlActivityName.Visible = true;
    //ddlProgrammer.Visible = true;
    //txtActivityGroupName.Visible = false;
    //txtActivityName.Visible = false;
    //txtActivityID.Enabled = true;
    rblVB.Enabled = true;
    rblCSharp.Enabled = true;
    rblNET.Enabled = true;
    rblJavaScript.Enabled = true;
    rblASP.Enabled = true;
    rblAJAX.Enabled = true;
    rblCodeLocation1.Enabled = true;
    rblCodeLocation2.Enabled = true;
    rblCodeLocation3.Enabled = true;
    rblCodeLocation4.Enabled = true;
    ddlLastModifiedBy.Enabled = true;
    rblDataControls.Enabled = true;
    rblOSSecurityDoc.Enabled = true;
    rblOSSecurityGroup.Enabled = true;
    rblIpadEnabled.Enabled = true;
    rblActivityDiscontinued.Enabled = true;
    btnSave.Enabled = true;
    txtModifiedDate.Enabled = true;
    txtProject.Enabled = true;
    rblPopUpWindow.Enabled = true;
    txtDocLink.Enabled = true;
    txtDocLink.Visible = true;
    txtNotes.Enabled = true;
    //lblProgrammer.Visible = false;
    //lblLastModifiedBy.Visible = false;
    //ddlProgrammer.Visible = true;
    //ddlLastModifiedBy.Visible = true;
  }

  protected void btnEdit_Click(object sender, EventArgs e)
  {
    EnableDetailControls();
    btnCancelEdit.Enabled = true;
  }


  protected void btnSave_Click(object sender, EventArgs e)
  {
    try
    {
      admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities();
      admin00100_details newActivity = new admin00100_details();
      string activityID = txtActivityID.Text;
      //for when the page loads after they open a search result record
      if (Request.QueryString["activityID"] != null && Request.QueryString["activityID"] != "")
      {
        string oldActivityID = Request.QueryString["activityID"];
        if (txtProject.Text != "" && txtActivityID.Text != "" && ddlLastModifiedBy.SelectedValue != "Please Select" && txtModifiedDate.Text != "" && rblVB.SelectedItem != null && rblCSharp.SelectedItem != null && rblASP.SelectedItem != null && rblJavaScript.SelectedItem != null && rblAJAX.SelectedItem != null && rblNET.SelectedItem != null && rblDataControls.SelectedItem != null && rblOSSecurityGroup.SelectedItem != null && rblOSSecurityDoc.SelectedItem != null && rblIpadEnabled.SelectedItem != null && rblActivityDiscontinued.SelectedItem != null && rblCodeLocation1.SelectedItem != null && rblCodeLocation2.SelectedItem != null && rblCodeLocation3.SelectedItem != null && rblCodeLocation4.SelectedItem != null && rblPopUpWindow.SelectedItem != null)
        {
          DateTime modifiedDate = Convert.ToDateTime(txtModifiedDate.Text);
          //now create an entry in ActivityModHistory table- first get new ActivityID
          var newActivityID = (from a in tracker.web_tracker_Activity
                               where a.OldActivityID == oldActivityID
                               //orderby a.ActivityID descending
                               select a).Single();

          bool doesRecordHaveOriginalProgrammer = newActivity.CheckIfOriginalProgrammerHasValidValue(oldActivityID);
          if (doesRecordHaveOriginalProgrammer == false)
          {
              if (ddlProgrammer.SelectedValue != "Please Select")
              {
                  newActivity.UpdateNewActivity(oldActivityID, ddlProgrammer, ddlLastModifiedBy, rblVB, rblCSharp, rblASP, rblJavaScript, rblAJAX, rblNET, rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4, rblActivityDiscontinued, rblPopUpWindow, txtProject, txtDocLink, txtNotes);
                  newActivity.UpdateLink(oldActivityID, txtDocLink, HyperLink1, lblURL);
                  newActivity.AddNewActivityHistory(Convert.ToInt32(newActivityID.ActivityID), oldActivityID, Convert.ToString(newActivityID.ActivityName), ddlLastModifiedBy.SelectedValue, modifiedDate);
                  newActivity.SetRadioButtonToNormalFont(rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblActivityDiscontinued, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4, ddlProgrammer, rblCSharp, rblNET, rblVB, rblASP, rblAJAX, rblJavaScript, rblPopUpWindow, txtProject, txtModifiedDate, ddlLastModifiedBy);
                  lblErrorDetails.Text = "Record successfully updated.";
                  lblErrorDetails.Visible = true;
              }
              else
              {
                  lblErrorDetails.Text = "Please select an original programmer.";
                  lblErrorDetails.Visible = true;
              }
          }
          else
          {
              newActivity.UpdateActivity(oldActivityID, ddlLastModifiedBy, rblVB, rblCSharp, rblASP, rblJavaScript, rblAJAX, rblNET, rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4, rblActivityDiscontinued, rblPopUpWindow, txtProject, txtDocLink, txtNotes);
              newActivity.UpdateLink(oldActivityID, txtDocLink, HyperLink1, lblURL);
              newActivity.AddNewActivityHistory(Convert.ToInt32(newActivityID.ActivityID), oldActivityID, Convert.ToString(newActivityID.ActivityName), ddlLastModifiedBy.SelectedValue, modifiedDate);
              newActivity.SetRadioButtonToNormalFont(rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblActivityDiscontinued, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4, ddlProgrammer, rblCSharp, rblNET, rblVB, rblASP, rblAJAX, rblJavaScript, rblPopUpWindow, txtProject, txtModifiedDate, ddlLastModifiedBy);
              lblErrorDetails.Text = "Record successfully updated.";
              lblErrorDetails.Visible = true;
          }
        }
        else
        {
          lblErrorDetails.Text = "All fields on this page are required.";
          lblErrorDetails.Visible = true;
        }
      }
      //**this executes when they are saving changes to a Latest Activity record
      if (Request.QueryString["latestActivityID"] != null && Request.QueryString["latestActivityID"] != "")
      {
        btnSave.Enabled = true;
        string oldActivityID = Request.QueryString["latestActivityID"];
        string test = ddlProgrammer.SelectedValue;
        string test1 = ddlLastModifiedBy.SelectedValue;
        string test2 = txtModifiedDate.Text;
        //Update method here
        if (ddlLastModifiedBy.SelectedValue != "Please Select" && ddlProgrammer.SelectedValue != "Please Select" && txtModifiedDate.Text != "")
        {

            //need to handle two situations- if there is already a valid programmer name in the Original Programmer field,
            //then allow updating of that field. If there isn't then it need to be updateable.
            newActivity.UpdateNewActivity(oldActivityID, ddlProgrammer, ddlLastModifiedBy, rblVB, rblCSharp, rblASP, rblJavaScript, rblAJAX, rblNET, rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4, rblActivityDiscontinued, rblPopUpWindow, txtProject, txtDocLink, txtNotes);
            newActivity.UpdateLink(oldActivityID, txtDocLink, HyperLink1, lblURL);
            DateTime modifiedDate = Convert.ToDateTime(txtModifiedDate.Text);
            //HyperLink1.NavigateUrl = "file://" + 
            //now create an entry in ActivityModHistory table- first get new ActivityID
            var newActivityID = (from a in tracker.web_tracker_Activity
                                 where a.OldActivityID == oldActivityID
                                 select a).Single();

            newActivity.AddNewActivityHistory(Convert.ToInt32(newActivityID.ActivityID), oldActivityID, Convert.ToString(newActivityID.ActivityName), ddlLastModifiedBy.SelectedValue, modifiedDate);
            newActivity.SetRadioButtonToNormalFont(rblDataControls, rblOSSecurityGroup, rblOSSecurityDoc, rblIpadEnabled, rblActivityDiscontinued, rblCodeLocation1, rblCodeLocation2, rblCodeLocation3, rblCodeLocation4, ddlProgrammer, rblCSharp, rblNET, rblVB, rblASP, rblAJAX, rblJavaScript, rblPopUpWindow, txtProject, txtModifiedDate, ddlLastModifiedBy);
            HyperLink1.Text = txtDocLink.Text;
            lblErrorDetails.Text = "Record successfully updated.";
            lblErrorDetails.Visible = true;
        }
        else
        {
            lblErrorDetails.Text = "All fields on this page are required.";
            lblErrorDetails.Visible = true;
        }
      }
      //if (ddlLastModifiedBy.SelectedValue == "Please Select" || ddlProgrammer.SelectedValue == "Please Select" || txtModifiedDate.Text == String.Empty)
      //{
      //  lblErrorDetails.Text = "All fields on this page are required.";
      //  lblErrorDetails.Visible = true;
      //}
    }
    catch (Exception ex)
    {
        lblErrorDetails.Text = "Unable to save, a system error has occurred:" + " " + ex.Message + ex.InnerException;
      lblErrorDetails.Visible = true;
    }
  }

  protected void btnCancelEdit_Click(object sender, EventArgs e)
  {
      DisableDetailControls();
  }

}