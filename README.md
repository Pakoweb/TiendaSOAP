# TiendaSOAP - Proyecto de Servicios Web SOAP

Proyecto de servicios web ASMX con MySQL para gestión de tienda/e-commerce.

## 🚀 Inicio Rápido

### 1. Configurar Base de Datos

```bash
# Ejecutar el script SQL en MySQL
mysql -u root -p < CreateDatabase.sql
```

### 2. Configurar Conexión

Edita `TiendaSOAP/Web.config` si tu MySQL usa contraseña:

```xml
<add name="TiendaDB" connectionString="Server=localhost;Database=TiendaDB;Uid=root;Pwd=TU_CONTRASEÑA;" />
```

### 3. Ejecutar Proyecto

1. Abre `TiendaSOAP.sln` en Visual Studio
2. Presiona F5 para ejecutar
3. Navega a los archivos `.asmx` para probar los servicios

## 📋 Servicios Implementados

### WsUsuarios (5 métodos)

- ValidarUsuario
- RegistrarUsuario
- ActualizarUsuario
- EliminarUsuario
- ObtenerUsuarios

### WsProductos (5 métodos)

- CrearProducto
- ActualizarProducto
- EliminarProducto
- ObtenerProductos
- BuscarProductos

### WsCategorias (5 métodos)

- CrearCategoria
- ObtenerCategorias
- ActualizarCategoria
- BorrarCategoria
- BuscarCategoria

### WsPedidos (4 métodos)

- CrearPedido (con transacciones)
- ObtenerPedidosPorUsuario
- ActualizarEstadoPedido
- HistorialCompras

### WsDetallesPedidos (1 método)

- ObtenerDetallesPorPedido

**Total: 20 métodos web**

## 🧪 Prueba Rápida

1. Ejecuta el proyecto
2. Navega a `http://localhost:[puerto]/WsUsuarios.asmx`
3. Prueba **ValidarUsuario** con:
   - nombreUsuario: `jperez`
   - contraseña: `pass123`

## 📚 Documentación Completa

Ver [walkthrough.md](file:///C:/Users/DAW2/.gemini/antigravity/brain/72d46dce-f8f7-4eae-ae28-bdec11ead3f3/walkthrough.md) para instrucciones detalladas y ejemplos de uso.

## 🗄️ Estructura de Base de Datos

- **Usuarios**: Autenticación y perfiles
- **Categorias**: Categorías de productos
- **Productos**: Catálogo con precios y stock
- **Pedidos**: Órdenes de compra
- **DetallePedidos**: Líneas de productos por pedido

## 📦 Datos de Prueba

El script SQL incluye:

- 4 usuarios (admin, jperez, mgarcia, lrodriguez)
- 6 categorías
- 16 productos
- 4 pedidos de ejemplo

## ⚙️ Requisitos

- Visual Studio 2019 o superior
- MySQL Server 5.7 o superior
- MySQL Connector/NET (incluido en packages)
- .NET Framework 4.7.2

## 🔧 Tecnologías

- ASP.NET Web Services (ASMX)
- MySQL
- ADO.NET
- XML Serialization
