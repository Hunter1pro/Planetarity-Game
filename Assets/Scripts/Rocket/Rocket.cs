using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour, ISetupRocket
{
    [SerializeField]
    private Rigidbody m_rigidbody;
    public Rigidbody Rigidbody => m_rigidbody;

    private RocketData rocketData;

    private Dictionary<int, ITakeDamage> planetsDamage;

    private void Start()
    {
        // Check
        this.m_rigidbody = this.m_rigidbody ? this.m_rigidbody : this.GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        // Setup in Editor
        this.m_rigidbody = this.m_rigidbody ? this.m_rigidbody : this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.PLANET_TAG))
        {
            if (planetsDamage.TryGetValue(collision.gameObject.GetInstanceID(), out ITakeDamage planet))
            {
                planet.TakeDamage(this.rocketData.Damage);
            }
            else
            {
                Debug.Log($"Planet {collision.collider.name} not Damagable", collision.gameObject);
            }

            MonoBehaviour.Destroy(this.gameObject);
        }
        else if (collision.collider.CompareTag(Constants.SUN_TAG))
        {
            MonoBehaviour.Destroy(this.gameObject);
        }
    }

    void ISetupRocket.RocketData(RocketData rocketData)
    {
        this.rocketData = rocketData;
    }

    void ISetupRocket.PlanetsDamage(Dictionary<int, ITakeDamage> planetsDamage)
    {
        this.planetsDamage = planetsDamage;
    }
}
