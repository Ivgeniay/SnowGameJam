using Assets.Scripts;
using UnityEngine;

public class SnowBallProjectile : MonoBehaviour, IBullet
{
    [SerializeField] private Transform impactEffect;
    public Transform Creator { get; set; } 
    private Rigidbody _rigidbody;
    private bool _isGhost;
    public bool Collided;
    private float damage;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(Vector3 velocity, bool isGhost = false)
    {
        _isGhost = isGhost;
        damage = velocity.magnitude;
        _rigidbody.AddForce(velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isGhost)
        {
            Instantiate(impactEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
        }
        Destroy(gameObject);
        Collided = true;

    }
    public Transform GetCreater() => Creator;
    public float GetDamage()
    {
        return damage;
    }
}
