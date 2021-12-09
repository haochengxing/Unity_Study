using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace haochengxing.editor
{
    [Configure]
    public class HotfixCfg
    {
        [Binding]
        static IEnumerable<Type> Bindings
        {
            get
            {
                var types = new List<Type>();
                types.Add(typeof(int));
                types.Add(typeof(float));
                types.Add(typeof(decimal));
                types.Add(typeof(short));

                return types;
            }
        }
    }
}


