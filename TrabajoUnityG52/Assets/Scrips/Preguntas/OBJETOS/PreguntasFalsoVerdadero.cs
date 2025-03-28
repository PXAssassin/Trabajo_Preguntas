using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreguntasFalsoVerdadero
{
    private string pregunta;
    private string respuesta;
    private string versiculo;
    private string dificultad;

    public PreguntasFalsoVerdadero()
    {
    }

    public PreguntasFalsoVerdadero(string pregunta, string respuesta, string versiculo, string dificultad)
    {
        this.Pregunta = pregunta;
        this.Respuesta = respuesta;
        this.Versiculo = versiculo;
        this.Dificultad = dificultad;
    }

    public string Pregunta { get => pregunta; set => pregunta = value; }
    public string Respuesta { get => respuesta; set => respuesta = value; }
    public string Versiculo { get => versiculo; set => versiculo = value; }
    public string Dificultad { get => dificultad; set => dificultad = value; }
}
