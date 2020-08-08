using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableActionScript : MonoBehaviour
{
    public float _timeLimit;
    public float _currentTime;
    
    public CrabStatus CurrentCrabStatus;
    public bool IsCarryingKey;
    public List<CollectableBehaviorScript.CollectableType> _collectedTypes;

    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collectedTypes = new List<CollectableBehaviorScript.CollectableType>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTime > 0 && CurrentCrabStatus == CrabStatus.Invincible)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            CurrentCrabStatus = CrabStatus.Normal;
            _currentTime = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            CheckCollisionType(collision);
        }
    }

    private void CheckCollisionType(Collision collision)
    {
        CollectableBehaviorScript collidedBehavior = collision.gameObject.GetComponent<CollectableBehaviorScript>();

        if(collidedBehavior != null)
        {
            switch (collidedBehavior.CurrentCollectableType)
            {
                case CollectableBehaviorScript.CollectableType.Weapon:
                    CurrentCrabStatus = CrabStatus.Invincible;
                    _currentTime = _timeLimit;
                    break;
                case CollectableBehaviorScript.CollectableType.Jam:
                    IsCarryingKey = true;
                    break;
            }
        }
    }

    public enum CrabStatus
    {
        Normal,
        Invincible
    }
}
