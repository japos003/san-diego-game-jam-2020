﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrabMovementScript : MonoBehaviour
{
    public float _movementDirection;
    public float _jumpForce;
    public float _animationTime;
    public CrabJumpStatus _crabJumpStatus;

    public float _coolOffTime;
    public bool IsControllable;

    public Vector3 _originalPosition;

    private Rigidbody _rigidbody;
    private float _pausedSeconds;
    private float _currentCoolOffTime;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_movementDirection == 0)
        {
            _movementDirection = 1.0f;
        }
        _currentCoolOffTime = _coolOffTime;
        if (_jumpForce == 0f) { _jumpForce = 250f; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_pausedSeconds >= 0.0f)
        {
            _pausedSeconds -= Time.deltaTime;
        }
        else if(_currentCoolOffTime >= 0.0f)
        {
            _currentCoolOffTime -= Time.deltaTime;
        }
        else
        {
            _pausedSeconds = 0;
            MoveCrab();
        }
    }

    private void MoveCrab()
    {
        float movement = _movementDirection * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 0);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Jump")) && _crabJumpStatus == CrabJumpStatus.Floor)
        {
            _rigidbody.velocity += new Vector3(0, _jumpForce, 0);
            _crabJumpStatus = CrabJumpStatus.Jump;
        } 
    }

    public void PauseMovementDuringSecond(float seconds)
    {
        _pausedSeconds = seconds;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            _movementDirection = -_movementDirection;

            if(_crabJumpStatus == CrabJumpStatus.Jump)
            {
                _rigidbody.velocity += new Vector3(0, _jumpForce, 0);
            }
        }

        if (collision.gameObject.tag == "Floor")
        {
            _crabJumpStatus = CrabJumpStatus.Floor;
        }

        if(collision.gameObject.tag == "Goal")
        {
            DetermineIfGoalReached(collision.gameObject);
        }
    }

    public void DetermineIfGoalReached(GameObject gameObject)
    {
        CollectableActionScript actionScript = GetComponent<CollectableActionScript>();

        if(actionScript != null && actionScript.IsCarryingKey)
        {
            // TODO: Go to next scene?
            StartCoroutine(PauseBeforeMovingToNextLevel());
        }
        else
        {
            _movementDirection = -_movementDirection;
        }
    }

    private IEnumerator PauseBeforeMovingToNextLevel()
    {

        yield return new WaitForSeconds(3);
        MoveToNextLevel();
    }

    public void MoveToNextLevel()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }

    public void PlaceInOriginalPosition()
    {
        transform.localPosition = _originalPosition;
        _currentCoolOffTime = _coolOffTime;
    }

    public enum CrabJumpStatus
    {
        Floor,
        Jump
    }
}
