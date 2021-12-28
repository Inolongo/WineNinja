using UnityEngine;

namespace mvc
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject playerGameObject;
    
        private void Awake()
        {
            PlayerModel.AxisChanged += OnAxisChanged;
        }

        private void OnAxisChanged()
        {
            playerGameObject.transform.position = new Vector3(
                playerGameObject.transform.position.x + PlayerModel.Speed * PlayerModel.AxisX * PlayerModel.DeltaTime,
                playerGameObject.transform.position.y + PlayerModel.Speed * PlayerModel.AxisY * PlayerModel.DeltaTime);
        }
    }
}
