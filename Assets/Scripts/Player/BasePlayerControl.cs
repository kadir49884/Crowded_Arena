using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePlayerControl : MonoBehaviour
{

    protected float speed;
    protected Rigidbody rb;
    Vector3 direction;

    protected Animator anim;

    protected Vector3 setPosY;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        direction = transform.forward;
        rb.velocity = direction * speed;

        setPosY = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        transform.localPosition = setPosY;
    }


}
