using System;

namespace mvc
{
    public static class PlayerModel
    {
        public static event Action AxisChanged;

        public static float AxisY
        {
            get => _axisY;
            set
            {
                _axisY = value;
                AxisChanged?.Invoke();
            }
        }

        public static float AxisX
        {
            get => _axisX;
            set
            {
                _axisX = value; 
                AxisChanged?.Invoke();
            }
        }
    
        public static float DeltaTime { get; set; }
        public static float Speed { get; set; }

        private static float _axisY;
        private static float _axisX;
    
    }
}
