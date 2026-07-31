using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    //Inspector Assignment:
    public Material highLightMaterial;
    private Material[] originalMaterials;
    private MeshRenderer[] meshRenderers;

    public GameObject weaponPrefab;
    public float lookRange = 3f;

    //Private Variables:
    private bool isLookedAt = false;
    private Camera playerCam;
    private PlayerShooting player;
    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalMaterials = new Material[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].material;
        }

        player = FindObjectOfType<PlayerShooting>();
        playerCam = player.GetComponentInChildren<Camera>();
    }


    void Update()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, lookRange))
        {
            if (hit.collider.GetComponentInParent<PickUp>() == this)
            {
                if (!isLookedAt)
                    SetLookedAt(true);

                return;
            }
        }
    }

    void SetLookedAt(bool lookedAt)
    {
        isLookedAt = lookedAt;

        if (lookedAt)
        {
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.material = highLightMaterial;
            }
            
        }
        else
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = originalMaterials[i];
            }
        }
    }

    void OnPickUp()
    {
        if (!isLookedAt) return;
        player.OnDrop();
        // Instantiate the new weapon as a child of the gun holder
        GameObject newWeapon = Instantiate(weaponPrefab, player.gunHolder);

        // Reset position and rotation to align with the holder
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;

        // Set the new gun reference in the player shooting script
        player.gun = newWeapon.GetComponent<Gun>();

        // Destroy the dropped weapon pickup object
        Destroy(gameObject);
    }
}
