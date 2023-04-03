using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public Transform target; // The player's transform
    public float distance = 10.0f; // Distance from the player
    public float xSpeed = 120.0f; // Speed of the x-axis rotation
    public float ySpeed = 120.0f; // Speed of the y-axis rotation
    public float yMinLimit = -20f; // Minimum angle for the y-axis rotation
    public float yMaxLimit = 80f; // Maximum angle for the y-axis rotation
    public float distanceMin = 0; // Minimum distance from the player
    public float distanceMax = 15f; // Maximum distance from the player
    public bool smoothFollow = true; // Whether to smooth the camera follow
    public float smoothTime = 0.2f; // Time for smoothing the camera follow

    private float x = 0.0f;
    private float y = 0.0f;
    private Vector3 prevPos;

    private Vector3 position;

    private Vector3 correctPosition;

    private Quaternion lockedRotation;

     bool isMovingForwards;
    bool isMovingSideways;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        prevPos = transform.position;

        isMovingForwards=false;
        isMovingSideways=false;
    }

    void LateUpdate(){
        if (target){

        //horizontalMovement = Input.GetAxis("Horizontal");
        //verticalMovement = Input.GetAxis("Vertical");

        isMovingForwards = Input.GetAxisRaw("Vertical") != 0;
        isMovingSideways = Input.GetAxisRaw("Horizontal") != 0;

        float rightHorizontal = Input.GetAxis("RightStickHorizontal");
        float rightVertical = Input.GetAxis("RightStickVertical");

        if (!isMovingForwards && !isMovingSideways){

            // Update the camera position based on the mouse input
            x += Input.GetAxis("Mouse X") * xSpeed * distance * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;


            x += rightHorizontal * xSpeed * distance * Time.deltaTime;
            y -= rightVertical * ySpeed * Time.deltaTime;


            

            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = lockedRotation * Quaternion.Euler(y, x, 0);

            Debug.Log("X " + rotation.x + "Y " + rotation.y);

            // Update the distance from the player based on the mouse scrollwheel input
            distance = Mathf.Clamp(distance , distanceMin, distanceMax);

            // Set the new camera position and smooth the follow if necessary
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
            /*if (smoothFollow)
            {
                transform.position = Vector3.Lerp(prevPos, position, smoothTime);
            }*/

            if (smoothFollow){
                transform.position = Vector3.Lerp(prevPos, position, smoothTime);
            }else {transform.position = position;}
            transform.rotation = rotation;
            prevPos = transform.position;
        } else {
            // Reset the camera position to its default position


            Vector3 playerForward = target.forward;
            playerForward = -playerForward;

            Vector3 offset = new Vector3(0, 0, -distance);
            Vector3 position = target.position + playerForward * offset.z + target.up * offset.y;
            correctPosition = position;
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(-playerForward, target.up);
            x=0;
            y=0;
            lockedRotation = transform.rotation;
            Debug.Log("X2 " + transform.rotation.x + "Y2 " + transform.rotation.y);

            
            /*
            transform.position = target.position - target.forward * distance;
            transform.rotation = Quaternion.LookRotation(-target.forward, target.up);*/
        }


        /*
            // Update the camera position based on the mouse input
            x += Input.GetAxis("Mouse X") * xSpeed * distance * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Update the distance from the player based on the mouse scrollwheel input
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            // Set the new camera position and smooth the follow if necessary
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(prevPos, position, smoothTime);
            }
            else
            {
                transform.position = position;
            }
            transform.rotation = rotation;
            prevPos = transform.position;*/
        }
    }

    // Helper function to clamp an angle between a minimum and maximum value
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    
}



/*
void LateUpdate()
{
    if (target)
    {
        // Check if the player is moving forwards
        bool isMovingForwards = Input.GetAxisRaw("Vertical") > 0;

        // Only update the camera position if the player is not moving forwards
        if (!isMovingForwards)
        {
            // Update the camera position based on the mouse input
            x += Input.GetAxis("Mouse X") * xSpeed * distance * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Update the distance from the player based on the mouse scrollwheel input
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            // Set the new camera position and smooth the follow if necessary
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(prevPos, position, smoothTime);
            }
            else
            {
                transform.position = position;
            }
            transform.rotation = rotation;
            prevPos = transform.position;
        }
        else
        {
            // Reset the camera position to its default position
            transform.position = target.position - target.forward * distance;
            transform.rotation = Quaternion.LookRotation(-target.forward, target.up);
        }
    }
}*/

