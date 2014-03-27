using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using ew = SoftArtisans.OfficeWriter.ExcelWriter;
using ssc1200028_ssc_spacemanModel;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for ssc1200027_Utilities
/// </summary>
public class ssc1200027_Utilities
{
    string assortmentPlanningConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ssc1200028_ssc_spacemanConnectionString"].ToString();

    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;

    }


    public string getjQueryCode(string jsCodetoRun)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("$(document).ready(function() {");
        sb.AppendLine(jsCodetoRun);
        sb.AppendLine(" });");

        return sb.ToString();
    }

    public void runjQueryCode(string jsCodetoRun, Page thispage)
    {

        ScriptManager requestSM = ScriptManager.GetCurrent(thispage);
        if (requestSM != null && requestSM.IsInAsyncPostBack)
        {
            ScriptManager.RegisterClientScriptBlock(thispage,
                                                    typeof(Page),
                                                    Guid.NewGuid().ToString(),
                                                    getjQueryCode(jsCodetoRun),
                                                    true);
        }
        else
        {
           thispage.ClientScript.RegisterClientScriptBlock(typeof(Page),
                                                   Guid.NewGuid().ToString(),
                                                   getjQueryCode(jsCodetoRun),
                                                   true);
        }
    }

    public void GenerateExcel(int snapshotid, Page page)
    {
        int rowsCount;
        int totalRows;
        string noValuePlaceholder = "None";
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            
            //get the products and product change history by snapshot ID
            IEnumerable<ssc_assortment_change_snapshot_detail> snapshotDetail = from p in context.ssc_assortment_change_snapshot_detail
                                                                                where p.snapshot_id == snapshotid
                                                                                select p;

            var snapshotHeader = (from s in context.ssc_assortment_change_snapshot
                                 where s.snapshot_id == snapshotid
                                 select s).SingleOrDefault();

            SoftArtisans.OfficeWriter.ExcelWriter.ExcelApplication eApp = new SoftArtisans.OfficeWriter.ExcelWriter.ExcelApplication();
            SoftArtisans.OfficeWriter.ExcelWriter.Workbook eWB = eApp.Create();
            SoftArtisans.OfficeWriter.ExcelWriter.Worksheet eWSPositionDetails = eWB.Worksheets.CreateWorksheet("Planograms");
            eWSPositionDetails.Select();
            //SoftArtisans.OfficeWriter.ExcelWriter.ColumnProperties colProps = eWSPositionDetails.GetColumnProperties(3);
            //colProps.AutoFitWidth();


            //write the spreadsheet headers
            eWSPositionDetails.Cells["A1"].Value = "Snapshot ID:";
            eWSPositionDetails.Cells["A1"].Style.Font.Bold = true;
            eWSPositionDetails.Cells["B1"].Value = snapshotHeader.snapshot_nbr.ToString();
            eWSPositionDetails.Cells["A2"].Value = "Associated Planogram Key ID";
            eWSPositionDetails.Cells["A2"].Style.Font.Bold = true;
            eWSPositionDetails.Cells["B2"].Value = snapshotHeader.snapshot_keyid.ToString();
            eWSPositionDetails.Cells["A3"].Value = "Associated Planogram Description:";
            eWSPositionDetails.Cells["A3"].Style.Font.Bold = true;
            eWSPositionDetails.Cells["B3"].Value = snapshotHeader.snapshot_desc;


            eWSPositionDetails.Cells["A5"].Value = "SKU";
            eWSPositionDetails.Cells["B5"].Value = "Description";
            eWSPositionDetails.Cells["C5"].Value = "Status";
            eWSPositionDetails.Cells["D5"].Value = "Exit Strategy";
            eWSPositionDetails.Cells["E5"].Value = "Discontinued By";
            eWSPositionDetails.Cells["F5"].Value = "Discontinued Comments";
            eWSPositionDetails.Cells["G5"].Value = "Replacement SKU";
            eWSPositionDetails.Cells["H5"].Value = "Replacement SKU Description";
            eWSPositionDetails.Cells["I5"].Value = "Replacement SKU Status";
            eWSPositionDetails.Cells["J5"].Value = "New Item Strategy";
            eWSPositionDetails.Cells["K5"].Value = "Facing Change";
            eWSPositionDetails.Cells["L5"].Value = "Facing Quantity";

            eWSPositionDetails.Cells["M5"].Value = "4400 STK Status";
            eWSPositionDetails.Cells["N5"].Value = "4400 Seasonal";
            eWSPositionDetails.Cells["O5"].Value = "4400 Inventory";
            eWSPositionDetails.Cells["P5"].Value = "4900 STK Status";
            eWSPositionDetails.Cells["Q5"].Value = "4900 Seasonal";
            eWSPositionDetails.Cells["R5"].Value = "4900 Inventory";
            eWSPositionDetails.Cells["S5"].Value = "4100 STK Status";
            eWSPositionDetails.Cells["T5"].Value = "4100 Seasonal";

            eWSPositionDetails.Cells["U5"].Value = "Direct Order Minimum";
            eWSPositionDetails.Cells["V5"].Value = "Warehouse Minimum";
            //eWSPositionDetails.Cells["U5"].Value = "4100 Dollar Min";
            //eWSPositionDetails.Cells["V5"].Value = "4100 Weight Min";
            //eWSPositionDetails.Cells["W5"].Value = "Direct Order Minimum";
            //eWSPositionDetails.Cells["X5"].Value = "Warehouse Minimum";

            for (int x = 0; x <= 24; x++)
            {
                eWSPositionDetails.Cells[4, x].Style.Font.Bold = true; 
            }

            int positionDetailsRowNumber = 4;
            foreach (var details in snapshotDetail)
            {
                positionDetailsRowNumber++;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 0].Value = details.product_id.ToString();
                eWSPositionDetails.Cells[positionDetailsRowNumber, 1].Value = details.product_desc;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 2].Value = details.product_status;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 3].Value = details.exit_strategy;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 4].Value = details.discontinued_by;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 5].Value = details.discontinued_comments;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 6].Value = details.product_repl_prod;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 7].Value = details.product_repl_prod_desc;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 8].Value = details.product_repl_status;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 9].Value = details.product_repl_strategy;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 10].Value = details.facing_change;
                eWSPositionDetails.Cells[positionDetailsRowNumber, 11].Value = details.facing_quantity.ToString();

                //for every detail record, need to check if the detail record has a product_status value of 'Replaced'. 
                //If so, then the ssc_sku_status values need to come from the replacement record, and not the original.
                //This is from the meeting on 1/8/2014.

                //check the detail table for the "Replaced" value
                if (details.product_status == "Replaced")
                {
                    //get the sku details by the product_repl_prod value
                    var replacementSkuDetail = (from s in context.ssc_sku_status
                                                where s.sku == details.product_repl_prod
                                                select s).SingleOrDefault();
                    if (replacementSkuDetail != null)
                    {
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 12].Value = replacementSkuDetail.stk44;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 13].Value = replacementSkuDetail.sea44;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 14].Value = replacementSkuDetail.inv44;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 15].Value = replacementSkuDetail.stk49;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 16].Value = replacementSkuDetail.sea49;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 17].Value = replacementSkuDetail.inv49;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 18].Value = replacementSkuDetail.stk41;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 19].Value = replacementSkuDetail.sea41;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 20].Value = replacementSkuDetail.dmn41;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 21].Value = replacementSkuDetail.wmn41;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 22].Value = replacementSkuDetail.dmn41;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 23].Value = replacementSkuDetail.wmn41;
                    }
                    else
                    {
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 12].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 13].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 14].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 15].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 16].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 17].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 18].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 19].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 20].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 21].Value = noValuePlaceholder;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 22].Value = noValuePlaceholder;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 23].Value = noValuePlaceholder;
                    }
                }
                if (details.product_status == "Discontinued" || details.product_status == "Active")
                {
                    var skuDetail = (from s in context.ssc_sku_status
                                     where s.sku == details.product_id
                                     select s).SingleOrDefault();

                    //need to handle lack of stock status- will throw nullref exception if not
                    if (skuDetail != null)
                    {
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 12].Value = skuDetail.stk44;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 13].Value = skuDetail.sea44;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 14].Value = skuDetail.inv44;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 15].Value = skuDetail.stk49;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 16].Value = skuDetail.sea49;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 17].Value = skuDetail.inv49;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 18].Value = skuDetail.stk41;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 19].Value = skuDetail.sea41;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 20].Value = skuDetail.dmn41;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 21].Value = skuDetail.wmn41;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 22].Value = skuDetail.dmn41;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 23].Value = skuDetail.wmn41;
                    }
                    else
                    {
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 12].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 13].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 14].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 15].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 16].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 17].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 18].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 19].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 20].Value = noValuePlaceholder;
                        eWSPositionDetails.Cells[positionDetailsRowNumber, 21].Value = noValuePlaceholder;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 22].Value = noValuePlaceholder;
                        //eWSPositionDetails.Cells[positionDetailsRowNumber, 23].Value = noValuePlaceholder;
                    }
                }
            }
            rowsCount = snapshotDetail.Count();
            totalRows = rowsCount + 6;
            eWSPositionDetails.CreateArea(0, 0, totalRows, 24).AutoFitWidth();
            eApp.Save(eWB, page.Response, "Planogram Assortment.xls", false);
        }
    }

    //get list of buyers
    public DataTable GetBuyers()
    {
        DataTable dtBuyers = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("select buyer_nbr, buyer_name ");
        sb.Append("from (select 'test' as buyer_nbr, 'Please Select' as buyer_name ");
        sb.Append("union all ");
        sb.Append("select * from dbo.ssc_assortment_change_snapshot_buyer_vw) as T ");
        sb.Append("order by case when buyer_nbr = 'test' then 0 else 1 end, buyer_name");
        //string sql = "select buyer_nbr, buyer_name from (select null as buyer_nbr, 'Please Select' as buyer_name union all select * from dbo.ssc_assortment_change_snapshot_buyer_vw) as T order by case when buyer_nbr is null then 0 else 1 end, buyer_name";
        string sql = sb.ToString();
        using (SqlConnection conn = new SqlConnection(assortmentPlanningConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtBuyers);
                }
            }
        }
        return dtBuyers;
    }


    //methods for copying a snapshot and associated detail records
    public void CopySnapshot(int snapshotid)
    {
        DataTable dtsnapshot = new DataTable();
        DataTable dtsnapshotCopy = new DataTable();
        DataTable dtDetails = new DataTable();
        DataTable dtDetailsCopy = new DataTable();
        int newSnapshotID = 0;
        int detailsTableCount;
        int i;
        string sql = "select * from ssc_assortment_change_snapshot where snapshot_id = @snapshotid";
        string detailSQL = "select * from ssc_assortment_change_snapshot_detail where snapshot_id = @snapshotid";
        using (SqlConnection conn = new SqlConnection(assortmentPlanningConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter prm = new SqlParameter("@snapshotid", snapshotid);
                cmd.Parameters.Add(prm);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtsnapshot);
                }
            }

            //now copy the record in the datatable and insert the record into the snapshot detail table
            dtsnapshotCopy = dtsnapshot.Clone();
            dtsnapshotCopy.ImportRow(dtsnapshot.Rows[0]);
            if (dtsnapshotCopy.Rows.Count > 0)
            {
                using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
                {
                    var snapshot = new ssc_assortment_change_snapshot();
                    snapshot.snapshot_nbr = dtsnapshotCopy.Rows[0]["snapshot_nbr"].ToString();
                    snapshot.snapshot_desc = dtsnapshotCopy.Rows[0]["snapshot_desc"].ToString();
                    snapshot.snapshot_keyid = Convert.ToInt32(dtsnapshotCopy.Rows[0]["snapshot_keyid"]);
                    snapshot.snapshot_status = dtsnapshotCopy.Rows[0]["snapshot_status"].ToString();
                    snapshot.snapshot_size = Convert.ToInt32(dtsnapshotCopy.Rows[0]["snapshot_size"]);
                    snapshot.snapshot_season = dtsnapshotCopy.Rows[0]["snapshot_season"].ToString();
                    snapshot.snapshot_year = Convert.ToInt32(dtsnapshotCopy.Rows[0]["snapshot_year"]);
                    snapshot.snapshot_location = dtsnapshotCopy.Rows[0]["snapshot_location"].ToString();
                    snapshot.snapshot_buyer = dtsnapshotCopy.Rows[0]["snapshot_buyer"].ToString();
                    snapshot.snapshot_chg_user = dtsnapshotCopy.Rows[0]["snapshot_chg_user"].ToString();
                    snapshot.snapshot_chg_date = Convert.ToDateTime(dtsnapshotCopy.Rows[0]["snapshot_chg_date"]);
                    context.AddTossc_assortment_change_snapshot(snapshot);
                    context.SaveChanges();

                    //get the snapshot_id of the snapshot copy that was just created
                   var snapshotID = (from s in context.ssc_assortment_change_snapshot
                                  orderby s.snapshot_id descending
                                  select new { s.snapshot_id }).FirstOrDefault();
                   newSnapshotID = snapshotID.snapshot_id;
                }
            }

            //now need to copy associated detail records
            using (SqlCommand cmd = new SqlCommand(detailSQL, conn))
            {
                SqlParameter detailprm = new SqlParameter("@snapshotid", snapshotid);
                cmd.Parameters.Add(detailprm);
                using (SqlDataAdapter detailAD = new SqlDataAdapter())
                {
                    detailAD.SelectCommand = cmd;
                    detailAD.Fill(dtDetails);
                }
            }

            dtDetailsCopy = dtDetails.Clone();
            detailsTableCount = dtDetails.Rows.Count;
            for (i = 0; i <= detailsTableCount - 1; i++)
            {
                dtDetailsCopy.ImportRow(dtDetails.Rows[i]);
            }

            //now insert the copied rows into the detail table
            using (ssc1200028_ssc_spacemanEntities1 newcontext = new ssc1200028_ssc_spacemanEntities1())
            {
                
                for (i = 0; i <= detailsTableCount - 1; i++)
                {
                    var snapshotDetail = new ssc_assortment_change_snapshot_detail();
                    //need to get the snapshot_id of the snapshot record that was just copied
                    //snapshotDetail.snapshot_id = Convert.ToInt32(dtDetailsCopy.Rows[i]["snapshot_id"]);
                    snapshotDetail.snapshot_id = newSnapshotID;
                    snapshotDetail.product_id = dtDetailsCopy.Rows[i]["product_id"].ToString();
                    snapshotDetail.product_upc = dtDetailsCopy.Rows[i]["product_upc"].ToString();
                    snapshotDetail.product_desc = dtDetailsCopy.Rows[i]["product_desc"].ToString();
                    snapshotDetail.product_status = dtDetailsCopy.Rows[i]["product_status"].ToString();
                    snapshotDetail.product_strategy = dtDetailsCopy.Rows[i]["product_strategy"].ToString();
                    snapshotDetail.product_repl_prod = dtDetailsCopy.Rows[i]["product_repl_prod"].ToString();
                    snapshotDetail.product_repl_upc = dtDetailsCopy.Rows[i]["product_repl_upc"].ToString();
                    snapshotDetail.product_repl_prod_desc = dtDetailsCopy.Rows[i]["product_repl_prod_desc"].ToString();
                    snapshotDetail.product_repl_status = dtDetailsCopy.Rows[i]["product_repl_status"].ToString();
                    snapshotDetail.product_repl_strategy = dtDetailsCopy.Rows[i]["product_repl_strategy"].ToString();
                    snapshotDetail.facing_change = dtDetailsCopy.Rows[i]["facing_change"].ToString();
                    snapshotDetail.facing_quantity = Convert.ToInt32(dtDetailsCopy.Rows[i]["facing_quantity"].ToString());
                    snapshotDetail.exit_strategy = dtDetailsCopy.Rows[i]["exit_strategy"].ToString();
                    snapshotDetail.discontinued_comments = dtDetailsCopy.Rows[i]["discontinued_comments"].ToString();
                    snapshotDetail.discontinued_by = dtDetailsCopy.Rows[i]["discontinued_by"].ToString();
                    newcontext.AddTossc_assortment_change_snapshot_detail(snapshotDetail);
                }
                newcontext.SaveChanges();
            }
        }
    }


    //to import SKU's from the PRODUCT table into a snapshot detail record when a snapshot is created.
    public void ImportSKU(int key, int snapshotid)
    {
        using (ssc1200028_ssc_spacemanEntities1 context = new ssc1200028_ssc_spacemanEntities1())
        {
            var skus = from s in context.PRODUCT_LIST
                       join p in context.PRODUCTs on s.product_id equals p.product_id
                       where s.Key_id == key
                       select new
                       {
                           p.product_id,
                           p.upc,
                           p.name,
                           s.Manual_Inv
                       };
            if (skus.Any())
            {
                foreach (var sku in skus)
                {
                    //now insert into the detail table
                    var snapshotDetail = new ssc_assortment_change_snapshot_detail();
                    snapshotDetail.snapshot_id = snapshotid;
                    snapshotDetail.product_id = sku.product_id;
                    snapshotDetail.product_desc = sku.name;
                    snapshotDetail.product_upc = sku.upc;
                    snapshotDetail.facing_quantity = sku.Manual_Inv;
                    snapshotDetail.product_status = "Active";
                    context.AddTossc_assortment_change_snapshot_detail(snapshotDetail);
                }
                context.SaveChanges();
            }
        }
    }

	public ssc1200027_Utilities()
	{
		
	}
}