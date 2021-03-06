using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

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

    public bool IsShooting { get { return bShot; } }
    bool bShot = false;
    public void Shoot(float power)
    {
        rb.freezeRotation = false;

        var shotDirection = transform.rotation * new Vector3(0, 0, 1);
        Debug.LogFormat("shot direction = {0}", shotDirection);
        rb.AddForce(shotDirection * power, ForceMode.Impulse);
        bShot = true;
        arrowObject.SetActive(false);

        StartCoroutine(CheckBallStops());
    }

    IEnumerator CheckBallStops()
    {
        yield return new WaitForSeconds(0.5f);

        while (rb.velocity != Vector3.zero || rb.angularVelocity != Vector3.zero)
        {
            yield return null;
        }

        OnBallStopped();
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

    public void Reset()
    {
        Stop();

        transform.position = initPos;
        transform.rotation = Quaternion.identity;
        onBallStop = null;
        bShot = false;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        arrowObject.SetActive(true);
    }

    Coroutine rotatingCoroutine;
    public void OnPointerDown(bool right)
    {
        Debug.Log("Rotation Started");
        rotatingCoroutine = StartCoroutine(Rotating(right));
    }

    public void OnPointerUP()
    {
        Debug.Log("Rotation ended");
        if(rotatingCoroutine != null)
        {
            StopCoroutine(rotatingCoroutine);
            rotatingCoroutine = null;
        }
    }


    IEnumerator Rotating(bool right)
    {
        var ratio = right ? turnRatio : -turnRatio;

        while(true)
        {
            transform.Rotate(0, ratio, 0);
            yield return null;
        }
    }

    public void RotateRight()
    {
        transform.Rotate(0, turnRatio, 0);
    }

    public void RotateLeft()
    {
        transform.Rotate(0, -turnRatio, 0);
    }

    
}
