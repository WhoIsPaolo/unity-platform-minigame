using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;

    private float _health;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        _health -= amount;

        //Visual Feedback
        StartCoroutine("DamageFeedback");

        //Game Over
        if (_health <= 0)
        {
            _health = 0;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DamageFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    public void RestoreHealth()
    {
        _health = totalHealth;
        _renderer.color = Color.white;
    }
}
