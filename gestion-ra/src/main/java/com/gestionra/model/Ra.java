package com.gestionra.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Entidad que representa un Resultado de Aprendizaje (RA).
 * Pertenece a una Materia y puede tener varios Criterios de Evaluación.
 */
@Entity
@Table(name = "resultados_aprendizaje")
public class Ra {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false)
    private String codigo; // Ej: "RA1", "RA2"

    @Column(nullable = false)
    private String descripcion;

    // Relación N:1 con Materia
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "materia_id", nullable = false)
    @JsonBackReference
    private Materia materia;

    // Relación 1:N con Criterio — al borrar un RA se borran sus criterios
    @OneToMany(mappedBy = "ra", cascade = CascadeType.ALL, orphanRemoval = true)
    @JsonManagedReference
    private List<Criterio> criterios = new ArrayList<>();

    // Campo auxiliar para exponer el ID de la materia en el JSON
    @Transient
    private Long materiaId;

    // ---- Constructores ----

    public Ra() {}

    public Ra(String codigo, String descripcion) {
        this.codigo = codigo;
        this.descripcion = descripcion;
    }

    // ---- Getters y Setters ----

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getCodigo() {
        return codigo;
    }

    public void setCodigo(String codigo) {
        this.codigo = codigo;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public Materia getMateria() {
        return materia;
    }

    public void setMateria(Materia materia) {
        this.materia = materia;
    }

    public List<Criterio> getCriterios() {
        return criterios;
    }

    public void setCriterios(List<Criterio> criterios) {
        this.criterios = criterios;
    }

    // Devuelve el ID de la materia para el JSON de respuesta
    public Long getMateriaId() {
        return materia != null ? materia.getId() : materiaId;
    }

    public void setMateriaId(Long materiaId) {
        this.materiaId = materiaId;
    }
}
