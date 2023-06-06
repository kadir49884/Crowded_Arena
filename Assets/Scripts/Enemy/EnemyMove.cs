using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent nMesh;

    [SerializeField]
    private Transform[] targetPos;
    private int i = 0;

    void Start()
    {
        nMesh = GetComponent<NavMeshAgent>();

        nMesh.SetDestination(targetPos[i].position);

    }

    public void ChangetTargetPos()
    {
        i++;
        if(i > 4)
        {
            i = 0;
        }
        nMesh.SetDestination(targetPos[i].position);
    }


}
