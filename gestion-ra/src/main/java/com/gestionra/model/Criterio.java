package com.gestionra.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;

/**
 * Entidad que representa un Criterio de Evaluación.
 * Pertenece a un Resultado de Aprendizaje (RA).
 */
@Entity
@Table(name = "criterios")
public class Criterio {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false)
    private String codigo; // Ej: "CE1.1", "CE1.2"

    @Column(nullable = false)
    private String descripcion;

    // Relación N:1 con Ra
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "ra_id", nullable = false)
    @JsonBackReference
    private Ra ra;

    // Campo auxiliar para exponer el ID del RA en el JSON
    @Transient
    private Long raId;

    // ---- Constructores ----

    public Criterio() {}

    public Criterio(String codigo, String descripcion) {
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

    public Ra getRa() {
        return ra;
    }

    public void setRa(Ra ra) {
        this.ra = ra;
    }

    // Devuelve el ID del RA para el JSON de respuesta
    public Long getRaId() {
        return ra != null ? ra.getId() : raId;
    }

    public void setRaId(Long raId) {
        this.raId = raId;
    }
}
