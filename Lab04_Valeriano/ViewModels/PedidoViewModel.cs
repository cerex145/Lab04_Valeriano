using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab04_Valeriano.Models;
using Lab04_Valeriano.Repositories;

namespace Lab04_Valeriano.ViewModels;

public partial class PedidoViewModel : ObservableObject
{
    private readonly PedidoRepository _repository;
    private readonly DetallePedidoRepository _detalleRepository;

    public PedidoViewModel() : this(new PedidoRepository(), new DetallePedidoRepository()) { }

    public PedidoViewModel(PedidoRepository repository, DetallePedidoRepository detalleRepository)
    {
        _repository = repository;
        _detalleRepository = detalleRepository;
        Pedidos = new ObservableCollection<Pedido>();
        ReporteDetalle = new ObservableCollection<DetallePedidoReporte>();
        FechaInicio = DateTime.Today.AddMonths(-1);
        FechaFin = DateTime.Today;
        Cargar();
    }

    public ObservableCollection<Pedido> Pedidos { get; }
    public ObservableCollection<DetallePedidoReporte> ReporteDetalle { get; }

    [ObservableProperty]
    private Pedido? pedidoSeleccionado;

    [ObservableProperty]
    private DateTime fechaInicio;

    [ObservableProperty]
    private DateTime fechaFin;

    [RelayCommand]
    private void Cargar()
    {
        Pedidos.Clear();
        foreach (var pedido in _repository.List())
        {
            Pedidos.Add(pedido);
        }
    }

    [RelayCommand]
    private void Nuevo()
    {
        PedidoSeleccionado = new Pedido { FechaPedido = DateTime.Today };
    }

    [RelayCommand]
    private void Guardar()
    {
        if (PedidoSeleccionado is null) return;

        if (PedidoSeleccionado.PedidoID == 0)
        {
            PedidoSeleccionado.PedidoID = _repository.Insert(PedidoSeleccionado);
        }
        else
        {
            _repository.Update(PedidoSeleccionado);
        }
        Cargar();
    }

    [RelayCommand]
    private void Eliminar()
    {
        if (PedidoSeleccionado is null || PedidoSeleccionado.PedidoID == 0) return;
        _repository.Delete(PedidoSeleccionado.PedidoID);
        Cargar();
    }

    [RelayCommand]
    private void GenerarReporte()
    {
        ReporteDetalle.Clear();
        foreach (var detalle in _detalleRepository.ListarPorFechas(FechaInicio, FechaFin))
        {
            ReporteDetalle.Add(detalle);
        }
    }
}
