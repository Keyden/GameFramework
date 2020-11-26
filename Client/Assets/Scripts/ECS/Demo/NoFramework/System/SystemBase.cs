using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramwork.Demo.Ecs
{
    public class SystemBase
    {
        public GameWorld world;
        public SystemBase(GameWorld world)
        {
            this.world = world;
        }

        
    }

}