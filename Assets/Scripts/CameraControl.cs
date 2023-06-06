using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{

    private static CameraControl instance = null;
    public static CameraControl Instance { get => instance; set => instance = value; }

    PlayerCollision playerCollision;

    private GameObject _playerObject;

    private GameObject _parent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField]
    private Vector3 offSet;

    [SerializeField]
    private Transform objectToFollow;

    private Vector3 cameraNewPos;
    private Vector3 posFollow;
    private float cameraRotateValue;

    private void Start()
    {
        playerCollision = PlayerCollision.Instance;
        _playerObject = playerCollision.gameObject;
        _parent = _playerObject.transform.parent.gameObject;
    }



    private void FixedUpdate()
    {
        posFollow = objectToFollow.position;
        cameraNewPos = posFollow + offSet;
        transform.position = Vector3.Lerp(transform.position, cameraNewPos, 0.5f);
        cameraRotateValue = transform.localEulerAngles.x;
    }

    public void ZoomOut()
    {
         
        cameraRotateValue =  0.001f * _parent.transform.childCount;
        offSet = new Vector3(offSet.x , offSet.y + cameraRotateValue, offSet.z - cameraRotateValue);
        
    }

}
