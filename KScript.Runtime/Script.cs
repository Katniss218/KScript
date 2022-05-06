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
        // Dynamic is slow - 2-10x slower (depends) than just a plain int array. In production, even a boxed int seems to be faster.
        // Possible solutions:
        // - Use a struct layout with multiple types of fields in the same place.
        // - Some 'unsafe' magic with pointers.
        public dynamic[] Stack { get; private set; }

        /// <summary>
        /// Points to the top of the stack.
        /// </summary>
        /// <remarks>
        /// Points to the last unused index.
        /// </remarks>
        public int StackPointer { get; private set; }
        /// <summary>
        /// Points to the base ('caller_frame_base_pointer') of the current stack frame.
        /// </summary>
        public int CurrentStackFramePointer { get; private set; }

        /// <summary>
        /// A list used to store the return values of the most recently returned function.
        /// </summary>
        public Stack<dynamic> ReturnStack { get; set; }

        // User-defined structs could be just sequences of variables, like they are in real programs.

        /// <summary>
        /// Instructions
        /// </summary>
        public (OpCode opCode, dynamic[] data)[] Op { get; private set; } // functions could be duplicates of these, that have a set place in memory to return something.
        /// <summary>
        /// Instruction pointer
        /// </summary>
        public int Current { get; private set; }

        public long OperationCounter { get; private set; }
        public long? MaxOperations { get; private set; }

        public Script( int stackSize, (OpCode opCode, dynamic[] data)[] op )
        {
            this.Stack = new dynamic[stackSize];
            this.StackPointer = 0;
            this.CurrentStackFramePointer = 0;

            this.Op = op;

            this.ReturnStack = new Stack<dynamic>();
        }

        /// <summary>
        /// Pushes 'a' onto the stack, increments the stack pointer.
        /// </summary>
        public void Push( dynamic a )
        {
            this.Stack[StackPointer++] = a;
        }

        /// <summary>
        /// Pops the top-most variable from the stack, decrements the stack pointer.
        /// </summary>
        public void Pop()
        {
            // Do not reset the value.
            this.StackPointer--;
        }

        /// <summary>
        /// The object that this script is attached to. I.e. a unit, a building, or whatever else.
        /// </summary>
        /// <remarks>
        /// Will be used for external calls and bindings against fields, and more.
        /// </remarks>
        //public object Owner { get; set; }

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
                    // Addition

                    case OpCode.ADD:

                        Stack[CurrentStackFramePointer + data[0]] += Stack[CurrentStackFramePointer + data[1]];
                        break;
                    case OpCode.ADD_CONST:

                        Stack[CurrentStackFramePointer + data[0]] += data[1];
                        break;

                    // Subtraction

                    case OpCode.SUBTRACT:

                        Stack[CurrentStackFramePointer + data[0]] -= Stack[CurrentStackFramePointer + data[1]];
                        break;
                    case OpCode.SUBTRACT_CONST:

                        Stack[CurrentStackFramePointer + data[0]] -= data[1];
                        break;

                    // Multiplication

                    case OpCode.MULTIPLY:

                        Stack[CurrentStackFramePointer + data[0]] *= Stack[CurrentStackFramePointer + data[1]];
                        break;
                    case OpCode.MULTIPLY_CONST:

                        Stack[CurrentStackFramePointer + data[0]] *= data[1];
                        break;

                    // Division

                    case OpCode.DIVIDE:

                        Stack[CurrentStackFramePointer + data[0]] /= Stack[CurrentStackFramePointer + data[1]];
                        break;
                    case OpCode.DIVIDE_CONST:

                        Stack[CurrentStackFramePointer + data[0]] /= data[1];
                        break;

                    // Modular division

                    case OpCode.MODULO:

                        Stack[CurrentStackFramePointer + data[0]] %= Stack[CurrentStackFramePointer + data[1]];
                        break;
                    case OpCode.MODULO_CONST:

                        Stack[CurrentStackFramePointer + data[0]] %= data[1];
                        break;

                    // Load

                    case OpCode.SET:

                        Stack[CurrentStackFramePointer + data[0]] = Stack[CurrentStackFramePointer + data[1]];
                        break;
                    case OpCode.SET_CONST:

                        Stack[CurrentStackFramePointer + data[0]] = data[1];
                        break;
                        
                    case OpCode.PUSH:

                        Stack[StackPointer++] = Stack[CurrentStackFramePointer + data[0]];
                        break;
                    case OpCode.PUSH_CONST:

                        Stack[StackPointer++] = data[0];
                        break;

                    // Control flow statements

                    case OpCode.GOTO_IF_ZERO:
                        if( Stack[CurrentStackFramePointer + data[0]] == 0 )
                        {
                            Current = CurrentStackFramePointer + data[1];
                            continue; // don't auto-increment the current
                        }

                        break;
                    case OpCode.GOTO:

                        Current = CurrentStackFramePointer + data[0];
                        continue; // don't auto-increment the current

                    case OpCode.PUSH_STACK_FRAME:

                        this.Stack[this.StackPointer] = this.CurrentStackFramePointer;
                        this.CurrentStackFramePointer = this.StackPointer++;
                        break;
                        
                    case OpCode.POP_STACK_FRAME:

                        // Doing it 'in reverse' allows to skip a temporary variable.
                        this.StackPointer = this.CurrentStackFramePointer; // pop the frame
                        this.CurrentStackFramePointer = this.Stack[this.CurrentStackFramePointer]; // restore the previous frame's frame pointer.
                        break;
                        
                    case OpCode.PUSH_RET:

                        ReturnStack.Push( Stack[CurrentStackFramePointer + data[0]] );
                        break;
                        
                    case OpCode.POP_RET:

                        Stack[CurrentStackFramePointer + data[0]] = ReturnStack.Pop();
                        break;

                    case OpCode.EXIT:

                        return;

                }

                Current++;
            }
        }

        /*
        
        stack_pointer
        stack
        {
            stack_frames
            [
                {
                    caller_frame_base_pointer
                    locals, parameters, etc* - dynamic size
                }
            ]
        }
        return_stack* - dynamic size

        */
    }
}