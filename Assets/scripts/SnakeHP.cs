using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHP : MonoBehaviour
{
    public SnakeMove Snake;
    public Text VisualSnakeHP;
    void Update()
    {
        VisualSnakeHP.text = Snake.Tails.Count.ToString();
    }
}
