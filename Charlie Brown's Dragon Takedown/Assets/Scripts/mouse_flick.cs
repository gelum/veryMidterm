using UnityEngine;
using System.Collections;

public class mouse_flick : MonoBehaviour {

    private bool isDragging = false;        // is dice being dragged
    private bool dragPlaneSet = false;      // have we set the drag plane
    private bool figuredOutFace = true;     // have we figured out the face
    private Plane dragPlane;                // the drag plane ( the die is dragged along this)
    private Vector3 moveTo;                 // vec3 of where we are moving to ( cursor[] - die[])
    private int randX, randY, randZ;        // random multipliers for torque, either -1 or 1

    private float dragDamper = 5.0f;        // damper for drag speed
    private string sideUp = "";             // which side is facing - stored as string atm
    private float addToY = 5.0f;            // how much should the die rise when we select

    public string type;                     // type of die (warrior, <colour> dragon)

	// Use this for initialization
	void Start () {
	    // nothing to see here atm
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

                if (!sideUp.Equals(""))
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

    string CalcSideUp()
    {
        float dotFwd = Vector3.Dot(transform.forward, Vector3.up);
        if (dotFwd >= 0.99f)
        {
            // 5
            //Debug.Log("5");
            if (type.Equals("warrior")) return "shield";
            else if (type.Equals("blue dragon")) return "tail";
            else if (type.Equals("green dragon")) return "tail";
            else if (type.Equals("red dragon")) return "tail";
        }
        if (dotFwd <= -0.99f)
        {
            // 2
            //Debug.Log("2");
            if (type.Equals("warrior")) return "axe";
            else if (type.Equals("blue dragon")) return "fire";
            else if (type.Equals("green dragon")) return "mountain";
            else if (type.Equals("red dragon")) return "fire";
        }
        float dotRight = Vector3.Dot(transform.right, Vector3.up);
        if (dotRight >= 0.99f)
        {
            // 4
            //Debug.Log("4");
            if (type.Equals("warrior")) return "shield";
            else if (type.Equals("blue dragon")) return "head";
            else if (type.Equals("green dragon")) return "head";
            else if (type.Equals("red dragon")) return "head";
        }
        if (dotRight <= -0.99f)
        {
            // 3
            //Debug.Log("3");
            if (type.Equals("warrior")) return "shield";
            else if (type.Equals("blue dragon")) return "wing";
            else if (type.Equals("green dragon")) return "wing";
            else if (type.Equals("red dragon")) return "wing";
        }
        float dotUp = Vector3.Dot(transform.up, Vector3.up);
        if (dotUp >= 0.99f)
        {
            // 6
            //Debug.Log("6");
            if (type.Equals("warrior")) return "axe";
            else if (type.Equals("blue dragon")) return "mountain";
            else if (type.Equals("green dragon")) return "fire";
            else if (type.Equals("red dragon")) return "fire";
        }
        if (dotUp <= -0.99f)
        {
            // 1
            //Debug.Log("1");
            if (type.Equals("warrior")) return "fire";
            else if (type.Equals("blue dragon")) return "mountain";
            else if (type.Equals("green dragon")) return "fire";
            else if (type.Equals("red dragon")) return "fire";
        }

        return "";
    }

}
