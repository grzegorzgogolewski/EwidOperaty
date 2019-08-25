Option Explicit

Global g_tworcaColumn As Integer
Global g_tworcaRow As Integer
Global g_tworcaOld As String
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


Sub WlaczZdarzenia()
    Application.EnableEvents = True
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
    
    CheckDzialka = Len(dzialka)

End Function
