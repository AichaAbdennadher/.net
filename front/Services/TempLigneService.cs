namespace front.Services
{
    public class TempLignesService
    {
        private List<LigneMedicament> _lignes = new();

        public List<LigneMedicament> GetLignes() => _lignes;

        public void AddLigne(LigneMedicament ligne) => _lignes.Add(ligne);

        public void Clear() => _lignes.Clear();
    }
}
