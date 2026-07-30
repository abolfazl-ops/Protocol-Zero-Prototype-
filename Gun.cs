using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
[Header("Gun Settings")]
public float reloadTime = 1f;
public float fireRate = 0.15f;
public int maxSize = 20;
public GameObject bullet;
public Transform bulletSpawnPoint;
public GameObject weaponFlash;

[Header("Recoil Settings")]
public float recoilDistance = 0.1f;
public float recoilSpeed = 15f;
public Vector3 reloadRotationOffset = new Vector3(60, 50, 50);

private int currentAmmo;
private bool isReloading = false;
private float nextTimeToFire = 0f;
private Quaternion initialRotation;
private Vector3 initialPosition;

void Start()
{
currentAmmo = maxSize;
initialRotation = transform.localRotation;
initialPosition = transform.localPosition;
}

public void Shoot()
{
if (isReloading || Time.time < nextTimeToFire) return;

if (currentAmmo <= 0)
{
StartCoroutine(Reload());
return;
}

nextTimeToFire = Time.time + fireRate;
currentAmmo--;

Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
Instantiate(weaponFlash, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

StopCoroutine(nameof(Recoil));
StartCoroutine(nameof(Recoil));
}

public void TryReload()
{
if (!isReloading && currentAmmo < maxSize)
{
StartCoroutine(Reload());
}
}

private IEnumerator Reload()
{
isReloading = true;
Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles + reloadRotationOffset);
float halfReload = reloadTime / 2;
float t = 0;

// Rotate to reload position
while (t < halfReload)
{
t += Time.deltaTime;
transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, t / halfReload);
yield return null;
}

// Rotate back
t = 0;
while (t < halfReload)
{
t += Time.deltaTime;
transform.localRotation = Quaternion.Lerp(targetRotation, initialRotation, t / halfReload);
yield return null;
}

currentAmmo = maxSize;
isReloading = false;
}

private IEnumerator Recoil()
{
Vector3 recoilTarget = initialPosition + new Vector3(0, 0, recoilDistance);
float t = 0;

while (t < 1f)
{
t += Time.deltaTime * recoilSpeed;
transform.localPosition = Vector3.Lerp(initialPosition, recoilTarget, t);
yield return null;
}

t = 0;
while (t < 1f)
{
t += Time.deltaTime * recoilSpeed;
transform.localPosition = Vector3.Lerp(recoilTarget, initialPosition, t);
yield return null;
}

transform.localPosition = initialPosition;
}
}