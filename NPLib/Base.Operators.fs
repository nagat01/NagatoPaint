[<AutoOpen>]
module Base.Operators

/// メソッドチェーン
let ($) x f = f x ; x

/// イベントを合体
let (<+>) = Observable.merge
/// イベントを合体(型なし)
let (<++>) o1 o2 = 
  Observable.map (fun x -> x :> obj) o1 <+>
  Observable.map (fun x -> x :> obj) o2

/// イベントを追加する
let (=>) event f = event |> Observable.subscribe f

let (<!) event f = event |> Observable.add f
