using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //CREACIÓN DEL SINGLETON QUE MANEJA EL AUDIO
    public static AudioManager Instance{get; private set;}
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sfx;
    
    //SUSCRIPCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnPlayerCaptured += StopBGM;
        EventManagerSO.Instance.OnReloadScene += PlayBGM;
    }
    
    //Para y activa la BGM. 
    private void PlayBGM()
    {
        bgm.Play();
    }

    private void StopBGM()
    {
        bgm.Stop();
    }

    //Hace sonar los efectos de sonido
    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    //DESUSCRIPCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnPlayerCaptured -= StopBGM;
        EventManagerSO.Instance.OnReloadScene -= PlayBGM;
    }
}
