using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Runtime
{
    /// <summary>
    /// A funny struct union to allow type punning. Used to access any kind of value.
    /// </summary>
    [StructLayout( LayoutKind.Explicit )]
    public struct StackElement
    {
        /// <summary>
        /// Access the data as an Int32.
        /// </summary>
        [FieldOffset( 0 )]
        public int Int32;

        /// <summary>
        /// Access the data as a Float32.
        /// </summary>
        [FieldOffset( 0 )]
        public float Float32;

        /// <summary>
        /// Access the data as a pointer to the stack.
        /// </summary>
        [FieldOffset( 0 )]
        public int Ptr;


        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


        public static implicit operator StackElement( int a ) => new StackElement()
        {
            Int32 = a
        };

        public static implicit operator StackElement( float a ) => new StackElement()
        {
            Float32 = a
        };
    }
}