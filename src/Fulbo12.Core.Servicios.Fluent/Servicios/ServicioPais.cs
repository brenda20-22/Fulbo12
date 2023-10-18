using Fulbo12.Core.CapaServicios.Servicios;
using FluentValidation;
using Fulbo12.Core.Persistencia.Repos;

namespace Fulbo12.Core.Servicios.Fluent.Servicios;
public class ServicioPais : AbstractValidator<Pais> ,IServicio<Pais>
{
    private readonly IRepoPais _repo;
    public ServicioPais(IRepoPais repo) => _repo = repo;
    public void Alta(Pais elemento)
    {
        
    }
}
