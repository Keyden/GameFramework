using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GameFramwork.Demo.Ecs
{
    /// <summary>
    /// 与Unity组件的桥接
    /// </summary>
    public class GameObjectComponent: BaseComponent
    {
        public GameObject gameObject;
        public Transform transform;
        public SpriteRenderer spriteRenderer;
    }
}
