using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;

namespace TestDragAndDrop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        int textCount = 0;

        
        
        
        
        
        
        
        
        
        
        
            

        SetupDnd(d => d.Set(DataFormats.Text, $"Text was dragged {++textCount} times"),
            DragDropEffects.Move);
    }

    private void SetupDnd(Action<DataObject> factory, DragDropEffects effects) =>
        SetupDnd(
            o =>
            {
                factory(o);
                return Task.CompletedTask;
            }, 
            effects);
        
        
        
        
        
        
        
        

    private void SetupDnd(Func<DataObject, Task> factory, DragDropEffects effects)
    {
        //Действие тащить
        async void DoDrag(object? sender, PointerPressedEventArgs e)
        {
            var dragData = new DataObject();
            await factory(dragData);

                
            //Результат действия тащить
            var result = await DragDrop.DoDragDrop(e, dragData, effects);
            switch (result)
            {
                case DragDropEffects.Move:
                    DragStateText.Text = "Действие выпалнено";
                    break;
                case DragDropEffects.None:
                    DragStateText.Text = "Действие отменено";
                    break;
            }
        }
        //Действие бросить
        void Drop(object? sender, DragEventArgs e)
        {
            if (e.Source is Control c && c.Name == "MoveTarget")
            {
                e.DragEffects = DragDropEffects.Move;
            }
            if (e.Data.Contains(DataFormats.Text))
            {
                DropState.Text = e.Data.GetText();
            }
        }
        DragMeText.PointerPressed += DoDrag;
        AddHandler(DragDrop.DropEvent, Drop);
    }
}