using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramwork.Demo.NoFrameworkEcs
{
    /// <summary>
    /// 操控
    /// </summary>
    public class InputSystem:SystemBase
    {
        public InputSystem(GameWorld world) : base(world) { }
        public void Update(SpeedComponent speed, PositionComponent position)
        {
            Vector2 delta = (Vector2)world.mainCamera.ScreenToWorldPoint(Input.mousePosition) - position.value;
            speed.value = Vector2.ClampMagnitude(speed.value + delta.normalized * Time.deltaTime, speed.maxValue);
        }
    }
}
