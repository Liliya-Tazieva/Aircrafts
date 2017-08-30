using UnityEngine;

public class Carrier : MonoBehaviour
{
    public float maxSpeed;
    public Vector2 velocity;
    private Rigidbody2D rigidBody;
    private float currentSpeed;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        velocity = rigidBody.velocity;
        currentSpeed = rigidBody.velocity.magnitude;
        if (Input.GetKey("down"))
        {
            if (rigidBody.velocity.y > 0.0001) rigidBody.AddForce(5f * new Vector2(0, -1));
        }
        if (Input.GetKey("up") && currentSpeed<maxSpeed)
        {
            rigidBody.AddForce(3f * Vector2.up);
        }
        if (Input.GetKey("right") && currentSpeed < maxSpeed)
        {
            rigidBody.AddForce(3f * Vector2.one);
        }
        if (Input.GetKey("left") && currentSpeed < maxSpeed)
        {
            rigidBody.AddForce(3f * new Vector2(-1, 1));
        }
    }
}
