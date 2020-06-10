Option Explicit

Global g_tworcaColumn As Integer
Global g_tworcaRow As Integer
Global g_tworcaOldId As String
Global g_tworcaOld As String
Global g_tworcaOldRegon As String
Global g_tworcaOldPesel As String

Global g_tworcaFound As Boolean
Global g_TworcaDic() As String

Global g_celColumn As Integer
Global g_celRow As Integer
Global g_celOld As String

Global g_celArchiwalnyColumn As Integer
Global g_celArchiwalnyRow As Integer
Global g_celArchiwalnyOld As String

Global g_uprawnionyColumn As Integer
Global g_uprawnionyRow As Integer
Global g_uprawnionyOld As String


Sub ImportForms()

    Application.VBE.ActiveVBProject.VBComponents.Import ActiveWorkbook.Path & "\" & "frmTworca.frm"
    Application.VBE.ActiveVBProject.VBComponents.Import ActiveWorkbook.Path & "\" & "frmCel.frm"
    Application.VBE.ActiveVBProject.VBComponents.Import ActiveWorkbook.Path & "\" & "frmCelArchiwalny.frm"
    Application.VBE.ActiveVBProject.VBComponents.Import ActiveWorkbook.Path & "\" & "frmUprawniony.frm"

End Sub

Function CheckDzialka(dzialka As String) As Integer

    dzialka = Replace(dzialka, "0", "")
    dzialka = Replace(dzialka, "1", "")
    dzialka = Replace(dzialka, "2", "")
    dzialka = Replace(dzialka, "3", "")
    dzialka = Replace(dzialka, "4", "")
    dzialka = Replace(dzialka, "5", "")
    dzialka = Replace(dzialka, "6", "")
    dzialka = Replace(dzialka, "7", "")
    dzialka = Replace(dzialka, "8", "")
    dzialka = Replace(dzialka, "9", "")

    dzialka = Replace(dzialka, "/", "")
    dzialka = Replace(dzialka, ";", "")
    
    dzialka = Replace(dzialka, "AR", "")
    dzialka = Replace(dzialka, "_", "")
    
    dzialka = Replace(dzialka, ".", "")

    CheckDzialka = Len(dzialka)

End Function

Sub Hyperlinks()

    Dim lineNumber As Integer

    lineNumber = 2

    While Cells(lineNumber, "A") <> ""

        Cells(lineNumber, "B").Select

        ActiveSheet.Hyperlinks.Add Anchor:=Selection, Address:=Cells(lineNumber, "B").Value, TextToDisplay:=Cells(lineNumber, "B").Value

        lineNumber = lineNumber + 1
    Wend
    
End Sub

Sub DzialkiPrzedPo()

    Dim licznikWierszy As Long
    
    Dim idMaterialu As String
    Dim operat As String
    
    Dim dzialkaPrzedSheet As Worksheet
    Dim dzialkaPrzed As String
    Dim dzialkaPrzedSplit As Variant
    Dim dzialkaPrzedLicznik As Long
    
    Dim dzialkaPoSheet As Worksheet
    Dim dzialkaPo As String
    Dim dzialkaPoSplit As Variant
    Dim dzialkaPoLicznik As Long
    
    Dim dzialka As Variant
    
    Set dzialkaPrzedSheet = ThisWorkbook.Sheets.Add(After:=ThisWorkbook.Sheets("PZG_MaterialZasobu"))
    dzialkaPrzedSheet.Name = "dzialkaPrzed"
    
    Set dzialkaPoSheet = ThisWorkbook.Sheets.Add(After:=ThisWorkbook.Sheets("dzialkaPrzed"))
    dzialkaPoSheet.Name = "dzialkaPo"
    
    dzialkaPrzedSheet.Cells(1, "A") = "PzgIdMaterialu"
    dzialkaPrzedSheet.Cells(1, "B") = "PzgOznMaterialuZasobu"
    dzialkaPrzedSheet.Cells(1, "C") = "DzialkaPrzed"
    
    dzialkaPoSheet.Cells(1, "A") = "PzgIdMaterialu"
    dzialkaPoSheet.Cells(1, "B") = "PzgOznMaterialuZasobu"
    dzialkaPoSheet.Cells(1, "C") = "DzialkaPrzed"

    licznikWierszy = 2
    
    dzialkaPrzedLicznik = 2
    dzialkaPoLicznik = 2

    While PZG_MaterialZasobu.Cells(licznikWierszy, "A") <> ""

        idMaterialu = PZG_MaterialZasobu.Cells(licznikWierszy, "B")
        operat = PZG_MaterialZasobu.Cells(licznikWierszy, "U")
        
        dzialkaPrzed = PZG_MaterialZasobu.Cells(licznikWierszy, "AH")
        dzialkaPrzedSplit = Split(dzialkaPrzed, ";")

        If dzialkaPrzed <> "" Then

            For Each dzialka In dzialkaPrzedSplit
                
                dzialkaPrzedSheet.Range(dzialkaPrzedSheet.Cells(dzialkaPrzedLicznik, "A"), dzialkaPrzedSheet.Cells(dzialkaPrzedLicznik, "C")).NumberFormat = "@"
                dzialkaPrzedSheet.Cells(dzialkaPrzedLicznik, "A") = idMaterialu
                dzialkaPrzedSheet.Cells(dzialkaPrzedLicznik, "B") = operat
                dzialkaPrzedSheet.Cells(dzialkaPrzedLicznik, "C") = dzialka

                dzialkaPrzedLicznik = dzialkaPrzedLicznik + 1
            Next

        End If
        
        dzialkaPo = PZG_MaterialZasobu.Cells(licznikWierszy, "AI")
        dzialkaPoSplit = Split(dzialkaPo, ";")

        If dzialkaPo <> "" Then

            For Each dzialka In dzialkaPoSplit
                
                dzialkaPoSheet.Range(dzialkaPoSheet.Cells(dzialkaPoLicznik, "A"), dzialkaPoSheet.Cells(dzialkaPoLicznik, "C")).NumberFormat = "@"
                dzialkaPoSheet.Cells(dzialkaPoLicznik, "A") = idMaterialu
                dzialkaPoSheet.Cells(dzialkaPoLicznik, "B") = operat
                dzialkaPoSheet.Cells(dzialkaPoLicznik, "C") = dzialka

                dzialkaPoLicznik = dzialkaPoLicznik + 1
            Next

        End If

        licznikWierszy = licznikWierszy + 1
    
    Wend
    
    dzialkaPrzedSheet.Cells.EntireColumn.AutoFit
    dzialkaPoSheet.Cells.EntireColumn.AutoFit

End Sub
