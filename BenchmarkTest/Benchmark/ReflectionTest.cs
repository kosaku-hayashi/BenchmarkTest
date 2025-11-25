using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkApp;

//[MinColumn, MaxColumn]
//[AsciiDocExporter]
//[HtmlExporter]
[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MarkdownExporterAttribute.GitHub]
public class ReflectionTest
{
    static PropertyInfo idProperty;
    static PropertyInfo nameProperty;
    static Action<TestModel, int> setId;
    static Action<TestModel, string> setName;

    static ReflectionTest()
    {
        var type = typeof(TestModel);
        idProperty = type.GetProperty(nameof(TestModel.Id))!;
        nameProperty = type.GetProperty(nameof(TestModel.Name))!;
        setId = CreateSetDelegate<TestModel,int>(idProperty);
        setName = CreateSetDelegate<TestModel,string>(nameProperty);
    }

    [Benchmark]
    public void NonReflection()
    {
        var model = new TestModel();
        model.Id = 100;
        model.Name = "name";
    }

    [Benchmark]
    public void Reflection()
    {
        var model = new TestModel();
        model.GetType().GetProperty(nameof(TestModel.Id))?.SetValue(model, 100);
        model.GetType().GetProperty(nameof(TestModel.Name))?.SetValue(model, "name");
    }

    [Benchmark]
    public void Cache()
    {
        var model = new TestModel();
        idProperty.SetValue(model, 100);
        nameProperty.SetValue(model, "name");
    }

    [Benchmark]
    public void ExpressionTree()
    {
        var model = new TestModel();
        setId(model, 100);
        setName(model, "name");
    }

    static Action<TModel,TValue> CreateSetDelegate<TModel,TValue>(PropertyInfo property)
    {
        var targetExp = Expression.Parameter(typeof(TModel), "target");
        var valueExp = Expression.Parameter(typeof(TValue), "value");
        var propertyExp = Expression.Property(targetExp, property);
        var assignExp = Expression.Assign(propertyExp, valueExp);

        var lambda = Expression.Lambda<Action<TModel, TValue>>(assignExp, targetExp, valueExp);
        return lambda.Compile();
    }

    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}