using Assets.Scripts.Player.Control;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private PlayerControlContext playerControlContext;
        [SerializeField] private IControllable controller;
        [SerializeField] private ShootingСontrol shootingСontrol;
        [SerializeField] private Projection projection;


        private void Awake() {
            if (controller is null) controller = GetComponent<IControllable>();
            if (shootingСontrol is null) shootingСontrol = GetComponent<ShootingСontrol>();
            if (playerControlContext is null) playerControlContext = controller.GetContext();
            if (projection is null) projection = GetComponent<Projection>();

        }

        private void Start() {
            playerControlContext.OnPlayerStateChanged += PlayerControlContext_OnPlayerStateChanged;
        }

        private void PlayerControlContext_OnPlayerStateChanged(object sender, PlayerState e) {
            if (playerControlContext.GetPlayerState() == PlayerState.Aim)
            {
                projection.EnableLine();
                shootingСontrol.TakeAim(projection);
            }
            else projection.DisableLine();
        }

        private void Update() {
            controller.Move();
            controller.GetContext();
        }
    }
}
