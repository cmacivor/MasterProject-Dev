using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ATSwebModel;

/// <summary>
/// Summary description for admin00100_details
/// </summary>
public class admin00100_details
{
  public void LoadSelectedDetails(string activityID, TextBox project, RadioButtonList dataControls, RadioButtonList osSecurityGroup, RadioButtonList osSecurityDoc, RadioButtonList ipad, RadioButtonList discontinued, RadioButtonList hathor, RadioButtonList popup, RadioButtonList hebe, RadioButtonList hebe2, RadioButtonList hebe4)
  {
    using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
    {
      var selectedActivity = (from t in tracker.web_tracker_Activity
                              where t.OldActivityID == activityID
                              select t).FirstOrDefault();
      if (selectedActivity != null)
      {
          if (selectedActivity.Project != null)
          {
              project.Text = selectedActivity.Project;
              project.Enabled = false;
          }
          else
          {
              project.BackColor = System.Drawing.Color.Red;
              project.ForeColor = System.Drawing.Color.White;
          }
          if (selectedActivity.ContainDataControls != null)
          {
              LoadRadioButton(selectedActivity.ContainDataControls, dataControls);
          }
          else
          {
              ShowUnpopulatedRadioButton(dataControls);
          }
          if (selectedActivity.OSSecurityGroupAvailable != null)
          {
              LoadRadioButton(selectedActivity.OSSecurityGroupAvailable, osSecurityGroup);
          }
          else
          {
              ShowUnpopulatedRadioButton(osSecurityGroup);
          }
          if (selectedActivity.OSSecurityDoc != null)
          {
              LoadRadioButton(selectedActivity.OSSecurityDoc, osSecurityDoc);
          }
          else
          {
              ShowUnpopulatedRadioButton(osSecurityDoc);
          }
          if (selectedActivity.IPadEnabled != null)
          {
              LoadRadioButton(selectedActivity.IPadEnabled, ipad);
          }
          else
          {
              ShowUnpopulatedRadioButton(ipad);
          }
          if (selectedActivity.ActivityDiscontinued != null)
          {
              LoadRadioButton(selectedActivity.ActivityDiscontinued, discontinued);
          }
          else
          {
              ShowUnpopulatedRadioButton(discontinued);
          }
          if (selectedActivity.PopUpWindow != null)
          {
              LoadRadioButton(selectedActivity.PopUpWindow, popup);
          }
          else
          {
              ShowUnpopulatedRadioButton(popup);
          }
          if (selectedActivity.HathorWebOpenssActivities != null)
          {
              LoadRadioButton(selectedActivity.HathorWebOpenssActivities, hathor);
          }
          else
          {
              ShowUnpopulatedRadioButton(hathor);
          }
          if (selectedActivity.HebeOpenssapps != null)
          {
              LoadRadioButton(selectedActivity.HebeOpenssapps, hebe);
          }
          else
          {
              ShowUnpopulatedRadioButton(hebe);
          }
          if (selectedActivity.HebeOpenssapps2 != null)
          {
              LoadRadioButton(selectedActivity.HebeOpenssapps2, hebe2);
          }
          else
          {
              ShowUnpopulatedRadioButton(hebe2);
          }
          if (selectedActivity.HebeOpenssapps4 != null)
          {
              LoadRadioButton(selectedActivity.HebeOpenssapps4, hebe4);
          }
          else
          {
              ShowUnpopulatedRadioButton(hebe4);
          }
      }
    }
  }


  private void LoadRadioButton(string result, RadioButtonList radiobuttons)
  {
      if (result.Any() || result != null)
      {
          if (result == "Yes")
          {
              radiobuttons.Items.FindByValue("Yes").Selected = true;
              radiobuttons.Font.Bold = true;
          }
          else if (result == "No")
          {
              radiobuttons.Items.FindByValue("No").Selected = true;
              radiobuttons.Font.Bold = true;
          }
          else
          {
              radiobuttons.Enabled = true;
              radiobuttons.Font.Bold = true;
              radiobuttons.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
          }
      }
      if (result.Equals(String.Empty) || result == null)
      {
          ShowUnpopulatedRadioButton(radiobuttons);
      }
  }

