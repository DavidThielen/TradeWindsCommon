# TradeWindsCommon

These are some basic classes I use throughout my code, including in some of my other NuGet projects.

> This is under the MIT license. If you find this very useful I ask (not a requirement) that you consider reading my book [I DON’T KNOW WHAT I’M DOING!: How a Programmer Became a Successful Startup CEO](https://a.co/d/bEpDlJR).
> 
> And if you like it, please review it on Amazon and/or GoodReads. The number of legitimate reviews helps a lot. Much appreciated.

## Extensions

These are some classes I created to extend a number of basic classes.

The StringExtensions has some pretty specific methods that we use to extract metadata from search strings. Included here because this is used by many apps for their tags, etc. It can extract [tag], {tag}, (tag), and @tag from a string. The @tag does not allow spaces (the space is the end delimiter).

## Debug

Trap() is a construct I discuss in my book No Bugs! Basically you place this when you write new code, at the entry to each function, inside every if, else, where, case, etc. 

When you first run that new code it will drop you in to the debugger at each trap. Mark that Trap() to be deleted and then single step through the new code. 90% of the time the code is fine. 10% of the time the code will do something unexpected and you can then fix the issue.

The way I mark the traps I hit is to put a * at the beginning of that line
```csharp
*     Trap();
```
That way the line numbers don't change so the debugger is in sync with the source code. But when your debug session is complete, a compile will fail until you remove those traps you've now walked through.

## Unit Tests

Not all of these have unit tests. But all have been tested thoroughly in use in our applications.

And some, like DoubleFinish and ConfigurationUtils can only be well tested in use in a Blazor application.