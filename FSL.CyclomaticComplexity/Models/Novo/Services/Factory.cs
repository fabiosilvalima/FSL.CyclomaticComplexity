using System;
using System.Web.Mvc;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    // generico
    public class Factory : IFactory
    {
        public object InstanceOf(string typeName)
        {
            return InstanceOf(Type.GetType(typeName));
        }

        public TConvertModel InstanceOf<TConvertModel>(Type type)
        {
            return (TConvertModel)InstanceOf(type);
        }

        public TConvertModel InstanceOf<TConvertModel>(string type)
        {
            return (TConvertModel)InstanceOf(type);
        }

        public object InstanceOf(Type type)
        {
            return DependencyResolver.Current.GetService(type);
        }

        public TModel InstanceOf<TModel>()
        {
            return DependencyResolver.Current.GetService<TModel>();
        }

        public TConvertModel InstanceOf<TConvertModel>(Enum enumType)
        {
            var typeName = GetNamespace($"{enumType.ToString()}{enumType.GetType().Name}");

            return InstanceOf<TConvertModel>(typeName);
        }
        
        private string GetNamespace(string typeName, string sufix = "Service")
        {
            return $"FSL.CyclomaticComplexity.Models.Novo.Services.{typeName}{sufix}";
        }
    }
}