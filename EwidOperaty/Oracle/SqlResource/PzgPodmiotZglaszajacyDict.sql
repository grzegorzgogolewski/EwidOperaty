SELECT 
    p.osoba_id, 
    p.typ, 
    p.nazwa, 
    p.pesel, 
    p.regon, 
    p.rodz_pet, 
    (CASE WHEN p.data_k IS NULL THEN 'akt' ELSE 'arch' END) stan,
    m.nazwa miejscowosc, 
    u.nazwa_upper ulica, 
    p.numer nr_d, 
    p.numer2 nr_m 
FROM 
    petenci p, slo_ulice u, slo_miejsc m
WHERE 
    p.ulica_id = u.ulica_id(+)
    AND u.miejsc_id = m.miejsc_id (+)
    AND (osoba_id IN (SELECT tworca_id FROM osr_operat) OR osoba_id IN (SELECT wykon_id FROM osr_kerg))
ORDER BY p.osoba_id