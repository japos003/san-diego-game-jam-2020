using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMovementScript : MonoBehaviour
{
    public float _movementDirection;
    public float _jumpForce;
    public CrabJumpStatus _crabJumpStatus;

    public Vector3 _originalPosition;

    private Rigidbody _rigidbody;

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

        if (_jumpForce == 0f) { _jumpForce = 250f; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = _movementDirection * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
              _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
              _crabJumpStatus = CrabJumpStatus.Jump;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            _movementDirection = -_movementDirection;

            if(_crabJumpStatus == CrabJumpStatus.Jump)
            {
                _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
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
            print("Goal Reached!");
        }
        else
        {
            _movementDirection = -_movementDirection;
        }
    }

    public void PlaceInOriginalPosition()
    {
        Transform currTransform = GetComponent<Transform>();
        currTransform.localPosition= _originalPosition;
        transform.localPosition = _originalPosition;
    }

    public enum CrabJumpStatus
    {
        Floor,
        Jump
    }
}
