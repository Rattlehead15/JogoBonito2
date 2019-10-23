using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionesGlobales : MonoBehaviour {
    DeepFry deepFry;
    public AudioSource fuente;
	// Use this for initialization
	void Start () {
        deepFry = Camera.main.GetComponent<DeepFry>();
	}

	// Update is called once per frame
	public void DeepFryOn () {
        Debug.Log("pija ");
        deepFry.enabled = true;
	}

    public void DeepFryOff()
    {
        Debug.Log("y re contra pija");
        deepFry.enabled = false;
    }

    public void Reproducir(AudioClip sonido)
    {
        fuente.clip = sonido;
        fuente.Play();
    }
}