  private void ShowUnpopulatedRadioButton(RadioButtonList radiobuttons)
  {
      radiobuttons.Enabled = true;
      radiobuttons.Font.Bold = true;
      radiobuttons.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
  }

  public void SetRadioButtonToNormalFont(RadioButtonList dataControls, RadioButtonList osSecurityGroup, RadioButtonList osSecurityDoc, RadioButtonList ipad, RadioButtonList discontinued, RadioButtonList hathor, RadioButtonList hebe, RadioButtonList hebe2, RadioButtonList hebe4, DropDownList originalprogrammer, RadioButtonList csharp, RadioButtonList net, RadioButtonList vb, RadioButtonList asp, RadioButtonList ajax, RadioButtonList javascript, RadioButtonList popup, TextBox project, TextBox lastmodifieddate, DropDownList lastmodifiedby)
  {
      //dataControls.Font.Bold = false;
      dataControls.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //osSecurityGroup.Font.Bold = false;
      osSecurityGroup.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //osSecurityDoc.Font.Bold = false;
      osSecurityDoc.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //ipad.Font.Bold = false;
      ipad.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //discontinued.Font.Bold = false;
      discontinued.ForeColor =  System.Drawing.ColorTranslator.FromHtml("#000000");
      //hathor.Font.Bold = false;
      hathor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //hebe.Font.Bold = false;
      hebe.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //hebe2.Font.Bold = false;
      hebe2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //hebe4.Font.Bold = false;
      hebe4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //originalprogrammer.Font.Bold = false;
      originalprogrammer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //csharp.Font.Bold = false;
      csharp.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //net.Font.Bold = false;
      net.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //vb.Font.Bold = false;
      vb.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //ajax.Font.Bold = false;
      ajax.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //javascript.Font.Bold = false;
      javascript.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      asp.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      //popup.Font.Bold = false;
      popup.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
      project.BackColor = System.Drawing.Color.White;
      project.ForeColor = System.Drawing.Color.Black;
      lastmodifieddate.BackColor = System.Drawing.Color.White;
      lastmodifieddate.ForeColor = System.Drawing.Color.Black;
      lastmodifiedby.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
  }

  private void LoadOriginalProgrammer(string activityID, DropDownList ddlProgrammer, Label lblProgrammer)
  {
      using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
      {
          var selectedActivity = (from t in tracker.web_tracker_Activity
                                  where t.OldActivityID == activityID
                                  select t).FirstOrDefault();
          if (selectedActivity != null)
          {
              if (selectedActivity.OriginalProgrammer != null)
              {
                  lblProgrammer.Text = selectedActivity.OriginalProgrammer.ToString();
                  lblProgrammer.Visible = true;
                  ddlProgrammer.Visible = false;
              }
              if (selectedActivity.OriginalProgrammer == null || selectedActivity.OriginalProgrammer.Equals(String.Empty) || selectedActivity.OriginalProgrammer == "")
              {
                  ddlProgrammer.Visible = true;
                  ddlProgrammer.Enabled = true;
                  ddlProgrammer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                  ddlProgrammer.SelectedValue = "Please Select";
                  lblProgrammer.Visible = false;
              }
          }
      }
  }

  private void LoadLastModifiedByDropDown(string activityID, DropDownList ddlLastModified)
  {
      using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
      {
          var selectedActivity = (from t in tracker.web_tracker_Activity
                                  where t.OldActivityID == activityID
                                  select t).FirstOrDefault();

          var programmers = from t in tracker.web_tracker_Programmer
                            where t.Name == selectedActivity.LastModifiedBy
                            select t;
          if (programmers.Any())
          {
              ddlLastModified.SelectedValue = selectedActivity.LastModifiedBy.ToString();
              ddlLastModified.Enabled = false;
          }
          else
          {
              ddlLastModified.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
          }
      }
  }

