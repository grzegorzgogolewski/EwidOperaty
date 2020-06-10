using System;
using System.Collections.Generic;
using System.Linq;

namespace EwidOperaty.Oracle.Dictionary
{
    public class PzgMaterialZasobu
    {
        public int IdOp { get; set; }
        public string PzgIdMaterialu { get; set; }              //  PZG_idMaterialu
        public DateTime? PzgDataPrzyjecia { get; set; }         //  Date
        public DateTime? DataWplywu { get; set; }               //  Date
        public string PzgNazwa { get; set; }                    //  PZG_NazwaMat
        public string PzgPolozenieObszaru { get; set; }         //  PZG_Polozenie
        public string Obreb { get; set; }                       //  CharacterString
        public string PzgTworcaOsobaId { get; set; }            //  PZG_PodmiotZglaszajacy
        public string PzgTworcaNazwa { get; set; }              //  PZG_PodmiotZglaszajacy
        public string PzgTworcaRegon { get; set; }              //  PZG_PodmiotZglaszajacy
        public string PzgTworcaPesel { get; set; }              //  PZG_PodmiotZglaszajacy
        public string PzgSposobPozyskania { get; set; }         //  PZG_SposobPozyskania
        public string PzgPostacMaterialu { get; set; }          //  PZG_Postac
        public string PzgRodzNosnika { get; set; }              //  PZG_NosnikNieelektroniczny
        public string PzgDostep { get; set; }                   //  PZG_RodzajDostepu
        public string PzgPrzyczynyOgraniczen { get; set; }      //  string
        public string PzgTypMaterialu { get; set; }             //  PZG_TypMaterialu
        public string PzgKatArchiwalna { get; set; }            //  PZG_KatArchiw
        public string PzgJezyk { get; set; }                    //  CharacterString
        public string PzgOpis { get; set; }                     //  CharacterString
        public string PzgOznMaterialuZasobu { get; set; }       //  CharacterString
        public string OznMaterialuZasobuTyp { get; set; }       //  SLO_OperatTyp
        public string OznMaterialuZasobuJedn { get; set; }      //  CharacterString
        public int? OznMaterialuZasobuNr { get; set; }          //  Integer
        public int? OznMaterialuZasobuRok { get; set; }         //  Integer
        public int? OznMaterialuZasobuTom { get; set; }         //  Integer
        public string OznMaterialuZasobuSepJednNr { get; set; } //  CharacterString
        public string OznMaterialuZasobuSepnrRok { get; set; }  //  CharacterString
        public string PzgDokumentWyl { get; set; }              //  CharacterString
        public DateTime? PzgDataWyl { get; set; }               //  Date
        public DateTime? PzgDataArchLubBrak { get; set; }       //  Date
        public string PzgCel { get; set; }                      //  PZG_CelPracy
        public string CelArchiwalny { get; set; }               //  SLO_CelPracyArchiwalny
        public string DzialkaPrzed { get; set; }                // CharacterString
        public string DzialkaPo { get; set; }                   // CharacterString
        public string Opis2 { get; set; }                       // CharacterString
        public int LiczbaSkanow { get; set; }                   //  Integer
        public int LiczbaDokS { get; set; }                     //  Integer
        public int? KergId { get; set; }                        // Integer
    }

    public class PzgMaterialZasobuDict : Dictionary<int, PzgMaterialZasobu>
    {
        public int GetOperatCount(string pzgIdMaterialu)
        {
            return Values.Where(o => o.PzgIdMaterialu == pzgIdMaterialu).ToList().Count;
        }

        public int GetIdOp(string pzgIdMaterialu)
        {
            return Values.Where(o => o.PzgIdMaterialu == pzgIdMaterialu).ToList()[0].IdOp;
        }

        public string GetIdMaterialu(int idOp)
        {
            return Values.Where(o => o.IdOp == idOp).ToList()[0].PzgIdMaterialu;
        }

        public string GetOznMaterialuZasobu(int idOp)
        {
            return Values.Where(o => o.IdOp == idOp).ToList()[0].PzgOznMaterialuZasobu;
        }

        public List<string> GetObrebGusList()
        {
            return Values.Select(o => o.Obreb).Distinct().ToList();
        }

        public int GetObrebId(string numerGus)
        {
            return DbDictionary.EgbObrebEwidencyjny.Values.Where(o => o.NumerGus == numerGus).ToList()[0].Mslink;
        }

    }
}
