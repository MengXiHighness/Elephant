using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Spring.Util;

namespace DS.AFP.Common.Core.Dynamic
{
    /// <summary>
    /// 动态类型（用于动态创建程序集）
    /// </summary>
    public class DynamicType
    {
        private const string ClassName = "MyClass";
        public static Type DynamicCreateType()
        {

            //动态创建程序集

            AssemblyName DemoName = new AssemblyName("DynamicAssembly");

            AssemblyBuilder dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(DemoName, AssemblyBuilderAccess.RunAndSave);

            //动态创建模块

            ModuleBuilder mb = dynamicAssembly.DefineDynamicModule(DemoName.Name, DemoName.Name + ".dll");

            //动态创建类MyClass

            TypeBuilder tb = mb.DefineType("MyClass", TypeAttributes.Public);

            //动态创建字段

            FieldBuilder fb = tb.DefineField("myField", typeof(System.String), FieldAttributes.Public);

            //动态创建构造函数

            Type[] clorType = new Type[] { typeof(System.String) };

            ConstructorBuilder cb1 = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, clorType);

            //生成指令

            ILGenerator ilg = cb1.GetILGenerator();//生成 Microsoft 中间语言 (MSIL) 指令

            ilg.Emit(OpCodes.Ldarg_0);

            ilg.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));

            ilg.Emit(OpCodes.Ldarg_0);

            ilg.Emit(OpCodes.Ldarg_1);

            ilg.Emit(OpCodes.Stfld, fb);

            ilg.Emit(OpCodes.Ret);

            CustomAttributeBuilder myCABuilder2 = ReflectionUtils.CreateCustomAttribute(new AssemblyDescriptionAttribute("aaaaa"));

            //动态创建属性

            PropertyBuilder pb = tb.DefineProperty("MyProperty", PropertyAttributes.HasDefault, typeof(string), null);

            //动态创建方法

            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName;

            MethodBuilder myMethod = tb.DefineMethod("get_Field", getSetAttr, typeof(string), Type.EmptyTypes);

            

            //生成指令

            ILGenerator numberGetIL = myMethod.GetILGenerator();

            numberGetIL.Emit(OpCodes.Ldarg_0);

            numberGetIL.Emit(OpCodes.Ldfld, fb);

            numberGetIL.Emit(OpCodes.Ret);

            pb.SetGetMethod(myMethod);

            pb.SetCustomAttribute(myCABuilder2);

            //使用动态类创建类型

            Type classType = tb.CreateType();

            //保存动态创建的程序集 (程序集将保存在程序目录下调试时就在Debug下)

          //  dynamicAssembly.Save(DemoName.Name + ".dll");

            //创建类

            
            return classType;

        }
    }
}
