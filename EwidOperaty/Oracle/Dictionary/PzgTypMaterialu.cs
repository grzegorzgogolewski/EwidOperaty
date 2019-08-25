using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgTypMaterialu
    {
        public int TypMaterId { get; set; }
        public string Nazwa { get; set; }
        public string GmlVal { get; set; }
    }

    public class PzgTypMaterialuDict : Dictionary<int, PzgTypMaterialu>
    {

    }
}
