using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramwork.Demo.NoFrameworkEcs
{
    public class Entity : DItem
    {
        public GameObjectComponent gameObject;
        public PositionComponent position;
        public SizeComponent size;
        public ColorComponent color;
        public TeamComponent team;
        public Entity()
        {
            gameObject = new GameObjectComponent() { entity = this };
            position = new PositionComponent() { entity = this };
            size = new SizeComponent() { entity = this };
            color = new ColorComponent() { entity = this };
            team = new TeamComponent() { entity = this };
        }
    }

}