SELECT 
    kd.id_dok,
    kd.wl,
    kd.id_gr idop,
    op.idmaterialu,
    op.obreb_id,
    kd.id_file,
    kd.path,
    kd.id_rodz_dok,
    kd.opis,
    SDO_UTIL.TO_WKTGEOMETRY(kd.geom) geom,
    blk.typ_pliku,
    blk.data,
    kd.data_d
FROM
    kdok_wsk kd, osr_operat op, blob.kdok_pliki blk
WHERE
    kd.id_gr = op.idop
    AND kd.id_file = blk.id_file
    AND kd.wl = 'operat'
    AND kd.data_k IS NULL
    AND NVL(op.obreb_id, 0) = :obreb_id
	--and op.idop IN (select idop  from gg_operaty)
    --AND op.idop IN (select idop from osr_operat WHERE rok_zgl >= 2019 OR IDMAT_ROK >= 2019)
	--and kd.data_d >= TO_DATE('26-09-2019 00:00:00','DD-MM-YYYY hh24:mi:ss')
UNION ALL
SELECT 
    kd.id_dok,
    kd.wl,
    OP.idop idop,
    op.idmaterialu,
    op.obreb_id,
    kd.id_file,
    kd.path,
    kd.id_rodz_dok,
    kd.opis,
    SDO_UTIL.TO_WKTGEOMETRY(kd.geom) geom,
	blk.typ_pliku,
    blk.data,
    kd.data_d
FROM
    kdok_wsk kd, osr_operat_sklad sk, osr_operat op, blob.kdok_pliki blk
WHERE
    kd.id_gr = sk.mat_id
	AND kd.id_file = blk.id_file
    AND sk.idop = op.idop
    AND kd.wl = 'szkice'
    AND kd.data_k IS NULL
	AND NVL(op.obreb_id, 0) = :obreb_id
	--and op.idop IN (select idop  from gg_operaty)
    --AND op.idop IN (select idop from osr_operat WHERE rok_zgl >= 2019 OR IDMAT_ROK >= 2019)
	--and kd.data_d >= TO_DATE('26-09-2019 00:00:00','DD-MM-YYYY hh24:mi:ss')
