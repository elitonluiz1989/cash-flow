﻿namespace CashFlow.Tools
{
    public static class Helpers
    {
        #region Enum
        public static List<TEnum> EnumToList<TEnum>()
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("The source is not an enum");
            }

            List<TEnum> list = new();

            foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
            {
                list.Add(type);
            }

            return list;
        }

        public static List<TResult> EnumToList<TEnum, TResult>(Func<TEnum, TResult> func)
            where TEnum : struct
            where TResult : notnull
        {
            if (!typeof(TEnum).IsEnum) {
                throw new ArgumentException("The source is not an enum");
            }

            List<TResult> list = new();

            foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
            {
                list.Add(func(type));
            }

            return list;
        }
        #endregion
    }
}
