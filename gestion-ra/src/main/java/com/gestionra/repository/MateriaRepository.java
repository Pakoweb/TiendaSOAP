package com.gestionra.repository;

import com.gestionra.model.Materia;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

/**
 * Repositorio JPA para la entidad Materia.
 * Spring Data genera automáticamente las consultas CRUD.
 */
@Repository
public interface MateriaRepository extends JpaRepository<Materia, Long> {
}
