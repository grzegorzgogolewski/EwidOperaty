SELECT 
    kd.id_dok,
    kd.wl,
    kd.id_gr idop,
    op.obreb_id,
    kd.id_file,
    kd.path,
    kd.id_rodz_dok,
    kd.opis,
    CASE SDO_EQUAL(kd.geom, op.geom) WHEN 'FALSE' THEN SDO_UTIL.TO_WKTGEOMETRY(kd.geom) ELSE NULL END geom,
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
	--AND UPPER(kd.path) NOT LIKE '%.JPG'
	--AND UPPER(kd.path) NOT LIKE '%.PDF'
    AND NVL(op.obreb_id, 0) = :obreb_id
UNION ALL
SELECT 
    kd.id_dok,
    kd.wl,
    OP.idop idop,
    op.obreb_id,
    kd.id_file,
    kd.path,
    kd.id_rodz_dok,
    kd.opis,
    CASE SDO_EQUAL(kd.geom, op.geom) WHEN 'FALSE' THEN SDO_UTIL.TO_WKTGEOMETRY(kd.geom) ELSE NULL END geom,
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
	--AND UPPER(kd.path) NOT LIKE '%.JPG'
	--AND UPPER(kd.path) NOT LIKE '%.PDF'
	AND NVL(op.obreb_id, 0) = :obreb_id
