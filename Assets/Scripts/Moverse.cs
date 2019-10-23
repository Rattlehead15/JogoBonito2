using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moverse : MonoBehaviour {
    public float Vida = 100.0f;
    [HideInInspector]
    public float horizontal = 0.0f;
    [HideInInspector]
    public float vertical = 0.0f;
    [HideInInspector]
    public bool corriendo = false;
    [HideInInspector]
    public bool poseT = false;
    [HideInInspector]
    public bool disparo = false;
    [HideInInspector]
    public bool salto = false;
    [HideInInspector]
    public bool cam = false;
    bool puedeMoverse = true;
    public float distanciaPiso;
    public float poderSalto;
    public float gravedad = 3.0f;
    public float velocidad = 1.0f;
    public float maxVel = 3.0f;
    public float zoom = 0.3f;
    bool puedeDisparar = false;
    public float velDisparo = 0.4f;
    Rigidbody rigido;
    Animator anim;
    Camera mainCam;

	// Use this for initialization
	void Start () {
        rigido = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        Mover();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void Mover()
    {
        if (!puedeMoverse)
        {
            rigido.velocity = Vector3.zero;
            return;
        }
        if (EnElPiso())
        {
            if (anim.GetBool("Cayendo"))
                anim.SetBool("Cayendo", false);
            if (salto)
            {
                rigido.AddForce(0, poderSalto, 0, ForceMode.Impulse);
            }
        }
        else
        {
            anim.SetBool("Cayendo", true);
            rigido.AddForce(0, -gravedad, 0, ForceMode.Force);
        }
        Vector3 direccion;
        direccion = ((horizontal * mainCam.transform.right) + (vertical * mainCam.transform.forward)).normalized;
        direccion.y = 0;
        direccion.Normalize();
        anim.SetBool("Corriendo", corriendo);
        if (poseT)
        {
            if (!anim.GetBool("OMINOSO"))
            {
                anim.SetBool("OMINOSO", true);
            }
            puedeDisparar = true;
        }
        else
        {
            if (anim.GetBool("OMINOSO"))
            {
                anim.SetBool("OMINOSO", false);
            }
            puedeDisparar = false;
        }
        if (puedeDisparar)
        {
            anim.applyRootMotion = true;
        }
        else
        {
            anim.applyRootMotion = false;
        }
        if (anim.GetBool("OMINOSO"))
        {
            transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation((horizontal == 0 && vertical == 0) ? transform.forward : direccion, Vector3.up);
        }
        anim.SetFloat("Vertical", Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
        float vnueva = corriendo ? velocidad * 1.3f : velocidad;
        rigido.velocity = new Vector3(direccion.x * vnueva, rigido.velocity.y, direccion.z * vnueva);
    }

    bool EnElPiso()
    {
        return Physics.Raycast(transform.position + new Vector3(0,distanciaPiso,0), -transform.up, distanciaPiso + 0.3f);
    }
}
