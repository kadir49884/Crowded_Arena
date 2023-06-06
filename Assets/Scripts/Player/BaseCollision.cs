using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using static UnityEngine.ParticleSystem;

public class BaseCollision : MonoBehaviour
{

    [SerializeField]
    protected GameObject materialObject;
    [SerializeField]
    protected GameObject distanceObject;
    [SerializeField]
    protected Text scoreText;

    protected float _colliderSizeX = 0;
    protected float _colliderSizeZ = 0;
    protected float _followDistance;
    protected float _distanceValueBase = 2f;
    protected float _boxCenterDistance;
    protected int _moneyCount = 0;

    protected CameraControl cameraControl;
    protected ObjectManager objectManager;
    protected Material _mat;
    
    protected GameObject _other;
    protected GameObject _particleObject;
    protected GameObject _transientParticle;

    private BoxCollider boxColliderObject;
    private ParticleSystem _particle1;
    private ParticleSystem _particle2;
    private MainModule main1;
    private MainModule main2;

    [SerializeField, ReadOnly]
    protected int _myPlayerCount = 1;

    [SerializeField, ReadOnly]
    protected int _arenaPlayerCount = 0;

    [SerializeField, ReadOnly]
    private int _playerCountWithMe = 0;


    public int MyPlayerCount { get => _myPlayerCount; set => _myPlayerCount = value; }
    public int MoneyCount { get => _moneyCount; set => _moneyCount = value; }
    public int ArenaPlayerCount { get => _arenaPlayerCount; set => _arenaPlayerCount = value; }
    public int PlayerCountWithMe { get => _playerCountWithMe; set => _playerCountWithMe = value; }

    

    protected virtual void Start()
    {
        boxColliderObject = GetComponents<BoxCollider>()[0];
        _mat = materialObject.GetComponent<Renderer>().material;
        cameraControl = CameraControl.Instance;
        objectManager = ObjectManager.Instance;
        scoreText.text = "1";
        scoreText.color = _mat.color;
        _particleObject = objectManager.ParticleObject;
    }

    

    //2
    public void CrashNeutral(GameObject newGameObject)
    {
        newGameObject.GetComponent<NeutralControl>()?.ToIntoCrashObject(gameObject, _mat);
        UpdatePlayerCount();

        _transientParticle = Instantiate(_particleObject, newGameObject.transform);
        Destroy(_transientParticle.gameObject, 1.5f);
        
        _particle1 = _transientParticle.GetComponent<ParticleSystem>();
        _particle2 =_transientParticle.transform.GetChild(0).GetComponent<ParticleSystem>();

        main1 = _particle1.main;
        main1.startColor = _mat.color;
        main2 = _particle2.main;
        main2.startColor = _mat.color;

        if (newGameObject.GetComponent<NeutralControl>()?.PlayerCrash == true)
        {
            cameraControl.ZoomOut();
        }
        WriteScore();
    }



    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.transform.parent.childCount < 3)
        //{
        //    DestroyFake(other.gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.transform.parent.childCount < 3)
        //{
        //    DestroyFake(other.gameObject);
        //}
    }

    //private void DestroyFake(GameObject newGameObject)
    //{
    //    newGameObject.GetComponent<FakeCollision>()?.DestroyFake(transform.parent.childCount, _mat, gameObject);
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.transform.parent.childCount < 3)
    //    {
    //        other.gameObject.GetComponent<FakeCollision>()?.OpenCrashLock();
    //    }
    //}

    //5
    public void UpdatePlayerCount()
    {
        Invoke(nameof(LateUpdatePlayerCount), 0.2f);
    }

    //4
    public void UpdateBoxSize()
    {
        _followDistance = distanceObject.transform.localPosition.z;
        _distanceValueBase = 0.04f * transform.parent.childCount + 2f;
        _followDistance += 0.1f;
        _colliderSizeX = 0.1f * _playerCountWithMe;
        _colliderSizeZ = 0.3f * _playerCountWithMe;
        _boxCenterDistance = -0.02f * _playerCountWithMe;

        Vector3 distancePos = distanceObject.transform.localPosition;
        distanceObject.transform.localPosition = new Vector3(distancePos.x, distancePos.y, _followDistance);
        boxColliderObject.size = new Vector3(_colliderSizeX + 6, 2, _colliderSizeZ + 6);
        boxColliderObject.center = new Vector3(0, 1, _boxCenterDistance);
    }


    private void LateUpdatePlayerCount()
    {
        _myPlayerCount = transform.parent.childCount - 1;
        _playerCountWithMe = _myPlayerCount - _arenaPlayerCount;

        for (int i = 1; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<NeutralControl>()?.UpdatePlayerCountParent(_distanceValueBase, _myPlayerCount, _playerCountWithMe);
        }
        WriteScore();
    }

    private void WriteScore()
    {
        if(_playerCountWithMe < 1)
        {
            _playerCountWithMe = 1;
        }
        scoreText.text = _playerCountWithMe.ToString();
    }


}
