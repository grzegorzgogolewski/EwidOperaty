VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} frmUprawniony 
   Caption         =   "Uprawniony"
   ClientHeight    =   9195.001
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   13110
   OleObjectBlob   =   "frmUprawniony.frx":0000
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "frmUprawniony"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub UserForm_Activate()

    ' ---------------------------------------------------------------------------------------------
    ' Dodanie wartosci do s³ownika
    ' ---------------------------------------------------------------------------------------------
    Dim licznik As Integer
    
    licznik = 2
    
    While SLO_OsobaUprawniona.Cells(licznik, "A") <> ""
        licznik = licznik + 1
    Wend
    
    Dim LiczbaWartosci As Integer
    
    LiczbaWartosci = licznik - 1
    
    Dim licznikLista As Long
    
    licznikLista = 0
    
    ListBoxUprawniony.Clear
    
    For licznik = 2 To LiczbaWartosci
        
        ListBoxUprawniony.AddItem
        ListBoxUprawniony.List(licznikLista, 0) = SLO_OsobaUprawniona.Cells(licznik, "I")
        ListBoxUprawniony.List(licznikLista, 1) = SLO_OsobaUprawniona.Cells(licznik, "E") + " " + SLO_OsobaUprawniona.Cells(licznik, "F") + " " + SLO_OsobaUprawniona.Cells(licznik, "G") + " " + SLO_OsobaUprawniona.Cells(licznik, "H")
        
        licznikLista = licznikLista + 1
        
    Next licznik
    ' ---------------------------------------------------------------------------------------------
    
    Dim aktualneWartosci As String
    Dim licznikWartosci As Integer
    
    aktualneWartosci = Worksheets("PZG_MaterialZasobu").Cells(g_uprawnionyRow, g_uprawnionyColumn)
    
    If InStr(1, aktualneWartosci, ";") > 0 Then
    
        Dim listaWartosci() As String
        
        listaWartosci = Split(aktualneWartosci, ";")
        
        For licznikWartosci = 0 To UBound(listaWartosci) - LBound(listaWartosci)
            
            For licznikLista = 0 To ListBoxUprawniony.ListCount - 1
            
                If ListBoxUprawniony.List(licznikLista, 0) = listaWartosci(licznikWartosci) Then
                
                    ListBoxUprawniony.Selected(licznikLista) = True
                
                End If
            
            Next licznikLista
        
        Next licznikWartosci
        
    Else
    
        For licznikLista = 0 To ListBoxUprawniony.ListCount - 1
        
            If ListBoxUprawniony.List(licznikLista, 0) = aktualneWartosci Then
            
                ListBoxUprawniony.Selected(licznikLista) = True
            
            End If
        
        Next licznikLista
    
    End If
    
    ListBoxUprawniony.SetFocus

End Sub

Private Sub CommandButtoAnuluj_Click()

    Worksheets("PZG_MaterialZasobu").Cells(g_uprawnionyRow, g_uprawnionyColumn) = g_uprawnionyOld

    frmUprawniony.Hide
    
    Worksheets("PZG_MaterialZasobu").Cells(g_uprawnionyRow, g_uprawnionyColumn).Select

End Sub

Private Sub CommandButtonWybierz_Click()

    Dim licznikLista As Integer
    Dim noweWartosci As String
    
    noweWartosci = ""
    
    For licznikLista = 0 To ListBoxUprawniony.ListCount - 1
        
        If ListBoxUprawniony.Selected(licznikLista) = True Then
        
            noweWartosci = noweWartosci & ListBoxUprawniony.List(licznikLista) & ";"
        
        End If
        
    Next licznikLista
    
    If noweWartosci <> "" Then noweWartosci = Left(noweWartosci, Len(noweWartosci) - 1)
    
    Worksheets("PZG_MaterialZasobu").Cells(g_uprawnionyRow, g_uprawnionyColumn) = noweWartosci

    g_uprawnionyOld = noweWartosci

    frmUprawniony.Hide
    
    Worksheets("PZG_MaterialZasobu").Cells(g_uprawnionyRow, g_uprawnionyColumn).Select

End Sub
