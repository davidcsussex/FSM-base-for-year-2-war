using UnityEngine;

public class CompassScript : MonoBehaviour
{


    public RectTransform compass;
    public GameObject player;
    public GameObject dest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        Vector3 direction = dest.transform.position - player.transform.position;
        Vector3 up = transform.up; // you don't want this to change by the compass rotation
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, up), up);
        compass.transform.rotation = Quaternion.Euler(0, 0, -gameObject.transform.rotation.eulerAngles.y);

      
    }

}
