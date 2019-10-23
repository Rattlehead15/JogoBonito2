using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class Linea {
    public string persona;
    [TextArea(3,10)]
    public string oracion;
    public UnityEvent acciones;
    public AudioClip sonido;
    public int cambiarACamara = 0;
    public bool deepFry = false;
    public bool menacing = false;
    public bool autoSkip = false;
}