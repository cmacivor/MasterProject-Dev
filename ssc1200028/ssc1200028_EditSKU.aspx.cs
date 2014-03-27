using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ssc1200028_ssc_spacemanModel;
using System.Web.Services;
using System.Collections;
using System.Text;
using System.Net.Mail;
//using ew = SoftArtisans.OfficeWriter.ExcelWriter;

public partial class EditSKU : System.Web.UI.Page
{
    public string ProductID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["snapshotID"] != null)
        {
            if (!Page.IsPostBack)
            {
                int snapshotID = Convert.ToInt32(Session["snapshotID"]);
                BindDiscontinuedGrid(snapshotID);
                BindDiscontinuedReplacedGrid(snapshotID);
                BindReplacementGrid(snapshotID);
                //List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT> snapshotList = new List<ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT>();
                ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT snapshot = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();
                //PopulateNewItemStrategyDropDown();
                using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
                {
                    snapshot.Snapshots = snapshot.GetSnapshotsBySnapshotID(context);
                    lblCurrentlySelectedSnapshot.Text = snapshot.Snapshots.SingleOrDefault().SnapshotDescription;
                    lblCurrentlySelectedSnapshotNumber.Text = snapshot.Snapshots.SingleOrDefault().SnapshotNumber;
                     
                    ssc_spaceman_SNAPSHOT_DETAIL skuDetails = new ssc_spaceman_SNAPSHOT_DETAIL();
                    skuDetails.Products = skuDetails.GetProductsBySnapshotID(context);

                    if (skuDetails.Products.Any())
                    {
                        BindGrid();
                    }
                    else
                    {
                        runjQueryCode("$('#divAddNewProduct').show();");
                        //need some kind of prompt to indicate that there are no SKU's for this snapshot
                        runjQueryCode("$('#divNoSkusMessage').show();");
                    }
                }
            }
        }
        else
        {
            //Response.Redirect("~/ssc1200028/ssc1200028_Home.aspx");
            lblError.Text = "The session has timed out. Please reselect the record you were last editing.";
            lblError.Visible = true;
        }
    }

    private void ShowExceptionMessage()
    {
        lblError.Text = "An an unknown error has occurred. Please contact Craig MacIvor and explain what you were doing and with which record(s).";
        lblError.Visible = true;
    }

    private void ShowTimeOutMessage()
    {
        lblError.Text = "The session has timed out. Please close this browser window and re-open the application to continue working.";
        lblError.Visible = true;
    }

    private void BindReplacementGrid(int snapshotid)
    {
        ssc1200028_Search search = new ssc1200028_Search();
        gdvReplacedOnly.DataSource = search.GetReplacedProductsBySnapshotID(snapshotid);
        gdvReplacedOnly.DataBind();
    }
        

    private void BindDiscontinuedReplacedGrid(int snapshotid)
    {
        ssc1200028_Search search = new ssc1200028_Search();
        gdvDiscontinuedReplaced.DataSource = search.GetReplacedProductsBySnapshotID(snapshotid);
        gdvDiscontinuedReplaced.DataBind();
    }

    private void BindDiscontinuedGrid(int snapshotid)
    {
        ssc1200028_Search search = new ssc1200028_Search();
        gdvDiscontinuedProducts.DataSource = search.GetDiscontinuedProductsBySnapshotID(snapshotid);
        gdvDiscontinuedProducts.DataBind();
    }


    private void BindGrid()
    {
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            ssc_spaceman_SNAPSHOT_DETAIL snapshotDetail = new ssc_spaceman_SNAPSHOT_DETAIL();
            ssc1200027_Utilities util = new ssc1200027_Utilities();
            DataView dtProducts = new DataView();
            //snapshotDetail.Products = snapshotDetail.GetActiveProductsBySnapshotID(context);
            //snapshotDetail.Products = snapshotDetail.GetProductsBySnapshotID(context);
            dtProducts = SortDataTable(GetProductDataTable(), true);
                

            var snapshotdesc = (from s in context.ssc_assortment_change_snapshot
                                where s.snapshot_id == snapshotDetail.SnapshotSessionID
                                select s).SingleOrDefault();
            lblCurrentlySelectedSnapshot.Text = snapshotdesc.snapshot_desc;

            //if (snapshotDetail.Products.Any())
            //{
            //    gdvProducts.DataSource = snapshotDetail.Products;
            //    gdvProducts.DataBind();
            //}
            if (dtProducts.Table.Rows.Count > 0)
            {
                gdvProducts.DataSource = dtProducts;
                gdvProducts.DataBind();
            }

            else
            {
                runjQueryCode("$('#divAddNewProduct').show();");
                //need some kind of prompt to indicate that there are no SKU's for this snapshot
                runjQueryCode("$('#divNoSkusMessage').show();");
            }
        }
    }

    protected void ddlDiscontinueSKUStatus_onSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDiscontinueSKUStatus.SelectedValue == "Replace this SKU")
        {
            divExitStrategy.Visible = true;
            divReplace.Visible = true;
            divDiscontinuedBy.Visible = false;
        }
        if (ddlDiscontinueSKUStatus.SelectedValue == "Discontinue Only")
        {
            divReplace.Visible = false;
            divExitStrategy.Visible = true;
            divDiscontinuedBy.Visible = true;
            divDiscontinueSKU.Visible = true;
        }
    }

    protected void gdvProducts_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            string product;
            int detailID = Convert.ToInt32(gdvProducts.DataKeys[e.NewSelectedIndex].Value);
            Session["detailID"] = detailID;
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                var productID = (from p in context.ssc_assortment_change_snapshot_detail
                                 where p.detail_id == detailID
                                 select p).SingleOrDefault();
                product = productID.product_id.ToString();

                

                //populate the fields in divDiscontinueSKU- check first if record is discontinued or replaced- need to avoid NullReference exception
                if (productID.product_status == "Discontinued")
                {
                    divDiscontinueSKU.Visible = true;
                    ddlDiscontinueExitStrategy.SelectedValue = productID.exit_strategy.ToString();
                    ddlDiscontinuedBy.SelectedValue = productID.discontinued_by.ToString().Trim();
                    txtDiscontinueComments.Text = productID.discontinued_comments.ToString();
                    //ddlDiscontinueSKUStatus.Value = "SelectStatus";
                    divExitStrategy.Visible = true;
                    divDiscontinuedBy.Visible = true;
                    lblCurrentStatus.Text = productID.product_status.ToString().Trim();
                    txtSkuChange.Text = productID.product_id.ToString().Trim();
                    divReplace.Visible = false;
                    btnSaveSKUChange.Enabled = false;
                    lblDiscontinueError.Text = "This SKU's status can no longer be changed.";
                    lblDiscontinueError.Visible = true;
                    //ddlDiscontinueSKUStatus.Enabled = false;
                }

                //if they select a record with a status of Replaced, we will want to get the replacement SKU
                if (productID.product_status == "Replaced")
                {
                    divDiscontinueSKU.Visible = true;
                    txtSkuChange.Text = productID.product_id.ToString().Trim();
                    ddlDiscontinueExitStrategy.SelectedValue = productID.exit_strategy.ToString().Trim();
                    
                    //this product hasn't been discontinued, so we don't populate the dddlDiscontinueSKUStatus drop down
                    
                    lblCurrentStatus.Text = productID.product_status.ToString().Trim();
                    txtNewSKU.Text = productID.product_repl_prod.ToString().Trim();
                    txtUPCReplace.Text = productID.product_repl_upc.ToString().Trim();
                    txtDescriptionReplace.Text = productID.product_repl_prod_desc.ToString().Trim();
                    txtNewItemStrategyReplace.Text = productID.product_repl_strategy.ToString().Trim();

                    btnSaveSKUChange.Enabled = false;
                    divReplace.Visible = true;
                    divExitStrategy.Visible = true;
                    divDiscontinuedBy.Visible = false;
                    btnSaveSKUChange.Enabled = false;
                    //ddlDiscontinueSKUStatus.Enabled = false;
                    lblDiscontinueError.Text = "This SKU's status can no longer be changed.";
                    lblDiscontinueError.Visible = true;
                }
                if (productID.product_status == "Active")
                {
                    btnSaveSKUChange.Enabled = true;
                    divDiscontinueSKU.Visible = true;
                    lblCurrentStatus.Text = productID.product_status.ToString().Trim();
                    ddlDiscontinueExitStrategy.SelectedValue = "Please Select";

                    ddlDiscontinueSKUStatus.SelectedValue = "Select Status";
                    txtDiscontinueComments.Text = "";
                    ddlDiscontinuedBy.SelectedValue = "Please Select";
                    txtNewSKU.Text = "";
                    txtUPCReplace.Text = "";
                    txtDescriptionReplace.Text = "";
                    txtNewItemStrategyReplace.Text = "";
                    txtSkuChange.Text = productID.product_id.ToString().Trim();
                    lblDiscontinueError.Visible = false;
                    divExitStrategy.Visible = false;
                    divDiscontinuedBy.Visible = false;
                    divReplace.Visible = false;
                }

                divAction.Visible = true;
                divChangeReport.Visible = false;
                divAddNewProduct.Visible = false;
                runjQueryCode("$('#divDiscontinueSKU').show();");
                //txtOldSKU.Text = productID.product_id.ToString();
                //}
            }
        }
        catch (Exception ex)
        {
            ShowExceptionMessage();
        }
    }

    [System.Web.Services.WebMethod]
    public static void AcceptFacingStatus(string sku, string status)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        int detailID = Convert.ToInt32(HttpContext.Current.Session["detailID"]);
        var record = (from s in context.ssc_assortment_change_snapshot_detail
                  where s.product_id == sku &&
                  s.detail_id == detailID
                  select s).SingleOrDefault();
        if (record != null)
        {
            record.product_status = status;
            context.SaveChanges();
        }
        
        //return "Data Submitted";
    }

    protected void btnSaveFacingStatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFacing.SelectedValue != "Please Select" &&  !String.IsNullOrEmpty(txtAdjustSKU.Text) && ddlFacingQuantity.SelectedValue != "Please Select")
            {
                divFacingError.Visible = false;
                lblFacingError.Visible = false;
                using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
                {
                    ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
                    List<ssc_spaceman_SNAPSHOT_DETAIL> selectedProduct = new List<ssc_spaceman_SNAPSHOT_DETAIL>();
                    selectedProduct = detail.GetProductBySnapshotIDAndSKU(context, txtAdjustSKU.Text);
                    if (selectedProduct.Any())
                    {
                        detail = selectedProduct.SingleOrDefault<ssc_spaceman_SNAPSHOT_DETAIL>();
                        detail.FacingChange = ddlFacing.SelectedValue;
                        detail.FacingQuantity = Convert.ToInt32(ddlFacingQuantity.SelectedValue);
                        detail.UpdateFacingStatus(context, detail);
                        ShowMenuChoice();
                        lblFacingError.Visible = false;
                        ssc1200027_Utilities util = new ssc1200027_Utilities();
                        util.runjQueryCode("alert('The facing quantity for this SKU has been updated.');", this);
                        divSKUDetails.Visible = false;
                        ddlFacing.SelectedValue = "PleaseSelect";
                        txtAdjustSKU.Text = "";

                    }
                    else
                    {
                        lblFacingError.Text = "This appears to be an invalid SKU. Please enter a SKU from the selected snapshot.";
                        lblFacingError.Visible = true;
                        divFacingError.Visible = true;
                    }
                }
                
            }
            else
            {
                divFacingError.Visible = true;
                lblFacingError.Text = "Please select a Facing status, Facing Quantity, and enter a SKU from the snapshot.";
                lblFacingError.Visible = true;
                runjQueryCode("$('#divFacingAdjustment').show();");
                runjQueryCode("$('#divbtnSaveFacingStatus').show();");
                ShowMenuChoice();
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }


    //protected void btnSaveFacingStatus_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlFacing.SelectedValue != "Please Select")
    //        {
    //            divFacingError.Visible = false;
    //            lblFacingError.Visible = false;
    //            using (ssc_spacemanEntities1 context = new ssc_spacemanEntities1())
    //            {
    //                //if (Session["snapshotID"] != null)
    //                //{

    //                    //int snapshotID = Convert.ToInt32(Session["snapshotID"]);
    //                    //var products = from p in context.ssc_assortment_change_snapshot_detail
    //                    //               where p.snapshot_id == snapshotID &&
    //                    //               (p.product_status == null ||
    //                    //               p.product_status == "")
    //                    //               select p;
    //               ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
    //               //ssc_spaceman_SNAPSHOT_DETAIL selectedProduct = new ssc_spaceman_SNAPSHOT_DETAIL();
    //               detail = detail.GetProductBySnapshotIDAndSKU(context, txtAdjustSKU.Text);
                   
    //               //activeProductDetails = detail.GetActiveProductsBySnapshotID(context);
    //               //     if (activeProductDetails.Any())
    //               //     {
    //               //         var product = (from p in activeProductDetails
    //               //                        where p.ProductID == txtAdjustSKU.Text
    //               //                        select p).SingleOrDefault();
    //                        detail.FacingChange = ddlFacing.SelectedValue;
    //                        detail.FacingQuantity = Convert.ToInt32(ddlFacingQuantity.SelectedValue);
    //                        //context.SaveChanges();
    //                        detail.UpdateFacingStatus(context, detail);
    //                        ShowMenuChoice();
    //                    }
    //                    ssc1200027_Utilities util = new ssc1200027_Utilities();
    //                    util.runjQueryCode("alert('The facing quantity for this SKU has been updated.');", this);
    //                //}
    //                //else
    //                //{
    //                //    ShowTimeOutMessage();
    //                //}
    //            //}
    //        }
    //        else
    //        {
    //            divFacingError.Visible = true;
    //            lblFacingError.Text = "Please select a Facing status.";
    //            lblFacingError.Visible = true;
    //            runjQueryCode("$('#divFacingAdjustment').show();");
    //            runjQueryCode("$('#divbtnSaveFacingStatus').show();");
    //            ShowMenuChoice();
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ShowExceptionMessage();
    //    }
    //}

    private void ShowMenuChoice()
    {
        string status = Request.Form["ddlExistingProductStatus"];
        if (status == "DiscontinueSKU")
        {
            runjQueryCode("$('#divDiscontinueSKU').show();");
        }
        if (status == "AddSKU")
        {
            runjQueryCode("$('#divAddNewProduct').show();");
        }
    }


    private void SaveSKUChange(string statusValue)
    {
        try
        {
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                if (Session["snapshotID"] != null)
                {
                    //var snapshotDetail = new ssc_assortment_change_snapshot_detail();
                    //snapshotDetail.snapshot_id = Convert.ToInt32(Session["snapshotID"]);
                    if (Session["detailID"] != null)
                    {
                        int detailID = Convert.ToInt32(Session["detailID"]);
                        var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                                               where s.detail_id == detailID
                                               select s).SingleOrDefault();
                        if (selectedProduct != null)
                        {
                            selectedProduct.product_status = statusValue;
                            selectedProduct.exit_strategy = ddlDiscontinueExitStrategy.SelectedValue;
                            selectedProduct.discontinued_comments = txtDiscontinueComments.Text;
                            selectedProduct.discontinued_by = ddlDiscontinuedBy.SelectedValue;
                            context.SaveChanges();
                        }
                    }
                    //need to update the same product in other snapshots with the same status value- disabled on 11/7, see below
                    //commenting out this code on 11/7 per the meeting- in order for comparisons across snapshots to take place,
                    //we shouldn't do this.

                    //var sameProducts = from s in context.ssc_assortment_change_snapshot_detail
                    //                   where s.product_id == selectedProduct.product_id
                    //                   select s;
                    //if (sameProducts.Any())
                    //{
                    //    foreach (var product in sameProducts)
                    //    {
                    //        product.product_status = statusValue;
                    //        product.product_strategy = ddlDiscontinueExitStrategy.SelectedValue;
                    //    }
                    //    context.SaveChanges();
                    //}
                }
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    
    //this is for discontinuing a SKU, or replacing it. If replaced, a new snapshot detail record will be created. ** commented out 
    //new approach on 1/23/2014- when the record is selected from the grid, the txtSKUChange textbox is populated with the Replacement SKU.

    protected void btnSaveSKUChange_Click(object sender, EventArgs e)
    {
        try
        {
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                ssc1200028_AssortmentPlanningBL bl = new ssc1200028_AssortmentPlanningBL();
                //string status = Request.Form["ddlDiscontinueSKUStatus"];
                if (ddlDiscontinueSKUStatus.SelectedValue == "Select Status")
                {
                    runjQueryCode("$('#divDiscontinueSKU').show();");
                    lblDiscontinueError.Text = "Please choose the status- whether you'd like to replace this SKU with another product or only discontinue it.";
                    lblDiscontinueError.Visible = true;
                }
                if (ddlDiscontinueSKUStatus.SelectedValue == "Discontinue Only")
                {
                    if (ddlDiscontinueExitStrategy.SelectedValue == "Please Select" || ddlDiscontinuedBy.SelectedValue == "Please Select")
                    {
                        lblDiscontinueError.Text = "Please make sure to choose your exit strategy and who is discontinuing the SKU.";
                        lblDiscontinueError.Visible = true;
                        runjQueryCode("$('#divDiscontinueSKU').show();");
                        runjQueryCode("$('#divDiscontinuedBy').show();");
                        runjQueryCode("$('#divExitStrategy').show();");
                        //runjQueryCode("$('.ddlDiscontinueSKUStatus').val('Discontinued');");
                        runjQueryCode("$('.ddlExistingProductStatus').val('DiscontinueSKU');");
                    }
                    else
                    {
                        //check to make sure the Comments field is within the character limit
                        if (txtDiscontinueComments.Text.Length <= 500)
                        {
                            if (Session["snapshotID"] != null)
                            {
                                int snapshotID = Convert.ToInt32(Session["snapshotID"]);

                                //to get the currently selected SKU
                                int detailID = Convert.ToInt32(Session["detailID"]);
                                    var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                                                           where s.detail_id == detailID
                                                           select s).SingleOrDefault();
                                    if (selectedProduct != null)
                                    {
                                        //need to handle if the SKU is original or not- is product_status Active, Replaced, or discontinued?
                                        
                                        if (selectedProduct.product_status == "Active")
                                        {
                                            //successfully tested on 1/24/2014
                                            bl.UpdateSKUStatusFromActiveToDiscontinued(context, "Discontinued", ddlDiscontinueExitStrategy.SelectedValue, txtDiscontinueComments.Text, ddlDiscontinuedBy.SelectedValue);

                                        }
                                        //if (selectedProduct.product_status == "Replaced")
                                        //{
                                        //    //to discontinue a replaced SKU
                                            
                                        //    bl.UpdateSKUFromReplacedToDiscontinued(context, "Discontinued", ddlDiscontinueExitStrategy.SelectedValue, txtDiscontinueComments.Text, ddlDiscontinuedBy.SelectedValue);
                                        //}
                                        //if (selectedProduct.product_status == "Discontinued")
                                        //{
                                        //    //need error message that the sku is already discontinued or something
                                        //}
                                    }
                                
                                //To discontinue an originally imported SKU
                                //SaveSKUChange(status);

                                //bl.UpdateSKUStatus(context, txtNewSKU.Text, status, ddlDiscontinueExitStrategy.SelectedValue, txtDiscontinueComments.Text, ddlDiscontinuedBy.SelectedValue);

                                //BindProductGrid();
                                BindGrid();
                                runjQueryCode("$('#divDiscontinueSKU').show();");
                                lblDiscontinueError.Text = "This SKU has been discontinued.";
                                lblDiscontinueError.Visible = true;
                                gdvProducts.SelectedRowStyle.Reset();
                                divSKUDetails.Visible = true;
                                divDiscontinueSKU.Visible = false;
                                BindDiscontinuedGrid(snapshotID);
                                BindReplacementGrid(snapshotID);
                                BindDiscontinuedReplacedGrid(snapshotID);
                            }
                            else
                            {
                                ShowTimeOutMessage();
                            }
                        }
                        else
                        {
                            lblDiscontinueError.Text = "The Comments field must be 500 characters or less.";
                            lblDiscontinueError.Visible = true;
                            runjQueryCode("$('#divDiscontinueSKU').show();");
                            //runjQueryCode("$('.ddlDiscontinueSKUStatus').val('Discontinued');");
                            //runjQueryCode("$('.ddlExistingProductStatus').val('DiscontinueSKU');");
                        }
                    }
                }

                if (ddlDiscontinueSKUStatus.SelectedValue == "Replace this SKU")
                {
                    if (ddlDiscontinueExitStrategy.SelectedValue == "Please Select" || string.IsNullOrWhiteSpace(txtUPCReplace.Text) || String.IsNullOrWhiteSpace(txtNewSKU.Text) || String.IsNullOrWhiteSpace(txtDescriptionReplace.Text))
                    {
                        //runjQueryCode("$('#divDiscontinueSKU').show();$('#divReplace').show();");
                        runjQueryCode("$('#divDiscontinueSKU').show();$('#divReplace').show();$('#divFacingEdit').show();$('#div1').show();$('#divbtnSaveFacingStatus').show();$('#divDiscontinuedBy').hide();$('#divFacingAdjustment').show();");
                        //runjQueryCode("$('#ddlDiscontinueSKUStatus').val('Replaced');");
                        lblDiscontinueError.Text = "All fields except the new item strategy are required.";
                        lblDiscontinueError.Visible = true;
                    }
                    else
                    {
                        //now check if the number of characters in New Item Strategy and Description is within limits
                        if (txtNewItemStrategyReplace.Text.Length <= 255 && txtDescriptionReplace.Text.Length <= 255)
                        {
                            //using (ssc_spacemanEntities1 context = new ssc_spacemanEntities1())
                            //{
                                if (Session["snapshotID"] != null)
                                {
                                    //var snapshotDetail = new ssc_assortment_change_snapshot_detail();
                                    int detailID = Convert.ToInt32(Session["detailID"]);
                                    var selectedProduct = (from s in context.ssc_assortment_change_snapshot_detail
                                                           where s.detail_id == detailID
                                                           select s).SingleOrDefault();
                                    if (selectedProduct != null)
                                    {
                                        //selectedProduct.product_status = status;
                                        //selectedProduct.exit_strategy = ddlDiscontinueExitStrategy.SelectedValue;
                                        //selectedProduct.product_repl_prod = txtNewSKU.Text;
                                        //selectedProduct.product_repl_prod_desc = txtDescriptionReplace.Text;
                                        ////selectedProduct.product_repl_strategy = ddlNewItemStrategyReplace.SelectedValue;
                                        //if (!String.IsNullOrWhiteSpace(txtNewItemStrategyReplace.Text))
                                        //{
                                        //    selectedProduct.product_repl_strategy = txtNewItemStrategyReplace.Text;
                                        //}



                                        //check what the product_status value is currently - if Active, it is the original SKU that's been imported
                                        //as per requirements from 1/8, the New Item Strategy is not a required field
                                        //successfully tested at 3:54 on 1/24/2013
                                        if (selectedProduct.product_status == "Active")
                                        {
                                            bl.UpdateSKUStatusFromActiveToReplaced(context, "Replaced", ddlDiscontinueExitStrategy.SelectedValue, txtNewSKU.Text, txtUPCReplace.Text, txtDescriptionReplace.Text, txtNewItemStrategyReplace.Text);
                                        }
                                        //test at 4:27 on 1/24-
                                        //if (selectedProduct.product_status == "Replaced")
                                        //{
                                        //    //to replace a SKU that has already been replaced- this will overwrite what was there before
                                        //    bl.UpdateReplacedSKUStatus(context, txtNewSKU.Text, txtUPCReplace.Text, txtDescriptionReplace.Text, txtNewItemStrategyReplace.Text);
                                        //}
                                        //if (selectedProduct.product_status == "Discontinued")
                                        //{
                                        //    runjQueryCode("alert('You cannot replace a SKU that has already been discontinued.');");
                                        //}
                                  

                                        //**Commenting this out on 1/9 per Thomas Ryan's comments that a new record isn't necessary
                                        //need to create a new SKU record here as well 
                                        //SaveNewSnapshotDetail(txtNewSKU.Text, txtUPCReplace.Text, txtDescriptionReplace.Text, txtNewItemStrategyReplace.Text, GetManualInv());
                                        divSKUDetails.Visible = true;
                                        divDiscontinueSKU.Visible = false;
                                        ddlDiscontinueExitStrategy.SelectedValue = "Please Select";
                                        txtNewSKU.Text = "";
                                        txtUPCReplace.Text = "";
                                        //ddlNewItemStrategyReplace.SelectedValue = "Please Select";
                                        txtNewItemStrategyReplace.Text = "";
                                        txtDescriptionReplace.Text = "";

                                        context.SaveChanges();
                                        BindGrid();
                                        gdvProducts.SelectedRowStyle.Reset();
                                        ssc1200027_Utilities util = new ssc1200027_Utilities();
                                        //util.runjQueryCode("alert('A new record has been created for the new SKU that was just entered.');", this);
                                    }

                                }
                                else
                                {
                                    ShowTimeOutMessage();
                                }
                            //}
                        }
                        //too many characters
                        else
                        {
                            runjQueryCode("$('#divDiscontinueSKU').show();$('#divReplace').show();$('#divFacingEdit').show();$('#div1').show();$('#divbtnSaveFacingStatus').show();$('#divDiscontinuedBy').hide();$('#divFacingAdjustment').show();");
                            //runjQueryCode("$('#ddlDiscontinueSKUStatus').val('Replaced');");
                            lblDiscontinueError.Text = "The New Item Strategy and Description fields cannot exceed 255 characters.";
                            lblDiscontinueError.Visible = true;
                        }
                    }
                }
                gdvProducts.SelectedRowStyle.Reset();
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }
    
    protected void btnSearchBySKU_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchBySKU.Text != "" || txtSearchBySKU.Text != null)
            {
                int snapshotID = Convert.ToInt32(Session["snapshotID"]);
                using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
                {
                    ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
                    detail.Products = detail.GetProductBySnapshotIDAndSKU(context, txtSearchBySKU.Text);
                    if (detail.Products.Any())
                    {
                        gdvProducts.DataSource = detail.Products;
                        gdvProducts.DataBind();
                        gdvProducts.SelectedRowStyle.Reset();
                        gdvProducts.Height = 70;
                    }
                }
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["snapshotID"] != null)
            {
                ssc1200027_Utilities util = new ssc1200027_Utilities();
                util.GenerateExcel(Convert.ToInt32(Session["snapshotID"]), this);
            }
            else
            {
                ShowTimeOutMessage();
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ssc1200028/ssc1200028_Home.aspx");
    }

    private void SaveNewSnapshotDetail(string productid, string upc, string productdesc, string strategy, int facing)
    {
        try
        {
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                if (Session["snapshotID"] != null)
                {
                    var snapshotDetail = new ssc_assortment_change_snapshot_detail();
                    snapshotDetail.snapshot_id = Convert.ToInt32(Session["snapshotID"]);
                    snapshotDetail.product_id = productid;
                    snapshotDetail.product_upc = upc;
                    snapshotDetail.product_desc = productdesc;
                    snapshotDetail.product_strategy = strategy;
                    snapshotDetail.facing_quantity = facing;
                    snapshotDetail.product_status = "Active";
                    context.AddTossc_assortment_change_snapshot_detail(snapshotDetail);
                    context.SaveChanges();
                    //BindProductGrid();
                    BindGrid();
                    txtDescription.Text = "";
                    txtNewItemStrategy.Text = "";
                    txtSKU.Text = "";
                    txtNewUPC.Text = "";
                    //ddlNewItemStrategy.SelectedValue = "Please Select";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "showDivAdd", "openAdd()");
                    CloseDialog("divAddNewProduct");
                    //upGrid.Update();
                }
                else
                {
                    ShowTimeOutMessage();
                }
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    /// <summary>
    /// Registers client script to close the dialog
    /// </summary>
    /// <param name="dialogId"></param>
    private void CloseDialog(string dialogId)
    {
        string script = string.Format(@"closeDialog('{0}')", dialogId);
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), UniqueID, script, true);
    }


    private int GetManualInv()
    {
        int facing = 0;
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            //get the keyID from snapshot
            if (Session["snapshotID"] != null)
            {
                int snapshotID = Convert.ToInt32(Session["snapshotID"]);
                var keyID = (from c in context.ssc_assortment_change_snapshot
                             where c.snapshot_id == snapshotID
                             select c).FirstOrDefault();
                if (keyID != null)
                {
                    var skus = (from s in context.PRODUCT_LIST
                                join p in context.PRODUCTs on s.product_id equals p.product_id
                                where s.Key_id == keyID.snapshot_keyid &&
                                s.product_id == txtSKU.Text
                                select new
                                {
                                    p.product_id,
                                    p.upc,
                                    p.name,
                                    s.Manual_Inv
                                }).FirstOrDefault();
                    if (skus != null)
                    {
                        facing = Convert.ToInt32(skus.Manual_Inv);
                        return facing;
                    }
                }
                return facing;
            }
            return facing;
        }
    }

    //this is for adding a new product
    //** From the 1/9 meeting - the new item strategy will come from the New Item Strategy textbox, and not the dropdown menu
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate("AddProduct");
            bool validSKU;
            if ((!Page.IsValid) || (String.IsNullOrWhiteSpace(txtSKU.Text) || String.IsNullOrWhiteSpace(txtDescription.Text)))
            {
                lblAddSKUError.Text = "The SKU, UPC, and Description fields are required.";
                lblAddSKUError.Visible = true;
                runjQueryCode("$('#divAddNewProduct').show();");
            }
            else
            {
                validSKU = IsSKUValid(txtSKU.Text);
                if (validSKU == true)
                {
                    //need to validate number of characters in the text box input fields
                    if (txtDescription.Text.Length <= 255 && txtNewItemStrategy.Text.Length <= 255)
                    {
                        SaveNewSnapshotDetail(txtSKU.Text, txtNewUPC.Text, txtDescription.Text, txtNewItemStrategy.Text, GetManualInv());
                        //CloseDialog("divAddNewProduct");
                        runjQueryCode("$('#divAddNewProduct').show();");
                        //BindProductGrid();
                        BindGrid();
                        ssc1200027_Utilities util = new ssc1200027_Utilities();
                        //util.runjQueryCode("alert('The SKU you just entered has been added.');", this);
                        lblInformation.Text = "The SKU " + txtSKU.Text + " you just entered has been added. Please change the Facing of another SKU, if applicable.";
                        //util.runjQueryCode("$('#divInformationMessage').delay(2000).fadeIn(1000);", this);
                        runjQueryCode("$('#divAddProductValidation').show();return false;");
                        //runjQueryCode("alert('The SKU " + txtSKU.Text + " has been successfully added to the snapshot.')");
                        //runjQueryCode("$('#divFacingAdjustment').fadeIn();");
                        divSKUDetails.Visible = true;
                        divAddNewProduct.Visible = false;
                    }
                    else
                    {
                        lblAddSKUError.Text = "Please shorten the New Item Strategy and Description to 255 characters or less.";
                        lblAddSKUError.Visible = true;
                        runjQueryCode("$('#divAddNewProduct').show();");
                    }
                }
                else
                {
                    lblAddSKUError.Text = "This is not a valid SKU.";
                    lblAddSKUError.Visible = true;
                    runjQueryCode("$('#divAddNewProduct').show();");
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error has occurred " + ex.Message;
            lblError.Visible = true;
        }
    }

    private string getjQueryCode(string jsCodetoRun)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("$(document).ready(function() {");
        sb.AppendLine(jsCodetoRun);
        sb.AppendLine(" });");

        return sb.ToString();
    }

    private void runjQueryCode(string jsCodetoRun)
    {

        ScriptManager requestSM = ScriptManager.GetCurrent(this);
        if (requestSM != null && requestSM.IsInAsyncPostBack)
        {
            ScriptManager.RegisterClientScriptBlock(this,
                                                    typeof(Page),
                                                    Guid.NewGuid().ToString(),
                                                    getjQueryCode(jsCodetoRun),
                                                    true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(typeof(Page),
                                                   Guid.NewGuid().ToString(),
                                                   getjQueryCode(jsCodetoRun),
                                                   true);
        }
    }

    //for the autocomplete- txtNewSKU
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCompletionList(string prefixText, int count, string contextKey)
    {
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            var skus = (from s in context.PRODUCTs where s.product_id.StartsWith(prefixText) select new { s.product_id }).Take(count).ToList();
            List<string> skuList = new List<string>();
            foreach (var item in skus)
            {
                skuList.Add(item.product_id.ToString());
            }
            return skuList;
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetSnapshotSKUS(string prefixText, int count, string contextKey)
    {
        int snapshotID = Convert.ToInt32(HttpContext.Current.Session["snapshotID"]);
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            var skus = (from s in context.ssc_assortment_change_snapshot_detail 
                        where s.product_id.StartsWith(prefixText) &&
                        s.snapshot_id == snapshotID //&&
                        //(s.product_status == null ||
                        //s.product_status == "")
                        select new { s.product_id }).Take(count).ToList();
            List<string> skuList = new List<string>();
            foreach (var item in skus)
            {
                skuList.Add(item.product_id.ToString());
            }
            return skuList;
        }
    }


    private bool IsSKUValid(string sku)
    {
        ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1();
        var record = (from p in context.PRODUCTs
                     where p.product_id == sku
                     select p).SingleOrDefault();
        if (record != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DisplaySearchResults(IQueryable searchresults)
    {
        pSearchResults.Visible = true;
        gdvMatchingSnapshots.DataSource = searchresults;
        gdvMatchingSnapshots.DataBind();
        gdvMatchingSnapshots.Visible = true;    
        runjQueryCode("$('#divChangeReport').show();");
        lblSearchSnapshotsError.Visible = false;
    }


    //**Commenting this out on 1/9/2014 per the meeting- users don't want the drop down showing previous entries
    //private void PopulateNewItemStrategyDropDown()
    //{
    //    using (ssc_spacemanEntities1 context = new ssc_spacemanEntities1())
    //    {
    //        var strategies = from s in context.ssc_assortment_change_NewItemStrategy
    //                         select s;
    //        if (strategies.Any())
    //        {
    //            ddlNewItemStrategy.DataSource = strategies;
    //            ddlNewItemStrategy.DataValueField = "New_Item_Strategy";
    //            ddlNewItemStrategy.DataTextField = "New_Item_Strategy";
    //            ddlNewItemStrategy.DataBind();

    //            ddlNewItemStrategyReplace.DataSource = strategies;
    //            ddlNewItemStrategyReplace.DataValueField = "New_Item_Strategy";
    //            ddlNewItemStrategyReplace.DataTextField = "New_Item_Strategy";
    //            ddlNewItemStrategyReplace.DataBind();
    //        }
    //    }
    //}

    //**Commenting this out on 1/9/2014 per the meeting- users don't want the drop down showing previous entries
    //protected void btnSaveNewItemStrategyReplace_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (!String.IsNullOrWhiteSpace(txtNewItemStrategyReplace.Text))
    //        {
    //            using (ssc_spacemanEntities1 context = new ssc_spacemanEntities1())
    //            {
    //                var strategy = new ssc_assortment_change_NewItemStrategy();
    //                strategy.New_Item_Strategy = txtNewItemStrategyReplace.Text;
    //                context.AddTossc_assortment_change_NewItemStrategy(strategy);
    //                context.SaveChanges();
    //                txtNewItemStrategyReplace.Text = "";
    //                runjQueryCode("$('#divDiscontinueSKU').show();");
    //                runjQueryCode("$('#divReplace').show();");
    //                runjQueryCode("$('#divDiscontinuedBy').hide();");
    //                //PopulateNewItemStrategyDropDown();
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ShowExceptionMessage();
    //    }
    //}


    //** Commenting this out on 1/9 per the discussion in the meeting in 1/8- users decided they don't want the ability to save
    //new item strategies

    //protected void btnSaveNewItemStrategy_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (!String.IsNullOrWhiteSpace(txtNewItemStrategy.Text))
    //        {
    //            lblAddSKUError.Visible = false;
    //            using (ssc_spacemanEntities1 context = new ssc_spacemanEntities1())
    //            {
    //                var strategy = new ssc_assortment_change_NewItemStrategy();
    //                strategy.New_Item_Strategy = txtNewItemStrategy.Text;
    //                context.AddTossc_assortment_change_NewItemStrategy(strategy);
    //                context.SaveChanges();
    //                PopulateNewItemStrategyDropDown();
    //                txtNewItemStrategy.Text = "";
    //                runjQueryCode("$('#divAddNewProduct').show();");
    //            }
    //        }
    //        else
    //        {
    //            runjQueryCode("$('#divAddNewProduct').show();");
    //            lblAddSKUError.Text = "Please enter a new item strategy";
    //            lblAddSKUError.Visible = true;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ShowExceptionMessage();
    //    }
    //}

    protected void btnSearchSnapshotCompare_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["snapshotID"] != null)
            {
                int snapshotid = Convert.ToInt32(Session["snapshotID"]);
                ssc1200028_Search search = new ssc1200028_Search();
                ssc1200027_Utilities util = new ssc1200027_Utilities();
                using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
                {
                    if (ddlSearchOptions.SelectedValue == "Year")
                    {
                        if (!String.IsNullOrWhiteSpace(txtSearchYear.Text))
                        {

                            //gdvMatchingSnapshots.DataSource = search.SearchByYear(Convert.ToInt32(txtSearchYear.Text));
                            //gdvMatchingSnapshots.DataSource = results;
                            //var results = search.SearchSnapshots(Convert.ToInt32(txtSizeSearch.Text), ddlSeasonSearch.SelectedValue, ddlChangeReportLocation.SelectedValue);
                            //gdvMatchingSnapshots.DataSource = results;

                            //int snapshotid = Convert.ToInt32(Session["snapshotID"]);
                            int year = Convert.ToInt32(txtSearchYear.Text);
                            var snapshotDetails = from c in context.ssc_assortment_change_snapshot
                                                  where c.snapshot_year == year &&
                                                  c.snapshot_id != snapshotid
                                                  select new { c.snapshot_id, c.snapshot_nbr, c.snapshot_desc };

                            DisplaySearchResults(snapshotDetails);
                        }
                        else
                        {
                            lblSearchSnapshotsError.Text = "Please enter a year.";
                            lblSearchSnapshotsError.Visible = true;
                            runjQueryCode("$('#divChangeReport').show();");
                        }
                    }
                    if (ddlSearchOptions.SelectedValue == "Description")
                    {
                        if (!String.IsNullOrWhiteSpace(txtPlanogramName.Text))
                        {
                            var results = search.SearchByDescription(txtPlanogramName.Text, snapshotid);
                            DisplaySearchResults(results);
                        }
                        else
                        {
                            lblSearchSnapshotsError.Text = "Please enter a description";
                            lblSearchSnapshotsError.Visible = true;
                            runjQueryCode("$('#divChangeReport').show();");
                        }
                    }
                    if (ddlSearchOptions.SelectedValue == "Season")
                    {
                        if (ddlSeasonSearch.SelectedValue != "Please Select")
                        {
                            var results = search.SearchBySeason(ddlSeasonSearch.SelectedValue, snapshotid);
                            DisplaySearchResults(results);
                        }
                        else
                        {
                            lblSearchSnapshotsError.Text = "Please enter a season";
                            lblSearchSnapshotsError.Visible = true;
                            runjQueryCode("$('#divChangeReport').show();");
                        }
                    }
                    if (ddlSearchOptions.SelectedValue == "Location")
                    {
                        if (ddlChangeReportLocation.SelectedValue != "Please Select")
                        {
                            var results = search.SearchByLocation(ddlChangeReportLocation.SelectedValue, snapshotid);
                            DisplaySearchResults(results);
                        }
                        else
                        {
                            lblSearchSnapshotsError.Text = "Please enter a location";
                            lblSearchSnapshotsError.Visible = true;
                            runjQueryCode("$('#divChangeReport').show();");
                        }
                    }
                    if (ddlSearchOptions.SelectedValue == "Size")
                    {
                        if (!String.IsNullOrWhiteSpace(txtSizeSearch.Text))
                        {
                            var results = search.SearchBySize(Convert.ToInt32(txtSizeSearch.Text), snapshotid);
                            DisplaySearchResults(results);
                        }
                        else
                        {
                            lblSearchSnapshotsError.Text = "Please enter a size";
                            lblSearchSnapshotsError.Visible = true;
                            runjQueryCode("$('#divChangeReport').show();");
                        }
                    }
                }
            }
            else
            {
                ShowTimeOutMessage();
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    //private void SelectSnapshot(int snapshotid)
    //{
    //    //int snapshotID = Convert.ToInt32(gdvProducts.DataKeys[e.NewSelectedIndex].Value);
    //    int snapshotID = snapshotid;
    //    using (ssc_spacemanEntities1 context = new ssc_spacemanEntities1())
    //    {
    //        //take this snapshotID and search in the previously selected snapshot for every SKU, display the 
    //        //ones that aren't in the previous snapshot. Then do the opposite, and display
    //        if (Session["snapshotID"] != null)
    //        {
    //            //get the products for previously selected snapshot
    //            int newSnapshotID = Convert.ToInt32(Session["snapshotID"]);
    //            var products = from p in context.ssc_assortment_change_snapshot_detail
    //                           where p.snapshot_id == newSnapshotID
    //                           select new { p.detail_id, p.product_id, p.product_desc };

    //            //get the products from the newly selected snapshot to compare with
    //            var compareProducts = from p in context.ssc_assortment_change_snapshot_detail
    //                                  where p.snapshot_id == snapshotID
    //                                  select new { p.detail_id, p.product_id, p.product_desc };

    //            //gdvDifference will show the SKU's that exist in the first snapshot, but not the second one.
    //            if (products.Any())
    //            {
    //                IQueryable onlyInFirstSnapshot = products.AsQueryable().Except(compareProducts);
    //                gdvDifference.DataSource = onlyInFirstSnapshot;
    //                gdvDifference.DataBind();
    //                gdvDifference.Visible = true;
    //            }

    //            if (compareProducts.Any())
    //            {
    //                IQueryable onlyInSecondSnapshot = compareProducts.AsQueryable().Except(products);
    //                gdvReverseDifference.DataSource = onlyInSecondSnapshot;
    //                gdvReverseDifference.DataBind();
    //                gdvReverseDifference.Visible = true;
    //            }
    //        }
    //        else
    //        {
    //            ShowTimeOutMessage();
    //        }
    //        //runjQueryCode("$('#divChangeReport').show();");
    //    }
    //}


    protected void gdvMatchingSnapshots_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            using (ssc1200028_ssc_spacemanEntities1 newContext = new ssc1200028_ssc_spacemanEntities1())
            {
                int snapshotID = Convert.ToInt32(gdvMatchingSnapshots.DataKeys[e.NewSelectedIndex].Value);
                Session["MatchingSnapshotID"] = snapshotID;
                ssc1200028_AssortmentPlanningBL bl = new ssc1200028_AssortmentPlanningBL();
                ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
                ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT sessionSnapshot = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();
                ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT selectedSnapshot = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();

                gdvDifference.DataSource = detail.GetIntersectOfFirstAndSecondSnapshots(newContext, detail.SnapshotSessionID, snapshotID);
                gdvDifference.DataBind();
                gdvDifference.Visible = true;

                selectedSnapshot.Snapshots = selectedSnapshot.GetSnapshotsBySnapshotID(newContext, snapshotID);
                sessionSnapshot.Snapshots = sessionSnapshot.GetSnapshotsBySnapshotID(newContext);
                lblDifferenceNote.Text = "This table shows Active SKUs from the <strong>" + sessionSnapshot.Snapshots.SingleOrDefault().SnapshotNumber + "</strong> snapshot that <strong>also exist</strong> (intersection) in the snapshot you just selected, <strong>" + selectedSnapshot.Snapshots.SingleOrDefault().SnapshotNumber + "</strong>";
                lblReverseDifferenceNote.Text = "This table shows Active SKUs from <strong>" + selectedSnapshot.Snapshots.SingleOrDefault().SnapshotNumber + "</strong> that <strong>do not exist</strong> in <strong>" + sessionSnapshot.Snapshots.SingleOrDefault().SnapshotNumber + "</strong>";
                pExplaination.Visible = true;
                
                gdvReverseDifference.DataSource = detail.GetExceptOfSecondAndFirstSnapshots(newContext, detail.SnapshotSessionID, snapshotID);
                gdvReverseDifference.DataBind();
                gdvReverseDifference.Visible = true;
                
                
                //SelectSnapshot(snapshotID);
                runjQueryCode("$('#divChangeReport').show();");
                pDifference.Visible = true;
                pReverseDifference.Visible = true;
                pReverseDifferenceDescription.Visible = true;
                pDifferenceNote.Visible = true;
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    //private void ResetSKUActionSelection()
    //{
    //    string status = Request.Form["ddlDiscontinueSKUStatus"];
    //    if (status == "SelectStatus")
    //    {
    //        runjQueryCode("$('#ddlDiscontinueSKUStatus').val('SelectStatus');");
    //    }
    //    if (status == "Discontinued")
    //    {
    //        runjQueryCode("$('#ddlDiscontinueSKUStatus').val('Discontinued');");
    //    }
    //    if (status == "Replaced")
    //    {
    //        runjQueryCode("$('#ddlDiscontinueSKUStatus').val('Replaced');");
    //    }
    //}

    protected void txtSku_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                var skuDetails = (from c in context.PRODUCTs
                                  where c.product_id == txtSKU.Text
                                  select c).SingleOrDefault();
                if (skuDetails != null)
                {
                    txtNewUPC.Text = skuDetails.upc;
                    txtDescription.Text = skuDetails.name;
                    runjQueryCode("$('#divAddNewProduct').show();");
                    //ResetSKUActionSelection();
                }
                else
                {
                    runjQueryCode("$('#divAddNewProduct').show();");
                    //ResetSKUActionSelection();
                }
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    protected void txtNewSku_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                var skuDetails = (from c in context.PRODUCTs
                                  where c.product_id == txtNewSKU.Text
                                  select c).SingleOrDefault();
                if (skuDetails != null)
                {
                    txtUPCReplace.Text = skuDetails.upc;
                    txtDescription.Text = skuDetails.name;
                    txtDescriptionReplace.Text = skuDetails.name;
                    runjQueryCode("$('#divDiscontinueSKU').show();");
                    runjQueryCode("$('#div1').show();");
                    runjQueryCode("$('#divReplace').show();");
                    runjQueryCode("$('#divbtnSaveFacingStatus').show();");
                    runjQueryCode("$('#divFacingAdjustment').show();");
                    runjQueryCode("$('#divDiscontinuedBy').hide();");
                    //runjQueryCode("$('#ddlDiscontinueSKUStatus').val('Replaced');");
                }
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    protected void gdvDifference_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvDifference.PageIndex = e.NewPageIndex;
            ssc1200028_AssortmentPlanningBL bl = new ssc1200028_AssortmentPlanningBL();
            ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
            ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT snapshot = new ssc_spaceman_ASSORTMENT_CHANGE_SNAPSHOT();
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                gdvDifference.DataSource = detail.GetIntersectOfFirstAndSecondSnapshots(context, detail.SnapshotSessionID, detail.MatchingSnapshotID);
                gdvDifference.DataBind();
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    protected void gdvReverseDifference_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            ssc1200028_AssortmentPlanningBL bl = new ssc1200028_AssortmentPlanningBL();
            ssc_spaceman_SNAPSHOT_DETAIL detail = new ssc_spaceman_SNAPSHOT_DETAIL();
            gdvReverseDifference.PageIndex = e.NewPageIndex;
            using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
            {
                gdvDifference.DataSource = detail.GetExceptOfSecondAndFirstSnapshots(context, detail.SnapshotSessionID, detail.MatchingSnapshotID);
                gdvDifference.DataBind();
            }
        }
        catch (Exception)
        {
            ShowExceptionMessage();
        }
    }

    private void HideSearchResults()
    {
        gdvDifference.Visible = false;
        gdvMatchingSnapshots.Visible = false;
        gdvReverseDifference.Visible = false;
        pReverseDifference.Visible = false;
        pReverseDifferenceDescription.Visible = false;
        pDifference.Visible = false;
        pDifferenceNote.Visible = false;
        pSearchResults.Visible = false;
        pExplaination.Visible = false;
    }

    protected void ddlSearchOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchOptions.SelectedValue == "Description")
        {
            divDescriptionSearch.Visible = true;
            divSizeSearch.Visible = false;
            divYearSearch.Visible = false;
            divSeasonSearch.Visible = false;
            divLocationSearch.Visible = false;
            runjQueryCode("$('#divChangeReport').show();");
            btnSearchSnapshotCompare.Visible = true;
            HideSearchResults();
            lblSearchSnapshotsError.Visible = false;
        }
        if (ddlSearchOptions.SelectedValue == "Year")
        {
            divYearSearch.Visible = true;
            runjQueryCode("$('#divChangeReport').show();");
            divDescriptionSearch.Visible = false;
            divSizeSearch.Visible = false;
            divSeasonSearch.Visible = false;
            divLocationSearch.Visible = false;
            btnSearchSnapshotCompare.Visible = true;
            HideSearchResults();
            lblSearchSnapshotsError.Visible = false;
        }
        if (ddlSearchOptions.SelectedValue == "Season")
        {
            divSeasonSearch.Visible = true;
            runjQueryCode("$('#divChangeReport').show();");
            divSizeSearch.Visible = false;
            divYearSearch.Visible = false;
            divLocationSearch.Visible = false;
            divDescriptionSearch.Visible = false;
            btnSearchSnapshotCompare.Visible = true;
            HideSearchResults();
            lblSearchSnapshotsError.Visible = false;
        }
        if (ddlSearchOptions.SelectedValue == "Size")
        {
            divSizeSearch.Visible = true;
            runjQueryCode("$('#divChangeReport').show();");
            divSeasonSearch.Visible = false;
            divYearSearch.Visible = false;
            divLocationSearch.Visible = false;
            divDescriptionSearch.Visible = false;
            btnSearchSnapshotCompare.Visible = true;
            HideSearchResults();
            lblSearchSnapshotsError.Visible = false;
        }
        if (ddlSearchOptions.SelectedValue == "Location")
        {
            divLocationSearch.Visible = true;
            runjQueryCode("$('#divChangeReport').show();");
            divYearSearch.Visible = false;
            divDescriptionSearch.Visible = false;
            divSizeSearch.Visible = false;
            btnSearchSnapshotCompare.Visible = true;
            divSeasonSearch.Visible = false;
            HideSearchResults();
            lblSearchSnapshotsError.Visible = false;
        }
    }


    //Sorting and paging methods for gdvProducts

    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }

    private string ToggleSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }


    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",

                                  GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",

                                  GridViewSortExpression, ToggleSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }

    private DataTable GetProductDataTable()
    {
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            ssc_spaceman_SNAPSHOT_DETAIL skuDetails = new ssc_spaceman_SNAPSHOT_DETAIL();
            //skuDetails.Products = skuDetails.GetActiveProductsBySnapshotID(context);
            skuDetails.Products = skuDetails.GetProductsBySnapshotID(context);
            DataTable dtProducts = new DataTable();
            ssc1200027_Utilities util = new ssc1200027_Utilities();
            dtProducts = util.ConvertToDataTable(skuDetails.Products);
            return dtProducts;
        }
    }

    private void Page_Error(object sender, EventArgs e)
    {
        Exception exc = Server.GetLastError();
        //if (exc is HttpUnhandledException)
        //{
        ShowExceptionMessage();
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("craig.macivor@sscoop.com");
        mail.To.Add("craig.macivor@sscoop.com");
        mail.Subject = "Exception thrown";
        mail.Body = exc.Message + "<br /><br /> " + exc.InnerException + "<br /><br />" + exc.StackTrace;
        SmtpClient client = new SmtpClient("mailserver2", 25);
        client.Send(mail);
        //}
        Server.ClearError();
    }

    protected void gdvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvProducts.DataSource = SortDataTable(GetProductDataTable(), true);
        gdvProducts.PageIndex = e.NewPageIndex;
        gdvProducts.DataBind();

        //if (Session["detailID"] != null)
        //{
        //    int dataKey = Convert.ToInt32(Session["detailID"]);
        //}
    }


    protected void gdvProducts_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        int pageIndex = gdvProducts.PageIndex;
        gdvProducts.DataSource = SortDataTable(GetProductDataTable(), false);
        gdvProducts.DataBind();
        gdvProducts.PageIndex = pageIndex;
    }

    protected void btnNoChange_Click(object sender, EventArgs e)
    {
        divSKUDetails.Visible = false;
        gdvProducts.SelectedRowStyle.Reset();
    }

    protected void btnAddSKU_Click(object sender, EventArgs e)
    {
        //divSKUDetails.Visible = true;
        divAddNewProduct.Visible = true;
        divChangeReport.Visible = false;
        divDiscontinueSKU.Visible = false;
        divSKUDetails.Visible = false;
        gdvProducts.SelectedRowStyle.Reset();
    }

    protected void btnChangeReport_Click(object sender, EventArgs e)
    {
        //divSKUDetails.Visible = true;
        divAddNewProduct.Visible = false;
        divChangeReport.Visible = true;
        gdvProducts.SelectedRowStyle.Reset();
        divDiscontinueSKU.Visible = false;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}