using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgRodzajDostepu
    {
        public int DostepId { get; set; }
        public string Nazwa { get; set; }
        public string GmlVal { get; set; }

        public PzgRodzajDostepu(int dostepId, string nazwa, string gmlVal)
        {
            DostepId = dostepId;
            Nazwa = nazwa;
            GmlVal = gmlVal;
        }
    }

    public class PzgRodzajDostepuDict : Dictionary<int, PzgRodzajDostepu>
    {

    }
}