  public void LoadSelectedDetails(string activityID, TextBox txtACG, TextBox txtAN, DropDownList originalprogrammer, Label lblOriginalProgrammer, TextBox oldactivityID, DropDownList lastModifiedby, RadioButtonList csharp, RadioButtonList net, RadioButtonList vb, RadioButtonList asp, RadioButtonList ajax, RadioButtonList javascript, TextBox lastmodifieddate, HyperLink doclink, TextBox txtlink, TextBox notes, Label linklabel)
  {
    using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
    {
      var selectedActivity = (from t in tracker.web_tracker_Activity
                              where t.OldActivityID == activityID
                              select t).FirstOrDefault();

      var selectedActivityModHistory = (from t in tracker.web_tracker_ActivityModHistory
                                        where t.ActivityID == selectedActivity.ActivityID
                                        orderby t.Date descending
                                        select t).FirstOrDefault();
      DetermineLinkStatus(activityID, txtlink, doclink, linklabel);
        if (selectedActivity.Notes != null)
        {
            notes.Text = selectedActivity.Notes.ToString();
            notes.Visible = true;
        }
        if (selectedActivity.ActivityGroupName != null)
        {
            txtACG.Text = selectedActivity.ActivityGroupName.ToString();
        }
        else
        {
            txtACG.Text = "None";
        }
        LoadOriginalProgrammer(activityID, originalprogrammer, lblOriginalProgrammer);
        if (selectedActivity.ActivityName != null)
        {
            txtAN.Text = selectedActivity.ActivityName.ToString();
        }
        else
        {
            txtAN.Text = "None";
        }
        if (selectedActivity.OldActivityID != null)
        {
            oldactivityID.Text = selectedActivity.OldActivityID.ToString();
        }
        else
        {
            oldactivityID.Text = "None";
        }
        if (selectedActivityModHistory != null)
        {
            string dateTime = selectedActivityModHistory.Date.ToString();
            string[] lastModifiedByDate = dateTime.Split(' ');
            lastmodifieddate.Text = lastModifiedByDate.ElementAt(0).ToString();
            lastmodifieddate.Enabled = false;
        }
        else
        {
            lastmodifieddate.BackColor = System.Drawing.Color.Red;
            lastmodifieddate.ForeColor = System.Drawing.Color.White;
        }
        LoadLastModifiedByDropDown(activityID, lastModifiedby);

        if (selectedActivity.C_ != null)
        {
            LoadRadioButton(selectedActivity.C_, csharp);
        }
        else
        {
            ShowUnpopulatedRadioButton(csharp);
        }
        if (selectedActivity.C_NET != null)
        {
            LoadRadioButton(selectedActivity.C_NET, net);
        }
        else
        {
            ShowUnpopulatedRadioButton(net);
        }
        if (selectedActivity.VB != null)
        {
            LoadRadioButton(selectedActivity.VB, vb);
        }
        else
        {
            ShowUnpopulatedRadioButton(vb);
        }
        if (selectedActivity.ASP != null)
        {
            LoadRadioButton(selectedActivity.ASP, asp);
        }
        else
        {
            ShowUnpopulatedRadioButton(asp);
        }
        if (selectedActivity.AJAX != null)
        {
            LoadRadioButton(selectedActivity.AJAX, ajax);
        }
        else
        {
            ShowUnpopulatedRadioButton(ajax);
        }
        if (selectedActivity.Javascript != null)
        {
            LoadRadioButton(selectedActivity.Javascript, javascript);
        }
        else
        {
            ShowUnpopulatedRadioButton(javascript);
        }
    }
  }

  public void UpdateLink(string oldActivityID, TextBox txtlink, HyperLink link, Label labellink)
  {
      using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
      {
          
         
          //string docsFolder = "\\" + "\\ceoii\\Finance\\Craig\\";
          string fullLocationPath = String.Empty;
          web_tracker_Activity activity = context.web_tracker_Activity.Single(record => record.OldActivityID == oldActivityID);
          if (txtlink.Text != "" || String.IsNullOrWhiteSpace(txtlink.Text))
          {

              //string docsFolder = "file://///ceoii/Finance/Craig/";

              //string directory = "\\\\ceoii\\Finance\\Craig\\";
              //string str = @"<a href= ""file:///";
              //str += directory;
              //str += txtlink.Text;
              //str += "\"";
              //str += ">";
              //str += directory;
              //str += txtlink.Text;
              //str += "</a>";

              activity.DocumentationLink = txtlink.Text;
              context.SaveChanges();

              //fullLocationPath = docsFolder + txtlink.Text;
              //activity.DocumentationLink = fullLocationPath;
              //context.SaveChanges();
              DetermineLinkStatus(oldActivityID, txtlink, link, labellink);
          }
      }
  }

