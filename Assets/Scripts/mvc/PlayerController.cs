using UnityEngine;

namespace mvc
{
    public class PlayerController : MonoBehaviour
    {
        private float axisX;
        private float axisY;

        private void Awake()
        {
            PlayerModel.Speed = ModelFromServer.PlayerSpeed;
        }

        private void Update()
        {
            axisX = Input.GetAxis("Horizontal");
            axisY = Input.GetAxis("Vertical");

            PlayerModel.AxisX = axisX;
            PlayerModel.AxisY = axisY;
            PlayerModel.DeltaTime = Time.deltaTime;
        }
    }
}