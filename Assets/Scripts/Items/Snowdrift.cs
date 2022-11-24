using UnityEngine;


public class Snowdrift : MonoBehaviour
{
    [SerializeField] private int Snowballs;
    [SerializeField] private Vector3 RotateSpeed = new Vector3(0, 0.2f, 0);


    private void OnTriggerEnter(Collider other)
    {
        var collisionBody = other.transform.GetComponent<Inventory>();
        if (collisionBody is null) return;

        collisionBody.AddSnowballs(Snowballs);
        Destroy(gameObject, 0.05f);
    }


    private void Update()
    {
        transform.Rotate(RotateSpeed);
    }
}
