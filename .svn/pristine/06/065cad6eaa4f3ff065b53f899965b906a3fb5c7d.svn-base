using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for dw_global_D_SKU_ROLLUP
/// </summary>
public class dw_global_D_SKU_ROLLUP
{

    private string dw_globalConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MarketCostSku"].ToString();
        }
    }

    //searches the d_sku_rollup_vw (a view of d_sku_rollup) by Product, Commodity, and Margin
    private DataTable GetSKURollUpVW(string pgrpkey, string commkey, string mgrpkey)
    {
        DataTable dtSKURollUpVW = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select * from d_sku_rollup_vw where pgrp_key = @pgrpkey and comm_key = @commkey and mgrp_key = @mgrpkey";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter pgrpPRM = new SqlParameter("@pgrpkey", pgrpkey);
                SqlParameter commPRM = new SqlParameter("@commkey", commkey);
                SqlParameter mgrpPRM = new SqlParameter("@mgrpkey", mgrpkey);
                cmd.Parameters.Add(pgrpPRM);
                cmd.Parameters.Add(commPRM);
                cmd.Parameters.Add(mgrpPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtSKURollUpVW);
                }
            }
        }
        return dtSKURollUpVW;
    }

    private DataTable GetSKURollUpVW(string pgrpkey)
    {
        DataTable dtSKURollUpVW = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select * from d_sku_rollup_vw where pgrp_key = @pgrpkey";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter pgrpPRM = new SqlParameter("@pgrpkey", pgrpkey);
                cmd.Parameters.Add(pgrpPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtSKURollUpVW);
                }
            }
        }
        return dtSKURollUpVW;
    }


    public DataTable GetProductsByMarginGroup(string mgrpDesc)
    {
        DataTable dtProducts = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select sku, sku_desc from dbo.d_SKU_Rollup_vw where mgrp_desc = @mgrpdesc order by sku";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                //SqlParameter commkeyPRM = new SqlParameter("@commdesc", commdesc);
                //SqlParameter pgrpkeyPRM = new SqlParameter("@pgrpdesc", pgrpDesc);
                SqlParameter mgrpkeyPRM = new SqlParameter("@mgrpdesc", mgrpDesc);
                //cmd.Parameters.Add(commkeyPRM);
                //cmd.Parameters.Add(pgrpkeyPRM);
                cmd.Parameters.Add(mgrpkeyPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtProducts);
                }
            }
        }
        return dtProducts;
    }

    //For getting SKU's by by Margin group. Takes 3 parameters- 4th level down
    public DataTable GetProductsByMarginGroup(string commdesc, string pgrpDesc, string mgrpDesc)
    {
        DataTable dtProducts = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select sku, sku_desc from dbo.d_SKU_Rollup_vw where comm_desc = @commdesc and pgrp_desc = @pgrpdesc and mgrp_desc = @mgrpdesc order by sku";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter commkeyPRM = new SqlParameter("@commdesc", commdesc);
                SqlParameter pgrpkeyPRM = new SqlParameter("@pgrpdesc", pgrpDesc);
                SqlParameter mgrpkeyPRM = new SqlParameter("@mgrpdesc", mgrpDesc);
                cmd.Parameters.Add(commkeyPRM);
                cmd.Parameters.Add(pgrpkeyPRM);
                cmd.Parameters.Add(mgrpkeyPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtProducts);
                }
            }
        }
        return dtProducts;
    }


    //For getting Margin groups-3rd level down from Commodities. Takes comm_key and pgrpkey as parameters- 3rd level down
    public DataTable GetMarginGroupByProductGroup(string commDesc, string pgrpDesc)
    {
        DataTable dtMarginGroups = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select mgrp_key, mgrp_desc from dbo.d_SKU_Rollup_vw where comm_desc = @commdesc and pgrp_desc = @pgrpdesc group by mgrp_key, mgrp_desc order by mgrp_desc";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter commkeyPRM = new SqlParameter("@commdesc", commDesc);
                SqlParameter pgrpkeyPRM = new SqlParameter("@pgrpdesc", pgrpDesc);
                cmd.Parameters.Add(commkeyPRM);
                cmd.Parameters.Add(pgrpkeyPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtMarginGroups);
                }
            }
        }
        return dtMarginGroups;
    }


    //For getting product groups- 2nd level down from Commodities. Takes comm_key as an argument
    public DataTable GetProductGroupByCommodity(string commDesc)
    {
        DataTable dtProductGroups = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select pgrp_key, pgrp_desc from dbo.d_SKU_Rollup_vw where comm_desc = @commdesc group by pgrp_key, pgrp_desc order by pgrp_desc";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter commkeyPRM = new SqlParameter("@commdesc", commDesc);
                cmd.Parameters.Add(commkeyPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtProductGroups);
                }
            }
        }
        return dtProductGroups;
    }

    //ssc9000004- gets the default list of top-level product categories displayed in the Product Gridview
    public DataTable GetMainProductCategories()
    {
        DataTable dtMainProductCategories = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "SELECT comm_key, comm_desc FROM dbo.d_SKU_Rollup_vw group by comm_key, comm_desc order by comm_key";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtMainProductCategories);
                }
            }
        }
        return dtMainProductCategories;
    }

    public string GetSKUDescriptionBySKU(string sku)
    {
        DataTable dtSKU = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select sku_desc from dbo.d_SKU_Rollup_vw where sku = @sku";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter commkeyPRM = new SqlParameter("@sku", sku);
                cmd.Parameters.Add(commkeyPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtSKU);
                }
            }
        }
        string skuDesc = dtSKU.Rows[0]["sku_desc"].ToString();
        return skuDesc;
    }

    public string GetProductGroupDescriptionByPGRPKey(string pgrpkey)
    {
        DataTable dtSKU = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select top 1 pgrp_desc from dbo.d_SKU_Rollup_vw where pgrp_key = @pgrpkey";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter pgrpPRM = new SqlParameter("@pgrpkey", pgrpkey);
                cmd.Parameters.Add(pgrpPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtSKU);
                }
            }
        }
        string pgrpDesc = dtSKU.Rows[0]["pgrp_desc"].ToString();
        return pgrpDesc;
    }

    public string GetMarginGroupDescriptionByMGRPKEY(string mgrpkey)
    {
        DataTable dtMGRP = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select top 1 mgrp_desc from dbo.d_SKU_Rollup_vw where mgrp_key = @mgrpkey";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter mgrpPRM = new SqlParameter("@mgrpkey", mgrpkey);
                cmd.Parameters.Add(mgrpPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtMGRP);
                }
            }
        }
        string mgrpDesc = dtMGRP.Rows[0]["mgrp_desc"].ToString();
        return mgrpDesc;
    }

	public dw_global_D_SKU_ROLLUP()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}