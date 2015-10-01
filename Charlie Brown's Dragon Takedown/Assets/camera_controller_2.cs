using UnityEngine;
using System.Collections;

public class camera_controller_2 : MonoBehaviour {
    //min and maximums for fov and fov start
    float minFov = 50f;     
    float maxFov = 100f;
    float fov = 50f;

    //speed for the scrolling 
    //is negative for proper and not inverted scrolling
    float sensitivity = -50f;
    
    // Use this for initialization
    void Start () { }   
	
    // Update is called once per frame
	void Update () {
        //x and y
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        
        //fov with scroll wheel
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;

        //fov with page up/down
        if (Input.GetKey(KeyCode.PageUp))
            fov += 1.5f;
        else if (Input.GetKey(KeyCode.PageDown))
            fov -= 1.5f;

        //min <= fov <= max
        fov = Mathf.Clamp(fov, minFov, maxFov);
       
        //moving the camera camera
        if (Camera.current != null) {
            Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
            Camera.main.fieldOfView = fov;
        }
    }
}
