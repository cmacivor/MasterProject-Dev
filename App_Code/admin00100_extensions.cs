using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Extensions
/// </summary>
public static class admin00100_extensions
{
  public static List<ListItem> GetSelectedItems(this ListBox lst)
  {
    return lst.Items.OfType<ListItem>().Where(i => i.Selected).ToList();
  }

  public static bool HasSelectedValue(this RadioButtonList list)
  {
      return list.SelectedItem != null;
  }

}