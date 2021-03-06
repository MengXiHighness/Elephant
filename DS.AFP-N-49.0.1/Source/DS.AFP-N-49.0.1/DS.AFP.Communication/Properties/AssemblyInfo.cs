﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("DS.AFP.Communication")]
[assembly: AssemblyDescription("通信基础组件")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("迪爱斯通信设备有限公司")]
[assembly: AssemblyProduct("DS.AFP.Communication")]
[assembly: AssemblyCopyright("Copyright © 迪爱斯通信设备有限公司 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("11286517-114e-4524-acc8-c980d2871545")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“内部版本号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.1.1")]
[assembly: AssemblyFileVersion("1.0.1.1")]

[assembly: SecurityTransparent]

[assembly: AllowPartiallyTrustedCallers]

// Use the .NET Framework 2.0 transparency rules (level 1 transparency) as default
#if (CLR4)
[assembly: SecurityRules(SecurityRuleSet.Level1)]
#endif
[assembly: System.Security.SecurityRules(System.Security.SecurityRuleSet.Level1)]