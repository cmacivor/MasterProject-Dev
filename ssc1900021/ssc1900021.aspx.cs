﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

public partial class ssc1900021 : System.Web.UI.Page
{

      protected void Page_Init(object sender, EventArgs e)
    {
        //ssc1900021OpenStreamSecurity.OpenStreamSecurity security = new ssc1900021OpenStreamSecurity.OpenStreamSecurity(true);
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            if (!IsPostBack)
            {
                BindGrid();
                ssc1900021OpenStreamSecurity.OpenStreamSecurity security = new ssc1900021OpenStreamSecurity.OpenStreamSecurity();
                security.SetIndividualControlPermissions(Panel1);
    
            }
        //}
        //catch (Exception)
        //{
        //    lblError.Text = "An error has occurred. Please contact Craig MacIvor.";
        //    lblError.Visible = true;
        //}
    }

    public void BindGrid()
    {
        GridView1.DataSource = GetTCStoreMerchXRef();
        GridView1.DataBind();
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string merchNbr = Convert.ToString(GridView1.DataKeys[e.NewSelectedIndex].Value);
        txtMerchNbr.Text = merchNbr;
        Hidden1.Value = merchNbr;
        lblError.Text = "";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Credit Card Transaction Query", "alert('Please provide required criteria!');", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Credit Card Transaction Query", "document.getElementById('divEditFields').style.display = 'block'; return false;", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrWhiteSpace(txtMerchNbr.Text) && !String.IsNullOrWhiteSpace(txtRSSLocationNbr.Text))
            {
                //check if RSS Location Number is more than 5 characters
                if (txtRSSLocationNbr.Text.Length == 5)
                {
                    Insert(txtRSSLocationNbr.Text, txtMerchNbr.Text);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "return confirm('Your massage to be Displayed.');", true);
                    BindGrid();
                    lblError.Text = "";
                    txtRSSLocationNbr.Text = "";
                    txtMerchNbr.Text = "";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Credit Card Transaction Query", "alert('A record with a Merch Number of " + txtMerchNbr.Text + " and RSS Location Number of " + txtRSSLocationNbr.Text + " has been successfully added.');", true);
                }
                else
                {
                    //runjQueryCode("alert('The RSS Location Number needs to be 5 characters.');", this);
                    //lblError.Text = "The RSS Location Number must be 5 characters.";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Credit Card Transaction Query", "alert('The RSS Location Number must be 5 characters.');", true);
                }
            }
            else
            {
                //runjQueryCode("alert('Both fields are required.');", this);
                lblError.Text = "Both fields are required.";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error has occurred: " + ex.Message;
        }
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string merchNbr = Convert.ToString(GridView1.DataKeys[e.RowIndex].Value);
            Delete(merchNbr);
            BindGrid();
            lblError.Visible = false;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Credit Card Transaction Query", "alert('A record with a Merch Number of " + merchNbr + " has been successfully deleted.');", true);
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error has occurred: " + ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrWhiteSpace(txtMerchNbr.Text))
            {
                if (!String.IsNullOrEmpty(Hidden1.Value) && !String.IsNullOrWhiteSpace(txtMerchNbr.Text))
                {
                    Update(Hidden1.Value, txtMerchNbr.Text);
                    BindGrid();
                    lblError.Text = "";
                    txtMerchNbr.Text = "";
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Credit Card Transaction Query", "alert('It looks like you want to add a new record. If so, click the Add New button. If you want to save changes to an existing record, please first select a record from the grid and click Save.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "A system error has occurred: " + ex.Message;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtMerchNbr.Text = "";
        txtRSSLocationNbr.Text = "";
    }

    public void Insert(string rsslocnumber, string merchnbr)
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["TC_MASTER"].ToString();
        ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
        using (SqlConnection conn = new SqlConnection(connection.TCMasterConnectionString))
        {
            string sql = "INSERT INTO dbo.TC_store_merch_xref VALUES (@rss, @merch)";
            SqlParameter rssParam = new SqlParameter("@rss", rsslocnumber);
            SqlParameter merchParam = new SqlParameter("@merch", merchnbr);
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(rssParam);
                cmd.Parameters.Add(merchParam);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }

    public  void Update(string oldMerchNbr, string newMerchNbr)
    {
        ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
        //string connectionString = ConfigurationManager.ConnectionStrings["TC_MASTER"].ToString();
        using (SqlConnection conn = new SqlConnection(connection.TCMasterConnectionString))
        {
            string sql = "update dbo.TC_store_merch_xref set Merch_Nbr = @newNumber where Merch_Nbr = @oldNumber";
            SqlParameter newParam = new SqlParameter("@newNumber", newMerchNbr);
            SqlParameter oldParam = new SqlParameter("@oldNumber", oldMerchNbr);
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(newParam);
                cmd.Parameters.Add(oldParam);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }

    private void Delete(string merchnbr)
    {
        ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
        using (SqlConnection conn = new SqlConnection(connection.TCMasterConnectionString))
        {
            string sql = "delete from dbo.TC_store_merch_xref where Merch_Nbr = @merch";
            SqlParameter param = new SqlParameter("@merch", merchnbr);
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(param);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }

    public DataTable GetTCStoreMerchXRef()
    {
        ssc1900021Utilities.ConnectionStrings connection = new ssc1900021Utilities.ConnectionStrings();
        DataTable dtTCStoreMerchXref = new DataTable();
        using (SqlConnection conn = new SqlConnection(connection.TCMasterConnectionString))
        {
            string sql = "select * from dbo.TC_store_merch_xref order by RSS_Location_Nbr";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dtTCStoreMerchXref);
                }
            }
        }
        return dtTCStoreMerchXref;
    }


    /// <summary>
    /// Sorting and Paging methods
    /// </summary>
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.DataSource = SortDataTable(GetTCStoreMerchXRef(), true);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
   

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        int pageIndex = GridView1.PageIndex;
        GridView1.DataSource = SortDataTable(GetTCStoreMerchXRef(), false);
        GridView1.DataBind();
        GridView1.PageIndex = pageIndex;
    }

}