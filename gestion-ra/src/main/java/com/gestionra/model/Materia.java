package com.gestionra.model;

import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Entidad que representa una Materia (asignatura).
 * Una Materia puede tener varios Resultados de Aprendizaje (RA).
 */
@Entity
@Table(name = "materias")
public class Materia {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false)
    private String nombre;

    private String curso;

    private String descripcion;

    // Relación 1:N con RA — al borrar una Materia se borran sus RA
    @OneToMany(mappedBy = "materia", cascade = CascadeType.ALL, orphanRemoval = true)
    @JsonManagedReference
    private List<Ra> resultadosAprendizaje = new ArrayList<>();

    // ---- Constructores ----

    public Materia() {}

    public Materia(String nombre, String curso, String descripcion) {
        this.nombre = nombre;
        this.curso = curso;
        this.descripcion = descripcion;
    }

    // ---- Getters y Setters ----

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getCurso() {
        return curso;
    }

    public void setCurso(String curso) {
        this.curso = curso;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public List<Ra> getResultadosAprendizaje() {
        return resultadosAprendizaje;
    }

    public void setResultadosAprendizaje(List<Ra> resultadosAprendizaje) {
        this.resultadosAprendizaje = resultadosAprendizaje;
    }
}
