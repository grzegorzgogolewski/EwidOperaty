using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgRodzajPracy
    {
        public int RodzId { get; set; }
        public string Nazwa { get; set; }
        public string NazwaPelna { get; set; }
        public string GmlVal { get; set; }
    }

    public class PzgRodzajPracyDict : Dictionary<int, PzgRodzajPracy>
    {

    }
}
