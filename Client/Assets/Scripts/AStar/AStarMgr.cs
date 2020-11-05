using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramwork.AStar
{
    /// <summary>
    /// A星寻路管理器
    /// </summary>
    public class AStarMgr
    {
        private static AStarMgr instance;

        public static AStarMgr Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AStarMgr();
                }
                return instance;
            }
        }

        //地图宽高
        public int mapW;
        public int mapH;

        //地图相关所有的格子对象容器
        private AStarNode[,] nodes;

        //开启列表
        private List<AStarNode> openList;

        //关闭列表
        private List<AStarNode> closeList;

        /// <summary>
        /// 初始化地图信息
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void InitMapInfo(int w,int h)
        {

        }

        /// <summary>
        /// 寻路方法
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public List<AStarNode> FindPath(Vector2 startPos,Vector2 endPos)
        {
            //判断的点是否合法
            //1.要在地图范围内
            //2.要不是阻挡

            //找每个点周围的8个点
            //判断这些点 是否是边界 是否是阻挡 是否子啊开启或者关闭列表 如果都不是 才放入开启列表
            //选出开启列表中寻路消耗最小的点放入关闭列表中,再从开启列表中移除
            //如果这个点已经是终点,得到最终结果返回数据
            //如果不是终点,继续寻路

            return null;
        }
    }
}

