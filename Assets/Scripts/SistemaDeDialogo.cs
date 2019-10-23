using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Cinemachine;
public class SistemaDeDialogo : MonoBehaviour {
    public static SistemaDeDialogo singleton;
    public GameObject panel;
    public Text texto;
    public Text persona;
    public Queue<Linea> oraciones = new Queue<Linea>();
    public GameObject[] camarasVirtuales;
    public GameObject menacing;
    public DeepFry deepFry;
    public int camaraActual = 1;
    public AudioSource audioSource;
    string textoActual;
    bool cambiando = false;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        } else if (singleton != this)
        {
            Destroy(gameObject);
        }
    }

    public void PasarLinea()
    {
        if (oraciones.Count > 0)
        {
            if(panel.activeInHierarchy == false)
            {
                panel.SetActive(true);
            }

            if (!cambiando)
            {
                cambiando = true;
                Linea lineaActual = oraciones.Dequeue();
                InterpretarLinea(lineaActual);
            }
            else
            {
                StopAllCoroutines();
                texto.text = textoActual;
                cambiando = false;
            }
        }
        else
        {
            if(cambiando)
            {
                StopAllCoroutines();
                texto.text = textoActual;
                cambiando = false;
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    void InterpretarLinea(Linea lineaActual)
    {
        Debug.Log("P");
        lineaActual.acciones.Invoke();
        textoActual = lineaActual.oracion;
        persona.text = lineaActual.persona;
        if (lineaActual.cambiarACamara != 0)
        {
            camarasVirtuales[camaraActual - 1].SetActive(false);
            camarasVirtuales[lineaActual.cambiarACamara - 1].SetActive(true);
            camaraActual = lineaActual.cambiarACamara;
        }
        //if (lineaActual.sonido != null)
        //{
        //    audioSource.Stop();
        //    audioSource.clip = lineaActual.sonido;
        //    audioSource.Play();
        //}
        menacing.SetActive(lineaActual.menacing);
        if (menacing.activeInHierarchy)
        {
            menacing.GetComponent<VideoPlayer>().Play();
        }
        else
        {
            menacing.GetComponent<VideoPlayer>().Stop();
        }
        //deepFry.enabled = lineaActual.deepFry;

        StartCoroutine(EscribirOracion(textoActual, lineaActual.autoSkip));
    }

    IEnumerator EscribirOracion(string oracion, bool autoSkip=false)
    {
        texto.text = "";
        foreach (char letra in oracion.ToCharArray())
        {
            texto.text += letra;
            yield return null;
        }
        cambiando = false;
        if (autoSkip)
        {
            yield return new WaitForSeconds(1);
            PasarLinea();
        }
    }

    public void MeterDialogo(Linea dialogo)
    {
        bool estabaVacia = oraciones.Count == 0;
        oraciones.Enqueue(dialogo);
        if(estabaVacia)
            PasarLinea();
    }
}
