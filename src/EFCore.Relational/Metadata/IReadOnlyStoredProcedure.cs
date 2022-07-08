// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     Represents a relational database function in a model.
/// </summary>
public interface IReadOnlyStoredProcedure : IReadOnlyAnnotatable
{
    /// <summary>
    ///     Gets the name of the function in the database.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Gets the schema of the function in the database.
    /// </summary>
    string? Schema { get; }

    /// <summary>
    ///     Gets the entity type in which this function is defined.
    /// </summary>
    IReadOnlyEntityType EntityType { get; }

    /// <summary>
    ///     Gets the parameters for this function.
    /// </summary>
    IReadOnlyList<IReadOnlyStoredProcedureParameter> Parameters { get; }

    /// <summary>
    ///     <para>
    ///         Creates a human-readable representation of the given metadata.
    ///     </para>
    ///     <para>
    ///         Warning: Do not rely on the format of the returned string.
    ///         It is designed for debugging only and may change arbitrarily between releases.
    ///     </para>
    /// </summary>
    /// <param name="options">Options for generating the string.</param>
    /// <param name="indent">The number of indent spaces to use before each new line.</param>
    /// <returns>A human-readable representation.</returns>
    string ToDebugString(MetadataDebugStringOptions options = MetadataDebugStringOptions.ShortDefault, int indent = 0)
    {
        var builder = new StringBuilder();
        var indentString = new string(' ', indent);

        builder
            .Append(indentString)
            .Append("StoredProcedure: ");

        if (Schema != null)
        {
            builder
                .Append(Schema)
                .Append('.');
        }

        builder.Append(Name);

        if ((options & MetadataDebugStringOptions.SingleLine) == 0)
        {
            var parameters = Parameters.ToList();
            if (parameters.Count != 0)
            {
                builder.AppendLine().Append(indentString).Append("  Parameters: ");
                foreach (var parameter in parameters)
                {
                    builder.AppendLine().Append(parameter.ToDebugString(options, indent + 4));
                }
            }

            if ((options & MetadataDebugStringOptions.IncludeAnnotations) != 0)
            {
                builder.Append(AnnotationsToDebugString(indent: indent + 2));
            }
        }

        return builder.ToString();
    }
}
