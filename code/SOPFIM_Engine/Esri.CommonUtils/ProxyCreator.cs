﻿using System;
using System.ComponentModel;
using Castle.DynamicProxy;

namespace Esri.CommonUtils
{
    public class ProxyCreator
    {
        public static T MakeINotifyPropertyChanged<T>() where T : class, new()
        {
            //Creates a proxy generator
            var proxyGen = new  ProxyGenerator();
 
            //Generates a proxy using our Interceptor and implementing INotifyPropertyChanged
            var proxy = proxyGen.CreateClassProxy(
              typeof (T),
              new  Type[] { typeof (INotifyPropertyChanged) },
              ProxyGenerationOptions.Default,
              new  NotifierInterceptor()
              );
 
            return proxy as T;
        }
    }
}