 
<!--Read Me file --> 

# WooliesX Technical Challenge

## How to build 
Open the solution in VS 2019. Make "WooliesX.TechChallenge" as the "startup project" and run. This should build the solution and display the URLs as below.

![](https://github.com/Matanavar/TechChallenge/blob/master/AzureFunctionURLs_Local.PNG)

The Azure Functions can be run locally with these URLs.

----

## Tech Challenge 
These Functions are hosted in Azure cloud and can be access with the below urls. 
                
### Excercise 1
 `<link>` : https://wooliesxtechchallenge20201210174053.azurewebsites.net/api/answers
### Excercise 2
`<link>` : https://wooliesxtechchallenge20201210174053.azurewebsites.net/api/sort?

(PS: Recommended sort seems to have failed the tests, need more clarification on the expected output.) 

Excercise 3
`<link>` : https://wooliesxtechchallenge20201210174053.azurewebsites.net/api/TrolleyTotal?

----

## Solution Details
+ Solution is developed using .NET Core and .NET Standard projects.
+ Template used: Http Triggered Azure functions in Visual Studio 2019.
+ Unit test: xUnit and Moq as mocking framework.
  
  