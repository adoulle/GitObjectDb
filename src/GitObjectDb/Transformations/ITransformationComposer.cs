using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using GitObjectDb.Models;

namespace GitObjectDb.Transformations
{
    /// <summary>
    /// Composes multiple <see cref="PropertyTransformation"/> to perform multiple changes at once.
    /// </summary>
    public interface ITransformationComposer : IEnumerable<ITransformation>
    {
#pragma warning disable CA1716 // Identifiers should not match keywords
        /// <summary>
        /// Adds a new <see cref="PropertyTransformation"/>.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property or field.</typeparam>
        /// <param name="node">The node.</param>
        /// <param name="propertyPicker">An expression that identifies the property or field that will have <paramref name="value" /> assigned.</param>
        /// <param name="value">The value to assign to the property or field identified by <paramref name="propertyPicker" />.</param>
        /// <returns>An <see cref="ITransformationComposer"/> which can be used to further customize the transformations.</returns>
        ITransformationComposer Update<TModel, TProperty>(TModel node, Expression<Func<TModel, TProperty>> propertyPicker, TProperty value)
#pragma warning restore CA1716 // Identifiers should not match keywords
            where TModel : IModelObject;

        /// <summary>
        /// Adds a new child removal.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TChildProperty">The type of the child property or field.</typeparam>
        /// <param name="node">The node.</param>
        /// <param name="propertyPicker">An expression that identifies the property or field that will have the <paramref name="child" /> added.</param>
        /// <param name="child">The child to add to the property or field identified by <paramref name="propertyPicker" />.</param>
        /// <returns>An <see cref="ITransformationComposer"/> which can be used to further customize the transformations.</returns>
        ITransformationComposer Add<TModel, TChildProperty>(TModel node, Expression<Func<TModel, TChildProperty>> propertyPicker, IModelObject child)
            where TModel : IModelObject
            where TChildProperty : ILazyChildren;

        /// <summary>
        /// Removes a new child removal.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TChildProperty">The type of the child property or field.</typeparam>
        /// <param name="node">The node.</param>
        /// <param name="propertyPicker">An expression that identifies the property or field that will have the <paramref name="child" /> removed.</param>
        /// <param name="child">The child to remove to the property or field identified by <paramref name="propertyPicker" />.</param>
        /// <returns>An <see cref="ITransformationComposer"/> which can be used to further customize the transformations.</returns>
        ITransformationComposer Remove<TModel, TChildProperty>(TModel node, Expression<Func<TModel, TChildProperty>> propertyPicker, IModelObject child)
            where TModel : IModelObject
            where TChildProperty : ILazyChildren;
    }
}