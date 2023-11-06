using Fulbo12.Core.Futbol;

namespace Fulbo12.Core.Persistencia.EFC.Repos;
public class RepoPosicion : RepoGenerico<Posicion>, IRepoPosicion
{
    public RepoPosicion(Fulbo12Contexto contexto) : base(contexto) { }
}