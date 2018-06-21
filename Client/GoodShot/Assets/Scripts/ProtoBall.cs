using UnityEngine;
using System.Collections;
using System;

public class ProtoBall : MonoBehaviour
{
    [SerializeField]
    float turnRatio = 1.0f;

    [SerializeField]
    GameObject arrowObject;

    Rigidbody rb;

    Vector3 initPos;

    public delegate void OnBallStop();
    public OnBallStop onBallStop;

    // Use this for initialization
    void Start()
	{
        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb != null, "ProtoBall doesn't have Rigidbody component.");

        initPos = transform.position;

        rb.freezeRotation = true;
    }

    bool bShot = false;
    public void Shoot(float power)
    {
        rb.freezeRotation = false;

        Debug.LogFormat("shot direction = {0}", transform.rotation * transform.forward);
        rb.AddForce(transform.rotation * transform.forward * power, ForceMode.Impulse);
        bShot = true;
        arrowObject.SetActive(false);
    }

    public void Reset()
    {
        Stop();

        transform.position = initPos;
        onBallStop = null;
        bShot = false;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        arrowObject.SetActive(true);
    }

    public void RotateRight()
    {
        transform.Rotate(0, turnRatio, 0);
    }

    public void RotateLeft()
    {
        transform.Rotate(0, -turnRatio, 0);
    }

    private void Update()
    {
        if(bShot)
        {
            if(rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero)
            {
                OnBallStopped();
            }
        }
    }

    void OnBallStopped()
    {
        if (onBallStop != null)
        {
            onBallStop();
        }

        bShot = false;
        arrowObject.SetActive(true);
        transform.rotation = Quaternion.identity;
    }
}
