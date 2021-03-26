using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public float shootingTime = 1f;

    private Transform _firePoint;
    private float lastShoot;
    private EnemyHealth _enemyHealth;

    // Start is called before the first frame update
    void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        // Always looking for the player
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x <= 0.0f) transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
        else if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time > lastShoot + 0.25f)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;

        Bullet bulletComponent = myBullet.GetComponent<Bullet>();
        bulletComponent.direction = new Vector2(transform.localScale.x, 0f);
    }

    private void OnDisable()
    {
        _enemyHealth.RestoreHealth();
    }
}
