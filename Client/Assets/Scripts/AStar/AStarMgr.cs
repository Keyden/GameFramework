using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Analytics;

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
        public AStarNode[,] nodes;

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
            nodes = new AStarNode[w, h];
            mapW = w;
            mapH = h;
            openList = new List<AStarNode>();
            closeList = new List<AStarNode>();

            for (int i = 0; i < w; i++)
            {
                for(int j = 0; j < h; j++)
                {
                    AStarNode node = new AStarNode(i, j, Random.Range(0, 100) < 20 ? E_Node_Type.Stop : E_Node_Type.walk);

                    nodes[i, j] = node;

                }
            }
        }

        /// <summary>
        /// 寻路方法
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public List<AStarNode> FindPath(Vector2 startPos,Vector2 endPos)
        {
            List<AStarNode> path;
            //判断的点是否合法
            //1.要在地图范围内
            if (startPos.x<0 || startPos.x> mapW || startPos.y<0 || startPos.y>mapH ||
                endPos.x < 0 || endPos.x > mapW || endPos.y < 0 || endPos.y > mapH
                )
            {
                Debug.Log("开始或者结束点在地图外");
                return null;
            }
            //2.判断是否是是阻挡
            AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
            AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
            if(start.type == E_Node_Type.Stop || end.type == E_Node_Type.Stop)
            {
                Debug.Log("开始或者结束点是阻挡");
                return null;
            }
            //清空关闭和开启列表
            openList.Clear();
            closeList.Clear();

            //把开始点放入关闭列表中
            start.father = null;
            start.f = 0;
            start.g = 0;
            start.h = 0;
            closeList.Add(start);

            while (true)
            {
                //找每个点周围的8个点
                //左上 x-1 y-1
                FindNearlyNodeToOpenList(start.x - 1, start.y - 1, 1.4f, start, end);
                //上 x y-1
                FindNearlyNodeToOpenList(start.x, start.y - 1, 1f, start, end);
                //右上 x+1 y-1
                FindNearlyNodeToOpenList(start.x + 1, start.y - 1, 1.4f, start, end);
                //左 x-1 y
                FindNearlyNodeToOpenList(start.x - 1, start.y, 1f, start, end);
                //右 x+1 y
                FindNearlyNodeToOpenList(start.x + 1, start.y, 1f, start, end);
                //左下 x-1 y+1
                FindNearlyNodeToOpenList(start.x - 1, start.y + 1, 1.4f, start, end);
                //下 x y+1
                FindNearlyNodeToOpenList(start.x, start.y + 1, 1f, start, end);
                //右下 x+1 y+1
                FindNearlyNodeToOpenList(start.x + 1, start.y + 1, 1.4f, start, end);

                //死路判断 开启列表为空都还没找到终点
                if(openList.Count == 0)
                {
                    Debug.Log("未找到路径");
                    return null;
                }

                //选出开启列表中寻路消耗最小的点放入关闭列表中,再从开启列表中移除
                openList.Sort(SortOpenList);//openList中第openList.Count-1个点是f值最小的点
                int count = openList.Count;
                closeList.Add(openList[count - 1]);
                //找到的这个点又变成新的起点,进行下一次寻路
                start = openList[count - 1];

                openList.RemoveAt(count - 1);



                //如果这个点已经是终点,得到最终结果返回数据
                //如果不是终点,继续寻路
                if (start == end)
                {
                    //找到路径


                    path = new List<AStarNode>();
                    path.Add(end);
                    while (end.father!=null)
                    {
                        path.Add(end.father);
                        end = end.father;
                    }
                    //列表反转
                    path.Reverse();

                    break;
                }
            }
            return path;
        }//

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int SortOpenList(AStarNode a,AStarNode b)
        {
            if(a.f > b.f)
            {
                return -1;
            }else if(a.f == b.f)
            {
                return 1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 把临近的点放入到开启列表中
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void FindNearlyNodeToOpenList(int x, int y, float g, AStarNode father, AStarNode end)
        {
            //边界判断
            if (x < 0 || x >= mapW || y < 0 || y >= mapH)
            {
                return;
            }

            AStarNode node = nodes[x, y];
            //判断这些点 是否是边界 是否是阻挡 是否子啊开启或者关闭列表 如果都不是 才放入开启列表
            if (node == null ||
                node.type == E_Node_Type.Stop ||
                closeList.Contains(node) ||
                openList.Contains(node)
                )
            {
                return;
            }

            //计算f值 f(寻路消耗) = g(离起点距离) + h(离终点距离)
            node.father = father;
            //计算g
            node.g = father.g + g;
            //计算h
            node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
            node.f = node.g + node.h;



            openList.Add(node);
        }
    }
}

