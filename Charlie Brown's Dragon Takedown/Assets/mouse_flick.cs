using UnityEngine;
using System.Collections;

public class mouse_flick : MonoBehaviour {

    private bool isDragging = false;
    private bool dragPlaneSet = false;
    private bool figuredOutFace = true;
    private Plane dragPlane;
    private Vector3 moveTo;
    private int randX, randY, randZ;

    float dragDamper = 5.0f;
    int sideUp = 0;
    float addToY = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        float dist;

        if ( Input.GetMouseButtonDown( 0) || Input.GetMouseButton(0))
        {
            if ( Physics.Raycast( ray, out hit))
            {
                if ( hit.transform.root.transform == transform)
                {
                    isDragging = true;
                    figuredOutFace = false;
                    GetComponent<Rigidbody>().useGravity = false;

                    if ( dragPlaneSet == false)
                    {
                        randX = Random.Range(1, 3);
                        randY = Random.Range(1, 3);
                        randZ = Random.Range(1, 3);

                        if (randX == 2) randX = -1;
                        if (randY == 2) randY = -1;
                        if (randZ == 2) randZ = -1;

                        dragPlane = new Plane(Vector3.up, transform.position + Vector3.up * addToY);
                        dragPlaneSet = true;
                    }
                }
            }
        }

        if ( isDragging)
        {
            bool hasHit = dragPlane.Raycast(ray, out dist);     // wtf

            if (hasHit)
            {
                moveTo = ray.GetPoint(dist);                    // keep the die in motion

                GetComponent<Rigidbody>().AddTorque(50 * randX, 50 * randY, 50 * randZ);   // add rotation to the die
            }
        }

        if ( Input.GetMouseButtonUp( 0) && isDragging)
        {
            isDragging = false;
            dragPlaneSet = false;
            GetComponent<Rigidbody>().useGravity = true;
        }

        // check to see if rigidbody stopped
        if ( !figuredOutFace && !isDragging)
        {
            float velocity = GetComponent<Rigidbody>().velocity.magnitude;
            if (velocity < 0.01)
            {
                sideUp = CalcSideUp();

                if (sideUp != 0)
                {
                    figuredOutFace = true;
                    Debug.Log("Side = " + CalcSideUp());
                }
            }
        }

	}

    void FixedUpdate()
    {
        if (!isDragging)
            return;

        Vector3 velocity = moveTo - transform.position;
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, velocity, dragDamper * Time.deltaTime);
    }

    int CalcSideUp()
    {
        float dotFwd = Vector3.Dot(transform.forward, Vector3.up);
        if (dotFwd >= 0.99f) return 5;
        if (dotFwd <= -0.99f) return 2;
        float dotRight = Vector3.Dot(transform.right, Vector3.up);
        if (dotRight >= 0.99f) return 4;
        if (dotRight <= -0.99f) return 3;
        float dotUp = Vector3.Dot(transform.up, Vector3.up);
        if (dotUp >= 0.99f) return 6;
        if (dotUp <= -0.99f) return 1;
        return 0;
    }

}
