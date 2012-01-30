module Np.Color

open System
open System.Windows.Media

/// 16進数の文字を整数値に変換
let hexLetterToInt (c:char) =
  let code = int c
  if   Char.IsDigit  c then code - int '0'
  elif Char.IsLetter c then code - int 'a' + 10
  else 0

let ofHex i =
  let r = i / pown 256 2 |> byte
  let g = i / pown 256 1 |> byte
  let b = i |> byte
  Color.FromRgb(r,g,b)    
