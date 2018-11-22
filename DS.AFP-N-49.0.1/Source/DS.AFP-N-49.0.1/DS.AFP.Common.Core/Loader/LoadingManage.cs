using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.AFP.Common.Core.Loader
{
    /// <summary>
    /// 初始化加载管理类（用来初始化数据，或提前做一些耗时的操作)
    /// </summary>
    public class LoadingManage : ILoadingManage
    {
        private static object lockObject = new object();
        private static IList<Action> loadingObjects = new List<Action>();

        /// <summary>
        /// 加载完成
        /// </summary>
        public void Loaded()
        {
            Parallel.Invoke(loadingObjects.ToArray<Action>());
        }

        public Action LoadingAction
        {
            set
            {
                lock (lockObject)
                {
                    loadingObjects.Add(value);
                }
                
            }
        }
    }
}
