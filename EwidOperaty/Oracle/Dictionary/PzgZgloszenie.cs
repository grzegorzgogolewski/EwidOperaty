using System;
using System.Collections.Generic;
using System.Linq;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgZgloszenie
    {
        public int KergId { get; set; }                             //  Integer
        public string PzgIdZgloszenia { get; set; }                 //  CharacterString
        public DateTime? PzgDataZgloszenia { get; set; }            //  Date
        public string IdZgloszeniaJedn { get; set; }                //  CharacterString
        public int IdZgloszeniaNr { get; set; }                     //  Integer
        public int? IdZgloszeniaRok { get; set; }                   //  Integer
        public int? IdZgloszeniaEtap { get; set; }                  //  Integer
        public string IdZgloszeniaSepJednNr { get; set; }           //  CharacterString
        public string IdZgloszeniaSepNrRok { get; set; }            //  CharacterString
        public string PzgPolozenieObszaru { get; set; }             //  PZG_Polozenie
        public string Obreb { get; set; }                           //  CharacterString
        public string PzgPodmiotZglaszajacyOsobaId { get; set; }    //  PZG_PodmiotZglaszajacy
        public string PzgPodmiotZglaszajacyNazwa { get; set; }      //  PZG_PodmiotZglaszajacy
        public string PzgPodmiotZglaszajacyRegon { get; set; }      //  PZG_PodmiotZglaszajacy
        public string PzgPodmiotZglaszajacyPesel { get; set; }      //  PZG_PodmiotZglaszajacy
        public string PzgCel { get; set; }                          //  PZG_CelPracy
        public string CelArchiwalny { get; set; }                   //  SLO_CelPracyArchiwalny
        public string PzgRodzaj { get; set; }                       //  PZG_RodzajPracy
        public string OsobaUprawniona { get; set; }                 //  SLO_OsobaUprawniona
    }

    public class PzgZgloszenieDict : Dictionary<int, PzgZgloszenie>
    {
        public string GetPzgIdZgloszenia(int kergId)
        {
            return Values.Where(z => z.KergId == kergId).ToList()[0].PzgIdZgloszenia;
        }

        public DateTime? GetPzgDataZgloszenia(int kergId)
        {
            return Values.Where(z => z.KergId == kergId).ToList()[0].PzgDataZgloszenia;
        }

        public string GetObreb(int kergId)
        {
            return Values.Where(z => z.KergId == kergId).ToList()[0].Obreb;
        }

        public string GetPzgRodzaj(int kergId)
        {
            return Values.Where(z => z.KergId == kergId).ToList()[0].PzgRodzaj;
        }

        public string GetOsobaUprawniona(int kergId)
        {
            return Values.Where(z => z.KergId == kergId).ToList()[0].OsobaUprawniona;
        }
    }
}
