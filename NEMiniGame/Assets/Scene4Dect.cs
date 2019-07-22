using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Dect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "Identifer5")
        {
            transform.parent.parent.GetComponent<Scene4Controler>().scene4AnimState = Scene4AnimState.End;
        }
    }
}
