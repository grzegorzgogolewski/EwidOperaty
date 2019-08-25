SELECT ( select nazwa from obreb where mslink = obreb_id) nazwa, ( select numer_gus from obreb where mslink = obreb_id) teryt, COUNT(*) FROM (
SELECT 
    op.obreb_id
FROM
    kdok_wsk kd, osr_operat op, blob.kdok_pliki blk
WHERE
    kd.id_gr = op.idop
    AND kd.id_file = blk.id_file
    AND kd.wl = 'operat'
    AND kd.data_k IS NULL

UNION ALL
SELECT 
    op.obreb_id
FROM
    kdok_wsk kd, osr_operat_sklad sk, osr_operat op, blob.kdok_pliki blk
WHERE
    kd.id_gr = sk.mat_id
	AND kd.id_file = blk.id_file
    AND sk.idop = op.idop
    AND kd.wl = 'szkice'
    AND kd.data_k IS NULL
	)
	group by obreb_id