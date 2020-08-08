using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private void Awake()
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
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckPlayer(collision.gameObject);
        }
    }

    private void CheckPlayer(GameObject player)
    {
        CollectableActionScript collectableAction = player.GetComponent<CollectableActionScript>();

        if(collectableAction != null)
        {
            DetermineDestiny(collectableAction);
        }
    }

    private void DetermineDestiny(CollectableActionScript collectableAction)
    {
        if(collectableAction.CurrentCrabStatus == CollectableActionScript.CrabStatus.Invincible)
        {
            DestroyEnemy();
        }

        // TODO: Disable enemy or game over?
    }

    private void DestroyEnemy()
    {
        _renderer.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.detectCollisions = false;
    }
}
