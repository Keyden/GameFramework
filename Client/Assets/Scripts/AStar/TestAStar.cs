using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramwork.AStar
{
    public class TestAStar : MonoBehaviour
    {
        //左上角第一个立方体位置
        public int beginX = -3;
        public int beginY = 5;

        //之后每一个立方体之间的偏移位置
        public int offsetX = 2;
        public int offsetY = -2;

        //地图格子的宽高
        public int mapW = 10;
        public int mapH = 10;

        private Vector2 beginPos = Vector2.right * -1;

        private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();
        private List<AStarNode> list;
        public Material redMat;
        public Material yellowMat;
        public Material greenMat;
        public Material whiteMat;

        // Start is called before the first frame update
        void Start()
        {
            AStarMgr.Instance.InitMapInfo(mapW, mapH);


            for (int i = 0; i < mapW; i++)
            {
                for (int j = 0; j < mapH; j++)
                {
                    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);
                    obj.name = i + "_" + j;

                    cubes.Add(obj.name, obj);

                    //得到格子判断是否是阻挡
                    AStarNode node = AStarMgr.Instance.nodes[i, j];
                    if(node.type == E_Node_Type.Stop)
                    {
                        obj.GetComponent<MeshRenderer>().material = redMat;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            //鼠标左键按下
            if (Input.GetMouseButtonDown(0))
            {
                //射线检测
                RaycastHit info;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out info, 1000))
                {
                    
                    if(beginPos == Vector2.right * -1)
                    {
                        //清理上一次路径

                        if (list != null)
                        {
                            for (int i = 0; i < list.Count; ++i)
                            {
                                cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = whiteMat;

                            }
                        }

                        string[] strs = info.collider.gameObject.name.Split('_');
                        beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                        info.collider.gameObject.GetComponent<MeshRenderer>().material = yellowMat;
                    }
                    else
                    {
                        string[] strs = info.collider.gameObject.name.Split('_');
                        Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                        cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material = whiteMat;
                        //寻路
                        list = AStarMgr.Instance.FindPath(beginPos, endPos);
                        
                        if (list != null)
                        {
                            for(int i = 0; i < list.Count; ++i)
                            {
                                cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = greenMat;

                            }
                        }

                        
                        beginPos = Vector2.right * -1;

                    }
                }
            }
        }
    }
}