using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = HP.ToString();
    }

}
