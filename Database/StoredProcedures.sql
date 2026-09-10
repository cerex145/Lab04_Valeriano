/* ============================================================
   NeptunoDB — Stored Procedures
   ------------------------------------------------------------
   Ejecutar despues de NeptunoDB.sql
   ============================================================ */

USE NeptunoDB;
GO

/* ===================== Productos ===================== */
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProductoID, NombreProducto, ProveedorID, CategoriaID, CantidadPorUnidad,
           PrecioUnidad, UnidadesEnExistencia, UnidadesEnPedido, NivelDeReorden, Descontinuado
    FROM dbo.Productos;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_ObtenerPorId
    @ProductoID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProductoID, NombreProducto, ProveedorID, CategoriaID, CantidadPorUnidad,
           PrecioUnidad, UnidadesEnExistencia, UnidadesEnPedido, NivelDeReorden, Descontinuado
    FROM dbo.Productos
    WHERE ProductoID = @ProductoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_Insertar
    @NombreProducto NVARCHAR(60),
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CantidadPorUnidad NVARCHAR(30) = NULL,
    @PrecioUnidad DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido SMALLINT,
    @NivelDeReorden SMALLINT,
    @Descontinuado BIT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Productos (NombreProducto, ProveedorID, CategoriaID, CantidadPorUnidad,
        PrecioUnidad, UnidadesEnExistencia, UnidadesEnPedido, NivelDeReorden, Descontinuado)
    VALUES (@NombreProducto, @ProveedorID, @CategoriaID, @CantidadPorUnidad,
        @PrecioUnidad, @UnidadesEnExistencia, @UnidadesEnPedido, @NivelDeReorden, @Descontinuado);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_Actualizar
    @ProductoID INT,
    @NombreProducto NVARCHAR(60),
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CantidadPorUnidad NVARCHAR(30) = NULL,
    @PrecioUnidad DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido SMALLINT,
    @NivelDeReorden SMALLINT,
    @Descontinuado BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Productos
    SET NombreProducto = @NombreProducto,
        ProveedorID = @ProveedorID,
        CategoriaID = @CategoriaID,
        CantidadPorUnidad = @CantidadPorUnidad,
        PrecioUnidad = @PrecioUnidad,
        UnidadesEnExistencia = @UnidadesEnExistencia,
        UnidadesEnPedido = @UnidadesEnPedido,
        NivelDeReorden = @NivelDeReorden,
        Descontinuado = @Descontinuado
    WHERE ProductoID = @ProductoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_Eliminar
    @ProductoID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Productos WHERE ProductoID = @ProductoID;
END
GO

/* ===================== Categorias ===================== */
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CategoriaID, NombreCategoria, Descripcion FROM dbo.Categorias;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_ObtenerPorId
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CategoriaID, NombreCategoria, Descripcion
    FROM dbo.Categorias WHERE CategoriaID = @CategoriaID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Insertar
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Categorias (NombreCategoria, Descripcion)
    VALUES (@NombreCategoria, @Descripcion);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Actualizar
    @CategoriaID INT,
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Categorias
    SET NombreCategoria = @NombreCategoria, Descripcion = @Descripcion
    WHERE CategoriaID = @CategoriaID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Eliminar
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Categorias WHERE CategoriaID = @CategoriaID;
END
GO

