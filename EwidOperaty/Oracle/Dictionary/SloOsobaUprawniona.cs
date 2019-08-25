using System.Collections.Generic;

namespace EwidOperaty.Oracle.Dictionary
{
    public class SloOsobaUprawniona
    {
        public int OsobaId { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string NrUpr { get; set; }
        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string NrD { get; set; }
        public string NrM { get; set; }
        public string GmlVal { get; set; }
    }

    public class SloOsobaUprawnionaDict : Dictionary<int, SloOsobaUprawniona>
    {

    }
}
