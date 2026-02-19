package com.gestionra.repository;

import com.gestionra.model.Ra;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Repositorio JPA para la entidad Ra (Resultado de Aprendizaje).
 * Incluye método derivado para buscar RA por ID de materia.
 */
@Repository
public interface RaRepository extends JpaRepository<Ra, Long> {

    // Spring Data genera la query automáticamente usando la relación materia -> id
    List<Ra> findByMateria_Id(Long materiaId);
}
