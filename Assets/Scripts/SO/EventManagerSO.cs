using System;
using UnityEngine;

public class EventManagerSO : MonoBehaviour
{
    //CREACIÓN DEL SINGLETON PARA EL MANAGER DE EVENTOS
    public static EventManagerSO Instance{get; private set;}
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
    
    //DECLARACIÓN DE EVENTOS: 
    public event Action<bool> OnCostumeUsed;
    public event Action<bool> OnCheckCostumePlace;
    public event Action<CostumeDataSO> OnPickUpCostume;
    public event Action<CostumeDataSO> OnGiveCostumeData;
    public event Action<bool> OnCheckCelda;
    public event Action OnPlayerCaptured;
    public event Action OnReloadScene;
    public event Action<int, Sprite> OnCostumePickedToUI;
    public event Action<int> OnCostumeSelectedToUI;
    public event Action<int, float, float> OnShowCostumeCD;
    public event Action<bool> OnAlarmTriggered;
    public event Action OnGetKey;
    public event Action<bool> OnCheckKey;
    
    //LLAMADA DE LOS DIFERENTES EVENTOS: 
    public void CostumeIsPicked(CostumeDataSO costume)
    {
        OnPickUpCostume?.Invoke(costume);
    }
    
    public void GiveCostumeData(CostumeDataSO currentCostumeData)
    {
        OnGiveCostumeData?.Invoke(currentCostumeData);
    }

    public void PlayerNotifiesIsInCelda(bool isInCelda)
    {
        Debug.Log("CELDA >>> LLAMADA AL EVENTO DEL EVENT MANAGER: " +  isInCelda);
        OnCheckCelda?.Invoke(isInCelda);
    }

    public void NotifiesPlayerIsCaptured()
    {
        OnPlayerCaptured?.Invoke();
    }

    public void NotifiesReload()
    {
        OnReloadScene?.Invoke();
    }

    public void PlayerNotifiesCostumeUsed(bool isUsingCostume)
    {
        OnCostumeUsed?.Invoke(isUsingCostume);
    }

    public void NotifiesCostumeUsedInplace(bool isCostumeInPlace)
    {
        OnCheckCostumePlace?.Invoke(isCostumeInPlace);
    }

    public void NotifiesCostumePickedToUI(int index, Sprite costumeDataCostumeIcon)
    {
        OnCostumePickedToUI?.Invoke(index, costumeDataCostumeIcon);
    }

    public void NotifieCostumeSelectedToUI(int currentIndex)
    {
        OnCostumeSelectedToUI?.Invoke(currentIndex);
    }

    public void NotifieShowCostumeCD(int costumeIndex, float timer, float cd)
    {
        OnShowCostumeCD?.Invoke(costumeIndex, timer, cd);
    }

    public void NotifiesAlarmIsTriggered(bool alarmTriggered)
    {
        OnAlarmTriggered?.Invoke(alarmTriggered);
    }

    public void NotifiesKeyObtained()
    {
        OnGetKey?.Invoke();
    }

    public void NotifiesHasKey(bool state)
    {
        OnCheckKey?.Invoke(state);
    }
}
