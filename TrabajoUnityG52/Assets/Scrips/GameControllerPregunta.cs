using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using models;
using System;
using System.IO;
using TMPro;
using Unity.VisualScripting;


public class GameControllerPregunta : MonoBehaviour
{

    //Para traer esos OBJETOS GamePregunta debo crear una lista de GameObjects para poder trabajar con ellos
    public List<GameObject> listaGamePreguntas;
    //Crear un objeto de TipoGameObject para leer los demas scripts
    GameObject controllSelected;

    //CantidadDePreguntasQue se Generaran
    public int faciles = 7;
    public int dificiles = 7;

    //Contador
    private int contador;

    //Paneles
    public GameObject Panel_Inicio;
    public GameObject PanelPM;
    public GameObject PanelFV;
    public GameObject PanelAB;
    public GameObject PanelResultados;

    //Contadores Aciertos y Fallos
    public TextMeshProUGUI txtFallos,txtAciertos;
    //Contadores de Score y Fallos
    private int contadorPuntos,contadorFallos;


    void Start()
    {
        contador = 0;
        contadorPuntos = 0;
        contadorFallos = 0;
        txtAciertos.gameObject.SetActive(false);
        txtFallos.gameObject.SetActive(false);
    }
  
    public void Iniciar()
    {
        txtAciertos.gameObject.SetActive(true);
        txtFallos.gameObject.SetActive(true);
        System.Random random = new System.Random();
        int i = random.Next(listaGamePreguntas.Count);
        controllSelected = listaGamePreguntas[i];
        Debug.Log("IndiceGenerado" + i);

        txtAciertos.gameObject.SetActive(true);
        txtFallos.gameObject.SetActive(true);
        bool faseinicial = (contador < faciles);
        if (controllSelected.GetComponent<LeerPreguntasMultiples>()!=null)
        {
            Panel_Inicio.SetActive(false);
            PanelFV.SetActive(false);
            PanelPM.SetActive(true);
            PanelAB.SetActive(false);
            PanelResultados.SetActive(false);
            LeerPreguntasMultiples controllerM = controllSelected.GetComponent<LeerPreguntasMultiples>();
            if (faseinicial)
            {
                controllerM.mostrarPreguntaFacil();
            }

            else
            {
                controllerM.mostrarPreguntaDificil();
            }
                
        }

        else if (controllSelected.GetComponent<LeerPreguntasAbiertas>() != null)
        {
            Panel_Inicio.SetActive(false);
            PanelFV.SetActive(false);
            PanelPM.SetActive(false);
            PanelAB.SetActive(true);
            PanelResultados.SetActive(false);

            LeerPreguntasAbiertas controllerM = controllSelected.GetComponent<LeerPreguntasAbiertas>();
            if (faseinicial)
            {
                controllerM.mostrarPreguntaFacilAB();
            }

            else
            {
                controllerM.mostrarPreguntaDificilAB();
            }
        }
        else if(controllSelected.GetComponent<LeerPreguntasFalsoVerdadero>()!=null)
        {
            Panel_Inicio.SetActive(false);
            PanelFV.SetActive(true);
            PanelPM.SetActive(false);
            PanelAB.SetActive(false);
            PanelResultados.SetActive(false);

            LeerPreguntasFalsoVerdadero controllerM = controllSelected.GetComponent<LeerPreguntasFalsoVerdadero>();
            if (faseinicial)
            {
                controllerM.mostrarPreguntaFacil();
            }

            else
            {
                controllerM.mostrarPreguntaDificil();
            }
        }
    }

    #region ActualizarStatsDelJuego En Base a Pregunta Acertada o no
    public void ActualizarStatsJuego(bool istrue)
    {
        if (istrue)
        {
            contadorPuntos++;
            
            Debug.Log("Respuesta correcta");
        }
        else
        {
            contadorFallos++;
            Debug.Log("Respuesta incorrecta");
        }

        ActualizarScore();
        contador++;

        if (contador >= faciles + dificiles)
        {
            PanelResultados.SetActive(true);
            MostrarResultados();
        }
        else
        {
            Invoke("Iniciar", 0.5f);
        }
    }


    public void SeñalRecibida_AB(bool istrue)
    {
        if (contador>=faciles+dificiles)
        {
            MostrarResultados();
        }
        else
        {
            Invoke("Iniciar", 0.5f);
        }
    }
    #endregion

    #region MostrarResultadosFinales
    public void MostrarResultados()
    {
        PanelResultados.SetActive(true);
        txtFallos.text = "Puntos Totales : "+contadorFallos;
        txtAciertos.text= "Tus Fallos: " + contadorPuntos;
        Timer paratimer = GameObject.FindObjectOfType<Timer>();
        paratimer.TimerStop();
    }

    public void ActualizarScore()
    {
        txtAciertos.text ="Puntos: "+ contadorPuntos;
        txtFallos.text = "Fallos: " + contadorFallos;
    }
    #endregion

    #region Volver Inicio
    public void ReiniciarJuego()
    {
        contador = 0;
        contadorPuntos = 0;
        contadorFallos = 0;
        txtAciertos.gameObject.SetActive(false);
        txtFallos.gameObject.SetActive(false);
        txtAciertos.text = "Puntos: ";
        txtFallos.text = "Fallos: ";


        Panel_Inicio.SetActive(true);
        PanelPM.SetActive(false);
        PanelFV.SetActive(false);
        PanelAB.SetActive(false);

        PanelResultados.SetActive(false);
    }
    #endregion
}
