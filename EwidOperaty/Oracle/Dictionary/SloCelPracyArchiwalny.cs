using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class SloCelPracyArchiwalny
    {
        public int CelId { get; set; }
        public string Nazwa { get; set; }
        public string Skrot { get; set; }
        public string NazwaSkr { get; set; }
        public string GmlVal { get; set; }
    }

    public class SloCelPracyArchiwalnyDict : Dictionary<int, SloCelPracyArchiwalny>
    {

    }
}
