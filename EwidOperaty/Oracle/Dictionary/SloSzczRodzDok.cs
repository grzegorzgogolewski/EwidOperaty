using System.Collections.Generic;
using System.Linq;

namespace EwidOperaty.Oracle.Dictionary
{
    public class SloSzczRodzDok
    {
        public int IdRodzDok { get; set; }
        public string Opis { get; set; }
        public string Prefix { get; set; }
        public int? NazdokId { get; set; }
        public string GmlVal { get; set; }
    }

    public class SloSzczRodzDokDict : Dictionary<int, SloSzczRodzDok>
    {
        public string GetPrefix(int idRodzDok)
        {
            return Values.Where(o => o.IdRodzDok == idRodzDok).ToList()[0].Prefix.Replace("%", "");
        }
    }
}
