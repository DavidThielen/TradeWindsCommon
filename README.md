# TradeWindsCommon

These are some basic classes I use throughout my code, including in some of my other NuGet projects.

> This is under the MIT license. If you find this very useful I ask (not a requirement) that you consider reading my book [I DON’T KNOW WHAT I’M DOING!: How a Programmer Became a Successful Startup CEO](https://a.co/d/bEpDlJR).
> 
> And if you like it, please review it on Amazon and/or GoodReads. The number of legitimate reviews helps a lot. Much appreciated.

## Extensions

These are some classes I created to extend a number of basic classes.

The StringExtensions has some pretty specific methods that we use to extract metadata from search strings. Included here because this is used by many apps for their tags, etc. It can extract [tag], {tag}, (tag), and @tag from a string. The @tag does not allow spaces (the space is the end delimiter).

## Unit Tests

Not all of these have unit tests. But all have been tested thoroughly in use in our applications.

And some, like DoubleFinish and ConfigurationUtils can only be well tested in use in a Blazor application.