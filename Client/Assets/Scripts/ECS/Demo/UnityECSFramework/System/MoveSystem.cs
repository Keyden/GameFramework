using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace GameFramwork.Demo.UnityECS
{
    public class MoveSystem : ComponentSystem
    {
        public struct Filter
        {
            public Transform tf;
            public MoveComponent moveComponent;
        }


        protected override void OnUpdate()
        {
            foreach(var entity in GetEntities<Filter>())
            {
                Vector3 pos = entity.tf.position + entity.moveComponent.moveDir * Time.deltaTime;
                entity.tf.position = pos;
            }
        }
    }
}
