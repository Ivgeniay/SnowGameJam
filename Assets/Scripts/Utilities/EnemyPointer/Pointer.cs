using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    [ExecuteInEditMode]
    public class Pointer : MonoBehaviour
    {
        [SerializeField] Transform pointerIconTransform;
        Transform player;
        Camera currentCamera;

        private Vector3 playerPosition;

        private void Awake()
        {
            if (player is null) player = GameObject.FindObjectOfType<PlayerBehavior>().transform;
            currentCamera = Camera.main;
        }

        private void Update()
        {
            playerPosition = player.position + new Vector3(0, 1.6f, 0);

            Vector3 direction = transform.position - playerPosition;

            Ray ray = new Ray(playerPosition, direction);
            Debug.DrawRay(playerPosition, direction);

            float minDistance = Mathf.Infinity;
            int planeIndex = 0;

            //0 left, 1 - right, 2 - down, 3 - up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(currentCamera);
            for (int i = 0; i < planes.Length; i++)
            {
                if (planes[i].Raycast(ray, out float distance)) {

                    if(distance < minDistance) {
                        minDistance = distance;
                        planeIndex = i;
                    }
                }
            }

            minDistance = Mathf.Clamp(minDistance, 0, direction.magnitude);
            Vector3 worldPosition = ray.GetPoint(minDistance);

            if (worldPosition.y <= 0.05f && worldPosition.y >= -0.05f) {
                pointerIconTransform.gameObject.SetActive(false);
                return;
            }
            else pointerIconTransform.gameObject.SetActive(true);

            pointerIconTransform.position = currentCamera.WorldToScreenPoint(worldPosition);
            pointerIconTransform.rotation = GetIconRotation(planeIndex);
        }

        private Quaternion GetIconRotation(int index)
        {
            switch (index)
            {
                case 0: 
                    return Quaternion.Euler(0f, 0f, 90f);
                case 1: 
                    return Quaternion.Euler(0f, 0f, -90f);
                case 2: 
                    return Quaternion.Euler(0f, 0f, 180f);
                case 3: 
                    return Quaternion.Euler(0f, 0f, 0f);
                default: return Quaternion.identity;
            }
        }
    }


}
