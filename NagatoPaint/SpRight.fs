[<AutoOpen>]
module NpUi_SpRight

open System.Windows.Controls
open System.Windows.Media

type SpRight(m:NpModel) as sp =
  inherit StackPanel()
  do sp |> bgco Colors.LightBlue
  let layerEdit = LayerEdit(m.Layers) $ sp.add
  let layerPanel = LayersPanel(m.Layers) $ sp.add