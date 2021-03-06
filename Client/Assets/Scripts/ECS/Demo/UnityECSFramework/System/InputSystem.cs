﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace GameFramwork.Demo.UnityECS
{
    public class InputSystem : ComponentSystem
    {

        //struct Data
        //{
        //    public ComponentArray<MoveComponent> moveArray;
        //}

        //[Inject] private Data _data;
        protected override void OnUpdate()
        {
            //for(int i = 0; i < _data.moveArray.Length; ++i)
            //{
            //    _data.moveArray[i].moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            //}
            Entities.ForEach((ref MoveComponent moveComponent) =>
            {
                moveComponent.moveDir = new float3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            });
        }
    }
}
