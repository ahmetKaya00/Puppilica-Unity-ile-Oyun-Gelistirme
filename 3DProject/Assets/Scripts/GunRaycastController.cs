using UnityEngine;
using UnityEngine.InputSystem;

public class GunRaycastController : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 300f;

    [SerializeField] private int damage = 25;

    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask hitLayers;

    private void Awake()
    {
        if(firePoint == null)
        {
            firePoint = transform;
        }
    }

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 0.5f);

        if(Physics.Raycast(firePoint.position,firePoint.forward, out hit, range, hitLayers))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if(rb != null)
            {
                Vector3 forceDirection = firePoint.forward;

                rb.AddForceAtPosition(forceDirection, hit.point, ForceMode.Impulse);

            }
            Target target = hit.collider.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

}
