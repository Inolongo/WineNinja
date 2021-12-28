using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private const float MINSpeed = 13;
    private const float MAXSpeed = 16;
    private const float MAXTorque = 4;

    [SerializeField] private int pointValue;
    [SerializeField] private TargetType targetType;
    
    
    public TargetType TargetType => targetType;
    public Action Spawned { get; set; }
    public event Action<Target> TargetFallen;
    public event Action TargetClicked;
    public event Action Stolknulsya;

    public ParticleSystem explosionParticle;
    private Rigidbody _targetRb;

    private void Awake()
    {
        GameManager.Instance.GameEnded += OnGameEnded;
        
        _targetRb = GetComponent<Rigidbody>();
        Spawned += AddImpulse;
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -6 || gameObject.transform.position.y > 10.5 )
        { 
            TargetFallen?.Invoke(this);

            TargetsPool.Instance.ReturnTargetToPool(this);
        }
    }

    private void OnDisable()
    {
        Reset();
    }

    private void OnDestroy()
    {
        GameManager.Instance.GameEnded -= OnGameEnded;
    }

    private void AddImpulse()
    {
        _targetRb.AddForce(Vector3.up * Random.Range(MINSpeed, MAXSpeed), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    private void Reset()
    {
        TargetClicked = null;
        TargetFallen = null;
        _targetRb.velocity = Vector3.zero;
    }

    private void OnMouseDown()
    {
        Instantiate(explosionParticle, transform.position,
            explosionParticle.transform.rotation);
        GameManager.Instance.UpdateScore(pointValue);
        TargetClicked?.Invoke();
        TargetsPool.Instance.ReturnTargetToPool(this);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if(other.gameObject.GetComponents<Target>() is null) return;
    //     
    //     TargetsPool.Instance.ReturnTargetToPool(this);
    //     
    //     Debug.Log("Collision");
    //
    //     Stolknulsya?.Invoke();
    // }

    private void OnGameEnded()
    {
        Debug.Log("GameEnded");

       TargetsPool.Instance.ReturnTargetToPool(this);
    }


    float RandomTorque()
    {
        return Random.Range(-MAXTorque, MAXTorque);
    }
}

public enum TargetType
{
    None,
    Pizza,
    Bomb,
    Byhlo,
    Cookie
}
