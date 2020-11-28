
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;


namespace GameFramwork.Demo.UnityECS
{
    public class MoveSystem : ComponentSystem
    {
        //public struct Filter
        //{
        //    public Transform tf;
        //    public MoveComponent moveComponent;
        //}


        protected override void OnUpdate()
        {
            //foreach(var entity in GetEntities<Filter>())
            //{
            //    Vector3 pos = entity.tf.position + entity.moveComponent.moveDir * Time.deltaTime;
            //    entity.tf.position = pos;
            //}

            Entities.ForEach((ref MoveComponent moveComponent, ref Translation tl) =>
            {

                float3 pos = tl.Value + moveComponent.moveDir * Time.deltaTime;
                
                
                tl.Value = pos;
            });
        }
    }
}
