using System.Collections;
using UnityEngine;

public class DoorPoliceController : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private int numberOfPolices;
    [SerializeField] private AudioClip closeDoorSfx;
    
    //Cierra la puerta automaticamente cuando todos los enemigos han entrado a la sala despues de haber actviado la alarma, luego se vuelve a abrir automaticamente despues de un tiempo. 
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var policesIn = PlayerPrefs.GetInt("PoliceIn") + 1;
            PlayerPrefs.SetInt("PoliceIn", policesIn);
        }
        
        if (PlayerPrefs.GetInt("PoliceIn") == numberOfPolices)
        {
            GetComponent<Animator>().SetTrigger("Close");
            AudioManager.Instance.PlaySound(closeDoorSfx);
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(10f);
        GetComponent<Animator>().SetTrigger("Open");
    }
}
