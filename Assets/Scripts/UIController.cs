using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES DE LAS PANTALLAS A MOSTRAR U OCULTAR
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private GameObject noKeyScreen;
    [SerializeField] private GameObject keyScreen;
    
    //SUSCRIPCCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnPlayerCaptured += ShowLossScreen;
        EventManagerSO.Instance.OnCheckKey += ShowNoKeyScreen;
    }

    //Muestra un mensaje cuando se llega a la puerta y no se tiene la llave para abrirla y acabar el nivel 
    //Muestra la pantalla de victoria cuando se interactua con la puerta y se tiene la llave. 
    private void ShowNoKeyScreen(bool hasKey)
    {
        if (hasKey)
        {
            keyScreen.SetActive(true);
            noKeyScreen.GetComponentInChildren<TMPro.TMP_Text>().text = "HAS CONSEGUIDO \n\nESCAPAR CON EXITO";
            EventManagerSO.Instance.NotifiesPlayerIsCaptured();
        }
        else
        {
            noKeyScreen.SetActive(true);
            noKeyScreen.GetComponentInChildren<TMPro.TMP_Text>().text = "NO PUEDES ESCAPAR \n\nSIN LA LLAVE";
            StartCoroutine(StopShowingNoKeyScreen());
        }
    }

    //Vuelve a empezar el nivel desde 0. 
    public void ReloadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        EventManagerSO.Instance.NotifiesReload();
    }

    //Muestra la pantalla de derrota cuando el jugador es capturado. 
    private void ShowLossScreen()
    {
        StartCoroutine(ShowLossScreenCoroutine());
    }

    IEnumerator ShowLossScreenCoroutine()
    {
        yield return new WaitForSeconds(1f);
        lossScreen.SetActive(true);
    }
    
    IEnumerator StopShowingNoKeyScreen()
    {
        yield return new WaitForSeconds(1.5f);
        noKeyScreen.SetActive(false);
    }

    //DESUSCRIPCCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnPlayerCaptured -= ShowLossScreen;
        EventManagerSO.Instance.OnCheckKey -= ShowNoKeyScreen;
    }
}
