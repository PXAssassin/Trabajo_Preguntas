using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LeerPreguntasAbiertas : MonoBehaviour
{

    private List<PreguntasAbiertas> preguntasAbFaciles= new List<PreguntasAbiertas>();
    private List<PreguntasAbiertas> preguntasAbDificiles= new List<PreguntasAbiertas>();

    public List<PreguntasAbiertas> PreguntasAbFaciles { get => preguntasAbFaciles; set => preguntasAbFaciles = value; }
    public List<PreguntasAbiertas> PreguntasAbDificiles { get => preguntasAbDificiles; set => preguntasAbDificiles = value; }


    //______________________________________________________________________
    //ELEMENTOS DE ASIGNACION PARA LOS BOTONES DE RESPUESTA
    public TextMeshProUGUI txtPreguntaPAB;
    public TextMeshProUGUI txtRespuesta;
    public TextMeshProUGUI txtVersiculo;
    public TextMeshProUGUI txtDificultad;
    //______________________________________________________________________


    // Start is called before the first frame update
    void Start()
    {
        PreguntasAbiertasMethod();
    }

    #region LecturaPreguntasAbiertas
    public void PreguntasAbiertasMethod()
    {
        string preguntaLeida = "";
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasAbiertas.txt"); //esto se debe recorrer linea por linea
            while ((preguntaLeida = sr1.ReadLine()) != null)//las preguntas se parten con "-" cuando inicia "_" es porque seguido esta una posible respuesta. y se organizan en posiciones Ej:[1,2,3,4,5]
            {

                string[] lineaPartida = preguntaLeida.Split("-");//Arreglo estatico para almacenar lo leido en preguntaLeida, se parte cada que se tope con "-", organice cada split como posiciones del arreglo
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                //string dificultad = lineaPartida[3].ToLower();
                
                if (lineaPartida[3].ToLower().Equals("facil"))
                {
                    string dificultad = lineaPartida[3];
                    PreguntasAbiertas objPAB_F = new PreguntasAbiertas(pregunta, respuesta, versiculo, dificultad);
                    preguntasAbFaciles.Add(objPAB_F);
                }
                else if (lineaPartida[3].ToLower().Equals("dificil"))
                {
                    string dificultad = lineaPartida[3];
                    PreguntasAbiertas objPAB_D = new PreguntasAbiertas(pregunta, respuesta, versiculo, dificultad);
                    preguntasAbDificiles.Add(objPAB_D);
                }
                else
                {
                    Debug.Log("Hay Objetos Que no hab sido leidos");
                }
            }
            Debug.Log("AbiertasFaciles" + preguntasAbFaciles.Count); 
            Debug.Log("AbiertasDificiles " + preguntasAbDificiles.Count); 
        }
        catch (Exception e)
        {
            Debug.Log("ERROR" + e.ToString());
        }
    }
    #endregion

    #region Metodos MostrarDificultad
    public void mostrarPreguntaFacilAB()
    {
        System.Random random = new System.Random();
        int i = random.Next(preguntasAbFaciles.Count);
        txtPreguntaPAB.text = preguntasAbFaciles[i].Pregunta;
        txtRespuesta.text = preguntasAbFaciles[i].Respuesta;
        txtVersiculo.text = preguntasAbFaciles[i].Versiculo;
        txtDificultad.text = preguntasAbFaciles[i].Dificultad;
    }

    public void mostrarPreguntaDificilAB()
    {
        System.Random random = new System.Random();
        int i = random.Next(preguntasAbDificiles.Count);
        txtPreguntaPAB.text = preguntasAbDificiles[i].Pregunta;
        txtRespuesta.text = preguntasAbDificiles[i].Respuesta;
        txtVersiculo.text = preguntasAbDificiles[i].Versiculo;
        txtDificultad.text = preguntasAbDificiles[i].Dificultad;
    }
    #endregion
    
    public void continuar()
    {
        bool istrue = true;
        enviarSeñal(istrue);
    }

    public void enviarSeñal(bool istrue)
    {
        GameControllerPregunta gcseñalenviada = GameObject.FindObjectOfType<GameControllerPregunta>();
        if (gcseñalenviada != null)
        {
            gcseñalenviada.SeñalRecibida_AB(istrue);
        }
    }

}
