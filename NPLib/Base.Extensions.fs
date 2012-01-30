[<AutoOpen>]
module Base.Extensions

open System
open System.Windows

type IObservable<'a> with
  /// イベントを購読したり、破棄したりする
  member __.toggled (f:'a -> unit) = 
    let disposable = ref Unchecked.defaultof<_>
    Toggler(
      // イベントを購読する
      fun _ -> disposable := __ |> Observable.subscribe f
      // イベントを破棄する
      ,fun _ -> (!disposable).Dispose()
    )