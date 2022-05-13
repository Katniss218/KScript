using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language
{
   public static class Utils
    {
        public static T[] SubArray<T>( this IList<T> arr, int start, int length )
        {
            T[] newArray = new T[length];

            for( int i = 0; i < newArray.Length; i++ )
            {
                newArray[i] = arr[start + i];
            }

            return newArray;
        }
    }
}
