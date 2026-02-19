package com.gestionra.service;

import com.gestionra.exception.ResourceNotFoundException;
import com.gestionra.model.Criterio;
import com.gestionra.model.Ra;
import com.gestionra.repository.CriterioRepository;
import com.gestionra.repository.RaRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Servicio con la lógica de negocio para Criterios de Evaluación.
 * Valida que el RA exista antes de crear un Criterio.
 */
@Service
public class CriterioService {

    @Autowired
    private CriterioRepository criterioRepository;

    @Autowired
    private RaRepository raRepository;

    // Obtener todos los criterios
    public List<Criterio> obtenerTodos() {
        return criterioRepository.findAll();
    }

    // Obtener un criterio por su ID
    public Criterio obtenerPorId(Long id) {
        return criterioRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Criterio no encontrado con id: " + id));
    }

    // Obtener los criterios de un RA concreto
    public List<Criterio> obtenerPorRaId(Long raId) {
        raRepository.findById(raId)
                .orElseThrow(() -> new ResourceNotFoundException("RA no encontrado con id: " + raId));
        return criterioRepository.findByRa_Id(raId);
    }

    // Crear un criterio asociado a un RA
    public Criterio crear(Long raId, Criterio criterio) {
        Ra ra = raRepository.findById(raId)
                .orElseThrow(() -> new ResourceNotFoundException("RA no encontrado con id: " + raId));
        criterio.setRa(ra);
        return criterioRepository.save(criterio);
    }

    // Actualizar un criterio existente
    public Criterio actualizar(Long id, Criterio criterioActualizado) {
        Criterio criterio = obtenerPorId(id);
        criterio.setCodigo(criterioActualizado.getCodigo());
        criterio.setDescripcion(criterioActualizado.getDescripcion());
        return criterioRepository.save(criterio);
    }

    // Eliminar un criterio
    public void eliminar(Long id) {
        Criterio criterio = obtenerPorId(id);
        criterioRepository.delete(criterio);
    }
}
