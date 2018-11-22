using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.AFP.Common.Core
{
    public static class CultureManager
    {
        #region 静态变量

        /// <summary>
        /// 当前应用程序CultureInfo
        /// </summary>
        private static CultureInfo _uiCulture;


        private static bool _synchronizeThreadCulture = true;

        #endregion

        #region 公共接口


        public static event EventHandler UICultureChanged;


        public static CultureInfo UICulture
        {
            get
            {
                if (_uiCulture == null)
                {
                    _uiCulture = Thread.CurrentThread.CurrentUICulture;
                }
                return _uiCulture;
            }
            set
            {
                if (value != UICulture)
                {
                    _uiCulture = value;
                    Thread.CurrentThread.CurrentUICulture = value;
                    if (SynchronizeThreadCulture)
                    {
                        SetThreadCulture(value);
                    }
                    if (UICultureChanged != null)
                    {
                        UICultureChanged(null, EventArgs.Empty);
                    }
                }
            }
        }


        public static bool SynchronizeThreadCulture
        {
            get { return _synchronizeThreadCulture; }
            set
            {
                _synchronizeThreadCulture = value;
                if (value)
                {
                    SetThreadCulture(UICulture);
                }
            }
        }

        #endregion

        #region 私有方法



        private static void SetThreadCulture(CultureInfo value)
        {
            if (value.IsNeutralCulture)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(value.Name);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = value;
            }
        }

        #endregion

    }
}
