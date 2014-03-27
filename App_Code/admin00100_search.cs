using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using ATSwebModel;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Search
/// </summary>
public class admin00100_search
{
    //string trackerDBConnectionString = ConfigurationManager.ConnectionStrings["TrackerDBConnectionString"].ToString();
    string atswebConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["admin00100_ATSwebConnectionString"].ToString();

    //not being used
    //public void LoadSearchResultsGridview(GridView grid, DropDownList activityGroupName, DropDownList activityName, DropDownList programmer, CheckBox vb, CheckBox csharp, CheckBox asp, CheckBox dotNet, CheckBox javascript, CheckBox ajax, TextBox other, Label error)
    //{
    //    DataTable dtResults = new DataTable();
    //    //dtResults = this.GetSearchResults(activityGroupName, activityName, programmer, vb, csharp, asp, dotNet, javascript, ajax, other, error);
    //    grid.DataSource = dtResults;
    //    grid.DataBind();
    //}

    //4/11/2013- not being used
    //public void SearchResults(GridView grid, DropDownList activityGroupName, DropDownList activityName, DropDownList originialProgrammer, CheckBoxList checkboxes, Label error)
    //{
    //  string activitygroupname = activityGroupName.SelectedValue;
    //  string activityname = activityName.SelectedValue;
    //  string originalprogrammer = originialProgrammer.SelectedValue;
    //  using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
    //  {
    //    var results = (from r in tracker.web_tracker_Activity
    //                   where r.ActivityGroupName == activitygroupname &&
    //                   r.ActivityName == activityname &&
    //                   r.OriginalProgrammer == originalprogrammer
    //                   select new { r.ActivityID, r.OldActivityID, r.ActivityName, r.LastModifiedBy });

    //    //this will get all checked items
    //    var checkedItems = from s in checkboxes.Items.Cast<ListItem>()
    //                       where s.Selected == true
    //                       select s;

    //    var asp = from c in checkedItems
    //              where c.Text.Contains("ASP")
    //              select c;

    //    var vb = from v in checkedItems
    //             where v.Text.Contains("VB")
    //             select v;

    //    var csharp = from c in checkedItems
    //                 where c.Text.Contains("C#")
    //                 select c;

    //    var net = from n in checkedItems
    //              where n.Text.Contains(".NET")
    //              select n;

    //    var ajax = from a in checkedItems
    //               where a.Text.Contains("AJAX")
    //               select a;

    //    var javascript = from j in checkedItems
    //                     where j.Text.Contains("Javascript")
    //                     select j;

    //    //for when Activity Name and Programmer are selected
    //    if (activityName.SelectedValue != " " && originialProgrammer.SelectedValue != "")
    //    {
    //      //if no language checkboxes are selected
    //      if (results.Any())
    //      {
    //        BindDataGrid(grid, results, error);
    //      }
    //      //for if they select an Activity Name and a Programmer and ASP VB, C#, and .NET are selected
    //      else if (results.Any() && ((asp.Any() && vb.Any() && csharp.Any() && net.Any())))
    //      {
    //        var Results = (from r in tracker.web_tracker_Activity
    //                       where r.ASP == "Yes" &&
    //                       r.ActivityGroupName == activitygroupname &&
    //                       r.ActivityName == activityname &&
    //                       r.OriginalProgrammer == originalprogrammer &&
    //                       r.VB == "Yes" &&
    //                       r.C_ == "Yes" &&
    //                       r.C_NET == "Yes"
    //                       select new { r.OldActivityID, r.ActivityGroupName, r.ActivityName, r.LastModifiedBy });

    //        BindDataGrid(grid, Results, error);
    //      }
    //      //for if they select records marked C#, .NET, and ASP
    //      else if (results.Any() && ((asp.Any() && csharp.Any() && net.Any())))
    //      {
    //        var Results = (from r in tracker.web_tracker_Activity
    //                       where r.ASP == "Yes" &&
    //                       r.ActivityGroupName == activitygroupname &&
    //                       r.ActivityName == activityname &&
    //                       r.OriginalProgrammer == originalprogrammer &&
    //                       r.C_ == "Yes" &&
    //                       r.C_NET == "Yes"
    //                       select new { r.OldActivityID, r.ActivityGroupName, r.ActivityName, r.LastModifiedBy });

