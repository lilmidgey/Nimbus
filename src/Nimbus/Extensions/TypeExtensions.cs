﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Nimbus.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsClosedTypeOf(this Type type, Type openGenericType)
        {
            if (!openGenericType.IsGenericType) throw new ArgumentException("It's a bit difficult to have a closed type of a non-open-generic type", "openGenericType");

            var interfaces = type.GetInterfaces();
            var typeAndItsInterfaces = new[] {type}.Union(interfaces);
            var genericTypes = typeAndItsInterfaces.Where(i => i.IsGenericType);
            var closedGenericTypes = genericTypes.Where(i => !i.IsGenericTypeDefinition);
            var assignableGenericTypes = closedGenericTypes.Where(i => openGenericType.IsAssignableFrom(i.GetGenericTypeDefinition()));

            return assignableGenericTypes.Any();
        }

        public static bool IsClosedTypeOf(this Type type, params Type[] openGenericTypes)
        {
            return openGenericTypes.Any(type.IsClosedTypeOf);
        }

        public static bool IsInstantiable(this Type type)
        {
            if (type.IsInterface) return false;
            if (type.IsAbstract) return false;
            if (type.IsGenericType) return false;

            return true;
        }

        public static Type[] GetGenericInterfacesClosing(this Type type, Type genericInterface)
        {
            var genericInterfaces = type.GetInterfaces()
                                        .Where(i => i.IsClosedTypeOf(genericInterface))
                                        .ToArray();
            return genericInterfaces;
        }

    }
}