  public void DetermineLinkStatus(string oldActivityID, TextBox txtlink, HyperLink link, Label lbllink)
  {
      
      using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
      {
          web_tracker_Activity activity = context.web_tracker_Activity.Single(record => record.OldActivityID == oldActivityID);
          if (activity.DocumentationLink == null || String.IsNullOrWhiteSpace(activity.DocumentationLink))
          {
              txtlink.Visible = true;
              link.Visible = false;
              lbllink.Visible = false;
          }
          else
          {
              //string directory = "\\\\ceoii\\Finance\\Craig\\";
              //string str = @"<a href= ""file:///";
              //str += directory;
              //str += activity.DocumentationLink.ToString();
               string doublequote = "\"";
              //str += ">";
              //str += directory;
              //str += activity.DocumentationLink.ToString();
              //str += "</a>";

               string str1 = "file:///\\\\ceoii\\iusers\\sysprog\\CorpAppsTeam\\Documentation\\OpenStreamSecurityDoc\\" + activity.DocumentationLink.ToString();
              lbllink.Text = "<a href = " + doublequote + str1 + doublequote + ">" + "Link to document" + "</a>";

              lbllink.Visible = true;  

              txtlink.Visible = false;
              //link.NavigateUrl = activity.DocumentationLink.ToString();
              //link.NavigateUrl = str;
              //link.Visible = true;
              //lbllink.Text = activity.DocumentationLink.ToString();
              //lbllink.Text = str;
          }
      }
  }

  public void UpdateActivity(string oldActivityID, DropDownList lastmod, RadioButtonList vb, RadioButtonList c, RadioButtonList asp, RadioButtonList javascript, RadioButtonList ajax, RadioButtonList net, RadioButtonList data, RadioButtonList ossec, RadioButtonList secdoc, RadioButtonList ipad, RadioButtonList hathor, RadioButtonList hebe, RadioButtonList hebe2, RadioButtonList hebe4, RadioButtonList disc, RadioButtonList popup, TextBox project, TextBox link, TextBox notes)
  {
    using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
    {
      web_tracker_Activity activity = context.web_tracker_Activity.Single(record => record.OldActivityID == oldActivityID);
      activity.LastModifiedBy = lastmod.SelectedValue;
      activity.VB = vb.SelectedValue;
      activity.C_ = c.SelectedValue;
      activity.ASP = asp.SelectedValue;
      activity.Javascript = javascript.SelectedValue;
      activity.AJAX = ajax.SelectedValue;
      activity.C_NET = net.SelectedValue;
      activity.ContainDataControls = data.SelectedValue;
      activity.OSSecurityGroupAvailable = ossec.SelectedValue;
      activity.OSSecurityDoc = secdoc.SelectedValue;
      activity.IPadEnabled = ipad.SelectedValue;
      activity.HathorWebOpenssActivities = hathor.SelectedValue;
      activity.HebeOpenssapps = hebe.SelectedValue;
      activity.HebeOpenssapps2 = hebe2.SelectedValue;
      activity.HebeOpenssapps4 = hebe4.SelectedValue;
      activity.ActivityDiscontinued = disc.SelectedValue;
      activity.PopUpWindow = popup.SelectedValue;
      activity.Project = project.Text;
      //activity.DocumentationLink = link.Text;
      activity.Notes = notes.Text;
      context.SaveChanges();
    }
  }

  public bool CheckIfOriginalProgrammerHasValidValue(string oldactivityID)
  {
      admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
      var Record = (from i in context.web_tracker_Activity
                                     where i.OldActivityID == oldactivityID
                                     select i).SingleOrDefault();

      var Programmers = from p in context.web_tracker_Programmer
                        where p.Name == Record.OriginalProgrammer
                        select p;
      if (Programmers.Any())
      {
          return true;
      }
      return false;
  }

