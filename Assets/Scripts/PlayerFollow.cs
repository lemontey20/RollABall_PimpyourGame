using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform PlayerTransform;//take the position of the player
    private Vector3 _cameraOffset;//affect a vector to the camera between a range of 0.01 and 1 meter
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;//create a float variable named the smooth factor of 0.5
    public bool LookAtPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;//take the camera position and compare it to the player position
    }


    void Update()//actualize every frame
    {
        Vector3 newPos = PlayerTransform.position + _cameraOffset;//initialise the value of the vector 3 
        transform.position = Vector3.Slerp(transform.position, newPos,SmoothFactor);//add the smooth effect
        if (LookAtPlayer)//if check the look at the player button in the unity window
            transform.LookAt(PlayerTransform);//then focus the camera on the direction of the player
    }
}
