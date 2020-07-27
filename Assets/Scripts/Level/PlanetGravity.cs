using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlanetGravity : MonoBehaviour
{
    [Header("The max Force near object, dont use scale")]
    [SerializeField, Range(0.1f, 50)]
    private float force;

    private SphereCollider sphereCollider;

    private void Start()
    {
        this.sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constants.ROCKET_TAG))
        {
            Vector3 dirrection = this.transform.position - other.transform.position;
            other.attachedRigidbody.AddForce(dirrection.normalized * CalcForce(this.force, other.transform.position), ForceMode.Force);
        }
    }

    /// <summary>
    /// Calc Force by Distance, more - less force
    /// </summary>
    /// <param name="force"></param>
    /// <param name="rocketPos"></param>
    /// <returns></returns>
    private float CalcForce(float force, Vector3 rocketPos)
    {
        return (1 - (Vector3.Distance(this.transform.position, rocketPos) / this.sphereCollider.radius)) * force;
    }
}
