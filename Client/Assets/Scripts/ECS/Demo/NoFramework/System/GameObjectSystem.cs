using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameFramwork.Demo.Ecs
{
    /// <summary>
    /// 和Unity显示部分的桥接
    /// </summary>
    public class GameObjectSystem : SystemBase
    {
        public GameObjectSystem(GameWorld world) : base(world) { }
        public void Add(GameObjectComponent e, PositionComponent position, SizeComponent size, ColorComponent color)
        {
            e.gameObject = new GameObject("Entity");
            e.transform = e.gameObject.transform;
            e.transform.localScale = Vector2.one * 0.001f;
            e.spriteRenderer = e.gameObject.AddComponent<SpriteRenderer>();
            e.spriteRenderer.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            Update(e, position, size, color);
        }

        public void Remove(GameObjectComponent go)
        {
            GameObject.Destroy(go.gameObject);
            go.transform = null;
            go.gameObject = null;
            go.spriteRenderer = null;
        }

        public void Update(GameObjectComponent go, PositionComponent position, SizeComponent size, ColorComponent color)
        {
            go.transform.position = position.value;
            go.transform.localScale = Vector2.one * Mathf.MoveTowards(go.transform.localScale.x, size.value * 11f, Mathf.Max(0.01f, Mathf.Abs(go.transform.localScale.x - size.value)) * 10f * Time.deltaTime);
            go.spriteRenderer.color = color.value;
        }

        public void SetToTop(GameObjectComponent go)
        {
            go.gameObject.AddComponent<SortingGroup>().sortingOrder = 1;
        }
    }
}
