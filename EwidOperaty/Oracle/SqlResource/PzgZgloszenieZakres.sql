SELECT
    zg.kerg_id ,
    zg.sygnatura pzg_idZgloszenia,
    zg. data_zgloszenia pzg_dataZgloszenia,
    zg.j_segr idZgloszeniaJedn,
    zg.nr_kol idZgloszeniaNr,
    zg.rok_zgl idZgloszeniaRok,
    zg.etap idZgloszeniaEtap,
    zg.sep_js_nr idZgloszeniaSepJednNr,
    zg.sep_nr_rok idZgloszeniaSepNrRok,
    --NULL pzg_polozenieObszaru,
    zg.geom.get_wkt() pzg_polozenieObszaru,
    zg.obreb_id obreb,
    zg.wykon_id pzg_podmiotZglaszajacy,
    (SELECT cel_id FROM (SELECT oc.kerg_id, wm_concat(oc.cel_id) cel_id FROM osr_kerg_cel oc GROUP BY oc.kerg_id) cele WHERE cele.kerg_id = zg.kerg_id) pzg_cel,
    zg.rodz_id pzg_rodzaj,
    (SELECT upraw_id FROM (SELECT ou.kerg_id, wm_concat(ou.upraw_id) upraw_id FROM osr_kerg_upraw ou GROUP BY ou.kerg_id) upr WHERE upr.kerg_id = zg.kerg_id) osobaUprawniona
FROM
    ewid4.osr_kerg zg
WHERE 
    zg.kerg_id = :kerg_id
ORDER BY kerg_id