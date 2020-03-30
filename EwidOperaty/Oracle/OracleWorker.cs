using EwidOperaty.Oracle.Dictionary;
using EwidOperaty.Tools;
using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EwidOperaty.Oracle
{
    public class OracleWorker
    {
        private readonly OracleConnection _oracleConnection = new OracleConnection();   //  Połączenie z bazą

        public OracleStatus Connect(string host, string database, string user, string pass)
        {
            try
            {
                _oracleConnection.ConnectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={1521}))(CONNECT_DATA=(SERVICE_NAME={database})));User Id={user};Password={pass};";
                _oracleConnection.Open();

                return new OracleStatus(true, $"Połączono z bazą {database}");
            }
            catch (Exception exception)
            {
                return new OracleStatus(false, exception.Message);
            }
        }

        public void Disconect()
        {
            _oracleConnection.Close(); // zamknięcie połączenia z bazą danych
        }

        public EgbGminaDict GetEgbGminaDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("EgbGminaDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        EgbGminaDict gminaDict = new EgbGminaDict();

                        while (reader.Read())
                        {
                            EgbGmina gmina = new EgbGmina
                            {
                                Mslink = reader.GetIntId("mslink"),
                                Nazwa = reader.GetString("nazwa"),
                                NumerGus = reader.GetString("numer_gus"),
                                ListId = reader.GetString("nazwa") + " [" + reader.GetString("numer_gus") + "]",
                                GmlVal = reader.GetString("numer_gus")
                            };

                            gminaDict.Add(gmina.Mslink, gmina);
                        }

                        return gminaDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new EgbGminaDict();
                }
            }
        }

        public EgbObrebEwidencyjnyDict GetEgbObrebEwidencyjnyDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("EgbObrebEwidencyjnyDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        EgbObrebEwidencyjnyDict obrebEwidencyjnyDict = new EgbObrebEwidencyjnyDict();

                        while (reader.Read())
                        {
                            EgbObrebEwidencyjny obrebEwidencyjny = new EgbObrebEwidencyjny
                            {
                                Mslink = reader.GetIntId("mslink"),
                                Nazwa = reader.GetString("nazwa"),
                                NumerGus = reader.GetString("numer_gus"),
                                GminaId = reader.GetInt32("gmina_id"),
                                Gmina = reader.GetString("gmina"),
                                ListId = "[" + reader.GetString("numer_gus") + "] " + reader.GetString("nazwa"),
                                GmlVal = reader.GetString("numer_gus")
                            };

                            obrebEwidencyjnyDict.Add(obrebEwidencyjny.Mslink, obrebEwidencyjny);
                        }

                        return obrebEwidencyjnyDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new EgbObrebEwidencyjnyDict();
                }
            }
        }

        public PzgNosnikNieelektronicznyDict GetPzgNosnikNieelektronicznyDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgNosnikNieelektronicznyDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgNosnikNieelektronicznyDict nosnikNieelektronicznyDict = new PzgNosnikNieelektronicznyDict();

                        while (reader.Read())
                        {
                            PzgNosnikNieelektroniczny nosnikNieelektroniczny = new PzgNosnikNieelektroniczny
                            {
                                RodznosId = reader.GetIntId("rodznos_id"),
                                Nazwa = reader.GetString("nazwa"),
                                NazwaPelna = reader.GetString("nazwa_pelna"),
                                GmlVal = GmlDictionaryValues.PzgNosnikNieelektroniczny[reader.GetIntId("rodznos_id") - 1]
                            };

                            nosnikNieelektronicznyDict.Add(nosnikNieelektroniczny.RodznosId, nosnikNieelektroniczny);
                        }

                        return nosnikNieelektronicznyDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgNosnikNieelektronicznyDict();
                }
            }
        }

        public PzgTypMaterialuDict GetPzgTypMaterialuDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgTypMaterialuDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgTypMaterialuDict typMaterialuDict = new PzgTypMaterialuDict();

                        while (reader.Read())
                        {
                            PzgTypMaterialu pzgTypMaterialu = new PzgTypMaterialu
                            {
                                TypMaterId = reader.GetIntId("typ_mater_id"),
                                Nazwa = reader.GetString("nazwa"),
                                GmlVal = GmlDictionaryValues.PzgTypMaterialu[reader.GetIntId("typ_mater_id") - 1]
                            };

                            typMaterialuDict.Add(pzgTypMaterialu.TypMaterId, pzgTypMaterialu);
                        }

                        return typMaterialuDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgTypMaterialuDict();
                }
            }
        }

        public PzgNazwaMatDict GetPzgNazwaMatDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgNazwaMatDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgNazwaMatDict nazwaMatDict = new PzgNazwaMatDict();

                        while (reader.Read())
                        {
                            PzgNazwaMat nazwaMat = new PzgNazwaMat
                            {
                                NazmatId = reader.GetIntId("nazmat_id"),
                                Nazwa = reader.GetString("nazwa"),
                                NazwaPelna = reader.GetString("nazwa_pelna"),
                                Typ = reader.GetInt32("typ"),
                                GmlVal = GmlDictionaryValues.PzgNazwaMat[reader.GetIntId("nazmat_id") - 1]
                            };

                            nazwaMatDict.Add(nazwaMat.NazmatId, nazwaMat);
                        }

                        return nazwaMatDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgNazwaMatDict();
                }
            }
        }

        public SloSzczRodzDokDict GetSloSzczRodzDokDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("SloSzczRodzDokDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        SloSzczRodzDokDict szczRodzDokDict = new SloSzczRodzDokDict();

                        while (reader.Read())
                        {
                            SloSzczRodzDok szczRodzDok = new SloSzczRodzDok
                            {
                                IdRodzDok = reader.GetIntId("id_rodz_dok"),
                                Opis = reader.GetString("opis"),
                                Prefix = reader.GetString("prefix"),
                                NazdokId = reader.GetInt32("nazdok_id"),
                                GmlVal = reader.GetString("gml_val")
                            };

                            szczRodzDokDict.Add(szczRodzDok.IdRodzDok, szczRodzDok);
                        }

                        return szczRodzDokDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new SloSzczRodzDokDict();
                }
            }
        }

        public SloOperatTypDict GetSloOperatTypDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("SloOperatTypDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        SloOperatTypDict operatTypDict = new SloOperatTypDict();

                        while (reader.Read())
                        {
                            SloOperatTyp operatTyp = new SloOperatTyp
                            {
                                OperattypId = reader.GetIntId("operattyp_id"),
                                Nazwa = reader.GetString("nazwa"),
                                Skrot = reader.GetString("skrot"),
                                GmlVal = reader.GetString("skrot")
                            };

                            operatTypDict.Add(operatTyp.OperattypId, operatTyp);
                        }

                        return operatTypDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new SloOperatTypDict();
                }
            }
        }

        public PzgCelPracyDict GetPzgCelPracyDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgCelPracyDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgCelPracyDict celPracyDict = new PzgCelPracyDict();

                        while (reader.Read())
                        {
                            PzgCelPracy celPracy = new PzgCelPracy
                            {
                                CelId = reader.GetIntId("cel_id"),
                                Nazwa = reader.GetString("nazwa"),
                                Skrot = reader.GetString("skrot"),
                                NazwaSkr = reader.GetString("nazwa_skr"),
                                GmlVal = GmlDictionaryValues.PzgCelPracy[reader.GetIntId("cel_id") - 1]
                            };

                            celPracyDict.Add(celPracy.CelId, celPracy);
                        }

                        return celPracyDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgCelPracyDict();
                }
            }
        }

        public SloCelPracyArchiwalnyDict GetSloCelPracyArchiwalnyDictt()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("SloCelPracyArchiwalnyDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        SloCelPracyArchiwalnyDict celPracyArchiwalnyDict = new SloCelPracyArchiwalnyDict();

                        while (reader.Read())
                        {
                            SloCelPracyArchiwalny sloCelPracyArchiwalny = new SloCelPracyArchiwalny
                            {
                                CelId = reader.GetIntId("cel_id"),
                                Nazwa = reader.GetString("nazwa"),
                                Skrot = reader.GetString("skrot"),
                                NazwaSkr = reader.GetString("nazwa_skr"),
                                GmlVal = reader.GetString("nazwa")
                            };

                            celPracyArchiwalnyDict.Add(sloCelPracyArchiwalny.CelId, sloCelPracyArchiwalny);
                        }

                        return celPracyArchiwalnyDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new SloCelPracyArchiwalnyDict();
                }
            }
        }

        public PzgRodzajPracyDict GetPzgRodzajPracyDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgRodzajPracyDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgRodzajPracyDict rodzajPracyDict = new PzgRodzajPracyDict();

                        while (reader.Read())
                        {
                            PzgRodzajPracy rodzajPracy = new PzgRodzajPracy
                            {
                                RodzId = reader.GetIntId("rodz_id"),
                                Nazwa = reader.GetString("nazwa"),
                                NazwaPelna = reader.GetString("nazwa_pelna"),
                                GmlVal = GmlDictionaryValues.PzgRodzajPracy[reader.GetIntId("rodz_id") - 1]
                            };

                            rodzajPracyDict.Add(rodzajPracy.RodzId, rodzajPracy);
                        }

                        return rodzajPracyDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgRodzajPracyDict();
                }
            }
        }

        public PzgPodmiotZglaszajacyDict GetPzgPodmiotZglaszajacyDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgPodmiotZglaszajacyDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgPodmiotZglaszajacyDict podmiotZglaszajacyDict = new PzgPodmiotZglaszajacyDict();

                        while (reader.Read())
                        {
                            PzgPodmiotZglaszajacy podmiotZglaszajacy = new PzgPodmiotZglaszajacy
                            {
                                OsobaId = reader.GetIntId("osoba_id"),
                                Typ = reader.GetString("typ"),
                                Nazwa = reader.GetString("nazwa"),
                                Pesel = reader.GetString("pesel"),
                                Regon = reader.GetString("regon"),
                                RodzPet = reader.GetInt32("rodz_pet"),
                                Stan = reader.GetString("stan"),
                                Miejscowosc = reader.GetString("miejscowosc"),
                                Ulica = reader.GetString("ulica"),
                                NrD = reader.GetString("nr_d"),
                                NrM = reader.GetString("nr_m"),
                                GmlVal = reader.GetString("nazwa")
                            };

                            podmiotZglaszajacyDict.Add(podmiotZglaszajacy.OsobaId, podmiotZglaszajacy);
                        }

                        return podmiotZglaszajacyDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgPodmiotZglaszajacyDict();
                }

            }
        }

        public SloOsobaUprawnionaDict GetSloOsobaUprawnionaDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("SloOsobaUprawnionaDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        SloOsobaUprawnionaDict osobaUprawnionaDict = new SloOsobaUprawnionaDict();

                        while (reader.Read())
                        {
                            SloOsobaUprawniona osobaUprawniona = new SloOsobaUprawniona
                            {
                                OsobaId = reader.GetIntId("osoba_id"),
                                Nazwisko = reader.GetString("nazwisko"),
                                Imie = reader.GetString("imie"),
                                NrUpr = reader.GetString("nr_upr"),
                                Miejscowosc = reader.GetString("miejscowosc"),
                                Ulica = reader.GetString("ulica"),
                                NrD = reader.GetString("nr_d"),
                                NrM = reader.GetString("nr_m"),
                                GmlVal = reader.GetString("imie") + "_" + reader.GetString("nazwisko") + "_" + reader.GetString("nr_upr")
                            };

                            osobaUprawnionaDict.Add(osobaUprawniona.OsobaId, osobaUprawniona);
                        }

                        return osobaUprawnionaDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new SloOsobaUprawnionaDict();
                }
            }
        }

        public PzgSposobPozyskaniaDict GetPzgSposobPozyskaniaDict()
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                command.CommandText = SqlResource.GetSqlText("PzgSposobPozyskaniaDict.sql");

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgSposobPozyskaniaDict sposobPozyskaniaDict = new PzgSposobPozyskaniaDict();

                        while (reader.Read())
                        {
                            PzgSposobPozyskania sposobPozyskania = new PzgSposobPozyskania
                            {
                                SpospozId = reader.GetIntId("spospoz_id"),
                                Nazwa = reader.GetString("nazwa"),
                                NazwaPelna = reader.GetString("nazwa_pelna"),
                                GmlVal = GmlDictionaryValues.PzgSposobPozyskania[reader.GetIntId("spospoz_id") - 1]
                            };

                            sposobPozyskaniaDict.Add(sposobPozyskania.SpospozId, sposobPozyskania);
                        }

                        return sposobPozyskaniaDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgSposobPozyskaniaDict();
                }
            }
        }

        public PzgPostacDict GetPzgPostacDict()
        {
            PzgPostacDict postacDict = new PzgPostacDict
            {
                { 1, new PzgPostac(1, "elektroniczna", GmlDictionaryValues.PzgPostac[0]) },
                { 2, new PzgPostac(2, "nieelektroniczna", GmlDictionaryValues.PzgPostac[1]) },
                { 3, new PzgPostac(3, "mieszana", GmlDictionaryValues.PzgPostac[2]) }
            };

            return postacDict;
        }

        public PzgRodzajDostepuDict GetPzgRodzajDostepuDict()
        {
            PzgRodzajDostepuDict rodzajDostepuDict = new PzgRodzajDostepuDict
            {
                { 1, new PzgRodzajDostepu(1, "bezOgraniczen", GmlDictionaryValues.PzgRodzajDostepu[0])},
                { 2, new PzgRodzajDostepu(2, "zOgraniczeniami", GmlDictionaryValues.PzgRodzajDostepu[1])}
            };

            return rodzajDostepuDict;
        }

        public PzgKatArchiwDict GetPzgKatArchiwDict()
        {
            PzgKatArchiwDict katArchiwDict = new PzgKatArchiwDict
            {
                { 1, new PzgKatArchiw(1, "A", GmlDictionaryValues.PzgKatArchiw[0])},
                { 2, new PzgKatArchiw(2, "BC", GmlDictionaryValues.PzgKatArchiw[1])},
                { 3, new PzgKatArchiw(3, "BE", GmlDictionaryValues.PzgKatArchiw[2])}
            };

            return katArchiwDict;
        }

        public PzgZgloszenieDict GetPzgZgloszenieDict(int obrebId)
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                OracleParameter obrebIdOracleParameter = new OracleParameter
                {
                    OracleDbType = OracleDbType.Int32,
                    ParameterName = "obreb_id"
                };

                command.CommandText = SqlResource.GetSqlText(GlobalValues.ZakresyChecked ? "PzgZgloszenieDictZakres.sql" : "PzgZgloszenieDictBezZakres.sql");

                command.Parameters.Clear();
                command.Parameters.Add(obrebIdOracleParameter);
                obrebIdOracleParameter.Value = obrebId;

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgZgloszenieDict zgloszenieDict = new PzgZgloszenieDict();

                        while (reader.Read())
                        {
                            PzgZgloszenie zgloszenie = new PzgZgloszenie
                            {
                                KergId = reader.GetIntId("kerg_id"),
                                PzgIdZgloszenia = reader.GetString("pzg_idZgloszenia"),
                                PzgDataZgloszenia = reader.GetDate("pzg_dataZgloszenia"),
                                IdZgloszeniaJedn = reader.GetString("idZgloszeniaJedn"),
                                IdZgloszeniaNr = reader.GetIntId("idZgloszeniaNr"),
                                IdZgloszeniaRok = reader.GetIntId("idZgloszeniaRok"),
                                IdZgloszeniaEtap = reader.GetInt32("idZgloszeniaEtap"),
                                IdZgloszeniaSepJednNr = reader.GetString("idZgloszeniaSepJednNr"),
                                IdZgloszeniaSepNrRok = reader.GetString("idZgloszeniaSepNrRok"),
                                PzgPolozenieObszaru = reader.GetString("pzg_polozenieObszaru"),
                                Obreb = reader.GetPzgZgloszenieAttr("obreb", string.Empty),
                                PzgPodmiotZglaszajacyNazwa = reader.GetPzgZgloszenieAttr("pzg_podmiotZglaszajacy", string.Empty),
                                PzgPodmiotZglaszajacyRegon = reader.GetPzgZgloszenieAttr("pzg_podmiotZglaszajacy", "regon"),
                                PzgPodmiotZglaszajacyPesel = reader.GetPzgZgloszenieAttr("pzg_podmiotZglaszajacy", "pesel"),
                                PzgCel = reader.GetPzgZgloszenieAttr("pzg_cel", "pzg_cel"),
                                CelArchiwalny = reader.GetPzgZgloszenieAttr("pzg_cel", "cel_archiwalny"),
                                PzgRodzaj = reader.GetPzgZgloszenieAttr("pzg_rodzaj", string.Empty),
                                OsobaUprawniona = reader.GetPzgZgloszenieAttr("osobaUprawniona", string.Empty)
                            };

                            zgloszenieDict.Add(zgloszenie.KergId, zgloszenie);
                        }

                        return zgloszenieDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"{e.Message}\n\n{e.Source}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgZgloszenieDict();
                }
            }
        }

        public PzgZgloszenie GetPzgZgloszenie(int kergId)
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                OracleParameter obrebIdOracleParameter = new OracleParameter
                {
                    OracleDbType = OracleDbType.Int32,
                    ParameterName = "kerg_id"
                };

                command.CommandText = SqlResource.GetSqlText(GlobalValues.ZakresyChecked ? "PzgZgloszenieZakres.sql" : "PzgZgloszenieBezZakres.sql");

                command.Parameters.Clear();
                command.Parameters.Add(obrebIdOracleParameter);
                obrebIdOracleParameter.Value = kergId;

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return new PzgZgloszenie();

                        reader.Read();

                        PzgZgloszenie zgloszenie = new PzgZgloszenie
                        {
                            KergId = reader.GetIntId("kerg_id"),
                            PzgIdZgloszenia = reader.GetString("pzg_idZgloszenia"),
                            PzgDataZgloszenia = reader.GetDate("pzg_dataZgloszenia"),
                            IdZgloszeniaJedn = reader.GetString("idZgloszeniaJedn"),
                            IdZgloszeniaNr = reader.GetIntId("idZgloszeniaNr"),
                            IdZgloszeniaRok = reader.GetIntId("idZgloszeniaRok"),
                            IdZgloszeniaEtap = reader.GetInt32("idZgloszeniaEtap"),
                            IdZgloszeniaSepJednNr = reader.GetString("idZgloszeniaSepJednNr"),
                            IdZgloszeniaSepNrRok = reader.GetString("idZgloszeniaSepNrRok"),
                            PzgPolozenieObszaru = reader.GetString("pzg_polozenieObszaru"),
                            Obreb = reader.GetPzgZgloszenieAttr("obreb", string.Empty),
                            PzgPodmiotZglaszajacyNazwa = reader.GetPzgZgloszenieAttr("pzg_podmiotZglaszajacy", string.Empty),
                            PzgPodmiotZglaszajacyRegon = reader.GetPzgZgloszenieAttr("pzg_podmiotZglaszajacy", "regon"),
                            PzgPodmiotZglaszajacyPesel = reader.GetPzgZgloszenieAttr("pzg_podmiotZglaszajacy", "pesel"),
                            PzgCel = reader.GetPzgZgloszenieAttr("pzg_cel", "pzg_cel"),
                            CelArchiwalny = reader.GetPzgZgloszenieAttr("pzg_cel", "cel_archiwalny"),
                            PzgRodzaj = reader.GetPzgZgloszenieAttr("pzg_rodzaj", string.Empty),
                            OsobaUprawniona = reader.GetPzgZgloszenieAttr("osobaUprawniona", string.Empty)
                        };

                        return zgloszenie;

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"{e.Message}\n\n{e.Source}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgZgloszenie();
                }
            }
        }

        public PzgMaterialZasobuDict GetPzgMaterialZasobuDict(int obrebId)
        {
            using (OracleCommand command = _oracleConnection.CreateCommand())
            {
                OracleParameter obrebIdOracleParameter = new OracleParameter
                {
                    OracleDbType = OracleDbType.Int32,
                    ParameterName = "obreb_id"
                };

                command.CommandText = SqlResource.GetSqlText(GlobalValues.ZakresyChecked ? "PzgMaterialZasobuDictZakres.sql" : "PzgMaterialZasobuDictBezZakres.sql");

                command.Parameters.Clear();
                command.Parameters.Add(obrebIdOracleParameter);
                obrebIdOracleParameter.Value = obrebId;

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        PzgMaterialZasobuDict operatDict = new PzgMaterialZasobuDict();

                        while (reader.Read())
                        {
                            PzgMaterialZasobu operat = new PzgMaterialZasobu
                            {
                                IdOp = reader.GetIntId("idop"),
                                PzgIdMaterialu = reader.GetString("pzg_IdMaterialu"),
                                PzgDataPrzyjecia = reader.GetDate("pzg_dataPrzyjecia"),
                                DataWplywu = reader.GetDate("dataWplywu"),
                                PzgNazwa = reader.GetPzgMaterialZasobuAttr("pzg_nazwa", string.Empty),
                                PzgPolozenieObszaru = reader.GetString("pzg_polozenieObszaru"),
                                Obreb = reader.GetPzgMaterialZasobuAttr("obreb", string.Empty),
                                PzgTworcaNazwa = reader.GetPzgMaterialZasobuAttr("pzg_tworca_id", string.Empty),
                                PzgTworcaRegon = reader.GetPzgMaterialZasobuAttr("pzg_tworca_id", "regon"),
                                PzgTworcaPesel = reader.GetPzgMaterialZasobuAttr("pzg_tworca_id", "pesel"),
                                PzgSposobPozyskania = reader.GetPzgMaterialZasobuAttr("pzg_sposobPozyskania", string.Empty),
                                PzgPostacMaterialu = reader.GetPzgMaterialZasobuAttr("pzg_postacMaterialu", string.Empty),
                                PzgRodzNosnika = reader.GetPzgMaterialZasobuAttr("pzg_rodzNosnika", string.Empty),
                                PzgDostep = reader.GetPzgMaterialZasobuAttr("pzg_dostep", string.Empty),
                                PzgPrzyczynyOgraniczen = reader.GetString("pzg_przyczynyOgraniczen"),
                                PzgTypMaterialu = reader.GetPzgMaterialZasobuAttr("pzg_typMaterialu", string.Empty),
                                PzgKatArchiwalna = reader.GetPzgMaterialZasobuAttr("pzg_katArchiwalna", string.Empty),
                                PzgJezyk = reader.GetString("pzg_jezyk"),
                                PzgOpis = reader.GetString("pzg_opis"),
                                PzgOznMaterialuZasobu = reader.GetString("pzg_oznMaterialuZasobu"),
                                OznMaterialuZasobuTyp = reader.GetPzgMaterialZasobuAttr("oznMaterialuZasobuTyp", string.Empty),
                                OznMaterialuZasobuJedn = reader.GetString("oznMaterialuZasobuJedn"),
                                OznMaterialuZasobuNr = reader.GetInt32("oznMaterialuZasobuNr"),
                                OznMaterialuZasobuRok = reader.GetInt32("oznMaterialuZasobuRok"),
                                OznMaterialuZasobuTom = reader.GetInt32("oznMaterialuZasobuTom"),
                                OznMaterialuZasobuSepJednNr = reader.GetString("oznMaterialuZasobuSepJednNr"),
                                OznMaterialuZasobuSepnrRok = reader.GetString("oznMaterialuZasobuSepNrRok"),
                                PzgDokumentWyl = reader.GetString("pzg_dokumentWyl"),
                                PzgDataWyl = reader.GetDate("pzg_dataWyl"),
                                PzgDataArchLubBrak = reader.GetDate("pzg_dataArchLubBrak"),
                                PzgCel = reader.GetPzgMaterialZasobuAttr("pzg_cel", "pzg_cel"),
                                CelArchiwalny = reader.GetPzgMaterialZasobuAttr("pzg_cel", "cel_archiwalny"),
                                DzialkaPrzed = reader.GetPzgMaterialZasobuAttr("dzialkaPrzed", string.Empty),
                                DzialkaPo = reader.GetPzgMaterialZasobuAttr("dzialkaPo", string.Empty),
                                Opis2 = reader.GetString("opis2"),
                                LiczbaSkanow = reader.GetIntId("liczba_skanow"),
                                LiczbaDokS = reader.GetIntId("liczba_doks"),
                                KergId = reader.GetInt32("kerg_id")
                            };

                            operatDict.Add(operat.IdOp, operat);
                        }

                        return operatDict;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new PzgMaterialZasobuDict();
                }
            }
        }

        public void SaveFilesForObreb(int obrebId, string dirName)
        {
            string obrebListId = obrebId == 0 ? "[brak obrębu] XXX" : DbDictionary.EgbObrebEwidencyjny.GetListId(obrebId);

            using (OracleCommand command = _oracleConnection.CreateCommand()) // polecenie dla połączenia
            {
                OracleParameter obrebIdOracleParameter = new OracleParameter
                {
                    OracleDbType = OracleDbType.Int32,
                    ParameterName = "obreb_id"
                };

                command.CommandText = SqlResource.GetSqlText("ToolsKdokWskBlob.sql");
                command.Parameters.Clear();
                command.Parameters.Add(obrebIdOracleParameter);
                obrebIdOracleParameter.Value = obrebId;

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idOp = reader.GetIntId("idop");
                            int idRodzDok = reader.GetIntId("id_rodz_dok");
                            string typPliku = reader.GetString("typ_pliku");
                            byte[] blobData = (byte[])reader["data"];
                            DateTime dataD = (DateTime)reader["data_d"];

                            string outDirectory = DbDictionary.PzgMaterialZasobu.GetIdMaterialu(idOp) != string.Empty ?
                                DbDictionary.PzgMaterialZasobu.GetIdMaterialu(idOp) : DbDictionary.PzgMaterialZasobu.GetOznMaterialuZasobu(idOp).Replace('/', '_');

                            if (!Directory.Exists(Path.Combine(dirName, obrebListId, outDirectory)))
                            {
                                Directory.CreateDirectory(Path.Combine(dirName, obrebListId, outDirectory));
                            }

                            string prefix = DbDictionary.SloSzczRodzDok.GetPrefix(idRodzDok);

                            string fileName = Path.GetFileNameWithoutExtension(typPliku);
                            string ext = Path.GetExtension(typPliku);

                            typPliku = fileName + " [" + prefix + "]" + ext;

                            string fileNameAndPath = Path.Combine(dirName, obrebListId, outDirectory, typPliku);

                            int counter = 1;

                            while (File.Exists(fileNameAndPath))
                            {
                                typPliku = fileName + " [" + prefix + "]" + "_" + $"{counter++:000}" + ext;
                                fileNameAndPath = Path.Combine(dirName, obrebListId, outDirectory, typPliku);
                            }

                            using (FileStream fs = new FileStream(fileNameAndPath, FileMode.CreateNew, FileAccess.Write))
                            {
                                fs.Write(blobData, 0, blobData.Length);
                            }

                            File.SetLastWriteTime(fileNameAndPath, dataD);
                            File.SetCreationTime(fileNameAndPath, dataD);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        public void SaveWktForObreb(int obrebId, string dirName)
        {
            string obrebListId = obrebId == 0 ? "[brak obrębu] XXX" : DbDictionary.EgbObrebEwidencyjny.GetListId(obrebId);

            using (OracleCommand command = _oracleConnection.CreateCommand()) // polecenie dla połączenia
            {
                OracleParameter obrebIdOracleParameter = new OracleParameter
                {
                    OracleDbType = OracleDbType.Int32,
                    ParameterName = "obreb_id"
                };

                command.CommandText = SqlResource.GetSqlText("ToolsKdokWskBlob.sql");
                command.Parameters.Clear();
                command.Parameters.Add(obrebIdOracleParameter);
                obrebIdOracleParameter.Value = obrebId;

                try
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idOp = reader.GetIntId("idop");
                            string typPliku = reader.GetString("typ_pliku");
                            string geom = reader.GetString("geom");
                            DateTime dataD = (DateTime)reader["data_d"];

                            string outDirectory = DbDictionary.PzgMaterialZasobu.GetIdMaterialu(idOp) != string.Empty ?
                                DbDictionary.PzgMaterialZasobu.GetIdMaterialu(idOp) : DbDictionary.PzgMaterialZasobu.GetOznMaterialuZasobu(idOp).Replace('/', '_');

                            if (!Directory.Exists(Path.Combine(dirName, obrebListId, outDirectory)))
                            {
                                Directory.CreateDirectory(Path.Combine(dirName, obrebListId, outDirectory));
                            }

                            string fileName = Path.GetFileNameWithoutExtension(typPliku);
                            const string ext = ".wkt";

                            string fileNameAndPath = Path.Combine(dirName, obrebListId, outDirectory, fileName + ext);

                            int counter = 2;

                            while (File.Exists(fileNameAndPath))
                            {
                                fileNameAndPath = Path.Combine(dirName, obrebListId, outDirectory, fileName + "_" + $"{counter++:000}" + ext);
                            }

                            File.WriteAllText(fileNameAndPath, geom, Encoding.UTF8);

                            File.SetLastWriteTime(fileNameAndPath, dataD);
                            File.SetCreationTime(fileNameAndPath, dataD);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
