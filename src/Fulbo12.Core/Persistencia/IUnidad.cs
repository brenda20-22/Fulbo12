using Fulbo12.Core.Persistencia.Repos;

namespace Fulbo12.Core.Persistencia;
public interface IUnidad
{
    IRepoPais RepoPais { get; }
    IRepoPersona RepoPersona { get; }
    IRepoLiga RepoLiga { get; }
    IRepoEquipo RepoEquipo { get; }
    IRepoFutbolista RepoFutbolista { get; }
    IRepoTipoFutbolista RepoTipoFutbolista { get; }
    IRepoPosicion RepoPosicion { get; }

    void Guardar();
    Task GuardarAsync();
}

