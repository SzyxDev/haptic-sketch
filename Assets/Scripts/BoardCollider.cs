using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCollider : MonoBehaviour
{
    public bool IsTipTouching { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsTipTouching = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Tip")
        {
            Debug.Log("Enter");
            IsTipTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Tip")
        {
            Debug.Log("Exit");
            IsTipTouching = false;
        }
    }
}
