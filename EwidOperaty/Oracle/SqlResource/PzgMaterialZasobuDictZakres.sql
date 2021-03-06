SELECT 
    op.idop, 
    op.idmaterialu pzg_IdMaterialu, 
    op.data_przyjecia pzg_dataPrzyjecia, 
    op.nazmat_id pzg_nazwa,
    --NULL pzg_polozenieObszaru,
    op.geom.get_wkt() pzg_polozenieObszaru,
    op.obreb_id obreb,
    op.tworca_id pzg_tworca_id,
    op.spospoz_id pzg_sposobPozyskania,
    op.postac_materialu pzg_postacMaterialu,
    op.rodznos_id pzg_rodzNosnika,
    op.dostep pzg_dostep,
    op.podst_praw_ogr_dostep pzg_przyczynyOgraniczen,
    op.typ_mater_id pzg_typMaterialu,
    op.kat_archiw pzg_katArchiwalna,
    op.jezyk pzg_jezyk,
    NULL pzg_opis,
    op.sygnatura pzg_oznMaterialuZasobu,
    op.operattyp_id oznMaterialuZasobuTyp,
    op.j_segr oznMaterialuZasobuJedn,
    op.nr_kol oznMaterialuZasobuNr,
    op.rok_zgl oznMaterialuZasobuRok,
    op.tom oznMaterialuZasobuTom,
	op.sep_js_nr oznMaterialuZasobuSepJednNr,
	op.sep_nr_rok oznMaterialuZasobuSepNrRok,
	op.dok_wyl pzg_dokumentWyl,
	null pzg_dataWyl,
	op.data_archlubbrak pzg_dataArchLubBrak,
    (SELECT cel_id FROM (SELECT oc.idop, listagg(oc.cel_id, ',') within group (order by oc.idop) as cel_id FROM osr_operat_cel oc GROUP BY oc.idop) cele WHERE cele.idop = op.idop) pzg_cel,
	(SELECT dlk FROM (SELECT idop, rtrim(xmlagg(XMLELEMENT(e, dlk, ',').EXTRACT('//text()')).GetClobVal(),',') as dlk FROM (select distinct od.idop, SUBSTR(EW_DLK_IDG5(d.mslink), 6) dlk from osr_operat_dlk od join kdzialka d using(dzialka_id) where od.stan_po = 0 union select distinct od.idop, SUBSTR(EW_DLK_IDG5(d.mslink), 6) dlk from osr_operat_dlk od join kdzialka_a d using(dzialka_id) where od.stan_po = 0) d GROUP BY d.idop) dz WHERE dz.idop = op.idop) dzialkaPrzed,
    (SELECT dlk FROM (SELECT idop, rtrim(xmlagg(XMLELEMENT(e, dlk, ',').EXTRACT('//text()')).GetClobVal(),',') as dlk FROM (select distinct od.idop, SUBSTR(EW_DLK_IDG5(d.mslink), 6) dlk from osr_operat_dlk od join kdzialka d using(dzialka_id) where od.stan_po = 1 union select distinct od.idop, SUBSTR(EW_DLK_IDG5(d.mslink), 6) dlk from osr_operat_dlk od join kdzialka_a d using(dzialka_id) where od.stan_po = 1) d GROUP BY d.idop) dz WHERE dz.idop = op.idop) dzialkaPo,
--	(SELECT dlk FROM (SELECT idop, rtrim(xmlagg(XMLELEMENT(e, dlk, ',').EXTRACT('//text()')).GetClobVal(),',') as dlk FROM (select distinct od.idop, EW_DLK_IDG5(d.mslink) dlk from osr_operat_dlk od join kdzialka d using(dzialka_id) where od.stan_po = 0 union select distinct od.idop, EW_DLK_IDG5(d.mslink) dlk from osr_operat_dlk od join kdzialka_a d using(dzialka_id) where od.stan_po = 0) d GROUP BY d.idop) dz WHERE dz.idop = op.idop) dzialkaPrzed,
--  (SELECT dlk FROM (SELECT idop, rtrim(xmlagg(XMLELEMENT(e, dlk, ',').EXTRACT('//text()')).GetClobVal(),',') as dlk FROM (select distinct od.idop, EW_DLK_IDG5(d.mslink) dlk from osr_operat_dlk od join kdzialka d using(dzialka_id) where od.stan_po = 1 union select distinct od.idop, EW_DLK_IDG5(d.mslink) dlk from osr_operat_dlk od join kdzialka_a d using(dzialka_id) where od.stan_po = 1) d GROUP BY d.idop) dz WHERE dz.idop = op.idop) dzialkaPo,
    op.uwagi opis2,
    op.data_wplywu dataWplywu,
    (SELECT count(*) FROM kdok_wsk kd WHERE kd.id_gr = op.idop AND wl = 'operat' and data_k IS NULL) liczba_skanow,
    (SELECT count(*) FROM kdok_wsk kd WHERE wl = 'szkice' AND kd.id_gr IN (SELECT mat_id FROM osr_operat_sklad os WHERE os.idop = op.idop) and data_k IS NULL) liczba_doks,
	op.kerg_id
FROM 
    ewid4.osr_operat op 
WHERE 
    NVL(op.obreb_id, 0) = :obreb_id
    --AND op.idop IN (select idop from gg_operaty)
ORDER BY idop