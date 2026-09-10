using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab04_Valeriano.Models;
using Lab04_Valeriano.Repositories;

namespace Lab04_Valeriano.ViewModels;

public partial class CategoriaViewModel : ObservableObject
{
    private readonly CategoriaRepository _repository;

    public CategoriaViewModel() : this(new CategoriaRepository()) { }

    public CategoriaViewModel(CategoriaRepository repository)
    {
        _repository = repository;
        Categorias = new ObservableCollection<Categoria>();
        Cargar();
    }

    public ObservableCollection<Categoria> Categorias { get; }

    [ObservableProperty]
    private Categoria? categoriaSeleccionada;

    [RelayCommand]
    private void Cargar()
    {
        Categorias.Clear();
        foreach (var categoria in _repository.List())
        {
            Categorias.Add(categoria);
        }
    }

    [RelayCommand]
    private void Nuevo()
    {
        CategoriaSeleccionada = new Categoria();
    }

    [RelayCommand]
    private void Guardar()
    {
        if (CategoriaSeleccionada is null) return;

        if (CategoriaSeleccionada.CategoriaID == 0)
        {
            CategoriaSeleccionada.CategoriaID = _repository.Insert(CategoriaSeleccionada);
        }
        else
        {
            _repository.Update(CategoriaSeleccionada);
        }
        Cargar();
    }

    [RelayCommand]
    private void Eliminar()
    {
        if (CategoriaSeleccionada is null || CategoriaSeleccionada.CategoriaID == 0) return;
        _repository.Delete(CategoriaSeleccionada.CategoriaID);
        Cargar();
    }
}
