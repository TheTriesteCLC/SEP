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
        public void setProp(string name, string value) 
        {
            newType.GetProperty(name).SetValue(instance, value);
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