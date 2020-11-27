using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramwork.Demo.NoFrameworkEcs
{
    public class SpeedComponent:BaseComponent
    {
        public Vector2 value;
        public float maxValue;
    }
}
