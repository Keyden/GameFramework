using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramwork.Demo.NoFrameworkEcs
{
    /// <summary>
    /// 临时特效型Component
    /// </summary>
    public class EatingComponent:BaseComponent
    {
        public GameObjectComponent go;
        public PositionComponent target;
        public Vector2 startOffest;
        public Vector2 endOffest;
        public float dur = 0.2f;
        public float endTime;

        /// <summary>
        /// 仅操作数据的方法可以存在
        /// </summary>
        /// <returns></returns>
        public float GetLifePercent()
        {
            return 1f - (endTime - Time.time) / dur;
        }

        public void Start()
        {
            endTime = Time.time + dur;
        }

        public Vector2 GetCurPosition()
        {
            return target.value + Vector2.Lerp(startOffest, endOffest, GetLifePercent());
        }
    }
}
