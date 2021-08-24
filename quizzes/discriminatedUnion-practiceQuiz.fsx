(**
// can't yet format YamlFrontmatter (["title: Dicriminated Union"; "category: Practice Quizzes"; "categoryindex: 1"; "index: 5"], Some { StartLine = 2 StartColumn = 0 EndLine = 6 EndColumn = 8 }) to pynb markdown

[![Binder](../images/badge-binder.svg)](https://mybinder.org/v2/gh/nhirschey/teaching/gh-pages?filepath=quizzes/discriminatedUnion-practiceQuiz.ipynb)&emsp;
[![Script](../images/badge-script.svg)](/Teaching//quizzes/discriminatedUnion-practiceQuiz.fsx)&emsp;
[![Notebook](../images/badge-notebook.svg)](/Teaching//quizzes/discriminatedUnion-practiceQuiz.ipynb)

This practice quiz emphasizes `Discriminated Unions`. They are useful for times when the data that you're representing has multiple mutually exclusive cases. 

Here is some good background reading for before you do these quesitions, particularly the F# for fun and profit link.

- Discriminated Union types

    - The F# language reference for [discriminated unions](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions)
    - If you want more a more in depth discussion, see F# for fun and profit's section on [discriminated unions](https://fsharpforfunandprofit.com/posts/discriminated-unions/)
    - The tour of F# section on [discriminated unions](https://docs.microsoft.com/en-us/dotnet/fsharp/tour#record-and-discriminated-union-types)


## Question 1
Create a discriminated union named `Action` with two cases: Buy and Sell.

1. Create a value named 'bAction' and assign `Buy` to it.
2. Create a value named 'sAction' and assign `Sell` to it.

<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type Action =
  | Buy
  | Sell
val bAction : Action = Buy
val sAction : Action = Sell
```

</details>
</span>
</p>
</div>

## Question 2
Create a single case discriminated union to represent a particular kind of string:

1. Create a discriminated union named Ticker with a single case Ticker of string. 
2. Then wrap the string "ABC" in your Ticker type and assign it to a value named 'aTicker'.
3. Then use pattern matching to unwrap the string in `aTicker` and assign it to a value named `aTickerString`.

Discriminated unions like this are usful if you want to make 
sure that you don't accidentally mix up two strings that represent
different things.
A function that takes an input with type `Ticker` will not accept any string,
it will only accept inputs that have time Ticker.

<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type Ticker = | Ticker of string
val aTicker : Ticker = Ticker "ABC"
val aTickerString : string = "ABC"
```

</details>
</span>
</p>
</div>

## Question 3
Create a single case discriminated union to represent a particular kind of float:

1. Create a discriminated union named Signal with a single case Signal of float. 
2. Then wrap the string float `1.0` in your Signal type and assign it to a value named 'aSignal'.
3. Then use pattern matching to unwrap the float in `aSignal` and assign it to a value named `aSignalFloat`.

<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type Signal = | Signal of float
val aSignal : Signal = Signal 1.2
val aSignalFloat : float = 1.2
```

</details>
</span>
</p>
</div>

## Question 4
Create a discriminated union called called `Funds` with two cases: MutualFund of string and HedgeFund of string.

1. Create a MutualFund case of the Fund union with the string "Fidelity Magellan". Assign it to a value named "magellan".
2. Create a HedgeFund case of the Fund union with the string "Renaissance Medallion". Assign it to a value named "renaissance".


<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type Funds =
  | MutualFund of string
  | HedgeFund of string
val magellan : Funds = MutualFund "Fidelity Magellan"
val renaissance : Funds = HedgeFund "Renaissance Medallion"
```

</details>
</span>
</p>
</div>

## Question 5
Define two types with the same cases.

*)
type Ambiguous1 = Up | Down
type Ambiguous2 = Up | Down
(**
If you try to assign Ambiguous1 to values, 
it can be hard for the compiler (and yourself)
to figure out which of these types you mean. If you write `Up`
the compiler will think that you meant to use whatever was defined
last (Ambigous2).

Use fully qualified names to show how to assign the `Up` case from Ambiguous1 to
a value named `ambiguous1` and the `Up` case from Ambiguous2 to a value named
`ambiguous2`. 

<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type Ambiguous1 =
  | Up
  | Down
type Ambiguous2 =
  | Up
  | Down
val ambiguous1 : Ambiguous1 = Up
val ambiguous2 : Ambiguous2 = Up
```

</details>
</span>
</p>
</div>

## Question 6
Imagine that analyst recommendations have the form

*)
type AnalystRec = Buy | Sell | Hold
(**
You have recommendations from two analysts

*)
let goldmanRec = Buy
let barclaysRec = Sell
(**
You want to act on goldman recommendations as follows:

*)
let actionOnGoldman (x: AnalystRec) =
    match x with
    | Buy | Hold -> "I am buying this!"
    | Sell -> "I am selling this!"
(**
The problem is that this `actionOnGoldman` function will
work for both `goldmanRec` and `barclaysRec`.

*)
actionOnGoldman goldmanRec // evaluates to "I am buying this!"
actionOnGoldman barclaysRec // evaluates to "I am selling this!"
(**
1. Create a single case union called `GoldmanRec` where the single case
is GoldmanRec of AnalystRec. 
2. Create a modified `actionOnGoldman` function called `actionOnGoldmanOnly` so that it will only work on recommendations with the type `GoldmanRec`.

If `wrappedGoldmanRec` is buy `GoldmanRec`, the result should be
```fsharp
actionOnGoldmanOnly wrappedGoldmanRec // evaluates to "I am buying this!"
actionOnGoldmanOnly barclaysRec // compiler error.
```

<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type AnalystRec =
  | Buy
  | Sell
  | Hold
type GoldmanRec = | GoldmanRec of AnalystRec
val goldmanRec : AnalystRec = Buy
val barclaysRec : AnalystRec = Sell
val actionOnGoldman : x:AnalystRec -> string
```

What we want:

```
val it : string = "I am buying this!"
```

Oops, we only want actionOnGoldman to work for Goldman recommendation.

```
val it : string = "I am buying this!"
```

---
** Actual answer starts here **

Constructing created value from scratch:

```
val wrappedGoldmanRec : GoldmanRec = GoldmanRec Buy
```

or, wrapping our previously created value:

```
val wrappedGoldmanRec2 : GoldmanRec = GoldmanRec Buy
val it : bool = true
```

Now, constructing recommendation function from scratch:

```
val actionOnGoldmanOnly : x:GoldmanRec -> string
```

or, constructing it from scratch with pattern matching:  

```
val actionOnGoldmanOnly2 : GoldmanRec -> string
```

or, since you see above that once we unwrap the goldman rec,
it is the same as our original function.

```
val actionOnGoldmanOnly3 : GoldmanRec -> string
```

Now let's check the results.
All the alternatives should return the same result.

```
val Goldman1 : string = "I am buying this!"
val Goldman2 : string = "I am buying this!"
val Goldman3 : string = "I am buying this!"
```

```fsharp
actionOnGoldmanOnly barclaysRec //compiles to error
actionOnGoldmanOnly2 barclaysRec //compiles to error
actionOnGoldmanOnly3 barclaysRec //compiles to error
```
These would all give compiler errors. 
So now we have what we want, actionOnGoldmanOnly only works for Goldman recommendations.

</details>
</span>
</p>
</div>

## Question 7
Imagine that stock tips have the form

*)
type StockTip = Buy | Sell | Hold
(**
You have recommendations from two people

*)
let friendRec = Buy
let professorRec = Sell
(**
You want to actions as follows:

*)
let actionOnFriend (x: StockTip) = x
let actionOnProfessor (x: StockTip) =
    match x with
    | Buy -> StockTip.Sell
    | Hold -> StockTip.Sell
    | Sell -> StockTip.Buy
(**
1. Create a two case union called `FriendOrFoe` where the two cases are Friend of StockTip and Professor of StockTip.
2. Create a function called `actionFriendOrFoe` that will properly handle tips from friends and tips from professors.

Show that `friendRec` and `professorRec` wrapped in the `FriendOrFoe` type are handled properly by `actionFriendOrFoe`.

<div style="padding-left: 40px;">
<p> 
<span>
<details>
<summary><p style="display:inline">answer</p></summary>

```
type StockTip =
  | Buy
  | Sell
  | Hold
val friendRec : StockTip = Buy
val professorRec : StockTip = Sell
val actionOnFriend : x:StockTip -> StockTip
val actionOnProfessor : x:StockTip -> StockTip
```

or, since we're doing the same thing with a professor's
Buy or Hold recommendation, this could also be written

```
val actionOnProfessor2 : x:StockTip -> StockTip
type FriendOrFoe =
  | Friend of StockTip
  | Professor of StockTip
val wrappedFriendRec : FriendOrFoe = Friend Buy
val wrappedProfessorRec : FriendOrFoe = Professor Sell
val actionOnFriendOrFoe : x:FriendOrFoe -> StockTip
```

```
val friendOrFoe1 : StockTip = Buy
val friendOrFoe2 : StockTip = Buy
val friendOrFoe3 : StockTip = Sell
```

</details>
</span>
</p>
</div>

*)

