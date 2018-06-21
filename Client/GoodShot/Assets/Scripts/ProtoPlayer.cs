using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoPlayer : MonoBehaviour {

    [SerializeField]
    ProtoBall ball;
    [SerializeField]
    float power = 100f;

    Rigidbody ballRB;
    Vector3 initBallPosition;

	// Use this for initialization
	void Start () {
        ballRB = ball.GetComponent<Rigidbody>();
        Debug.Assert(ballRB != null, "ProtoBall doesn't have Rigidbody component.");

        initBallPosition = ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShootBall(float powerRatio = 1.0f)
    {
        Debug.Log("Shot the ball.");
        ball.Shoot(power * powerRatio);
    }
}
