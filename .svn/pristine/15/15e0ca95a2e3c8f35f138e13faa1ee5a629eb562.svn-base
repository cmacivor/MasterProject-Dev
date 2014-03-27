using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// This class exposes methods to access the gl_cntr_data table in the dw_global database on Poseidon2. It is used by following applications:
/// ssc9000004 - Estimated Writedown Query
/// </summary>
public class dw_global_GL_CENTER_DATA
{
    private string dw_globalConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MarketCostSku"].ToString(); 
        }
    }

    public DataTable GetConfigData(string compDivDescr)
    {
        DataTable dtDescr = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            string sql = "select * from dbo.comp_div_descr where comp_div_descr = @descr";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter compPRM = new SqlParameter("@descr", compDivDescr);
                cmd.Parameters.Add(compPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtDescr);
                }
            }
        }
        return dtDescr;
    }


    //for getting Districts
    public DataTable GetDistricts(string compCode, string divCode)
    {
        DataTable dtRCOOPDistricts = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select comp, roll_up from dbo.gl_cntr_data gl ");
            sb.Append("where comp = @comp and gl.div_code = @divcode and ");
            sb.Append("gl.close_date is null ");
            sb.Append("group by roll_up, comp order by roll_up asc");

            string sql = sb.ToString();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter compPRM = new SqlParameter("@comp", compCode);
                SqlParameter divPRM = new SqlParameter("@divcode", divCode);
                cmd.Parameters.Add(compPRM);
                cmd.Parameters.Add(divPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtRCOOPDistricts);
                }
            }
        }
        return dtRCOOPDistricts;
    }

    //get the stores by comp, div code, and district ID
    public DataTable GetStores(string compCode, string divCode, string districtID)
    {
        DataTable dtStores = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        {   
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from dbo.gl_cntr_data gl ");
            sb.Append("where comp = @comp and gl.div_code = @divCode and ");
            sb.Append("gl.close_date is null and ");
            sb.Append("roll_up = @districtID ");
            sb.Append("order by roll_up asc");
            string sql = sb.ToString();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter compPRM = new SqlParameter("@comp", compCode);
                SqlParameter divPRM = new SqlParameter("@divCode", divCode);
                SqlParameter districtPRM = new SqlParameter("@districtID", districtID);
                cmd.Parameters.Add(compPRM);
                cmd.Parameters.Add(divPRM);
                cmd.Parameters.Add(districtPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtStores);
                }
            }
        }
        return dtStores;
    }

    //Retail Ops Division-Services districts
    //public DataTable GetOPSDivisionServicesDistricts()
    //{
    //    DataTable dtOpsDivDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select comp, roll_up from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'RSERV' and gl.div_code  = 'AG' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("wss.cust_type_grp = 'RS' ");
    //        sb.Append("group by roll_up, comp order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtOpsDivDistricts);
    //            }
    //        }
    //    }
    //    return dtOpsDivDistricts;
    //}

    //Get Agronomy districts
    //public DataTable GetAgronomyRetailAgDistricts()
    //{
    //    DataTable dtAgronRetailDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select comp, roll_up from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'RSERV' and gl.div_code  = 'AY' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("wss.cust_type_grp = 'RS' ");
    //        sb.Append("group by roll_up, comp order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtAgronRetailDistricts);
    //            }
    //        }
    //    }
    //    return dtAgronRetailDistricts;
    //}

    //Agronomy Division - Wholesale Facilities- talk to Darren Hutson about Division Code AY
    //public DataTable GetAgronomyWholesaleFacilitiesDistricts()
    //{
    //    DataTable dtWholesaleDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select comp, roll_up from dbo.gl_cntr_data gl ");
    //        sb.Append("where comp = 'SSC' and ");
    //        sb.Append("gl.roll_up not like 'FD%' and ");
    //        sb.Append("gl.roll_up not like 'PT%' and ");
    //        sb.Append("gl.roll_up not like 'GM%' and ");
    //        sb.Append("gl.roll_up not like 'FH%' and ");
    //        sb.Append("gl.close_date is null ");
    //        sb.Append("group by roll_up, comp order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtWholesaleDistricts);
    //            }
    //        }
    //    }
    //    return dtWholesaleDistricts;
    //}

    //get Agronomy TURF divsion districts
    //public DataTable GetAgronomyTurfDistricts()
    //{
    //    DataTable dtTurfDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select comp, roll_up from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'TURF' and gl.div_code  = 'TF' and ");
    //        sb.Append("gl.close_date is null ");
    //        sb.Append("group by roll_up, comp order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtTurfDistricts);
    //            }
    //        }
    //    }
    //    return dtTurfDistricts;
    //}


    ///*****Following methods are for drilling down from the district level to the store level.****

    ///--COOPS
    //public DataTable GetCOOPStoresByDistrictID(string districtID)
    //{
    //    DataTable dtCOOPStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select * from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'RCOOP' and gl.div_code = 'AG' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("wss.cust_type_grp = 'MC' and ");
    //        sb.Append("roll_up = @districtID ");
    //        sb.Append("order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtCOOPStores);
    //            }
    //        }
    //    }
    //    return dtCOOPStores;
    //}

    //--Retail Ops Division-Services
    //public DataTable GetRetailOpsDivisionStoresByDistrictID(string districtID)
    //{
    //    DataTable dtCOOPStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select * from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'RSERV' and gl.div_code  = 'AG' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("wss.cust_type_grp = 'RS' and ");
    //        sb.Append("roll_up = @districtID ");
    //        sb.Append("order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtCOOPStores);
    //            }
    //        }
    //    }
    //    return dtCOOPStores;
    //}


    //--Agronomy Division - Retail Ag
    //public DataTable GetRetailAgStoresByDistrictID(string districtID)
    //{
    //    DataTable dtCOOPStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select * from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'RSERV' and gl.div_code  = 'AY' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("wss.cust_type_grp = 'RS' and ");
    //        sb.Append("roll_up = @districtID ");
    //        sb.Append("order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtCOOPStores);
    //            }
    //        }
    //    }
    //    return dtCOOPStores;
    //}


    //Wholesale
    //public DataTable GetWholesaleStoresByDistrictID(string districtID)
    //{
    //    DataTable dtCOOPStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select * from dbo.gl_cntr_data gl ");
    //        sb.Append("where comp = 'SSC' and ");
    //        sb.Append("gl.roll_up not like 'FD%' and ");
    //        sb.Append("gl.roll_up not like 'PT%' and ");
    //        sb.Append("gl.roll_up not like 'GM%' and ");
    //        sb.Append("gl.roll_up not like 'FH%' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("roll_up = @districtID ");
    //        sb.Append("order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtCOOPStores);
    //            }
    //        }
    //    }
    //    return dtCOOPStores;
    //}


    //Agronomy- Turf
    //public DataTable GetTurfStoresByDistrictID(string districtID)
    //{
    //    DataTable dtCOOPStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("select * from dbo.gl_cntr_data gl ");
    //        sb.Append("join dbo.wss_custvend wss on ");
    //        sb.Append("wss.cvid = gl.store_num ");
    //        sb.Append("where comp = 'TURF' and gl.div_code  = 'TF' and ");
    //        sb.Append("gl.close_date is null and ");
    //        sb.Append("roll_up = @districtID ");
    //        sb.Append("order by roll_up asc");
    //        string sql = sb.ToString();
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtCOOPStores);
    //            }
    //        }
    //    }
    //    return dtCOOPStores;
    //}


    ///Following are methods written before discussion with Anne Clingenpeel- will keep for now


    //get a list of all districts
    //public DataTable GetAllDistricts()
    //{
    //    DataTable dtDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        string sql = "select roll_up from dbo.gl_cntr_data where roll_up != '' and div_code in ('AG', 'RE', 'AY', 'TF') group by roll_up order by roll_up";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtDistricts);
    //            }
    //        }
    //    }
    //    return dtDistricts;
    //}

    //gets a list of RCOOP districts
    //public DataTable GetRCOOPDistricts()
    //{
    //    DataTable dtRCOOPDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RCOOP' and roll_up in ('01A', '01B', '01D', '01E', '01F', '01G', '01Y', '02A', '02B', '02C', '02D', '02E', '02G', '02Y', '03A', '03Y', '04Y', '05Y')order by roll_up asc";
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RCOOP' and div_code = 'AG' order by roll_up asc";
    //        string sql = "select comp, roll_up from dbo.gl_cntr_data where comp = 'RCOOP' and div_code = 'AG' group by roll_up, comp order by roll_up asc";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtRCOOPDistricts);
    //            }
    //        }
    //    }
    //    return dtRCOOPDistricts;
    //}

    //gets a list of Retail Ops Division-Services districts
    //public DataTable GetRuralAmericaDistricts()
    //{
    //    DataTable dtRuralAmericaistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RCOOP' and roll_up in ('01A', '01B', '01D', '01E', '01F', '01G', '01Y', '02A', '02B', '02C', '02D', '02E', '02G', '02Y', '03A', '03Y', '04Y', '05Y')order by roll_up asc";
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RSERV' and div_code  in ('AG', 'RE')";
    //        string sql = "select comp, roll_up from dbo.gl_cntr_data where comp = 'RSERV' and div_code  in ('AG', 'RE') group by roll_up, comp order by roll_up asc";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtRuralAmericaistricts);
    //            }
    //        }
    //    }
    //    return dtRuralAmericaistricts;
    //}

    //gets a list of Agronomy Districts
    //public DataTable GetAgronomyDistricts()
    //{
    //    DataTable dtAgronomyDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RCOOP' and roll_up in ('01A', '01B', '01D', '01E', '01F', '01G', '01Y', '02A', '02B', '02C', '02D', '02E', '02G', '02Y', '03A', '03Y', '04Y', '05Y')order by roll_up asc";
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RSERV' and div_code  = 'AY'";
    //        //string sql = "select comp, roll_up from dbo.gl_cntr_data where comp = 'RSERV' and div_code  = 'AY' group by roll_up, comp";
    //        string sql = "select comp, roll_up from dbo.gl_cntr_data where comp in ('RSERV', 'TURF') and div_code in ('AY', 'TF') and roll_up != '' group by roll_up, comp";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtAgronomyDistricts);
    //            }
    //        }
    //    }
    //    return dtAgronomyDistricts;
    //}

    //gets a list of Turf districts
    //public DataTable GetTurfDistricts()
    //{
    //    DataTable dtTurfDistricts = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'RCOOP' and roll_up in ('01A', '01B', '01D', '01E', '01F', '01G', '01Y', '02A', '02B', '02C', '02D', '02E', '02G', '02Y', '03A', '03Y', '04Y', '05Y')order by roll_up asc";
    //        //string sql = "select * from dbo.gl_cntr_data where comp = 'TURF' and div_code  = 'TF' and roll_up != ''";
    //        string sql = "select comp, roll_up from dbo.gl_cntr_data where comp = 'TURF' and div_code  = 'TF' and roll_up != '' group by roll_up, comp";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtTurfDistricts);
    //            }
    //        }
    //    }
    //    return dtTurfDistricts;
    //}

    ///*****Following methods are for drilling down from the district level to the store level.****

    /// <summary>
    /// This method is for getting RCOOP stores by District
    /// </summary>
    /// <param name="districtID"></param>
    /// <returns></returns>
    //public DataTable GetRCOOPStoresByDistrict(string districtID)
    //{
    //    DataTable dtStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        string sql = "select * from dbo.gl_cntr_data where comp = 'RCOOP' and div_code = 'AG' and roll_up =  @districtID order by roll_up asc";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtStores);
    //            }
    //        }
    //    }
    //    return dtStores;
    //}

    /// <summary>
    /// This method is for getting Rural America stores by District
    /// </summary>
    /// <param name="districtID"></param>
    /// <returns></returns>
    //public DataTable GetRuralAmericaStoresByDistrict(string districtID)
    //{
    //    DataTable dtStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        string sql = "select * from dbo.gl_cntr_data where comp = 'RSERV' and div_code  in ('AG', 'RE') and roll_up = @districtID ";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtStores);
    //            }
    //        }
    //    }
    //    return dtStores;
    //}

    /// <summary>
    /// This method is for getting Agronomy stores by District
    /// </summary>
    /// <param name="districtID"></param>
    /// <returns></returns>
    //public DataTable GetAgronomyStoresByDistrict(string districtID)
    //{
    //    DataTable dtStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        string sql = "select * from dbo.gl_cntr_data where comp = 'RSERV' and div_code  = 'AY' and roll_up = @districtID ";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtStores);
    //            }
    //        }
    //    }
    //    return dtStores;
    //}

    /// <summary>
    /// This method is for getting Turf stores by District
    /// </summary>
    /// <param name="districtID"></param>
    /// <returns></returns>
    //public DataTable GetTurfStoresByDistrict(string districtID)
    //{
    //    DataTable dtStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        string sql = "select * from dbo.gl_cntr_data where comp = 'TURF' and div_code  = 'TF' and roll_up = @districtID ";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter districtIDPRM = new SqlParameter("@districtID", districtID);
    //            cmd.Parameters.Add(districtIDPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtStores);
    //            }
    //        }
    //    }
    //    return dtStores;
    //}

    //public DataTable GetRecordByStoreNumber(string storeNumber)
    //{
    //    DataTable dtStores = new DataTable();
    //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
    //    {
    //        string sql = "select * from dw_global.dbo.gl_cntr_data where store_num = @storeNumber";
    //        using (SqlCommand cmd = new SqlCommand(sql, conn))
    //        {
    //            SqlParameter storeNumberPRM = new SqlParameter("@storeNumber", storeNumber);
    //            cmd.Parameters.Add(storeNumberPRM);
    //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //            {
    //                ad.Fill(dtStores);
    //            }
    //        }
    //    }
    //    return dtStores;
    //}

	public dw_global_GL_CENTER_DATA()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    
}