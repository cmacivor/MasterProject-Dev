using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;


public partial class ssc1900021OpenStreamSecurity : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    
/// <summary>
/// Summary description for OpenStreamSecurity
/// </summary>
    public class OpenStreamSecurity
    {
        private string sUID = "";
        private string _userID = String.Empty;
        private string _pageName = String.Empty;
        private string _groupID = String.Empty;

        private string dw_globalConnectionString
        {
            get
            {
                //return System.Configuration.ConfigurationManager.ConnectionStrings["ATSweb"].ToString();
                return "Data Source=DNBII\\PROD1;Password=webadmin;User ID=ATSWEB;Initial Catalog=ATSweb";
            }
        }


        //properties that map to the tables

        public string PageName
        {
            get { return _pageName; }
            set
            {
                Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
                _pageName = Path.GetFileNameWithoutExtension(currentPage.AppRelativeVirtualPath);
            }
        }

        public string UserID { get; set; }
        public string ActivityID { get; set; }
        public string Permissions { get; set; }
        //public string UserID
        //{
        //    get { return _userID; }
        //    set
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString()))
        //        {
        //            _userID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString();
        //        }
        //        else
        //        {
        //            _userID = String.Empty;
        //        }
        //    }

        //}

        public string GroupID { get; set; }

        //public string GroupID
        //{
        //    get { return _groupID; }
        //    set
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["OpenSS"]["LGRP"].ToString()))
        //        {
        //            _groupID = HttpContext.Current.Request.Cookies["OpenSS"]["LGRP"].ToString();
        //        }
        //        //else if (HttpContext.Current.Server.MachineName == "CEM01WIN7D")
        //        //{
        //        //    HttpCookie cookie = new HttpCookie("OpenSS");
        //        //    sUID = "cem01";
        //        //    cookie["UserID"] = sUID;
        //        //    cookie["LGRP"] = "ssc1900023_admin";
        //        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //        //    _groupID = cookie.ToString();
        //        //}
        //        else
        //        {
        //            _groupID = String.Empty;
        //        }
        //    }
        //}

        public string GroupDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<OpenStreamSecurity> HasPermissions { get; set; }

        //this recursively gets all the controls in the current page
        private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
        where T : Control
        {
            foreach (Control control in controlCollection)
            {
                //if (control.GetType() == typeof(T))
                if (control is T) // This is cleaner
                    resultCollection.Add((T)control);

                if (control.HasControls())
                    GetControlList(control.Controls, resultCollection);
            }
        }

        //public void SetIndividualControlPermissions(WebControl control)
        //{
        //    //this.UserID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString();
        //    //this.GroupID = HttpContext.Current.Request.Cookies["OpenSS"]["LGRP"].ToString();

        //    //for testing
        //    this.UserID = "cem01/test01";
        //    //this.GroupID = "ssc1900021_admin";
        //    this.GroupID = "cem01/test01";

        //    Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        //    //if the activityID can be determined from the page name- need to handle if it's from the page or the parent folder
        //    this.PageName = Path.GetFileNameWithoutExtension(currentPage.AppRelativeVirtualPath);
        //    DataTable dtActivityAssignedToGroup = new DataTable();
        //    DataTable dtUserInGroup = new DataTable();
        //    string perms = String.Empty;
        //    string groupid = String.Empty;


        //    dtActivityAssignedToGroup = IsActivityAssignedToGroup(PageName, GroupID);
        //    //if yes:
        //    if (dtActivityAssignedToGroup.Rows.Count > 0)
        //    {
        //        dtUserInGroup = IsUserInGroup(UserID, GroupID);
        //        //if (isUserInGroup.Any())
        //        if (dtUserInGroup.Rows.Count > 0)
        //        {
        //            //if yes, get the permissions
        //            groupid = dtActivityAssignedToGroup.Rows[0]["group_id"].ToString();
        //            if (!String.IsNullOrEmpty(groupid))
        //            {
        //                perms = GetActivityPermissionsByGroupIDAndActivityID(groupid, PageName);
        //                if (perms == "R")
        //                {
        //                    if (control is GridView)
        //                    {
        //                        control.Enabled = true;
        //                        control.Visible = true;
        //                        //hide the command fields
        //                        HideGridViewEditColumns(control);
        //                    }
        //                    else
        //                    {
        //                        control.Enabled = false;
        //                        control.Visible = true;
        //                    }
        //                }
        //                if (String.IsNullOrEmpty(perms) || perms == null)
        //                {
        //                    control.Visible = false;
        //                }
        //                if (perms.Contains("U") || perms.Contains("D") || perms.Contains("F"))
        //                {
        //                    control.Enabled = true;
        //                    control.Visible = true;
        //                }
        //            }
        //        }
        //        //the user is not in this group
        //        else
        //        {
        //            if (control is GridView)
        //            {
        //                control.Enabled = true;
        //                HideGridViewEditColumns(control);
        //            }
        //            else
        //            {
        //                control.Enabled = false;
        //            }
        //        }
        //    }
        //    //for when the activity is not assigned to any group
        //    else
        //    {
        //        if (control is GridView)
        //        {
        //            control.Enabled = true;
        //            HideGridViewEditColumns(control);
        //        }
        //        else
        //        {
        //            control.Visible = false;
        //        }
        //    }
        //}

        private string GetPageName()
        {
            Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            string page = Path.GetFileNameWithoutExtension(currentPage.AppRelativeVirtualPath);
            return page;
        }

        

        public string GetActivityIDByPageNameOrParentFolder()
        {
            OpenStreamSecurity security = new OpenStreamSecurity();
            string notFound = "NotFound";
            //get the name of the current page
            this.PageName = GetPageName();
            //check if the page name exists as an activity id in the web_group activities table
            security = this.IsPageNameInWebGroupActivities(this.PageName);
            //if (!String.IsNullOrEmpty(security.ActivityID))
            if (security != null)
            {
                //yes, it's in the web_group_activites table- means that page holds the activityID
                return this.PageName;
            }
            else
            {
                //check if the parent folder is named with the Activity ID
                int s = 0;
                string folderName = String.Empty;
                string currentDirectory = HttpContext.Current.Server.MapPath(".");
                s = currentDirectory.IndexOf("ssc");
                folderName = currentDirectory.Substring(s);
                security = this.IsPageNameInWebGroupActivities(folderName);
                if (!String.IsNullOrEmpty(security.ActivityID))
                {
                    return folderName;
                }
                else
                {
                    return notFound;
                }
            }
        }

        private void SetIndividualControlProperties(HtmlControl c, string perms)
        {
            if (perms == "R")
            {
                c.Disabled = true;
                c.Visible = true;
               
            }
            if (String.IsNullOrEmpty(perms) || perms == null)
            {
                c.Visible = false;
            }
            if (perms.Contains("U") || perms.Contains("D") || perms.Contains("F"))
            {
                //c.Enabled = true;
                c.Disabled = false;
                c.Visible = true;
            }
        }


        private void SetIndividualControlProperties(WebControl c, string perms)
        {
            if (perms == "R")
            {
                if (c is GridView)
                {
                    c.Enabled = true;
                    c.Visible = true;
                    //hide the command fields
                    HideGridViewEditColumns(c);
                }
                if (c is Panel)
                {
                    c.Visible = false;
                }
                else
                {
                    c.Enabled = false;
                    c.Visible = true;
                }
            }
            if (String.IsNullOrEmpty(perms) || perms == null)
            {
                c.Visible = false;
            }
            if (perms.Contains("U") || perms.Contains("D") || perms.Contains("F"))
            {
                c.Enabled = true;
                c.Visible = true;
            }
        }

        private void SetControlProperties(List<WebControl> allControls, string perms)
        {
            foreach (WebControl c in allControls)
            {
                if (perms == "R")
                {
                    if (c is GridView)
                    {
                        c.Enabled = true;
                        c.Visible = true;
                        //hide the command fields
                        HideGridViewEditColumns(c);
                    }
                    else
                    {
                        c.Enabled = false;
                        c.Visible = true;
                    }
                }
                if (String.IsNullOrEmpty(perms) || perms == null)
                {
                    c.Visible = false;
                }
                if (perms.Contains("U") || perms.Contains("D") || perms.Contains("F"))
                {
                    c.Enabled = true;
                    c.Visible = true;
                }
            }

        }

        /// <summary>
        /// This can be used to manually set permissions at individual control level when there is no group. This overload is only to be used with HR apps,
        ///  because it uses the HRWeb session that is set upon login. It will look in the web_user_activity table only and then get the permissions from the group_server_control_permissions table.
        /// </summary>
        /// <param name="control"></param>
        public void SetIndividualControlPermissions(WebControl control)
        {
            this.ActivityID = GetActivityIDByPageNameOrParentFolder();
            //this.UserID = HttpContext.Current.Session["HR_User"].ToString();
            //string activityid = GetActivityIDByPageNameOrParentFolder();
            
            //for HR
            //string userid = HttpContext.Current.Session["HR_User"].ToString();
            
            //this.UserID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString();
            //    //for testing
            //this.UserID = "cem01/test01";
            this.UserID = "cem01";

            OpenStreamSecurity security = new OpenStreamSecurity();
            //the activity id is valid
            if (ActivityID != "NotFound")
            {
                List<OpenStreamSecurity> individualPerms = new List<OpenStreamSecurity>();
                individualPerms = IsUserInGroupWithActivitiesAssigned(this.UserID, this.ActivityID);
                //for when the activity is assigned directly to the user
                if (!individualPerms.Any())
                {
                    //query the web_server_control_permissions table by login_id and activity_id to get the permissions value
                    security = GetActivityPermissionsByLoginIDAndActivityID(this.UserID, this.ActivityID);
                    if (security != null)
                    {
                        SetIndividualControlProperties(control, security.Permissions);
                    }
                }
                //for when the activity is part of a group
                else
                {
                    OpenStreamSecurity individualSecurityByGroup = new OpenStreamSecurity();
                    string groupid = individualPerms.FirstOrDefault().GroupID;
                    individualSecurityByGroup = GetActivityPermissionsByGroupIDAndActivityID(groupid, ActivityID);
                    if (individualSecurityByGroup != null)
                    {
                        SetIndividualControlProperties(control, individualSecurityByGroup.Permissions);
                    }
                }
            }
        }


        public void SetIndividualHTMLControlPermissions(HtmlControl control)
        {
            this.ActivityID = GetActivityIDByPageNameOrParentFolder();
            //this.UserID = HttpContext.Current.Session["HR_User"].ToString();
            //string activityid = GetActivityIDByPageNameOrParentFolder();

            //for HR
            //string userid = HttpContext.Current.Session["HR_User"].ToString();

            //this.UserID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString();
            //    //for testing
            this.UserID = "cem01/test01";
            //this.UserID = "cem01";

            OpenStreamSecurity security = new OpenStreamSecurity();
            //the activity id is valid
            if (ActivityID != "NotFound")
            {
                List<OpenStreamSecurity> individualPerms = new List<OpenStreamSecurity>();
                individualPerms = IsUserInGroupWithActivitiesAssigned(this.UserID, this.ActivityID);
                //for when the activity is assigned directly to the user
                if (!individualPerms.Any())
                {
                    //query the web_server_control_permissions table by login_id and activity_id to get the permissions value
                    security = GetActivityPermissionsByLoginIDAndActivityID(this.UserID, this.ActivityID);
                    if (security != null)
                    {
                        SetIndividualControlProperties(control, security.Permissions);
                    }
                }
                //for when the activity is part of a group
                else
                {
                    OpenStreamSecurity individualSecurityByGroup = new OpenStreamSecurity();
                    string groupid = individualPerms.FirstOrDefault().GroupID;
                    individualSecurityByGroup = GetActivityPermissionsByGroupIDAndActivityID(groupid, ActivityID);
                    if (individualSecurityByGroup != null)
                    {
                        SetIndividualControlProperties(control, individualSecurityByGroup.Permissions);
                    }
                }
            }
        }


        //this is for testing
        private void SetControlPermissionsTest()
        {
            //get all dropdowns
            List<WebControl> allControls = new List<WebControl>();
            Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            GetControlList<WebControl>(currentPage.Controls, allControls);

            //if the activityID can be determined from the page name- need to handle if it's from the page or the parent folder
            this.ActivityID = GetActivityIDByPageNameOrParentFolder();
            //this.ActivityID = "ssc4900012";

            //this.UserID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString();
            //this.GroupID = HttpContext.Current.Request.Cookies["OpenSS"]["LGRP"].ToString();
            OpenStreamSecurity security = new OpenStreamSecurity();
            

            //for testing
            this.UserID = "cem01/test01";
            //this.GroupID = "ssc1900021_admin";
            this.GroupID = "cem01/test01";

            //if yes, get the permissions level for that group and set properties accordingly
            //this doesn't account for non-admin groups. Need to first get the UserID and find associated 
            //activities and their groups.
            //this entire branch was successfully tested on 2/11/2014, still n
            this.HasPermissions = IsUserInGroupWithActivitiesAssigned(this.UserID, this.ActivityID);
            if (!HasPermissions.Any())
            {
                //here, use the List<OpenStreamSecurity> that was just created to query the web_server_control_permissions
                //table by the group_id, activityid, and login_id to get the permissions
                //here, need to handle whether the activity is assigned directly to a user or not
                //this throws a nullreference exception when no rows are returned
                security = GetActivityPermissionsByLoginIDAndActivityID(this.UserID, this.ActivityID);
                if (security != null)
                {
                    SetControlProperties(allControls, security.Permissions);
                }
                //if this block executes, it's because the activity is is not assigned to the user. 
                //this should not ever execute because the OpenStream menu groups should handle this, but 
                //it should be included just in case
                else
                {
                    foreach (WebControl c in allControls)
                    {
                        c.Visible = false;
                    }
                }
                
            }
            //this branch should execute when the activity is assigned to a group
            else
            {
                OpenStreamSecurity securityByGroup = new OpenStreamSecurity();
                //the group_id value needs to come from the 
                string groupid = HasPermissions.FirstOrDefault().GroupID;
                securityByGroup = GetActivityPermissionsByGroupIDAndActivityID(groupid, this.ActivityID);
                if (securityByGroup != null)
                {
                    SetControlProperties(allControls, securityByGroup.Permissions);
                }
            }
           
        }


        //private void SetControlPermissions()
        //{
        //    //get all dropdowns
        //    List<WebControl> allControls = new List<WebControl>();
        //    Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        //    GetControlList<WebControl>(currentPage.Controls, allControls);

        //    //get the page name- should be the activity ID
        //    //string activityID = Path.GetFileNameWithoutExtension(currentPage.AppRelativeVirtualPath);
        //    string perms = String.Empty;

        //    //if the activityID can be determined from the page name- need to handle if it's from the page or the parent folder
        //    this.PageName = Path.GetFileNameWithoutExtension(currentPage.AppRelativeVirtualPath);

        //    //this.UserID = HttpContext.Current.Request.Cookies["OpenSS"]["UserID"].ToString();
        //    //this.GroupID = HttpContext.Current.Request.Cookies["OpenSS"]["LGRP"].ToString();

        //    DataTable dtActivityAssignedToGroup = new DataTable();
        //    DataTable dtUserInGroup = new DataTable();
        //    string groupid = String.Empty;

        //    //for testing
        //    this.UserID = "cem01/test01";
        //    //this.GroupID = "ssc1900021_admin";
        //    this.GroupID = "cem01/test01";

        //    //if yes, get the permissions level for that group and set properties accordingly
        //    //this doesn't account for non-admin groups. Need to first get the UserID and find associated 
        //    //activities and their groups.
        //    this.HasPermissions = IsUserInGroupWithActivitiesAssigned(this.UserID, this.PageName);
        //    if (HasPermissions.Any())
        //    {
        //        //here, use the List<OpenStreamSecurity> that was just created to query the web_server_control_permissions
        //        //table by the 
        //        dtActivityAssignedToGroup = IsActivityAssignedToGroup(PageName, GroupID);
        //        if (dtActivityAssignedToGroup.Rows.Count > 0)
        //        {
        //            dtUserInGroup = IsUserInGroup(UserID, GroupID);
        //            //if (isUserInGroup.Any())
        //            if (dtUserInGroup.Rows.Count > 0)
        //            {
        //                groupid = dtActivityAssignedToGroup.Rows[0]["group_id"].ToString();

        //                if (!String.IsNullOrEmpty(groupid))
        //                {
        //                    perms = GetActivityPermissionsByGroupIDAndActivityID(groupid, PageName);
        //                    if (!String.IsNullOrEmpty(perms))
        //                    {
        //                        foreach (WebControl c in allControls)
        //                        {
        //                            if (perms == "R")
        //                            {
        //                                if (c is GridView)
        //                                {
        //                                    c.Enabled = true;
        //                                    c.Visible = true;
        //                                    //hide the command fields
        //                                    HideGridViewEditColumns(c);
        //                                }
        //                                else
        //                                {
        //                                    c.Enabled = false;
        //                                    c.Visible = true;
        //                                }
        //                            }
        //                            if (String.IsNullOrEmpty(perms) || perms == null)
        //                            {
        //                                c.Visible = false;
        //                            }
        //                            if (perms.Contains("U") || perms.Contains("D") || perms.Contains("F"))
        //                            {
        //                                c.Enabled = true;
        //                                c.Visible = true;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            //the user is not in this group
        //            else
        //            {
        //                foreach (WebControl c in allControls)
        //                {
        //                    //c.Visible = false;
        //                    if (c is GridView)
        //                    {
        //                        c.Enabled = true;
        //                        HideGridViewEditColumns(c);
        //                    }
        //                    else
        //                    {
        //                        c.Enabled = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //for when the activity is not assigned to any group
        //    else
        //    {
        //        foreach (WebControl c in allControls)
        //        {
        //            //c.Visible = false;
        //            if (c is GridView)
        //            {
        //                c.Enabled = true;
        //                HideGridViewEditColumns(c);
        //            }
        //            else
        //            {
        //                c.Enabled = false;
        //            }
        //        }
        //    }
        //}

        private void HideGridViewEditColumns(WebControl wc)
        {
            GridView grid = new GridView();
            grid = (GridView)wc;
            IEnumerable<CommandField> editColumns = grid.Columns.OfType<CommandField>().AsEnumerable();
            foreach (var e in editColumns)
            {
                //e.ShowDeleteButton = false;
                //e.ShowSelectButton = false;
                e.Visible = false;
            }
            IEnumerable<TemplateField> editColumns2 = grid.Columns.OfType<TemplateField>().AsEnumerable();
            foreach (var c in editColumns2)
            {
                //e.ShowDeleteButton = false;
                //e.ShowSelectButton = false;
                c.Visible = false;
            }
        }



        //public string GetActivityPermissionsByGroupIDAndActivityID(string groupid, string activityid)
        //{
        //    DataTable dtActivity = new DataTable();
        //    string permissions = String.Empty;
        //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        //    {
        //        string sql = "select * from web_server_control_permissions wscp where wscp.activity_id = @activityid and wscp.group_id = @groupid";
        //        using (SqlCommand cmd = new SqlCommand(sql, conn))
        //        {
        //            SqlParameter activityPRM = new SqlParameter("@activityid", activityid);
        //            SqlParameter groupPRM = new SqlParameter("@groupid", groupid);
        //            cmd.Parameters.Add(activityPRM);
        //            cmd.Parameters.Add(groupPRM);
        //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        //            {
        //                ad.Fill(dtActivity);
        //            }
        //        }
        //    }
        //    permissions = dtActivity.Rows[0]["permissions"].ToString();
        //    return permissions;
        //}

        /// <summary>
        /// Only for use when an activity is assigned to a group
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="activityid"></param>
        /// <returns></returns>
        public OpenStreamSecurity GetActivityPermissionsByGroupIDAndActivityID(string groupid, string activityid)
        {
            OpenStreamSecurity security = default(OpenStreamSecurity);
            using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
            {
                string sql = "select * from web_server_control_permissions wscp where wscp.activity_id = @activityid and wscp.group_id = @groupid";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@activityid", activityid));
                command.Parameters.Add(new SqlParameter("@groupid", groupid));
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        security = new OpenStreamSecurity
                        {
                            ActivityID = reader["activity_id"].ToString(),
                            Permissions = reader["permissions"].ToString(),
                            GroupDescription = reader["group_id"].ToString()
                        };
                    }
                }
            }
            return security;
        }

        /// <summary>
        /// Only for use when an activity is assigned directly to a user
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="activityid"></param>
        /// <returns></returns>
        public OpenStreamSecurity GetActivityPermissionsByLoginIDAndActivityID(string userid, string activityid)
        {
            OpenStreamSecurity security = default(OpenStreamSecurity);
            using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
            {
                string sql = "select * from web_server_control_permissions where login_id = @userid and activity_id = @activityid";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@activityid", activityid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        security = new OpenStreamSecurity
                        {
                            ActivityID = reader["activity_id"].ToString(),
                            Permissions = reader["permissions"].ToString(),
                            UserID = reader["login_id"].ToString()
                        };
                    }
                }
            }
            return security;
        }

        public OpenStreamSecurity IsPageNameInWebGroupActivities(string pagename)
        {
            OpenStreamSecurity security = default(OpenStreamSecurity);
            using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
            {
                string sql = "select top 1 * from web_group_activities wga where wga.activity_id =  @activityid";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@activityid", pagename));
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        security = new OpenStreamSecurity
                        {
                            ActivityID = reader["activity_id"].ToString()
                        };
                    }
                }
            }
            return security;
        }

        //public DataTable IsPageNameInWebGroupActivities(string pagename)
        //{
        //    DataTable dtActivity = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        //    {
        //        string sql = "select * from web_group_activities wga where wga.activity_id =  @activityid";
        //        using (SqlCommand cmd = new SqlCommand(sql, conn))
        //        {
        //            SqlParameter activityPRM = new SqlParameter("@activityid", pagename);
        //            cmd.Parameters.Add(activityPRM);
        //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        //            {
        //                ad.Fill(dtActivity);
        //            }
        //        }
        //    }
        //    return dtActivity;
        //}


        //public DataTable IsActivityAssignedToGroup(string pagename, string group)
        //{
        //    DataTable dtActivity = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        //    {
        //        string sql = "select * from web_group_activities wga where wga.activity_id = @activityid and wga.group_id = @group";
        //        using (SqlCommand cmd = new SqlCommand(sql, conn))
        //        {
        //            SqlParameter activityPRM = new SqlParameter("@activityid", pagename);
        //            SqlParameter groupPRM = new SqlParameter("@group", group);
        //            cmd.Parameters.Add(activityPRM);
        //            cmd.Parameters.Add(groupPRM);
        //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        //            {
        //                ad.Fill(dtActivity);
        //            }
        //        }
        //    }
        //    return dtActivity;
        //}

        //public DataTable IsUserInGroup(string login, string groupid)
        //{
        //    DataTable dtActivity = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
        //    {
        //        string sql = "select * from web_user_groups wug where wug.login = @login and group_id = @group ";
        //        using (SqlCommand cmd = new SqlCommand(sql, conn))
        //        {
        //            SqlParameter activityPRM = new SqlParameter("@login", login);
        //            SqlParameter groupPRM = new SqlParameter("@group", groupid);
        //            cmd.Parameters.Add(activityPRM);
        //            cmd.Parameters.Add(groupPRM);
        //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        //            {
        //                ad.Fill(dtActivity);
        //            }
        //        }
        //    }
        //    return dtActivity;
        //}


        public List<OpenStreamSecurity> IsUserInGroupWithActivitiesAssigned(string user, string activityid)
        {
           
            using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select wug.login, wug.group_id, wg.descp as group_description, wu.first_name, wu.last_name, wga.activity_id ");
                sb.Append("from web_user_groups wug join web_groups wg on ");
                sb.Append("wug.group_id = wg.group_id join web_users wu ");
                sb.Append("on wu.login = wug.login join web_group_activities wga ");
                sb.Append("on wg.group_id = wga.group_id ");
                sb.Append("and wug.login = @userid ");
                sb.Append("and wga.activity_id = @activityid ");
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sb.ToString();
                command.Parameters.Add(new SqlParameter("@userid", user));
                command.Parameters.Add(new SqlParameter("@activityid", activityid));
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var result = new List<OpenStreamSecurity>();
                    {
                        while (reader.Read())
                        {
                            var sec = new OpenStreamSecurity
                            {
                                UserID = reader["login"].ToString(),
                                GroupID = reader["group_id"].ToString(),
                                GroupDescription = reader["group_description"].ToString(),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                ActivityID = reader["activity_id"].ToString(),
                            };
                            result.Add(sec);
                        }
                    }
                    return result;
                }
            }
        }


         public OpenStreamSecurity IsUserAndActivityInWebUserActivities(string user, string activityid)
        {
            OpenStreamSecurity security = default(OpenStreamSecurity);
            using (SqlConnection conn = new SqlConnection(dw_globalConnectionString))
            {
                string sql = "select top 1 * from web_user_activities where activity_id = @activityid and login = @user";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@activityid", activityid));
                command.Parameters.Add(new SqlParameter("@user", user));
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        security = new OpenStreamSecurity()
                        {
                            ActivityID = reader["activity_id"].ToString()
                        };
                    }

                }
            }
            return security;
        }

        /// <summary>
        /// constructors
        /// </summary>

        public OpenStreamSecurity(bool setAll)
        {
            if (setAll == true)
            {
                //SetControlPermissions();
                //SetControlPermissionsTest();
            }
        }

        public OpenStreamSecurity()
        {

        }
    }
}