    //        BindDataGrid(grid, Results, error);
    //      }
    //      //for handling records flagged ASP, VB, and .NET
    //      else if (results.Any() && ((asp.Any() && csharp.Any() && net.Any())))
    //      {
    //        var Results = (from r in tracker.web_tracker_Activity
    //                       where r.ASP == "Yes" &&
    //                       r.ActivityGroupName == activitygroupname &&
    //                       r.ActivityName == activityname &&
    //                       r.OriginalProgrammer == originalprogrammer &&
    //                       r.VB == "Yes" &&
    //                       r.C_NET == "Yes"
    //                       select new { r.OldActivityID, r.ActivityGroupName, r.ActivityName, r.LastModifiedBy });

    //        BindDataGrid(grid, Results, error);
    //      }
    //      //for records only flagged as ASP
    //      else if (asp.Any())
    //      {
    //        var Results = (from r in tracker.web_tracker_Activity
    //                       where r.ASP == "Yes" &&
    //                       r.ActivityGroupName == activitygroupname &&
    //                       r.ActivityName == activityname &&
    //                       r.OriginalProgrammer == originalprogrammer
    //                       select new { r.OldActivityID, r.ActivityGroupName, r.ActivityName, r.LastModifiedBy });

    //        BindDataGrid(grid, Results, error);
    //      }
    //      else
    //      {
    //        grid.Visible = false;
    //        error.Text = "No results found. Please try again.";
    //        error.Visible = true;
    //      }
    //    }
    //    //for only when Activity Group Name and Programmer are parameters, and no checkboxes are selected
    //    else if (activityName.SelectedValue == " " && originialProgrammer.SelectedValue != "")
    //    {
    //      SearchByActivityGroupNameAndProgrammer(activityGroupName, originialProgrammer, grid, error);
    //    }
    //    else
    //    {
    //      error.Text = "Please choose an Activity Group Name, and Programmer.";
    //      error.Visible = true;
    //    }
    //  }
    //}

    public IEnumerable<web_tracker_Activity> SearchByANandACG(ListBox acg, ListBox an, GridView grid, Label error)
    {
        //first count the number of Activity Names that have been selected
        admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
        int selectedCount = an.Items.Cast<ListItem>().Count(s => s.Selected);
        List<ListItem> selectedItems = an.GetSelectedItems();
        //if (selectedCount == 0 && acg.SelectedItem == null)
        //{
        //    error.Text = "No items have been selected.";
        //    error.Visible = true; 
        //}
        if (acg.SelectedItem != null && an.SelectedItem == null)
        {
            var activityGroupName = from s in context.web_tracker_Activity
                                    where s.ActivityGroupName == acg.SelectedValue
                                    select s;
            return activityGroupName;
        }
        if (selectedCount == 1)
        {
          string test = an.SelectedValue;
          var oneItem = from s in context.web_tracker_Activity
                        where s.OldActivityID == an.SelectedValue
                        select s;
          
          if (oneItem.Any() || an.SelectedValue == "0")
          {
            if (an.SelectedValue == "0")
            {
              var allItems = from s in context.web_tracker_Activity
                             where s.ActivityGroupName == acg.SelectedValue
                             select s;
              
              //BindDataGrid(grid, allItems, error);
              return allItems;
            }
            else
            {
                return oneItem;
            }
          }
        }
        if (selectedCount == 2)
        {
          string item1 = selectedItems.ElementAt(0).ToString();
          string item2 = selectedItems.ElementAt(1).ToString();
          string[] splitItem1 = item1.Split(',');
          string[] splitItem2 = item2.Split(',');
          string activityID1 = splitItem1.ElementAt(0).ToString();
          string activityID2 = splitItem2.ElementAt(0).ToString();
          var twoItems = from s in context.web_tracker_Activity
                         where s.OldActivityID == activityID1 ||
                         s.OldActivityID == activityID2
                         select s;
          //select new { s.OldActivityID, s.ActivityGroupName, s.ActivityName, s.LastModifiedBy });
          if (twoItems.Any())
          {
            //BindDataGrid(grid, twoItems, error);
             return twoItems;
          }
        }
        if (selectedCount == 3)
        {
          string item1 = selectedItems.ElementAt(0).ToString();
          string item2 = selectedItems.ElementAt(1).ToString();
          string item3 = selectedItems.ElementAt(2).ToString();
          string[] splitItem1 = item1.Split(',');
          string[] splitItem2 = item2.Split(',');
          string[] splitItem3 = item3.Split(',');
          string activityID1 = splitItem1.ElementAt(0).ToString();
          string activityID2 = splitItem2.ElementAt(0).ToString();
          string activityID3 = splitItem3.ElementAt(0).ToString();
          var threeItems = from s in context.web_tracker_Activity
                           where s.OldActivityID == activityID1 ||
                           s.OldActivityID == activityID2 ||
                           s.OldActivityID == activityID3
                           select s;
          //select new { s.OldActivityID, s.ActivityGroupName, s.ActivityName, s.LastModifiedBy });
          if (threeItems.Any())
          {
            //BindDataGrid(grid, threeItems, error);
              return threeItems;
          }
        }
        //if (selectedCount > 3)
        //{
        //  error.Text = "You may choose between one and three Activity Names. Click 'All Activities' to get all Activity Names associated with the selected Activity Group Name.";
        //  error.Visible = true;
        //}
      
      return Enumerable.Empty<web_tracker_Activity>();
    }

