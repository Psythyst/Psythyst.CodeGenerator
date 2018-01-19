using System;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// StringExtension Class.
    /// </summary>
    public static class StringExtension
    {
        public static string ToUppercaseFirst(this string Value) 
        {
            if (String.IsNullOrEmpty(Value) || String.IsNullOrWhiteSpace(Value)) 
                return String.Empty;

            return Char.ToUpper(Value[0]) + Value.Substring(1);
        }

        public static string ToLowercaseFirst(this string Value) 
        {
            if (String.IsNullOrEmpty(Value) || String.IsNullOrWhiteSpace(Value)) 
                return String.Empty;

            return Char.ToLower(Value[0]) + Value.Substring(1);
        }
    }
}