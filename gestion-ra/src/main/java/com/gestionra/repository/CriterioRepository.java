package com.gestionra.repository;

import com.gestionra.model.Criterio;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Repositorio JPA para la entidad Criterio de Evaluación.
 * Incluye método derivado para buscar Criterios por ID de RA.
 */
@Repository
public interface CriterioRepository extends JpaRepository<Criterio, Long> {

    List<Criterio> findByRa_Id(Long raId);
}