    public IEnumerable<web_tracker_Activity> SearchByActivityDescription(string input)
    {
        admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
        IEnumerable<web_tracker_Activity> items = (IEnumerable<web_tracker_Activity>) (from i in context.web_tracker_Activity
                        where i.ActivityName.Contains(input)
                        select i);
       return items;
    }



    private void BindDataGrid2(GridView grid, IEnumerable<web_tracker_Activity> results, Label error)
    {
        grid.DataSource = results;
        grid.DataBind();
        grid.Visible = true;
        error.Visible = false;
    }

    public IEnumerable<web_tracker_Activity> SearchByIpadEnabled(GridView grid, Label error)
    {
        admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
        
            IEnumerable<web_tracker_Activity> items = (from i in context.web_tracker_Activity
                        where i.IPadEnabled == "Yes"
                        select i).AsEnumerable();
            return items;
    }

    public IEnumerable<web_tracker_Activity> SearchByLanguageRadioButton(RadioButtonList language, Label error)
    {
        //get the language selected
        admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
        //string selected = language.Items.Cast<ListItem>().SingleOrDefault(li => li.Selected).ToString();
        if (language.HasSelectedValue())
        {
            if (language.SelectedValue == "ASP Classic")
            {
                var items = from i in context.web_tracker_Activity
                            where i.ASP == "Yes" &&
                            (i.C_NET == null || i.C_NET == String.Empty ||
                             i.C_ == null || i.C_ == String.Empty ||
                             i.C_NET == "No" || i.C_ == "No")
                            select i;
                return items;
            } 
            if (language.SelectedValue == "ASP.NET")
            {
                var items = from i in context.web_tracker_Activity
                            where i.ASP == "Yes" &&
                            i.C_NET == "Yes"
                            select i;
                return items;
            }
            if (language.SelectedValue == "VB")
            {
                var items = from i in context.web_tracker_Activity
                            where i.VB == "Yes"
                            select i;
                
                return items;
            }
            if (language.SelectedValue == "C#")
            {
                var items = from i in context.web_tracker_Activity
                            where i.C_ == "Yes"
                            select i;
                return items;
            }
            if (language.SelectedValue == ".NET")
            {
                var items = from i in context.web_tracker_Activity
                            where i.C_NET == "Yes"
                            select i;
                return items;
            }
            if (language.SelectedValue == "AJAX")
            {
                var items = from i in context.web_tracker_Activity
                            where i.AJAX == "Yes"
                            select i;
                return items;
            }
            if (language.SelectedValue == "Javascript")
            {
                var items = from i in context.web_tracker_Activity
                            where i.Javascript == "Yes"
                            select i;
                return items;
            }
        }
        return Enumerable.Empty<web_tracker_Activity>();
    }

    //not being used- works for the most part, but doesn't always return all applicable records
    //public void SearchByLanguage(CheckBoxList language, GridView grid, Label error)
    //{
    //  using (admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities())
    //  {
    //    int selectedCount = language.Items.Cast<ListItem>().Count(li => li.Selected);
    //    if (selectedCount > 0)
    //    {
    //      //now get which language was selected
    //      var checkedItems = from i in language.Items.Cast<ListItem>()
    //                         where i.Selected == true
    //                         select i;
    //      if (checkedItems.Any())
    //      {
    //        var asp = from c in checkedItems
    //                  where c.Text.Contains("ASP")
    //                  select c;

    //        var vb = from v in checkedItems
    //                 where v.Text.Contains("VB")
    //                 select v;

    //        var csharp = from c in checkedItems
    //                     where c.Text.Contains("C#")
    //                     select c;

    //        var net = from n in checkedItems
    //                  where n.Text.Contains(".NET")
    //                  select n;

