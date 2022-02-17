using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Runtime
{
    public class Script
    {
        /// <summary>
        /// Represents the memory assigned to this script. This part is used to store variables and objects.
        /// </summary>
        public dynamic[] Variables { get; set; }

        // objects could be just sequences of variables, like they are in real programs.

        public (OpCode opCode, dynamic[] data)[] Op { get; set; } // functions could be duplicates of these, that have a set place in memory to return something.
        public int Current { get; private set; }

        public long OperationCounter { get; set; }
        public long? MaxOperations { get; set; }

        /// <summary>
        /// The object that this script is attached to. I.e. a unit, a building, or whatever else.
        /// </summary>
        public object Owner { get; set; } // this will be used later to bind against fields and stuff.

        /// <summary>
        /// Runs a script
        /// </summary>
        public void Run()
        {
            while( Current < Op.Length )
            {
                var (opCode, data) = Op[Current];

                OperationCounter++;

                if( MaxOperations != null && OperationCounter > MaxOperations.Value )
                {
                    throw new Exception( "Exceeded the maximum operation count." );
                }

                switch( opCode )
                {
                    case OpCode.ADD:

                        Variables[data[0]] += Variables[data[1]];

                        break;
                    case OpCode.SUBTRACT:

                        Variables[data[0]] -= Variables[data[1]];

                        break;
                    case OpCode.GOTO_IF_ZERO:
                        if( Variables[data[0]] == 0 )
                        {
                            Current = data[1];
                            continue; // don't auto-increment the current
                        }

                        break;
                    case OpCode.GOTO:

                        Current = data[0];
                        continue; // don't auto-increment the current

                    case OpCode.EXIT:

                        return;
                }

                Current++;
            }
        }
    }
}