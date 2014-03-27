﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;

[System.Web.Script.Services.ScriptService]
public partial class ssc1900021_AJAX_ssc1900021v2 : System.Web.UI.Page
{
    [WebMethod(EnableSession = true)]
    public static object Numbers(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        return TCStoreMerchXref.NumbersBL(jtStartIndex, jtPageSize, jtSorting);
    }




    public class TCStoreMerchXref
    {
        public string Merch_Nbr { get; set; }
        public string RSS_Location_Nbr { get; set; }
        //public string RSS_Location_Nbr { get; set; }
        //public string Merch_Nbr { get; set; }

        public static object NumbersBL(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                TCStoreMerchXref stores = new TCStoreMerchXref();
                int recordCount = stores.RecordCount();
                List<TCStoreMerchXref> storeRecords = stores.GetNumbers(jtStartIndex, jtPageSize, jtSorting);

                return new { Result = "OK", Records = storeRecords, TotalRecordCount = recordCount };
            }
            catch (Exception ex)
            {
                return new { Result = "Error", Message = ex.Message };
            }
        }


        //*********
        //Data Access Methods
        //*********

        public int RecordCount()
        {
            int count = 0;
            //List<TCStoreMerchXref> records = new List<TCStoreMerchXref>();
            //records = this.GetNumbers();
            //count = records.Count();

            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            using (SqlConnection conn = new SqlConnection(connection.TestTCMasterConnectionString))
            {
                string sql = "select COUNT(*) from TC_store_merch_xref";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            return count;
        }


        public SqlCommand getsqlcommand(int jtStartIndex, int jtPageSize, string sorting)
        {
            int jtEndIndex = jtStartIndex + jtPageSize;
            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            using (SqlConnection conn = new SqlConnection(connection.TestTCMasterConnectionString))
            {
                //string sql = "select * from dbo.TC_store_merch_xref order by RSS_Location_Nbr";
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM ");
                sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY @Sort) AS Row, * FROM dbo.TC_store_merch_xref) ");
                //sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY RSS_Location_Nbr ASC) AS Row, * FROM dbo.TC_store_merch_xref)  ");
                sb.Append("AS StudentsWithRowNumbers ");
                sb.Append("WHERE Row > @StartIndex AND Row <= @EndIndex");

                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn))
                {
                    SqlParameter paramSort = new SqlParameter("@Sort", sorting);
                    SqlParameter paramStart = new SqlParameter("@StartIndex", jtStartIndex);
                    SqlParameter paramSize = new SqlParameter("@EndIndex", jtPageSize);
                    cmd.Parameters.Add(paramSort);
                    cmd.Parameters.Add(paramStart);
                    cmd.Parameters.Add(paramSize);

                    string query = cmd.CommandText;
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        query = query.Replace(p.ParameterName, p.Value.ToString());
                    }
                    return cmd;
                }

                
            }

        }


        public string getsql(int jtStartIndex, int jtPageSize, string sorting)
        {
            int jtEndIndex = jtStartIndex + jtPageSize;
            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            using (SqlConnection conn = new SqlConnection(connection.TestTCMasterConnectionString))
            {
                //string sql = "select * from dbo.TC_store_merch_xref order by RSS_Location_Nbr";
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM ");
                sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY @Sort) AS Row, * FROM dbo.TC_store_merch_xref) ");
                //sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY RSS_Location_Nbr ASC) AS Row, * FROM dbo.TC_store_merch_xref)  ");
                sb.Append("AS StudentsWithRowNumbers ");
                sb.Append("WHERE Row > @StartIndex AND Row <= @EndIndex");

                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn))
                {
                    SqlParameter paramSort = new SqlParameter("@Sort", sorting);
                    SqlParameter paramStart = new SqlParameter("@StartIndex", jtStartIndex);
                    SqlParameter paramSize = new SqlParameter("@EndIndex", jtPageSize);
                    cmd.Parameters.Add(paramSort);
                    cmd.Parameters.Add(paramStart);
                    cmd.Parameters.Add(paramSize);

                    string query = cmd.CommandText;
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        query = query.Replace(p.ParameterName, p.Value.ToString());
                    }
                    return query;
                }
            }
        }


        //to try using a datatable
        public DataTable dtGetNumbers(int jtStartIndex, int jtPageSize, string sorting)
        {
            DataTable dt = new DataTable();
            int jtEndIndex = jtStartIndex + jtPageSize;
            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            using (SqlConnection conn = new SqlConnection(connection.TestTCMasterConnectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM ");
                sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY " + sorting + ") AS Row, * FROM dbo.TC_store_merch_xref) ");
                //sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY RSS_Location_Nbr ASC) AS Row, * FROM dbo.TC_store_merch_xref)  ");
                sb.Append("AS StudentsWithRowNumbers ");
                sb.Append("WHERE Row > @StartIndex AND Row <= @EndIndex");

                string sql = sb.ToString();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //SqlParameter paramSort = new SqlParameter("@Sorting", sorting);
                    SqlParameter paramStart = new SqlParameter("@StartIndex", jtStartIndex);
                    SqlParameter paramSize = new SqlParameter("@EndIndex", jtEndIndex);
                    //cmd.Parameters.Add(paramSort);
                    cmd.Parameters.Add(paramStart);
                    cmd.Parameters.Add(paramSize);
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(dt);

                    }
                    //dt = this.GetData(cmd);
                }
            }
            return dt;
        }


        public DataTable GetData(SqlCommand cmd)
        {

            DataTable dt = new DataTable();
            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            String strConnString = connection.TestTCMasterConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
            }
            catch
            {
                return dt;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                sda.Dispose();
            }
            return dt;
        }

        public List<TCStoreMerchXref> GetAllNumbers()
        {
            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            using (SqlConnection conn = new SqlConnection(connection.TestTCMasterConnectionString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "select * from [dbo].[TC_store_merch_xref]";
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var result = new List<TCStoreMerchXref>();
                    while (reader.Read())
                    {
                        var tc = new TCStoreMerchXref
                        {
                            Merch_Nbr = reader["Merch_Nbr"].ToString(),
                            RSS_Location_Nbr = reader["RSS_Location_Nbr"].ToString()
                        };
                        result.Add(tc);
                    }
                    return result;
                }
            }
        }


        public List<TCStoreMerchXref> GetNumbers(int jtStartIndex, int jtPageSize, string sorting)
        {
            int jtEndIndex = jtStartIndex + jtPageSize;
            ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
            using (SqlConnection conn = new SqlConnection(connection.TestTCMasterConnectionString))
            {
                //string sql = "select * from dbo.TC_store_merch_xref order by RSS_Location_Nbr";
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM ");
                sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY " + sorting + ") AS Row, * FROM dbo.TC_store_merch_xref) ");
                //sb.Append("(SELECT ROW_NUMBER() OVER (ORDER BY RSS_Location_Nbr ASC) AS Row, * FROM dbo.TC_store_merch_xref)  ");
                sb.Append("AS StudentsWithRowNumbers ");
                sb.Append("WHERE Row > @StartIndex AND Row <= @EndIndex");
                //sb.Append("WHERE Row > 0 AND Row <= 10 ");
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sb.ToString();
                //SqlCommand command = new SqlCommand(sb.ToString(), conn);
                

                //command.Parameters.Add(new SqlParameter("@Sort", sorting));
                command.Parameters.Add(new SqlParameter("@StartIndex", jtStartIndex));
                command.Parameters.Add(new SqlParameter("@EndIndex", jtEndIndex));

                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var result = new List<TCStoreMerchXref>();
                    while (reader.Read())
                    {
                        var tc = new TCStoreMerchXref
                        {
                            Merch_Nbr = reader["Merch_Nbr"].ToString(),
                            RSS_Location_Nbr = reader["RSS_Location_Nbr"].ToString()
                        };
                        result.Add(tc);
                    }
                    return result;
                }
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //List<TCStoreMerchXref> records = new List<TCStoreMerchXref>();
        //TCStoreMerchXref tc = new TCStoreMerchXref();
        //records = tc.GetNumbers(0, 10, "RSS_Location_Nbr ASC");
        //GridView1.DataSource = records;
        //GridView1.DataBind();

        //Label1.Text = tc.getsql(0, 10, "RSS_Location_Nbr ASC");

        ////string sql = tc.getsql(0, 10, "RSS_Location_Nbr ASC");
        //DataTable data = new DataTable();
        

        //GridView2.DataSource = tc.dtGetNumbers(0, 10, "RSS_Location_Nbr");
        //GridView2.DataSource = tc.GetData(tc.getsqlcommand(0, 10, "RSS_Location_Nbr ASC"));

        //this works
        //string query = tc.getsql(0, 10, "RSS_Location_Nbr ASC");
        //SqlCommand cmd = new SqlCommand(query);
        //data = tc.GetData(cmd);
        //GridView2.DataSource = data;


        //GridView2.DataSource = tc.GetAllNumbers().Skip(0).Take(10);


        //GridView2.DataBind();
    }
}