using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyControl : MonoBehaviour
{
    private PlayerCollision playerCollision;

    private ParticleSystem moneyParticle;

    private ObjectManager objectManager;
    
    private void Start()
    {
        playerCollision = PlayerCollision.Instance;
        objectManager = ObjectManager.Instance;
        moneyParticle = objectManager.MoneyParticleObject.GetComponent<ParticleSystem>();
    }

    public void GetCollisionMoney()
    {
        playerCollision.MoneyCount++;
        playerCollision.WriteMoneyCount();
        Instantiate(moneyParticle, gameObject.transform);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Invoke(nameof(DestroyMoney), 1f);
    }

    private void DestroyMoney()
    {
        Destroy(gameObject);
    }

  

}
