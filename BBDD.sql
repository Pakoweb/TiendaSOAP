-- =============================================
-- Script de Creación de Base de Datos TiendaDB
-- Proyecto: TiendaSOAP - Servicio Web SOAP
-- =============================================

-- Crear la base de datos si no existe
CREATE DATABASE IF NOT EXISTS tiendasoap;
USE tiendasoap;

-- =============================================
-- Tabla: Usuarios
-- =============================================
DROP TABLE IF EXISTS DetallePedidos;
DROP TABLE IF EXISTS Pedidos;
DROP TABLE IF EXISTS Productos;
DROP TABLE IF EXISTS Categorias;
DROP TABLE IF EXISTS Usuarios;

CREATE TABLE Usuarios (
    UsuarioID INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contraseña VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_nombreusuario (NombreUsuario),
    INDEX idx_email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =============================================
-- Tabla: Categorias
-- =============================================
CREATE TABLE Categorias (
    CategoriaID INT AUTO_INCREMENT PRIMARY KEY,
    NombreCategoria VARCHAR(100) NOT NULL UNIQUE,
    INDEX idx_nombrecategoria (NombreCategoria)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =============================================
-- Tabla: Productos
-- =============================================
CREATE TABLE Productos (
    ProductoID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(200) NOT NULL,
    Descripcion TEXT,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    CategoriaID INT NOT NULL,
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID) ON DELETE RESTRICT,
    INDEX idx_nombre (Nombre),
    INDEX idx_categoria (CategoriaID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =============================================
-- Tabla: Pedidos
-- =============================================
CREATE TABLE Pedidos (
    PedidoID INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioID INT NOT NULL,
    FechaPedido DATETIME DEFAULT CURRENT_TIMESTAMP,
    Estado VARCHAR(50) NOT NULL DEFAULT 'Pendiente',
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID) ON DELETE CASCADE,
    INDEX idx_usuario (UsuarioID),
    INDEX idx_fecha (FechaPedido),
    INDEX idx_estado (Estado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =============================================
-- Tabla: DetallePedidos
-- =============================================
CREATE TABLE DetallePedidos (
    DetalleID INT AUTO_INCREMENT PRIMARY KEY,
    PedidoID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID) ON DELETE CASCADE,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID) ON DELETE RESTRICT,
    INDEX idx_pedido (PedidoID),
    INDEX idx_producto (ProductoID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =============================================
-- Datos de Prueba
-- =============================================

-- Insertar usuarios de prueba
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombre, Apellido, Email) VALUES
('admin', 'admin123', 'Administrador', 'Sistema', 'admin@tienda.com'),
('jperez', 'pass123', 'Juan', 'Pérez', 'jperez@email.com'),
('mgarcia', 'pass456', 'María', 'García', 'mgarcia@email.com'),
('lrodriguez', 'pass789', 'Luis', 'Rodríguez', 'lrodriguez@email.com');

-- Insertar categorías
INSERT INTO Categorias (NombreCategoria) VALUES
('Electrónica'),
('Ropa'),
('Hogar'),
('Deportes'),
('Libros'),
('Juguetes');

-- Insertar productos
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID) VALUES
-- Electrónica
('Smartphone Samsung Galaxy S23', 'Smartphone Android de última generación, 128GB', 899.99, 10, 1),
('Auriculares Sony WH-1000XM5', 'Auriculares inalámbricos con cancelación de ruido', 349.99, 25, 1),
('iPad Air 5', 'Tablet Apple con chip M1, 64GB', 599.99, 15, 1),
('Cámara Canon EOS R50', 'Cámara mirrorless compacta para creadores de contenido', 799.99, 8, 1),

-- Ropa
('Sudadera Hoodie Básica', 'Sudadera con capucha unisex gris vigoré', 45.50, 60, 2),
('Chaqueta Cuero Vegano', 'Chaqueta estilo biker color negro', 89.99, 20, 2),
('Vestido Floral Verano', 'Vestido ligero con estampado de flores', 35.99, 30, 2),

-- Hogar
('Batidora de Mano Braun', 'Batidora minipimer con accesorios, 1000W', 59.90, 25, 3),
('Juego Sábanas Algodón', 'Juego de sábanas para cama de 150cm, 100% algodón', 29.99, 40, 3),
('Lámpara Escritorio LED', 'Lámpara flexible con 3 modos de luz', 24.50, 50, 3),

-- Deportes
('Mancuernas Neopreno 5kg', 'Set de 2 mancuernas antideslizantes', 29.99, 30, 4),
('Esterilla Yoga Pro', 'Esterilla antideslizante con guías de alineación', 22.00, 45, 4),

-- Libros
('Hábitos Atómicos', 'James Clear - Un método fácil y comprobado', 18.50, 100, 5),
('El Señor de los Anillos', 'J.R.R. Tolkien - Edición ilustrada', 35.00, 20, 5),

-- Juguetes
('Juego de Mesa Catan', 'Juego de estrategia y comercio para familias', 42.00, 30, 6),
('Peluche Oso Gigante', 'Oso de peluche suave de 100cm', 32.99, 15, 6);

-- Insertar pedidos de prueba
INSERT INTO Pedidos (UsuarioID, Estado) VALUES
(2, 'Entregado'),
(2, 'Pendiente'),
(3, 'Enviado'),
(4, 'Pendiente');

-- Insertar detalles de pedidos
INSERT INTO DetallePedidos (PedidoID, ProductoID, Cantidad, PrecioUnitario) VALUES
-- Pedido 1 (Juan Pérez - Entregado)
(1, 1, 1, 899.99),
(1, 2, 1, 349.99),
(1, 3, 1, 599.99),

-- Pedido 2 (Juan Pérez - Pendiente)
(2, 5, 2, 45.50),
(2, 7, 1, 35.99),

-- Pedido 3 (María García - Enviado)
(3, 8, 1, 59.90),
(3, 13, 1, 18.50),

-- Pedido 4 (Luis Rodríguez - Pendiente)
(4, 15, 1, 42.00),
(4, 11, 1, 29.99);

-- =============================================
-- Verificación de datos
-- =============================================
SELECT 'Usuarios creados:' AS Info, COUNT(*) AS Total FROM Usuarios;
SELECT 'Categorías creadas:' AS Info, COUNT(*) AS Total FROM Categorias;
SELECT 'Productos creados:' AS Info, COUNT(*) AS Total FROM Productos;
SELECT 'Pedidos creados:' AS Info, COUNT(*) AS Total FROM Pedidos;
SELECT 'Detalles de pedidos creados:' AS Info, COUNT(*) AS Total FROM DetallePedidos;
