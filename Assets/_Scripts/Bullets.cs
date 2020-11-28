using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullets : MonoBehaviour, IObjectPoolNotifier {

    [SerializeField]
    float limit;
    public Vector3 origin;

    private Rigidbody _rb;

    private Rigidbody rb {
        get{
            if(_rb == null)
            {
                _rb = gameObject.GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }

    void FixedUpdate () {
        float dist = Vector3.Distance (origin, transform.position);

        if (dist > limit) {
            gameObject.ReturnToPool();
        }
    }

    void OnCollisionEnter (Collision col) {
        gameObject.ReturnToPool();
    }

    public void OnCreatedOrDequeuedFromPool(bool created)
    {
        transform.rotation = Quaternion.identity;
    }

    public void OnEnqueuedToPool()
    {
        Debug.Log("Returned to pool",gameObject);
    }
}