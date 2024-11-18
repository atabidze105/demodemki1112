using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo1112remake.Models;
using Metsys.Bson;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace demo1112remake;

public partial class ListWindow : Window
{
    User _LogUser = null;
    private List<Product> _Products = demo1112remake.Hepler.DBContext.Products.
        Include(x => x.CathegoryNavigation).
        Include(x => x.ManufacturerNavigation).
        Include(x => x.SupplierNavigation).OrderBy(x => x.Id).
        ToList();
    private List<Product> _FoundedProducts = null;

    public ListWindow()
    {
        _LogUser = new User();
        InitializeComponent();

        lbox_products.ItemsSource = _Products.ToList();
        tblock_login.Text = "Выполнен вход: Гость";

        btn_add.IsVisible = false;

        tblock_itemsCount.Text = $"{_Products.Count}/{_Products.Count}";
    }
    public ListWindow(User LoggedInUser)
    {
        _LogUser = LoggedInUser;
        InitializeComponent();

        lbox_products.ItemsSource = _Products.ToList();
        grid_listWindow.DataContext = LoggedInUser;

        if (_LogUser.Role != 1)
            btn_add.IsVisible = false;

        tblock_itemsCount.Text = $"{_Products.Count}/{_Products.Count}";
    }

    private void Panel_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (_LogUser.Role == 1)
        {
            Product product = lbox_products.SelectedItem as Product;
            AddWindow addWindow = new AddWindow(product, _LogUser);
            addWindow.Show();
            Close();
        }
    }

    private void RetutnToMain(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow main = new MainWindow();
        main.Show();
        Close();
    }

    private void addProduct(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        AddWindow addWindow = new AddWindow(_LogUser);
        addWindow.Show();
        Close();
    }

    private void TextInput(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        AllFiltrationSorting();
    }

    private void ComboBox_SortingSelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        AllFiltrationSorting();
    }

    private void ComboBox_FiltrationSelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        AllFiltrationSorting();
    }

    private void AllFiltrationSorting()
    {
        _FoundedProducts = SearchingFiltrating(Filtration(_Products));
        lbox_products.ItemsSource = Sorting(_FoundedProducts);
        tblock_itemsCount.Text = _FoundedProducts.Count > 0 ? $"{_FoundedProducts.Count}/{_Products.Count}" : $"{_Products.Count}/{_Products.Count}";
    }

    private List<Product> Filtration(List<Product> products)
    {
        if (cbox_filtration != null)
            switch (cbox_filtration.SelectedIndex)
            {
                case 1:
                    return products.Where(x => (x.ActualDiscount >= 0) && (x.ActualDiscount < 10)).ToList();
                case 2:
                    return products.Where(x => (x.ActualDiscount >= 10) && (x.ActualDiscount < 15)).ToList();
                case 3:
                    return products.Where(x => x.ActualDiscount >= 15).ToList();
            }
        return products;
    }

    private List<Product> SearchingFiltrating(List<Product> products)
    {
        if (tbox_searchbar != null)
        {
            List<Product> temp = new();
            string keyword = tbox_searchbar.Text;

            if (keyword == "")
                return temp = products.Where(x => x.Name.Trim().ToLower().Contains(keyword.Trim().ToLower())).ToList();
        }
        
        return products;
    }

    private List<Product> Sorting(List<Product> products)
    {
        if (cbox_sorting != null)
            switch (cbox_sorting.SelectedIndex)
            {
                case 1:
                    return products.OrderBy(x => x.Price).ToList();
                case 2:
                    return products.OrderByDescending(x => x.Price).ToList();
            }
        return products;
    }
}