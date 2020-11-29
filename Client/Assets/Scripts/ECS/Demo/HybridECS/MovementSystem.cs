using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameFramwork.Demo.UnityECS.HyBrid
{
    public class MovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation tl,ref MovementComponent mc) =>
            {
                tl.Value.y += mc.moveSpeed * Time.deltaTime;
                if(tl.Value.y>4|| tl.Value.y < -4)
                {
                    mc.moveSpeed *= -1f;
                }
            });
        }
    }
}