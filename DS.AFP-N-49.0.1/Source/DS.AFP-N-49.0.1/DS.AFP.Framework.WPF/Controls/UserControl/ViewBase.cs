using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    public interface IViewBase
    {      
        void Init(ViewParameters viewParameters);       
        bool Vildate();
        bool Save(ViewParameters viewParameters);
        bool Close(ViewParameters viewParameters);


    }
}
