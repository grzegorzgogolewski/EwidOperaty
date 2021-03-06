﻿using EwidOperaty.Oracle;
using EwidOperaty.Oracle.Dictionary;
using EwidOperaty.Tools;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using License;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.DataValidation.Contracts;
using static EwidOperaty.Tools.GlobalValues;

namespace EwidOperaty
{
    public partial class FormMain : Form
    {
        private readonly OracleWorker _oracleWorker = new OracleWorker();
        private readonly BackgroundWorker _getDataBackgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
        private readonly BackgroundWorker _saveDataBackgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
        private readonly BackgroundWorker _saveBlobBackgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
        private readonly BackgroundWorker _saveWktBackgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };

        public FormMain()
        {
            InitializeComponent();

            _getDataBackgroundWorker.DoWork += GetDataBackgroundWorkerOnDoWork;
            _getDataBackgroundWorker.RunWorkerCompleted += GetDataBackgroundWorkerOnRunWorkerCompleted;
            _getDataBackgroundWorker.ProgressChanged += GetDataBackgroundWorkerOnProgressChanged;

            _saveDataBackgroundWorker.DoWork += SaveDataBackgroundWorkerOnDoWork;
            _saveDataBackgroundWorker.RunWorkerCompleted += SaveDataBackgroundWorkerOnRunWorkerCompleted;
            _saveDataBackgroundWorker.ProgressChanged += SaveDataBackgroundWorkerOnProgressChanged;

            _saveBlobBackgroundWorker.DoWork += SaveBlobBackgroundWorkerOnDoWork;
            _saveBlobBackgroundWorker.RunWorkerCompleted += SaveBlobBackgroundWorkerOnRunWorkerCompleted;
            _saveBlobBackgroundWorker.ProgressChanged += SaveBlobBackgroundWorkerOnProgressChanged;

            _saveWktBackgroundWorker.DoWork += SaveWktBackgroundWorkerOnDoWork;
            _saveWktBackgroundWorker.RunWorkerCompleted += SaveWktBackgroundWorkerOnRunWorkerCompleted;
            _saveWktBackgroundWorker.ProgressChanged += SaveWktBackgroundWorkerOnProgressChanged;
        }

        // Procedura ładowania głównego okna aplikacji
        private void FormMain_Load(object sender, EventArgs e)
        {
            // ------------------------------------------------------------------------------------
            // Przypisanie parametrów do okna głównego aplikacji
            // ------------------------------------------------------------------------------------

            Text = Application.ProductName;
            Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Resources.ikona.ico") ?? throw new InvalidOperationException());

            groupBoxConnection.Text = @"Parametry połączenia";

            buttonConnect.Text = "Połącz";
            buttonDisconnect.Text = "Rozłącz";
            buttonOracleRead.Text = "Wczytaj dane o operatach";
            buttonSaveData.Text = "Zapisz dane do XLS";
            buttonSaveBlobToDisk.Text = "Zapisz skany na dysk";
            buttonSaveWktToDisk.Text = "Zapisz WKT na dysk";
            checkBoxBezObreb.Text = "Wczytać operaty bez obrębów?";
            checkBoxZakresRead.Text = "Wczytać zakresy?";
            checkBoxZakresZglWrite.Text = "Zapisać zakresy zgłoszeń?";
            checkBoxZakresOprWrite.Text = "Zapisać zakresy operatów?";

            buttonDisconnect.Enabled = false;
            buttonOracleRead.Enabled = false;
            buttonSaveData.Enabled = false;
            buttonSaveBlobToDisk.Enabled = false;
            buttonSaveWktToDisk.Enabled = false;

            Location = new Point(Convert.ToInt32(IniConfig.ReadIni("FormMain", "X")), Convert.ToInt32(IniConfig.ReadIni("FormMain", "Y")));

            textBoxHost.Text = IniConfig.ReadIni("Database", "Host");
            textBoxDatabase.Text = IniConfig.ReadIni("Database", "Database");
            textBoxUser.Text = IniConfig.ReadIni("Database", "User").UnProtectString();
            textBoxPass.Text = IniConfig.ReadIni("Database", "Pass").UnProtectString();

            checkBoxZakresRead.Checked = Convert.ToBoolean(IniConfig.ReadIni("FormMain", "checkBoxZakresRead"));
            checkBoxZakresZglWrite.Checked = Convert.ToBoolean(IniConfig.ReadIni("FormMain", "checkBoxZakresZglWrite"));
            checkBoxZakresOprWrite.Checked = Convert.ToBoolean(IniConfig.ReadIni("FormMain", "checkBoxZakresOprWrite"));
            checkBoxBezObreb.Checked = Convert.ToBoolean(IniConfig.ReadIni("FormMain", "checkBoxBezObreb"));

            toolStripStatusLabel.Text = @"Gotowy";
        }

        // Wywołanie obsługi zamykania okna głównego aplikacji
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ------------------------------------------------------------------------------------
            // Zapisanie parametrów połączenia do pliku konfiguracyjnego
            // ------------------------------------------------------------------------------------

            IniConfig.SaveIni("Database", "Host", textBoxHost.Text);
            IniConfig.SaveIni("Database", "Database", textBoxDatabase.Text);
            IniConfig.SaveIni("Database", "User", textBoxUser.Text.ProtectString());
            IniConfig.SaveIni("Database", "Pass", textBoxPass.Text.ProtectString());

            IniConfig.SaveIni("FormMain", "X", Location.X.ToString());
            IniConfig.SaveIni("FormMain", "Y", Location.Y.ToString());

