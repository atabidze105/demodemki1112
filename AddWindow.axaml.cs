using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using demo1112remake.Models;
using Metsys.Bson;
using demo1112remake.Context;
using static demo1112remake.Hepler;
using Avalonia.Media.Imaging;
using System.IO;

namespace demo1112remake;

public partial class AddWindow : Window
{
    User _LogUser;
    Product _Dummy = null;
    string _ImageAdd;
    string _ImageDelete;

    public AddWindow() //для предпросмотра
    {
        _Dummy = new Product();
        InitializeComponent();
        grid_addWindow.DataContext = _Dummy;
        tblock_header.Text = "Добавление товара";
        btn_add.Content = "Добавить";
        ComboBoxInit();
    }
    public AddWindow(User user) //добавление
    {
        _LogUser = user;
        _Dummy = new Product();
        InitializeComponent();
        grid_addWindow.DataContext = _Dummy;
        tblock_header.Text = "Добавление товара";
        btn_add.Content = "Добавить";
        ComboBoxInit();
    }
    public AddWindow(Product product, User user) //редактирование
    {
        _LogUser = user;
        _Dummy = product;
        InitializeComponent();
        grid_addWindow.DataContext = _Dummy;
        tblock_header.Text = "Редактирование товара";
        btn_del.IsVisible = true;
        btn_add.Content = "Сохранить";
        ComboBoxInit();

        cbox_cat.SelectedIndex = product.Cathegory - 1;
        cbox_man.SelectedIndex = product.Manufacturer - 1;
        cbox_sup.SelectedIndex = product.Supplier - 1;
        if (_Dummy.Image != null)
        {
            stpanel_preview.IsVisible = true;
            btn_delimage.IsVisible = true;
        }
    }

    private void ComboBoxInit()
    {
        List<string> cats = [];
        List<string> mans = [];
        List<string> sups = [];
        foreach (var manufacturer in DBContext.Manufacturers.ToList())
        {
            mans.Add(manufacturer.Name);
        }
        foreach (var cathegorie in DBContext.Cathegories.ToList())
        {
            cats.Add(cathegorie.Name);
        }
        foreach (var supplier in DBContext.Suppliers.ToList())
        {
            sups.Add(supplier.Name);
        }
        cbox_cat.ItemsSource = cats;
        cbox_man.ItemsSource = mans;
        cbox_sup.ItemsSource = sups;
        cbox_cat.SelectedIndex = 0;
        cbox_man.SelectedIndex = 0;
        cbox_sup.SelectedIndex = 0;
    }

    private void Return(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ListWindow listWindow = new ListWindow(_LogUser);
        listWindow.Show();
        Close();
    }

    private void Confirm(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if(_ImageDelete != null)
        {
            File.Delete($"Assets/{_ImageDelete}");
            _Dummy.Image = null;
        }
            
        if (_ImageAdd != null)
        {
            File.Copy(_ImageAdd, $"Assets/{Path.GetFileName(_ImageAdd)}", true);
            _Dummy.Image = Path.GetFileName(_ImageAdd);
        }
        _Dummy.Cathegory = cbox_cat.SelectedIndex + 1;
        _Dummy.Manufacturer = cbox_man.SelectedIndex + 1;
        _Dummy.Supplier = cbox_sup.SelectedIndex + 1;
        if (_Dummy.Id == 0)
            DBContext.Products.Add(_Dummy);
        DBContext.SaveChanges();
        new ListWindow(_LogUser).Show();
        Close();
    }

    private void ImageDelete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_Dummy.Image != null)
            _ImageDelete = _Dummy.Image;
        stpanel_preview.IsVisible = false;
        btn_delimage.IsVisible = false;
    }

    private async void ImageAdd(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog dialog = new();
        dialog.Filters.Add(ImageFilter);

        string[] result = await dialog.ShowAsync(this);
        if (result == null || result.Length == 0)
            return;

        string imagePath = result[0];
        string imageName = System.IO.Path.GetFileName(imagePath);
        _ImageAdd = imagePath;

        stpanel_preview.IsVisible = true;
        btn_delimage.IsVisible = true;
        img_preview.Source = new Bitmap(imagePath);
        tblock_preview.Text = imageName;
    }

    private readonly FileDialogFilter ImageFilter = new()
    {
        Extensions = new List<string>() { "jpg", "png" },
        Name = "файлы изображений"
    };

    private void Delete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_Dummy.Image != null)
            File.Delete($"Assets/{_Dummy.Image}");
        DBContext.Products.Remove(_Dummy);
        DBContext.SaveChanges();
        new ListWindow(_LogUser).Show();
        Close();
    }
}