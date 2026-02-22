namespace Eclipse.API.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Eclipse.API.Features;

    public static class ReflectionExtension
    {
        private const BindingFlags DefaultFlags =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance;

        public static object InvokeStaticMethod(this Type type, string methodName, params object[] parameters)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var method = type.GetMethod(methodName, DefaultFlags);

            if (method == null)
                throw new MissingMethodException(type.FullName, methodName);

            return method.Invoke(null, parameters);
        }


        public static void InvokeStaticEvent(this Type type, string eventName, params object[] parameters)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var field = type.GetField(eventName, DefaultFlags);

            if (field == null)
                throw new MissingFieldException(type.FullName, eventName);

            if (field.GetValue(null) is not MulticastDelegate eventDelegate)
                return;

            foreach (var handler in eventDelegate.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(parameters);
                }
                catch (Exception ex)
                {
                    Log.Error($"Error invoking handler '{handler.Method.Name}' from event '{eventName}': {ex}");
                }
            }
        }
        public static void CopyProperties(this object target, object source)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var type = target.GetType();

            if (type != source.GetType())
                throw new InvalidOperationException("Target and source type mismatch.");

            foreach (var property in type.GetProperties(DefaultFlags))
            {
                if (!property.CanRead || !property.CanWrite)
                    continue;

                var value = property.GetValue(source);
                property.SetValue(target, value);
            }
        }
        public static float GetFieldSafe(this object target, string fieldName, BindingFlags flags)
        {
            var field = target.GetType().GetField(fieldName, flags);
            if (field != null)
            {
                return (float)field.GetValue(target);
            }
            else
            {
                Log.Error($"Field '{fieldName}' not found on {target.GetType().Name}!");
                return default;
            }
        }
        public static T GetFieldValue<T>(this object target, string fieldName, BindingFlags flags)
        {
            var field = target.GetType().GetField(fieldName, flags);
            if (field == null)
            {
                Log.Error($"Field '{fieldName}' not found on {target.GetType().Name}!");
                return default;
            }
            return (T)field.GetValue(target);
        }
        public static void SetFieldSafe(this object target, string fieldName, object value, BindingFlags flags)
        {
            var field = target.GetType().GetField(fieldName, flags);
            if (field != null)
            {
                field.SetValue(target, value);
                Log.Info("Setted " + fieldName + " to " + value.ToString() + " on " + target.GetType().Name + "");
            }
            
            else
                Log.Error($"Field '{fieldName}' not found on {target.GetType().Name}!");
        }
    }
}