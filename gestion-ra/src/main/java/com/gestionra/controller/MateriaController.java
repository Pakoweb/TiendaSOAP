package com.gestionra.controller;

import com.gestionra.model.Materia;
import com.gestionra.service.MateriaService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Controlador REST para la entidad Materia.
 * Solo expone endpoints y delega la lógica al servicio.
 */
@RestController
@RequestMapping("/api/materias")
public class MateriaController {

    @Autowired
    private MateriaService materiaService;

    // GET /api/materias — Listar todas las materias
    @GetMapping
    public List<Materia> listarTodas() {
        return materiaService.obtenerTodas();
    }

    // GET /api/materias/{id} — Obtener materia por ID
    @GetMapping("/{id}")
    public Materia obtenerPorId(@PathVariable Long id) {
        return materiaService.obtenerPorId(id);
    }

    // POST /api/materias — Crear una nueva materia
    @PostMapping
    public ResponseEntity<Materia> crear(@RequestBody Materia materia) {
        Materia nueva = materiaService.crear(materia);
        return new ResponseEntity<>(nueva, HttpStatus.CREATED);
    }

    // PUT /api/materias/{id} — Actualizar una materia
    @PutMapping("/{id}")
    public Materia actualizar(@PathVariable Long id, @RequestBody Materia materia) {
        return materiaService.actualizar(id, materia);
    }

    // DELETE /api/materias/{id} — Eliminar una materia
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> eliminar(@PathVariable Long id) {
        materiaService.eliminar(id);
        return ResponseEntity.noContent().build();
    }
}
