using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Loader
{
    /// <summary>
    /// 用来初始化数据，或提前做一些耗时的操作（LoadingManage基类）
    /// </summary>
    public interface ILoadingManage
    {
        /// <summary>
        /// 加载完成
        /// </summary>
        void Loaded();

        /// <summary>
        /// 需要加载的执行函数
        /// </summary>
        Action LoadingAction {  set; }
    }
}
