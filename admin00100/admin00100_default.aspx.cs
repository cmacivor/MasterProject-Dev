using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using ATSwebModel;


public partial class admin00100_admin00100_default : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
        //lbActivityGroupName.Attributes.Add("Disabled", "true");
        //lboxAN.Attributes.Add("Disabled", "true");
        //lbActivityGroupName.Enabled = false;
        //lboxAN.Enabled = false;
        //admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
        
    }
  }

  protected void btnSearch_Click(object sender, EventArgs e)
  {
   //try
   //{
    if (IsPostBack)
    {
       admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities();
       admin00100_search search = new admin00100_search();
      if (rblSearchChoices.SelectedValue == "Programmer")
      {
          if (ddlProgrammer.SelectedValue != "Please Select")
          {
              var items = search.SearchByProgrammer(ddlProgrammer, lblError);
              SearchResults(items, gdvSearchResults, lblSearchResults);
          }
          else
          {
              lblError.Text = "Please choose a programmer.";
              lblError.Visible = true;
          }
      }
      if (rblSearchChoices.SelectedValue == "Activity")
      {
          int selectedCount = lboxAN.Items.Cast<ListItem>().Count(s => s.Selected);
          List<ListItem> selectedItems = lboxAN.GetSelectedItems();
          if (lbActivityGroupName.SelectedItem == null)
          {
              lblError.Text = "No items have been selected.";
              lblError.Visible = true;
          }
          else if (selectedCount > 3)
          {
              lblError.Text = "You may choose between one and three Activity Names. Click 'All Activities' to get all Activity Names associated with the selected Activity Group Name.";
              lblError.Visible = true;
          }
          else
          {
              string test = lboxAN.SelectedValue;
              var items = search.SearchByANandACG(lbActivityGroupName, lboxAN, gdvSearchResults, lblError);
              SearchResults(items, gdvSearchResults, lblSearchResults);
          }
      }
      if (rblSearchChoices.SelectedValue == "Language")
      {
          //string selected = rblLanguages.Items.Cast<ListItem>().SingleOrDefault(li => li.Selected).ToString();
        //search.SearchByLanguage(CheckBoxList1, gdvSearchResults, lblError);
          if (rblLanguages.HasSelectedValue())
          {
            var items = search.SearchByLanguageRadioButton(rblLanguages, lblError);
            SearchResults(items, gdvSearchResults, lblSearchResults);
          }
          else
          {
              lblError.Text = "Please select a language.";
              lblError.Visible = true;
          }
      }
      if (rblSearchChoices.SelectedValue == "IPad Enabled")
      {
          var items = search.SearchByIpadEnabled(gdvSearchResults, lblError);
          SearchResults(items, gdvSearchResults, lblSearchResults);
      }
      if (rblSearchChoices.SelectedValue == "Activity Description")
      {
          if (txtActivityDescription.Text != "")
          {
            var items = search.SearchByActivityDescription(txtActivityDescription.Text);
            SearchResults(items, gdvSearchResults, lblSearchResults);
          }
          else
          {
              lblError.Text = "Please enter an Activity Description.";
              lblError.Visible = true;
          }
      }
    }
  }

  private void SearchResults(IEnumerable<web_tracker_Activity> results, GridView grid, Label error)
  {
      admin00100_search search = new admin00100_search();
      if (results.Any())
            {
                search.BindDataGrid(gdvSearchResults, results, lblError);
                btnExport.Visible = true;
                error.Visible = false;
                gdvSearchResults.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                error.Text = "No results found";
                gdvSearchResults.Visible = false;
                error.Visible = true;
            }
    }
  

  protected void gdvSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
    try
    {
        admin00100_search refresh = new admin00100_search();
      if (rblSearchChoices.SelectedValue == "Programmer")
      {
        gdvSearchResults.PageIndex = e.NewPageIndex;
        gdvSearchResults.DataBind();
        //refresh.RefreshSearchResultsGrid(gdvSearchResults);
        //refresh.SearchByActivityGroupNameAndProgrammer(ddlActivityGroupName, ddlProgrammer, gdvSearchResults, lblError);
        var items = refresh.SearchByProgrammer(ddlProgrammer, lblError);
        SearchResults(items, gdvSearchResults, lblError);
      }
      if (rblSearchChoices.SelectedValue == "Language")
      {
        gdvSearchResults.PageIndex = e.NewPageIndex;
        gdvSearchResults.DataBind();
        //refresh.SearchByLanguage(CheckBoxList1, gdvSearchResults, lblError);
        var items = refresh.SearchByLanguageRadioButton(rblLanguages, lblError);
        SearchResults(items, gdvSearchResults, lblError);
      }
      if (rblSearchChoices.SelectedValue == "Activity")
      {
        gdvSearchResults.PageIndex = e.NewPageIndex;
        gdvSearchResults.DataBind();
        var items = refresh.SearchByANandACG(lbActivityGroupName, lboxAN, gdvSearchResults, lblError);
        SearchResults(items, gdvSearchResults, lblError);
      }
      if (rblSearchChoices.SelectedValue == "IPad Enabled")
      {
          gdvSearchResults.PageIndex = e.NewPageIndex;
          gdvSearchResults.DataBind();
          var items = refresh.SearchByIpadEnabled(gdvSearchResults, lblError);
          SearchResults(items, gdvSearchResults, lblError);
      }
      if (rblSearchChoices.SelectedValue == "Activity Description")
      {
          gdvSearchResults.PageIndex = e.NewPageIndex;
          gdvSearchResults.DataBind();
          var items = refresh.SearchByActivityDescription(txtActivityDescription.Text);
          SearchResults(items, gdvSearchResults, lblError);
      }
    }
    catch (Exception ex)
    {
      lblError.Text = "A system error has occurred:" + " " + ex.Message;
      lblError.Visible = true;
    }
  }

  protected void gdvSearchResults_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
  {
    try
    {
      string newActivityID = Convert.ToString(gdvSearchResults.DataKeys[e.NewSelectedIndex].Value);
      Response.Redirect("~/admin00100/admin00100_details.aspx?activityID=" + newActivityID);
    }
    catch (Exception ex)
    {
      lblError.Text = "A system error has occurred:" + " " + ex.Message;
      lblError.Visible = true;
    }
  }

  protected void rblSearchChoices_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      admin00100_search search = new admin00100_search();
      if (rblSearchChoices.SelectedValue == "Activity")
      {
        //lbActivityGroupName.Attributes.Remove("Disabled");
        //lboxAN.Attributes.Remove("Disabled");
        //lbActivityGroupName.Enabled = true;
        //lbActivityGroupName.Visible = true;
        //lboxAN.Visible = true;
        //lboxAN.Enabled = true;
        //ddlProgrammer.Enabled = false;
        ////CheckBoxList1.Enabled = false;
        //rblLanguages.Enabled = false;
         tblLanguages.Visible = false;
         tblActivityDescription.Visible = false;
         tblProgrammer.Visible = false;
         tblActivities.Visible = true;
         gdvSearchResults.Visible = false;
        lblActivityGroupName.Visible = true;
        lblActivityName.Visible = true;
        lblError.Visible = false;
        btnExport.Visible = false;
        lblSearchResults.Visible = false;
      }
      if (rblSearchChoices.SelectedValue == "Programmer")
      {
        //lbActivityGroupName.Attributes.Add("Disabled", "true");
        //lboxAN.Attributes.Add("Disabled", "true");
        //lbActivityGroupName.Enabled = false;
        //lboxAN.Enabled = false;
        //rblLanguages.Enabled = false;
        ////CheckBoxList1.Enabled = false;
        //ddlProgrammer.Enabled = true;
          tblProgrammer.Visible = true;
          tblActivities.Visible = false;
          tblActivityDescription.Visible = false;
          tblLanguages.Visible = false;
          gdvSearchResults.Visible = false;
          lblError.Visible = false;
          btnExport.Visible = false;
          lblSearchResults.Visible = false;
      }
      if (rblSearchChoices.SelectedValue == "Language")
      {
        sqldsActivityName.SelectParameters["group_descp"].DefaultValue = lbActivityGroupName.SelectedValue;
        //lboxAN.DataBind();

        //lbActivityGroupName.Attributes.Add("Disabled", "true");
        //lboxAN.Attributes.Add("Disabled", "true");
        //lbActivityGroupName.Enabled = false;
        //lboxAN.Enabled = false;
        //rblLanguages.Enabled = true;
        ////CheckBoxList1.Enabled = true;
        //ddlProgrammer.Enabled = false;
        lblSearchResults.Visible = false;
        tblLanguages.Visible = true;
        tblActivities.Visible = false;
        tblActivityDescription.Visible = false;
        tblProgrammer.Visible = false;
        gdvSearchResults.Visible = false;
        lblError.Visible = false;
        btnExport.Visible = false;
      }
      if (rblSearchChoices.SelectedValue == "IPad Enabled")
      {
          tblLanguages.Visible = false;
          tblActivities.Visible = false;
          tblActivityDescription.Visible = false;
          tblProgrammer.Visible = false;
          gdvSearchResults.Visible = false;
          lblError.Visible = false;
          btnExport.Visible = false;
          lblSearchResults.Visible = false;
      }
      if (rblSearchChoices.SelectedValue == "Activity Description")
      {
          tblLanguages.Visible = false;
          tblActivities.Visible = false;
          tblActivityDescription.Visible = true;
          tblProgrammer.Visible = false;
          gdvSearchResults.Visible = false;
          lblError.Visible = false;
          btnExport.Visible = false;
          lblSearchResults.Visible = false;
      }
    }
    catch (Exception ex)
    {
      lblError.Text = "A system error has occurred:" + " " + ex.Message;
      lblError.Visible = true;
    }
  }

  protected void lbActivityGroupName_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      ListBox lb = (ListBox)sender;
      sqldsActivityName.SelectParameters["group_descp"].DefaultValue = lb.SelectedValue;
      lboxAN.DataBind();
    }
    catch (Exception ex)
    {
      lblError.Text = "A system error has occurred:" + " " + ex.Message;
      lblError.Visible = true;
    }
  }

  protected void btnExport_Click(object sender, EventArgs e)
  {
      admin00100_search search = new admin00100_search();
      admin00100_reconciliation rec = new admin00100_reconciliation();
      if (rblSearchChoices.SelectedValue == "Activity Description")
      {
          if (txtActivityDescription.Text != "")
          {
              var items = search.SearchByActivityDescription(txtActivityDescription.Text);
              rec.CreateExcel(this.Page, items);
          }
          else
          {
              lblError.Text = "Please enter an Activity Description.";
              lblError.Visible = true;
          }
      }
      if (rblSearchChoices.SelectedValue == "IPad Enabled")
      {
          var items = search.SearchByIpadEnabled(gdvSearchResults, lblError);
          rec.CreateExcel(this.Page, items);
      }
      if (rblSearchChoices.SelectedValue == "Language")
      {
          var items = search.SearchByLanguageRadioButton(rblLanguages, lblError);
          rec.CreateExcel(this.Page, items);
      }
      if (rblSearchChoices.SelectedValue == "Programmer")
      {
          var items = search.SearchByProgrammer(ddlProgrammer, lblError);
          rec.CreateExcel(this.Page, items);
      }
      if (rblSearchChoices.SelectedValue == "Activity")
      {
          var items = search.SearchByANandACG(lbActivityGroupName, lboxAN, gdvSearchResults, lblError);
          rec.CreateExcel(this.Page, items);
      }
  }
}