            IniConfig.SaveIni("FormMain", "checkBoxZakresRead", checkBoxZakresRead.Checked.ToString());
            IniConfig.SaveIni("FormMain", "checkBoxZakresZglWrite", checkBoxZakresZglWrite.Checked.ToString());
            IniConfig.SaveIni("FormMain", "checkBoxZakresOprWrite", checkBoxZakresOprWrite.Checked.ToString());
            IniConfig.SaveIni("FormMain", "checkBoxBezObreb", checkBoxBezObreb.Checked.ToString());
        }


        // Nawiązanie połączenia z bazą danych Oracle
        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            OracleStatus oracleStatus = _oracleWorker.Connect(textBoxHost.Text, textBoxDatabase.Text, textBoxUser.Text, textBoxPass.Text);

            if (oracleStatus.Status)
            {
                toolStripStatusLabel.Text = @"Ładowanie słownika: EGB_ObrebEwidencyjny...";
                DbDictionary.EgbObrebEwidencyjny = _oracleWorker.GetEgbObrebEwidencyjnyDict();

                toolStripStatusLabel.Text = @"Ładowanie słownika: EGB_Gmina...";
                DbDictionary.EgbGmina = _oracleWorker.GetEgbGminaDict();

                foreach (EgbGmina gmina in DbDictionary.EgbGmina.Values)
                {
                    checkedListBoxGminy.Items.Add(gmina.ListId);
                }

                buttonConnect.Enabled = false;      //  dezaktywacja przycisku połączenia z bazą
                buttonDisconnect.Enabled = true;    //  aktywacja przycisku rozłączenia z bazą
                buttonOracleRead.Enabled = true;    //  aktywacja przycisku pobierania danych z bazy
                buttonSaveBlobToDisk.Enabled = true;    // aktywacja przycisku zapisywania skanów operatow na dysk
            }

            toolStripStatusLabel.Text = oracleStatus.Text;
        }

        // Rozłączenie z bazą danych
        private void ButtonDisconnect_Click(object sender, EventArgs e)
        {
            _oracleWorker.Disconect();

            buttonConnect.Enabled = true;       //  aktywacja przycisku połączenia z bazą
            buttonDisconnect.Enabled = false;   //  dezaktywacja przycisku rozłączenia z bazą
            buttonOracleRead.Enabled = false;   //  dezaktywacja przycisku pobierania danych z bazy
            buttonSaveData.Enabled = false;     //  dezaktywacja przycisku zapisu danych do XLS
            buttonSaveBlobToDisk.Enabled = false;

            checkedListBoxGminy.Items.Clear();
            checkedListBoxObreby.Items.Clear();

            toolStripStatusLabel.Text = @"Rozłączono z bazą";
        }

        // Wczytanie danych z bazy EWID i zapis do pliku XLSX
        private void ButtonOracleRead_Click(object sender, EventArgs e)
        {
            buttonOracleRead.Enabled = false;
            buttonDisconnect.Enabled = false;
            buttonSaveData.Enabled = false;
            buttonSaveBlobToDisk.Enabled = false;
            buttonSaveWktToDisk.Enabled = false;

            _getDataBackgroundWorker.RunWorkerAsync();
        }

        private void GetDataBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_NosnikNieelektroniczny...";
            DbDictionary.PzgNosnikNieelektroniczny = _oracleWorker.GetPzgNosnikNieelektronicznyDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_TypMaterialu...";
            DbDictionary.PzgTypMaterialu = _oracleWorker.GetPzgTypMaterialuDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_NazwaMat...";
            DbDictionary.PzgNazwaMat = _oracleWorker.GetPzgNazwaMatDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: SLO_SzczRodzDok...";
            DbDictionary.SloSzczRodzDok = _oracleWorker.GetSloSzczRodzDokDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: SLO_OperatTyp...";
            DbDictionary.SloOperatTyp = _oracleWorker.GetSloOperatTypDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_CelPracy...";
            DbDictionary.PzgCelPracy = _oracleWorker.GetPzgCelPracyDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: SLO_CelPracyArchiwalny...";
            DbDictionary.SloCelPracyArchiwalny = _oracleWorker.GetSloCelPracyArchiwalnyDictt();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_RodzajPracy...";
            DbDictionary.PzgRodzajPracy = _oracleWorker.GetPzgRodzajPracyDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_PodmiotZglaszajacy...";
            DbDictionary.PzgPodmiotZglaszajacy = _oracleWorker.GetPzgPodmiotZglaszajacyDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: SLO_OsobaUprawniona...";
            DbDictionary.SloOsobaUprawniona = _oracleWorker.GetSloOsobaUprawnionaDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_SposobPozyskania...";
            DbDictionary.PzgSposobPozyskania = _oracleWorker.GetPzgSposobPozyskaniaDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_Postac...";
            DbDictionary.PzgPostac = _oracleWorker.GetPzgPostacDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_RodzajDostepu...";
            DbDictionary.PzgRodzajDostepu = _oracleWorker.GetPzgRodzajDostepuDict();

            toolStripStatusLabel.Text = @"Ładowanie słownika: PZG_KatArchiw...";
            DbDictionary.PzgKatArchiw = _oracleWorker.GetPzgKatArchiwDict();

            _getDataBackgroundWorker.ReportProgress(0);

            DbDictionary.PzgZgloszenie = new PzgZgloszenieDict();

            DbDictionary.PzgMaterialZasobu = new PzgMaterialZasobuDict();

            if (IsOperatyWithoutObrebRead)
            {
                toolStripStatusLabel.Text = @"Ładowanie zgłoszeń bez obrębów...";

                DbDictionary.PzgZgloszenie.AddRange(_oracleWorker.GetPzgZgloszenieDict(0)); //  Wczytaj zgłoszenia bez przypisanego obrębu

                toolStripStatusLabel.Text = @"Ładowanie operatów bez obrębów...";

                DbDictionary.PzgMaterialZasobu.AddRange(_oracleWorker.GetPzgMaterialZasobuDict(0)); //  Wczytaj operaty bez przypisanego obrębu   
            }

            int counter = 1;

            foreach (object obreb in checkedListBoxObreby.CheckedItems)
            {
                toolStripStatusLabel.Text = $@"Ładowanie zgłoszeń: {obreb}";

                DbDictionary.PzgZgloszenie.AddRange(_oracleWorker.GetPzgZgloszenieDict(DbDictionary.EgbObrebEwidencyjny.GetObrebId(obreb.ToString())));

                int percentage = (counter++ * 100) / checkedListBoxObreby.CheckedItems.Count;
                _getDataBackgroundWorker.ReportProgress(percentage);
            }

            counter = 1;

            foreach (object obreb in checkedListBoxObreby.CheckedItems)
            {
                toolStripStatusLabel.Text = $@"Ładowanie operatów: {obreb}";

                DbDictionary.PzgMaterialZasobu.AddRange(_oracleWorker.GetPzgMaterialZasobuDict(DbDictionary.EgbObrebEwidencyjny.GetObrebId(obreb.ToString())));

                int percentage = (counter++ * 100) / checkedListBoxObreby.CheckedItems.Count;
                _getDataBackgroundWorker.ReportProgress(percentage);
            }

            foreach (PzgMaterialZasobu operat in DbDictionary.PzgMaterialZasobu.Values)
            {
                toolStripStatusLabel.Text = @"Ładowanie brakujących zgłoszeń...";

                if (operat.KergId != null && !DbDictionary.PzgZgloszenie.ContainsKey((int) operat.KergId))
                {
                    DbDictionary.PzgZgloszenie.Add((int) operat.KergId, _oracleWorker.GetPzgZgloszenie((int) operat.KergId));
                }
            }
            
        }

        private void GetDataBackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void GetDataBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonOracleRead.Enabled = true;
            buttonDisconnect.Enabled = true;
            buttonSaveData.Enabled = true;
            buttonSaveWktToDisk.Enabled = true;

            toolStripStatusLabel.Text = $"Pobrano {DbDictionary.PzgMaterialZasobu.Count} operatów oraz {DbDictionary.PzgZgloszenie.Count} zgłoszeń z bazy.";
        }
        
        private void ButtonSaveData_Click(object sender, EventArgs e)
        {
            string defaulFileName = checkedListBoxObreby.CheckedItems.Count == 1 ? checkedListBoxObreby.CheckedItems[0] + ".xlsm" : "wynik.xlsm";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsm)|*.xlsm",
                FileName = defaulFileName
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string fileName = saveFileDialog.FileName;

            buttonOracleRead.Enabled = false;
            buttonSaveData.Enabled = false;
            buttonSaveBlobToDisk.Enabled = false;
            buttonSaveWktToDisk.Enabled = false;

            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    buttonOracleRead.Enabled = true;

                    return;
                }
            }

            FileInfo xlsFile = new FileInfo(fileName);

            _saveDataBackgroundWorker.RunWorkerAsync(argument: xlsFile);
        }

        private void SaveDataBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo xlsFile = (FileInfo)e.Argument;

            const int operationsCountTotal = 21;

            int operationCount = 0;

            if (IsZakresyZglWrite) // Zapisywanie zakresów dla zgłoszeń
            {
                toolStripStatusLabel.Text = "Zapisywanie zakresów dla zgłoszeń...";
            
                using (StreamWriter csv = new StreamWriter(File.Open(Path.Combine(xlsFile.DirectoryName ?? throw new InvalidOperationException(), Path.GetFileNameWithoutExtension(xlsFile.Name) + "_zgloszenia.csv"), FileMode.Create), new UTF8Encoding(false)))
                {
                    csv.WriteLine("kerg_id,pzg_idZgloszenia,obreb,pzg_polozenieObszaru");

                    foreach (PzgZgloszenie kerg in DbDictionary.PzgZgloszenie.Values.Where(kerg => kerg.PzgPolozenieObszaru != string.Empty))
                    {
                        csv.WriteLine(kerg.KergId + "," + kerg.PzgIdZgloszenia + "," + kerg.Obreb + ",\"" + kerg.PzgPolozenieObszaru + "\"");

                        string folderObrebName = string.IsNullOrEmpty(kerg.Obreb) ? "[brak obrębu] XXX ZGL" : DbDictionary.EgbObrebEwidencyjny.GetListIdGus(kerg.Obreb) + " ZGL";

                        string fileName = kerg.PzgIdZgloszenia.Replace('/', '_') + ".wkt";

                        DirectoryInfo outputDir = Directory.CreateDirectory(Path.Combine(xlsFile.DirectoryName, folderObrebName));

                        File.WriteAllText(Path.Combine(outputDir.FullName, fileName), kerg.PzgPolozenieObszaru, new UTF8Encoding(false));
                    }
                }
            }

            if (IsZakresyOprWrite) // Zapisywanie zakresów dla operatów
            {
                toolStripStatusLabel.Text = "Zapisywanie zakresów dla operatów...";
                
                using (StreamWriter csv = new StreamWriter(File.Open(Path.Combine(xlsFile.DirectoryName ?? throw new InvalidOperationException(), Path.GetFileNameWithoutExtension(xlsFile.Name) + "_operaty.csv"), FileMode.Create), new UTF8Encoding(false)))
                {
                    csv.WriteLine("idop,pzg_IdMaterialu,pzg_oznMaterialuZasobu,obreb,pzg_polozenieObszaru");

                    foreach (PzgMaterialZasobu oper in DbDictionary.PzgMaterialZasobu.Values.Where(oper => oper.PzgPolozenieObszaru != string.Empty))
                    {
                        csv.WriteLine(oper.IdOp + "," + oper.PzgIdMaterialu + "," + oper.PzgOznMaterialuZasobu + "," + oper.Obreb + ",\"" + oper.PzgPolozenieObszaru + "\"");

                        string folderObrebName = string.IsNullOrEmpty(oper.Obreb) ? "[brak obrębu] XXX" : DbDictionary.EgbObrebEwidencyjny.GetListIdGus(oper.Obreb);

                        string fileName = string.IsNullOrEmpty(oper.PzgIdMaterialu) ? oper.Obreb + '_' + oper.PzgOznMaterialuZasobu.Replace('/', '_') + ".wkt" : oper.PzgIdMaterialu  + ".wkt";

                        DirectoryInfo outputDir = Directory.CreateDirectory(Path.Combine(xlsFile.DirectoryName, folderObrebName, Path.GetFileNameWithoutExtension(fileName)));
                        
                        File.WriteAllText(Path.Combine(outputDir.FullName, fileName), oper.PzgPolozenieObszaru, new UTF8Encoding(false));
                    }
                }
            }

            // przypisanie do obiektu informacji o zakresie zamiast samego zakresu
            foreach (PzgZgloszenie kerg in DbDictionary.PzgZgloszenie.Values.Where(kerg => kerg.PzgPolozenieObszaru != string.Empty))
            {
                kerg.PzgPolozenieObszaru = "Istnieje";
            }

            // przypisanie do obiektu informacji o zakresie zamiast samego zakresu
            foreach (PzgMaterialZasobu oper in DbDictionary.PzgMaterialZasobu.Values.Where(oper => oper.PzgPolozenieObszaru != string.Empty))
            {
                oper.PzgPolozenieObszaru = "Istnieje";
            }

            using (ExcelPackage xlsWorkbook = new ExcelPackage(xlsFile))
            {
                xlsWorkbook.Workbook.Properties.Title = "Dane z PZGiK";
                xlsWorkbook.Workbook.Properties.Author = "Grzegorz Gogolewski";
                xlsWorkbook.Workbook.Properties.Comments = "Raport wygenerowany przez aplikację: " + Application.ProductName + " [" + Application.ProductVersion + "]";
                xlsWorkbook.Workbook.Properties.Company = "GISNET";

                ExcelWorksheet xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_NosnikNieelektroniczny");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgNosnikNieelektroniczny.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_NosnikNieelektroniczny", xlsSheet.Cells["D2:D" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_TypMaterialu");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgTypMaterialu.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_TypMaterialu", xlsSheet.Cells["C2:C" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_NazwaMat");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgNazwaMat.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_NazwaMat", xlsSheet.Cells["E2:E" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("SLO_SzczRodzDok");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.SloSzczRodzDok.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("SLO_SzczRodzDok", xlsSheet.Cells["E2:E" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("EGB_ObrebEwidencyjny");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.EgbObrebEwidencyjny.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("EGB_ObrebEwidencyjny", xlsSheet.Cells["G2:G" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("EGB_Gmina");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.EgbGmina.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("EGB_Gmina", xlsSheet.Cells["E2:E" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("SLO_OperatTyp");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.SloOperatTyp.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("SLO_OperatTyp", xlsSheet.Cells["D2:D" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_CelPracy");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgCelPracy.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_CelPracy", xlsSheet.Cells["E2:E" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("SLO_CelPracyArchiwalny");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.SloCelPracyArchiwalny.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("SLO_CelPracyArchiwalny", xlsSheet.Cells["E2:E" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_RodzajPracy");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgRodzajPracy.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_RodzajPracy", xlsSheet.Cells["D2:D" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_PodmiotZglaszajacy");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgPodmiotZglaszajacy.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_PodmiotZglaszajacy", xlsSheet.Cells["L2:L" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("SLO_OsobaUprawniona");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.SloOsobaUprawniona.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("SLO_OsobaUprawniona", xlsSheet.Cells["I2:I" + xlsSheet.Dimension.Rows]); 

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_SposobPozyskania");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgSposobPozyskania.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_SposobPozyskania", xlsSheet.Cells["D2:D" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_Postac");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgPostac.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_Postac", xlsSheet.Cells["C2:C" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_RodzajDostepu");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgRodzajDostepu.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_RodzajDostepu", xlsSheet.Cells["C2:C" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_KatArchiw");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgKatArchiw.Values, PrintHeaders: true);
                xlsSheet.Hidden = eWorkSheetHidden.Hidden;
                xlsWorkbook.Workbook.Names.Add("PZG_KatArchiw", xlsSheet.Cells["C2:C" + xlsSheet.Dimension.Rows]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_Zgloszenie");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgZgloszenie.Values, PrintHeaders: true);
                xlsWorkbook.Workbook.Names.Add("PZG_IdZgloszenia", xlsSheet.Cells["B2:B" + (xlsSheet.Dimension.Rows + 1000)]);

                xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("PZG_MaterialZasobu");
                xlsSheet.Cells[1, 1].LoadFromCollection(DbDictionary.PzgMaterialZasobu.Values, PrintHeaders: true);

                xlsSheet.Select("B1");

                _saveDataBackgroundWorker.ReportProgress(++operationCount * 100 / operationsCountTotal);

                foreach (ExcelWorksheet sheet in xlsWorkbook.Workbook.Worksheets)
                {
                    toolStripStatusLabel.Text = $"Zapisywanie pliku {xlsFile.Name} [{sheet.Name}]";

                    int rowsCount = sheet.Dimension.Rows;
                    int columnsCount = sheet.Dimension.Columns;

                    sheet.Cells.Style.Font.Size = 11;
                    sheet.View.FreezePanes(2, 1);

                    if (sheet.Name == "PZG_MaterialZasobu")
                    {
                        columnsCount += 5;
                    }

                    // formatowanie nagłowka
                    using (ExcelRange range = sheet.Cells[1, 1, 1, columnsCount])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Color.SetColor(Color.Black);
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        range.Style.WrapText = true;
                        range.AutoFilter = true;

                        range.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    }

                    switch (sheet.Name)
                    {
                        case "PZG_MaterialZasobu":  //  Arkusz z operatami

                            // dodawanie informacji o zgłoszeniach dla rekordów operatów
                            for (int i = 2; i <= rowsCount; i++)
                            {
                                if (sheet.Cells[i, 39].Value == null) continue; //  jeśli operat nie ma przypisanego KERG to go pomiń

                                int kergId = (int)sheet.Cells[i, 39].Value;

                                sheet.Cells[i, 40].Value = DbDictionary.PzgZgloszenie.GetPzgIdZgloszenia(kergId);
                                sheet.Cells[i, 41].Value = DbDictionary.PzgZgloszenie.GetPzgDataZgloszenia(kergId);
                                sheet.Cells[i, 42].Value = DbDictionary.PzgZgloszenie.GetObreb(kergId);
                                sheet.Cells[i, 43].Value = DbDictionary.PzgZgloszenie.GetPzgRodzaj(kergId);
                                sheet.Cells[i, 44].Value = DbDictionary.PzgZgloszenie.GetOsobaUprawniona(kergId);
                            }

                            sheet.Cells[2, 1, rowsCount, columnsCount - 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                            sheet.Cells[2, 1, rowsCount, columnsCount].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                            sheet.View.FreezePanes(2, 3);

                            sheet.Cells[1, 1].Value = "IdOp\n[1]";
                            sheet.Column(1).Width = 8;
                            sheet.Cells[2, 1, rowsCount + 1000, 1].Style.Numberformat.Format = "General";
                            sheet.Column(1).Hidden = true;

                            sheet.Cells[1, 2].Value = "PzgIdMaterialu\n[2]";
                            sheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(2).Width = 19; 
                            sheet.Cells[2, 2, rowsCount + 1000, 2].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 3].Value = "PzgData\nPrzyjecia\n[3]";
                            sheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Cells[2, 3, rowsCount + 1000, 3].Style.Numberformat.Format = "yyyy-MM-dd";
                            sheet.Column(3).Width = 11; 
                            
                            sheet.Cells[1, 4].Value = "Data\nWplywu\n[4]";
                            sheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Cells[2, 4, rowsCount + 1000, 4].Style.Numberformat.Format = "yyyy-MM-dd";
                            sheet.Column(4).Width = 11;

                            sheet.Cells[1, 5].Value = "PzgNazwa\n[5]";
                            sheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255,235,156));
                            sheet.Column(5).Width = 17; 
                            sheet.Cells[2, 5, rowsCount + 1000, 5].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 6].Value = "PzgPolozenie\nObszaru\n[6]";
                            sheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Column(6).Width = 13;
                            sheet.Cells[2, 6, rowsCount + 1000, 6].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 7].Value = "Obreb\n[7]";
                            sheet.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(7).Width = 14; 
                            sheet.Cells[2, 7, rowsCount + 1000, 7].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 8].Value = "PzgTworcaOsobaId\n[8]";
                            //sheet.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(8).Width = 40; 
                            sheet.Cells[2, 8, rowsCount + 1000, 8].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 9].Value = "PzgTworcaNazwa\n[9]";
                            sheet.Cells[1, 9].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(9).Width = 40; 
                            sheet.Cells[2, 9, rowsCount + 1000, 9].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 10].Value = "PzgTworca\nRegon\n[10]";
                            //sheet.Cells[1, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Cells[2, 10, rowsCount + 1000, 10].Style.Numberformat.Format = "@";
                            sheet.Column(10).Width = 11; 

                            sheet.Cells[1, 11].Value = "PzgTworca\nPesel\n[11]";
                            sheet.Cells[2, 11, rowsCount + 1000, 11].Style.Numberformat.Format = "@";
                            sheet.Column(11).Width = 11; 

                            sheet.Cells[1, 12].Value = "PzgSposob\nPozyskania\n[12]";
                            sheet.Cells[1, 12].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 12, rowsCount + 1000, 12].Style.Numberformat.Format = "@";
                            sheet.Column(12).Width = 26; 

                            sheet.Cells[1, 13].Value = "PzgPostac\nMaterialu\n[13]";
                            sheet.Cells[1, 13].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 13, rowsCount + 1000, 13].Style.Numberformat.Format = "@";
                            sheet.Column(13).Width = 16;

                            sheet.Cells[1, 14].Value = "PzgRodz\nNosnika\n[14]";
                            sheet.Cells[1, 14].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 14, rowsCount + 1000, 14].Style.Numberformat.Format = "@";
                            sheet.Column(14).Width = 15;

                            sheet.Cells[1, 15].Value = "PzgDostep\n[15]";
                            sheet.Cells[1, 15].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Column(15).Width = 16; 
                            sheet.Cells[2, 15, rowsCount + 1000, 15].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 16].Value = "PzgPrzyczynyOgraniczen\n[16]";
                            sheet.Cells[1, 16].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Column(16).Width = 39; 
                            sheet.Cells[2, 16, rowsCount + 1000, 16].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 17].Value = "PzgTyp\nMaterialu\n[17]";
                            sheet.Cells[1, 17].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 17, rowsCount + 1000, 17].Style.Numberformat.Format = "@";
                            sheet.Column(17).Width = 12; 

                            sheet.Cells[1, 18].Value = "PzgKat\nArchiwalna\n[18]";
                            sheet.Cells[1, 18].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 18, rowsCount + 1000, 18].Style.Numberformat.Format = "@";
                            sheet.Column(18).Width = 11;

                            sheet.Cells[1, 19].Value = "PzgJezyk\n[19]";
                            sheet.Cells[1, 19].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Column(19).Width = 9; 
                            sheet.Cells[2, 19, rowsCount + 1000, 19].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 20].Value = "PzgOpis\n[20]";
                            sheet.Cells[1, 20].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Column(20).Width = 9; 
                            sheet.Cells[2, 20, rowsCount + 1000, 20].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 21].Value = "PzgOznMaterialu\nZasobu\n[21]";
                            sheet.Cells[1, 21].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Cells[2, 21, rowsCount + 1000, 21].Style.Numberformat.Format = "@";
                            sheet.Column(21).Width = 21; 

                            sheet.Cells[1, 22].Value = "OznMaterialu\nZasobu\nTyp\n[22]";
                            //sheet.Cells[1, 22].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Cells[2, 22, rowsCount + 1000, 22].Style.Numberformat.Format = "@";
                            sheet.Column(22).Width = 13; 

                            sheet.Cells[1, 23].Value = "OznMaterialu\nZasobu\nJedn\n[23]";
                            sheet.Cells[1, 23].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Cells[2, 23, rowsCount + 1000, 23].Style.Numberformat.Format = "@";
                            sheet.Column(23).Width = 13; 

                            sheet.Cells[1, 24].Value = "OznMaterialu\nZasobu\nNr\n[24]";
                            sheet.Cells[1, 24].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Column(24).Width = 13; 
                            sheet.Cells[2, 24, rowsCount + 1000, 24].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 25].Value = "OznMaterialu\nZasobu\nRok\n[25]";
                            sheet.Cells[1, 25].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Column(25).Width = 13; 
                            sheet.Cells[2, 25, rowsCount + 1000, 25].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 26].Value = "OznMaterialu\nZasobu\nTom\n[26]";
                            sheet.Cells[1, 26].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Column(26).Width = 13; 
                            sheet.Cells[2, 26, rowsCount + 1000, 26].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 27].Value = "OznMaterialu\nZasobu\nSepJednNr\n[27]";
                            //sheet.Cells[1, 27].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Cells[2, 27, rowsCount + 1000, 27].Style.Numberformat.Format = "@";
                            sheet.Column(27).Width = 13; 

                            sheet.Cells[1, 28].Value = "OznMaterialu\nZasobu\nSepnrRok\n[28]";
                            //sheet.Cells[1, 28].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 239, 206));
                            sheet.Cells[2, 28, rowsCount + 1000, 28].Style.Numberformat.Format = "@";
                            sheet.Column(28).Width = 13; 

                            sheet.Cells[1, 29].Value = "Pzg\nDokument\nWyl\n[29]";
                            sheet.Cells[2, 29, rowsCount + 1000, 29].Style.Numberformat.Format = "@";
                            sheet.Column(29).Width = 15; 

                            sheet.Cells[1, 30].Value = "PzgData\nWyl\n[30]";
                            sheet.Cells[2, 30, rowsCount + 1000, 30].Style.Numberformat.Format = "yyyy-MM-dd";
                            sheet.Column(30).Width = 11; 

                            sheet.Cells[1, 31].Value = "PzgData\nArch\nLubBrak\n[31]";
                            sheet.Cells[2, 31, rowsCount + 1000, 31].Style.Numberformat.Format = "yyyy-MM-dd";
                            sheet.Column(31).Width = 11;

                            sheet.Cells[1, 32].Value = "PzgCel\n[32]";
                            sheet.Cells[1, 32].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Column(32).Width = 40; 
                            sheet.Cells[2, 32, rowsCount + 1000, 32].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 33].Value = "Cel Archiwalny\n[33]";
                            sheet.Cells[1, 33].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 33, rowsCount + 1000, 33].Style.Numberformat.Format = "@";
                            sheet.Column(33).Width = 40; 

                            sheet.Cells[1, 34].Value = "Dzialka Przed\n[34]";
                            sheet.Cells[1, 34].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 34, rowsCount + 1000, 34].Style.Numberformat.Format = "@";
                            sheet.Column(34).Width = 25;

                            sheet.Cells[1, 35].Value = "Dzialka Po\n[35]";
                            sheet.Cells[1, 35].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Cells[2, 35, rowsCount + 1000, 35].Style.Numberformat.Format = "@";
                            sheet.Column(35).Width = 25;

                            sheet.Cells[1, 36].Value = "Opis2\n[36]";
                            sheet.Column(36).Width = 25; 
                            sheet.Cells[2, 36, rowsCount + 1000, 36].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 37].Value = "Liczba\nSkanów\n[37]";
                            sheet.Column(37).Width = 9;
                            sheet.Cells[2, 37, rowsCount + 1000, 37].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 38].Value = "Liczba\nDokS\n[38]";
                            sheet.Column(38).Width = 9;
                            sheet.Cells[2, 38, rowsCount + 1000, 38].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 39].Value = "KergId\n[39]";
                            sheet.Column(39).Width = 8;
                            sheet.Cells[2, 39, rowsCount + 1000, 39].Style.Numberformat.Format = "General";
                            sheet.Column(39).Hidden = true;

                            sheet.Cells[1, 40].Value = "PzgIdZgloszenia\n[40]";
                            sheet.Cells[1, 40].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(40).Width = 20;
                            sheet.Cells[2, 40, rowsCount + 1000, 40].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 41].Value = "PzgDataZgloszenia\n[41]";
                            sheet.Cells[1, 41].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(41).Width = 11;
                            sheet.Cells[2, 41, rowsCount + 1000, 41].Style.Numberformat.Format = "yyyy-MM-dd";

                            sheet.Cells[1, 42].Value = "Obreb\n[42]";
                            sheet.Cells[1, 42].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 199, 206));
                            sheet.Column(42).Width = 14;
                            sheet.Cells[2, 42, rowsCount + 1000, 42].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 43].Value = "PzgRodzaj\n[43]";
                            sheet.Cells[1, 43].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Column(43).Width = 40;
                            sheet.Cells[2, 43, rowsCount + 1000, 43].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 44].Value = "OsobaUprawniona\n[44]";
                            sheet.Cells[1, 44].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 235, 156));
                            sheet.Column(44).Width = 40;
                            sheet.Cells[2, 44, rowsCount + 1000, 44].Style.Numberformat.Format = "@";

                            // FORMATOWANIE WARUNKOWE
                            
                            IExcelConditionalFormattingExpression cfRuleDzPrzed = sheet.ConditionalFormatting.AddExpression(sheet.Cells[2, 34, rowsCount + 1000, 34]);
                            cfRuleDzPrzed.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            cfRuleDzPrzed.Style.Fill.BackgroundColor.Color = Color.Yellow;
                            cfRuleDzPrzed.Formula = "= CheckDzialka(AH2) <> 0";

                            IExcelConditionalFormattingExpression cfRuleDzPo = sheet.ConditionalFormatting.AddExpression(sheet.Cells[2, 35, rowsCount + 1000, 35]);
                            cfRuleDzPo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            cfRuleDzPo.Style.Fill.BackgroundColor.Color = Color.Yellow;
                            cfRuleDzPo.Formula = "= CheckDzialka(AI2) <> 0";

                            // SŁOWNIKI i POPRAWNOŚĆ DANYCH

                            // P musi mieć 3 kropki [2]
                            IExcelDataValidationCustom valPzgIdMaterialu = sheet.DataValidations.AddCustomValidation($"B2:B{rowsCount + 1000}");
                            valPzgIdMaterialu.ShowErrorMessage = true;
                            valPzgIdMaterialu.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgIdMaterialu.AllowBlank = false;
                            valPzgIdMaterialu.Formula.ExcelFormula = "=LEN(B2) - LEN(SUBSTITUTE(B2,\".\",\"\")) = 3";

                            // data przyjęcia między 1900 a dniem wygenerowania arkusza [3]
                            IExcelDataValidationDateTime valPzgDataPrzyjecia = sheet.DataValidations.AddDateTimeValidation($"C2:C{rowsCount + 1000}");
                            valPzgDataPrzyjecia.ShowErrorMessage = true;
                            valPzgDataPrzyjecia.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgDataPrzyjecia.AllowBlank = false;
                            valPzgDataPrzyjecia.Formula.Value = DateTime.Parse("1900-01-01");
                            valPzgDataPrzyjecia.Formula2.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                            // data wpływu między 1900 a dniem wygenerowania arkusza [4]
                            IExcelDataValidationDateTime valDataWplywu = sheet.DataValidations.AddDateTimeValidation($"D2:D{rowsCount + 1000}");
                            valDataWplywu.ShowErrorMessage = true;
                            valDataWplywu.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valDataWplywu.AllowBlank = false;
                            valDataWplywu.Formula.Value = DateTime.Parse("1900-01-01");
                            valDataWplywu.Formula2.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                            // nazwa materialu ze słownika PZG_NazwaMat [5]
                            IExcelDataValidationList valPzgNazwa = sheet.DataValidations.AddListValidation($"E2:E{rowsCount + 1000}");
                            valPzgNazwa.ShowErrorMessage = true;
                            valPzgNazwa.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgNazwa.AllowBlank = false;
                            valPzgNazwa.Formula.ExcelFormula = "=PZG_NazwaMat";

                            // nazwa obrębu ze słownika
                            IExcelDataValidationList valObreb = sheet.DataValidations.AddListValidation($"G2:G{rowsCount + 1000}");
                            valObreb.ShowErrorMessage = true;
                            valObreb.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valObreb.AllowBlank = false;
                            valObreb.Formula.ExcelFormula = "=EGB_ObrebEwidencyjny";

                            // sposób pozyskania ze słownika
                            IExcelDataValidationList valPzgSposobPozyskania = sheet.DataValidations.AddListValidation($"L2:L{rowsCount + 1000}");
                            valPzgSposobPozyskania.ShowErrorMessage = true;
                            valPzgSposobPozyskania.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgSposobPozyskania.AllowBlank = false;
                            valPzgSposobPozyskania.Formula.ExcelFormula = "=PZG_SposobPozyskania";

                            // postać materiału ze słownika
                            IExcelDataValidationList valPzgPostacMaterialu = sheet.DataValidations.AddListValidation($"M2:M{rowsCount + 1000}");
                            valPzgPostacMaterialu.ShowErrorMessage = true;
                            valPzgPostacMaterialu.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgPostacMaterialu.AllowBlank = false;
                            valPzgPostacMaterialu.Formula.ExcelFormula = "=PZG_Postac";

                            // rodzaj nośnika ze słownika
                            IExcelDataValidationList valPzgRodzNosnika = sheet.DataValidations.AddListValidation($"N2:N{rowsCount + 1000}");
                            valPzgRodzNosnika.ShowErrorMessage = true;
                            valPzgRodzNosnika.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgRodzNosnika.AllowBlank = false;
                            valPzgRodzNosnika.Formula.ExcelFormula = "=PZG_NosnikNieelektroniczny";

                            // rodzaj dostępu ze słownika
                            IExcelDataValidationList valPzgDostep = sheet.DataValidations.AddListValidation($"O2:O{rowsCount + 1000}");
                            valPzgDostep.ShowErrorMessage = true;
                            valPzgDostep.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgDostep.AllowBlank = false;
                            valPzgDostep.Formula.ExcelFormula = "=PZG_RodzajDostepu";

                            // typ materialu ze słownika
                            IExcelDataValidationList valPzgTypMaterialu = sheet.DataValidations.AddListValidation($"Q2:Q{rowsCount + 1000}");
                            valPzgTypMaterialu.ShowErrorMessage = true;
                            valPzgTypMaterialu.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgTypMaterialu.AllowBlank = false;
                            valPzgTypMaterialu.Formula.ExcelFormula = "=PZG_TypMaterialu";

                            // kategoria archiwalna ze słownika
                            IExcelDataValidationList valPzgKatArchiwalna = sheet.DataValidations.AddListValidation($"R2:R{rowsCount + 1000}");
                            valPzgKatArchiwalna.ShowErrorMessage = true;
                            valPzgKatArchiwalna.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgKatArchiwalna.AllowBlank = false;
                            valPzgKatArchiwalna.Formula.ExcelFormula = "=PZG_KatArchiw";

                            // numer zgloszenia ze słownika ze słownika
                            IExcelDataValidationList valPzgIdZgloszenia = sheet.DataValidations.AddListValidation($"AN2:AN{rowsCount + 1000}");
                            valPzgIdZgloszenia.ShowErrorMessage = true;
                            valPzgIdZgloszenia.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgIdZgloszenia.AllowBlank = false;
                            valPzgIdZgloszenia.Formula.ExcelFormula = "=PZG_IdZgloszenia";

                            // data zgłoszenia z zakresu
                            IExcelDataValidationDateTime valPzgDataZgloszenia = sheet.DataValidations.AddDateTimeValidation($"AO2:AO{rowsCount + 1000}");
                            valPzgDataZgloszenia.ShowErrorMessage = true;
                            valPzgDataZgloszenia.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgDataZgloszenia.AllowBlank = false;
                            valPzgDataZgloszenia.Formula.Value = DateTime.Parse("1900-01-01");
                            valPzgDataZgloszenia.Formula2.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                            // obręb ze słownika
                            IExcelDataValidationList valObrebZgl = sheet.DataValidations.AddListValidation($"AP2:AP{rowsCount + 1000}");
                            valObrebZgl.ShowErrorMessage = true;
                            valObrebZgl.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valObrebZgl.AllowBlank = false;
                            valObrebZgl.Formula.ExcelFormula = "=EGB_ObrebEwidencyjny";

                            // rodzaj pracy ze słownika
                            IExcelDataValidationList valPzgRodzaj = sheet.DataValidations.AddListValidation($"AQ2:AQ{rowsCount + 1000}");
                            valPzgRodzaj.ShowErrorMessage = true;
                            valPzgRodzaj.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                            valPzgRodzaj.AllowBlank = false;
                            valPzgRodzaj.Formula.ExcelFormula = "=PZG_RodzajPracy";

                            break;

                        case "PZG_Zgloszenie":  //  Arkusz ze zgłoszeniami

                            sheet.View.FreezePanes(2, 3);

                            sheet.Cells[1, 1].Value = "KergId\n[1]";
                            sheet.Column(1).Width = 8;  
                            sheet.Cells[2, 1, rowsCount, 1].Style.Numberformat.Format = "General";

                            sheet.Cells[1, 2].Value = "IdZgloszenia\n[2]";
                            sheet.Column(2).Width = 20; 
                            sheet.Cells[2, 2, rowsCount, 2].Style.Numberformat.Format = "@";

                            sheet.Cells[1, 3].Value = "Data\nZgloszenia\n[3]";
                            sheet.Column(3).Width = 11;
                            sheet.Cells[2, 3, rowsCount, 3].Style.Numberformat.Format = "yyyy-MM-dd";

                            sheet.Cells[1, 4].Value = "Jedn\n[4]";
                            sheet.Cells[2, 4, rowsCount, 4].Style.Numberformat.Format = "@";
                            sheet.Column(4).Width = 12;

                            sheet.Cells[1, 5].Value = "Nr\n[5]";
                            sheet.Cells[2, 5, rowsCount, 5].Style.Numberformat.Format = "General";
                            sheet.Column(5).Width = 12;

                            sheet.Cells[1, 6].Value = "Rok\n[6]";
                            sheet.Cells[2, 6, rowsCount, 6].Style.Numberformat.Format = "General";
                            sheet.Column(6).Width = 12;

                            sheet.Cells[1, 7].Value = "Etap\n[7]";
                            sheet.Cells[2, 7, rowsCount, 7].Style.Numberformat.Format = "General";
                            sheet.Column(7).Width = 12;

                            sheet.Cells[1, 8].Value = "Sep\nJednNr\n[8]";
                            sheet.Cells[2, 8, rowsCount, 8].Style.Numberformat.Format = "@";
                            sheet.Column(8).Width = 12;

                            sheet.Cells[1, 9].Value = "Sep\nNrRok\n[9]";
                            sheet.Cells[2, 9, rowsCount, 9].Style.Numberformat.Format = "@";
                            sheet.Column(9).Width = 12;

                            sheet.Cells[1, 10].Value = "Polozenie\nObszaru\n[10]";
                            sheet.Cells[2, 10, rowsCount, 10].Style.Numberformat.Format = "@";
                            sheet.Column(10).Width = 13;

                            sheet.Cells[1, 11].Value = "Obreb\n[11]";
                            sheet.Cells[2, 11, rowsCount, 11].Style.Numberformat.Format = "@";
                            sheet.Column(11).Width = 14;

                            sheet.Cells[1, 12].Value = "OsobaId\n[12]";
                            sheet.Cells[2, 12, rowsCount, 12].Style.Numberformat.Format = "@";
                            sheet.Column(12).Width = 14;

                            sheet.Cells[1, 13].Value = "Podmiot\nNazwa\n[13]";
                            sheet.Cells[2, 13, rowsCount, 13].Style.Numberformat.Format = "@";
                            sheet.Column(13).Width = 40;

                            sheet.Cells[1, 14].Value = "Podmiot\nRegon\n[14]";
                            sheet.Cells[2, 14, rowsCount, 14].Style.Numberformat.Format = "@";
                            sheet.Column(14).Width = 12;

                            sheet.Cells[1, 15].Value = "Podmiot\nPesel\n[15]";
                            sheet.Cells[2, 15, rowsCount, 15].Style.Numberformat.Format = "@";
                            sheet.Column(15).Width = 12;

                            sheet.Cells[1, 16].Value = "PzgCel\n[16]";
                            sheet.Cells[2, 16, rowsCount, 16].Style.Numberformat.Format = "@";
                            sheet.Column(16).Width = 40;

                            sheet.Cells[1, 17].Value = "CelArchiwalny\n[17]";
                            sheet.Cells[2, 17, rowsCount, 17].Style.Numberformat.Format = "@";
                            sheet.Column(17).Width = 40;

                            sheet.Cells[1, 18].Value = "PzgRodzaj\n[18]";
                            sheet.Cells[2, 18, rowsCount, 18].Style.Numberformat.Format = "@";
                            sheet.Column(18).Width = 40;

                            sheet.Cells[1, 19].Value = "OsobaUprawniona\n[19]";
                            sheet.Cells[2, 19, rowsCount, 19].Style.Numberformat.Format = "@";
                            sheet.Column(19).Width = 40;

                            break;
                    }

                    _saveDataBackgroundWorker.ReportProgress((++operationCount * 100) / operationsCountTotal);
                }

                _saveDataBackgroundWorker.ReportProgress((++operationCount * 100) / operationsCountTotal);

                // --------------------------------------------------------------------------------
                // Dodanie kodu makra
                // --------------------------------------------------------------------------------

                xlsWorkbook.Workbook.CreateVBAProject();

                xlsWorkbook.Workbook.Worksheets["PZG_MaterialZasobu"].CodeModule.Code = VbResource.GetVbText("PZG_MaterialZasobu.vb");
                
                xlsWorkbook.Workbook.VbaProject.Modules.AddModule("mdlMain").Code = VbResource.GetVbText("mdlMain.vb");

                // --------------------------------------------------------------------------------

                toolStripStatusLabel.Text = $@"Zapisywanie pliku {xlsFile.Name} [Finalizacja...]";

                xlsWorkbook.Save();

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmTworca.frm"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmTworca.frm"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmTworca.frx"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmTworca.frx"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmCel.frm"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmCel.frm"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmCel.frx"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmCel.frx"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmCelArchiwalny.frm"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmCelArchiwalny.frm"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmCelArchiwalny.frx"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmCelArchiwalny.frx"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmUprawniony.frm"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmUprawniony.frm"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                using (FileStream fileFrm = new FileStream(Path.Combine(xlsFile.DirectoryName, "frmUprawniony.frx"), FileMode.Create))
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("EwidOperaty.Tools.VbResource.frmUprawniony.frx"))
                    {
                        source?.CopyTo(fileFrm);
                    }
                }

                _saveDataBackgroundWorker.ReportProgress((++operationCount * 100) / operationsCountTotal);

            }
        }

        private void SaveDataBackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        // zakończenie procesu zapisywania pliku XLS
        private void SaveDataBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonSaveData.Enabled = true;
            buttonOracleRead.Enabled = true;
            buttonSaveBlobToDisk.Enabled = true;
            buttonSaveWktToDisk.Enabled = true;

            toolStripStatusLabel.Text = @"Plik zapisano.";
        }

        // Zaznaczanie wszystkich obrębów na liście obrębów
        private void ContextMenuObreby_ZaznaczWszystko_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxObreby.Items.Count; i++)
            {
                checkedListBoxObreby.SetItemChecked(i, true);
            }
        }

        private void ContextMenuObreby_OdzaznaczWszystko_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxObreby.Items.Count; i++)
            {
                checkedListBoxObreby.SetItemChecked(i, false);
            }
        }

        // Zaznaczanie wszystkich gmin na liście gmin
        private void ContextMenuGminy_ZaznaczWszystko_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxGminy.Items.Count; i++)
            {
                checkedListBoxGminy.SetItemChecked(i, true);
            }
        }

        private void ContextMenuGminy_OdznaczWszystko_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxGminy.Items.Count; i++)
            {
                checkedListBoxGminy.SetItemChecked(i, false);
            }
        }

        private void ButtonSaveBlobToDisk_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                SelectedPath = IniConfig.ReadIni("Files", "RecentFolder")
            };

            DialogResult result = fbd.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            IniConfig.SaveIni("Files", "RecentFolder", fbd.SelectedPath);

            buttonSaveBlobToDisk.Enabled = false;
            buttonOracleRead.Enabled = false;
            buttonSaveData.Enabled = false;
            buttonSaveWktToDisk.Enabled = false;

            DbDictionary.SloSzczRodzDok = _oracleWorker.GetSloSzczRodzDokDict();

            _saveBlobBackgroundWorker.RunWorkerAsync(argument: fbd.SelectedPath);
        }

        private void SaveBlobBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            string folderName = e.Argument.ToString();

            if (GlobalValues.IsOperatyWithoutObrebRead)
            {
                _oracleWorker.SaveFilesForObreb(0, folderName);
            }

            int counter = 1;

            foreach (object obreb in checkedListBoxObreby.CheckedItems)
            {
                toolStripStatusLabel.Text = $@"Zapisywanie plików dla obrębu: {obreb}";

                _oracleWorker.SaveFilesForObreb(DbDictionary.EgbObrebEwidencyjny.GetObrebId(obreb.ToString()), folderName);

                int percentage = (counter++ * 100) / checkedListBoxObreby.CheckedItems.Count;
                _saveBlobBackgroundWorker.ReportProgress(percentage);
            }
        }

        private void SaveBlobBackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void SaveBlobBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonSaveBlobToDisk.Enabled = true;
            buttonOracleRead.Enabled = true;
            buttonSaveData.Enabled = true;
            buttonSaveWktToDisk.Enabled = true;

            MessageBox.Show(@"Zapisano pliki na dysk", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonSaveWktToDisk_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                SelectedPath = IniConfig.ReadIni("Files", "RecentFolder")
            };

            DialogResult result = fbd.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            IniConfig.SaveIni("Files", "RecentFolder", fbd.SelectedPath);

            buttonSaveBlobToDisk.Enabled = false;
            buttonOracleRead.Enabled = false;
            buttonSaveData.Enabled = false;
            buttonSaveWktToDisk.Enabled = false;

            _saveWktBackgroundWorker.RunWorkerAsync(argument: fbd.SelectedPath);
        }

        private void SaveWktBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            string folderName = e.Argument.ToString();

            if (GlobalValues.IsOperatyWithoutObrebRead)
            {
                _oracleWorker.SaveWktOperatForObreb(0, folderName);
            }

            int counter = 1;

            foreach (object obreb in checkedListBoxObreby.CheckedItems)
            {
                toolStripStatusLabel.Text = $"Zapisywanie plików WKT dla obrębu: {obreb}";

                _oracleWorker.SaveWktOperatForObreb(DbDictionary.EgbObrebEwidencyjny.GetObrebId(obreb.ToString()), folderName);

                int percentage = (counter++ * 100) / checkedListBoxObreby.CheckedItems.Count;
                _saveWktBackgroundWorker.ReportProgress(percentage);
            }
        }

        private void SaveWktBackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void SaveWktBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonSaveBlobToDisk.Enabled = true;
            buttonOracleRead.Enabled = true;
            buttonSaveData.Enabled = true;
            buttonSaveWktToDisk.Enabled = true;

            MessageBox.Show(@"Zapisano pliki WKT na dysk", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // obsługa filtrowania obrębów na podstawie wybranych gmin
        private void CheckedListBoxGminy_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<string> gminaList = (from object item in checkedListBoxGminy.CheckedItems select item.ToString()).ToList();    // lista wybranych gmin

            if (e.NewValue == CheckState.Checked)
            {
                gminaList.Add(checkedListBoxGminy.Items[e.Index].ToString());       // jeśli została wybrana nowa gmina to dodaj ją do listy
            }
            else
            {
                gminaList.Remove(checkedListBoxGminy.Items[e.Index].ToString());    // jeśli została odznaczona gmina to usuń ją z listy
            }

            List<string> obrebList = new List<string>();    // lista wyświetlonych obrębów

            foreach (List<string> b in from item in gminaList // przeleć całą listę gmin
                select DbDictionary.EgbGmina.GetGminaId(item) into gminaId // pobierz ID gminy z listy
                select DbDictionary.EgbObrebEwidencyjny.Values.Where(p => p.GminaId == gminaId).ToList() into a // pobierz listę obrębów dla wybranej gminy
                select a.Select(p => p.ListId).ToList()) // pobierz listę nazw obrębów dla wybranej gminy
            {
                obrebList.AddRange(b);      // dodaj do listy obrębów
            }

            //foreach (string item in gminaList)      // przeleć całą listę gmin
            //{
            //    int gminaId = DbDictionary.EgbGmina.GetGminaId(item);   // pobierz ID gminy z listy

            //    List<EgbObrebEwidencyjny> a = DbDictionary.EgbObrebEwidencyjny.Values.Where(p => p.GminaId == gminaId).ToList(); // pobierz listę obrębów dla wybranej gminy

            //    List<string> b = a.Select(p => p.ListId).ToList();      // pobierz listę nazw obrębów dla wybranej gminy

            //    obrebList.AddRange(b);      // dodaj do listy obrębów
            //}

            checkedListBoxObreby.Items.Clear(); // wyczyść listę obrębów

            foreach (string obreb in obrebList)     // dodaj do listy obrębów wszystkie obręby dla wybranych gmin
            {
                checkedListBoxObreby.Items.Add(obreb);
            }

        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            MyLicense license = LicenseHandler.ReadLicenseFile(out LicenseStatus licStatus, out string validationMsg);

            switch (licStatus)
            {
                case LicenseStatus.Undefined:

                    MessageBox.Show("Brak pliku z licencją!\nIdentyfikator komputera: " + LicenseHandler.GenerateUid("EwidOperaty"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.Exit();
                    break;

                case LicenseStatus.Valid:

                    toolStripStatusLabel.Text = $"Licencja typu: '{license.Type}', ważna do: {license.LicenseEnd}";
                    break;

                case LicenseStatus.Invalid:
                case LicenseStatus.Cracked:
                case LicenseStatus.Expired:

                    MessageBox.Show(validationMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.Exit();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void CheckBoxBezObreb_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                IsOperatyWithoutObrebRead = checkBox.Checked;
            }
        }

        private void CheckBoxZakresRead_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                IsZakresyRead = checkBox.Checked;
            }
        }

        private void CheckBoxZakresZglWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                IsZakresyZglWrite = checkBox.Checked;
            }
        }

        private void CheckBoxZakresOprWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                IsZakresyOprWrite = checkBox.Checked;
            }
        }
    }
}

