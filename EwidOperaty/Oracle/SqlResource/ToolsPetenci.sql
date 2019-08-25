  SELECT
    upr.osoba_id, 
	upr.nazwisko, 
	upr.imie, 
	upr.nr_upr, 
	m.nazwa miejscowosc, 
	k.kod,
	u.nazwa_upper ulica, 
	upr.numer nr_d, 
	upr.numer2 nr_m, 
	upr.nip,
	upr.pesel,
	upr.telefon,
	upr.email,
	upr.data_k
FROM 
    uprawnieni upr, slo_ulice u, slo_miejsc m, kod_poczt k
WHERE
    upr.ulica_id = u.ulica_id(+)
    AND u.miejsc_id = m.miejsc_id (+)
    AND upr.kod_id = k.kod_id
    AND upr.osoba_id in (SELECT upraw_id FROM osr_kerg_upraw)
ORDER BY upr.osoba_id