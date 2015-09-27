using UnityEngine;
using System.Collections;

public class mouse_flick : MonoBehaviour {

    private bool isDragging = false;
    private Plane dragPlane;
    private Vector3 moveTo;

    float dragDamper = 5.0f;
    float addToY = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        float dist;

        if ( Input.GetMouseButtonDown( 0))
        {
            if ( Physics.Raycast( ray, out hit))
            {
                if ( hit.transform.root.transform == transform)
                {
                    isDragging = true;
                    GetComponent<Rigidbody>().useGravity = false;

                    dragPlane = new Plane(Vector3.up, transform.position + Vector3.up * addToY);
                }
            }
        }

        if ( isDragging)
        {
            bool hasHit = dragPlane.Raycast(ray, out dist);

            if (hasHit)
            {
                moveTo = ray.GetPoint(dist);
                GetComponent<Rigidbody>().AddTorque(5, 5, 5);
            }
        }

        if ( Input.GetMouseButtonUp( 0) && isDragging)
        {
            isDragging = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
	}

    void FixedUpdate()
    {
        if (!isDragging)
            return;

        Vector3 velocity = moveTo - transform.position;
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, velocity, dragDamper * Time.deltaTime);
    }

}
