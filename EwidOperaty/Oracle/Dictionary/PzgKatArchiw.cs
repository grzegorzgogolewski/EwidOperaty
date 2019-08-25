using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgKatArchiw
    {
        public int KatId { get; set; }
        public string Nazwa { get; set; }
        public string GmlVal { get; set; }

        public PzgKatArchiw(int katId, string nazwa, string gmlVal)
        {
            KatId = katId;
            Nazwa = nazwa;
            GmlVal = gmlVal;
        }
    }

    public class PzgKatArchiwDict : Dictionary<int, PzgKatArchiw>
    {

    }

}