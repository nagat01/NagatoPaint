[<Struct>]
type ShortRateTreeNode = 
  val DF : double
  val PU : double
  val PM : double
  val PD : double
  val IU : int
  val IM : int
  val ID : int

  new (df, pu, pm, pd, iu, im, id) = {
    DF = df
    PU = pu
    PM = pm
    PD = pd
    IU = iu
    IM = im
    ID = id
    }

let induceBackward (nodes : ShortRateTreeNode []) (values : double []) : double [] =
  let next k = values.[values.Length / 2 + k]
  let value (node : ShortRateTreeNode) =
    (node.PU * next node.IU + node.PM * next node.IM + node.PD * next node.ID) * node.DF
  Array.map value nodes

let ITERATION = 10000

let run () =
  let maturityValues i = Array.create 201 (double i)
  let createNode j = ShortRateTreeNode(1.0, 1.0/6.0, 2.0/3.0, 1.0/6.0, j+1, j, j-1)
  let testTree = [| for i in 0..99 -> Array.map createNode [|-i..i|] |]
  let value i = (Array.foldBack induceBackward testTree (maturityValues i)).[0]
  stdout.WriteLine (Array.max (Array.init ITERATION value))

type ShortRateTreeNode2 = 
  { 
    DF : double
    PU : double
    PM : double
    PD : double
    IU : int
    IM : int
    ID : int
  }

let induceBackward2 (nodes : ShortRateTreeNode2 []) (values : double []) : double [] =
  let halfLength = values.Length / 2
  let next k = values.[halfLength + k]
  let value (node : ShortRateTreeNode2) =
    (node.PU * next node.IU + node.PM * next node.IM + node.PD * next node.ID) * node.DF
  Array.map value nodes

let run2 () =
  // let maturityValues i = Array.create 201 (double i)
  let createNode j = { DF=1.0; PU=1.0/6.0; PM=2.0/3.0; PD=1.0/6.0; IU=j+1; IM=j; ID=j-1 }
  let testTree = [ for i in 99 .. -1 .. 0 -> [| for j in -i..i -> createNode j |] ]
  let value i = 
    let mutable result = Array.create 201 (float i)
    for nodes in testTree do
      result <- induceBackward2 nodes result
    result.[0]    
    // (Array.foldBack induceBackward2 testTree (maturityValues i)).[0]
  Seq.init ITERATION value
  |> Seq.max
  |> printfn "%f"

run2 ()