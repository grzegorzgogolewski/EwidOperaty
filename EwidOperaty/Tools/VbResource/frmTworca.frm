VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} frmTworca 
   Caption         =   "Twórca"
   ClientHeight    =   7410
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   18060
   OleObjectBlob   =   "frmTworca.frx":0000
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "frmTworca"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit

Private Sub CommandButtoAnuluj_Click()

    Application.EnableEvents = False

    Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn) = g_tworcaOld

    frmTworca.Hide

    Application.EnableEvents = True

End Sub

Private Sub CommandButtonWybierz_Click()

    Application.EnableEvents = False
    
    If ListBoxTworca.ListCount = 0 Then
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn) = g_tworcaOld
        Application.EnableEvents = True
        Exit Sub
    End If

    Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn).Select
    
    If ListBoxTworca.ListIndex >= 0 Then
    
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn) = ListBoxTworca.List(ListBoxTworca.ListIndex, 1)        ' NAZWA
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn + 1) = ListBoxTworca.List(ListBoxTworca.ListIndex, 2)    ' REGON
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn + 2) = ListBoxTworca.List(ListBoxTworca.ListIndex, 3)    ' PESEL
    
    Else
    
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn) = ""        ' NAZWA
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn + 1) = ""    ' REGON
        Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn + 2) = ""    ' PESEL
        
    End If
    
    frmTworca.Hide
    
    Application.EnableEvents = True
    
End Sub

Private Sub CommandButtoUsun_Click()
    
    Application.EnableEvents = False

    Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn) = ""
    Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn + 1) = ""
    Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn + 2) = ""

    frmTworca.Hide

    Application.EnableEvents = True
    
End Sub

Private Sub UserForm_Activate()

    Application.EnableEvents = False
    
    ListBoxTworca.ColumnWidths = "60;500;50;50;30;50"
    ListBoxTworca.ColumnCount = 6
    
    Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn).Select

    Dim tworcaSearch As String
    
    ' ---------------------------------------------------------------------------------------------
    ' Dodanie Twórców do s³ownika
    ' ---------------------------------------------------------------------------------------------
    Dim licznik As Long
    
    licznik = 2
    
    While PZG_PodmiotZglaszajacy.Cells(licznik, "A") <> ""
        licznik = licznik + 1
    Wend
    
    Dim LiczbaWykonawcow As Long
    
    LiczbaWykonawcow = licznik - 1
    
    ReDim g_TworcaDic(1 To LiczbaWykonawcow, 1 To 6) As String
    
    For licznik = 1 To LiczbaWykonawcow
        g_TworcaDic(licznik, 1) = PZG_PodmiotZglaszajacy.Cells(licznik + 1, "C")  ' nazwa
        g_TworcaDic(licznik, 2) = PZG_PodmiotZglaszajacy.Cells(licznik + 1, "E")  ' REGON
        g_TworcaDic(licznik, 3) = PZG_PodmiotZglaszajacy.Cells(licznik + 1, "D")  ' PESEL
        g_TworcaDic(licznik, 4) = PZG_PodmiotZglaszajacy.Cells(licznik + 1, "H") & " " & PZG_PodmiotZglaszajacy.Cells(licznik + 1, "I") & " " & PZG_PodmiotZglaszajacy.Cells(licznik + 1, "J") & " " & PZG_PodmiotZglaszajacy.Cells(licznik + 1, "K")  ' adres
        g_TworcaDic(licznik, 5) = PZG_PodmiotZglaszajacy.Cells(licznik + 1, "A")  ' osoba_id
        g_TworcaDic(licznik, 6) = PZG_PodmiotZglaszajacy.Cells(licznik + 1, "G")  ' stan
    Next licznik
    
    ' ---------------------------------------------------------------------------------------------
    
    tworcaSearch = Worksheets("PZG_MaterialZasobu").Cells(g_tworcaRow, g_tworcaColumn)
    
    Dim licznikLista As Long
    
    licznikLista = 0
    
    ListBoxTworca.Clear
    
    For licznik = 1 To LiczbaWykonawcow
    
        If InStr(1, UCase(g_TworcaDic(licznik, 1)), UCase(tworcaSearch)) > 0 Then
            
            ListBoxTworca.AddItem
            ListBoxTworca.List(licznikLista, 0) = g_TworcaDic(licznik, 5)   '   OSOBA_ID
            ListBoxTworca.List(licznikLista, 1) = g_TworcaDic(licznik, 1)   '   NAZWA
            ListBoxTworca.List(licznikLista, 2) = g_TworcaDic(licznik, 2)   '   REGON
            ListBoxTworca.List(licznikLista, 3) = g_TworcaDic(licznik, 3)   '   PESEL
            ListBoxTworca.List(licznikLista, 4) = g_TworcaDic(licznik, 6)   '   STAN
            ListBoxTworca.List(licznikLista, 5) = g_TworcaDic(licznik, 4)   '   ADRES
            
            licznikLista = licznikLista + 1
            
        End If
    
    Next licznik
    
    'If ListBoxTworca.ListCount > 0 Then ListBoxTworca.Selected(0) = True
    
    ListBoxTworca.SetFocus
    
    Application.EnableEvents = True
    
End Sub

