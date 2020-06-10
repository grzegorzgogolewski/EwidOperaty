Option Explicit

Private Sub Worksheet_Change(ByVal Target As Range)
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgTworcaNazwa

    If Target.Column = 9 And Target.Row > 1 Then

        g_tworcaColumn = Target.Column
        g_tworcaRow = Target.Row
        
        If Not frmTworca.Visible Then
        
            Range(Cells(g_tworcaRow, g_tworcaColumn - 1), Cells(g_tworcaRow, g_tworcaColumn + 2)).Select
            
            With Selection.Interior
                .PatternColor = 65535
                .Pattern = xlGray16
            End With
        
            frmTworca.Show
            
            Range(Cells(g_tworcaRow, g_tworcaColumn - 1), Cells(g_tworcaRow, g_tworcaColumn + 2)).Interior.Pattern = xlNone
            
        End If

    End If

    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgCel

    If Target.Column = 32 And Target.Row > 1 Then

        g_celColumn = Target.Column
        g_celRow = Target.Row

        If Not frmCel.Visible Then
        
            Cells(g_celRow, g_celColumn).Select
            
            With Selection.Interior
                .PatternColor = 65535
                .Pattern = xlGray16
            End With
            
            frmCel.Show
            
            Cells(g_celRow, g_celColumn).Interior.Pattern = xlNone
            
        End If

    End If
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgCelArchiwalny
    
    If Target.Column = 33 And Target.Row > 1 Then

        g_celArchiwalnyColumn = Target.Column
        g_celArchiwalnyRow = Target.Row
        
        If Not frmCelArchiwalny.Visible Then
        
            Cells(g_celArchiwalnyRow, g_celArchiwalnyColumn).Select
            
            With Selection.Interior
                .PatternColor = 65535
                .Pattern = xlGray16
            End With
            
            frmCelArchiwalny.Show
            
            Cells(g_celArchiwalnyRow, g_celArchiwalnyColumn).Interior.Pattern = xlNone
            
        End If

    End If

    ' ---------------------------------------------------------------------------------------------
    ' Obsługa Uprawniony

    If Target.Column = 44 And Target.Row > 1 Then

        g_uprawnionyColumn = Target.Column
        g_uprawnionyRow = Target.Row
        
        If Not frmUprawniony.Visible Then
        
            Cells(g_uprawnionyRow, g_uprawnionyColumn).Select
            
            With Selection.Interior
                .PatternColor = 65535
                .Pattern = xlGray16
            End With
            
            frmUprawniony.Show
            
            Cells(g_uprawnionyRow, g_uprawnionyColumn).Interior.Pattern = xlNone
            
        End If

    End If

End Sub

Private Sub Worksheet_SelectionChange(ByVal Target As Range)
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgTworcaNazwa
    
    If Target.Column = 9 And Target.Row > 1 And Target.CountLarge = 1 Then
        
        g_tworcaOld = Target.Value

    End If
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgCel

    If Target.Column = 32 And Target.Row > 1 And Target.CountLarge = 1 Then
        
        g_celOld = Target.Value
    
    End If
    
    ' ---------------------------------------------------------------------------------------------
    ' Obsługa PzgCelArchiwalny
    
    If Target.Column = 33 And Target.Row > 1 And Target.CountLarge = 1 Then

        g_celArchiwalnyOld = Target.Value

    End If

    ' ---------------------------------------------------------------------------------------------
    ' Obsługa Uprawniony

    If Target.Column = 44 And Target.Row > 1 And Target.CountLarge = 1 Then

        g_uprawnionyOld = Target.Value

    End If
    
End Sub
