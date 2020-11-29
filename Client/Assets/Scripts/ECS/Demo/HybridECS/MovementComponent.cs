using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace GameFramwork.Demo.UnityECS.HyBrid
{
    public struct MovementComponent : IComponentData
    {
        public float moveSpeed;
    }
}