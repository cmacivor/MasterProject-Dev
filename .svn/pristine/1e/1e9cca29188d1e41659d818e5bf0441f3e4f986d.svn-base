using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using ATSwebModel;
using System.Data.SqlClient;
using SoftArtisans.OfficeWriter.ExcelWriter;
using System.Web.UI;


/// <summary>
/// Summary description for admin00100_reconciliation
/// </summary>
public class admin00100_reconciliation
{
  string atswebConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["admin00100_ATSwebConnectionString"].ToString();

  public void GetLatestActivities(GridView gridview)
  {

    DataTable dtActivityID = this.GetActivityID();

    admin00100_TrackerDBEntities latestTrackerContext = new admin00100_TrackerDBEntities();
    foreach (DataRow id in dtActivityID.Rows)
    {
      string oldActivityID = Convert.ToString(id["activity_id"].ToString());
      string activityName = Convert.ToString(id["descp"].ToString());
      string activityGroupName = Convert.ToString(id["group_descp"].ToString());
      //now check if each activityID is present in the OldActivityID column of the Activities table
      var isActivityIDInActivities = (from a in latestTrackerContext.web_tracker_Activity
                                      where a.OldActivityID == oldActivityID
                                      select a).Count();
      //if nothing is found, need to insert all columns from web_activities into Activities
      if (isActivityIDInActivities == 0)
      {
        web_tracker_Activity activity = new web_tracker_Activity
        {
          ActivityGroupName = activityGroupName,
          ActivityName = activityName,
          OldActivityID = oldActivityID
        };
        latestTrackerContext.AddToweb_tracker_Activity(activity);
        latestTrackerContext.SaveChanges();
      }
      

    }
    this.GetNewlyReconciledRecords(gridview);
    latestTrackerContext.Dispose();
  }

  //need to do the opposite of the GetLatestActivities method
  public void GetActivitiesDeletedFromOpenSS()
  {
    //first get all ActivityID's from OldActivityID in Activities table
    admin00100_TrackerDBEntities allID = new admin00100_TrackerDBEntities();
    admin00100_details details = new admin00100_details();
    var oldActivityID = from a in allID.web_tracker_Activity
                        where a.OldActivityID != "n/a" &&
                        a.OldActivityID != " "
                        select a.OldActivityID;

    if (oldActivityID.Any())
    {
      //for each id in Activities, query web_activities- if it's not found, mark it as deleted in Activities
      foreach (var id in oldActivityID)
      {
        DataTable dtDeleted = new DataTable();
        string activityID = Convert.ToString(id);
        dtDeleted = this.GetRecordsDeletedFromWebActivities(activityID);

        //need to get associated details from Activities table
        if (dtDeleted.Rows.Count == 0)
        {
          //now flag the record as deleted in the Activities table
          web_tracker_Activity activity = allID.web_tracker_Activity.Single(record => record.OldActivityID == activityID);
          activity.Deleted = "True";
        }
      }
    }
    allID.SaveChanges();
    allID.Dispose();
  }

  private DataTable GetRecordsDeletedFromWebActivities(string activityID)
  {
    DataTable dtActivityIDs = new DataTable();
    using (SqlConnection conn = new SqlConnection(atswebConnectionString))
    {
      string sql = "select distinct activity_id, descp, group_descp from web_activities where activity_id = @activityid ";
      using (SqlCommand cmd = new SqlCommand(sql, conn))
      {
        SqlParameter prm = new SqlParameter("@activityid", activityID);
        cmd.Parameters.Add(prm);
        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        {
          ad.Fill(dtActivityIDs);
        }
      }
    }
    return dtActivityIDs;
  }

  public void GetDeletedRecords(GridView grid)
  {
    admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
    var deleted = from d in context.web_tracker_Activity
                  where d.Deleted == "True"
                  select d;
    grid.DataSource = deleted;
    grid.DataBind();
    context.Dispose();
  }

  public void GetIncompleteRecords(GridView grid)
  {
    admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
    var newRecords = from a in context.web_tracker_Activity
                     where a.OriginalProgrammer == null ||
                     a.ASP == null ||
                     a.VB == null ||
                     a.C_ == null ||
                     a.C_NET == null ||
                     a.AJAX == null ||
                     a.Javascript == null ||
                     a.Other == null ||
                     a.LastModifiedBy == null ||
                     a.ContainDataControls == null ||
                     a.OSSecurityGroupAvailable == null ||
                     a.OSSecurityDoc == null ||
                     a.IPadEnabled == null ||
                     a.HathorWebOpenssActivities == null ||
                     a.HebeOpenssapps == null ||
                     a.HebeOpenssapps2 == null ||
                     a.HebeOpenssapps4 == null ||
                     a.ActivityDiscontinued == null
                     select a;

    grid.DataSource = newRecords;
    grid.DataBind();
    context.Dispose();
  }