/* ===================== Proveedores ===================== */
CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProveedorID, CompaniaNombre, NombreContacto, CargoContacto, Direccion,
           Ciudad, CodigoPostal, Pais, Telefono, Fax
    FROM dbo.Proveedores;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_ObtenerPorId
    @ProveedorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProveedorID, CompaniaNombre, NombreContacto, CargoContacto, Direccion,
           Ciudad, CodigoPostal, Pais, Telefono, Fax
    FROM dbo.Proveedores WHERE ProveedorID = @ProveedorID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Insertar
    @CompaniaNombre NVARCHAR(60),
    @NombreContacto NVARCHAR(40) = NULL,
    @CargoContacto NVARCHAR(40) = NULL,
    @Direccion NVARCHAR(80) = NULL,
    @Ciudad NVARCHAR(30) = NULL,
    @CodigoPostal NVARCHAR(10) = NULL,
    @Pais NVARCHAR(30) = NULL,
    @Telefono NVARCHAR(24) = NULL,
    @Fax NVARCHAR(24) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Proveedores (CompaniaNombre, NombreContacto, CargoContacto, Direccion,
        Ciudad, CodigoPostal, Pais, Telefono, Fax)
    VALUES (@CompaniaNombre, @NombreContacto, @CargoContacto, @Direccion,
        @Ciudad, @CodigoPostal, @Pais, @Telefono, @Fax);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Actualizar
    @ProveedorID INT,
    @CompaniaNombre NVARCHAR(60),
    @NombreContacto NVARCHAR(40) = NULL,
    @CargoContacto NVARCHAR(40) = NULL,
    @Direccion NVARCHAR(80) = NULL,
    @Ciudad NVARCHAR(30) = NULL,
    @CodigoPostal NVARCHAR(10) = NULL,
    @Pais NVARCHAR(30) = NULL,
    @Telefono NVARCHAR(24) = NULL,
    @Fax NVARCHAR(24) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Proveedores
    SET CompaniaNombre = @CompaniaNombre,
        NombreContacto = @NombreContacto,
        CargoContacto = @CargoContacto,
        Direccion = @Direccion,
        Ciudad = @Ciudad,
        CodigoPostal = @CodigoPostal,
        Pais = @Pais,
        Telefono = @Telefono,
        Fax = @Fax
    WHERE ProveedorID = @ProveedorID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_Eliminar
    @ProveedorID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Proveedores WHERE ProveedorID = @ProveedorID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Proveedor_BuscarPorContactoCiudad
    @NombreContacto NVARCHAR(40) = NULL,
    @Ciudad NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProveedorID, CompaniaNombre, NombreContacto, CargoContacto, Direccion,
           Ciudad, CodigoPostal, Pais, Telefono, Fax
    FROM dbo.Proveedores
    WHERE (@NombreContacto IS NULL OR @NombreContacto = '' OR NombreContacto LIKE '%' + @NombreContacto + '%')
      AND (@Ciudad IS NULL OR @Ciudad = '' OR Ciudad LIKE '%' + @Ciudad + '%');
END
GO

/* ===================== Pedidos ===================== */
CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PedidoID, ClienteID, EmpleadoID, FechaPedido, FechaRequerida, FechaEnvio,
           TransportistaID, Destinatario, CiudadDestino, PaisDestino
    FROM dbo.Pedidos;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_ObtenerPorId
    @PedidoID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PedidoID, ClienteID, EmpleadoID, FechaPedido, FechaRequerida, FechaEnvio,
           TransportistaID, Destinatario, CiudadDestino, PaisDestino
    FROM dbo.Pedidos WHERE PedidoID = @PedidoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Insertar
    @ClienteID INT = NULL,
    @EmpleadoID INT = NULL,
    @FechaPedido DATE,
    @FechaRequerida DATE = NULL,
    @FechaEnvio DATE = NULL,
    @TransportistaID INT = NULL,
    @Destinatario NVARCHAR(60) = NULL,
    @CiudadDestino NVARCHAR(30) = NULL,
    @PaisDestino NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Pedidos (ClienteID, EmpleadoID, FechaPedido, FechaRequerida, FechaEnvio,
        TransportistaID, Destinatario, CiudadDestino, PaisDestino)
    VALUES (@ClienteID, @EmpleadoID, @FechaPedido, @FechaRequerida, @FechaEnvio,
        @TransportistaID, @Destinatario, @CiudadDestino, @PaisDestino);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Actualizar
    @PedidoID INT,
    @ClienteID INT = NULL,
    @EmpleadoID INT = NULL,
    @FechaPedido DATE,
    @FechaRequerida DATE = NULL,
    @FechaEnvio DATE = NULL,
    @TransportistaID INT = NULL,
    @Destinatario NVARCHAR(60) = NULL,
    @CiudadDestino NVARCHAR(30) = NULL,
    @PaisDestino NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Pedidos
    SET ClienteID = @ClienteID,
        EmpleadoID = @EmpleadoID,
        FechaPedido = @FechaPedido,
        FechaRequerida = @FechaRequerida,
        FechaEnvio = @FechaEnvio,
        TransportistaID = @TransportistaID,
        Destinatario = @Destinatario,
        CiudadDestino = @CiudadDestino,
        PaisDestino = @PaisDestino
    WHERE PedidoID = @PedidoID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Eliminar
    @PedidoID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Pedidos WHERE PedidoID = @PedidoID;
END
GO

/* ===================== Reporte DetallePedidos ===================== */
CREATE OR ALTER PROCEDURE dbo.sp_DetallePedido_ListarPorFechas
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT dp.PedidoID, p.FechaPedido, dp.ProductoID, dp.PrecioUnidad, dp.Cantidad, dp.Descuento,
           CAST(dp.PrecioUnidad * dp.Cantidad * (1 - dp.Descuento) AS DECIMAL(10,2)) AS Subtotal
    FROM dbo.DetallePedidos dp
    INNER JOIN dbo.Pedidos p ON dp.PedidoID = p.PedidoID
    WHERE p.FechaPedido BETWEEN @FechaInicio AND @FechaFin
    ORDER BY p.FechaPedido, dp.PedidoID, dp.ProductoID;
END
GO
