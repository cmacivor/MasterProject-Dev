using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ssc1200028_ssc_spacemanModel;

/// <summary>
/// Summary description for Search
/// </summary>
public class ssc1200028_Search
{
    public int Key_id { get; set; }
    public string ProductID { get; set; }

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
    public List<ssc1200028_Search> Planograms { get; set; }

    public IEnumerable<ssc_assortment_change_snapshot> SearchSnapshots(int size, string season, string location)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var resultsBySize = (from snapshots in context.ssc_assortment_change_snapshot
                            where snapshots.snapshot_size == size
                            select snapshots).ToList();

        var resultsBySeason = (from snapshots in context.ssc_assortment_change_snapshot
                              where snapshots.snapshot_season == season
                              select snapshots).ToList();

        var resultsByLocation = (from snapshots in context.ssc_assortment_change_snapshot
                                where snapshots.snapshot_location == location
                                select snapshots).ToList();

        //var results = resultsBySize.Intersect(resultsBySeason).Intersect(resultsByLocation);
        var results = resultsBySize.Union(resultsBySeason).Union(resultsByLocation).Distinct();

        return results;
    }



    public List<ssc1200028_Search> SearchPlanogramsBySKU(ssc1200028_ssc_spacemanEntities1 context, string sku)
    {

        var results = from s in context.PRODUCT_LIST
                      join pl in context.PLANO_KEY
                      on s.Key_id equals pl.Key_id
                      where s.product_id == sku &&
                      pl.Group1 != "Archive"
                      select new
                      {
                          s.Key_id,
                          s.product_id
                      };


        //var results = from s in context.PRODUCT_LIST
        //              where s.product_id == sku
        //              select new
        //              {
        //                  s.Key_id,
        //                  s.product_id
        //              };
        return results.ToList()
            .Select(s => new ssc1200028_Search()
            {
                Key_id = s.Key_id,
                ProductID = s.product_id
            }).ToList();
    }

    public List<ssc1200028_Search> SearchSnapshotsBySKU(string sku, ssc1200028_ssc_spacemanEntities1 context)
    {
        var results = from snapshots in context.ssc_assortment_change_snapshot
                      join details in context.ssc_assortment_change_snapshot_detail
                      on snapshots.snapshot_id equals details.snapshot_id
                      where details.product_id == sku
                      select new
                      {
                          snapshots.snapshot_id,
                          snapshots.snapshot_nbr,
                          snapshots.snapshot_desc
                      };

        return results.ToList()
            .Select(s => new ssc1200028_Search()
            {
                SnapshotID = s.snapshot_id,
                SnapshotDescription = s.snapshot_desc,
                SnapshotNumber = s.snapshot_nbr
            }).ToList();

        //return results;
    }

    public IQueryable SearchBySeason(string season, int id)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var resultsBySeason = from snapshots in context.ssc_assortment_change_snapshot
                              where snapshots.snapshot_season == season &&
                              snapshots.snapshot_id != id
                              select snapshots;
        return resultsBySeason;
    }

    public IQueryable SearchByLocation(string location, int id)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var resultsByLocation = from snapshots in context.ssc_assortment_change_snapshot
                                where snapshots.snapshot_location == location &&
                                snapshots.snapshot_id != id
                                select snapshots;
        return resultsByLocation;
    }

    public IQueryable SearchByYear(int year)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var resultsByLocation = from snapshots in context.ssc_assortment_change_snapshot
                                where snapshots.snapshot_year == year
                                select new { snapshots.snapshot_id, snapshots.snapshot_nbr, snapshots.snapshot_desc };
        return resultsByLocation;
    }

    public IQueryable SearchByDescription(string desc, int id)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var resultsByDescription = from snapshots in context.ssc_assortment_change_snapshot
                                   where 
                                   snapshots.snapshot_id != id &&
                                   snapshots.snapshot_desc.Contains(desc)
                                   select new { snapshots.snapshot_id, snapshots.snapshot_nbr, snapshots.snapshot_desc };
        return resultsByDescription;
    }

    public IQueryable SearchBySize(int size, int id)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var resultsBySize = from snapshots in context.ssc_assortment_change_snapshot
                            where snapshots.snapshot_size == size &&
                            snapshots.snapshot_id != id
                            select new { snapshots.snapshot_id, snapshots.snapshot_nbr, snapshots.snapshot_desc };
        return resultsBySize;
    }

    
    public IQueryable GetDiscontinuedProductsBySnapshotID(int snapshotid)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var discontinuedProducts = (from s in context.ssc_assortment_change_snapshot_detail
                                    where s.snapshot_id == snapshotid &&
                                    s.product_status.Contains("Discontinued")
                                    select new { s.detail_id, s.product_id, s.product_desc });
        return discontinuedProducts;
    }


    public IQueryable GetReplacedProductsBySnapshotID(int snapshotid)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var replacedProducts = (from s in context.ssc_assortment_change_snapshot_detail
                                join d in context.PRODUCTs
                                on s.product_id equals d.product_id
                                where s.snapshot_id == snapshotid &&
                                s.product_status == "Replaced" &&
                                s.product_repl_prod != null
                                select new { s.detail_id, s.product_id, s.product_desc, s.product_repl_prod, s.product_repl_prod_desc, d.name });
        return replacedProducts;
    }


    private IEnumerable<ssc_assortment_change_snapshot> SearchReplaceSKUsByExitStrategy(string productstrategy)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        IEnumerable<ssc_assortment_change_snapshot> results = (IEnumerable<ssc_assortment_change_snapshot>) from snapshotDetails in context.ssc_assortment_change_snapshot_detail
                      where snapshotDetails.product_status == "Replaced" &&
                      snapshotDetails.product_strategy == productstrategy
                      select snapshotDetails;

        return results;
    }



	public ssc1200028_Search()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}