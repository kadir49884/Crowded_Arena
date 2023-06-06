using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerCollision : BaseCollision
{

    private static PlayerCollision instance = null;
    public static PlayerCollision Instance { get => instance; set => instance = value; }
    public int FakeCount { get => fakeCount; set => fakeCount = value; }

    [SerializeField]
    private Text moneyText;

    private int fakeCount = 0;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();
    }


    public void CrashMePlayer( GameObject newGameObject)
    {
        newGameObject.GetComponent<NeutralControl>().PlayerCrash = true;
        newGameObject.GetComponent<NeutralControl>().FakeCrash = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MoneyControl>()?.GetCollisionMoney();
        other.GetComponent<WallScript>()?.ReduceChangeOpacity();
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<WallScript>()?.PlusChangeOpacity();
    }
    

    public void WriteMoneyCount()
    {
        moneyText.text = MoneyCount.ToString();
    }

}
