using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgSposobPozyskania
    {
        public int SpospozId { get; set; }
        public string Nazwa { get; set; }
        public string NazwaPelna { get; set; }
        public string GmlVal { get; set; }
    }

    public class PzgSposobPozyskaniaDict : Dictionary<int, PzgSposobPozyskania>
    {

    }
}
