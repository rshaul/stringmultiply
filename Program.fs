open System
open System.IO
open System.Collections.Generic
open System.Text
open System.Linq

let addc (c1 : char) (c2 : char) (r : int) =
    let n1 = int(Char.GetNumericValue(c1))
    let n2 = int(Char.GetNumericValue(c2))
    let n = n1 + n2 + r
    (n % 10, n / 10)

let adds_actual (s1 : char seq) (s2 : char seq) =
    let o = new StringBuilder()
    let mutable r = 0
    let e1 = s1.GetEnumerator()
    let e2 = s2.GetEnumerator()
    while e1.MoveNext() && e2.MoveNext() do
        let result = addc e1.Current e2.Current r
        ignore(o.Append(fst(result)))
        r <- snd(result)
    if r <> 0 then
        ignore(o.Append(r))
    new String(o.ToString().ToCharArray().Reverse().ToArray())

let adds (s1 : string) (s2 : string) =
    let r1 = s1.ToCharArray().Reverse().ToList()
    let r2 = s2.ToCharArray().Reverse().ToList()
    while r1.Count < r2.Count do
        r1.Add('0')
    while r2.Count < r1.Count do
        r2.Add('0')
    adds_actual r1 r2

let muln (n1 : int) (n2 : int) (r : int) =
    let n = (n1 * n2) + r
    (n % 10, n / 10)

let mulcs (c1 : char) (s2: char[]) (index : int) =
    let o = new StringBuilder()
    for i = 0 to (index-1) do
        ignore(o.Append('0'))
    let mutable r = 0
    let n1 = int(Char.GetNumericValue(c1))
    for c2 in s2 do
        let n2 = int(Char.GetNumericValue(c2))
        let result = muln n1 n2 r
        ignore(o.Append(fst(result)))
        r <- snd(result)
    if r <> 0 then
        ignore(o.Append(r))
    new String(o.ToString().ToCharArray().Reverse().ToArray())

let muls_actual (s1 : char[]) (s2 : char[]) =
    let results = new List<String>()
    for i = 0 to (s1.Length - 1) do
        let c1 = s1.[i]
        results.Add(mulcs c1 s2 i)
    let mutable s = ""
    for result in results do
        s <- adds s result
    s

let muls (s1 : string) (s2 : string) =
    let r1 = s1.ToCharArray().Reverse().ToArray()
    let r2 = s2.ToCharArray().Reverse().ToArray()
    muls_actual r1 r2

let mainF () =
    let start = DateTime.Now
    let f = (new StreamReader("numbers.txt")).ReadToEnd().Trim()
    let s = f.Split(',')
    let s1 = s.[0]
    let s2 = s.[0]
    let r = muls s1 s2
    Console.WriteLine(r)
    Console.WriteLine("Result Length: " + r.Length.ToString())
    Console.WriteLine(s1.Length.ToString() + "x" + s2.Length.ToString() + " Time: " + (DateTime.Now - start).TotalSeconds.ToString() + " seconds")
    Console.ReadLine()

let mainC () =
    while true do
        Console.Write "Enter Number 1: "
        let n1 = Console.ReadLine().Trim()
        Console.Write "Enter Number 2: "
        let n2 = Console.ReadLine().Trim()
        Console.WriteLine("String: " + muls n1 n2)
        try
            Console.WriteLine("StdLib: " + (Decimal.Parse(n1) * Decimal.Parse(n2)).ToString())
        with
            e -> Console.WriteLine(e.Message)

mainC()