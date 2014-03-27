using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// This class maps to the dw_work database on Poseidon2. It includes methods and objects that are used in the following activities:
/// ssc9000004- Estimated Writedown Query
/// </summary>
public class dw_work_SSC9000004_CRITERIA
{
    private string dw_workConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["IFTA"].ToString();
        }
    }

    private string cookie = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"];
    private DateTime _dateEntered = DateTime.Now;
    private string _activityID = "ssc9000004";
    private string _pageIdentifier = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("0#") + DateTime.Now.Day.ToString("0#") + DateTime.Now.Hour.ToString("0#") + DateTime.Now.Minute.ToString("0#") + DateTime.Now.Second.ToString("0#") + HttpContext.Current.Request.Cookies["OpenSS"]["UserID"];
    private string _userID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"];
    

    public string GroupType { get; set; }
    public string Item { get; set; }
    public string ItemDescription { get; set; }


    public string OpenSSCookie
    {
        get { return cookie; }
    }

    //read-only properties
    public string OpenSSUser
    {
        get { return _userID; }
    }

    public DateTime DateEntered
    {
        get { return _dateEntered; }
    }

    public string PageIdentifier
    {
        get { return _pageIdentifier; }
    }

    public string ActivityID
    {
        get { return _activityID; }
    }

    public DataTable GetProductParametersByUserID(string userid)
    {
        DataTable dtParams = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_workConnectionString))
        {
            string sql = "select * from ssc9000004_criteria where GroupType in ('Commodity', 'Product Group', 'Margin Group', 'Sku') and OpenSSUser = @user";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter userPRM = new SqlParameter("@user", userid);
                cmd.Parameters.Add(userPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtParams);
                }
            }
        }
        return dtParams;
    }

    public DataTable GetEntityParametersByUserID(string userid)
    {
        DataTable dtParams = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_workConnectionString))
        {
            string sql = "select * from dbo.ssc9000004_criteria where GroupType in ('Entity', 'District', 'Store #') and OpenSSUser = @user";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter prm = new SqlParameter("@user", userid);
                cmd.Parameters.Add(prm);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtParams);
                }
            }
        }
        return dtParams;
    }

    public DataTable GetParametersByUserID(string userid)
    {
        DataTable dtParams = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_workConnectionString))
        {
            string sql = "select * from dw_work.dbo.ssc9000004_criteria where OpenSSUser = @user";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter prm = new SqlParameter("@user", userid);
                cmd.Parameters.Add(prm);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtParams);
                }
            }
        }
        return dtParams;
    }

    private DataTable doesParameterSetExist(string group, string item, string itemDesc, string openSSUser)
    {
        DataTable dtParams = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_workConnectionString))
        {
            string sql = "select * from dbo.ssc9000004_criteria where GroupType = @entity and Item = @item and ItemDesc = @itemDesc and OpenSSUser = @user";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlParameter groupPRM = new SqlParameter("@entity", group);
                SqlParameter itemPRM = new SqlParameter("@item", item);
                SqlParameter itemDescPRM = new SqlParameter("@itemDesc", itemDesc);
                SqlParameter userPRM = new SqlParameter("@user", openSSUser);
                cmd.Parameters.Add(groupPRM);
                cmd.Parameters.Add(itemPRM);
                cmd.Parameters.Add(itemDescPRM);
                cmd.Parameters.Add(userPRM);
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtParams);
                }
            }
        }
        return dtParams;
    }

    public void InsertParameters(string groupType, string item, string itemDesc,  string openSSUser, string pageIdentifier, string activityID)
    {
        DataTable dtDoParamsExist = new DataTable();
        using (SqlConnection conn = new SqlConnection(dw_workConnectionString))
        {
            //need to first query whether this set of parameters already exists
            dtDoParamsExist = this.doesParameterSetExist(groupType, item, itemDesc, openSSUser);
            if (dtDoParamsExist.Rows.Count == 0)
            {
                //string sql = "INSERT INTO dbo.ssc9000004_criteria(GroupType, Item, ItemDesc, DateEntered, OpenSSUser, PageIdentifier, ActivityID) VALUES (@groupType, @item, @itemDesc, @dateEntered, @openSSUser, @pageIdentifier, @activityID)";
                string sql = "INSERT INTO dbo.ssc9000004_criteria(GroupType, Item, ItemDesc, DateEntered, OpenSSUser, PageIdentifier, ActivityID) VALUES (@groupType, @item, @itemDesc, getdate(), @openSSUser, @pageIdentifier, @activityID)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@groupType", groupType);
                    cmd.Parameters.AddWithValue("@item", item);
                    cmd.Parameters.AddWithValue("@itemDesc", itemDesc);
                    //cmd.Parameters.AddWithValue("@dateEntered", dateEntered);
                    cmd.Parameters.AddWithValue("@openSSUser", openSSUser);
                    cmd.Parameters.AddWithValue("@pageIdentifier", pageIdentifier);
                    cmd.Parameters.AddWithValue("@activityID", activityID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public void DeleteCriteriaRecord(int id)
    {
        using (SqlConnection conn = new SqlConnection(dw_workConnectionString))
        {
            string sql = "delete from dbo.ssc9000004_criteria where ParameterID = @id";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

	public dw_work_SSC9000004_CRITERIA()
	{
		//
		// TODO: Add constructor logic here
		//

	}
}