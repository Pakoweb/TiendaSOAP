package com.gestionra.controller;

import com.gestionra.model.Ra;
import com.gestionra.service.RaService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Controlador REST para la entidad Ra (Resultado de Aprendizaje).
 * Incluye endpoints anidados bajo /api/materias/{materiaId}/ras.
 */
@RestController
@RequestMapping("/api")
public class RaController {

    @Autowired
    private RaService raService;

    // GET /api/ras — Listar todos los RA
    @GetMapping("/ras")
    public List<Ra> listarTodos() {
        return raService.obtenerTodos();
    }

    // GET /api/ras/{id} — Obtener RA por ID
    @GetMapping("/ras/{id}")
    public Ra obtenerPorId(@PathVariable Long id) {
        return raService.obtenerPorId(id);
    }

    // GET /api/materias/{materiaId}/ras — Listar RA de una materia
    @GetMapping("/materias/{materiaId}/ras")
    public List<Ra> listarPorMateria(@PathVariable Long materiaId) {
        return raService.obtenerPorMateriaId(materiaId);
    }

    // POST /api/materias/{materiaId}/ras — Crear RA en una materia
    @PostMapping("/materias/{materiaId}/ras")
    public ResponseEntity<Ra> crear(@PathVariable Long materiaId, @RequestBody Ra ra) {
        Ra nuevo = raService.crear(materiaId, ra);
        return new ResponseEntity<>(nuevo, HttpStatus.CREATED);
    }

    // PUT /api/ras/{id} — Actualizar un RA
    @PutMapping("/ras/{id}")
    public Ra actualizar(@PathVariable Long id, @RequestBody Ra ra) {
        return raService.actualizar(id, ra);
    }

    // DELETE /api/ras/{id} — Eliminar un RA
    @DeleteMapping("/ras/{id}")
    public ResponseEntity<Void> eliminar(@PathVariable Long id) {
        raService.eliminar(id);
        return ResponseEntity.noContent().build();
    }
}
