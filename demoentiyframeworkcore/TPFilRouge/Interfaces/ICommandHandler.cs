namespace TPFilRouge.Interfaces;

public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand command);
}