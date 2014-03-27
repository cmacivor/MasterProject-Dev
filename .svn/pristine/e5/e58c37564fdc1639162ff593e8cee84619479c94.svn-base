using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ssc1200028_ssc_spacemanModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ssc_spaceman_SNAPSHOT_DETAIL
/// </summary>
public class ssc_spaceman_SNAPSHOT_DETAIL
{
    public int DetailID { get; set; }
    public int SnapshotID { get; set; }
    public string ProductID { get; set; }
    public string UPC { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    //New Item Strategy maps to the product_strategy field- this is populated when a new SKU is added. 
    public string NewItemStrategy { get; set; }
    public string ReplacementProduct { get; set; }
    public string ReplacementProductDescription { get; set; }
    public string ReplacementProductStatus { get; set; }
    //ReplacementProductStrategy maps to product_repl_strategy
    public string ReplacementProductStrategy { get; set; }
    public string FacingChange { get; set; }
    public int FacingQuantity { get; set; }
    public string ExitStrategy { get; set; }
    public string DiscontinuedComments { get; set; }
    public string DiscontinuedBy { get; set; }

    public List<ssc_spaceman_SNAPSHOT_DETAIL> Products { get; set; }

    private const string SnapshotIDSession = "snapshotID";
    private const string detailIDSession = "detailID";
    private const string matchingSnapshotID = "MatchingSnapshotID";
    private int selectedSnapshot;
    private int selectedDetail;
    private int selectedMatchingSnapshotID;

    /// <summary>
    /// This handles the session that's instantiated when a user selects a search result on the Comparison tab
    /// </summary>
    public int MatchingSnapshotID
    {
        get
        {
            if (HttpContext.Current.Session[matchingSnapshotID] != null)
            {
                selectedDetail = Convert.ToInt32(HttpContext.Current.Session[matchingSnapshotID]);
                return selectedDetail;
                //return HttpContext.Current.Session[SnapshotIDSession] as string;
            }
            else
            {
                return selectedDetail;
            }
        }
        set
        {
            HttpContext.Current.Session[matchingSnapshotID] = value;
        }
    }

    /// <summary>
    /// This handles the session created when a record is selected from the main grid
    /// </summary>
    public int DetailSessionID
    {
        get
        {
            if (HttpContext.Current.Session[detailIDSession] != null)
            {
                selectedDetail = Convert.ToInt32(HttpContext.Current.Session[detailIDSession]);
                return selectedDetail;
                //return HttpContext.Current.Session[SnapshotIDSession] as string;
            }
            else
            {
                return selectedDetail;
            }
        }
        set
        {
            HttpContext.Current.Session[detailIDSession] = value;
        }
    }

    /// <summary>
    /// This holds the session ID selected on the Home page- used to populate the main grid
    /// </summary>
    public int SnapshotSessionID
    {
        get
        {
            if (HttpContext.Current.Session[SnapshotIDSession] != null)
            {
                selectedSnapshot = Convert.ToInt32(HttpContext.Current.Session[SnapshotIDSession]);
                return selectedSnapshot;
                //return HttpContext.Current.Session[SnapshotIDSession] as string;
            }
            else
            {
                return selectedSnapshot;
            }
        }
        set
        {
            HttpContext.Current.Session[SnapshotIDSession] = value;
        }
    }

    public void UpdateFacingStatus(ssc1200028_ssc_spacemanEntities1 context, ssc_spaceman_SNAPSHOT_DETAIL detailObject)
    {
        var detail = (from d in context.ssc_assortment_change_snapshot_detail
                      where d.detail_id == detailObject.DetailID
                      select d).SingleOrDefault();
        detail.facing_change = detailObject.FacingChange;
        detail.facing_quantity = detailObject.FacingQuantity;
        context.SaveChanges();
    }

    public List<ssc_spaceman_SNAPSHOT_DETAIL> GetDetailRecordByDetailID(ssc1200028_ssc_spacemanEntities1 context)
    {
        var detail = from p in context.ssc_assortment_change_snapshot_detail
                     where p.detail_id == DetailSessionID
                     select new
                     {
                         p.detail_id,
                         p.snapshot_id,
                         p.product_id,
                         p.product_upc,
                         p.product_desc,
                         p.product_status,
                         p.product_strategy,
                         p.product_repl_prod,
                         p.product_repl_prod_desc,
                         p.product_repl_status,
                         p.product_repl_strategy,
                         p.facing_change,
                         p.facing_quantity,
                         p.exit_strategy,
                         p.discontinued_comments,
                         p.discontinued_by
                     };

        return detail.ToList()
           .Select(p => new ssc_spaceman_SNAPSHOT_DETAIL()
           {
               DetailID = p.detail_id,
               SnapshotID = p.snapshot_id,
               ProductID = p.product_id,
               UPC = p.product_upc,
               Description = p.product_desc,
               Status = p.product_status,
               NewItemStrategy = p.product_strategy,
               ReplacementProduct = p.product_repl_prod,
               ReplacementProductDescription = p.product_repl_prod_desc,
               ReplacementProductStatus = p.product_repl_status,
               ReplacementProductStrategy = p.product_repl_strategy,
               FacingChange = p.facing_change,
               FacingQuantity = Convert.ToInt32(p.facing_quantity),
               ExitStrategy = p.exit_strategy,
               DiscontinuedComments = p.discontinued_comments,
               DiscontinuedBy = p.discontinued_by
           }).ToList();
    }

    public List<ssc_spaceman_SNAPSHOT_DETAIL> GetProductBySnapshotIDAndSKU(ssc1200028_ssc_spacemanEntities1 context, string sku)
    {
        //ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        var detail = from p in context.ssc_assortment_change_snapshot_detail
                     where p.snapshot_id == SnapshotSessionID //&&
                     //p.product_status == "Active" && 
                   &&  p.product_id == sku
                     select new
                     {
                         p.detail_id,
                         p.snapshot_id,
                         p.product_id,
                         p.product_upc,
                         p.product_desc,
                         p.product_status,
                         p.product_strategy,
                         p.product_repl_prod,
                         p.product_repl_prod_desc,
                         p.product_repl_status,
                         p.product_repl_strategy,
                         p.facing_change,
                         p.facing_quantity,
                         p.exit_strategy,
                         p.discontinued_comments,
                         p.discontinued_by
                     };

        return detail.ToList()
            .Select(p => new ssc_spaceman_SNAPSHOT_DETAIL()
            {
                DetailID = p.detail_id,
                SnapshotID = p.snapshot_id,
                ProductID = p.product_id,
                UPC = p.product_upc,
                Description = p.product_desc,
                Status = p.product_status,
                NewItemStrategy = p.product_strategy,
                ReplacementProduct = p.product_repl_prod,
                ReplacementProductDescription = p.product_repl_prod_desc,
                ReplacementProductStatus = p.product_repl_status,
                ReplacementProductStrategy = p.product_repl_strategy,
                FacingChange = p.facing_change,
                FacingQuantity = Convert.ToInt32(p.facing_quantity),
                ExitStrategy = p.exit_strategy,
                DiscontinuedComments = p.discontinued_comments,
                DiscontinuedBy = p.discontinued_by
            }).ToList();
    }

    

    /// <summary>
    /// This gets detail records by the snapshot ID held in the session. 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public List<ssc_spaceman_SNAPSHOT_DETAIL> GetProductsBySnapshotID(ssc1200028_ssc_spacemanEntities1 context)
    {
        var products = from p in context.ssc_assortment_change_snapshot_detail
                       where p.snapshot_id == SnapshotSessionID
                       orderby p.product_id //&&
                       //(p.product_status == null ||
                       //p.product_status == "")
                       select new
                       { 
                         p.detail_id,
                         p.snapshot_id,
                         p.product_id,
                         p.product_upc,
                         p.product_desc,
                         p.product_status,
                         p.product_strategy,
                         p.product_repl_prod,
                         p.product_repl_prod_desc,
                         p.product_repl_status,
                         p.product_repl_strategy,
                         p.facing_change,
                         p.facing_quantity,
                         p.exit_strategy,
                         p.discontinued_comments,
                         p.discontinued_by
                       };

        return products.ToList()
            .Select(p => new ssc_spaceman_SNAPSHOT_DETAIL()
            {
                DetailID = p.detail_id,
                SnapshotID = p.snapshot_id,
                ProductID = p.product_id,
                UPC = p.product_upc,
                Description = p.product_desc,
                Status = p.product_status,
                NewItemStrategy = p.product_strategy,
                ReplacementProduct = p.product_repl_prod,
                ReplacementProductDescription = p.product_repl_prod_desc,
                ReplacementProductStatus = p.product_repl_status,
                ReplacementProductStrategy = p.product_repl_strategy,
                FacingChange = p.facing_change,
                FacingQuantity = Convert.ToInt32(p.facing_quantity),
                ExitStrategy = p.exit_strategy,
                DiscontinuedComments = p.discontinued_comments,
                DiscontinuedBy = p.discontinued_by
            }).ToList();
        
    }

    //public List<ssc_spaceman_SNAPSHOT_DETAIL> GetActiveProductsBySnapshotSessionID(ssc_spacemanEntities1 context)
    //{
    //    List<ssc_spaceman_SNAPSHOT_DETAIL> activeSKUs = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
    //    activeSKUs = (from p in context.ssc_assortment_change_snapshot_detail
    //                  where p.snapshot_id == SnapshotSessionID &&
    //                  p.product_status == "Active"
    //                  orderby p.product_id
    //                  select new ssc_spaceman_SNAPSHOT_DETAIL
    //                  {
    //                      //DetailID = p.detail_id,
    //                      //SnapshotID = p.snapshot_id,
    //                      ProductID = p.product_id,
    //                      //UPC = p.product_upc,
    //                      Description = p.product_desc,
    //                      //Status = p.product_status,
    //                      //NewItemStrategy = p.product_strategy,
    //                      //ReplacementProduct = p.product_repl_prod,
    //                      //ReplacementProductDescription = p.product_repl_prod_desc,
    //                      //ReplacementProductStatus = p.product_repl_status,
    //                      //ReplacementProductStrategy = p.product_repl_strategy,
    //                      //FacingChange = p.facing_change,
    //                      //FacingQuantity = p.facing_quantity,
    //                      //ExitStrategy = p.exit_strategy,
    //                      //DiscontinuedComments = p.discontinued_comments,
    //                      //DiscontinuedBy = p.discontinued_by
    //                  }).ToList();
    //    return activeSKUs;
    //}

    public List<ssc_spaceman_SNAPSHOT_DETAIL> GetActiveProductsBySnapshotID(ssc1200028_ssc_spacemanEntities1 context, int snapshotid)
    {
        List<ssc_spaceman_SNAPSHOT_DETAIL> detailsBySnapshotID = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
        detailsBySnapshotID = (from p in context.ssc_assortment_change_snapshot_detail
                               where p.snapshot_id == snapshotid &&
                               p.product_status == "Active"
                               orderby p.product_id
                               select new ssc_spaceman_SNAPSHOT_DETAIL
                               {
                                   //DetailID = p.detail_id,
                                   //SnapshotID = p.snapshot_id,
                                   ProductID = p.product_id,
                                   //UPC = p.product_upc,
                                   Description = p.product_desc,
                                   //Status = p.product_status,
                                   //NewItemStrategy = p.product_strategy,
                                   //ReplacementProduct = p.product_repl_prod,
                                   //ReplacementProductDescription = p.product_repl_prod_desc,
                                   //ReplacementProductStatus = p.product_repl_status,
                                   //ReplacementProductStrategy = p.product_repl_strategy,
                                   //FacingChange = p.facing_change,
                                   //FacingQuantity = p.facing_quantity,
                                   //ExitStrategy = p.exit_strategy,
                                   //DiscontinuedComments = p.discontinued_comments,
                                   //DiscontinuedBy = p.discontinued_by
                               }).ToList();
        return detailsBySnapshotID;
    }

    public List<ssc_spaceman_SNAPSHOT_DETAIL> GetIntersectOfFirstAndSecondSnapshots(ssc1200028_ssc_spacemanEntities1 context, int sessionSnapshotID, int selectedSnapshotID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select product_id as ProductID, product_desc as Description from ssc_assortment_change_snapshot_detail ");
        sb.Append("where snapshot_id = @sessionSnapshotID ");
        sb.Append("and product_status = 'Active' ");
        sb.Append("intersect ");
        sb.Append("select product_id as ProductID, product_desc as Description from ssc_assortment_change_snapshot_detail ");
        sb.Append("where snapshot_id = @selectedSnapshotID ");
        sb.Append("and product_status = 'Active' ");
        sb.Append("order by product_id");

        SqlParameter paramSessionSnapID = new SqlParameter("@sessionSnapshotID", sessionSnapshotID);
        SqlParameter paramSelectedSnapID = new SqlParameter("@selectedSnapshotID", selectedSnapshotID);

        string sql = sb.ToString();
        
        var skus = context.ExecuteStoreQuery<ssc_spaceman_SNAPSHOT_DETAIL>(sql, paramSessionSnapID, paramSelectedSnapID).ToList();
        return skus;
        
    }

    public List<ssc_spaceman_SNAPSHOT_DETAIL> GetExceptOfSecondAndFirstSnapshots(ssc1200028_ssc_spacemanEntities1 context, int sessionSnapshotID, int selectedSnapshotID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select product_id as ProductID, product_desc as Description from ssc_assortment_change_snapshot_detail ");
        sb.Append("where snapshot_id = @selectedSnapshotID ");
        sb.Append("and product_status = 'Active' ");
        sb.Append("except ");
        sb.Append("select product_id as ProductID, product_desc as Description from ssc_assortment_change_snapshot_detail ");
        sb.Append("where snapshot_id = @sessionSnapshotID ");
        sb.Append("and product_status = 'Active' ");


        SqlParameter paramSessionSnapID = new SqlParameter("@sessionSnapshotID", sessionSnapshotID);
        SqlParameter paramSelectedSnapID = new SqlParameter("@selectedSnapshotID", selectedSnapshotID);

        string sql = sb.ToString();
        
        var skus = context.ExecuteStoreQuery<ssc_spaceman_SNAPSHOT_DETAIL>(sql, paramSelectedSnapID, paramSessionSnapID).ToList();
        return skus;
        
    }


	public ssc_spaceman_SNAPSHOT_DETAIL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}