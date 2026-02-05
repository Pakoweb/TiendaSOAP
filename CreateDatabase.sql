-- =============================================
-- Script de Creación de Base de Datos TiendaDB
-- Proyecto: TiendaSOAP - Servicio Web SOAP
-- =============================================

-- Crear la base de datos si no existe
CREATE DATABASE IF NOT EXISTS TiendaDB;
USE TiendaDB;

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
('Laptop HP 15', 'Laptop HP 15 pulgadas, 8GB RAM, 256GB SSD', 599.99, 15, 1),
('Mouse Logitech', 'Mouse inalámbrico Logitech M185', 19.99, 50, 1),
('Teclado Mecánico', 'Teclado mecánico RGB retroiluminado', 79.99, 25, 1),
('Monitor Samsung 24"', 'Monitor LED Full HD 24 pulgadas', 159.99, 10, 1),

-- Ropa
('Camiseta Nike', 'Camiseta deportiva Nike Dri-FIT', 29.99, 100, 2),
('Pantalón Vaquero Levi''s', 'Pantalón vaquero Levi''s 501 Original', 69.99, 40, 2),
('Zapatillas Adidas', 'Zapatillas running Adidas Ultraboost', 149.99, 30, 2),

-- Hogar
('Cafetera Nespresso', 'Cafetera de cápsulas Nespresso', 129.99, 20, 3),
('Aspiradora Dyson', 'Aspiradora sin cable Dyson V11', 399.99, 8, 3),
('Sartén Tefal', 'Sartén antiadherente Tefal 28cm', 34.99, 45, 3),

-- Deportes
('Balón Fútbol Nike', 'Balón de fútbol Nike Premier League', 24.99, 60, 4),
('Raqueta Tenis Wilson', 'Raqueta de tenis Wilson Pro Staff', 189.99, 12, 4),

-- Libros
('El Quijote', 'Don Quijote de la Mancha - Edición completa', 19.99, 35, 5),
('Cien Años de Soledad', 'Gabriel García Márquez', 14.99, 50, 5),

-- Juguetes
('LEGO Star Wars', 'Set LEGO Star Wars Millennium Falcon', 159.99, 18, 6),
('Muñeca Barbie', 'Barbie Fashionista', 24.99, 40, 6);

-- Insertar pedidos de prueba
INSERT INTO Pedidos (UsuarioID, Estado) VALUES
(2, 'Entregado'),
(2, 'Pendiente'),
(3, 'Enviado'),
(4, 'Pendiente');

-- Insertar detalles de pedidos
INSERT INTO DetallePedidos (PedidoID, ProductoID, Cantidad, PrecioUnitario) VALUES
-- Pedido 1 (Juan Pérez - Entregado)
(1, 1, 1, 599.99),
(1, 2, 1, 19.99),
(1, 3, 1, 79.99),

-- Pedido 2 (Juan Pérez - Pendiente)
(2, 5, 2, 29.99),
(2, 7, 1, 149.99),

-- Pedido 3 (María García - Enviado)
(3, 8, 1, 129.99),
(3, 13, 1, 19.99),

-- Pedido 4 (Luis Rodríguez - Pendiente)
(4, 15, 1, 159.99),
(4, 11, 1, 24.99);

-- =============================================
-- Verificación de datos
-- =============================================
SELECT 'Usuarios creados:' AS Info, COUNT(*) AS Total FROM Usuarios;
SELECT 'Categorías creadas:' AS Info, COUNT(*) AS Total FROM Categorias;
SELECT 'Productos creados:' AS Info, COUNT(*) AS Total FROM Productos;
SELECT 'Pedidos creados:' AS Info, COUNT(*) AS Total FROM Pedidos;
SELECT 'Detalles de pedidos creados:' AS Info, COUNT(*) AS Total FROM DetallePedidos;
