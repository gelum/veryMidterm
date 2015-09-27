using UnityEngine;
using System.Collections;

public class die_controller : MonoBehaviour {

    int diceCount;
    GameObject diceObject;
    Camera cam;

    Vector3 initPos;
    float initXpose;

    // Use this for initialization
    void Start () {
        GameObject myGameObject = GameObject.Find("dragon_die"); // Make a new GO.
        Rigidbody gameObjectsRigidBody = myGameObject.AddComponent<Rigidbody>(); // Add the rigidbody.
        gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.

        diceObject = myGameObject;
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //initial click to roll a dice
            initPos = Input.mousePosition;

            //return x component of dice from screen to view point
            initXpose = cam.ScreenToViewportPoint(Input.mousePosition).x;
        }

        //current position of mouse
        Vector3 currentPos = Input.mousePosition;

        //get all position along with mouse pointer movement
        Vector3 newPos = cam.ScreenToWorldPoint(new Vector3(currentPos.x, currentPos.y, Mathf.Clamp(currentPos.y / 10, 10, 50)));

        //translate from screen to world coordinates  
        newPos = cam.ScreenToWorldPoint(currentPos);

        if (Input.GetMouseButtonUp(0))
        {
            initPos = cam.ScreenToWorldPoint(initPos);

            //Method use to roll the dice
            RollTheDice(newPos);
            //use identify face value on dice
            //StartCoroutine(GetDiceCount());
        }
    }

    //Method Roll the Dice
    void RollTheDice(Vector3 lastPos)
    {
        diceObject.GetComponent<Rigidbody>().AddTorque(Vector3.Cross(lastPos, initPos) * 1000, ForceMode.Impulse);
        lastPos.y += 12;
        lastPos.z += 12;
        lastPos.x += 12;
        diceObject.GetComponent<Rigidbody>().AddForce(((lastPos - initPos).normalized) * (Vector3.Distance(lastPos, initPos)) * 25 * diceObject.GetComponent<Rigidbody>().mass);
    }

    //////Coroutine to get dice count
    //void GetDiceCount()
    //{
    //    if (Vector3.Dot(transform.forward, Vector3.up) >= 1)
    //        diceCount = 5;
    //    if (Vector3.Dot(-transform.forward, Vector3.up) >= 1)
    //        diceCount = 2;
    //    if (Vector3.Dot(transform.up, Vector3.up) >= 1)
    //        diceCount = 3;
    //    if (Vector3.Dot(-transform.up, Vector3.up) >= 1)
    //        diceCount = 4;
    //    if (Vector3.Dot(transform.right, Vector3.up) >= 1)
    //        diceCount = 6;
    //    if (Vector3.Dot(-transform.right, Vector3.up) >= 1)
    //        diceCount = 1;
    //    Debug.Log("diceCount :" + diceCount);
    //}

}
