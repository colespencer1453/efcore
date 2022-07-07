// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IMutableStoredProcedure" /> that an entity type is mapped to.
/// </summary>
public class StoredProcedureBuilder : IInfrastructure<EntityTypeBuilder>, IInfrastructure<IConventionDbFunctionBuilder>
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public StoredProcedureBuilder(StoredProcedure sproc, EntityTypeBuilder entityTypeBuilder)
    {
        Builder = ((StoredProcedure)sproc).Builder;
        EntityTypeBuilder = entityTypeBuilder;
    }

    private EntityTypeBuilder EntityTypeBuilder { get; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    protected virtual InternalStoredProcedureBuilder Builder { [DebuggerStepThrough] get; }

    /// <inheritdoc />
    IConventionDbFunctionBuilder IInfrastructure<IConventionDbFunctionBuilder>.Instance
    {
        [DebuggerStepThrough]
        get => Builder;
    }

    /// <summary>
    ///     The function being configured.
    /// </summary>
    public virtual IMutableDbFunction Metadata
        => Builder.Metadata;

    /// <summary>
    ///     Returns an object that can be used to configure a parameter with the given name.
    ///     If no parameter with the given name exists, then a new parameter will be added.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-database-functions">Database functions</see> for more information and examples.
    /// </remarks>
    /// <param name="name">The parameter name.</param>
    /// <returns>The builder to use for further parameter configuration.</returns>
    public virtual StoredProcedureBuilder HasParameter(string name, DbFunctionParameterBuilder builder)
        => new(Builder.HasParameter(name, ConfigurationSource.Explicit).Metadata);

    EntityTypeBuilder IInfrastructure<EntityTypeBuilder>.Instance => EntityTypeBuilder;
}
