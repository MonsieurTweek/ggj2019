using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float speed = 5.0f;
    public string hitTag = "";
    [SerializeField]
    private float timeToLive = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("ARROW COLLIDE");
        GameObject.Destroy(this.gameObject);
    }
}
