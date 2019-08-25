using System.Collections.Generic;
using System.Linq;

namespace EwidOperaty.Oracle.Dictionary
{
    public class EgbObrebEwidencyjny
    {
        public int Mslink { get; set; }
        public string Nazwa { get; set; }
        public string NumerGus { get; set; }
        public int? GminaId { get; set; }
        public string Gmina { get; set; }
        public string ListId { get; set; }
        public string GmlVal { get; set; }
    }

    public class EgbObrebEwidencyjnyDict : Dictionary<int, EgbObrebEwidencyjny>
    {

        public int GetObrebId(string listId)
        {
            return Values.Where(o => o.ListId == listId).ToList()[0].Mslink;
        }

        public int GetObrebIdGus(string numerGus)
        {
            return Values.Where(o => o.NumerGus == numerGus).ToList()[0].Mslink;
        }

        public string GetListId(int obrebId)
        {
            return Values.Where(o => o.Mslink == obrebId).ToList()[0].ListId;
        }


    }
}
