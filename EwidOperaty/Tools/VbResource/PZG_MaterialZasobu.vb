Option Explicit

Private Sub Worksheet_Change(ByVal Target As Range)

    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgTworcaNazwa

    If Target.Column = 8 And Target.Row > 1 Then

        g_tworcaColumn = Target.Column
        g_tworcaRow = Target.Row

        frmTworca.Show

    End If
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgCel
    
    If Target.Column = 31 And Target.Row > 1 Then

        g_celColumn = Target.Column
        g_celRow = Target.Row

        frmCel.Show

    End If
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgCelArchiwalny
    
    If Target.Column = 32 And Target.Row > 1 Then

        g_celArchiwalnyColumn = Target.Column
        g_celArchiwalnyRow = Target.Row

        frmCelArchiwalny.Show

    End If

    ' ---------------------------------------------------------------------------------------------
    ' Obsługa Uprawniony

    If Target.Column = 43 And Target.Row > 1 Then

        g_uprawnionyColumn = Target.Column
        g_uprawnionyRow = Target.Row

        frmUprawniony.Show

    End If

End Sub

Private Sub Worksheet_SelectionChange(ByVal Target As Range)

    If Not Intersect(Target, Range("H:H")) Is Nothing Then

        If Target.CountLarge = 1 And Target.Row > 1 Then

            g_tworcaOld = Target

        End If

    End If
    
    If Not Intersect(Target, Range("AE:AE")) Is Nothing Then

        If Target.CountLarge = 1 And Target.Row > 1 Then

            g_celOld = Target

        End If

    End If

    If Not Intersect(Target, Range("AF:AF")) Is Nothing Then

        If Target.CountLarge = 1 And Target.Row > 1 Then

            g_celArchiwalnyOld = Target
        
        End If

    End If

    If Not Intersect(Target, Range("AQ:AQ")) Is Nothing Then

        If Target.CountLarge = 1 And Target.Row > 1 Then

            g_uprawnionyOld = Target

        End If

    End If



End Sub
