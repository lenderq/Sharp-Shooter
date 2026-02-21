using StarterAssets;
using System.Threading;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeaponSO;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] TextMeshProUGUI ammoText;

    [SerializeField] float zoomDefault = 40f;

    WeaponSO currentWeaponSO;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;
    

    float timeSinceLastShot = 0f;

    float defaultRotationSpeed;

    const string SHOOT_STRING = "Shoot";

    int currentAmmo = 0;

    private void Awake()
    {
        firstPersonController = GetComponentInParent<FirstPersonController>();
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    private void Start()
    {
        SwitchWeapon(startingWeaponSO);
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }

        ammoText.text = currentAmmo.ToString("D2");
    }

    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    private void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            zoomVignette.SetActive(true);
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            cinemachineCamera.Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            zoomVignette.SetActive(false);
            weaponCamera.fieldOfView = zoomDefault;
            cinemachineCamera.Lens.FieldOfView = zoomDefault;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;

        this.currentWeaponSO = weaponSO;

        AdjustAmmo(currentWeaponSO.MagazineSize);
    }
}
