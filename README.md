# DialogueScript

## What it is?
DialogueScript is a domain-specific language designed for use in graph-based dialogue systems (cutscenes, conditions, state change, etc.).

It is meant to be translated directly into C# or C++ for use with Unity or Unreal Engine based games.

This repository contains the DialogueScript language specification in the form of two .g4 files which are used by [ANTLR](https://www.antlr.org/) to generate a lexer and a parser.

DialogueScript is not meant to be a general purpose programming language. There are no loops, no type definitions, no function defintions, and there is no direct memory allocation. These scripts are purely for interacting with code already defined in your game.

## Quick Start
* Install Java 1.6 or higher.
* Checkout this repo.
* Run `java_generate.sh`

This will generate a lexer and a parser both written in Java as well as file called `test.sh`. `test.sh` is a shell script that will parse the provided `example_script.ds` DialogueScript file and start a GUI to display the resulting parse tree.

Below is an annotated example script to expose you to some of the core concepts of DialogueScript. For a more complete overview, please see `example_script.ds`.

## Example Code
```
// A DialogueScript script is made up of a sequence of "scheduled blocks". 
// Scheduled blocks are just like the blocks that exist in C-family languages.
// Example: 
// { /*code goes here*/ }
//
// The only difference is that each scheduled block and wait for zero or more
// "flags" to be set before executing. They can also set flags immediatly after
// they finish their own execution. 
// Here are some examples:

// Scheduled Block 0
// This is the simplest possible scheduled block. It's essentially a noop that 
// executes immedately.
<<>>

// Scheduled Block 1
// This is anoter scheduled block that executes code immediately.
// Once execution completes, this block sets two flags named "Flag1" and 
// "Flag2" which are then used to trigger any other scheduled blocks waiting on 
// those two particular flags.
<<
    // Everything here executes immediately.
    int x = 10;
    if (x == 10)
    {
        // ".." is the string concat operator.
        string stringVariable = "string" .. "concat";

        // This is a function invocation. You can't actually define a function
        // inside DialogueScript. It is expected that the actual function
        // definition will be written in the codebase used for your game.
        // DialogueScript generated code will assume the presense of 
        // the "CallFunction" function, and if it's not there, the compiler will
        // complain bitterly.
        CallFunction(stringVariable);
    } 
    else 
    {
        // Use "::" to navigate namespaces.
        namespace::SomeObject.CallAnotherFunction(10, "string literal");
    }
    
    // Here is where this scheduled block sets its flags. They are set 
    // immediately after execution completes.
>Flag1,Flag2>

// Scheduled Block 2
// This block won't execute until "Flag1" and "Flag2" are set by other scheduled 
// blocks. In this case, only one scheduled block is setting both of those 
// flags, however those flags could just as easily have been set by two 
// different scheduled blocks.
<Flag1,Flag2<
    // This is an asynchronous function. Async functions should execute and 
    // return immediatly, but they also take in a callback argument that should 
    // them be invoked once the asynchronous work has been completed. Until the 
    // callback is invoked, the scheduled block will not set "Flag3" and so any 
    // other scheduled blocks waiting for "Flag3" will also be waiting on this 
    // async function to call it's callback.
    // 
    // NOTE: you can't see the callback argument here, but it would be in the 
    // generated code.
    string stringReturnValue = AnAsyncFunction{1, "string arg"};
    System::Println(stringReturnValue);
>Flag3>

// Scheduled Block 3
<Flag3<
    // This will only execute after Flag3 is set.
    System::Println("AnAsyncFunction called its callback! We made it!");
>>
```