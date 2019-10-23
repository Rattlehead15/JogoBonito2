using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugar : MonoBehaviour {
    public GameObject jugador;

    public void OnEnable()
    {
        jugador.GetComponent<Moverse>().enabled = true;
    }

    public void OnDisable()
    {
        jugador.GetComponent<Moverse>().enabled = false;
    }
}
