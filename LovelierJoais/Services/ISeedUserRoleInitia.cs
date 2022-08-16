namespace LovelierJoais.Services
{
    public interface ISeedUserRoleInitial
    {
        //Contrato para criação dos perfis
        void SeedRoles();

        //Contrato para criar usuarios e atribui-los nos perfis
        void SeedUsers();
    }
}
