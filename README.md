# DialogueScript

## What is is?
DialogueScript is a domain-specific language designed for graph-based dialogue systems.

It is meant to be translated directly into C# or C++ for use with Unity or Unreal Engine based games.

This repository contains the DialogueScript language specification in the form of two .g4 files which are used by [ANTLR](https://www.antlr.org/) to generate a lexer and a parser.

## What it is not?
A general purpose programming language.

## Quick Start
* Install Java 1.6 or higher.
* Checkout this repo.
* Run `java_generate.sh`

This will generate a file called `test.sh` which will parse the provided `example_script.ds` DialogueScript file and start a GUI to display the parse tree.

Below is an example script to expose you to some of the core concepts of DialogueScript. For a complete overview, please see `example_script.ds`.

## Example Code
```
// A Dialogue Script is made up of a sequence of "scheduled blocks".

// Scheduled Block 0
// This is the simplest possible scheduled block. It's essentially a noop.
<<>>

// Scheduled Block 1
// This is a simple scheduled block that executes code immediately.
// Once execution completes, this block sets two flags named "Flag1" and 
// "Flag2" which are used to trigger any other scheduled blocks waiting on those
// flags.
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
    
    // Here's where the flags are set. They are set immediately after execution
    // finishes.
>Flag1,Flag2>

// Scheduled Block 2
// This block won't execute until "Flag1" and "Flag2" are set by the previous
// scheduled block. Those flags could just as easily have been set by two 
// different scheduled blocks.
<Flag1,Flag2<
    // This is an asynchronous function. Async functions should execute and 
    // return immediatly, but they also take in a callback argument for them
    // to call once the "real" work has been completed. Until the callback is
    // invoked, the scheduled block will not set "Flag3" and so any scheduled
    // blocks waiting for "Flag3" will also be waiting on this async function
    // to call it's callback.
    // 
    // NOTE: you can't see the callback argument here. It would be in the 
    // generated code
    string stringReturnValue = SpecialFunction{1, "string arg"};
>Flag3>

// Scheduled Block 3
<Flag3<
    System::Println("SpecialFunction called its callback! We made it!");
>>
```