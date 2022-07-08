// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     Represents a relational database function in a model.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-database-functions">Database functions</see> for more information and examples.
/// </remarks>
public interface IStoredProcedure : IReadOnlyStoredProcedure, IAnnotatable
{
    /// <summary>
    ///     Gets the entity type in which this function is defined.
    /// </summary>
    new IEntityType EntityType { get; }

    /// <summary>
    ///     Gets the parameters for this function
    /// </summary>
    new IReadOnlyList<IStoredProcedureParameter> Parameters { get; }

    /// <summary>
    ///     Gets the associated <see cref="IStoreFunction" />.
    /// </summary>
    IStoreFunction StoreFunction { get; }
}
