using System;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.DynamicProxy;

namespace Esri.CommonUtils
{
    public class NotifierInterceptor : IInterceptor 
    {
        private PropertyChangedEventHandler _handler;
        public static Dictionary<String, PropertyChangedEventArgs> Cache =
          new Dictionary<string, PropertyChangedEventArgs>();

        public void Intercept(IInvocation invocation)
        {
            //Each subscription to the PropertyChangedEventHandler is intercepted (add)
            if (invocation.Method.Name == "add_PropertyChanged")
            {
                _handler = (PropertyChangedEventHandler)
                      Delegate.Combine(_handler, (Delegate)invocation.Arguments[0]);
                invocation.ReturnValue = _handler;
            }
            //Each de-subscription to the PropertyChangedEventHandler is intercepted (remove)
            else if (invocation.Method.Name == "remove_PropertyChanged")
            {
                _handler = (PropertyChangedEventHandler)
                   Delegate.Remove(_handler, (Delegate)invocation.Arguments[0]);
                invocation.ReturnValue = _handler;
            }
            //Each setter raise a PropertyChanged event
            else if (invocation.Method.Name.StartsWith("set_"))
            {
                //Do the setter execution
                invocation.Proceed();
                //Launch the event after the execution
                if (_handler != null)
                {
                    PropertyChangedEventArgs arg =
                      retrievePropertyChangedArg(invocation.Method.Name);
                    _handler(invocation.Proxy, arg);
                }
            }
            else invocation.Proceed();
        }

        private PropertyChangedEventArgs retrievePropertyChangedArg(String methodName)
        {
            PropertyChangedEventArgs arg = null;
            NotifierInterceptor.Cache.TryGetValue(methodName, out arg);
            if (arg == null)
            {
                arg = new PropertyChangedEventArgs(methodName.Substring(4));
                NotifierInterceptor.Cache.Add(methodName, arg);
            }
            return arg;
        }
    }
}