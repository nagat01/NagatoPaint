﻿[<AutoOpen>]
module Np.Base_Objects

open System

/// 変更を同期させる値
type Shared<'a>(value:'a) =
  let mutable _value = value
  /// 変更通知イベント
  let changed = Event<'a>()
  let inits = ResizeArray<unit Lazy>()
  /// 値
  member __.V = _value

  member __.Change value =
    _value <- value
    changed.Trigger _value
  
  member __.Changed = changed.Publish
  member __.AddInits init = inits.Add init

  member __.Initialize = 
    _value |> changed.Trigger
    for init in inits do
      init.Value

type sh<'a> = Shared<'a>


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
/// on'  オンにする時に実行される関数
/// off' オフにする時に実行する関数
type Toggler(on' :unit->unit, off': unit->unit) =
  let mutable _isOn = false
  member __.Toggle =
    if not _isOn then on'() else off'()
    _isOn <- not _isOn

  /// ONもしくはOFFにする
  member __.Set isOn =
    if isOn <> _isOn then __.Toggle

  member __.On  = __.Set true
  member __.Off = __.Set false
