using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SoftArtisans.OfficeWriter.ExcelWriter;
using System.Text;
using System.Web.Services;


public partial class ssc9000004v2 : System.Web.UI.Page
{

    private string OpenSSCookie
    {
        get;
        set;
    }

    //private const string OpenSSCookie = "cem01";
    
        
    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.OpenSSCookie = Request.Cookies["OpenSS"]["UserID"].ToString();
        //this.OpenSSCookie = "cem01";
        if (!IsPostBack)
        {
            PopulateEntityGrid();
            PopulateMainProductDropDown();
            PopulateProductGrid();

            txtInventoryDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
        }
    }

    private void PopulateEntityGrid()
    {
        try
        {
            //string user = Request.Cookies["OpenSS"]["UserID"].ToString();
            string user = OpenSSCookie;
            //now get a datatable by the user ID
            DataTable dtEntityParametersByUserID = new DataTable();
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            dtEntityParametersByUserID = criteria.GetEntityParametersByUserID(user);
            //if (dtEntityParametersByUserID.Rows.Count > 0)
            //{
            gdvSelectedEntityParameters.DataSource = dtEntityParametersByUserID;
            gdvSelectedEntityParameters.DataBind();
            //}
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void PopulateProductGrid()
    {
        try
        {
            string user = OpenSSCookie;
            DataTable dtProducts = new DataTable();
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            dtProducts = criteria.GetProductParametersByUserID(user);
            gdvSelectedProductParameters.DataSource = dtProducts;
            gdvSelectedProductParameters.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    private string GetDescriptionValue(string selected)
    {
        dw_global_GL_CENTER_DATA gl = new dw_global_GL_CENTER_DATA();
        DataTable dtConfigData = new DataTable();
        //look up the comp and div code by the selected value
        dtConfigData = gl.GetConfigData(ddlEntitiesNoAjax.SelectedValue);
        string desc = dtConfigData.Rows[0][3].ToString();
        return desc;
    }

    //gets the comp value by comp_div_descr from the comp_div_descr table
    private string GetCompValue(string selected)
    {
        dw_global_GL_CENTER_DATA gl = new dw_global_GL_CENTER_DATA();
        DataTable dtConfigData = new DataTable();
        //look up the comp and div code by the selected value
        dtConfigData = gl.GetConfigData(ddlEntitiesNoAjax.SelectedValue);
        string Entity = dtConfigData.Rows[0][0].ToString();
        return Entity;
    }

    private string GetDescription(string selected)
    {
        string[] selectedArr = selected.Split(' ');
        return selectedArr[2];
    }

    protected void gdvSelectedEntityParameters_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int row = Convert.ToInt32(gdvSelectedEntityParameters.DataKeys[e.RowIndex].Value);
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            criteria.DeleteCriteriaRecord(row);
            PopulateEntityGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void gdvSelectedProductParameters_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int row = Convert.ToInt32(gdvSelectedProductParameters.DataKeys[e.RowIndex].Value);
            int test = row;
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            criteria.DeleteCriteriaRecord(row);
            PopulateProductGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }


    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        System.Collections.Generic.List<MarginGroup> MarginGroups = new List<MarginGroup>();
        Decimal frtPerPound = 0;
        Decimal frtPerTon = 0;
        Decimal frtPerItem = 0;
        DateTime inventoryDate = DateTime.Parse(txtInventoryDate.Text);

        switch (radFrtPer.SelectedIndex)
        {
            case 0:
                frtPerPound = Convert.ToDecimal(txtFreightPerPound.Text);
                frtPerTon = Convert.ToDecimal(txtFreightPerPound.Text) * Convert.ToDecimal(2000);
                break;
            case 1:
                frtPerPound = Convert.ToDecimal(txtFreightPerPound.Text) / Convert.ToDecimal(2000);
                frtPerTon = Convert.ToDecimal(txtFreightPerPound.Text);
                break;
            default:
                break;
        }

        frtPerItem = Convert.ToDecimal(txtFreightPerItem.Text);
        dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();

        StringBuilder sb = new StringBuilder();
        StringBuilder stores = new StringBuilder();
        StringBuilder skus = new StringBuilder();
        sb.Append("declare @eff_date DateTime; " + System.Environment.NewLine);
        sb.Append("SET @eff_date = '" + inventoryDate.ToShortDateString() + "'; " + System.Environment.NewLine);
        sb.Append("declare @frt_per_pound money; " + System.Environment.NewLine);
        sb.Append("declare @frt_per_each money; " + System.Environment.NewLine);
        sb.Append("set @frt_per_pound = " + frtPerPound.ToString() + "; " + System.Environment.NewLine);
        sb.Append("set @frt_per_each = " + txtFreightPerItem.Text.Trim() + "; " + System.Environment.NewLine);
        //sb.Append(" SELECT top 64000 d.dist_mgr, a.store_nbr, c.cntr_desc, a.sku, b.sku_desc, a.qty_oh, a.auc, a.mkt_cost, " + txtFreightPerPound.Text + " frt, a.mkt_cost + " + txtFreightPerPound.Text + " mkt_dlvd, a.auc - (a.mkt_cost + " + txtFreightPerPound.Text + ") wd_ton, (a.auc - (a.mkt_cost + " + txtFreightPerPound.Text + ")) * a.qty_oh WD_est, a.uom, a.sku_weight ");

        sb.Append("   SELECT distinct sku INTO #ssc9000004_skus from " + System.Environment.NewLine);
        sb.Append(" 	( " + System.Environment.NewLine);
        sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where pgrp_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Product Group' and OpenSSUser = '" + OpenSSCookie + "' and  ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        sb.Append(" 	union all " + System.Environment.NewLine);
        sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where comm_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Commodity' and OpenSSUser = '" + OpenSSCookie + "' and  ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        sb.Append(" 	union all " + System.Environment.NewLine);
        sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where mgrp_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Margin Group' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        sb.Append(" 	union all " + System.Environment.NewLine);
        sb.Append(" 	SELECT Item sku from dw_work.dbo.ssc9000004_criteria where GroupType = 'Sku' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' " + System.Environment.NewLine);
        sb.Append(" 	) " + System.Environment.NewLine);
        sb.Append(" 	a; " + System.Environment.NewLine);

        sb.Append(" SELECT top 64000 d.dist_mgr, a.store_nbr, c.cntr_desc, a.sku, b.sku_desc, a.qty_oh, a.auc, a.mkt_cost, " + System.Environment.NewLine);
        sb.Append(" @frt_per_pound frt_per_pound, " + System.Environment.NewLine);

        sb.Append(" case when uom = 'LB' or uom = 'BG' then @frt_per_pound * sku_weight else @frt_per_each end frt_per_sku, " + System.Environment.NewLine);
        sb.Append(" case when uom = 'LB' or uom = 'BG' then a.mkt_cost + (@frt_per_pound * sku_weight) else a.mkt_cost + @frt_per_each end mkt_dlvd,  " + System.Environment.NewLine);
        sb.Append(" case when uom = 'LB' or uom = 'BG' then (a.mkt_cost + (@frt_per_pound * sku_weight)) - a.auc else (a.mkt_cost + @frt_per_each) - a.auc end [wd_sku], " + System.Environment.NewLine);
        sb.Append(" case when uom = 'LB' or uom = 'BG' then ((a.mkt_cost + (@frt_per_pound * sku_weight)) - a.auc) * a.qty_oh else ((a.mkt_cost + @frt_per_each) - a.auc) * a.qty_oh end WD_est, " + System.Environment.NewLine);

        //changing per Joe's suggestion
        //sb.Append(" a.uom, a.sku_weight, a.mgrp, c.comp, c.div_code " + System.Environment.NewLine);
        sb.Append(" a.uom, a.sku_weight, a.mgrp, comp = cd.comp_div_descr, c.div_code " + System.Environment.NewLine);

        sb.Append(" INTO #lcm  " + System.Environment.NewLine);
        //sb.Append(" INTO dw_work.dbo.lcmPound ");
        //sb.Append(" INTO dw_work.dbo.lcmTon ");
        sb.Append(" FROM  " + System.Environment.NewLine);
        sb.Append(" POSEIDON2.retail_inventory.dbo.retail_inventory a " + System.Environment.NewLine);
        sb.Append(" left join #ssc9000004_skus rx " + System.Environment.NewLine);
        sb.Append("   on a.sku = rx.sku  " + System.Environment.NewLine);
        sb.Append(" left join dw_global.dbo.d_sku_rollup b " + System.Environment.NewLine);
        sb.Append("   on a.sku = b.sku " + System.Environment.NewLine);
        sb.Append(" left join dw_global.dbo.gl_cntr_data c " + System.Environment.NewLine);
        sb.Append("   on a.store_nbr = c.store_num " + System.Environment.NewLine);

        //adding these lines per Joe's suggestion
        sb.Append(" left join dw_global.dbo.comp_div_descr cd " + System.Environment.NewLine);
        sb.Append("  on cd.div_code = c.div_code and cd.comp = c.comp " + System.Environment.NewLine);
        
        sb.Append(" left join " + System.Environment.NewLine);
        //sb.Append(@" (SELECT district, dist_mgr FROM OpenQuery([DNB\DNB_2K01], 'SELECT distinct district, dist_mgr FROM ssc_gl.dbo.gl_district_data')) d " + System.Environment.NewLine);
        sb.Append(@" (SELECT district, dist_mgr FROM OpenQuery([DNBII\Prod1], 'SELECT distinct district, dist_mgr FROM ssc_gl.dbo.gl_district_data')) d " + System.Environment.NewLine);
        sb.Append("   on c.reg + c.dist = d.district " + System.Environment.NewLine);
        sb.Append(" where a.eff_date = @eff_date " + System.Environment.NewLine);
        sb.Append(" and a.sku not like '888%' " + System.Environment.NewLine);
        sb.Append(" and rx.sku is not null " + System.Environment.NewLine);
        //sb.Append(" and a.sku in  " + System.Environment.NewLine);
        //sb.Append(" ( " + System.Environment.NewLine);
        //sb.Append(" 	SELECT distinct sku from " + System.Environment.NewLine);
        //sb.Append(" 	( " + System.Environment.NewLine);
        //sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where pgrp_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Product Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        //sb.Append(" 	union all " + System.Environment.NewLine);
        //sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where comm_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Commodity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        //sb.Append(" 	union all " + System.Environment.NewLine);
        //sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where mgrp_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Margin Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        //sb.Append(" 	union all " + System.Environment.NewLine);
        //sb.Append(" 	SELECT Item sku from dw_work.dbo.ssc9000004_criteria where GroupType = 'Sku' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' " + System.Environment.NewLine);
        //sb.Append(" 	) " + System.Environment.NewLine);
        //sb.Append(" 	a " + System.Environment.NewLine);
        //sb.Append(" ) " + System.Environment.NewLine);
        sb.Append("     and a.store_nbr in " + System.Environment.NewLine);
        sb.Append(" ( " + System.Environment.NewLine);
        sb.Append(" 	SELECT distinct store_num from " + System.Environment.NewLine);
        sb.Append(" 	( " + System.Environment.NewLine);
        sb.Append(" 	SELECT store_num from dw_global.dbo.gl_cntr_data where comp in (SELECT Item FROM dw_work..ssc9000004_criteria WHERE GroupType = 'Entity' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        sb.Append(" 	union all " + System.Environment.NewLine);
        sb.Append(" 	SELECT store_num from dw_global.dbo.gl_cntr_data where reg + dist in (SELECT Item FROM dw_work..ssc9000004_criteria WHERE GroupType = 'District' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
        sb.Append(" 	union all " + System.Environment.NewLine);
        sb.Append(" 	SELECT Item store_num from dw_work..ssc9000004_criteria WHERE GroupType = 'Store #'  and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' " + System.Environment.NewLine);
        sb.Append(" 	) a " + System.Environment.NewLine);
        sb.Append(" ); ");


       
        //string sp = "sp_Estimated_Writedown";

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MarketCostSku"].ToString());
        cn.Open();
        //changing to use a stored procedure instead of concatenated hardcoded SQL
        //SqlCommand cmd = new SqlCommand(sp, cn);
        //cmd.CommandType = CommandType.StoredProcedure;

        ////add the parameters
        //cmd.Parameters.Add("@eff_date", SqlDbType.DateTime).Value = criteria.DateEntered;
        //cmd.Parameters.Add("@frt_per_pound", SqlDbType.Money).Value = frtPerPound;
        //cmd.Parameters.Add("@frt_per_each", SqlDbType.Money).Value = frtPerItem;
        //cmd.Parameters.Add("@openSSUser", SqlDbType.VarChar).Value = criteria.OpenSSUser;
        //cmd.Parameters.Add("@pageIdentifier", SqlDbType.VarChar).Value = criteria.PageIdentifier;
        //cmd.Parameters.Add("@activityID", SqlDbType.Char).Value = criteria.ActivityID;

        SqlCommand cmd = new SqlCommand(sb.ToString(), cn);
        cmd.CommandTimeout = 120;
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            lblStatus.Text = ex.Message;
            lblStatus.Visible = true;
            cn.Close();
            return;
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            lblStatus.Visible = true;
            cn.Close();
            return;
        }

        
        cmd.CommandText = "SELECT a.*, b.mgrp_desc FROM #lcm a left join dw_global.dbo.d_sku_rollup b on a.sku = b.sku order by a.dist_mgr, a.store_nbr, a.mgrp, a.sku ";
        //cmd.CommandText = " SELECT a.*, b.mgrp_desc, c.comp_div_descr FROM #lcm a left join dw_global.dbo.d_sku_rollup b on a.sku = b.sku join dw_global.dbo.comp_div_descr c on a.div_code = c.div_code and a.comp = c.comp order by a.dist_mgr, a.store_nbr, a.mgrp, a.sku";
        SqlDataReader rdr = cmd.ExecuteReader();


        if (rdr.HasRows == false)
        {
            lblStatus.Text = "No records returned.  Please check your selection criteria.";
            lblStatus.Visible = true;
            rdr.Close();
            cn.Close();
            return;
        }


        ExcelApplication xlw;
        Workbook wb;
        Worksheet wsData;
        Worksheet wsSummary;
        xlw = new ExcelApplication();
        wb = xlw.Open(Server.MapPath("ssc9000004Template.xls"));

        wsData = wb.Worksheets[1];
        wsData.Name = "Data";

        SoftArtisans.OfficeWriter.ExcelWriter.Style stylCurrency = wb.CreateStyle();
        stylCurrency.NumberFormat = wb.NumberFormat.CreateAccounting(0, true, null);


        //this is for converting between different units of weight between different SKUs
        Int32 j = 1;
        Int32 currentRowPlus1 = 1;
        while (rdr.Read())
        {

            Decimal auc = Convert.ToDecimal(rdr["auc"].ToString());
            Decimal frt;
            Decimal sku_weight = Convert.ToDecimal(rdr["sku_weight"].ToString());
            switch (rdr["uom"].ToString().ToUpper())
            {
                case "LB":
                case "BG":
                    frt = sku_weight * frtPerPound;
                    break;
                case "EA":
                    frt = frtPerItem;
                    break;
                default:
                    frt = frtPerItem;
                    break;
            }
            Decimal qty_oh = Convert.ToDecimal(rdr["qty_oh"].ToString());
            if (qty_oh <= 0)
            {
                qty_oh = 0;
            }

            //this is where the calculations for the Summary tab appear
            Decimal mkt_dlvd = frt + Convert.ToDecimal(rdr["mkt_cost"].ToString());
            Decimal wd = mkt_dlvd - auc;
            Decimal wd_est = wd * qty_oh;


            //populates the Data tab
            currentRowPlus1 = j + 1;
            wsData.Cells[j, 0].Value = rdr["dist_mgr"]; //DM
            //wsData.Cells[j, 1].Value = rdr["comp_div_descr"];
            wsData.Cells[j, 1].Value = rdr["comp"];
            wsData.Cells[j, 2].Value = rdr["store_nbr"]; //store_nbr
            wsData.Cells[j, 3].Value = rdr["cntr_desc"]; //store_name
            wsData.Cells[j, 4].Value = rdr["sku"]; //sku
            wsData.Cells[j, 5].Value = rdr["sku_desc"].ToString();
            wsData.Cells[j, 6].Value = rdr["mgrp"].ToString();
            wsData.Cells[j, 7].Value = rdr["mgrp_desc"].ToString();
            wsData.Cells[j, 8].Value = Convert.ToDecimal(rdr["qty_oh"].ToString());
            wsData.Cells[j, 9].Value = rdr["uom"]; //AUC
            wsData.Cells[j, 10].Value = auc; //AUC
            wsData.Cells[j, 11].Value = rdr["mkt_cost"]; //mkt
            wsData.Cells[j, 12].Value = frt; //frt
            wsData.Cells[j, 13].Value = mkt_dlvd; //mkt_dlvd

            MarginGroup myLocatedObject = MarginGroups.Find(delegate(MarginGroup m) { return m.Mgrp == rdr["mgrp"].ToString() && m.Entity == rdr["comp"].ToString(); });
            if (myLocatedObject == null)
            {
                //creates the MarginGroup object- need to add the comp_div_descr column from dw_global.dbo.comp_div_descr
                //MarginGroup mg = new MarginGroup(rdr["mgrp"].ToString(), rdr["mgrp_desc"].ToString(), rdr["comp"].ToString(), rdr["comp_div_descr"].ToString(), wd_est);
                MarginGroup mg = new MarginGroup(rdr["mgrp"].ToString(), rdr["mgrp_desc"].ToString(), rdr["comp"].ToString(), wd_est);
                MarginGroups.Add(mg);
            }
            else
            {
                myLocatedObject.Add_WD_est(wd_est);
            }

            if (qty_oh > 0 && wd < 0)
            {

                wsData.Cells[j, 14].Value = wd;
                wsData.Cells[j, 15].Value = wd_est;
            }

            Decimal ton_mkt_auc, ton_mkt_dlvd, ton_qoh;
            switch (rdr["uom"].ToString().ToUpper())
            {
                case "LB":
                case "BG":
                    ton_mkt_auc = (auc / sku_weight) * Convert.ToDecimal(2000.00);
                    ton_mkt_dlvd = (((Convert.ToDecimal(rdr["mkt_cost"].ToString()) / sku_weight) * Convert.ToDecimal(2000.00)) + (frtPerPound * Convert.ToDecimal(2000.00)));
                    ton_qoh = (qty_oh * sku_weight) / Convert.ToDecimal(2000.00);

                    wsData.Cells[j, 16].Value = ton_qoh;
                    wsData.Cells[j, 17].Value = ton_mkt_auc;
                    wsData.Cells[j, 18].Value = Convert.ToDecimal(rdr["mkt_cost"].ToString()) / sku_weight * Convert.ToDecimal(2000.00);
                    wsData.Cells[j, 19].Value = frtPerTon;
                    wsData.Cells[j, 20].Value = ton_mkt_dlvd;
                    if (qty_oh > 0 && wd < 0)
                    {
                        wsData.Cells[j, 21].Value = ton_mkt_dlvd - ton_mkt_auc;
                        wsData.Cells[j, 22].Value = ton_qoh * (ton_mkt_dlvd - ton_mkt_auc);
                    }
                    break;
                default:
                    break;
            }
            j++;
        }
        rdr.Close();


        SoftArtisans.OfficeWriter.ExcelWriter.Style styleGlobal = wb.CreateStyle();
        styleGlobal.Font.Size = 8;

        styleGlobal.Font.Name = "Arial";
        SoftArtisans.OfficeWriter.ExcelWriter.Area aUsedCols = wsData.CreateAreaOfColumns(0, 18);
        //aUsedCols.AutoFitWidth();
        styleGlobal.Font.Size = 8;

        Area pookie = wsData.CreateAreaOfColumns(4, 1);
        pookie.SetColumnWidth(0, pookie.GetColumnWidth(0) + 8);
        pookie = wsData.CreateAreaOfColumns(7, 1);
        pookie.SetColumnWidth(0, pookie.GetColumnWidth(0) + 8);

        wsSummary = wb.Worksheets[0];
        wsSummary.Cells[0, 0].Value = "Retail LCM est created " + DateTime.Now.ToShortDateString() + " inventory as of " + DateTime.Parse(txtInventoryDate.Text).ToShortDateString();

        Int32 rowCounter = 2;

        rowCounter++;
        MarginGroups.Sort();
        foreach (MarginGroup mg in MarginGroups)
        {
            wsSummary.Cells[rowCounter, 0].Value = mg.Entity;
            wsSummary.Cells[rowCounter, 1].Value = mg.Mgrp;
            wsSummary.Cells[rowCounter, 2].Value = mg.Mgrp_name;
            wsSummary.Cells[rowCounter, 3].Value = mg.WD_est;
            wsSummary.Cells[rowCounter, 4].Value = mg.WD_est_neg;
            rowCounter++;
        }

        Worksheet wsCriteria;
        wsCriteria = wb.Worksheets[2];
        int critCurrentRow = 3;

        wsCriteria.Cells[critCurrentRow, 0].Value = "Inventory Date";
        critCurrentRow++;
        wsCriteria.Cells[critCurrentRow, 0].Value = txtInventoryDate.Text;

        critCurrentRow = 3;
        if (radFrtPer.SelectedIndex == 0)
            wsCriteria.Cells[critCurrentRow, 1].Value = "Freight/Pound";
        else
            wsCriteria.Cells[critCurrentRow, 1].Value = "Freight/Ton";
        critCurrentRow++;
        wsCriteria.Cells[critCurrentRow, 1].Value = txtFreightPerPound.Text;
        critCurrentRow++;

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 2].Value = "Freight/Item";
        critCurrentRow++;
        wsCriteria.Cells[critCurrentRow, 2].Value = txtFreightPerItem.Text;
        critCurrentRow++;

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 3].Value = "Commodities";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Commodity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Commodity' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 3].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 4].Value = "Product Groups";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Product Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Product Group' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 4].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 5].Value = "Margin Groups";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Margin Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Margin Group' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 5].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 6].Value = "SKUs";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Sku' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Sku' and OpenSSUser = '" + OpenSSCookie + "'  and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 6].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 7].Value = "Entities";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Entity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Entity' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 7].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 8].Value = "Districts";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'District' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'District' and OpenSSUser = '" + OpenSSCookie + "' and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 8].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        critCurrentRow = 3;
        wsCriteria.Cells[critCurrentRow, 9].Value = "Stores";
        critCurrentRow++;
        //cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Store #' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
        cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Store #' and OpenSSUser = '" + OpenSSCookie + "'  and ActivityID = 'ssc9000004' ";
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            wsCriteria.Cells[critCurrentRow, 9].Value = rdr["Item"].ToString();
            critCurrentRow++;
        }
        rdr.Close();

        Area critArea = wsCriteria.CreateAreaOfColumns(0, 10);

        wsCriteria.Cells[0, 0].Value = "This is the criteria that was selected.";

        cn.Close();
        xlw.Save(wb, Page.Response, "lcm.xls", false);
    }

    private void PopulateDistrictsDropDown(DataTable dtDistricts)
    {
        try
        {
            ddlDistrictsNoAjax.DataSource = dtDistricts;
            ddlDistrictsNoAjax.DataTextField = dtDistricts.Columns[1].ToString();
            ddlDistrictsNoAjax.DataValueField = dtDistricts.Columns[1].ToString();
            ddlDistrictsNoAjax.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void PopulateStoresDropDown(DataTable dtStores)
    {
        try
        {
            ddlStores.DataSource = dtStores;
            ddlStores.DataTextField = dtStores.Columns[3].ToString();
            ddlStores.DataValueField = dtStores.Columns[3].ToString();
            ddlStores.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void PopulateMainProductDropDown()
    {
        try
        {
            DataTable dtMainCommodities = new DataTable();
            dw_global_D_SKU_ROLLUP skuRollUp = new dw_global_D_SKU_ROLLUP();
            dtMainCommodities = skuRollUp.GetMainProductCategories();
            ddlMainProductCategories.DataSource = dtMainCommodities;
            ddlMainProductCategories.DataTextField = dtMainCommodities.Columns[1].ToString();
            ddlMainProductCategories.DataValueField = dtMainCommodities.Columns[1].ToString();
            ddlMainProductCategories.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error has occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void ddlEntitiesNoAjax_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEntitiesNoAjax.SelectedValue == "Select All")
            {
            
            }
            else if (ddlEntitiesNoAjax.SelectedValue == "Please Select")
            {

            }
            else
            {
                dw_global_GL_CENTER_DATA gl = new dw_global_GL_CENTER_DATA();
                DataTable dtDistricts = new DataTable();
                DataTable dtConfigData = new DataTable();
                //look up the comp and div code by the selected value
                dtConfigData = gl.GetConfigData(ddlEntitiesNoAjax.SelectedValue);
                dtDistricts = gl.GetDistricts(dtConfigData.Rows[0][0].ToString(), dtConfigData.Rows[0][2].ToString());
                PopulateDistrictsDropDown(dtDistricts);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error has occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }


    protected void ddlDistrictsNoAjax_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dw_global_GL_CENTER_DATA gl = new dw_global_GL_CENTER_DATA();
            DataTable dtStores = new DataTable();
            DataTable dtConfigData = new DataTable();
            //look up the comp and div code by the selected value
            dtConfigData = gl.GetConfigData(ddlEntitiesNoAjax.SelectedValue);
            dtStores = gl.GetStores(dtConfigData.Rows[0][0].ToString(), dtConfigData.Rows[0][2].ToString(), ddlDistrictsNoAjax.SelectedValue);
            PopulateStoresDropDown(dtStores);
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnSelectEntity_Click1(object sender, EventArgs e)
    {
        try
        {
            string entity = "Entity";
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            if (ddlEntitiesNoAjax.SelectedValue != "Please Select" && ddlEntitiesNoAjax.SelectedValue != "Select All")
            {
                criteria.GroupType = entity;
                criteria.Item = GetCompValue(ddlEntitiesNoAjax.SelectedValue);
                criteria.ItemDescription = GetDescriptionValue(ddlEntitiesNoAjax.SelectedValue);
                criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
                PopulateEntityGrid();
            }
            if (ddlEntitiesNoAjax.SelectedValue == "Please Select")
            {
                lblError.Text = "Please Select an entity";
                lblError.Visible = true;
            }
            if (ddlEntitiesNoAjax.SelectedValue == "Select All")
            {
                dw_work_SSC9000004_CRITERIA coops = new dw_work_SSC9000004_CRITERIA();
                dw_work_SSC9000004_CRITERIA services = new dw_work_SSC9000004_CRITERIA();
                dw_work_SSC9000004_CRITERIA retail = new dw_work_SSC9000004_CRITERIA();
                dw_work_SSC9000004_CRITERIA turf = new dw_work_SSC9000004_CRITERIA();

                coops.GroupType = entity;
                services.GroupType = entity;
                retail.GroupType = entity;
                turf.GroupType = entity;

                coops.Item = "RCOOP";
                services.Item = "RSERV";
                retail.Item = "RSERV";
                turf.Item = "TURF";

                coops.ItemDescription = "Retail Ops - Coops";
                services.ItemDescription = "Retail Ops - Services";
                retail.ItemDescription = "Agronomy - Retail Locations";
                turf.ItemDescription = "Agronomy - Turf";

                coops.InsertParameters(coops.GroupType, coops.Item, coops.ItemDescription, coops.OpenSSUser, coops.PageIdentifier, coops.ActivityID);
                services.InsertParameters(services.GroupType, services.Item, services.ItemDescription, services.OpenSSUser, services.PageIdentifier, services.ActivityID);
                retail.InsertParameters(retail.GroupType, retail.Item, retail.ItemDescription, retail.OpenSSUser, retail.PageIdentifier, retail.ActivityID);
                turf.InsertParameters(turf.GroupType, turf.Item, turf.ItemDescription, turf.OpenSSUser, turf.PageIdentifier, turf.ActivityID);
                PopulateEntityGrid();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnSelectDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            criteria.GroupType = "District";
            criteria.Item = ddlDistrictsNoAjax.SelectedValue;
            criteria.ItemDescription = ddlDistrictsNoAjax.SelectedValue;
            criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
            PopulateEntityGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnSelectStore_Click(object sender, EventArgs e)
    {
        try
        {
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            criteria.GroupType = "Store #";
            criteria.Item = ddlStores.SelectedValue;
            criteria.ItemDescription = ddlStores.SelectedValue;
            criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
            PopulateEntityGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void ddlMainProductCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtProductGroups = new DataTable();
            dw_global_D_SKU_ROLLUP sku = new dw_global_D_SKU_ROLLUP();
            dtProductGroups = sku.GetProductGroupByCommodity(ddlMainProductCategories.SelectedValue);
            ddlProductGroup.DataSource = dtProductGroups;
            ddlProductGroup.DataTextField = dtProductGroups.Columns[1].ToString();
            ddlProductGroup.DataValueField = dtProductGroups.Columns[1].ToString();
            ddlProductGroup.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMarginGroups = new DataTable();
            dw_global_D_SKU_ROLLUP sku = new dw_global_D_SKU_ROLLUP();
            dtMarginGroups = sku.GetMarginGroupByProductGroup(ddlMainProductCategories.SelectedValue, ddlProductGroup.SelectedValue);
            ddlMarginGroups.DataSource = dtMarginGroups;
            ddlMarginGroups.DataTextField = dtMarginGroups.Columns[1].ToString();
            ddlMarginGroups.DataValueField = dtMarginGroups.Columns[1].ToString();
            ddlMarginGroups.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void ddlMarginGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtProducts = new DataTable();
            dw_global_D_SKU_ROLLUP sku = new dw_global_D_SKU_ROLLUP();
            dtProducts = sku.GetProductsByMarginGroup(ddlMainProductCategories.SelectedValue, ddlProductGroup.SelectedValue, ddlMarginGroups.SelectedValue);
            ddlSKU.DataSource = dtProducts;
            ddlSKU.DataTextField = dtProducts.Columns[1].ToString();
            ddlSKU.DataValueField = dtProducts.Columns[1].ToString();
            ddlSKU.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnAddMainCategory_Click(object sender, EventArgs e)
    {
        try
        {
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            criteria.GroupType = "Commodity";
            criteria.Item = ddlMainProductCategories.SelectedValue.Substring(0, 2);
            criteria.ItemDescription = ddlMainProductCategories.SelectedValue;
            criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
            PopulateProductGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnAddProductGroup_Click(object sender, EventArgs e)
    {
        try
        {
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            criteria.GroupType = "Product Group";
            criteria.Item = ddlProductGroup.SelectedValue.Substring(0, 2);
            criteria.ItemDescription = ddlProductGroup.SelectedValue;
            criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
            PopulateProductGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnAddMarginGroup_Click(object sender, EventArgs e)
    {
        try
        {
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            dw_global_D_SKU_ROLLUP rollup = new dw_global_D_SKU_ROLLUP();
            criteria.GroupType = "Margin Group";
            string[] selectedArr = ddlMarginGroups.SelectedValue.Split(' ');
            criteria.Item = selectedArr[0];
            criteria.ItemDescription = rollup.GetMarginGroupDescriptionByMGRPKEY(criteria.Item);
            criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
            PopulateProductGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnAddSKU_Click(object sender, EventArgs e)
    {
        try
        {
            dw_work_SSC9000004_CRITERIA criteria = new dw_work_SSC9000004_CRITERIA();
            dw_global_D_SKU_ROLLUP rollup = new dw_global_D_SKU_ROLLUP();
            criteria.GroupType = "Sku";
            string[] selectedArr = ddlSKU.SelectedValue.Split(' ');
            criteria.Item = selectedArr[0];
            criteria.ItemDescription = rollup.GetSKUDescriptionBySKU(criteria.Item);
            criteria.InsertParameters(criteria.GroupType, criteria.Item, criteria.ItemDescription, criteria.OpenSSUser, criteria.PageIdentifier, criteria.ActivityID);
            PopulateProductGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error as occurred: " + ex.Message;
            lblError.Visible = true;
        }
    }

    class MarginGroup : IComparable<MarginGroup>
    {
        String _mgrp;

        public String Mgrp
        {
            get { return _mgrp; }
            set { _mgrp = value; }
        }
        String _mgrp_name;

        public String Mgrp_name
        {
            get { return _mgrp_name; }
            set { _mgrp_name = value; }
        }
        String _entity;

        public String Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        //adding Entity Description per Denise's email on 11/20
        String _entityDescription;

        public String EntityDescription
        {
            get { return _entityDescription; }
            set { _entityDescription = value; }
        }

        Decimal _WD_est;

        public Decimal WD_est
        {
            get { return _WD_est; }
        }

        Decimal _WD_est_neg;

        public Decimal WD_est_neg
        {
            get { return _WD_est_neg; }
        }

        public void Add_WD_est(Decimal WD_est)
        {
            _WD_est += WD_est;
            if (WD_est < 0)
                _WD_est_neg += WD_est;
        }

        //the constructor
        public MarginGroup(String Mgrp, String mGrp_name, String Entity, Decimal WD_est)
        {
            this.Mgrp = Mgrp;
            this.Mgrp_name = mGrp_name;
            this.Entity = Entity;
            _WD_est = WD_est;
            if (WD_est < 0)
                _WD_est_neg = WD_est;

        }

        public Boolean MatchesEntityAndMgrp(String Entity, String Margin)
        {
            if (this.Mgrp == Margin && this.Entity == Entity)
                return true;
            else
                return false;
        }

        public int CompareTo(MarginGroup x)
        {
            if (this.Entity.CompareTo(x.Entity) != 0)
            {
                return this.Entity.CompareTo(x.Entity);
            }
            else
            {
                return this.Mgrp.CompareTo(x.Mgrp);
            }
        }
    }


    
}