using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    public float TranslationX;
    public float TranslationY;
    public float FlyingPositionX;
    public float FlyingPositionY;
    public GameObject CarrierGameObject;
    public Canvas PopupCanvas;
    public float Radius;
    private Vector2 centre;
    private float angle;
    private Carrier carrier;
    private PopupMenu popupMenu;
    public float time;
    private bool wasChosen;
    private bool wasLounched;
    private float minVelocity;
    private float maxVelocity;
    private float angularVelocity;
    private Vector2 carrierVelocity;
    private Rigidbody2D rigidbody;

    private Vector3 seek (Vector3 target, Vector3 position)
    {
        var velocity3D = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
        var desiredVelocity = Vector3.Normalize(target - position) * maxVelocity;
        var steering = desiredVelocity - velocity3D;
        return steering;
    }

    private Vector3 persuit(Vector3 target, Vector3 position)
    {
        var distance = target - position;
        var T = Convert.ToInt32(distance.magnitude / maxVelocity);
        var futurePosition = target + new Vector3(carrierVelocity.x, carrierVelocity.y, 0) * T;
        return seek(futurePosition, position);
    }

    private void fly()
    {
        var position = transform.position;
        var target = new Vector3(centre.x, centre.y, transform.position.z);
        angle += angularVelocity * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        target = centre + offset;
        var steering = persuit(target, position);
        var velocity3D = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
        velocity3D = velocity3D + steering;
        rigidbody.velocity = new Vector2(velocity3D.x, velocity3D.y);

    }

    private void land()
    {
        var position = transform.position;
        var target = new Vector3(CarrierGameObject.transform.position.x + TranslationX,
            CarrierGameObject.transform.position.y + TranslationY, transform.position.z);
        var steering = persuit(target, position);
        var velocity3D = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
        velocity3D = velocity3D + steering;
        rigidbody.velocity = new Vector2(velocity3D.x, velocity3D.y);
        if ((position - target).magnitude<0.1)
        {
            wasLounched = false;
            wasChosen = false;
            time = -1.0f;
        }
    }

    private void stay()
    {
        transform.position = new Vector3(CarrierGameObject.transform.position.x + TranslationX,
            CarrierGameObject.transform.position.y + TranslationY, transform.position.z);
    }

    void Start()
    {
        time = -1;
        popupMenu = PopupCanvas.GetComponent<PopupMenu>();
        carrier = CarrierGameObject.GetComponent<Carrier>();
        rigidbody = GetComponent<Rigidbody2D>();
        wasChosen = false;
        wasLounched = false;
    }

    void OnMouseDown()
    {
        wasChosen = true;
        popupMenu.Show();
    }
    
    void Update()
    {
        centre = new Vector2(CarrierGameObject.transform.position.x + FlyingPositionX,
            CarrierGameObject.transform.position.y + FlyingPositionY);
        carrierVelocity = carrier.velocity;
        if (rigidbody.velocity.magnitude < minVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
        }
        if (rigidbody.velocity.magnitude > maxVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }
        if (Input.GetKeyDown("h") && wasChosen && popupMenu.Hidden && time == -1.0f)
        {
            minVelocity = popupMenu.minVelocity;
            maxVelocity = popupMenu.maxVelocity;
            angularVelocity = popupMenu.angularVelocity;
            wasLounched = true;
            time = 20f;
        }
        if (wasChosen && wasLounched && time>0)
        {
            fly();
        }
        if (wasChosen && wasLounched && time < 0)
        {
            land();
        }
        if (time < 0 && !wasLounched)
        {
            stay();
        }
        if (time > 0) 
        {
            time -= Time.deltaTime;
        }
    }
}
