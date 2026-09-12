namespace Client.Application.Common.Models.AccountService;

public record MovimientoDto(
    Guid Id,
    Guid CuentaId,
    string TipoMovimiento,
    DateTime Fecha,
    decimal Valor,
    decimal Saldo);

public record GetMovimientosByCuentaResponse(List<MovimientoDto> Movimientos);