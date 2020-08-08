using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableActionScript : MonoBehaviour
{
    public float _timeLimit;
    public float _currentTime;
    
    public CrabStatus CurrentCrabStatus;

    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            CurrentCrabStatus = CrabStatus.Invincible;
            _currentTime = _timeLimit;
        }
    }

    public enum CrabStatus
    {
        Normal,
        Invincible
    }
}
