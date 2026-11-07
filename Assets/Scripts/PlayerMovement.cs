using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour{
    [SerializeField] int hitPoints;

    [Header("Movement Speed and Boundaries")]
    [SerializeField] float linearSpeed = 50.25f; 
    [SerializeField] float xMovementClamp = 510f; 
    [SerializeField] float yMovementClamp = 510f; 
    [SerializeField] float zMovementClamp = 510f; 
    
    [Header("Euler Angles")]
    [SerializeField] float angularSpeed = 10.25f; 
    [SerializeField] float maxRollAngle = 60f;
    [SerializeField] float maxPitchAngle = 30f;
    [SerializeField] float maxYawAngle = 45f;
    //
    private Vector3 delta;
    
    private float wingsInput = 0f;
    public bool GamePaused {get; private set;} 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
   
    void Start(){

        delta = Vector3.zero;
        GamePaused = false;
    }

    // Update is called once per frame
    void Update(){
        UpdateRotation();
        UpdatePosition();
    }

    private void UpdatePosition(){
        float xOffset = delta.x * linearSpeed * Time.deltaTime;
        float yOffset = delta.y * linearSpeed * Time.deltaTime;
        float zOffset = delta.z * linearSpeed * Time.deltaTime;
        float updatedX = Mathf.Clamp(transform.localPosition.x +xOffset, -xMovementClamp, xMovementClamp);
        float updatedY = Mathf.Clamp(transform.localPosition.y +yOffset, -yMovementClamp, yMovementClamp);
        float updatedZ = Mathf.Clamp(transform.localPosition.z +zOffset, -zMovementClamp, zMovementClamp);
        transform.localPosition = new Vector3(updatedX, updatedY, updatedZ);
    }

    private void UpdateRotation(){
        float roll = -delta.x * maxRollAngle;
        float pitch = -delta.y * maxPitchAngle;
        float yaw = delta.z * maxYawAngle;
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, angularSpeed *Time.deltaTime);
    
    }

    public void OnMove3D(InputValue value){
        delta = value.Get<Vector3>();
        Debug.Log($"Direction called {delta.x}, {delta.y}, {delta.z}.");
    }

    public void OnTilt(InputValue value){
        wingsInput = -value.Get<float>();
        Debug.Log($"New wings input is {wingsInput}.");
    } 

    
    void OnDamaged(){
        hitPoints --;
        //play damage audio and effect.
        if(hitPoints <= 0)
            OnGameOver();
    }

    void OnGameOver(){
        //stop the timeline.
        //disable input.
        //show game over sequence.
    }
}
