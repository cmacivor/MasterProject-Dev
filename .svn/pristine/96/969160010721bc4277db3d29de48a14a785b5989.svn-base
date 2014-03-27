using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using SoftArtisans.OfficeWriter.ExcelWriter;

public partial class ssc9000004 : System.Web.UI.Page
{
  Int32 lowestDrillableLevel;
  String[] selectionFieldItemField = new String[3];
  String[] selectionFieldDescField = new String[3];
  List<HiddenField> hdnMHMStoreSelectionSelectionFields;
  List<HiddenField> hdnMHMStoreSelectionSelectionValues;
  String selectStatmentFromClause = "";

  protected void Page_Load(object sender, EventArgs e)
  {
    lblStatus.Text = "";
    lblPanel1Status.Text = "";
    lblPanel2Status.Text = "";
    MHMStoreSelectionGridView1.Visible = radStoreSelectionShow.Checked;
    lblDrillOrder.Visible = radStoreSelectionShow.Checked;
    ddlMHMStoreSelectionStoreSelectionOrder.Visible = radStoreSelectionShow.Checked;

    GridView1.Visible = radProductsShow.Checked;

    if (!IsPostBack)
    {
      //if (Server.MachineName == "MHM01XPD")
      if (Server.MachineName == "CEM01WIN7D")
      {
        HttpCookie cookie = new HttpCookie("OpenSS");
        //cookie["UserID"] = "mhm02";
        cookie["UserID"] = "cem01";
        Response.Cookies.Add(cookie);
        hdnMHMStoreSelectionPageIdentifier.Value = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("0#") + DateTime.Now.Day.ToString("0#") + DateTime.Now.Hour.ToString("0#") + DateTime.Now.Minute.ToString("0#") + DateTime.Now.Second.ToString("0#") + Request.Cookies["OpenSS"]["UserID"];
        //hdnMHMStoreSelectionPageIdentifier.Value = "20090319151256mhm02";
        hdnMHMStoreSelectionPageIdentifier.Value = "20090319151256cem01";
      }
      else
      {
        hdnMHMStoreSelectionPageIdentifier.Value = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("0#") + DateTime.Now.Day.ToString("0#") + DateTime.Now.Hour.ToString("0#") + DateTime.Now.Minute.ToString("0#") + DateTime.Now.Second.ToString("0#") + Request.Cookies["OpenSS"]["UserID"];
      }

      //SqlConnection cn = new SqlConnection(@"Data Source=Poseidon2;User ID=dwbatch;Password=DWADMIN;Initial Catalog=dw_work;");
      //SqlConnection cn = new SqlConnection(@"Data Source=DNB\DNB_2K01;User ID=prbatch;Password=prjobs1;Initial Catalog=ATSWeb;");
      SqlConnection cn = new SqlConnection(@"Data Source=DNBII\Prod1;User ID=prbatch;Password=prjobs1;Initial Catalog=ATSWeb;");
      cn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM Poseidon2.dw_work.dbo.ssc9000004_criteria where DateDiff(hour, DateEntered, getdate()) > 24", cn);
      cmd.ExecuteNonQuery();
      cn.Close();
      
      //object sqlActivity;
      //sqlActivity = Server.CreateObject("SQLUser.Activity");
      //String Authenticate = sqlActivity.Authenticate(Request.Cookies["OpenSS"]["UserID"], Request.Cookies["OpenSS"]["UserID"], "ssc9000004", Request.ServerVariables["Remote_Host"], Request.Cookies["OpenSS"]["SessionKey"]);
    }

    //Required for store selection grid - begin
    hdnMHMStoreSelectionSelectionFields = new List<HiddenField>();
    hdnMHMStoreSelectionSelectionValues = new List<HiddenField>();


    //foreach (Control hf in UpdatePanel3.Controls)
    //{
    //  String pookie = hf.GetType().ToString();

    //  if (hf.GetType().ToString().EndsWith("HiddenField"))
    //  {
    //    if (hf.ID.StartsWith("hdnMHMStoreSelectionSelectionField") && hf.ID.EndsWith("Name"))
    //    {
    //      hdnMHMStoreSelectionSelectionFields.Add((HiddenField)hf);
    //    }
    //    else if (hf.ID.StartsWith("hdnMHMStoreSelectionSelectionField") && hf.ID.EndsWith("Value"))
    //    {
    //      hdnMHMStoreSelectionSelectionValues.Add((HiddenField)hf);
    //    }
    //  }
    //}

    hdnMHMStoreSelectionSelectionFields.Add(hdnMHMStoreSelectionSelectionField1Name);
    hdnMHMStoreSelectionSelectionFields.Add(hdnMHMStoreSelectionSelectionField2Name);
    hdnMHMStoreSelectionSelectionFields.Add(hdnMHMStoreSelectionSelectionField3Name);

    hdnMHMStoreSelectionSelectionValues.Add(hdnMHMStoreSelectionSelectionField1Value);
    hdnMHMStoreSelectionSelectionValues.Add(hdnMHMStoreSelectionSelectionField2Value);
    hdnMHMStoreSelectionSelectionValues.Add(hdnMHMStoreSelectionSelectionField3Value);

    if (!IsPostBack)
    {
      hdnMHMStoreSelectionSelectionField1Name.Value = "Entity";
      hdnMHMStoreSelectionSelectionField2Name.Value = "District";
      hdnMHMStoreSelectionSelectionField3Name.Value = "Store #";

      int iForWhile = 1;
      ListItem li;
      HiddenField h;
      while (FindControl("hdnMHMStoreSelectionSelectionField" + iForWhile.ToString() + "Name") != null)
      {
        h = (HiddenField)FindControl("hdnMHMStoreSelectionSelectionField" + iForWhile.ToString() + "Name");
        li = new ListItem(h.Value);
        li.Selected = true;
        chklstMHMStoreSelectionShowSelected.Items.Add(li);
        iForWhile++;
      }
    }
    setupMHMStoreSelectionSelection();
    //Required for store selection grid - end

    //if (!IsPostBack)
    //{
    //  setUpDropdown(Request.Cookies["OpenSS"]["UserID"]);
    //}
    if (!IsPostBack)
    {
      txtInventoryDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
      hdnCurrentSelectionType.Value = "Commodity";
      setUpDropdown(Request.Cookies["OpenSS"]["UserID"]);
    }
  }


