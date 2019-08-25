using System.Collections.Generic;
using System.Linq;

namespace EwidOperaty.Oracle.Dictionary
{
    public class EgbGmina
    {
        public int Mslink { get; set; }
        public string Nazwa { get; set; }
        public string NumerGus { get; set; }
        public string ListId { get; set; }
        public string GmlVal { get; set; }
    }

    public class EgbGminaDict : Dictionary<int, EgbGmina>
    {
        public int GetGminaId(string listId)
        {
            return Values.Where(o => o.ListId == listId).ToList()[0].Mslink;
        }
    }
}