  public void UpdateNewActivity(string activityID, DropDownList originalProgrammer, DropDownList lastmod, RadioButtonList vb, RadioButtonList c, RadioButtonList asp, RadioButtonList javascript, RadioButtonList ajax, RadioButtonList net, RadioButtonList data, RadioButtonList ossec, RadioButtonList secdoc, RadioButtonList ipad, RadioButtonList hathor, RadioButtonList hebe, RadioButtonList hebe2, RadioButtonList hebe4, RadioButtonList disc, RadioButtonList popup, TextBox project, TextBox link, TextBox notes)
  {
      using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
      {
          web_tracker_Activity activity = context.web_tracker_Activity.Single(record => record.OldActivityID == activityID);
          activity.OriginalProgrammer = originalProgrammer.SelectedValue;
          activity.LastModifiedBy = lastmod.SelectedValue;
          activity.VB = vb.SelectedValue;
          activity.C_ = c.SelectedValue;
          activity.ASP = asp.SelectedValue;
          activity.Javascript = javascript.SelectedValue;
          activity.AJAX = ajax.SelectedValue;
          activity.C_NET = net.SelectedValue;
          activity.ContainDataControls = data.SelectedValue;
          activity.OSSecurityGroupAvailable = ossec.SelectedValue;
          activity.OSSecurityDoc = secdoc.SelectedValue;
          activity.IPadEnabled = ipad.SelectedValue;
          activity.HathorWebOpenssActivities = hathor.SelectedValue;
          activity.HebeOpenssapps = hebe.SelectedValue;
          activity.HebeOpenssapps2 = hebe2.SelectedValue;
          activity.HebeOpenssapps4 = hebe4.SelectedValue;
          activity.ActivityDiscontinued = disc.SelectedValue;
          activity.PopUpWindow = popup.SelectedValue;
          activity.Project = project.Text;
          //activity.DocumentationLink = link.Text;
          activity.Notes = notes.Text;
          context.SaveChanges();
      }
  }


  public void AddNewActivityHistory(int newActivityID, string oldActivityID, string activityName, string lastModified, DateTime date)
  {
    using (admin00100_TrackerDBEntities newcontext = new admin00100_TrackerDBEntities())
    {
      web_tracker_ActivityModHistory history = new web_tracker_ActivityModHistory
      {
        ActivityID = newActivityID,
        OldActivityID = oldActivityID,
        ActivityName = activityName,
        LastModifiedBy = lastModified,
        Date = date
      };
      newcontext.AddToweb_tracker_ActivityModHistory(history);
      newcontext.SaveChanges();
    }
  }

  public void AddNewActivity(DropDownList programmer, TextBox id, DropDownList lastmod, RadioButtonList vb, RadioButtonList c, RadioButtonList asp, RadioButtonList javascript, RadioButtonList ajax, RadioButtonList net, TextBox other, RadioButtonList data, RadioButtonList ossec, RadioButtonList secdoc, RadioButtonList ipad, RadioButtonList hathor, RadioButtonList hebe, RadioButtonList hebe2, RadioButtonList hebe4, RadioButtonList disc)
  {
    using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
    {
      web_tracker_Activity activity = new web_tracker_Activity
      {
        //ActivityGroupName = acg.Text,
        //ActivityName = an.Text,
        OriginalProgrammer = programmer.SelectedValue,
        OldActivityID = id.Text,
        LastModifiedBy = lastmod.SelectedValue,
        VB = vb.SelectedValue,
        C_ = c.SelectedValue,
        ASP = asp.SelectedValue,
        Javascript = javascript.SelectedValue,
        AJAX = ajax.SelectedValue,
        C_NET = net.SelectedValue,
        Other = other.Text,
        ContainDataControls = data.SelectedValue,
        OSSecurityGroupAvailable = ossec.SelectedValue,
        OSSecurityDoc = secdoc.SelectedValue,
        IPadEnabled = ipad.SelectedValue,
        HathorWebOpenssActivities = hathor.SelectedValue,
        HebeOpenssapps = hebe.SelectedValue,
        HebeOpenssapps2 = hebe2.SelectedValue,
        HebeOpenssapps4 = hebe4.SelectedValue,
        ActivityDiscontinued = disc.SelectedValue
      };

      context.AddToweb_tracker_Activity(activity);
      context.SaveChanges();
    }
  }

}