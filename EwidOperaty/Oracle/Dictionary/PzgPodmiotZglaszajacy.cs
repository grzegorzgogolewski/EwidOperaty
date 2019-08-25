using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgPodmiotZglaszajacy
    {
        public int OsobaId { get; set; }
        public string Typ { get; set; }
        public string Nazwa { get; set; }
        public string Pesel { get; set; }
        public string Regon { get; set; }
        public int? RodzPet { get; set; }
        public string Stan { get; set; }
        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string NrD { get; set; }
        public string NrM { get; set; }
        public string GmlVal { get; set; }
    }

    public class PzgPodmiotZglaszajacyDict : Dictionary<int, PzgPodmiotZglaszajacy>
    {

    }
}
