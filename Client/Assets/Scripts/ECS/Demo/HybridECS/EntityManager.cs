using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameFramwork.Demo.UnityECS.HyBrid
{
    public class EntityManager : MonoBehaviour
    {
        public GameObject goPrefab;

        public int XNum = 10;
        public int YNum = 10;


        private Entity entity;
        private Unity.Entities.EntityManager entityMgr;

        private void Start()
        {
            if(goPrefab == null)
            {
                Debug.Log("goPrefab is null请检查");
            }

            entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(goPrefab, World.Active);

            entityMgr = World.Active.EntityManager;

            for(int x = 0; x < XNum; x++)
            {
                for(int y = 0; y < YNum; y++)
                {
                    //克隆一个实体
                    var entityClone = entityMgr.Instantiate(entity);
                    //对于克隆实体，定义其初始位置
                    Vector3 position = transform.TransformPoint(new float3(x - XNum / 2, noise.cnoise(new float2(x, y) * 0.12f), y - YNum / 2));

                    //实体管理器中设置组件参数
                    entityMgr.SetComponentData(entityClone, new Translation()
                    {
                        Value = position
                    });

                    //把定义的组件加入到实体管理器
                    entityMgr.AddComponentData(entityClone, new MovementComponent() { moveSpeed = 1f });
                }
            }
        }
    }
}