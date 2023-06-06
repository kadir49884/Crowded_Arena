using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class NeutralControl : BasePlayerControl
{
    [SerializeField]
    private GameObject _neutralBody;

    private bool _instantiateControl;

    private float _distanceValue = 2;
    private float distance = 0;
    private float _colliderDistance;
    private int _playerCount = 0;


    private bool _playerCrash = false;
    [SerializeField, ReadOnly]
    private bool _fakeCrash = false;
    private bool _isCrashed = false;
    private bool _neutralInPlayer = true;


    private GameObject followObject;
    private GameObject rotaterObject;
    private CapsuleCollider _capsulControl;
    private PlayerCollision playerCollision;
    private Material _bodyMaterial;
    private Quaternion _newRotate;
    private bool inArena = false;
    private ObjectManager objectManager;
    private GameObject arenaObject;
    private GameObject _parentFirstChild;
    private int _playerCountWithParent = 0;

    private bool _fight = false;

    public bool IsCrashed { get => _isCrashed; set => _isCrashed = value; }
    public bool PlayerCrash { get => _playerCrash; set => _playerCrash = value; }
    public bool FakeCrash { get => _fakeCrash; set => _fakeCrash = value; }
    public bool InstantiateControl { get => _instantiateControl; set => _instantiateControl = value; }

    //private void Awake()
    //{
    //    _instantiateControl = false;
    //}

    protected override void Start()
    {
        base.Start();
        anim.SetBool("Walk", true);
        playerCollision = PlayerCollision.Instance;
        _bodyMaterial = _neutralBody.GetComponent<Renderer>().material;
        _capsulControl = GetComponent<CapsuleCollider>();
        speed = 1;
        objectManager = ObjectManager.Instance;
        arenaObject = objectManager.ArenaPoint;
        _playerCountWithParent = 99;
    }

  

    protected override void FixedUpdate()
    {
        DistanceControl();
        base.FixedUpdate();
    }

    private void DistanceControl()
    {
        if (IsCrashed)
        {
            distance = Vector3.Distance(transform.position, followObject.transform.position);
            if (!inArena)
            {
                if (distance > _distanceValue)
                {
                    speed = 9;
                    _neutralInPlayer = false;
                }
                else if (distance < _distanceValue)
                {
                    speed = 5.9f;
                    _neutralInPlayer = true;
                }

                if (distance > _colliderDistance)
                {
                    _capsulControl.enabled = false;
                }
                else if (!_capsulControl.enabled)
                {
                    _capsulControl.enabled = true;
                }
            }

            if ((!_neutralInPlayer || inArena) && !_fight)
            {
                transform.LookAt(followObject.transform.position);
            }
            else if(!_fight)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, rotaterObject.transform.localRotation, 0.1f);
            }

        }
        _newRotate = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        transform.localRotation = _newRotate;
    }


    // 1
    private void OnTriggerEnter(Collider other)
    {
        GameObject _other = other.gameObject;

        if (!IsCrashed && (_other.GetComponent<BaseCollision>()?.MoneyCount > 0 || InstantiateControl) && !inArena)
        {
            _other.GetComponent<PlayerCollision>()?.CrashMePlayer(gameObject);
            _other.GetComponent<FakeCollision>()?.CrashMeFake(gameObject);
            _other.GetComponent<BaseCollision>()?.CrashNeutral(gameObject);

            if (!InstantiateControl)
            {
                _other.GetComponent<BaseCollision>().MoneyCount--;
            }

            playerCollision.WriteMoneyCount();
            _parentFirstChild = transform.parent.GetChild(0).gameObject;
            _capsulControl.isTrigger = false;
        }

        if (IsCrashed && _parentFirstChild != null && !inArena)
        {
            ShowScore(_parentFirstChild);
        }

        if (IsCrashed && _other.GetComponent<BaseCollision>()?.PlayerCountWithMe > _playerCountWithParent && !inArena)
        {

            _other.GetComponent<BaseCollision>()?.CrashNeutral(gameObject);
            _other.GetComponent<BaseCollision>()?.UpdateBoxSize();

            _parentFirstChild = transform.parent.GetChild(0).gameObject;
            _parentFirstChild.GetComponent<PlayerCollision>()?.CrashMePlayer(gameObject);
            _parentFirstChild.GetComponent<FakeCollision>()?.CrashMeFake(gameObject);
        }

    }

    private void ShowScore(GameObject newGameObject)
    {
        newGameObject.GetComponent<BaseCollision>()?.UpdatePlayerCount();

    }

    //3
    public void ToIntoCrashObject(GameObject newGameObject, Material newMat)
    {
        transform.parent = newGameObject.transform.parent;
        rotaterObject = newGameObject;

        if (PlayerCrash)
        {
            followObject = newGameObject.transform.GetChild(0).gameObject;
        }
        else if (FakeCrash && IsCrashed)
        {
            followObject = newGameObject.transform.GetChild(0).gameObject;
        }
        else if (FakeCrash)
        {
            followObject = newGameObject.transform.parent.GetChild(1).gameObject;
        }

        newGameObject.gameObject.GetComponent<BaseCollision>()?.UpdateBoxSize();
        ShowScore(newGameObject);

        _neutralBody.GetComponent<Renderer>().material = newMat;

        rb.mass = 1;
        rb.drag = 0;
        anim.SetBool("Run", true);
        _isCrashed = true;
    }

    //6
    public void UpdatePlayerCountParent(float newDistance, int newPlayerCount, int _newCountWithParent)
    {
        _playerCountWithParent = _newCountWithParent;
        _playerCount = newPlayerCount;
        _distanceValue = newDistance;
        _colliderDistance = newDistance * 5;

    }
    public void ChangeRotation()
    {
        if (!PlayerCrash && !FakeCrash)
        {
            Quaternion newRotate = transform.rotation;
            newRotate.y += 180;
            transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f, RotateMode.Fast).SetRelative(true);
        }
    }

    public void PlayerGoArena(GameObject newObject)
    {
        if (IsCrashed && PlayerCrash)
        {
            newObject.GetComponent<DoorPlayer>().InArenaPlayerCount++;
            CommonGoArena();
            
        }
    }

    public void FakeGoArena(GameObject newObject)
    {
        if (IsCrashed && FakeCrash)
        {
            newObject.GetComponent<DoorFake>().InArenaFakeCount++;
            CommonGoArena();
        }
    }

    private void CommonGoArena()
    {
        speed = 8f;
        followObject = arenaObject;
        _capsulControl.isTrigger = true;

        if (_parentFirstChild != null)
        {
            transform.parent.GetChild(0).GetComponent<BaseCollision>().ArenaPlayerCount++;
            transform.parent.GetChild(0).GetComponent<BaseCollision>().UpdatePlayerCount();
        }
        inArena = true;

    }

    public void TriggerClose()
    {
        _capsulControl.isTrigger = false;
    }


    public void PlayerGoArenaPoint()
    {
        CommonArenaCenter();
    }

    public void FakeGoArenaPoint()
    {
        CommonArenaCenter();
        transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
    }

    private void CommonArenaCenter()
    {
        if (IsCrashed)
        {
            speed = 0;
            anim.SetBool("Run", false);
            _fight = true;
        }
    }


}
