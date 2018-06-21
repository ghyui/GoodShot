using UnityEngine;
using System.Collections;
using System;

public class ProtoBall : MonoBehaviour
{
    Rigidbody rb;

    Vector3 initPos;

    // Use this for initialization
    void Start()
	{
        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb != null, "ProtoBall doesn't have Rigidbody component.");

        initPos = transform.position;
    }

    public void Shoot(float power)
    {
        rb.AddForce(new Vector3(0, 1, 1) * power, ForceMode.Impulse);
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = initPos;
    }
}
