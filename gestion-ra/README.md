# Gestión RA — API REST con Spring Boot

API REST para gestionar **Materias**, **Resultados de Aprendizaje (RA)** y **Criterios de Evaluación**, desarrollada con Spring Boot siguiendo arquitectura MVC.

## 🏗️ Estructura del Proyecto

```
com.gestionra
├── controller/
│   ├── MateriaController.java      # Endpoints REST de Materias
│   ├── RaController.java           # Endpoints REST de RA
│   └── CriterioController.java     # Endpoints REST de Criterios
├── service/
│   ├── MateriaService.java         # Lógica de negocio de Materias
│   ├── RaService.java              # Lógica de negocio de RA
│   └── CriterioService.java        # Lógica de negocio de Criterios
├── model/
│   ├── Materia.java                # Entidad JPA
│   ├── Ra.java                     # Entidad JPA
│   └── Criterio.java               # Entidad JPA
├── repository/
│   ├── MateriaRepository.java      # JPA Repository
│   ├── RaRepository.java           # JPA Repository
│   └── CriterioRepository.java     # JPA Repository
├── exception/
│   ├── ResourceNotFoundException.java
│   └── GlobalExceptionHandler.java
└── GestionRaApplication.java       # Clase principal
```

## ⚙️ Requisitos

- **Java 17** o superior (JDK 17+)
- **MySQL 5.7+** (incluido en XAMPP)
- **Maven** (incluido como wrapper `mvnw.cmd`)

## 🚀 Instalación y Ejecución

### 1. Crear la base de datos

Asegúrate de que MySQL esté corriendo (XAMPP) y crea la base de datos:

```sql
CREATE DATABASE IF NOT EXISTS gestionra;
```

> **Nota:** Spring Boot crea las tablas automáticamente al arrancar.

### 2. Configurar conexión (si es necesario)

Edita `src/main/resources/application.properties` si tu MySQL usa contraseña:

```properties
spring.datasource.password=TU_CONTRASEÑA
```

### 3. Ejecutar la aplicación

```bash
cd gestion-ra
mvnw.cmd spring-boot:run
```

La API estará disponible en `http://localhost:8080`

## 📋 Endpoints

### Materias (`/api/materias`)

| Método | Endpoint              | Descripción         |
|--------|-----------------------|---------------------|
| GET    | `/api/materias`       | Listar todas        |
| GET    | `/api/materias/{id}`  | Obtener por ID      |
| POST   | `/api/materias`       | Crear               |
| PUT    | `/api/materias/{id}`  | Actualizar          |
| DELETE | `/api/materias/{id}`  | Eliminar            |

### Resultados de Aprendizaje (`/api/ras`)

| Método | Endpoint                       | Descripción                |
|--------|--------------------------------|----------------------------|
| GET    | `/api/ras`                     | Listar todos               |
| GET    | `/api/ras/{id}`                | Obtener por ID             |
| GET    | `/api/materias/{id}/ras`       | Listar RA de una materia   |
| POST   | `/api/materias/{id}/ras`       | Crear RA en una materia    |
| PUT    | `/api/ras/{id}`                | Actualizar                 |
| DELETE | `/api/ras/{id}`                | Eliminar                   |

### Criterios de Evaluación (`/api/criterios`)

| Método | Endpoint                       | Descripción                   |
|--------|--------------------------------|-------------------------------|
| GET    | `/api/criterios`               | Listar todos                  |
| GET    | `/api/criterios/{id}`          | Obtener por ID                |
| GET    | `/api/ras/{id}/criterios`      | Listar criterios de un RA     |
| POST   | `/api/ras/{id}/criterios`      | Crear criterio en un RA       |
| PUT    | `/api/criterios/{id}`          | Actualizar                    |
| DELETE | `/api/criterios/{id}`          | Eliminar                      |

## 🧪 Ejemplos de Pruebas (curl)

### Crear una Materia

```bash
curl -X POST http://localhost:8080/api/materias \
  -H "Content-Type: application/json" \
  -d '{"nombre":"DWEC","curso":"2º DAM","descripcion":"Desarrollo Web en Entorno Cliente"}'
```

**Respuesta (201 Created):**
```json
{
  "id": 1,
  "nombre": "DWEC",
  "curso": "2º DAM",
  "descripcion": "Desarrollo Web en Entorno Cliente",
  "resultadosAprendizaje": []
}
```

### Listar todas las Materias

```bash
curl http://localhost:8080/api/materias
```

### Obtener una Materia por ID

```bash
curl http://localhost:8080/api/materias/1
```

### Actualizar una Materia

```bash
curl -X PUT http://localhost:8080/api/materias/1 \
  -H "Content-Type: application/json" \
  -d '{"nombre":"DWEC Actualizado","curso":"2º DAM","descripcion":"Módulo actualizado"}'
```

### Eliminar una Materia

```bash
curl -X DELETE http://localhost:8080/api/materias/1
```

**Respuesta: 204 No Content**

### Crear un RA en una Materia

```bash
curl -X POST http://localhost:8080/api/materias/1/ras \
  -H "Content-Type: application/json" \
  -d '{"codigo":"RA1","descripcion":"Selecciona las arquitecturas y tecnologías de programación"}'
```

### Crear un Criterio en un RA

```bash
curl -X POST http://localhost:8080/api/ras/1/criterios \
  -H "Content-Type: application/json" \
  -d '{"codigo":"CE1.1","descripcion":"Se han caracterizado las tecnologías de programación"}'
```

### Obtener error 404

```bash
curl http://localhost:8080/api/materias/999
```

**Respuesta (404):**
```json
{
  "timestamp": "2026-02-19T09:15:00",
  "status": 404,
  "error": "Not Found",
  "mensaje": "Materia no encontrada con id: 999"
}
```

## 🧪 Ejemplos de Pruebas (Postman)

### Importar colección

1. Abrir Postman
2. Crear una nueva colección "Gestión RA"
3. Añadir las siguientes peticiones:

| Nombre            | Método | URL                                             | Body (JSON)                                                                                    |
|-------------------|--------|--------------------------------------------------|-----------------------------------------------------------------------------------------------|
| Crear Materia     | POST   | `http://localhost:8080/api/materias`             | `{"nombre":"DWEC","curso":"2º DAM","descripcion":"Desarrollo Web en Entorno Cliente"}`        |
| Listar Materias   | GET    | `http://localhost:8080/api/materias`             | —                                                                                             |
| Obtener Materia   | GET    | `http://localhost:8080/api/materias/1`           | —                                                                                             |
| Actualizar Materia| PUT    | `http://localhost:8080/api/materias/1`           | `{"nombre":"DWEC v2","curso":"2º DAM","descripcion":"Actualizado"}`                           |
| Eliminar Materia  | DELETE | `http://localhost:8080/api/materias/1`           | —                                                                                             |
| Crear RA          | POST   | `http://localhost:8080/api/materias/1/ras`       | `{"codigo":"RA1","descripcion":"Selecciona arquitecturas y tecnologías"}`                     |
| Crear Criterio    | POST   | `http://localhost:8080/api/ras/1/criterios`      | `{"codigo":"CE1.1","descripcion":"Se han caracterizado las tecnologías"}`                     |

> **Headers**: En POST y PUT, añadir `Content-Type: application/json`

## 🔧 Tecnologías

- Spring Boot 3.4.3
- Spring Data JPA + Hibernate
- MySQL
- Maven
- Java 17
