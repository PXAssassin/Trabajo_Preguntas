using models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeerPreguntasMultiples : MonoBehaviour
{
    //______________________________________________________________________
    //DECLARACION DE LISTAS PARA EL ALMACENAMIENTO SEPARADO PARA NUESTRAS PREGUNTAS DIFICILES  Y FACILES
    private List<PreguntasMultiples> preguntasMdificil = new List<PreguntasMultiples>();
    private List<PreguntasMultiples> preguntasMfacil = new List<PreguntasMultiples>();
    //______________________________________________________________________

    //______________________________________________________________________
    //GETTE Y SETTER LISTAS :P
    public List<PreguntasMultiples> PreguntasMdificil { get => preguntasMdificil; set => preguntasMdificil = value; }
    public List<PreguntasMultiples> PreguntasMfacil { get => preguntasMfacil; set => preguntasMfacil = value; }
    //______________________________________________________________________

    //______________________________________________________________________
    //ELEMENTOS DE ASIGNACION PARA LOS BOTONES DE RESPUESTA
    string respuestaCorrecta;
    public TextMeshProUGUI txtPregunta;
    public TextMeshProUGUI txtrespuesta1;
    public TextMeshProUGUI txtrespuesta2;
    public TextMeshProUGUI txtrespuesta3;
    public TextMeshProUGUI txtrespuesta4;
    //______________________________________________________________________

    public Button btnOpcion1;
    public Button btnOpcion2;
    public Button btnOpcion3;
    public Button btnOpcion4;



    // Start is called before the first frame update
    void Start()
    {

        PreguntasMultiplesMetod();
    }


    //______________________________________________________________________
    //METODO PARA LEER PREGUNTAS MULTIPLES
    public void PreguntasMultiplesMetod()
    {
        string preguntaLeida = "";
        
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasM.txt"); //esto se debe recorrer linea por linea
            while ((preguntaLeida = sr1.ReadLine()) != null)//las preguntas se parten con "-" cuando inicia "_" es porque seguido esta una posible respuesta. y se organizan en posiciones Ej:[1,2,3,4,5]
            {
                string[] lineaPartida = preguntaLeida.Split("-");//Arreglo estatico para almacenar lo leido en preguntaLeida, se parte cada que se tope con "-", organice cada split como posiciones del arreglo
                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta = lineaPartida[5];
                string versiculo = lineaPartida[6];
                //string dificultad = lineaPartida[7].ToLower();

                //PreguntasMultiples objPM = new PreguntasMultiples(pregunta, respuesta1, respuesta2, respuesta3, respuesta4, respuestaCorrecta, versiculo, dificultad);
                if (lineaPartida[7].ToLower().Equals("facil"))
                {
                    string dificultad = lineaPartida[7];
                    PreguntasMultiples objPM_F = new PreguntasMultiples(pregunta, respuesta1, respuesta2, respuesta3, respuesta4, respuestaCorrecta, versiculo, dificultad);
                    preguntasMfacil.Add(objPM_F);

                }
                else if (lineaPartida[7].ToLower().Equals("dificil"))
                {
                    string dificultad = lineaPartida[7];
                    PreguntasMultiples objPM_D = new PreguntasMultiples(pregunta, respuesta1, respuesta2, respuesta3, respuesta4, respuestaCorrecta, versiculo, dificultad);
                    preguntasMdificil.Add(objPM_D);
                }
                else
                {
                    Debug.Log("Hay Objetos Que no han sido leidos");
                }
                
            }
            Debug.Log("Tamañano PMP_DIFICILES " + preguntasMdificil.Count); //count es como el length de JAVA
            Debug.Log("Tamañano PMP_FACILES " + preguntasMfacil.Count);  //count es como el length de JAVA
        }  
        catch (Exception e)
        {
            Debug.Log("ERROR" + e.ToString());
        }
    }
    //______________________________________________________________________



    //logica de seleccion de preguntas
    //______________________________________________________________________
    public void mostrarPreguntaFacil()
    {
        btnOpcion1.interactable = true;
        btnOpcion2.interactable = true;
        btnOpcion3.interactable = true;
        btnOpcion4.interactable = true;

        System.Random random = new System.Random();
        int i = random.Next(preguntasMfacil.Count);
        txtPregunta.text = preguntasMfacil[i].Pregunta;
        txtrespuesta1.text = preguntasMfacil[i].Respuesta1;
        txtrespuesta2.text = preguntasMfacil[i].Respuesta2;
        txtrespuesta3.text = preguntasMfacil[i].Respuesta3;
        txtrespuesta4.text = preguntasMfacil[i].Respuesta4;
        respuestaCorrecta = preguntasMfacil[i].RespuestaCorrecta;
    }
    public void mostrarPreguntaDificil()
    {
        btnOpcion1.interactable = true;
        btnOpcion2.interactable = true;
        btnOpcion3.interactable = true;
        btnOpcion4.interactable = true;

        System.Random random = new System.Random();
        int i = random.Next(preguntasMdificil.Count);
        txtPregunta.text = preguntasMdificil[i].Pregunta;
        txtrespuesta1.text = preguntasMdificil[i].Respuesta1;
        txtrespuesta2.text = preguntasMdificil[i].Respuesta2;
        txtrespuesta3.text = preguntasMdificil[i].Respuesta3;
        txtrespuesta4.text = preguntasMdificil[i].Respuesta4;
        respuestaCorrecta = preguntasMdificil[i].RespuestaCorrecta;
    }
    //______________________________________________________________________



    //______________________________________________________________________
    //logica botones de respuestas

    public void respuesta1()
    {
        btnOpcion1.interactable = false;
        btnOpcion2.interactable = false;
        btnOpcion3.interactable = false;
        btnOpcion4.interactable = false;

        bool istrue = txtrespuesta1.text.Equals(respuestaCorrecta);
        enviarSeñalMP(istrue);
    }
    public void respuesta2()
    {
        btnOpcion1.interactable = false;
        btnOpcion2.interactable = false;
        btnOpcion3.interactable = false;
        btnOpcion4.interactable = false;
        bool istrue = txtrespuesta2.text.Equals(respuestaCorrecta);
        enviarSeñalMP(istrue);
    }
    public void respuesta3()
    {
        btnOpcion1.interactable = false;
        btnOpcion2.interactable = false;
        btnOpcion3.interactable = false;
        btnOpcion4.interactable = false;
        bool istrue = txtrespuesta3.text.Equals(respuestaCorrecta);
        enviarSeñalMP(istrue);
    }
    public void respuesta4()
    {
        btnOpcion1.interactable = false;
        btnOpcion2.interactable = false;
        btnOpcion3.interactable = false;
        btnOpcion4.interactable = false;
        bool istrue = txtrespuesta4.text.Equals(respuestaCorrecta);
        enviarSeñalMP(istrue);
    }
    //______________________________________________________________________

    //logica
    public void enviarSeñalMP(bool istrue)
    {
        GameControllerPregunta gcseñalenviada = GameObject.FindObjectOfType<GameControllerPregunta>();
        if(gcseñalenviada != null)
        {
            gcseñalenviada.ActualizarStatsJuego(istrue);
        }
    }
    
}