  public void GetNewlyReconciledRecords(GridView grid)
  {
    admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
    var newRecords = from a in context.web_tracker_Activity
                     where a.OriginalProgrammer == null &&
                     a.ASP == null &&
                     a.VB == null &&
                     a.C_ == null &&
                     a.C_NET == null &&
                     a.AJAX == null &&
                     a.Javascript == null &&
                     a.Other == null &&
                     a.LastModifiedBy == null &&
                     a.ContainDataControls == null &&
                     a.OSSecurityGroupAvailable == null &&
                     a.OSSecurityDoc == null &&
                     a.IPadEnabled == null &&
                     a.HathorWebOpenssActivities == null &&
                     a.HebeOpenssapps == null &&
                     a.HebeOpenssapps2 == null &&
                     a.HebeOpenssapps4 == null &&
                     a.ActivityDiscontinued == null
                     orderby a.ReconcileDate descending
                     select a;

    grid.DataSource = newRecords;
    grid.DataBind();
    context.Dispose();
  }

  public void CreateExcel(Page page, IEnumerable<web_tracker_Activity> items)
  {
      admin00100_TrackerDBEntities context = new admin00100_TrackerDBEntities();
      if (items.Any())
      {
          ExcelApplication eApp = new ExcelApplication();
          Workbook eWB = eApp.Create();
          Worksheet eWS = eWB.Worksheets[0];
          eWS.Cells["A1"].Value = "Activity Group Name";
          eWS.Cells["B1"].Value = "Activity Name";
          eWS.Cells["C1"].Value = "Activity ID";
          eWS.Cells["D1"].Value = "Original Programmer";
          eWS.Cells["E1"].Value = "ASP";
          eWS.Cells["F1"].Value = "VB";
          eWS.Cells["G1"].Value = "C#";
          eWS.Cells["H1"].Value = ".NET";
          eWS.Cells["I1"].Value = "AJAX";
          eWS.Cells["J1"].Value = "Javascript";
          eWS.Cells["K1"].Value = "Other";
          eWS.Cells["L1"].Value = "Last Modified By";
          eWS.Cells["M1"].Value = "Contains Data Controls?";
          eWS.Cells["N1"].Value = "OS Security Group Available?";
          eWS.Cells["O1"].Value = "OS Security Doc?";
          eWS.Cells["P1"].Value = "IPad Enabled?";
          eWS.Cells["Q1"].Value = "\\hathor\\web\\openss\\activities";
          eWS.Cells["R1"].Value = "\\hebe\\openssapps";
          eWS.Cells["S1"].Value = "\\hebe\\openssapps2";
          eWS.Cells["T1"].Value = "\\hebe\\openssapps4";
          eWS.Cells["U1"].Value = "Is Actitivity Discontinued?";
          eWS.Cells["V1"].Value = "Pop Up Window?";
          eWS.Cells["W1"].Value = "Project";

          for (int x = 0; x <= 24; x++)
          {
              eWS.Cells[0, x].Style.Font.Bold = true;
          }
          int rowNumber = 1;
          foreach (var i in items)
          {
              rowNumber++;
              eWS.Cells[rowNumber, 0].Value = i.ActivityGroupName;
              eWS.Cells[rowNumber, 1].Value = i.ActivityName;
              eWS.Cells[rowNumber, 2].Value = i.OldActivityID;
              eWS.Cells[rowNumber, 3].Value = i.OriginalProgrammer;
              eWS.Cells[rowNumber, 4].Value = i.ASP;
              eWS.Cells[rowNumber, 5].Value = i.VB;
              eWS.Cells[rowNumber, 6].Value = i.C_;
              eWS.Cells[rowNumber, 7].Value = i.C_NET;
              eWS.Cells[rowNumber, 8].Value = i.AJAX;
              eWS.Cells[rowNumber, 9].Value = i.Javascript;
              eWS.Cells[rowNumber, 10].Value = i.Other;
              eWS.Cells[rowNumber, 11].Value = i.LastModifiedBy;
              eWS.Cells[rowNumber, 12].Value = i.ContainDataControls;
              eWS.Cells[rowNumber, 13].Value = i.OSSecurityGroupAvailable;
              eWS.Cells[rowNumber, 14].Value = i.OSSecurityDoc;
              eWS.Cells[rowNumber, 15].Value = i.IPadEnabled;
              eWS.Cells[rowNumber, 16].Value = i.HathorWebOpenssActivities;
              eWS.Cells[rowNumber, 17].Value = i.HebeOpenssapps;
              eWS.Cells[rowNumber, 18].Value = i.HebeOpenssapps2;
              eWS.Cells[rowNumber, 19].Value = i.HebeOpenssapps4;
              eWS.Cells[rowNumber, 20].Value = i.ActivityDiscontinued;
              eWS.Cells[rowNumber, 21].Value = i.PopUpWindow;
              eWS.Cells[rowNumber, 22].Value = i.Project;
              eWS.CreateArea(0, 0, rowNumber, 24).AutoFitWidth();
          }
          eApp.Save(eWB, page.Response, "Activities.xls", true);
      }
      context.Dispose();
  }

 


  private DataTable GetActivityID()
  {
    DataTable dtActivityIDs = new DataTable();
    using (SqlConnection conn = new SqlConnection(atswebConnectionString))
    {
      string sql = "select distinct activity_id, descp, group_descp from web_activities";
      using (SqlCommand cmd = new SqlCommand(sql, conn))
      {
        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        {
          ad.Fill(dtActivityIDs);
        }
      }
    }
    return dtActivityIDs;
  }
}