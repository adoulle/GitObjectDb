using GitObjectDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace GitObjectDb.SystemTextJson;

internal class NodeTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    public NodeTypeInfoResolver(IDataModel model)
    {
        Model = model;
    }

    public IDataModel Model { get; }

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var result = base.GetTypeInfo(type, options);

        if (typeof(Node).IsAssignableFrom(type))
        {
            ExcludeIgnoreDataMemberDecoratedProperties(type, result);
            AddPolymorphismOptions(type, result);
        }

        return result;
    }

    private static void ExcludeIgnoreDataMemberDecoratedProperties(Type type, JsonTypeInfo jsonTypeInfo)
    {
        foreach (var property in jsonTypeInfo.Properties)
        {
            var propertyInfo = type.GetProperty(property.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var attribute = propertyInfo?.GetCustomAttribute<IgnoreDataMemberAttribute>(true);
            if (attribute is not null)
            {
                property.ShouldSerialize = (_, _) => false;
            }
        }
    }

    private void AddPolymorphismOptions(Type type, JsonTypeInfo jsonTypeInfo)
    {
        jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
        {
            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
        };
        AddDerivedTypes(type, jsonTypeInfo.PolymorphismOptions.DerivedTypes);
    }

    private void AddDerivedTypes(Type nodeType, ICollection<JsonDerivedType> derivedTypes)
    {
        foreach (var type in from description in Model.NodeTypes
                             let modelType = description.Type
                             where nodeType.IsAssignableFrom(modelType)
                             where !modelType.IsAbstract
                             select modelType)
        {
            derivedTypes.Add(new(type, type.FullName));
        }
    }
}
