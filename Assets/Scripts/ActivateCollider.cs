using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCollider : MonoBehaviour
{
    [SerializeField] GameObject collider;
    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
        
    }

    private void Activate()
    {
        collider.SetActive(true);
    }

    public void Deactivate()
    {
        collider.SetActive(false);
    }
}
