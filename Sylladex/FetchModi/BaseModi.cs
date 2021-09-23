using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System;

public class BaseModi
{
    Type[] _params;
    MethodInfo _method;

    public MethodInfo GetRetrieveMethod()
    {
        if (_method == null)
        {
            _method = this
                .GetType()
                .GetMethods()
                .Where(m => m.GetCustomAttribute<RetrieveAttribute>() != null)
                .Single();
        }
        return _method;
    }

    IEnumerable<Type> GetParameterType()
    {
        if (_params == null)
        {
            var types = GetRetrieveMethod()
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            _params = types;
        }
        return _params;
    }
}