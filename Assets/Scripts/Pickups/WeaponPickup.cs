using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO;

    ActiveWeapon activeWeapon;

    private void Start()
    {
        activeWeapon = FindFirstObjectByType<ActiveWeapon>();
    }

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {      
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
