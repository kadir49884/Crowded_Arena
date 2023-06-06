using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerControl : BasePlayerControl
{

    private CanvasManager canvasManager;
    private float _eularObject;
    private float _eularStatic = 90;
    private float _horizantal;
    private float _vertical;
    private Quaternion _newRotate;


    protected override void Start()
    {
        
        base.Start();
        anim.SetBool("Run", true);
        canvasManager = CanvasManager.Instance;
        speed = 6;
    }


    protected override void FixedUpdate()
    {
        //rb.velocity = transform.forward * 5;

        if (_vertical > 0)
        {
            _eularObject = _horizantal * _eularStatic;
        }
        else if (_vertical * _horizantal < 0)
        {
            _eularObject = 90 + _vertical * -_eularStatic;
        }
        else if (_vertical * _horizantal > 0)
        {
            _eularObject = 180 + _horizantal * -_eularStatic;
        }

        base.FixedUpdate();
    }
    private void Update()
    {
        _horizantal = canvasManager.JoystickObject.Horizontal;
        _vertical = canvasManager.JoystickObject.Vertical;

        //dir = Vector3.right * _horizantal + Vector3.forward * _vertical;

        //transform.LookAt(rb.velocity);
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        //TODO : transformda sadece y rotasyon alınacak. diğerleri sabit kalacak.

        _newRotate = Quaternion.Euler(0, _eularObject, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _newRotate, 0.3f);
    }
}
