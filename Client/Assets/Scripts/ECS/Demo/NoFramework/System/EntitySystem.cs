using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramwork.Demo.Ecs
{
    /// <summary>
    /// 增删物体和场景初始化
    /// </summary>
    public class EntitySystem :SystemBase
    {
        public EntitySystem(GameWorld world) : base(world) { }

        public void AddEntity(Entity e)
        {
            world.entitys.DelayAdd(e);
            world.gameObjectSystem.Add(e.gameObject, e.position, e.size, e.color);
        }

        public void RemoveEntity(Entity e)
        {
            world.entitys.DelayRemove(e);
            if (e.gameObject != null)
                world.gameObjectSystem.Remove(e.gameObject);
        }

        public void AddRandomEntity()
        {
            Entity e = new Entity();
            e.size.value = 0.025f;
            e.team.id = 0;
            e.position.value = new Vector2(UnityEngine.Random.Range(world.screenRect.xMin + e.size.value, world.screenRect.xMax - e.size.value), UnityEngine.Random.Range(world.screenRect.yMin + e.size.value, world.screenRect.yMax - e.size.value));
            AddEntity(e);
        }

        public void AddMoveAbleEnity(MoveAbleEntity e)
        {
            this.AddEntity(e);
            world.playerEntitys.Add(e);

            world.moveSystem.Add(e.speed);
            world.gameObjectSystem.SetToTop(e.gameObject);
        }

        public void InitScene()
        {
            for (int i = 0; i < 50; i++)
            {
                AddRandomEntity();
            }

            for (int i = 0; i < 2; i++)
            {
                MoveAbleEntity playerEntity = new MoveAbleEntity();
                playerEntity.position.value = Vector2.zero;
                playerEntity.size.value = 0.05f;
                playerEntity.color.value = Color.yellow;
                playerEntity.speed.maxValue = 1f;
                playerEntity.team.id = 1;
                playerEntity.position.value = new Vector2(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f));
                AddMoveAbleEnity(playerEntity);
            }
        }
    }
}
