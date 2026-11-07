using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour{

    private float audioVolume = 0.5f;
    private AudioSource musicSource;
    public bool GamePaused {get; private set;} 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake(){
        musicSource  = GetComponent<AudioSource>();
    }
    
    void Start(){
        musicSource.volume = audioVolume;
        GamePaused = false;
    }

    
    public void OnPause(InputValue value){
        
        if(value.Get<float>() <= 0f) { return;}
        GamePaused = !GamePaused;
        Debug.Log($"GamePaused: {GamePaused}");
    }

    public void OnVolumeChange(InputValue value){
        audioVolume += value.Get<float>();// * sensitivity;
        audioVolume = Mathf.Clamp(audioVolume, 0f, 1f);
        musicSource.volume = audioVolume;
    }

    public void OnMuteVolume(InputValue value){
        if(value.Get<float>() <= 0f) { return;}
        musicSource.mute = !musicSource.mute;
        Debug.Log($"Music muted: {musicSource.mute}");
        //disable ui volume controller if muteded. Reanable on unmute.
    }

    
    public void OnGameOver(){
        //stop the timeline.
        //disable input.
        //show game over sequence.
    }
}
