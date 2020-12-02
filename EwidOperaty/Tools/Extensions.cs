using EwidOperaty.Oracle;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace EwidOperaty.Tools
{
    public static class Extensions
    {
        #region Rozszerzenie dla Reader

        public static int? GetInt32(this OracleDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal) ? (int?)null : reader.GetInt32(ordinal);
        }

        public static int GetIntId(this OracleDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            return reader.GetInt32(ordinal);
        }

        public static string GetString(this OracleDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal) ? string.Empty : reader.GetValue(ordinal).ToString();
        }

        public static DateTime? GetDate(this OracleDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);
        }

        public static string GetPzgZgloszenieAttr(this OracleDataReader reader, string columnName, string attrName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            switch (columnName)
            {
                case "obreb":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.EgbObrebEwidencyjny[reader.GetIntId("obreb")].GmlVal;

                case "pzg_podmiotZglaszajacy" when reader.IsDBNull(ordinal):
                {
                    return string.Empty;
                }

                case "pzg_podmiotZglaszajacy":
                {
                    switch (attrName)
                    {
                        case "osoba_id":
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_podmiotZglaszajacy")].OsobaId.ToString();
                        case "regon":
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_podmiotZglaszajacy")].Regon;
                        case "pesel":
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_podmiotZglaszajacy")].Pesel;
                        default:
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_podmiotZglaszajacy")].GmlVal;
                    }
                }

                case "pzg_cel":

                    string pzgCeleLista = reader.GetString("pzg_cel");

                    if (string.IsNullOrEmpty(pzgCeleLista))
                    {
                        return string.Empty;
                    }

                    if (pzgCeleLista.Contains(","))
                    {
                        string[] pzgCeleListaSplit = pzgCeleLista.Split(',');

                        string pzgCel = "";

                        foreach (string pzgCelIdLista in pzgCeleListaSplit)
                        {
                            int pzgCelId = Convert.ToInt32(pzgCelIdLista);

                            if (DbDictionary.PzgCelPracy.ContainsKey(pzgCelId))
                            {
                                pzgCel = pzgCel + DbDictionary.PzgCelPracy[pzgCelId].GmlVal + "@";
                            }
                            else
                            {
                                pzgCel = pzgCel + DbDictionary.SloCelPracyArchiwalny[pzgCelId].GmlVal + "@";
                            }
                        }

                        return pzgCel.TrimEnd('@');
                    }
                    else
                    {
                        int pzgCelId = Convert.ToInt32(pzgCeleLista);

                        return DbDictionary.PzgCelPracy.ContainsKey(pzgCelId) ? DbDictionary.PzgCelPracy[pzgCelId].GmlVal : DbDictionary.SloCelPracyArchiwalny[pzgCelId].GmlVal;
                    }

                case "pzg_rodzaj":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgRodzajPracy[reader.GetIntId("pzg_rodzaj")].GmlVal;

                case "osobaUprawniona" when reader.IsDBNull(ordinal):
                {
                    return string.Empty;
                }

                case "osobaUprawniona":
                {

                    string osobyUprawnionaLista = reader.GetString("osobaUprawniona");

                    // jeśli w bazie jest wartośc pusta zwróć wartość pustą
                    if (string.IsNullOrEmpty(osobyUprawnionaLista))
                    {
                        return string.Empty;
                    }

                    // jeśli jest więcej niż jeden uprawniony
                    if (osobyUprawnionaLista.Contains(","))
                    {
                        string[] osobyUprawnionaListaSplit = osobyUprawnionaLista.Split(',');

                        string osobaUprawniona = "";

                        foreach (string osobaUprawnionaLista in osobyUprawnionaListaSplit)
                        {
                            osobaUprawniona = osobaUprawniona + DbDictionary.SloOsobaUprawniona[Convert.ToInt32(osobaUprawnionaLista)].GmlVal + ";";
                        }

                        return osobaUprawniona.TrimEnd(';');
                    }
                    else // jesli jest tylko jeden uprawniony
                    {
                        return DbDictionary.SloOsobaUprawniona[Convert.ToInt32(osobyUprawnionaLista)].GmlVal;
                    }
                }

                default:
                    return "BRAK KOLUMNY";
            }

        }

        public static string GetPzgMaterialZasobuAttr(this OracleDataReader reader, string columnName, string attrName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            switch (columnName)
            {
                case "pzg_nazwa":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgNazwaMat[reader.GetIntId("pzg_nazwa")].GmlVal;

                case "obreb":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.EgbObrebEwidencyjny[reader.GetIntId("obreb")].GmlVal;

                case "pzg_tworca_id" when reader.IsDBNull(ordinal):
                {
                    return string.Empty;
                }

                case "pzg_tworca_id":
                {
                    switch (attrName)
                    {
                        case "osoba_id":
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_tworca_id")].OsobaId.ToString();
                        case "regon":
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_tworca_id")].Regon;
                        case "pesel":
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_tworca_id")].Pesel;
                        default:
                            return DbDictionary.PzgPodmiotZglaszajacy[reader.GetIntId("pzg_tworca_id")].GmlVal;
                    }
                }

                case "pzg_sposobPozyskania":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgSposobPozyskania[reader.GetIntId("pzg_sposobPozyskania")].GmlVal;

                case "pzg_postacMaterialu":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgPostac[reader.GetIntId("pzg_postacMaterialu")].GmlVal;

                case "pzg_rodzNosnika":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgNosnikNieelektroniczny[reader.GetIntId("pzg_rodzNosnika")].GmlVal;

                case "pzg_dostep":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgRodzajDostepu[reader.GetIntId("pzg_dostep")].GmlVal;

                case "pzg_typMaterialu":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgTypMaterialu[reader.GetIntId("pzg_typMaterialu")].GmlVal;

                case "pzg_katArchiwalna":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.PzgKatArchiw[reader.GetIntId("pzg_katArchiwalna")].GmlVal;

                case "pzg_cel":

                    string pzgCeleLista = reader.GetString("pzg_cel");

                    if (string.IsNullOrEmpty(pzgCeleLista))
                    {
                        return string.Empty;
                    }

                    if (pzgCeleLista.Contains(","))
                    {
                        string[] pzgCeleListaSplit = pzgCeleLista.Split(',');

                        string pzgCel = "";

                        foreach (string pzgCelIdLista in pzgCeleListaSplit)
                        {
                            int pzgCelId = Convert.ToInt32(pzgCelIdLista);

                            if (DbDictionary.PzgCelPracy.ContainsKey(pzgCelId))
                            {
                                pzgCel = pzgCel + DbDictionary.PzgCelPracy[pzgCelId].GmlVal + "@";
                            }
                            else
                            {
                                pzgCel = pzgCel + DbDictionary.SloCelPracyArchiwalny[pzgCelId].GmlVal + "@";
                            }
                        }

                        return pzgCel.TrimEnd(';');
                    }
                    else
                    {
                        int pzgCelId = Convert.ToInt32(pzgCeleLista);

                        return DbDictionary.PzgCelPracy.ContainsKey(pzgCelId) ? DbDictionary.PzgCelPracy[pzgCelId].GmlVal : DbDictionary.SloCelPracyArchiwalny[pzgCelId].GmlVal;
                    }

                case "dzialkaPrzed":
                    return reader.GetString("dzialkaPrzed").Replace(',', ';');

                case "dzialkaPo":
                    return reader.GetString("dzialkaPo").Replace(',', ';');

                case "oznMaterialuZasobuTyp":
                    return reader.IsDBNull(ordinal) ? string.Empty : DbDictionary.SloOperatTyp[reader.GetIntId("oznMaterialuZasobuTyp")].GmlVal;

                default:
                    return "BRAK KOLUMNY";
            }

        }

        #endregion Rozszerzenie dla Reader

        public static void AddRange<T, TS>(this Dictionary<T, TS> source, Dictionary<T, TS> collection)
        {
            foreach (KeyValuePair<T, TS> item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
            }
        }
    }
}
