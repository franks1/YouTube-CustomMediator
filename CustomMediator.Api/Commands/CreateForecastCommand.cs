using CustomMediator.Abstractions.Commands;

namespace CustomMediator.Api.Commands;

public record CreateForecastCommand(string City): ICommand;