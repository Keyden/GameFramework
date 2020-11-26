using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramwork.Demo.Ecs
{
    public class EatSystem : SystemBase
    {
        public EatSystem(GameWorld world) : base(world) { }
        public void Update(PositionComponent sourcePosition, SizeComponent sourceSize, PositionComponent targetPosition, SizeComponent targetSize, Entity target)
        {
            float sizeSum = sourceSize.value + targetSize.value + 0.05f;
            if ((sourcePosition.value - target.position.value).sqrMagnitude < sizeSum * sizeSum)
            {
                sourceSize.value = Mathf.Sqrt(sourceSize.value * sourceSize.value + targetSize.value * targetSize.value);
                Kill(target, sourcePosition);
            }
        }
        public void Kill(Entity food, PositionComponent sourcePosition)
        {
            world.eatingSystem.CreateFrom(food.gameObject, food.position, sourcePosition);

            world.entitySystem.RemoveEntity(food);
            world.entitySystem.AddRandomEntity();
        }
    }
}
