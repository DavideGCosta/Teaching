(**
---
title: Portfolios
category: Practice Quizzes
categoryindex: 1
index: 6
---
*)

(**
[![Binder](../images/badge-binder.svg)](https://mybinder.org/v2/gh/nhirschey/teaching/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script](../images/badge-script.svg)]({{root}}/{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook](../images/badge-notebook.svg)]({{root}}/{{fsdocs-source-basename}}.ipynb)
*)

(*** hide,define-output:preDetails ***)
"""
<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

"""

(*** hide,define-output:postDetails ***)
"""

</details>
</span>
</p>
</div>
"""

(**
# Some good things to reference

[Anonymous Records](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/anonymous-records#syntax). You can read the above link for details, but the point of these is quite simple.

Records have been our main type for holding data for an observation. We've typically defined these ahead of time with a name before using them. This is good for important types that you will use frequently.

If you're using a particular record in only a few lines of code, then it can feel cumbersome to define the type beforehand. Anonymous records are a good solution in these circumstances. They are records that you can essentially use like regular records that we've been using, but you don't have to define the name of the record ahead of time.

I rarely use anonymous records, but you might find them useful for exploratory data manipulation. They're also kind of nice for these short problems because I don't need to define a record for each problem.
*)

(**
# Anonymous records
*)

(**
## Question 1

1. Create a *record* named `ExampleRec` that has an `X` field of type int and a `Y` field of type int. Create an example `ExampleRec` and assign it to a value named `r`.
2. Create an *anonymous record* that has an `X` field of type int and a `Y` field of type int. Create an example of the anonymous record and assign it to a value named `ar`.
*)

(*** include-it-raw:preDetails ***)

(**
A regular named record: 
*)

(*** define: ExampleRec, define-output: ExampleRec ***)
type ExampleRec = { X : int; Y : int }

let r = { X = 1; Y = 2}
(*** condition:html, include:ExampleRec ***)
(*** condition:html, include-fsi-output:ExampleRec ***)

(**
An anonymous record. The difference is:

1. We did not define the type ahead of time.
2. We put the pipe symbol `"|>"` inside the curly braces.
*)

(*** define: ExampleRec1, define-output: ExampleRec1 ***)
let ar = {| X = 1; Y = 2|}
(*** condition:html, include:ExampleRec1 ***)
(*** condition:html, include-fsi-output:ExampleRec1 ***)

(**
Note that they are not the same type, so if you
compare them they will be different even though
the X and Y fields have the same values.
For example, running `r = ar` 
will give a compiler error.
*)

(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
## Question 2
Imagine you have this array
*)

open System
type ArExample = { Date : DateTime; Value: float}
let arr = [|{ Date = DateTime(1990,1,1); Value = 1.25}
            { Date = DateTime(1990,1,2); Value = 2.25}
            { Date = DateTime(1991,1,1); Value = 3.25} |]

(**
1. Group the observations by a tuple of `(year,month)` and find the 
minimum value for each group. Report the result as a tuple of the group
and the minimum value [so it will be `((year, month), minValue)`].
2. Now, the same thing with anonymous records.
Group the observations by an Anonymous Record `{| Year = year; Month= month|}` and find the 
minimum value for each group. Report the result as an Anonymous record with a Group
field for the group and a value field for the minimum value [so it will be
`{| Group = {| Year = year; Month= month|}; Value = minValue |}`].
*)

(*** include-it-raw:preDetails ***)

(**
here I will explicitly put year and month in the final result
*)

(*** define: RecordsAndTransformations, define-output: RecordsAndTransformations ***)
arr 
|> Array.groupBy(fun x -> x.Date.Year, x.Date.Month)
|> Array.map(fun (group, xs) ->
    let year, month = group // Explicitly access year, month; same as let a,b = (1,2)
    let minValue = xs |> Array.map(fun x -> x.Value)|> Array.min
    (year, month), minValue) // explicitly put it in the result
(*** condition:html, include:RecordsAndTransformations ***)
(*** condition:html, include-fsi-output:RecordsAndTransformations ***)

(**
here I will explicitly put year and month in the final result,
but I will deconstruct them using pattern matching in the
function input
*)

(*** define: RecordsAndTransformations1, define-output: RecordsAndTransformations1 ***)
arr 
|> Array.groupBy(fun x -> x.Date.Year, x.Date.Month)
|> Array.map(fun ((year, month), xs) -> // Explicitly pattern match year, month in function input
    let minValue = xs |> Array.map(fun x -> x.Value)|> Array.min
    (year, month), minValue) // explicitly put it in the result
(*** condition:html, include:RecordsAndTransformations1 ***)
(*** condition:html, include-fsi-output:RecordsAndTransformations1 ***)

(**
or
since I'm just returning the grouping variable, there's really
no need to deconstruct it into `(year, month)` at any point.
*)

(*** define: RecordsAndTransformations2, define-output: RecordsAndTransformations2 ***)
arr 
|> Array.groupBy(fun x -> x.Date.Year, x.Date.Month)
|> Array.map(fun (group, xs) -> // match group to (year,month) together
    let minValue = xs |> Array.map(fun x -> x.Value)|> Array.min
    group, minValue)
(*** condition:html, include:RecordsAndTransformations2 ***)
(*** condition:html, include-fsi-output:RecordsAndTransformations2 ***)

(**
Now using anonymous records
This is where anonymous records can be useful.
For example, sometimes grouping by many things, 
using anonymous records like this make it more clear what the different
grouping variables are because they have names.
It's like a middle ground between tuples with no clear naming structure
and regular named records that are very explicit.
*)

(*** define: RecordsAndTransformations3, define-output: RecordsAndTransformations3 ***)
arr 
|> Array.groupBy(fun x -> {| Year = x.Date.Year; Month = x.Date.Month |})
|> Array.map(fun (group, xs) -> 
    let year, month = group.Year, group.Month // explicit deconstruct 
    let minValue = xs |> Array.map(fun x -> x.Value)|> Array.min
    {| Group = {| Year = year; Month = month|}; Value = minValue |})
(*** condition:html, include:RecordsAndTransformations3 ***)
(*** condition:html, include-fsi-output:RecordsAndTransformations3 ***)

(**
or, do the same thing by returning the whole group without deconstructing
*)

(*** define: RecordsAndTransformations4, define-output: RecordsAndTransformations4 ***)
arr 
|> Array.groupBy(fun x -> {| Year = x.Date.Year; Month = x.Date.Month |})
|> Array.map(fun (group, xs) -> 
    let minValue = xs |> Array.map(fun x -> x.Value)|> Array.min
    {| Group = group; Value = minValue |})
(*** condition:html, include:RecordsAndTransformations4 ***)
(*** condition:html, include-fsi-output:RecordsAndTransformations4 ***)

(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
# Portfolio Returns
*)

(**
## Question 1
Imagine that you have the following positions in your portfolio.
For each position you have a weight and a return.
What is the return of the entire portfolio?
*)

type PortReturnPos = { Id: string;  Weight: float; Return: float}
let stockPos = { Id = "stock"; Weight = 0.25; Return = 0.1 }
let bondPos = { Id = "bond"; Weight = 0.75; Return = 0.05}

(**
1. Group the observations by a tuple of `(year,month)` and find the 
minimum value for each group. Report the result as a tuple of the group
and the minimum value [so it will be `((year, month), minValue)`].
2. Now, the same thing with anonymous records.
Group the observations by an Anonymous Record `{| Year = year; Month= month|}` and find the 
minimum value for each group. Report the result as an Anonymous record with a Group
field for the group and a value field for the minimum value [so it will be
`{| Group = {| Year = year; Month= month|}; Value = minValue |}`].
*)

(*** include-it-raw:preDetails ***)

(**
Remember that portfolio returns are a weighted average
of the returns of the stocks in the portfolio. The weights
are the position weights.
*)

(*** define: PortfolioRet1, define-output: PortfolioRet1 ***)
let stockAndBondPort = 
    stockPos.Weight*stockPos.Return + bondPos.Weight*bondPos.Return
(*** condition:html, include:PortfolioRet1 ***)
(*** condition:html, include-fsi-output:PortfolioRet1 ***)

(**
or, doing the multiplication and summation with collections
*)

(*** define: PortfolioRet2, define-output: PortfolioRet2 ***)
let weightXreturn =
    [|stockPos;bondPos|]
    |> Array.map(fun pos -> pos.Weight*pos.Return)
(*** condition:html, include:PortfolioRet2 ***)
(*** condition:html, include-fsi-output:PortfolioRet2 ***)

(**
now sum
*)

(*** define: PortfolioRet3, define-output: PortfolioRet3 ***)
let stockAndBondPort2 = weightXreturn |> Array.sum
(*** condition:html, include:PortfolioRet3 ***)
(*** condition:html, include-fsi-output:PortfolioRet3 ***)

(**
check
*)

(*** define: PortfolioRet4, define-output: PortfolioRet4 ***)
stockAndBondPort = stockAndBondPort2 // evaluates to true
(*** condition:html, include:PortfolioRet4 ***)
(*** condition:html, include-fsi-output:PortfolioRet4 ***)

(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
## Question 2
Imagine that you have the following positions in your portfolio.
For each position you have a weight and a return.
What is the return of the entire portfolio?
*)

(*** define: Positions ***)
let positions =
    [|{ Id = "stock"; Weight = 0.25; Return = 0.12 }
      { Id = "bond"; Weight = 0.25; Return = 0.22 }
      { Id = "real-estate"; Weight = 0.5; Return = -0.15 } |]
(*** condition:html, include:Positions ***)

(*** include-it-raw:preDetails ***)
(*** define: EntirePortRet, define-output: EntirePortRet ***)

let threeAssetPortfolioReturn =
    positions
    |> Array.map(fun pos -> pos.Weight*pos.Return)
    |> Array.sum

(*** condition:html, include:EntirePortRet ***)
(*** condition:html, include-fsi-output:EntirePortRet ***)
(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
## Question 3
Imagine that you have the following positions in your portfolio.
For each position you have a weight and a return.
What is the return of the entire portfolio?
*)

(*** define: positionsWithShort ***)
let positionsWithShort =
    [|{ Id = "stock"; Weight = 0.25; Return = 0.12 }
      { Id = "bond"; Weight = -0.25; Return = 0.22 }
      { Id = "real-estate"; Weight = 1.0; Return = -0.15 } |]
(*** condition:html, include:positionsWithShort ***)

(*** include-it-raw:preDetails ***)
(*** define: EntirePortRetWithShort, define-output: EntirePortRetWithShort ***)

let positionsWithShortReturn =
    positionsWithShort
    |> Array.map(fun pos -> pos.Weight*pos.Return)
    |> Array.sum


(*** condition:html, include:EntirePortRetWithShort ***)
(*** condition:html, include-fsi-output:EntirePortRetWithShort ***)
(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
# Sharpe Ratios
*)

(**
## Question 1
Imagine that you have the following array of *annual* returns in
excess of the risk-free rate. What is the *annualized* Sharpe ratio?
*)

let rets = [| 0.1; -0.4; 0.2; 0.15; -0.03 |]

(**
Note that the units are such that 0.1 is 10%.
*)

(*** include-it-raw:preDetails ***)

(*** define: FStats, define-output: FStats ***)
#r "nuget: FSharp.Stats"
open FSharp.Stats
(*** condition:html, include:FStats ***)

(**
we get `stDev` from `FSharp.Stats`
there is only a `Seq.stDev`, not `Array.stDev`.
We can use `Seq.stDev` with array because you
can pipe any collection to a `Seq`.
*)

(*** define: AnnualizedSR, define-output: AnnualizedSR ***)
let retsAvg = rets |> Array.average
let retsStdDev = rets |> Seq.stDev 
let retsSharpeRatio = retsAvg/retsStdDev
(*** condition:html, include:AnnualizedSR ***)
(*** condition:html, include-fsi-output:AnnualizedSR ***)

(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
## Question 2
Imagine that you have the following array of *monthly* returns in
excess of the risk-free rate. What is the *annualized* Sharpe ratio?

```fsharp
let rets = [| 0.1; -0.4; 0.2; 0.15; -0.03 |]
//Note that the units are such that 0.1 is 10%.
```
*)

(*** include-it-raw:preDetails ***)

(**
remember that to annualize an arithmetic return,
we do `return *` (# compounding periods per year)
to annualize a standard deviation, 
we do `sd * sqrt`(# compounding periods per year)
*)

(*** define: MonthlyRetAnnualSR, define-output: MonthlyRetAnnualSR ***)
let monthlyRetsAnnualizedAvg = 12.0*(rets |> Array.average)
(*** condition:html, include:MonthlyRetAnnualSR ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR ***)

(**
or
*)

(*** define: MonthlyRetAnnualSR1, define-output: MonthlyRetAnnualSR1 ***)
let monthlyRetsAnnualizedAvg2 = 
    rets 
    |> Array.average 
    // now we're going to use a lambda expression.
    // this is the same idea as when we do Array.map(fun x -> ...)
    // except now we're only piping a float, not an array so
    // we're leaving off the "Array.map" 
    |> (fun avg -> 12.0 * avg) 
(*** condition:html, include:MonthlyRetAnnualSR1 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR1 ***)

(**
or, in two steps
*)

(*** define: MonthlyRetAnnualSR2, define-output: MonthlyRetAnnualSR2 ***)
let monthlyRetsAvg = rets |> Array.average
let monthlyRetsAnnualizedAvg3 = 12.0*monthlyRetsAvg
(*** condition:html, include:MonthlyRetAnnualSR2 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR2 ***)

(**
now the standard deviation
*)

(*** define: MonthlyRetAnnualSR3, define-output: MonthlyRetAnnualSR3 ***)
let monthlyRetsAnnualizedSd = 
    rets 
    |> Seq.stDev
    |> fun monthlySd -> sqrt(12.0) * monthlySd
(*** condition:html, include:MonthlyRetAnnualSR3 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR3 ***)

(**
or, in two steps
*)

(*** define: MonthlyRetAnnualSR4, define-output: MonthlyRetAnnualSR4 ***)
let monthlyRetsSd = rets |> Seq.stDev
let monthlyRetsAnnualizedSd2 = sqrt(12.0)*monthlyRetsSd
(*** condition:html, include:MonthlyRetAnnualSR4 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR4 ***)

(**
SharpeRatio
*)

(*** define: MonthlyRetAnnualSR5, define-output: MonthlyRetAnnualSR5 ***)
let annualizedSharpeFromMonthly =
    monthlyRetsAnnualizedAvg / monthlyRetsAnnualizedSd
(*** condition:html, include:MonthlyRetAnnualSR5 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR5 ***)

(**
or, since: 
$$\frac{12.0}{sqrt(12.0) } = sqrt(12.0)$$
then:
$$\frac{monthlyRetsAvg \times 12.0}{monthlyRetsSd \times sqrt(12.0)} = sqrt(12.0)\times\frac{monthlyRetsAvg}{monthlyRetsSd}$$
*)

(*** define: MonthlyRetAnnualSR6, define-output: MonthlyRetAnnualSR6 ***)
let annualizedSharpeFromMonthly2 =
    sqrt(12.0) * (monthlyRetsAvg/monthlyRetsSd)
(*** condition:html, include:MonthlyRetAnnualSR6 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR6 ***)

(**
Check  
we have to round because floating point math gives us slightly different #'s
recall from the fundamentals lecture how floating point math is inexact.
*)

(*** define: MonthlyRetAnnualSR7, define-output: MonthlyRetAnnualSR7 ***)
Math.Round(annualizedSharpeFromMonthly,6) = Math.Round(annualizedSharpeFromMonthly2,6) // true
(*** condition:html, include:MonthlyRetAnnualSR7 ***)
(*** condition:html, include-fsi-output:MonthlyRetAnnualSR7 ***)

(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.


(**
## Question 3
Imagine that you have the following array of *daily* returns in
excess of the risk-free rate. What is the *annualized* Sharpe ratio?

```fsharp
let rets = [| 0.1; -0.4; 0.2; 0.15; -0.03 |]
//Note that the units are such that 0.1 is 10%.
```
*)

(*** include-it-raw:preDetails ***)

(**
Convention for daily is 252 trading days per year.
so annualize daily by multiplying by `sqrt(252.0)`.
*)

(*** define: dailyRetAnnualSR, define-output: dailyRetAnnualSR ***)
let annualizedSharpeFromDaily =
    let avgRet = rets |> Array.average
    let stdevRet = rets |> Seq.stDev
    sqrt(252.0) * (avgRet/stdevRet)
(*** condition:html, include:dailyRetAnnualSR ***)
(*** condition:html, include-fsi-output:dailyRetAnnualSR ***)

(**
or in multiple steps
*)

(*** define: dailyRetAnnualSR1, define-output: dailyRetAnnualSR1 ***)
let dailyAvgRet = rets |> Array.average
let dailyStDevRet = rets |> Seq.stDev
let annualizedSharpeFromDaily2 =
    sqrt(252.0) * (dailyAvgRet/dailyStDevRet)
(*** condition:html, include:dailyRetAnnualSR1 ***)
(*** condition:html, include-fsi-output:dailyRetAnnualSR1 ***)

(*** include-it-raw:postDetails ***)

(*** condition:ipynb ***)
// write your code here, see website for solution.




