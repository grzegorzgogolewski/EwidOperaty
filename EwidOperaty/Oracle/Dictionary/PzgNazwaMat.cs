using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgNazwaMat
    {
        public int NazmatId { get; set; }
        public string Nazwa { get; set; }
        public string NazwaPelna { get; set; }
        public int? Typ { get; set; }
        public string GmlVal { get; set; }
    }

    public class PzgNazwaMatDict : Dictionary<int, PzgNazwaMat>
    {

    }
}
