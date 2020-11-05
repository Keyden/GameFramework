using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramwork
{
    /// <summary>
    /// 模块管理基类
    /// </summary>
    public abstract class ManagerBase
    {
        /// <summary>
        /// 模块优先级,优先级高的模块会先被轮询,并且后关闭
        /// </summary>
        public virtual int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}

