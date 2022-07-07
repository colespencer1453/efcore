// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class StoredProcedure : ConventionAnnotatable//, IMutableStoredProcedure, IConventionStoredProcedure, IRuntimeStoredProcedure
{
    private readonly List<DbFunctionParameter> _parameters = new();
    private string? _schema;
    private string? _name;
    private InternalDbFunctionBuilder? _builder;

    private ConfigurationSource _configurationSource;
    private ConfigurationSource? _schemaConfigurationSource;
    private ConfigurationSource? _nameConfigurationSource;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public StoredProcedure(
        IMutableEntityType entityType,
        ConfigurationSource configurationSource)
    {
        EntityType = entityType;
        _configurationSource = configurationSource;
        _builder = new(this, ((IConventionModel)entityType).Builder);
    }

    /// <inheritdoc />
    public virtual IMutableEntityType EntityType { get; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalStoredProcedureBuilder Builder
    {
        [DebuggerStepThrough]
        get => _builder ?? throw new InvalidOperationException(CoreStrings.ObjectRemovedFromModel);
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual bool IsInModel
        => _builder is not null;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual void SetRemovedFromModel()
        => _builder = null;

    /// <summary>
    ///     Indicates whether the function is read-only.
    /// </summary>
    public override bool IsReadOnly
        => ((Annotatable)EntityType).IsReadOnly;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IEnumerable<IDbFunction> GetDbFunctions(IReadOnlyModel model)
        => ((SortedDictionary<string, IDbFunction>?)model[RelationalAnnotationNames.DbFunctions])
            ?.Values
            ?? Enumerable.Empty<IDbFunction>();

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IReadOnlyDbFunction? FindDbFunction(IReadOnlyModel model, MethodInfo methodInfo)
        => model[RelationalAnnotationNames.DbFunctions] is SortedDictionary<string, IDbFunction> functions
            && functions.TryGetValue(GetFunctionName(methodInfo), out var dbFunction)
                ? dbFunction
                : null;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IReadOnlyDbFunction? FindDbFunction(IReadOnlyModel model, string name)
        => model[RelationalAnnotationNames.DbFunctions] is SortedDictionary<string, IDbFunction> functions
            && functions.TryGetValue(name, out var dbFunction)
                ? dbFunction
                : null;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static DbFunction AddDbFunction(
        IMutableModel model,
        MethodInfo methodInfo,
        ConfigurationSource configurationSource)
    {
        var function = new DbFunction(methodInfo, model, configurationSource);

        GetOrCreateFunctions(model).Add(function.ModelName, function);
        return function;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static DbFunction AddDbFunction(
        IMutableModel model,
        string name,
        Type returnType,
        ConfigurationSource configurationSource)
    {
        var function = new DbFunction(name, returnType, null, model, configurationSource);

        GetOrCreateFunctions(model).Add(name, function);
        return function;
    }

    private static SortedDictionary<string, IDbFunction> GetOrCreateFunctions(IMutableModel model)
        => (SortedDictionary<string, IDbFunction>)(
            model[RelationalAnnotationNames.DbFunctions] ??= new SortedDictionary<string, IDbFunction>(StringComparer.Ordinal));

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static DbFunction? RemoveDbFunction(
        IMutableModel model,
        MethodInfo methodInfo)
    {
        if (model[RelationalAnnotationNames.DbFunctions] is SortedDictionary<string, IDbFunction> functions)
        {
            var name = GetFunctionName(methodInfo);
            if (functions.TryGetValue(name, out var function))
            {
                var dbFunction = (DbFunction)function;
                functions.Remove(name);
                dbFunction.SetRemovedFromModel();

                return dbFunction;
            }
        }

        return null;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static DbFunction? RemoveDbFunction(
        IMutableModel model,
        string name)
    {
        if (model[RelationalAnnotationNames.DbFunctions] is SortedDictionary<string, IDbFunction> functions
            && functions.TryGetValue(name, out var function))
        {
            functions.Remove(name);
            ((DbFunction)function).SetRemovedFromModel();
        }

        return null;
    }

    /// <inheritdoc />
    [DebuggerStepThrough]
    public virtual ConfigurationSource GetConfigurationSource()
        => _configurationSource;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [DebuggerStepThrough]
    public virtual void UpdateConfigurationSource(ConfigurationSource configurationSource)
        => _configurationSource = configurationSource.Max(_configurationSource);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual string? Schema
    {
        get => _schema ?? EntityType.GetDefaultSchema();
        set => SetSchema(value, ConfigurationSource.Explicit);
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual string? SetSchema(string? schema, ConfigurationSource configurationSource)
    {
        EnsureMutable();

        _schema = schema;

        _schemaConfigurationSource = configurationSource.Max(_schemaConfigurationSource);

        return schema;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual ConfigurationSource? GetSchemaConfigurationSource()
        => _schemaConfigurationSource;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual string Name
    {
        get => _name ?? MethodInfo?.Name ?? ModelName;
        set => SetName(value, ConfigurationSource.Explicit);
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual string? SetName(string? name, ConfigurationSource configurationSource)
    {
        EnsureMutable();

        _name = name;

        _nameConfigurationSource = configurationSource.Max(_nameConfigurationSource);

        return name;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual ConfigurationSource? GetNameConfigurationSource()
        => _nameConfigurationSource;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual IReadOnlyList<DbFunctionParameter> Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }
    
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [DisallowNull]
    public virtual IStoreFunction? StoreFunction { get; set; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public override string ToString()
        => ((IDbFunction)this).ToDebugString(MetadataDebugStringOptions.SingleLineDefault);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public virtual DebugView DebugView
        => new(
            () => ((IDbFunction)this).ToDebugString(),
            () => ((IDbFunction)this).ToDebugString(MetadataDebugStringOptions.LongDefault));

    /// <inheritdoc />
    IConventionDbFunctionBuilder IConventionDbFunction.Builder
    {
        [DebuggerStepThrough]
        get => Builder;
    }
    
    /// <inheritdoc />
    IReadOnlyList<IReadOnlyDbFunctionParameter> IReadOnlyDbFunction.Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }

    /// <inheritdoc />
    IReadOnlyList<IConventionDbFunctionParameter> IConventionDbFunction.Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }

    /// <inheritdoc />
    IReadOnlyList<IMutableDbFunctionParameter> IMutableDbFunction.Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }

    /// <inheritdoc />
    IReadOnlyList<IDbFunctionParameter> IDbFunction.Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }
    
    /// <inheritdoc />
    IStoreFunction IDbFunction.StoreFunction
        => StoreFunction!; // Relational model creation ensures StoreFunction is populated

    IStoreFunction IRuntimeDbFunction.StoreFunction
    {
        get => StoreFunction!;
        set => StoreFunction = value;
    }
}
