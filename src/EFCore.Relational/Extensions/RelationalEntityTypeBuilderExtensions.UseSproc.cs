// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Relational database specific extension methods for <see cref="EntityTypeBuilder" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and examples.
/// </remarks>
public static partial class RelationalEntityTypeBuilderExtensions
{
    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder UpdateUsingStoredProcedure(
        this EntityTypeBuilder entityTypeBuilder,
        Action<NamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sproc = entityTypeBuilder.Metadata.SetUpdateStoredProcedure();
        buildAction(new NamingStoredProcedureBuilder(sproc, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionStoredProcedureBuilder? UpdateUsingStoredProcedure(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool fromDataAnnotation = false)
    {
        var sproc = entityTypeBuilder.Metadata.GetUpdateStoredProcedure();
        if (sproc == null)
        {
            sproc = entityTypeBuilder.Metadata.SetUpdateStoredProcedure(fromDataAnnotation);
        }
        else
        {
            sproc.UpdateConfigurationSource(fromDataAnnotation
                ? ConfigurationSource.DataAnnotation
                : ConfigurationSource.Convention);
        }

        return sproc?.Builder;
    }
    
    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder DeleteUsingStoredProcedure(
        this EntityTypeBuilder entityTypeBuilder,
        Action<NamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sproc = entityTypeBuilder.Metadata.SetDeleteStoredProcedure();
        buildAction(new NamingStoredProcedureBuilder(sproc, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionStoredProcedureBuilder? DeleteUsingStoredProcedure(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool fromDataAnnotation = false)
    {
        var sproc = entityTypeBuilder.Metadata.GetDeleteStoredProcedure();
        if (sproc == null)
        {
            sproc = entityTypeBuilder.Metadata.SetDeleteStoredProcedure(fromDataAnnotation);
        }
        else
        {
            sproc.UpdateConfigurationSource(fromDataAnnotation
                ? ConfigurationSource.DataAnnotation
                : ConfigurationSource.Convention);
        }

        return sproc?.Builder;
    }
    /// <summary>
    ///     Configures the stored procedure that the entity type would use for inserts when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder InsertUsingStoredProcedure(
        this EntityTypeBuilder entityTypeBuilder,
        Action<NamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sproc = entityTypeBuilder.Metadata.SetInsertStoredProcedure();
        buildAction(new NamingStoredProcedureBuilder(sproc, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionStoredProcedureBuilder? UpdateUsingStoredProcedure(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool fromDataAnnotation = false)
    {
        var sproc = entityTypeBuilder.Metadata.GetUpdateStoredProcedure();
        if (sproc == null)
        {
            sproc = entityTypeBuilder.Metadata.SetUpdateStoredProcedure(fromDataAnnotation);
        }
        else
        {
            sproc.UpdateConfigurationSource(fromDataAnnotation
                ? ConfigurationSource.DataAnnotation
                : ConfigurationSource.Convention);
        }

        return sproc?.Builder;
    }
}
