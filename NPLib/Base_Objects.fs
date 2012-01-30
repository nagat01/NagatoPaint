[<AutoOpen>]
module Np.Base_Objects

/// 変更を同期させる値
type SharedValue<'a when 'a : equality>(value:'a) =
  let mutable v = value
  let updated = Event<'a>()
  member o.Value = v
  member o.Update value =
    let old = v 
    v <- value
    if old <> v then v |> updated.Trigger
  
  member o.Updated = updated.Publish
  member o.Add handler = updated.Publish.Add handler
  member o.Subscribe handler = updated.Publish.Subscribe handler

  member o.Initialize = v |> updated.Trigger

type sv<'a when 'a : equality> = SharedValue<'a>


/// ないかもしれない値を管理するオブジェクト
type OptionBox<'a>(?value:'a) = 
  let mutable _value = value
  /// 値をセット、ゲット
  member __.Value
    with get ()    = _value.Value 
    and  set value = _value <- Some value
  /// 値はあるか？
  member __.IsSome = _value.IsSome
  /// 値はないか？
  member __.IsNone = _value.IsNone
  /// 値を消去
  member __.Clear  = _value <- None
  /// 値があれば関数を適用
  member __.Apply f =
    match _value with
    | Some v -> f v
    | None   -> ()

type obox<'a> = OptionBox<'a>



/// トグルする
/// fToOn  オンにする時に実行する関数
/// fToOff オフにする時に実行する関数
type Toggler(fToOn :unit->unit, fToOff: unit->unit) =
  let mutable isOn = false
  member __.Toggle =
    if not isOn then
      fToOn()
    else
      fToOff()
    isOn <- not isOn

  member __.On =
    if not isOn then
      fToOn()
    isOn <- true

  member __.Off =
    if isOn then
      fToOff()
    isOn <- false