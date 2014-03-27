using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

    lblStatus.Text = "";

    
    //foreach (String x in Request.QueryString)
    //{
    //  Response.Write(x + " = " + Request.QueryString[x].ToString() + "<BR>");
    //}

    //for (int i = 0; i < Request.Form.Count; i++)
    //{
    //  Session[Request.Form.GetKey(i)] = Request.Form[i].ToString();
    //  Response.Write(Request.Form[i].ToString() + "<BR>");
    //}


		

    if (Request.QueryString["App"] == null || Request.QueryString["App"] == "" || Request.QueryString["UID"] == null || Request.QueryString["UID"] == "" || Request.QueryString["LGRP"] == null || Request.QueryString["LGRP"] == "")
    {
      lblStatus.Text = "Error obtaining activity and user information";
      return;
    }
    else
    {
      HttpCookie cookie = new HttpCookie("OpenSS");
      cookie["UserID"] = Request.QueryString["UID"].ToString();
      cookie["LGRP"] = Request.QueryString["lgrp"].ToString();
      Response.Cookies.Add(cookie);
    }

    System.IO.TextWriter tw1 = new System.IO.StreamWriter(Server.MapPath("session_tracking.txt"), true);
    tw1.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
    foreach (String qvalue in Request.QueryString)
    {
      tw1.WriteLine(qvalue + " = " + Request.QueryString[qvalue]);
    }
    tw1.WriteLine("REMOTE_ADDR: " + Request.ServerVariables["REMOTE_ADDR"]);
    tw1.WriteLine("Session: " + Session.SessionID);
    tw1.WriteLine("");
    tw1.Close();
    
        



		OpenStreamGlobal.UpdateSession();
	
    //Response.Write("App = " + Request.QueryString["App"] + "<br>");
    switch (Request.QueryString["App"])
    {
      case "ssc4000501":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc4000501/ssc4000501_Main.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
	 case "sschr00002":
        hdnApp.Value = "http://hebe/OpenSSApps4/sschr00002/sschr00002.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindowWithAddressAndBack()");
        break;
	 case "ari1000001":
        hdnApp.Value = "http://hebe/OpenSSApps4/ari1000001.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindowWithAddressAndBack()");
        break;
      case "ssc1000012":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1000012/ssc1000012.aspx");
        break;
      case "ssc1200004":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1200004/ssc1200004.aspx");
        break;
      case "ssc1200026":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1200026/default.aspx");
        break;
      case "ssc1400009":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1400009/default.aspx");
        break;
      case "ssc3000102":
        Response.Redirect("http://hebe/OpenSSApps4/ssc3000102/ssc3000102.aspx");
        break;
      case "ssc1200009":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1200009/ssc1200009.aspx");
        break;
      case "ssc1900006":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900006/ssc1900006_Main.aspx");
        break;
      case "ssc1900007":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900007/ssc1900007_Main.aspx");
        break;
      case "ssc1900009":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900009/ssc1900009_wait.aspx");
        break;
      case "ssc1900010":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900010/ssc1900010.aspx");
        break;
      case "ssc1900012":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900012/default.aspx");
        break;
      case "ssc1900014":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900014/default.aspx");
        break;
      case "ssc1900015":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900015/default.aspx");
        break;
      case "ssc1900016":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900016/ssc1900016_default.aspx");
        break;
      case "ssc1900017":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900017/default.aspx");
        break;
      case "ssc1900018":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900018/default.aspx");
        break;
      case "ssc1900021":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc1900021/ssc1900021.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
      case "ssc3000005":
        Response.Redirect("http://hebe/OpenSSApps4/ssc3000005/default.aspx?SessionKey=" + Request.QueryString["SessionKey"].ToString());
        break;
      case "ssc4000012":
        Response.Redirect("http://hebe/OpenSSApps4/ssc4000012.aspx");
        break;
      case "ssc1600001":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1600001/ssc1600001.aspx");
        break;
      case "ssc9000004":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc9000004/ssc9000004.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
      case "ssc9000011":
        Response.Redirect("http://hebe/OpenSSApps4/ssc9000011/default.aspx");
        break;
      case "ssc9000012":
        Response.Redirect("http://hebe/OpenSSApps4/ssc9000012/default.aspx");
        break;
      case "ssc5000103":
        Response.Redirect("http://hebe/OpenSSApps4/ssc5000103/default.aspx");
        break;
      case "ssc5000104":
        Response.Redirect("http://hebe/OpenSSApps4/ssc5000104/default.aspx");
        break;
      case "ssc5000105":
        Response.Redirect("http://hebe/OpenSSApps4/ssc5000105/default.aspx");
        break;
      case "ssc1500011":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1500011/default.aspx");
        break;
      case "admin00100":
        Response.Redirect("http://hebe/OpenSSApps4/admin00100/default.aspx");
        break;
      case "ssc3000034":
        Response.Redirect("http://hebe/OpenSSApps4/ssc3000034/default.aspx");
        break;
      case "ssc3000028":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc3000028/ssc3000028_import.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
        case "ssc3000028_ledger_drill":
        Response.Redirect("http://hebe/OpenSSApps4/ssc3000028/ssc3000000_ABS021_0.aspx");
        break;
      case "ssc3000023":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc3000023/ssc3000023_import.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
      case "ssc3000035":
        Response.Redirect("http://hebe/OpenSSApps4/ssc3000035/ssc3000035.aspx");
        break;
      case "ssc3000040":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc3000040/ssc3000040_Default.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
      case "sscrpt0048":
        Response.Redirect("http://hebe/OpenSSApps4/sscrpt0048/sscrpt0048_Task_Home.aspx");
        break;
      case "ssc1100007":
        Response.Redirect("http://hebe/OpenSSApps4/ssc1100007/default.aspx");
        break;
      case "ssc5000010":
        Response.Redirect("http://hebe/OpenSSApps4/ssc5000010/ssc5000010.aspx");
        break;
      case "sscrpt0054":
        Response.Redirect("http://hebe/OpenSSApps4/sscrpt0054/default.aspx");
        break;
      case "sscrpt0056":
        Response.Redirect("http://hebe/OpenSSApps4/sscrpt0056/sscrpt0056.aspx");
        break;
      case "ssc1200024":
        System.IO.TextWriter tw = new System.IO.StreamWriter(Server.MapPath("Default_to_ssc1200024.txt"), true);
        tw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        foreach (String qvalue in Request.QueryString)
        {
          tw.WriteLine(qvalue + " = " + Request.QueryString[qvalue]);
        }
        tw.WriteLine("");
        tw.Close();
        ssc1200024.FID = null;
        ssc1200024.FNM = null;
        ssc1200024.sppage = null;
        ssc1200024.LVL = null;
        ssc1200024.CRM_incentive_module = null;
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc1200024/ssc1200024_start.aspx";

        if (Request.UserAgent != null && Request.UserAgent.IndexOf("AppleWebKit") > 0)
          Response.Redirect("http://hebe/OpenSSApps4/ssc1200024/ssc1200024_start.aspx");
        else
          body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;
      case "sscrpt0055":
        hdnApp.Value = "http://hebe/OpenSSApps4/sscrpt0055/sscrpt0055_schedule.aspx";
        if (Request.UserAgent != null && Request.UserAgent.IndexOf("AppleWebKit") > 0)
          Response.Redirect("http://hebe/OpenSSApps4/sscrpt0055/sscrpt0055.aspx");
        else
          body.Attributes.Add("onload", "OpenAppInNewWindow()");
        break;

      case "tsmcrm":
        System.IO.TextWriter twx = new System.IO.StreamWriter(Server.MapPath("Default_to_ssc1200024x.txt"), true);
        twx.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        foreach (String qvalue in Request.QueryString)
        {
          twx.WriteLine(qvalue + " = " + Request.QueryString[qvalue]);
        }
        twx.WriteLine("");
        twx.Close();
        Response.Write("Coming from incentives<br>");
        
        if (Request.QueryString["module"] != null && Request.QueryString["module"] == "ssc6000014")
        {
          ssc1200024.FID = null;
          ssc1200024.FNM = null;
          ssc1200024.sppage = null;
          ssc1200024.LVL = null;
          ssc1200024.CRM_incentive_module = null;

          HttpCookie cookie = new HttpCookie("ssc1200024_ipad_login");
          cookie.Value = Request.QueryString["UID"].ToString();
          Response.Cookies.Add(cookie);
          Response.Redirect("ssc1200024/ssc1200024_ipad.aspx");
        }
        
        ssc1200024.FID = Request.QueryString["FID"];
        ssc1200024.FNM = Request.QueryString["FNM"];
        ssc1200024.sppage = Request.QueryString["sppage"];
        if (Request.QueryString["IsMgr"] != null && Request.QueryString["IsMgr"].ToString() == "no")
        {
          ssc1200024.sppage = "1";
        }
        ssc1200024.LVL = Request.QueryString["LVL"];
        ssc1200024.CRM_incentive_module = Request.QueryString["module"];
        Response.Redirect("ssc1200024/ssc1200024_start.aspx");
        //    ssc1200024.CRM_FSA_emp_id = Request.QueryString["FID"];
        //    ssc1200024.UsersName = Request.QueryString["FNM"];
        //    Response.Redirect("ssc1200024_start.aspx");
        break;
      case "ssc1900008":
        //hdnApp.Value = "http://hebe/OpenSSApps4/ssc1900008/ssc1900008.aspx";
        //body.Attributes.Add("onload", "OpenAppInNewWindow()");
        Response.Redirect("http://hebe/OpenSSApps4/ssc1900008/ssc1900008.aspx");
        break;
      case "ssc6000022":
        hdnApp.Value = "http://hebe/OpenSSApps4/ssc6000022/ssc6000022_ED.aspx";
        body.Attributes.Add("onload", "OpenAppInNewWindow()");
        //Response.Redirect("http://hebe/OpenSSApps4/ssc6000022/ssc6000022_ED.aspx");
        break;
      default:
        lblStatus.Text = "Error loading page";
        break;
    }
  }
}