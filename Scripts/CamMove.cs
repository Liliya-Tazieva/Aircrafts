using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject Carrier;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Carrier.transform.position.x, Carrier.transform.position.y,
            transform.position.z);
    }
}
