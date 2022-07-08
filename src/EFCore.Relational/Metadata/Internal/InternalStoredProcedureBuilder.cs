// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class InternalStoredProcedureBuilder :
    AnnotatableBuilder<StoredProcedure, IConventionModelBuilder>,
    IConventionStoredProcedureBuilder
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public InternalStoredProcedureBuilder(StoredProcedure storedProcedure, IConventionModelBuilder modelBuilder)
        : base(storedProcedure, modelBuilder)
    {
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalStoredProcedureBuilder? HasName(string? name, ConfigurationSource configurationSource)
    {
        if (CanSetName(name, configurationSource))
        {
            Metadata.SetName(name, configurationSource);
            return this;
        }

        return null;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual bool CanSetName(string? name, ConfigurationSource configurationSource)
        => (name != "" || configurationSource == ConfigurationSource.Explicit)
            && (configurationSource.Overrides(Metadata.GetNameConfigurationSource())
                || Metadata.Name == name);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalStoredProcedureBuilder? HasSchema(string? schema, ConfigurationSource configurationSource)
    {
        if (CanSetSchema(schema, configurationSource))
        {
            Metadata.SetSchema(schema, configurationSource);
            return this;
        }

        return null;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual bool CanSetSchema(string? schema, ConfigurationSource configurationSource)
        => configurationSource.Overrides(Metadata.GetSchemaConfigurationSource())
            || Metadata.Schema == schema;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalStoredProcedureParameterBuilder HasParameter(string name, ConfigurationSource configurationSource)
    {
        var parameter = Metadata.FindParameter(name);
        if (parameter == null)
        {
            throw new ArgumentException(
                RelationalStrings.StoredProcedureInvalidParameterName(Metadata.MethodInfo?.DisplayName(), name));
        }

        return parameter.Builder;
    }

    IConventionStoredProcedure IConventionStoredProcedureBuilder.Metadata
    {
        [DebuggerStepThrough]
        get => Metadata;
    }

    /// <inheritdoc />
    [DebuggerStepThrough]
    IConventionStoredProcedureBuilder? IConventionStoredProcedureBuilder.HasName(string? name, bool fromDataAnnotation)
        => HasName(name, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

    /// <inheritdoc />
    [DebuggerStepThrough]
    bool IConventionStoredProcedureBuilder.CanSetName(string? name, bool fromDataAnnotation)
        => CanSetName(name, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

    /// <inheritdoc />
    [DebuggerStepThrough]
    IConventionStoredProcedureBuilder? IConventionStoredProcedureBuilder.HasSchema(string? schema, bool fromDataAnnotation)
        => HasSchema(schema, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

    /// <inheritdoc />
    [DebuggerStepThrough]
    bool IConventionStoredProcedureBuilder.CanSetSchema(string? schema, bool fromDataAnnotation)
        => CanSetSchema(schema, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

    /// <inheritdoc />
    [DebuggerStepThrough]
    IConventionStoredProcedureParameterBuilder IConventionStoredProcedureBuilder.HasParameter(string name, bool fromDataAnnotation)
        => HasParameter(name, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);
}
