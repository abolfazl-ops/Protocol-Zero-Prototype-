using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Gun gun;
    private bool isHoldingShoot;

// Triggered by 'Shoot' action in Input System
    public void OnShoot()
    {
        isHoldingShoot = true;
    }

// Triggered by 'Shoot Release' action
    public void OnShootRelease()
    {
        isHoldingShoot = false;
    }

// Triggered by 'Reload' action (Pressing R)
    public void OnReload()
    {
        if (gun != null)
        {
            gun.TryReload();
        }
    }

    void Update()
    {
// Continuously fire if holding the mouse button
        if (isHoldingShoot && gun != null)
        {
            gun.Shoot();
        }
    }
}