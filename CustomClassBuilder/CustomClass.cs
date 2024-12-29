using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SEP.CustomClassBuilder
{
    internal class CustomClass
    {
        public string className;
        public List<(string PropertyName, Type PropertyType)> properties;
        public Type newType;
        public dynamic instance;
        private static Type CreateNewClass(string className, List<(string PropertyName, Type PropertyType)> properties)
        {
            var assemblyName = new AssemblyName("DynamicAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            var typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public);

            // Add properties
            foreach (var property in properties)
            {
                var fieldBuilder = typeBuilder.DefineField($"_{property.PropertyName}", property.PropertyType, FieldAttributes.Private);

                var propertyBuilder = typeBuilder.DefineProperty(property.PropertyName, PropertyAttributes.HasDefault, property.PropertyType, null);

                // Define the 'get' accessor
                var getMethodBuilder = typeBuilder.DefineMethod(
                    $"get_{property.PropertyName}",
                    MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                    property.PropertyType,
                    Type.EmptyTypes);

                var getIl = getMethodBuilder.GetILGenerator();
                getIl.Emit(OpCodes.Ldarg_0);
                getIl.Emit(OpCodes.Ldfld, fieldBuilder);
                getIl.Emit(OpCodes.Ret);

                // Define the 'set' accessor
                var setMethodBuilder = typeBuilder.DefineMethod(
                    $"set_{property.PropertyName}",
                    MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                    null,
                    new[] { property.PropertyType });

                var setIl = setMethodBuilder.GetILGenerator();
                setIl.Emit(OpCodes.Ldarg_0);
                setIl.Emit(OpCodes.Ldarg_1);
                setIl.Emit(OpCodes.Stfld, fieldBuilder);
                setIl.Emit(OpCodes.Ret);

                propertyBuilder.SetGetMethod(getMethodBuilder);
                propertyBuilder.SetSetMethod(setMethodBuilder);
            }

            // Create the class
            return typeBuilder.CreateType();
        }
        public CustomClass(string className, List<(string PropertyName, Type PropertyType)> properties)
        {
            this.className = className;
            this.properties = properties;
            this.newType = CreateNewClass(className, properties);
            this.instance = Activator.CreateInstance(newType);
        }
        public CustomClass(string className, List<(string PropertyName, Type PropertyType, string PropertyValue)> multProperties)
        {
            this.className = className;
            this.properties = multProperties?.Select(property => (property.PropertyName, property.PropertyType)).ToList()
           ?? new List<(string PropertyName, Type PropertyType)>();
            this.newType = CreateNewClass(className, this.properties);
            this.instance = Activator.CreateInstance(newType);

            foreach (var prop in multProperties)
            {
                this.setProp(prop.PropertyName, prop.PropertyValue);
            }
        }
        public void setProp(string name, string value)
        {
            System.Diagnostics.Debug.WriteLine(name);
            System.Diagnostics.Debug.WriteLine(value);
            System.Diagnostics.Debug.WriteLine(value);
            var property = newType.GetProperty(name);
            if (property == null)
            {
                throw new ArgumentException($"Property '{name}' does not exist on type '{newType.Name}'.");
            }
            var targetType = property.PropertyType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }
            object convertedValue = null;
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null)
                    {
                        throw new InvalidCastException($"Cannot assign null or empty string to non-nullable type '{targetType.Name}'.");
                    }
                    convertedValue = null;
                }
                else
                {
                    if (targetType.IsEnum)
                    {
                        convertedValue = Enum.Parse(targetType, value);
                    }
                    else if (targetType == typeof(DateTime))
                    {
                        convertedValue = DateTime.Parse(value);
                    }
                    else if (targetType == typeof(bool))
                    {
                        if(value == "true")
                        {
                            convertedValue = true;
                        }else
                        {
                            convertedValue = false;
                        }
                    }
                    else
                    {
                        convertedValue = Convert.ChangeType(value, targetType);
                    }
                }
                property.SetValue(instance, convertedValue);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Cannot convert value '{value}' to type '{targetType.Name}'.", ex);
            }
        }

        public object getProp(string name)
        {
            return newType.GetProperty(name).GetValue(instance);
        }
        public BsonDocument ToBsonDocument()
        {
            var bsonDocument = new BsonDocument();

            // Add dynamic instance properties and values
            foreach (var property in newType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyName = property.Name;
                var value = property.GetValue(instance);

                if (value != null)
                {
                    bsonDocument[propertyName] = BsonValue.Create(value);
                }
                else
                {
                    bsonDocument[propertyName] = BsonNull.Value;
                }
            }

            return bsonDocument;
        }
    }
}