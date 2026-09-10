using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab04_Valeriano.Models;
using Lab04_Valeriano.Repositories;

namespace Lab04_Valeriano.ViewModels;

public partial class ProductoViewModel : ObservableObject
{
    private readonly ProductoRepository _repository;

    public ProductoViewModel() : this(new ProductoRepository()) { }

    public ProductoViewModel(ProductoRepository repository)
    {
        _repository = repository;
        Productos = new ObservableCollection<Producto>();
        Cargar();
    }

    public ObservableCollection<Producto> Productos { get; }

    [ObservableProperty]
    private Producto? productoSeleccionado;

    [RelayCommand]
    private void Cargar()
    {
        Productos.Clear();
        foreach (var producto in _repository.List())
        {
            Productos.Add(producto);
        }
    }

    [RelayCommand]
    private void Nuevo()
    {
        ProductoSeleccionado = new Producto();
    }

    [RelayCommand]
    private void Guardar()
    {
        if (ProductoSeleccionado is null) return;

        if (ProductoSeleccionado.ProductoID == 0)
        {
            ProductoSeleccionado.ProductoID = _repository.Insert(ProductoSeleccionado);
        }
        else
        {
            _repository.Update(ProductoSeleccionado);
        }
        Cargar();
    }

    [RelayCommand]
    private void Eliminar()
    {
        if (ProductoSeleccionado is null || ProductoSeleccionado.ProductoID == 0) return;
        _repository.Delete(ProductoSeleccionado.ProductoID);
        Cargar();
    }
}
