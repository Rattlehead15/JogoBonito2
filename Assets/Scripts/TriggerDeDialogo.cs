using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeDialogo : MonoBehaviour {
    public Linea[] conversacion;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (Linea linea in conversacion)
            {
                SistemaDeDialogo.singleton.MeterDialogo(linea);
            }
        }
    }
}