    //        var ajax = from a in checkedItems
    //                   where a.Text.Contains("AJAX")
    //                   select a;

    //        var javascript = from j in checkedItems
    //                         where j.Text.Contains("Javascript")
    //                         select j;
    //        //for .NET
    //        if (net.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.C_NET == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for C#
    //        if (csharp.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.C_ == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for VB
    //        if (vb.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.VB == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for anything containing javascript
    //        if (javascript.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.Javascript == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for anything containing AJAX
    //        if (ajax.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.AJAX == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }

    //        //for when they select ASP and VB
    //        if (asp.Any() && vb.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.ASP == "Yes" &&
    //                                l.VB == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for ASP and C#
    //        if (csharp.Any() && asp.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.ASP == "Yes" &&
    //                                l.C_ == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for ASP Classic
    //        if (asp.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.ASP == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for ASP, NET, and VB
    //        if (asp.Any() && net.Any() && vb.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.ASP == "Yes" &&
    //                                l.C_NET == "Yes" &&
    //                                l.VB == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //        //for ASP, NET, and C#
    //        if (asp.Any() && net.Any() && csharp.Any())
    //        {
    //          var languageRecords = from l in context.web_tracker_Activity
    //                                where l.ASP == "Yes" &&
    //                                l.C_NET == "Yes" &&
    //                                l.C_ == "Yes"
    //                                select l;
    //          //select new { l.OldActivityID, l.ActivityGroupName, l.ActivityName, l.LastModifiedBy });
    //          BindDataGrid(grid, languageRecords, error);
    //        }
    //      }
    //      else
    //      {
    //        error.Text = "No items have been selected.";
    //        error.Visible = true;
    //      }
    //    }
    //    context.Dispose();
    //  }
    //}


    public IEnumerable<web_tracker_Activity> SearchByProgrammer(DropDownList programmer, Label error)
    {
        admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
        if (programmer.SelectedValue != "Please Select" && programmer.SelectedValue != "Other")
        {
          string test = programmer.SelectedValue;
          var records = from r in context.web_tracker_Activity
                        where r.OriginalProgrammer == programmer.SelectedValue
                        select r;
          return records;
        }
        if (programmer.SelectedValue == "Other")
        {
          var otherProgrammers = from r in context.web_tracker_Activity
                                 where r.OriginalProgrammer != "Jones" &&
                                 r.OriginalProgrammer != "Prestosa" &&
                                 r.OriginalProgrammer != "MacIvor" &&
                                 r.OriginalProgrammer != "Levines" &&
                                 r.OriginalProgrammer != "Pegram" &&
                                 r.OriginalProgrammer != "Ryan" &&
                                 r.OriginalProgrammer != "Alford" &&
                                 r.OriginalProgrammer != "Montgomery" &&
                                 r.OriginalProgrammer != "Redford" &&
                                 r.OriginalProgrammer != "ATS"
                                 select r;
          return otherProgrammers;
        }
      return Enumerable.Empty<web_tracker_Activity>();
   }
        

    //4/11/2013- not being used
    //public void SearchByActivityGroupName(ListBox acg, GridView grid, Label error)
    //{
    //  admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
    //  List<ListItem> selectedItems = acg.GetSelectedItems();
    //  int count = acg.GetSelectedIndices().Length;

    //  if (count == 0)
    //  {
    //    error.Text = "Please choose between one and five Activity Group Names.";
    //    error.Visible = true;
    //  }
    //  if (count == 1)
    //  {
    //    string item = selectedItems.ElementAt(0).ToString();
    //    var oneACG = (from a in context.web_tracker_Activity
    //                  where a.ActivityGroupName == item
    //                  select new { a.OldActivityID, a.ActivityGroupName, a.ActivityName, a.LastModifiedBy });
    //    BindDataGrid(grid, oneACG, error);
    //  }
    //  if (count == 2)
    //  {
    //    string item1 = selectedItems.ElementAt(0).ToString();
    //    string item2 = selectedItems.ElementAt(1).ToString();
    //    var twoACG = (from a in context.web_tracker_Activity
    //                  where a.ActivityGroupName == item1 ||
    //                  a.ActivityGroupName == item2
    //                  select new { a.OldActivityID, a.ActivityGroupName, a.ActivityName, a.LastModifiedBy });
    //    BindDataGrid(grid, twoACG, error);
    //  }
    //  if (count == 3)
    //  {
    //    string item1 = selectedItems.ElementAt(0).ToString();
    //    string item2 = selectedItems.ElementAt(1).ToString();
    //    string item3 = selectedItems.ElementAt(2).ToString();
    //    var threeACG = (from a in context.web_tracker_Activity
    //                    where a.ActivityGroupName == item1 ||
    //                    a.ActivityGroupName == item2
    //                    select new { a.OldActivityID, a.ActivityGroupName, a.ActivityName, a.LastModifiedBy });
    //    BindDataGrid(grid, threeACG, error);
    //  }
    //  if (count > 3)
    //  {
    //    error.Text = "Please choose three Activity Group Names or less.";
    //    error.Visible = true;
    //  }
    //}



