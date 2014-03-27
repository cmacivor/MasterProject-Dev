using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ssc1200028_ssc_spacemanModel;

/// <summary>
/// Summary description for ssc1200028_AssortmentPlanningBL
/// </summary>
public class ssc1200028_AssortmentPlanningBL
{
    
    //When saving changes to a SKU, need to handle if it is an Active SKU or whether it's been flagged as Replaced or Discontinued
    public void UpdateSKUStatus(ssc1200028_ssc_spacemanEntities1 context, string replacementSKU, string replacementProdUPC, string replacSKUdesc, string replProdStrategy, string statusValue, string discontinueExitStrategy, string discontinueComments, string discontinuedBy)
    {
        //first get the details of the selected SKU and check 
        ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        detail.Products = detail.GetDetailRecordByDetailID(context);
        var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                               where s.detail_id == detail.DetailSessionID
                               select s).SingleOrDefault();
        //if status is Active, then this is an original record
        if (detail.Products.SingleOrDefault().Status == "Active")
        {
            
            if (selectedProduct != null)
            {
                selectedProduct.product_status = statusValue;
                selectedProduct.exit_strategy = discontinueExitStrategy;
                selectedProduct.discontinued_comments = discontinueComments;
                selectedProduct.discontinued_by = discontinuedBy;
                context.SaveChanges();
            }
        }
        //
        if (detail.Products.SingleOrDefault().Status == "Discontinued")
        {
            //selectedProduct.product_status = statusValue;
            //selectedProduct.exit_strategy = discontinueExitStrategy;
            //selectedProduct.discontinued_comments = discontinueComments;
            //selectedProduct.discontinued_by = discontinuedBy;
            //context.SaveChanges();
        }
        //need to handle this differently - 
        if (detail.Products.SingleOrDefault().Status == "Replaced")
        {
            //two branches here -if the status they selected is Discontinued:
            if (statusValue == "Discontinued")
            {
                //selectedProduct.product_repl_status = statusValue;
                //selectedProduct.exit_strategy = discontinueExitStrategy;
                //selectedProduct.discontinued_comments = discontinueComments;
                //selectedProduct.discontinued_by = discontinuedBy;
            }
            if (statusValue == "Replaced")
            {
                //selectedProduct.product_repl_prod = replacementSKU;
                //selectedProduct.product_repl_upc = replacementProdUPC;
                //selectedProduct.product_repl_prod_desc = replacSKUdesc;
                //selectedProduct.product_repl_status = "Active";
                //selectedProduct.product_repl_strategy = replProdStrategy;
                //context.SaveChanges();
            }
        }
    }

    public void UpdateSKUStatusFromActiveToDiscontinued(ssc1200028_ssc_spacemanEntities1 context, string statusValue, string discontinueExitStrategy, string discontinueComments, string discontinuedBy)
    {
        ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        detail.Products = detail.GetDetailRecordByDetailID(context);
        var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                               where s.detail_id == detail.DetailSessionID
                               select s).SingleOrDefault();

        selectedProduct.product_status = statusValue;
        selectedProduct.exit_strategy = discontinueExitStrategy;
        selectedProduct.discontinued_comments = discontinueComments;
        selectedProduct.discontinued_by = discontinuedBy;
        context.SaveChanges();


    }

    public void UpdateSKUStatusFromActiveToReplaced(ssc1200028_ssc_spacemanEntities1 context, string productStatus, string exitStrategy, string newSKU, string newUPC, string newSKUDescription, string newItemStrategy)
    {
        ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        detail.Products = detail.GetDetailRecordByDetailID(context);
        var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                               where s.detail_id == detail.DetailSessionID
                               select s).SingleOrDefault();

        selectedProduct.product_status = productStatus;
        selectedProduct.exit_strategy = exitStrategy;
        selectedProduct.product_repl_prod = newSKU;
        selectedProduct.product_repl_upc = newUPC;
        selectedProduct.product_repl_prod_desc = newSKUDescription;
        selectedProduct.product_repl_status = "Active";
        selectedProduct.product_repl_strategy = newItemStrategy;
        context.SaveChanges();
    }



    /// <summary>
    /// for updating a detail record when an original SKU is marked as "Replaced", and the user is now discontinuing the 
    /// Replacement SKU
    /// </summary>
    /// <param name="context"></param>
    public void UpdateSKUFromReplacedToDiscontinued(ssc1200028_ssc_spacemanEntities1 context, string statusValue, string discontinueExitStrategy, string discontinueComments, string discontinuedBy)
    {
        ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        detail.Products = detail.GetDetailRecordByDetailID(context);
        var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                               where s.detail_id == detail.DetailSessionID
                               select s).SingleOrDefault();

        selectedProduct.product_repl_status = statusValue;
        selectedProduct.exit_strategy = discontinueExitStrategy;
        selectedProduct.discontinued_comments = discontinueComments;
        selectedProduct.discontinued_by = discontinuedBy;
        context.SaveChanges();
    }

    /// <summary>
    /// for updating a detail record when an original SKU is already marked as "Replaced" and the user is updating
    /// the sku with another replacement SKU.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="replacementSKU"></param>
    /// <param name="replacementProdUPC"></param>
    /// <param name="replacSKUdesc"></param>
    public void UpdateReplacedSKUStatus(ssc1200028_ssc_spacemanEntities1 context, string replacementSKU, string replacementProdUPC, string replacSKUdesc, string replProdStrategy)
    {
        ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
        detail.Products = detail.GetDetailRecordByDetailID(context);
        var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                               where s.detail_id == detail.DetailSessionID
                               select s).SingleOrDefault();

        selectedProduct.product_repl_prod = replacementSKU;
        selectedProduct.product_repl_upc = replacementProdUPC;
        selectedProduct.product_repl_prod_desc = replacSKUdesc;
        selectedProduct.product_repl_status = "Active";
        selectedProduct.product_repl_strategy = replProdStrategy;
        context.SaveChanges();
    }




    //need a method to return false when no records are found by given planogram name

    public bool CheckForPlanogramBySKU(ssc1200028_ssc_spacemanEntities1 context, string sku)
    {
        ssc1200028_Search search = new ssc1200028_Search();
        search.Planograms = search.SearchPlanogramsBySKU(context, sku);
        if (search.Planograms.Any())
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// This checks to make sure the selected key ID is in a list of Planograms returned by the query that populates the Search drop down
    /// </summary>
    /// <param name="context"></param>
    /// <param name="keyid"></param>
    /// <returns></returns>
    public bool CheckForActivePlanogram(ssc1200028_ssc_spacemanEntities1 context, int keyid)
    {
        ssc_spaceman_PLANO_KEY planoKey = new ssc_spaceman_PLANO_KEY();
        List<ssc_spaceman_PLANO_KEY> validKeys = new List<ssc_spaceman_PLANO_KEY>();
        validKeys = planoKey.GetPlanograms();

        var key = from v in validKeys
                  where v.Key_id == keyid
                  select v;
        if (key.Any())
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// //need to get Snapshots by name if entered Key ID is valid
    /// </summary>
    /// <param name="context"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsByValidKeyID(ssc1200028_ssc_spacemanEntities1 context, int keyid)
    {
        ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT DL = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();
        List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
        //bool isNamevalid = CheckForValidKeyID(context, keyid);
        bool isNamevalid = CheckForActivePlanogram(context, keyid);
        if (isNamevalid == true)
        {
            snapshots = DL.GetSnapshotsByKeyID(context, keyid);
            return snapshots;
        }
        return snapshots;
    }


    //Methods to compare planograms in the Comparison tab
    //public IEnumerable<ssc_spaceman_SNAPSHOT_DETAIL> GetDifferenceSKUs(ssc_spacemanEntities1 context, int selectedSnapshotID)
    //{
    //    List<ssc_spaceman_SNAPSHOT_DETAIL> skusFromSelectedSearchResultSnapshot = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
    //    List<ssc_spaceman_SNAPSHOT_DETAIL> skusBySnapshotIDInSession = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
    //    ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
    //    //this is for the SKUs from the snapshot in the session- in the main grid
    //    //successfully tested - does get correct products 
    //    skusBySnapshotIDInSession = detail.GetActiveProductsBySnapshotSessionID(context);
    //    //this is for the SKUs in the snapshot that the user selected in the grid of search results
    //    //successfully tested
    //    skusFromSelectedSearchResultSnapshot = detail.GetActiveProductsBySnapshotID(context, selectedSnapshotID);
    //    //this throws a casting exception- cannot use .ToList() and cast 
    //    //SKUsOnlyInFirstSnapshot = (List<ssc_spaceman_SNAPSHOT_DETAIL>) skusBySnapshotIDInSession.ToList<ssc_spaceman_SNAPSHOT_DETAIL>().Except(skusFromSelectedSearchResultSnapshot);
    //    //IEnumerable<ssc_spaceman_SNAPSHOT_DETAIL> SKUSOnlyInFirstSnapshot = skusBySnapshotIDInSession.Except(skusFromSelectedSearchResultSnapshot);
    //    IEnumerable<ssc_spaceman_SNAPSHOT_DETAIL> SKUSOnlyInFirstSnapshot = skusBySnapshotIDInSession.Intersect(skusFromSelectedSearchResultSnapshot);
    //    return SKUSOnlyInFirstSnapshot.ToList<ssc_spaceman_SNAPSHOT_DETAIL>();
    //}

    //public IEnumerable<ssc_spaceman_SNAPSHOT_DETAIL> GetReverseDifferenceSKUs(ssc_spacemanEntities1 context, int selectedSnapshotID)
    //{
    //    List<ssc_spaceman_SNAPSHOT_DETAIL> skusFromSelectedSearchResultSnapshot = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
    //    List<ssc_spaceman_SNAPSHOT_DETAIL> skusBySnapshotIDInSession = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
    //    ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
    //    //this is for the SKUs from the snapshot in the session- in the main grid
    //    //successfully tested - does get correct products 
    //    skusBySnapshotIDInSession = detail.GetActiveProductsBySnapshotSessionID(context);
    //    //this is for the SKUs in the snapshot that the user selected in the grid of search results
    //    //successfully tested
    //    skusFromSelectedSearchResultSnapshot = detail.GetActiveProductsBySnapshotID(context, selectedSnapshotID);
    //    IEnumerable<ssc_spaceman_SNAPSHOT_DETAIL> reverseDiffSkus = skusFromSelectedSearchResultSnapshot.Except(skusBySnapshotIDInSession);
    //    //IEnumerable<ssc_spaceman_SNAPSHOT_DETAIL> reverseDiffSkus = skusFromSelectedSearchResultSnapshot.Intersect(skusBySnapshotIDInSession);
    //    return reverseDiffSkus.ToList<ssc_spaceman_SNAPSHOT_DETAIL>();
    //}




    //public bool CheckForValidPlanogramName(ssc_spacemanEntities1 context, string enteredPlanogramName)
    //{
    //    var planoKey = new ssc_spaceman_PLANO_KEY();
    //    planoKey.PlanogramNames = planoKey.GetPlanogramsByName(context, enteredPlanogramName);
    //    if (planoKey.PlanogramNames.Any())
    //    {
    //        return true;
    //    }
    //    return false;
    //}


    /// <summary>
    ///  // to get Snapshots by name and key id if entered planogram name is valid
    /// </summary>
    /// <param name="context"></param>
    /// <param name="name"></param>
    /// <param name="keyid"></param>
    /// <returns></returns>
    //public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsByValidPlanogramName(ssc_spacemanEntities1 context, string name, int keyid)
    //{
    //    ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT DL = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();
    //    List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
    //    bool isNamevalid = CheckForValidPlanogramName(context, name);
    //    if (isNamevalid == true)
    //    {
    //        snapshots = DL.GetSnapshotsByPlanogramNameAndKeyID(context, name, keyid);
    //        return snapshots;
    //    }
    //    return snapshots;
    //}

    /// <summary>
    /// //need to get Snapshots by name if entered planogram name is valid
    /// </summary>
    /// <param name="context"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    //public List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> GetSnapshotsByValidPlanogramName(ssc_spacemanEntities1 context, string name)
    //{
    //    ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT DL = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();
    //    List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshots = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
    //    bool isNamevalid = CheckForValidPlanogramName(context, name);
    //    if (isNamevalid == true)
    //    {
    //        snapshots = DL.GetSnapshotsByPlanogramName(context, name);
    //        return snapshots;
    //    }
    //    return snapshots;
    //}

    

	public ssc1200028_AssortmentPlanningBL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}