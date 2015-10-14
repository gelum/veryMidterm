using UnityEngine;
using System.Collections;

public class camera_controller_2 : MonoBehaviour {

    // Use this for initialization
    void Start () { }   
	
    // Update is called once per frame
	void Update () {
        float zMin = 25f;
        float zMax = 150f;

        //x, y, z movement
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        float zAxisValue = Input.GetAxis("Mouse ScrollWheel") * 20f;//*10: Increase speed

        //z movement:page up, page down
        if (Input.GetKey(KeyCode.PageUp))
            zAxisValue += 1.5f;
        else if (Input.GetKey(KeyCode.PageDown))
            zAxisValue -= 1.5f;
//        Debug.Log(Camera.main.transform.position);
        
        //moving the camera
        if (Camera.main != null)
        {
            //the y for the camera is the z for the world
            if ((Camera.main.transform.position.y >= zMax) && (zAxisValue < 0))
                Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0)); 
            else if ((Camera.main.transform.position.y <= zMin) && (zAxisValue > 0)) 
                Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0));
            else
                Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
        }
    }
}
