[<AutoOpen>]
module Np.Base_Geometry

open System.Windows
                                    
let (+.) (p1:Point) (p2:Point) =
  Point(p1.X + p2.X, p1.Y + p2.Y)

let (-.) (p1:Point) (p2:Point) =
  Point(p1.X - p2.X, p1.Y - p2.Y)

let distance (p1:Point) (p2:Point) =
  ((p1.X - p2.X) ** 2. + (p1.Y - p2.Y) ** 2.) ** 0.5