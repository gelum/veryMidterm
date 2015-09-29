using UnityEngine;
using System.Collections;

public class camera_controller : MonoBehaviour {

    float zoomAmount = 0;
    float maxClamp = 2;        // currently used for min/max
                                // clamping
    float scrollSpeed = 25f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");

        zoomAmount += Input.GetAxis("Mouse ScrollWheel");
        zoomAmount = Mathf.Clamp(zoomAmount, -maxClamp, maxClamp);
        Debug.Log("zoomAmount = " + zoomAmount);
        float zAxisTranslate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), maxClamp - Mathf.Abs(zoomAmount));

        if (Camera.main != null)
        {
            Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisTranslate * scrollSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"))));
        }

    }
}
