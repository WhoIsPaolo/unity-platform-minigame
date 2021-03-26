using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;

    private float _lenght, _startpos;

    private void Awake()
    {
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startpos = transform.position.x;
    }

    private void Update()
    {
        float temp_dist = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(_startpos + distance, transform.position.y, transform.position.x);

        if (temp_dist > _startpos + _lenght) _startpos += _lenght;
        else if (temp_dist < _startpos - _lenght) _startpos -= _lenght;
    }
}
