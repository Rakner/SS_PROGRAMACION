using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private int _currentIndex;

    void Awake()
    {
        _currentIndex = 0;
        _currentWeapon =  weapons[_currentIndex];
    }

    void OnEnable()
    {
        InputController.Instance.OnScroll += ChangeWeaponWithScroll;
        //InputController.Instance.OnNewWeapon += ChangeWeapon;
        //InputController.Instance.OnUseWeapon += UseWeapon;
    }

    private void UseWeapon()
    {
        _currentWeapon.Use();
    }

    private void ChangeWeapon(int index)
    {
        _currentWeapon.gameObject.SetActive(false);

        _currentIndex = index;
        _currentWeapon =  weapons[_currentIndex];
        _currentWeapon.gameObject.SetActive(true);
    }

    private void ChangeWeaponWithScroll(float direction)
    {
        Debug.Log("-------------" + direction);
        _currentWeapon.gameObject.SetActive(false);

        if (direction > 0)
        {
            _currentIndex = (_currentIndex + 1) % weapons.Length;
        }
        else
        {
            _currentIndex =  (_currentIndex - 1) % weapons.Length;
        }
        
        _currentWeapon =  weapons[_currentIndex];
        _currentWeapon.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        InputController.Instance.OnScroll -= ChangeWeaponWithScroll;
        //InputController.Instance.OnNewWeapon -= ChangeWeapon;
        //InputController.Instance.OnUseWeapon -= UseWeapon;
    }
}
