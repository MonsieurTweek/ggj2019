using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float speed = 5.0f;
    public string hitTag = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("ARROW COLLIDE");
            GameObject.Destroy(this.gameObject);
    }
}
