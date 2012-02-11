[<AutoOpen>]
module LayerUi

open System
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging

type LayerEdit(layers:Layers) as sp =
  inherit StackPanel()
  let btnAddLayer = 
    Button(Content="レイヤーを追加")
    $ sp.add
  do 
    btnAddLayer.Click =>~ fun _ ->
      layers.AddLayer(Layer.ofSize 600 600)
  

/// レイヤーの情報を表示するパネル
type LayerPanel (layer:Layer) as sp =
  inherit StackPanel()
  do 
    sp $ sp_horizontal 
    |> bgco Colors.LightSalmon

  /// レイヤーが選択されているか
  let rbxIsSelected = RadioButton() $ sp.add
  do
    rbxIsSelected.Checked =>~ fun _ ->
      layer.IsSelected.Change <| true
    layer.IsSelected.Changed =>~ fun __ ->
      rbxIsSelected.IsChecked <- Nullable __

    
  /// レイヤーの表示非表示
  let cbxIsVisible = CheckBox() $ sp.add
  do 
    cbxIsVisible.Click =>~ fun _ ->
      if cbxIsVisible.IsChecked.HasValue then
        layer.IsVisible.Change <| cbxIsVisible.IsChecked.Value
    layer.IsVisible.Changed =>~ fun __ ->
      cbxIsVisible.IsChecked <- Nullable __

  /// レイヤー名
  let tbxName = TextBox() $ sp.add
  do
    tbxName.TextChanged =>~ fun _ ->
      tbxName.Text |> layer.Name.Replace
    layer.Name.Changed =>~ fun _ ->
      tbxName.Text <- layer.Name.V
  
/// レイヤーの集合を表示するパネル
type LayersPanel (layers:Layers) as sp =
  inherit StackPanel()
  do 
    layers.Layers.Changed =>~ fun __ ->
      sp.Children.Clear()
      for layer in __ do
        LayerPanel layer |> sp.add
