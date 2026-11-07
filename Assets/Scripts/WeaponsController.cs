using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsController : MonoBehaviour{

    [SerializeField] ParticleSystem[] weaponsArray;
    [SerializeField] int nBulletsPerShot =1;
    [SerializeField] RectTransform crosshair;
    
    private bool isFiring;
    private Vector2 crosshairPosition;
    private static readonly Vector2 initialCrosshairPosition = new(-25f, 30f);
    
    void Awake(){
        isFiring = false;
    }

    public void Start(){
        Cursor.visible = false;
        if (crosshair != null)
        {
            crosshairPosition = crosshair.position;
        }
        EmitFire();
    }

    public void Update(){
        UpdateCrosshair();
        
    }

    public void OnFire(InputValue value){
        //isFiring = value.isPressed;
        //Debug.Log($"isFiring state: {isFiring}.");
    }

    public void OnAim(InputValue value){
        Debug.Log($"[aim] OnAim called");
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        crosshairPosition = value.Get<Vector2>();
        Debug.Log($"[aim] New mouse position: {crosshairPosition}");
    }

    /*public void OnCrossLeft(InputValue value){
        Debug.Log($"[crosshair] OnCrossLeft called");
        if(value.isPressed){
            crosshairPosition = crosshairPosition + new Vector2(-5f, 0f);
            Debug.Log("[crosshair] cross jumped 5f to the left");
        }
    }

    public void OnCrossRight(InputValue value){
       Debug.Log($"[crosshair] OnCrossRight called");
        if(value.isPressed){
            crosshairPosition = crosshairPosition + new Vector2(+5f, 0f);
            Debug.Log("[crosshair] cross moved +5f to the right");
        }
    }

    public void OnCross2D(InputValue value){
        Debug.Log($"[crosshair] OnCross2D called");
        Vector2 delta = value.Get<Vector2>();
        Debug.Log($"[crosshair] Got delta: {crosshairPosition}");
        crosshairPosition += 10*delta;
        Debug.Log($"[crosshair] New cross position: {crosshairPosition}");
    }
*/

    private void EmitFire(){
        if (weaponsArray != null)
        {
            foreach(ParticleSystem weapon in weaponsArray){
                if (weapon != null){
                    //weapon.Emit(nBulletsPerShot);
                    var emissionModule = weapon.emission;
                    emissionModule.enabled = true;
                }
            }
        }
        //play shot audio
        //Debug.Log($" Cannon Fires!!!");
    } 

    private void UpdateCrosshair(){
        if (crosshair != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            crosshair.Translate(mouseDelta);
            Debug.Log($"[crosshair] New crosshair position: {crosshair.position}");
        }
    }

}
