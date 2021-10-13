using System;

namespace Dio.CadastroMidia.Classes.Util
{
    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static object InvocarMetodo(object obj, string name)
        {
            return obj.GetType().GetMethod(name).Invoke(obj, null);
        }

        public static object InvocarMetodo(object obj, string name, params object[] parameters)
        {
            return obj.GetType().GetMethod(name).Invoke(obj, parameters);
        }
    }
}