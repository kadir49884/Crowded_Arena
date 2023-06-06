using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance = null;

    public static ObjectManager Instance { get => instance; set => instance = value; }
    public GameObject NeutralObject { get => neutralObject; set => neutralObject = value; }
    public GameObject ParticleObject { get => particleObject; set => particleObject = value; }
    public GameObject MoneyParticleObject { get => moneyParticleObject; set => moneyParticleObject = value; }
    public GameObject ArenaPoint { get => arenaPoint; set => arenaPoint = value; }

    [SerializeField]
    private GameObject neutralObject;
    [SerializeField]
    private GameObject particleObject;
    [SerializeField]
    private GameObject moneyParticleObject;
    [SerializeField]
    private GameObject arenaPoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    

}
