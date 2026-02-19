-- =============================================
-- Datos de prueba para Gestión RA
-- Se ejecuta automáticamente al iniciar la app
-- =============================================

-- Solo insertar si la tabla está vacía (evita duplicados en reinicios)
INSERT INTO materias (nombre, curso, descripcion)
SELECT * FROM (SELECT 'Desarrollo Web en Entorno Cliente' AS nombre, '2º DAM' AS curso, 'Módulo profesional de desarrollo frontend con JavaScript' AS descripcion) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM materias WHERE nombre = 'Desarrollo Web en Entorno Cliente') LIMIT 1;

INSERT INTO materias (nombre, curso, descripcion)
SELECT * FROM (SELECT 'Desarrollo Web en Entorno Servidor' AS nombre, '2º DAM' AS curso, 'Módulo profesional de desarrollo backend con Java/PHP' AS descripcion) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM materias WHERE nombre = 'Desarrollo Web en Entorno Servidor') LIMIT 1;

INSERT INTO materias (nombre, curso, descripcion)
SELECT * FROM (SELECT 'Diseño de Interfaces Web' AS nombre, '2º DAM' AS curso, 'Módulo profesional de diseño y maquetación web' AS descripcion) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM materias WHERE nombre = 'Diseño de Interfaces Web') LIMIT 1;

-- RA para DWEC (materia 1)
INSERT INTO resultados_aprendizaje (codigo, descripcion, materia_id)
SELECT * FROM (SELECT 'RA1' AS codigo, 'Selecciona las arquitecturas y tecnologías de programación sobre clientes web' AS descripcion, 1 AS materia_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM resultados_aprendizaje WHERE codigo = 'RA1' AND materia_id = 1) LIMIT 1;

INSERT INTO resultados_aprendizaje (codigo, descripcion, materia_id)
SELECT * FROM (SELECT 'RA2' AS codigo, 'Escribe sentencias simples, aplicando la sintaxis del lenguaje' AS descripcion, 1 AS materia_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM resultados_aprendizaje WHERE codigo = 'RA2' AND materia_id = 1) LIMIT 1;

-- RA para DWES (materia 2)
INSERT INTO resultados_aprendizaje (codigo, descripcion, materia_id)
SELECT * FROM (SELECT 'RA1' AS codigo, 'Selecciona las arquitecturas y tecnologías de programación web en entorno servidor' AS descripcion, 2 AS materia_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM resultados_aprendizaje WHERE codigo = 'RA1' AND materia_id = 2) LIMIT 1;

INSERT INTO resultados_aprendizaje (codigo, descripcion, materia_id)
SELECT * FROM (SELECT 'RA2' AS codigo, 'Escribe sentencias ejecutables por un servidor web' AS descripcion, 2 AS materia_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM resultados_aprendizaje WHERE codigo = 'RA2' AND materia_id = 2) LIMIT 1;

-- RA para DIW (materia 3)
INSERT INTO resultados_aprendizaje (codigo, descripcion, materia_id)
SELECT * FROM (SELECT 'RA1' AS codigo, 'Planifica la creación de una interfaz web valorando y aplicando especificaciones de diseño' AS descripcion, 3 AS materia_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM resultados_aprendizaje WHERE codigo = 'RA1' AND materia_id = 3) LIMIT 1;

INSERT INTO resultados_aprendizaje (codigo, descripcion, materia_id)
SELECT * FROM (SELECT 'RA2' AS codigo, 'Crea interfaces web homogéneos definiendo y aplicando estilos' AS descripcion, 3 AS materia_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM resultados_aprendizaje WHERE codigo = 'RA2' AND materia_id = 3) LIMIT 1;

-- Criterios para DWEC RA1 (ra_id = 1)
INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE1.1' AS codigo, 'Se han caracterizado las tecnologías de programación de clientes web' AS descripcion, 1 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE1.1' AND ra_id = 1) LIMIT 1;

INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE1.2' AS codigo, 'Se han identificado los mecanismos de ejecución de código en navegadores' AS descripcion, 1 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE1.2' AND ra_id = 1) LIMIT 1;

-- Criterios para DWEC RA2 (ra_id = 2)
INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE2.1' AS codigo, 'Se ha identificado la sintaxis básica del lenguaje' AS descripcion, 2 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE2.1' AND ra_id = 2) LIMIT 1;

INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE2.2' AS codigo, 'Se han escrito sentencias de asignación de datos' AS descripcion, 2 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE2.2' AND ra_id = 2) LIMIT 1;

-- Criterios para DWES RA1 (ra_id = 3)
INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE1.1' AS codigo, 'Se han identificado las tecnologías de desarrollo web en entorno servidor' AS descripcion, 3 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE1.1' AND ra_id = 3) LIMIT 1;

INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE1.2' AS codigo, 'Se han reconocido las ventajas de la generación dinámica de páginas' AS descripcion, 3 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE1.2' AND ra_id = 3) LIMIT 1;

-- Criterios para DWES RA2 (ra_id = 4)
INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE2.1' AS codigo, 'Se han reconocido los mecanismos de generación de páginas web' AS descripcion, 4 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE2.1' AND ra_id = 4) LIMIT 1;

INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE2.2' AS codigo, 'Se han identificado las principales tecnologías asociadas' AS descripcion, 4 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE2.2' AND ra_id = 4) LIMIT 1;

-- Criterios para DIW RA1 (ra_id = 5)
INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE1.1' AS codigo, 'Se han reconocido las pautas de accesibilidad al contenido' AS descripcion, 5 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE1.1' AND ra_id = 5) LIMIT 1;

INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE1.2' AS codigo, 'Se han analizado las interacciones del usuario y la respuesta de la aplicación' AS descripcion, 5 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE1.2' AND ra_id = 5) LIMIT 1;

-- Criterios para DIW RA2 (ra_id = 6)
INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE2.1' AS codigo, 'Se han identificado las propiedades de estilos CSS' AS descripcion, 6 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE2.1' AND ra_id = 6) LIMIT 1;

INSERT INTO criterios (codigo, descripcion, ra_id)
SELECT * FROM (SELECT 'CE2.2' AS codigo, 'Se han aplicado estilos utilizando hojas de estilo en cascada' AS descripcion, 6 AS ra_id) AS tmp
WHERE NOT EXISTS (SELECT 1 FROM criterios WHERE codigo = 'CE2.2' AND ra_id = 6) LIMIT 1;
