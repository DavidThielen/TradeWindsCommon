
// Copyright (c) 2024 Trade Winds Studios (David Thielen)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.ComponentModel;
using System.Reflection;

namespace TradeWindsCommon.Extensions
{
	/// <summary>
	/// Extensions to access private class members.
	/// </summary>
	public static class AccessPrivateMembers
	{
		/// <summary>
		/// Returns a _private_ Property Value from a given Object. Uses Reflection.
		/// Throws a ArgumentOutOfRangeException if the Property is not found.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is returned</param>
		/// <param name="propName">Property name as string.</param>
		/// <returns>PropertyValue</returns>
		public static T? GetPrivatePropertyValue<T>(this object obj, string propName)
		{
			if (obj == null) 
                throw new ArgumentNullException("obj");
			PropertyInfo? pi = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (pi == null) 
                throw new ArgumentOutOfRangeException(propName, 
                $"Property {propName} was not found in Type {obj.GetType().FullName}");
			return (T?)pi.GetValue(obj, null);
		}

		/// <summary>
		/// Returns a private Property Value from a given Object. Uses Reflection.
		/// Throws a ArgumentOutOfRangeException if the Property is not found.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is returned</param>
		/// <param name="propName">Property name as string.</param>
		/// <returns>PropertyValue</returns>
		public static T? GetPrivateFieldValue<T>(this object obj, string propName)
		{
			if (obj == null) 
                throw new ArgumentNullException("obj");
			Type? t = obj.GetType();
			FieldInfo? fi = null;
			while (fi == null && t != null)
			{
				fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				t = t.BaseType;
			}
			if (fi == null) 
                throw new ArgumentOutOfRangeException(propName,
                $"Field {propName} was not found in Type {obj.GetType().FullName}");
			return (T?)fi.GetValue(obj);
		}

		/// <summary>
		/// Returns a field value from a given Object. Uses Reflection. Returns null if not found.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is returned</param>
		/// <param name="propName">Property name as string.</param>
		/// <returns>PropertyValue or null</returns>
		public static T? GetFieldValueOrDefault<T>(this object obj, string propName) where T: class
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			Type? t = obj.GetType();
			FieldInfo? fi = null;
			while (fi == null && t != null)
			{
				fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				t = t.BaseType;
			}

			if (fi == null)
				return null;
			return (T?)fi.GetValue(obj);
		}

		/// <summary>
		/// Sets a _private_ Property Value from a given Object. Uses Reflection.
		/// Throws a ArgumentOutOfRangeException if the Property is not found.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is set</param>
		/// <param name="propName">Property name as string.</param>
		/// <param name="val">Value to set. null is a valid value.</param>
		/// <returns>PropertyValue</returns>
		public static void SetPrivatePropertyValue<T>(this object obj, string propName, T? val)
		{
			Type? t = obj.GetType();
			PropertyInfo? propertyInfo = null;

			while (propertyInfo == null && t != null)
			{
				propertyInfo = t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic 
						| BindingFlags.Instance | BindingFlags.SetProperty);
				t = t.BaseType;
			}

			if (propertyInfo == null)
				throw new ArgumentOutOfRangeException(propName,
					$"Property {propName} was not found in Type {obj.GetType().FullName} or its ancestor types.");

			propertyInfo.SetValue(obj, val);
		}

		/// <summary>
		/// Set a private Property Value on a given Object. Uses Reflection.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is returned</param>
		/// <param name="propName">Property name as string.</param>
		/// <param name="val">the value to set</param>
		/// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
		public static void SetPrivateFieldValue<T>(this object obj, string propName, T val)
		{
			if (obj == null) 
                throw new ArgumentNullException("obj");
			Type? t = obj.GetType();
			FieldInfo? fi = null;
			while (fi == null && t != null)
			{
				fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				t = t.BaseType;
			}
			if (fi == null) 
                throw new ArgumentOutOfRangeException(propName,
                    $"Field {propName} was not found in Type {obj.GetType().FullName}");
			fi.SetValue(obj, val);
		}

		public static string GetDescription(this Enum genericEnum)
		{
			Type genericEnumType = genericEnum.GetType();
			MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
			if ((memberInfo.Length > 0))
			{
				var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attributes.Any())
					return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
			}
			return genericEnum.ToString();
		}
	}
}
