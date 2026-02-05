# Solución de Errores de Compilación - TiendaSOAP

## Problema

Al presionar F5 en Visual Studio, el proyecto da error de compilación.

## Causa

He añadido nuevos archivos de clases modelo al proyecto, pero Visual Studio necesita recargar el proyecto para reconocerlos.

## Solución - Opción 1: Recargar Proyecto (Recomendado)

1. **Cerrar Visual Studio completamente**
2. **Volver a abrir** `TiendaSOAP.sln`
3. En el **Explorador de soluciones**, verifica que veas estos archivos en la carpeta `datos`:
   - ✅ Categorias.cs (ya existía)
   - ✅ Usuario.cs (nuevo)
   - ✅ Producto.cs (nuevo)
   - Pedido.cs (nuevo)
   - ✅ DetallePedido.cs (nuevo)
4. Haz clic derecho en la solución → **Rebuild Solution**
5. Presiona **F5** para ejecutar

## Solución - Opción 2: Recargar Solo el Proyecto

Si no quieres cerrar Visual Studio:

1. En el **Explorador de soluciones**, haz clic derecho en el proyecto **TiendaSOAP**
2. Selecciona **Unload Project** (Descargar proyecto)
3. Haz clic derecho nuevamente en **TiendaSOAP (unavailable)**
4. Selecciona **Reload Project** (Recargar proyecto)
5. Haz clic derecho en la solución → **Rebuild Solution**
6. Presiona **F5**

## Solución - Opción 3: Limpiar y Recompilar

1. En Visual Studio, ve a **Build** → **Clean Solution**
2. Luego **Build** → **Rebuild Solution**
3. Presiona **F5**

## Verificación de Archivos

Asegúrate de que estos archivos existen en tu proyecto:

### Carpeta `datos/`

- [x] `Categorias.cs`
- [x] `Usuario.cs`
- [x] `Producto.cs`
- [x] `Pedido.cs`
- [x] `DetallePedido.cs`

### Servicios Web

- [x] `WsUsuarios.asmx.cs`
- [x] `WsProductos.asmx.cs`
- [x] `WsCategorias.asmx.cs`
- [x] `WsPedidos.asmx.cs`
- [x] `WsDetallesPedidos.asmx.cs`

## Si el Error Persiste

### Error: "No se puede encontrar la base de datos"

Asegúrate de haber ejecutado el script SQL:

```bash
mysql -u root -p < CreateDatabase.sql
```

O desde MySQL Workbench, abre y ejecuta el archivo `CreateDatabase.sql`.

### Error: "Could not load file or assembly"

1. Ve a **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Ejecuta:
   ```
   Update-Package -reinstall
   ```

### Error: "The type or namespace name 'tiendasoap' could not be found"

Esto significa que Visual Studio no reconoce los nuevos archivos. Usa la **Opción 1** (cerrar y reabrir Visual Studio).

### Error de Conexión MySQL

Verifica en `Web.config` que la cadena de conexión sea correcta:

```xml
<add name="TiendaDB" connectionString="Server=localhost;Database=TiendaDB;Uid=root;Pwd=;" />
```

Si tu MySQL usa contraseña, cambia `Pwd=;` a `Pwd=tucontraseña;`

## Próximos Pasos Después de Compilar

Una vez que el proyecto compile correctamente:

1. **Ejecuta el proyecto** (F5)
2. El navegador se abrirá mostrando la lista de servicios
3. **Prueba un servicio**:
   - Haz clic en `WsUsuarios.asmx`
   - Haz clic en `ValidarUsuario`
   - Introduce:
     - nombreUsuario: `jperez`
     - contraseña: `pass123`
   - Haz clic en **Invoke**
   - Deberías ver los datos del usuario en XML

## Contacto

Si después de seguir estos pasos el error persiste, por favor comparte:

1. El mensaje de error exacto que aparece
2. La pestaña "Error List" de Visual Studio
3. La pestaña "Output" de Visual Studio
