// MIT License - See __LICENSE_AND_INFO file for Licensing details and requirements

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public static class VhHelper
{
    /// <summary>
    /// Gets value that is used for display in the UI from the <see cref="DisplayAttribute"/>.
    /// If no <see cref="DisplayAttribute"/> then returns the object name applying <see cref="MiscellaneousHelper.SplitCamelCase(string)"/>
    /// </summary>
    /// <param name="obj">The object of the elements of source.</param>
    /// <returns>A value that is used for display in the UI</returns>
    public static string AppDisplayName(object obj)
    {
        var attr = GetCustomAttribute<DisplayAttribute>(obj);
        if (attr == null)
        {
            return SplitCamelCase(obj.GetType().Name);
        }

        return string.IsNullOrWhiteSpace(attr.Name)
             ? SplitCamelCase(obj.GetType().Name)
             : attr.Name;
    }

    /// <summary>
    /// Gets value that is used for display in the UI from the <see cref="DisplayAttribute"/>.
    /// If no <see cref="DisplayAttribute"/> then returns the object name applying <see cref="MiscellaneousHelper.SplitCamelCase(string)"/>
    /// </summary>
    /// <param name="obj">The object of the elements of source.</param>
    /// <returns>A value that is used for display in the UI</returns>
    public static string AppDisplayName(Type type)
    {
        var attr = GetCustomAttribute<DisplayAttribute>(type);
        if (attr == null)
        {
            return SplitCamelCase(type.Name);
        }

        return string.IsNullOrWhiteSpace(attr.Name)
             ? SplitCamelCase(type.Name)
             : attr.Name;
    }

    /// <summary>
    /// Gets value that is used for display in the UI from the <see cref="DisplayAttribute"/>.
    /// If no <see cref="DisplayAttribute"/> then returns the property name applying <see cref="MiscellaneousHelper.SplitCamelCase(string)"/>
    /// </summary>
    /// <param name="obj">The object of the elements of source.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>A value that is used for display in the UI</returns>
    public static string AppDisplayName(object obj, string propertyName)
    {
        var attr = GetCustomAttribute<DisplayAttribute>(obj, propertyName);
        if (attr == null)
        {
            return SplitCamelCase(propertyName);
        }

        return string.IsNullOrWhiteSpace(attr.Name)
             ? SplitCamelCase(propertyName)
             : attr.Name;
    }

    /// <summary>
    /// Gets value that is used for display in the UI from the <see cref="DisplayAttribute"/>.
    /// If no <see cref="DisplayAttribute"/> then returns the property name applying <see cref="MiscellaneousHelper.SplitCamelCase(string)"/>
    /// </summary>
    /// <param name="type">The type of the elements of source.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>A value that is used for display in the UI</returns>
    public static string AppDisplayName(Type type, string propertyName)
    {
        var attr = GetCustomAttribute<DisplayAttribute>(type, propertyName);
        if (attr == null)
        {
            return SplitCamelCase(propertyName);
        }

        return string.IsNullOrWhiteSpace(attr.Name)
             ? SplitCamelCase(propertyName)
             : attr.Name;
    }

    /// <summary>
    /// Get the 1st Custom Attribute of type TAttribute
    /// </summary>
    /// <typeparam name="TAttribute">Type of Attribute to Get</typeparam>
    /// <param name="obj">The object.</param>
    /// <returns>1st Attribute of type TAttribute -or- null if not found</returns>
    public static TAttribute GetCustomAttribute<TAttribute>(object obj) where TAttribute : Attribute
    {
        var custAttributes = obj.GetType().GetCustomAttributes(typeof(TAttribute), false);

        return (TAttribute)custAttributes.FirstOrDefault();
    }

    /// <summary>
    /// Get the 1st Custom Attribute of type TAttribute for the property
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="obj"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    private static TAttribute GetCustomAttribute<TAttribute>(object obj, string propertyName) where TAttribute : Attribute
    {
        var propInfo = obj.GetType().GetProperty(propertyName);
        if (propInfo == null)
        {
            return null;
        }

        var custAttributes = propInfo.GetCustomAttributes(typeof(TAttribute), false);

        return (TAttribute)custAttributes.FirstOrDefault();
    }

    /// <summary>
    /// Get the 1st Custom Attribute of Type
    /// </summary>
    /// <typeparam name="TAttribute">Type of Attribute to Get</typeparam>
    /// <param name="obj">The object of the elements of source.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>1st Attribute of type TAttribute -or- null if not found</returns>
    public static TAttribute GetCustomAttribute<TAttribute>(Type type, string propertyName) where TAttribute : Attribute
    {
        var propInfo = type.GetProperty(propertyName);
        if (propInfo == null)
        {
            return null;
        }

        var custAttributes = propInfo.GetCustomAttributes(typeof(TAttribute), false);

        return (TAttribute)custAttributes.FirstOrDefault();
    }

    /// <summary>
    /// Split String on Camel Case
    /// </summary>
    /// <param name="input">The string to split.</param>
    /// <returns>Split String on Camel Case</returns>
    private static string SplitCamelCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        return System.Text.RegularExpressions.Regex.Replace(
            input,
            "([A-Z])",
            " $1",
            System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }
}