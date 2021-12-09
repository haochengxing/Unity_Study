using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace haochengxing {

    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigureAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BindingAttribute : Attribute
    {

    }


    public class Configure
    {
        [MenuItem("Reflection/Test")]
        public static void GetConfigureByTags()
        {

            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies() 
                        from type in assembly.GetTypes()
                        where type.IsDefined(typeof(ConfigureAttribute),false)
                        select type;

            foreach (var type in types)
            {
                foreach (var prop in type.GetProperties(BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.DeclaredOnly))
                {
                    if (prop.IsDefined(typeof(BindingAttribute),false) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        foreach (object applyTo in prop.GetValue(null,null) as IEnumerable)
                        {
                            Debug.Log(applyTo);
                        }
                    }
                } 
            }
        }
    }
}
    


