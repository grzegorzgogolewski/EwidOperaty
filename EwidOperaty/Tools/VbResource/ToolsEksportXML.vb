Sub EksportXML()

    Dim licznik As Long
    Dim nazwaPliku As String
    Dim nazwaFolderu As String
   
    nazwaFolderu = "c:\Temp\dobrcz\XLS\koronowo\" & Left(ActiveWorkbook.Name, 4)
    
    licznik = 2
    
    While Cells(licznik, "B") <> "" Or Cells(licznik, "T") <> ""
    
        If Cells(licznik, "B") <> "" Then
            nazwaPliku = Cells(licznik, "B") & ".xml"
        Else
            nazwaPliku = Replace(Cells(licznik, "T"), "/", "_") & ".xml"
        End If
        
        Dim posS As Integer
        Dim posK As Integer
        
        Dim pierwszyCzlon, drugiCzlon, trzeciCzlon, czwartyCzlon As String
        
        If Cells(licznik, "B") <> "" Then
            posS = 1
            posK = InStr(posS, Cells(licznik, "B"), ".")
            pierwszyCzlon = Mid(Cells(licznik, "B"), posS, posK - posS)
            
            posS = posK + 1
            posK = InStr(posS, Cells(licznik, "B"), ".")
            drugiCzlon = Mid(Cells(licznik, "B"), posS, posK - posS)
            
            posS = posK + 1
            posK = InStr(posS, Cells(licznik, "B"), ".")
            trzeciCzlon = Mid(Cells(licznik, "B"), posS, posK - posS)
            
            posS = posK + 1
            posK = Len(Cells(licznik, "B"))
            czwartyCzlon = Mid(Cells(licznik, "B"), posS, posK - posS)
        Else
            pierwszyCzlon = ""
            drugiCzlon = ""
            trzeciCzlon = ""
            czwartyCzlon = ""
        End If
        
        Dim oStream
        Set oStream = CreateObject("ADODB.Stream")
        
        With oStream
            .Open
            .Charset = "utf-8"
            
            .WriteText "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf
            .WriteText "<schema xmlns=""http://www.w3.org/2001/XMLSchema""" & vbCrLf
            .WriteText "targetNamespace=""http://www.w3.org/2001/XMLSchema"" elementFormDefault=""qualified""" & vbCrLf
            .WriteText "attributeFormDefault=""unqualified"" version=""1.0"">" & vbCrLf
            .WriteText "<!-- ============================================================-->" & vbCrLf
            
            .WriteText "<PZG_MaterialZasobu>" & vbCrLf
            
            .WriteText vbTab & "<pzg_IdMaterialu>" & vbCrLf
            .WriteText vbTab & vbTab & "<pierwszyCzlon>" & pierwszyCzlon & "</pierwszyCzlon>" & vbCrLf
            .WriteText vbTab & vbTab & "<drugiCzlon>" & drugiCzlon & "</drugiCzlon>" & vbCrLf
            .WriteText vbTab & vbTab & "<trzeciCzlon>" & trzeciCzlon & "</trzeciCzlon>" & vbCrLf
            .WriteText vbTab & vbTab & "<czwartyCzlon>" & czwartyCzlon & "</czwartyCzlon>" & vbCrLf
            .WriteText vbTab & "</pzg_IdMaterialu>" & vbCrLf
            
            .WriteText vbTab & "<pzg_dataPrzyjecia>" & Format(Cells(licznik, "C"), "yyyy-mm-dd") & "</pzg_dataPrzyjecia>" & vbCrLf
            .WriteText vbTab & "<pzg_dataWplywu>" & Format(Cells(licznik, "D"), "yyyy-mm-dd") & "</pzg_dataWplywu>" & vbCrLf
            .WriteText vbTab & "<pzg_nazwa>" & Cells(licznik, "E") & "</pzg_nazwa>" & vbCrLf
            .WriteText vbTab & "<pzg_polozenieObszaru>" & Cells(licznik, "B") & ".wkt" & "</pzg_polozenieObszaru>" & vbCrLf
            .WriteText vbTab & "<obreb>" & Cells(licznik, "G") & "</obreb>" & vbCrLf
            
            .WriteText vbTab & "<pzg_tworca>" & vbCrLf
            .WriteText vbTab & vbTab & "<nazwa>" & Cells(licznik, "H") & "</nazwa>" & vbCrLf
            .WriteText vbTab & vbTab & "<REGON>" & Cells(licznik, "I") & "</REGON>" & vbCrLf
            .WriteText vbTab & vbTab & "<PESEL>" & Cells(licznik, "J") & "</PESEL>" & vbCrLf
            .WriteText vbTab & "</pzg_tworca>" & vbCrLf
            
            .WriteText vbTab & "<pzg_sposobPozyskania>" & Cells(licznik, "K") & "</pzg_sposobPozyskania>" & vbCrLf
            .WriteText vbTab & "<pzg_postacMaterialu>" & Cells(licznik, "L") & "</pzg_postacMaterialu>" & vbCrLf
            .WriteText vbTab & "<pzg_rodzNosnika>" & Cells(licznik, "M") & "</pzg_rodzNosnika>" & vbCrLf
            .WriteText vbTab & "<pzg_dostep>" & Cells(licznik, "N") & "</pzg_dostep>" & vbCrLf
            .WriteText vbTab & "<pzg_przyczynyOgraniczen>" & Cells(licznik, "O") & "</pzg_przyczynyOgraniczen>" & vbCrLf
            .WriteText vbTab & "<pzg_typMaterialu>" & Cells(licznik, "P") & "</pzg_typMaterialu>" & vbCrLf
            .WriteText vbTab & "<pzg_katArchiwalna>" & Cells(licznik, "Q") & "</pzg_katArchiwalna>" & vbCrLf
            .WriteText vbTab & "<pzg_jezyk>" & Cells(licznik, "R") & "</pzg_jezyk>" & vbCrLf
            .WriteText vbTab & "<pzg_opis>" & Cells(licznik, "S") & "</pzg_opis>" & vbCrLf
            
            .WriteText vbTab & "<pzg_oznMaterialuZasobu>" & Cells(licznik, "T") & "</pzg_oznMaterialuZasobu>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuTyp>" & Cells(licznik, "U") & "</oznMaterialuZasobuTyp>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuJedn>" & Cells(licznik, "V") & "</oznMaterialuZasobuJedn>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuNr>" & Cells(licznik, "W") & "</oznMaterialuZasobuNr>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuRok>" & Cells(licznik, "X") & "</oznMaterialuZasobuRok>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuTom>" & Cells(licznik, "Y") & "</oznMaterialuZasobuTom>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuSepJednNr>" & Cells(licznik, "Z") & "</oznMaterialuZasobuSepJednNr>" & vbCrLf
            .WriteText vbTab & "<oznMaterialuZasobuSepNrRok>" & Cells(licznik, "AA") & "</oznMaterialuZasobuSepNrRok>" & vbCrLf
            
            .WriteText vbTab & "<pzg_dokumentWyl>" & Cells(licznik, "AB") & "</pzg_dokumentWyl>" & vbCrLf
            .WriteText vbTab & "<pzg_dataWyl>" & Cells(licznik, "AC") & "</pzg_dataWyl>" & vbCrLf
            .WriteText vbTab & "<pzg_dataArchLubBrak>" & Cells(licznik, "AD") & "</pzg_dataArchLubBrak>" & vbCrLf
            
            
            Dim Result() As String
            Dim i As Integer
            
            Result = Split(Cells(licznik, "AE"), ";")
            
            If UBound(Result()) < 0 Then
                .WriteText vbTab & "<pzg_cel></pzg_cel>" & vbCrLf
            Else
                For i = LBound(Result()) To UBound(Result())
                    .WriteText vbTab & "<pzg_cel>" & Result(i) & "</pzg_cel>" & vbCrLf
                Next i
            End If
            
            Result = Split(Cells(licznik, "AF"), ";")
            
            If UBound(Result()) < 0 Then
                .WriteText vbTab & "<celArchiwalny></celArchiwalny>" & vbCrLf
            Else
                For i = LBound(Result()) To UBound(Result())
                    .WriteText vbTab & "<celArchiwalny>" & Result(i) & "</celArchiwalny>" & vbCrLf
                Next i
            End If
            
            Dim obreb
            
            obreb = Cells(licznik, "G")
            
            Result = Split(Cells(licznik, "AG"), ";")
            
            If UBound(Result()) < 0 Then
                .WriteText vbTab & "<dzialkaPrzed></dzialkaPrzed>" & vbCrLf
            Else
                For i = LBound(Result()) To UBound(Result())
                    .WriteText vbTab & "<dzialkaPrzed>" & obreb & "." & Result(i) & "</dzialkaPrzed>" & vbCrLf
                Next i
            End If
            
            Result = Split(Cells(licznik, "AH"), ";")
            
            If UBound(Result()) < 0 Then
                .WriteText vbTab & "<dzialkaPo></dzialkaPo>" & vbCrLf
            Else
                For i = LBound(Result()) To UBound(Result())
                    .WriteText vbTab & "<dzialkaPo>" & obreb & "." & Result(i) & "</dzialkaPo>" & vbCrLf
                Next i
            End If
            
            .WriteText vbTab & "<opis2>" & Cells(licznik, "AI") & "</opis2>" & vbCrLf
            
            .WriteText "</PZG_MaterialZasobu>" & vbCrLf
            
            If Cells(licznik, "AK") <> "" Then
            
                .WriteText "<PZG_Zgloszenie>" & vbCrLf
                
                .WriteText vbTab & "<pzg_idZgloszenia>" & Cells(licznik, "AK") & "</pzg_idZgloszenia>" & vbCrLf
                .WriteText vbTab & "<idZgloszeniaJedn></idZgloszeniaJedn>" & vbCrLf
                .WriteText vbTab & "<idZgloszeniaNr></idZgloszeniaNr>" & vbCrLf
                .WriteText vbTab & "<idZgloszeniaRok></idZgloszeniaRok>" & vbCrLf
                .WriteText vbTab & "<idZgloszeniaEtap></idZgloszeniaEtap>" & vbCrLf
                .WriteText vbTab & "<idZgloszeniaSepJednNr></idZgloszeniaSepJednNr>" & vbCrLf
                .WriteText vbTab & "<idZgloszeniaSepNrRok></idZgloszeniaSepNrRok>" & vbCrLf
                
                .WriteText vbTab & "<pzg_dataZgloszenia>" & Format(Cells(licznik, "AL"), "yyyy-mm-dd") & "</pzg_dataZgloszenia>" & vbCrLf
                .WriteText vbTab & "<pzg_polozenieObszaru></pzg_polozenieObszaru>" & vbCrLf
                .WriteText vbTab & "<obreb>" & Cells(licznik, "AM") & "</obreb>" & vbCrLf
                
                .WriteText vbTab & "<pzg_podmiotZglaszajacy>" & vbCrLf
                .WriteText vbTab & vbTab & "<nazwa>" & Cells(licznik, "H") & "</nazwa>" & vbCrLf
                .WriteText vbTab & vbTab & "<REGON>" & Cells(licznik, "I") & "</REGON>" & vbCrLf
                .WriteText vbTab & vbTab & "<PESEL>" & Cells(licznik, "J") & "</PESEL>" & vbCrLf
                .WriteText vbTab & "</pzg_podmiotZglaszajacy>" & vbCrLf
                
                Dim Uprawniony() As String
                
                Result = Split(Cells(licznik, "AO"), ";")
                
                If UBound(Result()) < 0 Then
                    .WriteText vbTab & "<osobaUprawniona>" & vbCrLf
                    .WriteText vbTab & vbTab & "<imie></imie>" & vbCrLf
                    .WriteText vbTab & vbTab & "<nazwisko></nazwisko>" & vbCrLf
                    .WriteText vbTab & vbTab & "<numer_uprawnien></numer_uprawnien>" & vbCrLf
                    .WriteText vbTab & "</osobaUprawniona>" & vbCrLf
                Else
                    
                    For i = LBound(Result()) To UBound(Result())
                        
                        .WriteText vbTab & "<osobaUprawniona>" & vbCrLf
                        
                        Uprawniony = Split(Result(i), "_")
                        
                        If UBound(Uprawniony()) = 2 Then
                            .WriteText vbTab & vbTab & "<imie>" & Uprawniony(0) & "</imie>" & vbCrLf
                            .WriteText vbTab & vbTab & "<nazwisko>" & Uprawniony(1) & "</nazwisko>" & vbCrLf
                            .WriteText vbTab & vbTab & "<numer_uprawnien>" & Uprawniony(2) & "</numer_uprawnien>" & vbCrLf
                        End If
                        
                        .WriteText vbTab & "</osobaUprawniona>" & vbCrLf
                    
                    Next i
                
                End If
                
                Result = Split(Cells(licznik, "AE"), ";")
            
                If UBound(Result()) < 0 Then
                    .WriteText vbTab & "<pzg_cel></pzg_cel>" & vbCrLf
                Else
                    For i = LBound(Result()) To UBound(Result())
                        .WriteText vbTab & "<pzg_cel>" & Result(i) & "</pzg_cel>" & vbCrLf
                    Next i
                End If
                
                Result = Split(Cells(licznik, "AF"), ";")
                
                If UBound(Result()) < 0 Then
                    .WriteText vbTab & "<celArchiwalny></celArchiwalny>" & vbCrLf
                Else
                    For i = LBound(Result()) To UBound(Result())
                        .WriteText vbTab & "<celArchiwalny>" & Result(i) & "</celArchiwalny>" & vbCrLf
                    Next i
                End If
                
                .WriteText vbTab & "<pzg_rodzaj>" & Cells(licznik, "AN") & "</pzg_rodzaj>" & vbCrLf
                
                .WriteText "</PZG_Zgloszenie>" & vbCrLf
            
            End If
            
            .WriteText "</schema>" & vbCrLf
            
            .SaveToFile nazwaFolderu & "\" & nazwaPliku, 2
            
        End With
        
        Set oStream = Nothing
            
        licznik = licznik + 1
    
    Wend

End Sub