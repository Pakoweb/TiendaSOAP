package com.gestionra.service;

import com.gestionra.exception.ResourceNotFoundException;
import com.gestionra.model.Materia;
import com.gestionra.model.Ra;
import com.gestionra.repository.MateriaRepository;
import com.gestionra.repository.RaRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Servicio con la lógica de negocio para Resultados de Aprendizaje (RA).
 * Valida que la Materia exista antes de crear un RA.
 */
@Service
public class RaService {

    @Autowired
    private RaRepository raRepository;

    @Autowired
    private MateriaRepository materiaRepository;

    // Obtener todos los RA
    public List<Ra> obtenerTodos() {
        return raRepository.findAll();
    }

    // Obtener un RA por su ID
    public Ra obtenerPorId(Long id) {
        return raRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("RA no encontrado con id: " + id));
    }

    // Obtener los RA de una materia concreta
    public List<Ra> obtenerPorMateriaId(Long materiaId) {
        // Verificamos que la materia exista
        materiaRepository.findById(materiaId)
                .orElseThrow(() -> new ResourceNotFoundException("Materia no encontrada con id: " + materiaId));
        return raRepository.findByMateria_Id(materiaId);
    }

    // Crear un RA asociado a una materia
    public Ra crear(Long materiaId, Ra ra) {
        Materia materia = materiaRepository.findById(materiaId)
                .orElseThrow(() -> new ResourceNotFoundException("Materia no encontrada con id: " + materiaId));
        ra.setMateria(materia);
        return raRepository.save(ra);
    }

    // Actualizar un RA existente
    public Ra actualizar(Long id, Ra raActualizado) {
        Ra ra = obtenerPorId(id);
        ra.setCodigo(raActualizado.getCodigo());
        ra.setDescripcion(raActualizado.getDescripcion());
        return raRepository.save(ra);
    }

    // Eliminar un RA (y sus criterios en cascada)
    public void eliminar(Long id) {
        Ra ra = obtenerPorId(id);
        raRepository.delete(ra);
    }
}
