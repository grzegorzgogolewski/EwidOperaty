using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgNosnikNieelektroniczny
    {
        public int RodznosId { get; set; }
        public string Nazwa { get; set; }
        public string NazwaPelna { get; set; }
        public string GmlVal { get; set; }
    }

    public class PzgNosnikNieelektronicznyDict : Dictionary<int, PzgNosnikNieelektroniczny>
    {

    }
}
