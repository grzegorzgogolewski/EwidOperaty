using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class SloOperatTyp
    {
        public int OperattypId { get; set; }
        public string Nazwa { get; set; }
        public string Skrot { get; set; }
        public string GmlVal { get; set; }
    }

    public class SloOperatTypDict : Dictionary<int, SloOperatTyp>
    {

    }
}
