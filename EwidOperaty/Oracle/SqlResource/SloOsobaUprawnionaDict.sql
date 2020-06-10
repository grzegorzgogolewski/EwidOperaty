SELECT
    upr.osoba_id, upr.nazwisko, upr.imie, upr.nr_upr, m.nazwa miejscowosc, u.nazwa_upper ulica, upr.numer nr_d, upr.numer2 nr_m
FROM 
    uprawnieni upr, slo_ulice u, slo_miejsc m
WHERE
    upr.osoba_id IN (SELECT upraw_id from osr_kerg_upraw)
    AND upr.ulica_id = u.ulica_id(+)
    AND u.miejsc_id = m.miejsc_id (+)
ORDER BY upr.imie, upr.nazwisko