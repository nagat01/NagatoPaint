// [<AutoOpen>]
module Np.Color

open System
open System.Windows.Media

/// 16進数の文字 -> int
let hexLetterToInt (c:char) =
  let code = int c
  if   Char.IsDigit  c then code - int '0'
  elif Char.IsLetter c then code - int 'a' + 10
  else 0

/// 16進数 -> Color
let ofHex i =
  let r = i / pown 256 2 |> byte
  let g = i / 256 |> byte
  let b = i |> byte
  Color.FromRgb(r,g,b)
  
/// Color -> int
let toInt (co:Color) =
  (int co.A <<< 24) + 
  (int co.R <<< 16) + 
  (int co.G <<<  8) + 
  int co.B
