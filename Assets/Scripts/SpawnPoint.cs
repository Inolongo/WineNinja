using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class SpawnPoint
    {
        [SerializeField] private Transform _pointPosition;
        private bool _isAvailable = true;

        public Transform PointPosition => _pointPosition;

        public bool IsAvailable
        {
            get => _isAvailable;
            set => _isAvailable = value;
        }
    }
}