SELECT 
    o.mslink, o.nazwa, o.numer_gus, o.gmina_id, g.nazwa gmina 
FROM 
    ewid4.obreb o, ewid4.gmina g
WHERE 
    o.gmina_id = g.mslink
    AND data_arch IS NULL 
ORDER BY o.numer_gus