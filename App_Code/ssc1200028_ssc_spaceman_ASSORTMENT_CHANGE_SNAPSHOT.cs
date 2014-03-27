using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ssc1200028_ssc_spacemanModel;

/// <summary>
/// Summary description for ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT
/// </summary>
public class ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT
{
    public int SnapshotID { get; set; }
    public string SnapshotNumber { get; set; }
    public string SnapshotDescription { get; set; }
    public int AssociatedPlanogramKeyID { get; set; }
    public string Status { get; set; }
    public int Size { get; set; }
    public string Season { get; set; }
    public int Year { get; set; }
    public string Location { get; set; }
    public string Buyer { get; set; }
    public DateTime DateAdded { get; set; }
    public string ChgUser { get; set; }
    public DateTime ChangeDate { get; set; }

    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> Snapshots { get; set; }

    public int GetKeyIDBySnapshotID(ssc1200028_ssc_spacemanEntities1 context, int snapshotid)
    {
        var key = (from c in context.ssc_assortment_change_snapshot
                   where c.snapshot_id == snapshotid
                   select new { c.snapshot_keyid }).SingleOrDefault().snapshot_keyid;
        int keyid = Convert.ToInt32(key);
        return keyid;
    }

    /// <summary>
    /// Gets snapshots by snapshotID, but not from the session- only the snapshotID
    /// </summary>
    /// <param name="snapshotid"></param>
    /// <returns></returns>
    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsBySnapshotID(ssc1200028_ssc_spacemanEntities1 context, int snapshotid)
    {
        List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
        snapshots = (from s in context.ssc_assortment_change_snapshot
                     where s.snapshot_id == snapshotid
                     select new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT
                     {
                         SnapshotNumber = s.snapshot_nbr
                     }).ToList();
        return snapshots;
    }


    /// <summary>
    /// This will get the details of a snapshot that was selected from the Home page and is stored in the snapshotID session
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsBySnapshotID(ssc1200028_ssc_spacemanEntities1 context)
    {
        List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshot = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
        ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        snapshot = (from s in context.ssc_assortment_change_snapshot
                    where s.snapshot_id == detail.SnapshotSessionID
                    select new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT
                    {
                        SnapshotNumber = s.snapshot_nbr
                    }).ToList();
        return snapshot;
    }


    public IQueryable GetSnapshotByKeyID(int keyID, ssc1200028_ssc_spacemanEntities1 context)
    {
        var selectedSnapshots = from c in context.ssc_assortment_change_snapshot
                                where c.snapshot_keyid == keyID
                                select new { c.snapshot_keyid, c.snapshot_id, c.snapshot_nbr, c.snapshot_desc };
            
        return selectedSnapshots;
    }

    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsByPlanogramNameAndKeyID(ssc1200028_ssc_spacemanEntities1 context, string planogramDesc, int keyid)
    {
        //List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
       var snapshots = from s in context.ssc_assortment_change_snapshot
                     where s.snapshot_desc.Contains(planogramDesc) &&
                     s.snapshot_keyid == keyid
                     select new 
                     {
                         s.snapshot_id,
                         s.snapshot_nbr,
                         s.snapshot_desc,
                         s.snapshot_keyid,
                         s.snapshot_status,
                         s.snapshot_size,
                         s.snapshot_season,
                         s.snapshot_year,
                         s.snapshot_location,
                         s.snapshot_buyer
                     };
       return snapshots.ToList()
            .Select(s => new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT()
            {
                SnapshotID = s.snapshot_id,
                SnapshotNumber = s.snapshot_nbr,
                SnapshotDescription = s.snapshot_desc,
                AssociatedPlanogramKeyID = s.snapshot_keyid,
                Status = s.snapshot_status,
                Size = Convert.ToInt32(s.snapshot_size),
                Season = s.snapshot_season,
                Year = Convert.ToInt32(s.snapshot_year),
                Location = s.snapshot_location,
                Buyer = s.snapshot_buyer,
            }).ToList();
    }

    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsByPlanogramName(ssc1200028_ssc_spacemanEntities1 context, string planogramDesc)
    {
        //List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
         var snapshots = from s in context.ssc_assortment_change_snapshot
                         where s.snapshot_desc.Contains(planogramDesc)
                         select new 
                         {
                             s.snapshot_id,
                             s.snapshot_nbr,
                             s.snapshot_desc,
                             s.snapshot_keyid,
                             s.snapshot_status,
                             s.snapshot_size,
                             s.snapshot_season,
                             s.snapshot_year,
                             s.snapshot_location,
                             s.snapshot_buyer
                         };

         return snapshots.ToList()
             .Select(s => new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT()
             {
                 SnapshotID = s.snapshot_id,
                 SnapshotNumber = s.snapshot_nbr,
                 SnapshotDescription = s.snapshot_desc,
                 AssociatedPlanogramKeyID = s.snapshot_keyid,
                 Status = s.snapshot_status,
                 Size = Convert.ToInt32(s.snapshot_size),
                 Season = s.snapshot_season,
                 Year = Convert.ToInt32(s.snapshot_year),
                 Location = s.snapshot_location,
                 Buyer = s.snapshot_buyer,
             }).ToList();
    }



    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsByKeyID(ssc1200028_ssc_spacemanEntities1 context, int keyid)
    {
        List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();

        var snapshotsEF = from s in context.ssc_assortment_change_snapshot
                          where s.snapshot_keyid == keyid
                          select new
                          {
                              s.snapshot_id,
                              s.snapshot_nbr,
                              s.snapshot_desc,
                              s.snapshot_keyid,
                              s.snapshot_status,
                              s.snapshot_size,
                              s.snapshot_season,
                              s.snapshot_year,
                              s.snapshot_location,
                              s.snapshot_buyer
                          };

        return snapshotsEF.ToList()
            .Select(s => new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT()
            {
                SnapshotID = s.snapshot_id,
                SnapshotNumber = s.snapshot_nbr,
                SnapshotDescription = s.snapshot_desc,
                AssociatedPlanogramKeyID = s.snapshot_keyid,
                Status = s.snapshot_status,
                Size = Convert.ToInt32(s.snapshot_size),
                Season = s.snapshot_season,
                Year = Convert.ToInt32(s.snapshot_year),
                Location = s.snapshot_location,
                Buyer = s.snapshot_buyer,
            }).ToList();
    }

    //public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> ConvertToList(IQueryable<ssc_assortment_change_snapshot> snapshots)
    //{
    //    var list = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
    //    foreach (var item in snapshots)
    //    {
    //        list.Add(new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT()
    //        {
    //            SnapshotID = item.snapshot_id,
    //            SnapshotNumber = item.snapshot_nbr,
    //            SnapshotDescription = item.snapshot_desc,
    //            AssociatedPlanogramKeyID = item.snapshot_keyid
    //        });
           
    //    }
    //    return list;
    //    //return snapshots.ToList()
    //    //    .Select(s => new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT()
    //    //    {
    //    //        SnapshotID = s.snapshot_id,
    //    //        SnapshotNumber = s.snapshot_nbr,
    //    //        SnapshotDescription = s.snapshot_desc,
    //    //        AssociatedPlanogramKeyID = s.snapshot_keyid,
    //    //        Status = s.snapshot_status,
    //    //        Size = Convert.ToInt32(s.snapshot_size),
    //    //        Season = s.snapshot_season,
    //    //        Year = Convert.ToInt32(s.snapshot_year),
    //    //        Location = s.snapshot_location,
    //    //        Buyer = s.snapshot_buyer,
    //    //    }).ToList();
    //}

    //constructor
	public ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}