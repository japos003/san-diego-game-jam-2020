using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMovementScript : MonoBehaviour
{
    public float _movementDirection = 1.0f;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float movement = _movementDirection * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 0);
    }
}
