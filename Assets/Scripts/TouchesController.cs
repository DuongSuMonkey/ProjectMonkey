using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchesController : MonoBehaviour
{
    [SerializeField] private List<Touches> touches;
    [SerializeField] private float TimeChange;
    [SerializeField] private float timer;
    [SerializeField] private int count = 0;
    private void Start()
    {
        foreach (var touch in touches)
        {
            touch.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (ChangeTextColor.Instance.IsFinal)
        {
            if (count < touches.Count)
            {
                timer += Time.deltaTime;
            }
            else
            {
                return;
            }
            
            if (timer >= TimeChange && count < touches.Count)
            {
                foreach (var touch in touches)
                {
                    touch.gameObject.SetActive(false);
                }
                touches[count].gameObject.SetActive(true);
                count++;
                timer = 0.0f;
            }


        }
    }
}
