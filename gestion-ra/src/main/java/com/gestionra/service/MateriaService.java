package com.gestionra.service;

import com.gestionra.exception.ResourceNotFoundException;
import com.gestionra.model.Materia;
import com.gestionra.repository.MateriaRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Servicio con la lógica de negocio para Materias.
 * El controlador delega aquí todas las operaciones.
 */
@Service
public class MateriaService {

    @Autowired
    private MateriaRepository materiaRepository;

    // Obtener todas las materias
    public List<Materia> obtenerTodas() {
        return materiaRepository.findAll();
    }

    // Obtener una materia por su ID
    public Materia obtenerPorId(Long id) {
        return materiaRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Materia no encontrada con id: " + id));
    }

    // Crear una nueva materia
    public Materia crear(Materia materia) {
        return materiaRepository.save(materia);
    }

    // Actualizar una materia existente
    public Materia actualizar(Long id, Materia materiaActualizada) {
        Materia materia = obtenerPorId(id);
        materia.setNombre(materiaActualizada.getNombre());
        materia.setCurso(materiaActualizada.getCurso());
        materia.setDescripcion(materiaActualizada.getDescripcion());
        return materiaRepository.save(materia);
    }

    // Eliminar una materia (y sus RA/Criterios en cascada)
    public void eliminar(Long id) {
        Materia materia = obtenerPorId(id);
        materiaRepository.delete(materia);
    }
}
