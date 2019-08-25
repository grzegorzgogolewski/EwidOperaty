SELECT 
    p.osoba_id, 
	p.typ, 
	p.nazwa, 
	p.skrot,
	p.pesel, 
	p.regon, 
	p.rodz_pet, 
	m.nazwa miejscowosc, 
	k.kod,
	u.nazwa_upper ulica, 
	p.numer nr_d, 
	p.numer2 nr_m, 
	p.petent_id, 
	p.data_k,
	(SELECT count(*) FROM osr_kerg o WHERE o.wykon_id = p.osoba_id) ilosc 
FROM 
    petenci p, slo_ulice u, slo_miejsc m, kod_poczt k
WHERE 
    p.ulica_id = u.ulica_id(+)
    AND u.miejsc_id = m.miejsc_id (+)
    AND p.kod_id = k.kod_id (+)
    --AND osoba_id IN (SELECT wykon_id FROM osr_kerg)
ORDER BY p.osoba_id