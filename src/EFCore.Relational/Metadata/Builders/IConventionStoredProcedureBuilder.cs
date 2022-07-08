// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IConventionStoredProcedure" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-conventions">Model building conventions</see> for more information and examples.
/// </remarks>
public interface IConventionStoredProcedureBuilder : IConventionAnnotatableBuilder
{
    /// <summary>
    ///     The function being configured.
    /// </summary>
    new IConventionStoredProcedure Metadata { get; }

    /// <summary>
    ///     Sets the name of the database function.
    /// </summary>
    /// <param name="name">The name of the function in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The same builder instance if the configuration was applied,
    ///     <see langword="null" /> otherwise.
    /// </returns>
    IConventionStoredProcedureBuilder? HasName(string? name, bool fromDataAnnotation = false);

    /// <summary>
    ///     Returns a value indicating whether the given name can be set for the database function.
    /// </summary>
    /// <param name="name">The name of the function in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns><see langword="true" /> if the given name can be set for the database function.</returns>
    bool CanSetName(string? name, bool fromDataAnnotation = false);

    /// <summary>
    ///     Sets the schema of the database function.
    /// </summary>
    /// <param name="schema">The schema of the function in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The same builder instance if the configuration was applied,
    ///     <see langword="null" /> otherwise.
    /// </returns>
    IConventionStoredProcedureBuilder? HasSchema(string? schema, bool fromDataAnnotation = false);

    /// <summary>
    ///     Returns a value indicating whether the given schema can be set for the database function.
    /// </summary>
    /// <param name="schema">The schema of the function in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns><see langword="true" /> if the given schema can be set for the database function.</returns>
    bool CanSetSchema(string? schema, bool fromDataAnnotation = false);

    /// <summary>
    ///     Returns an object that can be used to configure a parameter with the given name.
    /// </summary>
    /// <param name="name">The parameter name.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The builder to use for further parameter configuration.</returns>
    IConventionDbFunctionParameterBuilder HasParameter(string name, bool fromDataAnnotation = false);
}
