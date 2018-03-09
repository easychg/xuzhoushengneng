using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CoreLibrary
{
    class ReflactHelper
    {
        public static int Model2Datatable<T>(T model) {

            Type type = model.GetType();
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(model, null);
                string name = i.Name;
            }  
            return 0;
        }
    }
}
