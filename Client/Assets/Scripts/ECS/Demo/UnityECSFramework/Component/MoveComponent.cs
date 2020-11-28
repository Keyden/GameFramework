using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace GameFramwork.Demo.UnityECS
{


    public struct MoveComponent : IComponentData
    {

        public float3 moveDir;
    }
}