  protected void setupMHMStoreSelectionSelection()
  {
    String login = Request.Cookies["OpenSS"]["UserID"];
    //login = "mhm02";
    //login = "mg16243a";
    //login = "mg79687a";
    //login = "mg11695a";
    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"^((os)|(cm)|(mg)|(am)|(cb))[0-9]{5}");

    System.Text.StringBuilder storesForSelect = new System.Text.StringBuilder();
    if (re.IsMatch(login)) //Its a store login
    {
      if (!IsPostBack)
        ddlMHMStoreSelectionStoreSelectionOrder.SelectedValue = "Store";

      String loginType = login.Substring(0, 2).ToUpper();
      String loginStore = login.Substring(2, 5);

      switch (loginType)
      {
        case "CM":
        case "CB":
          ddlStores.Items.Add(new ListItem("", ""));
          ddlStores.Items.Add(new ListItem("All", "All"));
          //SqlConnection cn = new SqlConnection(@"Data Source=DNB\DNB_2K01;User ID=prbatch;Password=prjobs1;Initial Catalog=ATSWeb;");
          SqlConnection cn = new SqlConnection(@"Data Source=DNBII\Prod1;User ID=prbatch;Password=prjobs1;Initial Catalog=ATSWeb;");
          cn.Open();
          SqlCommand cmd = new SqlCommand("", cn);
          SqlDataReader rdr = null;
          cmd.CommandText = "web_RetailReports_sp";
          cmd.Parameters.Add(new SqlParameter("strLGRP", login));
          cmd.CommandType = CommandType.StoredProcedure;
          rdr = cmd.ExecuteReader();
          while (rdr.Read())
          {
            if (storesForSelect.Length == 0)
              storesForSelect.Append(", ''" + rdr["center_id"].ToString().Trim() + "''");
            else
              storesForSelect.Append("''" + rdr["center_id"].ToString().Trim() + "''");
          }
          //storesForSelect.Remove(0, 2);
          rdr.Close();
          cn.Close();
          break;

        case "OS":
        case "MG":
        case "AM":
          storesForSelect.Append("''" + loginStore + "''");

          break;
      }
      storesForSelect.Insert(0, " and store_num in (");
      storesForSelect.Append(") ");
    }

    //selectStatmentFromClause = @" FROM OpenQuery([DNB\DNB_2K01], 'SELECT * FROM ssc_gl.dbo.gl_cntr_data a left join ssc_gl.dbo.gl_district_data b on a.reg + a.dist = b.district where cntr_desc not like ''%(CL)%'' and cntr_desc not like ''%\[CL]%'' escape ''\'' and a.comp in (''RSERV'', ''RCOOP'', ''TURF'') and b.district is not null " + storesForSelect + "') ";
    selectStatmentFromClause = @" FROM OpenQuery([DNBII\Prod1], 'SELECT * FROM ssc_gl.dbo.gl_cntr_data a left join ssc_gl.dbo.gl_district_data b on a.reg + a.dist = b.district where cntr_desc not like ''%(CL)%'' and cntr_desc not like ''%\[CL]%'' escape ''\'' and a.comp in (''RSERV'', ''RCOOP'', ''TURF'') and b.district is not null " + storesForSelect + "') ";

