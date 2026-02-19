package com.gestionra.controller;

import com.gestionra.model.Criterio;
import com.gestionra.service.CriterioService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Controlador REST para la entidad Criterio de Evaluación.
 * Incluye endpoints anidados bajo /api/ras/{raId}/criterios.
 */
@RestController
@RequestMapping("/api")
public class CriterioController {

    @Autowired
    private CriterioService criterioService;

    // GET /api/criterios — Listar todos los criterios
    @GetMapping("/criterios")
    public List<Criterio> listarTodos() {
        return criterioService.obtenerTodos();
    }

    // GET /api/criterios/{id} — Obtener criterio por ID
    @GetMapping("/criterios/{id}")
    public Criterio obtenerPorId(@PathVariable Long id) {
        return criterioService.obtenerPorId(id);
    }

    // GET /api/ras/{raId}/criterios — Listar criterios de un RA
    @GetMapping("/ras/{raId}/criterios")
    public List<Criterio> listarPorRa(@PathVariable Long raId) {
        return criterioService.obtenerPorRaId(raId);
    }

    // POST /api/ras/{raId}/criterios — Crear criterio en un RA
    @PostMapping("/ras/{raId}/criterios")
    public ResponseEntity<Criterio> crear(@PathVariable Long raId, @RequestBody Criterio criterio) {
        Criterio nuevo = criterioService.crear(raId, criterio);
        return new ResponseEntity<>(nuevo, HttpStatus.CREATED);
    }

    // PUT /api/criterios/{id} — Actualizar un criterio
    @PutMapping("/criterios/{id}")
    public Criterio actualizar(@PathVariable Long id, @RequestBody Criterio criterio) {
        return criterioService.actualizar(id, criterio);
    }

    // DELETE /api/criterios/{id} — Eliminar un criterio
    @DeleteMapping("/criterios/{id}")
    public ResponseEntity<Void> eliminar(@PathVariable Long id) {
        criterioService.eliminar(id);
        return ResponseEntity.noContent().build();
    }
}
