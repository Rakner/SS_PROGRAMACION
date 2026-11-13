using UnityEngine;

public class MoveDetector : MonoBehaviour
{
    private bool _playerHasMoved;
    
    //Comprueba que el jugador no salga del rango de escucha del enemigo mientras esta disfrazado, si lo hace lo detectará aunque este disfrazado cambiando el estado de la IA. 
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log(other.gameObject.name + ": " + _playerHasMoved);
            _playerHasMoved = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerHasMoved = true;
            Debug.Log("SALGOOOO: " + other.gameObject.name + ": " + _playerHasMoved);
        }
    }

    //Lllamada de la IA enemiga para comprobar el estado de esta condición
    public bool PlayerHasMoved()
    {
        return _playerHasMoved;
    }
}
