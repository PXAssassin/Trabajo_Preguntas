using models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeerPreguntasFalsoVerdadero : MonoBehaviour
{
    private List<PreguntasFalsoVerdadero> preguntasFVfaciles= new List<PreguntasFalsoVerdadero>();
    private List<PreguntasFalsoVerdadero> preguntasFVdificiles = new List<PreguntasFalsoVerdadero>();


    public List<PreguntasFalsoVerdadero> PreguntasFVfaciles { get => preguntasFVfaciles; set => preguntasFVfaciles = value; }
    public List<PreguntasFalsoVerdadero> PreguntasFVdificiles { get => preguntasFVdificiles; set => preguntasFVdificiles = value; }

    //______________________________________________________________________
    //ELEMENTOS DE ASIGNACION PARA LOS BOTONES DE RESPUESTA
    string respustaCorrecta;
    public TextMeshProUGUI txtPregunta; 
    public TextMeshProUGUI txtopcion1;
    public TextMeshProUGUI txtopcion2;
    public TextMeshProUGUI txtVersiculo;
    public TextMeshProUGUI txtDificultad;
    //______________________________________________________________________

    public Button btnOpcion1;
    public Button btnOpcion2;

    // Start is called before the first frame update
    void Start()
    {
        PreguntasFalsoVerdaderoMethod();
       
    }



    public void PreguntasFalsoVerdaderoMethod()
    {
        string preguntaLeida = "";
        
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/preguntasFalso_Verdadero.txt"); //esto se debe recorrer linea por linea
            while ((preguntaLeida = sr1.ReadLine()) != null)//las preguntas se parten con "-" cuando inicia "_" es porque seguido esta una posible respuesta. y se organizan en posiciones Ej:[1,2,3,4,5]
            {
                
                string[] lineaPartida = preguntaLeida.Split("-");//Arreglo estatico para almacenar lo leido en preguntaLeida, se parte cada que se tope con "-", organice cada split como posiciones del arreglo
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                if (lineaPartida[3].ToLower().Equals("facil"))
                {
                    string dificultad = lineaPartida[3];
                    PreguntasFalsoVerdadero objPFV_F = new PreguntasFalsoVerdadero(pregunta, respuesta, versiculo, dificultad);
                    preguntasFVfaciles.Add(objPFV_F);
                }
                else if (lineaPartida[3].ToLower().Equals("dificil"))
                {
                    string dificultad = lineaPartida[3];
                    PreguntasFalsoVerdadero objPFV_D = new PreguntasFalsoVerdadero(pregunta, respuesta, versiculo, dificultad);
                    preguntasFVdificiles.Add(objPFV_D);
                }
                else
                {
                    Debug.Log("Hay Objetos Que no hab sido leidos");
                }
            }
            Debug.Log("FalsoVerdadero Dificiles" + preguntasFVdificiles.Count); //count es como el length de JAVA
            Debug.Log("FalsoVerdadero Faciles " + preguntasFVfaciles.Count); //count es como el length de JAVA
        }
        catch (Exception e)
        {
            Debug.Log("ERROR" + e.ToString());
        }
    }



    //logica de seleccion de preguntas
    //______________________________________________________________________
    public void mostrarPreguntaFacil()
    {
        btnOpcion1.interactable = true;
        btnOpcion2.interactable = true;
        System.Random random = new System.Random();
        int i = random.Next(preguntasFVfaciles.Count);

        txtPregunta.text = preguntasFVfaciles[i].Pregunta;
        if (preguntasFVfaciles[i].Respuesta.Equals("true"))
        {
            respustaCorrecta = "Verdadero";
            txtopcion1.text = "Verdadero";
            txtopcion2.text = "Falso";

        }
        else
        {
            respustaCorrecta = "Falso";
            txtopcion1.text = "Verdadero";
            txtopcion2.text = "Falso";

        }
        txtVersiculo.text = preguntasFVfaciles[i].Versiculo;
        txtDificultad.text = "Dificultad : " + preguntasFVfaciles[i].Dificultad;
    }

    public void mostrarPreguntaDificil()
    {
        btnOpcion1.interactable = true;
        btnOpcion2.interactable = true;
        System.Random nrandom = new System.Random();
        int i = nrandom.Next(preguntasFVdificiles.Count);
        txtPregunta.text = preguntasFVdificiles[i].Pregunta;

        if (preguntasFVdificiles[i].Respuesta.Equals("true"))
        {
            respustaCorrecta = "Verdadero";
            txtopcion1.text = "Verdadero";
            txtopcion2.text = "Falso";

        }
        else
        {
            respustaCorrecta = "Falso";
            txtopcion1.text = "Verdadero";
            txtopcion2.text = "Falso";

        }
        txtVersiculo.text = preguntasFVdificiles[i].Versiculo;
        txtDificultad.text = "Dificultad : "+preguntasFVdificiles[i].Dificultad;
    }
    //______________________________________________________________________



    //______________________________________________________________________
    //logica botones de respuestas


    
     public void respuesta1()
     {
        btnOpcion1.interactable = false;
        btnOpcion2.interactable = false;
        bool istrue = txtopcion1.text.Equals(respustaCorrecta);
        enviarSeñalFV(istrue);
     }
     public void respuesta2()
     {
        btnOpcion1.interactable = false;
        btnOpcion2.interactable = false;
        bool istrue = txtopcion2.text.Equals(respustaCorrecta);
        enviarSeñalFV(istrue);
     }
     //______________________________________________________________________

     //logica
     public void enviarSeñalFV(bool istrue)
     {
         GameControllerPregunta gcseñalenviada = GameObject.FindObjectOfType<GameControllerPregunta>();
         if (gcseñalenviada != null)
         {
             gcseñalenviada.ActualizarStatsJuego(istrue);
         }
     }
    
}
