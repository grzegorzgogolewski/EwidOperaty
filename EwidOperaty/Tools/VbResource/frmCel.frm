VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} frmCel 
   Caption         =   "Cel"
   ClientHeight    =   6735
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   6000
   OleObjectBlob   =   "frmCel.frx":0000
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "frmCel"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub UserForm_Activate()

    ' ---------------------------------------------------------------------------------------------
    ' Dodanie waartosci do s³ownika
    ' ---------------------------------------------------------------------------------------------
    Dim licznik As Integer
    
    licznik = 2
    
    While PZG_CelPracy.Cells(licznik, "A") <> ""
        licznik = licznik + 1
    Wend
    
    Dim LiczbaWartosci As Integer
    
    LiczbaWartosci = licznik - 1
    
    ListBoxCel.Clear
    
    For licznik = 2 To LiczbaWartosci
        
        ListBoxCel.AddItem PZG_CelPracy.Cells(licznik, "E")
        
    Next licznik
    ' ---------------------------------------------------------------------------------------------
    
    Dim aktualneWartosci As String
    Dim licznikWartosci As Integer
    Dim licznikLista As Integer
    
    aktualneWartosci = Worksheets("PZG_MaterialZasobu").Cells(g_celRow, g_celColumn)
    
    If InStr(1, aktualneWartosci, ";") > 0 Then
    
        Dim listaWartosci() As String
        
        listaWartosci = Split(aktualneWartosci, ";")
        
        For licznikWartosci = 0 To UBound(listaWartosci) - LBound(listaWartosci)
            
            For licznikLista = 0 To ListBoxCel.ListCount - 1
            
                If ListBoxCel.List(licznikLista) = listaWartosci(licznikWartosci) Then
                
                    ListBoxCel.Selected(licznikLista) = True
                
                End If
            
            Next licznikLista
        
        Next licznikWartosci
        
    Else
    
        For licznikLista = 0 To ListBoxCel.ListCount - 1
        
            If ListBoxCel.List(licznikLista) = aktualneWartosci Then
            
                ListBoxCel.Selected(licznikLista) = True
            
            End If
        
        Next licznikLista
    
    End If
    
    ListBoxCel.SetFocus

End Sub

Private Sub CommandButtoAnuluj_Click()
   
    Worksheets("PZG_MaterialZasobu").Cells(g_celRow, g_celColumn) = g_celOld
    
    frmCel.Hide
    
    Worksheets("PZG_MaterialZasobu").Cells(g_celRow, g_celColumn).Select

End Sub

Private Sub CommandButtonWybierz_Click()

    Dim licznikLista As Integer
    Dim noweWartosci As String
    
    noweWartosci = ""
    
    For licznikLista = 0 To ListBoxCel.ListCount - 1
        
        If ListBoxCel.Selected(licznikLista) = True Then
        
            noweWartosci = noweWartosci & ListBoxCel.List(licznikLista) & ";"
        
        End If
        
    Next licznikLista
    
    If noweWartosci <> "" Then noweWartosci = Left(noweWartosci, Len(noweWartosci) - 1)
    
    Worksheets("PZG_MaterialZasobu").Cells(g_celRow, g_celColumn) = noweWartosci
    
    g_celOld = noweWartosci

    frmCel.Hide
    
    Worksheets("PZG_MaterialZasobu").Cells(g_celRow, g_celColumn).Select

End Sub
