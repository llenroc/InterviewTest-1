using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace InterviewTest.UnitTests.Extensions
{
    public static class Extension
    {
        public static bool IsOrderedBy<T, TProperty>(this IList<T> list, Expression<Func<T, TProperty>> propertyExpression) where TProperty : IComparable<TProperty>
        {
            var member = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)member.Member;
            IComparable<TProperty> previousValue = null;
            for (int i = 0; i < list.Count(); i++)
            {
                var currentValue = (TProperty)propertyInfo.GetValue(list[i], null);
                if (previousValue == null)
                {
                    previousValue = currentValue;
                    continue;
                }

                if (previousValue.CompareTo(currentValue) > 0) return false;
                previousValue = currentValue;

            }

            return true;
        }
    }
}
