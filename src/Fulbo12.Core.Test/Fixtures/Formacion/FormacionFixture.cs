namespace Fulbo12.Core.Formacion.Fixtures
{
    public class FormacionFixture
    {
        public PosicionEnCanchaFixture PosicionesEnCancha { get; set; }
        public FormacionFixture()
        {
            PosicionesEnCancha = new PosicionEnCanchaFixture ();
        }
    }
}