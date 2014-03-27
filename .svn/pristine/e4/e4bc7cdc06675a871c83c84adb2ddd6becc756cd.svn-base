using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ssc1200028_ssc_spacemanModel;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.EntityClient;

/// <summary>
/// Summary description for ssc_spaceman_PLANO_KEY
/// </summary>
public class ssc_spaceman_PLANO_KEY
{
    public int Key_id { get; set; }
    public string Planogram_Name { get; set; }
    public string Group1 { get; set; }
    public IEnumerable<ssc_spaceman_PLANO_KEY> Planograms { get; set; }
    public List<ssc_spaceman_PLANO_KEY> PlanogramNames { get; set; }

    public List<ssc_spaceman_PLANO_KEY> GetPlanogramsByName(ssc1200028_ssc_spacemanEntities1 contextObject, string planogramName)
    {
       List<ssc_spaceman_PLANO_KEY> planograms = new List<ssc_spaceman_PLANO_KEY>();

       planograms = (from p in contextObject.PLANO_KEY
                     where p.Planogram.Contains(planogramName) &&
                     p.Group1 != "Archive"
                     select new ssc_spaceman_PLANO_KEY
                     {
                         Key_id = p.Key_id,
                         Planogram_Name = p.Planogram
                     }).ToList();
        if (planograms.Any())
        {
            return planograms;
        }
        return planograms;
    }


    public List<ssc_spaceman_PLANO_KEY> GetPlanograms()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select 1000 as Key_id, 'Please Select Planogram' as Planogram_Name ");
        sb.Append("union all select Key_id, CAST(Key_id as varchar(40)) + ', ' + Planogram ");
        sb.Append("as Planogram_Name from dbo.PLANO_KEY pk ");
        sb.Append("where pk.Group1 not in ('Archive') ");
        sb.Append("order by Key_id");

        string sql = sb.ToString();

        using (var context = new ssc1200028_ssc_spacemanEntities1())
        {
            var Planograms = context.ExecuteStoreQuery<ssc_spaceman_PLANO_KEY>(sql).ToList();
            return Planograms;
        }
    }

    public List<ssc_spaceman_PLANO_KEY> GetPlanoKeyByKeyID(ssc1200028_ssc_spacemanEntities1 context, int keyid)
    {
        List<ssc_spaceman_PLANO_KEY> planoKey = new List<ssc_spaceman_PLANO_KEY>();

        planoKey = (from p in context.PLANO_KEY
                    where p.Key_id == keyid
                    select new ssc_spaceman_PLANO_KEY
                    {
                        Key_id = p.Key_id,
                        Planogram_Name = p.Planogram
                    }).ToList();
        if (planoKey.Any())
        {
            return planoKey;
        }
        return planoKey;
    }

	public ssc_spaceman_PLANO_KEY()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}