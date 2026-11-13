using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowCostumeInUIController : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private GameObject[] costumeIcons;
    [SerializeField] private GameObject[] cdIcons;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color deselectedColor;

    //SUSCRIPCCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnCostumePickedToUI += PutIconInUI;
        EventManagerSO.Instance.OnCostumeSelectedToUI += ChangeSelectedCostumeIcon;
        EventManagerSO.Instance.OnShowCostumeCD += InvokeShowCostumeCD;
    }

    //Muestra el CD de cada disfraz independientemente por su UI y slot correspondiente. 
    private void InvokeShowCostumeCD(int index, float timer, float cd)
    {
        cdIcons[index].SetActive(true);
        cdIcons[index].GetComponent<Image>().fillAmount = 1 - (timer / cd);
        Debug.Log("VALOR: " + (1 - (timer / cd)) + " TIMER: " + timer + " CD: " + cd);
        Debug.Log("FILL AMOUNT: " + cdIcons[index].GetComponent<Image>().fillAmount);

        if (cdIcons[index].GetComponent<Image>().fillAmount <= 0)
        {
            cdIcons[index].GetComponent<Image>().fillAmount = 1;
            cdIcons[index].SetActive(false);
        }
    }

    //Añade un icono del disfraz que se acaba de conseguir a la UI. 
    private void PutIconInUI(int index, Sprite icon)
    {
        costumeIcons[index].GetComponentsInChildren<Image>()[0].enabled = true;
        costumeIcons[index].GetComponentsInChildren<Image>()[0].sprite = icon;
    }

    //Cambia el marco del disfraz según este seleccionado o no para indicar al jugador cual esta usando o puede usar.
    private void ChangeSelectedCostumeIcon(int indexToChange)
    {
        var index = 0;
        Debug.Log("Change selected costume icon" + index);
        foreach (var costumeIcon in costumeIcons)
        {
            if (index == indexToChange)
            {
                costumeIcon.GetComponentsInChildren<Image>()[1].color = selectedColor;
            }
            else
            {
                costumeIcon.GetComponentsInChildren<Image>()[1].color = deselectedColor;
            }
            index++;
        }
        
    }

    //DESUSCRIPCCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnCostumePickedToUI -= PutIconInUI;
        EventManagerSO.Instance.OnCostumeSelectedToUI -= ChangeSelectedCostumeIcon;
        EventManagerSO.Instance.OnShowCostumeCD -= InvokeShowCostumeCD;
    }
}
