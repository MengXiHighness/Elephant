using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections.ObjectModel;
using DS.AFP.Common.Core.Reflect;
using System.Collections;
using Nelibur.ObjectMapper;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 数据适配（负责数据填充，dataTable、ObservableCollection、IList等的数据转换）
    /// </summary>
    public static class DataAdapterExtensions
    {
        /// <summary>
        /// 类型转换，可以动态把一个类型数据填充到另一个属性一样的类型
        /// </summary>
        /// <typeparam name="A">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">源数据</param>
        /// <returns>目标数据</returns>
        public static T ConvertEntity<A, T>(this A data) where T : new()
        {
            if (data == null)
                return default(T);

            T t = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = typeof(A).GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Type type = p.PropertyType;
                if (!type.IsGenericType)
                {
                    var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name && p1.PropertyType.IsAssignableFrom(p.PropertyType));

                    if (prop == null)
                        continue;
                    p.FastSetValue(t, prop.FastGetValue(data));
                }
                else
                {
                    Type genericTypeDefinition = type.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);

                        if (prop == null)
                            continue;
                        if (prop.FastGetValue(data) == null)
                            continue;
                        if (prop.PropertyType == type)
                        {
                            p.FastSetValue(t, prop.FastGetValue(data));
                            continue;
                        }
                        object obj = Convert.ChangeType(prop.FastGetValue(data), Nullable.GetUnderlyingType(type));
                        p.FastSetValue(t, obj);
                    }
                    else if (genericTypeDefinition == typeof(IList<>))
                    {
                        //obj = Convert.ChangeType(o[p.Name], Nullable.GetUnderlyingType(type));
                        Type innerType = type.GetGenericArguments()[0];
                        var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                        var o1 = a.FastGetValue(data);
                        IList o2 = o1 as IList;
                        if (o1 == null)
                            continue;
                        Type[] typeArgs = { innerType };
                        Type constructed = typeof(List<>).MakeGenericType(typeArgs);

                        var innerListObj = Activator.CreateInstance(constructed);
                        IList innerListObj1 = innerListObj as IList;
                        foreach (var i in o2)
                        {
                            PropertyInfo[] innerProperties = innerType.GetProperties();
                            PropertyInfo[] innerdataProperties = i.GetType().GetProperties();
                            var innerObj = Activator.CreateInstance(innerType);
                            foreach (PropertyInfo pi in innerProperties)
                            {
                                Type type1 = pi.PropertyType;
                                if (!type1.IsGenericType)
                                {
                                    var prop = innerdataProperties.FirstOrDefault(p1 => p1.Name == pi.Name && p1.PropertyType.IsAssignableFrom(type1));

                                    if (prop == null)
                                        continue;

                                    pi.FastSetValue(innerObj, prop.FastGetValue(i));
                                }
                            }
                            innerListObj1.Add(innerObj);
                        }
                        p.FastSetValue(t, innerListObj);

                    }
                }
            }
            return t;
        }

        public static T ConvertEntity<S, T>(this S sdata, T tdata) where T : new()
        {
            return TinyMapper.Map<S, T>(sdata, tdata);
        }

        /// <summary>
        /// 可以对内部集合也进行转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tdata"></param>
        /// <returns></returns>
        public static T ConvertEntity<T>(this object tdata) where T : new()
        {
            //return TinyMapper.Map<T>(data);
            if (tdata == null)
                return default(T);

            T t = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = tdata.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Type type = p.PropertyType;
                if (!type.IsGenericType)
                {
                    var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name && p1.PropertyType.IsAssignableFrom(p.PropertyType));

                    if (prop == null)
                        continue;
                    p.FastSetValue(t, prop.FastGetValue(tdata));
                }
                else
                {
                    Type genericTypeDefinition = type.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);

                        if (prop == null)
                            continue;
                        if (prop.FastGetValue(tdata) == null)
                            continue;
                        if (prop.PropertyType == type)
                        {
                            p.FastSetValue(t, prop.FastGetValue(tdata));
                            continue;
                        }
                        object obj = Convert.ChangeType(prop.FastGetValue(tdata), Nullable.GetUnderlyingType(type));
                        p.FastSetValue(t, obj);
                    }
                    else if (genericTypeDefinition == typeof(IList<>))
                    {
                        //obj = Convert.ChangeType(o[p.Name], Nullable.GetUnderlyingType(type));
                        Type innerType = type.GetGenericArguments()[0];
                        var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                        var o1 = a.FastGetValue(tdata);
                        IList o2 = o1 as IList;
                        if (o1 == null)
                            continue;
                        Type[] typeArgs = { innerType };
                        Type constructed = typeof(List<>).MakeGenericType(typeArgs);

                        var innerListObj = Activator.CreateInstance(constructed);
                        IList innerListObj1 = innerListObj as IList;
                        foreach (var i in o2)
                        {
                            PropertyInfo[] innerProperties = innerType.GetProperties();
                            PropertyInfo[] innerdataProperties = i.GetType().GetProperties();
                            var innerObj = Activator.CreateInstance(innerType);
                            foreach (PropertyInfo pi in innerProperties)
                            {
                                Type type1 = pi.PropertyType;
                                if (!type1.IsGenericType)
                                {
                                    var prop = innerdataProperties.FirstOrDefault(p1 => p1.Name == pi.Name && p1.PropertyType.IsAssignableFrom(type1));

                                    if (prop == null)
                                        continue;

                                    pi.FastSetValue(innerObj, prop.FastGetValue(i));
                                }
                            }
                            innerListObj1.Add(innerObj);
                        }
                        p.FastSetValue(t, innerListObj);

                    }
                }
            }
            return t;
        }


        /// <summary>
        /// 可以对内部集合也进行转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public static T ConvertEntity<T>(this object sdata,T tdata) where T : new()
        {
            //return TinyMapper.Map<T>(data);
            if (sdata == null)
                return default(T);

           
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = sdata.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Type type = p.PropertyType;
                if (!type.IsGenericType)
                {
                    var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name && p1.PropertyType.IsAssignableFrom(p.PropertyType));

                    if (prop == null)
                        continue;
                    p.FastSetValue(tdata, prop.FastGetValue(sdata));
                }
                else
                {
                    Type genericTypeDefinition = type.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);

                        if (prop == null)
                            continue;
                        if (prop.FastGetValue(sdata) == null)
                            continue;
                        if (prop.PropertyType == type)
                        {
                            p.FastSetValue(tdata, prop.FastGetValue(sdata));
                            continue;
                        }
                        object obj = Convert.ChangeType(prop.FastGetValue(sdata), Nullable.GetUnderlyingType(type));
                        p.FastSetValue(tdata, obj);
                    }
                    else if (genericTypeDefinition == typeof(IList<>))
                    {
                        //obj = Convert.ChangeType(o[p.Name], Nullable.GetUnderlyingType(type));
                        Type innerType = type.GetGenericArguments()[0];
                        var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                        var o1 = a.FastGetValue(sdata);
                        IList o2 = o1 as IList;
                        if (o1 == null)
                            continue;
                        Type[] typeArgs = { innerType };
                        Type constructed = typeof(List<>).MakeGenericType(typeArgs);

                        var innerListObj = Activator.CreateInstance(constructed);
                        IList innerListObj1 = innerListObj as IList;
                        foreach (var i in o2)
                        {
                            PropertyInfo[] innerProperties = innerType.GetProperties();
                            PropertyInfo[] innerdataProperties = i.GetType().GetProperties();
                            var innerObj = Activator.CreateInstance(innerType);
                            foreach (PropertyInfo pi in innerProperties)
                            {
                                Type type1 = pi.PropertyType;
                                if (!type1.IsGenericType)
                                {
                                    var prop = innerdataProperties.FirstOrDefault(p1 => p1.Name == pi.Name && p1.PropertyType.IsAssignableFrom(type1));

                                    if (prop == null)
                                        continue;

                                    pi.FastSetValue(innerObj, prop.FastGetValue(i));
                                }
                            }
                            innerListObj1.Add(innerObj);
                        }
                        p.FastSetValue(tdata, innerListObj);

                    }
                }
            }
            return tdata;
        }


        /// <summary>
        /// ObservableCollection中的类型转换，可以动态把一个类型数据填充到另一个属性一样的类型
        /// </summary>
        /// <typeparam name="A">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">源数据</param>
        /// <returns>目标数据</returns>
        public static ObservableCollection<T> ConvertObservableCollectionEntity<A, T>(this IList<A> data) where T : new()
        {
            if (data == null)
                return null;

            ObservableCollection<T> list = new ObservableCollection<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = typeof(A).GetProperties();

            foreach (A o in data)
            {
                T t = new T();
                foreach (PropertyInfo p in properties)
                {
                    Type type = p.PropertyType;
                    if (!type.IsGenericType)
                    {
                        var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name && p1.PropertyType.IsAssignableFrom(p.PropertyType));
                        if (a != null)
                            p.FastSetValue(t, a.FastGetValue(o));
                    }
                    else
                    {
                        Type genericTypeDefinition = type.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);

                            if (prop == null)
                                continue;
                            if (prop.FastGetValue(o) == null)
                                continue;
                            if (prop.PropertyType == type)
                            {
                                p.FastSetValue(t, prop.FastGetValue(o));
                                continue;
                            }
                            object obj = Convert.ChangeType(prop.FastGetValue(o), Nullable.GetUnderlyingType(type));
                            p.FastSetValue(t, obj);
                        }
                        else if (genericTypeDefinition == typeof(IList<>))
                        {

                            Type innerType = type.GetGenericArguments()[0];
                            var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                            var o1 = a.FastGetValue(o);
                            IList o2 = o1 as IList;
                            if (o1 == null)
                                continue;
                            Type[] typeArgs = { innerType };
                            Type constructed = typeof(List<>).MakeGenericType(typeArgs);

                            var innerListObj = Activator.CreateInstance(constructed);
                            IList innerListObj1 = innerListObj as IList;
                            foreach (var i in o2)
                            {
                                PropertyInfo[] innerProperties = innerType.GetProperties();
                                PropertyInfo[] innerdataProperties = i.GetType().GetProperties();
                                var innerObj = Activator.CreateInstance(innerType);
                                foreach (PropertyInfo pi in innerProperties)
                                {
                                    Type type1 = pi.PropertyType;
                                    if (!type1.IsGenericType)
                                    {
                                        var prop = innerdataProperties.FirstOrDefault(p1 => p1.Name == pi.Name && p1.PropertyType.IsAssignableFrom(type1));

                                        if (prop == null)
                                            continue;

                                        pi.FastSetValue(innerObj, prop.FastGetValue(i));
                                    }
                                }
                                innerListObj1.Add(innerObj);
                            }
                            p.FastSetValue(t, innerListObj);

                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }


        public static IList<T> ConvertListEntity<A,T>(this IList<A> data) where T : new()
        {
            IList<T> list = new List<T>();

            if (data == null)
                return list;

            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = typeof(A).GetProperties();

            foreach (A o in data)
            {
                T t = new T();
                foreach (PropertyInfo p in properties)
                {
                    Type type = p.PropertyType;
                    if (!type.IsGenericType)
                    {
                        var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name && p1.PropertyType.IsAssignableFrom(p.PropertyType));
                        if (a != null)
                            p.FastSetValue(t, a.FastGetValue(o));
                    }
                    else
                    {
                        Type genericTypeDefinition = type.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            var prop = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);

                            if (prop == null)
                                continue;
                            if (prop.PropertyType == type)
                            {
                                p.FastSetValue(t, prop.FastGetValue(o));
                                continue;
                            }
                            object obj = Convert.ChangeType(prop.FastGetValue(o), Nullable.GetUnderlyingType(type));
                            p.FastSetValue(t, obj);
                        }
                        else  if (genericTypeDefinition == typeof(IList<>))
                        {
                            
                            Type innerType = type.GetGenericArguments()[0];
                            var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                            var o1 = a.FastGetValue(o);
                            if (o1 == null)
                                continue;
                            IList o2 = o1 as IList;
                            Type[] typeArgs = { innerType };
                            Type constructed = typeof(List<>).MakeGenericType(typeArgs);

                            var innerListObj = Activator.CreateInstance(constructed);
                            IList innerListObj1 = innerListObj as IList;
                            foreach (var i in o2)
                            {
                                PropertyInfo[] innerProperties = innerType.GetProperties();
                                PropertyInfo[] innerdataProperties = i.GetType().GetProperties();
                                var innerObj = Activator.CreateInstance(innerType);
                                foreach (PropertyInfo pi in innerProperties)
                                {
                                    Type type1 = pi.PropertyType;
                                    if (!type1.IsGenericType)
                                    {
                                        var prop = innerdataProperties.FirstOrDefault(p1 => p1.Name == pi.Name && p1.PropertyType.IsAssignableFrom(type1));

                                        if (prop == null)
                                            continue;

                                        pi.FastSetValue(innerObj, prop.FastGetValue(i));
                                    }
                                }
                                innerListObj1.Add(innerObj);
                            }
                            p.FastSetValue(t, innerListObj);
                      
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }


        /// <summary>
        /// dataTable 转 ObservableCollection
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">原DataTable</param>
        /// <returns></returns>
        public static ObservableCollection<T> ConvertObservableCollectionEntity<T>(this DataTable data) where T : new()
        {

            ObservableCollection<T> list = new ObservableCollection<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (DataRow o in data.Rows)
            {
                T t = new T();
                foreach (PropertyInfo p in properties)
                {
                    if (data.Columns.Contains(p.Name))
                    {
                        if (o[p.Name] == null || o[p.Name] == System.DBNull.Value)
                        {
                            continue;
                        }
                        Type type = p.PropertyType;
                        if (type != typeof(string) && o[p.Name].ToString() == "")
                        {
                            continue;
                        }
                        object obj = null;
                        if (!type.IsGenericType)
                        {
                            obj = Convert.ChangeType(o[p.Name], type);
                        }
                        else
                        {
                            Type genericTypeDefinition = type.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                obj = Convert.ChangeType(o[p.Name], Nullable.GetUnderlyingType(type));
                            }
                        }
                        p.FastSetValue(t, obj);
                    }

                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// dataTable 转 IList
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">原DataTable</param>
        /// <returns></returns>
        public static IList<T> ConvertList<T>(this DataTable data) where T : new()
        {
            IList<T> list = new List<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (DataRow o in data.Rows)
            {
                T t = new T();
                foreach (PropertyInfo p in properties)
                {
                    if (data.Columns.Contains(p.Name))
                    {
                        if (o[p.Name] == null || o[p.Name] == System.DBNull.Value)
                        {
                            continue;
                        }
                        Type type = p.PropertyType;
                        if (type != typeof(string) && o[p.Name].ToString() == "")
                        {
                            continue;
                        }
                        object obj = null;
                        if (!type.IsGenericType)
                        {
                            obj = Convert.ChangeType(o[p.Name], type);
                        }
                        else
                        {
                            Type genericTypeDefinition = type.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                obj = Convert.ChangeType(o[p.Name], Nullable.GetUnderlyingType(type));
                            }
                        }
                        p.FastSetValue(t, obj);

                    }

                }
                list.Add(t);
            }
            return list;
        }

        
    }
}
