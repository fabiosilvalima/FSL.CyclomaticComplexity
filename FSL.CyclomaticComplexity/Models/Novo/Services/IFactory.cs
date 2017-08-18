using System;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public interface IFactory
    {
        TModel InstanceOf<TModel>();
        object InstanceOf(string typeName);
        TConvertModel InstanceOf<TConvertModel>(string type);
        TConvertModel InstanceOf<TConvertModel>(Type type);
        TConvertModel InstanceOf<TConvertModel>(Enum enumType);
    }
}