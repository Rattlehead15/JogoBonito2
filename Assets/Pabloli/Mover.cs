using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Mover : MonoBehaviour {
    [HideInInspector]
    public float horizontal = 0.0f;
    [HideInInspector]
    public float vertical = 0.0f;
    [HideInInspector]
    public bool corriendo = false;
    [HideInInspector]
    public bool salto = false;
    [HideInInspector]
    public bool bailando = false;
    public float distanciaPiso;
    public float poderSalto;
    public float gravedad = 3.0f;
    public float velocidad = 1.0f;
    public float maxVel = 3.0f;
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
        Moverse();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        salto = Input.GetButtonDown("Jump");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("Baile");
            bailando = true;
        }
        if (horizontal < 0.1f && vertical < 0.1f)
        {
            rigido.velocity = new Vector3(0, rigido.velocity.y, 0);
        }
        else if(horizontal < 0.1f)
        {
            rigido.velocity = new Vector3(0, rigido.velocity.y, rigido.velocity.z);
        }
        else if(vertical < 0.1f)
        {
            rigido.velocity = new Vector3(rigido.velocity.x, rigido.velocity.y, 0);
        }
    }

    void Moverse()
    {
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
        if (direccion != Vector3.zero)
            transform.forward = direccion;
        anim.SetBool("Corriendo", corriendo);
        anim.SetFloat("Vertical", Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
        float vnueva = corriendo ? velocidad * 1.3f : velocidad;
        if (direccion != Vector3.zero && !bailando)
            rigido.velocity = new Vector3(direccion.x * vnueva, rigido.velocity.y, direccion.z * vnueva);
        else
            rigido.velocity = new Vector3(0, rigido.velocity.y, 0);
    }

    bool EnElPiso()
    {
        return Physics.Raycast(transform.position + new Vector3(0,distanciaPiso,0), -transform.up, distanciaPiso + 0.3f);
    }
}
