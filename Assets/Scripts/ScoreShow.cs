using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreShow : MonoBehaviour
{

    CameraControl cameraControl;

    private void Start()
    {
        
        cameraControl = CameraControl.Instance;
    }


    void Update()
    {
        transform.LookAt(cameraControl.transform.position);
    }
}
