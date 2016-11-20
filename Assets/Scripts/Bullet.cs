using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private GameObject firedBy;

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(10, firedBy, health.scoreOnKill);
        }

        Destroy(gameObject);
    }

    public void setFiredBy(GameObject firedByParam)
    {
        this.firedBy = firedByParam;
    }
}