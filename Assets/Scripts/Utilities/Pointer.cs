using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    [ExecuteAlways]
    public class Pointer : MonoBehaviour
    {
        [SerializeField] Transform pinterIconTransform;
        Transform player;
        Camera currentCamera;

        private void Awake()
        {
            if (player is null) player = GameObject.FindObjectOfType<PlayerBehavior>().transform;
            currentCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 direction = transform.position - player.position;
            Ray ray = new Ray(player.position, direction);
            Debug.DrawRay(player.transform.position, direction);

            float minDistance = Mathf.Infinity;
            int planeIndex = 0;
            //0 left, 1 - right, 2 - down, 3 - up

            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(currentCamera);
            for (int i = 0; i < planes.Length; i++)
            {
                if (planes[i].Raycast(ray, out float distance))
                {
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        planeIndex = i;
                    }
                }
            }

            minDistance = Mathf.Clamp(minDistance, 0, direction.magnitude);
            Vector3 worldPosition = ray.GetPoint(minDistance);

            pinterIconTransform.position = currentCamera.WorldToScreenPoint(worldPosition);
            pinterIconTransform.rotation = GetIconRotation(planeIndex);
        }

        private Quaternion GetIconRotation(int index)
        {
            switch (index)
            {
                case 0: 
                    return Quaternion.Euler(0, 0, 90);
                case 1: 
                    return Quaternion.Euler(0, 0, -90);
                case 2: 
                    return Quaternion.Euler(0, 0, 180);
                case 3: 
                    return Quaternion.Euler(0, 0, 0);
                default: return Quaternion.identity;
            }
        }
    }


}