    switch (ddlMHMStoreSelectionStoreSelectionOrder.SelectedValue)
    {
      case "Entity, District, Store":
        hdnMHMStoreSelectionSelectionField1Name.Value = "Entity";
        hdnMHMStoreSelectionSelectionField2Name.Value = "District";
        hdnMHMStoreSelectionSelectionField3Name.Value = "Store #";

        selectionFieldItemField[0] = "comp";
        selectionFieldDescField[0] = "comp";
        selectionFieldItemField[1] = "district";
        selectionFieldDescField[1] = "dist_desc";
        selectionFieldItemField[2] = "store_num";
        selectionFieldDescField[2] = "cntr_desc";

        lowestDrillableLevel = 2;
        break;
      case "District, Entity, Store":
        hdnMHMStoreSelectionSelectionField1Name.Value = "District";
        hdnMHMStoreSelectionSelectionField2Name.Value = "Entity";
        hdnMHMStoreSelectionSelectionField3Name.Value = "Store #";

        selectionFieldItemField[0] = "district";
        selectionFieldDescField[0] = "dist_desc";
        selectionFieldItemField[1] = "comp";
        selectionFieldDescField[1] = "comp";
        selectionFieldItemField[2] = "store_num";
        selectionFieldDescField[2] = "cntr_desc";

        lowestDrillableLevel = 2;
        break;
      case "Entity, Store":
        hdnMHMStoreSelectionSelectionField1Name.Value = "Entity";
        hdnMHMStoreSelectionSelectionField2Name.Value = "Store #";
        hdnMHMStoreSelectionSelectionField3Name.Value = "";

        selectionFieldItemField[0] = "comp";
        selectionFieldDescField[0] = "comp";
        selectionFieldItemField[1] = "store_num";
        selectionFieldDescField[1] = "cntr_desc";

        lowestDrillableLevel = 1;
        break;
      case "District, Store":
        hdnMHMStoreSelectionSelectionField1Name.Value = "District";
        hdnMHMStoreSelectionSelectionField2Name.Value = "Store #";
        hdnMHMStoreSelectionSelectionField3Name.Value = "";

        selectionFieldItemField[0] = "district";
        selectionFieldDescField[0] = "dist_desc";
        selectionFieldItemField[1] = "store_num";
        selectionFieldDescField[1] = "cntr_desc";

        lowestDrillableLevel = 1;
        break;
      case "Store":
        hdnMHMStoreSelectionSelectionField1Name.Value = "Store #";
        hdnMHMStoreSelectionSelectionField2Name.Value = "";
        hdnMHMStoreSelectionSelectionField3Name.Value = "";

        selectionFieldItemField[0] = "store_num";
        selectionFieldDescField[0] = "store_num";

        lowestDrillableLevel = 0;
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }

  }

  protected void cmdMHMStoreSelectionDrillDown_Click(object sender, EventArgs e)
  {
    String commandArg = ((Button)sender).CommandArgument;
    commandArg = commandArg.Substring(0, commandArg.IndexOf("|"));

    for (int i = 0; i < hdnMHMStoreSelectionSelectionValues.Count; i++)
    {
      if (hdnMHMStoreSelectionSelectionValues[i].Value == "")
      {
        hdnMHMStoreSelectionSelectionValues[i].Value = commandArg;
        break;
      }
    }
    MHMStoreSelectionGridView1.DataBind();
  }

  protected void sdsMHMStoreSelectionCommodities_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
  {
    string mainFromClause = @" FROM gl_cntr_data a left join gl_district_data b  " +
      @" 	on a.reg + a.dist = b.district " +
      @" where cntr_desc not like '%(CL)%' and cntr_desc not like '%\[CL]%' escape '\' " +
      @" and b.district is not null ";

    mainFromClause = selectStatmentFromClause;

    StringBuilder sbExtraWhere = new StringBuilder();
    for (int i = 0; i < hdnMHMStoreSelectionSelectionValues.Count; i++)
    {
      if (hdnMHMStoreSelectionSelectionValues[i].Value == "")
      {
        e.Command.CommandText = "SELECT " + selectionFieldItemField[i] + " item, " + selectionFieldDescField[i] + " description " + mainFromClause + " " + sbExtraWhere + " group by " + selectionFieldItemField[i] + ", " + selectionFieldDescField[i] + " order by " + selectionFieldItemField[i] + " ";
        break;
      }
      else
      {
        if (sbExtraWhere.Length == 0)
          sbExtraWhere.Append(" WHERE " + selectionFieldItemField[i] + " = '" + hdnMHMStoreSelectionSelectionValues[i].Value + "' ");
        else
          sbExtraWhere.Append(" and " + selectionFieldItemField[i] + " = '" + hdnMHMStoreSelectionSelectionValues[i].Value + "' ");
      }
    }



  }

  protected void MHMStoreSelectionGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
  {
    switch (e.Row.RowType)
    {
      case DataControlRowType.Header:
        for (int i = 0; i < hdnMHMStoreSelectionSelectionValues.Count; i++)
        {
          if (hdnMHMStoreSelectionSelectionValues[i].Value == "")
          {
            //e.Row.Cells[2].Text = hdnMHMStoreSelectionSelectionFields[i].Value;
            MHMStoreSelectionGridView1.Columns[2].HeaderText = hdnMHMStoreSelectionSelectionFields[i].Value;
            break;
          }

        }
        if (hdnMHMStoreSelectionSelectionValues[0].Value == "")
        {
          ((Button)e.Row.Cells[0].FindControl("cmdMHMStoreSelectionClear")).Enabled = false;
        }
        break;

      case DataControlRowType.DataRow:
        //if (hdnMHMStoreSelectionSelectionValues[hdnMHMStoreSelectionSelectionValues.Count - 2].Value != "")
        //  ((Button)e.Row.Cells[0].FindControl("cmdMHMStoreSelectionDrillDown")).Enabled = false;




        if (lowestDrillableLevel == 0 || hdnMHMStoreSelectionSelectionValues[lowestDrillableLevel - 1].Value != "")
          ((Button)e.Row.Cells[0].FindControl("cmdMHMStoreSelectionDrillDown")).Enabled = false;
        break;

      case DataControlRowType.Footer:
        if (hdnMHMStoreSelectionSelectionValues[0].Value == "")
          ((Button)e.Row.Cells[0].FindControl("cmdMHMStoreSelectionDrillUp")).Enabled = false;
        break;
    }
  }


  protected void cmdMHMStoreSelectionClear_Click(object sender, EventArgs e)
  {
    for (int i = 0; i < hdnMHMStoreSelectionSelectionValues.Count; i++)
    {
      hdnMHMStoreSelectionSelectionValues[i].Value = "";
    }
    MHMStoreSelectionGridView1.DataBind();
  }

  protected void cmdMHMStoreSelectionDrillUp_Click(object sender, EventArgs e)
  {
    for (int i = hdnMHMStoreSelectionSelectionValues.Count - 1; i >= 0; i--)
    {
      if (hdnMHMStoreSelectionSelectionValues[i].Value != "")
      {
        hdnMHMStoreSelectionSelectionValues[i].Value = "";
        break;
      }
    }
    MHMStoreSelectionGridView1.DataBind();
  }

  protected void cmdMHMSelectionAddCommodity_Click(object sender, EventArgs e)
  {
    StringBuilder sb = new StringBuilder();
    foreach (GridViewRow gvr in MHMStoreSelectionGridView1.Rows)
    {
      if (gvr.RowType == DataControlRowType.DataRow)
      {
        CheckBox cb = (CheckBox)gvr.FindControl("cmdMHMStoreSelectionAdd");
        if (cb.Checked)
        {
          String commandArg = ((Button)gvr.FindControl("cmdMHMStoreSelectionDrillDown")).CommandArgument;
          String itemKey = commandArg.Substring(0, commandArg.IndexOf("|"));
          String itemDesc = commandArg.Substring(commandArg.IndexOf("|") + 1);

          if (sb.Length != 0)
            sb.Append(",'" + itemKey + "'");
          else
            sb.Append("'" + itemKey + "'");
          cb.Checked = false;
        }
      }
    }

    if (sb.Length == 0)
    {
      lblPanel1Status.Text = "Please select at least one item.";
    }
    else
    {
      SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MarketCostSku"].ToString());
      cn.Open();
      SqlCommand cmd = new SqlCommand("", cn);
      for (int i = 0; i < hdnMHMStoreSelectionSelectionValues.Count; i++)
      {
        if (hdnMHMStoreSelectionSelectionValues[i].Value == "")
        {
          cmd.CommandText = "DELETE FROM Poseidon2.dw_work.dbo.ssc9000004_criteria WHERE GroupType = '" + hdnMHMStoreSelectionSelectionFields[i].Value + "' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' and Item in (" + sb + "); INSERT INTO Poseidon2.dw_work.dbo.ssc9000004_criteria (GroupType, Item, ItemDesc, DateEntered, OpenSSUser, PageIdentifier, ActivityID) SELECT distinct '" + hdnMHMStoreSelectionSelectionFields[i].Value + "', " + selectionFieldItemField[i] + ", " + selectionFieldDescField[i] + ", '" + DateTime.Now.ToString() + "', '" + Request.Cookies["OpenSS"]["UserID"] + "', '" + hdnMHMStoreSelectionPageIdentifier.Value + "', 'ssc9000004' " + selectStatmentFromClause + " WHERE " + selectionFieldItemField[i] + " in (" + sb + ") ";
          break;
        }
      }
      cmd.ExecuteNonQuery();
      cn.Close();
      gvMHMStoreSelectionCommodities.DataBind();
    }
  }

  protected void sdsMHMStoreSelectionSelected_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
  {
    StringBuilder sb = new StringBuilder();
    e.Command.CommandText = "";
    for (int i = 0; i < chklstMHMStoreSelectionShowSelected.Items.Count; i++)
    {
      if (chklstMHMStoreSelectionShowSelected.Items[i].Selected)
      {
        if (sb.Length == 0)
          sb.Append("'" + chklstMHMStoreSelectionShowSelected.Items[i].Text + "'");
        else
          sb.Append(", '" + chklstMHMStoreSelectionShowSelected.Items[i].Text + "'");
      }
    }

    if (sb.Length == 0)
      e.Command.CommandText = "SELECT * FROM dw_work.dbo.ssc9000004_criteria where 1 = 2";
    else
      e.Command.CommandText = "SELECT * FROM dw_work.dbo.ssc9000004_criteria " +
        " WHERE GroupType in (" + sb.ToString() + ") " +
        " and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "'  " +
        " and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "'";
  }

  protected void chklstMHMStoreSelectionShowSelected_SelectedIndexChanged(object sender, EventArgs e)
  {
    gvMHMStoreSelectionCommodities.DataBind();
  }

  protected void ddlEntities_SelectedIndexChanged(object sender, EventArgs e)
  {
    setUpDropdown(Request.Cookies["OpenSS"]["UserID"]);
    MHMStoreSelectionGridView1.DataBind();
  }

  protected void ddlMHMStoreSelectionStoreSelectionOrder_SelectedIndexChanged(object sender, EventArgs e)
  {
    //Empty all hidden field values
    foreach (HiddenField hf in hdnMHMStoreSelectionSelectionValues)
    {
      hf.Value = "";
    }
    MHMStoreSelectionGridView1.DataBind();
  }

  protected void setUpDropdown(String userName)
  {
    ddlStores.Items.Clear();

    //pnlStoreFinder.Visible = true;
    String login = null;
    //login = "mhm02";
    //login = "mg16243a";
    //login = "mg79687a";
    //login = "mg11695a";
    login = userName;

    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"^((os)|(cm)|(mg)|(am)|(cb))[0-9]{5}");

    //SqlConnection cn = new SqlConnection(@"Data Source=Poseidon2;User ID=dwbatch;Password=DWADMIN;Initial Catalog=dw_global;");
    //SqlConnection cn = new SqlConnection(@"Data Source=DNB\DNB_2K01;User ID=prbatch;Password=prjobs1;Initial Catalog=ATSWeb;");
    SqlConnection cn = new SqlConnection(@"Data Source=DNBii\Prod1;User ID=prbatch;Password=prjobs1;Initial Catalog=ATSWeb;");
    cn.Open();
    SqlCommand cmd = new SqlCommand("", cn);
    SqlDataReader rdr = null;


    if (re.IsMatch(login)) //Its a store login
    {
      String loginType = login.Substring(0, 2).ToUpper();
      String loginStore = login.Substring(2, 5);
      System.Text.StringBuilder storesForSelect = new System.Text.StringBuilder();

      switch (loginType)
      {
        case "CM":
        case "CB":
          ddlStores.Items.Add(new ListItem("", ""));
          ddlStores.Items.Add(new ListItem("All", "All"));
          cmd.CommandText = "web_RetailReports_sp";
          cmd.Parameters.Add(new SqlParameter("strLGRP", login));
          cmd.CommandType = CommandType.StoredProcedure;
          rdr = cmd.ExecuteReader();
          while (rdr.Read())
          {
            storesForSelect.Append(", '" + rdr["center_id"].ToString().Trim() + "'");
          }
          storesForSelect.Remove(0, 2);
          rdr.Close();
          break;

        case "OS":
        case "MG":
        case "AM":
          storesForSelect.Append("'" + loginStore + "'");

          break;
      }

      cmd.CommandType = CommandType.Text;
      cmd.CommandText = "SELECT store_num, cntr_desc, comp FROM ssc_gl..gl_cntr_data WHERE store_num in (" + storesForSelect + ") ";
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        ListItem li = new ListItem(rdr["store_num"].ToString().Trim() + " - " + rdr["cntr_desc"].ToString().Trim(), rdr["store_num"].ToString().Trim());
        if (li.Value == loginStore)
          li.Selected = true;
        ddlStores.Items.Add(li);
      }
      rdr.Close();

    }
    else
    {
      ddlStores.Items.Add(new ListItem("", ""));
      cmd.CommandText = @"SELECT * FROM ssc_gl..gl_cntr_data where comp = '" + ddlEntities.SelectedValue + @"' and cntr_desc not like '%(CL)%' and cntr_desc not like '%\[CL]%' escape '\' ORDER BY store_num";
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        ListItem li = new ListItem(rdr["store_num"].ToString().Trim() + " - " + rdr["cntr_desc"].ToString().Trim(), rdr["store_num"].ToString().Trim());
        ddlStores.Items.Add(li);
      }
      rdr.Close();
    }
    cn.Close();




  }


  protected void cmdDrillDown_Click(object sender, EventArgs e)
  {
    String commandArg = ((Button)sender).CommandArgument;
    commandArg = commandArg.Substring(0, commandArg.IndexOf("|"));
    switch (hdnCurrentSelectionType.Value)
    {
      case "Commodity":
        hdnCommDrill.Value = commandArg;
        hdnProdDrill.Value = "";
        hdnMarginDrill.Value = "";
        hdnCurrentSelectionType.Value = "Product";
        break;
      case "Product":
        //hdnCommDrill.Value = commandArg;
        hdnProdDrill.Value = commandArg;
        hdnMarginDrill.Value = "";
        hdnCurrentSelectionType.Value = "Margin";
        break;
      case "Margin":
        //hdnCommDrill.Value = commandArg;
        //hdnProdDrill.Value = commandArg;
        hdnMarginDrill.Value = commandArg;
        hdnCurrentSelectionType.Value = "Sku";
        break;
      default:
        throw new ArgumentOutOfRangeException("Selection Type", "Allowed types are Commodity, Product, and Margin");
        break;
    }
    GridView1.DataBind();


  }


  protected void sdsCommodities_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
  {
    switch (hdnCurrentSelectionType.Value)
    {
      case "Commodity":
        e.Command.CommandText = "SELECT comm_key item, comm_desc description FROM dbo.d_SKU_Rollup_vw group by comm_key, comm_desc order by comm_key";
        break;
      case "Product":
        e.Command.CommandText = "SELECT pgrp_key item, pgrp_desc description FROM dbo.d_SKU_Rollup_vw WHERE comm_key = '" + hdnCommDrill.Value + "' group by pgrp_key, pgrp_desc order by pgrp_key";
        break;
      case "Margin":
        e.Command.CommandText = "SELECT mgrp_key item, mgrp_desc description FROM dbo.d_SKU_Rollup_vw WHERE comm_key = '" + hdnCommDrill.Value + "' and pgrp_key = '" + hdnProdDrill.Value + "' group by mgrp_key, mgrp_desc order by mgrp_key";
        break;
      case "Sku":
        e.Command.CommandText = "SELECT sku item, sku_desc description FROM dbo.d_SKU_Rollup_vw WHERE comm_key = '" + hdnCommDrill.Value + "' and pgrp_key = '" + hdnProdDrill.Value + "' and mgrp_key = '" + hdnMarginDrill.Value + "' order by sku, sku_desc";
        break;
      default:
        break;
    }


  }


  protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
  {
    if (e.Row.RowType == DataControlRowType.Header)
    {
      e.Row.Cells[2].Text = hdnCurrentSelectionType.Value;
      if (hdnCurrentSelectionType.Value == "Commodity")
      {
        ((Button)e.Row.Cells[0].FindControl("cmdClear")).Enabled = false;
      }
      //switch (hdnCurrentSelectionType.Value)
      //{
      //  case "commodity":
      //    e.Row.Cells[2].Text = "asdf";
      //    break;
      //  case "product":
      //    e.Row.Cells[2].Text = "asdf";
      //    break;
      //  case "margin":
      //    e.Row.Cells[2].Text = "asdf";
      //    break;
      //  case "sku":
      //    e.Row.Cells[2].Text = "asdf";
      //    break;
      //  default:
      //    break;
      //}
    }
    if (e.Row.RowType == DataControlRowType.DataRow)
    {
      if (hdnCurrentSelectionType.Value == "Sku")
      {
        ((Button)e.Row.FindControl("cmdDrillDown")).Enabled = false;
      }


    }
    if (e.Row.RowType == DataControlRowType.Footer)
    {
      if (hdnCurrentSelectionType.Value == "Commodity")
      {
        ((Button)e.Row.Cells[0].FindControl("cmdDrillUp")).Enabled = false;
      }
    }
  }


  protected void cmdClear_Click(object sender, EventArgs e)
  {
    hdnCommDrill.Value = "";
    hdnProdDrill.Value = "";
    hdnMarginDrill.Value = "";
    hdnCurrentSelectionType.Value = "Commodity";
    GridView1.DataBind();

  }



  protected void cmdDrillUp_Click(object sender, EventArgs e)
  {
    //Response.Write("Before:<BR>");
    //Response.Write("hdnCurrentSelectionType = " + hdnCurrentSelectionType.Value + "<BR>");
    //Response.Write("hdnCommDrill = " + hdnCommDrill.Value + "<BR>");
    //Response.Write("hdnProdDrill = " + hdnProdDrill.Value + "<BR>");
    //Response.Write("hdnMarginDrill = " +hdnMarginDrill.Value + "<BR>");
    //Response.Write("<BR><BR>"); 

    switch (hdnCurrentSelectionType.Value)
    {
      case "Commodity":
        hdnCommDrill.Value = "";
        hdnProdDrill.Value = "";
        hdnMarginDrill.Value = "";
        hdnCurrentSelectionType.Value = "Commodity";
        break;
      case "Product":
        hdnCommDrill.Value = "";
        hdnProdDrill.Value = "";
        hdnMarginDrill.Value = "";
        hdnCurrentSelectionType.Value = "Commodity";
        break;
      case "Margin":
        //hdnCommDrill.Value = "";
        hdnProdDrill.Value = "";
        hdnMarginDrill.Value = "";
        hdnCurrentSelectionType.Value = "Product";
        break;
      case "Sku":
        //hdnCommDrill.Value = "";
        //hdnProdDrill.Value = "";
        hdnMarginDrill.Value = "";
        hdnCurrentSelectionType.Value = "Margin";
        break;
      default:
        throw new ArgumentOutOfRangeException("Selection Type", "Allowed types are Commodity, Product, and Margin");
        break;
    }
    GridView1.DataBind();
    //Response.Write("After:<BR>");
    //Response.Write("hdnCurrentSelectionType = " + hdnCurrentSelectionType.Value + "<BR>");
    //Response.Write("hdnCommDrill = " + hdnCommDrill.Value + "<BR>");
    //Response.Write("hdnProdDrill = " + hdnProdDrill.Value + "<BR>");
    //Response.Write("hdnMarginDrill = " + hdnMarginDrill.Value + "<BR>");
    //Response.Write("<BR><BR>"); 
  }
  protected void cmdAddCommodity_Click(object sender, EventArgs e)
  {
    StringBuilder sb = new StringBuilder();
    foreach (GridViewRow gvr in GridView1.Rows)
    {
      if (gvr.RowType == DataControlRowType.DataRow)
      {
        CheckBox cb = (CheckBox)gvr.FindControl("chkAdd");
        if (cb.Checked)
        {
          String commandArg = ((Button)gvr.FindControl("cmdDrillDown")).CommandArgument;
          String itemKey = commandArg.Substring(0, commandArg.IndexOf("|"));
          String itemDesc = commandArg.Substring(commandArg.IndexOf("|") + 1);
          if (sb.Length != 0)
            sb.Append(",'" + itemKey + "'");
          else
            sb.Append("'" + itemKey + "'");
          cb.Checked = false;
        }
      }
    }
    if (sb.Length == 0)
    {
      lblPanel2Status.Text = "Please select at least one item.";
    }
    else
    {



      String groupType = "";
      String dataField = "";
      String selectField1 = "";
      String selectField2 = "";
      switch (hdnCurrentSelectionType.Value)
      {
        case "Commodity":
          groupType = "Commodity";
          dataField = "comm";
          selectField1 = "comm";
          selectField2 = "comm_desc";
          break;
        case "Product":
          groupType = "Product Group";
          dataField = "pgrp";
          selectField1 = "pgrp";
          selectField2 = "pgrp_desc";
          break;
        case "Margin":
          groupType = "Margin Group";
          dataField = "mgrp";
          selectField1 = "mgrp";
          selectField2 = "mgrp_desc";
          break;
        case "Sku":
          groupType = "Sku";
          dataField = "sku";
          selectField1 = "sku";
          selectField2 = "sku_desc";
          break;
        default:
          throw new ArgumentOutOfRangeException("Selection Type", "Allowed types are Commodity, Product Group, Margin Group, and Sku");
          break;
      }



      SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MarketCostSku"].ToString());
      cn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM dw_work.dbo.ssc9000004_criteria WHERE GroupType = '" + groupType + "' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' and Item in (" + sb + "); INSERT INTO dw_work.dbo.ssc9000004_criteria (GroupType, Item, ItemDesc, DateEntered, OpenSSUser, PageIdentifier, ActivityID) SELECT distinct '" + groupType + "', " + selectField1 + ", " + selectField2 + ", getDate(), '" + Request.Cookies["OpenSS"]["UserID"] + "', '" + hdnMHMStoreSelectionPageIdentifier.Value + "', 'ssc9000004' FROM dw_global.dbo.d_Sku_Rollup WHERE " + dataField + " in (" + sb + ") ", cn);
      //Response.Write(cmd.CommandText);
      cmd.ExecuteNonQuery();
      cn.Close();
    }
    gvCommodities.DataBind();

  }
  protected void cmdRemove_Click(object sender, EventArgs e)
  {
    Response.Write(((Button)sender).CommandArgument);
    //commodityList.Remove(((Button)sender).CommandArgument);
    //gvCommodities.DataBind();
  }
  protected void sdsSelected_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
  {


    StringBuilder sb = new StringBuilder();

    e.Command.CommandText = "";
    if (chklstShowSelected.Items[0].Selected)
    {
      if (sb.Length == 0)
        sb.Append("'Commodity'");
      else
        sb.Append(", 'Commodity'");
    }
    if (chklstShowSelected.Items[1].Selected)
    {
      if (sb.Length == 0)
        sb.Append("'Product Group'");
      else
        sb.Append(", 'Product Group'");
    }
    if (chklstShowSelected.Items[2].Selected)
    {
      if (sb.Length == 0)
        sb.Append("'Margin Group'");
      else
        sb.Append(", 'Margin Group'");
    }
    if (chklstShowSelected.Items[3].Selected)
    {
      if (sb.Length == 0)
        sb.Append("'Sku'");
      else
        sb.Append(", 'Sku'");
    }

    if (sb.Length == 0)
      e.Command.CommandText = "SELECT * FROM dw_work.dbo.ssc9000004_criteria where 1 = 2";
    else
      e.Command.CommandText = "SELECT * FROM dw_work.dbo.ssc9000004_criteria " +
        " WHERE GroupType in (" + sb.ToString() + ") " +
        " and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "'  " +
        " and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "'";

  }
  protected void chklstShowSelected_SelectedIndexChanged(object sender, EventArgs e)
  {
    gvCommodities.DataBind();
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
    sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where pgrp_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Product Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
    sb.Append(" 	union all " + System.Environment.NewLine);
    sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where comm_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Commodity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
    sb.Append(" 	union all " + System.Environment.NewLine);
    sb.Append(" 	SELECT sku from dw_global.dbo.d_SKU_Rollup_vw where mgrp_key in (SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Margin Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
    sb.Append(" 	union all " + System.Environment.NewLine);
    sb.Append(" 	SELECT Item sku from dw_work.dbo.ssc9000004_criteria where GroupType = 'Sku' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' " + System.Environment.NewLine);
    sb.Append(" 	) " + System.Environment.NewLine);
    sb.Append(" 	a; " + System.Environment.NewLine);

    sb.Append(" SELECT top 64000 d.dist_mgr, a.store_nbr, c.cntr_desc, a.sku, b.sku_desc, a.qty_oh, a.auc, a.mkt_cost, " + System.Environment.NewLine);
    sb.Append(" @frt_per_pound frt_per_pound, " + System.Environment.NewLine);

    sb.Append(" case when uom = 'LB' or uom = 'BG' then @frt_per_pound * sku_weight else @frt_per_each end frt_per_sku, " + System.Environment.NewLine);
    sb.Append(" case when uom = 'LB' or uom = 'BG' then a.mkt_cost + (@frt_per_pound * sku_weight) else a.mkt_cost + @frt_per_each end mkt_dlvd,  " + System.Environment.NewLine);
    sb.Append(" case when uom = 'LB' or uom = 'BG' then (a.mkt_cost + (@frt_per_pound * sku_weight)) - a.auc else (a.mkt_cost + @frt_per_each) - a.auc end [wd_sku], " + System.Environment.NewLine);
    sb.Append(" case when uom = 'LB' or uom = 'BG' then ((a.mkt_cost + (@frt_per_pound * sku_weight)) - a.auc) * a.qty_oh else ((a.mkt_cost + @frt_per_each) - a.auc) * a.qty_oh end WD_est, " + System.Environment.NewLine);

    sb.Append(" a.uom, a.sku_weight, a.mgrp, c.comp " + System.Environment.NewLine);

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
    sb.Append(" 	SELECT store_num from dw_global.dbo.gl_cntr_data where comp in (SELECT Item FROM dw_work..ssc9000004_criteria WHERE GroupType = 'Entity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
    sb.Append(" 	union all " + System.Environment.NewLine);
    sb.Append(" 	SELECT store_num from dw_global.dbo.gl_cntr_data where reg + dist in (SELECT Item FROM dw_work..ssc9000004_criteria WHERE GroupType = 'District' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004') " + System.Environment.NewLine);
    sb.Append(" 	union all " + System.Environment.NewLine);
    sb.Append(" 	SELECT Item store_num from dw_work..ssc9000004_criteria WHERE GroupType = 'Store #'  and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' " + System.Environment.NewLine);
    sb.Append(" 	) a " + System.Environment.NewLine);
    sb.Append(" ); ");
    //sb.Append(" SELECT * FROM #lcm; ");
    

    //Response.Write(sb.ToString());
    //return;

    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MarketCostSku"].ToString());
    cn.Open();
    SqlCommand cmd = new SqlCommand(sb.ToString(), cn);
    cmd.CommandTimeout = 120;
    try
    {
      cmd.ExecuteNonQuery();
    }
    catch (SqlException ex)
    {
      lblStatus.Text = ex.Message;
      cn.Close();
      return;
    }
    catch (Exception ex)
    {
      lblStatus.Text = ex.Message;
      cn.Close();
      return;
    }
    cmd.CommandText = "SELECT a.*, b.mgrp_desc FROM #lcm a left join dw_global.dbo.d_sku_rollup b on a.sku = b.sku order by a.dist_mgr, a.store_nbr, a.mgrp, a.sku ";
    SqlDataReader rdr = cmd.ExecuteReader();


    if (rdr.HasRows == false)
    {
      lblStatus.Text = "No records returned.  Please check your selection criteria.";
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

      Decimal mkt_dlvd = frt + Convert.ToDecimal(rdr["mkt_cost"].ToString());
      Decimal wd = mkt_dlvd - auc;
      Decimal wd_est = wd * qty_oh;



      currentRowPlus1 = j + 1;
      wsData.Cells[j, 0].Value = rdr["dist_mgr"]; //DM
      wsData.Cells[j, 1].Value = rdr["store_nbr"]; //store_nbr
      wsData.Cells[j, 2].Value = rdr["cntr_desc"]; //store_name
      wsData.Cells[j, 3].Value = rdr["sku"]; //sku
      wsData.Cells[j, 4].Value = rdr["sku_desc"].ToString();
      wsData.Cells[j, 5].Value = rdr["mgrp"].ToString();
      wsData.Cells[j, 6].Value = rdr["mgrp_desc"].ToString();
      wsData.Cells[j, 7].Value = Convert.ToDecimal(rdr["qty_oh"].ToString());
      wsData.Cells[j, 8].Value = rdr["uom"]; //AUC
      wsData.Cells[j, 9].Value = auc; //AUC
      wsData.Cells[j, 10].Value = rdr["mkt_cost"]; //mkt
      wsData.Cells[j, 11].Value = frt; //frt
      wsData.Cells[j, 12].Value = mkt_dlvd; //mkt_dlvd

      MarginGroup myLocatedObject = MarginGroups.Find(delegate(MarginGroup m) { return m.Mgrp == rdr["mgrp"].ToString() && m.Entity == rdr["comp"].ToString(); });
      if (myLocatedObject == null)
      {
        MarginGroup mg = new MarginGroup(rdr["mgrp"].ToString(), rdr["mgrp_desc"].ToString(), rdr["comp"].ToString(), wd_est);
        MarginGroups.Add(mg);
      }
      else
      {
        myLocatedObject.Add_WD_est(wd_est);
      }
      
      
      
      if (qty_oh > 0 && wd < 0)
      {

        
       
        
        wsData.Cells[j, 13].Value = wd;
        wsData.Cells[j, 14].Value = wd_est;
      }

      Decimal ton_mkt_auc, ton_mkt_dlvd, ton_qoh;
      switch (rdr["uom"].ToString().ToUpper())
      {
        case "LB":
        case "BG":
          ton_mkt_auc = (auc / sku_weight) * Convert.ToDecimal(2000.00);
          ton_mkt_dlvd = (((Convert.ToDecimal(rdr["mkt_cost"].ToString()) / sku_weight) * Convert.ToDecimal(2000.00)) + (frtPerPound * Convert.ToDecimal(2000.00)));
          ton_qoh = (qty_oh * sku_weight) / Convert.ToDecimal(2000.00);

          wsData.Cells[j, 15].Value = ton_qoh;
          wsData.Cells[j, 16].Value = ton_mkt_auc;
          wsData.Cells[j, 17].Value = Convert.ToDecimal(rdr["mkt_cost"].ToString()) / sku_weight * Convert.ToDecimal(2000.00);
          wsData.Cells[j, 18].Value = frtPerTon;
          wsData.Cells[j, 19].Value = ton_mkt_dlvd;
          if (qty_oh > 0 && wd < 0)
          {
            wsData.Cells[j, 20].Value = ton_mkt_dlvd - ton_mkt_auc;
            wsData.Cells[j, 21].Value = ton_qoh * (ton_mkt_dlvd - ton_mkt_auc);
          }
          break;
        default:
          break;
      }
      j++;
    }
    rdr.Close();










    //String critString = "Transaction Type=" + myReader["TransactionType"] + "<br>" +
    //"Start Date=" + myReader["StartDate"] + "<br>" +
    //"Stop Date=" + myReader["StopDate"] + "<br>" +
    //"Entity=" + myReader["Entity"] + "<br>" +
    //"Region=" + myReader["Region"] + "<br>" +
    //"District=" + myReader["District"] + "<br>" +
    //"Store=" + myReader["stores"] + "<br>" +
    //"Commodity=" + myReader["Commodity"] + "<br>" +
    //"Product Group=" + myReader["ProductGroup"] + "<br>" +
    //"Margin Group=" + myReader["MarginGroup"] + "<br>" +
    //"Sku=" + myReader["sku"];
    //lblCriteria.Text = critString;




    //Worksheet wsCrit = wb.Worksheets[1];
    //wsCrit.Name = "Criteria";
    //wsCrit.Cells[0, 0].Value = "Start Date";
    //wsCrit.Cells[0, 0].Style.Font.Bold = true;
    //wsCrit.Cells[0, 1].Value = Convert.ToDateTime(myReader["store_nbr"].ToString()).ToShortDateString();
    //wsCrit.Cells[1, 0].Value = "Stop Date";
    //wsCrit.Cells[1, 0].Style.Font.Bold = true;
    //wsCrit.Cells[1, 1].Value = Convert.ToDateTime(myReader["StopDate"].ToString()).ToShortDateString();
    //wsCrit.Cells[2, 0].Value = "Entity";
    //wsCrit.Cells[2, 0].Style.Font.Bold = true;
    //wsCrit.Cells[2, 1].Value = myReader["Entity"].ToString();
    //wsCrit.Cells[3, 0].Value = "Region";
    //wsCrit.Cells[3, 0].Style.Font.Bold = true;
    //wsCrit.Cells[3, 1].Value = myReader["Region"].ToString();
    //wsCrit.Cells[4, 0].Value = "District";
    //wsCrit.Cells[4, 0].Style.Font.Bold = true;
    //wsCrit.Cells[4, 1].Value = myReader["District"].ToString();
    //wsCrit.Cells[5, 0].Value = "stores";
    //wsCrit.Cells[5, 0].Style.Font.Bold = true;
    //wsCrit.Cells[5, 1].Value = myReader["stores"].ToString();
    //wsCrit.Cells[6, 0].Value = "Commodity";
    //wsCrit.Cells[6, 0].Style.Font.Bold = true;
    //wsCrit.Cells[6, 1].Value = myReader["Commodity"].ToString();
    //wsCrit.Cells[7, 0].Value = "ProductGroup";
    //wsCrit.Cells[7, 0].Style.Font.Bold = true;
    //wsCrit.Cells[7, 1].Value = myReader["ProductGroup"].ToString();
    //wsCrit.Cells[8, 0].Value = "MarginGroup";
    //wsCrit.Cells[8, 0].Style.Font.Bold = true;
    //wsCrit.Cells[8, 1].Value = myReader["MarginGroup"].ToString();
    //wsCrit.Cells[9, 0].Value = "sku";
    //wsCrit.Cells[9, 0].Style.Font.Bold = true;
    //wsCrit.Cells[9, 1].Value = myReader["sku"].ToString();


    

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

    //aUsedCols = wsCrit.CreateAreaOfColumns(0, 11);
    //aUsedCols.AutoFitWidth();

    //wsData.Cells[0, 4].Value = "Report Created " + DateTime.Now;
    //wsData.Cells[0, 4].Style.Font.Bold = true;
    //wsData.Cells[0, 0].Value = "Something goes here";
    //wsData.Cells[0, 0].Style.Font.Bold = true;




    wsSummary = wb.Worksheets[0];
    wsSummary.Cells[0, 0].Value = "Retail LCM est created " + DateTime.Now.ToShortDateString() + " inventory as of " + DateTime.Parse(txtInventoryDate.Text).ToShortDateString();
    //cmd.CommandText = " SELECT a.comp, a.mgrp, b.mgrp_desc, sum(a.WD_est) total " +
    //  " from #lcm a left join dw_global.dbo.d_sku_rollup b " +
    //  " on a.sku = b.sku " +
    //  " group by a.comp, a.mgrp, b.mgrp_desc " +
    //  " order by a.comp, a.mgrp, b.mgrp_desc ";

    //rdr = cmd.ExecuteReader();
    Int32 rowCounter = 2;
    //String lastComp = "";
    //String lastMargin = "";
    //double compTotal = 0;
    //while (rdr.Read())
    //{
    //  if (Convert.ToDouble(rdr["total"]) < 0)
    //  {
    //    if (rowCounter == 2)
    //    {
    //      lastComp = rdr["comp"].ToString();
    //      lastMargin = rdr["mgrp_desc"].ToString();
    //    }
    //    if (lastComp != rdr["comp"].ToString())
    //    {
    //      wsSummary.Cells[rowCounter, 2].Value = compTotal; rowCounter++;
    //      compTotal = 0;
    //      rowCounter++;
    //      lastComp = rdr["comp"].ToString();
    //    }
    //    wsSummary.Cells[rowCounter, 0].Value = rdr["comp"];
    //    wsSummary.Cells[rowCounter, 1].Value = rdr["mgrp_desc"];
    //    wsSummary.Cells[rowCounter, 2].Value = rdr["total"];
    //    compTotal += Convert.ToDouble(rdr["total"]);
    //    rowCounter++;
    //  }
    //}
    //wsSummary.Cells[rowCounter, 1].Value = "Total";
    //wsSummary.Cells[rowCounter, 2].Value = compTotal; 

    //rdr.Close();

    //rowCounter++;
    //rowCounter++;
    //wsSummary.Cells[rowCounter, 0].Value = "Entity";
    //wsSummary.Cells[rowCounter, 0].Style.Font.Bold = true;
    //wsSummary.Cells[rowCounter, 1].Value = "Margin";
    //wsSummary.Cells[rowCounter, 1].Style.Font.Bold = true;
    //wsSummary.Cells[rowCounter, 2].Value = "";
    //wsSummary.Cells[rowCounter, 2].Style.Font.Bold = true;
    //wsSummary.Cells[rowCounter, 3].Value = "wd_est";
    //wsSummary.Cells[rowCounter, 3].Style.Font.Bold = true;
    //wsSummary.Cells[rowCounter, 4].Value = "wd_est Ttl";
    //wsSummary.Cells[rowCounter, 4].Style.Font.Bold = true;
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
    //Area SummaryTotal = wsSummary.CreateAreaOfColumns(0, 2);
    //SummaryTotal.AutoFitWidth();
    //SummaryTotal = wsSummary.CreateAreaOfColumns(4, 1);
    //SummaryTotal.SetColumnWidth(0, SummaryTotal.GetColumnWidth(0) + 8);
    //SummaryTotal = wsSummary.CreateAreaOfColumns(7, 1);
    //SummaryTotal.SetColumnWidth(0, SummaryTotal.GetColumnWidth(0) + 8);




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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Commodity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Product Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Margin Group' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Sku' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Entity' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'District' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
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
    cmd.CommandText = " SELECT Item from dw_work.dbo.ssc9000004_criteria where GroupType = 'Store #' and OpenSSUser = '" + Request.Cookies["OpenSS"]["UserID"] + "' and PageIdentifier = '" + hdnMHMStoreSelectionPageIdentifier.Value + "' and ActivityID = 'ssc9000004' ";
    rdr = cmd.ExecuteReader();
    while (rdr.Read())
    {
      wsCriteria.Cells[critCurrentRow, 9].Value = rdr["Item"].ToString();
      critCurrentRow++;
    }
    rdr.Close();




    Area critArea = wsCriteria.CreateAreaOfColumns(0, 10);
    //critArea.AutoFitWidth();

    wsCriteria.Cells[0, 0].Value = "This is the criteria that was selected.";












    cn.Close();
    xlw.Save(wb, Page.Response, "lcm.xls", false);


  }


  protected void radStoreSelectionShow_CheckedChanged(object sender, EventArgs e)
  {
    if (radStoreSelectionShow.Checked)
      radStoreSelectionShow.Text = "Show";
  }
  protected void radProductsShow_CheckedChanged(object sender, EventArgs e)
  {
    if (radProductsShow.Checked)
      radProductsShow.Text = "Show";
  }
  protected void radStoreSelectionHide_CheckedChanged(object sender, EventArgs e)
  {
    if (radStoreSelectionHide.Checked)
      radStoreSelectionShow.Text = "Show selection grid";
  }

  protected void radProductsHide_CheckedChanged(object sender, EventArgs e)
  {
    if (radProductsShow.Checked)
      radProductsShow.Text = "Show selection grid";
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
