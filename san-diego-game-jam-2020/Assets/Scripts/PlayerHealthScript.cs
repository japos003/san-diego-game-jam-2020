using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int HealthStatus;
    public int MaxHealthStatus;
    public int CollisionHitPoints;
    public float DisabledTime;
    public float CurrentDisabledTime;

    private Rigidbody _rigidBody;
    private CollectableActionScript _collectableActionScript;

    void Awake()
    {
        _collectableActionScript = GetComponent<CollectableActionScript>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(HealthStatus <= 0)
        {
            DisablePlayerWithinTimeLimit();
            UpdateDisabledTime();
        }
        // TODO: Determine if the player should be able to control the crab again?
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(IsEnemyOrObjectile(collision) && _collectableActionScript.CurrentCrabStatus != CollectableActionScript.CrabStatus.Invincible)
        {
            // TODO: Should it depend on each type of enemy and objectile?
            HealthStatus -= CollisionHitPoints;
        }

        if(HealthStatus < 0)
        {
            HealthStatus = 0;
            DisablePlayerWithinTimeLimit();
        }
    }

    private bool IsEnemyOrObjectile(Collision collision)
    {
        return collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Objectile";
    }

    private void UpdateDisabledTime()
    {
        CurrentDisabledTime -= Time.deltaTime;

        if(CurrentDisabledTime <= 0)
        {
            HealthStatus = MaxHealthStatus;
        }
    }

    private void DisablePlayerWithinTimeLimit()
    {
        // TODO: Disable player while health is below level?
        CurrentDisabledTime = DisabledTime;
    }
}
