using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviorScript : MonoBehaviour
{
    public CollectableType CurrentCollectableType;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Collectable animation script goes here?
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && this.enabled)
        {
            this.enabled = false;
            _renderer.enabled = false;
            _rigidbody.detectCollisions = false;
            _rigidbody.isKinematic = false;

            CollectableActionScript collectableAction = collision.gameObject.GetComponent<CollectableActionScript>();
            AddToPlayer(collectableAction);
        }
    }

    private void AddToPlayer(CollectableActionScript collectableAction)
    {
        if(collectableAction != null)
        {
            collectableAction._collectedTypes.Add(CurrentCollectableType);
        }
    }

    public enum CollectableType
    {
        Weapon,
        Jam
    }
}
