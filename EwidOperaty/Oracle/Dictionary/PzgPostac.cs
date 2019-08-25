using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgPostac
    {
        public int PostacId { get; set; }
        public string Nazwa { get; set; }
        public string GmlVal { get; set; }

        public PzgPostac(int postacId, string nazwa, string gmlVal)
        {
            PostacId = postacId;
            Nazwa = nazwa;
            GmlVal = gmlVal;
        }
    }

    public class PzgPostacDict : Dictionary<int, PzgPostac>
    {

    }
}
