using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramwork.AStar
{
    /// <summary>
    /// 格子类型
    /// </summary>
    public enum E_Node_Type
    {
        walk,//可以走

        Stop,//不能走
    }


    public class AStarNode
    {
        //格子对象坐标
        public int x;
        public int y;


        //寻路消耗
        public float f;
        //离起点的距离
        public float g;
        //离终点的距离
        public float h;
        //父对象
        public AStarNode father;

        //格子类型
        public E_Node_Type type;

        public int FindIndex { get; set; } = 0;

        public AStarNode(int x,int y,E_Node_Type type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}

