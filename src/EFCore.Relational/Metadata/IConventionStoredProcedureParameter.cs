// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     Represents a <see cref="IConventionStoredProcedure" /> parameter.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-database-functions">Database functions</see> for more information and examples.
/// </remarks>
public interface IConventionStoredProcedureParameter : IConventionAnnotatable, IReadOnlyStoredProcedureParameter
{
    /// <summary>
    ///     The <see cref="IConventionStoredProcedure" /> to which this parameter belongs.
    /// </summary>
    new IConventionStoredProcedure IStoredProcedure { get; }

    /// <summary>
    ///     The <see cref="IConventionStoredProcedureParameterBuilder" /> for configuring this function parameter.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the function has been removed from the model.</exception>
    new IConventionStoredProcedureParameterBuilder Builder { get; }

    /// <summary>
    ///     Returns the configuration source for the parameter.
    /// </summary>
    /// <returns>The configuration source for the parameter.</returns>
    ConfigurationSource GetConfigurationSource();

    /// <summary>
    ///     Sets the store type of the parameter.
    /// </summary>
    /// <param name="storeType">The store type of the parameter.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    string? SetStoreType(string? storeType, bool fromDataAnnotation = false);

    /// <summary>
    ///     Returns the configuration source for <see cref="IReadOnlyStoredProcedureParameter.StoreType" />.
    /// </summary>
    /// <returns>The configuration source for <see cref="IReadOnlyStoredProcedureParameter.StoreType" />.</returns>
    ConfigurationSource? GetStoreTypeConfigurationSource();

    /// <summary>
    ///     Sets the type mapping of the parameter.
    /// </summary>
    /// <param name="typeMapping">The type mapping of the parameter in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    RelationalTypeMapping? SetTypeMapping(RelationalTypeMapping? typeMapping, bool fromDataAnnotation = false);

    /// <summary>
    ///     Returns the configuration source for <see cref="IReadOnlyStoredProcedureParameter.TypeMapping" />.
    /// </summary>
    /// <returns>The configuration source for <see cref="IReadOnlyStoredProcedureParameter.TypeMapping" />.</returns>
    ConfigurationSource? GetTypeMappingConfigurationSource();
}