    //not being used
    //public void SearchByActivityGroupNameAndProgrammer(DropDownList activityGroupName, DropDownList originialProgrammer, GridView grid, Label error)
    //{
    //  string activitygroupname = activityGroupName.SelectedValue;
    //  string originalprogrammer = originialProgrammer.SelectedValue;
    //  using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
    //  {
    //    var Results = (from r in tracker.web_tracker_Activity
    //                   where r.ActivityGroupName == activitygroupname &&
    //                   r.OriginalProgrammer == originalprogrammer
    //                   select new { r.OldActivityID, r.ActivityGroupName, r.ActivityName, r.LastModifiedBy });
    //    BindDataGrid(grid, Results, error);
    //  }
    //}



    public void BindDataGrid(GridView grid, IEnumerable<web_tracker_Activity> results, Label error)
    {
      grid.DataSource = results;
      grid.DataBind();
      grid.Visible = true;
      error.Visible = false;
    }

    //4/11/2013- not being used
    //private DataTable GetSearchResults(DropDownList activityGroupName, DropDownList activityName, DropDownList programmer, CheckBox vb, CheckBox csharp, CheckBox asp, CheckBox dotNet, CheckBox javascript, CheckBox ajax, TextBox other, Label error)
    //{
    //    string sql = String.Empty;
    //    DataTable dtResults = new DataTable();
    //    string selectClause = "select ActivityID, OldActivityID, ActivityName, LastModifiedBy from Activities where";
    //    string whereActivityGroupName = "ActivityGroupName = @activityGroupName";
    //    string whereActivityName = "ActivityName = @activityName";
    //    string whereProgrammer = "OriginalProgrammer = @originalProgrammer";
    //    string whereASP = "ASP = @asp";
    //    string whereDotNet = "[.NET] = @Net";
    //    string whereVB = "VB = @VB";
    //    string whereCSharp = "C# = @csharp";
    //    string whereAjax = "AJAX = @ajax";
    //    string whereJavascript = "Javascript = @javascript";
    //    string andClause = "and";

    //    if (activityName.SelectedValue != "" && programmer.SelectedValue != "")
    //    {
    //        //using (SqlConnection conn = new SqlConnection(trackerDBConnectionString))
    //        {
    //            sql = selectClause + " " + whereActivityGroupName + " " + andClause + " " + whereActivityName + " " + andClause + " " + whereProgrammer;
    //            using (SqlCommand cmd = new SqlCommand(sql, conn))
    //            {
    //                SqlParameter activityGroupNameParameter = new SqlParameter("@activityGroupName", activityGroupName.SelectedValue);
    //                SqlParameter activityNameParameter = new SqlParameter("@activityName", activityName.SelectedValue);
    //                SqlParameter programmerParameter = new SqlParameter("@originalProgrammer", programmer.SelectedValue);
    //                cmd.Parameters.Add(activityGroupNameParameter);
    //                cmd.Parameters.Add(activityNameParameter);
    //                cmd.Parameters.Add(programmerParameter);
    //                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //                {
    //                    ad.Fill(dtResults);
    //                }
    //            }
    //        }
    //    }
    //    else if (activityName.SelectedValue != "" && programmer.SelectedValue != "" && vb.Checked)
    //    {
    //        sql = selectClause + whereActivityGroupName + andClause + whereActivityName + andClause + whereProgrammer + andClause + whereVB;
    //    }
    //    else
    //    {
    //        error.Text = "Please choose an Activity Group Name, Activity Name, and Programmer.";
    //        error.Visible = true;
    //    }
    //    return dtResults;
    //}




