// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     Represents a relational database function in an model in
///     the form that can be mutated while the model is being built.
/// </summary>
public interface IMutableStoredProcedure : IReadOnlyStoredProcedure, IMutableAnnotatable
{
    /// <summary>
    ///     Gets or sets the name of the function in the database.
    /// </summary>
    new string Name { get; set; }

    /// <summary>
    ///     Gets or sets the schema of the function in the database.
    /// </summary>
    new string? Schema { get; set; }

    /// <summary>
    ///     Gets the entity type in which this function is defined.
    /// </summary>
    new IMutableEntityType EntityType { get; }

    /// <summary>
    ///     Gets the parameters for this function
    /// </summary>
    new IReadOnlyList<IMutableStoredProcedureParameter> Parameters { get; }
}
