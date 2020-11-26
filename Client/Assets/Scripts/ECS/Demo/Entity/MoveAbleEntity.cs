using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramwork.Demo.Ecs
{
    public class MoveAbleEntity:Entity
    {
        public SpeedComponent speed;
        public MoveAbleEntity() : base()
        {
            speed = new SpeedComponent() { entity = this };
        }
    }
}