    //private string BuildQueryString(DropDownList activityGroupName, DropDownList activityName, DropDownList programmer, CheckBox vb, CheckBox csharp, CheckBox asp, CheckBox dotNet, CheckBox javascript, CheckBox ajax, TextBox other, Label error)
    //{
    //    string sql = String.Empty;
    //    string selectClause = "select ActivityID, OldActivityID, ActivityName, LastModifiedBy from Activities where";
    //    string whereActivityGroupName = "ActivityGroupName = @activityGroupName";
    //    string whereActivityName = "ActivityName = @activityName";
    //    string whereProgrammer = "OriginalProgrammer = @originalProgrammer";
    //    string whereASP = "ASP = @asp";
    //    string whereDotNet = "[.NET] = @Net";
    //    string whereVB = "VB = @VB";
    //    string whereCSharp = "C# = @csharp";
    //    string whereAjax = "AJAX = @ajax";
    //    string whereJavascript = "Javascript = @javascript";
    //    string andClause = "and";

    //    if (activityName.SelectedValue != "" && programmer.SelectedValue != "")
    //    {
    //        sql = selectClause + whereActivityGroupName + andClause + whereActivityName + andClause + whereProgrammer;
    //    }
    //    else if (activityName.SelectedValue != "" && programmer.SelectedValue != "" && vb.Checked)
    //    {
    //        sql = selectClause + whereActivityGroupName + andClause + whereActivityName + andClause + whereProgrammer + andClause + whereVB;
    //    }
    //    else
    //    {
    //        error.Text = "Please choose an Activity Group Name, Activity Name, and Programmer.";
    //    }
    //    return sql;
    //}





    public void PopulateActivityGroupNameDropDown(DropDownList activityGroupName)
    {

      DataTable dtactivityGroupNames = new DataTable();
      dtactivityGroupNames = this.GetActivityGroupNames();
      //activityGroupName.DataValueField = dtactivityGroupNames.Columns["activity_id"].ToString();
      //activityGroupName.DataTextField = dtactivityGroupNames.Columns["group_descp"].ToString();
      activityGroupName.DataSource = dtactivityGroupNames;
      activityGroupName.DataValueField = "group_descp";
      activityGroupName.DataTextField = "group_descp";
      activityGroupName.DataBind();
    }

    public void PopulateActivityNameDropDown(DropDownList activityName)
    {
      DataTable dtActivityNames = new DataTable();
      dtActivityNames = this.GetActivityNames();
      //activityName.DataValueField = dtActivityNames.Columns["activity_id"].ToString();
      //activityName.DataTextField = dtActivityNames.Columns["descp"].ToString();
      activityName.DataSource = dtActivityNames;
      activityName.DataValueField = "descp";
      activityName.DataTextField = "descp";
      activityName.DataBind();
    }

    //to populate ddlActivityName
    public DataTable GetActivityNames()
    {
      DataTable dtActivityNames = new DataTable();
      using (SqlConnection conn = new SqlConnection(atswebConnectionString))
      {
        string sql = "select distinct descp from web_activities";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
          using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
          {
            ad.Fill(dtActivityNames);
          }
        }
      }
      return dtActivityNames;
    }

    //this works- for pulling group_descp to populate ddlActivityGroupName dropdown
    public DataTable GetActivityGroupNames()
    {
      DataTable dtactivityGroupNames = new DataTable();
      using (SqlConnection conn = new SqlConnection(atswebConnectionString))
      {
        string sql = "select distinct group_descp from web_activities";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
          using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
          {
            ad.Fill(dtactivityGroupNames);
          }
        }
      }
      return dtactivityGroupNames;
    }

    public void RefreshSearchResultsGrid(GridView grid, IQueryable results)
    {
      using (admin00100_TrackerDBEntities tracker = new admin00100_TrackerDBEntities())
      {
        grid.DataSource = results;
        grid.DataBind();
      }
    }

    


    //throws constraint exception- no primary key defined on the table

    //public DataTable GetWebActivities()
    //{
    //    DataTable dtactivityGroupNames = new DataTable();
    //    WebActivitiesTableAdapters.web_activitiesTableAdapter webActivities = new WebActivitiesTableAdapters.web_activitiesTableAdapter();
    //    dtactivityGroupNames = webActivities.GetActivityGroupNames();
    //    return dtactivityGroupNames;
    //}


    //this returns null for some reason- no metadata?

    //public void PopulateActivityGroupNameDropDown(DropDownList activityGroupName)
    //{
    //    web_activities act = new web_activities();
    //    var activities = from a in act.group_descp
    //                     select new { act.group_descp };
    //    activityGroupName.DataValueField = "group_descp";
    //    activityGroupName.DataTextField = "group_descp";
    //    activityGroupName.DataSource = activities;
    //    activityGroupName.DataBind();
    //}
  }
