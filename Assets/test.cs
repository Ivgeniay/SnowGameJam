using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    [SerializeField] private InputActionReference XYAxis;
    [SerializeField] private Transform centre;
    [SerializeField] private float sensity = 100;
    [SerializeField] private Vector3 startPosition;

    //[SerializeField] private float sensityX = 9;
    //[SerializeField] private float sensityY = 9;

    //[SerializeField] private float minX = -30;
    //[SerializeField] private float maxX = 30;

    //[SerializeField] private float minY = -30;
    //[SerializeField] private float maxY = 30;


    void Start() {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        //transform.RotateAround(
        //    centre.transform.position,
        //    Getg(),
        //    Time.deltaTime * sensity);

    }

    private Vector3 Getg()
    {
        var rotationX = -InputManager.Instance.GetPlayerLook().y;
        var rotationY = InputManager.Instance.GetPlayerLook().x;

        return new Vector3(rotationX, rotationY, 0);
    }



}
