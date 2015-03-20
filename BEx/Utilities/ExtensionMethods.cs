using System.Globalization;

namespace BEx
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this decimal source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this double source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this int source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this long source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
    }
}