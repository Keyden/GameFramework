using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Rendering;
using Unity.Transforms;

namespace GameFramwork.Demo.UnityECS
{
   



    public class EntitiesManager : MonoBehaviour
    {
        [SerializeField]
        private Mesh mesh;

        [SerializeField]
        private Material material;


        private void Start()
        {
            EntityManager entityManager = World.Active.EntityManager;

            EntityArchetype entityArchetype = entityManager.CreateArchetype(
                typeof(MoveComponent),
                typeof(RenderMesh),
                typeof(Translation),
                typeof(LocalToWorld)
                );


            NativeArray<Entity> entityArray = new NativeArray<Entity>(100000, Allocator.Temp);

            entityManager.CreateEntity(entityArchetype, entityArray);

            for(int i =0;i< entityArray.Length; i++)
            {
                Entity entity = entityArray[i];

                entityManager.SetSharedComponentData(entity, new RenderMesh
                {
                    mesh = mesh,
                    material = material
                });
            }

            entityArray.Dispose();
        }
    }
}