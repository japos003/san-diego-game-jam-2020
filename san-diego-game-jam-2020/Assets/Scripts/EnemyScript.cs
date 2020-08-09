using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject _jarIcon;
    public float _animationTime;

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
            DetermineDestiny(player, collectableAction);
        }
    }

    private void DetermineDestiny(GameObject player, CollectableActionScript collectableAction)
    {
        if(collectableAction.CurrentCrabStatus == CollectableActionScript.CrabStatus.Invincible)
        {
            DestroyEnemy(player);
        }
        else
        {
            ShowCutscene();
            //TODO: Display cutscene, then place crab back at original point
            CrabMovementScript script = player.GetComponent<CrabMovementScript>();
            if(script != null)
            {
                script.PlaceInOriginalPosition();
            }

        }
    }

    private void ShowCutscene()
    {
        // TODO: Show cutscene here
    }

    private void DestroyEnemy(GameObject player)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.detectCollisions = false;

        CollectableActionScript action = player.GetComponent<CollectableActionScript>();
        action._collectedTypes.Add(CollectableBehaviorScript.CollectableType.Jam);
        action.IsCarryingKey = true;

        CrabMovementScript movementScript = player.GetComponent<CrabMovementScript>();
        movementScript.PauseMovementDuringSecond(_animationTime);

        StartCoroutine(ExecuteAnimation());

        _jarIcon.SetActive(true);
    }

    private IEnumerator ExecuteAnimation()
    {
        // TODO: Implement Animation event here.

        yield return new WaitForSeconds(_animationTime);
        _renderer.enabled = false;
    }
}
