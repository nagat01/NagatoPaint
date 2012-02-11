[<AutoOpen>]
module TE

open System.Windows
open System.Windows.Controls
open Microsoft.Win32

// basic controls //
type ContentControl with
  member control.add x = control.Content <- x

type ItemsControl with
  member control.add x = control.Items.Add x |> ignore

// panels //
type Panel with
  member panel.add x = panel.Children.Add x |> ignore

type Grid with
  member __.addTo (col,row) (uie:UIElement) =
    Grid.SetColumn (uie, col)
    Grid.SetRow    (uie, row)
    __.add uie |> ignore

let private addToDockPanel dock uie (dp:DockPanel) =
  DockPanel.SetDock(uie , dock)
  dp.add uie 

type DockPanel with
  member dp.addTop    uie = addToDockPanel Dock.Top    uie dp
  member dp.addBottom uie = addToDockPanel Dock.Bottom uie dp
  member dp.addLeft   uie = addToDockPanel Dock.Left   uie dp
  member dp.addRight  uie = addToDockPanel Dock.Right  uie dp

type Canvas with
  member canvas.addTo (p:Point) uie =
    Canvas.SetLeft(uie,p.X)
    Canvas.SetTop (uie,p.Y)
    canvas.add uie 

  member canvas.addZ z uie =
    Canvas.SetZIndex(uie,z)
    canvas.add uie 

type FileDialog with
  member __.showAndOk =
    let result = __.ShowDialog()
    result.HasValue && result.Value